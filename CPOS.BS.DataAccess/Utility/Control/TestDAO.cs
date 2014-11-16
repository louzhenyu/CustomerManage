/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2012/12/5 13:56:28
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
using System.Data;
using System.Data.SqlClient;
using System.Text;


using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;

using JIT.Utility.Log;
using JIT.CPOS.BS.DataAccess.Base;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Utility;

namespace JIT.CPOS.BS.DAL
{
    /// <summary>
    /// 表OperationLog的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class TestDAO : BaseCPOSDAO
    {
        protected BasicTenantUserInfo CurrentUserInfo;  
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TestDAO(BasicTenantUserInfo pUserInfo)
            : base(pUserInfo, true)
        {
            this.CurrentUserInfo = pUserInfo;
        }
        #endregion

        #region GetList
        /// <summary>
        /// 公用的GetList方法
        /// </summary>
        /// <param name="pageIndex">当前页面</param>
        /// <param name="pageSize">页面显示数</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strOrder">排序条件</param>
        /// <param name="tableName">表名或查询的sql语句</param>
        /// <param name="RowCount">out 返回的总数量</param>
        /// <returns>DataSet</returns>
        public DataSet GetList(int pageIndex, int pageSize, string strWhere, string strOrder, string tableName, out int RowCount)
        {
            UtilityEntity model = new UtilityEntity();
            if (!string.IsNullOrEmpty(tableName) && tableName.Length < 30 && tableName.Trim().IndexOf(' ') < 0)
            {
                model.TableName = tableName;
            }
            else
            {
                model.TableName = "(" + tableName + ") as A";
            }

            model.PageIndex = pageIndex;
            model.PageSize = pageSize;
            model.PageWhere = strWhere;
            model.PageSort = strOrder;
            new UtilityDAO(this.CurrentUserInfo).PagedQuery(model);
            RowCount = 0;
            DataSet ds = null;
            if (model != null)
            {
                RowCount = model.PageTotal;
                ds = model.PageDataSet;
            }
            return ds;
        }
        #endregion

        #region NonQuery
        /// <summary>
        /// 根据SQl语句返回受影响行数
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="Params">参数(数组)</param>
        /// <returns>int</returns>
        public int? NonQuery(string sql, IDbTransaction pTran)
        {
            UtilityEntity model = new UtilityEntity();
            model.CustomSql = sql;
            new UtilityDAO(this.CurrentUserInfo).Query(model, null);
            return model.OpResultID;
        }
        #endregion

    }
}
