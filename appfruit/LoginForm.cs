using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace appfruit
{
    public partial class LoginForm : Form
    {
        private enum UserType
        {
            Customer,
            Administrator
        }

        private UserType currentUserType = UserType.Customer;
        private bool isDragging = false;
        private Point dragStart;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("用户名和密码不能为空！", "登录错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["FruitAppConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    bool loginSuccess = false;
                    string userTypeString = currentUserType.ToString(); // 将枚举转换为字符串以便存储到 CurrentUser

                    if (currentUserType == UserType.Customer)
                    {
                        string query = "SELECT COUNT(*) FROM Customers WHERE CustomerName = @CustomerName AND PasswordHash = @PasswordHash";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@CustomerName", username);
                        command.Parameters.AddWithValue("@PasswordHash", GetHash(password));

                        int count = (int)command.ExecuteScalar();
                        loginSuccess = count > 0;

                        if (loginSuccess)
                        {
                            // Update LastVisitTime for the customer
                            string updateQuery = "UPDATE Customers SET LastVisitTime = GETDATE() WHERE CustomerName = @CustomerName";
                            SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                            updateCommand.Parameters.AddWithValue("@CustomerName", username);
                            updateCommand.ExecuteNonQuery();
                        }
                    }
                    else // Administrator
                    {
                        string query = "SELECT COUNT(*) FROM Administrators WHERE Username = @Username AND PasswordHash = @PasswordHash";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@PasswordHash", GetHash(password));

                        int count = (int)command.ExecuteScalar();
                        loginSuccess = count > 0;
                    }

                    if (loginSuccess)
                    {
                        // 设置全局的 CurrentUser 静态类属性
                        CurrentUser.UserName = username;
                        CurrentUser.UserType = userTypeString; // 如果需要在全局使用 UserType，则添加到 CurrentUser 中
                        Console.WriteLine($"当前用户名 (从 LoginForm 设置): {CurrentUser.UserName}"); // 调试信息
                        //MessageBox.Show($"{userTypeString}登录成功！", "登录成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MessageBox.Show($"欢迎 {username}来到HFruit！", "登录成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("用户名或密码错误！", "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"数据库错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string GetHash(string input)
        {
            using (System.Security.Cryptography.SHA256 sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (currentUserType == UserType.Customer)
            {
                RegisterForm registerForm = new RegisterForm();
                registerForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("管理员无注册权限！", "权限错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                currentUserType = UserType.Customer;
                btnRegister.Enabled = true;
                btnRegister.Text = "注册";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                currentUserType = UserType.Administrator;
                btnRegister.Enabled = false;
                btnRegister.Text = "NO!";
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            btnRegister.Enabled = true;
            btnRegister.Text = "Register";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoginForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y < panel1.Height)
            {
                isDragging = true;
                dragStart = new Point(e.X, e.Y);
            }
        }

        private void LoginForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentScreenPos = PointToScreen(new Point(e.X, e.Y));
                this.Location = new Point(currentScreenPos.X - dragStart.X, currentScreenPos.Y - dragStart.Y);
            }
        }

        private void LoginForm_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            LoginForm_MouseDown(sender, e);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            LoginForm_MouseMove(sender, e);
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            LoginForm_MouseUp(sender, e);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // 此方法目前没有实际的绘图逻辑
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            int cornerRadius = 15;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, cornerRadius * 2, cornerRadius * 2, 180, 90);
            path.AddArc(this.Width - cornerRadius * 2, 0, cornerRadius * 2, cornerRadius * 2, 270, 90);
            path.AddArc(this.Width - cornerRadius * 2, this.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            path.AddArc(0, this.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            path.CloseFigure();
            this.Region = new Region(path);
        }
    }
}