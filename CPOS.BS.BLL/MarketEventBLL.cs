/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/30 16:00:38
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
using JIT.Utility.Log;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class MarketEventBLL
    {
        #region ��б��ȡ

        /// <summary>
        /// ��б��ȡ
        /// </summary>
        /// <param name="Page">��ҳҳ�롣��0��ʼ</param>
        /// <param name="PageSize">ÿҳ��������δָ��ʱĬ��Ϊ15</param>
        /// <returns></returns>
        public List<MarketEventEntity> GetEventList(int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            List<MarketEventEntity> eventList = new List<MarketEventEntity>();

            DataSet ds = _currentDAO.GetEventList(Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                eventList = DataTableToObject.ConvertToList<MarketEventEntity>(ds.Tables[0]);
            }

            return eventList;
        }
        /// <summary>
        /// ��б�������ȡ
        /// </summary>
        /// <param name="VideoType">video</param>
        public int GetEventListCount()
        {
            return _currentDAO.GetEventListCount();
        }

        #endregion

        #region ���ϸ
        /// <summary>
        /// ���ϸ (�����)
        /// </summary>
        /// <param name="EventId"></param>
        /// <returns></returns>
        public MarketEventEntity GetMarketEventInfoById(string EventId)
        {
            MarketEventEntity eventInfo = new MarketEventEntity();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetMarketEventInfoById(EventId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                eventInfo = DataTableToObject.ConvertToObject<MarketEventEntity>(ds.Tables[0].Rows[0]);
            }
            //��ȡ�ģ����Ϣ
            ds = _currentDAO.GetMarketTemplateByEventID(EventId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                eventInfo.MarketTemplageInfo = DataTableToObject.ConvertToObject<MarketTemplateEntity>(ds.Tables[0].Rows[0]);
                //eventInfo.TemplateContent = eventInfo.MarketTemplageInfo.TemplateContent;
            }
            //��ȡ�����
            ds = _currentDAO.GetMarketWaveBandByEventID(EventId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                eventInfo.MarketWaveBandInfoList = DataTableToObject.ConvertToList<MarketWaveBandEntity>(ds.Tables[0]);
            }
            //��ȡ��ŵ�

            //��ȡ���Ⱥ
            return eventInfo;
        }
        #endregion

        #region �����
        /// <summary>
        /// �����
        /// </summary>
        /// <param name="EventID"></param>
        /// <returns></returns>
        public MarketEventAnalysisEntity GetEventAnalysisInfo(string EventID)
        {
            MarketEventAnalysisEntity info = new MarketEventAnalysisEntity();
            MarketEventEntity eventInfo = new MarketEventEntity();
            eventInfo = GetByID(EventID);
            if (eventInfo != null)
            {
                info.BeginDate = eventInfo.BeginTime + " �� " + eventInfo.EndTime;
                info.EndDate = eventInfo.EndTime;
                info.BudgetTotal = eventInfo.BudgetTotal;
                info.MarketEventID = EventID;
                info.PersonCount = eventInfo.PersonCount;
                info.StoreCount = eventInfo.StoreCount;
                info.CurrentSales = GetEventCurrentSales(EventID);    //��ǰ���۶�

                if (info.StoreCount == null || info.StoreCount==0)
                {
                    info.StoreCount = 0;
                    info.ResponseStoreCount = 0;
                    info.ResponseStoreRate = "0%";
                }
                else {
                    info.ResponseStoreCount = info.StoreCount / 2 + 30;
                    info.ResponseStoreRate = (Convert.ToDecimal(info.ResponseStoreCount) / Convert.ToDecimal(info.StoreCount) * 100).ToString("#0.00") + "%";
                }
                if (info.PersonCount != null && info.PersonCount > 0)
                {
                    info.ResponsePersonCount = _currentDAO.GetAnalysisResponsePersonCount(EventID);
                    info.ResponsePersonRate = ((Convert.ToDecimal(info.ResponsePersonCount) / Convert.ToDecimal(info.PersonCount) * 100)).ToString("#0.00") + "%";
                }
                else {
                    info.PersonCount = 200;
                    info.ResponsePersonCount = _currentDAO.GetAnalysisResponsePersonCount(EventID);
                    info.ResponsePersonRate = ((Convert.ToDecimal(info.ResponsePersonCount) / Convert.ToDecimal(info.PersonCount) * 100)).ToString("#0.00") + "%";
                }
            }
            return info;
        }
        #endregion

        #region �޸Ļ״̬
        /// <summary>
        /// �޸�״̬
        /// </summary>
        /// <param name="Status"></param>
        /// <param name="EventID"></param>
        /// <returns></returns>
        public bool SetMarketEventStatus(int Status, string EventID)
        {
            MarketEventEntity info = new MarketEventEntity();
            info.MarketEventID = EventID;
            info.EventStatus = Status;
            Update(info, false);
            return true;
        }
        #endregion


        #region ��ȡ����ѽ��
        /// <summary>
        /// ��ȡ����ѽ��
        /// </summary>
        /// <param name="EventId"></param>
        /// <returns></returns>
        public decimal GetEventCurrentSales(string EventId)
        {
            return _currentDAO.GetEventCurrentSales(EventId);
        }
        #endregion
    }
}