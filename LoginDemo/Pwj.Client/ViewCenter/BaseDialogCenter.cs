using GalaSoft.MvvmLight.Messaging;
using Pwj.Interfaces;
using Pwj.Shared.Common;
using Pwj.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Pwj.Client.ViewCenter
{
    /// <summary>
    /// 功能描述    ： View/ViewModel 弹出式控制基类  
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/1/29 16:09:35 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/1/29 16:09:35 
    /// </summary>
    public class BaseDialogCenter<TView> where TView : Window, new()
    {
        public BaseDialogCenter(IBaseDialog viewModel)
        {
            this.viewModel = viewModel;//(IBaseDialog)viewModel.BaseAop(typeof(IBaseDialog));
        }

        public TView view = new TView();
        public IBaseDialog viewModel;

        /// <summary>
        /// 绑定默认ViewModel
        /// </summary>
        protected void BindDefaultViewModel()
        {
            view.DataContext = viewModel;
        }

        /// <summary>
        /// 打开窗口
        /// </summary>
        /// <returns></returns>
        public virtual async Task<bool> ShowDialog()
        {
            this.SubscribeMessenger();
            this.SubscribeEvent();
            this.BindDefaultViewModel();
            var result = view.ShowDialog();
            return await Task.FromResult((bool)result);
        }
        /// <summary>
        /// 注册默认事件
        /// </summary>
        public void SubscribeEvent()
        {
            view.MouseDown += (sender, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    view.DragMove();
            };
        }


        public virtual void SubscribeMessenger()
        {
            //最小化
            Messenger.Default.Register<string>(this, "WindowMinimize", (arg) =>
            {
                view.WindowState = System.Windows.WindowState.Minimized;
            });
            //最大化
            Messenger.Default.Register<string>(this, "WindowMaximize", (arg) =>
            {
                if (view.WindowState == System.Windows.WindowState.Maximized)
                    view.WindowState = System.Windows.WindowState.Normal;
                else
                    view.WindowState = System.Windows.WindowState.Maximized;
            });
            //关闭系统
            Messenger.Default.Register<string>(this, "Exit", async (arg) =>
            {
                //if (!await Msg.Question("确认退出系统?"))
                //    return;
                Environment.Exit(0);
            });
        }

        public virtual void UnsubscribeMessenger()
        {
           Messenger.Default.Unregister(this);
        }

    }
}
