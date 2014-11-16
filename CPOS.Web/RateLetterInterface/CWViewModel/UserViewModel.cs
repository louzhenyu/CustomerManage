using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.RateLetterInterface
{
    /// <summary>
    /// 用户视图模型。
    /// </summary>
    public class UserViewModel
    {

        /// <summary>
        /// 用户标识
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户图像
        /// </summary>
        public string HighImageUrl { get; set; }

        /// <summary>
        /// 第三方用户标识
        /// </summary>
        public string VoipAccount { get; set; }

        /// <summary>
        /// VoIP密码
        /// </summary>
        public string VoipPwd { get; set; }

        /// <summary>
        /// 子账户Id
        /// </summary>
        public string SubAccountSid { get; set; }

        /// <summary>
        ///     子账户的授权令牌
        /// </summary>
        public string SubToken { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string UserGender { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string UserEmail { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string UserTelephone { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string Dept { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public string JobFunc { get; set; }

        /// <summary>
        /// 直接上级名称
        /// </summary>
        public string SuperiorName { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public string UserBirthday { set; get; }

        /// <summary>
        /// 创建讨论组权限
        /// </summary>
        public bool CreateGroupRight { set; get; }

        /// <summary>
        /// 电话
        /// </summary>
        public string UserCellphone { set; get; }
    }
}