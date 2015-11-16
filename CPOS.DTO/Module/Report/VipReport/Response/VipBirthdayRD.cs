using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Report.VipReport.Response
{
    public class VipBirthdayRD : IAPIResponseData
    {
        /// <summary>
        /// 总叶数
        /// </summary>
        public int TotalPageCount { get; set; }
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 会员列表信息
        /// </summary>
        public List<VipBirthdayInfo> VipBirthdayInfoList { get; set; }
    }
    public class VipBirthdayInfo {
        /// <summary>
        /// 卡号
        /// </summary>
        public string VipCardCode { get; set; }

        /// <summary>
        /// 卡类型名称
        /// </summary>
        public string VipCardTypeName { get; set; }
        /// <summary>
        /// 卡状态ID
        /// </summary>
        public int VipCardStatusId { get; set; }
        /// <summary>
        /// 会员名称
        /// </summary>
        public string VipName { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public string Birthday { get; set; }

        /// <summary>
        /// 门店ID
        /// </summary>
        public string MembershipUnit { get; set; }
        /// <summary>
        /// 最近消费    
        /// </summary>
        public string SpendingDateShow { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary> 
        public string MembershipUnitName { get; set; }
        /// <summary>
        /// 入会时间
        /// </summary>
        public string MembershipTime { get; set; }
    }
}
