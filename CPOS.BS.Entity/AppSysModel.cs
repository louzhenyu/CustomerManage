using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 应用系统类
    /// </summary>
    [Serializable]
    [XmlRootAttribute("data")]
    public class AppSysModel
    {
        /// <summary>
        /// 应用系统标识[保存必须]
        /// </summary>
        [XmlElement("def_app_id")] 
        public string Def_App_Id { get; set; }

        /// <summary>
        /// 应用系统号码[保存必须]
        /// </summary>
        [XmlElement("def_app_code")] 
        public string Def_App_Code { get; set; }

        /// <summary>
        /// 应用系统名称[保存必须]
        /// </summary>
       [XmlElement("def_app_name")] 
        public string Def_App_Name { get; set; }
    }
}
