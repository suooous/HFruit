using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// CurrentUser.cs (新文件)
namespace appfruit
{
    public static class CurrentUser
    {
        // 此属性将保存当前登录用户的用户名。
        // 您应该在成功登录后设置此属性。
        public static string UserName { get; set; }
        public static string UserType { get; set; }

        // 如果全局需要，您也可以在此处存储 CustomerId、Email 等
        public static int CustomerId { get; set; }
    }
}
