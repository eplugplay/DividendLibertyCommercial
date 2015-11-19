using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;

namespace DividendLiberty
{
    public static class uti
    {
        //public FileTypes filetypes { get; set; }
        public static string GetFilePath(FileTypes type)
        {
            string xmlPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/" + GetFileName(type);
            return xmlPath;
        }

        public static string GetLocalFilePath(FileTypes type)
        {
            string localPath = Path.Combine(Directory.GetCurrentDirectory(), GetFileName(type));
            return localPath;
        }

        public static string GetFileName(FileTypes type)
        {
            string returnType = "";
            switch (type)
            {
                case FileTypes.xml: returnType = "DividendLibertyStocks.xml"; break;
                case FileTypes.ini: returnType = "DividendLibertyConfig.ini"; break;
                case FileTypes.excel: returnType = "DividendLiberty.xls"; break;
                default: break;
            }
            return returnType;
        }

        public static void SaveIniFile(string section, string key, string keyValue)
        {
            INIFile iniFile = new INIFile(GetFilePath(FileTypes.ini));
            iniFile.Write(section, key, keyValue);
        }

        public static string ReadIniFile(string section, string key)
        {
            INIFile iniFile = new INIFile(GetFileName(FileTypes.ini));
            return iniFile.Read(section, key);
        }

        public static DataTable GetXMLData()
        {
            DataSet dtXML = new DataSet();
            dtXML.ReadXml(uti.GetFilePath(FileTypes.xml));
            DataTable dt = dtXML.Tables[1];
            return dt;
        }

        public static string IncrementStockID()
        {
            DataTable dt = uti.GetXMLData();
            int count = dt.Rows.Count + 1;
            return count.ToString();
        }

