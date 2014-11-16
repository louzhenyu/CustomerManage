/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/25 9:57:34
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
    public partial class ReservationServiceScheduleEntity : BaseEntity
    {
        #region 属性集
        public decimal? BigClassTermStart { get; set; }//时间段大类开始
        public decimal? BigClassTermEnd { get; set; }//时间段大类结束
        public decimal? SmallClassTermStart { get; set; }//时间段小类开始
        public decimal? SmallClassTermEnd { get; set; }//时间段小类结束
        public string PositionTitle { get; set; }//工位标题
        public string ServiceTitle { get; set; }//服务标题
        public int? Status { get; set; }//状态 0空|1满

        #endregion
    }
}