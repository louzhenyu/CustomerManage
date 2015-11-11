using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 角色模板类
    /// </summary>
    [Serializable]
    public class RoleModel
    {
        /// <summary>
        /// 角色标识【保存必须】
        /// </summary>
        public string Role_Id { get; set; }

        /// <summary>
        /// 应用系统标识【保存必须】
        /// </summary>
        public string Def_App_Id { get; set; }
        /// <summary>
        /// 角色号码【保存必须】
        /// </summary>
        public string Role_Code { get; set; }
        /// <summary>
        /// 角色名称【保存必须】
        /// </summary>
        public string Role_Name { get; set; }
        /// <summary>
        /// 角色英文描述
        /// </summary>
        public string Role_Eng_Name { get; set; }
        /// <summary>
        /// 是否系统保留【保存必须】
        /// </summary>
        public int Is_Sys { get; set; }
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

        /// <summary>
        /// 创建人名称
        /// </summary>
        public string Create_User_Name { get; set; }

        /// <summary>
        /// 修改人名称
        /// </summary>
        public string Modify_User_Name { get; set; }

        /// <summary>
        /// 默认描述
        /// </summary>
        public string Default_Flag_Desc { get; set; }
        /// <summary>
        /// 行号
        /// </summary>
        public int Row_No { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int ICount { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage { get; set; }


        /// <summary>
        /// 菜单角色关系信息集合
        /// </summary>
        [XmlIgnore()]
        public IList<RoleMenuModel> RoleMenuInfoList { get; set; }
        /// <summary>
        /// 角色集合
        /// </summary>
        [XmlIgnore()]
        public IList<RoleModel> RoleInfoList { get; set; }

        /// <summary>
        /// 客户标识
        /// </summary>
        [XmlIgnore()]
        public string customer_id { get; set; }
        /// <summary>
        /// 系统名称
        /// </summary>
        public string Def_App_Name { get; set; }

        /// <summary>
        /// 门店类型的标识
        /// </summary>
        public string type_id { get; set; }

        /// <summary>
        /// 门店类型的等级
        /// </summary>
        public int org_level { get; set; }

        public string type_name { get; set; }

    }
}
