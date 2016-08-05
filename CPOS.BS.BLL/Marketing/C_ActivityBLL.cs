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
    /// 业务处理：  
    /// </summary>
    public partial class C_ActivityBLL
    {
        /// <summary>
        /// 事务
        /// </summary>
        /// <returns></returns>
        public SqlTransaction GetTran()
        {
            return this._currentDAO.GetTran();
        }
        /// <summary>
        /// 根据活动ID获取目标群体名称,如果IsAllCardType = 1,返回所有卡类型
        /// </summary>
        /// <param name="ActivityID"></param>
        /// <returns></returns>
        public string GetTargetGroups(int IsAllCardType, string ActivityID)
        {
            return _currentDAO.GetTargetGroups(IsAllCardType, ActivityID);
        }

        /// <summary>
        /// 根据活动ID获取目标群体ID,如果IsAllCardType = 1,返回所有卡类型
        /// </summary>
        /// <param name="ActivityID"></param>
        /// <returns></returns>
        public List<string> GetTargetGroupId(int IsAllCardType, string ActivityID)
        {
            return _currentDAO.GetTargetGroupId(IsAllCardType, ActivityID);
        }

        /// <summary>
        /// 条件获取获取持卡人数
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
        /// 检查活动名称是否有效
        /// </summary>
        /// <param name="activityName"></param>
        /// <returns></returns>
        public bool IsActivityNameValid(string activityName)
        {
            return _currentDAO.IsActivityNameValid(activityName);
        }

        /// <summary>
        /// 说明：获取活动未送奖品的会员信息(活动目标群体是所有会员卡用户)
        /// 使用：定时送券使用
        /// </summary>
        /// <param name="activityId">活动ID</param>
        /// <param name="prizesId">奖品ID</param>
        /// <returns></returns>
        public DataSet GetSendPrizeVipID(Guid activityId, Guid prizesId)
        {
            return _currentDAO.GetSendPrizeVipID(activityId, prizesId);
        }
        /// <summary>
        /// 说明：获取活动所有未送奖品的会员信息（活动目标群体是某个卡类别用户）
        /// 使用：定时送券使用
        /// </summary>
        /// <param name="activityId">活动ID</param>
        /// <param name="prizesId">奖品ID</param>
        /// <param name="VipCardTypeID">卡类型ID</param>
        /// <returns></returns>
        public DataSet GetSendPrizeVipID(Guid activityId, Guid prizesId, string vipCardTypeID)
        {
            return _currentDAO.GetSendPrizeVipID(activityId, prizesId, vipCardTypeID);
        }

        /// <summary>
        /// 当活动类型为3(充值活动）,检查已有活动起止时间是否重叠，重叠返回true
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