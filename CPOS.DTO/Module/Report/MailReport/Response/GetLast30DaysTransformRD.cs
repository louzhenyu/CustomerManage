using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Report.MailReport.Response
{
    public class GetLast30DaysTransformRD : IAPIResponseData
    {
        public GetLast30DaysTransformRD(R_WxO2OPanel_30DaysEntity dbEntity)
        {
            if (dbEntity == null)
                return;
            Rate_OrderVipPayCount_UV = dbEntity.Rate_OrderVipPayCount_UV;
            Rate_OrderVipCount_UV = dbEntity.Rate_OrderVipCount_UV;
            Rate_OrderVipPayCount_OrderVipCount = dbEntity.Rate_OrderVipPayCount_OrderVipCount;
            WxUV = dbEntity.WxUV;
            WxOrderVipCount = dbEntity.WxOrderVipCount;
            WxOrderVipPayCount = dbEntity.WxOrderVipPayCount;
            Rate_UV_Last = dbEntity.Rate_UV_Last;
            Rate_OrderVipCount_Last = dbEntity.Rate_OrderVipCount_Last;
            Rate_OrderVipPayCount_Last = dbEntity.Rate_OrderVipPayCount_Last;
            WxPV = dbEntity.WxPV;
            WxOrderCount = dbEntity.WxOrderCount;
            WxOrderPayCount = dbEntity.WxOrderPayCount;
            Rate_PV_Last = dbEntity.Rate_PV_Last;
            Rate_OrderCount_Last = dbEntity.Rate_OrderCount_Last;
            Rate_OrderPayCount_Last = dbEntity.Rate_OrderPayCount_Last;
            WxOrderMoney = dbEntity.WxOrderMoney;
            WxOrderPayMoney = dbEntity.WxOrderPayMoney;
            WxOrderAVG = dbEntity.WxOrderAVG;
            Rate_OrderMoney_Last = dbEntity.Rate_OrderMoney_Last;
            Rate_OrderPayMoney_Last = dbEntity.Rate_OrderPayMoney_Last;
            Rate_OrderAVG_Last = dbEntity.Rate_OrderAVG_Last;
        }
        /// <summary>
        /// 成交访客比
        /// </summary>
        public string Rate_OrderVipPayCount_UV { get; set; }
        /// <summary>
        /// 下单访客比
        /// </summary>
        public string Rate_OrderVipCount_UV { get; set; }
        /// <summary>
        /// 成交下单比
        /// </summary>
        public string Rate_OrderVipPayCount_OrderVipCount { get; set; }
        /// <summary>
        /// 云店访客人数
        /// </summary>
        public int? WxUV { get; set; }
        /// <summary>
        /// 云店下单人数
        /// </summary>
        public int? WxOrderVipCount { get; set; }
        /// <summary>
        /// 云店成交人数
        /// </summary>
        public int? WxOrderVipPayCount { get; set; }
        /// <summary>
        /// 访客人数比
        /// </summary>
        public string Rate_UV_Last { get; set; }
        /// <summary>
        /// 下单人数比
        /// </summary>
        public string Rate_OrderVipCount_Last { get; set; }
        /// <summary>
        /// 成交人数比
        /// </summary>
        public string Rate_OrderVipPayCount_Last { get; set; }
        /// <summary>
        /// 云店页面浏览量
        /// </summary>
        public int? WxPV { get; set; }
        /// <summary>
        /// 云店下单笔数
        /// </summary>
        public int? WxOrderCount { get; set; }
        /// <summary>
        /// 云店成交笔数
        /// </summary>
        public int? WxOrderPayCount { get; set; }
        /// <summary>
        /// 页面浏览比
        /// </summary>
        public string Rate_PV_Last { get; set; }
        /// <summary>
        /// 下单笔数比
        /// </summary>
        public string Rate_OrderCount_Last { get; set; }
        /// <summary>
        /// 成交笔数比
        /// </summary>
        public string Rate_OrderPayCount_Last { get; set; }
        /// <summary>
        /// 云店下单金额
        /// </summary>
        public decimal? WxOrderMoney { get; set; }
        /// <summary>
        /// 云店成交金额
        /// </summary>
        public decimal? WxOrderPayMoney { get; set; }
        /// <summary>
        /// 云店客单价
        /// </summary>
        public decimal? WxOrderAVG { get; set; }
        /// <summary>
        /// 下单金额比
        /// </summary>
        public string Rate_OrderMoney_Last { get; set; }
        /// <summary>
        /// 成交金额比
        /// </summary>
        public string Rate_OrderPayMoney_Last { get; set; }
        /// <summary>
        /// 客单价比
        /// </summary>
        public string Rate_OrderAVG_Last { get; set; }
    }
}
