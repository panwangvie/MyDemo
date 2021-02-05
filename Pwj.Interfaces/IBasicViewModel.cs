using Pwj.Shared.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pwj.Interfaces
{
    /// <summary>
    /// 功能描述    ：IBasicViewModel  
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/1/29 14:46:51 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/1/29 14:46:51 
    /// </summary>

    #region 模块接口

    /// <summary>
    /// 实现基础的增删改查、分页、权限接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseViewModel<TEntity> : IOrdinary<TEntity>, IDataPager, IAuthority where TEntity : class
    { }

    //public interface IUserViewModel : IBaseViewModel<UserDto>
    //{ }

    //public interface IGroupViewModel : IBaseViewModel<GroupDto>
    //{ }

    //public interface IMenuViewModel : IBaseViewModel<MenuDto>
    //{ }

    //public interface IBasicViewModel : IBaseViewModel<TEntity>
    //{ }

    #endregion

    #region 组件接口

    public interface IComponentViewModel { }
    public interface ISkinViewModel : IComponentViewModel { }
    public interface IDashboardViewModel : IComponentViewModel { }
    public interface IHomeViewModel : IComponentViewModel { }
    public interface ILoginViewModel : IBaseDialog { }
    public interface IMainViewModel : IBaseDialog
    {
        Task InitDefaultView();
    }

    #endregion

    #region ICenter

    public interface ILoginCenter
    {
        Task<bool> ShowDialog();
    }
    public interface IMainCenter
    {
        Task<bool> ShowDialog();
    }
    public interface IUserCenter : IBaseCenter { }
    public interface IMenuCenter : IBaseCenter { }
    public interface IGroupCenter : IBaseCenter { }
    public interface IBasicCenter : IBaseCenter { }
    public interface IHomeCenter : IBaseCenter { }
    public interface IDashboardCenter : IBaseCenter { }
    public interface ISkinCenter : IBaseCenter { }
    public interface IMsgCenter
    {
        Task<bool> Show(object obj);
    }

    #endregion
    /// <summary>
    /// 弹窗窗口基础接口
    /// </summary>
    public interface IBaseDialog
    {
        bool DialogIsOpen { get; set; }

        void SnackBar(string msg);

        ICommand ExitCommand { get; }
    }
}
