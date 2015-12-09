using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity.WX
{
    /// <summary>
    /// 微信模板消息实体对象
    /// 订单发货提醒
    /// </summary>
    public class CommonData
    {
        public DataInfo first { get; set; }
        public DataInfo keyword1 { get; set; }
        public DataInfo keyword2 { get; set; }
        public DataInfo keyword3 { get; set; }
        public DataInfo remark { get; set; }
    }
    /// <summary>
    /// 微信模板消息实体对象
    /// 账户余额变动通知
    /// </summary>
    public class Balance
    {
        public DataInfo first { get; set; }
        public DataInfo keyword1 { get; set; }
        public DataInfo keyword2 { get; set; }
        public DataInfo keyword3 { get; set; }
        public DataInfo keyword4 { get; set; }
        public DataInfo keyword5 { get; set; }
        public DataInfo remark { get; set; }
    }
    /// <summary>
    /// 微信模板消息实体对象
    /// 返现到账通知
    /// </summary>
    public class CashBack
    {
        public DataInfo first { get; set; }
        public DataInfo order { get; set; }
        public DataInfo money { get; set; }
        public DataInfo remark { get; set; }
    }
    /// <summary>
    /// 微信模板消息实体对象
    /// 付款成功通知
    /// </summary>
    public class PaySuccess
    {
        public DataInfo first { get; set; }
        public DataInfo orderProductPrice { get; set; }
        public DataInfo orderProductName { get; set; }
        public DataInfo orderAddress { get; set; }
        public DataInfo orderName { get; set; }
        public DataInfo remark { get; set; }
    }
    /// <summary>
    /// 积分变动通知
    /// </summary>
    public class IntegralChange 
    {
        public DataInfo first { get; set; }
        public DataInfo FieldName { get; set; }
        public DataInfo Account { get; set; }
        public DataInfo change { get; set; }
        public DataInfo CreditChange { get; set; }
        public DataInfo CreditTotal { get; set; }

        public DataInfo Remark { get; set; }
    }
    public class DataInfo
    {
        public string value { get; set; }
        public string color { get; set; }
    }
}
