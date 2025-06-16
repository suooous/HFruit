using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq; // Ensure this reference exists for LINQ queries
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration; // Ensure this reference exists for accessing application configuration
using System.Data.SqlClient; // Ensure this reference exists for database operations

namespace appfruit
{
    public partial class FruitBrowseUserControl : UserControl
    {
        // Define an event that the main form (Customhomepage) can subscribe to,
        // to be notified when shopping cart contents are updated.
        public event EventHandler FruitAddedToCart;

        private List<Fruit> fruits; // Stores the list of all fruits loaded from the database
        private string _imageFolderPath; // Stores the path to fruit image files

        // Fruit Class: Represents a fruit product
        // It's usually recommended to put this class in a separate Fruit.cs file,
        // but for demonstration purposes, it can be here.
        public class Fruit
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int Stock { get; set; }
            public string ImagePath { get; set; } // Image filename relative to _imageFolderPath
        }

        // CurrentUser Class: Typically a static class or singleton used to access
        // information about the currently logged-in user throughout the application.
        // (Usually in a separate CurrentUser.cs file)


        /// <summary>
        /// Constructor for FruitBrowseUserControl.
        /// </summary>
        /// <param name="imageFolderPath">The folder path where fruit images are located.</param>
        public FruitBrowseUserControl(string imageFolderPath)
        {
            InitializeComponent(); // Initialize the controls' components (defined in Designer.cs)
            _imageFolderPath = imageFolderPath;

            // 构造函数中不再立即加载数据，改由 RefreshData() 在适当时候调用
            // LoadFruitsFromDatabase(); // 移除此行
            // LoadFruitCards(); // 移除此行
        }

        /// <summary>
        /// 当控件加载时调用。
        /// </summary>
        private void FruitBrowseUserControl_Load(object sender, EventArgs e)
        {
            // 在控件首次加载时调用 RefreshData，确保显示最新数据
            RefreshData();
        }

        /// <summary>
        /// 刷新水果数据并重新加载水果卡片显示。
        /// 这是外部（如 AdminDashboardForm）调用以更新显示的方法。
        /// </summary>
        public void RefreshData()
        {
            LoadFruitsFromDatabase(); // 重新从数据库加载最新数据
            LoadFruitCards();         // 根据新加载的数据重新创建和显示水果卡片
        }


