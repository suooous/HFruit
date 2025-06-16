using System.Drawing; // **重要：确保此行存在且正确**
using System.Windows.Forms; // 通常已存在

namespace appfruit
{
    partial class ShoppingCartUserControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvShoppingCart;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Button btnPurchase;
        private System.Windows.Forms.Button btnClearCart; // 新增清除购物车按钮

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
            this.dgvShoppingCart = new System.Windows.Forms.DataGridView();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.btnPurchase = new System.Windows.Forms.Button();
            this.btnClearCart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShoppingCart)).BeginInit();
            this.SuspendLayout();
            //
            // dgvShoppingCart
            //
            this.dgvShoppingCart.AllowUserToAddRows = false;
            this.dgvShoppingCart.AllowUserToDeleteRows = false;
            this.dgvShoppingCart.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvShoppingCart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShoppingCart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvShoppingCart.Location = new System.Drawing.Point(0, 0);
            this.dgvShoppingCart.Name = "dgvShoppingCart";
            this.dgvShoppingCart.ReadOnly = true;
            this.dgvShoppingCart.RowHeadersWidth = 51;
            this.dgvShoppingCart.RowTemplate.Height = 27;
            this.dgvShoppingCart.Size = new System.Drawing.Size(680, 350); // 根据需要调整大小
            this.dgvShoppingCart.TabIndex = 0;
            // 添加这一行来处理单元格点击事件，用于删除或修改数量等操作
            this.dgvShoppingCart.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvShoppingCart_CellContentClick);
            //
            // lblTotalAmount
            //
            this.lblTotalAmount.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTotalAmount.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotalAmount.Location = new System.Drawing.Point(0, 350); // 位于 DGV 下方
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.lblTotalAmount.Size = new System.Drawing.Size(680, 50); // 调整高度
            this.lblTotalAmount.TabIndex = 1;
            this.lblTotalAmount.Text = "总计: $0.00";
            // **核心修改：将 System.Windows.Forms.ContentAlignment 更改为 System.Drawing.ContentAlignment**
            this.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // btnPurchase
            //
            this.btnPurchase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPurchase.Location = new System.Drawing.Point(580, 410); // 调整位置
            this.btnPurchase.Name = "btnPurchase";
            this.btnPurchase.Size = new System.Drawing.Size(90, 30);
            this.btnPurchase.TabIndex = 2;
            this.btnPurchase.Text = "立即购买";
            this.btnPurchase.UseVisualStyleBackColor = true;
            this.btnPurchase.Click += new System.EventHandler(this.btnPurchase_Click);
            //
            // btnClearCart
            //
            this.btnClearCart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearCart.Location = new System.Drawing.Point(480, 410); // 位于购买按钮旁边
            this.btnClearCart.Name = "btnClearCart";
            this.btnClearCart.Size = new System.Drawing.Size(90, 30);
            this.btnClearCart.TabIndex = 3;
            this.btnClearCart.Text = "清空购物车";
            this.btnClearCart.UseVisualStyleBackColor = true;
            this.btnClearCart.Click += new System.EventHandler(this.btnClearCart_Click);
            //
            // ShoppingCartUserControl
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            // 确保按钮添加到控件集合中，并且顺序合理
            this.Controls.Add(this.btnPurchase);
            this.Controls.Add(this.btnClearCart);
            this.Controls.Add(this.lblTotalAmount); // 先添加标签，确保它在按钮后面，这样按钮可以覆盖它的一部分
            this.Controls.Add(this.dgvShoppingCart);
            this.Name = "ShoppingCartUserControl";
            this.Size = new System.Drawing.Size(680, 450);
            this.Load += new System.EventHandler(this.ShoppingCartUserControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShoppingCart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}