using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    public class ManageUserInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { set; get; }
        /// <summary>
        /// 用户Code
        /// </summary>
        public string UserCode { set; get; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { set; get; }
        /// <summary>
        /// 性别 : 0未知  1男  2女
        /// </summary>
        public string UserGender { set; get; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string UserEmail { set; get; }
        /// <summary>
        /// 手机
        /// </summary>
        public string UserTelephone { set; get; }
        /// <summary>
        /// 电话
        /// </summary>
        public string UserCellphone { set; get; }
    }
}
