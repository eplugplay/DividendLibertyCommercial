namespace DividendLiberty
{
    partial class Shares
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
            this.gpShares = new System.Windows.Forms.GroupBox();
            this.dtpPurchaseDate = new System.Windows.Forms.DateTimePicker();
            this.lblPurchaseDate = new System.Windows.Forms.Label();
            this.btnGetPrice = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtPurchasePrice = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNumberOfShares = new System.Windows.Forms.TextBox();
            this.lblSymbol = new System.Windows.Forms.Label();
            this.gpShares.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpShares
            // 
            this.gpShares.BackColor = System.Drawing.Color.MidnightBlue;
            this.gpShares.Controls.Add(this.dtpPurchaseDate);
            this.gpShares.Controls.Add(this.lblPurchaseDate);
            this.gpShares.Controls.Add(this.btnGetPrice);
            this.gpShares.Controls.Add(this.btnSave);
            this.gpShares.Controls.Add(this.txtPurchasePrice);
            this.gpShares.Controls.Add(this.label1);
            this.gpShares.Controls.Add(this.txtNumberOfShares);
            this.gpShares.Controls.Add(this.lblSymbol);
            this.gpShares.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpShares.ForeColor = System.Drawing.Color.White;
            this.gpShares.Location = new System.Drawing.Point(12, 12);
            this.gpShares.Name = "gpShares";
            this.gpShares.Size = new System.Drawing.Size(421, 107);
            this.gpShares.TabIndex = 0;
            this.gpShares.TabStop = false;
            this.gpShares.Text = "Shares";
            // 
            // dtpPurchaseDate
            // 
            this.dtpPurchaseDate.CalendarMonthBackground = System.Drawing.Color.AliceBlue;
            this.dtpPurchaseDate.Location = new System.Drawing.Point(108, 19);
            this.dtpPurchaseDate.Name = "dtpPurchaseDate";
            this.dtpPurchaseDate.Size = new System.Drawing.Size(233, 20);
            this.dtpPurchaseDate.TabIndex = 1;
            // 
            // lblPurchaseDate
            // 
            this.lblPurchaseDate.AutoSize = true;
            this.lblPurchaseDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPurchaseDate.Location = new System.Drawing.Point(7, 25);
            this.lblPurchaseDate.Name = "lblPurchaseDate";
            this.lblPurchaseDate.Size = new System.Drawing.Size(95, 13);
            this.lblPurchaseDate.TabIndex = 24;
            this.lblPurchaseDate.Text = "Purchase Date:";
            // 
            // btnGetPrice
            // 
            this.btnGetPrice.ForeColor = System.Drawing.Color.Black;
            this.btnGetPrice.Location = new System.Drawing.Point(355, 44);
            this.btnGetPrice.Name = "btnGetPrice";
            this.btnGetPrice.Size = new System.Drawing.Size(56, 23);
            this.btnGetPrice.TabIndex = 20;
            this.btnGetPrice.TabStop = false;
            this.btnGetPrice.Text = "Price";
            this.btnGetPrice.UseVisualStyleBackColor = true;
            this.btnGetPrice.Click += new System.EventHandler(this.btnGetPrice_Click);
            // 
            // btnSave
            // 
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(355, 70);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(56, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.TabStop = false;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtPurchasePrice
            // 
            this.txtPurchasePrice.BackColor = System.Drawing.Color.AliceBlue;
            this.txtPurchasePrice.Location = new System.Drawing.Point(108, 45);
            this.txtPurchasePrice.Name = "txtPurchasePrice";
            this.txtPurchasePrice.Size = new System.Drawing.Size(233, 20);
            this.txtPurchasePrice.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Purchase Price:";
            // 
            // txtNumberOfShares
            // 
            this.txtNumberOfShares.BackColor = System.Drawing.Color.AliceBlue;
            this.txtNumberOfShares.Location = new System.Drawing.Point(108, 71);
            this.txtNumberOfShares.Name = "txtNumberOfShares";
            this.txtNumberOfShares.Size = new System.Drawing.Size(233, 20);
            this.txtNumberOfShares.TabIndex = 3;
            // 
            // lblSymbol
            // 
            this.lblSymbol.AutoSize = true;
            this.lblSymbol.Location = new System.Drawing.Point(26, 77);
            this.lblSymbol.Name = "lblSymbol";
            this.lblSymbol.Size = new System.Drawing.Size(73, 13);
            this.lblSymbol.TabIndex = 3;
            this.lblSymbol.Text = "# of Shares";
            // 
            // Shares
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(443, 126);
            this.Controls.Add(this.gpShares);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Shares";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Shares";
            this.Load += new System.EventHandler(this.Shares_Load);
            this.gpShares.ResumeLayout(false);
            this.gpShares.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpShares;
        private System.Windows.Forms.TextBox txtPurchasePrice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNumberOfShares;
        private System.Windows.Forms.Label lblSymbol;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnGetPrice;
        private System.Windows.Forms.DateTimePicker dtpPurchaseDate;
        private System.Windows.Forms.Label lblPurchaseDate;
    }
}