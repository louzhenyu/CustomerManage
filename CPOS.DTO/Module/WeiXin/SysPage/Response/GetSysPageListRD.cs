using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.SysPage.Response
{
    public	class GetSysPageListRD:IAPIResponseData
	{

        public ListPageInfo[] PageList{get;set;}
        public int TotalPageCount { get; set; }
	}
    public class ListPageInfo
    {
        /// <summary>
        /// 标识 
        /// </summary>
        public string PageId{get;set;}

        /// <summary>
        /// 标题名称
        /// </summary>
        public string Title{get;set;}

        /// <summary>
        /// Key
        /// </summary>
        public string PageKey{get;set;}

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version{get;set;}

        /// <summary>
        /// 更新时间
        /// </summary>
        public string LastUpdateTime{get;set;}

        /// <summary>
        /// 客户版本映射
        /// </summary>
        public string MappingID { get; set; }
    }
}
