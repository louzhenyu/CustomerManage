using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingData.Response
{
    public class GetEventPrizeListRD : IAPIResponseData
    {



        public List<EventPrizeInfo> EventPrizeList { get; set; }

        public int TotalCount { get; set; }
        public int TotalPage { get; set; }


     
       

    }


    public class EventPrizeInfo
    {
        public string CouponTypeID { get; set; }
        public int winnerCount { get; set; }
        public int RemindCount { get; set; }

        public int NotUsedCount { get; set; }
        public int UsedCount { get; set; }
        public int prizeSale { get; set; }

        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public String PrizesID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String PrizeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String PrizeShortDesc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String PrizeDesc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String LogoURL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ImageUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ContentText { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ContentUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? Price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? DisplayIndex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? CountTotal { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? CountLeft { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String EventId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String LastUpdateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsDelete { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PrizeTypeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? Point { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsAutoPrizes { get; set; }
        /// <summary>
        /// 奖品等级
        /// </summary>
        public int PrizeLevel { get; set; }

        public decimal Probability { get; set; }

        #endregion
      


    }
}
