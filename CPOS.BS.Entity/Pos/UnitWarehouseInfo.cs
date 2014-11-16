using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity.Pos
{
    /// <summary>
    /// 仓库与单位的关联关系
    /// </summary>
    [Serializable]
    public class UnitWarehouseInfo
    {
        public UnitWarehouseInfo()
            : this(new UnitInfo(), new WarehouseInfo())
        { }

        public UnitWarehouseInfo(UnitInfo unit)
            : this(unit, new WarehouseInfo())
        { }

        public UnitWarehouseInfo(WarehouseInfo warehouse)
            : this(new UnitInfo(), warehouse)
        { }

        public UnitWarehouseInfo(UnitInfo unit, WarehouseInfo warehouse)
        {
            this.Unit = unit;
            this.Warehouse = warehouse;
        }
        /// <summary>
        /// ID
        /// </summary>
        [XmlElement("unit_warehouse_id")]
        public string ID
        { get; set; }

        /// <summary>
        /// 仓库
        /// </summary>
        [XmlIgnore()]
        public WarehouseInfo Warehouse
        { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [XmlIgnore()]
        public UnitInfo Unit
        { get; set; }

    }
}
