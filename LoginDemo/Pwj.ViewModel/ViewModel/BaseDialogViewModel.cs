using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Pwj.Shared.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pwj.ViewModel.ViewModel
{
    /// <summary>
    /// 功能描述    ：MVVM基类  
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/1/29 15:18:31 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/1/29 15:18:31 
    /// </summary>
    public class BaseDialogViewModel: ViewModelBase
    {
        public BaseDialogViewModel()
        {
            ExitCommand = new RelayCommand(Exit);
        }

        private bool _isOpen;

        /// <summary>
        /// 窗口是否显示
        /// </summary>
        public bool DialogIsOpen
        {
            get { return _isOpen; }
            set { _isOpen = value; RaisePropertyChanged(); }
        }



        public ICommand ExitCommand { get; private set; }

        /// <summary>
        /// 通知异常
        /// </summary>
        /// <param name="msg"></param>
        [TryEx]

        public virtual void SnackBar(string msg)
        {
            Messenger.Default.Send(msg, "Snackbar");
        }

        /// <summary>
        /// 传递True代表需要确认用户是否关闭,你可以选择传递false强制关闭
        /// </summary>
        [TryEx]
        public virtual void Exit()
        {
            Messenger.Default.Send("", "Exit");
        }
    }
}
