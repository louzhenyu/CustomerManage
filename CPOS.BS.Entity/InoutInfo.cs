using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 进出库模板
    /// </summary>
    [Serializable]
    [XmlRootAttribute("data")]
    public class InoutInfo
    {
        /// <summary>
        /// 入出库单主标识 (必须)
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 订单号(必须)
        /// </summary>
        public string order_no { get; set; }
        /// <summary>
        /// 订单类型标识(必须)
        /// </summary>
        public string order_type_id { get; set; }
        /// <summary>
        /// 订单类型理由标识(必须)
        /// </summary>
        public string order_reason_id { get; set; }
        /// <summary>
        /// 红单标识（-1=红单，1=正单）(必须)
        /// </summary>
        public string red_flag { get; set; }
        /// <summary>
        /// 原单据标识
        /// </summary>
        public string ref_order_id { get; set; }
        /// <summary>
        /// 原单据号码
        /// </summary>
        public string ref_order_no { get; set; }
        /// <summary>
        /// 仓库标识
        /// </summary>
        public string warehouse_id { get; set; }
        /// <summary>
        /// 订单日期(必须)
        /// </summary>
        public string order_date { get; set; }
        /// <summary>
        /// 要求完成日期
        /// </summary>
        public string request_date { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public string complete_date { get; set; }
        /// <summary>
        /// 创建单位(必须)
        /// </summary>
        public string create_unit_id { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string unit_id { get; set; }
        /// <summary>
        /// 调整单位名称
        /// </summary>
        public string unit_name { get; set; }
        /// <summary>
        /// 相关单位标识
        /// </summary>
        public string related_unit_id { get; set; }
        /// <summary>
        /// 相关单位号码
        /// </summary>
        public string related_unit_code { get; set; }
        /// <summary>
        /// 终端标识
        /// </summary>
        public string pos_id { get; set; }
        /// <summary>
        /// 班次标识
        /// </summary>
        public string shift_id { get; set; }
        /// <summary>
        /// 销售人员
        /// </summary>
        public string sales_user { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal total_amount { get; set; }
        /// <summary>
        /// 折扣
        /// </summary>
        public decimal discount_rate { get; set; }
        /// <summary>
        /// 实际金额
        /// </summary>
        public decimal actual_amount { get; set; }
        /// <summary>
        /// 积分
        /// </summary>
        public decimal receive_points { get; set; }
        /// <summary>
        /// 支付积分
        /// </summary>
        public decimal pay_points { get; set; }
        /// <summary>
        /// 支付标识
        /// </summary>
        public string pay_id { get; set; }
        /// <summary>
        /// 支付方式名称【显示】
        /// </summary>
        public string payment_name { get; set; }
        /// <summary>
        /// 打印次数
        /// </summary>
        public int print_times { get; set; }
        /// <summary>
        /// 承运商标识
        /// </summary>
        public string carrier_id { get; set; }
        /// <summary>
        /// 承运商
        /// </summary>
        public string carrier_name { get; set; }
        /// <summary>
        /// 承运商地址
        /// </summary>
        public string carrier_address { get; set; }
        /// <summary>
        /// 承运商电话
        /// </summary>
        public string carrier_tel { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 订单操作状态
        /// </summary>
        public string optiontext { get; set; }
        /// <summary>
        /// 订单操作状态值
        /// </summary>
        public int optionvalue { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 状态描述
        /// </summary>
        public string status_desc { get; set; }
        /// <summary>
        /// 总数量
        /// </summary>
        public decimal total_qty { get; set; }
        /// <summary>
        /// 零售总价
        /// </summary>
        public decimal total_retail { get; set; }
        /// <summary>
        /// 找零
        /// </summary>
        public decimal keep_the_change { get; set; }
        /// <summary>
        /// 抹零
        /// </summary>
        public decimal wiping_zero { get; set; }
        /// <summary>
        /// vip卡号
        /// </summary>
        public string vip_no { get; set; }
        /// <summary>
        /// vip用户名
        /// </summary>
        public string vip_name { get; set; }
        /// <summary>
        /// vip code
        /// </summary>
        public string vip_code { get; set; }
        /// <summary>
        /// vipPhone
        /// </summary>
        public string vipPhone { get; set; }
        public int vipLevel { get; set; }
        public string vipLevelDesc { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string create_time { get; set; }
        /// <summary>
        /// 创建人标识
        /// </summary>
        public string create_user_id { get; set; }
        /// <summary>
        /// 审批时间
        /// </summary>
        public string approve_time { get; set; }
        /// <summary>
        /// 审批人标识
        /// </summary>
        public string approve_user_id { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public string send_time { get; set; }
        /// <summary>
        /// 发送人标识
        /// </summary>
        public string send_user_id { get; set; }
        /// <summary>
        /// 验收时间
        /// </summary>
        public string accpect_time { get; set; }
        /// <summary>
        /// 验收人标识
        /// </summary>
        public string accpect_user_id { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public string modify_time { get; set; }
        /// <summary>
        /// 修改人标识
        /// </summary>
        public string modify_user_id { get; set; }
        /// <summary>
        /// 创建单位名称
        /// </summary>
        public string create_unit_name { get; set; }
        /// <summary>
        /// 创建人名称
        /// </summary>
        public string create_user_name { get; set; }
        /// <summary>
        /// 审批人名称
        /// </summary>
        public string approve_user_name { get; set; }
        /// <summary>
        /// 发送人名称
        /// </summary>
        public string send_user_name { get; set; }
        /// <summary>
        /// 验收人名称
        /// </summary>
        public string accpect_user_name { get; set; }
        /// <summary>
        /// 修改人名称
        /// </summary>
        public string modify_user_name { get; set; }
        /// <summary>
        /// 订单类型1号码
        /// </summary>
        public string order_type_code { get; set; }
        /// <summary>
        /// 订单类型1名称
        /// </summary>
        public string order_type_name { get; set; }
        /// <summary>
        /// 订单类型2号码
        /// </summary>
        public string order_reason_code { get; set; }
        /// <summary>
        /// 订单类型2名称
        /// </summary>
        public string order_reason_name { get; set; }
        /// <summary>
        /// 销售单位标识
        /// </summary>
        public string sales_unit_id { get; set; }
        /// <summary>
        /// 采购单位标识
        /// </summary>
        public string purchase_unit_id { get; set; }
        /// <summary>
        /// 销售单位名称
        /// </summary>
        public string sales_unit_name { get; set; }
        /// <summary>
        /// 采购单位名称
        /// </summary>
        public string purchase_unit_name { get; set; }
        /// <summary>
        /// 仓库
        /// </summary>
        public string warehouse_name { get; set; }
        /// <summary>
        /// 销售仓库标识
        /// </summary>
        public string sales_warehouse_id { get; set; }
        /// <summary>
        /// 销售仓库
        /// </summary>
        public string sales_warehouse_name { get; set; }
        /// <summary>
        /// 采购仓库标识
        /// </summary>
        public string purchase_warehouse_id { get; set; }
        /// <summary>
        /// 采购仓库
        /// </summary>
        public string purchase_warehouse_name { get; set; }

        #region update by wzq

        public decimal integral { get; set; }
      
        #endregion
        /// <summary>
        /// inout 明细集合
        /// </summary>
        [XmlIgnore()]
        public IList<InoutDetailInfo> InoutDetailList
        {
            get;

            set;
        }
        /// <summary>
        /// inout集合
        /// </summary>
        [XmlIgnore()]
        public IList<InoutInfo> InoutInfoList { get; set; }
        /// <summary>
        /// 表单类型(必须)
        /// </summary>
        public string BillKindCode { get; set; }
        /// <summary>
        /// 单据来源标识(必须)
        /// </summary>
        public string data_from_id { get; set; }
        /// <summary>
        /// 单据来源名称
        /// </summary>
        public string data_from_name { get; set; }
        /// <summary>
        /// 行号
        /// </summary>
        public Int64 Row_No { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int ICount { get; set; }
        /// <summary>
        /// 操作 新建=“Create”，修改=“Modify”
        /// </summary>
        public string operate { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        private string _if_flag = "0";
        /// <summary>
        /// 是否下载标识
        /// </summary>
        public string if_flag
        {
            get { return _if_flag; }
            set
            {

                _if_flag = value;
            }
        }

        /// <summary>
        /// 客户标识
        /// </summary>
        public string customer_id { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string bat_id { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public Int64 displayIndex { get; set; }

        public string Field1 { get; set; }
        /// <summary>
        /// 快递单号
        /// </summary>
        public string Field2 { get; set; }
        /// <summary>
        /// 收件人
        /// </summary>
        public string Field3 { get; set; }
        /// <summary>
        /// 配送地址
        /// </summary>
        public string Field4 { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string Field5 { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Field6 { get; set; }
        /// <summary>
        /// 订单配送状态 0 = 已取消 1=未支付 2=已支付 3=配送中 4=已完成
        /// </summary>
        public string Field7 { get; set; }
        /// <summary>
        /// 配送方式  对应表 Delivery
        /// </summary>
        public string Field8 { get; set; }
        /// <summary>
        /// 配送时间
        /// </summary>
        public string Field9 { get; set; }
        /// <summary>
        /// 订单状态描述
        /// </summary>
        public string Field10 { get; set; }
        /// <summary>
        /// 支付类型 对应表DefrayType 
        /// </summary>
        public string Field11 { get; set; }
        public string Field12 { get; set; }
        public string Field13 { get; set; }
        public string Field14 { get; set; }
        public string Field15 { get; set; }
        public string Field16 { get; set; }
        public string Field17 { get; set; }
        public string Field18 { get; set; }
        public string Field19 { get; set; }
        public string Field20 { get; set; }
        /// <summary>
        /// 支付方式名称
        /// </summary>
        public string DefrayTypeName { get; set; }
        /// <summary>
        /// 配送方式
        /// </summary>
        public string DeliveryName { get; set; }
     

        /// <summary>
        /// 未审核
        /// </summary>
        public int StatusCount1 { get; set; }
        /// <summary>
        /// 未付款
        /// </summary>
        public int StatusCount2 { get; set; }

        /// <summary>
        /// 未发货
        /// </summary>
        public int StatusCount3 { get; set; }

        /// <summary>
        /// 已发货
        /// </summary>
        public int StatusCount4 { get; set; }

        /// <summary>
        /// 已完成
        /// </summary>
        public int StatusCount5 { get; set; }

        public int StatusCount6 { get; set; }

        public int StatusCount7 { get; set; }

        public int StatusCount8 { get; set; }

        public int StatusCount9 { get; set; }

        public int StatusCount10 { get; set; }

        public int StatusCount11 { get; set; }

        public int StatusCount12 { get; set; }

        public int StatusCount13 { get; set; }
        /// <summary>
        /// 已取消
        /// </summary>
        public int StatusCount0 { get; set; }
        /// <summary>
        /// 审核未通过
        /// </summary>
        public int StatusCount99 { get; set; }

        public Int64 timestamp { get; set; }

        public string linkMan { get; set; }
        public string linkTel { get; set; }
        public string address { get; set; }

        public string vipId { get; set; }
        public string openId { get; set; }
        public string phone { get; set; }
        public string vipRealName { get; set; }
        /// <summary>
        /// 配送费
        /// </summary>
        public decimal DeliveryAmount { get; set; }
        public decimal couponAmount { get; set; }//优惠券折扣
        public decimal vipEndAmount { get; set; }//余额支付

        public decimal IntegralBack { get; set; }//返回的积分
        public decimal AmountBack { get; set; }//返回的现金

        


        /// <summary>
        /// 积分折扣
        /// </summary>
        public decimal pay_pointsAmount { get; set; }
        /// <summary>
        /// 统计各状态的订单数量(jifeng.cao 20140418)
        /// </summary>
        public IList<StatusManager> StatusManagerList { get; set; }
        
        /// <summary>
        /// 渠道ID
        /// </summary>
        /// add by donal 2014-9-28 14:29:00
        public string ChannelId { get; set; }

        /// <summary>
        /// 场景
        /// </summary>
        /// add by doanl 2014-9-28 14:29:56
        public int OSSId { get; set; } 

        /// <summary>
        /// 推荐会员
        /// </summary>
        /// add by donal 2014-9-28 14:30:40
        public string RecommandVip { get; set; }

        /// <summary>
        /// 佣金
        /// </summary>
        public decimal ReturnCash { get; set; }

    }

    /// <summary>
    /// 统计对应状态的订单数量(jifeng.cao 20140418)
    /// </summary>
    public class StatusManager
    {
        /// <summary>
        /// 订单状态值
        /// </summary>
        public int StatusType { get; set; }
        /// <summary>
        /// 订单状态值名称
        /// </summary>
        public string StatusTypeName { get; set; }
        /// <summary>
        /// 对应状态的订单数量
        /// </summary>
        public int StatusCount { get; set; }

    }

}
