using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 会员查询
    /// </summary>
    public class VipSearchEntity
    {
        #region 属性
        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 页面数量
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 会员：会员名，会员号码。。。。
        /// </summary>
        public string VipInfo { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 渠道
        /// </summary>
        public string UnitId { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string VipSourceId { get; set; }
        /// <summary>
        /// 状态：0=全部 1=潜在会员，2=正式会员
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 级别:0=全部 1=基本，2=高级
        /// </summary>
        public int VipLevel { get; set; }
        /// <summary>
        /// 注册时间起（yyyy-MM-dd）
        /// </summary>
        public string RegistrationDateBegin { get; set; }
        /// <summary>
        /// 注册时间止（yyyy-MM-dd）
        /// </summary>
        public string RegistrationDateEnd { get; set; }
        /// <summary>
        /// 最近消费时间起(yyyy-MM-dd)
        /// </summary>
        public string RecentlySalesDateBegin { get; set; }
        /// <summary>
        /// 最近消费时间止(yyyy-MM-dd)
        /// </summary>
        public string RecentlySalesDateEnd { get; set; }
        /// <summary>
        /// 积分起
        /// </summary>
        public int IntegrationBegin { get; set; }

        /// <summary>
        /// 积分止
        /// </summary>
        public int IntegrationEnd { get; set; }
        /// <summary>
        /// 当前登录用户
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 当前客户
        /// </summary>
        public string CustomerId { get; set; }

        public string HigherVipId { get; set; }

        /// <summary>
        /// 性别 【查询】1=男，2=女
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// 您的姓名 【查询】
        /// </summary>
        public string UserName { get; set; }

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
        /// EventId 
        /// </summary>
        public string EventId { get; set; }
        /// <summary>
        /// 标签 
        /// </summary>
        public string Tags { get; set; }
        /// <summary>
        /// 会籍店 
        /// </summary>
        public string MembershipShopId { get; set; }
        /// <summary>
        /// 角色 
        /// </summary>
        public string RoleCode { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string OrderBy { get; set; }
        public double Distance { get; set; }
        #endregion
    }
}
