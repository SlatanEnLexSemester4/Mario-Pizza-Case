using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Mario_Data_Conversion_Tool.Converters;
using Mario_Data_Conversion_Tool.DataTypes;

namespace Mario_Data_Conversion_Tool
{
    class MarioWinkelsConverter
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly log4net.ILog logwarn = log4net.LogManager.GetLogger("logWarn");

        String fileName;
        int headerLines = 3;

        public MarioWinkelsConverter(string fileName)
        {
            this.fileName = fileName;
        }

        public int Convert()
        {
            log.Info("- - - - -");
            log.Info("Running : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            log.Info("- - - - -");

            List<Winkel> winkels = ReadFile();
            Upload(winkels);
            return winkels.Count;
        }

        public List<Winkel> ReadFile() 
        {
            log.Info("- - - - -");
            log.Info("Running : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            log.Info("- - - - -");

            string line;
            int winkelCounter = 0;
            string tempName = "";
            string tempStreet = "";
            string tempNumber = "";
            string tempCity = "";
            string tempCountryCode = "";
            string tempZipcode = "";
            string tempPhoneNumber = "";
            List<Winkel> winkels = new List<Winkel>();

            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(fileName);

            // Read header lines to skip them.
            for(int i = 0; i < headerLines; i++)
            {
                file.ReadLine();
            }

            // Read lines in file and 
            while ((line = file.ReadLine()) != null)
            {
                switch (winkelCounter)
                {
                    case 0:
                        tempName = line;
                        winkelCounter++;
                        break;
                    case 1:
                        tempStreet = line;
                        winkelCounter++;
                        break;
                    case 2:
                        tempNumber = line;
                        winkelCounter++;
                        break;
                    case 3:
                        tempCity = line;
                        winkelCounter++;
                        break;
                    case 4:
                        tempCountryCode = line;
                        winkelCounter++;
                        break;
                    case 5:
                        tempZipcode = line;
                        winkelCounter++;
                        break;
                    case 6:
                        tempPhoneNumber = line;
                        winkelCounter++;
                        break;
                    case 7:
                        winkelCounter = 0;
                        winkels.Add(new Winkel(tempName, tempStreet, tempNumber, tempCity, tempCountryCode, tempZipcode, tempPhoneNumber));
                        log.Info("Succesfully added line:" + tempName);
                        tempName = "";
                        tempStreet = "";
                        tempNumber = "";
                        tempCity = "";
                        tempCountryCode = "";
                        tempZipcode = "";
                        tempPhoneNumber = "";
                        break;
                    default:
                        Console.WriteLine("Default case");
                        break;
                }
            }

            // Write last store to list
            winkels.Add(new Winkel(tempName, tempStreet, tempNumber, tempCity, tempCountryCode, tempZipcode, tempPhoneNumber));

            file.Close();
            return winkels;
        }

        public void Upload(List<Winkel> winkels)
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
                command.CommandText = "DROP TABLE IF EXISTS dbo.Winkels";
                command.ExecuteNonQuery();
                command.CommandText = "CREATE TABLE dbo.Winkels (Naam varchar(200),Straat varchar(200),Nummer varchar(200),Plaats varchar(200),LandCode varchar(200),Postcode varchar(200),Telefoon varchar(200))";
                command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                logwarn.Warn(e.Message);
                throw;
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }

            foreach (Winkel winkel in winkels)
            {

                ExecuteQuery(winkel.Name.ToString(),
                    winkel.Streetname.ToString(),
                    winkel.Number.ToString(),
                    winkel.City.ToString(),
                    winkel.Countrycode.ToString(),
                    winkel.Zipcode.ToString(),
                    winkel.Telephonenumber.ToString());
            }

        }
        private void ExecuteQuery(
            string Name,
            string Streetname,
            string Number,
            string City,
            string Countrycode,
            string Zipcode,
            string Telephonenumber)
        {
            SqlConnection conn = SqlConnectionMaker.ReturnConnection();

            String query = "INSERT INTO dbo.Winkels (Naam,Straat,Nummer,Plaats,LandCode,Postcode,Telefoon) VALUES (@Naam,@Straat,@Nummer,@Plaats,@LandCode,@Postcode,@Telefoon)";
            try
            {

                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = query;

                command.Parameters.AddWithValue("@Naam ", Name);
                command.Parameters.AddWithValue("@Straat ", Streetname);
                command.Parameters.AddWithValue("@Nummer ", Number);
                command.Parameters.AddWithValue("@Plaats ", City);
                command.Parameters.AddWithValue("@LandCode ", Countrycode);
                command.Parameters.AddWithValue("@Postcode ", Zipcode);
                command.Parameters.AddWithValue("@Telefoon ", Telephonenumber);

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                logwarn.Warn(e.Message);
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
