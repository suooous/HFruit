using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace appfruit
{
    public partial class CustomerManagementUserControl : UserControl
    {
        public CustomerManagementUserControl()
        {
            InitializeComponent();
            // dataGridViewCustomers 是 Designer.cs 中定义的 DataGridView
            // 允许自动生成列，方便从 DataTable 加载数据
            dataGridViewCustomers.AutoGenerateColumns = true;
        }

        private void CustomerManagementUserControl_Load(object sender, EventArgs e)
        {
            RefreshData(); // 控件加载时刷新数据
        }

        /// <summary>
        /// 刷新顾客数据，从数据库重新加载并显示。
        /// </summary>
        public void RefreshData()
        {
            LoadCustomersIntoGrid();
        }

        /// <summary>
        /// 从数据库加载顾客数据并填充 DataGridView。
        /// </summary>
        private void LoadCustomersIntoGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["FruitAppConnection"].ConnectionString;
            DataTable customersTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT CustomerId, CustomerName, Email, RegisterTime, LastVisitTime FROM Customers";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                try
                {
                    connection.Open();
                    adapter.Fill(customersTable);
                    dataGridViewCustomers.DataSource = customersTable;

                    // 隐藏密码哈希列，因为它是敏感信息
                    if (dataGridViewCustomers.Columns.Contains("PasswordHash"))
                    {
                        dataGridViewCustomers.Columns["PasswordHash"].Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("加载顾客数据失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 处理删除顾客按钮点击事件。
        /// </summary>
        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            if (dataGridViewCustomers.SelectedRows.Count > 0)
            {
                // 获取选中行的 CustomerId
                int customerId = Convert.ToInt32(dataGridViewCustomers.SelectedRows[0].Cells["CustomerId"].Value);
                string customerName = dataGridViewCustomers.SelectedRows[0].Cells["CustomerName"].Value.ToString();

                DialogResult result = MessageBox.Show($"确定要删除顾客 '{customerName}' (ID: {customerId}) 吗？此操作不可撤销！", "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    DeleteCustomerFromDatabase(customerId);
                }
            }
            else
            {
                MessageBox.Show("请选择一个要删除的顾客。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 从数据库中删除指定ID的顾客。
        /// </summary>
        /// <param name="customerId">要删除的顾客ID。</param>
        private void DeleteCustomerFromDatabase(int customerId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["FruitAppConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // 注意：在实际应用中，删除顾客可能还需要处理与该顾客相关的订单、购物车等数据，
                // 例如设置外键级联删除，或者手动删除相关记录。这里只删除 Customers 表中的记录。
                string deleteQuery = "DELETE FROM Customers WHERE CustomerId = @CustomerId";
                SqlCommand command = new SqlCommand(deleteQuery, connection);
                command.Parameters.AddWithValue("@CustomerId", customerId);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"顾客 (ID: {customerId}) 删除成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshData(); // 刷新 DataGridView
                    }
                    else
                    {
                        MessageBox.Show("未找到要删除的顾客。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("删除顾客失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // 可以根据需要添加其他功能，例如：
        // private void btnAddCustomer_Click(object sender, EventArgs e) { /* 实现添加顾客逻辑 */ }
        // private void btnEditCustomer_Click(object sender, EventArgs e) { /* 实现编辑顾客逻辑 */ }
    }
}