
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BcdeditDemo
{
    /// <summary>
    ///  
    /// </summary>
    public class BcdeditCommand
    {
        /// <summary>
        /// 获取某个标识符的启动信息
        /// </summary>
        public static readonly string enumBootmgr1 = @"bcdedit /enum {bootmgr}";
        public static readonly string enumEFUI = @"bcdedit /enum {4e542f75-bf4f-11eb-997f-d99b009e83e4}";
        public static readonly string enumAll = @"bcdedit /enum all";
        /// <summary>
        /// 获取所有的 windows启动管理器列表的命令
        /// </summary>
        public static readonly string enumBootmgr = @"bcdedit /enum bootmgr";

        public static readonly string deviceStr = "device";
        public static readonly string pathStr = "path";
        public static readonly string descriptionStr = "description";

        /// <summary>
        /// 强制关机命令
        /// </summary>
        public static readonly string shutdownStr = "shutdown -s -f -t 0";
        /// <summary>
        /// 强制重启命令
        /// </summary>
        public static readonly string restartStr = "shutdown -r -f -t 0";

        public static readonly string androidId = "{4e542f75-bf4f-11eb-997f-d99b009e83e4}";

        /// <summary>
        /// windows系统自带的启动引导的标识符
        /// </summary>
        public static readonly string bootmgrId = "bootmgr";
        /// <summary>
        /// windows系统的启动引导的description描述
        /// </summary>
        public static readonly string osWindows = "os windows";
        /// <summary>
        /// 安卓系统的启动引导的 description描述
        /// </summary>
        public static readonly string UEFIOS = "UEFI OS";

    }
}
