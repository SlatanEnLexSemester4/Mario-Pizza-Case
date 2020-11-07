using Mario_Data_Conversion_Tool.Converters;
using System;
using System.Text;

namespace Mario_Data_Conversion_Tool
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly log4net.ILog logwarn = log4net.LogManager.GetLogger("logWarn");

        static void Main(string[] args)
        {
            log.Info("- - - - -");
            log.Info("Running " + System.AppDomain.CurrentDomain.FriendlyName);
            log.Info("- - - - -");
            logwarn.Info("- - - - -");
            logwarn.Info("Running " + System.AppDomain.CurrentDomain.FriendlyName);
            logwarn.Info("- - - - -");

            Console.OutputEncoding = Encoding.UTF8;

            String marioWinkelsFile = "C:\\Temp\\MarioData\\Winkels Mario.txt";
            String extraIngredientenFile = "C:\\Temp\\MarioData\\Extra Ingredienten.csv";
            String pizzaBodemsFile = "C:\\Temp\\MarioData\\pizzabodems.xlsx";
            String overigeProductenFile = "C:\\Temp\\MarioData\\Overige producten.xlsx";
            String pizzaIngredientenFile = "C:\\Temp\\MarioData\\pizza_ingredienten.xlsx";
            String[] orderDataFiles = {"C:\\Temp\\MarioData\\MarioOrderData01_10000.csv",
                                            "C:\\Temp\\MarioData\\MarioOrderData02_10000.csv",
                                            "C:\\Temp\\MarioData\\MarioOrderData03_10000.csv",
                                            "C:\\Temp\\MarioData\\MarioOrderData04_10000.csv"};
            String postcodeTabelFile = "C:\\Temp\\MarioData\\Postcode tabel.mdb";

            log.Info("- - - - -");
            log.Info("Succesfuly loaded files");
            log.Info("- - - - -");

            MarioWinkelsConverter winkelConverter = new MarioWinkelsConverter(marioWinkelsFile);
            MarioExtraIngredientenConverter extraIngredientConverter = new MarioExtraIngredientenConverter(extraIngredientenFile);
            MarioPizzaBodemsConverter pizzaBodemsConverter = new MarioPizzaBodemsConverter(pizzaBodemsFile);
            MarioOverigeProductenConverter overigeProductenConverter = new MarioOverigeProductenConverter(overigeProductenFile);
            MarioPizzaIngredientenConverter pizzaIngredientenConverter = new MarioPizzaIngredientenConverter(pizzaIngredientenFile);
            MarioOrderDataConverter orderDataConvertor = new MarioOrderDataConverter(orderDataFiles);
            MarioPostcodeTabelConverter postcodeTabelConverter = new MarioPostcodeTabelConverter(postcodeTabelFile);


            int winkelLines = winkelConverter.Convert();
            int extraIngredientLines = extraIngredientConverter.Convert();
            int pizzaBodemsLines = pizzaBodemsConverter.Convert();
            int overigeProductenLines = overigeProductenConverter.Convert();
            int pizzaIngredientenLines = pizzaIngredientenConverter.Convert();
            int orderLines = orderDataConvertor.Convert();
            var gemeentePostcodeLines = postcodeTabelConverter.Convert();

            log.Info("- - - - -");
            log.Info("Ending : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            log.Info("- - - - -");

            log.Info("- - - - -");
            log.Info("Total lines added during import:");
            log.Info("Winkels:            " + winkelLines);
            log.Info("Extra Ingredienten: " + extraIngredientLines);
            log.Info("Pizza Bodems:       " + pizzaBodemsLines);
            log.Info("Overige Producten:  " + overigeProductenLines);
            log.Info("Pizza Ingredienten: " + pizzaIngredientenLines);
            log.Info("Orders:             " + orderLines);
            log.Info("Gemeenten:          " + gemeentePostcodeLines.Item1);
            log.Info("Postcodes:          " + gemeentePostcodeLines.Item2);
            log.Info("- - - - -");

        }
    }
}
