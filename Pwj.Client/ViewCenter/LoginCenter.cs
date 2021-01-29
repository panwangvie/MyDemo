using Pwj.Client.View;
using Pwj.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwj.Client.ViewCenter
{
    /// <summary>
    /// 功能描述    ：登录控制类  
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/1/29 16:16:20 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/1/29 16:16:20 
    /// </summary>
    public class LoginCenter : BaseDialogCenter<LoginWin>, ILoginCenter
    {
        public LoginCenter(ILoginViewModel viewModel) : base(viewModel) { }
    }
}
