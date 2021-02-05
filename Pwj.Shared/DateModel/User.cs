﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwj.Shared.DateModel
{
    /// <summary>
    /// 功能描述    ：用户  
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/2/5 11:17:49 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/2/5 11:17:49 
    /// </summary>
    public partial class User : BaseEntity
    {
        /// <summary>
        /// 账户名称
        /// </summary>
        [Description("账户名称")]
        public string Account { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Description("用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [Description("地址")]
        public string Address { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [Description("电话")]
        public string Tel { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Description("邮箱")]
        public string Email { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// 最后登出时间
        /// </summary>
        public DateTime LastLogouTime { get; set; }

        /// <summary>
        /// 是否锁定
        /// </summary>
        public int IsLocked { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 是否为管理员  0/1 false/true
        /// </summary>
        public int FlagAdmin { get; set; }

        /// <summary>
        /// 是否在线
        /// </summary>
        public string FlagOnline { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        public int LoginCounter { get; set; }
    }
}
