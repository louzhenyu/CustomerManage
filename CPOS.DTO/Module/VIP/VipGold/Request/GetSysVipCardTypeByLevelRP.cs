using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module
{
   public class GetSysVipCardTypeByLevelRP : IAPIRequestParameter
    {
        /// <summary>
        /// 会员卡等级
        /// </summary>
        public int Level { get; set; }
        public void Validate()
        {

        }
    }
}
