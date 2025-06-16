namespace appfruit
{
    partial class AdminDashboardForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel sidebarPanel;
        private System.Windows.Forms.FlowLayoutPanel sidebarFlowPanel;
        private System.Windows.Forms.Button btnBrowseFruits; // 水果浏览按钮
        private System.Windows.Forms.Button btnAddFruit;      // 增加水果按钮
        private System.Windows.Forms.Button btnSalesStatistics; // 销售统计按钮
        private System.Windows.Forms.Button btnDeleteFruit;     // 水果删除按钮
        private System.Windows.Forms.Button btnManageCustomers; // 顾客管理按钮
        private System.Windows.Forms.Button btnAIQA; // AI QA 按钮
        private System.Windows.Forms.Panel mainContentPanel; // 主内容面板

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

        #region Windows Form Designer generated code

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.sidebarPanel = new System.Windows.Forms.Panel();
            this.sidebarFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnBrowseFruits = new System.Windows.Forms.Button();
            this.btnAddFruit = new System.Windows.Forms.Button();
            this.btnSalesStatistics = new System.Windows.Forms.Button();
            this.btnDeleteFruit = new System.Windows.Forms.Button();
            this.btnManageCustomers = new System.Windows.Forms.Button();
            this.btnAIQA = new System.Windows.Forms.Button();
            this.mainContentPanel = new System.Windows.Forms.Panel();
            this.sidebarPanel.SuspendLayout();
            this.sidebarFlowPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // sidebarPanel
            // 
            this.sidebarPanel.BackColor = System.Drawing.Color.LightGray;
            this.sidebarPanel.Controls.Add(this.sidebarFlowPanel);
            this.sidebarPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebarPanel.Location = new System.Drawing.Point(0, 0);
            this.sidebarPanel.Name = "sidebarPanel";
            this.sidebarPanel.Size = new System.Drawing.Size(120, 486);
            this.sidebarPanel.TabIndex = 0;
            // 
            // sidebarFlowPanel
            // 
            this.sidebarFlowPanel.Controls.Add(this.btnBrowseFruits);
            this.sidebarFlowPanel.Controls.Add(this.btnAddFruit);
            this.sidebarFlowPanel.Controls.Add(this.btnSalesStatistics);
            this.sidebarFlowPanel.Controls.Add(this.btnDeleteFruit);
            this.sidebarFlowPanel.Controls.Add(this.btnManageCustomers);
            this.sidebarFlowPanel.Controls.Add(this.btnAIQA);
            this.sidebarFlowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sidebarFlowPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.sidebarFlowPanel.Location = new System.Drawing.Point(0, 0);
            this.sidebarFlowPanel.Name = "sidebarFlowPanel";
            this.sidebarFlowPanel.Padding = new System.Windows.Forms.Padding(10);
            this.sidebarFlowPanel.Size = new System.Drawing.Size(120, 486);
            this.sidebarFlowPanel.TabIndex = 0;
            this.sidebarFlowPanel.WrapContents = false;
            // 
            // btnBrowseFruits
            // 
            this.btnBrowseFruits.BackColor = System.Drawing.Color.Orange;
            this.btnBrowseFruits.FlatAppearance.BorderSize = 0;
            this.btnBrowseFruits.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseFruits.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnBrowseFruits.ForeColor = System.Drawing.Color.White;
            this.btnBrowseFruits.Location = new System.Drawing.Point(13, 13);
            this.btnBrowseFruits.Name = "btnBrowseFruits";
            this.btnBrowseFruits.Size = new System.Drawing.Size(94, 40);
            this.btnBrowseFruits.TabIndex = 0;
            this.btnBrowseFruits.Text = "🍎 水果浏览";
            this.btnBrowseFruits.UseVisualStyleBackColor = false;
            this.btnBrowseFruits.Click += new System.EventHandler(this.btnBrowseFruits_Click);
            // 
            // btnAddFruit
            // 
            this.btnAddFruit.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnAddFruit.FlatAppearance.BorderSize = 0;
            this.btnAddFruit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddFruit.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddFruit.ForeColor = System.Drawing.Color.White;
            this.btnAddFruit.Location = new System.Drawing.Point(13, 59);
            this.btnAddFruit.Name = "btnAddFruit";
            this.btnAddFruit.Size = new System.Drawing.Size(94, 40);
            this.btnAddFruit.TabIndex = 1;
            this.btnAddFruit.Text = "➕ 增加水果";
            this.btnAddFruit.UseVisualStyleBackColor = false;
            this.btnAddFruit.Click += new System.EventHandler(this.btnAddFruit_Click);
            // 
            // btnSalesStatistics
            // 
            this.btnSalesStatistics.BackColor = System.Drawing.Color.SlateBlue;
            this.btnSalesStatistics.FlatAppearance.BorderSize = 0;
            this.btnSalesStatistics.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalesStatistics.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSalesStatistics.ForeColor = System.Drawing.Color.White;
            this.btnSalesStatistics.Location = new System.Drawing.Point(13, 105);
            this.btnSalesStatistics.Name = "btnSalesStatistics";
            this.btnSalesStatistics.Size = new System.Drawing.Size(94, 40);
            this.btnSalesStatistics.TabIndex = 2;
            this.btnSalesStatistics.Text = "📈 销售统计";
            this.btnSalesStatistics.UseVisualStyleBackColor = false;
            this.btnSalesStatistics.Click += new System.EventHandler(this.btnSalesStatistics_Click);
            // 
            // btnDeleteFruit
            // 
            this.btnDeleteFruit.BackColor = System.Drawing.Color.IndianRed;
            this.btnDeleteFruit.FlatAppearance.BorderSize = 0;
            this.btnDeleteFruit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteFruit.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDeleteFruit.ForeColor = System.Drawing.Color.White;
            this.btnDeleteFruit.Location = new System.Drawing.Point(13, 151);
            this.btnDeleteFruit.Name = "btnDeleteFruit";
            this.btnDeleteFruit.Size = new System.Drawing.Size(94, 40);
            this.btnDeleteFruit.TabIndex = 3;
            this.btnDeleteFruit.Text = "🗑️ 删除水果";
            this.btnDeleteFruit.UseVisualStyleBackColor = false;
            this.btnDeleteFruit.Click += new System.EventHandler(this.btnDeleteFruit_Click);
            // 
            // btnManageCustomers
            // 
            this.btnManageCustomers.BackColor = System.Drawing.Color.DarkCyan;
            this.btnManageCustomers.FlatAppearance.BorderSize = 0;
            this.btnManageCustomers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageCustomers.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnManageCustomers.ForeColor = System.Drawing.Color.White;
            this.btnManageCustomers.Location = new System.Drawing.Point(13, 197);
            this.btnManageCustomers.Name = "btnManageCustomers";
            this.btnManageCustomers.Size = new System.Drawing.Size(94, 40);
            this.btnManageCustomers.TabIndex = 4;
            this.btnManageCustomers.Text = "👥 顾客管理";
            this.btnManageCustomers.UseVisualStyleBackColor = false;
            this.btnManageCustomers.Click += new System.EventHandler(this.btnManageCustomers_Click);
            // 
            // btnAIQA
            // 
            this.btnAIQA.BackColor = System.Drawing.Color.Purple;
            this.btnAIQA.FlatAppearance.BorderSize = 0;
            this.btnAIQA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAIQA.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAIQA.ForeColor = System.Drawing.Color.White;
            this.btnAIQA.Location = new System.Drawing.Point(13, 243);
            this.btnAIQA.Name = "btnAIQA";
            this.btnAIQA.Size = new System.Drawing.Size(94, 40);
            this.btnAIQA.TabIndex = 5;
            this.btnAIQA.Text = "🤖 AI 问答";
            this.btnAIQA.UseVisualStyleBackColor = false;
            this.btnAIQA.Click += new System.EventHandler(this.btnAIQA_Click);
            // 
            // mainContentPanel
            // 
            this.mainContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContentPanel.Location = new System.Drawing.Point(120, 0);
            this.mainContentPanel.Name = "mainContentPanel";
            this.mainContentPanel.Size = new System.Drawing.Size(898, 486);
            this.mainContentPanel.TabIndex = 1;
            this.mainContentPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mainContentPanel_Paint);
            // 
            // AdminDashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 486);
            this.Controls.Add(this.mainContentPanel);
            this.Controls.Add(this.sidebarPanel);
            this.Name = "AdminDashboardForm";
            this.Text = "管理员仪表盘";
            this.Load += new System.EventHandler(this.AdminDashboardForm_Load);
            this.sidebarPanel.ResumeLayout(false);
            this.sidebarFlowPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}