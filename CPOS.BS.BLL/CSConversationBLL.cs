/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/5 18:00:39
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
    /// 业务处理： 交流记录 
    /// </summary>
    public partial class CSConversationBLL
    {
        /// <summary>
        /// 接收消息列表
        /// </summary>
        /// <param name="isCS">是否是客服1：是0：否</param>
        /// <param name="personID">当前用户ID</param>
        /// <param name="messageId">当前消息ID，如果要获取所有消息，则消息ID为NULL</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>消息列表</returns>
        public DataSet GetMessageVipInfo(string personID, string customerId)
        {
           return  this._currentDAO.GetMessageVipInfo(personID, customerId);
        }

    }

}