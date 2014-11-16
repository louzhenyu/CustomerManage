using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity.Exchange
{
    /// <summary>
    /// 通告信息
    /// </summary>
    [Serializable]
    public class AnnounceInfo : Pos.ObjectOperateInfo
    {
        public AnnounceInfo()
        {
            AnnounceUnits = new List<AnnounceUnitInfo>();
        }

        /// <summary>
        /// ID
        /// </summary>
        [XmlElement("announce_id")]
        public string ID
        { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        [XmlElement("announce_no")]
        public int No
        { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [XmlElement("announce_type")]
        public string Type
        { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [XmlElement("announce_title")]
        public string Title
        { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [XmlElement("announce_content")]
        public string Content
        { get; set; }

        /// <summary>
        /// 发布人
        /// </summary>
        [XmlElement("announce_publisher")]
        public string Publisher
        { get; set; }

        /// <summary>
        /// 起始日期
        /// </summary>
        [XmlElement("begin_date")]
        public string BeginDate
        { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        [XmlElement("end_date")]
        public string EndDate
        { get; set; }

        /// <summary>
        /// 允许下发
        /// </summary>
        [XmlElement("allow_download")]
        public int AllowDownload
        { get; set; }

        /// <summary>
        /// 通告单位
        /// </summary>
        [XmlIgnore()]
        public IList<AnnounceUnitInfo> AnnounceUnits
        { get; set; }
    }
}
