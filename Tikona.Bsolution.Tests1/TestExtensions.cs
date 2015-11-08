using Bns.Framework.Common.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tikona.Bsolution.Tests
{
    public static class TestExtensions
    {
        public static bool IsEqual(this RecoveryDetails lhs, RecoveryDetails rhs)
        {
            if (lhs.WorkOrder != rhs.WorkOrder) { return false; }
            if (lhs.User != rhs.User) { return false; }
            if (lhs.Date != rhs.Date) { return false; }
            if (lhs.Customer != rhs.Customer) { return false; }
            if (lhs.Address != rhs.Address) { return false; }
            if (lhs.Mobile != rhs.Mobile) { return false; }
            if (lhs.City != rhs.City) { return false; }
            if (lhs.PinCode != rhs.PinCode) { return false; }
            return true;
        }

        public static bool IsEqual(this EmailRiderSettings lhs, EmailRiderSettings rhs)
        {
            if (lhs.From != rhs.From) { return false; }
            if (lhs.To != rhs.To) { return false; }
            if (lhs.Cc != rhs.Cc) { return false; }
            if (lhs.Bcc != rhs.Bcc) { return false; }
            if (lhs.UserName != rhs.UserName) { return false; }
            if (lhs.Password != rhs.Password) { return false; }
            if (lhs.Host != rhs.Host) { return false; }
            if (lhs.Port != rhs.Port) { return false; }
            return true;
        }

        public static bool IsEqual(this MailReceiverSettings lhs, MailReceiverSettings rhs)
        {
            if (lhs.User != rhs.User) { return false; }
            if (lhs.Password != rhs.Password) { return false; }
            if (lhs.Server != rhs.Server) { return false; }
            if (lhs.Port != rhs.Port) { return false; }
            if (lhs.UseSsl != rhs.UseSsl) { return false; }
            return true;
        }

        public static bool IsEqual(this MailReaderSettings lhs, MailReaderSettings rhs)
        {
            if (lhs.AutoStart!= rhs.AutoStart) { return false; }
            if (lhs.Interval != rhs.Interval) { return false; }
            if (lhs.TimeStamp != rhs.TimeStamp) { return false; }
            return true;
        }
    }
}
