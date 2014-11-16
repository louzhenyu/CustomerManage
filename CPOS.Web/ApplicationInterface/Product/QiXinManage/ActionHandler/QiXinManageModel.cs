using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Product.QiXinManage.ActionHandler
{
    #region 个人信息详情
    /// <summary>
    /// 个人信息详情
    /// </summary>
    public class PersonDetailInfo
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
        /// 生日
        /// </summary>
        public string UserBirthday { set; get; }
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
        /// <summary>
        /// 所属部门Id
        /// </summary>
        public string UnitID { set; get; }
        /// <summary>
        /// 所属部门名称
        /// </summary>
        public string UnitName { set; get; }
        /// <summary>
        /// 是否有建群权限
        /// </summary>
        public bool IsIMGroupCreator { set; get; }

    }

    #endregion

    #region 部门直接个人成员信息
    /// <summary>
    /// 部门直接个人成员信息
    /// </summary>
    public class PersonListItemInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { set; get; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { set; get; }
        /// <summary>
        /// 所属部门名称
        /// </summary>
        public string UnitName { set; get; }
        /// <summary>
        /// 用户职务
        /// </summary>
        public string JobFuncName { set; get; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string UserEmail { set; get; }
    }

    #endregion

    #region 部门信息
    /// <summary>
    /// 部门信息
    /// </summary>
    public class DepartmentInfo
    {
        /// <summary>
        /// 部门标识
        /// </summary>
        public string UnitID { set; get; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string UnitName { set; get; }
        /// <summary>
        /// 部门负责人
        /// </summary>
        public string Leader { set; get; }
        /// <summary>
        /// 父部门标识
        /// </summary>
        public string ParentUnitID { set; get; }
    }
    #endregion

    #region 部门直接成员
    /// <summary>
    /// 部门直接成员
    /// </summary>
    public class DepartmentTotalMember
    {
        /// <summary>
        /// 部门标识
        /// </summary>
        public string UnitID { set; get; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string UnitName { set; get; }
        /// <summary>
        /// 部门直接成员
        /// </summary>
        public List<PersonListItemInfo> DeptDirectPersMemberList { set; get; }
        /// <summary>
        /// 子级直接成员
        /// </summary>
        public List<DepartmentTotalMember> SubDepartmentList { set; get; }
    }
    #endregion

    #region 群组信息
    /// <summary>
    /// 群组信息
    /// </summary>
    public class IMGroupItemInfo
    {
        /// <summary>
        /// 群组ID
        /// </summary>
        public Guid? ChatGroupID { set; get; }
        /// <summary>
        /// 群组名称
        /// </summary>
        public string GroupName { set; get; }
        /// <summary>
        /// 群描述
        /// </summary>
        public string Description { set; get; }
        /// <summary>
        /// 群组人数
        /// </summary>
        public int UserCount { set; get; }
        /// <summary>
        /// 群组级别
        /// </summary>
        public int GroupLevel { set; get; }
        /// <summary>
        /// 云通讯群组ID
        /// </summary>
        public string BingGroupID { set; get; }
    }
    #endregion
}