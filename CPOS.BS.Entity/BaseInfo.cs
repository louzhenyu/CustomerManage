using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 基础信息集合（用户接口下载）
    /// </summary>
    public class BaseInfo
    {
        /// <summary>
        /// 当前用户详细信息
        /// </summary>
        public User.UserInfo CurrUserInfo { get; set; }

        /// <summary>
        /// 菜单集合
        /// </summary>
        public IList<MenuModel> CurrMenuInfoList { get; set; }

        /// <summary>
        /// 角色集合
        /// </summary>
        public IList<RoleModel> CurrRoleInfoList { get; set; }

        /// <summary>
        /// 角色与菜单关系集合
        /// </summary>
        public IList<RoleMenuModel> CurrRoleMenuInfoList { get; set; }

        /// <summary>
        /// 营业员集合
        /// </summary>
        public IList<User.UserInfo> CurrSalesUserInfoList { get; set; }

        /// <summary>
        /// 营业员与角色关系
        /// </summary>
        public IList<User.UserRoleInfo> CurrSalesUserRoleInfoList { get; set; }

        /// <summary>
        /// 组织列表
        /// </summary>
        public IList<UnitInfo> CurrUnitInfoList { get; set; }

    }
}
