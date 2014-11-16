using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 类型
    /// </summary>
    [Serializable]
    [XmlRootAttribute("data")]
    public class TypeInfo
    {
        /// <summary>
        /// 类型标识
        /// </summary>
        [XmlElement("type_id")]
        public string Type_Id { get; set; }
        /// <summary>
        /// 号码
        /// </summary>
        public string Type_Code { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        [XmlElement("type_name")]
        public string Type_Name { get; set; }
        /// <summary>
        /// 类型英文名称
        /// </summary>
        [XmlElement("type_name_en")]
        public string Type_Name_En { get; set; }
        /// <summary>
        /// 类型域
        /// </summary>
        [XmlElement("type_domain")]
        public string Type_Domain { get; set; }
        /// <summary>
        /// 系统标志
        /// </summary>
        [XmlElement("type_system_flag")]
        public int Type_System_Flag { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [XmlElement("status")]
        public int Status { get; set; }
        /// <summary>
        /// 系统标志描述
        /// </summary>
        [XmlElement("type_system_flag_desc")]
        public string Type_System_Flag_Desc { get; set; }
        /// <summary>
        /// 状态描述
        /// </summary>
        [XmlElement("status_desc")]
        public string Status_Desc { get; set; }
    }
}
