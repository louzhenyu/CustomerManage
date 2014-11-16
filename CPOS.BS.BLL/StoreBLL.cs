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
    public partial class StoreBLL
    {
        #region GetList
        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="Page">��ҳҳ�롣��0��ʼ</param>
        /// <param name="PageSize">ÿҳ��������δָ��ʱĬ��Ϊ15</param>
        /// <returns></returns>
        public IList<StoreEntity> GetList(StoreEntity entity, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            IList<StoreEntity> list = new List<StoreEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetList(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<StoreEntity>(ds.Tables[0]);
            }
            return list;
        }
        /// <summary>
        /// GetListCount
        /// </summary>
        /// <param name="entity">entity</param>
        public int GetListCount(StoreEntity entity)
        {
            return _currentDAO.GetListCount(entity);
        }
        #endregion

        #region ��ȡͬ�������ŵ�

        /// <summary>
        /// ��ȡͬ�������ŵ�
        /// </summary>
        /// <param name="latestTime">���ͬ��ʱ��</param>
        /// <returns></returns>
        public DataSet GetSynWelfareStoreList(string latestTime)
        {
            return this._currentDAO.GetSynWelfareStoreList(latestTime);
        }

        #endregion

        public DataSet GetCityDsByUnit(string cityName, string cityCode,string customerId)
        {
            return this._currentDAO.GetCityDsByUnit(cityName, cityCode, customerId);
        }
        public DataSet GetHotleStarAndPrice()
        {
            return this._currentDAO.GetHotleStarAndPrice();
        }
        public DataSet GetHotleList(string cityCode, string dateFrom, string dateTo, string hotleName,
           decimal priceFrom, decimal priceTo, string hotleStra, decimal log, decimal lat, string customerId,string orderItem,string orderType,decimal radius)
        {
            return this._currentDAO.GetHotleList(cityCode, dateFrom, dateTo, hotleName, priceFrom, priceTo, hotleStra, log, lat, customerId, orderItem, orderType, radius);
        }



        #region ��ȡ�Ƶ�����ͷ����б� ����
        public DataSet GetHotelDetails(string unitID)
        {
            return this._currentDAO.GetHotelDetails(unitID);
        }
        public DataSet GetHotelRoomSku(string unitID, DateTime beginDate, DateTime endDate)
        {
            return this._currentDAO.GetHotelRoomSku(unitID, beginDate, endDate);
        }
        #endregion 
    }
}