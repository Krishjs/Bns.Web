using Bns.Framework.Common.Extraction;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Bns.Framework.Common.Messaging
{
    public class PdfExtractor
    {
        public static RecoveryDetails ExtractInfo(string filepath)
        {
            var pdfReader = new iTextSharp.text.pdf.PdfReader(filename: filepath);
            string text = PdfTextExtractor.GetTextFromPage(pdfReader, pageNumber: 1);
            var recoveryDetails = new RecoveryDetails();
            recoveryDetails.WorkOrder = GetValueFromText(text, ExtractionRegex.WorkOrder);
            recoveryDetails.User = GetValueFromText(text, ExtractionRegex.User);
            recoveryDetails.Date = GetValueFromText(text, ExtractionRegex.Date);
            recoveryDetails.Customer = GetValueFromText(text, ExtractionRegex.Customer);
            recoveryDetails.Address = GetValueFromText(text, ExtractionRegex.Address, RegexOptions.Singleline);
            recoveryDetails.Mobile = GetValueFromText(text, ExtractionRegex.Mobile);
            recoveryDetails.City = GetValueFromText(text, ExtractionRegex.City);
            recoveryDetails.PinCode = GetValueFromText(text, ExtractionRegex.PinCode);
            return recoveryDetails;
        }

        public static Dictionary<string, string> ExtractInfoWithPolicy(string filepath)
        {
            var pdfReader = new iTextSharp.text.pdf.PdfReader(filename: filepath);
            string text = PdfTextExtractor.GetTextFromPage(pdfReader, pageNumber: 1);
            var dict = new Dictionary<string, string>();
            foreach (ExtractionSetting setting in SettingsManager.ExtractionSettings.Settings)
            {
                dict.Add(setting.Key, GetValueFromText(text, setting.ExtractionRegex, (RegexOptions)Enum.Parse(typeof(RegexOptions),setting.RegexOption)));
            }
            return dict;
        }

        private static string GetValueFromText(string text, string regex, RegexOptions option = RegexOptions.None)
        {
            MatchCollection mat = Regex.Matches(text, regex, option);
            if (mat.Count > 0)
                return mat[0].Groups["token"].ToString().Trim(trimChars: new char[] { ' ' });
            return string.Empty;
        }
    }
}