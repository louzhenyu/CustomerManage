/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/13 16:48:24
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
    /// 表TInOutStatusNode的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class TInOutStatusNodeDAO : Base.BaseCPOSDAO, ICRUDable<TInOutStatusNodeEntity>, IQueryable<TInOutStatusNodeEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TInOutStatusNodeDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(TInOutStatusNodeEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(TInOutStatusNodeEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [TInOutStatusNode](");
            strSql.Append("[NodeCode],[NodeValue],[PreviousValue],[NextValue],[PayMethod],[Sequence],[CustomerID],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[DeliveryMethod],[ActionDesc],[ActionDescEn],[NodeID])");
            strSql.Append(" values (");
            strSql.Append("@NodeCode,@NodeValue,@PreviousValue,@NextValue,@PayMethod,@Sequence,@CustomerID,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@DeliveryMethod,@ActionDesc,@ActionDescEn,@NodeID)");           

			Guid? pkGuid;
			if (pEntity.NodeID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.NodeID;

            SqlParameter[] parameters = 
            {
                    new SqlParameter("@NodeCode",SqlDbType.NVarChar,100),
					new SqlParameter("@NodeValue",SqlDbType.NVarChar,100),
					new SqlParameter("@PreviousValue",SqlDbType.NVarChar,100),
					new SqlParameter("@NextValue",SqlDbType.NVarChar,100),
					new SqlParameter("@PayMethod",SqlDbType.NVarChar,100),
					new SqlParameter("@Sequence",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar,100),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar,100),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar,100),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@DeliveryMethod",SqlDbType.NVarChar,100),
					new SqlParameter("@ActionDesc",SqlDbType.NVarChar,100),
					new SqlParameter("@ActionDescEn",SqlDbType.NVarChar,100),
					new SqlParameter("@NodeID",SqlDbType.UniqueIdentifier,16)
            };
            parameters[0].Value = pEntity.NodeCode;
            parameters[1].Value = pEntity.NodeValue;
            parameters[2].Value = pEntity.PreviousValue;
            parameters[3].Value = pEntity.NextValue;
            parameters[4].Value = pEntity.PayMethod;
            parameters[5].Value = pEntity.Sequence;
            parameters[6].Value = pEntity.CustomerID;
            parameters[7].Value = pEntity.CreateBy;
            parameters[8].Value = pEntity.CreateTime;
            parameters[9].Value = pEntity.LastUpdateBy;
            parameters[10].Value = pEntity.LastUpdateTime;
            parameters[11].Value = pEntity.IsDelete;
            parameters[12].Value = pEntity.DeliveryMethod;
            parameters[13].Value = pEntity.ActionDesc;
            parameters[14].Value = pEntity.ActionDescEn;
            parameters[15].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.NodeID = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public TInOutStatusNodeEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [TInOutStatusNode] where NodeID='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            TInOutStatusNodeEntity m = null;
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
        public TInOutStatusNodeEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [TInOutStatusNode] where isdelete=0");
            //读取数据
            List<TInOutStatusNodeEntity> list = new List<TInOutStatusNodeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    TInOutStatusNodeEntity m;
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
        public void Update(TInOutStatusNodeEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(TInOutStatusNodeEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.NodeID==null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [TInOutStatusNode] set ");
            if (pIsUpdateNullField || pEntity.NodeCode!=null)
                strSql.Append( "[NodeCode]=@NodeCode,");
            if (pIsUpdateNullField || pEntity.NodeValue!=null)
                strSql.Append( "[NodeValue]=@NodeValue,");
            if (pIsUpdateNullField || pEntity.PreviousValue!=null)
                strSql.Append( "[PreviousValue]=@PreviousValue,");
            if (pIsUpdateNullField || pEntity.NextValue!=null)
                strSql.Append( "[NextValue]=@NextValue,");
            if (pIsUpdateNullField || pEntity.PayMethod!=null)
                strSql.Append( "[PayMethod]=@PayMethod,");
            if (pIsUpdateNullField || pEntity.Sequence!=null)
                strSql.Append( "[Sequence]=@Sequence,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");

            if (pIsUpdateNullField || pEntity.DeliveryMethod != null)
                strSql.Append("[DeliveryMethod]=@DeliveryMethod,");
            if (pIsUpdateNullField || pEntity.ActionDesc != null)
                strSql.Append("[ActionDesc]=@ActionDesc,");
            if (pIsUpdateNullField || pEntity.ActionDescEn != null)
                strSql.Append("[ActionDescEn]=@ActionDescEn,");

            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where NodeID=@NodeID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@NodeCode",SqlDbType.NVarChar),
					new SqlParameter("@NodeValue",SqlDbType.NVarChar),
					new SqlParameter("@PreviousValue",SqlDbType.NVarChar),
					new SqlParameter("@NextValue",SqlDbType.NVarChar),
					new SqlParameter("@PayMethod",SqlDbType.NVarChar),
					new SqlParameter("@Sequence",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),

                    new SqlParameter("@DeliveryMethod",SqlDbType.NVarChar),
                    new SqlParameter("@ActionDesc",SqlDbType.NVarChar),
                    new SqlParameter("@ActionDescEn",SqlDbType.NVarChar),

					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@NodeID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.NodeCode;
			parameters[1].Value = pEntity.NodeValue;
			parameters[2].Value = pEntity.PreviousValue;
			parameters[3].Value = pEntity.NextValue;
			parameters[4].Value = pEntity.PayMethod;
			parameters[5].Value = pEntity.Sequence;
			parameters[6].Value = pEntity.CustomerID;
			parameters[7].Value = pEntity.LastUpdateBy;

            parameters[8].Value = pEntity.DeliveryMethod;
            parameters[9].Value = pEntity.ActionDesc;
            parameters[10].Value = pEntity.ActionDescEn;

			parameters[11].Value = pEntity.LastUpdateTime;
			parameters[12].Value = pEntity.NodeID;

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
        public void Update(TInOutStatusNodeEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(TInOutStatusNodeEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(TInOutStatusNodeEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(TInOutStatusNodeEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.NodeID==null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.NodeID, pTran);           
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
            sql.AppendLine("update [TInOutStatusNode] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where NodeID=@NodeID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@NodeID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(TInOutStatusNodeEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.NodeID==null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.NodeID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(TInOutStatusNodeEntity[] pEntities)
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
            sql.AppendLine("update [TInOutStatusNode] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where NodeID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public TInOutStatusNodeEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [TInOutStatusNode] where isdelete=0 ");
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
            List<TInOutStatusNodeEntity> list = new List<TInOutStatusNodeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    TInOutStatusNodeEntity m;
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
        public PagedQueryResult<TInOutStatusNodeEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [NodeID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [TInOutStatusNode] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [TInOutStatusNode] where isdelete=0 ");
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
            PagedQueryResult<TInOutStatusNodeEntity> result = new PagedQueryResult<TInOutStatusNodeEntity>();
            List<TInOutStatusNodeEntity> list = new List<TInOutStatusNodeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    TInOutStatusNodeEntity m;
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
        public TInOutStatusNodeEntity[] QueryByEntity(TInOutStatusNodeEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<TInOutStatusNodeEntity> PagedQueryByEntity(TInOutStatusNodeEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(TInOutStatusNodeEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.NodeID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NodeID", Value = pQueryEntity.NodeID });
            if (pQueryEntity.NodeCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NodeCode", Value = pQueryEntity.NodeCode });
            if (pQueryEntity.NodeValue!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NodeValue", Value = pQueryEntity.NodeValue });
            if (pQueryEntity.PreviousValue!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PreviousValue", Value = pQueryEntity.PreviousValue });
            if (pQueryEntity.NextValue!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NextValue", Value = pQueryEntity.NextValue });
            if (pQueryEntity.PayMethod!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PayMethod", Value = pQueryEntity.PayMethod });
            if (pQueryEntity.Sequence!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Sequence", Value = pQueryEntity.Sequence });
            if (pQueryEntity.CustomerID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.LastUpdateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.LastUpdateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.IsDelete!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });
            if (pQueryEntity.DeliveryMethod != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DeliveryMethod", Value = pQueryEntity.DeliveryMethod });
            if (pQueryEntity.ActionDesc != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ActionDesc", Value = pQueryEntity.ActionDesc });
            if (pQueryEntity.ActionDescEn != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ActionDescEn", Value = pQueryEntity.ActionDescEn });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out TInOutStatusNodeEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new TInOutStatusNodeEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["NodeID"] != DBNull.Value)
			{
				pInstance.NodeID =  (Guid)pReader["NodeID"];
			}
			if (pReader["NodeCode"] != DBNull.Value)
			{
				pInstance.NodeCode =  Convert.ToString(pReader["NodeCode"]);
			}
			if (pReader["NodeValue"] != DBNull.Value)
			{
				pInstance.NodeValue =  Convert.ToString(pReader["NodeValue"]);
			}
			if (pReader["PreviousValue"] != DBNull.Value)
			{
				pInstance.PreviousValue =  Convert.ToString(pReader["PreviousValue"]);
			}
			if (pReader["NextValue"] != DBNull.Value)
			{
				pInstance.NextValue =  Convert.ToString(pReader["NextValue"]);
			}
			if (pReader["PayMethod"] != DBNull.Value)
			{
				pInstance.PayMethod =  Convert.ToString(pReader["PayMethod"]);
			}
			if (pReader["Sequence"] != DBNull.Value)
			{
				pInstance.Sequence =   Convert.ToInt32(pReader["Sequence"]);
			}
			if (pReader["CustomerID"] != DBNull.Value)
			{
				pInstance.CustomerID =  Convert.ToString(pReader["CustomerID"]);
			}
			if (pReader["CreateBy"] != DBNull.Value)
			{
				pInstance.CreateBy =  Convert.ToString(pReader["CreateBy"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
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
            if (pReader["DeliveryMethod"] != DBNull.Value)
            {
                pInstance.DeliveryMethod = Convert.ToString(pReader["DeliveryMethod"]);
            }
            if (pReader["ActionDesc"] != DBNull.Value)
            {
                pInstance.ActionDesc = Convert.ToString(pReader["ActionDesc"]);
            }
            if (pReader["ActionDescEn"] != DBNull.Value)
            {
                pInstance.ActionDescEn = Convert.ToString(pReader["ActionDescEn"]);
            }

        }
        #endregion
    }
}
