using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Request
{
    public class GetVipCardTypeRP : IAPIRequestParameter
    {
        /// <summary>
        /// 会员ID
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        public void Validate()
        {
            //const int ERROR_LACK_Phone = 311;//VipCardTypeID不能为空             
            //if (string.IsNullOrEmpty(Phone))
            //{
            //    throw new APIException("请求参数中缺少Phone或值为空.") { ErrorCode = ERROR_LACK_Phone };
            //}
        } 
    }
}