        public static bool ValidateStock(string symbol)
        {
            DataTable dt = uti.GetXMLData();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["symbol"].ToString() == symbol.ToUpper())
                {
                    return true;
                }
            }
            return false;
        }

        public static bool DividendStatsValid(List<int> lst)
        {
            if (lst.Count == 1)
            {
                return true;
            }
            return false;
        }

        public static List<StockInfo> LoadStockInfo(string id, string symbol, string name, string industry, string shares, string cost, string exDiv, string payDate, string interval)
        {
            List<StockInfo> lst = new List<StockInfo>();
            StockInfo stockInfo = new StockInfo();
            stockInfo.ID = id;
            stockInfo.Symbol = symbol;
            stockInfo.Name = name;
            stockInfo.Industry = industry;
            stockInfo.Shares = shares;
            stockInfo.Cost = cost;
            stockInfo.ExDiv = exDiv;
            stockInfo.PayDate = payDate;
            stockInfo.Interval = interval;
            lst.Add(stockInfo);
            return lst;
        }

        public static void ClearListViewColors(ListView lv)
        {
            for (int i = 0; i < lv.Items.Count; i++)
            {
                lv.InvokeEx(x => x.Items[i].BackColor = Color.White);
            }
        }

        public static bool IsLettersOnly(string input)
        {
            if (Regex.IsMatch(input, @"^[a-zA-Z]+$"))
            {
                return true;
            }
            return false;
        }

        public static void SetStockIndexSymbol(ListView lv)
        {
            Program.MainMenu.SelectedIndex = lv.SelectedItems[0].Index;
            Program.MainMenu.Symbol = lv.Items[Program.MainMenu.SelectedIndex].SubItems[1].Text;
        }

        public static decimal GetDivPrice(decimal shares, decimal annualDiv)
        {
            decimal toReturn = (shares * annualDiv);
            return toReturn;
        }

        public static string[] SplitStockData(string val)
        {
            string[] split = val.Split('\n');
            return split;
        }

        public static string GetStockSymbols(DataTable dt)
        {
            string symbols = "";
            int count = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                symbols += dt.Rows[i]["symbol"].ToString() + "+";
                count++;
            }
            return symbols = symbols.Substring(0, symbols.Length - 1);
        }

        public static Color GetHighlightColor()
        {
            return Color.BurlyWood;
        }

        public static decimal GetTotalSectorCount(List<decimal> lst)
        {
            decimal sectorCount = 0;
            for (int i = 0; i < lst.Count; i++)
            {
                sectorCount += lst[i];
            }
            return sectorCount;
        }

        public static DataTable ConvertExcelData(DataTable dt, string[] yields, string[] annualDiv, string[] company)
        {
            decimal totalCost = 0;
            decimal yearlyDividends = 0;
            DataTable dtFinal = new DataTable();
            dtFinal.Columns.Add("Symbol", typeof(string));
            dtFinal.Columns.Add("Company", typeof(string));
            dtFinal.Columns.Add("Industry", typeof(string));
            dtFinal.Columns.Add("Shares", typeof(double));
            dtFinal.Columns.Add("Price", typeof(double));
            dtFinal.Columns.Add("Annual Dividend", typeof(double));
            dtFinal.Columns.Add("Yield", typeof(double));
            dtFinal.Columns.Add("Monthly Dividends", typeof(double));
            dtFinal.Columns.Add("Quarterly Dividends", typeof(double));
            dtFinal.Columns.Add("Yearly Dividends", typeof(double));
            dtFinal.Columns.Add("Cost Basis", typeof(double));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["active"].ToString() == "true")
                {
                    DataRow dr = dtFinal.NewRow();
                    decimal shares = Convert.ToDecimal(dt.Rows[i]["shares"]);
                    decimal cost = Convert.ToDecimal(dt.Rows[i]["cost"]);
                    decimal annDiv = annualDiv[i] == "N/A" ? 0 : Convert.ToDecimal(annualDiv[i]);
                    decimal yearlyDiv = Math.Round(shares * annDiv, 2);
                    dr["Symbol"] = dt.Rows[i]["symbol"].ToString();
                    dr["Company"] = company[i].Length == 0 ? "" : company[i].ToString();
                    dr["Industry"] = dt.Rows[i]["industry"].ToString();
                    dr["Shares"] = Convert.ToDouble(shares);
                    dr["Price"] = Convert.ToDouble(cost);
                    dr["Annual Dividend"] = Convert.ToDouble(annDiv);
                    dr["Yield"] = yields[i] == "N/A" ? 0 : Convert.ToDouble(yields[i]);
                    dr["Monthly Dividends"] = Convert.ToDouble(Math.Round(yearlyDiv / 12, 2));
                    dr["Quarterly Dividends"] = Convert.ToDouble(Math.Round(yearlyDiv / 4, 2));
                    dr["Yearly Dividends"] = Convert.ToDouble(yearlyDiv);
                    dr["Cost Basis"] = Math.Round(Convert.ToDouble(shares * cost), 2);
                    yearlyDividends += Convert.ToDecimal(yearlyDiv);
                    totalCost += Math.Round(Convert.ToDecimal(dt.Rows[i]["shares"]) * Convert.ToDecimal(dt.Rows[i]["cost"]), 2);
                    dtFinal.Rows.Add(dr);
                }
            }

            DataRow drEmpty = dtFinal.NewRow();
            drEmpty["Yield"] = 0;
            //drEmpty["Annual Dividend"] = 0;
            drEmpty["Monthly Dividends"] = 0;
            drEmpty["Quarterly Dividends"] = 0;
            drEmpty["Yearly Dividends"] = 0;
            drEmpty["Cost Basis"] = 0;
            dtFinal.Rows.Add(drEmpty);

            return dtFinal;
        }

        public static DataTable SortDataTable(DataTable dt, string order)
        {
            DataView view = dt.DefaultView;
            view.Sort = "symbol " + order;
            DataTable dtXml = view.ToTable();
            return dtXml;
        }
    }

    public enum FileTypes
    {
        xml,
        ini,
        excel,
    }

    public static class ISynchronizeInvokeExtensions
    {
        public static void InvokeEx<T>(this T @this, Action<T> action) where T : ISynchronizeInvoke
        {
            if (@this.InvokeRequired)
            {
                @this.Invoke(action, new object[] { @this });
            }
            else
            {
                action(@this);
            }
        }
    }
}
