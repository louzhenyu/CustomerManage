using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity.Pos
{
    /// <summary>
    /// �ֿ���Ϣ
    /// </summary>
    [Serializable]
    public class WarehouseInfo : ObjectOperateInfo
    {
        public WarehouseInfo()
            : base()
        {
            this.Unit = new UnitInfo();
        }

        [XmlElement("warehouse_id")]
        public string warehouse_id
        { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [XmlElement("wh_code")]
        public string wh_code
        { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [XmlElement("wh_name")]
        public string wh_name
        { get; set; }

        /// <summary>
        /// Ӣ������
        /// </summary>
        [XmlElement("wh_name_en")]
        public string wh_name_en
        { get; set; }

        /// <summary>
        /// ��ַ
        /// </summary>
        [XmlElement("wh_address")]
        public string wh_address
        { get; set; }

        /// <summary>
        /// ��ϵ��
        /// </summary>
        [XmlElement("wh_contacter")]
        public string wh_contacter
        { get; set; }

        /// <summary>
        /// �绰
        /// </summary>
        [XmlElement("wh_tel")]
        public string wh_tel
        { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [XmlElement("wh_fax")]
        public string wh_fax
        { get; set; }

        /// <summary>
        /// ״̬
        /// </summary>
        [XmlElement("wh_status")]
        public int wh_status
        { get; set; }

        /// <summary>
        /// ״̬����
        /// </summary>
        [XmlIgnore()]
        public string wh_status_desc
        { get; set; }

        /// <summary>
        /// �Ƿ�ȱʡ�ֿ�(1:�ǣ�0-��
        /// </summary>
        [XmlElement("is_default")]
        public int is_default
        { get; set; }

        /// <summary>
        /// �Ƿ�ȱʡ�ֿ�����
        /// </summary>
        [XmlIgnore()]
        public string is_default_desc
        { get; set; }


        /// <summary>
        /// ��ע
        /// </summary>
        [XmlElement("wh_remark")]
        public string wh_remark
        { get; set; }

        [XmlElement("unit_id")]
        public string unit_id
        { get; set; }

        [XmlElement("unit_code")]
        public string unit_code
        { get; set; }

        [XmlElement("unit_name")]
        public string unit_name
        { get; set; }

        [XmlElement("unit_name_short")]
        public string unit_name_short
        { get; set; }
        

        /// <summary>
        /// ������λ
        /// </summary>
        [XmlIgnore()]
        public UnitInfo Unit
        { get; set; }

        [XmlIgnore()]
        public string create_user_name
        { get; set; }

        [XmlIgnore()]
        public string modify_user_name
        { get; set; }

        [XmlIgnore()]
        public DateTime create_time
        { get; set; }

        [XmlIgnore()]
        public DateTime modify_time
        { get; set; }
    }
}
