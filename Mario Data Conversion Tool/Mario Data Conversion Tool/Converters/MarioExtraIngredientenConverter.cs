using Mario_Data_Conversion_Tool.DataTypes;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace Mario_Data_Conversion_Tool
{
    class MarioExtraIngredientenConverter
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        String fileName;
        int headerLines = 1;

        public MarioExtraIngredientenConverter(string fileName)
        {
            this.fileName = fileName;
        }

        public void ReadFile()
        {
            log.Info("- - - - -");
            log.Info("Running : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            log.Info("- - - - -");

            string line;
            List<ExtraIngredient> extraIngredienten = new List<ExtraIngredient>();

            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(fileName);

            // Read header lines to skip them.
            for (int i = 0; i < headerLines; i++)
            {
                file.ReadLine();
            }

            // Read lines in file and 
            while ((line = file.ReadLine()) != null)
            {
                var values = line.Split(";", StringSplitOptions.None);
                values[1] = new string(values[1].Where(c => (Char.IsDigit(c) || c == '.' || c == ',')).ToArray());

                extraIngredienten.Add(new ExtraIngredient(values[0], decimal.Parse(values[1], CultureInfo.InvariantCulture)));
                log.Info("Added line:" + line);

            }
            file.Close();

        }
    }
}
