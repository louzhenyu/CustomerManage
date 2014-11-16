using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity
{
    [Serializable]
    [XmlRootAttribute("data")]
    public class CustomerInfo
    {
        public CustomerInfo()
            : base()
        {

        }

        /// <summary>
        /// ID
        /// </summary>
        [XmlElement("customer_id")]
        public string ID
        { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [XmlElement("customer_code")]
        public string Code
        { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [XmlElement("customer_name")]
        public string Name
        { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        [XmlElement("customer_name_en")]
        public string EnglishName
        { get; set; }


        /// <summary>
        /// 地址
        /// </summary>
        [XmlElement("customer_address")]
        public string Address
        { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        [XmlElement("customer_post_code")]
        public string PostCode
        { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [XmlElement("customer_contacter")]
        public string Contacter
        { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [XmlElement("customer_tel")]
        public string Tel
        { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        [XmlElement("customer_fax")]
        public string Fax
        { get; set; }

        /// <summary>
        /// E-Mail
        /// </summary>
        [XmlElement("customer_email")]
        public string Email
        { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [XmlElement("customer_cell")]
        public string Cell
        { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [XmlElement("customer_memo")]
        public string Memo
        { get; set; }

        /// <summary>
        /// 起始日期
        /// </summary>
        [XmlElement("customer_start_date")]
        public string StartDate
        { get; set; }

        /// <summary>
        /// 状态(编码)
        /// </summary>
        [XmlElement("customer_status")]
        public int Status
        { get; set; }
        /// <summary>
        /// 城市标识
        /// </summary>
        [XmlElement("city_id")]
        public string city_id { get; set; }

        /// <summary>
        /// 状态描述
        /// </summary>
        [XmlIgnore()]
        public string StatusDescription
        { get; set; }
    }
}
