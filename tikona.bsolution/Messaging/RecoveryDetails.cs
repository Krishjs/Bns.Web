using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bns.Framework.Common.Messaging
{
    public class RecoveryDetails
    {
        public string WorkOrder { get; set; }
        public string User { get; set; }
        public string Customer { get; set; }
        public string Date { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
    }
}