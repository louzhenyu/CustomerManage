/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/9/1 18:49:15
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
    /// 表TOrderPushStrategy的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class TOrderPushStrategyDAO : Base.BaseCPOSDAO, ICRUDable<TOrderPushStrategyEntity>, IQueryable<TOrderPushStrategyEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TOrderPushStrategyDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(TOrderPushStrategyEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(TOrderPushStrategyEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [TOrderPushStrategy](");
            strSql.Append("[CustomerId],[OrderStatus],[IsIOSPush],[IsAndroidPush],[IsWXPush],[IsSMSPush],[IsEmailPush],[PushInfo],[PushObject],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[Id])");
            strSql.Append(" values (");
            strSql.Append("@CustomerId,@OrderStatus,@IsIOSPush,@IsAndroidPush,@IsWXPush,@IsSMSPush,@IsEmailPush,@PushInfo,@PushObject,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@Id)");            

			Guid? pkGuid;
			if (pEntity.Id == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.Id;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@OrderStatus",SqlDbType.Int),
					new SqlParameter("@IsIOSPush",SqlDbType.Int),
					new SqlParameter("@IsAndroidPush",SqlDbType.Int),
					new SqlParameter("@IsWXPush",SqlDbType.Int),
					new SqlParameter("@IsSMSPush",SqlDbType.Int),
					new SqlParameter("@IsEmailPush",SqlDbType.Int),
					new SqlParameter("@PushInfo",SqlDbType.NVarChar),
					new SqlParameter("@PushObject",SqlDbType.Int),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@Id",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.CustomerId;
			parameters[1].Value = pEntity.OrderStatus;
			parameters[2].Value = pEntity.IsIOSPush;
			parameters[3].Value = pEntity.IsAndroidPush;
			parameters[4].Value = pEntity.IsWXPush;
			parameters[5].Value = pEntity.IsSMSPush;
			parameters[6].Value = pEntity.IsEmailPush;
			parameters[7].Value = pEntity.PushInfo;
			parameters[8].Value = pEntity.PushObject;
			parameters[9].Value = pEntity.CreateBy;
			parameters[10].Value = pEntity.CreateTime;
			parameters[11].Value = pEntity.LastUpdateBy;
			parameters[12].Value = pEntity.LastUpdateTime;
			parameters[13].Value = pEntity.IsDelete;
			parameters[14].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.Id = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public TOrderPushStrategyEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [TOrderPushStrategy] where Id='{0}'  and isdelete=0 ", id.ToString());
            //读取数据
            TOrderPushStrategyEntity m = null;
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
        public TOrderPushStrategyEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [TOrderPushStrategy] where 1=1  and isdelete=0");
            //读取数据
            List<TOrderPushStrategyEntity> list = new List<TOrderPushStrategyEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    TOrderPushStrategyEntity m;
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
        public void Update(TOrderPushStrategyEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(TOrderPushStrategyEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.Id.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [TOrderPushStrategy] set ");
                        if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.OrderStatus!=null)
                strSql.Append( "[OrderStatus]=@OrderStatus,");
            if (pIsUpdateNullField || pEntity.IsIOSPush!=null)
                strSql.Append( "[IsIOSPush]=@IsIOSPush,");
            if (pIsUpdateNullField || pEntity.IsAndroidPush!=null)
                strSql.Append( "[IsAndroidPush]=@IsAndroidPush,");
            if (pIsUpdateNullField || pEntity.IsWXPush!=null)
                strSql.Append( "[IsWXPush]=@IsWXPush,");
            if (pIsUpdateNullField || pEntity.IsSMSPush!=null)
                strSql.Append( "[IsSMSPush]=@IsSMSPush,");
            if (pIsUpdateNullField || pEntity.IsEmailPush!=null)
                strSql.Append( "[IsEmailPush]=@IsEmailPush,");
            if (pIsUpdateNullField || pEntity.PushInfo!=null)
                strSql.Append( "[PushInfo]=@PushInfo,");
            if (pIsUpdateNullField || pEntity.PushObject!=null)
                strSql.Append( "[PushObject]=@PushObject,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@OrderStatus",SqlDbType.Int),
					new SqlParameter("@IsIOSPush",SqlDbType.Int),
					new SqlParameter("@IsAndroidPush",SqlDbType.Int),
					new SqlParameter("@IsWXPush",SqlDbType.Int),
					new SqlParameter("@IsSMSPush",SqlDbType.Int),
					new SqlParameter("@IsEmailPush",SqlDbType.Int),
					new SqlParameter("@PushInfo",SqlDbType.NVarChar),
					new SqlParameter("@PushObject",SqlDbType.Int),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@Id",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.CustomerId;
			parameters[1].Value = pEntity.OrderStatus;
			parameters[2].Value = pEntity.IsIOSPush;
			parameters[3].Value = pEntity.IsAndroidPush;
			parameters[4].Value = pEntity.IsWXPush;
			parameters[5].Value = pEntity.IsSMSPush;
			parameters[6].Value = pEntity.IsEmailPush;
			parameters[7].Value = pEntity.PushInfo;
			parameters[8].Value = pEntity.PushObject;
			parameters[9].Value = pEntity.LastUpdateBy;
			parameters[10].Value = pEntity.LastUpdateTime;
			parameters[11].Value = pEntity.Id;

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
        public void Update(TOrderPushStrategyEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(TOrderPushStrategyEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(TOrderPushStrategyEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.Id.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.Id.Value, pTran);           
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
            sql.AppendLine("update [TOrderPushStrategy] set  isdelete=1 where Id=@Id;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@Id",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(TOrderPushStrategyEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.Id.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.Id;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(TOrderPushStrategyEntity[] pEntities)
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
            sql.AppendLine("update [TOrderPushStrategy] set  isdelete=1 where Id in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public TOrderPushStrategyEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [TOrderPushStrategy] where 1=1  and isdelete=0 ");
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
            List<TOrderPushStrategyEntity> list = new List<TOrderPushStrategyEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    TOrderPushStrategyEntity m;
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
        public PagedQueryResult<TOrderPushStrategyEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [Id] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [TOrderPushStrategy] where 1=1  and isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [TOrderPushStrategy] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<TOrderPushStrategyEntity> result = new PagedQueryResult<TOrderPushStrategyEntity>();
            List<TOrderPushStrategyEntity> list = new List<TOrderPushStrategyEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    TOrderPushStrategyEntity m;
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
        public TOrderPushStrategyEntity[] QueryByEntity(TOrderPushStrategyEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<TOrderPushStrategyEntity> PagedQueryByEntity(TOrderPushStrategyEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(TOrderPushStrategyEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.Id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Id", Value = pQueryEntity.Id });
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
            if (pQueryEntity.OrderStatus!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderStatus", Value = pQueryEntity.OrderStatus });
            if (pQueryEntity.IsIOSPush!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsIOSPush", Value = pQueryEntity.IsIOSPush });
            if (pQueryEntity.IsAndroidPush!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsAndroidPush", Value = pQueryEntity.IsAndroidPush });
            if (pQueryEntity.IsWXPush!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsWXPush", Value = pQueryEntity.IsWXPush });
            if (pQueryEntity.IsSMSPush!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsSMSPush", Value = pQueryEntity.IsSMSPush });
            if (pQueryEntity.IsEmailPush!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsEmailPush", Value = pQueryEntity.IsEmailPush });
            if (pQueryEntity.PushInfo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PushInfo", Value = pQueryEntity.PushInfo });
            if (pQueryEntity.PushObject!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PushObject", Value = pQueryEntity.PushObject });
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

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out TOrderPushStrategyEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new TOrderPushStrategyEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["Id"] != DBNull.Value)
			{
				pInstance.Id =  (Guid)pReader["Id"];
			}
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}
			if (pReader["OrderStatus"] != DBNull.Value)
			{
				pInstance.OrderStatus =   Convert.ToInt32(pReader["OrderStatus"]);
			}
			if (pReader["IsIOSPush"] != DBNull.Value)
			{
				pInstance.IsIOSPush =   Convert.ToInt32(pReader["IsIOSPush"]);
			}
			if (pReader["IsAndroidPush"] != DBNull.Value)
			{
				pInstance.IsAndroidPush =   Convert.ToInt32(pReader["IsAndroidPush"]);
			}
			if (pReader["IsWXPush"] != DBNull.Value)
			{
				pInstance.IsWXPush =   Convert.ToInt32(pReader["IsWXPush"]);
			}
			if (pReader["IsSMSPush"] != DBNull.Value)
			{
				pInstance.IsSMSPush =   Convert.ToInt32(pReader["IsSMSPush"]);
			}
			if (pReader["IsEmailPush"] != DBNull.Value)
			{
				pInstance.IsEmailPush =   Convert.ToInt32(pReader["IsEmailPush"]);
			}
			if (pReader["PushInfo"] != DBNull.Value)
			{
				pInstance.PushInfo =  Convert.ToString(pReader["PushInfo"]);
			}
			if (pReader["PushObject"] != DBNull.Value)
			{
				pInstance.PushObject =   Convert.ToInt32(pReader["PushObject"]);
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

        }
        #endregion
    }
}
