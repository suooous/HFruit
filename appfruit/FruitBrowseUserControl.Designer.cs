namespace appfruit
{
    partial class FruitBrowseUserControl
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.fruitBrowsePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblMinPrice = new System.Windows.Forms.Label();
            this.txtMinPrice = new System.Windows.Forms.TextBox();
            this.lblMaxPrice = new System.Windows.Forms.Label();
            this.txtMaxPrice = new System.Windows.Forms.TextBox();
            this.lblSearchName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // fruitBrowsePanel
            // 
            this.fruitBrowsePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fruitBrowsePanel.AutoScroll = true;
            this.fruitBrowsePanel.BackColor = System.Drawing.Color.White;
            this.fruitBrowsePanel.Location = new System.Drawing.Point(4, 46);
            this.fruitBrowsePanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.fruitBrowsePanel.Name = "fruitBrowsePanel";
            this.fruitBrowsePanel.Size = new System.Drawing.Size(844, 531);
            this.fruitBrowsePanel.TabIndex = 0;
            this.fruitBrowsePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.fruitBrowsePanel_Paint_1);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(533, 12);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(132, 25);
            this.txtSearch.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnSearch.Font = new System.Drawing.Font("华文中宋", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSearch.Location = new System.Drawing.Point(673, 7);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(115, 30);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "搜索";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblMinPrice
            // 
            this.lblMinPrice.AutoSize = true;
            this.lblMinPrice.Font = new System.Drawing.Font("华文中宋", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMinPrice.Location = new System.Drawing.Point(13, 15);
            this.lblMinPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMinPrice.Name = "lblMinPrice";
            this.lblMinPrice.Size = new System.Drawing.Size(73, 17);
            this.lblMinPrice.TabIndex = 3;
            this.lblMinPrice.Text = "最低价格:";
            // 
            // txtMinPrice
            // 
            this.txtMinPrice.Location = new System.Drawing.Point(100, 12);
            this.txtMinPrice.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtMinPrice.Name = "txtMinPrice";
            this.txtMinPrice.Size = new System.Drawing.Size(105, 25);
            this.txtMinPrice.TabIndex = 4;
            // 
            // lblMaxPrice
            // 
            this.lblMaxPrice.AutoSize = true;
            this.lblMaxPrice.Font = new System.Drawing.Font("华文中宋", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMaxPrice.Location = new System.Drawing.Point(227, 15);
            this.lblMaxPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaxPrice.Name = "lblMaxPrice";
            this.lblMaxPrice.Size = new System.Drawing.Size(73, 17);
            this.lblMaxPrice.TabIndex = 5;
            this.lblMaxPrice.Text = "最高价格:";
            // 
            // txtMaxPrice
            // 
            this.txtMaxPrice.Location = new System.Drawing.Point(313, 12);
            this.txtMaxPrice.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtMaxPrice.Name = "txtMaxPrice";
            this.txtMaxPrice.Size = new System.Drawing.Size(105, 25);
            this.txtMaxPrice.TabIndex = 6;
            // 
            // lblSearchName
            // 
            this.lblSearchName.AutoSize = true;
            this.lblSearchName.Font = new System.Drawing.Font("华文中宋", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSearchName.Location = new System.Drawing.Point(440, 15);
            this.lblSearchName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSearchName.Name = "lblSearchName";
            this.lblSearchName.Size = new System.Drawing.Size(73, 17);
            this.lblSearchName.TabIndex = 7;
            this.lblSearchName.Text = "搜索名称:";
            // 
            // FruitBrowseUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblSearchName);
            this.Controls.Add(this.txtMaxPrice);
            this.Controls.Add(this.lblMaxPrice);
            this.Controls.Add(this.txtMinPrice);
            this.Controls.Add(this.lblMinPrice);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.fruitBrowsePanel);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FruitBrowseUserControl";
            this.Size = new System.Drawing.Size(848, 577);
            this.Load += new System.EventHandler(this.FruitBrowseUserControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel fruitBrowsePanel;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblMinPrice;
        private System.Windows.Forms.TextBox txtMinPrice;
        private System.Windows.Forms.Label lblMaxPrice;
        private System.Windows.Forms.TextBox txtMaxPrice;
        private System.Windows.Forms.Label lblSearchName; // Declaration for the new label
    }
}