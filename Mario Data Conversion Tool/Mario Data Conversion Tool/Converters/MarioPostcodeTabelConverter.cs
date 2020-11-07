using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data;
using System.Data.SqlClient;

namespace Mario_Data_Conversion_Tool.Converters
{
    class MarioPostcodeTabelConverter
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly log4net.ILog logwarn = log4net.LogManager.GetLogger("logWarn");
        
        String fileName;

        public MarioPostcodeTabelConverter(string fileName)
        {
            this.fileName = fileName;
        }

        public Tuple<int, int> Convert() {
            int gemeentenLines = 0;
            int postcodeLines = 0;

            String myConnectionString = "Provider=Microsoft.JET.OLEDB.4.0;" + "data source=" + fileName;
            {
                try
                {
                    // Open OleDb Connection
                    OleDbConnection myConnection = new OleDbConnection();
                    myConnection.ConnectionString = myConnectionString;
                    myConnection.Open();
                    // Execute Queries
                    OleDbCommand cmd = myConnection.CreateCommand();
                    cmd.CommandText = "SELECT * FROM `GEMEENTEN`";
                    OleDbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection); // close conn after complete

                    // Load the result into a DataTable
                    DataTable gemeenten = new DataTable();
                    gemeenten.Load(reader);

                    gemeenten.TableName = "dbo.Gemeenten";
                    gemeenten.Columns["N42_GEM_KODE"].ColumnName = "Kode";
                    gemeenten.Columns["N42_GEM_NAAM"].ColumnName = "Naam";

                    gemeentenLines = gemeenten.Rows.Count;
                    myConnection.ConnectionString = myConnectionString;
                    myConnection.Open();
                    cmd.CommandText = "SELECT * FROM `POSTCODES`";
                    reader = cmd.ExecuteReader(CommandBehavior.CloseConnection); // close conn after complete

                    DataTable postcodes = new DataTable();
                    postcodes.Load(reader);

                    postcodes.TableName = "dbo.Postcodes";
                    postcodes.Columns["A13_POSTCODE"].ColumnName = "Postcode";
                    postcodes.Columns["A13_REEKSIND"].ColumnName = "Reeksind";
                    postcodes.Columns["A13_BREEKPUNT_VAN"].ColumnName = "Breekpunt_van";
                    postcodes.Columns["A13_BREEKPUNT_TEM"].ColumnName = "Breekpunt_tem";
                    postcodes.Columns["A13_WOONPLAATS"].ColumnName = "Woonplaats";
                    postcodes.Columns["A13_STRAATNAAM"].ColumnName = "Straatnaam";
                    postcodes.Columns["A13_GEMEENTECODE"].ColumnName = "Gemeentecode";


                    SqlConnection conn = SqlConnectionMaker.ReturnConnection();
                    postcodeLines = postcodes.Rows.Count;

                    try
                    {
                        conn.Open();
                        SqlCommand command = conn.CreateCommand();
                        command.CommandText = "DROP TABLE IF EXISTS dbo.Gemeenten";
                        command.ExecuteNonQuery();
                        command.CommandText = "CREATE TABLE dbo.Gemeenten (Kode varchar(200),Naam varchar(200))";
                        command.ExecuteNonQuery();

                    }
                    catch (Exception e)
                    {
                        logwarn.Warn(e.Message);
                        throw;
                    }
                    finally
                    {
                        conn.Close();
                    }

                    try
                    {

                        conn.Open();
                        SqlCommand command = conn.CreateCommand();
                        command.CommandText = "DROP TABLE IF EXISTS dbo.Postcodes";
                        command.ExecuteNonQuery();
                        command.CommandText = "CREATE TABLE dbo.Postcodes (Postcode varchar(200),Reeksind varchar(200),Breekpunt_van varchar(200),Breekpunt_tem varchar(200),Woonplaats varchar(200),Straatnaam varchar(200),Gemeeentecode varchar(200))";
                        command.ExecuteNonQuery();

                    }
                    catch (Exception e)
                    {
                        logwarn.Warn(e.Message);
                        throw;
                    }
                    finally
                    {
                        conn.Close();
                    }
                    conn.Open();
                    SqlBulkCopy bulkcopy = new SqlBulkCopy(conn);
                    bulkcopy.DestinationTableName = gemeenten.TableName;
                    try
                    {
                        bulkcopy.WriteToServer(gemeenten);
                    }
                    catch (Exception e)
                    {
                        logwarn.Warn(e.Message);
                    }

                    bulkcopy.DestinationTableName = postcodes.TableName;
                    try
                    {
                        bulkcopy.WriteToServer(postcodes);
                    }
                    catch (Exception e)
                    {
                        logwarn.Warn(e.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }



                }
                catch (Exception ex)
                {
                    Console.WriteLine("OLEDB Connection FAILED: " + ex.Message);
                    logwarn.Warn(ex.Message);
                }
            }
            return Tuple.Create(gemeentenLines, postcodeLines);

        }
    }
}
