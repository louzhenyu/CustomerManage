/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/10/16 16:35:20
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
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class PushIOSMessageBLL
    {
        #region 市场活动使用APP推送消息
        /// <summary>
        /// 市场活动使用APP推送消息
        /// </summary>
        /// <param name="MarketingId">活动标识</param>
        /// <returns></returns>
        public bool SetMarketPushApp(string MarketEventId)
        {
            bool bReturn = _currentDAO.SetMarketPushApp(MarketEventId);
            return bReturn;

        }
        #endregion
    }
}