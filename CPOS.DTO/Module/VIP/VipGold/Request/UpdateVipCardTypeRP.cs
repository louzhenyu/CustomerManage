using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Request
{
    public class UpdateVipCardTypeRP : IAPIRequestParameter
    {
        /// <summary>
        /// 实体卡会员VIPID
        /// </summary>
        public string BindVipID { get; set; }
        /// <summary>
        /// 卡类型ID
        /// </summary>
        public int VipCardTypeID { get; set; }
        public void Validate()
        {
            const int ERROR_LACK_VipCardTypeID = 312;//操作类型（1=卡等级编辑；2=升级条件编辑；3=基本权益编辑；） 
            const int ERROR_LACK_BindVipID = 313;//实体卡会员VIPID 
            if (VipCardTypeID == 0)
            {
                throw new APIException("请求参数中缺少VipCardTypeID或值为空.") { ErrorCode = ERROR_LACK_VipCardTypeID };
            }
            if (string.IsNullOrEmpty(BindVipID))
            {
                throw new APIException("请求参数中缺少BindVipID或值为空.") { ErrorCode = ERROR_LACK_BindVipID };
            }
        } 
    }
}
