/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/26 14:59:01
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
    public partial class IMGroupEntity : BaseEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public IMGroupEntity()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public Guid? ChatGroupID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String GroupName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String LogoUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CustomerID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Telephone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? UserCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? GroupLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsPublic { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? ApproveNeededLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? InvitationLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? ChatLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? QuitLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BindGroupID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CreateBy { get; set; }

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
        public Int32? IsDelete { get; set; }


        #endregion

    }
}