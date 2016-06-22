using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 登录用户信息
    /// </summary>
    [Serializable]
    public class  LoggingSessionInfo : BasicUserInfo
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string Conn { get; set; }

        private string currentLanguageKindId;
        /// <summary>
        /// 语言
        /// </summary>
        public string CurrentLanguageKindId
        {
            get { return currentLanguageKindId; }
            set { currentLanguageKindId = value; }
        }
        

        private User.UserInfo currentUser;
        /// <summary>
        /// 登录用户
        /// </summary>
        public User.UserInfo CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; }
        }
        private User.UserRoleInfo currentUserRole;
        /// <summary>
        /// 登录用户的登录角色
        /// </summary>
        public User.UserRoleInfo CurrentUserRole
        {
            get { return currentUserRole; }
            set { currentUserRole = value; }
        }

        /// <summary>
        /// 管理平台用户登录传输的信息
        /// </summary>
        public LoggingManager CurrentLoggingManager { get; set; }

        /// <summary>
        /// TODO:拜访Lang
        /// </summary>
        public int Lang { get; set; }

        /// <summary>
        /// TODO:拜访ClientDistributorID
        /// </summary>
        public string ClientDistributorID {
            get { return "0"; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                ClientDistributorID = value;
            }
        }

        /// <summary>
        /// TODO:拜访ImgPath
        /// </summary>
        public string ImgPath { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string ClientName { get; set; }

        public string JsonSerialize()
        {
            throw new NotImplementedException();
        }
    }
}
