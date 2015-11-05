namespace Bns.Framework.Common.Messaging
{ 
    static class ExtractionRegex
    {
        public static readonly string WorkOrder = @"WO ID:(?<token>\s\w+)";

        public static readonly string User = @"USER ID:(?<token>\s\w+)";

        public static readonly string Date = @"DATE:(?<token>\s\w+)";

        public static readonly string Customer = @"Customer Name:(?<token>.*\w+)";

        public static readonly string Address = @"Address :(?<token>.*)City";

        public static readonly string Mobile = @"Mobile No:(?<token>\s\w+)";

        public static readonly string City = @"City :(?<token>\s\w+)";

        public static readonly string PinCode = @"PinCode :(?<token>\s\w+)";
    }
}