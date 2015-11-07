using System.Configuration;

namespace Bns.Framework.Common.Messaging
{
    public class MailReceiverSettings : ConfigurationSection
    {
        [ConfigurationProperty("User", IsRequired = true)]
        public string User
        {
            get { return (string)this["User"]; }
            set { this["User"] = value; }
        }
        [ConfigurationProperty("Password", IsRequired = true)]
        public string Password
        {
            get { return (string)this["Password"]; }
            set { this["Password"] = value; }
        }
        [ConfigurationProperty("Server", IsRequired = true)]
        public string Server
        {
            get { return (string)this["Server"]; }
            set { this["Server"] = value; }
        }
        [ConfigurationProperty("Port", IsRequired = true)]
        public int Port
        {
            get { return (int)this["Port"]; }
            set { this["Port"] = value; }
        }
        [ConfigurationProperty("UseSsl", DefaultValue = false)]
        public bool UseSsl
        {
            get { return (bool)this["UseSsl"]; }
            set { this["UseSsl"] = value; }
        }
    }
}