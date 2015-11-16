/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/20 11:22:28
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
    public partial class VipCardEntity : BaseEntity 
    {
        #region 属性集
        /// <summary>
        /// 会员名
        /// </summary>
        public string VipName { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string VipRealName { get; set; }
        /// <summary>
        /// 会籍店
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 会籍店ID
        /// </summary>
        public string UnitID { get; set; }
        /// <summary>
        /// 卡等级
        /// </summary>
        public string VipCardGradeName { get; set; }
        /// <summary>
        /// 卡状态
        /// </summary>
        public string VipStatusName { get; set; }
        /// <summary>
        /// VipId
        /// </summary>
        public string VipId { get; set; }
        /// <summary>
        /// 会员卡类别名称
        /// </summary>
        public string VipCardTypeName { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public Int64 DisplayIndex { get; set; }
        /// <summary>
        /// 批量会员卡集合
        /// </summary>
        public IList<VipCardEntity> VipCardInfoList { get; set; }
        /// <summary>
        /// 总数量
        /// </summary>
        public int ICount { get; set; }
        /// <summary>
        /// 页面数量
        /// </summary>
        public int maxRowCount { get; set; }
        /// <summary>
        /// 开始行号
        /// </summary>
        public int startRowIndex { get; set; }
        /// <summary>
        /// 车牌
        /// </summary>
        public string CarCode { get; set; }
        /// <summary>
        /// 状态代码
        /// </summary>
        public string VipCardStatusCode { get; set; }
        
        /// <summary>
        /// 会员手机号码
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 会员卡状态名称
        /// </summary>
        public string VipCardStatusName { get; set; }
        /// <summary>
        /// 会员卡类型图片Url
        /// </summary>
        public string picUrl { get; set; }
        /// <summary>
        /// 会员编号
        /// </summary>
        public string VipCode { get; set; }
        /// <summary>
        /// 会员生日
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// 会员性别
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// 会员ID
        /// </summary>
        public string VIPID { get; set; }
        /// <summary>
        /// 会员卡状态变更记录集合
        /// </summary>
        public List<VipCardStatusChangeLogEntity> StatusLogList { get; set; }
        #endregion
    }
}