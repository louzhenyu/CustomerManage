using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.UnitAndItem.Item.Request
{
   public  class PraiseItemRP : IAPIRequestParameter
    {
        #region IAPIRequestParameter 成员

        public void Validate()
        {
            
        }
        public string ItemID { get; set; }
        #endregion
    }
}
