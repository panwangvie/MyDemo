using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwj.Interfaces
{
    /// <summary>
    /// 功能描述    ：IBaseCenter  
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/1/29 14:49:50 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/1/29 14:49:50 
    /// </summary>
    public interface IBaseCenter
    {
        /// <summary>
        /// 关联默认数据上下文
        /// </summary>
        void BindDefaultModel();

        object GetView();

        /// <summary>
        /// 关联默认数据上下文(包含权限相关)
        /// </summary>
        Task BindDefaultModel(int AuthValue = 0);

        /// <summary>
        /// 关联表格列
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        void BindDataGridColumns();
    }
}
