using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bns.Framework.Common.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tikona.Bsolution.Tests;

namespace Bns.Framework.Common.Messaging.Tests
{
    [TestClass()]
    public class PdfExtractorTests
    {
        [TestMethod()]
        public void ExtractInfoTest()
        {
            //Arrange
            var expected = new RecoveryDetails()
            {
                Customer = "KARTHIK R",
                User = "1113082840",
                Date = "03/11/2015",
                WorkOrder = "6844858",
                Mobile = "9790947232",
                Address = "NO 10/57,MGR STREET,LAKSHMIPURAM,THIRUVANMIYUR\nNEAR GANDHI STATUE\n",
                City = "CHENNAI",
                PinCode = "600041"
            };

            //Act 
            RecoveryDetails actual = PdfExtractor.ExtractInfo(filepath: @"D:\Mobile Gopal\1113082840.pdf");

            //Assert
            Assert.IsTrue(expected.AreEqual(actual));

        }


    }
}