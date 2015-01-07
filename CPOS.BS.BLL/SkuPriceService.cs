using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace JIT.CPOS.BS.BLL
{
    public class SkuPriceService : BaseService
    {
        JIT.CPOS.BS.DataAccess.SkuPriceService skuPriceService = null;
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="loggingSessionInfo">当前的用户信息</param>
        public SkuPriceService(LoggingSessionInfo loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
            skuPriceService = new DataAccess.SkuPriceService(loggingSessionInfo);
        }
        #endregion


        #region 获取对应sku价格集合
        /// <summary>
        /// 获取对应sku价格集合
        /// </summary>
        /// <param name="skuId">sku标识</param>
        /// <returns></returns>
        public IList<SkuPriceInfo> GetSkuPriceListBySkuId(string skuId)
        {
            IList<SkuPriceInfo> skuInfoList = new List<SkuPriceInfo>();
            DataSet ds = new DataSet();
            ds = skuPriceService.GetSkuPriceListBySkuId(skuId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                skuInfoList = DataTableToObject.ConvertToList<SkuPriceInfo>(ds.Tables[0]);
            }
            return skuInfoList;
        }
        #endregion

        /// <summary>
        /// 获取价格（包括人人销售价格）
        /// </summary>
        /// <param name="skuIds"></param>
        /// <returns></returns>
        public IList<SkuPrice> GetPriceListBySkuIds(string skuIds, string EventId)
        {
            IList<SkuPrice> PriceList = new List<SkuPrice>();
            DataSet ds = new DataSet();
            ds = skuPriceService.GetPriceListBySkuIds(skuIds,EventId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                PriceList = DataTableToObject.ConvertToList<SkuPrice>(ds.Tables[0]);
            }
            return PriceList;
        }

        #region 删除sku相关价格
        /// <summary>
        /// 删除sku相关价格
        /// </summary>
        /// <param name="skuInfo">sku对象</param>
        /// <returns></returns>
        public bool DeleteSkuPriceInfo(SkuInfo skuInfo)
        {
            return skuPriceService.DeleteSkuPriceInfo(skuInfo);
        }
        #endregion

        /// <summary>
        /// 根据订单Id获取订单的sku个数和sku佣金价格集合
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<OrderDetail> GetSkuPrice(string orderId)
        {
            List<OrderDetail> orderDetail = new List<OrderDetail>();
            DataSet ds = new DataSet();
            ds = skuPriceService.GetSkuPrice(orderId, "D1A9373A2D2F4A09B824E9F59EFEAABF");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                orderDetail = DataTableToObject.ConvertToList<OrderDetail>(ds.Tables[0]);
            }
            return orderDetail;
        }


    }
}
