using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace appfruit
{
    public partial class RegisterForm : Form
    {
        private string currentVerifyCode;       // 当前验证码
        private int verifyCodeExpireSeconds = 120; // 验证码有效期（秒）
        private int remainingSeconds;          // 剩余有效时间
        private string currentEmail;            // 当前输入的邮箱

        public RegisterForm()
        {
            InitializeComponent();
            remainingSeconds = verifyCodeExpireSeconds;
        }

        private void btnSendCode_Click(object sender, EventArgs e)
        {
            currentEmail = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(currentEmail))
            {
                MessageBox.Show("请先输入邮箱地址！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 验证邮箱格式
            if (!IsValidEmail(currentEmail))
            {
                MessageBox.Show("请输入正确的邮箱格式！", "格式错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 生成验证码
            currentVerifyCode = GenerateVerifyCode(6); // 6位数字验证码

            // 发送邮件
            if (SendVerifyCodeEmail(currentEmail, currentVerifyCode))
            {
                MessageBox.Show("验证码已发送到您的邮箱，请查收！", "发送成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                StartVerifyCodeTimer(); // 启动倒计时
            }
            else
            {
                MessageBox.Show("验证码发送失败，请稍后重试！", "发送失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 生成随机验证码
        private string GenerateVerifyCode(int length)
        {
            StringBuilder code = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                code.Append(random.Next(0, 10));
            }
            return code.ToString();
        }

        // 验证邮箱格式
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // 发送验证码邮件
        private bool SendVerifyCodeEmail(string email, string verifyCode)
        {
            try
            {
                // 从配置文件获取SMTP设置（需提前配置）
                string smtpServer = ConfigurationManager.AppSettings["SMTPServer"] ?? "smtp.qq.com";
                int smtpPort = int.Parse(ConfigurationManager.AppSettings["SMTPPort"] ?? "587");
                string senderEmail = ConfigurationManager.AppSettings["SenderEmail"] ?? "your_email@qq.com";
                string senderPassword = ConfigurationManager.AppSettings["SenderPassword"] ?? "your_email_password";

                // 邮件内容
                string subject = "Healthy Fruits 注册验证码";
                string body = $"您的注册验证码为：{verifyCode}，有效期为{verifyCodeExpireSeconds / 60}分钟。请不要将验证码泄露给他人。";

                // 创建邮件
                MailMessage message = new MailMessage();
                message.From = new MailAddress(senderEmail);
                message.To.Add(new MailAddress(email));
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = false;

                // 配置SMTP客户端
                SmtpClient client = new SmtpClient(smtpServer, smtpPort);
                client.EnableSsl = true; // 启用SSL加密
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);

                // 发送邮件
                client.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"邮件发送错误：{ex.Message}");
                return false;
            }
        }

        // 启动验证码倒计时
        private void StartVerifyCodeTimer()
        {
            remainingSeconds = verifyCodeExpireSeconds;
            btnSendCode.Enabled = false;
            btnSendCode.Text = $"{remainingSeconds}秒后重试";
            timerVerifyCode.Start();
        }

        // 倒计时计时器事件
        private void timerVerifyCode_Tick(object sender, EventArgs e)
        {
            remainingSeconds--;
            if (remainingSeconds <= 0)
            {
                timerVerifyCode.Stop();
                btnSendCode.Enabled = true;
                btnSendCode.Text = "发送验证码";
            }
            else
            {
                btnSendCode.Text = $"{remainingSeconds}秒后重试";
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string email = txtEmail.Text.Trim();
            string verifyCode = txtVerifyCode.Text.Trim();

            // 输入验证
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("用户名、密码和邮箱均不可为空！", "注册错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(verifyCode) || verifyCode != currentVerifyCode)
            {
                MessageBox.Show("验证码错误或已过期，请重新获取！", "验证失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 从配置文件获取数据库连接字符串
            string connectionString = ConfigurationManager.ConnectionStrings["FruitAppConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // 检查用户名或邮箱是否已存在
                    string checkQuery = @"
                        SELECT COUNT(*) FROM Customers WHERE CustomerName = @Username OR Email = @Email";
                    SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                    checkCommand.Parameters.AddWithValue("@Username", username);
                    checkCommand.Parameters.AddWithValue("@Email", email);

                    int existsCount = Convert.ToInt32(checkCommand.ExecuteScalar());
                    if (existsCount > 0)
                    {
                        MessageBox.Show("用户名或邮箱已被注册，请更换！", "注册错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 对密码进行哈希处理
                    string passwordHash = GetHash(password);

                    // 插入新客户记录
                    string insertQuery = @"
                        INSERT INTO Customers (CustomerName, PasswordHash, Email, RegisterTime)
                        VALUES (@CustomerName, @PasswordHash, @Email, GETDATE())";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@CustomerName", username);
                    insertCommand.Parameters.AddWithValue("@PasswordHash", passwordHash);
                    insertCommand.Parameters.AddWithValue("@Email", email);

                    insertCommand.ExecuteNonQuery();
                    MessageBox.Show("注册成功！请使用账号登录。", "注册成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"注册失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // 密码哈希方法
        private string GetHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {

        }
    }
}