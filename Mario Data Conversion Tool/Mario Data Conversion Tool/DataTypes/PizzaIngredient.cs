using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace Mario_Data_Conversion_Tool.DataTypes
{
    class PizzaIngredient
    {
        public SqlString Catergory { get; private set; }
        public SqlString Subcategory { get; private set; }
        public SqlString Name { get; private set; }
        public SqlString Description { get; private set; }
        public SqlMoney Price { get; private set; }
        public SqlMoney DeliveryFee { get; private set; }
        public SqlBoolean Spicy { get; private set; }
        public SqlBoolean Vegetarian { get; private set; }
        public SqlBoolean Availability { get; private set; }
        public SqlDecimal Amount { get; private set; }
        public SqlString IngredientName { get; private set; }
        public SqlString StandardSauce { get; private set; }

        public PizzaIngredient(SqlString catergory, SqlString subcategory, SqlString name, SqlString description, SqlMoney price, SqlMoney deliveryFee, SqlBoolean spicy, SqlBoolean vegetarian, SqlBoolean availability, SqlDecimal amount, SqlString ingredientName, SqlString standardSauce)
        {
            Catergory = catergory;
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
