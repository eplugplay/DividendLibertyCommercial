using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DividendLiberty
{
    public partial class Shares : Form
    {
        public bool Edit { get; set; }
        public string ID { get; set; }
        public string DividendPriceID { get; set; }
        public bool CurrentDiv { get; set; }
        public Shares(bool edit, string id, string dividendPriceID, bool currentDiv)
        {
            ID = id;
            Edit = edit;
            DividendPriceID = dividendPriceID;
            CurrentDiv = currentDiv;
            InitializeComponent();
        }

        private void Shares_Load(object sender, EventArgs e)
        {
            if (Edit)
            {
                LoadSharesInfo();
                btnSave.Text = "Update";
            }
        }

        public void LoadSharesInfo()
        {
            DataTable dt = DividendStocks.GetSharePriceInfo(DividendPriceID);
            txtPurchasePrice.Text = dt.Rows[0]["purchaseprice"].ToString();
            txtNumberOfShares.Text = dt.Rows[0]["numberofshares"].ToString();
            dtpPurchaseDate.Value = Convert.ToDateTime(dt.Rows[0]["purchasedate"]);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtNumberOfShares.Text == "")
            {
                MessageBox.Show("Please enter number of shares.");
                return;
            }
            try
            {
                decimal.Parse(txtNumberOfShares.Text);
            }
            catch
            {
                MessageBox.Show("Please enter numbers only.");
                txtNumberOfShares.Focus();
                return;
            }
            if (txtPurchasePrice.Text == "")
            {
                MessageBox.Show("Please enter purchase price.");
                return;
            }
            try
            {
                decimal.Parse(txtPurchasePrice.Text);
            }
            catch
            {
                MessageBox.Show("Please enter numbers only.");
                txtPurchasePrice.Focus();
                return;
            }
            PleaseWait pw = new PleaseWait();
            pw.Show();
            Application.DoEvents();
            if (Edit)
            {
                DividendStocks.UpdateShare(Convert.ToDecimal(txtPurchasePrice.Text), Convert.ToDecimal(txtNumberOfShares.Text), DividendPriceID, dtpPurchaseDate.Value);
            }
            else
            {
                DividendStocks.NewShare(Convert.ToDecimal(txtPurchasePrice.Text), Convert.ToDecimal(txtNumberOfShares.Text), ID, dtpPurchaseDate.Value);
            }
            MainMenu._Dividends.LoadDividendStock();
            LoadAllMainDividends();
            SelectCurrentStock();
            pw.Close();
            this.Close();
        }

        public void SelectCurrentStock()
        {
            if (CurrentDiv)
            {
                //Program.MainMenu.lbCurrentDividends.ClearSelected();
                //Program.MainMenu.lbCurrentDividends.SelectedValue = Convert.ToInt32(ID);
            }
            else
            {
                //Program.MainMenu.lbAllDividends.ClearSelected();
                //Program.MainMenu.lbAllDividends.SelectedValue = Convert.ToInt32(ID);
            }
        }

        public void LoadAllMainDividends()
        {
            if (CurrentDiv)
            {
                //Program.MainMenu.LoadCurrentDividends();
            }
            else
            {
                //Program.MainMenu.LoadAllDividends();
            }
        }

        public void GetSharePrice()
        {
            decimal TotalSharePrice = 0;
            TotalSharePrice = Convert.ToDecimal(txtNumberOfShares.Text) * Convert.ToDecimal(txtPurchasePrice.Text);
            MessageBox.Show("$" + Math.Round(TotalSharePrice, 2).ToString());
        }

        private void btnGetPrice_Click(object sender, EventArgs e)
        {
            GetSharePrice();
        }
    }
}
