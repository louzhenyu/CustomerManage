using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity.Exchange
{
    /// <summary>
    /// 通告信息（查询）
    /// </summary>
    public class AnnounceQueryInfo : AnnounceInfo, IObjectQuery
    {
        public AnnounceQueryInfo()
            : base()
        { }

        /// <summary>
        /// 是否允许下发描述
        /// </summary>
        [XmlIgnore()]
        public string AllowDownloadDescription
        { get; set; }

        /// <summary>
        /// 通告单位
        /// </summary>
        [XmlIgnore()]
        public JIT.CPOS.BS.Entity.UnitInfo Unit
        {
            get
            {
                if (AnnounceUnits == null || AnnounceUnits.Count == 0)
                    return null;
                else
                    return AnnounceUnits[0].Unit;
            }
        }

        #region IObjectQuery Members

        public int RecordCount
        {
            get;
            set;
        }

        #endregion
    }
}
