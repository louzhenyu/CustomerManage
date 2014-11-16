/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/30 16:00:39
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
    public partial class MarketStoreEntity : BaseEntity 
    {
        #region 属性集
        /// <summary>
        /// StoreCode
        /// </summary>
        public string StoreCode { get; set; }
        /// <summary>
        /// BusinessDistrict
        /// </summary>
        public string BusinessDistrict { get; set; }
        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// MembersCount
        /// </summary>
        public int MembersCount { get; set; }
        /// <summary>
        /// SalesYear
        /// </summary>
        public decimal SalesYear { get; set; }
        /// <summary>
        /// Opened
        /// </summary>
        public string Opened { get; set; }
        /// <summary>
        /// Longitude
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        /// Latitude
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        /// MarketStoreEntityList
        /// </summary>
        public IList<MarketStoreEntity> MarketStoreEntityList { get; set; }
        #endregion
    }
}