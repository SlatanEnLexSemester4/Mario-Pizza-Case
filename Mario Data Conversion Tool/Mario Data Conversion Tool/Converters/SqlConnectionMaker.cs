using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Mario_Data_Conversion_Tool.Converters
{
    class SqlConnectionMaker
    {
        

        public static SqlConnection ReturnConnection()
        {
            string Server = "localhost\\MSSQLSERVER01";
            string Database = "ShadowDB";
            var conn = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + Database + ";User=mario;Password=mario") ;
            return conn;
        }
    }
}
