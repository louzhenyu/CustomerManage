using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.SysPage.Request
{
   public class GetSysPageDetailRP:IAPIRequestParameter
    {
       /// <summary>
       /// 标识
       /// </summary>
       public string PageId { get; set; }

       public void Validate()
       {
           
       }
    }
}
