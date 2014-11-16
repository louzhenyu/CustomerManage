using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity.Exchange
{
    /// <summary>
    /// ͨ����Ϣ
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
        /// ���
        /// </summary>
        [XmlElement("announce_no")]
        public int No
        { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [XmlElement("announce_type")]
        public string Type
        { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [XmlElement("announce_title")]
        public string Title
        { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [XmlElement("announce_content")]
        public string Content
        { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        [XmlElement("announce_publisher")]
        public string Publisher
        { get; set; }

        /// <summary>
        /// ��ʼ����
        /// </summary>
        [XmlElement("begin_date")]
        public string BeginDate
        { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [XmlElement("end_date")]
        public string EndDate
        { get; set; }

        /// <summary>
        /// �����·�
        /// </summary>
        [XmlElement("allow_download")]
        public int AllowDownload
        { get; set; }

        /// <summary>
        /// ͨ�浥λ
        /// </summary>
        [XmlIgnore()]
        public IList<AnnounceUnitInfo> AnnounceUnits
        { get; set; }
    }
}
