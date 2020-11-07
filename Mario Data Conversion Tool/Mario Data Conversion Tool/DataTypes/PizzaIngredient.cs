using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace Mario_Data_Conversion_Tool.DataTypes
{
    class PizzaIngredient
    {
        public SqlString Category { get; private set; }
        public SqlString Subcategory { get; private set; }
        public SqlString Name { get; private set; }
        public SqlString Description { get; private set; }
        public SqlDecimal Price { get; private set; }
        public SqlDecimal DeliveryFee { get; private set; }
        public SqlBoolean Spicy { get; private set; }
        public SqlBoolean Vegetarian { get; private set; }
        public SqlBoolean Availability { get; private set; }
        public SqlDecimal Amount { get; private set; }
        public SqlString IngredientName { get; private set; }
        public SqlString StandardSauce { get; private set; }

        public PizzaIngredient(SqlString category, SqlString subcategory, SqlString name, SqlString description, SqlDecimal price, SqlDecimal deliveryFee, SqlBoolean spicy, SqlBoolean vegetarian, SqlBoolean availability, SqlDecimal amount, SqlString ingredientName, SqlString standardSauce)
        {
            Category = category;
            Subcategory = subcategory;
            Name = name;
            Description = description;
            Price = price;
            DeliveryFee = deliveryFee;
            Spicy = spicy;
            Vegetarian = vegetarian;
            Availability = availability;
            Amount = amount;
            IngredientName = ingredientName;
            StandardSauce = standardSauce;
        }
    }
}
