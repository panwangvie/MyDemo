using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace Pwj.Shared.DateModel
{
    /// <summary>
    /// 功能描述    ：菜单  
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/2/5 11:17:58 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/2/5 11:17:58 
    /// </summary>
    public class Menu : BaseEntity
    {
        /// <summary>
        /// 菜单代码
        /// </summary>
        [Description("菜单代码")]
        public string MenuCode { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [Description("菜单名称")]
        public string MenuName { get; set; }

        /// <summary>
        /// 菜单标题
        /// </summary>
        [Description("菜单标题")]
        public string MenuCaption { get; set; }

        /// <summary>
        /// 命名空间
        /// </summary>
        public string MenuNameSpace { get; set; }

        /// <summary>
        /// 所属权限值
        /// </summary>
        public int MenuAuth { get; set; }
    }
}
