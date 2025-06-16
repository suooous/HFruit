namespace appfruit
{
    partial class DeleteFruitUserControl
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
            this.labelTitle = new System.Windows.Forms.Label();
            this.flowLayoutPanelFruits = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTitle.Location = new System.Drawing.Point(20, 20);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(144, 31);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "🗑️ 删除水果";
            // 
            // flowLayoutPanelFruits
            // 
            this.flowLayoutPanelFruits.AutoScroll = true;
            this.flowLayoutPanelFruits.Location = new System.Drawing.Point(20, 70);
            this.flowLayoutPanelFruits.Name = "flowLayoutPanelFruits";
            this.flowLayoutPanelFruits.Size = new System.Drawing.Size(740, 360);
            this.flowLayoutPanelFruits.TabIndex = 1;
            this.flowLayoutPanelFruits.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanelFruits_Paint);
            // 
            // DeleteFruitUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanelFruits);
            this.Controls.Add(this.labelTitle);
            this.Name = "DeleteFruitUserControl";
            this.Size = new System.Drawing.Size(780, 450);
            this.Load += new System.EventHandler(this.DeleteFruitUserControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelFruits;
    }
}