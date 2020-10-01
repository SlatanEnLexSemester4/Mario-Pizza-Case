using System;
using System.Text;

namespace Mario_Data_Conversion_Tool
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            log.Info("- - - - -");
            log.Info("Running " + System.AppDomain.CurrentDomain.FriendlyName);
            log.Info("- - - - -");

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

            MarioWinkelsConverter winkelConverter = new MarioWinkelsConverter(marioWinkelsFile);
            MarioExtraIngredientenConverter extraIngredientConverter = new MarioExtraIngredientenConverter(extraIngredientenFile);
            MarioPizzaBodemsConverter pizzaBodemsConverter = new MarioPizzaBodemsConverter(pizzaBodemsFile);
            MarioOverigeProductenConverter overigeProductenConverter = new MarioOverigeProductenConverter(overigeProductenFile);
            MarioPizzaIngredientenConverter pizzaIngredientenConverter = new MarioPizzaIngredientenConverter(pizzaIngredientenFile);
            MarioOrderDataConverter orderDataConvertor = new MarioOrderDataConverter(orderDataFiles);

            
            winkelConverter.ReadFile();

            extraIngredientConverter.ReadFile();

            pizzaBodemsConverter.ReadFile();

            overigeProductenConverter.ReadFile();

            pizzaIngredientenConverter.ReadFile();

            orderDataConvertor.ReadFiles();

        }
    }
}
