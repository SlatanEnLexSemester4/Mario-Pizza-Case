using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace Mario_Data_Conversion_Tool.DataTypes
{
    class Winkel
    {
        public SqlString Name { get; private set; }
        public SqlString Streetname { get; private set; }
        public SqlString Number { get; private set; }
        public SqlString City { get; private set; }
        public SqlString Countrycode { get; private set; }
        public SqlString Zipcode { get; private set; }
        public SqlString Telephonenumber { get; private set; }

        public Winkel()
        {
        }

        public Winkel(SqlString name, SqlString streetname, SqlString number, SqlString city, SqlString countrycode, SqlString zipcode, SqlString telephonenumber)
        {
            Name = name;
            Streetname = streetname;
            Number = number;
            City = city;
            Countrycode = countrycode;
            Zipcode = zipcode;
            Telephonenumber = telephonenumber;
        }


    }
}
