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
        /// ���ݻID��ȡĿ��Ⱥ������,���IsAllCardType = 1,�������п�����
        /// </summary>
        /// <param name="ActivityID"></param>
        /// <returns></returns>
        public string GetTargetGroups(int IsAllCardType, string ActivityID)
        {
            return _currentDAO.GetTargetGroups(IsAllCardType, ActivityID);
        }

        /// <summary>
        /// ���ݻID��ȡĿ��Ⱥ��ID,���IsAllCardType = 1,�������п�����
        /// </summary>
        /// <param name="ActivityID"></param>
        /// <returns></returns>
        public List<string> GetTargetGroupId(int IsAllCardType, string ActivityID)
        {
            return _currentDAO.GetTargetGroupId(IsAllCardType, ActivityID);
        }

        /// <summary>
        /// ������ȡ��ȡ�ֿ�����
        /// </summary>
        /// <returns></returns>
        public int GetHolderCardCount(List<string> vipCardTypeIdList)
        {
            return _currentDAO.GetHolderCardCount(vipCardTypeIdList);
        }

        public int GetBirthHolderCardCount(List<string> vipCardTypeIdList, string activityStartTime,
            string activityEndTime)
        {
            return _currentDAO.GetBirthHolderCardCount(vipCardTypeIdList, activityStartTime, activityEndTime);
        }

        public int GetTargetCount(List<string> vipCardTypeIdList, int activityType, string activityStartTime, string activityEndTime,
            int isLongTime)
        {
            int result = 0;
            if (activityType == 1)
            {
                string endTime = "";
                if (!string.IsNullOrWhiteSpace(activityEndTime))
                    endTime = activityEndTime + " 23:59:59";
                else
                    endTime = "2099-01-01 23:59:59";
                if (isLongTime == 1)
                {
                    endTime = "2099-01-01 23:59:59";
                }
                result = GetBirthHolderCardCount(vipCardTypeIdList, activityStartTime, endTime);
            }
            else
            {
                result = GetHolderCardCount(vipCardTypeIdList);
            }
            return result;
        }

        /// <summary>
        /// ��������Ƿ���Ч
        /// </summary>
        /// <param name="activityName"></param>
        /// <returns></returns>
        public bool IsActivityNameValid(string activityName)
        {
            return _currentDAO.IsActivityNameValid(activityName);
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
            return _currentDAO.GetSendPrizeVipID(activityId, prizesId, vipCardTypeID);
        }

        /// <summary>
        /// �������Ϊ3(��ֵ���,������л��ֹʱ���Ƿ��ص����ص�����true
        /// </summary>
        /// <returns></returns>
        public bool IsActivityOverlap(string customerId, string activityId, int activityType, string startTime, string endTime, List<string> vipCardTypeIdList)
        {
            return _currentDAO.IsActivityOverlap(customerId, activityId, activityType, startTime, endTime, vipCardTypeIdList);
        }

        public bool IsActivityValid(string activityId)
        {
            return _currentDAO.IsActivityValid(activityId);
        }
    }
}