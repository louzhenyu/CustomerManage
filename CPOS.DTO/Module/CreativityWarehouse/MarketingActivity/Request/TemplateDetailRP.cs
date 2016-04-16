using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;
namespace JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingActivity.Request
{
  public  class TemplateDetailRP : IAPIRequestParameter
    {
      public void Validate()
        { }
        /// <summary>
        /// 主题id/模版id
        /// </summary>
      public string TemplateId { get; set; }
      /// <summary>
      /// 商户创意仓库Id
      /// </summary>
      public string CTWEventId{get;set;}
    }
}
