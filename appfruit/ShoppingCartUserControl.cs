// ShoppingCartUserControl.cs
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;
using System.Collections.Generic; // 用于 List<T>

namespace appfruit
{
    public partial class ShoppingCartUserControl : UserControl
    {
        public ShoppingCartUserControl()
        {
            InitializeComponent();
            // 配置 DataGridView 列
            dgvShoppingCart.AutoGenerateColumns = false;

            // 添加数据列
            dgvShoppingCart.Columns.Add("FruitId", "ID"); // 用于内部跟踪，可设置为不可见
            dgvShoppingCart.Columns["FruitId"].Visible = false; // 让ID列不可见
            dgvShoppingCart.Columns.Add("FruitName", "水果名称");
            dgvShoppingCart.Columns.Add("Price", "单价");
            dgvShoppingCart.Columns.Add("Quantity", "数量");
            dgvShoppingCart.Columns.Add("Subtotal", "小计");

            // 新增：添加一个按钮列用于删除单个商品
            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            btnDelete.Name = "btnDelete";
            btnDelete.Text = "删除";
            btnDelete.UseColumnTextForButtonValue = true; // 这会使所有按钮显示“删除”文本
            dgvShoppingCart.Columns.Add(btnDelete);


            // 将数据列设为只读
            // 按钮列不需要设置为只读，因为其目的是点击
            foreach (DataGridViewColumn col in dgvShoppingCart.Columns)
            {
                if (col.Name != "btnDelete") // 确保不将按钮列设为只读
                {
                    col.ReadOnly = true;
                }
            }
        }

