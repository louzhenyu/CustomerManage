/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/17 14:52:47
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
    /// ҵ����  
    /// </summary>
    public partial class LPrizeWinnerBLL
    {  
         /// <summary>
        /// ����openID�ͻID ��ȡ�н���Ϣ
        /// </summary>
        /// <param name="vipID">vipID</param>
        /// <param name="eventID">�ID</param>
        /// <returns></returns>
        public SqlDataReader GetWinnerInfo(string vipID, string eventID)
        {
            return this._currentDAO.GetWinnerInfo(vipID, eventID);
        }
        public string GetWinnerInfoString(string vipID, string eventID)
        {
            return this._currentDAO.GetWinnerInfoString(vipID, eventID);
        }
        #region ��ȡ�Ʒ���н�����
        public IList<JIT.CPOS.BS.Entity.VipEntity> GetPrizesWinnerByGroupBrand(string PrizeBrand, string EventId, long Timestamp, string RoundId)
        {
            IList<VipEntity> list = new List<VipEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetPrizesWinnerByGroupBrand(PrizeBrand, EventId, Timestamp, RoundId);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<VipEntity>(ds.Tables[0]);
            }
            return list;
        }
        #endregion

        #region ��ȡ���ʱ���
        /// <summary>
        /// �����н��������ʱ���
        /// </summary>
        /// <param name="EventId"></param>
        /// <returns></returns>
        public string GetMaxTimestamp(string EventId, string RoundId)
        {
            string bReturn = _currentDAO.GetMaxTimestamp(EventId, RoundId);
            if(bReturn== null || bReturn.Equals(""))
            {
                bReturn = "0";
            }
            return bReturn;
        }
        #endregion

        #region ��ȡ��н����� Jermyn20131211
        /// <summary>
        /// ��ȡ�н�����
        /// </summary>
        /// <param name="EventId"></param>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public LPrizeWinnerEntity GetPrizeWinnerListByEventId(string EventId, int Page, int PageSize)
        {
            LPrizeWinnerEntity info = new LPrizeWinnerEntity();
            IList<LPrizeWinnerEntity> list = new List<LPrizeWinnerEntity>();
            DataSet ds = new DataSet();
            info.ICount = _currentDAO.GetPrizeWinnerListByEventIdCount(EventId);
            ds = _currentDAO.GetPrizeWinnerListByEventId(EventId, Page, PageSize);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<LPrizeWinnerEntity>(ds.Tables[0]);
            }
            info.PrizeWinnerList = list;
            return info;
        }
        #endregion

        public DataSet GetPrizeCouponTypeMapping(string prizeWinnerID,IDbTransaction tran)
        {
            return _currentDAO.GetPrizeCouponTypeMapping(prizeWinnerID,tran);
        }
        /// <summary>
        /// ���ݻId��ȡ���10���н�������
        /// </summary>
        /// <param name="strEventId"></param>
        /// <returns></returns>
        public DataSet GetTop10PizewWinnerListByEventId(string strEventId)
        {
            return _currentDAO.GetTop10PizewWinnerListByEventId(strEventId);
        }
        
    }
}