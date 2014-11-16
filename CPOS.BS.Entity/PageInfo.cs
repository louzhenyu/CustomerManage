using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 应用系统下的页面
    /// </summary>
    [Serializable]
    public class PageInfo
    {
        /// <summary>
        /// 号码
        /// </summary>
        public const string LANGUAGE_OBJECT_KIND_CODE = "AppSys.Page";

        private string id;
        private string menuId;
        private string pageCode;
        private string urlPath;
        private string description;
        /// <summary>
        /// Id
        /// </summary>
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// 页面所属的菜单的Id
        /// </summary>
        public string MenuId
        {
            get { return menuId; }
            set { menuId = value; }
        }
        /// <summary>
        /// 页面在应用系统中的编码
        /// </summary>
        public string PageCode
        {
            get { return pageCode; }
            set { pageCode = value; }
        }
        /// <summary>
        /// 页面在应用系统中的Url路径
        /// </summary>
        public string UrlPath
        {
            get { return urlPath; }
            set { urlPath = value; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}
