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

            String marioWinkelsFile = "C:\\Users\\lex\\Downloads\\MarioData\\Winkels Mario.txt";
            String extraIngredientenFile = "C:\\Users\\lex\\Downloads\\MarioData\\Extra Ingredienten.csv";
            String pizzaBodemsFile = "C:\\Users\\lex\\Downloads\\MarioData\\pizzabodems.xlsx";
            String overigeProductenFile = "C:\\Users\\lex\\Downloads\\MarioData\\Overige producten.xlsx";
            String pizzaIngredientenFile = "C:\\Users\\lex\\Downloads\\MarioData\\pizza_ingredienten.xlsx";
            String[] orderDataFiles = {"C:\\Users\\lex\\Downloads\\MarioData\\MarioOrderData01_10000.csv",
                                            "C:\\Users\\lex\\Downloads\\MarioData\\MarioOrderData02_10000.csv",
                                            "C:\\Users\\lex\\Downloads\\MarioData\\MarioOrderData03_10000.csv",
                                            "C:\\Users\\lex\\Downloads\\MarioData\\MarioOrderData04_10000.csv"};

            log.Info("- - - - -");
            log.Info("Succesfuly loaded files");
            log.Info("- - - - -");

            MarioWinkelsConverter winkelConverter = new MarioWinkelsConverter(marioWinkelsFile);
            MarioExtraIngredientenConverter extraIngredientConverter = new MarioExtraIngredientenConverter(extraIngredientenFile);
            MarioPizzaBodemsConverter pizzaBodemsConverter = new MarioPizzaBodemsConverter(pizzaBodemsFile);
            MarioOverigeProductenConverter overigeProductenConverter = new MarioOverigeProductenConverter(overigeProductenFile);
            MarioPizzaIngredientenConverter pizzaIngredientenConverter = new MarioPizzaIngredientenConverter(pizzaIngredientenFile);
            MarioOrderDataConverter orderDataConvertor = new MarioOrderDataConverter(orderDataFiles);


            //winkelConverter.Convert();
            extraIngredientConverter.Convert();
            //pizzaBodemsConverter.Convert();
            //overigeProductenConverter.Convert();
            //pizzaIngredientenConverter.Convert();
            orderDataConvertor.Convert();

            log.Info("- - - - -");
            log.Info("Ending : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            log.Info("- - - - -");

        }
    }
}
