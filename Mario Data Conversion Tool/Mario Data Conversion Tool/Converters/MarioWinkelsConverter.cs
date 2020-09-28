using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Mario_Data_Conversion_Tool.DataTypes;

namespace Mario_Data_Conversion_Tool
{
    class MarioWinkelsConverter
    {
        String fileName;
        int headerLines = 3;

        public MarioWinkelsConverter(string fileName)
        {
            this.fileName = fileName;
        }

        public void ReadFile() 
        {
            string line;
            int winkelCounter = 0;
            string tempName = "";
            string tempStreet = "";
            string tempNumber = "";
            string tempCity = "";
            string tempCountryCode = "";
            string tempZipcode = "";
            string tempPhoneNumber = "";
            List<Winkel> winkels = new List<Winkel>();

            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(fileName);

            // Read header lines to skip them.
            for(int i = 0; i < headerLines; i++)
            {
                file.ReadLine();
            }

            // Read lines in file and 
            while ((line = file.ReadLine()) != null)
            {
                switch (winkelCounter)
                {
                    case 0:
                        tempName = line;
                        winkelCounter++;
                        break;
                    case 1:
                        tempStreet = line;
                        winkelCounter++;
                        break;
                    case 2:
                        tempNumber = line;
                        winkelCounter++;
                        break;
                    case 3:
                        tempCity = line;
                        winkelCounter++;
                        break;
                    case 4:
                        tempCountryCode = line;
                        winkelCounter++;
                        break;
                    case 5:
                        tempZipcode = line;
                        winkelCounter++;
                        break;
                    case 6:
                        tempPhoneNumber = line;
                        winkelCounter++;
                        break;
                    case 7:
                        winkelCounter = 0;
                        winkels.Add(new Winkel(tempName, tempStreet, tempNumber, tempCity, tempCountryCode, tempZipcode, tempPhoneNumber));
                        tempName = "";
                        tempStreet = "";
                        tempNumber = "";
                        tempCity = "";
                        tempCountryCode = "";
                        tempZipcode = "";
                        tempPhoneNumber = "";
                        break;
                    default:
                        Console.WriteLine("Default case");
                        break;
                }
            }

            // Write last store to list
            winkels.Add(new Winkel(tempName, tempStreet, tempNumber, tempCity, tempCountryCode, tempZipcode, tempPhoneNumber));

            file.Close();
            // Suspend the screen.  
            foreach (Winkel winkel in winkels){
                System.Console.WriteLine(winkel.Name + " " + winkel.Streetname + " " + winkel.Number + " " + winkel.City + " " + winkel.Countrycode + " " + winkel.Zipcode + " " + winkel.Telephonenumber);
            }
        }

        public void TestFunction() {
            Winkel bla = new Winkel("Winkel 1", "Straat 1", "Nummer 1", "Stad 1", "Landcode 1", "Postcode 1", "Telefoonnummer 1");
            Console.WriteLine(bla.Name);

         }
    }
}
