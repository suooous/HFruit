using System;
using System.Windows.Forms;
using System.Drawing;

namespace appfruit
{
    partial class Customhomepage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel sidebarPanel;
        private System.Windows.Forms.FlowLayoutPanel sidebarFlowPanel;
        private System.Windows.Forms.Button btnShoppingCart;
        private System.Windows.Forms.Button btnStatistics;
        private System.Windows.Forms.Button btnAI_QA;
        private System.Windows.Forms.Button btnBrowseFruits;

        private System.Windows.Forms.Panel mainContentPanel;

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
            this.sidebarPanel = new System.Windows.Forms.Panel();
            this.sidebarFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnBrowseFruits = new System.Windows.Forms.Button();
            this.btnShoppingCart = new System.Windows.Forms.Button();
            this.btnStatistics = new System.Windows.Forms.Button();
            this.btnAI_QA = new System.Windows.Forms.Button();
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
            this.sidebarPanel.Size = new System.Drawing.Size(120, 450);
            this.sidebarPanel.TabIndex = 0;
            // 
            // sidebarFlowPanel
            // 
            this.sidebarFlowPanel.Controls.Add(this.btnBrowseFruits);
            this.sidebarFlowPanel.Controls.Add(this.btnShoppingCart);
            this.sidebarFlowPanel.Controls.Add(this.btnStatistics);
            this.sidebarFlowPanel.Controls.Add(this.btnAI_QA);
            this.sidebarFlowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sidebarFlowPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.sidebarFlowPanel.Location = new System.Drawing.Point(0, 0);
            this.sidebarFlowPanel.Name = "sidebarFlowPanel";
            this.sidebarFlowPanel.Padding = new System.Windows.Forms.Padding(10);
            this.sidebarFlowPanel.Size = new System.Drawing.Size(120, 450);
            this.sidebarFlowPanel.TabIndex = 0;
            this.sidebarFlowPanel.WrapContents = false;
            this.sidebarFlowPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.sidebarFlowPanel_Paint);
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
            // btnShoppingCart
            // 
            this.btnShoppingCart.BackColor = System.Drawing.Color.SkyBlue;
            this.btnShoppingCart.FlatAppearance.BorderSize = 0;
            this.btnShoppingCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShoppingCart.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnShoppingCart.ForeColor = System.Drawing.Color.White;
            this.btnShoppingCart.Location = new System.Drawing.Point(13, 59);
            this.btnShoppingCart.Name = "btnShoppingCart";
            this.btnShoppingCart.Size = new System.Drawing.Size(94, 40);
            this.btnShoppingCart.TabIndex = 1;
            this.btnShoppingCart.Text = "🛒 购物车";
            this.btnShoppingCart.UseVisualStyleBackColor = false;
            this.btnShoppingCart.Click += new System.EventHandler(this.btnShoppingCart_Click);
            // 
            // btnStatistics
            // 
            this.btnStatistics.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnStatistics.FlatAppearance.BorderSize = 0;
            this.btnStatistics.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStatistics.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStatistics.ForeColor = System.Drawing.Color.White;
            this.btnStatistics.Location = new System.Drawing.Point(13, 105);
            this.btnStatistics.Name = "btnStatistics";
            this.btnStatistics.Size = new System.Drawing.Size(94, 40);
            this.btnStatistics.TabIndex = 2;
            this.btnStatistics.Text = "📊 统计情况";
            this.btnStatistics.UseVisualStyleBackColor = false;
            this.btnStatistics.Click += new System.EventHandler(this.btnStatistics_Click);
            // 
            // btnAI_QA
            // 
            this.btnAI_QA.BackColor = System.Drawing.Color.SlateBlue;
            this.btnAI_QA.FlatAppearance.BorderSize = 0;
            this.btnAI_QA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAI_QA.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAI_QA.ForeColor = System.Drawing.Color.White;
            this.btnAI_QA.Location = new System.Drawing.Point(13, 151);
            this.btnAI_QA.Name = "btnAI_QA";
            this.btnAI_QA.Size = new System.Drawing.Size(94, 40);
            this.btnAI_QA.TabIndex = 3;
            this.btnAI_QA.Text = "🤖 AI问答";
            this.btnAI_QA.UseVisualStyleBackColor = false;
            this.btnAI_QA.Click += new System.EventHandler(this.btnAI_QA_Click);
            // 
            // mainContentPanel
            // 
            this.mainContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContentPanel.Location = new System.Drawing.Point(120, 0);
            this.mainContentPanel.Name = "mainContentPanel";
            this.mainContentPanel.Size = new System.Drawing.Size(680, 450);
            this.mainContentPanel.TabIndex = 1;
            this.mainContentPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mainContentPanel_Paint);
            // 
            // Customhomepage
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mainContentPanel);
            this.Controls.Add(this.sidebarPanel);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Customhomepage";
            this.Text = "Welcome come to HFruit";
            this.Load += new System.EventHandler(this.Customhomepage_Load);
            this.sidebarPanel.ResumeLayout(false);
            this.sidebarFlowPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}