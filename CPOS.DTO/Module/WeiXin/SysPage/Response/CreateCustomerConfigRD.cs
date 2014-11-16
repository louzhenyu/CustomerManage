using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.SysPage.Response
{
    public class CreateCustomerConfigRD : IAPIResponseData
    {
        public ConfigInfo[] PageKeyList { get; set; }
    }
    public class ConfigInfo
    {
        public string path { get; set; }
        /// <summary>
        /// 直接取Node=1的Title
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 直接取
        /// </summary>
        public object plugin { get; set; }

        /// <summary>
        /// 直接取JsonValue中的Script
        /// </summary>
        public object script { get; set; }
        /// <summary>
        /// 当Node=2时，判断DefaultHtml=? 如果为1。则对应取出DefaultHtml=1的CSS
        /// </summary>
        public object style { get; set; }  //对应源文件的CSS

        /// <summary>
        /// 'param':{
        ///	'canAgian':true,		/*是否可重复刮*/
        ///	'canShare':true		/*是否可分享*/
        ///	}
        /// </summary>
        public object param { get; set; }
    }
    public class ConfigVersion
    {
        public string APP_DEBUG { get; set; }
        public string APP_CACHE { get; set; }
        public string APP_NAME { get; set; }
        public string APP_CODE { get; set; }
        public string APP_VERSION { get; set; }
        public string APP_OPTION_MENU { get; set; }
        public string APP_TOOL_BAR { get; set; }
        public string APP_JSLIB { get; set; }
        public string APP_TYPE { get; set; }
        public string AJAX_PARAMS { get; set; }

        public string APP_WX_TITLE{get;set;}
        public string APP_WX_ICO{get;set;}
        public string APP_WX_DES { get; set; }
    }
}
