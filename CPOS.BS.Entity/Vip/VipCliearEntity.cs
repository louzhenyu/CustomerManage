/*
  * Author		:陆荣平
 * EMail		:lurp@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/26 14:03:46
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
    /// VipCliearEntity
    /// </summary>
    public class VipClearEntity : BaseEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VipClearEntity()
        {
        }
        #endregion

        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 无效数量
        /// </summary>
        public int? InvalidNum { get; set; }
        /// <summary>
        /// 重复数量
        /// </summary>
        public int? DuplicateNum { get; set; }
        /// <summary>
        /// 缺憾数量
        /// </summary>
        public int? DrawbackNum { get; set; }
        /// <summary>
        /// 清洗批次ID
        /// </summary>
        public int? VIPClearID { get; set; }
    }

    public class VipDuplicateClearEntity 
    {
        public VipDuplicateClearEntity()
        { 
        
        
        }

        /// <summary>
        /// 清洗批次ID
        /// </summary>
        public int? VIPClearID{get;set;}
        /// <summary>
        /// 清洗明细ID
        /// </summary>
        public int? VIPClearListID { get; set; }
        /// <summary>
        /// 会员ID
        /// </summary>
        public string VipID { get; set; }
        /// <summary>
        /// 需要更改的会员ID
        /// </summary>
        public string UpdateVipID { get; set; }
        /// <summary>
        /// 需要修改的字段名称
        /// </summary>
         public string VIPFieldName { get; set; }
        /// <summary>
        /// 重复分组ID
        /// </summary>
        public string DuplicateGroup { get; set; }
    
    }
}
