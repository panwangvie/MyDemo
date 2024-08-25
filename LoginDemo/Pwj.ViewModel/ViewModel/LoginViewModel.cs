using GalaSoft.MvvmLight.Command;
using Pwj.Interfaces;
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
    /// 功能描述    ：LoginViewModel  
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/1/29 15:17:35 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/1/29 15:17:35 
    /// </summary>
    public class LoginViewModel: BaseDialogViewModel, ILoginViewModel
    {
        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
            
        }

       

        #region Property
        private string _userName;
        private string _passWord;
        private string _report;
        private string _isCancel;
        //private readonly IUserRepository repository;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; RaisePropertyChanged(); }
        }

        public string PassWord
        {
            get { return _passWord; }
            set { _passWord = value; RaisePropertyChanged(); }
        }

        public string Report
        {
            get { return _report; }
            set { _report = value; RaisePropertyChanged(); }
        }

        public string IsCancel
        {
            get { return _isCancel; }
            set { _isCancel = value; RaisePropertyChanged(); }
        }


        #endregion

        #region Command
        [TryEx]
        public virtual async void Login()
        {
            if (DialogIsOpen) return;
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(PassWord))
            {
                SnackBar("请输入用户名密码!");
                return;
            }
            new Exception();
            DialogIsOpen = true;
            // return await new Task<bool>(() => false);
        }
        public ICommand LoginCommand { get; }

        

        #endregion
    }
}
