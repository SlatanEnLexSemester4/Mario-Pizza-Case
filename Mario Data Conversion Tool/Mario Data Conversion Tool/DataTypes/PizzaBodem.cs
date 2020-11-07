using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace Mario_Data_Conversion_Tool.DataTypes
{
    class PizzaBodem
    {
        public SqlString Name { get; private set; }
        public SqlString Diameter { get; private set; }
        public SqlString Description { get; private set; }
        public SqlDecimal Fee { get; private set; }
        public SqlBoolean Available { get; private set; }

        public PizzaBodem(SqlString name, SqlString diameter, SqlString description, SqlDecimal fee, SqlBoolean available)
        {
            Name = name;
            Diameter = diameter;
            Description = description;
            Fee = fee;
            Available = available;
        }
    }
}
