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

namespace DividendLiberty
{
    public static class uti
    {
        public static string GetXMLPath()
        {
            string xmlPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/DividendStocksData.xml";
            return xmlPath;
        }

        public static DataTable GetXMLData()
        {
            DataSet dtXML = new DataSet();
            dtXML.ReadXml(uti.GetXMLPath());
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

        public static string[] SplitStockData(string val)
        {
            string[] split = val.Split('\n');
            return split;
        }

        public static string GetMultiSymbols(DataTable dt)
        {
            string toReturn = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                toReturn += dt.Rows[i]["symbol"].ToString() + "+";
            }
            toReturn = toReturn.Substring(0, toReturn.Length - 1);
            return toReturn;
        }

        public static string FilterDuplicates(string symbols)
        {
            string Symbols = "";
            string[] Split = symbols.Split('+');
            List<string> lstSymbols = new List<string>();
            for (int i = 0; i < Split.Length; i++)
            {
                if (!lstSymbols.Contains(Split[i]))
                {
                    lstSymbols.Add(Split[i]);
                }
            }
            for (int i = 0; i < lstSymbols.Count; i++)
            {
                Symbols += lstSymbols[i] + "+"; 
            }
            return Symbols = Symbols.Substring(0, Symbols.Length - 1);
        }
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
