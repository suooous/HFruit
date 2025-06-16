using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace appfruit
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LoginForm loginForm = new LoginForm();

            // 以模态方式显示登录窗体
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                // 登录成功后，根据 CurrentUser.UserType 判断打开哪个页面
                if (CurrentUser.UserType == "Customer") // LoginForm 中 UserType.Customer.ToString() 结果是 "Customer"
                {
                    Application.Run(new Customhomepage());
                }
                else if (CurrentUser.UserType == "Administrator") // LoginForm 中 UserType.Administrator.ToString() 结果是 "Administrator"
                {
                    // 确保您已经创建了 AdminDashboardForm 这个窗体
                    Application.Run(new AdminDashboardForm());
                }
                else
                {
                    // 如果 UserType 是未知的或未设置（理论上不应该发生，除非登录逻辑有误）
                    MessageBox.Show("未知用户类型，无法启动主页。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit(); // 退出应用程序
                }
            }
            else
            {
                // 登录失败或被取消，退出应用程序
                Application.Exit();
            }
        }
    }
}