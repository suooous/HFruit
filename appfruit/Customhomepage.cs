using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing; // Make sure this is included if you use Font or Color in other parts

namespace appfruit
{
    public partial class Customhomepage : Form
    {
        private string _imageFolderPath;
        private UserControl currentActiveControl; // 跟踪当前活动的控件

        // 声明用户控件实例
        private FruitBrowseUserControl fruitBrowseControl;
        private ShoppingCartUserControl shoppingCartControl;
        private StatisticsUserControl statisticsControl; // <-- This is declared
        private AIQAUserControl aiqaControl;

        public Customhomepage()
        {
            InitializeComponent();

            string appExecutablePath = AppDomain.CurrentDomain.BaseDirectory;
            string projectRootPath = Path.GetFullPath(Path.Combine(appExecutablePath, @"..\..\"));
            _imageFolderPath = Path.Combine(projectRootPath, "Fimage");
            _imageFolderPath = Path.GetFullPath(_imageFolderPath);
            EnsureImagesFolderExists();

            // 如果用户控件保持状态，则只初始化一次
            fruitBrowseControl = new FruitBrowseUserControl(_imageFolderPath);
            shoppingCartControl = new ShoppingCartUserControl();
            statisticsControl = new StatisticsUserControl(); // <-- UNCOMMENT THIS LINE! 🎉
            aiqaControl = new AIQAUserControl();

            // 订阅 fruitBrowseControl 的 FruitAddedToCart 事件
            fruitBrowseControl.FruitAddedToCart += FruitBrowseControl_FruitAddedToCart;

            // 默认加载：FruitBrowseUserControl
            LoadUserControl(fruitBrowseControl);
        }

        private void EnsureImagesFolderExists()
        {
            if (!Directory.Exists(_imageFolderPath))
            {
                Directory.CreateDirectory(_imageFolderPath);
            }
        }

        // 修改后的 LoadUserControl 以处理现有实例
        private void LoadUserControl(UserControl userControl)
        {
            // Add a null check here to prevent issues if a null userControl is ever passed
            if (userControl == null)
            {
                MessageBox.Show("尝试加载一个空的用户控件。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (currentActiveControl == userControl)
            {
                // 如果请求的控件已激活，则不执行任何操作
                return;
            }

            mainContentPanel.Controls.Clear();

            userControl.Dock = DockStyle.Fill;
            mainContentPanel.Controls.Add(userControl);
            currentActiveControl = userControl;

            // ShoppingCartUserControl 的特定刷新逻辑
            if (userControl is ShoppingCartUserControl cartControl)
            {
                cartControl.LoadShoppingCart(); // 确保显示时购物车已刷新
            }
            // Add specific refresh logic for StatisticsUserControl here
            else if (userControl is StatisticsUserControl statsControl) // <-- Add this
            {
                statsControl.RefreshStatistics(); // Call RefreshStatistics when showing the control
            }
            // 如果需要，可以在此处添加其他用户控件的特定刷新逻辑
        }

        // FruitBrowseUserControl.FruitAddedToCart 的事件处理程序
        private void FruitBrowseControl_FruitAddedToCart(object sender, EventArgs e)
        {
            // 如果当前显示的是购物车控件，则刷新它
            if (currentActiveControl == shoppingCartControl)
            {
                shoppingCartControl.LoadShoppingCart();
            }
            // 您可能还想在主窗体上显示临时通知
            // 或者更改购物车图标的计数。
        }

        // 侧边栏按钮点击事件处理方法

        private void btnBrowseFruits_Click(object sender, EventArgs e)
        {
            LoadUserControl(fruitBrowseControl);
        }

        private void btnShoppingCart_Click(object sender, EventArgs e)
        {
            LoadUserControl(shoppingCartControl);
        }

        private void btnStatistics_Click(object sender, EventArgs e)
        {
            LoadUserControl(statisticsControl); // This will now work correctly
        }

        private void btnAI_QA_Click(object sender, EventArgs e)
        {
            LoadUserControl(aiqaControl);
        }

        private void Customhomepage_Load(object sender, EventArgs e)
        {
            // ... (your existing Customhomepage_Load logic)
        }

        private void Customhomepage_Load_1(object sender, EventArgs e)
        {
            // This is a duplicate Load event handler; it's good practice to remove it.
        }

        private void sidebarFlowPanel_Paint(object sender, PaintEventArgs e)
        {
            // ...
        }

        private void mainContentPanel_Paint(object sender, PaintEventArgs e)
        {
            // ...
        }
    }
}