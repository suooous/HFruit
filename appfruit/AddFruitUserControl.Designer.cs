namespace appfruit
{
    partial class AddFruitUserControl
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.lblStock = new System.Windows.Forms.Label(); // Changed from lblQuantity to lblStock
            this.txtStock = new System.Windows.Forms.TextBox(); // Changed from txtQuantity to txtStock
            this.lblImageUrl = new System.Windows.Forms.Label(); // Changed from lblDescription to lblImageUrl
            this.txtImageUrl = new System.Windows.Forms.TextBox(); // Changed from txtDescription/txtImagePath to txtImageUrl
            this.btnAdd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.Location = new System.Drawing.Point(30, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(204, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "增加水果信息";
            //
            // lblName
            //
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(30, 90);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(82, 15);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "水果名称：";
            //
            // txtName
            //
            this.txtName.Location = new System.Drawing.Point(120, 87);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(200, 25);
            this.txtName.TabIndex = 2;
            //
            // lblPrice
            //
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(30, 130);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(82, 15);
            this.lblPrice.TabIndex = 3;
            this.lblPrice.Text = "价格 (元)：";
            //
            // txtPrice
            //
            this.txtPrice.Location = new System.Drawing.Point(120, 127);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(200, 25);
            this.txtPrice.TabIndex = 4;
            //
            // lblStock
            //
            this.lblStock.AutoSize = true;
            this.lblStock.Location = new System.Drawing.Point(30, 170);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(82, 15);
            this.lblStock.TabIndex = 5;
            this.lblStock.Text = "库存数量："; // Changed label text
            //
            // txtStock
            //
            this.txtStock.Location = new System.Drawing.Point(120, 167);
            this.txtStock.Name = "txtStock";
            this.txtStock.Size = new System.Drawing.Size(200, 25);
            this.txtStock.TabIndex = 6;
            //
            // lblImageUrl
            //
            this.lblImageUrl.AutoSize = true;
            this.lblImageUrl.Location = new System.Drawing.Point(30, 210); // Adjusted position
            this.lblImageUrl.Name = "lblImageUrl";
            this.lblImageUrl.Size = new System.Drawing.Size(82, 15);
            this.lblImageUrl.TabIndex = 7;
            this.lblImageUrl.Text = "图片路径："; // Changed label text
            //
            // txtImageUrl
            //
            this.txtImageUrl.Location = new System.Drawing.Point(120, 207); // Adjusted position
            this.txtImageUrl.Name = "txtImageUrl";
            this.txtImageUrl.Size = new System.Drawing.Size(200, 25); // Changed to single line
            this.txtImageUrl.TabIndex = 8;
            //
            // btnAdd
            //
            this.btnAdd.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(120, 250); // Adjusted position
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 35);
            this.btnAdd.TabIndex = 9; // Adjusted tab index
            this.btnAdd.Text = "添加水果";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            //
            // AddFruitUserControl
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtImageUrl); // Changed from txtImagePath
            this.Controls.Add(this.lblImageUrl); // Changed from lblImagePath
            this.Controls.Add(this.txtStock); // Changed from txtQuantity
            this.Controls.Add(this.lblStock); // Changed from lblQuantity
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblTitle);
            this.Name = "AddFruitUserControl";
            this.Size = new System.Drawing.Size(600, 300); // Adjusted size as we have fewer fields
            this.Load += new System.EventHandler(this.AddFruitUserControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label lblStock; // Changed from lblQuantity
        private System.Windows.Forms.TextBox txtStock; // Changed from txtQuantity
        private System.Windows.Forms.Label lblImageUrl; // Changed from lblDescription
        private System.Windows.Forms.TextBox txtImageUrl; // Changed from txtDescription/txtImagePath
        private System.Windows.Forms.Button btnAdd;
    }
}