using Mario_Data_Conversion_Tool.DataTypes;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Mario_Data_Conversion_Tool
{
    class MarioOverigeProductenConverter
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        String fileName;

        public MarioOverigeProductenConverter(string fileName)
        {
            this.fileName = fileName;
        }

        public void ReadFile()
        {
            log.Info("- - - - -");
            log.Info("Running : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            log.Info("- - - - -");

            ExcelPackage xlPackage = new ExcelPackage(new FileInfo(fileName));
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            string tempCategory = "";
            string tempSubtempCategory = "";
            string tempName = "";
            string tempDescription = "";
            string tempPrice = "";
            Boolean tempSpicy;
            Boolean tempVegetarian;
            List<OverigProduct> overigeProducten = new List<OverigProduct>();

            var myWorksheet = xlPackage.Workbook.Worksheets.First(); //select sheet here
            var totalRows = myWorksheet.Dimension.End.Row;
            var totalColumns = myWorksheet.Dimension.End.Column;

            for (int rowNum = 2; rowNum <= totalRows; rowNum++) //select starting row here
            {
                tempCategory = myWorksheet.GetValue(rowNum, 1).ToString();
                tempSubtempCategory = myWorksheet.GetValue(rowNum, 2).ToString();
                tempName = myWorksheet.GetValue(rowNum, 3).ToString();
                if (myWorksheet.GetValue(rowNum, 4) != null)
                {
                    tempDescription = myWorksheet.GetValue(rowNum, 4).ToString();
                    tempDescription = tempDescription.Replace("_x000D_", "").Replace("\n", "").Replace("\r", "");
                }
                tempPrice = myWorksheet.GetValue(rowNum, 5).ToString();
                tempPrice = new string(tempPrice.Where(c => (Char.IsDigit(c) || c == '.' || c == ',')).ToArray());
                tempPrice = tempPrice.Replace(",", ".");
                if (myWorksheet.GetValue(rowNum, 6).ToString() == "Ja")
                {
                    tempSpicy = true;
                }
                else
                {
                    tempSpicy = false;
                }
                if (myWorksheet.GetValue(rowNum, 7).ToString() == "Ja")
                {
                    tempVegetarian = true;
                }
                else
                {
                    tempVegetarian = false;
                }
                
                overigeProducten.Add(new OverigProduct(tempCategory, tempSubtempCategory, tempName, tempDescription, decimal.Parse(tempPrice, CultureInfo.InvariantCulture), tempSpicy, tempVegetarian));
                log.Info("Succesfully added line:" + rowNum);

                tempCategory = "";
                tempSubtempCategory = "";
                tempName = "";
                tempDescription = "";
                tempPrice = "";
                tempSpicy = false;
                tempVegetarian = false;

            }

        }
    }
}
