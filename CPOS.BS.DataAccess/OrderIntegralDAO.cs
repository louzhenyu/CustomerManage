/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/2/27 13:31:25
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
using JIT.CPOS.BS.DataAccess.Utility;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.Reflection;

namespace JIT.CPOS.BS.DataAccess
{

    /// <summary>
    /// 数据访问：  
    /// 表OrderIntegral的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class OrderIntegralDAO : BaseCPOSDAO, ICRUDable<OrderIntegralEntity>, IQueryable<OrderIntegralEntity>
    {

        #region GetList
        public PagedQueryResult<OrderIntegralEntity> GetList(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pageIndex, int pageSize)
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
            model.TableName = "(" + string.Format(SqlMap.SQL_GETORDERINTEGRALLIST, sqlWhere.ToString()) + ") as A";
            model.PageIndex = pageIndex;
            model.PageSize = pageSize;
            model.PageSort = sqlOrder.ToString();
            new UtilityDAO(this.CurrentUserInfo).PagedQuery(model);

            //返回值
            PagedQueryResult<OrderIntegralEntity> pEntity = new PagedQueryResult<OrderIntegralEntity>();
            pEntity.RowCount = model.PageTotal;
            if (model.PageDataSet != null
                && model.PageDataSet.Tables != null
                && model.PageDataSet.Tables.Count > 0)
            {
                pEntity.Entities = DataLoader.LoadFrom<OrderIntegralEntity>(model.PageDataSet.Tables[0]);
            }
            return pEntity;
        }


        public OrderIntegralEntity[] GetAllList(IWhereCondition[] pWhereConditions)
        {

            StringBuilder sqlWhere = new StringBuilder();
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    sqlWhere.AppendFormat(" and {0}", item.GetExpression());
                }
            }

            string sql = string.Format(@"SELECT oi.*,ti.item_name,ti.item_code,vp.VipName,vp.VipCode 
                FROM dbo.OrderIntegral oi 
                LEFT JOIN dbo.T_Item ti ON oi.ItemID=ti.item_id and ti.status='1' 
                LEFT JOIN dbo.Vip vp ON oi.VIPID=vp.VIPID and ti.status=1 
                WHERE oi.IsDelete=0 {0} ORDER BY CreateTime desc", sqlWhere.ToString());

            var ds = SQLHelper.ExecuteDataset(sql);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return DataLoader.LoadFrom<OrderIntegralEntity>(ds.Tables[0]);
            }
            return new OrderIntegralEntity[0];
        }

        #endregion

    }
}
