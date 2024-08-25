using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwj.Interfaces
{
    /// <summary>
    /// 功能描述    ：数据分页接口  
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/1/29 14:53:35 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/1/29 14:53:35 
    public interface IDataPager
    {
        /// <summary>
        /// 总行数
        /// </summary>
        int TotalCount { get; set; }

        /// <summary>
        /// 每页数量
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        int PageIndex { get; set; }

        /// <summary>
        /// 总行数
        /// </summary>
        int PageCount { get; set; }

        /// <summary>
        /// 首页
        /// </summary>
        Task GoHomePage();

        /// <summary>
        /// 上一页
        /// </summary>
        Task GoOnPage();

        /// <summary>
        /// 下一页
        /// </summary>
        Task GoNextPage();

        /// <summary>
        /// 尾页
        /// </summary>
        Task GoEndPage();

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pageIndex"></param>
        Task GetPageData(int pageIndex);
    }
}
