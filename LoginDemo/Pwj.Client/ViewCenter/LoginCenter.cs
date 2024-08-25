using GalaSoft.MvvmLight.Messaging;
using Pwj.Client.View;
using Pwj.Interfaces;
using Pwj.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwj.Client.ViewCenter
{
    /// <summary>
    /// 功能描述    ：登录控制类  
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/1/29 16:16:20 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/1/29 16:16:20 
    /// </summary>
    public class LoginCenter : BaseDialogCenter<LoginWin>, ILoginCenter
    {
        public LoginCenter(ILoginViewModel viewModel) : base(viewModel) { }

        public override void SubscribeMessenger()
        {
            Messenger.Default.Register<string>(view, "Snackbar", (arg) =>
            {
                App.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    //var messageQueue = view.SnackbarThree.MessageQueue;
                    //messageQueue.Enqueue(arg);
                }));
            });
            Messenger.Default.Register<string>(view, "NavigationPage", async (arg) =>
            {
                var dialog = NetCoreProvider.ResolveNamed<IMainCenter>("MainCenter");
                this.UnsubscribeMessenger();
                view.Close();
                await dialog.ShowDialog();
            });
            base.SubscribeMessenger();
        }

        public override void UnsubscribeMessenger()
        {
            base.UnsubscribeMessenger();
        }
    }
}
