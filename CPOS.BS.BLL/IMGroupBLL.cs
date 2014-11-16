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
    /// ҵ����  
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
        /// ���ݴ�����Id��ȡ���û�������Ⱥ�鼯��
        /// </summary>
        /// <param name="userID">�û�ID</param>
        /// <param name="customerID">�ͻ�ID</param>
        /// <returns></returns>
        public DataSet GetIMGroupByUserID(string userID, string customerID)
        {
            return _currentDAO.GetIMGroupUser(userID, customerID);
        }

        #region Ⱥ���û���������
        /// <summary>
        /// Ⱥ���û���������
        /// count ���Ӹ���
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
        /// ��ѯ�ƶ���Ⱥ�Ƿ��Ǹ��û������ġ�
        /// </summary>
        /// <param name="userID">�û�ID</param>
        /// <param name="groupID">Ⱥ��ID</param>
        /// <param name="customerID">�ͻ�ID</param>
        /// <returns></returns>
        public DataSet GetIMGroupUserByGroupID(string userID, string groupID, string customerID)
        {
            return _currentDAO.GetIMGroupUserByGroupID(userID, groupID, customerID);
        }

        #region Ⱥ����Ϣ
        public DataSet GetIMGroups(string userID, string customerID, string groupName, string chatGroupID, string bindGroupID, int pageIndex, int pageSize)
        {
            return _currentDAO.GetIMGroups(userID, customerID, groupName, chatGroupID, bindGroupID, pageIndex, pageSize);
        }

        public DataSet GetIMGroups(string customerID, string chatGroupID, string bindGroupID)
        {
            return _currentDAO.GetIMGroups(customerID, chatGroupID, bindGroupID);
        }
        #endregion

        #region ����Ⱥ�����ƹ���-���ź�̨
        /// <summary>
        /// ����Ⱥ�����ƹ���
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