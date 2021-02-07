using Pwj.Shared.DateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwj.Shared.Dto
{
    /// <summary>
    /// 功能描述    ：UerInfoDto  
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/2/5 11:20:04 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/2/5 11:20:04 
    /// </summary>
    public class UserInfoDto
    {
        public User User { get; set; }

        public List<Menu> Menus { get; set; }

    }
}
