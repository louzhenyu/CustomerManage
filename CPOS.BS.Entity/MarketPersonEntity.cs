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
    public partial class MarketPersonEntity : BaseEntity 
    {
        #region 属性集
        /// <summary>
        /// 总数量
        /// </summary>
        public int ICount { get; set; }
        /// <summary>
        /// 人群集合
        /// </summary>
        public IList<MarketPersonEntity> MarketPersonInfoList { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public Int64 DisplayIndex { get; set; }

        public IList<VipEntity> vipInfoList { get; set; }

        public string VipCode { get; set; }
        public int VipLevel { get; set; }
        public string VipName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string WeiXin { get; set; }
        public decimal Integration { get; set; }
        public decimal PurchaseAmount { get; set; }
        public int PurchaseCount { get; set; }
        /// <summary>
        /// 性别 【查询】1=男，2=女
        /// </summary>
        public Int32? Gender { get; set; }
        /// <summary>
        /// 您的姓名 【查询】
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 您所在的企业名称 【查询】
        /// </summary>
        public string Enterprice { get; set; }

        /// <summary>
        /// 企业有多少家连锁门店 【查询】
        /// </summary>
        public string IsChainStores { get; set; }

        /// <summary>
        /// 您是否对微信营销感兴趣 【查询】
        /// </summary>
        public string IsWeiXinMarketing { get; set; }
        /// <summary>
        /// 性别 
        /// </summary>
        public string GenderInfo { get; set; }
        #endregion
    }
}