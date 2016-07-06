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
using System.Drawing;

namespace DividendLiberty
{
    public static class DividendStocks
    {
        public static int GetTotalSharePrice(List<int> lstID, out decimal totalPrice, out string[] msg)
        {
            DataTable dt = uti.GetXMLData();
            DataView dv = dt.DefaultView;
            dv.Sort = "symbol asc";
            dt = dv.ToTable();
            totalPrice = 0;
            int array = 0;
            msg = new string[9999];
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
                            msg[array++] = dt.Rows[i]["symbol"].ToString() + ": $" + Math.Round(((decimal)numShares * price), 2) + "\n\n";
                        }
                    }
                }
                msg[array] = "---------------------------\n\n";
            }
            catch (Exception e)
            {

            }
            return array;
        }

        public static int GetDividendPrice(ListView lv, List<int> lstID, out decimal totalDividendPrice, out decimal quarterlyDividendPrice, out decimal monthlyDividendPrice, out string[] msg)
        {
            msg = new string[9999];
            totalDividendPrice = 0;
            quarterlyDividendPrice = 0;
            monthlyDividendPrice = 0;
            decimal numShares = 0;
            decimal yield = 0;
            int divider = 0;
            int array = 0;
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
                                MessageBox.Show("Could not connect to Yahoo to complete the results!, please try again later.");
                                return 0;
                            }
                            yield = Convert.ToDecimal(yieldTemp == "" || yieldTemp == "N/A" ? 0 : Convert.ToDecimal(yieldTemp));
                            totalDividendPrice += (numShares * yield);
                            msg[array++] = lv.Items[i].SubItems[1].Text + ":\n" + "Monthly: $" + Math.Round((numShares * yield / 12), 2) + "\n" + "Quarterly: $" + Math.Round((numShares * yield / 4), 2) + "\n" + "Yearly: $" + Math.Round((numShares * yield), 2) + "\n\n";
                            //msg += lv.Items[i].SubItems[1].Text + ":\n";
                            //msg += "Monthly: $" + Math.Round((numShares * yield / 12), 2) + "\n";
                            //msg += "Quarterly: $" + Math.Round((numShares * yield / 4), 2) + "\n";
                            //msg += "Yearly: $" + Math.Round((numShares * yield), 2) + "\n\n";
                            if (i % 6 == 0)
                            {
                                divider++;
                            }
                        }
                    }
                    if (b == lstID.Count - 1)
                    {
                        msg[array] = "--------------------------------------\n\n";
                    }
                }
                quarterlyDividendPrice = totalDividendPrice / 4;
                monthlyDividendPrice = totalDividendPrice / 12;
            }
            catch (Exception e)
            {

            }
            return array;
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

        public static string GetHighlightSymbols(ListView lv)
        {
            string symbol = "";
            string monthYear = "";
            string dtpMonthYear = "";
            for (int i = 0; i < lv.Items.Count; i++)
            {
                string date = lv.Items[i].SubItems[7].Text;
                string[] dateSplit = date.Split('/');
                if (date != "N/A")
                {
                    monthYear = dateSplit[0].Trim() + "/" + dateSplit[2];
                    dtpMonthYear = Program.MainMenu.dtpPayDate.Value.ToString("M/yyyy");
                    if (monthYear == dtpMonthYear)
                    {
                        symbol += lv.Items[i].SubItems[1].Text.ToString() + ",";
                    }
                }
            }
            return symbol = symbol.Length == 0 ? "" : symbol.Substring(0, symbol.Length -1);
        }

        public static void HighlightPayDate(ListView lv)
        {
            PleaseWait pw = new PleaseWait();
            pw.Show();
            Application.DoEvents();
            decimal totalDiv = 0;
            decimal quarterlyDiv = 0;
            int cnt = 0;
            string monthYear = "";
            string dtpMonthYear = "";
            string individualDivData = "";
            decimal div = 0;
            //lv.SelectedItems.Clear();
            uti.ClearListViewColors(lv);
            Program.MainMenu.lstID.Clear();
            string stockSymbols = GetHighlightSymbols(lv);
            string[] AnnualDiv = uti.SplitStockData(YahooFinance.GetValues(stockSymbols, YahooFinance.GetCodes(YahooCodes.annualDividend), true));
            int annDivCnt = 0;
            for (int i = 0; i < lv.Items.Count; i++)
            {
                string date = lv.Items[i].SubItems[7].Text;
                string[] dateSplit = date.Split('/');
                if (date != "N/A")
                {
                    monthYear = dateSplit[0].Trim() + "/" + dateSplit[2];
                    dtpMonthYear = Program.MainMenu.dtpPayDate.Value.ToString("M/yyyy");
                    string dividendInterval = lv.Items[i].SubItems[8].Text.ToString();
                    //if (dividendInterval == "Monthly")
                    //{
                    //    lv.SelectedIndices.Add(i);
                    //    totalDiv += 
                    //    individualDivData += symbol + ": $" + Math.Round((GetDiv(Convert.ToInt32(drv["id"]), dt) / 4), 2) + "\n\n";
                    //}
                    if (monthYear == dtpMonthYear)
                    {
                        lv.Items[i].BackColor = uti.GetHighlightColor();
                        lv.Items[i].Selected = true;
                        lv.Items[i].Focused = true;
                        lv.TopItem = lv.Items[i];
                        string symbol = lv.Items[i].SubItems[1].Text.ToString();
                        try
                        {
                            div = Convert.ToDecimal(AnnualDiv[annDivCnt++]);
                        }
                        catch
                        {
                            pw.Close();
                            MessageBox.Show("Could not highlight, yahoo connection was lost. Please try again later.");
                            return;
                        }
                        decimal divReceived = uti.GetDivPrice(Convert.ToDecimal(lv.Items[i].SubItems[4].Text.ToString()), div);
                        totalDiv += divReceived;
                        individualDivData += symbol + ": $" + Math.Round(divReceived / 4, 2) + " (Pay Date: " + lv.Items[i].SubItems[7].Text.ToString() + ")\n\n";
                        Program.MainMenu.lstID.Add(Convert.ToInt32(lv.Items[i].Tag));
                        cnt++;
                    }
                }
            }
            lv.SelectedItems.Clear();
            quarterlyDiv = totalDiv / 4;
            pw.Close();
            if (cnt != 0)
            {
                MessageBox.Show(string.Format("{0} results\n\n" + "{1} \n\n" + individualDivData + "Total: ${2}\n\n", cnt, "Dividends for " + dtpMonthYear + "", Math.Round(quarterlyDiv, 2)));
            }
            else
            {
                MessageBox.Show(string.Format("No Results for {0}", dtpMonthYear));
            }
        }

        public static void ShowIndustryPercentages(ListView lv)
        {
            decimal portfolioCnt = Convert.ToDecimal(lv.Items.Count);
            decimal percentage = 0;
            List<string> lstIndustries = new List<string>() {"Consumer Discretionary", "Consumer Staples", "Energy", "Financials", "Health Care", "Industrials", "Information Technology", 
                                                            "Materials", "Telecommunication Services", "Utilities", "Equity Precious Metals" };
            List<decimal> percentages = new List<decimal>();
            List<decimal> sectorCount = new List<decimal>();
            decimal cnt = 0;
            for (int i = 0; i < lstIndustries.Count; i++)
            {
                for (int a = 0; a < lv.Items.Count; a++)
                {
                    if (lv.Items[a].SubItems[3].Text == lstIndustries[i].ToString())
                    {
                        cnt++;
                    }
                }
                percentage = cnt == 0 ? 0 : (cnt / portfolioCnt) * 100;
                percentages.Add(percentage);
                sectorCount.Add(cnt);
                cnt = 0;
            }
            decimal totalSectors = uti.GetTotalSectorCount(sectorCount);
            string msg = "";
            for (int i = 0; i < lstIndustries.Count; i++)
            {
                msg += sectorCount[i] + " - " + lstIndustries[i] + ": " + Math.Round(percentages[i], 2) + "%" + "\n\n";
            }
            msg = msg.Substring(0, msg.Length - 2);
            msg += "\n\nTotal Sectors: " + totalSectors;
            MessageBox.Show(msg);
        }

        public static void HighlightAllNextToBuy(ListView lv)
        {
            int cnt = 0;
            try
            {
                lv.SelectedItems.Clear();
                DataTable dt = uti.GetXMLData();
                uti.ClearListViewColors(lv);
                for (int i = 0; i < lv.Items.Count; i++)
                {
                    for (int a = 0; a < dt.Rows.Count; a++)
                    {
                        if (dt.Rows[a]["nexttobuy"].ToString() == "yes" && dt.Rows[a]["active"].ToString().Trim() == "false")
                        {
                            if (Convert.ToInt32(lv.Items[i].Tag) == Convert.ToInt32(dt.Rows[a]["id"]))
                            {
                                lv.Items[i].BackColor = uti.GetHighlightColor();
                                lv.Items[i].Selected = true;
                                lv.Items[i].Focused = true;
                                lv.TopItem = lv.Items[i];
                                Program.MainMenu.lstID.Add(Convert.ToInt32(lv.Items[i].Tag));
                                cnt++;
                            }
                        }
                    }
                }
                lv.SelectedItems.Clear();
            }
            catch
            {

            }
            MessageBox.Show(string.Format("{0} results.", cnt));
        }

        public static void Highlight(ListView lv, ComboBox ddl, bool showMsg)
        {
            decimal count = 0;
            decimal percentage = Convert.ToDecimal(lv.Items.Count);
            lv.SelectedItems.Clear();
            uti.ClearListViewColors(Program.MainMenu.lvAllDividends);
            Program.MainMenu.lstID.Clear();
            for (int i = 0; i < lv.Items.Count; i++)
            {
                if (lv.Items[i].SubItems[3].Text == ddl.Text)
                {
                    count++;
                    Program.MainMenu.lstID.Add(Convert.ToInt32(lv.Items[i].Tag));
                    lv.Items[i].BackColor = uti.GetHighlightColor();
                    lv.Items[i].Selected = true;
                    lv.Items[i].Focused = true;
                    lv.TopItem = lv.Items[i];
                }
                else
                {
                    lv.Items[i].BackColor = Color.White;
                }
            }
            lv.SelectedItems.Clear();
            percentage = (count / percentage) * 100;
            if (showMsg)
            {
                MessageBox.Show(count + " " + ddl.Text + ": " + Math.Round(percentage, 2) + "%");
            }
        }

        public static void SearchSymbol(TextBox tb, ListView lv)
        {
            lv.SelectedItems.Clear();
            uti.ClearListViewColors(lv);
            Program.MainMenu.lstID.Clear();
            for (int i = 0; i < lv.Items.Count; i++)
            {
                if (lv.Items[i].SubItems[1].Text == tb.Text.ToUpper())
                {
                    Program.MainMenu.lstID.Add(Convert.ToInt32(lv.Items[i].Tag));
                    lv.Items[i].BackColor = uti.GetHighlightColor();
                    lv.Items[i].Selected = true;
                    lv.Items[i].Focused = true;
                    lv.TopItem = lv.Items[i];
                    //lv.Select();
                }
            }
        }

        public static void CalculateResults()
        {
            PleaseWait pw = new PleaseWait();
            pw.Show();
            Application.DoEvents();
            uti.ClearListViewColors(Program.MainMenu.lvAllDividends);
            uti.ClearListViewColors(Program.MainMenu.lvCurrentDividends);
            Program.MainMenu.lstID.Clear();
            decimal TotalDividendCount = 0;
            decimal TotalDividendStockValue = 0;
            decimal YearDiv = 0;
            decimal QuarterDiv = 0;
            decimal MonthlyDiv = 0;
            decimal DividendTotalPercentage = 0;
            //decimal MarketTotalPrice = 0;
            DataTable dt = uti.GetXMLData();
            decimal Purchaseprice = 0;
            string symbols = uti.GetStockSymbols(dt);
            string[] AnnualDiv = uti.SplitStockData(YahooFinance.GetValues(symbols, YahooFinance.GetCodes(YahooCodes.annualDividend), true));
            string[] DivYield = uti.SplitStockData(YahooFinance.GetValues(symbols, YahooFinance.GetCodes(YahooCodes.dividendYield), true));
            // l1 is last trade price, c1 change in price, b is bid price, a is ask price; Ask price is current price as you're asking for a price when selling therefore that is the price of your portfolio
            string[] CurrentStockPrice = uti.SplitStockData(YahooFinance.GetValues(symbols, YahooFinance.GetCodes(YahooCodes.currentPrice), true));
            //decimal val = dt.Rows.Count;
            //decimal StatusVal = 0;
            //val = Math.Round(90 / val, 0);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["active"].ToString() == "true")
                {
                    string id = dt.Rows[i]["id"].ToString();
                    Purchaseprice = Convert.ToDecimal(dt.Rows[i]["cost"]);
                    YearDiv += AnnualDiv[i] == "N/A" ? 0 : AnnualDiv[i] == "" ? 0 : (Convert.ToDecimal(dt.Rows[i]["shares"]) * Convert.ToDecimal(AnnualDiv[i]));
                    TotalDividendStockValue += (Convert.ToDecimal(dt.Rows[i]["shares"]) * Purchaseprice);
                    TotalDividendCount++;
                    DividendTotalPercentage += DivYield[i] == "N/A" ? 0 : DivYield[i].ToString() == "" ? 0 : Convert.ToDecimal(DivYield[i]);
                    if (dt.Rows[i]["symbol"].ToString() == "SIL")
                    {
                        DividendTotalPercentage += (decimal).09;
                    }
                    //if (dt.Rows[i]["symbol"].ToString() == "GDX")
                    //{
                    //    DividendTotalPercentage -= Convert.ToDecimal(DivYield[i]);
                    //    DividendTotalPercentage += (decimal).80;
                    //}
                    //MarketTotalPrice += (Convert.ToDecimal(dt.Rows[i]["shares"]) * (CurrentStockPrice[i].ToString() == "N/A" ? 0 : Convert.ToDecimal(CurrentStockPrice[i])));
                    //StatusVal += val;
                    //if (StatusVal < 88)
                    //    pbStatus.InvokeEx(x => x.Value = Convert.ToInt32(StatusVal));
                }
            }
            DividendTotalPercentage = DividendTotalPercentage / TotalDividendCount;
            QuarterDiv = (YearDiv / 4);
            MonthlyDiv = (YearDiv / 12);
            //pbStatus.InvokeEx(x => x.Value = 100);
            //pbStatus.InvokeEx(x => x.Visible = false);
            //lblStatus.InvokeEx(x => x.Visible = false);
            pw.Close();
            MessageBox.Show("Cost Basis: $" + Math.Round(TotalDividendStockValue, 2) + "\n\nAnnual Dividend: $" + Math.Round(YearDiv, 2) + "\n\n" + "Quarterly Dividend: $" + Math.Round(QuarterDiv, 2) + "\n\nMonthly Dividend: $" + Math.Round(MonthlyDiv, 2) + "\n\nPortfolio Dividend Yield: " + Math.Round(DividendTotalPercentage, 2) + "%");
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
