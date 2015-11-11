using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Xml;
using System.Xml.Linq;

namespace DividendLiberty
{
    public static class DividendStocks
    {
        public static DataTable GetCurrentDividends()
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT ds.id, dp.numberofshares, dp.purchaseprice, ds.symbol, ds.dividendinterval FROM dividendstocks ds JOIN dividendprice dp ON ds.id=dp.dividendstockid WHERE ds.stockactive='true' order by ds.Symbol";
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception e)
            {

            }
            return dt;
        }

        public static DataTable GetDividend(string id)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT ds.symbol, ds.stockname, ds.industry, dp.numberofshares, ds.stockactive, dp.purchaseprice, ds.dividendinterval FROM dividendstocks ds join dividendprice dp on ds.id = dp.dividendstockid WHERE ds.id=@id";
                        cmd.Parameters.AddWithValue("id", id);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }

                    // if shares data doesn't exist (not bought yet)
                    if (dt.Rows.Count == 0)
                    {
                        using (var cmd = cnn.CreateCommand())
                        {
                            cmd.CommandText = "SELECT ds.symbol, ds.stockname, ds.industry, ds.stockactive, ds.dividendinterval FROM dividendstocks ds WHERE ds.id=@id";
                            cmd.Parameters.AddWithValue("id", id);
                            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
            return dt;
        }

        public static void GetTotalSharePrice(string dividendstockid, out decimal totalPrice)
        {
            DataTable dt = new DataTable();
            //decimal transactionPrice = (decimal)9.99;
            totalPrice = 0;
            int numShares = 0;
            decimal price = 0;
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT purchaseprice, numberofshares FROM dividendprice WHERE dividendstockid=@dividendstockid";
                        cmd.Parameters.AddWithValue("dividendstockid", dividendstockid);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        numShares = Convert.ToInt32(dt.Rows[i]["numberofshares"]);
                        price = Convert.ToDecimal(dt.Rows[i]["purchaseprice"]);
                        totalPrice += ((decimal)numShares * price);
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public static void GetDividendPrice(string symbol, string dividendstockid, out decimal totalDividendPrice, out decimal quarterlyDividendPrice, out decimal monthlyDividendPrice)
        {
            DataTable dt = new DataTable();
            totalDividendPrice = 0;
            quarterlyDividendPrice = 0;
            monthlyDividendPrice = 0;
            int numShares = 0;
            decimal yield = 0;
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT purchaseprice, numberofshares FROM dividendprice WHERE dividendstockid=@dividendstockid";
                        cmd.Parameters.AddWithValue("dividendstockid", dividendstockid);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        numShares = Convert.ToInt32(dt.Rows[i]["numberofshares"]);
                        yield = Convert.ToDecimal(YahooFinance.GetValues(symbol, "d", false));
                        totalDividendPrice += ((decimal)numShares * yield);
                    }
                }
                quarterlyDividendPrice = totalDividendPrice / 4;
                monthlyDividendPrice = totalDividendPrice / 12;
            }
            catch (Exception e)
            {

            }
        }

        public static DataTable GetSharePriceInfo(string id)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT purchaseprice, numberofshares, purchasedate FROM dividendprice WHERE id=@id";
                        cmd.Parameters.AddWithValue("id", id);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception e)
            {

            }
            return dt;
        }

        public static DataTable GetDividendActionDate(string id)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT dp.id, dp.purchasedate FROM dividendstocks ds join dividendprice dp on ds.id = dp.dividendstockid WHERE ds.id=@id ORDER BY purchasedate";
                        cmd.Parameters.AddWithValue("id", id);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception e)
            {

            }
            return dt;
        }

        public static DataTable GetPurchasePrice(string id)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT purchaseprice, numberofshares FROM dividendprice WHERE id=@id";
                        cmd.Parameters.AddWithValue("id", id);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception e)
            {

            }
            return dt;
        }

        public static void LoadDividends(ListView lv, string active)
        {
            try
            {
                lv.Clear();
                DataTable dt = uti.GetXMLData();
                DataView view = dt.DefaultView;
                view.Sort = "symbol asc";
                DataTable dtXml = view.ToTable();
                lv.View = View.Details;
                lv.Columns.Add("");
                lv.Columns.Add("Symbol");
                lv.Columns.Add("Name");
                lv.Columns.Add("Industry");
                lv.Columns.Add("Shares");
                lv.Columns.Add("Cost");
                lv.Columns.Add("Ex-Dividend");
                lv.Columns.Add("Pay Date");
                lv.Columns.Add("Pay Interval");
                lv.Columns.Add("");
                int count = 1;
                for (int i = 0; i < dtXml.Rows.Count; i++)
                {
                    if (dtXml.Rows[i]["active"].ToString() == active)
                    {
                        string symbol = dtXml.Rows[i]["symbol"].ToString();
                        ListViewItem lvItem = new ListViewItem(count++.ToString());
                        lvItem.SubItems.Add(symbol);
                        lvItem.Tag = dtXml.Rows[i]["id"].ToString();
                        lvItem.SubItems.Add(YahooFinance.GetValues(symbol, "n", false));
                        lvItem.SubItems.Add(dtXml.Rows[i]["industry"].ToString());
                        lvItem.SubItems.Add(dtXml.Rows[i]["shares"].ToString());
                        lvItem.SubItems.Add(dtXml.Rows[i]["cost"].ToString());
                        lvItem.SubItems.Add(YahooFinance.GetValues(symbol, "q", false));
                        lvItem.SubItems.Add(YahooFinance.GetValues(symbol, "r1", false));
                        lvItem.SubItems.Add(dtXml.Rows[i]["interval"].ToString());
                        lv.Items.Add(lvItem);
                    }
                    else
                    {

                    }
                }

                for (int i = 0; i < 7; i++)
                {
                    lv.Columns[i].TextAlign = HorizontalAlignment.Center;
                }
                lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            catch (Exception e)
            {

            }
        }

        public static DataTable GetCurrentStocksData(MySqlConnection cnn, string stockactive)
        {
            using (var cmd = cnn.CreateCommand())
            {
                cmd.CommandText = "SELECT ds.id, ds.symbol, ds.stockname, ds.industry FROM dividendstocks ds WHERE ds.stockactive=@stockactive order by id";
                cmd.Parameters.AddWithValue("stockactive", stockactive);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static DataTable BuildDividendTable(DataTable dt)
        {
            DataTable dtFinal = new DataTable();
            dtFinal.Columns.Add("id", typeof(int));
            dtFinal.Columns.Add("symbol", typeof(string));
            dtFinal.Columns.Add("stockname", typeof(string));
            dtFinal.Columns.Add("industry", typeof(string));
            dtFinal.Columns.Add("numberofshares", typeof(int));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dtFinal.NewRow();
                dr["id"] = Convert.ToInt32(dt.Rows[i]["id"]);
                dr["symbol"] = dt.Rows[i]["symbol"].ToString();
                dr["stockname"] = dt.Rows[i]["stockname"].ToString();
                dr["industry"] = dt.Rows[i]["industry"].ToString();
                dtFinal.Rows.Add(dr);
            }
            return dtFinal;
        }

        public static DataTable AddPrice(DataTable dt, MySqlConnection cnn)
        {
            DataTable dtTemp = new DataTable();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                using (var cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "SELECT purchaseprice, numberofshares FROM dividendprice WHERE dividendstockid=@dividendstockid order by dividendstockid";
                    cmd.Parameters.AddWithValue("dividendstockid", dt.Rows[i]["id"].ToString());
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dtTemp);
                }
                int numShares = 0;
                for (int a = 0; a < dtTemp.Rows.Count; a++)
                {
                    numShares += Convert.ToInt32(dtTemp.Rows[a]["numberofshares"]);
                }

                dt.Rows[i]["numberofshares"] = numShares;
                dtTemp.Clear();
            }
            return dt;
        }


        //public static void UpdateDividendStock(string id, string stockactive)
        //{
        //    try
        //    {
        //        using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
        //        {
        //            cnn.Open();
        //            using (var cmd = cnn.CreateCommand())
        //            {
        //                cmd.CommandText = "UPDATE dividendstocks SET stockactive=@stockactive WHERE id=@id";
        //                cmd.Parameters.AddWithValue("id", id);
        //                cmd.Parameters.AddWithValue("stockactive", stockactive);
        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //}

        public static string NewDividendStock(string symbol, string industry, string interval)
        {
            string newID = uti.IncrementStockID();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(uti.GetXMLPath());
                string strNamespace = doc.DocumentElement.NamespaceURI;
                XmlNode node = doc.CreateNode(XmlNodeType.Element, string.Format("dividendstock"), strNamespace);
                XmlAttribute idAttr = doc.CreateAttribute("ID");
                idAttr.Value = newID;
                node.Attributes.Append(idAttr);
                XmlNode nodeID = doc.CreateElement("id");
                nodeID.InnerText = newID;
                node.AppendChild(nodeID);
                XmlNode nodeSymbol = doc.CreateElement("symbol");
                nodeSymbol.InnerText = symbol;
                node.AppendChild(nodeSymbol);

                XmlNode nodeIndustry = doc.CreateElement("industry");
                nodeIndustry.InnerText = industry;
                node.AppendChild(nodeIndustry);

                XmlNode nodeActive = doc.CreateElement("active");
                nodeActive.InnerText = "false";
                node.AppendChild(nodeActive);

                XmlNode nodeInterval = doc.CreateElement("interval");
                nodeInterval.InnerText = interval;
                node.AppendChild(nodeInterval);

                XmlNode nodeCost = doc.CreateElement("cost");
                nodeCost.InnerText = "";
                node.AppendChild(nodeCost);

                XmlNode nodePurchaseDate = doc.CreateElement("purchasedate");
                nodePurchaseDate.InnerText = "";
                node.AppendChild(nodePurchaseDate);

                XmlNode nodeShares = doc.CreateElement("shares");
                nodeShares.InnerText = "";
                node.AppendChild(nodeShares);

                doc.DocumentElement["dividendstocks"].AppendChild(node);
                doc.Save(uti.GetXMLPath());
            }
            catch (Exception e)
            {

            }
            return newID;
        }

        public static void MoveStock(string id, string origSymbol, string active)
        {
            try
            {
                DataTable dt = uti.GetXMLData();
                XmlDocument doc = new XmlDocument();
                doc.Load(uti.GetXMLPath());
                XmlNodeList elements = doc.SelectNodes(string.Format("//dividendstock[@ID='{0}']", id));
                if (elements[0]["symbol"].InnerText.ToString() == origSymbol)
                {
                    elements[0]["active"].InnerText = active;
                    doc.Save(uti.GetXMLPath());
                }
            }
            catch (Exception e)
            {

            }
        }

        public static void UpdateDividendStock(string id, string origSymbol, string symbol, string industry, string interval)
        {
            try
            {
                DataTable dt = uti.GetXMLData();
                XmlDocument doc = new XmlDocument();
                doc.Load(uti.GetXMLPath());
                XmlNodeList elements = doc.SelectNodes(string.Format("//dividendstock[@ID='{0}']", id));
                if (elements[0]["symbol"].InnerText.ToString() == origSymbol)
                {
                    elements[0]["symbol"].InnerText = symbol.ToUpper();
                    elements[0]["industry"].InnerText = industry;
                    elements[0]["interval"].InnerText = interval;
                    doc.Save(uti.GetXMLPath());
                }
            }
            catch (Exception e)
            {

            }
        }

        public static void NewShare(decimal purchaseprice, decimal numberofshares, string dividendstockid, DateTime purchasedate)
        {
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO dividendprice (purchaseprice, numberofshares, dividendstockid, purchasedate) VALUES (@purchaseprice, @numberofshares, @dividendstockid, @purchasedate)";
                        cmd.Parameters.AddWithValue("purchaseprice", purchaseprice);
                        cmd.Parameters.AddWithValue("numberofshares", numberofshares);
                        cmd.Parameters.AddWithValue("dividendstockid", dividendstockid);
                        cmd.Parameters.AddWithValue("purchasedate", purchasedate);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public static void UpdateShare(decimal purchaseprice, decimal numberofshares, string id, DateTime purchasedate)
        {
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE dividendprice SET purchaseprice=@purchaseprice, numberofshares=@numberofshares, purchasedate=@purchasedate WHERE id=@id";
                        cmd.Parameters.AddWithValue("purchaseprice", purchaseprice);
                        cmd.Parameters.AddWithValue("numberofshares", numberofshares);
                        cmd.Parameters.AddWithValue("purchasedate", purchasedate);
                        cmd.Parameters.AddWithValue("id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public static void DeleteShare(string id)
        {
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "DELETE FROM dividendprice WHERE id=@id";
                        cmd.Parameters.AddWithValue("id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public static int LoadNextPurchase(int id)
        {
            int nextPurchase = 0;
            using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
            {
                cnn.Open();
                using (var cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "SELECT nextpurchase FROM dividendstocks WHERE id=@id order by symbol";
                    cmd.Parameters.AddWithValue("id", id);
                    try
                    {
                        using (var rdr = cmd.ExecuteReader())
                        {
                            rdr.Read();
                            nextPurchase = Convert.ToInt32(rdr["nextpurchase"]);
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            return nextPurchase;
        }

        public static void SaveNextPurchase(int id, int nextPurchase)
        {
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE dividendstocks SET nextpurchase=@nextpurchase WHERE id=@id";
                        cmd.Parameters.AddWithValue("id", id);
                        cmd.Parameters.AddWithValue("nextpurchase", nextPurchase);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {

            }
        }

        public static DataTable GetAllNextToBuy(int id)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM dividendstocks WHERE nextpurchase='1'";
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception e)
            {

            }
            return dt;
        }

        //public static void Get(string id)
        //{
        //    try
        //    {
        //        using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ToString()))
        //        {
        //            cnn.Open();
        //            using (var cmd = cnn.CreateCommand())
        //            {
        //                cmd.CommandText = "DELETE FROM dividendprice WHERE id=@id";
        //                cmd.Parameters.AddWithValue("id", id);
        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //}
    }

    public struct StockInfo
    {
        public string ID { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Industry { get; set; }
        public string Shares { get; set; }
        public string Cost { get; set; }
        public string ExDiv { get; set; }
        public string PayDate { get; set; }
        public string Interval { get; set; }
    }
}
