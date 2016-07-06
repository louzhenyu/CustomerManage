using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity.SAP
{
    [XmlRoot("BO")]
    public class SAPOrder
    {
        public SAPOrder()
        {
            AdmInfo = new AdmInfoEntity()
            {
                Object = "UDSORDR",
                Version = 2
            };
            this.UDSORDR = new UDSORDR();
            this.UDSORDR3 = new List<UDSORDR3Row>();
            this.UDSORDR31 = new List<UDSORDR31Row>();
            this.UDSORDR1 = new List<UDSORDR1Row>();
            this.UDSORDR2 = new List<UDSORDR2Row>();
        }
        public AdmInfoEntity AdmInfo { get; set; }

        public UDSORDR UDSORDR { get; set; }
        [XmlArrayItem("row")]
        public List<UDSORDR3Row> UDSORDR3 { get; set; }
        [XmlArrayItem("row")]
        public List<UDSORDR31Row> UDSORDR31 { get; set; }
        [XmlArrayItem("row")]
        public List<UDSORDR1Row> UDSORDR1 { get; set; }
        [XmlArrayItem("row")]
        public List<UDSORDR2Row> UDSORDR2 { get; set; }

    }

    #region 订单主表信息

    /// <summary>
    /// 订单主表信息
    /// </summary>
    public class UDSORDRRow
    {
        /// <summary>
        /// 订单条码(唯一)    order_no主表
        /// </summary>
        public string oBarCode { get; set; }
        /// <summary>
        /// 订单来源ID  OrderId主表
        /// </summary>
        public string SourceID { get; set; }
        /// <summary>
        /// 订单来源描述  写死SCRM
        /// </summary>
        public string SourceDesc { get; set; }
        /// <summary>
        /// 源编号 OrderId主表
        /// </summary>
        public string SourceEntry { get; set; }
        /// <summary>
        /// 订单类型编号    对应在T_Order_Reason_Type表
        /// </summary>
        public string oTypeNo { get; set; }
        /// <summary>
        /// 订单类型中文描述
        /// </summary>
        public string oType { get; set; }
        /// <summary>
        /// 客户类型    写死个人客户
        /// </summary>
        public string CardType { get; set; }
        /// <summary>
        /// 客户编号    VIPcardcode   vipcard表
        /// </summary>
        public string CardCode { get; set; }
        /// <summary>
        /// 客户名称   viprealname为空就取  vipname  vip表
        /// </summary>
        public string CardName { get; set; }
        /// <summary>
        /// 客户联系人   viprealname为空就取  vipname  vip表
        /// </summary>
        public string CardCntctPrsn { get; set; }
        /// <summary>
        /// 省代码
        /// </summary>
        public string ProvinceCode { get; set; }
        /// <summary>
        /// 省描述
        /// </summary>
        public string ProvinceName { get; set; }
        /// <summary>
        /// 市代码
        /// </summary>
        public string CityCode { get; set; }
        /// <summary>
        /// 市描述
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 区代码
        /// </summary>
        public string StreetCode { get; set; }
        /// <summary>
        /// 区描述
        /// </summary>
        public string StreetName { get; set; }
        /// <summary>
        /// 配送波次   默认A
        /// </summary>
        public string Wave { get; set; }
        /// <summary>
        /// 销售渠道编号  data_From_Id
        /// </summary>
        public string ChannlBPNo { get; set; }
        /// <summary>
        /// 销售渠道    sysvipsource表
        /// </summary>
        public string ChannlBP { get; set; }
        /// <summary>
        /// 销售员编号   sales_user
        /// </summary>
        public string SlpNo { get; set; }
        /// <summary>
        /// 销售员 user表取name
        /// </summary>
        public string SlpName { get; set; }
        /// <summary>
        /// 订单状态-1 正在创建
        ///-2 创键错误
        ///A 已创建
        ///P 分拣中
        ///D 配送中
        ///C 已完成
        ///L 已取消
        /// </summary>
        public string OrderStatus { get; set; }
        /// <summary>
        /// 配送状态    默认空
        /// </summary>
        public string DistStatus { get; set; }
        /// <summary>
        /// 下单人员    订单主表createby
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// 下单日期    订单主表createTime
        /// </summary>
        public string CreateDate { get; set; }
        /// <summary>
        /// 发货方式    主表flied8  关联delivery
        /// </summary>
        public string DocDueType { get; set; }
        /// <summary>
        /// 配送日期    配送时间/发货时间    主表flied9
        /// </summary>
        public string RequDate { get; set; }
        /// <summary>
        /// 要求开票日期  发票表预计开票时间
        /// </summary>
        public string BillDate { get; set; }
        /// <summary>
        /// 是否货票同行
        /// </summary>
        public string GoodsAndInvoice { get; set; }
        /// <summary>
        /// 送前通知
        /// </summary>
        public string IsCallBeDeli { get; set; }
        /// <summary>
        /// 收货地址-类型 默认0
        /// </summary>
        public string ShipAddrType { get; set; }
        /// <summary>
        /// 收货地址-地址 Field4
        /// </summary>
        public string ShipAddrInfo { get; set; }
        /// <summary>
        /// 收货地址-联系人    Field14
        /// </summary>
        public string ShipAddrCntct1 { get; set; }
        /// <summary>
        /// 收货地址-手机 Field6
        /// </summary>
        public string ShipAddrPhone1 { get; set; }
        /// <summary>
        /// 收货地址-固话    Field6
        /// </summary>
        public string ShipAddrTel1 { get; set; }
        /// <summary>
        /// 收货地址-联系人    默认空
        /// </summary>
        public string ShipAddrCntct2 { get; set; }
        /// <summary>
        /// 收货地址-手机 默认空
        /// </summary>
        public string ShipAddrPhone2 { get; set; }
        /// <summary>
        /// 收货地址-固话    默认空
        /// </summary>
        public string ShipAddrTel2 { get; set; }
        /// <summary>
        /// 配送时段编号  reserveQuantumID
        /// </summary>
        public string Duration { get; set; }
        /// <summary>
        /// 配送时段描述  reserveQuantum
        /// </summary>
        public string DeliveryInterval { get; set; }
        /// <summary>
        /// 签收方式编号  默认空
        /// </summary>
        public string SignWayNo { get; set; }
        /// <summary>
        /// 签收方式    默认空
        /// </summary>
        public string SignWay { get; set; }
        /// <summary>
        /// 首次送货送卡  第一期默认N
        /// </summary>
        public string sCardWthFD { get; set; }
        /// <summary>
        /// 包装备注
        /// </summary>
        public string PackRemarks { get; set; }
        /// <summary>
        /// 物流线路编号  默认空
        /// </summary>
        public string LogiLineNo { get; set; }
        /// <summary>
        /// 物流线路    默认空
        /// </summary>
        public string LogiLine { get; set; }
        /// <summary>
        /// 物流线路    默认空
        /// </summary>
        public string LogiRemarks { get; set; }
        /// <summary>
        /// 折扣方式    默认空
        /// </summary>
        public string DiscType { get; set; }
        /// <summary>
        /// 折扣备注
        /// </summary>
        public string DiscRemarks { get; set; }
        /// <summary>
        /// 交易类型    默认1
        /// </summary>
        public string TranType { get; set; }
        /// <summary>
        /// 订单积分    receive_points
        /// </summary>
        public string DoPoints { get; set; }
        /// <summary>
        /// 是否开票    flied19  如果不为空采去取开票详细信息
        /// </summary>
        public string IsInvoice { get; set; }
        /// <summary>
        /// 是否送卡    第一期默认N
        /// </summary>
        public string IsSendCard { get; set; }
        /// <summary>
        /// 物流代收款   货到付款实收金额，线上支付填0
        /// </summary>
        public string AmountPay { get; set; }
        /// <summary>
        /// 购买类型    默认普通
        /// </summary>
        public string BuyType { get; set; }
        /// <summary>
        /// 金额合计    total_amount
        /// </summary>
        public string TotalAmount { get; set; }
        /// <summary>
        /// 运费  
        /// </summary>
        public string TotalExpns { get; set; }
        /// <summary>
        /// 抵扣额 total_amount-actual_amount
        /// </summary>
        public string Deduction { get; set; }
        /// <summary>
        /// 赠送合计    等于抵扣额
        /// </summary>
        public string TotalGift { get; set; }
        /// <summary>
        /// 跑单时间    默认当前时间
        /// </summary>
        public string RUN_END_TIME { get; set; }
        /// <summary>
        /// 所属地域编号
        /// </summary>
        public string RegionNo { get; set; }
        /// <summary>
        /// 所属地域
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// 发货地点编号
        /// </summary>
        public string IssuLocaNo { get; set; }
        /// <summary>
        /// 发货地点
        /// </summary>
        public string IssuLoca { get; set; }
        /// <summary>
        /// 成本中心    默认空 
        /// </summary>
        public string PrcCode { get; set; }
        /// <summary>
        /// 运输方式编号  默认空
        /// </summary>
        public string TranModNo { get; set; }
        /// <summary>
        /// 运输方式    默认空
        /// </summary>
        public string TranMod { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BalanceMethodID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BalanceMethod { get; set; }
        /// <summary>
        /// 订单序号    默认0
        /// </summary>
        public string BoxNum { get; set; }
        /// <summary>
        /// 源单卡号    默认空
        /// </summary>
        public string CardNo { get; set; }
        /// <summary>
        /// 源单编号    默认空
        /// </summary>
        public string OrderCodeOrigial { get; set; }
        /// <summary>
        /// 跑单号
        /// </summary>
        public string OrderRunID { get; set; }
    }

    /// <summary>
    /// 订单主表信息
    /// </summary>
    public class UDSORDR
    {
        public UDSORDR()
        {
            row = new UDSORDRRow();
        }
        public UDSORDRRow row { get; set; }
    }

    #endregion

    #region 商品信息

    /// <summary>
    /// 商品主表
    /// </summary>
    public class UDSORDR3Row
    {
        /// <summary>
        /// boss的ID    我们没有   默认给-1
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 跑单时间    默认当前时间
        /// </summary>
        public string RUN_END_TIME { get; set; }
        /// <summary>
        /// 跑单号 默认-1
        /// </summary>
        public string ORDER_RUN_ID { get; set; }
        /// <summary>
        /// 行号
        /// </summary>
        public string LineID { get; set; }
        /// <summary>
        /// 订单号 order_no
        /// </summary>
        public string oBarCode { get; set; }
        /// <summary>
        /// 默认0
        /// </summary>
        public string ORDER_ITEM_ID { get; set; }
        /// <summary>
        /// 对照编号    skuid
        /// </summary>
        public string U_OldCode { get; set; }
        /// <summary>
        /// 产品编号    skuid
        /// </summary>
        public string ItemCode { get; set; }
        /// <summary>
        /// 产品名称    itemname
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 计量单位
        /// </summary>
        public string ItemUnit { get; set; }
        /// <summary>
        /// 数量  order_qty
        /// </summary>
        public string Qty { get; set; }
        /// <summary>
        /// 单价  std_price
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// 折扣  discount_rate转换  除以100
        /// </summary>
        public string Discount { get; set; }
        /// <summary>
        /// 折扣金额    std_price-enter_price
        /// </summary>
        public string DiscountAmount { get; set; }
        /// <summary>
        /// 折扣单价    enter_price
        /// </summary>
        public string DiscountPrice { get; set; }
        /// <summary>
        /// 小计  enter_amount
        /// </summary>
        public string LineTotal { get; set; }
        /// <summary>
        /// 产品类型    默认I
        /// </summary>
        public string ItemType { get; set; }
        /// <summary>
        /// 虚拟商品标识  ifservice
        /// </summary>
        public string IsVirItem { get; set; }
        /// <summary>
        /// 预分拣标识   默认空
        /// </summary>
        public string IsPreSort { get; set; }
        /// <summary>
        /// 仓库编号    默认空
        /// </summary>
        public string WhsCode { get; set; }
        /// <summary>
        /// 状态  默认A
        /// </summary>
        public string LineStatus { get; set; }
        /// <summary>
        /// 备注  默认空
        /// </summary>
        public string LineRemarks { get; set; }
        /// <summary>
        /// 存储方式    属性表取
        /// </summary>
        public string StoreMthd { get; set; }
    }

    #endregion

    #region BOM信息
    
    /// <summary>
    /// 商品BOM表
    /// </summary>
    public class UDSORDR31Row
    {
        /// <summary>
        /// 跑单时间    默认当前时间
        /// </summary>
        public string RUN_END_TIME { get; set; }
        /// <summary>
        /// 跑单号 默认-1
        /// </summary>
        public string ORDER_RUN_ID { get; set; }
        /// <summary>
        /// 父级行号
        /// </summary>
        public string LineID { get; set; }
        /// <summary>
        /// 当前表行号
        /// </summary>
        public string cLineID { get; set; }
        /// <summary>
        /// 来源订单ID   默认-1
        /// </summary>
        public string ORDER_ID { get; set; }
        /// <summary>
        /// BOSS订单号   orderno
        /// </summary>
        public string oBarCodeBoss { get; set; }
        /// <summary>
        /// 来源单行号   默认-1
        /// </summary>
        public string LineIDFPBoss { get; set; }
        /// <summary>
        /// 产品编号    skuid
        /// </summary>
        public string ItemCode { get; set; }
        /// <summary>
        /// 对照编号    skuid
        /// </summary>
        public string U_OldCode { get; set; }
        /// <summary>
        /// 产品名称    itemname
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 数量  order_qty
        /// </summary>
        public string Qty { get; set; }
        /// <summary>
        /// 计量单位
        /// </summary>
        public string ItemUnit { get; set; }
        /// <summary>
        /// 单价  std_price
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// 折扣  discount_rate转换  除以100
        /// </summary>
        public string Discount { get; set; }
        /// <summary>
        /// 小计  enter_amount
        /// </summary>
        public string LineTotal { get; set; }
        /// <summary>
        /// 产品类型    默认I
        /// </summary>
        public string ItemType { get; set; }

        /// <summary>
        /// 仓库编号    默认空
        /// </summary>
        public string WhsCode { get; set; }
        /// <summary>
        /// 状态  默认A
        /// </summary>
        public string LineStatus { get; set; }
        /// <summary>
        /// 备注  默认空
        /// </summary>
        public string LineRemarks { get; set; }
        /// <summary>
        /// 默认-1
        /// </summary>
        public string ORDER_BOX_ID { get; set; }
    }
    #endregion

    #region 订单发票信息
    
    /// <summary>
    /// 订单发票表
    /// </summary>
    public class UDSORDR1Row
    {
        /// <summary>
        /// 跑单时间    默认当前时间
        /// </summary>
        public string RUN_END_TIME { get; set; }
        /// <summary>
        /// 跑单号 默认-1
        /// </summary>
        public string ORDER_RUN_ID { get; set; }
        /// <summary>
        /// 行号
        /// </summary>
        public string LineID { get; set; }
        /// <summary>
        /// 来源订单ID   默认-1
        /// </summary>
        public string ORDER_ID { get; set; }
        /// <summary>
        /// 开票部门编号  DeptID   
        /// </summary>
        public string BillDepNo { get; set; }
        /// <summary>
        /// 开票部门    DeptName
        /// </summary>
        public string BillDepName { get; set; }
        /// <summary>
        /// 发票类型编号  InvoiceType
        /// </summary>
        public string InvoTypeNo { get; set; }
        /// <summary>
        /// 发票类型   InvoiceContentType

        /// </summary>
        public string InvoType { get; set; }
        /// <summary>
        /// 发票抬头    InvoiceHeader
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 发票金额    InvoiceAmount
        /// </summary>
        public string Amount { get; set; }
        /// <summary>
        /// 发票内容    InvoiceContent
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 出票日期  ExpectInvoiceDate
        /// </summary>
        public string InvoiceDate { get; set; }
        /// <summary>
        /// 实际开票日期   InvoiceDate
        /// </summary>
        public string BillDate { get; set; }
        /// <summary>
        /// 发票号码  InvoiceNo
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 开票人代码  LastUpdateBy
        /// </summary>
        public string DrawerNo { get; set; }
        /// <summary>
        /// 开票人名称  user username
        /// </summary>
        public string DrawerName { get; set; }
        /// <summary>
        /// 省代码 
        /// </summary>
        public string ProvinceCode { get; set; }
        /// <summary>
        /// 省描述
        /// </summary>
        public string ProvinceName { get; set; }
        /// <summary>
        /// 市代码
        /// </summary>
        public string CityCode { get; set; }
        /// <summary>
        /// 市描述
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 区代码
        /// </summary>
        public string StreetCode { get; set; }
        /// <summary>
        /// 区描述
        /// </summary>
        public string StreetName { get; set; }
        /// <summary>
        /// 收票地址    DeliveryAddress
        /// </summary>
        public string BillAddress { get; set; }
        /// <summary>
        /// 发货方式    DeliveryMethod
        /// </summary>
        public string BillMethod { get; set; }
        /// <summary>
        /// 行状态 默认1
        /// </summary>
        public string LineStatus { get; set; }
        /// <summary>
        /// 税号  TaxNo
        /// </summary>
        public string FaxNo { get; set; }
        /// <summary>
        /// 备注  默认空
        /// </summary>
        public string LineRemarks { get; set; }
        /// <summary>
        /// BOSS订单号 orderno
        /// </summary>
        public string oBarCodeBoss { get; set; }
        /// <summary>
        /// 默认0
        /// </summary>
        public string LineIDPayBoss { get; set; }
        /// <summary>
        /// 最后更新人   LastUpdateBy
        /// </summary>
        public string LastUpdateman { get; set; }
        /// <summary>
        /// 最后更新时间  LastUpdateTime
        /// </summary>
        public string LastUpdateMan1 { get; set; }
    }
    #endregion

    #region 订单结算信息
    
    /// <summary>
    /// 订单结算表
    /// </summary>
    public class UDSORDR2Row
    {
        /// <summary>
        /// 跑单时间    默认当前时间
        /// </summary>
        public string RUN_END_TIME { get; set; }
        /// <summary>
        /// 跑单号 默认-1
        /// </summary>
        public string ORDER_RUN_ID { get; set; }
        /// <summary>
        /// 父级行号
        /// </summary>
        public string LineID { get; set; }
        /// <summary>
        /// 来源订单ID   默认-1
        /// </summary>
        public string ORDER_ID { get; set; }
        /// <summary>
        /// 结算方式编号   
        /// </summary>
        public string MethodNo { get; set; }
        /// <summary>
        /// 结算方式
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// 收款信息-类型
        /// </summary>
        public string RecType { get; set; }
        /// <summary>
        /// 收款信息-类型编号   默认空

        /// </summary>
        public string RecTypeNo { get; set; }
        /// <summary>
        /// 收款信息-支付方式    默认空
        /// </summary>
        public string PayMode { get; set; }
        /// <summary>
        /// 收款信息-支付方式编号    默认空
        /// </summary>
        public string PayModeNo { get; set; }
        /// <summary>
        /// 收款信息-实收金额    total_amount
        /// </summary>
        public string Collected { get; set; }
        /// <summary>
        /// 收款信息-收款凭证号  默认空
        /// </summary>
        public string VoucherNo { get; set; }
        /// <summary>
        /// 收款信息-付款时间   createtime
        /// </summary>
        public string PayDate { get; set; }
        /// <summary>
        /// 收款信息-财务审核  默认Y
        /// </summary>
        public string Fchecked { get; set; }
        /// <summary>
        /// 收款信息-审核时间  默认当前时间
        /// </summary>
        public string CheckDate { get; set; }
        /// <summary>
        /// 行状态  默认-1
        /// </summary>
        public string LineStatus { get; set; }
        /// <summary>
        /// 备注  默认空
        /// </summary>
        public string LineRemarks { get; set; }
        /// <summary>
        /// BOSS订单号 orderno
        /// </summary>
        public string oBarCodeBoss { get; set; }
        /// <summary>
        /// 默认0
        /// </summary>
        public string LineIDPayBoss { get; set; }
    }
    #endregion
}
