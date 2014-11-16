using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 角色菜单关系
    /// </summary>
    [Serializable]
    public class RoleMenuModel
    {
        /// <summary>
        /// 角色菜单标识【保存必须】
        /// </summary>
        public string Role_Menu_Id { get; set; }
        /// <summary>
        /// 角色标识【保存必须】
        /// </summary>
        public string Role_Id { get; set; }
        /// <summary>
        /// 菜单标识【保存必须】
        /// </summary>
        public string Menu_Id { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string Create_User_Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string Create_Time { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string Modify_User_id { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public string Modify_Time { get; set; }
    }
}
