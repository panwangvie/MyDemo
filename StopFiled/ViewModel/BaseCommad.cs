using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StopFiled.ViewModel
{
    public class BaseCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<object> mExecuteAction;              // 执行命令;
        private Func<bool> mCanExecuteFunc;         // 命令是否可以执行;

        public BaseCommand(Action action)
        {
            mExecuteAction = (r) => { action.Invoke(); };
        }

        public BaseCommand(Action<object> action)
        {
            mExecuteAction = action;

        }

        public BaseCommand(Action<object> action, Func<bool> func)
        {
            mExecuteAction = action;
            mCanExecuteFunc = func;
        }


        public bool CanExecute(object parameter)
        {
            if (mCanExecuteFunc != null)
            {
                return mCanExecuteFunc.Invoke();
            }
            else
            {
                return true;
            }
        }

        public void Execute(object parameter = null)
        {
            mExecuteAction.Invoke(parameter);
        }
    }
}