        // 调用此方法刷新购物车显示
        public void LoadShoppingCart()
        {
            if (CurrentUser.UserName == null)
            {
                dgvShoppingCart.Rows.Clear();
                lblTotalAmount.Text = "总计: $0.00 (请登录)";
                btnPurchase.Enabled = false;
                btnClearCart.Enabled = false;
                return;
            }

            dgvShoppingCart.Rows.Clear(); // 清除现有行
            decimal totalAmount = 0;

            string connectionString = ConfigurationManager.ConnectionStrings["FruitAppConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                        SELECT sc.FruitId, f.Name, f.Price, sc.Quantity, f.Stock
                        FROM ShoppingCart sc
                        JOIN Fruits f ON sc.FruitId = f.Id
                        WHERE sc.UserName = @UserName";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserName", CurrentUser.UserName);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int fruitId = Convert.ToInt32(reader["FruitId"]); // 获取 FruitId
                        string fruitName = reader["Name"].ToString();
                        decimal price = Convert.ToDecimal(reader["Price"]);
                        int quantity = Convert.ToInt32(reader["Quantity"]);
                        int stock = Convert.ToInt32(reader["Stock"]); // 获取实际库存以进行验证

                        // 检查购物车数量是否超过当前库存
                        if (quantity > stock)
                        {
                            // 可选：将购物车数量更新为可用库存或提示用户
                            MessageBox.Show($"购物车中 {fruitName} 的数量 ({quantity}) 超过当前库存 ({stock})。请调整数量。", "库存不足警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            // 您可能希望在此处更新数据库中的购物车数量，或者只是标记它。
                            // 为简单起见，我们将让购买逻辑处理它。
                        }

                        decimal subtotal = price * quantity;
                        totalAmount += subtotal;

                        // 将 FruitId 添加到行中，即使它不可见，方便后续操作
                        dgvShoppingCart.Rows.Add(fruitId, fruitName, price.ToString("F2"), quantity, subtotal.ToString("F2"), "删除"); // "删除" 是按钮文本
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("加载购物车失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            lblTotalAmount.Text = $"总计: ${totalAmount:F2}";
            btnPurchase.Enabled = (dgvShoppingCart.Rows.Count > 0); // 仅当购物车有商品时才启用购买
            btnClearCart.Enabled = (dgvShoppingCart.Rows.Count > 0); // 仅当购物车有商品时才启用清除
        }

        private void ShoppingCartUserControl_Load(object sender, EventArgs e)
        {
            // 当控件可见时，初始加载购物车
            LoadShoppingCart();
        }

        // 新增：DataGridView 的 CellContentClick 事件处理程序
        private void dgvShoppingCart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 确保点击的是按钮列并且不是标题行
            if (e.ColumnIndex == dgvShoppingCart.Columns["btnDelete"].Index && e.RowIndex >= 0)
            {
                // 获取被点击行的 FruitId
                int fruitId = Convert.ToInt32(dgvShoppingCart.Rows[e.RowIndex].Cells["FruitId"].Value);
                string fruitName = dgvShoppingCart.Rows[e.RowIndex].Cells["FruitName"].Value.ToString();

                DialogResult confirmResult = MessageBox.Show($"您确定要从购物车中删除 {fruitName} 吗？", "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmResult == DialogResult.Yes)
                {
                    DeleteCartItem(fruitId);
                }
            }
        }

        // 新增：删除购物车单个商品的私有方法
        private void DeleteCartItem(int fruitId)
        {
            if (CurrentUser.UserName == null)
            {
                MessageBox.Show("请先登录才能删除商品。", "未登录", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["FruitAppConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string deleteQuery = "DELETE FROM ShoppingCart WHERE UserName = @UserName AND FruitId = @FruitId";
                    SqlCommand command = new SqlCommand(deleteQuery, connection);
                    command.Parameters.AddWithValue("@UserName", CurrentUser.UserName);
                    command.Parameters.AddWithValue("@FruitId", fruitId);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("商品已从购物车中删除。", "删除成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadShoppingCart(); // 刷新购物车显示
                    }
                    else
                    {
                        MessageBox.Show("未找到要删除的商品，或删除失败。", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("删除商品失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnPurchase_Click(object sender, EventArgs e)
        {
            if (CurrentUser.UserName == null)
            {
                MessageBox.Show("请先登录才能进行购买。", "未登录", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dgvShoppingCart.Rows.Count == 0)
            {
                MessageBox.Show("您的购物车是空的，无法购买。", "购物车为空", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show($"您确定要购买购物车中的所有商品吗？总计 {lblTotalAmount.Text}", "确认购买", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["FruitAppConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction(); // 开始一个事务以确保原子性

                try
                {
                    // 1. 获取当前购物车商品及其实际库存
                    List<(int fruitId, int quantity, string fruitName, int currentStock)> itemsToPurchase = new List<(int, int, string, int)>();
                    string getCartQuery = @"
                        SELECT sc.FruitId, sc.Quantity, f.Name, f.Stock
                        FROM ShoppingCart sc
                        JOIN Fruits f ON sc.FruitId = f.Id
                        WHERE sc.UserName = @UserName";
                    SqlCommand getCartCommand = new SqlCommand(getCartQuery, connection, transaction);
                    getCartCommand.Parameters.AddWithValue("@UserName", CurrentUser.UserName);
                    SqlDataReader reader = getCartCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        itemsToPurchase.Add((
                            reader.GetInt32(0),
                            reader.GetInt32(1),
                            reader.GetString(2),
                            reader.GetInt32(3)
                        ));
                    }
                    reader.Close();

                    // 2. 验证库存并更新 Fruits 表
                    foreach (var item in itemsToPurchase)
                    {
                        if (item.quantity > item.currentStock)
                        {
                            transaction.Rollback(); // 如果库存不足则回滚
                            MessageBox.Show($"购买失败：{item.fruitName} (ID: {item.fruitId}) 库存不足。所需数量: {item.quantity}, 实际库存: {item.currentStock}", "库存不足", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            LoadShoppingCart(); // 刷新购物车以显示正确状态
                            return;
                        }

                        string updateStockQuery = "UPDATE Fruits SET Stock = Stock - @Quantity WHERE Id = @FruitId";
                        SqlCommand updateStockCommand = new SqlCommand(updateStockQuery, connection, transaction);
                        updateStockCommand.Parameters.AddWithValue("@Quantity", item.quantity);
                        updateStockCommand.Parameters.AddWithValue("@FruitId", item.fruitId);
                        updateStockCommand.ExecuteNonQuery();
                    }

                    // 3. 插入到 PurchaseHistory 表中
                    string insertHistoryQuery = "INSERT INTO PurchaseHistory (UserName, FruitId, Quantity, PurchaseDate) VALUES (@UserName, @FruitId, @Quantity, GETDATE())";
                    SqlCommand insertHistoryCommand = new SqlCommand(insertHistoryQuery, connection, transaction);
                    insertHistoryCommand.Parameters.AddWithValue("@UserName", CurrentUser.UserName);
                    // 在循环内部为 FruitId 和 Quantity 添加参数
                    insertHistoryCommand.Parameters.Add("@FruitId", SqlDbType.Int);
                    insertHistoryCommand.Parameters.Add("@Quantity", SqlDbType.Int);

                    foreach (var item in itemsToPurchase)
                    {
                        insertHistoryCommand.Parameters["@FruitId"].Value = item.fruitId;
                        insertHistoryCommand.Parameters["@Quantity"].Value = item.quantity;
                        insertHistoryCommand.ExecuteNonQuery();
                    }

                    // 4. 从 ShoppingCart 表中清除商品
                    string clearCartQuery = "DELETE FROM ShoppingCart WHERE UserName = @UserName";
                    SqlCommand clearCartCommand = new SqlCommand(clearCartQuery, connection, transaction);
                    clearCartCommand.Parameters.AddWithValue("@UserName", CurrentUser.UserName);
                    clearCartCommand.ExecuteNonQuery();

                    transaction.Commit(); // 如果所有步骤都成功，则提交事务
                    MessageBox.Show("购买成功！您的订单已处理。", "购买成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadShoppingCart(); // 重新加载购物车以显示为空
                    // 可选：触发事件以更新水果浏览页面的库存显示
                    // OnPurchaseCompleted?.Invoke(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // 任何错误都回滚
                    MessageBox.Show("购买失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClearCart_Click(object sender, EventArgs e)
        {
            if (CurrentUser.UserName == null)
            {
                MessageBox.Show("请先登录才能清空购物车。", "未登录", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dgvShoppingCart.Rows.Count == 0)
            {
                MessageBox.Show("购物车已经是空的了。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show("您确定要清空购物车吗？", "确认清空", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["FruitAppConnection"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = "DELETE FROM ShoppingCart WHERE UserName = @UserName";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@UserName", CurrentUser.UserName);
                        command.ExecuteNonQuery();
                        MessageBox.Show("购物车已清空。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadShoppingCart(); // 刷新显示
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("清空购物车失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}