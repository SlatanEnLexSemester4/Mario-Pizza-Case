using System;
using System.Text;

namespace Mario_Data_Conversion_Tool
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            String marioWinkelsFile = "C:\\Users\\lex\\Downloads\\MarioData\\Winkels Mario.txt";
            String extraIngredientenFile = "C:\\Users\\lex\\Downloads\\MarioData\\Extra Ingredienten.csv";
            String pizzaBodemsFile = "C:\\Users\\lex\\Downloads\\MarioData\\pizzabodems.xlsx";
            String overigeProductenFile = "C:\\Users\\lex\\Downloads\\MarioData\\Overige producten.xlsx";

            MarioWinkelsConverter winkelConverter = new MarioWinkelsConverter(marioWinkelsFile);
            MarioExtraIngredientenConverter extraIngredientConverter = new MarioExtraIngredientenConverter(extraIngredientenFile);
            MarioPizzaBodemsConverter pizzaBodemsConverter = new MarioPizzaBodemsConverter(pizzaBodemsFile);
            MarioOverigeProductenConverter overigeProductenConverter = new MarioOverigeProductenConverter(overigeProductenFile);

            winkelConverter.ReadFile();
            extraIngredientConverter.ReadFile();
            pizzaBodemsConverter.ReadFile();
            overigeProductenConverter.ReadFile();

        }
    }
}
