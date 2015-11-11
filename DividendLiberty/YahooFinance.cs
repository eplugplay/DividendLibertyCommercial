using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace DividendLiberty
{
    public static class YahooFinance
    {
        public static string GetValues(string symbol, string code, bool isMulti)
        {
            string value = "";
            WebClient client = new WebClient();
            var url = string.Format("http://download.finance.yahoo.com/d/quotes.csv?s={0}&f={1}", symbol, code);
            value = client.DownloadString(url);
            if (!isMulti)
            {
                value = value.Replace("\"", "");
                value = value.Replace("\n", "");
            }
            else
            {
                value = value.Replace("\"", "");
            }
            return value.Trim();
        }
    }
}
