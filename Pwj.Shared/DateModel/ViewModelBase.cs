using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Pwj.Shared.DateModel
{
    /// <summary>
    /// 功能描述    ：ViewModelBase  
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/2/5 10:54:06 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/2/5 10:54:06 
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void RaisePropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }

}
