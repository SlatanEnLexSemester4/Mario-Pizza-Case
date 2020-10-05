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
        public DateTime OrderDate { get; private set; }
        public SqlString DeliveryType { get; private set; }
        public DateTime DeliveryDate { get; private set; }
        public SqlString DeliveryTime { get; private set; }
        public SqlString Product { get; private set; }
        public SqlString PizzaCrust { get; private set; }
        public SqlString PizzaSauce { get; private set; }
        public SqlDecimal Price { get; private set; }
        public SqlDecimal DeliveryCost { get; private set; }
        public SqlDecimal Amount { get; private set; }
        public SqlString ExtraIngredients { get; private set; }
        public SqlDecimal ExtraIngredientPrice { get; private set; }
        public SqlDecimal LinePrice { get; private set; }
        public SqlDecimal TotalPrice { get; private set; }
        public SqlString UsedVoucher { get; private set; }
        public SqlDecimal VoucherDiscount { get; private set; }
        public SqlDecimal TotalPriceAfterDiscount { get; private set; }

        public Order(SqlString storeName, SqlString customerName, SqlString phoneNumber, SqlString email, SqlString address, SqlString city, DateTime orderDate, SqlString deliveryType, DateTime deliveryDate, SqlString deliveryTime, SqlString product, SqlString pizzaCrust, SqlString pizzaSauce, SqlDecimal price, SqlDecimal deliveryCost, SqlDecimal amount, SqlString extraIngredients, SqlDecimal extraIngredientPrice, SqlDecimal linePrice, SqlDecimal totalPrice, SqlString usedVoucher, SqlDecimal voucherDiscount, SqlDecimal totalPriceAfterDiscount)
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
