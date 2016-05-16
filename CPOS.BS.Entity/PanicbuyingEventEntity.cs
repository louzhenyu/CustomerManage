/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/25 13:51:19
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
using System.Text;
using JIT.Utility;
using JIT.Utility.Entity;

namespace JIT.CPOS.BS.Entity
{

    /// <summary>
    /// 实体：  
    /// </summary>
    public partial class PanicbuyingEventEntity : BaseEntity 
    {
        #region 属性集
        public int Qty { get; set; }
        public int RemainQty { get; set; }
        public string EventStatusStr { get; set; }
        #endregion
    }
    public class KJEventItemInfo
    {
        public string EventId { get; set; }
        public string ItemId { get; set; }

        public string ItemName { get; set; }

        public string ImageUrl { get; set; }

        public decimal MinPrice { get; set; }

        public decimal MinBasePrice { get; set; }

        public int Qty { get; set; }

        public int SoldQty { get; set; }

        public int PromotePersonCount { get; set; }
    }


    public class KJEventItemDetailInfo
    {    
        public string ItemId { get; set; }

        public string EventItemMappingID { get; set; }

        public string ItemName { get; set; }

        public int SinglePurchaseQty { get; set; }

        public decimal MinPrice { get; set; } //最小原价

        public decimal MinBasePrice { get; set; } //最小底价

        public int PromotePersonCount { get; set; } //累计参与人数

        public int CurrentQty { get; set; } //Sku累计的剩余库存

        public int SoldQty { get; set; } //Sku累计的已售数量(+保留数量)

        public int RemainingQty { get; set; } //剩余数量

        public string StopReason { get; set; } //停止原因

        public DateTime EventEndTime { get; set; } //活动结束时间

        public DateTime EventBeginTime { get; set; } //活动开始时间

        public decimal BargaingingInterval { get; set; } //可砍时间
        public string Prop1Name { get; set; }

        public string Prop2Name { get; set; }


        public string ItemIntroduce { get; set; }

    }
}