using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JIT.CPOS.DTO.Base;
namespace JIT.CPOS.DTO.Module.WeiXin.SysPage.Request
{
   public class GetSysPageListRP:IAPIRequestParameter
    {
       /// <summary>
       /// 模糊查询
       /// </summary>
       public string Key { get; set; }

       /// <summary>
       /// 名称的模糊查询
       /// </summary>
       public string Name { get; set; }

       /// <summary>
       /// 开始页.默认0开始
       /// </summary>
       public int? PageIndex { get; set; }

       /// <summary>
       /// 页显示数量。默认15
       /// </summary>
       public int? PageSize { get; set; }

       public void Validate()
       {
          
       }
    }
}
