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
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// 数据访问：  
    /// 表TInoutStatus的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class TInoutStatusDAO : BaseCPOSDAO, ICRUDable<TInoutStatusEntity>, IQueryable<TInoutStatusEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TInoutStatusDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo, true)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(TInoutStatusEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(TInoutStatusEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [TInoutStatus](");
            strSql.Append("[OrderID],[OrderStatus],[CheckResult],[PayMethod],[DeliverCompanyID],[DeliverOrder],[StatusRemark],[PicUrl],[Remark],[CustomerID],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[InoutStatusID])");
            strSql.Append(" values (");
            strSql.Append("@OrderID,@OrderStatus,@CheckResult,@PayMethod,@DeliverCompanyID,@DeliverOrder,@StatusRemark,@PicUrl,@Remark,@CustomerID,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@InoutStatusID)");

            Guid? pkGuid;
            if (pEntity.InoutStatusID == null)
                pkGuid = Guid.NewGuid();
            else
                pkGuid = pEntity.InoutStatusID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@OrderID",SqlDbType.NVarChar,100),
					new SqlParameter("@OrderStatus",SqlDbType.Int),
					new SqlParameter("@CheckResult",SqlDbType.Int),
					new SqlParameter("@PayMethod",SqlDbType.Int),
					new SqlParameter("@DeliverCompanyID",SqlDbType.NVarChar,100),
					new SqlParameter("@DeliverOrder",SqlDbType.NVarChar,200),
					new SqlParameter("@StatusRemark",SqlDbType.NVarChar,1000),
					new SqlParameter("@PicUrl",SqlDbType.NVarChar,1000),
					new SqlParameter("@Remark",SqlDbType.NVarChar,1000),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar,100),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar,100),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar,100),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@InoutStatusID",SqlDbType.UniqueIdentifier,16)
            };
            parameters[0].Value = pEntity.OrderID;
            parameters[1].Value = pEntity.OrderStatus;
            parameters[2].Value = pEntity.CheckResult;
            parameters[3].Value = pEntity.PayMethod;
            parameters[4].Value = pEntity.DeliverCompanyID;
            parameters[5].Value = pEntity.DeliverOrder;
            parameters[6].Value = pEntity.StatusRemark;
            parameters[7].Value = pEntity.PicUrl;
            parameters[8].Value = pEntity.Remark;
            parameters[9].Value = pEntity.CustomerID;
            parameters[10].Value = pEntity.CreateBy;
            parameters[11].Value = pEntity.CreateTime;
            parameters[12].Value = pEntity.LastUpdateBy;
            parameters[13].Value = pEntity.LastUpdateTime;
            parameters[14].Value = pEntity.IsDelete;
            parameters[15].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            pEntity.InoutStatusID = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public TInoutStatusEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [TInoutStatus] where InoutStatusID='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            TInoutStatusEntity m = null;
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
        public TInoutStatusEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [TInoutStatus] where isdelete=0 and CustomerID='" + CurrentUserInfo.CurrentLoggingManager.Customer_Id + "'");
            //读取数据
            List<TInoutStatusEntity> list = new List<TInoutStatusEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    TInoutStatusEntity m;
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
        public void Update(TInoutStatusEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.InoutStatusID.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
            //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [TInoutStatus] set ");
            strSql.Append("[OrderID]=@OrderID,[OrderStatus]=@OrderStatus,[CheckResult]=@CheckResult,[PayMethod]=@PayMethod,[DeliverCompanyID]=@DeliverCompanyID,[DeliverOrder]=@DeliverOrder,[StatusRemark]=@StatusRemark,[PicUrl]=@PicUrl,[Remark]=@Remark,[CustomerID]=@CustomerID,[LastUpdateBy]=@LastUpdateBy,[LastUpdateTime]=@LastUpdateTime");
            strSql.Append(" where InoutStatusID=@InoutStatusID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@OrderID",SqlDbType.NVarChar,100),
					new SqlParameter("@OrderStatus",SqlDbType.Int),
					new SqlParameter("@CheckResult",SqlDbType.Int),
					new SqlParameter("@PayMethod",SqlDbType.Int),
					new SqlParameter("@DeliverCompanyID",SqlDbType.NVarChar,100),
					new SqlParameter("@DeliverOrder",SqlDbType.NVarChar,200),
					new SqlParameter("@StatusRemark",SqlDbType.NVarChar,1000),
					new SqlParameter("@PicUrl",SqlDbType.NVarChar,1000),
					new SqlParameter("@Remark",SqlDbType.NVarChar,1000),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar,100),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar,100),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@InoutStatusID",SqlDbType.UniqueIdentifier,16)
            };
            parameters[0].Value = pEntity.OrderID;
            parameters[1].Value = pEntity.OrderStatus;
            parameters[2].Value = pEntity.CheckResult;
            parameters[3].Value = pEntity.PayMethod;
            parameters[4].Value = pEntity.DeliverCompanyID;
            parameters[5].Value = pEntity.DeliverOrder;
            parameters[6].Value = pEntity.StatusRemark;
            parameters[7].Value = pEntity.PicUrl;
            parameters[8].Value = pEntity.Remark;
            parameters[9].Value = pEntity.CustomerID;
            parameters[10].Value = pEntity.LastUpdateBy;
            parameters[11].Value = pEntity.LastUpdateTime;
            parameters[12].Value = pEntity.InoutStatusID;

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
        public void Update(TInoutStatusEntity pEntity)
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(TInoutStatusEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(TInoutStatusEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.InoutStatusID.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.InoutStatusID.Value, pTran);
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
            sql.AppendLine("update [TInoutStatus] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where InoutStatusID=@InoutStatusID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.NVarChar,Value=CurrentUserInfo.UserID},
                new SqlParameter{ParameterName="@InoutStatusID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(TInoutStatusEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (!item.InoutStatusID.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.InoutStatusID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(TInoutStatusEntity[] pEntities)
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
            sql.AppendLine("update [TInoutStatus] set LastUpdateTime='" + DateTime.Now.ToString() + "',LastUpdateBy='" + CurrentUserInfo.UserID + "',IsDelete=1 where InoutStatusID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public TInoutStatusEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [TInoutStatus] where isdelete=0 and CustomerID='" + CurrentUserInfo.CurrentLoggingManager.Customer_Id + "' ");
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
            List<TInoutStatusEntity> list = new List<TInoutStatusEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    TInoutStatusEntity m;
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
        public PagedQueryResult<TInoutStatusEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [InoutStatusID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [TInoutStatus] where isdelete=0 and CustomerID='" + CurrentUserInfo.CurrentLoggingManager.Customer_Id + "' ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [TInoutStatus] where isdelete=0 and CustomerID='" + CurrentUserInfo.CurrentLoggingManager.Customer_Id + "' ");
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
            PagedQueryResult<TInoutStatusEntity> result = new PagedQueryResult<TInoutStatusEntity>();
            List<TInoutStatusEntity> list = new List<TInoutStatusEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    TInoutStatusEntity m;
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
        public TInoutStatusEntity[] QueryByEntity(TInoutStatusEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<TInoutStatusEntity> PagedQueryByEntity(TInoutStatusEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(TInoutStatusEntity pQueryEntity)
        {
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.InoutStatusID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "InoutStatusID", Value = pQueryEntity.InoutStatusID });
            if (pQueryEntity.OrderID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderID", Value = pQueryEntity.OrderID });
            if (pQueryEntity.OrderStatus != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderStatus", Value = pQueryEntity.OrderStatus });
            if (pQueryEntity.CheckResult != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CheckResult", Value = pQueryEntity.CheckResult });
            if (pQueryEntity.PayMethod != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PayMethod", Value = pQueryEntity.PayMethod });
            if (pQueryEntity.DeliverCompanyID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DeliverCompanyID", Value = pQueryEntity.DeliverCompanyID });
            if (pQueryEntity.DeliverOrder != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DeliverOrder", Value = pQueryEntity.DeliverOrder });
            if (pQueryEntity.StatusRemark != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StatusRemark", Value = pQueryEntity.StatusRemark });
            if (pQueryEntity.PicUrl != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PicUrl", Value = pQueryEntity.PicUrl });
            if (pQueryEntity.Remark != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Remark", Value = pQueryEntity.Remark });
            if (pQueryEntity.CustomerID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });
            if (pQueryEntity.CreateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.CreateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.LastUpdateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.LastUpdateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.IsDelete != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out TInoutStatusEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new TInoutStatusEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["InoutStatusID"] != DBNull.Value)
            {
                pInstance.InoutStatusID = (Guid)pReader["InoutStatusID"];
            }
            if (pReader["OrderID"] != DBNull.Value)
            {
                pInstance.OrderID = Convert.ToString(pReader["OrderID"]);
            }
            if (pReader["OrderStatus"] != DBNull.Value)
            {
                pInstance.OrderStatus = Convert.ToInt32(pReader["OrderStatus"]);
            }
            if (pReader["CheckResult"] != DBNull.Value)
            {
                pInstance.CheckResult = Convert.ToInt32(pReader["CheckResult"]);
            }
            if (pReader["PayMethod"] != DBNull.Value)
            {
                pInstance.PayMethod = Convert.ToInt32(pReader["PayMethod"]);
            }
            if (pReader["DeliverCompanyID"] != DBNull.Value)
            {
                pInstance.DeliverCompanyID = Convert.ToString(pReader["DeliverCompanyID"]);
            }
            if (pReader["DeliverOrder"] != DBNull.Value)
            {
                pInstance.DeliverOrder = Convert.ToString(pReader["DeliverOrder"]);
            }
            if (pReader["StatusRemark"] != DBNull.Value)
            {
                pInstance.StatusRemark = Convert.ToString(pReader["StatusRemark"]);
            }
            if (pReader["PicUrl"] != DBNull.Value)
            {
                pInstance.PicUrl = Convert.ToString(pReader["PicUrl"]);
            }
            if (pReader["Remark"] != DBNull.Value)
            {
                pInstance.Remark = Convert.ToString(pReader["Remark"]);
            }
            if (pReader["CustomerID"] != DBNull.Value)
            {
                pInstance.CustomerID = Convert.ToString(pReader["CustomerID"]);
            }
            if (pReader["CreateBy"] != DBNull.Value)
            {
                pInstance.CreateBy = Convert.ToString(pReader["CreateBy"]);
            }
            if (pReader["CreateTime"] != DBNull.Value)
            {
                pInstance.CreateTime = Convert.ToDateTime(pReader["CreateTime"]);
            }
            if (pReader["LastUpdateBy"] != DBNull.Value)
            {
                pInstance.LastUpdateBy = Convert.ToString(pReader["LastUpdateBy"]);
            }
            if (pReader["LastUpdateTime"] != DBNull.Value)
            {
                pInstance.LastUpdateTime = Convert.ToDateTime(pReader["LastUpdateTime"]);
            }
            if (pReader["IsDelete"] != DBNull.Value)
            {
                pInstance.IsDelete = Convert.ToInt32(pReader["IsDelete"]);
            }

        }
        #endregion
    }
}
