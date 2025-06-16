using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace appfruit
{
    public partial class AdminDashboardForm : Form
    {
        private UserControl currentActiveControl; // 跟踪当前活动的控件

        // 声明管理员页面所需的用户控件实例
        private FruitBrowseUserControl fruitBrowseControl;
        private AddFruitUserControl addFruitControl;
        private AdminSalesStatisticsUserControl adminSalesStatisticsControl;
        // 新增用户控件实例
        private DeleteFruitUserControl deleteFruitControl;
        private CustomerManagementUserControl customerManagementControl;
        // AI QA 用户控件
        private AIQAUserControl aiQAUserControl; // Declare the AI QA User Control

        public AdminDashboardForm()
        {
            InitializeComponent();

            // 初始化所有用户控件
            string appExecutablePath = AppDomain.CurrentDomain.BaseDirectory;
            string projectRootPath = Path.GetFullPath(Path.Combine(appExecutablePath, @"..\..\"));
            string imageFolderPath = Path.Combine(projectRootPath, "Fimage");
            imageFolderPath = Path.GetFullPath(imageFolderPath);

            fruitBrowseControl = new FruitBrowseUserControl(imageFolderPath);
            addFruitControl = new AddFruitUserControl();
            adminSalesStatisticsControl = new AdminSalesStatisticsUserControl();
            deleteFruitControl = new DeleteFruitUserControl(imageFolderPath);
            customerManagementControl = new CustomerManagementUserControl();
            aiQAUserControl = new AIQAUserControl(); // Initialize the AI QA User Control

            // 默认加载：水果浏览页面
            LoadUserControl(fruitBrowseControl);
        }

        /// <summary>
        /// 动态加载并显示指定的用户控件。
        /// </summary>
        /// <param name="userControl">要加载的用户控件实例。</param>
        private void LoadUserControl(UserControl userControl)
        {
            if (userControl == null)
            {
                MessageBox.Show("尝试加载一个空的用户控件。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 在方法开头进行一次类型检查和转换，避免重复声明同名局部变量
            FruitBrowseUserControl browseControl = userControl as FruitBrowseUserControl;
            AdminSalesStatisticsUserControl statsControl = userControl as AdminSalesStatisticsUserControl;
            AddFruitUserControl addControl = userControl as AddFruitUserControl;
            DeleteFruitUserControl deleteControl = userControl as DeleteFruitUserControl;
            CustomerManagementUserControl customerControl = userControl as CustomerManagementUserControl;
            AIQAUserControl aiControl = userControl as AIQAUserControl; // Get AI QA User Control instance


            // 改进：如果尝试加载当前已经激活的控件，只刷新数据，不重新清除/添加控件
            if (currentActiveControl == userControl)
            {
                if (browseControl != null)
                {
                    browseControl.RefreshData(); // 刷新水果浏览数据
                }
                else if (statsControl != null)
                {
                    statsControl.RefreshData(); // 刷新销售统计数据
                }
                else if (deleteControl != null)
                {
                    deleteControl.RefreshData(); // 刷新删除水果页面，以便显示最新水果列表
                }
                else if (customerControl != null)
                {
                    customerControl.RefreshData(); // 刷新顾客管理数据
                }
                // For AIQAUserControl, no specific RefreshData or ClearForm is typically needed on re-selection
                // unless its internal state needs resetting or reloading, which is not common for a chat UI.
                // else if (aiControl != null) { /* aiControl.ResetChat(); */ } // Example if a reset is needed
                return;
            }

            mainContentPanel.Controls.Clear(); // 清除当前面板中的所有控件

            userControl.Dock = DockStyle.Fill; // 让用户控件填充整个面板
            mainContentPanel.Controls.Add(userControl); // 添加新的用户控件
            currentActiveControl = userControl; // 更新当前活动的控件

            // 为特定用户控件添加刷新/初始化逻辑
            if (statsControl != null)
            {
                statsControl.RefreshData();
            }
            else if (addControl != null)
            {
                addControl.ClearForm(); // 每次加载时清空表单
            }
            else if (browseControl != null)
            {
                browseControl.RefreshData();
            }
            else if (deleteControl != null)
            {
                deleteControl.RefreshData(); // 每次加载时刷新数据，以显示最新水果列表
            }
            else if (customerControl != null)
            {
                customerControl.RefreshData(); // 每次加载时刷新数据
            }
            // No specific action needed for AIQAUserControl upon initial load
        }

        // 侧边栏按钮点击事件处理方法

        private void btnBrowseFruits_Click(object sender, EventArgs e)
        {
            LoadUserControl(fruitBrowseControl);
        }

        private void btnAddFruit_Click(object sender, EventArgs e)
        {
            LoadUserControl(addFruitControl);
        }

        private void btnSalesStatistics_Click(object sender, EventArgs e)
        {
            LoadUserControl(adminSalesStatisticsControl);
        }

        // 新增的水果删除按钮点击事件
        private void btnDeleteFruit_Click(object sender, EventArgs e)
        {
            LoadUserControl(deleteFruitControl);
        }

        // 新增的顾客管理按钮点击事件
        private void btnManageCustomers_Click(object sender, EventArgs e)
        {
            LoadUserControl(customerManagementControl);
        }

        // 新增的 AI QA 按钮点击事件
        private void btnAIQA_Click(object sender, EventArgs e)
        {
            LoadUserControl(aiQAUserControl);
        }


        private void AdminDashboardForm_Load(object sender, EventArgs e)
        {
            // 管理员仪表盘加载时的逻辑
            // 默认加载已经在构造函数中设置 (LoadUserControl(fruitBrowseControl);)
        }

        private void mainContentPanel_Paint(object sender, PaintEventArgs e)
        {
            // This event handler is often not needed unless you're doing custom drawing.
            // You can usually leave it empty or remove it if not used.
        }
    }
}