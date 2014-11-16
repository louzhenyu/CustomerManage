using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Product.QiXinManage
{
    #region 部门信息
    /// <summary>
    /// 部门信息
    /// </summary>
    public class UnitInfo
    {
        public string UnitID { set; get; }
        public string UnitCode { set; get; }
        public string UnitName { set; get; }
        public string Leader { set; get; }
        public string DeptDesc { set; get; }
    }
    #endregion

    #region 用户信息
    public class UserInfo
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        public string UserID { set; get; }
        /// <summary>
        /// 用户Code
        /// </summary>
        public string UserCode { set; get; }
        /// <summary>
        /// email
        /// </summary>
        public string UserEmail { set; get; }
        /// <summary>
        /// 性别 
        /// 1男 2女
        /// </summary>
        public string UserGender { set; get; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { set; get; }
        /// <summary>
        /// 英文名
        /// </summary>
        public string UserNameEn { set; get; }
        /// <summary>
        /// 电话
        /// </summary>
        public string UserCellphone { set; get; }
        /// <summary>
        /// 手机
        /// </summary>
        public string UserTelephone { set; get; }
        /// <summary>
        /// 用户状态
        /// </summary>
        public string UserStatus { set; get; }

        public string UserBirthday { set; get; }
        /// <summary>
        /// 直接上级
        /// </summary>
        public string LeaderName { set; get; }

        public string LineManagerID { set; get; }
        /// <summary>
        /// 头像url
        /// </summary>
        public string HighImageUrl { set; get; }
        /// <summary>
        /// 积分
        /// </summary>
        public int Integral { set; get; }
        /// <summary>
        /// 云通讯帐号
        /// </summary>
        public string VoipAccount { set; get; }
        /// <summary>
        /// 部门
        /// </summary>
        public string DeptName { set; get; }
        /// <summary>
        /// 职务
        /// </summary>
        public string JobFuncName { set; get; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { set; get; }
        /// <summary>
        /// 最后一次修改时间
        /// </summary>
        public string ModifyTime { set; get; }
    }
    #endregion
}