using Bns.Framework.Common.ExcelConvertion;
using Bns.Framework.Common.Messaging;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace tikona.bsolution
{
    public class SheetUpdater
    {
        private static string virtualpath = "";

        public static void SetPath(string path)
        {
            virtualpath = path;
        }

        public static int ColumnIndexFromAlphabet(string alphbet)
        {
            string alph = "0ABCDEFGHIJKLMNOPQRSTUVWXYZAAABACAD";
            return alph.IndexOf(alphbet, StringComparison.InvariantCultureIgnoreCase);
        }

        public static void Update(Dictionary<string,string> extractedvalues)
        {
            using (var excel = new ExcelPackage(GetTemplateFileInfo()))
            {
                var sheet = excel.Workbook.Worksheets[1];
                var settings = SettingsManager.ExcelOutputSettings.Settings;
                sheet.Select();
                int rowIndex = GetLastRow(sheet);
                sheet.InsertRow(rowIndex, 1);
                foreach (ExcelOutputSetting setting in settings)
                {
                    string value = IsSourceFromExtraction(setting.Source) ? extractedvalues[setting.Value] : setting.Value;
                    ExcelRangeBase cell = sheet.Cells[rowIndex, ColumnIndexFromAlphabet(setting.Column)];
                    if (setting.Type == "D")
                    {
                        DateTime date = new DateTime(GetIntFromStringSplit(value, 2), GetIntFromStringSplit(value, 1), GetIntFromStringSplit(value, 0));
                        cell.Style.Numberformat.Format = "m/d/yyyy";
                        cell.Value = date.ToOADate();
                        //cell.Calculate();
                    }
                    else if (setting.Type == "N")
                    {
                        cell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; 
                        cell.Style.Numberformat.Format = "0";
                        cell.Value = Convert.ToInt64(value);
                    }
                    else
                    {
                        cell.Value = value;
                    }                    
                }
                excel.Save();
            }
        }

        private static int GetIntFromStringSplit(string value, int index)
        {
            return Convert.ToInt32(value.Split('/')[index]);
        }

        private static int GetLastRow(ExcelWorksheet sheet)
        {
            int i = 2;
            for (; i < sheet.Dimension.Rows;)
            {
                if (sheet.GetValue(i, 1) == null)
                    return i;
                i++;
            }
            return 2;
        }

        //TODO Source to a enum and check with enum.
        private static bool IsSourceFromExtraction(string source)
        {
            return source == "E";
        }

        public static FileInfo GetTemplateFileInfo()
        {
            return new FileInfo(virtualpath);
        }

        public static FileInfo SaveFileInfo()
        {
            return new FileInfo("");
        }
    }
}