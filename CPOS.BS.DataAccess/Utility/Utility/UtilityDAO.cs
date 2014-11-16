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
using JIT.Utility.Reflection;

namespace JIT.CPOS.BS.DataAccess.Utility
{

    /// <summary>
    /// 表OperationLog的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class UtilityDAO : BaseCPOSDAO
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public UtilityDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo, true)
        {
        }
        #endregion

        //#region 修改，删除功能
        ///// <summary>
        ///// 修改状态，删除数据使用
        ///// </summary>
        ///// <param name="pEntity">实体实例</param>   
        //public void Update(UtilityEntity pEntity)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("update " + pEntity.TableName + " set ");
        //    strSql.Append(pEntity.UptField + "=" + pEntity.UptValue);
        //    strSql.Append(" where " + pEntity.UptWhereField + " in (" + pEntity.UptWhereValue + ")");
        //    int result = 0;
        //    result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
        //    pEntity.ResultNum = result;
        //}
        //#endregion

        //#region 清空功能
        ///// <summary>
        ///// 清空数据使用
        ///// </summary>
        ///// <param name="pEntity">实体实例</param>  
        //public void Delete(UtilityEntity pEntity)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append(" delete " + pEntity.TableName);
        //    strSql.Append(" where " + pEntity.DeleteField + " in (" + pEntity.DeleteValue + ")");
        //    int result = 0;
        //    result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
        //    pEntity.ResultNum = result;
        //}
        //#endregion

        #region 分页查询
        public void PagedQuery(UtilityEntity pEntity)
        {
            //组织SQL
            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();
            //分页SQL
            pagedSql.AppendFormat("select ");
            pagedSql.AppendFormat(" * from ( select row_number()over( order by ");
            if (pEntity.PageSort != null && pEntity.PageSort != "")
            {
                pagedSql.AppendFormat(" " + pEntity.PageSort + " )as ID,");
            }
            else
            {
                pagedSql.AppendFormat(" isdelete asc ) as ID,");
            }
            pagedSql.AppendFormat(" * from " + pEntity.TableName + "   where isdelete=0  ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from " + pEntity.TableName + "  where isdelete=0 ");
            //过滤条件
            if (pEntity.PageWhere != null && pEntity.PageWhere != "")
            {
                pagedSql.AppendFormat(pEntity.PageWhere);
                totalCountSql.AppendFormat(pEntity.PageWhere);
            }
            //取指定页的数据

            pagedSql.AppendFormat(" ) as ABCD where ID>{0} and ID<={1}", pEntity.PageSize * (pEntity.PageIndex - 1), pEntity.PageSize * (pEntity.PageIndex));

            pEntity.ResultError = pagedSql.ToString();
            //执行语句并返回结果    
            using (DataSet ds = this.SQLHelper.ExecuteDataset(pagedSql.ToString()))
            {
                int totalCount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(totalCountSql.ToString()));    //计算总行数
                pEntity.PageTotal = totalCount;
                int remainder = 0;
                pEntity.PageCount = Math.DivRem(totalCount, pEntity.PageSize, out remainder);
                if (remainder > 0)
                {
                    pEntity.PageCount++;
                }
                pEntity.PageDataSet = ds;
            }
        }
        #endregion

        #region 自定义SQL查询
        public void GetAll(UtilityEntity pEntity)
        {
            //执行语句并返回结果    
            using (DataSet ds = this.SQLHelper.ExecuteDataset(pEntity.CustomSql.ToString()))
            {
                pEntity.PageDataSet = ds;
            }
        }

        /// <summary>
        /// 返回影响行数
        /// </summary>
        /// <param name="pEntity"></param>
        public void Query(UtilityEntity pEntity, IDbTransaction pTran)
        {
            int result = 0;
            if (pTran != null)
            {
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, pEntity.CustomSql.ToString(), null);
            }
            else
            {
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, pEntity.CustomSql.ToString(), null);
            }
            pEntity.OpResultID = result;
        }

        public void GetScalar(UtilityEntity pEntity, IDbTransaction pTran)
        {
            int result = 0;
            object obj = null;
            if (pTran != null)
            {
                obj = this.SQLHelper.ExecuteScalar((SqlTransaction)pTran, CommandType.Text, pEntity.CustomSql.ToString(), null);
            }
            else
            {
                obj = this.SQLHelper.ExecuteScalar(CommandType.Text, pEntity.CustomSql.ToString(), null);
            }
            if (obj != null)
            {
                result = int.Parse(obj.ToString());
            }
            pEntity.OpResultID = result;
        }
        #endregion

        #region 通用分页查询
        public PagedQueryResult<T> GetList<T>(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, string sql, int pageIndex, int pageSize) where T : class,IEntity, new()
        {
            StringBuilder sqlWhere = new StringBuilder();
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    sqlWhere.AppendFormat(" and {0}", item.GetExpression());
                }
            }
            StringBuilder sqlOrder = new StringBuilder();
            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                foreach (var item in pOrderBys)
                {
                    sqlOrder.AppendFormat(" {0} {1},", item.FieldName, item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                }
                sqlOrder.Remove(sqlOrder.Length - 1, 1);
            }

            //通用分页查询
            UtilityEntity model = new UtilityEntity();
            model.TableName = "(" + string.Format(sql, CurrentUserInfo.ClientID, CurrentUserInfo.ClientDistributorID) + sqlWhere.ToString() + ") as A";
            model.PageIndex = pageIndex;
            model.PageSize = pageSize;
            model.PageSort = sqlOrder.ToString();
            new UtilityDAO(this.CurrentUserInfo).PagedQuery(model);

            //返回值
            PagedQueryResult<T> pEntity = new PagedQueryResult<T>();
            pEntity.RowCount = model.PageTotal;
            if (model.PageDataSet != null
                && model.PageDataSet.Tables != null
                && model.PageDataSet.Tables.Count > 0)
            {
                pEntity.Entities = DataLoader.LoadFrom<T>(model.PageDataSet.Tables[0]);
            }
            return pEntity;
        }
        #endregion
    }
}
