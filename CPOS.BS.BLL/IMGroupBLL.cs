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
    public partial class IMGroupBLL
    {
        public DataSet GetUserInGroups(string userID, string customerID, int pageIndex, int pageSize)
        {
            return _currentDAO.GetUserInGroups(userID, customerID, pageIndex, pageSize);
        }

        public void DeleteGroup(string userID, string customerID, string chatGroupId)
        {
            _currentDAO.DeleteGroup(userID, customerID, chatGroupId);
        }

        /// <summary>
        /// 根据创建人Id获取该用户创建的群组集合
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="customerID">客户ID</param>
        /// <returns></returns>
        public DataSet GetIMGroupByUserID(string userID, string customerID)
        {
            return _currentDAO.GetIMGroupUser(userID, customerID);
        }

        #region 群组用户数量更改
        /// <summary>
        /// 群组用户数量更改
        /// count 正加负减
        /// </summary>
        /// <param name="count"></param>
        /// <param name="chatGroupID"></param>
        /// <returns></returns>
        public void UpdateGroupUserCount(int count, string chatGroupID)
        {
            _currentDAO.UpdateGroupUserCount(count, chatGroupID);
        }
        #endregion


        /// <summary>
        /// 查询制定的群是否是该用户创建的。
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="groupID">群组ID</param>
        /// <param name="customerID">客户ID</param>
        /// <returns></returns>
        public DataSet GetIMGroupUserByGroupID(string userID, string groupID, string customerID)
        {
            return _currentDAO.GetIMGroupUserByGroupID(userID, groupID, customerID);
        }

        #region 群组信息
        public DataSet GetIMGroups(string userID, string customerID, string groupName, string chatGroupID, string bindGroupID, int pageIndex, int pageSize)
        {
            return _currentDAO.GetIMGroups(userID, customerID, groupName, chatGroupID, bindGroupID, pageIndex, pageSize);
        }

        public DataSet GetIMGroups(string customerID, string chatGroupID, string bindGroupID)
        {
            return _currentDAO.GetIMGroups(customerID, chatGroupID, bindGroupID);
        }
        #endregion

        #region 根据群组名称过滤-企信后台
        /// <summary>
        /// 根据群组名称过滤
        /// </summary>
        /// <param name="pGroupName"></param>
        /// <returns></returns>
        public DataTable GetIMGroupByGroupName(string pGroupName)
        {
            return this._currentDAO.GetIMGroupByGroupName(pGroupName);
        }
        #endregion
    }
}