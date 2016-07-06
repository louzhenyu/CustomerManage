/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/24 14:53:08
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
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// 数据访问：  
    /// 表T_SuperRetailTrader的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_SuperRetailTraderDAO : Base.BaseCPOSDAO, ICRUDable<T_SuperRetailTraderEntity>, IQueryable<T_SuperRetailTraderEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_SuperRetailTraderDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(T_SuperRetailTraderEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(T_SuperRetailTraderEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //初始化固定字段
			pEntity.IsDelete=0;
			pEntity.CreateTime=DateTime.Now;
			pEntity.LastUpdateTime=pEntity.CreateTime;
			pEntity.CreateBy=CurrentUserInfo.UserID;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [T_SuperRetailTrader](");
            strSql.Append("[SuperRetailTraderCode],[SuperRetailTraderName],[SuperRetailTraderLogin],[SuperRetailTraderPass],[SuperRetailTraderMan],[SuperRetailTraderPhone],[SuperRetailTraderAddress],[SuperRetailTraderFrom],[SuperRetailTraderFromId],[HigheSuperRetailTraderID],[JoinTime],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[CustomerId],[Status],[SuperRetailTraderID])");
            strSql.Append(" values (");
            strSql.Append("@SuperRetailTraderCode,@SuperRetailTraderName,@SuperRetailTraderLogin,@SuperRetailTraderPass,@SuperRetailTraderMan,@SuperRetailTraderPhone,@SuperRetailTraderAddress,@SuperRetailTraderFrom,@SuperRetailTraderFromId,@HigheSuperRetailTraderID,@JoinTime,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@CustomerId,@Status,@SuperRetailTraderID)");            

			Guid? pkGuid;
			if (pEntity.SuperRetailTraderID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.SuperRetailTraderID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@SuperRetailTraderCode",SqlDbType.VarChar),
					new SqlParameter("@SuperRetailTraderName",SqlDbType.NVarChar),
					new SqlParameter("@SuperRetailTraderLogin",SqlDbType.NVarChar),
					new SqlParameter("@SuperRetailTraderPass",SqlDbType.NVarChar),
					new SqlParameter("@SuperRetailTraderMan",SqlDbType.NVarChar),
					new SqlParameter("@SuperRetailTraderPhone",SqlDbType.VarChar),
					new SqlParameter("@SuperRetailTraderAddress",SqlDbType.NVarChar),
					new SqlParameter("@SuperRetailTraderFrom",SqlDbType.VarChar),
					new SqlParameter("@SuperRetailTraderFromId",SqlDbType.NVarChar),
					new SqlParameter("@HigheSuperRetailTraderID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@JoinTime",SqlDbType.DateTime),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.NVarChar),
					new SqlParameter("@SuperRetailTraderID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.SuperRetailTraderCode;
			parameters[1].Value = pEntity.SuperRetailTraderName;
			parameters[2].Value = pEntity.SuperRetailTraderLogin;
			parameters[3].Value = pEntity.SuperRetailTraderPass;
			parameters[4].Value = pEntity.SuperRetailTraderMan;
			parameters[5].Value = pEntity.SuperRetailTraderPhone;
			parameters[6].Value = pEntity.SuperRetailTraderAddress;
			parameters[7].Value = pEntity.SuperRetailTraderFrom;
			parameters[8].Value = pEntity.SuperRetailTraderFromId;
			parameters[9].Value = pEntity.HigheSuperRetailTraderID;
			parameters[10].Value = pEntity.JoinTime;
			parameters[11].Value = pEntity.CreateTime;
			parameters[12].Value = pEntity.CreateBy;
			parameters[13].Value = pEntity.LastUpdateBy;
			parameters[14].Value = pEntity.LastUpdateTime;
			parameters[15].Value = pEntity.IsDelete;
			parameters[16].Value = pEntity.CustomerId;
			parameters[17].Value = pEntity.Status;
			parameters[18].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.SuperRetailTraderID = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public T_SuperRetailTraderEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_SuperRetailTrader] where SuperRetailTraderID='{0}'  and isdelete=0 ", id.ToString());
            //读取数据
            T_SuperRetailTraderEntity m = null;
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
        public T_SuperRetailTraderEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_SuperRetailTrader] where 1=1  and isdelete=0");
            //读取数据
            List<T_SuperRetailTraderEntity> list = new List<T_SuperRetailTraderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_SuperRetailTraderEntity m;
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
        public void Update(T_SuperRetailTraderEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(T_SuperRetailTraderEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.SuperRetailTraderID.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_SuperRetailTrader] set ");
                        if (pIsUpdateNullField || pEntity.SuperRetailTraderCode!=null)
                strSql.Append( "[SuperRetailTraderCode]=@SuperRetailTraderCode,");
            if (pIsUpdateNullField || pEntity.SuperRetailTraderName!=null)
                strSql.Append( "[SuperRetailTraderName]=@SuperRetailTraderName,");
            if (pIsUpdateNullField || pEntity.SuperRetailTraderLogin!=null)
                strSql.Append( "[SuperRetailTraderLogin]=@SuperRetailTraderLogin,");
            if (pIsUpdateNullField || pEntity.SuperRetailTraderPass!=null)
                strSql.Append( "[SuperRetailTraderPass]=@SuperRetailTraderPass,");
            if (pIsUpdateNullField || pEntity.SuperRetailTraderMan!=null)
                strSql.Append( "[SuperRetailTraderMan]=@SuperRetailTraderMan,");
            if (pIsUpdateNullField || pEntity.SuperRetailTraderPhone!=null)
                strSql.Append( "[SuperRetailTraderPhone]=@SuperRetailTraderPhone,");
            if (pIsUpdateNullField || pEntity.SuperRetailTraderAddress!=null)
                strSql.Append( "[SuperRetailTraderAddress]=@SuperRetailTraderAddress,");
            if (pIsUpdateNullField || pEntity.SuperRetailTraderFrom!=null)
                strSql.Append( "[SuperRetailTraderFrom]=@SuperRetailTraderFrom,");
            if (pIsUpdateNullField || pEntity.SuperRetailTraderFromId!=null)
                strSql.Append( "[SuperRetailTraderFromId]=@SuperRetailTraderFromId,");
            if (pIsUpdateNullField || pEntity.HigheSuperRetailTraderID!=null)
                strSql.Append( "[HigheSuperRetailTraderID]=@HigheSuperRetailTraderID,");
            if (pIsUpdateNullField || pEntity.JoinTime!=null)
                strSql.Append( "[JoinTime]=@JoinTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.Status!=null)
                strSql.Append( "[Status]=@Status");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where SuperRetailTraderID=@SuperRetailTraderID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@SuperRetailTraderCode",SqlDbType.VarChar),
					new SqlParameter("@SuperRetailTraderName",SqlDbType.NVarChar),
					new SqlParameter("@SuperRetailTraderLogin",SqlDbType.NVarChar),
					new SqlParameter("@SuperRetailTraderPass",SqlDbType.NVarChar),
					new SqlParameter("@SuperRetailTraderMan",SqlDbType.NVarChar),
					new SqlParameter("@SuperRetailTraderPhone",SqlDbType.VarChar),
					new SqlParameter("@SuperRetailTraderAddress",SqlDbType.NVarChar),
					new SqlParameter("@SuperRetailTraderFrom",SqlDbType.VarChar),
					new SqlParameter("@SuperRetailTraderFromId",SqlDbType.NVarChar),
					new SqlParameter("@HigheSuperRetailTraderID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@JoinTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.NVarChar),
					new SqlParameter("@SuperRetailTraderID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.SuperRetailTraderCode;
			parameters[1].Value = pEntity.SuperRetailTraderName;
			parameters[2].Value = pEntity.SuperRetailTraderLogin;
			parameters[3].Value = pEntity.SuperRetailTraderPass;
			parameters[4].Value = pEntity.SuperRetailTraderMan;
			parameters[5].Value = pEntity.SuperRetailTraderPhone;
			parameters[6].Value = pEntity.SuperRetailTraderAddress;
			parameters[7].Value = pEntity.SuperRetailTraderFrom;
			parameters[8].Value = pEntity.SuperRetailTraderFromId;
			parameters[9].Value = pEntity.HigheSuperRetailTraderID;
			parameters[10].Value = pEntity.JoinTime;
			parameters[11].Value = pEntity.LastUpdateBy;
			parameters[12].Value = pEntity.LastUpdateTime;
			parameters[13].Value = pEntity.CustomerId;
			parameters[14].Value = pEntity.Status;
			parameters[15].Value = pEntity.SuperRetailTraderID;

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
        public void Update(T_SuperRetailTraderEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(T_SuperRetailTraderEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(T_SuperRetailTraderEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.SuperRetailTraderID.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.SuperRetailTraderID.Value, pTran);           
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pID">标识符的值</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(object pID, IDbTransaction pTran)
        {
            if (pID == null)
                return ;   
            //组织参数化SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [T_SuperRetailTrader] set  isdelete=1 where SuperRetailTraderID=@SuperRetailTraderID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@SuperRetailTraderID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
            };
            //执行语句
            int result = 0;
            if (pTran != null)
                result=this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), parameters);
            else
                result=this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), parameters);
            return ;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(T_SuperRetailTraderEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.SuperRetailTraderID.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.SuperRetailTraderID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(T_SuperRetailTraderEntity[] pEntities)
        { 
            Delete(pEntities, null);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pIDs">标识符值数组</param>
        public void Delete(object[] pIDs)
        {
            Delete(pIDs,null);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pIDs">标识符值数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(object[] pIDs, IDbTransaction pTran) 
        {
            if (pIDs == null || pIDs.Length==0)
                return ;
            //组织参数化SQL
            StringBuilder primaryKeys = new StringBuilder();
            foreach (object item in pIDs)
            {
                primaryKeys.AppendFormat("'{0}',",item.ToString());
            }
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [T_SuperRetailTrader] set  isdelete=1 where SuperRetailTraderID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
            //执行语句
            int result = 0;   
            if (pTran == null)
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), null);
            else
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran,CommandType.Text, sql.ToString());       
        }
        #endregion

        #region IQueryable 成员
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <returns></returns>
        public T_SuperRetailTraderEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_SuperRetailTrader] where 1=1  and isdelete=0 ");
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
            List<T_SuperRetailTraderEntity> list = new List<T_SuperRetailTraderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_SuperRetailTraderEntity m;
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
        public PagedQueryResult<T_SuperRetailTraderEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                    if(item!=null)
                    {
                        pagedSql.AppendFormat(" {0} {1},", StringUtils.WrapperSQLServerObject(item.FieldName), item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                    }
                }
                pagedSql.Remove(pagedSql.Length - 1, 1);
            }
            else
            {
                pagedSql.AppendFormat(" [SuperRetailTraderID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [T_SuperRetailTrader] where 1=1  and isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [T_SuperRetailTrader] where 1=1  and isdelete=0 ");
            //过滤条件
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    if(item!=null)
                    {
                        pagedSql.AppendFormat(" and {0}", item.GetExpression());
                        totalCountSql.AppendFormat(" and {0}", item.GetExpression());
                    }
                }
            }
            pagedSql.AppendFormat(") as A ");
            //取指定页的数据
            pagedSql.AppendFormat(" where ___rn >{0} and ___rn <={1}", pPageSize * (pCurrentPageIndex-1), pPageSize * (pCurrentPageIndex));
            //执行语句并返回结果
            PagedQueryResult<T_SuperRetailTraderEntity> result = new PagedQueryResult<T_SuperRetailTraderEntity>();
            List<T_SuperRetailTraderEntity> list = new List<T_SuperRetailTraderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    T_SuperRetailTraderEntity m;
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
        public T_SuperRetailTraderEntity[] QueryByEntity(T_SuperRetailTraderEntity pQueryEntity, OrderBy[] pOrderBys)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity(pQueryEntity);
            return Query(queryWhereCondition,  pOrderBys);            
        }

        /// <summary>
        /// 分页根据实体条件查询实体
        /// </summary>
        /// <param name="pQueryEntity">以实体形式传入的参数</param>
        /// <param name="pOrderBys">排序组合</param>
        /// <returns>符合条件的实体集</returns>
        public PagedQueryResult<T_SuperRetailTraderEntity> PagedQueryByEntity(T_SuperRetailTraderEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity( pQueryEntity);
            return PagedQuery(queryWhereCondition, pOrderBys, pPageSize, pCurrentPageIndex);
        }

        #endregion

        #region 工具方法
        /// <summary>
        /// 根据实体非Null属性生成查询条件。
        /// </summary>
        /// <returns></returns>
        protected IWhereCondition[] GetWhereConditionByEntity(T_SuperRetailTraderEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.SuperRetailTraderID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SuperRetailTraderID", Value = pQueryEntity.SuperRetailTraderID });
            if (pQueryEntity.SuperRetailTraderCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SuperRetailTraderCode", Value = pQueryEntity.SuperRetailTraderCode });
            if (pQueryEntity.SuperRetailTraderName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SuperRetailTraderName", Value = pQueryEntity.SuperRetailTraderName });
            if (pQueryEntity.SuperRetailTraderLogin!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SuperRetailTraderLogin", Value = pQueryEntity.SuperRetailTraderLogin });
            if (pQueryEntity.SuperRetailTraderPass!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SuperRetailTraderPass", Value = pQueryEntity.SuperRetailTraderPass });
            if (pQueryEntity.SuperRetailTraderMan!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SuperRetailTraderMan", Value = pQueryEntity.SuperRetailTraderMan });
            if (pQueryEntity.SuperRetailTraderPhone!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SuperRetailTraderPhone", Value = pQueryEntity.SuperRetailTraderPhone });
            if (pQueryEntity.SuperRetailTraderAddress!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SuperRetailTraderAddress", Value = pQueryEntity.SuperRetailTraderAddress });
            if (pQueryEntity.SuperRetailTraderFrom!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SuperRetailTraderFrom", Value = pQueryEntity.SuperRetailTraderFrom });
            if (pQueryEntity.SuperRetailTraderFromId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SuperRetailTraderFromId", Value = pQueryEntity.SuperRetailTraderFromId });
            if (pQueryEntity.HigheSuperRetailTraderID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "HigheSuperRetailTraderID", Value = pQueryEntity.HigheSuperRetailTraderID });
            if (pQueryEntity.JoinTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "JoinTime", Value = pQueryEntity.JoinTime });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.LastUpdateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.LastUpdateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.IsDelete!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
            if (pQueryEntity.Status!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Status", Value = pQueryEntity.Status });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out T_SuperRetailTraderEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new T_SuperRetailTraderEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["SuperRetailTraderID"] != DBNull.Value)
			{
				pInstance.SuperRetailTraderID =  (Guid)pReader["SuperRetailTraderID"];
			}
			if (pReader["SuperRetailTraderCode"] != DBNull.Value)
			{
				pInstance.SuperRetailTraderCode =  Convert.ToString(pReader["SuperRetailTraderCode"]);
			}
			if (pReader["SuperRetailTraderName"] != DBNull.Value)
			{
				pInstance.SuperRetailTraderName =  Convert.ToString(pReader["SuperRetailTraderName"]);
			}
			if (pReader["SuperRetailTraderLogin"] != DBNull.Value)
			{
				pInstance.SuperRetailTraderLogin =  Convert.ToString(pReader["SuperRetailTraderLogin"]);
			}
			if (pReader["SuperRetailTraderPass"] != DBNull.Value)
			{
				pInstance.SuperRetailTraderPass =  Convert.ToString(pReader["SuperRetailTraderPass"]);
			}
			if (pReader["SuperRetailTraderMan"] != DBNull.Value)
			{
				pInstance.SuperRetailTraderMan =  Convert.ToString(pReader["SuperRetailTraderMan"]);
			}
			if (pReader["SuperRetailTraderPhone"] != DBNull.Value)
			{
				pInstance.SuperRetailTraderPhone =  Convert.ToString(pReader["SuperRetailTraderPhone"]);
			}
			if (pReader["SuperRetailTraderAddress"] != DBNull.Value)
			{
				pInstance.SuperRetailTraderAddress =  Convert.ToString(pReader["SuperRetailTraderAddress"]);
			}
			if (pReader["SuperRetailTraderFrom"] != DBNull.Value)
			{
				pInstance.SuperRetailTraderFrom =  Convert.ToString(pReader["SuperRetailTraderFrom"]);
			}
			if (pReader["SuperRetailTraderFromId"] != DBNull.Value)
			{
				pInstance.SuperRetailTraderFromId =  Convert.ToString(pReader["SuperRetailTraderFromId"]);
			}
			if (pReader["HigheSuperRetailTraderID"] != DBNull.Value)
			{
				pInstance.HigheSuperRetailTraderID =  (Guid)pReader["HigheSuperRetailTraderID"];
			}
			if (pReader["JoinTime"] != DBNull.Value)
			{
				pInstance.JoinTime =  Convert.ToDateTime(pReader["JoinTime"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}
			if (pReader["CreateBy"] != DBNull.Value)
			{
				pInstance.CreateBy =  Convert.ToString(pReader["CreateBy"]);
			}
			if (pReader["LastUpdateBy"] != DBNull.Value)
			{
				pInstance.LastUpdateBy =  Convert.ToString(pReader["LastUpdateBy"]);
			}
			if (pReader["LastUpdateTime"] != DBNull.Value)
			{
				pInstance.LastUpdateTime =  Convert.ToDateTime(pReader["LastUpdateTime"]);
			}
			if (pReader["IsDelete"] != DBNull.Value)
			{
				pInstance.IsDelete =   Convert.ToInt32(pReader["IsDelete"]);
			}
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}
			if (pReader["Status"] != DBNull.Value)
			{
				pInstance.Status =  Convert.ToString(pReader["Status"]);
			}

        }
        #endregion
    }
}
