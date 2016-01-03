using Microsoft.VisualStudio.TestTools.UnitTesting;
using tikona.bsolution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bns.Framework.Common.Messaging;

namespace tikona.bsolution.Tests
{
    [TestClass()]
    public class ExcelCreatorTests
    {
        [TestMethod()]
        public void CreateExcelTest()
        {
            SheetUpdater.Update(PdfExtractor.ExtractInfoWithPolicy(@"C:\Users\krishnan\Source\Repos\Bns.Web\tikona.bsolution\Pdf\1111034444.pdf"););

          
        }
    }
}