using Bns.Framework.Common.Errors;
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

        private MailReader()
        {

        }

        private bool isRead;

        public static void SetTimeStamp(DateTime date)
        {
            timeStamp = date;
        }

        public static void SetInterval(int time)
        {
            timeInterval = time;
        }

        public static void SetPath(string path)
        {
            virtualpath = path;
        }

        public static void StopRead()
        {
            Current.IsRead = false;
        }

        public static void StartRead(MailSettings settings)
        {
            Current.IsRead = true;
            while (Current.IsRead)
            {
                ErrorQueue.Enqueue("Reader Started At: " + DateTime.Now.ToString());
                RefreshInbox(settings);                
                Thread.Sleep(TimeSpan.FromMinutes(value: timeInterval));
                timeStamp = timeStamp.AddMinutes(value: timeInterval);
            }
        }

        private static void RefreshInbox(MailSettings settings)
        {
            using (var client = new ImapClient())
            {
                try
                {
                    client.Connect(settings.Server, port: 143, useSsl: false);

                    client.AuthenticationMechanisms.Remove(item: "XOAUTH2");

                    client.Authenticate(settings.User, settings.Password);

                    IMailFolder inbox = client.Inbox;
                    inbox.Open(FolderAccess.ReadOnly);

                    BinarySearchQuery query = SearchQuery.DeliveredAfter(timeStamp)
                        .And(SearchQuery.SubjectContains(text: "Work Order - 'SF Recover Equipment'"));

                    IList<UniqueId> indexes = inbox.Search(query);
                    ErrorQueue.Enqueue("Total Mails Read: " + indexes.Count);
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
                            Send(PdfExtractor.ExtractInfo(file));
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

        private static void RemoveIfAboveTen(string virtualpath)
        {
            if (Directory.Exists(virtualpath))
            {
                var dir = new  DirectoryInfo(virtualpath);
                if (dir.GetFiles().Count() > 10)
                {
                    IEnumerable<FileInfo> files = dir.GetFiles().OrderByDescending(f => f.CreationTime).Skip(count:10);
                    foreach (var file in files)
                    {                        
                        file.Delete();
                        ErrorQueue.Enqueue("Files Delete:" + file.Name);
                    }
                }
            }
        }

        public static void Send(RecoveryDetails details)
        {
            RelayRider.Send(details);
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

    }
}
