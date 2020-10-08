using Mario_Data_Conversion_Tool.Converters;
using Mario_Data_Conversion_Tool.DataTypes;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Mario_Data_Conversion_Tool
{
    class MarioPizzaIngredientenConverter
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly log4net.ILog logwarn = log4net.LogManager.GetLogger("logWarn");

        String fileName;

        public MarioPizzaIngredientenConverter(string fileName)
        {
            this.fileName = fileName;
        }

        public void Convert()
        {
            log.Info("- - - - -");
            log.Info("Running : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            log.Info("- - - - -");

            List<PizzaIngredient> pizzaIngredienten = ReadFile();
            Upload(pizzaIngredienten);
        }

        public List<PizzaIngredient> ReadFile()
        {
            log.Info("- - - - -");
            log.Info("Running : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            log.Info("- - - - -");

            ExcelPackage xlPackage = new ExcelPackage(new FileInfo(fileName));
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            string tempCategory = "";
            string tempSubtempCategory = "";
            string tempProductName = "";
            string tempDescription = "";
            string tempPrice = "";
            string tempDeliveryFee = "";
            Boolean tempSpicy;
            Boolean tempVegetarian;
            Boolean tempAvailability;
            decimal tempAmount = 0;
            string tempIngredientName = "";
            string tempPizzaSauceStandard = "";

            List<PizzaIngredient> pizzaIngredienten = new List<PizzaIngredient>();

            var myWorksheet = xlPackage.Workbook.Worksheets.First(); //select sheet here
            var totalRows = myWorksheet.Dimension.End.Row;
            var totalColumns = myWorksheet.Dimension.End.Column;

            for (int rowNum = 2; rowNum <= totalRows; rowNum++) //select starting row here
            {
                tempCategory = myWorksheet.GetValue(rowNum, 1).ToString();

                tempSubtempCategory = myWorksheet.GetValue(rowNum, 2).ToString();

                tempProductName = myWorksheet.GetValue(rowNum, 3).ToString();

                tempDescription = myWorksheet.GetValue(rowNum, 4).ToString();

                tempPrice = myWorksheet.GetValue(rowNum, 5).ToString();
                tempPrice = new string(tempPrice.Where(c => (Char.IsDigit(c) || c == '.' || c == ',')).ToArray());
                tempPrice = tempPrice.Replace(",", ".");

                tempDeliveryFee = myWorksheet.GetValue(rowNum, 6).ToString();
                tempDeliveryFee = new string(tempDeliveryFee.Where(c => (Char.IsDigit(c) || c == '.' || c == ',')).ToArray());
                tempDeliveryFee = tempDeliveryFee.Replace(",", ".");

                if (myWorksheet.GetValue(rowNum, 7).ToString() == "Ja")
                {
                    tempSpicy = true;
                }
                else
                {
                    tempSpicy = false;
                }
                if (myWorksheet.GetValue(rowNum, 8).ToString() == "Ja")
                {
                    tempVegetarian = true;
                }
                else
                {
                    tempVegetarian = false;
                }
                if (myWorksheet.GetValue(rowNum, 9).ToString() == "Ja")
                {
                    tempAvailability = true;
                }
                else
                {
                    tempAvailability = false;
                }

                tempAmount = decimal.Parse(myWorksheet.GetValue(rowNum, 10).ToString());

                tempIngredientName = myWorksheet.GetValue(rowNum, 11).ToString();

                tempPizzaSauceStandard = myWorksheet.GetValue(rowNum, 12).ToString();

                
                pizzaIngredienten.Add(new PizzaIngredient(tempCategory, tempSubtempCategory, tempProductName, tempDescription, decimal.Parse(tempPrice, CultureInfo.InvariantCulture), decimal.Parse(tempDeliveryFee, CultureInfo.InvariantCulture), tempSpicy, tempVegetarian, tempAvailability, tempAmount, tempIngredientName, tempPizzaSauceStandard));
                log.Info("Succesfully added line:" + rowNum);

                tempCategory = "";
                tempSubtempCategory = "";
                tempProductName = "";
                tempDescription = "";
                tempPrice = "";
                tempDeliveryFee = "";
                tempSpicy = false;
                tempVegetarian = false;
                tempAvailability = false;
                tempAmount = 0;
                tempIngredientName = "";
                tempPizzaSauceStandard = "";

            }
            return pizzaIngredienten;
        }

        public void Upload(List<PizzaIngredient> pizzaIngredienten)
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
                command.CommandText = "DROP TABLE IF EXISTS dbo.PizzaIngredienten";
                command.ExecuteNonQuery();
                command.CommandText = "CREATE TABLE dbo.PizzaIngredienten (Categorie varchar(200),Subcategorie varchar(200),Productnaam varchar(200),Productomschrijving varchar(500),Prijs varchar(200),Bezorgtoeslag varchar(200),Spicy varchar(200),Vegetarisch varchar(200),Beschikbaar varchar(200),AantaalkeerIngredient varchar(200),Ingredientnaam varchar(200),PizzasausStandaard varchar(200))";
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

            foreach (PizzaIngredient pizzaIngredient in pizzaIngredienten)
            {

                ExecuteQuery(pizzaIngredient.Category.ToString(),
                    pizzaIngredient.Subcategory.ToString(),
                    pizzaIngredient.Name.ToString(),
                    pizzaIngredient.Description.ToString(),
                    pizzaIngredient.Price.ToString(),
                    pizzaIngredient.DeliveryFee.ToString(),
                    pizzaIngredient.Spicy.ToString(),
                    pizzaIngredient.Vegetarian.ToString(),
                    pizzaIngredient.Availability.ToString(),
                    pizzaIngredient.Amount.ToString(),
                    pizzaIngredient.IngredientName.ToString(),
                    pizzaIngredient.StandardSauce.ToString());
            }

        }
        private void ExecuteQuery(
            string Category,
            string Subcategory,
            string Name,
            string Description,
            string Price,
            string DeliveryFee,
            string Spicy,
            string Vegetarian,
            string Availability,
            string Amount,
            string IngredientName,
            string StandardSauce)
        {
            SqlConnection conn = SqlConnectionMaker.ReturnConnection();

            String query = "INSERT INTO dbo.PizzaIngredienten (Categorie,Subcategorie,Productnaam,Productomschrijving,Prijs,Bezorgtoeslag,Spicy,Vegetarisch,Beschikbaar,AantaalkeerIngredient,Ingredientnaam,PizzasausStandaard) VALUES (@Categorie,@Subcategorie,@Productnaam,@Productomschrijving,@Prijs,@Bezorgtoeslag,@Spicy,@Vegetarisch,@Beschikbaar,@AantaalkeerIngredient,@Ingredientnaam,@PizzasausStandaard)";
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
                command.Parameters.AddWithValue("@Bezorgtoeslag ", DeliveryFee);
                command.Parameters.AddWithValue("@Spicy ", Spicy);
                command.Parameters.AddWithValue("@Vegetarisch ", Vegetarian);
                command.Parameters.AddWithValue("@Beschikbaar ", Availability);
                command.Parameters.AddWithValue("@AantaalkeerIngredient ", Amount);
                command.Parameters.AddWithValue("@Ingredientnaam ", IngredientName);
                command.Parameters.AddWithValue("@PizzasausStandaard ", StandardSauce);

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
