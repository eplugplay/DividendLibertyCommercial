using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Net;

namespace DividendLiberty
{
    public partial class MainMenu : Form
    {
        public static Dividends _Dividends;
        public bool CurrentDiv { get; set; }
        public int ID { get; set; }
        public int SelectedIndex { get; set; }
        public List<int> lstID = new List<int>();
        public string Symbol { get; set; }
        public MainMenu()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDividends(false, false);
        }

        public void OpenDividends(bool edit, bool currentDiv)
        {
            List<StockInfo> lstStockInfo = new List<StockInfo>(); ;
            PleaseWait pw = new PleaseWait();
            pw.Show();
            Application.DoEvents();
            if (_Dividends == null || _Dividends.IsDisposed)
            {
                if (edit)
                {
                    if (currentDiv)
                    {
                        if (uti.DividendStatsValid(lstID))
                        {
                            lstStockInfo = GetStockInfoList(lvCurrentDividends);
                        }
                        else
                        {
                            pw.Close();
                            return;
                        }
                    }
                    else
                    {
                        if (uti.DividendStatsValid(lstID))
                        {
                            lstStockInfo = GetStockInfoList(lvAllDividends);
                        }
                        else
                        {
                            pw.Close();
                            return;
                        }
                    }
                    _Dividends = new Dividends(edit, currentDiv, lstStockInfo);
                }
                else
                {
                    _Dividends = new Dividends(edit, currentDiv);
                }
                _Dividends.Show();
            }
            else
            {
                if (_Dividends.WindowState == FormWindowState.Minimized)
                {
                    _Dividends.WindowState = FormWindowState.Normal;
                }
                else
                {
                    _Dividends.BringToFront();
                }
            }
            pw.Close();
        }

        #region backgroundworkers
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //pbStatus.InvokeEx(x => x.Value = 10);
            //pbStatus.InvokeEx(x => x.Visible = true);
            //lblStatus.InvokeEx(x => x.Visible = true);
            CalculateResults();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
        #endregion

        public void CalculateResults()
        {
            PleaseWait pw = new PleaseWait();
            pw.Show();
            Application.DoEvents();
            uti.ClearListViewColors(lvAllDividends);
            uti.ClearListViewColors(lvCurrentDividends);
            lstID.Clear();
            decimal TotalDividendCount = 0;
            decimal TotalDividendStockValue = 0;
            decimal YearDiv = 0;
            decimal QuarterDiv = 0;
            decimal MonthlyDiv = 0;
            decimal DividendTotalPercentage = 0;
            decimal MarketTotalPrice = 0;
            DataTable dt = uti.GetXMLData();
            decimal Purchaseprice = 0;
            string symbols = uti.GetStockSymbols(dt);
            string[] AnnualDiv = uti.SplitStockData(YahooFinance.GetValues(symbols, "d", true));
            string[] DivYield = uti.SplitStockData(YahooFinance.GetValues(symbols, "y", true));
            // l1 is last trade price, c1 change in price, b is bid price, a is ask price; Ask price is current price as you're asking for a price when selling therefore that is the price of your portfolio
            string[] CurrentStockPrice = uti.SplitStockData(YahooFinance.GetValues(symbols, "a", true));
            //decimal val = dt.Rows.Count;
            //decimal StatusVal = 0;
            //val = Math.Round(90 / val, 0);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["active"].ToString() == "true")
                {
                    string id = dt.Rows[i]["id"].ToString();
                    Purchaseprice = Convert.ToDecimal(dt.Rows[i]["cost"]);
                    YearDiv += (Convert.ToDecimal(dt.Rows[i]["shares"]) * Convert.ToDecimal(AnnualDiv[i]));
                    TotalDividendStockValue += (Convert.ToDecimal(dt.Rows[i]["shares"]) * Purchaseprice);
                    TotalDividendCount++;
                    DividendTotalPercentage += DivYield[i] == "N/A" ? 0 : Convert.ToDecimal(DivYield[i]);
                    if (dt.Rows[i]["symbol"].ToString() == "SIL")
                    {
                        DividendTotalPercentage += (decimal).09;
                    }
                    //if (dt.Rows[i]["symbol"].ToString() == "GDX")
                    //{
                    //    DividendTotalPercentage -= Convert.ToDecimal(DivYield[i]);
                    //    DividendTotalPercentage += (decimal).80;
                    //}
                    MarketTotalPrice += (Convert.ToDecimal(dt.Rows[i]["shares"]) * Convert.ToDecimal(CurrentStockPrice[i]));
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
            MessageBox.Show("Cost Basis: $" + Math.Round(TotalDividendStockValue, 2) + "\n\nMarket Value: $" + Math.Round(MarketTotalPrice, 2) + "\n\nAnnual Dividend: $" + Math.Round(YearDiv, 2) + "\n\n" + "Quarterly Dividend: $" + Math.Round(QuarterDiv, 2) + "\n\nMonthly Dividend: $" + Math.Round(MonthlyDiv, 2) + "\n\nPortfolio Dividend Yield: " + Math.Round(DividendTotalPercentage, 2) + "%");
        }

        public void LoadDividends(ListView lv, string active)
        {
            string LvNames = "";
            string StockDataType = "";
            if (!File.Exists(uti.GetXMLPath()))
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "DividendStocksData.xml");
                File.Copy(path, uti.GetXMLPath(), true);
            }
            else
            {

            }
            if (lv.Name == "lvCurrentDividends")
            {
                LvNames = "Portfolio Data";
            }
            else
            {
                LvNames = "Non Portfolio Data";
            }
            DataTable dt = uti.GetXMLData();
            DataView view = dt.DefaultView;
            view.Sort = "symbol asc";
            DataTable dtXml = view.ToTable();
            string symbols = uti.GetStockSymbols(dtXml);
            string stockNames = YahooFinance.GetValues(symbols, "n", true);
            int count = 0;
            if (stockNames == "")
            {
                count++;
                StockDataType += "stock names,";
            }
            string[] names = uti.SplitStockData(stockNames);

