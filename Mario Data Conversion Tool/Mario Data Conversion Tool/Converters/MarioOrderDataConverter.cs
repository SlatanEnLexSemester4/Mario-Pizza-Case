using Mario_Data_Conversion_Tool.Converters;
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

        public int Convert()
        {
            log.Info("- - - - -");
            log.Info("Running : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            log.Info("- - - - -");

            List<Order> orders = ReadFiles();
            Upload(orders);
            return orders.Count;
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

                    string tempDeliveryType = "";

                    if (values.Length != 1)
                    {
                        if (values[6] != "")
                        {
                            values[6] = DateTime.Parse(values[6], cultureinfo).ToString("yyyy-MM-dd");
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
                            values[8] = DateTime.Parse(values[8], cultureinfo).ToString("yyyy-MM-dd");

                        }                  
                        if (values[13] != "")
                        {
                            values[13] = new string(values[13].Where(c => (Char.IsDigit(c) || c == '.' || c == ',')).ToArray());
                            values[13] = decimal.Parse(values[13], cultureinfo).ToString();
                            values[13] = values[13].Replace(",", ".");
                        }

                        if (values[14] != "" && values[14] != " ")
                        {
                            values[14] = new string(values[14].Where(c => (Char.IsDigit(c) || c == '.' || c == ',')).ToArray());
                            values[14] = decimal.Parse(values[14], cultureinfo).ToString();
                            values[14] = values[14].Replace(",", ".");
                        }

                        if (values[17] != "")
                        {
                            values[17] = new string(values[17].Where(c => (Char.IsDigit(c) || c == '.' || c == ',')).ToArray());
                            values[17] = decimal.Parse(values[17], cultureinfo).ToString();
                            values[17] = values[17].Replace(",", ".");
                        }

                        if (values[18] != "")
                        {
                            values[18] = new string(values[18].Where(c => (Char.IsDigit(c) || c == '.' || c == ',')).ToArray());
                            values[18] = decimal.Parse(values[18], cultureinfo).ToString();
                            values[18] = values[18].Replace(",", ".");
                        }

                        if (values[19] != "")
                        {
                            values[19] = new string(values[19].Where(c => (Char.IsDigit(c) || c == '.' || c == ',')).ToArray());
                            values[19] = decimal.Parse(values[19], cultureinfo).ToString();
                            values[19] = values[19].Replace(",", ".");
                        }

                        if (values[21] != "")
                        {
                            values[21] = new string(values[21].Where(c => (Char.IsDigit(c) || c == '.' || c == ',')).ToArray());
                            values[21] = decimal.Parse(values[21], cultureinfo).ToString();
                            values[21] = values[21].Replace(",", ".");
                        }

                        if (values[22] != "")
                        {
                            values[22] = new string(values[22].Where(c => (Char.IsDigit(c) || c == '.' || c == ',')).ToArray());
                            values[22] = decimal.Parse(values[22], cultureinfo).ToString();
                            values[22] = values[22].Replace(",", ".");
                        }

                        orders.Add(new Order(values[0],
                            values[1],
                            values[2],
                            values[3],
                            values[4],
                            values[5],
                            values[6],
                            tempDeliveryType,
                            values[8],
                            values[9],
                            values[10],
                            values[11],
                            values[12],
                            values[13],
                            values[14],
                            values[15],
                            values[16],
                            values[17],
                            values[18],
                            values[19],
                            values[20],
                            values[21],
                            values[22]));
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
            
            //Drop table and create new one
            SqlConnection conn = SqlConnectionMaker.ReturnConnection();

            try
            {

                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "DROP TABLE IF EXISTS dbo.MarioOrderData";
                command.ExecuteNonQuery();
                command.CommandText = "CREATE TABLE dbo.MarioOrderData (Orderid int IDENTITY, Winkelnaam varchar(200),Klantnaam varchar(200),Telefoon varchar(200),Email varchar(200),Adres varchar(200),Woonplaats varchar(200),Besteldatum varchar(200),AfleverType varchar(200),AfleverDatum varchar(200),AfleverMoment varchar(200),Product varchar(200),PizzaBodem varchar(200),PizzaSaus varchar(200),Prijs varchar(200),Bezorgkosten varchar(200),Aantal varchar(200),ExtraIngredienten varchar(200),PrijsExtraIngredient varchar(200),Regelprijs varchar(200),Totaalprijs varchar(200),GebruikteCoupon varchar(200),CouponKorting varchar(200),TeBetalen varchar(200))";
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

            foreach (Order order in orders)
            {
                ExecuteQuery(order.StoreName.ToString(), 
                    order.CustomerName.ToString(), 
                    order.PhoneNumber.ToString(), 
                    order.Email.ToString(), 
                    order.Address.ToString(),
                    order.City.ToString(),
                    order.OrderDate.ToString(),
                    order.DeliveryType.ToString(),
                    order.DeliveryDate.ToString(),
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
            SqlConnection conn = SqlConnectionMaker.ReturnConnection();

            String query = "INSERT INTO dbo.MarioOrderData (Winkelnaam,Klantnaam,Telefoon,Email,Adres,Woonplaats,Besteldatum,AfleverType,AfleverDatum,AfleverMoment,Product,PizzaBodem,PizzaSaus,Prijs,Bezorgkosten,Aantal,ExtraIngredienten,PrijsExtraIngredient,Regelprijs,Totaalprijs,GebruikteCoupon,CouponKorting,TeBetalen) VALUES (@Winkelnaam,@Klantnaam,@Telefoon,@Email,@Adres,@Woonplaats,@Besteldatum,@AfleverType,@AfleverDatum,@AfleverMoment,@Product,@PizzaBodem,@PizzaSaus,@Prijs,@Bezorgkosten,@Aantal,@ExtraIngredienten,@PrijsExtraIngredient,@Regelprijs,@Totaalprijs,@GebruikteCoupon,@CouponKorting,@TeBetalen)";
            log.Info(Product + PizzaCrust);
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
