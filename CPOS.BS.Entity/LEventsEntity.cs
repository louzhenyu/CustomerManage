/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/18 10:54:13
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
    public partial class LEventsEntity : BaseEntity
    {
        #region 属性集

        public string MobileModuleID { get; set; }

        /// <summary>
        /// 已关注会员
        /// </summary>
        public int hasVipCount { get; set; }
        /// <summary>
        /// 新采集会员
        /// </summary>
        public int newVipCount { get; set; }
        /// <summary>
        /// 已下订单数
        /// </summary>
        public int hasOrderCount { get; set; }
        /// <summary>
        /// 已付款订单数
        /// </summary>
        public int hasPayCount { get; set; }
        /// <summary>
        /// 已销售订单额
        /// </summary>
        public decimal hasSalesAmount { get; set; }
        /// <summary>
        /// 报名数量
        /// </summary>
        public int signUpCount { get; set; }
        /// <summary>
        /// 销售金额
        /// </summary>
        public decimal salesAmount { get; set; }
        /// <summary>
        /// 距离开始日期天数
        /// </summary>
        public int distanceDays { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public IList<LEventsEntity> EventList { get; set; }


        /// <summary>
        /// 还有几天活动开始，如果正在进行中，就返回0
        /// </summary>
        public int IntervalDays { get; set; }

        /// <summary>
        /// 当前用户报名本活动状态。
        /// 0：未报名；1：报名待审批；2：已报名并通过审批
        /// </summary>
        public int ApplyStatus { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 该活动的报名问卷ID，0表示没有问卷
        /// </summary>
        public string QuestionaireID { get; set; }

        /// <summary>
        /// 是否可以申请
        /// </summary>
        public int CanApply { get; set; }
        /// <summary>
        /// 是否能作管理员协助签到
        /// </summary>
        public int CanAdminCheckin { get; set; }
        /// <summary>
        /// 对于未报名且未开始的活动
        /// </summary>
        public int ButtonType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ButtonText { get; set; }
        /// <summary>
        /// 当前用户对本活动的付款状态
        /// </summary>
        public int PayStatus { get; set; }
        /// <summary>
        /// 签到状态
        /// </summary>
        public int CheckinStatus { get; set; }
        /// <summary>
        /// 活动起始时间
        /// </summary>
        public DateTime StartTimeStr { get; set; }
        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime EndTimeStr { get; set; }
        /// <summary>
        /// 开始时间时间戳
        /// </summary>
        public long StartTimeUnix { get; set; }
        /// <summary>
        /// 结束时间时间戳
        /// </summary>
        public long EndTimeUnix { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string EventTypeName { get; set; }
        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 起始时间字符串
        /// </summary>
        public string StartTimeText { get; set; }
        /// <summary>
        /// 终止时间字符串
        /// </summary>
        public string EndTimeText { get; set; }
        ///// <summary>
        ///// 起始时间
        ///// </summary>
        //public DateTime StartTime { get; set; }
        /////// <summary>
        /////// 终止时间
        /////// </summary>
        ////public DateTime EndTime { get; set; }

        /// <summary>
        /// 报名人员数量
        /// </summary>
        public int AppliesCount { get; set; }
        /// <summary>
        /// 签到人员数量
        /// </summary>
        public int CheckinsCount { get; set; }
        /// <summary>
        /// 公众平台
        /// </summary>
        public string WeiXinPublicID { get; set; }
        /// <summary>
        /// EventType
        /// </summary>
        public int EventType { get; set; }
        /// <summary>
        ///奖品数量
        /// </summary>
        public int PrizesCount { get; set; }
        /// <summary>
        ///父活动标题
        /// </summary>
        public string ParentEventTitle { get; set; }
        /// <summary>
        /// 微信固定二维码分类
        /// </summary>
        public string QRCodeTypeId { get; set; }
        /// <summary>
        /// 已报名数量【返校日活动专用】
        /// </summary>
        public int HaveSignedCount { get; set; }
        /// <summary>
        /// 是否报名【返校日活动专用】
        /// </summary>
        public int IsSignUp { get; set; }
        /// <summary>
        /// 可以报名的数量 【返校日活动专用】
        /// </summary>
        public int AllowCount { get; set; }
        /// <summary>
        /// 剩余数量 【返校日活动专用】
        /// </summary>
        public int OverCount { get; set; }
        /// <summary>
        /// 微信固定二维码
        /// </summary>
        public string WXCode { get; set; }
        /// <summary>
        /// 微信固定二维码图片链接
        /// </summary>
        public string WXCodeImageUrl { get; set; }
        /// <summary>
        /// 轮次数量
        /// </summary>
        public int RoundCount { get; set; }

        /// <summary>
        /// 关系表
        /// </summary>
        public List<WMenuMTextMappingEntity> listMenutextMapping { set; get; }

        /// <summary>
        /// 关系表
        /// </summary>
        public List<WMaterialTextEntity> listMenutext { set; get; }

        /// <summary>
        /// 抽奖方式
        /// </summary>
        public int? DrawMethod { set; get; }


        public string MaxWQRCod { set; get; }
        #endregion
    }
}