/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/3/21 14:59:53
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

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class T_CTW_LEventBLL
    {
        /// <summary>
        /// �����̻�CTWEventId��ȡ��Ϣ
        /// </summary>
        /// <param name="strCTWEventId"></param>
        /// <returns></returns>
        public DataSet GetLeventInfoByCTWEventId(string strCTWEventId)
        {
            return this._currentDAO.GetLeventInfoByCTWEventId(strCTWEventId);
        }
        public DataSet GetMaterialTextInfo(string strOnlineQRCodeId)
        {
            return this._currentDAO.GetMaterialTextInfo(strOnlineQRCodeId);
        }
        public void ChangeCTWEventStart(string strCTWEventId)
        {
            this._currentDAO.ChangeCTWEventStart(strCTWEventId);
        }
        /// <summary>
        /// ��ȡ�̻���������
        /// </summary>
        /// <param name="strCTWEventId"></param>
        /// <returns></returns>
        public DataSet GetLeventInfo(string strStatus, string strActivityGroupCode, string strEventName)
        {
            return this._currentDAO.GetLeventInfo(strStatus, strActivityGroupCode, strEventName);
        }
        /// <summary>
        /// ���ݻ���ͻid��ȡ��Ϣ
        /// </summary>
        /// <param name="intType">1:��Ϸ�2�������</param>
        /// <param name="strEventid">�id</param>
        /// <returns></returns>
        public DataSet GetEventInfoByLEventId(int intType, string strEventid)
        {
            return this._currentDAO.GetEventInfoByLEventId(intType, strEventid);
        }
        /// <summary>
        /// ͳ�Ƹ���Ӫ�����ͻ����
        /// </summary>
        /// <returns></returns>
        public DataSet GetEventStatusCount(string strCustomerId)
        {
            return this._currentDAO.GetEventStatusCount(strCustomerId);
        }


        public DataSet GetT_CTW_LEventList(string EventName, string BeginTime, string EndTime, string EventStatus, string ActivityGroupId, int PageSize, int PageIndex, string customerid)
        {
            return this._currentDAO.GetT_CTW_LEventList(EventName, BeginTime, EndTime, EventStatus, ActivityGroupId, PageSize, PageIndex, customerid);
        }

        public DataSet GetEventPrizeList(string CTWEventId, int PageSize, int PageIndex, string customerid)
        {
            return this._currentDAO.GetEventPrizeList(CTWEventId, PageSize, PageIndex, customerid);
        }


        public DataSet GetEventPrizeDetailList(string CTWEventId, int PageSize, int PageIndex, string customerid)
        {
            return this._currentDAO.GetEventPrizeDetailList(CTWEventId, PageSize, PageIndex, customerid);
        }
        /// <summary>
        /// ����Ϸ�Ĵ���ֿ�ͳ��
        /// </summary>
        /// <param name="strCTWEventId"></param>
        /// <returns></returns>
        public DataSet GetCTW_LEventStats(string strCTWEventId)
        {
            return this._currentDAO.GetCTW_LEventStats(strCTWEventId);
        }
        /// <summary>
        /// GetCTW_PanicbuyingEventRankingStats
        /// </summary>
        /// <param name="strCTWEventId"></param>
        /// <returns></returns>
        public DataSet GetCTW_PanicbuyingEventRankingStats(string strCTWEventId)
        {
            return this._currentDAO.GetCTW_PanicbuyingEventRankingStats(strCTWEventId);
        }



        public DataSet GeEventItemList(string CTWEventId, int PageSize, int PageIndex, string customerid)
        {
            return this._currentDAO.GeEventItemList(CTWEventId, PageSize, PageIndex, customerid);
        }

        public DataSet GeEventItemDetailList(string CTWEventId, int PageSize, int PageIndex, string customerid)
        {
            return this._currentDAO.GeEventItemDetailList(CTWEventId, PageSize, PageIndex, customerid);
        }

        /// <summary>
        /// ��ȡ��������Ĵ���ֿ��ͳ��
        /// </summary>
        /// <param name="cTwEventId"></param>
        /// <returns></returns>
        public DataSet GetPanicbuyingEventStats(string cTwEventId)
        {
            return this._currentDAO.GetPanicbuyingEventStats(cTwEventId);
        }
        /// <summary>
        /// ��ȡ��Ϸ�������Ա��������
        /// </summary>
        /// <param name="cTwEventId"></param>
        /// <returns></returns>
        public DataSet GetVipAddRankingStats(string cTwEventId)
        {
            return this._currentDAO.GetVipAddRankingStats(cTwEventId);
        }
    }
}