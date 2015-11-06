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
        public static bool AreEqual(this RecoveryDetails lhs, RecoveryDetails rhs)
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
    }
}
