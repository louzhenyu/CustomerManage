/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/29 17:49:07
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.Web;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class DonateBLL
    {
        /// <summary>
        /// 订单号生成
        /// </summary>
        /// <returns></returns>
        public Order CreateOrderNo()
        {
            string url = "http://o2oapi.aladingyidong.com/OnlineShopping/data/Data.aspx?action=setOrderInfo&ReqContent={\"common\":{\"openId\":\"oxbbcjrc2RgSbCyYt_5fYMvmqqtE\",\"customerId\":\"e703dbedadd943abacf864531decdac1\",\"userId\":\"8a52e19010474d5283b224a38b4f0aa5\",\"locale\":null},\"special\":{\"qty\":1,\"totalAmount\":33,\"action\":\"setOrderInfo\",\"orderDetailList\":[{\"skuId\":\"\",\"salesPrice\":33,\"qty\":1}]}}";

            //Request Create Service
            return HttpWebClient.DoHttpRequest(url, string.Empty).DeserializeJSONTo<Order>();
        }
        /// <summary>
        /// 提交捐赠信息
        /// </summary>
        /// <param name="dEn">数据实体</param>
        /// <returns>返回订单号</returns>
        public string RecordInfo(DonateEntity dEn)
        {
            //订单号
            string orderNo = CreateOrderNo().content.orderId ?? string.Empty;

            //设置赋值
            dEn.DonateId = Guid.NewGuid().ToString();
            dEn.CustomerId = CurrentUserInfo.ClientID;
            dEn.OrderId = orderNo;

            //执行创建
            _currentDAO.Create(dEn);

            //返回订单号
            return orderNo;
        }
    }

    #region 订单序列化实体
    public class Order
    {
        public string Code { get; set; }
        public string description { get; set; }
        public string exception { get; set; }
        public string data { get; set; }
        public string searchCount { get; set; }
        public Detail content { get; set; }
    }
    public class Detail
    {
        public string orderId { get; set; }
        public int? Status { get; set; }
    }
    #endregion
}