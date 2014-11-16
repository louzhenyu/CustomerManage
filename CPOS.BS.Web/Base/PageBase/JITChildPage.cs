/*
 * Author		:roy.tian
 * EMail		:jun.tian@jitmarketing.cn
 * Company		:JIT
 * Create On	:19/2/2012 10:03:10 AM
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using JIT.Utility.Log;
using System.Configuration;
using System.Web.UI.WebControls;
using JIT.CPOS.BS.Web.Session;
using JIT.Utility;

namespace JIT.CPOS.BS.Web.PageBase
{
    public abstract class JITChildPage : JITPage
    {
        protected string StaticUrl
        {
            get
            {
                string staticUrl = ConfigurationManager.AppSettings["staticUrl"];
                if (string.IsNullOrEmpty(staticUrl))
                {
                    staticUrl = "";
                }
                return staticUrl;
            }
        }
        public JITChildPage()
            : base()
        {
            pageType = 2;
        }
    }
}