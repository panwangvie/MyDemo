using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwj.Interfaces
{
    /// <summary>
    /// 功能描述    ：IAuthority  
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/1/29 14:54:39 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/1/29 14:54:39 
    /// </summary>
    /// <summary>
    /// 权限接口
    /// </summary>
    public interface IAuthority
    {
        /// <summary>
        /// 初始化权限
        /// </summary>
        /// <param name="authValue"></param>
        /// <returns></returns>
        void InitPermissions(int AuthValue);

        ObservableCollection<object> ToolBarCommandList { get; set; }
    }
}
