using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Extension.ActivityApply.Request
{
    public class SetActivityApplyRP : IAPIRequestParameter
    {
        #region 属性
        public string Nickname { get; set; }
        public string Territory { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string LikeTea { get; set; }

        #endregion

        public void Validate()
        {

        }
    }
}
