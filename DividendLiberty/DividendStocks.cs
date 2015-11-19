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
        public static void GetTotalSharePrice(List<int> lstID, out decimal totalPrice)
        {
            DataTable dt = uti.GetXMLData();
            totalPrice = 0;
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int b = 0; b < lstID.Count; b++)
                    {
                        if (Convert.ToInt32(dt.Rows[i]["id"]) == lstID[b])
                        {
                            decimal numShares = Convert.ToDecimal(dt.Rows[i]["shares"]);
                            decimal price = Convert.ToDecimal(dt.Rows[i]["cost"]);
                            totalPrice += ((decimal)numShares * price);
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public static void GetDividendPrice(ListView lv, List<int> lstID, out decimal totalDividendPrice, out decimal quarterlyDividendPrice, out decimal monthlyDividendPrice)
        {
            //DataTable dt = uti.GetXMLData();
            totalDividendPrice = 0;
            quarterlyDividendPrice = 0;
            monthlyDividendPrice = 0;
            decimal numShares = 0;
            decimal yield = 0;
            try
            {
                for (int b = 0; b < lstID.Count; b++)
                {
                    for (int i = 0; i < lv.Items.Count; i++)
                    {
                        if (Convert.ToInt32(lv.Items[i].Tag) == lstID[b])
                        {
                            string yieldTemp = "";
                            numShares = lv.Items[i].SubItems[4].Text == "" ? 0 : Convert.ToDecimal(lv.Items[i].SubItems[4].Text);
                            try
                            {
                                yieldTemp = YahooFinance.GetValues(lv.Items[i].SubItems[1].Text, "d", false);
                            }
                            catch
                            {
                                MessageBox.Show("Could not connect to Yahoo!, please try again later.");
                                return;
                            }
                            yield = Convert.ToDecimal(yieldTemp == "" ? "0" : yieldTemp);
                            totalDividendPrice += (numShares * yield);
                        }
                    }
                }
                quarterlyDividendPrice = totalDividendPrice / 4;
                monthlyDividendPrice = totalDividendPrice / 12;
            }
            catch (Exception e)
            {

            }
        }

        public static void LoadDividends(ListView lv, string[] names, string[] exDiv, string[] payDate, string active, DataTable dtXml)
        {
            try
            {
                lv.Clear();
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
                        lvItem.SubItems.Add(names.Length == 1 ? "" : names[i]);
                        lvItem.SubItems.Add(dtXml.Rows[i]["industry"].ToString());
                        lvItem.SubItems.Add(dtXml.Rows[i]["shares"].ToString());
                        lvItem.SubItems.Add(dtXml.Rows[i]["cost"].ToString());
                        lvItem.SubItems.Add(exDiv.Length == 1 ? "" : exDiv[i]);
                        lvItem.SubItems.Add(payDate.Length == 1 ? "" : payDate[i]);
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

        public static string NewDividendStock(string symbol, string industry, string interval)
        {
            string newID = uti.IncrementStockID();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(uti.GetFilePath(FileTypes.xml));
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
                nodePurchaseDate.InnerText = "0";
                node.AppendChild(nodePurchaseDate);

                XmlNode nodeShares = doc.CreateElement("shares");
                nodeShares.InnerText = "0";
                node.AppendChild(nodeShares);

                XmlNode nodeNextToBuy = doc.CreateElement("nexttobuy");
                nodeNextToBuy.InnerText = "no";
                node.AppendChild(nodeNextToBuy);

                doc.DocumentElement["dividendstocks"].AppendChild(node);
                doc.Save(uti.GetFilePath(FileTypes.xml));
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
                XmlDocument doc = new XmlDocument();
                doc.Load(uti.GetFilePath(FileTypes.xml));
                XmlNodeList elements = doc.SelectNodes(string.Format("//dividendstock[@ID='{0}']", id));
                if (elements[0]["symbol"].InnerText.ToString() == origSymbol)
                {
                    elements[0]["active"].InnerText = active;
                    doc.Save(uti.GetFilePath(FileTypes.xml));
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
                XmlDocument doc = new XmlDocument();
                doc.Load(uti.GetFilePath(FileTypes.xml));
                XmlNodeList elements = doc.SelectNodes(string.Format("//dividendstock[@ID='{0}']", id));
                if (elements[0]["symbol"].InnerText.ToString() == origSymbol)
                {
                    elements[0]["symbol"].InnerText = symbol.ToUpper();
                    elements[0]["industry"].InnerText = industry;
                    elements[0]["interval"].InnerText = interval;
                    doc.Save(uti.GetFilePath(FileTypes.xml));
                }
            }
            catch (Exception e)
            {

            }
        }

        public static void UpdateShare(string id, string origSymbol, string cost, string shares, string purchasedate)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(uti.GetFilePath(FileTypes.xml));
                XmlNodeList elements = doc.SelectNodes(string.Format("//dividendstock[@ID='{0}']", id));
                if (elements[0]["symbol"].InnerText.ToString() == origSymbol)
                {
                    elements[0]["cost"].InnerText = cost;
                    elements[0]["shares"].InnerText = shares;
                    elements[0]["purchasedate"].InnerText = purchasedate;
                    doc.Save(uti.GetFilePath(FileTypes.xml));
                }
            }
            catch (Exception e)
            {

            }
        }

        public static string LoadNextPurchase(string symbol)
        {
            string nextToBuy = "";
            DataTable dt = uti.GetXMLData();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["symbol"].ToString() == symbol)
                {
                    nextToBuy = dt.Rows[i]["nexttobuy"].ToString();
                }
            }
            return nextToBuy;
        }

        public static void SaveNextPurchase(int id, string nexttobuy, string symbol)
        {
            try
            {
                string filePath = uti.GetFilePath(FileTypes.xml);
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                XmlNodeList elements = doc.SelectNodes(string.Format("//dividendstock[@ID='{0}']", id));
                if (elements[0]["symbol"].InnerText.ToString() == symbol)
                {
                    elements[0]["nexttobuy"].InnerText = nexttobuy;
                    doc.Save(filePath);
                }
            }
            catch
            {

            }
        }
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
