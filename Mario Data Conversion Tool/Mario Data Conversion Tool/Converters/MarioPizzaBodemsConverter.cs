using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Mario_Data_Conversion_Tool.Converters;
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
            Upload(pizzaBodems);
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

        public void Upload(List<PizzaBodem> pizzaBodems)
        {
            log.Info("- - - - -");
            log.Info("Running : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            log.Info("- - - - -");

            //Drop table and create new one
            SqlConnection conn = SqlConnectionMaker.ReturnConnection();

            try
            {

                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "DROP TABLE IF EXISTS dbo.PizzaBodems";
                command.ExecuteNonQuery();
                command.CommandText = "CREATE TABLE dbo.PizzaBodems (Naam varchar(200),Diameter varchar(200),Omschrijving varchar(200),Toeslag varchar(200),Beschikbaar varchar(200))";
                command.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }

            foreach (PizzaBodem pizzaBodem in pizzaBodems)
            {

                ExecuteQuery(pizzaBodem.Name.ToString(),
                    pizzaBodem.Diameter.ToString(),
                    pizzaBodem.Description.ToString(),
                    pizzaBodem.Fee.ToString(),
                    pizzaBodem.Available.ToString());
            }

        }
        private void ExecuteQuery(
            string Name,
            string Diameter,
            string Description,
            string Fee,
            string Available)
        {
            SqlConnection conn = SqlConnectionMaker.ReturnConnection();

            String query = "INSERT INTO dbo.PizzaBodems (Naam,Diameter,Omschrijving,Toeslag,Beschikbaar) VALUES (@Naam,@Diameter,@Omschrijving,@Toeslag,@Beschikbaar)";
            try
            {

                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = query;

                command.Parameters.AddWithValue("@Naam ", Name);
                command.Parameters.AddWithValue("@Diameter ", Diameter);
                command.Parameters.AddWithValue("@Omschrijving ", Description);
                command.Parameters.AddWithValue("@Toeslag ", Fee);
                command.Parameters.AddWithValue("@Beschikbaar ", Available);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }
        }
    }
}
