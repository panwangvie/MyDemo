using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwj.Shared.Query
{
    /// <summary>
    /// 功能描述    ：搜索基类  
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/2/4 17:48:31 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/2/4 17:48:31 
    /// </summary>

    public class QueryParameters
    {
        private int _pageIndex = 0;
        public int PageIndex
        {
            get => _pageIndex;
            set => _pageIndex = value;
        }

        private int _pageSize = 10;
        public virtual int PageSize
        {
            get => _pageSize;
            set => _pageSize = value;
        }

        public string Search { get; set; }

    }
}
