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
    public static class DividendsCache
    {
        public static void LoadInitialDividendCache(string[] ids, string[] symbols, string[] exDividend, string[] annualDiv, string[] payDates, string[] eps, string[] divPercent,
            string[] stockname, string[] marketCap, string[] peRatio, string[] openPrice, string[] currentPrice, string[] fiftyTwoWeekLow, string[]fiftyTwoWeekHigh, string[] daysRange, DataTable dt)
        {
            //DataTable dt = uti.GetXMLData(FileTypes.cache);
            try
            {
                bool isPass = dt.Rows.Count == ids.Length && dt.Rows.Count == symbols.Length && dt.Rows.Count == exDividend.Length && dt.Rows.Count == annualDiv.Length && dt.Rows.Count == payDates.Length && dt.Rows.Count == eps.Length && dt.Rows.Count == divPercent.Length
                    && dt.Rows.Count == stockname.Length && dt.Rows.Count == marketCap.Length && dt.Rows.Count == peRatio.Length && dt.Rows.Count == openPrice.Length && dt.Rows.Count == currentPrice.Length &&
                    dt.Rows.Count == fiftyTwoWeekLow.Length && dt.Rows.Count == fiftyTwoWeekHigh.Length && dt.Rows.Count == daysRange.Length;
                if (isPass)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(uti.GetFilePath(FileTypes.cache));
                    int cnt = 0;
                    if (dt.Rows.Count == 0)
                    {
                        string strNamespace = doc.DocumentElement.NamespaceURI;
                        XmlNode node = doc.CreateNode(XmlNodeType.Element, string.Format("dividendstock"), strNamespace);
                        XmlAttribute idAttr = doc.CreateAttribute("ID");
                        idAttr.Value = ids[0];
                        node.Attributes.Append(idAttr);
                        XmlNode nodeID = doc.CreateElement(DivCacheCodes.id.ToString());
                        nodeID.InnerText = ids[0];
                        node.AppendChild(nodeID);

                        XmlNode nodeSymbol = doc.CreateElement(DivCacheCodes.symbol.ToString());
                        nodeSymbol.InnerText = symbols[0];
                        node.AppendChild(nodeSymbol);

                        XmlNode nodeExDiv = doc.CreateElement(DivCacheCodes.exDividend.ToString());
                        nodeExDiv.InnerText = exDividend[0];
                        node.AppendChild(nodeExDiv);

                        XmlNode nodeAnnualDiv = doc.CreateElement(DivCacheCodes.annualDiv.ToString());
                        nodeAnnualDiv.InnerText = annualDiv[0];
                        node.AppendChild(nodeAnnualDiv);

                        XmlNode nodePayDates = doc.CreateElement(DivCacheCodes.payDates.ToString());
                        nodePayDates.InnerText = payDates[0];
                        node.AppendChild(nodePayDates);

                        XmlNode nodeEps = doc.CreateElement(DivCacheCodes.eps.ToString());
                        nodeEps.InnerText = eps[0];
                        node.AppendChild(nodeEps);

                        XmlNode nodeDivPercent = doc.CreateElement(DivCacheCodes.divPercent.ToString());
                        nodeDivPercent.InnerText = divPercent[0];
                        node.AppendChild(nodeDivPercent);

                        XmlNode nodeStockName = doc.CreateElement(DivCacheCodes.stockname.ToString());
                        nodeStockName.InnerText = stockname[0];
                        node.AppendChild(nodeStockName);

                        XmlNode nodeMarketCap = doc.CreateElement(DivCacheCodes.marketCap.ToString());
                        nodeMarketCap.InnerText = marketCap[0];
                        node.AppendChild(nodeMarketCap);

                        XmlNode nodePeRatio = doc.CreateElement(DivCacheCodes.peRatio.ToString());
                        nodePeRatio.InnerText = peRatio[0];
                        node.AppendChild(nodePeRatio);

                        XmlNode nodeOpenPrice = doc.CreateElement(DivCacheCodes.openPrice.ToString());
                        nodeOpenPrice.InnerText = openPrice[0];
                        node.AppendChild(nodeOpenPrice);


                        XmlNode nodeCurrentPrice = doc.CreateElement(DivCacheCodes.currentPrice.ToString());
                        nodeCurrentPrice.InnerText = currentPrice[0];
                        node.AppendChild(nodeCurrentPrice);


                        XmlNode nodeFiftyTwoWeekLow = doc.CreateElement(DivCacheCodes.fiftyTwoWeekLow.ToString());
                        nodeFiftyTwoWeekLow.InnerText = fiftyTwoWeekLow[0];
                        node.AppendChild(nodeFiftyTwoWeekLow);

                        XmlNode nodeFiftyTwoWeekHigh = doc.CreateElement(DivCacheCodes.fiftyTwoWeekHigh.ToString());
                        nodeFiftyTwoWeekHigh.InnerText = fiftyTwoWeekHigh[0];
                        node.AppendChild(nodeFiftyTwoWeekHigh);

                        XmlNode nodeDaysRange = doc.CreateElement(DivCacheCodes.daysRange.ToString());
                        nodeDaysRange.InnerText = daysRange[0];
                        node.AppendChild(nodeDaysRange);

                        doc.DocumentElement["dividendstocks"].AppendChild(node);
                        doc.Save(uti.GetFilePath(FileTypes.cache));
                        dt = uti.GetXMLData(FileTypes.cache);
                    }
                    for (int a = 0; a < ids.Length; a++)
                    {
                        for (int b = 0; b < dt.Rows.Count; b++)
                        {
                            if (dt.Rows[b]["id"].ToString() == ids[a])
                            {
                                string id = dt.Rows[b]["id"].ToString();

                                if (dt.Rows[b]["symbol"].ToString() != symbols[a])
                                {
                                    UpdateDividendCache(id, "symbol", symbols[a]);
                                }
                                if (dt.Rows[b]["exDividend"].ToString() != exDividend[a])
                                {
                                    UpdateDividendCache(id, "exDividend", exDividend[a]);
                                }
                                if (dt.Rows[b]["annualDiv"].ToString() != annualDiv[a])
                                {
                                    UpdateDividendCache(id, "annualDiv", annualDiv[a]);
                                }
                                if (dt.Rows[b]["payDates"].ToString() != payDates[a])
                                {
                                    UpdateDividendCache(id, "payDates", payDates[a]);
                                }
                                if (dt.Rows[b]["eps"].ToString() != eps[a])
                                {
                                    UpdateDividendCache(id, "eps", eps[a]);
                                }

                                if (dt.Rows[b]["divPercent"].ToString() != divPercent[a])
                                {
                                    UpdateDividendCache(id, "divPercent", divPercent[a]);
                                }
                                if (dt.Rows[b]["stockname"].ToString() != stockname[a])
                                {
                                    UpdateDividendCache(id, "stockname", stockname[a]);
                                }
                                if (dt.Rows[b]["marketCap"].ToString() != marketCap[a])
                                {
                                    UpdateDividendCache(id, "marketCap", marketCap[a]);
                                }
                                if (dt.Rows[b]["peRatio"].ToString() != peRatio[a])
                                {
                                    UpdateDividendCache(id, "peRatio", peRatio[a]);
                                }

                                if (dt.Rows[b]["openPrice"].ToString() != openPrice[a])
                                {
                                    UpdateDividendCache(id, "openPrice", openPrice[a]);
                                }
                                if (dt.Rows[b]["currentPrice"].ToString() != currentPrice[a])
                                {
                                    UpdateDividendCache(id, "currentPrice", currentPrice[a]);
                                }
                                if (dt.Rows[b]["fiftyTwoWeekLow"].ToString() != fiftyTwoWeekLow[a])
                                {
                                    UpdateDividendCache(id, "fiftyTwoWeek", fiftyTwoWeekLow[a]);
                                }
                                if (dt.Rows[b]["fiftyTwoWeekHigh"].ToString() != fiftyTwoWeekHigh[a])
                                {
                                    UpdateDividendCache(id, "fiftyTwoWeek", fiftyTwoWeekHigh[a]);
                                }
                                if (dt.Rows[b]["daysRange"].ToString() != daysRange[a])
                                {
                                    UpdateDividendCache(id, "daysRange", daysRange[a]);
                                }
                            }
                            else
                            {
                                if (!symbolExist(symbols[cnt], dt))
                                {
                                    //int newID = uti.IncrementStockID(FileTypes.cache);
                                    string strNamespace = doc.DocumentElement.NamespaceURI;
                                    XmlNode node = doc.CreateNode(XmlNodeType.Element, string.Format("dividendstock"), strNamespace);
                                    XmlAttribute idAttr = doc.CreateAttribute("ID");
                                    idAttr.Value = ids[cnt];
                                    node.Attributes.Append(idAttr);
                                    XmlNode nodeID = doc.CreateElement("id");
                                    nodeID.InnerText = ids[cnt];
                                    node.AppendChild(nodeID);

                                    XmlNode nodeSymbol = doc.CreateElement("symbol");
                                    nodeSymbol.InnerText = symbols[cnt];
                                    node.AppendChild(nodeSymbol);

                                    XmlNode nodeExDiv = doc.CreateElement("exDividend");
                                    nodeExDiv.InnerText = exDividend[cnt];
                                    node.AppendChild(nodeExDiv);

                                    XmlNode nodeAnnualDiv = doc.CreateElement("annualDiv");
                                    nodeAnnualDiv.InnerText = annualDiv[cnt];
                                    node.AppendChild(nodeAnnualDiv);

                                    XmlNode nodePayDates = doc.CreateElement("payDates");
                                    nodePayDates.InnerText = payDates[cnt];
                                    node.AppendChild(nodePayDates);

                                    XmlNode nodeEps = doc.CreateElement("eps");
                                    nodeEps.InnerText = eps[cnt];
                                    node.AppendChild(nodeEps);

                                    XmlNode nodeDivPercent = doc.CreateElement("divPercent");
                                    nodeDivPercent.InnerText = divPercent[cnt];
                                    node.AppendChild(nodeDivPercent);

                                    XmlNode nodeStockName = doc.CreateElement("stockname");
                                    nodeStockName.InnerText = stockname[cnt];
                                    node.AppendChild(nodeStockName);

                                    XmlNode nodeMarketCap = doc.CreateElement("marketCap");
                                    nodeMarketCap.InnerText = marketCap[cnt];
                                    node.AppendChild(nodeMarketCap);

                                    XmlNode nodePeRatio = doc.CreateElement("peRatio");
                                    nodePeRatio.InnerText = peRatio[cnt];
                                    node.AppendChild(nodePeRatio);

                                    XmlNode nodeOpenPrice = doc.CreateElement("openPrice");
                                    nodeOpenPrice.InnerText = openPrice[cnt];
                                    node.AppendChild(nodeOpenPrice);

                                    XmlNode nodeCurrentPrice = doc.CreateElement("currentPrice");
                                    nodeCurrentPrice.InnerText = currentPrice[cnt];
                                    node.AppendChild(nodeCurrentPrice);

                                    XmlNode nodeFiftyTwoWeekLow = doc.CreateElement("fiftyTwoWeekLow");
                                    nodeFiftyTwoWeekLow.InnerText = fiftyTwoWeekLow[cnt];
                                    node.AppendChild(nodeFiftyTwoWeekLow);

                                    XmlNode nodeFiftyTwoWeekHigh = doc.CreateElement("fiftyTwoWeekHigh");
                                    nodeFiftyTwoWeekHigh.InnerText = fiftyTwoWeekHigh[cnt];
                                    node.AppendChild(nodeFiftyTwoWeekHigh);

                                    XmlNode nodeDaysRange = doc.CreateElement("daysRange");
                                    nodeDaysRange.InnerText = daysRange[cnt];
                                    node.AppendChild(nodeDaysRange);

                                    doc.DocumentElement["dividendstocks"].AppendChild(node);
                                    //newID++;
                                    doc.Save(uti.GetFilePath(FileTypes.cache));
                                    dt = uti.GetXMLData(FileTypes.cache);
                                }
                            }
                        }
                        cnt++;
                    }
                }
            }
            catch (Exception e)
            {
            }
        }

        public static void UpdateDividendCache(string id, string col, string val)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(uti.GetFilePath(FileTypes.cache));
                XmlNodeList elements = doc.SelectNodes(string.Format("//dividendstock[@ID='{0}']", id));
                elements[0][col].InnerText = val;
                doc.Save(uti.GetFilePath(FileTypes.cache));
            }
            catch (Exception e)
            {

            }
        }

        public static bool symbolExist(string toCompare, DataTable dt)
        {
            for (int a = 0; a < dt.Rows.Count; a++)
            {
                if (dt.Rows[a]["symbol"].ToString() == toCompare)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
