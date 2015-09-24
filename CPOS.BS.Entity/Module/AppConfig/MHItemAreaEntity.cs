/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/31 15:58:37
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
    public partial class MHItemAreaEntity : BaseEntity
    {
        #region 属性集
        public string ItemName { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal SalesPrice { get; set; }
        public DateTime AddedTime { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public string TypeId { get; set; }

        public decimal DiscountRate
        {
            get
            {
                return Math.Ceiling(SalesPrice / Price * 1000)/10;//现在要只留一位小数6.7折扣
                
            }
        }

        public string DeadlineTime
        {
            get
            {
                if (EndTime >= DateTime.Now)
                {
                    TimeSpan span = EndTime - DateTime.Now;
                    return string.Format("还剩余{0}天{1}时{2}分{3}秒", span.Days, span.Hours, span.Minutes, span.Seconds);
                }
                else
                    return string.Format("活动结束");

            }
        }

        public long? DeadlineSecond
        {
            get
            {
                if (EndTime >= DateTime.Now)
                    return Convert.ToInt64((EndTime - DateTime.Now).TotalSeconds);
                else
                    return 0;

            }
        }
        #endregion
    }
}