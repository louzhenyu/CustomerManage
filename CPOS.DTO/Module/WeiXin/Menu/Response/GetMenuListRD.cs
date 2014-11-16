using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.Menu.Response
{
    public class GetMenuListRD : IAPIResponseData
    {
        public MenuInfo[] MenuList { get; set; }
    }

    public class MenuInfo
    {
        /// <summary>
        /// 标识
        /// </summary>
        public string MenuId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        public string ParentId { get; set; }
        /// <summary>
        /// 层级
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 排序(第一层横排,其它层竖排)	
        /// </summary>
        public int DisplayColumn { get; set; }
        /// <summary>
        /// 状态, 1=启用，0=停用	
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 微信帐号
        /// </summary>
        public string WeiXinId { get; set; }

        public string ApplicationId { get; set; }

        /// <summary>
        /// 微信菜单
        /// </summary>
        public MenuChildInfo[] SubMenus { get; set; }
    
    }


    public class MenuChildInfo
    {
        /// <summary>
        /// 标识
        /// </summary>
        public string MenuId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        public string ParentId { get; set; }
        /// <summary>
        /// 层级
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 排序(第一层横排,其它层竖排)	
        /// </summary>
        public int DisplayColumn { get; set; }
        /// <summary>
        /// 状态, 1=启用，0=停用	
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 微信帐号
        /// </summary>
        public string WeiXinId { get; set; }

        public string ApplicationId { get; set; }


    }
   
   
}
