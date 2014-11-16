using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.SysPage.Response
{
   public class GetCustomerPageSettingRD:IAPIResponseData
    {
       public CustomerPageSettingInfo[] PageBaseInfo { get; set; }
       public string PageTitle { get; set; }
       public string PageHtmls { get; set; }
       public string PagePara { get; set; }
       public string JsonValue { get; set; }

       public string URL { get; set; }
    }
   public class CustomerPageSettingInfo 
   {
       public string ModuleName { get; set; }
       public string Version { get; set; }
       public string Auther { get; set; }
       public string LastUpdateTime { get; set; }
       public string DoMainUrl { get; set; }
   }
}
