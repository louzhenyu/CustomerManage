using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipManager.Response
{
    public class GetVipListRD : IAPIResponseData
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPageCount { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get;set;}
        /// <summary>
        /// 会员信息集合
        /// </summary>
        public List<VipInfo> VipInfoList { get; set; }
    }

    public class VipInfo {
        /// <summary>
        /// 卡ID
        /// </summary>
        public string VipCardID { get; set; }
        /// <summary>
        /// 会员ID
        /// </summary>
        public string VIPID { get; set; }
        /// <summary>
        /// 会员编号
        /// </summary>
        public string VipCode { get; set; }
        /// <summary>
        /// 会员卡号
        /// </summary>
        public string VipCardCode { get; set; }
        /// <summary>
        /// 会员名称
        /// </summary>
        public string VipName { get; set; }
        /// <summary>
        /// 会员昵称
        /// </summary>
        public string VipRealName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int? Gender { get; set; }
        /// <summary>
        /// 卡类型名称
        /// </summary>
        public string VipCardTypeName { get; set; }
        /// <summary>
        /// 卡状态ID
        /// </summary>
        public int? VipCardStatusId { get; set; }
        
        /// <summary>
        /// 注册时间
        /// </summary>
        public string MembershipTime { get; set; }
        /// <summary>
        /// 办卡门店
        /// </summary>
        public string MembershipUnitName { get; set; }
    }
}
