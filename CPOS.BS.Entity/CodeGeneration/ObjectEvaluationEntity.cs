/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/2/28 17:54:09
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

namespace JIT.CPOS.Entity
{
    /// <summary>
    /// 实体：  
    /// </summary>
    public partial class ObjectEvaluationEntity : BaseEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public ObjectEvaluationEntity()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public String ItemEvaluationID { get; set; }

        /// <summary>
        /// 商品ID或者门店ID
        /// </summary>
        public String ObjectID { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        public String MemberID { get; set; }

        /// <summary>
        /// 客户ID
        /// </summary>
        public String ClientID { get; set; }

        /// <summary>
        /// 评价内容
        /// </summary>
        public String Content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? StarLevel { get; set; }

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
        /// 来自平台
        /// </summary>
        public String Platform { get; set; }


        #endregion

    }
}