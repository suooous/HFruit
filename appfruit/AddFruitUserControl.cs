using System;
using System.Configuration; // Used for accessing connection strings from App.config
using System.Data.SqlClient; // Used for SQL Server database operations
using System.Windows.Forms;

namespace appfruit
{
    public partial class AddFruitUserControl : UserControl
    {
        public AddFruitUserControl()
        {
            InitializeComponent();
        }

        private void AddFruitUserControl_Load(object sender, EventArgs e)
        {
            ClearForm(); // Ensures the form is empty when loaded
        }

        /// <summary>
        /// Clears the form fields.
        /// </summary>
        public void ClearForm()
        {
            txtName.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtStock.Text = string.Empty; // Changed from txtQuantity to txtStock
            txtImageUrl.Text = string.Empty; // Changed from txtDescription/txtImagePath to txtImageUrl
            // You might also want to clear any error messages or reset the state
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Get form data
            string name = txtName.Text.Trim();
            string priceText = txtPrice.Text.Trim();
            string stockText = txtStock.Text.Trim(); // Changed from quantityText to stockText
            string imageUrl = txtImageUrl.Text.Trim(); // Changed from description/imagePath to imageUrl

            // Data validation
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(priceText) || string.IsNullOrEmpty(stockText))
            {
                MessageBox.Show("水果名称、价格和库存不能为空。", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(priceText, out decimal price) || price <= 0)
            {
                MessageBox.Show("请输入有效的水果价格。", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 'Stock' corresponds to 'Quantity' in the previous code logic, ensure it's a valid integer
            if (!int.TryParse(stockText, out int stock) || stock < 0) // Stock can be 0, but not negative
            {
                MessageBox.Show("请输入有效的水果库存数量。", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["FruitAppConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // Modified query to match your Fruits table schema
                    string query = @"INSERT INTO Fruits (Name, Price, Stock, ImageUrl)
                                     VALUES (@Name, @Price, @Stock, @ImageUrl)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@Stock", stock); // Changed from @Quantity to @Stock
                    command.Parameters.AddWithValue("@ImageUrl", string.IsNullOrEmpty(imageUrl) ? (object)DBNull.Value : imageUrl); // Changed from @Description/@ImagePath to @ImageUrl

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("水果信息添加成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm(); // Clear the form after successful addition
                    }
                    else
                    {
                        MessageBox.Show("水果信息添加失败。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"添加水果时发生数据库错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}