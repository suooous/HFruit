using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting; // 引入图表控件命名空间
using System.Configuration; // 用于访问 App.config 中的连接字符串
using System.Data.SqlClient; // 用于 SQL Server 数据库操作
using System.Collections.Generic; // 用于 Dictionary

namespace appfruit
{
    public partial class AdminSalesStatisticsUserControl : UserControl
    {
        public AdminSalesStatisticsUserControl()
        {
            InitializeComponent();
            // 在构造函数中不直接加载数据，等待 RefreshData() 方法调用
        }

        private void AdminSalesStatisticsUserControl_Load(object sender, EventArgs e)
        {
            RefreshData(); // 用户控件加载时刷新数据
        }

        /// <summary>
        /// 刷新销售统计数据，从数据库加载所有购买记录并更新柱状图。
        /// </summary>
        public void RefreshData()
        {
            // 清除现有图表数据
            chartSalesStatistics.Series.Clear();
            chartSalesStatistics.Titles.Clear();
            chartSalesStatistics.ChartAreas.Clear();

            // 设置图表区域
            ChartArea chartArea = new ChartArea("SalesChartArea");
            chartArea.AxisX.Interval = 1;
            chartArea.AxisX.Title = "水果名称";
            chartArea.AxisY.Title = "总销售数量";
            chartSalesStatistics.ChartAreas.Add(chartArea);

            // 添加图表标题
            chartSalesStatistics.Titles.Add("平台水果总销售统计");

            // 添加数据系列
            Series series = new Series("总销售数量");
            series.ChartType = SeriesChartType.Column;
            series.IsValueShownAsLabel = true;
            series.Color = System.Drawing.Color.OrangeRed; // 设置柱子颜色
            chartSalesStatistics.Series.Add(series);

            string connectionString = ConfigurationManager.ConnectionStrings["FruitAppConnection"].ConnectionString;

            // 使用 Dictionary 存储每种水果的总销售数量
            Dictionary<string, int> fruitSales = new Dictionary<string, int>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // SQL 查询：联接 PurchaseHistory 和 Fruits 表，计算每种水果的总销售量
                    string query = @"
                        SELECT F.Name, SUM(PH.Quantity) AS TotalQuantity
                        FROM PurchaseHistory PH
                        JOIN Fruits F ON PH.FruitId = F.Id
                        GROUP BY F.Name
                        ORDER BY TotalQuantity DESC;"; // 按销售数量降序排列

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string fruitName = reader["Name"].ToString();
                        int totalQuantity = Convert.ToInt32(reader["TotalQuantity"]);
                        fruitSales.Add(fruitName, totalQuantity);
                    }
                    reader.Close();

                    // 将汇总后的数据添加到图表系列中
                    if (fruitSales.Count > 0)
                    {
                        foreach (var entry in fruitSales)
                        {
                            series.Points.AddXY(entry.Key, entry.Value);
                        }
                    }
                    else
                    {
                        chartSalesStatistics.Titles.Clear();
                        chartSalesStatistics.Titles.Add("当前没有销售记录。");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"加载销售统计时发生数据库错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void chartSalesStatistics_Click(object sender, EventArgs e)
        {

        }
    }
}