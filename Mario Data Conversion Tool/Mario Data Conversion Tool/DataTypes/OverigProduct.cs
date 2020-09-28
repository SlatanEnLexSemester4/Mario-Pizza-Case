using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace Mario_Data_Conversion_Tool.DataTypes
{
    class OverigProduct
    {
        public SqlString Category { get; private set; }
        public SqlString Subcategory { get; private set; }
        public SqlString Name { get; private set; }
        public SqlString Description { get; private set; }
        public SqlMoney Price { get; private set; }
        public SqlBoolean Spicy { get; private set; }
        public SqlBoolean Vegetarian { get; private set; }

        public OverigProduct(SqlString catergory, SqlString subcategory, SqlString name, SqlString description, SqlMoney price, SqlBoolean spicy, SqlBoolean vegetarian)
        {
            Category = catergory;
            Subcategory = subcategory;
            Name = name;
            Description = description;
            Price = price;
            Spicy = spicy;
            Vegetarian = vegetarian;
        }
    }
}
