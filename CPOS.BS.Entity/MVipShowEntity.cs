/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/10/10 10:06:07
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
    public partial class MVipShowEntity : BaseEntity 
    {
        #region 属性集
        /// <summary>
        /// UnitName
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// VipName
        /// </summary>
        public string VipName { get; set; }
        /// <summary>
        /// DisplayIndex
        /// </summary>
        public Int64 DisplayIndex { get; set; }
        /// <summary>
        /// ImageList
        /// </summary>
        public IList<ObjectImagesEntity> ImageList { get; set; }
        /// <summary>
        /// 是否赞过
        /// </summary>
        public int IsPraise { get; set; }

        public string UserId { get; set; }

        public int IsTop { get; set; }  //是否要查询最热 1=是，0=否
        public int IsMe { get; set; }   //是否要查询我的 1=是，0=否
        public int IsNewest { get; set; }   //是否最新更新 1=是，0=否
        public string ItemName { get; set; }
        public string ItemId { get; set; }
        public string BeginTime { get; set; }

        private bool _UseDelete = true;
        public bool UseDelete
        {
            get { return _UseDelete; }
            set { _UseDelete = value; }
        }
        public int ImageCount { get; set; }

        private string _OrderBy = "PraiseCount";
        public string OrderBy
        {
            get { return _OrderBy; }
            set { _OrderBy = value; }
        }

        #endregion
    }
}