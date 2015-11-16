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
        /// 根据活动ID获取目标群体名称
        /// </summary>
        /// <param name="ActivityID"></param>
        /// <returns></returns>
        public string GetTargetGroups(string ActivityID) {
            return _currentDAO.GetTargetGroups(ActivityID);
        }

        /// <summary>
        /// 条件获取获取持卡人数
        /// </summary>
        /// <returns></returns>
        public int GetholderCardCount(string VipCardTypeID, string ActivityID) {
            return _currentDAO.GetholderCardCount(VipCardTypeID, ActivityID);
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
            return _currentDAO.GetSendPrizeVipID(activityId, prizesId,vipCardTypeID);
        }


    }
}