            string exDividend = YahooFinance.GetValues(symbols, "r1", true);
            if (exDividend == "")
            {
                count++;
                StockDataType += "dividend dates,";
            }
            string[] exDiv = uti.SplitStockData(exDividend);
            string payDates = YahooFinance.GetValues(symbols, "q", true);
            if (payDates == "")
            {
                count++;
                StockDataType += "pay dates,";
            }
            string[] payDate = uti.SplitStockData(payDates);
            if (count > 0)
            {
                MessageBox.Show("Error! " + StockDataType + " in " + LvNames + " could not be loaded and cannot connect to Yahoo, please try again later.");
            }
            DividendStocks.LoadDividends(lv, names, payDate, exDiv, active, dtXml);
            Program.PleaseWait.Close();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            string result = "";
            ddlIndustry.SelectedIndex = 0;
            ddlIndustryAll.SelectedIndex = 0;
            dtpPayDate.Format = DateTimePickerFormat.Custom;
            dtpPayDate.CustomFormat = "MM/yyyy";
            dtpPayDate.ShowUpDown = true;
            lvAllDividends.FullRowSelect = true;
            lvCurrentDividends.FullRowSelect = true;
            LoadDividends(lvCurrentDividends, "true");
            LoadDividends(lvAllDividends, "false");
            if (result != "")
            {
                MessageBox.Show(result);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (lstID.Count != 0)
            {
                AddRemoveDividends(lvAllDividends, "true");
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstID.Count != 0)
            {
                AddRemoveDividends(lvCurrentDividends, "false");
                HighlightNextBuy();
            }
        }

        public void UncheckNextBuy()
        {
            chkNextBuy.CheckedChanged -= chkNextBuy_CheckedChanged;
            chkNextBuy.Checked = false;
            chkNextBuy.CheckedChanged += chkNextBuy_CheckedChanged;
        }

        public void AddRemoveDividends(ListView lv, string stockActive)
        {
            PleaseWait pw = new PleaseWait();
            pw.Show();
            Application.DoEvents();
            UncheckNextBuy();
            for (int i = 0; i < lv.Items.Count; i++)
            {
                if (lstID.Contains(Convert.ToInt32(lv.Items[i].Tag)))
                {
                    DividendStocks.MoveStock(lv.Items[i].Tag.ToString(), lv.Items[i].SubItems[1].Text, stockActive);
                }
            }
            LoadDividends(lvCurrentDividends, "true");
            LoadDividends(lvAllDividends, "false");
            SelectStocks();
            pw.Close();
        }

        public void SelectStocks()
        {
            if (!CurrentDiv)
            {
                CurrentDiv = true;
                lvAllDividends.SelectedItems.Clear();
                SelectMultiple(lvCurrentDividends);
            }
            else
            {
                CurrentDiv = false;
                lvCurrentDividends.SelectedItems.Clear();
                SelectMultiple(lvAllDividends);
            }
        }

