/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/14 9:45:29
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
    public partial class VipEntity : BaseEntity
    {
        #region 属性集
        /// <summary>
        /// 行号
        /// </summary>
        public Int64 DisplayIndex { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int ICount { get; set; }

        /// <summary>
        /// VIP集合
        /// </summary>
        public IList<VipEntity> vipInfoList { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string VipSourceName { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public string VipLevelDesc { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string StatusDesc { get; set; }
        /// <summary>
        /// 最近消费门店
        /// </summary>
        public string LastUnit { get; set; }
        /// <summary>
        /// 给上级贡献积分
        /// </summary>
        public decimal IntegralForHightUser { get; set; }

        /// <summary>
        /// 您的姓名 【查询】
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 导购员ID
        /// </summary>
        public string UserId { get; set; }

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

        /// <summary>
        /// 订购数量
        /// </summary>
        public int OrderCount { get; set; }
        /// <summary>
        /// 月份 
        /// </summary>
        public string yearMonth { get; set; }
        /// <summary>
        /// 月份新增会员数量 
        /// </summary>
        public int vipAddupCount { get; set; }
        /// <summary>
        /// 环比 
        /// </summary>
        public decimal vipMonthMoM { get; set; }
        /// <summary>
        /// 排序 
        /// </summary>
        public Int64 displayIndex { get; set; }
        /// <summary>
        /// 贵宾数量 
        /// </summary>
        public int vipVisitantCount { get; set; }
        /// <summary>
        /// 贵宾数量环比 
        /// </summary>
        public decimal vipVisitantMonthMoM { get; set; }
        /// <summary>
        /// 微信关注数量 
        /// </summary>
        public int vipWeiXinAddupCount { get; set; }
        /// <summary>
        /// 微信关注环比 
        /// </summary>
        public int vipWeiXinMonthMoM { get; set; }
        /// <summary>
        /// 会籍店
        /// </summary>
        public string MembershipShop { get; set; }
        /// <summary>
        /// 流水号
        /// </summary>
        public string SerialNumber { get; set; }
        /// <summary>
        /// 缩写的标签内容
        /// </summary>
        public string VipTagsShort { get; set; }

        /// <summary>
        /// 完整的标签内容
        /// </summary>
        public string VipTagsLong { get; set; }
        /// <summary>
        /// 当前查询的积分（可能是会员的，可能是导购员的，可能是门店的）
        /// </summary>
        public string SearchIntegral { get; set; }
        /// <summary>
        /// 门店会员数量
        /// </summary>
        public int UnitCount { get; set; }
        /// <summary>
        /// 门店销售金额
        /// </summary>
        public string UnitSalesAmount { get; set; }
        /// <summary>
        /// 门店标识
        /// </summary>
        public string UnitId { get; set; }
        /// <summary>
        /// 奖励类型，多选，用逗号分隔
        /// </summary>
        public string IntegralSourceIds { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public string BeginDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public string EndDate { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 页面数量
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 导购员推介数量
        /// </summary>
        public int VipCount { get; set; }
        /// <summary>
        /// 查询金额
        /// </summary>
        public string SearchAmount { get; set; }
        /// <summary>
        /// 1未付款
        /// </summary>
        public int Status1 { get; set; }
        /// <summary>
        /// 0已取消
        /// </summary>
        public int Status0 { get; set; }
        /// <summary>
        /// 2待处理
        /// </summary>
        public int Status2 { get; set; }
        /// <summary>
        /// 3已发货
        /// </summary>
        public int Status3 { get; set; }
        /// <summary>
        /// 会员标签数量
        /// </summary>
        public int VipTagsCount { get; set; }
        /// <summary>
        /// 距离
        /// </summary>
        public decimal Distance { get; set; }

        #endregion
    }
}