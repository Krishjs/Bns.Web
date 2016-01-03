using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Bns.Framework.Common.Extraction
{
    public class ExtractionSettings : ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection = true , IsRequired = true)]
        public ExtractionSettingsCollection Settings {
            get { return (ExtractionSettingsCollection)this[""]; }
            set { this[""] = value; }
        }
    }

    public class ExtractionSettingsCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ExtractionSetting();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ExtractionSetting)element).Key;
        }
    }

    public class ExtractionSetting : ConfigurationElement
    {
        [ConfigurationProperty("Key", IsRequired = true)]
        public string Key
        {
            get { return (string)this["Key"]; }
            set { this["Key"] = value; }
        }

        [ConfigurationProperty("ExtractionRegex", IsRequired = true)]
        public string ExtractionRegex
        {
            get { return (string)this["ExtractionRegex"]; }
            set { this["ExtractionRegex"] = value; }
        }

        [ConfigurationProperty("RegexOption", IsRequired = false,DefaultValue ="None")]
        public string RegexOption
        {
            get { return (string)this["RegexOption"]; }
            set { this["RegexOption"] = value; }
        }
    }
}