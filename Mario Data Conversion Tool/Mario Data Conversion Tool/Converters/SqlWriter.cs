using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Mario_Data_Conversion_Tool.Converters
{
    class SqlWriter
    {
        string Server = "localhost\\lex";
        string Database = "ShadowDB";

        public void Insert(String input)
        {
            ExecuteQuery(input);
        }
        
        private void ExecuteQuery(string query)
        {
            var conn = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + Database + ";");
            try
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = query;
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
