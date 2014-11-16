using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity.Pos
{
    /// <summary>
    /// pos信息
    /// </summary>
    [Serializable]
    [XmlRoot("data")]
    public class PosInfo : ObjectOperateInfo
    {
        /// <summary>
        /// ==
        /// </summary>
        public PosInfo()
            : base()
        { }

        /// <summary>
        /// ID
        /// </summary>
        [XmlElement("terminal_id")]
        public string ID
        { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [XmlElement("terminal_type")]
        public string Type
        { get; set; }

        /// <summary>
        /// 类型描述
        /// </summary>
        [XmlIgnore()]
        public string TypeDescription
        { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [XmlElement("terminal_code")]
        public string Code
        { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        [XmlElement("terminal_sn")]
        public string SN
        { get; set; }

        /// <summary>
        /// 购买日期
        /// </summary>
        [XmlElement("terminal_purchase_date")]
        public string PurchaseDate
        { get; set; }

        /// <summary>
        /// 出保日期
        /// </summary>
        [XmlElement("terminal_insurance_date")]
        public string InsuranceDate
        { get; set; }

        /// <summary>
        /// 数据交换地址
        /// </summary>
        [XmlElement("terminal_ws")]
        public string WS
        { get; set; }

        /// <summary>
        /// 备用数据交换地址
        /// </summary>
        [XmlElement("terminal_ws2")]
        public string WS2
        { get; set; }

        /// <summary>
        /// 软件的版本
        /// </summary>
        [XmlElement("terminal_software_version")]
        public string SoftwareVersion
        { get; set; }

        /// <summary>
        /// 数据库的版本
        /// </summary>
        [XmlElement("terminal_db_version")]
        public string DBVersion
        { get; set; }

        /// <summary>
        /// 持有方式
        /// </summary>
        [XmlElement("terminal_hold_type")]
        public string HoldType
        { get; set; }

        /// <summary>
        /// 持有方式描述
        /// </summary>
        [XmlIgnore()]
        public string HoldTypeDescription
        { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        [XmlElement("terminal_brand")]
        public string Brand
        { get; set; }

        /// <summary>
        /// 型号
        /// </summary>
        [XmlElement("terminal_model")]
        public string Model
        { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [XmlElement("terminal_remark")]
        public string Remark
        { get; set; }

        /// <summary>
        /// 钱箱
        /// </summary>
        [XmlElement("terminal_have_cashbox")]
        public int HaveCashbox
        { get; set; }

        /// <summary>
        /// 钱箱编号
        /// </summary>
        [XmlElement("terminal_cashbox_no")]
        public string CashboxNo
        { get; set; }

        /// <summary>
        /// 小票打印机
        /// </summary>
        [XmlElement("terminal_have_printer")]
        public int HavePrinter
        { get; set; }

        /// <summary>
        /// 小票打印机编号
        /// </summary>
        [XmlElement("terminal_printer_no")]
        public string PrinterNo
        { get; set; }

        /// <summary>
        /// 扫描枪
        /// </summary>
        [XmlElement("terminal_have_scanner")]
        public int HaveScanner
        { get; set; }

        /// <summary>
        /// 扫描枪编号
        /// </summary>
        [XmlElement("terminal_scanner_no")]
        public string ScannerNo
        { get; set; }

        /// <summary>
        /// 刷卡器
        /// </summary>
        [XmlElement("terminal_have_ecard")]
        public int HaveEcard
        { get; set; }

        /// <summary>
        /// 刷卡器编号
        /// </summary>
        [XmlElement("terminal_ecard_no")]
        public string EcardNo
        { get; set; }

        /// <summary>
        /// 支架
        /// </summary>
        [XmlElement("terminal_have_holder")]
        public int HaveHolder
        { get; set; }

        /// <summary>
        /// 支架编号
        /// </summary>
        [XmlElement("terminal_holder_no")]
        public string HolderNo
        { get; set; }

        /// <summary>
        /// 客显
        /// </summary>
        [XmlElement("terminal_have_client_display")]
        public int HaveClientDisplay
        { get; set; }

        /// <summary>
        /// 客显编号
        /// </summary>
        [XmlElement("terminal_client_display_no")]
        public string ClientDisplayNo
        { get; set; }

        /// <summary>
        /// 其他设备
        /// </summary>
        [XmlElement("terminal_have_other_device")]
        public int HaveOtherDevice
        { get; set; }

        /// <summary>
        /// 其他设备编号
        /// </summary>
        [XmlElement("terminal_other_device_no")]
        public string OtherDeviceNo
        { get; set; }
    }
}
