/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/2/18 17:51:09
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
using System.Data;
using System.Linq;
using System.Reflection;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 实体： 客户菜单表 
    /// </summary>
    public partial class ClientMenuButtonEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public ClientMenuButtonEntity()
        {
        }
        #endregion     

        #region 属性集
        /// <summary>
        /// 自动编号
        /// </summary>
        public Guid? ClientMenuID { get; set; }

        /// <summary>
        /// 用于菜单权限控制
        /// </summary>
        public string MenuCode { get; set; }

        /// <summary>
        /// 菜单名称(客户自定义)
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 菜单名称(客户自定义)英文
        /// </summary>
        public string MenuNameEn { get; set; }

        /// <summary>
        /// 菜单顺序
        /// </summary>
        public int? MenuOrder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MenuUrl { get; set; }

        /// <summary>
        /// 样式名称
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// 上级编号
        /// </summary>
        public Guid? ParentID { get; set; }

        /// <summary>
        /// 是否统计
        /// </summary>
        public int? IsCount { get; set; }

        /// <summary>
        /// 客户编号(关联Client表)
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


        public Guid? ClientButtonID { get; set; }
        public string ButtonText { get; set; }
        public string ButtonTextEn { get; set; }
        public string ButtonCode { get; set; }

        #endregion        
    }
}