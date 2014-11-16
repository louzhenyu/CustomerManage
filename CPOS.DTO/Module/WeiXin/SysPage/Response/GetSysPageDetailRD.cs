using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.SysPage.Response
{
   public class GetSysPageDetailRD:IAPIResponseData
    {
       /// <summary>
       /// 模板页明细信息
       /// </summary>
       public PageInfoList[] PageInfo { get; set; }

       public ModulePageMappingList[] ModulePageMappingInfo { get; set; }

       public class PageInfoList
       {
           /// <summary>
           /// 标识 
           /// </summary>
           public string PageId { get; set; }
           /// <summary>
           /// 作者
           /// </summary>
           public string Author { get; set; }
           /// <summary>
           /// 版本
           /// </summary>
           public string Version { get; set; }

           /// <summary>
           /// Json内容
           /// </summary>
           public string PageJson{get;set;}

           /// <summary>
           /// 模块名称
           /// </summary>
           public string ModuleName { get; set; }
       }


       public class ModulePageMappingList
       {
           public string VocaVerMappingID { get; set; }            }
    }
}
