using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace Mario_Data_Conversion_Tool.DataTypes
{
    class ExtraIngredient
    {
        public SqlString Name { get; private set; }
        public SqlDecimal ExtraPrice { get; private set; }

        public ExtraIngredient(SqlString name, SqlDecimal extraPrice)
        {
            Name = name;
            ExtraPrice = extraPrice;
        }


    }
}
