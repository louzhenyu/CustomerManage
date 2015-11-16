using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.SysVipCardType.Request
{
    public class SetSpecialDateRP : IAPIRequestParameter
    {
        /// <summary>
        /// 卡类型ID
        /// </summary>
        public int VipCardTypeID { get; set; }
        /// <summary>
        /// 假日标识
        /// </summary>
        public string HolidayID { get; set; }
        /// <summary>
        /// 不可用积分（0=可用；1=不可用）
        /// </summary>
        public int NoAvailablePoints { get; set; }
        /// <summary>
        /// 不可用折扣（0=可用；1=不可用）
        /// </summary>
        public int NoAvailableDiscount { get; set; }
        /// <summary>
        /// 不可回馈积分（0=可用；1=不可用）
        /// </summary>
        public int NoRewardPoints { get; set; }
        public void Validate()
        {

        }
    }
}
