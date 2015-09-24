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
    /// ʵ�壺  
    /// </summary>
    public partial class MHItemAreaEntity : BaseEntity
    {
        #region ���Լ�
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
                return Math.Ceiling(SalesPrice / Price * 1000)/10;//����Ҫֻ��һλС��6.7�ۿ�
                
            }
        }

        public string DeadlineTime
        {
            get
            {
                if (EndTime >= DateTime.Now)
                {
                    TimeSpan span = EndTime - DateTime.Now;
                    return string.Format("��ʣ��{0}��{1}ʱ{2}��{3}��", span.Days, span.Hours, span.Minutes, span.Seconds);
                }
                else
                    return string.Format("�����");

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