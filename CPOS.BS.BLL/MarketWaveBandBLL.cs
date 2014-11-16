/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/30 16:00:39
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
    public partial class MarketWaveBandBLL
    {
        #region GetList
        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="Page">��ҳҳ�롣��0��ʼ</param>
        /// <param name="PageSize">ÿҳ��������δָ��ʱĬ��Ϊ15</param>
        /// <returns></returns>
        public IList<MarketWaveBandEntity> GetList(MarketWaveBandEntity entity, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            IList<MarketWaveBandEntity> list = new List<MarketWaveBandEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetList(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<MarketWaveBandEntity>(ds.Tables[0]);
            }
            return list;
        }
        /// <summary>
        /// GetListCount
        /// </summary>
        /// <param name="entity">entity</param>
        public int GetListCount(MarketWaveBandEntity entity)
        {
            return _currentDAO.GetListCount(entity);
        }
        #endregion
    
        /// <summary>
        /// ��ȡ�����
        /// </summary>
        /// <param name="EventID">���ʶ</param>
        /// <param name="Page">ҳ��</param>
        /// <param name="PageSize">ҳ������</param>
        /// <returns></returns>
        public MarketWaveBandEntity GetMarketWaveBandByEventID(string EventID, int Page, int PageSize)
        {
            MarketWaveBandEntity marketWaveBandInfo = new MarketWaveBandEntity();
            try
            {
                DataSet ds = new DataSet();
                ds = _currentDAO.GetWaveBandByEventID(EventID, Page, PageSize);
                if(ds!= null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0){
                    marketWaveBandInfo.MarketWaveBandInfoList = DataTableToObject.ConvertToList<MarketWaveBandEntity>(ds.Tables[0]);
                    marketWaveBandInfo.ICount = _currentDAO.GetWaveBandByEventIDCount(EventID);
                }
                return marketWaveBandInfo;
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
    }
}