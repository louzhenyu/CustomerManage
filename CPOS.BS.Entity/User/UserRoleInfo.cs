using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity.User
{
    /// <summary>
    /// 用户与单位、角色的关系
    /// </summary>
    [Serializable]
    public class UserRoleInfo
    {
        private string id;
        private string userId;
        private string unitId;
        private string roleId;
        private int defaultFlag = 0;

        private string userName;
        private string unitName;
        private string unitShortName;
        private string applicationDescription;
        private string roleName;
        /// <summary>
        /// Id
        /// </summary>
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// 用户
        /// </summary>
        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        /// <summary>
        /// 单位
        /// </summary>
        public string UnitId
        {
            get { return unitId; }
            set { unitId = value; }
        }
        /// <summary>
        /// 角色
        /// </summary>
        public string RoleId
        {
            get { return roleId; }
            set { roleId = value; }
        }
        /// <summary>
        /// 用户是否是该角色在该单位下的缺省用户(1:是,0:不是)
        /// </summary>
        public int DefaultFlag
        {
            get { return defaultFlag; }
            set { defaultFlag = value; }
        }

        /// <summary>
        /// 用户名称(查询结果用)
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        /// <summary>
        /// 单位名称(简称)(查询结果用)
        /// </summary>
        public string UnitName
        {
            get { return unitName; }
            set { unitName = value; }
        }
        /// <summary>
        /// 单位名称(简称)(查询结果用)
        /// </summary>
        public string UnitShortName
        {
            get { return unitShortName; }
            set { unitShortName = value; }
        }
        /// <summary>
        /// 角色所属的应用系统的描述(查询结果用)
        /// </summary>
        public string ApplicationDescription
        {
            get { return applicationDescription; }
            set { applicationDescription = value; }
        }
        /// <summary>
        /// 角色名称(查询结果用)
        /// </summary>
        public string RoleName
        {
            get { return roleName; }
            set { roleName = value; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 用户Role集合
        /// </summary>
        [XmlIgnore()]
        public IList<UserRoleInfo> UserRoleInfoList { get; set; }
    }
}
