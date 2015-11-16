using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VIPCardManage.Response
{
    public class GetVipCardBalanceChangeListRD : IAPIResponseData
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
        /// 会员卡余额变动记录列表信息集合
        /// </summary>
        public List<VipCardBalanceInfo> VipCardList { get; set; }
    }

    public class VipCardBalanceInfo {
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 门店ID
        /// </summary>
        public string UnitID { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 储值增减
        /// </summary>
        public decimal ChangeAmount { get; set; }
        /// <summary>
        /// 原因
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        /// 图片Url
        /// </summary>
        public string ImageUrl { get; set; }
    }
}
