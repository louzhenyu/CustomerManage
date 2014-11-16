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
    /// 类型服务
    /// </summary>
    public class TypeService : BaseService
    {
        JIT.CPOS.BS.DataAccess.TypeService typeService = null;
        #region 构造函数
        public TypeService(LoggingSessionInfo loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
            typeService = new DataAccess.TypeService(loggingSessionInfo);
        }
        #endregion

        #region 获取某个域的类型集合
        /// <summary>
        /// 获取某个域的类型集合
        /// </summary>
        /// <param name="loggingSessionInfo">登录信息</param>
        /// <param name="type_domaion">域</param>
        /// <returns></returns>
        public IList<TypeInfo> GetTypeInfoListByDomain(string type_domaion)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("type_domian", type_domaion);
            //return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<TypeInfo>("Type.SelectByDomain", _ht);
            IList<TypeInfo> typeInfoList = new List<TypeInfo>();
            DataSet ds = new DataSet();
            ds = typeService.GetTypeInfoListByDomain(type_domaion);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                typeInfoList = DataTableToObject.ConvertToList<TypeInfo>(ds.Tables[0]);
            }
            return typeInfoList;
        }
        #endregion

        #region 获取单个类型信息
        /// <summary>
        /// 获取单个类型信息
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="type_id"></param>
        /// <returns></returns>
        public TypeInfo GetTypeInfoById(string type_id)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("Type_Id", type_id);
            //return (TypeInfo)cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject("Type.SelectById", _ht);
            TypeInfo typeInfo = new TypeInfo();
            DataSet ds = new DataSet();
            ds = typeService.GetTypeInfoById(type_id);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                typeInfo = DataTableToObject.ConvertToObject<TypeInfo>(ds.Tables[0].Rows[0]);
            }
            return typeInfo;
        }
        #endregion
    }
}
