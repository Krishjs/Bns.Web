using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bns.Framework.Common.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bns.Framework.Common.Messaging.Tests
{
    [TestClass()]
    public class MailReaderTests
    {
        [TestMethod()]
        public void StartReadTest()
        {

            MailReader.Init(( @"F:\GitHub\Bnc.Web\Tikona.Bsolution.Tests1\Pdf\"));
            
        }
    }
}