namespace DividendLiberty
{
    partial class Dividends
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtSymbol = new System.Windows.Forms.TextBox();
            this.lblSymbol = new System.Windows.Forms.Label();
            this.lblStockName = new System.Windows.Forms.Label();
            this.txtStockName = new System.Windows.Forms.TextBox();
            this.lblSharePrice = new System.Windows.Forms.Label();
            this.txtSharePrice = new System.Windows.Forms.TextBox();
            this.lblIndustry = new System.Windows.Forms.Label();
            this.lblNumberShare = new System.Windows.Forms.Label();
            this.txtNumberOfShares = new System.Windows.Forms.TextBox();
            this.lblAnnualDividend = new System.Windows.Forms.Label();
            this.txtAnnualDividend = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDividendPercent = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.gpDividendInfo = new System.Windows.Forms.GroupBox();
            this.ddlDividendInterval = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCurrentPrice = new System.Windows.Forms.TextBox();
            this.txtExDividend = new System.Windows.Forms.TextBox();
            this.txtPayDate = new System.Windows.Forms.TextBox();
            this.lblOpenPrice = new System.Windows.Forms.Label();
            this.txtOpenPrice = new System.Windows.Forms.TextBox();
            this.txtMarketCap = new System.Windows.Forms.TextBox();
            this.lblDaysRange = new System.Windows.Forms.Label();
            this.txtDayRange = new System.Windows.Forms.TextBox();
            this.lbl52WeekMiddle = new System.Windows.Forms.Label();
            this.txt52WeekHigh = new System.Windows.Forms.TextBox();
            this.lbl52WeekRange = new System.Windows.Forms.Label();
            this.txt52WeekLow = new System.Windows.Forms.TextBox();
            this.lblPERatio = new System.Windows.Forms.Label();
            this.txtPERatio = new System.Windows.Forms.TextBox();
            this.lblPayDate = new System.Windows.Forms.Label();
            this.lblExDividend = new System.Windows.Forms.Label();
            this.ddlIndustry = new System.Windows.Forms.ComboBox();
            this.lblMarketCap = new System.Windows.Forms.Label();
            this.btnDividendPrice = new System.Windows.Forms.Button();
            this.btnGetSharePrice = new System.Windows.Forms.Button();
            this.ddlSharePurchaseDate = new System.Windows.Forms.ComboBox();
            this.lblSharePurchaseDate = new System.Windows.Forms.Label();
            this.btnNewShares = new System.Windows.Forms.Button();
            this.btnEditShares = new System.Windows.Forms.Button();
            this.btnDeleteShares = new System.Windows.Forms.Button();
            this.gpSharesOptions = new System.Windows.Forms.GroupBox();
            this.gpDividendInfo.SuspendLayout();
            this.gpSharesOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSymbol
            // 
            this.txtSymbol.BackColor = System.Drawing.Color.AliceBlue;
            this.txtSymbol.ForeColor = System.Drawing.Color.Black;
            this.txtSymbol.Location = new System.Drawing.Point(123, 21);
            this.txtSymbol.Name = "txtSymbol";
            this.txtSymbol.Size = new System.Drawing.Size(210, 20);
            this.txtSymbol.TabIndex = 0;
            // 
            // lblSymbol
            // 
            this.lblSymbol.AutoSize = true;
            this.lblSymbol.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSymbol.Location = new System.Drawing.Point(58, 27);
            this.lblSymbol.Name = "lblSymbol";
            this.lblSymbol.Size = new System.Drawing.Size(51, 13);
            this.lblSymbol.TabIndex = 1;
            this.lblSymbol.Text = "Symbol:";
            // 
            // lblStockName
            // 
            this.lblStockName.AutoSize = true;
            this.lblStockName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStockName.Location = new System.Drawing.Point(372, 28);
            this.lblStockName.Name = "lblStockName";
            this.lblStockName.Size = new System.Drawing.Size(80, 13);
            this.lblStockName.TabIndex = 3;
            this.lblStockName.Text = "Stock Name:";
            // 
            // txtStockName
            // 
            this.txtStockName.BackColor = System.Drawing.Color.AliceBlue;
            this.txtStockName.ForeColor = System.Drawing.Color.Black;
            this.txtStockName.Location = new System.Drawing.Point(458, 25);
            this.txtStockName.Name = "txtStockName";
            this.txtStockName.ReadOnly = true;
            this.txtStockName.Size = new System.Drawing.Size(214, 20);
            this.txtStockName.TabIndex = 1;
            // 
            // lblSharePrice
            // 
            this.lblSharePrice.AutoSize = true;
            this.lblSharePrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSharePrice.Location = new System.Drawing.Point(15, 96);
            this.lblSharePrice.Name = "lblSharePrice";
            this.lblSharePrice.Size = new System.Drawing.Size(104, 13);
            this.lblSharePrice.TabIndex = 7;
            this.lblSharePrice.Text = "Purchased Price:";
            // 
            // txtSharePrice
            // 
            this.txtSharePrice.ForeColor = System.Drawing.Color.Black;
            this.txtSharePrice.Location = new System.Drawing.Point(127, 93);
            this.txtSharePrice.Name = "txtSharePrice";
            this.txtSharePrice.ReadOnly = true;
            this.txtSharePrice.Size = new System.Drawing.Size(210, 20);
            this.txtSharePrice.TabIndex = 6;
            this.txtSharePrice.TabStop = false;
            // 
            // lblIndustry
            // 
            this.lblIndustry.AutoSize = true;
            this.lblIndustry.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIndustry.Location = new System.Drawing.Point(53, 50);
            this.lblIndustry.Name = "lblIndustry";
            this.lblIndustry.Size = new System.Drawing.Size(56, 13);
            this.lblIndustry.TabIndex = 5;
            this.lblIndustry.Text = "Industry:";
            // 
            // lblNumberShare
            // 
            this.lblNumberShare.AutoSize = true;
            this.lblNumberShare.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberShare.Location = new System.Drawing.Point(27, 121);
            this.lblNumberShare.Name = "lblNumberShare";
            this.lblNumberShare.Size = new System.Drawing.Size(77, 13);
            this.lblNumberShare.TabIndex = 11;
            this.lblNumberShare.Text = "# of Shares:";
            // 
            // txtNumberOfShares
            // 
            this.txtNumberOfShares.ForeColor = System.Drawing.Color.Black;
            this.txtNumberOfShares.Location = new System.Drawing.Point(127, 118);
            this.txtNumberOfShares.Name = "txtNumberOfShares";
            this.txtNumberOfShares.ReadOnly = true;
            this.txtNumberOfShares.Size = new System.Drawing.Size(210, 20);
            this.txtNumberOfShares.TabIndex = 7;
            this.txtNumberOfShares.TabStop = false;
            // 
            // lblAnnualDividend
            // 
            this.lblAnnualDividend.AutoSize = true;
            this.lblAnnualDividend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnnualDividend.Location = new System.Drawing.Point(39, 131);
            this.lblAnnualDividend.Name = "lblAnnualDividend";
            this.lblAnnualDividend.Size = new System.Drawing.Size(70, 13);
            this.lblAnnualDividend.TabIndex = 9;
            this.lblAnnualDividend.Text = "Ann. Divid:";
            // 
            // txtAnnualDividend
            // 
            this.txtAnnualDividend.BackColor = System.Drawing.Color.AliceBlue;
            this.txtAnnualDividend.ForeColor = System.Drawing.Color.Black;
            this.txtAnnualDividend.Location = new System.Drawing.Point(123, 128);
            this.txtAnnualDividend.Name = "txtAnnualDividend";
            this.txtAnnualDividend.ReadOnly = true;
            this.txtAnnualDividend.Size = new System.Drawing.Size(210, 20);
            this.txtAnnualDividend.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(36, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Dividend %:";
            // 
            // txtDividendPercent
            // 
            this.txtDividendPercent.BackColor = System.Drawing.Color.AliceBlue;
            this.txtDividendPercent.ForeColor = System.Drawing.Color.Black;
            this.txtDividendPercent.Location = new System.Drawing.Point(123, 101);
            this.txtDividendPercent.Name = "txtDividendPercent";
            this.txtDividendPercent.ReadOnly = true;
            this.txtDividendPercent.Size = new System.Drawing.Size(211, 20);
            this.txtDividendPercent.TabIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(572, 233);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(73, 23);
            this.btnSave.TabIndex = 14;
            this.btnSave.TabStop = false;
            this.btnSave.Text = "Update";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gpDividendInfo
            // 
            this.gpDividendInfo.BackColor = System.Drawing.Color.MidnightBlue;
            this.gpDividendInfo.Controls.Add(this.ddlDividendInterval);
            this.gpDividendInfo.Controls.Add(this.label3);
            this.gpDividendInfo.Controls.Add(this.label2);
            this.gpDividendInfo.Controls.Add(this.txtCurrentPrice);
            this.gpDividendInfo.Controls.Add(this.txtExDividend);
            this.gpDividendInfo.Controls.Add(this.txtPayDate);
            this.gpDividendInfo.Controls.Add(this.lblOpenPrice);
            this.gpDividendInfo.Controls.Add(this.txtOpenPrice);
            this.gpDividendInfo.Controls.Add(this.txtMarketCap);
            this.gpDividendInfo.Controls.Add(this.lblDaysRange);
            this.gpDividendInfo.Controls.Add(this.txtDayRange);
            this.gpDividendInfo.Controls.Add(this.lbl52WeekMiddle);
            this.gpDividendInfo.Controls.Add(this.txt52WeekHigh);
            this.gpDividendInfo.Controls.Add(this.lbl52WeekRange);
            this.gpDividendInfo.Controls.Add(this.txt52WeekLow);
            this.gpDividendInfo.Controls.Add(this.lblPERatio);
            this.gpDividendInfo.Controls.Add(this.txtPERatio);
            this.gpDividendInfo.Controls.Add(this.lblPayDate);
            this.gpDividendInfo.Controls.Add(this.lblExDividend);
            this.gpDividendInfo.Controls.Add(this.ddlIndustry);
            this.gpDividendInfo.Controls.Add(this.lblMarketCap);
            this.gpDividendInfo.Controls.Add(this.btnSave);
            this.gpDividendInfo.Controls.Add(this.txtSymbol);
            this.gpDividendInfo.Controls.Add(this.label1);
            this.gpDividendInfo.Controls.Add(this.lblSymbol);
            this.gpDividendInfo.Controls.Add(this.txtDividendPercent);
            this.gpDividendInfo.Controls.Add(this.txtStockName);
            this.gpDividendInfo.Controls.Add(this.lblStockName);
            this.gpDividendInfo.Controls.Add(this.lblAnnualDividend);
            this.gpDividendInfo.Controls.Add(this.lblIndustry);
            this.gpDividendInfo.Controls.Add(this.txtAnnualDividend);
            this.gpDividendInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpDividendInfo.ForeColor = System.Drawing.Color.White;
            this.gpDividendInfo.Location = new System.Drawing.Point(5, 5);
            this.gpDividendInfo.Name = "gpDividendInfo";
            this.gpDividendInfo.Size = new System.Drawing.Size(681, 275);
            this.gpDividendInfo.TabIndex = 15;
            this.gpDividendInfo.TabStop = false;
            this.gpDividendInfo.Text = "Shares Info:";
            // 
            // ddlDividendInterval
            // 
            this.ddlDividendInterval.BackColor = System.Drawing.Color.AliceBlue;
            this.ddlDividendInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlDividendInterval.ForeColor = System.Drawing.Color.Black;
            this.ddlDividendInterval.FormattingEnabled = true;
            this.ddlDividendInterval.Items.AddRange(new object[] {
            "Monthly",
            "Quarterly",
            "Yearly"});
            this.ddlDividendInterval.Location = new System.Drawing.Point(123, 74);
            this.ddlDividendInterval.Name = "ddlDividendInterval";
            this.ddlDividendInterval.Size = new System.Drawing.Size(210, 21);
            this.ddlDividendInterval.TabIndex = 51;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 52;
            this.label3.Text = "Dividend Interval";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(367, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 50;
            this.label2.Text = "Current Price:";
            // 
            // txtCurrentPrice
            // 
            this.txtCurrentPrice.BackColor = System.Drawing.Color.AliceBlue;
            this.txtCurrentPrice.ForeColor = System.Drawing.Color.Black;
            this.txtCurrentPrice.Location = new System.Drawing.Point(458, 131);
            this.txtCurrentPrice.Name = "txtCurrentPrice";
            this.txtCurrentPrice.ReadOnly = true;
            this.txtCurrentPrice.Size = new System.Drawing.Size(214, 20);
            this.txtCurrentPrice.TabIndex = 49;
            // 
            // txtExDividend
            // 
            this.txtExDividend.BackColor = System.Drawing.Color.AliceBlue;
            this.txtExDividend.ForeColor = System.Drawing.Color.Black;
            this.txtExDividend.Location = new System.Drawing.Point(123, 154);
            this.txtExDividend.Name = "txtExDividend";
            this.txtExDividend.ReadOnly = true;
            this.txtExDividend.Size = new System.Drawing.Size(210, 20);
            this.txtExDividend.TabIndex = 6;
            // 
            // txtPayDate
            // 
            this.txtPayDate.BackColor = System.Drawing.Color.AliceBlue;
            this.txtPayDate.ForeColor = System.Drawing.Color.Black;
            this.txtPayDate.Location = new System.Drawing.Point(123, 180);
            this.txtPayDate.Name = "txtPayDate";
            this.txtPayDate.ReadOnly = true;
            this.txtPayDate.Size = new System.Drawing.Size(211, 20);
            this.txtPayDate.TabIndex = 48;
            // 
            // lblOpenPrice
            // 
            this.lblOpenPrice.AutoSize = true;
            this.lblOpenPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOpenPrice.Location = new System.Drawing.Point(378, 108);
            this.lblOpenPrice.Name = "lblOpenPrice";
            this.lblOpenPrice.Size = new System.Drawing.Size(74, 13);
            this.lblOpenPrice.TabIndex = 46;
            this.lblOpenPrice.Text = "Open Price:";
            // 
            // txtOpenPrice
            // 
            this.txtOpenPrice.BackColor = System.Drawing.Color.AliceBlue;
            this.txtOpenPrice.ForeColor = System.Drawing.Color.Black;
            this.txtOpenPrice.Location = new System.Drawing.Point(458, 105);
            this.txtOpenPrice.Name = "txtOpenPrice";
            this.txtOpenPrice.ReadOnly = true;
            this.txtOpenPrice.Size = new System.Drawing.Size(214, 20);
            this.txtOpenPrice.TabIndex = 45;
            // 
            // txtMarketCap
            // 
            this.txtMarketCap.BackColor = System.Drawing.Color.AliceBlue;
            this.txtMarketCap.ForeColor = System.Drawing.Color.Black;
            this.txtMarketCap.Location = new System.Drawing.Point(458, 51);
            this.txtMarketCap.Name = "txtMarketCap";
            this.txtMarketCap.ReadOnly = true;
            this.txtMarketCap.Size = new System.Drawing.Size(214, 20);
            this.txtMarketCap.TabIndex = 3;
            // 
            // lblDaysRange
            // 
            this.lblDaysRange.AutoSize = true;
            this.lblDaysRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDaysRange.Location = new System.Drawing.Point(372, 184);
            this.lblDaysRange.Name = "lblDaysRange";
            this.lblDaysRange.Size = new System.Drawing.Size(80, 13);
            this.lblDaysRange.TabIndex = 39;
            this.lblDaysRange.Text = "Days Range:";
            // 
            // txtDayRange
            // 
            this.txtDayRange.BackColor = System.Drawing.Color.AliceBlue;
            this.txtDayRange.ForeColor = System.Drawing.Color.Black;
            this.txtDayRange.Location = new System.Drawing.Point(458, 181);
            this.txtDayRange.Name = "txtDayRange";
            this.txtDayRange.ReadOnly = true;
            this.txtDayRange.Size = new System.Drawing.Size(215, 20);
            this.txtDayRange.TabIndex = 38;
            // 
            // lbl52WeekMiddle
            // 
            this.lbl52WeekMiddle.AutoSize = true;
            this.lbl52WeekMiddle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl52WeekMiddle.Location = new System.Drawing.Point(562, 160);
            this.lbl52WeekMiddle.Name = "lbl52WeekMiddle";
            this.lbl52WeekMiddle.Size = new System.Drawing.Size(11, 13);
            this.lbl52WeekMiddle.TabIndex = 37;
            this.lbl52WeekMiddle.Text = "-";
            // 
            // txt52WeekHigh
            // 
            this.txt52WeekHigh.BackColor = System.Drawing.Color.AliceBlue;
            this.txt52WeekHigh.ForeColor = System.Drawing.Color.Black;
            this.txt52WeekHigh.Location = new System.Drawing.Point(579, 157);
            this.txt52WeekHigh.Name = "txt52WeekHigh";
            this.txt52WeekHigh.ReadOnly = true;
            this.txt52WeekHigh.Size = new System.Drawing.Size(93, 20);
            this.txt52WeekHigh.TabIndex = 36;
            // 
            // lbl52WeekRange
            // 
            this.lbl52WeekRange.AutoSize = true;
            this.lbl52WeekRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl52WeekRange.Location = new System.Drawing.Point(349, 158);
            this.lbl52WeekRange.Name = "lbl52WeekRange";
            this.lbl52WeekRange.Size = new System.Drawing.Size(103, 13);
            this.lbl52WeekRange.TabIndex = 35;
            this.lbl52WeekRange.Text = "52 Week Range:";
            // 
            // txt52WeekLow
            // 
            this.txt52WeekLow.BackColor = System.Drawing.Color.AliceBlue;
            this.txt52WeekLow.Location = new System.Drawing.Point(458, 157);
            this.txt52WeekLow.Name = "txt52WeekLow";
            this.txt52WeekLow.ReadOnly = true;
            this.txt52WeekLow.Size = new System.Drawing.Size(99, 20);
            this.txt52WeekLow.TabIndex = 34;
            // 
            // lblPERatio
            // 
            this.lblPERatio.AutoSize = true;
            this.lblPERatio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPERatio.Location = new System.Drawing.Point(385, 81);
            this.lblPERatio.Name = "lblPERatio";
            this.lblPERatio.Size = new System.Drawing.Size(67, 13);
            this.lblPERatio.TabIndex = 31;
            this.lblPERatio.Text = "P/E Ratio:";
            // 
            // txtPERatio
            // 
            this.txtPERatio.BackColor = System.Drawing.Color.AliceBlue;
            this.txtPERatio.ForeColor = System.Drawing.Color.Black;
            this.txtPERatio.Location = new System.Drawing.Point(458, 78);
            this.txtPERatio.Name = "txtPERatio";
            this.txtPERatio.ReadOnly = true;
            this.txtPERatio.Size = new System.Drawing.Size(214, 20);
            this.txtPERatio.TabIndex = 30;
            // 
            // lblPayDate
            // 
            this.lblPayDate.AutoSize = true;
            this.lblPayDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPayDate.Location = new System.Drawing.Point(47, 186);
            this.lblPayDate.Name = "lblPayDate";
            this.lblPayDate.Size = new System.Drawing.Size(63, 13);
            this.lblPayDate.TabIndex = 29;
            this.lblPayDate.Text = "Pay Date:";
            // 
            // lblExDividend
            // 
            this.lblExDividend.AutoSize = true;
            this.lblExDividend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExDividend.Location = new System.Drawing.Point(30, 160);
            this.lblExDividend.Name = "lblExDividend";
            this.lblExDividend.Size = new System.Drawing.Size(79, 13);
            this.lblExDividend.TabIndex = 22;
            this.lblExDividend.Text = "Ex-Dividend:";
            // 
            // ddlIndustry
            // 
            this.ddlIndustry.BackColor = System.Drawing.Color.AliceBlue;
            this.ddlIndustry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlIndustry.ForeColor = System.Drawing.Color.Black;
            this.ddlIndustry.FormattingEnabled = true;
            this.ddlIndustry.Items.AddRange(new object[] {
            "Consumer Discretionary",
            "Consumer Staples",
            "Energy",
            "Financials",
            "Health Care",
            "Industrials",
            "Information Technology",
            "Materials",
            "Telecommunication Services",
            "Utilities",
            "Equity Precious Metals",
            "Other"});
            this.ddlIndustry.Location = new System.Drawing.Point(123, 47);
            this.ddlIndustry.Name = "ddlIndustry";
            this.ddlIndustry.Size = new System.Drawing.Size(210, 21);
            this.ddlIndustry.TabIndex = 2;
            // 
            // lblMarketCap
            // 
            this.lblMarketCap.AutoSize = true;
            this.lblMarketCap.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarketCap.Location = new System.Drawing.Point(376, 54);
            this.lblMarketCap.Name = "lblMarketCap";
            this.lblMarketCap.Size = new System.Drawing.Size(76, 13);
            this.lblMarketCap.TabIndex = 17;
            this.lblMarketCap.Text = "Market Cap:";
            // 
            // btnDividendPrice
            // 
            this.btnDividendPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDividendPrice.ForeColor = System.Drawing.Color.Black;
            this.btnDividendPrice.Location = new System.Drawing.Point(208, 33);
            this.btnDividendPrice.Name = "btnDividendPrice";
            this.btnDividendPrice.Size = new System.Drawing.Size(129, 23);
            this.btnDividendPrice.TabIndex = 18;
            this.btnDividendPrice.TabStop = false;
            this.btnDividendPrice.Text = "Get Dividend Price";
            this.btnDividendPrice.UseVisualStyleBackColor = true;
            this.btnDividendPrice.Click += new System.EventHandler(this.btnDividendPrice_Click);
            // 
            // btnGetSharePrice
            // 
            this.btnGetSharePrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetSharePrice.ForeColor = System.Drawing.Color.Black;
            this.btnGetSharePrice.Location = new System.Drawing.Point(127, 33);
            this.btnGetSharePrice.Name = "btnGetSharePrice";
            this.btnGetSharePrice.Size = new System.Drawing.Size(75, 23);
            this.btnGetSharePrice.TabIndex = 15;
            this.btnGetSharePrice.TabStop = false;
            this.btnGetSharePrice.Text = "Get Price";
            this.btnGetSharePrice.UseVisualStyleBackColor = true;
            this.btnGetSharePrice.Click += new System.EventHandler(this.btnGetSharePrice_Click);
            // 
            // ddlSharePurchaseDate
            // 
            this.ddlSharePurchaseDate.BackColor = System.Drawing.Color.AliceBlue;
            this.ddlSharePurchaseDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlSharePurchaseDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlSharePurchaseDate.ForeColor = System.Drawing.Color.Black;
            this.ddlSharePurchaseDate.FormattingEnabled = true;
            this.ddlSharePurchaseDate.Location = new System.Drawing.Point(127, 66);
            this.ddlSharePurchaseDate.Name = "ddlSharePurchaseDate";
            this.ddlSharePurchaseDate.Size = new System.Drawing.Size(210, 21);
            this.ddlSharePurchaseDate.TabIndex = 6;
            this.ddlSharePurchaseDate.TabStop = false;
            this.ddlSharePurchaseDate.SelectedIndexChanged += new System.EventHandler(this.ddlSharePurchaseDate_SelectedIndexChanged);
            // 
            // lblSharePurchaseDate
            // 
            this.lblSharePurchaseDate.AutoSize = true;
            this.lblSharePurchaseDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSharePurchaseDate.Location = new System.Drawing.Point(15, 69);
            this.lblSharePurchaseDate.Name = "lblSharePurchaseDate";
            this.lblSharePurchaseDate.Size = new System.Drawing.Size(92, 13);
            this.lblSharePurchaseDate.TabIndex = 20;
            this.lblSharePurchaseDate.Text = "First Purchase:";
            // 
            // btnNewShares
            // 
            this.btnNewShares.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewShares.ForeColor = System.Drawing.Color.Black;
            this.btnNewShares.Location = new System.Drawing.Point(40, 173);
            this.btnNewShares.Name = "btnNewShares";
            this.btnNewShares.Size = new System.Drawing.Size(94, 23);
            this.btnNewShares.TabIndex = 21;
            this.btnNewShares.TabStop = false;
            this.btnNewShares.Text = "New Share";
            this.btnNewShares.UseVisualStyleBackColor = true;
            this.btnNewShares.Click += new System.EventHandler(this.btnNewShares_Click);
            // 
            // btnEditShares
            // 
            this.btnEditShares.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditShares.ForeColor = System.Drawing.Color.Black;
            this.btnEditShares.Location = new System.Drawing.Point(140, 173);
            this.btnEditShares.Name = "btnEditShares";
            this.btnEditShares.Size = new System.Drawing.Size(92, 23);
            this.btnEditShares.TabIndex = 22;
            this.btnEditShares.TabStop = false;
            this.btnEditShares.Text = "Edit Share";
            this.btnEditShares.UseVisualStyleBackColor = true;
            this.btnEditShares.Click += new System.EventHandler(this.btnEditShares_Click);
            // 
            // btnDeleteShares
            // 
            this.btnDeleteShares.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteShares.ForeColor = System.Drawing.Color.Black;
            this.btnDeleteShares.Location = new System.Drawing.Point(238, 173);
            this.btnDeleteShares.Name = "btnDeleteShares";
            this.btnDeleteShares.Size = new System.Drawing.Size(99, 23);
            this.btnDeleteShares.TabIndex = 23;
            this.btnDeleteShares.TabStop = false;
            this.btnDeleteShares.Text = "Delete Share";
            this.btnDeleteShares.UseVisualStyleBackColor = true;
            this.btnDeleteShares.Click += new System.EventHandler(this.btnDeleteShares_Click);
            // 
            // gpSharesOptions
            // 
            this.gpSharesOptions.BackColor = System.Drawing.Color.MidnightBlue;
            this.gpSharesOptions.Controls.Add(this.btnDeleteShares);
            this.gpSharesOptions.Controls.Add(this.btnDividendPrice);
            this.gpSharesOptions.Controls.Add(this.btnEditShares);
            this.gpSharesOptions.Controls.Add(this.ddlSharePurchaseDate);
            this.gpSharesOptions.Controls.Add(this.btnGetSharePrice);
            this.gpSharesOptions.Controls.Add(this.btnNewShares);
            this.gpSharesOptions.Controls.Add(this.txtSharePrice);
            this.gpSharesOptions.Controls.Add(this.txtNumberOfShares);
            this.gpSharesOptions.Controls.Add(this.lblNumberShare);
            this.gpSharesOptions.Controls.Add(this.lblSharePurchaseDate);
            this.gpSharesOptions.Controls.Add(this.lblSharePrice);
            this.gpSharesOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpSharesOptions.ForeColor = System.Drawing.Color.White;
            this.gpSharesOptions.Location = new System.Drawing.Point(692, 12);
            this.gpSharesOptions.Name = "gpSharesOptions";
            this.gpSharesOptions.Size = new System.Drawing.Size(343, 204);
            this.gpSharesOptions.TabIndex = 21;
            this.gpSharesOptions.TabStop = false;
            this.gpSharesOptions.Text = "Shares Options:";
            // 
            // Dividends
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(1047, 292);
            this.Controls.Add(this.gpSharesOptions);
            this.Controls.Add(this.gpDividendInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Dividends";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Dividend Stock";
            this.Load += new System.EventHandler(this.Dividends_Load);
            this.gpDividendInfo.ResumeLayout(false);
            this.gpDividendInfo.PerformLayout();
            this.gpSharesOptions.ResumeLayout(false);
            this.gpSharesOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtSymbol;
        private System.Windows.Forms.Label lblSymbol;
        private System.Windows.Forms.Label lblStockName;
        private System.Windows.Forms.TextBox txtStockName;
        private System.Windows.Forms.Label lblSharePrice;
        private System.Windows.Forms.TextBox txtSharePrice;
        private System.Windows.Forms.Label lblIndustry;
        private System.Windows.Forms.Label lblNumberShare;
        private System.Windows.Forms.TextBox txtNumberOfShares;
        private System.Windows.Forms.Label lblAnnualDividend;
        private System.Windows.Forms.TextBox txtAnnualDividend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDividendPercent;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox gpDividendInfo;
        private System.Windows.Forms.Button btnGetSharePrice;
        private System.Windows.Forms.Label lblMarketCap;
        private System.Windows.Forms.Button btnDividendPrice;
        private System.Windows.Forms.Label lblSharePurchaseDate;
        private System.Windows.Forms.ComboBox ddlSharePurchaseDate;
        private System.Windows.Forms.Button btnEditShares;
        private System.Windows.Forms.Button btnNewShares;
        private System.Windows.Forms.Button btnDeleteShares;
        private System.Windows.Forms.GroupBox gpSharesOptions;
        private System.Windows.Forms.ComboBox ddlIndustry;
        private System.Windows.Forms.Label lblExDividend;
        private System.Windows.Forms.Label lblPayDate;
        private System.Windows.Forms.Label lblPERatio;
        private System.Windows.Forms.TextBox txtPERatio;
        private System.Windows.Forms.Label lbl52WeekMiddle;
        private System.Windows.Forms.TextBox txt52WeekHigh;
        private System.Windows.Forms.Label lbl52WeekRange;
        private System.Windows.Forms.TextBox txt52WeekLow;
        private System.Windows.Forms.Label lblDaysRange;
        private System.Windows.Forms.TextBox txtDayRange;
        private System.Windows.Forms.TextBox txtMarketCap;
        private System.Windows.Forms.Label lblOpenPrice;
        private System.Windows.Forms.TextBox txtOpenPrice;
        private System.Windows.Forms.TextBox txtExDividend;
        private System.Windows.Forms.TextBox txtPayDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCurrentPrice;
        private System.Windows.Forms.ComboBox ddlDividendInterval;
        private System.Windows.Forms.Label label3;
    }
}