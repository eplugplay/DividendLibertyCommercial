﻿using System;
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