        public void SelectMultiple(ListView lv)
        {
            for (int a = 0; a < lv.Items.Count; a++)
            {
                for (int b = 0; b < lstID.Count; b++)
                {
                    if (lstID[b] == Convert.ToInt32(lv.Items[a].Tag))
                    {
                        lv.Items[a].BackColor = uti.GetHighlightColor();
                        lv.Items[a].Selected = true;
                        lv.Items[a].Focused = true;
                        lv.TopItem = lv.Items[a];
                    }
                }
            }
        }

        public int GetTagCount(List<int> lst, int tag)
        {
            int count = 0;
            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i] == tag)
                {
                    count++;
                }
            }
            return count;
        }

        public List<int> RemoveTags(List<int> lst, int tag)
        {
            List<int> tempList = new List<int>();
            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i] != tag)
                {
                    tempList.Add(lst[i]);
                }
            }
            return tempList;
        }

        public void HighlightMultipleControlColor(ListView lv)
        {
            int index = lv.SelectedItems[0].Index;
            int tag = Convert.ToInt32(lv.Items[index].Tag);
            lstID.Add(tag);

            if (GetTagCount(lstID, tag) == 2)
            {
                lstID = RemoveTags(lstID, tag);
            }

            lv.SelectedItems.Clear();
            for (int i = 0; i < lv.Items.Count; i++)
            {
                if (lstID.Contains(Convert.ToInt32(lv.Items[i].Tag)))
                {
                    lv.Items[i].BackColor = uti.GetHighlightColor();
                }
                if (!lstID.Contains(Convert.ToInt32(lv.Items[i].Tag)))
                {
                    lv.Items[i].BackColor = Color.White;
                }
            }
        }

        public void HighlightMultipleShiftColor(ListView lv)
        {
            lstID.Clear();
            for (int i = 0; i < lv.SelectedItems.Count; i++)
            {
                lstID.Add(Convert.ToInt32(lv.SelectedItems[i].Tag));
            }
            lv.SelectedItems.Clear();
            for (int i = 0; i < lv.Items.Count; i++)
            {
                if (lstID.Contains(Convert.ToInt32(lv.Items[i].Tag)))
                {
                    lv.Items[i].BackColor = uti.GetHighlightColor();
                }
                if (!lstID.Contains(Convert.ToInt32(lv.Items[i].Tag)))
                {
                    lv.Items[i].BackColor = Color.White;
                }
            }
        }

        public void HighlightSingleColor(ListView lv)
        {
            int index = lv.SelectedItems[0].Index;
            int tag = Convert.ToInt32(lv.Items[index].Tag);
            string color = lv.Items[index].BackColor.Name;
            lstID.Clear();
            lstID.Add(tag);
            lv.SelectedItems.Clear();
            for (int i = 0; i < lv.Items.Count; i++)
            {
                if (lstID.Contains(Convert.ToInt32(lv.Items[i].Tag)))
                {
                    lv.Items[i].BackColor = uti.GetHighlightColor();
                }
                else
                {
                    lv.Items[i].BackColor = Color.White;
                }
            }
        }

        private void lvCurrentDividends_MouseClick(object sender, MouseEventArgs e)
        {
            if (Control.ModifierKeys != Keys.Control)
            {
                uti.ClearListViewColors(lvAllDividends);
                uti.SetStockIndexSymbol(lvCurrentDividends);
                HighlightSingleColor(lvCurrentDividends);
            }
        }

        private void lvCurrentDividends_MouseUp(object sender, MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                uti.SetStockIndexSymbol(lvCurrentDividends);
                HighlightMultipleControlColor(lvCurrentDividends);
            }
            CurrentDiv = true;
        }


        private void lvCurrentDividends_MouseDown(object sender, MouseEventArgs e)
        {
            //var info = lvCurrentDividends.HitTest(e.X, e.Y);
            //try
            //{
            //    int s = info.Item.Index;
            //}
            //catch
            //{
            //    uti.ClearListViewColors(lvCurrentDividends, lstID);
            //    uti.ClearListViewColors(lvAllDividends, lstID);
            //}
        }

        private void lvAllDividends_MouseClick(object sender, MouseEventArgs e)
        {
            if (Control.ModifierKeys != Keys.Control && Control.ModifierKeys != Keys.Shift)
            {
                uti.ClearListViewColors(lvCurrentDividends);
                lstID.Clear();
                uti.SetStockIndexSymbol(lvAllDividends);
                HighlightSingleColor(lvAllDividends);
                HighlightNextBuy();
            }
        }

        private void lvAllDividends_MouseUp(object sender, MouseEventArgs e)
        {
            var info = lvAllDividends.HitTest(e.X, e.Y);
            try
            {
                int s = info.Item.Index;
                if (Control.ModifierKeys == Keys.Control)
                {
                    uti.SetStockIndexSymbol(lvAllDividends);
                    HighlightMultipleControlColor(lvAllDividends);
                }
                else if(Control.ModifierKeys == Keys.Shift)
                {
                    HighlightMultipleShiftColor(lvAllDividends);
                }
            }
            catch
            {
                lstID.Clear();
                uti.ClearListViewColors(lvAllDividends);
                uti.ClearListViewColors(lvCurrentDividends);
            }
            CurrentDiv = false;
        }

        private void lvCurrentDividends_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenDividends(true, CurrentDiv);
        }

        private void lvAllDividends_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenDividends(true, CurrentDiv);
        }


        public List<StockInfo> GetStockInfoList(ListView lv)
        {
            List<StockInfo> lst = new List<StockInfo>();
            lst = uti.LoadStockInfo(lstID[0].ToString(), Symbol,
                    lv.Items[SelectedIndex].SubItems[2].Text, lv.Items[SelectedIndex].SubItems[3].Text, lv.Items[SelectedIndex].SubItems[4].Text,
                    lv.Items[SelectedIndex].SubItems[5].Text, lv.Items[SelectedIndex].SubItems[6].Text, lv.Items[SelectedIndex].SubItems[7].Text,
                    lv.Items[SelectedIndex].SubItems[8].Text);
            return lst;
        }

        public void GetDividendPrice()
        {
            uti.ClearListViewColors(lvAllDividends);
            decimal TotalDividendPrice = 0;
            decimal QuarterlyDividendPrice = 0;
            decimal MonthlyDividendPrice = 0;
            DividendStocks.GetDividendPrice(lvCurrentDividends, lstID, out TotalDividendPrice, out QuarterlyDividendPrice, out MonthlyDividendPrice);
            MessageBox.Show("Yearly: $" + Math.Round(TotalDividendPrice, 2).ToString() + "\n\nQuarterly: $" + Math.Round(QuarterlyDividendPrice, 2) + "\n\nMonthly: $" + Math.Round(MonthlyDividendPrice, 2));
        }

        public void GetSharePrice()
        {
            uti.ClearListViewColors(lvAllDividends);
            decimal totalPrice = 0;
            DividendStocks.GetTotalSharePrice(lstID, out totalPrice);
            MessageBox.Show("$" + Math.Round(totalPrice, 2).ToString());
        }

        private void btnHighlight_Click(object sender, EventArgs e)
        {
            Highlight(lvCurrentDividends, ddlIndustry, true);
        }

        public void SearchSymbol(TextBox tb, ListView lv)
        {
            lv.SelectedItems.Clear();
            uti.ClearListViewColors(lv);
            lstID.Clear();
            for (int i = 0; i < lv.Items.Count; i++)
            {
                if (lv.Items[i].SubItems[1].Text == tb.Text.ToUpper())
                {
                    lstID.Add(Convert.ToInt32(lv.Items[i].Tag));
                    lv.Items[i].BackColor = uti.GetHighlightColor();
                    lv.Items[i].Selected = true;
                    lv.Items[i].Focused = true;
                    lv.TopItem = lv.Items[i];
                    //lv.Select();
                }
            }
        }

        public void Highlight(ListView lv, ComboBox ddl, bool showMsg)
        {
            decimal count = 0;
            decimal percentage = Convert.ToDecimal(lv.Items.Count);
            lv.SelectedItems.Clear();
            uti.ClearListViewColors(lvAllDividends);
            lstID.Clear();
            for (int i = 0; i < lv.Items.Count; i++)
            {
                if (lv.Items[i].SubItems[3].Text == ddl.Text)
                {
                    count++;
                    lstID.Add(Convert.ToInt32(lv.Items[i].Tag));
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
            percentage = (count / percentage) * 100;
            if (showMsg)
            {
                MessageBox.Show(count + " " + ddl.Text + ": " + Math.Round(percentage, 2) + "%");
            }
        }

        public void HighlightAllNextToBuy(ListView lv)
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
                                lstID.Add(Convert.ToInt32(lv.Items[i].Tag));
                                cnt++;
                            }
                        }
                    }
                }
            }
            catch
            {

            }
            MessageBox.Show(string.Format("{0} results.", cnt));
        }

        public void HighlightNextBuy()
        {
            try
            {
                if (lstID.Count == 1)
                {
                    chkNextBuy.CheckedChanged -= chkNextBuy_CheckedChanged;
                    chkNextBuy.Checked = DividendStocks.LoadNextPurchase(Symbol) == "yes" ? true : false;
                    chkNextBuy.CheckedChanged += chkNextBuy_CheckedChanged;
                }
            }
            catch
            {

            }
        }

        public void ShowIndustryPercentages(ListView lv)
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

        private void btnHighlightAll_Click(object sender, EventArgs e)
        {
            Highlight(lvAllDividends, ddlIndustryAll, true);
        }

        private void txtSearchSymbol_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchSymbol.Text != "")
            {
                SearchSymbol(txtSearchSymbol, lvCurrentDividends);
            }
            else
            {
                uti.ClearListViewColors(lvCurrentDividends);
            }
        }

        private void txtSearchAllSymbol_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchAllSymbol.Text != "")
            {
                SearchSymbol(txtSearchAllSymbol, lvAllDividends);
            }
            else
            {
                uti.ClearListViewColors(lvAllDividends);
            }
        }

        private void chkNextBuy_CheckedChanged(object sender, EventArgs e)
        {
            if (!CurrentDiv)
            {
                if (lstID.Count == 0)
                {
                    UncheckNextBuy();
                    MessageBox.Show("Please select a stock.");
                    return;
                }
                if (lstID.Count <= 1)
                {
                    if (chkNextBuy.Checked)
                    {
                        DividendStocks.SaveNextPurchase(Convert.ToInt32(lstID[0]), "yes", Symbol);
                    }
                    else
                    {
                        DividendStocks.SaveNextPurchase(Convert.ToInt32(lstID[0]), "no", Symbol);
                    }
                }
                else
                {
                    UncheckNextBuy();
                    MessageBox.Show("Please select one stock at a time.");
                }
            }
            else
            {
                UncheckNextBuy();
                MessageBox.Show("Please select one stock in the non portfolio section.");
            }
        }

        private void btnPayDate_Click(object sender, EventArgs e)
        {
            HighlightPayDate(lvCurrentDividends);
        }

        public void HighlightPayDate(ListView lv)
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
            lv.SelectedItems.Clear();
            uti.ClearListViewColors(lv);
            lstID.Clear();
            for (int i = 0; i < lv.Items.Count; i++)
            {
                string date = lv.Items[i].SubItems[7].Text;
                string[] dateSplit = date.Split('/');
                if (date != "N/A")
                {
                    monthYear = dateSplit[0].Trim() + "/" + dateSplit[2];
                    dtpMonthYear = dtpPayDate.Value.ToString("M/yyyy");
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
                            div = Convert.ToDecimal(YahooFinance.GetValues(symbol, "d", false));
                        }
                        catch
                        {
                            MessageBox.Show("Could not highlight, yahoo connection was lost. Please try again later.");
                            return;
                        }
                        decimal divReceived = uti.GetDivPrice(Convert.ToDecimal(lv.Items[i].SubItems[4].Text.ToString()), div);
                        totalDiv += divReceived;
                        individualDivData += symbol + ": $" + Math.Round(divReceived/4, 2) + " (Pay Date: " + lv.Items[i].SubItems[7].Text.ToString() + ")\n\n";
                        lstID.Add(Convert.ToInt32(lv.Items[i].Tag));
                        cnt++;
                    }
                }
            }
            lv.SelectedItems.Clear();
            quarterlyDiv = totalDiv / 4;
            pw.Close();
            if (cnt != 0)
            {
                MessageBox.Show(string.Format("{0} results\n\n" + "{1} \n\n" + individualDivData + "Total: ${2}\n\n", cnt, "Dividends for " + dtpMonthYear + "",  Math.Round(quarterlyDiv, 2)));
            }
            else
            {
                MessageBox.Show(string.Format("No Results for {0}", dtpMonthYear));
            }
        }

        public string GetDividendInterval(int id, DataTable dt)
        {
            string divInterval = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToInt32(dt.Rows[i]["id"]) == id)
                {
                    divInterval = dt.Rows[i]["dividendinterval"].ToString();
                }
            }
            return divInterval;
        }

        private void lvAllDividends_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Control.ModifierKeys != Keys.Control)
            {
        
            }
        }

        private void getCostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetSharePrice();
        }

        private void getDividendsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetDividendPrice();
        }

        private void showSectorPercentagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowIndustryPercentages(lvCurrentDividends);
        }

        private void calculateResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (backgroundWorker1.IsBusy)
                {
                    MessageBox.Show("Please wait until data is processed.");
                    return;
                }
                backgroundWorker1.RunWorkerAsync();
            }
            catch
            {

            }
        }

        private void highlightNextPurchasesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HighlightAllNextToBuy(lvAllDividends);
        }

        private void showPercentagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowIndustryPercentages(lvAllDividends);
        }
    }
}
