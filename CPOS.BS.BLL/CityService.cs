using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess;
using System.Data;
using System.Data.SqlClient;


namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 城市服务
    /// </summary>
    public class CityService : BaseService
    {
        JIT.CPOS.BS.DataAccess.CityService cityService = null;
        #region 构造函数
        public CityService(LoggingSessionInfo loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
            cityService = new DataAccess.CityService(loggingSessionInfo);
        }
        #endregion

        #region 获取城市信息
        /// <summary>
        /// 获取城市信息
        /// </summary>
        /// <returns></returns>
        public IList<CityInfo> GetCityInfoList(string pStoreID = null)
        {
            IList<CityInfo> cityInfoList = new List<CityInfo>();
            DataSet ds = new DataSet();
            ds = cityService.GetCityInfoList(pStoreID);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                cityInfoList = DataTableToObject.ConvertToList<CityInfo>(ds.Tables[0]);
            }
            return cityInfoList;
        }
        /// <summary>
        /// 获取同市的区县信息
        /// </summary>
        /// <param name="DistrictID"></param>
        /// <returns></returns>
        public IList<CityInfo> GetSameLevelDistrictsByDistrictID(string DistrictID)
        {
            IList<CityInfo> cityInfoList = new List<CityInfo>();
            DataSet ds = new DataSet();
            ds = cityService.GetSameLevelDistrictsByDistrictID(DistrictID);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                cityInfoList = DataTableToObject.ConvertToList<CityInfo>(ds.Tables[0]);
            }
            return cityInfoList;
        }
        /// <summary>
        /// 获取单个城市信息
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="city_id"></param>
        /// <returns></returns>
        public CityInfo GetCityById(string city_id)
        {
            CityInfo cityInfo = new CityInfo();
            DataSet ds = new DataSet();
            ds = cityService.GetCityById(city_id);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                cityInfo = DataTableToObject.ConvertToObject<CityInfo>(ds.Tables[0].Rows[0]);
            }
            return cityInfo;
        }

        #endregion

        #region 获取省
        /// <summary>
        /// 获取省
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <returns></returns>
        public IList<CityInfo> GetProvinceList()
        {
            IList<CityInfo> cityInfoList = new List<CityInfo>();
            DataSet ds = new DataSet();
            ds = cityService.GetProvinceList();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                cityInfoList = DataTableToObject.ConvertToList<CityInfo>(ds.Tables[0]);
            }
            return cityInfoList;
        }
        #endregion

        #region 获取市
        /// <summary>
        /// 根据省获取市
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="city_code">省号码</param>
        /// <returns></returns>
        public IList<CityInfo> GetCityListByProvince(string city_code)
        {
            IList<CityInfo> cityInfoList = new List<CityInfo>();
            DataSet ds = new DataSet();
            ds = cityService.GetCityListByProvince(city_code);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                cityInfoList = DataTableToObject.ConvertToList<CityInfo>(ds.Tables[0]);
            }
            return cityInfoList;

        }


        public SqlDataReader GetCityNameList(string customerId)
        {
            return cityService.GetCityNameList(customerId);
        }
        #endregion

        #region 获取地区
        /// <summary>
        /// 根据市获取地区
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="city_code">市号码</param>
        /// <returns></returns>
        public IList<CityInfo> GetAreaListByCity(string city_code)
        {
            IList<CityInfo> cityInfoList = new List<CityInfo>();
            DataSet ds = new DataSet();
            ds = cityService.GetAreaListByCity(city_code);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                cityInfoList = DataTableToObject.ConvertToList<CityInfo>(ds.Tables[0]);
            }
            return cityInfoList;
        }
        #endregion

        #region
        /// <summary>
        /// 根据城市号码获取城市标识
        /// </summary>
        /// <param name="city_code">城市号码</param>
        /// <returns></returns>
        public string GetCityGUIDByCityCode(string city_code)
        {
            return cityService.GetCityGUIDByCityCode(city_code);
        }
        #endregion


        #region GetCityByCityCode
        public string GetCityByCityCode(string city_code)
        {
            return cityService.GetCityByCityCode(city_code);
        }
        #endregion
    }
}
