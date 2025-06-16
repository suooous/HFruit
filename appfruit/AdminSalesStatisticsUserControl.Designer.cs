namespace appfruit
{
    partial class AdminSalesStatisticsUserControl
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartSalesStatistics = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartSalesStatistics)).BeginInit();
            this.SuspendLayout();
            // 
            // chartSalesStatistics
            // 
            chartArea1.Name = "ChartArea1";
            this.chartSalesStatistics.ChartAreas.Add(chartArea1);
            this.chartSalesStatistics.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartSalesStatistics.Legends.Add(legend1);
            this.chartSalesStatistics.Location = new System.Drawing.Point(0, 0);
            this.chartSalesStatistics.Name = "chartSalesStatistics";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartSalesStatistics.Series.Add(series1);
            this.chartSalesStatistics.Size = new System.Drawing.Size(600, 400);
            this.chartSalesStatistics.TabIndex = 0;
            this.chartSalesStatistics.Text = "销售统计图表";
            this.chartSalesStatistics.Click += new System.EventHandler(this.chartSalesStatistics_Click);
            // 
            // AdminSalesStatisticsUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chartSalesStatistics);
            this.Name = "AdminSalesStatisticsUserControl";
            this.Size = new System.Drawing.Size(600, 400);
            this.Load += new System.EventHandler(this.AdminSalesStatisticsUserControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartSalesStatistics)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartSalesStatistics;
    }
}