using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.VipList.Request
{
    public class GetMyVipListRP : IAPIRequestParameter
    {
        public void Validate()
        {
            if (this.PageIndex < 0)
            {
                PageIndex = 0;
            }
            if (this.PageSize < 1)
            {
                PageSize = 15;
            }
        }
        /// <summary>
        /// 门店ID
        /// </summary>
        public string UnitID { get; set; }
        /// <summary>
        /// 会员状态（0：停用会员，1：潜在会员，2：注册会员，all：全部）
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public string OrderBy { get; set; }
        /// <summary>
        /// 页码。为空则默认为0
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页记录数。为空则为15
        /// </summary>
        public int PageSize { get; set; }
    }
}
