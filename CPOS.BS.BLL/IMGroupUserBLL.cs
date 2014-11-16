/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/26 14:59:01
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
    public partial class IMGroupUserBLL
    {
        public DataSet GetIMGroupUser(string userID, string chatGroupID, string customerID)
        {
            return _currentDAO.GetIMGroupUser(userID, chatGroupID, customerID);
        }

        public DataSet GetIMGroupUser(string chatGroupID, string customerID, int pageIndex, int pageSize)
        {
            return _currentDAO.GetIMGroupUser(chatGroupID, customerID, pageIndex, pageSize);
        }

        public void AddUsersGroup(List<string> userIDList, string chatGroupID, string customerID)
        {
            _currentDAO.AddUsersGroup(userIDList, chatGroupID, customerID);
        }

        public void DelUsersGroup(List<string> userIDList, string chatGroupID, string customerID)
        {
            _currentDAO.DelUsersGroup(userIDList, chatGroupID, customerID);
        }


        /// <summary>
        /// 根据群组ID获取群组用户
        /// </summary>
        /// <param name="groupID">群组ID</param>
        /// <param name="customerID">客户ID</param>
        /// <returns></returns>
        public DataSet GetGroupUserByGroupID(string groupID, string customerID)
        {
            return _currentDAO.GetGroupUserByGroupID(groupID, customerID);
        }
    }
}