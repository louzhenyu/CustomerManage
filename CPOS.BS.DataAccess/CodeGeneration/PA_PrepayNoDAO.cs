/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/13 20:41:21
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
using CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess.Base;
using JIT.CPOS.BS.Entity;
using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;

namespace CPOS.BS.DataAccess
{
    /// <summary>
    /// 数据访问：  
    /// 表PA_PrepayNo的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class PA_PrepayNoDAO : BaseCPOSDAO, ICRUDable<PA_PrepayNoEntity>, IQueryable<PA_PrepayNoEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public PA_PrepayNoDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(PA_PrepayNoEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(PA_PrepayNoEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");

            //初始化固定字段
            pEntity.CreateTime = DateTime.Now;


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [PA_PrepayNo](");
            strSql.Append("[OrderNo],[PrepayNo],[CreateTime],[CustomerId],[TradeType],[Field1],[Field2],[Field3],[Field4],[Field5],[OrderId])");
            strSql.Append(" values (");
            strSql.Append("@OrderNo,@PrepayNo,@CreateTime,@CustomerId,@TradeType,@Field1,@Field2,@Field3,@Field4,@Field5,@OrderId)");

            string pkString = pEntity.OrderId;

            SqlParameter[] parameters =
            {
                    new SqlParameter("@OrderNo",SqlDbType.NVarChar),
                    new SqlParameter("@PrepayNo",SqlDbType.NVarChar),
                    new SqlParameter("@CreateTime",SqlDbType.DateTime),
                    new SqlParameter("@CustomerId",SqlDbType.NVarChar),
                    new SqlParameter("@TradeType",SqlDbType.NVarChar),
                    new SqlParameter("@Field1",SqlDbType.NVarChar),
                    new SqlParameter("@Field2",SqlDbType.NVarChar),
                    new SqlParameter("@Field3",SqlDbType.NVarChar),
                    new SqlParameter("@Field4",SqlDbType.NVarChar),
                    new SqlParameter("@Field5",SqlDbType.NVarChar),
                    new SqlParameter("@OrderId",SqlDbType.NVarChar)
            };
            parameters[0].Value = pEntity.OrderNo;
            parameters[1].Value = pEntity.PrepayNo;
            parameters[2].Value = pEntity.CreateTime;
            parameters[3].Value = pEntity.CustomerId;
            parameters[4].Value = pEntity.TradeType;
            parameters[5].Value = pEntity.Field1;
            parameters[6].Value = pEntity.Field2;
            parameters[7].Value = pEntity.Field3;
            parameters[8].Value = pEntity.Field4;
            parameters[9].Value = pEntity.Field5;
            parameters[10].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            pEntity.OrderId = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public PA_PrepayNoEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [PA_PrepayNo] where OrderId='{0}'  ", id.ToString());
            //读取数据
            PA_PrepayNoEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                    break;
                }
            }
            //返回
            return m;
        }

        /// <summary>
        /// 获取所有实例
        /// </summary>
        /// <returns></returns>
        public PA_PrepayNoEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [PA_PrepayNo] where 1=1 ");
            //读取数据
            List<PA_PrepayNoEntity> list = new List<PA_PrepayNoEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    PA_PrepayNoEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //返回
            return list.ToArray();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(PA_PrepayNoEntity pEntity, IDbTransaction pTran)
        {
            Update(pEntity, pTran, true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(PA_PrepayNoEntity pEntity, IDbTransaction pTran, bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.OrderId == null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
            //初始化固定字段


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [PA_PrepayNo] set ");
            if (pIsUpdateNullField || pEntity.OrderNo != null)
                strSql.Append("[OrderNo]=@OrderNo,");
            if (pIsUpdateNullField || pEntity.PrepayNo != null)
                strSql.Append("[PrepayNo]=@PrepayNo,");
            if (pIsUpdateNullField || pEntity.CustomerId != null)
                strSql.Append("[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.TradeType != null)
                strSql.Append("[TradeType]=@TradeType,");
            if (pIsUpdateNullField || pEntity.Field1 != null)
                strSql.Append("[Field1]=@Field1,");
            if (pIsUpdateNullField || pEntity.Field2 != null)
                strSql.Append("[Field2]=@Field2,");
            if (pIsUpdateNullField || pEntity.Field3 != null)
                strSql.Append("[Field3]=@Field3,");
            if (pIsUpdateNullField || pEntity.Field4 != null)
                strSql.Append("[Field4]=@Field4,");
            if (pIsUpdateNullField || pEntity.Field5 != null)
                strSql.Append("[Field5]=@Field5");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where OrderId=@OrderId ");
            SqlParameter[] parameters =
            {
                    new SqlParameter("@OrderNo",SqlDbType.NVarChar),
                    new SqlParameter("@PrepayNo",SqlDbType.NVarChar),
                    new SqlParameter("@CustomerId",SqlDbType.NVarChar),
                    new SqlParameter("@TradeType",SqlDbType.NVarChar),
                    new SqlParameter("@Field1",SqlDbType.NVarChar),
                    new SqlParameter("@Field2",SqlDbType.NVarChar),
                    new SqlParameter("@Field3",SqlDbType.NVarChar),
                    new SqlParameter("@Field4",SqlDbType.NVarChar),
                    new SqlParameter("@Field5",SqlDbType.NVarChar),
                    new SqlParameter("@OrderId",SqlDbType.NVarChar)
            };
            parameters[0].Value = pEntity.OrderNo;
            parameters[1].Value = pEntity.PrepayNo;
            parameters[2].Value = pEntity.CustomerId;
            parameters[3].Value = pEntity.TradeType;
            parameters[4].Value = pEntity.Field1;
            parameters[5].Value = pEntity.Field2;
            parameters[6].Value = pEntity.Field3;
            parameters[7].Value = pEntity.Field4;
            parameters[8].Value = pEntity.Field5;
            parameters[9].Value = pEntity.OrderId;

            //执行语句
            int result = 0;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Update(PA_PrepayNoEntity pEntity)
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(PA_PrepayNoEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(PA_PrepayNoEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.OrderId == null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.OrderId, pTran);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pID">标识符的值</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(object pID, IDbTransaction pTran)
        {
            if (pID == null)
                return;
            //组织参数化SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [PA_PrepayNo] set  where OrderId=@OrderId;");
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter{ParameterName="@OrderId",SqlDbType=SqlDbType.VarChar,Value=pID}
            };
            //执行语句
            int result = 0;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), parameters);
            return;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(PA_PrepayNoEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (pEntity.OrderId == null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.OrderId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(PA_PrepayNoEntity[] pEntities)
        {
            Delete(pEntities, null);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pIDs">标识符值数组</param>
        public void Delete(object[] pIDs)
        {
            Delete(pIDs, null);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pIDs">标识符值数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(object[] pIDs, IDbTransaction pTran)
        {
            if (pIDs == null || pIDs.Length == 0)
                return;
            //组织参数化SQL
            StringBuilder primaryKeys = new StringBuilder();
            foreach (object item in pIDs)
            {
                primaryKeys.AppendFormat("'{0}',", item.ToString());
            }
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [PA_PrepayNo] set  where OrderId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
            //执行语句
            int result = 0;
            if (pTran == null)
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), null);
            else
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString());
        }
        #endregion

        #region IQueryable 成员
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <returns></returns>
        public PA_PrepayNoEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [PA_PrepayNo] where 1=1  ");
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    sql.AppendFormat(" and {0}", item.GetExpression());
                }
            }
            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                sql.AppendFormat(" order by ");
                foreach (var item in pOrderBys)
                {
                    sql.AppendFormat(" {0} {1},", item.FieldName, item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                }
                sql.Remove(sql.Length - 1, 1);
            }
            //执行SQL
            List<PA_PrepayNoEntity> list = new List<PA_PrepayNoEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    PA_PrepayNoEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //返回结果
            return list.ToArray();
        }
        /// <summary>
        /// 执行分页查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <param name="pPageSize">每页的记录数</param>
        /// <param name="pCurrentPageIndex">以0开始的当前页码</param>
        /// <returns></returns>
        public PagedQueryResult<PA_PrepayNoEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [OrderId] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [PA_PrepayNo] where 1=1  ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [PA_PrepayNo] where 1=1  ");
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
            PagedQueryResult<PA_PrepayNoEntity> result = new PagedQueryResult<PA_PrepayNoEntity>();
            List<PA_PrepayNoEntity> list = new List<PA_PrepayNoEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    PA_PrepayNoEntity m;
                    this.Load(rdr, out m);
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
        /// 根据实体条件查询实体
        /// </summary>
        /// <param name="pQueryEntity">以实体形式传入的参数</param>
        /// <param name="pOrderBys">排序组合</param>
        /// <returns>符合条件的实体集</returns>
        public PA_PrepayNoEntity[] QueryByEntity(PA_PrepayNoEntity pQueryEntity, OrderBy[] pOrderBys)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity(pQueryEntity);
            return Query(queryWhereCondition, pOrderBys);
        }

        /// <summary>
        /// 分页根据实体条件查询实体
        /// </summary>
        /// <param name="pQueryEntity">以实体形式传入的参数</param>
        /// <param name="pOrderBys">排序组合</param>
        /// <returns>符合条件的实体集</returns>
        public PagedQueryResult<PA_PrepayNoEntity> PagedQueryByEntity(PA_PrepayNoEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity(pQueryEntity);
            return PagedQuery(queryWhereCondition, pOrderBys, pPageSize, pCurrentPageIndex);
        }

        #endregion

        #region 工具方法
        /// <summary>
        /// 根据实体非Null属性生成查询条件。
        /// </summary>
        /// <returns></returns>
        protected IWhereCondition[] GetWhereConditionByEntity(PA_PrepayNoEntity pQueryEntity)
        {
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.OrderId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderId", Value = pQueryEntity.OrderId });
            if (pQueryEntity.OrderNo != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderNo", Value = pQueryEntity.OrderNo });
            if (pQueryEntity.PrepayNo != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PrepayNo", Value = pQueryEntity.PrepayNo });
            if (pQueryEntity.CreateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CustomerId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
            if (pQueryEntity.TradeType != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TradeType", Value = pQueryEntity.TradeType });
            if (pQueryEntity.Field1 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field1", Value = pQueryEntity.Field1 });
            if (pQueryEntity.Field2 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field2", Value = pQueryEntity.Field2 });
            if (pQueryEntity.Field3 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field3", Value = pQueryEntity.Field3 });
            if (pQueryEntity.Field4 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field4", Value = pQueryEntity.Field4 });
            if (pQueryEntity.Field5 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field5", Value = pQueryEntity.Field5 });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out PA_PrepayNoEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new PA_PrepayNoEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["OrderId"] != DBNull.Value)
            {
                pInstance.OrderId = Convert.ToString(pReader["OrderId"]);
            }
            if (pReader["OrderNo"] != DBNull.Value)
            {
                pInstance.OrderNo = Convert.ToString(pReader["OrderNo"]);
            }
            if (pReader["PrepayNo"] != DBNull.Value)
            {
                pInstance.PrepayNo = Convert.ToString(pReader["PrepayNo"]);
            }
            if (pReader["CreateTime"] != DBNull.Value)
            {
                pInstance.CreateTime = Convert.ToDateTime(pReader["CreateTime"]);
            }
            if (pReader["CustomerId"] != DBNull.Value)
            {
                pInstance.CustomerId = Convert.ToString(pReader["CustomerId"]);
            }
            if (pReader["TradeType"] != DBNull.Value)
            {
                pInstance.TradeType = Convert.ToString(pReader["TradeType"]);
            }
            if (pReader["Field1"] != DBNull.Value)
            {
                pInstance.Field1 = Convert.ToString(pReader["Field1"]);
            }
            if (pReader["Field2"] != DBNull.Value)
            {
                pInstance.Field2 = Convert.ToString(pReader["Field2"]);
            }
            if (pReader["Field3"] != DBNull.Value)
            {
                pInstance.Field3 = Convert.ToString(pReader["Field3"]);
            }
            if (pReader["Field4"] != DBNull.Value)
            {
                pInstance.Field4 = Convert.ToString(pReader["Field4"]);
            }
            if (pReader["Field5"] != DBNull.Value)
            {
                pInstance.Field5 = Convert.ToString(pReader["Field5"]);
            }

        }
        #endregion
    }
}
