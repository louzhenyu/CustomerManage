using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 菜单模板
    /// </summary>
    [Serializable]
    [XmlRootAttribute("data")]
    //[XmlRoot("data")]
  
    public class MenuModel
    {


        /// <summary>
        /// 菜单标识【保存必须】
        /// </summary>
        [XmlElement("menu_id")] 
        public string Menu_Id { get; set; }
        /// <summary>
        /// 应用系统标识【保存必须】
        /// </summary>
        [XmlElement("reg_app_id")] 
        public string Reg_App_Id  { get; set; }
        /// <summary>
        /// 菜单号码【保存必须】
        /// </summary>
        [XmlElement("menu_code")] 
        public string Menu_Code  { get; set; }
        /// <summary>
        /// 父节点标识【保存必须】
        /// </summary>
        [XmlElement("parent_menu_id")] 
        public string Parent_Menu_Id  { get; set; }
        /// <summary>
        /// 菜单级别【保存必须】
        /// </summary>
        [XmlElement("menu_level")] 
        public int Menu_Level  { get; set; }
        /// <summary>
        /// 连接地址
        /// </summary>
        [XmlElement("url_path")] 
        public string  Url_Path { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        [XmlElement("icon_path")] 
        public string Icon_Path  { get; set; }
        /// <summary>
        /// 显示次序【保存必须】
        /// </summary>
        [XmlElement("display_index")] 
        public int Display_Index { get; set; }
	    /// <summary>
        /// 菜单名称【保存必须】
	    /// </summary>
        [XmlElement("menu_name")] 
        public string Menu_Name { get; set; }
        /// <summary>
        /// 用户标识
        /// </summary>
        [XmlElement("user_flag")] 
        public int User_Flag { get; set; }
        /// <summary>
        /// 菜单英文名称
        /// </summary>
        [XmlElement("menu_eng_name")] 
        public string Menu_Eng_Name { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [XmlElement("status")] 
        public int Status { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [XmlElement("create_user_id")] 
        public string Create_User_Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [XmlElement("create_time")] 
        public string Create_Time { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [XmlElement("modify_user_id")] 
        public string Modify_User_id { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [XmlElement("modify_time")] 
        public string Modify_Time { get; set; }

        /// <summary>
        /// 菜单下的子菜单列表
        /// </summary>
        [XmlIgnore()]
        public IList<MenuModel> SubMenuList { get; set; }

        /// <summary>
        /// 菜单下的页面
        /// </summary>
        [XmlIgnore()]
        public IList<PageInfo> PageList { get; set; }

        /// <summary>
        /// JS访问时的路径
        /// </summary>
        [XmlIgnore()]
        public string URLPathWithID
        {
            get
            {
                if (Menu_Level == 3)
                {
                    if (!string.IsNullOrEmpty(Url_Path) && Url_Path.IndexOf("?") > 0)
                        return Url_Path + "&cur_menu_id=" + Menu_Id;
                    else
                        return Url_Path + "?cur_menu_id=" + Menu_Id;
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 客户标识
        /// </summary>
        [XmlIgnore()]
        public string customer_id { get; set; }

        /// <summary>
        /// 前端页面勾选标记
        /// </summary>
        [XmlIgnore()]
        public string check_flag { get; set; }

        /// <summary>
        /// 前端页面是否子节点标记
        /// </summary>
        [XmlIgnore()]
        public string leaf_flag { get; set; }

        /// <summary>
        /// 前端页面是否展开标记
        /// </summary>
        [XmlIgnore()]
        public string expanded_flag { get; set; }

        /// <summary>
        /// 前端页面cls标记
        /// </summary>
        [XmlIgnore()]
        public string cls_flag { get; set; }

        /// <summary>
        /// 前端页面菜单下的子菜单列表
        /// </summary>
        [XmlIgnore()]
        public IList<MenuModel> children { get; set; }

        [XmlIgnore()]
        public string Reg_App_Name { get; set; }

        [XmlIgnore()]
        public string Parent_Menu_Name { get; set; }
        //支持老的树结构
        public string id { get{ return  Menu_Id; }}
        public string text { get { return Menu_Name; } }
        public string state { get { return expanded_flag == "true" ? "open" : "closed"; } }

    
      [JsonProperty(PropertyName = "checked")]//转换成json数据时按照这个名称
        public bool Checked { get { return check_flag=="true"?true:false; } }

    }
}
