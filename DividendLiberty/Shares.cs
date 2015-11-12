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
        public bool CurrentDiv { get; set; }
        List<StockInfo> LstStockInfo = new List<StockInfo>();
        public Shares(bool edit, bool currentDiv, List<StockInfo> lstStockInfo)
        {
            LstStockInfo = lstStockInfo;
            Edit = edit;
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
            DataTable dt = uti.GetXMLData();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["symbol"].ToString() == LstStockInfo[0].Symbol)
                {
                   txtPurchasePrice.Text = dt.Rows[i]["cost"].ToString();
                   txtNumberOfShares.Text = dt.Rows[i]["shares"].ToString();
                   dtpPurchaseDate.Value = Convert.ToDateTime(dt.Rows[i]["purchasedate"]);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //if (MainMenu._Dividends.txtSymbol.Text == "")
            //{
            //    MessageBox.Show("Please enter symbol.");
            //    return;
            //}
            //else
            //{
            //    try
            //    {
            //        YahooFinance.GetValues(MainMenu._Dividends.txtSymbol.Text, "n", false);
            //    }
            //    catch
            //    {
            //        MessageBox.Show("Please enter valide symbol.");
            //        return;
            //    }
            //}
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
            DividendStocks.UpdateShare(LstStockInfo[0].ID, LstStockInfo[0].Symbol, txtPurchasePrice.Text, txtNumberOfShares.Text, dtpPurchaseDate.Value.ToString());
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
                Program.MainMenu.LoadDividends(Program.MainMenu.lvCurrentDividends, "true");
            }
            else
            {
                Program.MainMenu.LoadDividends(Program.MainMenu.lvAllDividends, "false");
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
