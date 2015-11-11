using System;
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
        static public Shares _Shares;
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
                ReloadMainDividends();
                //Program.MainMenu.lbAllDividends.SelectedValue = Convert.ToInt32(ID);
                pw.Close();
                this.Close();
            }
            else
            {
                if (uti.ValidateStock(txtSymbol.Text))
                {
                    pw.Close();
                    MessageBox.Show(string.Format("{0} already exist.", txtSymbol.Text.ToUpper()));
                    return;
                }
                DividendStocks.NewDividendStock(txtSymbol.Text, ddlIndustry.Text, ddlDividendInterval.Text);
                ReloadMainDividends();
                //Program.MainMenu.lbAllDividends.SelectedValue = Convert.ToInt32(ID);
                pw.Close();
                this.Close();
            }
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
                btnSave.Text = "Update";
                this.Text = "Edit Dividend Stock";
            }
            else
            {
                HideTextBoxes();
                gpSharesOptions.Enabled = false;
                btnSave.Text = "Save";
                ddlDividendInterval.SelectedIndex = 0;
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
        }

        public void LoadDividendStock()
        {
            //DataTable dt = DividendStocks.GetDividend(ID);
            txtSymbol.Text = LstStockInfo[0].Symbol;
            txtStockName.Text = LstStockInfo[0].Name;
            ddlIndustry.SelectedIndex = ddlIndustry.FindString(LstStockInfo[0].Industry);
            ddlDividendInterval.SelectedIndex = ddlDividendInterval.FindString(LstStockInfo[0].Interval);
            txtAnnualDividend.Text = YahooFinance.GetValues(LstStockInfo[0].Symbol, "d", false);
            txtDividendPercent.Text = YahooFinance.GetValues(LstStockInfo[0].Symbol, "y", false);
            txtMarketCap.Text = YahooFinance.GetValues(LstStockInfo[0].Symbol, "j1", false);
            txtExDividend.Text = YahooFinance.GetValues(LstStockInfo[0].Symbol, "q", false);
            txtPayDate.Text = YahooFinance.GetValues(LstStockInfo[0].Symbol, "r1", false);
            txtPERatio.Text = YahooFinance.GetValues(LstStockInfo[0].Symbol, "r", false);
            txtDayRange.Text = YahooFinance.GetValues(LstStockInfo[0].Symbol, "m", false);
            txt52WeekLow.Text = YahooFinance.GetValues(LstStockInfo[0].Symbol, "j", false);
            txt52WeekHigh.Text = YahooFinance.GetValues(LstStockInfo[0].Symbol, "k", false);
            txtCurrentPrice.Text = YahooFinance.GetValues(LstStockInfo[0].Symbol, "a", false);
            txtOpenPrice.Text = YahooFinance.GetValues(LstStockInfo[0].Symbol, "o", false);
            // load purchase dates
            LoadPurchaseDates();
            LoadPurchaseData();
        }

        public void LoadPurchaseDates()
        {
            txtSharePrice.Clear();
            txtNumberOfShares.Clear();
            ddlSharePurchaseDate.SelectedIndexChanged -= ddlSharePurchaseDate_SelectedIndexChanged;
            ddlSharePurchaseDate.DataSource = DividendStocks.GetDividendActionDate(ID);
            ddlSharePurchaseDate.DisplayMember = "purchasedate";
            ddlSharePurchaseDate.ValueMember = "id";
            if (ddlSharePurchaseDate.Text != "")
            {
                btnNewShares.Enabled = false;
                btnDeleteShares.Enabled = true;
                btnEditShares.Enabled = true;
                btnGetSharePrice.Enabled = true;
                btnDividendPrice.Enabled = true;
            }
            else
            {
                btnNewShares.Enabled = true;
                btnDeleteShares.Enabled = false;
                btnEditShares.Enabled = false;
                btnGetSharePrice.Enabled = false;
                btnDividendPrice.Enabled = false;
            }
            ddlSharePurchaseDate.SelectedIndexChanged += ddlSharePurchaseDate_SelectedIndexChanged;
        }

        private void ddlSharePurchaseDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSharePurchaseDate.SelectedIndex != -1)
            {
                LoadPurchaseData();
            }
        }

        public void LoadPurchaseData()
        {
            if (ddlSharePurchaseDate.SelectedValue != null)
            {
                DataTable dt = DividendStocks.GetPurchasePrice(ddlSharePurchaseDate.SelectedValue.ToString());
                txtSharePrice.Text = dt.Rows[0]["purchaseprice"].ToString();
                txtNumberOfShares.Text = dt.Rows[0]["numberofshares"].ToString();
            }
        }

        public bool ValidateAll()
        {
            if (txtSymbol.Text == "")
            {
                MessageBox.Show("Please enter symbol.");
                return false;
            }
            if (ddlIndustry.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Industry.");
                return false;
            }
            return true;
        }

        private void btnGetSharePrice_Click(object sender, EventArgs e)
        {
            if (txtNumberOfShares.Text != "")
            {
                decimal TotalSharePrice = 0;
                TotalSharePrice = Convert.ToDecimal(txtNumberOfShares.Text) * Convert.ToDecimal(txtSharePrice.Text);
                MessageBox.Show("$" + Math.Round(TotalSharePrice, 2).ToString());
            }
        }

        private void btnDividendPrice_Click(object sender, EventArgs e)
        {
            if (txtNumberOfShares.Text != "")
            {
                decimal TotalDividendPrice = Convert.ToDecimal(txtAnnualDividend.Text) * Convert.ToDecimal(txtNumberOfShares.Text);
                decimal QuarterlyDividendPrice = TotalDividendPrice / 4;
                decimal MonthlyDividendPrice = TotalDividendPrice / 12;
                MessageBox.Show("Yearly: $" + Math.Round(TotalDividendPrice, 2).ToString() + "\n\nQuarterly: $" + Math.Round(QuarterlyDividendPrice, 2) + "\n\nMonthly: $" + Math.Round(MonthlyDividendPrice, 2));
            }
        }


        public void OpenSharesForm(bool edit)
        {
            if (_Shares == null || _Shares.IsDisposed)
            {
                _Shares = new Shares(edit, ID, ddlSharePurchaseDate.SelectedValue == null ? "" : ddlSharePurchaseDate.SelectedValue.ToString(), CurrentDiv);
                _Shares.Show();
            }
            else
            {
                if (_Shares.WindowState == FormWindowState.Minimized)
                {
                    _Shares.WindowState = FormWindowState.Normal;
                }
                else
                {
                    _Shares.BringToFront();
                }
            }
        }


        private void btnNewShares_Click(object sender, EventArgs e)
        {
            OpenSharesForm(false);
        }

        private void btnEditShares_Click(object sender, EventArgs e)
        {
            if (txtSharePrice.Text != "")
            {
                OpenSharesForm(true);
            }
        }

        private void btnDeleteShares_Click(object sender, EventArgs e)
        {
            if (txtSharePrice.Text != "")
            {
                if (MessageBox.Show("Delete?", "Delete?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    PleaseWait pw = new PleaseWait();
                    pw.Show();
                    Application.DoEvents();
                    DividendStocks.DeleteShare(ddlSharePurchaseDate.SelectedValue.ToString());
                    LoadPurchaseDates();
                    LoadPurchaseData();
                    ReloadMainDividends();
                    pw.Close();
                }
            }
        }
    }
}
