using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.Module.Response
{
    public class GetSysModuleListRD : IAPIResponseData
    {
        public ModulePageInfo[] SysModuleList { get; set; }
    }

    public class ModulePageInfo
    {
        public string PageID { get; set; }//String	模板Page标识
        public string ModuleName { get; set; }//String	Code
        public string PageCode { get; set; }//String	页面类别码,根据类别来确定前端页面显示如活动、系统功能等
        public string URLTemplate { get; set; }//URL模板
    }
}
