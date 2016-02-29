using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.ServicesLog.Response
{
    public class GetVipAmountdetailListRD : IAPIResponseData
    {
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPageCount { get; set; }

        public List<VipAmountdetailData> VipAmountdetailList { get; set; }
    }
    public class VipAmountdetailData
    {
        /// <summary>
        /// 会员ID
        /// </summary>
        public string VipID { get; set; }
        /// <summary>
        /// 会员名称
        /// </summary>
        public string VipName { get; set; }
        /// <summary>
        /// 服务时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 销售金额
        /// </summary>
        public decimal SalesAmout { get; set; }
        /// <summary>
        /// 返利金额
        /// </summary>
        public decimal Amout { get; set; }
        /// <summary>
        /// 图片Url（会员头像）
        /// </summary>
        public string HeadImgUrl { get; set; }
        /// <summary>
        /// 创建员工ID
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreateByName { get; set; }
    }
}
