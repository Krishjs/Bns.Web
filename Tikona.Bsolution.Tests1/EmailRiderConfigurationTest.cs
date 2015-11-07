using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tikona.Bsolution.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Bns.Framework.Common.Messaging.Tests
{
    [TestClass]
    public class EmailRiderConfigurationTest
    {
        [TestMethod]
        public void ValidateEmailRiderSettingsConfiguration()
        {
            //Arrange
            var expected = new EmailRiderSettings();
            expected.From       = "xxxxxxxxh@xxxx.xxx";
            expected.To         = "xxxxxxxxh@xxxx.xxx";
            expected.Cc         = "xxxxxxxxh@xxxx.xxx";
            expected.Bcc        = "xxxxxxxxh@xxxx.xxx";
            expected.UserName   = "xxxxxxuserxxxxxx";
            expected.Password   = "xxxxxxpassxxxword";
            expected.Host       = "xxxxxxxxx.xxxxxxxxx.in";
            expected.Port       = 25;

            var asm = Assembly.Load("tikona.bsolution");

            //Act
            var sut = (EmailRiderSettings)System.Configuration.ConfigurationManager.GetSection("MailSettings/EmailRiderSettings");

            //Assert
            Assert.IsTrue(sut.IsEqual(expected));
        }

        [TestMethod]
        public void ValidateEmailReceiverSettingsConfiguration()
        {
            //Arrange
            var expected = new MailReceiverSettings();
            expected.Server = "xxxxxxxxx.xxxxxxxxx.in";
            expected.User = "xxxxxxuserxxxxxx";
            expected.Password = "xxxxxxpassxxxword";
            expected.UseSsl = false;
            expected.Port = 143;
            //Act
            var sut = (MailReceiverSettings)System.Configuration.ConfigurationManager.GetSection("MailSettings/MailReceiverSettings");

            //Assert
            Assert.IsTrue(sut.IsEqual(expected));
        }
    }
}
