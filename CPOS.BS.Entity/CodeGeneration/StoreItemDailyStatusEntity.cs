/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/29 11:20:31
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
    public partial class StoreItemDailyStatusEntity : BaseEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public StoreItemDailyStatusEntity()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public Guid? StoreItemDailyStatusID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String StoreID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ChannelID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String SkuID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? StatusDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CanReserveBeginTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CanReserveEndTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? StockAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? UsedAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? StorePrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? LowestPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ReservationRemark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? NeedCreditCard { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? NeedPrepay { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CancellationRemark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? Price1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? Price2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? Price3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? Price4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? Price5 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? Price6 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? Price7 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? Price8 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? Price9 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? Price10 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col5 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col6 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col7 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col8 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col9 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col10 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ClientID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }

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
        public Decimal? SourcePrice { get; set; }


        #endregion

    }
}