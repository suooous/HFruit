using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting; // 引入图表控件命名空间
using System.Configuration; // 用于访问 App.config 中的连接字符串
using System.Data.SqlClient; // 用于 SQL Server 数据库操作

namespace appfruit
{
    public partial class StatisticsUserControl : UserControl
    {
        public StatisticsUserControl()
        {
            InitializeComponent();
            // 构造函数中不直接加载数据，而是等待 RefreshStatistics() 方法调用
            // 这是为了确保每次显示此控件时都能获取最新数据
        }

        private void StatisticsUserControl_Load(object sender, EventArgs e)
        {
            // 当用户控件加载时，自动刷新统计数据
            RefreshStatistics();
        }

        /// <summary>
        /// 刷新统计数据，从数据库加载当前用户的购买历史并更新柱状图。
        /// </summary>
        public void RefreshStatistics()
        {
            // 清除现有图表数据，确保每次刷新都是新的数据
            chartPurchaseHistory.Series.Clear();
            chartPurchaseHistory.Titles.Clear();
            chartPurchaseHistory.ChartAreas.Clear();

            // 设置图表区域（ChartArea）
            ChartArea chartArea = new ChartArea("MainChartArea");
            chartArea.AxisX.Interval = 1; // 确保所有X轴标签（水果名称）都显示
            chartArea.AxisX.Title = "水果名称";
            chartArea.AxisY.Title = "购买数量";
            chartPurchaseHistory.ChartAreas.Add(chartArea);

            // 添加图表标题
            chartPurchaseHistory.Titles.Add("用户购买历史柱状图");

            // 添加数据系列（Series）
            Series series = new Series("购买数量");
            series.ChartType = SeriesChartType.Column; // 设置图表类型为柱状图
            series.IsValueShownAsLabel = true; // 在柱子上显示数值标签
            series.Color = Color.DodgerBlue; // 设置柱子颜色
            chartPurchaseHistory.Series.Add(series);

            // 检查当前用户是否已登录
            if (string.IsNullOrEmpty(CurrentUser.UserName))
            {
                // 如果没有登录信息，则显示提示信息并返回
                MessageBox.Show("未登录用户，无法加载购买历史。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                chartPurchaseHistory.Titles.Clear();
                chartPurchaseHistory.Titles.Add("请先登录以查看购买历史");
                return;
            }

            string username = CurrentUser.UserName; // 获取当前登录用户的用户名
            string connectionString = ConfigurationManager.ConnectionStrings["FruitAppConnection"].ConnectionString; // 从配置文件获取连接字符串

            // 使用 Dictionary 存储每种水果的总购买数量
            Dictionary<string, int> fruitQuantities = new Dictionary<string, int>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // SQL 查询：联接 PurchaseHistory 和 Fruits 表，根据当前用户查询购买记录
                    string query = @"
                        SELECT F.Name, PH.Quantity
                        FROM PurchaseHistory PH
                        JOIN Fruits F ON PH.FruitId = F.Id
                        WHERE PH.UserName = @UserName;"; // 使用参数化查询防止 SQL 注入

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserName", username); // 为参数赋值

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string fruitName = reader["Name"].ToString();
                        int quantity = Convert.ToInt32(reader["Quantity"]);

                        // 累加相同水果的购买数量
                        if (fruitQuantities.ContainsKey(fruitName))
                        {
                            fruitQuantities[fruitName] += quantity;
                        }
                        else
                        {
                            fruitQuantities.Add(fruitName, quantity);
                        }
                    }
                    reader.Close();

                    // 将汇总后的数据添加到图表系列中
                    if (fruitQuantities.Count > 0)
                    {
                        // 按购买数量降序排序，使图表更具可读性
                        foreach (var entry in fruitQuantities.OrderByDescending(x => x.Value))
                        {
                            series.Points.AddXY(entry.Key, entry.Value);
                        }
                    }
                    else
                    {
                        // 如果没有购买记录，则显示相应提示
                        chartPurchaseHistory.Titles.Clear();
                        chartPurchaseHistory.Titles.Add("当前用户没有购买历史记录。");
                    }
                }
                catch (Exception ex)
                {
                    // 捕获并显示数据库连接或查询错误
                    MessageBox.Show($"加载购买历史时发生数据库错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void chartPurchaseHistory_Click(object sender, EventArgs e)
        {

        }
    }
}