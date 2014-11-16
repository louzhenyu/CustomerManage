using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity.Pos
{
    /// <summary>
    /// pos与门店的关系
    /// </summary>
    public class PosUnitInfo
    {
        public PosUnitInfo()
            : this(new UnitInfo(), new PosInfo())
        { }

        public PosUnitInfo(UnitInfo unit)
            : this(unit, new PosInfo())
        { }

        public PosUnitInfo(PosInfo pos)
            : this(new UnitInfo(), pos)
        { }

        public PosUnitInfo(UnitInfo unit, PosInfo pos)
        {
            this.Unit = unit;
            this.Pos = pos;
        }

        public string ID
        { get; set; }

        /// <summary>
        /// 门店
        /// </summary>
        public UnitInfo Unit
        { get; set; }

        /// <summary>
        /// 终端
        /// </summary>
        public PosInfo Pos
        { get; set; }

        /// <summary>
        /// 分配时间
        /// </summary>
        public DateTime AllocateTime
        { get; set; }

        /// <summary>
        /// pos编号
        /// </summary>
        public string PosNo
        { get; set; }
    }
}
