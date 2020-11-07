using Mario_Data_Conversion_Tool.Converters;
using Mario_Data_Conversion_Tool.DataTypes;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Mario_Data_Conversion_Tool
{
    class MarioOverigeProductenConverter
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly log4net.ILog logwarn = log4net.LogManager.GetLogger("logWarn");

        String fileName;

        public MarioOverigeProductenConverter(string fileName)
        {
            this.fileName = fileName;
        }

        public int Convert()
        {
            log.Info("- - - - -");
            log.Info("Running : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            log.Info("- - - - -");

            List<OverigProduct> overigeProducten = ReadFile();
            Upload(overigeProducten);
            return overigeProducten.Count;
        }

        public List<OverigProduct> ReadFile()
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
                    logwarn.Warn("Unwanted characters in description in file: " + fileName + " row: " + rowNum);
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

                tempCategory = "";
                tempSubtempCategory = "";
                tempName = "";
                tempDescription = "";
                tempPrice = "";
                tempSpicy = false;
                tempVegetarian = false;

            }
            return overigeProducten;

        }

        public void Upload(List<OverigProduct> overigeProducten)
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
                command.CommandText = "DROP TABLE IF EXISTS dbo.OverigeProducten";
                command.ExecuteNonQuery();
                command.CommandText = "CREATE TABLE dbo.OverigeProducten (Categorie varchar(200),Subcategorie varchar(200),Productnaam varchar(200),Productomschrijving varchar(200),Prijs varchar(200),Spicy varchar(200),Vegetarisch varchar(200))";
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

            foreach (OverigProduct overigProduct in overigeProducten)
            {
                
                ExecuteQuery(overigProduct.Category.ToString(),
                    overigProduct.Subcategory.ToString(),
                    overigProduct.Name.ToString(),
                    overigProduct.Description.ToString(),
                    overigProduct.Price.ToString(),
                    overigProduct.Spicy.ToString(),
                    overigProduct.Vegetarian.ToString());
            }

        }
        private void ExecuteQuery(
            string Category,
            string Subcategory,
            string Name,
            string Description,
            string Price,
            string Spicy,
            string Vegetarian)
        {
            SqlConnection conn = SqlConnectionMaker.ReturnConnection();

            String query = "INSERT INTO dbo.OverigeProducten (Categorie,Subcategorie,Productnaam,Productomschrijving,Prijs,Spicy,Vegetarisch) VALUES (@Categorie,@Subcategorie,@Productnaam,@Productomschrijving,@Prijs,@Spicy,@Vegetarisch)";
            try
            {

                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = query;

                command.Parameters.AddWithValue("@Categorie ", Category);
                command.Parameters.AddWithValue("@Subcategorie ", Subcategory);
                command.Parameters.AddWithValue("@Productnaam ", Name);
                command.Parameters.AddWithValue("@Productomschrijving ", Description);
                command.Parameters.AddWithValue("@Prijs ", Price);
                command.Parameters.AddWithValue("@Spicy ", Spicy);
                command.Parameters.AddWithValue("@Vegetarisch ", Vegetarian);
                
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
