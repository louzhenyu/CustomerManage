using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Report.MailReport.Response
{
    public class GetLast7DaysOperationDataRD : IAPIResponseData
    {
        public GetLast7DaysOperationDataRD(R_WxO2OPanel_7DaysEntity dbEntity)
        {
            if (dbEntity == null)
                return;
            WxUV = dbEntity.WxUV;
            OfflineUV = dbEntity.OfflineUV;
            WxOrderPayCount = dbEntity.WxOrderPayCount;
            OfflineOrderPayCount = dbEntity.OfflineOrderPayCount;
            WxOrderPayMoney = dbEntity.WxOrderPayMoney;
            OfflineOrderPayMoney = dbEntity.OfflineOrderPayMoney;
            WxOrderAVG = dbEntity.WxOrderAVG;
            OfflineOrderAVG = dbEntity.OfflineOrderAVG;
        }
        /// <summary>
        /// 云店访客数
        /// </summary>
        public int? WxUV { get; set; }
        /// <summary>
        /// 门店访客数
        /// </summary>
        public int? OfflineUV { get; set; }
        /// <summary>
        /// 云店成交笔数
        /// </summary>
        public int? WxOrderPayCount { get; set; }
        /// <summary>
        /// 门店成交笔数
        /// </summary>
        public int? OfflineOrderPayCount { get; set; }
        /// <summary>
        /// 云店成交金额
        /// </summary>
        public decimal? WxOrderPayMoney { get; set; }
        /// <summary>
        /// 门店成交金额
        /// </summary>
        public decimal? OfflineOrderPayMoney { get; set; }
        /// <summary>
        /// 云店客单价
        /// </summary>
        public decimal? WxOrderAVG { get; set; }
        /// <summary>
        /// 门店客单价
        /// </summary>
        public decimal? OfflineOrderAVG { get; set; }
    }
}
