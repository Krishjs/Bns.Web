﻿using Bns.Framework.Common.Errors;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using tikona.bsolution;

namespace Bns.Framework.Common.Messaging
{
    public class MailReader
    {
        private static readonly Lazy<MailReader> mailReaderResolver =
            new Lazy<MailReader>(GetMailReaderInstance);

        private static MailReader mailReader;

        private static DateTime timeStamp;

        private static int timeInterval = 10;

        private static string virtualpath = "";

        private static MailReader Current
        {
            get
            {
                if (mailReader == null)
                {
                    mailReader = mailReaderResolver.Value;
                }

                return mailReader;
            }
        }

        public bool IsTaskRuning
        {
            get
            {
                return this.isTaskRuning;
            }

            set
            {
                this.isTaskRuning = value;
            }
        }

        public bool IsRead
        {
            get
            {
                return isRead;
            }

            set
            {
                this.isRead = value;
            }
        }

        public CancellationTokenSource CancelToken
        {
            get
            {
                if (this.cancelToken == null)
                {
                    this.cancelToken = new CancellationTokenSource();
                }

                return this.cancelToken;
            }

            set
            {
                this.cancelToken = value;
            }
        }

        private MailReader()
        {

        }

        private bool isRead;
        private bool isTaskRuning;
        private CancellationTokenSource cancelToken;

        public static void SetTimeStamp(DateTime date)
        {
            timeStamp = date;
        }

        public static void SetInterval(int time)
        {
            timeInterval = time;
        }

        internal static void StartTask()
        {
            var settings = SettingsManager.MailReceiverSettings;
            Task.Factory.StartNew(() => StartRead(settings), Current.CancelToken.Token);
        }

        public static void SetPath(string path)
        {
            virtualpath = path;
        }

        public static void StopTask()
        {
            Current.CancelToken.Cancel();
            Current.IsTaskRuning = false;
            Current.IsRead = false;
            ErrorQueue.Enqueue("Service Stoped:" + DateTime.Now);
        }

        public static void StartRead(MailReceiverSettings settings)
        {
            Current.IsRead = true;

            if (Current.IsTaskRuning)
                return;

            while (Current.IsRead)
            {
                Current.IsTaskRuning = true;
                ErrorQueue.Enqueue("Reader Started At: " + DateTime.Now.ToString());
                RefreshInbox(settings);
                Thread.Sleep(TimeSpan.FromMinutes(value: timeInterval));
                timeStamp = timeStamp.AddMinutes(value: timeInterval);
            }
        }

        public static string GetDetailsWithinSpecificTimeSpan(DateTime from, DateTime to, MailReceiverSettings settings)
        {
            using (var client = new ImapClient())
            {
                try
                {
                    IMailFolder inbox = AuthenticateAndgetInbox(settings, client);

                    BinarySearchQuery query = SearchQuery.DeliveredAfter(from)
                        .And(SearchQuery.SubjectContains(text: "Work Order - 'SF Recover Equipment'"))
                        .And(SearchQuery.DeliveredBefore(to));

                    IList<UniqueId> indexes = inbox.Search(query);
                    var dicts = new List<Dictionary<string, string>>();
                    foreach (var index in indexes)
                    {
                        MimeMessage msg = inbox.GetMessage(index);
                        foreach (var ma in msg.Attachments)
                        {
                            string file = virtualpath + ma.ContentType.Name;
                            RemoveIfAboveTen(virtualpath);
                            if (!File.Exists(file))
                            {
                                DownloadAttachement((MimePart)ma, file);
                            }
                            dicts.Add(PdfExtractor.ExtractInfoWithPolicy(file));
                        }
                    }
                    client.Disconnect(quit: true);
                    return SheetUpdater.Create(dicts, string.Format("{0}-{1}_", from.ToString("dd_MM_yyyy"), to.ToString("dd_MM_yyyy")));
                }
                catch (Exception ex)
                {
                    ErrorQueue.Enqueue(ex.Message);
                }
            }
            return string.Empty;
        }

        private static void RefreshInbox(MailReceiverSettings settings)
        {
            using (var client = new ImapClient())
            {
                try
                {
                    IMailFolder inbox = AuthenticateAndgetInbox(settings, client);

                    BinarySearchQuery query = SearchQuery.DeliveredAfter(timeStamp)
                        .And(SearchQuery.SubjectContains(text: "Work Order - 'SF Recover Equipment'"));

                    IList<UniqueId> indexes = inbox.Search(query);
                    ErrorQueue.Enqueue("Total Mails Read: " + indexes.Count);
                    foreach (var index in indexes)
                    {
                        MimeMessage msg = inbox.GetMessage(index);
                        if (msg.Date.UtcDateTime >= timeStamp)
                        {
                            foreach (var ma in msg.Attachments)
                            {
                                string file = virtualpath + ma.ContentType.Name;
                                RemoveIfAboveTen(virtualpath);
                                if (!File.Exists(file))
                                {
                                    DownloadAttachement((MimePart)ma, file);
                                }
                                var details = PdfExtractor.ExtractInfoWithPolicy(file);
                                Send(details);
                                UpdateSheet(details);
                            }
                        }
                    }

                    client.Disconnect(quit: true);
                }
                catch (Exception ex)
                {
                    ErrorQueue.Enqueue(ex.Message);
                }
            }
        }

        private static IMailFolder AuthenticateAndgetInbox(MailReceiverSettings settings, ImapClient client)
        {
            client.Connect(settings.Server, port: settings.Port, useSsl: settings.UseSsl);
            client.AuthenticationMechanisms.Remove(item: "XOAUTH2");
            client.Authenticate(settings.User, settings.Password);
            IMailFolder inbox = client.Inbox;
            inbox.Open(FolderAccess.ReadOnly);
            return inbox;
        }

        private static void RemoveIfAboveTen(string virtualpath)
        {
            if (Directory.Exists(virtualpath))
            {
                var dir = new DirectoryInfo(virtualpath);
                if (dir.GetFiles().Count() > 10)
                {
                    IEnumerable<FileInfo> files = dir.GetFiles().OrderByDescending(f => f.CreationTime).Skip(count: 10);
                    foreach (var file in files)
                    {
                        file.Delete();
                        ErrorQueue.Enqueue("Files Delete:" + file.Name);
                    }
                }
            }
        }

        public static void Send(Dictionary<string, string> details)
        {
            RelayRider.Send(details);
        }

        public static void UpdateSheet(Dictionary<string, string> details)
        {
            SheetUpdater.Update(details);
        }

        public static void DownloadAttachement(MimePart entity, string filepath)
        {
            using (var stream = File.Create(filepath))
            {
                entity.ContentObject.DecodeTo(stream);
            }
        }

        private static MailReader GetMailReaderInstance()
        {
            return new MailReader();
        }

        public static void Init(string path)
        {
            if (SettingsManager.MailReaderSettings.AutoStart)
            {
                ErrorQueue.Enqueue("Task Auto Started at " + DateTime.Now);
                ErrorQueue.Enqueue("Task Auto Started with Interval " + SettingsManager.MailReaderSettings.Interval);
                ErrorQueue.Enqueue("Task Auto Started with TimeStamp " + SettingsManager.MailReaderSettings.TimeStamp);
                SetInterval(SettingsManager.MailReaderSettings.Interval);
                SetTimeStamp(SettingsManager.MailReaderSettings.TimeStamp);
                SetPath(path);
                //StartTask();
            }
        }
    }
}
