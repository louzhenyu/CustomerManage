using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity.SAP
{
    [XmlRoot("BusinessPartners")]
    public class BusinessPartnersEntity
    {
        public BusinessPartnersEntity()
        {
            row = new BusinessPartnersRow();
        }
        public BusinessPartnersRow row { get; set; }
    }
    /// <summary>
    /// SAP用户需要传的数据
    /// </summary>
    public class BusinessPartnersRow
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        public string CardCode { get; set; }
        /// <summary>
        /// 旧系统编号
        /// </summary>
        public string AliasName { get; set; }
        /// <summary>
        /// 会员名称
        /// </summary>
        public string CardName { get; set; }
        /// <summary>
        /// 会员类型
        /// </summary>
        public string U_MembType { get; set; }
        /// <summary>
        /// 会员卡类型
        /// </summary>
        public string CardType { get; set; }
        /// <summary>
        /// 会员等级
        /// </summary>
        public string GlobalLocationNumber { get; set; }
    }
}
