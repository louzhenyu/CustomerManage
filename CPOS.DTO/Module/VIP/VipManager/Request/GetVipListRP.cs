using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipManager.Request
{
    public class GetVipListRP : IAPIRequestParameter
    {
        /// <summary>
        /// 会员卡号
        /// </summary>
        public string VipCardCode { get; set; }
        /// <summary>
        /// 卡内码
        /// </summary>
        public int VipTypeID { get; set; }
        /// <summary>
        /// 会员名称
        /// </summary>
        public string VipName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 叶大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 叶索引
        /// </summary>
        public int PageIndex { get; set; }

        public void Validate(){ }
    }
}
