/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/2/27 11:48:20
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
    /// 实体： 拜访步骤信息明细 
    /// </summary>
    public partial class VisitingTaskStepObjectBrandEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VisitingTaskStepObjectBrandEntity()
        {
        }
        #endregion     

        #region 属性集

        /// <summary>
        /// 自动编号
        /// </summary>
        public int? BrandID { get; set; }

        /// <summary>
        /// 品牌编号
        /// </summary>
        public string BrandNo { get; set; }

        /// <summary>
        /// 品牌名称
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// 是否自有品牌(0-否,1-是)
        /// </summary>
        public int? IsOwner { get; set; }

        /// <summary>
        /// 品牌等级
        /// </summary>
        public int? BrandLevel { get; set; }

        /// <summary>
        /// 上级品牌编号
        /// </summary>
        public int? ParentID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ClientID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ClientDistributorID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastUpdateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? IsDelete { get; set; }

        public string ParentBrandName { get; set; }
        public string BrandCompany { get; set; }
        public int? CategoryID { get; set; }
        public string CategoryName { get; set; }
        #endregion

    }
}