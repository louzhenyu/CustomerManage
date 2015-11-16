using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
namespace JIT.CPOS.DTO.Module.VIP.VIPCardManage.Response
{ /// <summary>
    /// 获取会员卡管理列表响应内容
    /// </summary>
    public class GetVIPCardListRD : IAPIResponseData
    {
        /// <summary>
        /// 当前页码，将请求的页码数同时返回给客户端
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalPageCount { get; set; }
        /// <summary>
        /// 会员卡管理列表信息集合
        /// </summary>
        public List<VipCardInfo> VipCardList { get; set; }
    }
    /// <summary>
    /// 会员卡管理列表信息显示业务对象
    /// </summary>
    public class VipCardInfo
    {
        /// <summary>
        /// 会员卡ID
        /// </summary>
        public string VipCardID { get; set; }
        /// <summary>
        /// 会员卡号码
        /// </summary>
        public string VipCardCode { get; set; }

        /// <summary>
        /// 会员卡类型名称
        /// </summary>
        public string VipCardName { get; set; }
        /// <summary>
        /// 会员名称
        /// </summary>
        public string VipName { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 卡内余额
        /// </summary>
        public decimal BalanceAmount { get; set; }
        /// <summary>
        /// 办卡日期
        /// </summary>
        public string MembershipTime { get; set; }
        /// <summary>
        /// 办卡门店
        /// </summary>
        public string MembershipUnitName { get; set; }
        /// <summary>
        /// 会员卡状态ID
        /// </summary>
        public int VipCardStatusID { get; set; }
        /// <summary>
        /// 售卡员工
        /// </summary>
        public string SalesUserName { get; set; }
        /// <summary>
        /// 图片Url
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 会员ID
        /// </summary>
        public string VIPID { get; set; }
    }
}
