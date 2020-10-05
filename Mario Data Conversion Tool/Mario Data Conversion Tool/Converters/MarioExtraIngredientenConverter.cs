using Mario_Data_Conversion_Tool.DataTypes;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using Mario_Data_Conversion_Tool.Converters;

namespace Mario_Data_Conversion_Tool
{
    class MarioExtraIngredientenConverter
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly log4net.ILog logwarn = log4net.LogManager.GetLogger("logWarn");

        String fileName;
        int headerLines = 1;

        public MarioExtraIngredientenConverter(string fileName)
        {
            this.fileName = fileName;
        }

        public void Convert()
        {
            log.Info("- - - - -");
            log.Info("Running : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            log.Info("- - - - -");

            List<ExtraIngredient> extraIngredienten = ReadFile();
            Upload(extraIngredienten);

        }

        public List<ExtraIngredient> ReadFile()
        {
            log.Info("- - - - -");
            log.Info("Running : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            log.Info("- - - - -");

            string line;
            List<ExtraIngredient> extraIngredienten = new List<ExtraIngredient>();

            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(fileName);

            // Read header lines to skip them.
            for (int i = 0; i < headerLines; i++)
            {
                file.ReadLine();
            }

            // Read lines in file and 
            while ((line = file.ReadLine()) != null)
            {
                var values = line.Split(";", StringSplitOptions.None);
                values[1] = new string(values[1].Where(c => (Char.IsDigit(c) || c == '.' || c == ',')).ToArray());

                extraIngredienten.Add(new ExtraIngredient(values[0], decimal.Parse(values[1], CultureInfo.InvariantCulture)));
                log.Info("Added line:" + line);

            }
            file.Close();
            return extraIngredienten;

        }

        public void Upload(List<ExtraIngredient> extraIngredienten)
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
                command.CommandText = "DROP TABLE IF EXISTS dbo.ExtraIngredienten";
                command.ExecuteNonQuery();
                command.CommandText = "CREATE TABLE dbo.ExtraIngredienten (Ingredient varchar(200), ExtraPrice varchar(200))";
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


            foreach(ExtraIngredient extraIngredient in extraIngredienten)
            {
                ExecuteQuery(extraIngredient.Name.ToString(), extraIngredient.ExtraPrice.ToString());
            }

        }
        private void ExecuteQuery(string ingredient, string price)
        {
            SqlConnection conn = SqlConnectionMaker.ReturnConnection();

            String query = "INSERT INTO dbo.ExtraIngredienten (Ingredient,ExtraPrice) VALUES (@Ingredient,@ExtraPrice)";

            try
            {

                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = query;
                command.Parameters.AddWithValue("@Ingredient ", ingredient);
                command.Parameters.AddWithValue("@ExtraPrice ", price);
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
