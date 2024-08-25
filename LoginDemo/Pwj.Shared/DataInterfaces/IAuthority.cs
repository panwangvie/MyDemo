
using Pwj.Shared.Common;
using System.Collections.ObjectModel;

namespace Pwj.Shared.DataInterfaces
{
    /// <summary>
    /// 功能描述    ：ILog  
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/1/29 15:28:00 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/1/29 15:28:00 
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

        ObservableCollection<CommandStruct> ToolBarCommandList { get; set; }
    }
}
