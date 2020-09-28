using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace Mario_Data_Conversion_Tool.DataTypes
{
    class Order
    {
        public SqlString StoreName { get; private set; }
        public SqlString CustomerName { get; private set; }
        public SqlString PhoneNumber { get; private set; }
        public SqlString Email { get; private set; }
        public SqlString Address { get; private set; }
        public SqlString City { get; private set; }
        public SqlDateTime OrderDate { get; private set; }
        public SqlString DeliveryType { get; private set; }
        public SqlDateTime DeliveryDate { get; private set; }
        public SqlString DeliveryTime { get; private set; }
        public SqlString Product { get; private set; }
        public SqlString PizzaCrust { get; private set; }
        public SqlString PizzaSauce { get; private set; }
        public SqlMoney Price { get; private set; }
        public SqlMoney DeliveryCost { get; private set; }
        public SqlDecimal Amount { get; private set; }
        public SqlString ExtraIngredients { get; private set; }
        public SqlMoney ExtraIngredientPrice { get; private set; }
        public SqlMoney LinePrice { get; private set; }
        public SqlMoney TotalPrice { get; private set; }
        public SqlString UsedVoucher { get; private set; }
        public SqlMoney VoucherDiscount { get; private set; }
        public SqlMoney TotalPriceAfterDiscount { get; private set; }

        public Order(SqlString storeName, SqlString customerName, SqlString phoneNumber, SqlString email, SqlString address, SqlString city, SqlDateTime orderDate, SqlString deliveryType, SqlDateTime deliveryDate, SqlString deliveryTime, SqlString product, SqlString pizzaCrust, SqlString pizzaSauce, SqlMoney price, SqlMoney deliveryCost, SqlDecimal amount, SqlString extraIngredients, SqlMoney extraIngredientPrice, SqlMoney linePrice, SqlMoney totalPrice, SqlString usedVoucher, SqlMoney voucherDiscount, SqlMoney totalPriceAfterDiscount)
        {
            StoreName = storeName;
            CustomerName = customerName;
            PhoneNumber = phoneNumber;
            Email = email;
            Address = address;
            City = city;
            OrderDate = orderDate;
            DeliveryType = deliveryType;
            DeliveryDate = deliveryDate;
            DeliveryTime = deliveryTime;
            Product = product;
            PizzaCrust = pizzaCrust;
            PizzaSauce = pizzaSauce;
            Price = price;
            DeliveryCost = deliveryCost;
            Amount = amount;
            ExtraIngredients = extraIngredients;
            ExtraIngredientPrice = extraIngredientPrice;
            LinePrice = linePrice;
            TotalPrice = totalPrice;
            UsedVoucher = usedVoucher;
            VoucherDiscount = voucherDiscount;
            TotalPriceAfterDiscount = totalPriceAfterDiscount;
        }
    }
}
