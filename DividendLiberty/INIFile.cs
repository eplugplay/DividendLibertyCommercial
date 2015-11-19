using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace DividendLiberty
{
    public class INIFile
    {
        private string filePath;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
        string key,
        string val,
        string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
        string key,
        string def,
        StringBuilder retVal,
        int size,
        string filePath);

        public INIFile(string filePath)
        {
            this.filePath = filePath;
        }

        public void Write(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value.ToLower(), this.filePath);
        }

        public string Read(string section, string key)
        {
            StringBuilder SB = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, "", SB, 255, this.filePath);
            return SB.ToString();
        }

        public string FilePath
        {
            get { return this.filePath; }
            set { this.filePath = value; }
        }
    }

    public static class INIFileOptions
    {
        public static string[,] GetINISectionKeys()
        {
            string[,] sectionKeys = new string[2, 11];
            sectionKeys[0, 0] = "Column1";
            sectionKeys[0, 1] = "Column2";
            sectionKeys[0, 2] = "Column3";
            sectionKeys[0, 3] = "Column4";
            sectionKeys[0, 4] = "Column5";
            sectionKeys[0, 5] = "Column6";
            sectionKeys[0, 6] = "Column7";
            sectionKeys[0, 7] = "Column8";
            sectionKeys[0, 8] = "Column9";
            sectionKeys[0, 9] = "Column10";
            sectionKeys[0, 10] = "Column11";

            sectionKeys[1, 0] = "Symbols";
            sectionKeys[1, 2] = "Industry";
            sectionKeys[1, 3] = "Shares";
            sectionKeys[1, 4] = "Price";
            sectionKeys[1, 5] = "Annual Dividend";
            sectionKeys[1, 6] = "Yield";
            sectionKeys[1, 7] = "Monthly Dividends";
            sectionKeys[1, 8] = "Quarterly Dividends";
            sectionKeys[1, 9] = "Yearly Dividends";
            sectionKeys[1, 10] = "Cost Basis";
            return sectionKeys;
        }

        public static string[] SetINIValues(string[] Newalues)
        {
            string[] values = new string[11];
            values[0] = Newalues[0];
            values[1] = Newalues[1];
            values[2] = Newalues[2];
            values[3] = Newalues[3];
            values[4] = Newalues[4];
            values[5] = Newalues[5];
            values[6] = Newalues[6];
            values[7] = Newalues[7];
            values[8] = Newalues[8];
            values[9] = Newalues[9];
            values[10] = Newalues[10];
            return values;
        }
    }
}
