/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/17 14:52:46
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
using System.Data.SqlClient;


namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class LLotteryLogBLL
    {
        /// <summary>
        /// 事务
        /// </summary>
        /// <returns></returns>
        public SqlTransaction GetTran()
        {
            return this._currentDAO.GetTran();
        }

        #region 活动抽奖日志列表
        /// <summary>
        /// 活动抽奖日志列表
        /// </summary>
        /// <param name="EventID">活动ID</param>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public GetResponseParams<LLotteryLogEntity> GetEventLotteryLog(string EventID, int Page, int PageSize)
        {
            #region 判断对象不能为空
            if (EventID == null || EventID.ToString().Equals(""))
            {
                return new GetResponseParams<LLotteryLogEntity>
                {
                    Flag = "0",
                    Code = "419",
                    Description = "活动ID不能为空",
                };
            }
            if (PageSize == 0) PageSize = 15;
            #endregion

            GetResponseParams<LLotteryLogEntity> response = new GetResponseParams<LLotteryLogEntity>();
            response.Flag = "1";
            response.Code = "200";
            response.Description = "成功";
            try
            {
                #region 业务处理
                LLotteryLogEntity usersInfo = new LLotteryLogEntity();

                usersInfo.ICount = _currentDAO.GetEventLotteryLogCount(EventID);

                IList<LLotteryLogEntity> usersInfoList = new List<LLotteryLogEntity>();
                if (usersInfo.ICount > 0)
                {
                    DataSet ds = new DataSet();
                    ds = _currentDAO.GetEventLotteryLogList(EventID, Page, PageSize);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        usersInfoList = DataTableToObject.ConvertToList<LLotteryLogEntity>(ds.Tables[0]);
                    }
                }

                usersInfo.EntityList = usersInfoList;
                #endregion
                response.Params = usersInfo;
                return response;
            }
            catch (Exception ex)
            {
                response.Flag = "0";
                response.Code = "103";
                response.Description = "错误:" + ex.ToString();
                return response;
            }
        }
        #endregion

 
    }
}