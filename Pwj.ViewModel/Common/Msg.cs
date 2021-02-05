using GalaSoft.MvvmLight.Messaging;
using Pwj.Interfaces;
using Pwj.Shared.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwj.ViewModel.Common
{
    /// <summary>
    /// 功能描述    ：Msg  
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/2/1 16:50:33 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/2/1 16:50:33 
    /// </summary>
    public enum Notify
    {
        /// <summary>
        /// 错误
        /// </summary>
        [Description("错误")]
        Error,
        /// <summary>
        /// 警告
        /// </summary>
        [Description("警告")]
        Warning,
        /// <summary>
        /// 提示信息
        /// </summary>
        [Description("提示信息")]
        Info,
        /// <summary>
        /// 询问信息
        /// </summary>
        [Description("询问信息")]
        Question,
    }

    /// <summary>
    /// 消息类
    /// </summary>
    public class Msg
    {
        /// <summary>
        /// 信息提示
        /// </summary>
        /// <param name="msg"></param>
        public static void Info(string msg)
        {
            Messenger.Default.Send(msg, "Snackbar");
        }

        /// <summary>
        /// 错误提示
        /// </summary>
        /// <param name="msg"></param>
        public static void Error(string msg)
        {
            Messenger.Default.Send(msg, "Snackbar");
        }

        /// <summary>
        /// 真香警告
        /// </summary>
        /// <param name="msg"></param>
        public static void Warning(string msg)
        {
            Messenger.Default.Send(msg, "Snackbar");
        }

        /// <summary>
        /// 真香询问
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public async static Task<bool> Question(string msg)
        {
            return await Show(Notify.Question, msg);
        }

        /// <summary>
        /// 弹出窗口
        /// </summary>
        /// <param name="notify">类型</param>
        /// <param name="msg">文本信息</param>
        /// <returns></returns>
        private async static Task<bool> Show(Notify notify, string msg)
        {
            string Icon = string.Empty;
            string Color = string.Empty;
            switch (notify)
            {
                case Notify.Error:
                    Icon = "CommentWarning";
                    Color = "#FF4500";
                    break;
                case Notify.Warning:
                    Icon = "CommentWarning";
                    Color = "#FF8247";
                    break;
                case Notify.Info:
                    Icon = "CommentProcessingOutline";
                    Color = "#1C86EE";
                    break;
                case Notify.Question:
                    Icon = "CommentQuestionOutline";
                    Color = "#20B2AA";
                    break;
            }
            var dialog = NetCoreProvider.ResolveNamed<IMsgCenter>("MsgCenter");
            var result = await dialog.Show(new { Msg = msg, Color, Icon });
            return result;
        }
    }
}
