using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 模板
    /// </summary>
    public class PaymentTypeInfo
    {
        public string Payment_Type_Id { get; set; }
        public string Payment_Type_Name { get; set; }
        public string Payment_Type_Code { get; set; }
        public string Status { get; set; }
        public string PaymentCompany { get; set; }
        public string PaymentItemType { get; set; }
        public string LogoURL { get; set; }
        public string PaymentDesc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String LastUpdateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsDelete { get; set; }
    }
}
