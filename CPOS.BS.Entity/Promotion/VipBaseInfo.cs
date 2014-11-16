using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity.Promotion
{
    /// <summary>
    /// 会员基础信息
    /// </summary>
    [Serializable]
    public class VipBaseInfo : Pos.ObjectOperateInfo
    {
        public VipBaseInfo()
            : base()
        { }

        /// <summary>
        /// ID
        /// </summary>
        [XmlElement("vip_id")]
        public string ID
        { get; set; }

        /// <summary>
        /// 会员卡号
        /// </summary>
        [XmlElement("vip_no")]
        public string No
        { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [XmlElement("vip_name")]
        public string Name
        { get; set; }


        /// <summary>
        /// 性别
        /// </summary>
        [XmlElement("vip_gender")]
        public string Gender
        { get; set; }

        /// <summary>
        /// 英文名
        /// </summary>
        [XmlElement("vip_name_en")]
        public string EnglishName
        { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [XmlElement("vip_identity_no")]
        public string IdentityNo
        { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [XmlElement("vip_birthday")]
        public string Birthday
        { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [XmlElement("vip_address")]
        public string Address
        { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        [XmlElement("vip_postcode")]
        public string Postcode
        { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [XmlElement("vip_cell")]
        public string Cell
        { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [XmlElement("vip_email")]
        public string Email
        { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        [XmlElement("vip_qq")]
        public string QQ
        { get; set; }

        /// <summary>
        /// MSN
        /// </summary>
        [XmlElement("vip_msn")]
        public string MSN
        { get; set; }

        /// <summary>
        /// 微博
        /// </summary>
        [XmlElement("vip_weibo")]
        public string Weibo
        { get; set; }

        /// <summary>
        /// 当前积分
        /// </summary>
        [XmlElement("vip_points")]
        public int Points
        { get; set; }

        /// <summary>
        /// 办卡时间
        /// </summary>
        [XmlElement("activate_time")]
        public DateTime ActivateTime
        { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [XmlElement("vip_status")]
        public int Status
        { get; set; }

        /// <summary>
        /// 有效期限
        /// </summary>
        [XmlElement("vip_expired_date")]
        public string ExpiredDate
        { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [XmlElement("vip_remark")]
        public string Remark
        { get; set; }

        /// <summary>
        /// 版本(不可修改）
        /// </summary>
        public int Version
        { get; set; }
    }
}
