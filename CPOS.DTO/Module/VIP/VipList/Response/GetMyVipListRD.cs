using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.VipList.Response
{
    public class GetMyVipListRD : IAPIResponseData
    {
        /// <summary>
        /// 总会员数
        /// </summary>
        public int MyVipCount { get; set; }
        /// <summary>
        /// 排名
        /// </summary>
        public int Ranking { get; set; }
        /// <summary>
        /// 注册会员数
        /// </summary>
        public int Registered { get; set; }
        /// <summary>
        /// 潜在会员数
        /// </summary>
        public int Latent { get; set; }
        /// <summary>
        /// 停用会员数
        /// </summary>
        public int Disabled { get; set; }
        /// <summary>
        /// 注册会员列表
        /// </summary>
        public MyVipInfo[] MyVipList { get; set; }
        public int TotalCount { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPageCount { get; set; }
    }
    /// <summary>
    /// 我的会员列表信息
    /// </summary>
    public class MyVipInfo
    {
        public string VipID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string VipName { get; set; }
        /// <summary>
        /// 头像URL
        /// </summary>
        public string VipPhoto { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 会员状态
        /// </summary>
        public int? Status { get; set; }
    }
}
