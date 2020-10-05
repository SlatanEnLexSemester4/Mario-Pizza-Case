using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Mario_Data_Conversion_Tool.DataTypes;
using OfficeOpenXml;


namespace Mario_Data_Conversion_Tool
{
    class MarioPizzaBodemsConverter
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly log4net.ILog logwarn = log4net.LogManager.GetLogger("logWarn");

        String fileName;

        public MarioPizzaBodemsConverter(string fileName)
        {
            this.fileName = fileName;
        }

        public void Convert()
        {
            log.Info("- - - - -");
            log.Info("Running : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            log.Info("- - - - -");

            List<PizzaBodem> pizzaBodems = ReadFile();
        }

        public List<PizzaBodem> ReadFile()
        {
            log.Info("- - - - -");
            log.Info("Running : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            log.Info("- - - - -");

            ExcelPackage xlPackage = new ExcelPackage(new FileInfo(fileName));
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            string tempName = "";
            string tempDiameter = "";
            string tempDescription = "";
            string tempFee = "";
            Boolean tempAvailable;
            List<PizzaBodem> pizzaBodems = new List<PizzaBodem>();

            var myWorksheet = xlPackage.Workbook.Worksheets.First(); //select sheet here
            var totalRows = myWorksheet.Dimension.End.Row;
            var totalColumns = myWorksheet.Dimension.End.Column;

            for (int rowNum = 2; rowNum <= totalRows; rowNum++) //select starting row here
            {
                tempName = myWorksheet.GetValue(rowNum, 1).ToString();
                tempDiameter = myWorksheet.GetValue(rowNum, 2).ToString();
                tempDescription = myWorksheet.GetValue(rowNum, 3).ToString();
                tempFee = myWorksheet.GetValue(rowNum, 4).ToString();
                tempFee = new string(tempFee.Where(c => (Char.IsDigit(c) || c == '.' || c == ',')).ToArray());
                tempFee = tempFee.Replace(",", ".");
                if (myWorksheet.GetValue(rowNum, 5).ToString() == "Ja")
                {
                    tempAvailable = true;
                } else
                {
                    tempAvailable = false;
                }
                //System.Console.WriteLine(tempName + " " + tempDiameter + " " + tempDescription +" " + decimal.Parse(tempFee) +" " + tempAvailable);
                pizzaBodems.Add(new PizzaBodem(tempName, tempDiameter, tempDescription, decimal.Parse(tempFee, CultureInfo.InvariantCulture), tempAvailable));
                log.Info("Succesfully added line:" + rowNum);
                tempName = "";
                tempDiameter = "";
                tempDescription = "";
                tempFee = "";
                tempAvailable = false;

            }
            return pizzaBodems;
        }
    }
}
