using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity.Pos
{
    /// <summary>
    /// �ֿ��뵥λ�Ĺ�����ϵ
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
        /// �ֿ�
        /// </summary>
        [XmlIgnore()]
        public WarehouseInfo Warehouse
        { get; set; }

        /// <summary>
        /// ��λ
        /// </summary>
        [XmlIgnore()]
        public UnitInfo Unit
        { get; set; }

    }
}
