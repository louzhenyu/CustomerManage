using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity.Exchange
{
    /// <summary>
    /// 通告与单位的关系
    /// </summary>
    public class AnnounceUnitInfo
    {
        public AnnounceUnitInfo(AnnounceInfo announce)
            : this(announce, new UnitInfo())
        { }

        public AnnounceUnitInfo(JIT.CPOS.BS.Entity.UnitInfo unit)
            : this(new AnnounceInfo(), unit)
        { }

        public AnnounceUnitInfo(AnnounceInfo announce, JIT.CPOS.BS.Entity.UnitInfo unit)
        {
            this.Announce = announce;
            this.Unit = unit;
        }

        public AnnounceUnitInfo()
            : this(new AnnounceInfo(), new UnitInfo())
        { }

        /// <summary>
        /// 业务通告信息
        /// </summary>
        [XmlIgnore()]
        public AnnounceInfo Announce
        { get; set; }

        /// <summary>
        /// 单位信息
        /// </summary>
        [XmlIgnore()]
        public JIT.CPOS.BS.Entity.UnitInfo Unit
        { get; set; }
    }
}
