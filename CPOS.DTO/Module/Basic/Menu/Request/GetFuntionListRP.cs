using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Basic.Menu.Request
{
    public class GetFuntionListRP : IAPIRequestParameter
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleID { get; set; }
        /// <summary>
        /// MenuID
        /// </summary>
        public string MenuID { get; set; }

        public void Validate() { }
    }
}