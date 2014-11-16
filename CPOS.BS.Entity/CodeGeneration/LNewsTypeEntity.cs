/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/1/3 10:59:10
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
    public partial class LNewsTypeEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public LNewsTypeEntity()
        {
        }
        #endregion     

        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public String NewsTypeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String NewsTypeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ParentTypeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? TypeLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsDelete { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String LastUpdateBy { get; set; }

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
        public String CustomerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsVisble { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? ChannelCode { get; set; }

        /// <summary>
        /// 父类别
        /// </summary>
        public string ParentTypeName { set; get; }


        #endregion

    }
}