/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/8/30 11:11:11
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
    /// 业务处理：  
    /// </summary>
    public partial class StoreBrandMappingBLL
    {
        #region 获取商品门店集合
        public StoreBrandMappingEntity GetStoreListByItem(string ItemId
            , int Page
            , int PageSize
            , string Longitude
            , string Latitude
            , out string strError)
        {
            StoreBrandMappingEntity storeInfo = new StoreBrandMappingEntity();
            try
            {
                storeInfo.TotalCount = _currentDAO.GetStoreListByItemCount(ItemId, Page, PageSize, Longitude, Latitude);
                IList<StoreBrandMappingEntity> list = new List<StoreBrandMappingEntity>();
                DataSet ds = new DataSet();
                ds = _currentDAO.GetStoreListByItem(ItemId, Page, PageSize, Longitude, Latitude);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    list = DataTableToObject.ConvertToList<StoreBrandMappingEntity>(ds.Tables[0]);
                }
                strError = "ok";
                storeInfo.StoreBrandList = list;
            }
            catch (Exception ex) {
                strError = ex.ToString();
            }
            return storeInfo;
        }
        #endregion

        #region 获取门店详情
        /// <summary>
        /// 获取门店详情
        /// </summary>
        /// <param name="StoreId"></param>
        /// <returns></returns>
        public StoreBrandMappingEntity GetStoreDetail(string StoreId)
        {
            StoreBrandMappingEntity info = new StoreBrandMappingEntity();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetStoreDetail(StoreId);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                info = DataTableToObject.ConvertToObject<JIT.CPOS.BS.Entity.StoreBrandMappingEntity>(ds.Tables[0].Rows[0]);
            }
            ObjectImagesBLL imageServer = new ObjectImagesBLL(this.CurrentUserInfo);
            info.ImageList = imageServer.QueryByEntity(new ObjectImagesEntity()
                        {
                            ObjectId = StoreId
                        }, null);
            
            return info;
        }
        #endregion

        #region 获取同步福利门店品牌关系

        /// <summary>
        /// 获取同步福利门店品牌关系
        /// </summary>
        /// <param name="latestTime">最后同步时间</param>
        /// <returns></returns>
        public DataSet GetSynWelfareStoreBrandMappingList(string latestTime)
        {
            return this._currentDAO.GetSynWelfareStoreBrandMappingList(latestTime);
        }

        #endregion

        #region 获取城市的门店集合
        public StoreBrandMappingEntity GetStoreListByCity(string city
            , int Page
            , int PageSize
            , string Longitude
            , string Latitude
            , string beginDate
            ,string endDate
            ,out string strError)
        {
            if (Longitude == null || Longitude.Length == 0) Longitude = "0";
            if (Latitude == null || Latitude.Length == 0) Latitude = "0";
            StoreBrandMappingEntity storeInfo = new StoreBrandMappingEntity();
            try
            {
                storeInfo.TotalCount = _currentDAO.GetStoreListByCityCount(city, Page, PageSize, Longitude, Latitude);
                IList<StoreBrandMappingEntity> list = new List<StoreBrandMappingEntity>();
                DataSet ds = new DataSet();
                ds = _currentDAO.GetStoreListByCity(city, Page, PageSize, Longitude, Latitude,beginDate,endDate);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    list = DataTableToObject.ConvertToList<StoreBrandMappingEntity>(ds.Tables[0]);
                }
                strError = "ok";
                storeInfo.StoreBrandList = list;
            }
            catch (Exception ex)
            {
                strError = ex.ToString();
            }
            return storeInfo;
        }
        #endregion
    }
}