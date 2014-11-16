/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/12/16 13:44:32
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
    public partial class TUnitSortBLL
    {
        #region 获取某个客户的所有门店所在的城市列表，地级市

        /// <summary>
        /// 获取某个客户的所有门店所在的城市列表，地级市
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <returns></returns>
        public DataSet GetCityListByCustomerId(string customerId)
        {
            return _currentDAO.GetCityListByCustomerId(customerId);
        }

        #endregion

        #region 获取门店集合
        public StoreBrandMappingEntity GetStoreListByItem(string cityName, int Page, int PageSize, string Longitude, string Latitude, out string strError)
        {
            StoreBrandMappingEntity storeInfo = new StoreBrandMappingEntity();
            try
            {
                storeInfo.TotalCount = _currentDAO.GetStoreListByItemCount(cityName, Page, PageSize, Longitude, Latitude);
                IList<StoreBrandMappingEntity> list = new List<StoreBrandMappingEntity>();
                DataSet ds = new DataSet();
                ds = _currentDAO.GetStoreListByItem(cityName, Page, PageSize, Longitude, Latitude);
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