using Pwj.Shared.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwj.Shared.DataInterfaces
{
    /// <summary>
    /// 功能描述    ：日志接口  
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/1/29 15:28:00 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/1/29 15:28:00 
    /// </summary>
     
    public interface IFLog
    {
        void Fatal(string msg, Exception ex = null);
        void Fatal(string message, params object[] args);
        void Error(string msg, Exception ex = null);
        void Error(Exception ex);
        void Error(string message, params object[] args);
        void Info(string msg, Exception ex = null);
        void Info(string message, params object[] args);
        void Warn(string msg, Exception ex = null);
        void Warn(string message, params object[] args);
        void Debug(string msg, Exception ex = null);
        void Debug(string message, params object[] args);
    }
}
