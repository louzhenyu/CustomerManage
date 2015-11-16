/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/9/9 17:12:31
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
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using System.Data.SqlClient;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class C_ActivityBLL
    {
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public SqlTransaction GetTran()
        {
            return this._currentDAO.GetTran();
        }
        /// <summary>
        /// ���ݻID��ȡĿ��Ⱥ������
        /// </summary>
        /// <param name="ActivityID"></param>
        /// <returns></returns>
        public string GetTargetGroups(string ActivityID) {
            return _currentDAO.GetTargetGroups(ActivityID);
        }

        /// <summary>
        /// ������ȡ��ȡ�ֿ�����
        /// </summary>
        /// <returns></returns>
        public int GetholderCardCount(string VipCardTypeID, string ActivityID) {
            return _currentDAO.GetholderCardCount(VipCardTypeID, ActivityID);
        }
        /// <summary>
        /// ˵������ȡ�δ�ͽ�Ʒ�Ļ�Ա��Ϣ(�Ŀ��Ⱥ�������л�Ա���û�)
        /// ʹ�ã���ʱ��ȯʹ��
        /// </summary>
        /// <param name="activityId">�ID</param>
        /// <param name="prizesId">��ƷID</param>
        /// <returns></returns>
        public DataSet GetSendPrizeVipID(Guid activityId, Guid prizesId)
        {
            return _currentDAO.GetSendPrizeVipID(activityId, prizesId);
        }
        /// <summary>
        /// ˵������ȡ�����δ�ͽ�Ʒ�Ļ�Ա��Ϣ���Ŀ��Ⱥ����ĳ��������û���
        /// ʹ�ã���ʱ��ȯʹ��
        /// </summary>
        /// <param name="activityId">�ID</param>
        /// <param name="prizesId">��ƷID</param>
        /// <param name="VipCardTypeID">������ID</param>
        /// <returns></returns>
        public DataSet GetSendPrizeVipID(Guid activityId, Guid prizesId, string vipCardTypeID)
        {
            return _currentDAO.GetSendPrizeVipID(activityId, prizesId,vipCardTypeID);
        }


    }
}