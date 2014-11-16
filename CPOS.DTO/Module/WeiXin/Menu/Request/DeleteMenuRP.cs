using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.Menu.Request
{
    public class DeleteMenuRP : IAPIRequestParameter
    {
        public string MenuId { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(MenuId) || MenuId == "")
            {
                throw new Exception("MenuId不能为空");
            }
        }
    }
}
