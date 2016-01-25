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
using JIT.CPOS.BS.Entity.Interface;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class LPrizePoolsBLL
    {
        #region 46 ҡ�� Jermyn20130509
        /// <summary>
        /// ҡ��
        /// </summary>
        /// <param name="UserName">�û���</param>
        /// <param name="UserID">�û���ʶ</param>
        /// <param name="EventID">���ʶ</param>
        /// <param name="Longitude"></param>
        /// <param name="Latitude"></param>
        /// <returns></returns>
        public GetResponseParams<ShakeOffLotteryResult> SetShakeOffLottery(string UserName, string UserID, string EventID, float Longitude, float Latitude)
        {
            #region �ж϶�����Ϊ��
            if (EventID == null || EventID.ToString().Equals(""))
            {
                return new GetResponseParams<ShakeOffLotteryResult>
                {
                    Flag = "0",
                    Code = "419",
                    Description = "EventID����Ϊ��",
                };
            }
            if (UserID == null || UserID.ToString().Equals(""))
            {
                return new GetResponseParams<ShakeOffLotteryResult>
                {
                    Flag = "0",
                    Code = "405",
                    Description = "UserId����Ϊ��",
                };
            }
            #endregion

            GetResponseParams<ShakeOffLotteryResult> response = new GetResponseParams<ShakeOffLotteryResult>();
            response.Flag = "1";
            response.Code = "200";
            response.Description = "�ɹ�";
            try
            {
                ShakeOffLotteryResult eventsInfo = new ShakeOffLotteryResult();
                DataSet ds = new DataSet();
                ds = _currentDAO.SetShakeOffLottery(UserName, UserID, EventID, Longitude, Latitude);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    eventsInfo = DataTableToObject.ConvertToObject<ShakeOffLotteryResult>(ds.Tables[0].Rows[0]);
                }

                response.Params = eventsInfo;
                return response;
            }
            catch (Exception ex)
            {
                response.Flag = "0";
                response.Code = "103";
                //response.Description = "ʧ��:" + ex.ToString();
                return response;
            }
        }
        #endregion 


        public DataSet GetEventWinningInfo(string userName, string vipId, string eventId, float longitude, float latitude, string customerId, string reCommandId, int pointsLotteryFlag)
        {
            return this._currentDAO.GetEventWinningInfo(userName, vipId, eventId, longitude, latitude, customerId, reCommandId, pointsLotteryFlag);
        }

        public DataSet GetPersonWinnerList(string vipId, string eventId)
        {
            return this._currentDAO.GetPersonWinnerList(vipId, eventId);
        }
        #region  �н������ѻ�� Jermyn20131223
        /// <summary>
        /// ҡ��
        /// </summary>
        /// <param name="UserName">�û���</param>
        /// <param name="UserID">�û���ʶ</param>
        /// <param name="EventID">���ʶ</param>
        /// <param name="Longitude"></param>
        /// <param name="Latitude"></param>
        /// <returns></returns>
        public GetResponseParams<ShakeOffLotteryResult> SetShakeOffLotteryBySales(string UserName, string UserID, string EventID, float Longitude, float Latitude)
        {
            #region �ж϶�����Ϊ��
            if (EventID == null || EventID.ToString().Equals(""))
            {
                return new GetResponseParams<ShakeOffLotteryResult>
                {
                    Flag = "0",
                    Code = "419",
                    Description = "EventID����Ϊ��",
                };
            }
            if (UserID == null || UserID.ToString().Equals(""))
            {
                return new GetResponseParams<ShakeOffLotteryResult>
                {
                    Flag = "0",
                    Code = "405",
                    Description = "UserId����Ϊ��",
                };
            }
            #endregion

            GetResponseParams<ShakeOffLotteryResult> response = new GetResponseParams<ShakeOffLotteryResult>();
            response.Flag = "1";
            response.Code = "200";
            response.Description = "�ɹ�";
            try
            {
                ShakeOffLotteryResult eventsInfo = new ShakeOffLotteryResult();
                DataSet ds = new DataSet();
                ds = _currentDAO.SetShakeOffLotteryBySales(UserName, UserID, EventID, Longitude, Latitude);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    eventsInfo = DataTableToObject.ConvertToObject<ShakeOffLotteryResult>(ds.Tables[0].Rows[0]);
                }

                response.Params = eventsInfo;
                return response;
            }
            catch (Exception ex)
            {
                response.Flag = "0";
                response.Code = "103";
                //response.Description = "ʧ��:" + ex.ToString();
                return response;
            }
        }
        #endregion 


        public DataSet GetUserPrizeWinnerLog(string eventId, string vipId)
        {
            return this._currentDAO.GetUserPrizeWinnerLog(eventId, vipId);
        }
        /// <summary>
        /// ���һ����Ʒ
        /// </summary>
        /// <param name="strEventId"></param>
        /// <returns></returns>
        public DataSet GetRandomPrizeByEventId(string strEventId)
        {
            return this._currentDAO.GetRandomPrizeByEventId(strEventId);

        } 
    }
}