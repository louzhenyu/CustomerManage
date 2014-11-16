/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/2/27 14:15:00
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
using System.Text;

using JIT.Utility.Web;
using System.Web;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.Session;

namespace JIT.CPOS.BS.Web.PageBase
{
    /// <summary>
    /// JITMPTreeHandler 
    /// </summary>
    public abstract class JITCPOSTreeHandler : JITTreeHandler<LoggingSessionInfo>
    {

        protected override LoggingSessionInfo CurrentUserInfo
        {
            get { return new SessionManager().CurrentUserLoginInfo; }
            //get { return SessionManager.CurrentUserInfo; }
            //get { return null; }
        }

        protected override void Authenticate()
        {
            //do nothing
        }

        protected string Request(string key)
        {
            if (HttpContext.Current.Request[key] == null) return string.Empty;
            return HttpContext.Current.Request[key];
        }
    }
}
