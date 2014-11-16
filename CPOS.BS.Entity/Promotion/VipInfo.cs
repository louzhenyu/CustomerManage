using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity.Promotion
{
    /// <summary>
    /// ��Ա��Ϣ
    /// </summary>
    [Serializable]
    public class VipInfo : VipBaseInfo
    {
        public VipInfo()
            : base()
        {
            this.ActivateUnit = new UnitInfo();
            this.Type = new VipTypeInfo();
        }
       
        /// <summary>
        /// �쿨�ŵ�
        /// </summary>
        [XmlIgnore()]
        public UnitInfo ActivateUnit
        { get; set; }

        /// <summary>
        /// ״̬����
        /// </summary>
        [XmlIgnore()]
        public string StatusDescription
        { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [XmlIgnore()]
        public VipTypeInfo Type
        { get; set; }
    }
}
