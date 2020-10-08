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
        public SqlDecimal Price { get; private set; }
        public SqlBoolean Spicy { get; private set; }
        public SqlBoolean Vegetarian { get; private set; }

        public OverigProduct(SqlString category, SqlString subcategory, SqlString name, SqlString description, SqlDecimal price, SqlBoolean spicy, SqlBoolean vegetarian)
        {
            Category = category;
            Subcategory = subcategory;
            Name = name;
            Description = description;
            Price = price;
            Spicy = spicy;
            Vegetarian = vegetarian;
        }
    }
}
