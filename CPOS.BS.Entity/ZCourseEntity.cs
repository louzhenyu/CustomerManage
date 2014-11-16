/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/17 9:59:17
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
    public partial class ZCourseEntity : BaseEntity 
    {
        #region 属性集
        /// <summary>
        /// DisplayIndex
        /// </summary>
        public Int64 DisplayIndex { get; set; }
        /// <summary>
        /// CourseReflectionsList
        /// </summary>
        public IList<ZCourseReflectionsEntity> CourseReflectionsList { get; set; }
        /// <summary>
        /// NewsList
        /// </summary>
        public IList<LNewsEntity> NewsList { get; set; }
        /// <summary>
        /// ImageList
        /// </summary>
        public IList<ObjectImagesEntity> ImageList { get; set; }
        /// <summary>
        /// ImageUrl
        /// </summary>
        public string ImageUrl { get; set; }
        public string OrderBy { get; set; }
        #endregion
    }
}