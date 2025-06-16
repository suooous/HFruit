using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace appfruit
{
    public partial class DeleteFruitUserControl : UserControl
    {
        private List<Fruit> fruits; // 存储从数据库加载的水果列表
        private string _imageFolderPath; // 图像文件夹路径

        // Fruit Class (与 FruitBrowseUserControl 中的定义相同，或者可以共享一个公共类文件)
        public class Fruit
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int Stock { get; set; }
            public string ImagePath { get; set; }
        }

        public DeleteFruitUserControl(string imageFolderPath)
        {
            InitializeComponent();
            _imageFolderPath = imageFolderPath;
            // 默认显示水果列表
            // RefreshData(); // 可以在这里调用，也可以在 AdminDashboardForm 中加载时调用
        }

        private void DeleteFruitUserControl_Load(object sender, EventArgs e)
        {
            RefreshData(); // 确保控件加载时数据已加载
        }

        /// <summary>
        /// 刷新水果数据并重新加载显示。
        /// </summary>
        public void RefreshData()
        {
            LoadFruitsFromDatabase(); // 重新从数据库加载最新数据
            DisplayFruitsForDeletion(); // 根据新加载的数据重新显示
        }

        /// <summary>
        /// 从数据库加载所有水果数据。
        /// </summary>
        private void LoadFruitsFromDatabase()
        {
            fruits = new List<Fruit>();
            string connectionString = ConfigurationManager.ConnectionStrings["FruitAppConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Name, Price, Stock, ImageUrl FROM Fruits ORDER BY Name"; // 添加排序
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        fruits.Add(new Fruit
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Price = reader.GetDecimal(2),
                            Stock = reader.GetInt32(3),
                            ImagePath = reader.IsDBNull(4) ? null : reader.GetString(4)
                        });
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("加载水果数据失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 在 FlowLayoutPanel 中显示水果列表，每个水果带一个删除按钮。
        /// </summary>
        private void DisplayFruitsForDeletion()
        {
            // 清除现有显示
            flowLayoutPanelFruits.Controls.Clear();

            if (fruits == null || !fruits.Any())
            {
                Label noFruitsLabel = new Label();
                noFruitsLabel.Text = "目前没有水果可供删除。";
                noFruitsLabel.AutoSize = true;
                noFruitsLabel.Location = new Point(10, 10);
                flowLayoutPanelFruits.Controls.Add(noFruitsLabel);
                return;
            }

            foreach (var fruit in fruits)
            {
                // 创建一个 Panel 作为每个水果的容器
                Panel fruitPanel = new Panel();
                fruitPanel.BorderStyle = BorderStyle.FixedSingle;
                fruitPanel.Size = new Size(300, 60); // 根据内容调整大小
                fruitPanel.Margin = new Padding(5);
                fruitPanel.Tag = fruit; // 将水果对象存储在 Tag 中，方便后续操作

                // 显示水果名称
                Label nameLabel = new Label();
                nameLabel.Text = fruit.Name;
                nameLabel.Font = new Font("宋体", 12, FontStyle.Bold);
                nameLabel.AutoSize = true;
                nameLabel.Location = new Point(10, 10);
                fruitPanel.Controls.Add(nameLabel);

                // 显示库存和价格（可选）
                Label infoLabel = new Label();
                infoLabel.Text = $"库存: {fruit.Stock} | 价格: ${fruit.Price:F2}";
                infoLabel.Font = new Font("宋体", 9, FontStyle.Regular);
                infoLabel.AutoSize = true;
                infoLabel.Location = new Point(10, 35);
                fruitPanel.Controls.Add(infoLabel);

                // 删除按钮
                Button deleteButton = new Button();
                deleteButton.Text = "删除";
                deleteButton.BackColor = Color.Red;
                deleteButton.ForeColor = Color.White;
                deleteButton.Location = new Point(fruitPanel.Width - 80, 15); // 右侧对齐
                deleteButton.Size = new Size(70, 30);
                deleteButton.Tag = fruit.Id; // 将水果ID存储在 Tag 中
                deleteButton.Click += DeleteButton_Click;
                fruitPanel.Controls.Add(deleteButton);

                flowLayoutPanelFruits.Controls.Add(fruitPanel);
            }
        }

        /// <summary>
        /// 删除按钮点击事件处理。
        /// </summary>
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int fruitIdToDelete = (int)btn.Tag;

            DialogResult result = MessageBox.Show($"确定要删除 ID 为 {fruitIdToDelete} 的水果吗？此操作不可撤销！", "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                DeleteFruitFromDatabase(fruitIdToDelete);
            }
        }

        /// <summary>
        /// 从数据库中删除指定ID的水果。
        /// </summary>
        /// <param name="fruitId">要删除的水果ID。</param>
        private void DeleteFruitFromDatabase(int fruitId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["FruitAppConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string deleteQuery = "DELETE FROM Fruits WHERE Id = @FruitId";
                SqlCommand command = new SqlCommand(deleteQuery, connection);
                command.Parameters.AddWithValue("@FruitId", fruitId);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("水果删除成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // 刷新显示，以移除已删除的水果
                        RefreshData();
                    }
                    else
                    {
                        MessageBox.Show("未找到要删除的水果。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("删除水果失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void flowLayoutPanelFruits_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}