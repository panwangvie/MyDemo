using log4net;
using log4net.Config;
using Pwj.Shared.DataInterfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pwj.Shared.DateModel
{
    /// <summary>
    /// 功能描述    ：日志实体  
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/2/1 8:56:22 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/2/1 8:56:22 
    /// </summary>
    public sealed class FlashLogger: IFLog
    {
        /// <summary>
        /// 记录消息Queue
        /// </summary>
        private readonly ConcurrentQueue<FlashLogMessage> _que;

        /// <summary>
        /// 信号
        /// </summary>
        private readonly ManualResetEvent _mre;

        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog _log;


        public FlashLogger()
        {
            var configFile = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config"));
            if (!configFile.Exists)
            {
                throw new Exception("未配置log4net配置文件！");
            }

            // 设置日志配置文件路径
            XmlConfigurator.Configure(configFile);

            _que = new ConcurrentQueue<FlashLogMessage>();
            _mre = new ManualResetEvent(false);
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            Register();

        }

        /// <summary>
        /// 另一个线程记录日志，只在程序初始化时调用一次
        /// </summary>
        public void Register()
        {
            Thread t = new Thread(new ThreadStart(WriteLog));
            t.IsBackground = false;
            t.Start();
        }

        /// <summary>
        /// 从队列中写日志至磁盘
        /// </summary>
        private void WriteLog()
        {
            while (true)
            {
                // 等待信号通知
                _mre.WaitOne();

                FlashLogMessage msg;
                // 判断是否有内容需要如磁盘 从列队中获取内容，并删除列队中的内容
                while (_que.Count > 0 && _que.TryDequeue(out msg))
                {
                    // 判断日志等级，然后写日志
                    switch (msg.Level)
                    {
                        case FlashLogLevel.Debug:
                            _log.Debug(msg.Message, msg.Exception);
                            break;
                        case FlashLogLevel.Info:
                            _log.Info(msg.Message, msg.Exception);
                            break;
                        case FlashLogLevel.Error:
                            _log.Error(msg.Message, msg.Exception);
                            break;
                        case FlashLogLevel.Warn:
                            _log.Warn(msg.Message, msg.Exception);
                            break;
                        case FlashLogLevel.Fatal:
                            _log.Fatal(msg.Message, msg.Exception);
                            break;
                    }
                }

                // 重新设置信号
                _mre.Reset();
                Thread.Sleep(1);
            }
        }


        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="message">日志文本</param>
        /// <param name="level">等级</param>
        /// <param name="ex">Exception</param>
        private void EnqueueMessage(string message, FlashLogLevel level, Exception ex = null)
        {

            if ((level == FlashLogLevel.Debug && _log.IsDebugEnabled)
             || (level == FlashLogLevel.Error && _log.IsErrorEnabled)
             || (level == FlashLogLevel.Fatal && _log.IsFatalEnabled)
             || (level == FlashLogLevel.Info && _log.IsInfoEnabled)
             || (level == FlashLogLevel.Warn && _log.IsWarnEnabled))
            {
                _que.Enqueue(new FlashLogMessage
                {
                    Message = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff") + "]\r\n" + message,
                    Level = level,
                    Exception = ex
                });

                // 通知线程往磁盘中写日志
                _mre.Set();
            }
        }


        public  void Error(Exception ex)
        {
            EnqueueMessage(ex.Message, FlashLogLevel.Fatal, ex);
        }


        public void Error(string msg, Exception ex = null)
        {
            EnqueueMessage(msg, FlashLogLevel.Error, ex);
        }

        public void Debug(string msg, Exception ex = null)
        {
            EnqueueMessage(msg, FlashLogLevel.Debug, ex);
        }

        public void Fatal(string msg, Exception ex = null)
        {
            EnqueueMessage(msg, FlashLogLevel.Fatal, ex);
        }

        public void Info(string msg, Exception ex = null)
        {
            EnqueueMessage(msg, FlashLogLevel.Info, ex);
        }

        public void Warn(string msg, Exception ex = null)
        {
            EnqueueMessage(msg, FlashLogLevel.Warn, ex);
        }

        public void LogStart()
        {

        }
        public  void LogEnd()
        {

        }

        public void Fatal(string message, params object[] args)
        {
        }

        public void Error(string message, params object[] args)
        {
        }

        public void Info(string message, params object[] args)
        {
        }

        public void Warn(string message, params object[] args)
        {
        }

        public void Debug(string message, params object[] args)
        {
        }
    }

    /// <summary>
    /// 日志等级
    /// </summary>
    public enum FlashLogLevel
    {
        Debug,
        Info,
        Error,
        Warn,
        Fatal
    }

    /// <summary>
    /// 日志内容
    /// </summary>
    public class FlashLogMessage
    {
        public string Message { get; set; }
        public FlashLogLevel Level { get; set; }
        public Exception Exception { get; set; }

    }
}
