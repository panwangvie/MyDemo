using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pwj.Interfaces
{
    /// <summary>
    /// 功能描述    ：IOrdinary  
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/1/29 14:51:34 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/1/29 14:51:34 
    /// </summary>
    public interface IOrdinary<TEntity> where TEntity : class

    {
        /// <summary>
        /// 选中表单
        /// </summary>
        TEntity GridModel { get; set; }
        /// <summary>
        /// 页索引
        /// </summary>
        int SelectPageIndex { get; set; }

        /// <summary>
        /// 搜索参数
        /// </summary>
        string Search { get; set; }

        /// <summary>
        /// 表单
        /// </summary>
        ObservableCollection<TEntity> GridModelList { get; set; }

        /// <summary>
        /// 搜索命令
        /// </summary>
        ICommand QueryCommand { get; }

        /// <summary>
        /// 其它命令
        /// </summary>
        ICommand ExecuteCommand { get; }

        /// <summary>
        /// 添加
        /// </summary>
        void AddAsync();

        /// <summary>
        /// 更新
        /// </summary>
        void UpdateAsync();

        /// <summary>
        /// 编辑
        /// </summary>
        Task DeleteAsync();

        /// <summary>
        /// 保存
        /// </summary>
        Task SaveAsync();

        /// <summary>
        /// 取消
        /// </summary>
        void Cancel();
    }
}