        /// <summary>
        /// Loads all fruit data from the database into the 'fruits' list.
        /// </summary>
        private void LoadFruitsFromDatabase()
        {
            fruits = new List<Fruit>();
            // Get the database connection string from the application configuration
            string connectionString = ConfigurationManager.ConnectionStrings["FruitAppConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // SQL query statement to select all relevant fruit information
                string query = "SELECT Id, Name, Price, Stock, ImageUrl FROM Fruits";
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open(); // Open the database connection
                    SqlDataReader reader = command.ExecuteReader(); // Execute the query and get the data reader
                    while (reader.Read()) // Read data row by row
                    {
                        fruits.Add(new Fruit
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Price = reader.GetDecimal(2),
                            Stock = reader.GetInt32(3),
                            // Check if ImagePath is DBNull, if so, set to null
                            ImagePath = reader.IsDBNull(4) ? null : reader.GetString(4)
                        });
                    }
                    reader.Close(); // Close the data reader
                }
                catch (Exception ex)
                {
                    // Handle exceptions if loading fruit data fails
                    MessageBox.Show("加载水果数据失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Clears the fruit Browse panel and reloads all fruit cards based on the 'fruits' list.
        /// </summary>
        private void LoadFruitCards()
        {
            fruitBrowsePanel.Controls.Clear(); // Clear all existing fruit cards from the panel
            foreach (var fruit in fruits)
            {
                CreateFruitCard(fruit); // Create and add a card for each fruit
            }
        }

        /// <summary>
        /// Creates and configures a visual card (Panel) for a single fruit object,
        /// including image, name, price, stock, and an "Add to Cart" button.
        /// </summary>
        /// <param name="fruit">The fruit object for which to create the card.</param>
        ///
        private void CreateFruitCard(Fruit fruit)
        {
            // 使用您的 RoundedPanel 作为卡片的基础
            RoundedPanel cardPanel = new RoundedPanel();
            cardPanel.CornerRadius = 15; // 可以根据需要调整圆角半径
            cardPanel.Width = 200;
            cardPanel.Height = 220; // 检查这个高度，确保所有内容都能容纳
            cardPanel.Margin = new Padding(10);
            cardPanel.Tag = fruit;

            // ... (PictureBox code remains the same) ...
            PictureBox pictureBox = new PictureBox();
            pictureBox.Width = 180;
            pictureBox.Height = 100;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Location = new Point(10, 10);
            // ... (图片加载try-catch块) ...
            try
            {
                if (!string.IsNullOrEmpty(fruit.ImagePath) && File.Exists(Path.Combine(_imageFolderPath, fruit.ImagePath)))
                {
                    pictureBox.Image = Image.FromFile(Path.Combine(_imageFolderPath, fruit.ImagePath));
                }
                else
                {
                    pictureBox.Image = SystemIcons.Question.ToBitmap();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载图片 {fruit.ImagePath} 失败: {ex.Message}", "图片加载错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pictureBox.Image = SystemIcons.Error.ToBitmap();
            }
            cardPanel.Controls.Add(pictureBox);


            // 2. 水果名称 (Label) - 位于图片下方，居中显示
            Label nameLabel = new Label();
            nameLabel.Text = fruit.Name;
            nameLabel.Font = new Font("宋体", 12, FontStyle.Bold);
            nameLabel.AutoSize = true;
            using (Graphics g = this.CreateGraphics())
            {
                SizeF nameSize = g.MeasureString(nameLabel.Text, nameLabel.Font);
                int nameX = (cardPanel.Width - (int)nameSize.Width) / 2;
                nameLabel.Location = new Point(nameX, pictureBox.Bottom + 5);
            }
            cardPanel.Controls.Add(nameLabel);

            // 3. 价格和库存的容器 Panel
            // 这个 infoPanel 不需要太多样式，它只是一个布局容器
            Panel infoPanel = new Panel();
            infoPanel.Width = cardPanel.Width - 20;
            infoPanel.Height = 30; // 确保高度足够容纳内容
            infoPanel.Location = new Point(10, nameLabel.Bottom + 5);
            // infoPanel.BorderStyle = BorderStyle.FixedSingle; // 辅助调试边框，完成后可移除或注释
            cardPanel.Controls.Add(infoPanel);

            // --- 美化部分开始 ---

            // 3a. 库存 RoundedPanel 及 Label (左边)
            RoundedPanel stockContainerPanel = new RoundedPanel(); // **改为 RoundedPanel**
            stockContainerPanel.CornerRadius = 8; // 设置圆角
            stockContainerPanel.Width = infoPanel.Width / 2 - 2; // 减去少量间隙
            stockContainerPanel.Height = infoPanel.Height;
            stockContainerPanel.Location = new Point(0, 0);
            stockContainerPanel.BackColor = Color.FromArgb(240, 240, 240); // 浅灰色背景
                                                                           // stockContainerPanel.BorderStyle = BorderStyle.FixedSingle; // 如果RoundedPanel自己绘制了边框，这里不需要再设置
            infoPanel.Controls.Add(stockContainerPanel);

            Label stockLabel = new Label();
            stockLabel.Text = $"库存: {fruit.Stock}";
            stockLabel.Font = new Font("宋体", 9, FontStyle.Regular); // 字体大小稍微调小
            stockLabel.AutoSize = true;
            stockLabel.ForeColor = fruit.Stock > 0 ? Color.DimGray : Color.Red; // 非0库存使用暗灰色，0库存使用红色
            using (Graphics g = this.CreateGraphics())
            {
                SizeF stockSize = g.MeasureString(stockLabel.Text, stockLabel.Font);
                int stockX = (stockContainerPanel.Width - (int)stockSize.Width) / 2;
                int stockY = (stockContainerPanel.Height - (int)stockSize.Height) / 2;
                stockLabel.Location = new Point(stockX, stockY);
            }
            stockContainerPanel.Controls.Add(stockLabel);

            // 3b. 价格 RoundedPanel 及 Label (右边)
            RoundedPanel priceContainerPanel = new RoundedPanel(); // **改为 RoundedPanel**
            priceContainerPanel.CornerRadius = 8; // 设置圆角
            priceContainerPanel.Width = infoPanel.Width / 2 - 2; // 减去少量间隙
            priceContainerPanel.Height = infoPanel.Height;
            priceContainerPanel.Location = new Point(stockContainerPanel.Right + 4, 0); // 在库存Panel右侧留出少量间隙
            priceContainerPanel.BackColor = Color.FromArgb(240, 240, 240); // 浅灰色背景
                                                                           // priceContainerPanel.BorderStyle = BorderStyle.FixedSingle; // 如果RoundedPanel自己绘制了边框，这里不需要再设置
            infoPanel.Controls.Add(priceContainerPanel);

            Label priceLabel = new Label();
            priceLabel.Text = $"价格: ${fruit.Price:F2}";
            priceLabel.Font = new Font("宋体", 9, FontStyle.Regular); // 字体大小稍微调小
            priceLabel.AutoSize = true;
            //priceLabel.BackColor = Color.Pink;
            priceLabel.ForeColor = Color.ForestGreen; // 价格使用绿色
            using (Graphics g = this.CreateGraphics())
            {
                SizeF priceSize = g.MeasureString(priceLabel.Text, priceLabel.Font);
                int priceX = (priceContainerPanel.Width - (int)priceSize.Width) / 2;
                int priceY = (priceContainerPanel.Height - (int)priceSize.Height) / 2;
                priceLabel.Location = new Point(priceX, priceY);
            }
            priceContainerPanel.Controls.Add(priceLabel);

            // --- 美化部分结束 ---

            // 4. 加入购物车按钮 (Button) - 位于最下方，居中显示
            Button addButton = new Button();
            addButton.Text = "加入购物车";
            addButton.BackColor = Color.Orange;
            addButton.Size = new Size(100, 30);
            // 计算按钮的居中位置
            int buttonX = (cardPanel.Width - addButton.Width) / 2;
            addButton.Location = new Point(buttonX, infoPanel.Bottom + 10);
            addButton.Tag = fruit;
            addButton.Click += (sender, e) => AddToCart((Fruit)((Button)sender).Tag);
            addButton.Enabled = fruit.Stock > 0;
            cardPanel.Controls.Add(addButton);

            // 将整个水果卡片添加到主浏览面板
            fruitBrowsePanel.Controls.Add(cardPanel);
        }


        /// <summary>
        /// Handles the "Add to Cart" button click event, adding the selected fruit to the shopping cart (database).
        /// </summary>
        /// <param name="fruit">The fruit object to add to the shopping cart.</param>
        private void AddToCart(Fruit fruit)
        {

            Console.WriteLine($"当前用户名: {CurrentUser.UserName}"); // 调试信息
            // Check if the user is logged in
            if (CurrentUser.UserName == null)
            {
                MessageBox.Show("请先登录才能将商品加入购物车。", "未登录", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Check if fruit stock is sufficient
            if (fruit.Stock <= 0)
            {
                MessageBox.Show($"{fruit.Name} 库存不足，无法加入购物车。", "库存不足", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["FruitAppConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // 1. Check if this fruit already exists in the user's shopping cart
                    string checkQuery = "SELECT Quantity FROM ShoppingCart WHERE UserName = @UserName AND FruitId = @FruitId";
                    SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                    checkCommand.Parameters.AddWithValue("@UserName", CurrentUser.UserName);
                    checkCommand.Parameters.AddWithValue("@FruitId", fruit.Id);
                    object existingQuantity = checkCommand.ExecuteScalar();

                    if (existingQuantity != null)
                    {
                        // 2. Item already exists in the cart, update quantity
                        int currentQuantityInCart = Convert.ToInt32(existingQuantity);
                        int newQuantity = currentQuantityInCart + 1;

                        // Re-check if stock is sufficient to add this one
                        if (newQuantity > fruit.Stock)
                        {
                            MessageBox.Show($"加入购物车失败：{fruit.Name} 库存只有 {fruit.Stock} 个，您购物车中已有 {currentQuantityInCart} 个。", "库存不足", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        string updateQuery = "UPDATE ShoppingCart SET Quantity = @Quantity WHERE UserName = @UserName AND FruitId = @FruitId";
                        SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@Quantity", newQuantity);
                        updateCommand.Parameters.AddWithValue("@UserName", CurrentUser.UserName);
                        updateCommand.Parameters.AddWithValue("@FruitId", fruit.Id);
                        updateCommand.ExecuteNonQuery();
                        MessageBox.Show($"已将 {fruit.Name} 的数量更新为 {newQuantity}。", "更新购物车", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // 3. Item not in cart, insert a new entry
                        string insertQuery = "INSERT INTO ShoppingCart (UserName, FruitId, Quantity) VALUES (@UserName, @FruitId, @Quantity)";
                        SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@UserName", CurrentUser.UserName);
                        insertCommand.Parameters.AddWithValue("@FruitId", fruit.Id);
                        insertCommand.Parameters.AddWithValue("@Quantity", 1); // Initial quantity to add is 1
                        insertCommand.ExecuteNonQuery();
                        MessageBox.Show($"已将 {fruit.Name} 加入购物车！", "加入购物车", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // Trigger event to notify the main form or other controls that cart contents have changed
                    FruitAddedToCart?.Invoke(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("加入购物车失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Handles the search button click event, filtering and displaying fruit cards
        /// based on the search term and price range.
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim(); // Get search box text and remove leading/trailing spaces
            decimal minPrice = -1; // Default to a value that won't filter if not entered
            decimal maxPrice = -1; // Default to a value that won't filter if not entered

            // Try to parse min price
            if (!string.IsNullOrWhiteSpace(txtMinPrice.Text))
            {
                if (!decimal.TryParse(txtMinPrice.Text, out minPrice))
                {
                    MessageBox.Show("最低价格输入无效，请输入一个数字。", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Try to parse max price
            if (!string.IsNullOrWhiteSpace(txtMaxPrice.Text))
            {
                if (!decimal.TryParse(txtMaxPrice.Text, out maxPrice))
                {
                    MessageBox.Show("最高价格输入无效，请输入一个数字。", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Validate price range
            if (minPrice != -1 && maxPrice != -1 && minPrice > maxPrice)
            {
                MessageBox.Show("最低价格不能高于最高价格。", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            fruitBrowsePanel.Controls.Clear(); // Clear all currently displayed fruit cards

            // Use LINQ to filter the fruit list.
            // For case-insensitive search, convert both fruit name and search term to lowercase for comparison.
            // Also apply price range filtering.
            var filteredFruits = fruits.Where(f =>
                (string.IsNullOrEmpty(searchTerm) || f.Name.ToLower().Contains(searchTerm.ToLower())) &&
                (minPrice == -1 || f.Price >= minPrice) &&
                (maxPrice == -1 || f.Price <= maxPrice)
            ).ToList();

            if (filteredFruits.Any()) // If matching fruits are found
            {
                foreach (var fruit in filteredFruits)
                {
                    CreateFruitCard(fruit); // Create and display a card for each filtered fruit
                }
            }
            else // If no matching fruits are found
            {
                Label noResultsLabel = new Label();
                noResultsLabel.Text = "未找到匹配的水果。";
                noResultsLabel.AutoSize = true;
                noResultsLabel.Location = new Point(50, 50); // Position the message
                fruitBrowsePanel.Controls.Add(noResultsLabel); // Add the message to the panel
            }
        }

        private void fruitBrowsePanel_Paint(object sender, PaintEventArgs e)
        {
            // You can add custom drawing logic here if needed
        }

        // private void FruitBrowseUserControl_Load(object sender, EventArgs e)
        // {
        //     // 此方法现在只会在控件加载时调用一次 RefreshData()。
        //     // 如果您希望它在每次显示时都刷新（例如，当从其他 UserControl 切换回来时），
        //     // 那么外部容器 (AdminDashboardForm) 需要调用 RefreshData()。
        // }

        private void fruitBrowsePanel_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}