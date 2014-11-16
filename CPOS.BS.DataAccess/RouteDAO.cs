/*
 * Author		:jun.tian
 * EMail		:jun.tian@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/1 9:34:52
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
using JIT.Utility.Reflection;

namespace JIT.CPOS.BS.DataAccess
{
    public partial class RouteDAO
    {
        #region GetRouteList
        /// <summary>
        /// 获取路线分页列表
        /// </summary>
        /// <param name="pWhereConditions"></param>
        /// <param name="pOrderBys"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagedQueryResult<RouteViewEntity> GetRouteList(RouteViewEntity entity, OrderBy[] pOrderBys, int pageIndex, int pageSize)
        {
            StringBuilder sqlWhere = new StringBuilder();
            if (entity != null 
                && entity.RouteName != null
                && !string.IsNullOrEmpty(entity.RouteName.ToString()))
            {
                sqlWhere.AppendFormat(" and A.RouteName like '%{0}%'", entity.RouteName);
            }
            if (entity != null 
                && entity.ClientStructureID != null 
                && !string.IsNullOrEmpty(entity.ClientStructureID.ToString()))
            {
                sqlWhere.AppendFormat(" and C.ClientStructureID in (select StructureID from dbo.fnGetStructure('{0}',1))", entity.ClientStructureID);
            }
            if (entity != null
                && entity.ClientUserID != null
                && Convert.ToInt32(entity.ClientUserID) > 0)
            {
                sqlWhere.AppendFormat(" and C.ClientUserID='{0}'", entity.ClientUserID);
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

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"select distinct A.*,B.ClientUserID,C.Name as UserName,D.PositionName
            from [Route] A
            inner join RouteUserMapping B on A.RouteID=B.RouteID
            INNER JOIN fnGetClientUser({2}, 1) S ON B.ClientUserID = S.ClientUserID
            left join ClientUser C on B.ClientUserID=C.ClientUserID
            left join ClientPosition D on C.ClientPositionID=D.ClientPositionID
            where A.IsDelete=0 and A.ClientID='{0}' and A.ClientDistributorID={1}" + sqlWhere.ToString(), CurrentUserInfo.ClientID, CurrentUserInfo.ClientDistributorID, CurrentUserInfo.UserID);

            //通用分页查询
            UtilityEntity model = new UtilityEntity();
            model.TableName = "(" + sql.ToString() + ") as A";
            model.PageIndex = pageIndex;
            model.PageSize = pageSize;
            model.PageSort = sqlOrder.ToString();
            new UtilityDAO(this.CurrentUserInfo).PagedQuery(model);

            //返回值
            PagedQueryResult<RouteViewEntity> pEntity = new PagedQueryResult<RouteViewEntity>();
            pEntity.RowCount = model.PageTotal;
            if (model.PageDataSet != null
                && model.PageDataSet.Tables != null
                && model.PageDataSet.Tables.Count > 0)
            {
                pEntity.Entities = DataLoader.LoadFrom<RouteViewEntity>(model.PageDataSet.Tables[0], new DirectPropertyNameMapping());
            }
            return pEntity;
        }
        #endregion

        #region DeleteRoute
        /// <summary>
        /// 删除路线
        /// </summary>
        public void DeleteRoute(Guid id, IDbTransaction pTran)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"
            update Route set isDelete=1 where RouteID='{0}' and ClientID='{1}' and ClientDistributorID={2}
            update RouteCycleMapping set isDelete=1 where RouteID='{0}' and ClientID='{1}' and ClientDistributorID={2}
            update RoutePOPMapping set isDelete=1 where RouteID='{0}' and ClientID='{1}' and ClientDistributorID={2}
            update RouteUserMapping set isDelete=1 where RouteID='{0}' and ClientID='{1}' and ClientDistributorID={2}
            ");
            //删除关联信息
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery(
                    (SqlTransaction)pTran,
                    CommandType.Text,
                    string.Format(sql.ToString(),
                    id.ToString(), 
                    CurrentUserInfo.ClientID, 
                    CurrentUserInfo.ClientDistributorID));
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(
                    sql.ToString(), 
                    id.ToString(),
                    CurrentUserInfo.ClientID,
                    CurrentUserInfo.ClientDistributorID));
            }
        }
        #endregion

        #region GenerateCallDayPlanning
        public void GenerateCallDayPlanning(Guid id,string uid, IDbTransaction pTran)
        {
            SqlParameter[] pars = new SqlParameter[] { 
                new SqlParameter("@route_id",id),
                new SqlParameter("@userids",uid)
            };
            int result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.StoredProcedure, "spCallDayPlanning", pars);
            
        }
        #endregion
    }
}