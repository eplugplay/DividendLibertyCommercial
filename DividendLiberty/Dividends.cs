﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace DividendLiberty
{
    public partial class Dividends : Form
    {
        public bool Edit { get; set; }
        public string ID { get; set; }
        bool CurrentDiv {get;set;}
        string Symbol { get; set; }
        List<StockInfo> LstStockInfo = new List<StockInfo>();
        public Dividends(bool edit, bool currentDiv, List<StockInfo> lstStockInfo)
        {
            Edit = edit;
            CurrentDiv = currentDiv;
            LstStockInfo = lstStockInfo;
            Symbol = lstStockInfo[0].Symbol;
            InitializeComponent();
        }

        public Dividends(bool edit, bool currentDiv)
        {
            Edit = edit;
            CurrentDiv = currentDiv;
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateAll())
            {
                return;
            }

            PleaseWait pw = new PleaseWait();
            pw.Show();
            Application.DoEvents();

            if (Edit)
            {
                DividendStocks.UpdateDividendStock(LstStockInfo[0].ID, Symbol, txtSymbol.Text, ddlIndustry.Text, ddlDividendInterval.Text);
                DividendStocks.UpdateShare(LstStockInfo[0].ID, Symbol, txtCost.Text, txtNumberOfShares.Text, dtpPurchaseDate.Value.ToString("MM-dd-yyyy"));
                ReloadMainDividends();
            }
            else
            {
                if (uti.ValidateStock(txtSymbol.Text))
                {
                    pw.Close();
                    MessageBox.Show(string.Format("{0} already exist.", txtSymbol.Text.ToUpper()));
                    return;
                }
                string newID = DividendStocks.NewDividendStock(txtSymbol.Text.ToUpper(), ddlIndustry.Text, ddlDividendInterval.Text);
                DividendStocks.UpdateShare(newID, txtSymbol.Text, txtCost.Text, txtNumberOfShares.Text, dtpPurchaseDate.Value.ToString("MM-dd-yyyy"));
                ReloadMainDividends();
            }

            pw.Close();
            this.Close();
        }

        public void ReloadMainDividends()
        {
            if (CurrentDiv)
            {
                Program.MainMenu.LoadDividends(Program.MainMenu.lvCurrentDividends, "true");
            }
            else
            {
                Program.MainMenu.LoadDividends(Program.MainMenu.lvAllDividends, "false");
            }
        }

        private void Dividends_Load(object sender, EventArgs e)
        {
            ddlIndustry.SelectedIndex = 0;
            if (Edit)
            {
                LoadDividendStock();
                LoadPurchaseInfo();
                btnSave.Text = "Update";
                this.Text = "Edit Dividend Stock";
            }
            else
            {
                HideTextBoxes();
                //gpSharesOptions.Enabled = false;
                btnSave.Text = "Save";
                txtNumberOfShares.Text = "0";
                txtCost.Text = "0";
                ddlDividendInterval.SelectedIndex = 1;
                lblReq1.Show();
                lblReq2.Show();
                lblReq3.Show();
                lblRequired.Show();
            }
        }

        public void HideTextBoxes()
        {
            txtAnnualDividend.Enabled = false;
            txtMarketCap.Enabled = false;
            txtDividendPercent.Enabled = false;
            txtExDividend.Enabled = false;
            txtPERatio.Enabled = false;
            txtPayDate.Enabled = false;
            txtOpenPrice.Enabled = false;
            txt52WeekHigh.Enabled = false;
            txt52WeekLow.Enabled = false;
            txtCurrentPrice.Enabled = false;
            txtDayRange.Enabled = false;
            txtStockName.Enabled = false;
        }

        public void LoadDividendStock()
        {
            //DataTable dt = DividendStocks.GetDividend(ID);
            txtSymbol.Text = LstStockInfo[0].Symbol;
            txtStockName.Text = LstStockInfo[0].Name;
            ddlIndustry.SelectedIndex = ddlIndustry.FindString(LstStockInfo[0].Industry);
            ddlDividendInterval.SelectedIndex = ddlDividendInterval.FindString(LstStockInfo[0].Interval);
            txtAnnualDividend.Text = YahooFinance.GetValues(LstStockInfo[0].Symbol, YahooFinance.GetCodes(YahooCodes.annualDividend), false);
            txtDividendPercent.Text = YahooFinance.GetValues(LstStockInfo[0].Symbol, YahooFinance.GetCodes(YahooCodes.dividendYield), false);
            txtMarketCap.Text = YahooFinance.GetValues(LstStockInfo[0].Symbol, YahooFinance.GetCodes(YahooCodes.marketCap), false);
            txtExDividend.Text = YahooFinance.GetValues(LstStockInfo[0].Symbol, YahooFinance.GetCodes(YahooCodes.exDividend), false);
            txtPayDate.Text = YahooFinance.GetValues(LstStockInfo[0].Symbol, YahooFinance.GetCodes(YahooCodes.payDate), false);
            txtPERatio.Text = YahooFinance.GetValues(LstStockInfo[0].Symbol, YahooFinance.GetCodes(YahooCodes.peRatio), false);
            txtDayRange.Text = YahooFinance.GetValues(LstStockInfo[0].Symbol, YahooFinance.GetCodes(YahooCodes.dayRange), false);
            txt52WeekLow.Text = YahooFinance.GetValues(LstStockInfo[0].Symbol, YahooFinance.GetCodes(YahooCodes.fiftyTwoWeekLow), false);
            txt52WeekHigh.Text = YahooFinance.GetValues(LstStockInfo[0].Symbol, YahooFinance.GetCodes(YahooCodes.fiftyTwoWeekHigh), false);
            txtCurrentPrice.Text = YahooFinance.GetValues(LstStockInfo[0].Symbol, YahooFinance.GetCodes(YahooCodes.currentPrice), false);
            txtOpenPrice.Text = YahooFinance.GetValues(LstStockInfo[0].Symbol, YahooFinance.GetCodes(YahooCodes.openPrice), false);
        }

        public void LoadPurchaseInfo()
        {
            txtCost.Clear();
            txtNumberOfShares.Clear();
            DataTable dt = uti.GetXMLData();
            DataTable dtFinal = dt.Copy();
            for (int i = dt.Rows.Count -1; i >= 0; i--)
            {
                if (dt.Rows[i]["symbol"].ToString() == LstStockInfo[0].Symbol)
                {
                    dtpPurchaseDate.Value = Convert.ToDateTime(dt.Rows[i]["purchasedate"]);
                    txtCost.Text = dtFinal.Rows[i]["cost"].ToString();
                    txtNumberOfShares.Text = dtFinal.Rows[i]["shares"].ToString();
                }
            }
        }

        public bool ValidateAll()
        {
            if (txtSymbol.Text == "")
            {
                MessageBox.Show("Please enter symbol.");
                return false;
            }
            //if(!uti.IsLettersOnly(txtSymbol.Text))
            //{
            //    MessageBox.Show("Please enter letters only for symbol.");
            //    return false;
            //}
            if (ddlIndustry.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Industry.");
                return false;
            }
            if (txtNumberOfShares.Text == "")
            {
                MessageBox.Show("Please enter number of shares.");
                return false;
            }
            try
            {
                decimal.Parse(txtNumberOfShares.Text);
            }
            catch
            {
                MessageBox.Show("Please enter numbers only.");
                txtNumberOfShares.Focus();
                return false;
            }
            if (txtCost.Text == "")
            {
                MessageBox.Show("Please enter purchase price.");
                return false;
            }
            try
            {
                decimal.Parse(txtCost.Text);
            }
            catch
            {
                MessageBox.Show("Please enter numbers only.");
                txtCost.Focus();
                return false;
            }
            return true;
        }

        private void btnGetSharePrice_Click(object sender, EventArgs e)
        {
            if (txtNumberOfShares.Text != "" && txtCost.Text != "")
            {
                decimal TotalSharePrice = 0;
                TotalSharePrice = Convert.ToDecimal(txtNumberOfShares.Text) * Convert.ToDecimal(txtCost.Text);
                MessageBox.Show("$" + Math.Round(TotalSharePrice, 2).ToString());
            }
        }

        private void btnDividendPrice_Click(object sender, EventArgs e)
        {
            if (txtNumberOfShares.Text != "" && txtCost.Text != "")
            {
                decimal TotalDividendPrice = Convert.ToDecimal(txtAnnualDividend.Text) * Convert.ToDecimal(txtNumberOfShares.Text);
                decimal QuarterlyDividendPrice = TotalDividendPrice / 4;
                decimal MonthlyDividendPrice = TotalDividendPrice / 12;
                MessageBox.Show("Yearly: $" + Math.Round(TotalDividendPrice, 2).ToString() + "\n\nQuarterly: $" + Math.Round(QuarterlyDividendPrice, 2) + "\n\nMonthly: $" + Math.Round(MonthlyDividendPrice, 2));
            }
        }
    }
}
