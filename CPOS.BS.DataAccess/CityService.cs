using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity.User;
using JIT.CPOS.BS.Entity;
using JIT.Utility;
using System.Data;
using System.Data.SqlClient;
using JIT.Utility.DataAccess;
using System.Collections;

namespace JIT.CPOS.BS.DataAccess
{
    public class CityService : Base.BaseCPOSDAO
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="loggingSessionInfo">当前的用户信息</param>
        public CityService(LoggingSessionInfo loggingSessionInfo)
            : base(loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
        }
        #endregion

        #region
        /// <summary>
        /// 获取所有城市
        /// </summary>
        /// <returns></returns>
        public DataSet GetCityInfoList(string pStoreID)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(pStoreID))
                sql = string.Format(@"select t.* from T_City t where 
                                      exists ( select 1 from t_unit a join T_City b on a.unit_city_id=b.city_id 
                                      where a.unit_id='{0}' and b.city1_name=t.city1_name and b.city2_name=t.city2_name )", pStoreID);
            else
                sql = "select * from T_City";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DistrictID"></param>
        /// <returns></returns>
        public DataSet GetSameLevelDistrictsByDistrictID(string DistrictID)
        {
            string sql = string.Empty;
            sql = string.Format(@"select *,SUBSTRING(t.city_code,1,4) from T_City t 
                                where exists(select 1 from T_City where city2_name=t.city2_name and city_id='{0}')", DistrictID);
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 获取单个城市信息
        /// </summary>
        /// <param name="city_id"></param>
        /// <returns></returns>
        public DataSet GetCityById(string city_id)
        {
            string sql = "select a.* From t_city a where a.city_id = '" + city_id + "'";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 获取省
        /// </summary>
        /// <returns></returns>
        public DataSet GetProvinceList()
        {
            string sql = "select '' city_id "
                      + " ,city1_name "
                      + " ,'' city2_name "
                      + " ,'' city3_name "
                      + " ,city_code "
                      + " ,city1_name city_name "
                      + " From ( "
                      + " select distinct city1_name,SUBSTRING(city_code,1,2) city_code From T_City) x "
                      + " order by x.city_code;";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 获取地级市
        /// </summary>
        /// <param name="city_code"></param>
        /// <returns></returns>
        public DataSet GetCityListByProvince(string city_code)
        {
            string sql = "select '' city_id "
                          + " ,'' city1_name "
                          + " ,city2_name "
                          + " ,'' city3_name "
                          + " ,city_code "
                          + " ,city2_name city_name "
                          + " From ( "
                          + " select distinct city2_name,SUBSTRING(city_code,1,4) city_code From T_City where SUBSTRING(city_code,1,2) = '" + city_code + "') x "
                          + " order by x.city_code;";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        public DataSet GetAreaListByCity(string city_code)
        {
            string sql = "select a.*,city3_name city_name From t_city a where SUBSTRING(a.city_code,1,4) = '" + city_code + "';";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        public string GetCityGUIDByCityCode(string city_code)
        {
            string sql = "select top 1 city_id From t_city where city_code = '" + city_code + "' ";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region GetCityByCityCode
        public string GetCityByCityCode(string city_code)
        {
            string sql = "";
            if (string.IsNullOrEmpty(city_code))
            {
                return "";
            }
            if (city_code.Length == 2)
            {
                sql = "select top 1 city1_name From t_city where left(city_code,2) = '" + city_code + "' ";
            }
            else if (city_code.Length == 4)
            {
                sql = "select top 1 city1_name+city2_name From t_city where left(city_code,4) = '" + city_code + "' ";
            }
            else if (city_code.Length == 6)
            {
                sql = "select top 1 city1_name+city2_name+city3_name From t_city where left(city_code,6) = '" + city_code + "' ";
            }
            string res = "";
            object result = this.SQLHelper.ExecuteScalar(sql);
            if (result != null)
            {
                res = result.ToString();
            }
            return res;
        }
        #endregion


        public SqlDataReader GetCityNameList(string customerId)
        {
            string sql = string.Format(@"select distinct city2_name CityName,left(city_code,4) as code from T_City c,t_unit u
                                                    where c.city_id = u.unit_city_id and u.type_Id = 'EB58F1B053694283B2B7610C9AAD2742' and status=1  and u.customer_id = '{0}' ", customerId);
            return this.SQLHelper.ExecuteReader(sql);
        }
    }
}
