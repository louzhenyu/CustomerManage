/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/25 13:51:19
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
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{

    /// <summary>
    /// 数据访问：  
    /// 表PanicbuyingEvent的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class PanicbuyingEventDAO : Base.BaseCPOSDAO, ICRUDable<PanicbuyingEventEntity>, IQueryable<PanicbuyingEventEntity>
    {
        /// <summary>
        /// 创建活动
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public string AddPanicbuyingEvent(PanicbuyingEventEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");

            //初始化固定字段
            pEntity.CreateTime = DateTime.Now;
            pEntity.CreateBy = CurrentUserInfo.UserID;
            pEntity.LastUpdateTime = pEntity.CreateTime;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;
            pEntity.IsDelete = 0;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [PanicbuyingEvent](");
            strSql.Append("[EventName],[EventTypeId],[BeginTime],[EndTime],[EventRemark],[CustomerID],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[EventStatus],[EventId])");
            strSql.Append(" values (");
            strSql.Append("@EventName,@EventTypeId,@BeginTime,@EndTime,@EventRemark,@CustomerID,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@EventStatus,@EventId)");

            Guid? pkGuid;
            if (pEntity.EventId == null)
                pkGuid = Guid.NewGuid();
            else
                pkGuid = pEntity.EventId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@EventName",SqlDbType.NVarChar),
					new SqlParameter("@EventTypeId",SqlDbType.Int),
					new SqlParameter("@BeginTime",SqlDbType.DateTime),
					new SqlParameter("@EndTime",SqlDbType.DateTime),
					new SqlParameter("@EventRemark",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@EventStatus",SqlDbType.Int),
					new SqlParameter("@EventId",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.EventName;
            parameters[1].Value = pEntity.EventTypeId;
            parameters[2].Value = pEntity.BeginTime;
            parameters[3].Value = pEntity.EndTime;
            parameters[4].Value = pEntity.EventRemark;
            parameters[5].Value = pEntity.CustomerID;
            parameters[6].Value = pEntity.CreateTime;
            parameters[7].Value = pEntity.CreateBy;
            parameters[8].Value = pEntity.LastUpdateBy;
            parameters[9].Value = pEntity.LastUpdateTime;
            parameters[10].Value = pEntity.IsDelete;
            parameters[11].Value = pEntity.EventStatus;
            parameters[12].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            pEntity.EventId = pkGuid;
            return pkGuid.ToString();
        }

        /// <summary>
        /// 获取活动列表
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <param name="pPageSize">每页的记录数</param>
        /// <param name="pCurrentPageIndex">以0开始的当前页码</param>
        /// <returns></returns>
        public PagedQueryResult<PanicbuyingEventEntity> GetPanicbuyingEvent(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            //组织SQL
            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();
            //分页SQL
            pagedSql.AppendFormat("select * from (select row_number()over( order by ");
            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                foreach (var item in pOrderBys)
                {
                    if (item != null)
                    {
                        pagedSql.AppendFormat(" {0} {1},", StringUtils.WrapperSQLServerObject(item.FieldName), item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                    }
                }
                pagedSql.Remove(pagedSql.Length - 1, 1);
            }
            else
            {
                pagedSql.AppendFormat(" [EventId] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from VwPanicBuyingEvent where 1=1 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from VwPanicBuyingEvent where 1=1 ");
            //过滤条件
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    if (item != null)
                    {
                        pagedSql.AppendFormat(" and {0}", item.GetExpression());
                        totalCountSql.AppendFormat(" and {0}", item.GetExpression());
                    }
                }
            }
            pagedSql.AppendFormat(") as A ");
            //取指定页的数据
            pagedSql.AppendFormat(" where ___rn >{0} and ___rn <={1}", pPageSize * (pCurrentPageIndex - 1), pPageSize * (pCurrentPageIndex));
            //执行语句并返回结果
            PagedQueryResult<PanicbuyingEventEntity> result = new PagedQueryResult<PanicbuyingEventEntity>();
            List<PanicbuyingEventEntity> list = new List<PanicbuyingEventEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    PanicbuyingEventEntity m;
                    this.LoadEvent(rdr, out m);
                    list.Add(m);
                }
            }
            result.Entities = list.ToArray();
            int totalCount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(totalCountSql.ToString()));    //计算总行数
            result.RowCount = totalCount;
            int remainder = 0;
            result.PageCount = Math.DivRem(totalCount, pPageSize, out remainder);
            if (remainder > 0)
                result.PageCount++;
            return result;
        }

        public PagedQueryResult<PanicbuyingEventEntity> GetPanicbuyingEventList(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            //组织SQL
            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();
            //分页SQL
            pagedSql.AppendFormat("select * from (select row_number()over( order by ");
            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                foreach (var item in pOrderBys)
                {
                    if (item != null)
                    {
                        pagedSql.AppendFormat(" {0} {1},", StringUtils.WrapperSQLServerObject(item.FieldName), item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                    }
                }
                pagedSql.Remove(pagedSql.Length - 1, 1);
            }
            else
            {
                pagedSql.AppendFormat(" [EventId] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [VwPanicBuyingEvent] WITH(NOLOCK) where 1=1 and EventStatus='已上架'");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [VwPanicBuyingEvent] WITH(NOLOCK) where 1=1  and EventStatus='已上架' ");
            //过滤条件
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    if (item != null)
                    {
                        pagedSql.AppendFormat(" and {0}", item.GetExpression());
                        totalCountSql.AppendFormat(" and {0}", item.GetExpression());
                    }
                }
            }
            pagedSql.AppendFormat(") as A ");
            //取指定页的数据
            pagedSql.AppendFormat(" where ___rn >{0} and ___rn <={1}", pPageSize * (pCurrentPageIndex - 1), pPageSize * (pCurrentPageIndex));
            //执行语句并返回结果
            PagedQueryResult<PanicbuyingEventEntity> result = new PagedQueryResult<PanicbuyingEventEntity>();
            List<PanicbuyingEventEntity> list = new List<PanicbuyingEventEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    PanicbuyingEventEntity m;
                    this.LoadPanicbuyingEvent(rdr, out m);
                    list.Add(m);
                }
            }
            result.Entities = list.ToArray();
            int totalCount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(totalCountSql.ToString()));    //计算总行数
            result.RowCount = totalCount;
            int remainder = 0;
            result.PageCount = Math.DivRem(totalCount, pPageSize, out remainder);
            if (remainder > 0)
                result.PageCount++;
            return result;
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void LoadEvent(SqlDataReader pReader, out PanicbuyingEventEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new PanicbuyingEventEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["EventId"] != DBNull.Value)
            {
                pInstance.EventId = (Guid)pReader["EventId"];
            }
            if (pReader["EventName"] != DBNull.Value)
            {
                pInstance.EventName = Convert.ToString(pReader["EventName"]);
            }
            if (pReader["EventTypeId"] != DBNull.Value)
            {
                pInstance.EventTypeId = Convert.ToInt32(pReader["EventTypeId"]);
            }
            if (pReader["BeginTime"] != DBNull.Value)
            {
                pInstance.BeginTime = Convert.ToDateTime(pReader["BeginTime"]);
            }
            if (pReader["EndTime"] != DBNull.Value)
            {
                pInstance.EndTime = Convert.ToDateTime(pReader["EndTime"]);
            }
            if (pReader["CustomerID"] != DBNull.Value)
            {
                pInstance.CustomerID = Convert.ToString(pReader["CustomerID"]);
            }

            if (pReader["EventStatus"] != DBNull.Value)
            {
                pInstance.EventStatusStr = pReader["EventStatus"].ToString();
            }
            if (pReader["Qty"] != DBNull.Value)
            {
                pInstance.Qty = Convert.ToInt32(pReader["Qty"].ToString());
            }
            if (pReader["RemainQty"] != DBNull.Value)
            {
                pInstance.RemainQty = Convert.ToInt32(pReader["RemainQty"].ToString());
            }
        }
        protected void LoadPanicbuyingEvent(SqlDataReader pReader, out PanicbuyingEventEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new PanicbuyingEventEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["EventId"] != DBNull.Value)
            {
                pInstance.EventId = (Guid)pReader["EventId"];
            }
            if (pReader["EventName"] != DBNull.Value)
            {
                pInstance.EventName = Convert.ToString(pReader["EventName"]);
            }
            if (pReader["EventTypeId"] != DBNull.Value)
            {
                pInstance.EventTypeId = Convert.ToInt32(pReader["EventTypeId"]);
            }
            if (pReader["BeginTime"] != DBNull.Value)
            {
                pInstance.BeginTime = Convert.ToDateTime(pReader["BeginTime"]);
            }
            if (pReader["EndTime"] != DBNull.Value)
            {
                pInstance.EndTime = Convert.ToDateTime(pReader["EndTime"]);
            }
            if (pReader["CustomerID"] != DBNull.Value)
            {
                pInstance.CustomerID = Convert.ToString(pReader["CustomerID"]);
            }

            if (pReader["EventStatus"] != DBNull.Value)
            {
                pInstance.EventStatusStr = pReader["EventStatus"].ToString();
            }
  
        }
         public DataSet GetPanicbuyingEvent(string pEvenid)
        {
            string str = "select BeginTime,EndTime  from PanicbuyingEvent where EventId='" + pEvenid + "'";
            return this.SQLHelper.ExecuteDataset(str);
        
        }

        /// <summary>
        /// 获取活动详情
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public PanicbuyingEventEntity GetPanicbuyingEventDetails(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"select * from [PanicbuyingEvent] p
                                        inner join VwPanicBuyingEvent vwp on vwp.EventId=p.EventId
                                        where p.EventId='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            PanicbuyingEventEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                    if (rdr["Qty"] != DBNull.Value)
                    {
                        m.Qty = Convert.ToInt32(rdr["Qty"]);
                    }
                    if (rdr["RemainQty"] != DBNull.Value)
                    {
                        m.RemainQty = Convert.ToInt32(rdr["RemainQty"]);
                    }
                    break;
                }
            }
            //返回
            return m;
        }
    }
}
