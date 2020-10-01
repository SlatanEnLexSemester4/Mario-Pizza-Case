using Mario_Data_Conversion_Tool.DataTypes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Mario_Data_Conversion_Tool
{
    class MarioOrderDataConverter
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        String[] fileNames;
        int headerLines = 5;

        public MarioOrderDataConverter(string[] fileNames)
        {
            this.fileNames = fileNames;
        }

        public void ReadFiles()
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
                            values[7],
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
        }
    }
}
