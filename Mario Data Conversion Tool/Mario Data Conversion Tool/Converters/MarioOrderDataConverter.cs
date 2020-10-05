using Mario_Data_Conversion_Tool.DataTypes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Mario_Data_Conversion_Tool
{
    class MarioOrderDataConverter
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly log4net.ILog logwarn = log4net.LogManager.GetLogger("logWarn");

        String[] fileNames;
        int headerLines = 5;

        public MarioOrderDataConverter(string[] fileNames)
        {
            this.fileNames = fileNames;
        }

        public void Convert()
        {
            log.Info("- - - - -");
            log.Info("Running : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            log.Info("- - - - -");

            List<Order> orders = ReadFiles();
            Upload(orders);
        }

        public List<Order> ReadFiles()
        {
            log.Info("- - - - -");
            log.Info("Running : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            log.Info("- - - - -");

            string line;
            CultureInfo cultureinfo = new System.Globalization.CultureInfo("nl-NL");
            List<Order> orders = new List<Order>();

            foreach (string fileName in fileNames)
            {
                log.Info("- - - - -");
                log.Info("Filename : " + fileName);
                log.Info("- - - - -");

                // Read the file and display it line by line.  
                System.IO.StreamReader file =
                    new System.IO.StreamReader(fileName);

                // Read header lines to skip them.
                for (int i = 0; i < headerLines; i++)
                {
                    file.ReadLine();
                }

                int lineCounter = 1;

                // Read lines in file and 
                while ((line = file.ReadLine()) != null)
                {
                    var values = line.Split(";", StringSplitOptions.None);

                    DateTime tempOrderDate;
                    string tempDeliveryType = "";
                    DateTime tempDeliveryDate;
                    decimal tempPrice;
                    decimal tempDeliveryCost;
                    decimal tempAmount;
                    decimal tempExtraIngredientPrice;
                    decimal tempLinePrice;
                    decimal tempTotalPrice;
                    decimal tempVoucherDiscount;
                    decimal tempPriceAfterDiscount;

                    if (values.Length != 1)
                    {
                        if (values[6] != "")
                        {
                            tempOrderDate = DateTime.Parse(values[6], cultureinfo);

                        }
                        else
                        {
                            tempOrderDate = DateTime.Parse("01/01/1800");
                        }
                        if (values[7] == "Bezorgen")
                        {
                            tempDeliveryType = "1";
                        }
                        else if (values[7] == "Afhalen")
                        {
                            tempDeliveryType = "0";
                        }
                        if (values[8] != "")
                        {
                            tempDeliveryDate = DateTime.Parse(values[8], cultureinfo);

                        }
                        else
                        {
                            tempDeliveryDate = DateTime.Parse("01/01/1800");
                        }                        
                        if (values[13] != "")
                        {
                            values[13] = new string(values[13].Where(c => (Char.IsDigit(c) || c == '.' || c == ',')).ToArray());
                            tempPrice = decimal.Parse(values[13], cultureinfo);
                        }
                        else
                        {
                            tempPrice = 0;
                        }

                        if (values[14] != "" && values[14] != " ")
                        {
                            values[14] = new string(values[14].Where(c => (Char.IsDigit(c) || c == '.' || c == ',')).ToArray());
                            tempDeliveryCost = decimal.Parse(values[14], cultureinfo);
                        }
                        else
                        {
                            tempDeliveryCost = 0;
                        }

                        if (values[15] != "")
                        {
                            tempAmount = decimal.Parse(values[15]);
                        }
                        else
                        {
                            tempAmount = 0;
                        }

                        if (values[17] != "")
                        {
                            values[17] = new string(values[17].Where(c => (Char.IsDigit(c) || c == '.' || c == ',')).ToArray());
                            tempExtraIngredientPrice = decimal.Parse(values[17], cultureinfo);
                        }
                        else
                        {
                            tempExtraIngredientPrice = 0;
                        }

                        if (values[18] != "")
                        {
                            values[18] = new string(values[18].Where(c => (Char.IsDigit(c) || c == '.' || c == ',')).ToArray());
                            tempLinePrice = decimal.Parse(values[18], cultureinfo);
                        }
                        else
                        {
                            tempLinePrice = 0;
                        }

                        if (values[19] != "")
                        {
                            values[19] = new string(values[19].Where(c => (Char.IsDigit(c) || c == '.' || c == ',')).ToArray());
                            tempTotalPrice = decimal.Parse(values[19], cultureinfo);
                        }
                        else
                        {
                            tempTotalPrice = 0;
                        }

                        if (values[21] != "")
                        {
                            values[21] = new string(values[21].Where(c => (Char.IsDigit(c) || c == '.' || c == ',')).ToArray());
                            tempVoucherDiscount = decimal.Parse(values[21], cultureinfo);
                        }
                        else
                        {
                            tempVoucherDiscount = 0;
                        }

                        if (values[22] != "")
                        {
                            values[22] = new string(values[22].Where(c => (Char.IsDigit(c) || c == '.' || c == ',')).ToArray());
                            tempPriceAfterDiscount = decimal.Parse(values[22], cultureinfo);
                        }
                        else
                        {
                            tempPriceAfterDiscount = 0;
                        }


                        orders.Add(new Order(values[0],
                            values[1],
                            values[2],
                            values[3],
                            values[4],
                            values[5],
                            tempOrderDate,
                            tempDeliveryType,
                            tempDeliveryDate,
                            values[9],
                            values[10],
                            values[11],
                            values[12],
                            tempPrice,
                            tempDeliveryCost,
                            tempAmount,
                            values[16],
                            tempExtraIngredientPrice,
                            tempLinePrice,
                            tempTotalPrice,
                            values[20],
                            tempVoucherDiscount,
                            tempPriceAfterDiscount));
                        log.Info("Succesfully added line:" + lineCounter);
                    }
                    lineCounter++;
                }
                file.Close();          
            }
            return orders;
        }

        public void Upload(List<Order> orders)
        {
            log.Info("- - - - -");
            log.Info("Running : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            log.Info("- - - - -");
            CultureInfo cultureinfo = new System.Globalization.CultureInfo("nl-NL");

            foreach (Order order in orders)
            {
                ExecuteQuery(order.StoreName.ToString(), 
                    order.CustomerName.ToString(), 
                    order.PhoneNumber.ToString(), 
                    order.Email.ToString(), 
                    order.Address.ToString(),
                    order.City.ToString(),
                    order.OrderDate.ToString("u", cultureinfo),
                    order.DeliveryType.ToString(),
                    order.DeliveryDate.ToString("u", cultureinfo),
                    order.DeliveryTime.ToString(),
                    order.Product.ToString(),
                    order.PizzaCrust.ToString(),
                    order.PizzaSauce.ToString(),
                    order.Price.ToString(),
                    order.DeliveryCost.ToString(),
                    order.Amount.ToString(),
                    order.ExtraIngredients.ToString(),
                    order.ExtraIngredientPrice.ToString(),
                    order.LinePrice.ToString(),
                    order.TotalPrice.ToString(),
                    order.UsedVoucher.ToString(),
                    order.VoucherDiscount.ToString(),
                    order.TotalPriceAfterDiscount.ToString());
            }

        }
        private void ExecuteQuery(
            string StoreName,
            string CustomerName, 
            string PhoneNumber, 
            string Email, 
            string Address, 
            string City, 
            string OrderDate, 
            string DeliveryType, 
            string DeliveryDate, 
            string DeliveryTime,
            string Product,
            string PizzaCrust,
            string PizzaSauce,
            string Price,
            string DeliveryCost,
            string Amount,
            string ExtraIngredients,
            string ExtraIngredientPrice,
            string LinePrice,
            string TotalPrice,
            string UsedVoucher,
            string VoucherDiscount,
            string TotalPriceAfterDiscount)
        {
            string Server = "localhost";
            string Database = "ShadowDB";

            var conn = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + Database + ";User ID=mario;Password=mario");

            String query = "INSERT INTO dbo.MarioOrderData (Winkelnaam,Klantnaam,Telefoon,Email,Adres,Woonplaats,Besteldatum,AfleverType,AfleverDatum,AfleverMoment,Product,PizzaBodem,PizzaSaus,Prijs,Bezorgkosten,Aantal,ExtraIngredienten,PrijsExtraIngredient,Regelprijs,Totaalprijs,GebruikteCoupon,CouponKorting,TeBetalen) VALUES (@Winkelnaam,@Klantnaam,@Telefoon,@Email,@Adres,@Woonplaats,@Besteldatum,@AfleverType,@AfleverDatum,@AfleverMoment,@Product,@PizzaBodem,@PizzaSaus,@Prijs,@Bezorgkosten,@Aantal,@ExtraIngredienten,@PrijsExtraIngredient,@Regelprijs,@Totaalprijs,@GebruikteCoupon,@CouponKorting,@TeBetalen)";
            log.Info(OrderDate + " " + DeliveryDate + " " + DeliveryTime);
            try
            {

                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = query;

                command.Parameters.AddWithValue("@Winkelnaam ", StoreName);
                command.Parameters.AddWithValue("@Klantnaam ", CustomerName);
                command.Parameters.AddWithValue("@Telefoon ", PhoneNumber);
                command.Parameters.AddWithValue("@Email ", Email);
                command.Parameters.AddWithValue("@Adres ", Address);
                command.Parameters.AddWithValue("@Woonplaats ", City);
                command.Parameters.AddWithValue("@Besteldatum ", OrderDate);
                command.Parameters.AddWithValue("@AfleverType ", DeliveryType);
                command.Parameters.AddWithValue("@AfleverDatum ", DeliveryDate);
                command.Parameters.AddWithValue("@AfleverMoment ", DeliveryTime);
                command.Parameters.AddWithValue("@Product ", Product);
                command.Parameters.AddWithValue("@PizzaBodem ", PizzaCrust);
                command.Parameters.AddWithValue("@PizzaSaus ", PizzaSauce);
                command.Parameters.AddWithValue("@Prijs ", Price);
                command.Parameters.AddWithValue("@Bezorgkosten ", DeliveryCost);
                command.Parameters.AddWithValue("@Aantal ", Amount);
                command.Parameters.AddWithValue("@ExtraIngredienten ", ExtraIngredients);
                command.Parameters.AddWithValue("@PrijsExtraIngredient ", ExtraIngredientPrice);
                command.Parameters.AddWithValue("@Regelprijs ", LinePrice);
                command.Parameters.AddWithValue("@Totaalprijs ", TotalPrice);
                command.Parameters.AddWithValue("@GebruikteCoupon ", UsedVoucher);
                command.Parameters.AddWithValue("@CouponKorting ", VoucherDiscount);
                command.Parameters.AddWithValue("@TeBetalen ", TotalPriceAfterDiscount);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }
        }
    }
}
