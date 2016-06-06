/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/4/14 16:14:12
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
    /// 表T_LEventsSharePersonLog的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_LEventsSharePersonLogDAO : Base.BaseCPOSDAO, ICRUDable<T_LEventsSharePersonLogEntity>, IQueryable<T_LEventsSharePersonLogEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_LEventsSharePersonLogDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(T_LEventsSharePersonLogEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(T_LEventsSharePersonLogEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [T_LEventsSharePersonLog](");
            strSql.Append("[BusTypeCode],[ObjectId],[ShareVipID],[ShareOpenID],[ShareCount],[BeShareVipID],[BeShareOpenID],[ShareURL],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[CustomerId],[IsDelete],[ShareVipType],[SharePersonLogId])");
            strSql.Append(" values (");
            strSql.Append("@BusTypeCode,@ObjectId,@ShareVipID,@ShareOpenID,@ShareCount,@BeShareVipID,@BeShareOpenID,@ShareURL,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@CustomerId,@IsDelete,@ShareVipType,@SharePersonLogId)");            

			Guid? pkGuid;
			if (pEntity.SharePersonLogId == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.SharePersonLogId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@BusTypeCode",SqlDbType.VarChar),
					new SqlParameter("@ObjectId",SqlDbType.NVarChar),
					new SqlParameter("@ShareVipID",SqlDbType.NVarChar),
					new SqlParameter("@ShareOpenID",SqlDbType.NVarChar),
					new SqlParameter("@ShareCount",SqlDbType.Int),
					new SqlParameter("@BeShareVipID",SqlDbType.NVarChar),
					new SqlParameter("@BeShareOpenID",SqlDbType.NVarChar),
					new SqlParameter("@ShareURL",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerId",SqlDbType.VarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@ShareVipType",SqlDbType.Int),
					new SqlParameter("@SharePersonLogId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.BusTypeCode;
			parameters[1].Value = pEntity.ObjectId;
			parameters[2].Value = pEntity.ShareVipID;
			parameters[3].Value = pEntity.ShareOpenID;
			parameters[4].Value = pEntity.ShareCount;
			parameters[5].Value = pEntity.BeShareVipID;
			parameters[6].Value = pEntity.BeShareOpenID;
			parameters[7].Value = pEntity.ShareURL;
			parameters[8].Value = pEntity.CreateTime;
			parameters[9].Value = pEntity.CreateBy;
			parameters[10].Value = pEntity.LastUpdateBy;
			parameters[11].Value = pEntity.LastUpdateTime;
			parameters[12].Value = pEntity.CustomerId;
			parameters[13].Value = pEntity.IsDelete;
			parameters[14].Value = pEntity.ShareVipType;
			parameters[15].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.SharePersonLogId = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public T_LEventsSharePersonLogEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_LEventsSharePersonLog] where SharePersonLogId='{0}'  and isdelete=0 ", id.ToString());
            //读取数据
            T_LEventsSharePersonLogEntity m = null;
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
        public T_LEventsSharePersonLogEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_LEventsSharePersonLog] where 1=1  and isdelete=0");
            //读取数据
            List<T_LEventsSharePersonLogEntity> list = new List<T_LEventsSharePersonLogEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_LEventsSharePersonLogEntity m;
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
        public void Update(T_LEventsSharePersonLogEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(T_LEventsSharePersonLogEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.SharePersonLogId.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_LEventsSharePersonLog] set ");
                        if (pIsUpdateNullField || pEntity.BusTypeCode!=null)
                strSql.Append( "[BusTypeCode]=@BusTypeCode,");
            if (pIsUpdateNullField || pEntity.ObjectId!=null)
                strSql.Append( "[ObjectId]=@ObjectId,");
            if (pIsUpdateNullField || pEntity.ShareVipID!=null)
                strSql.Append( "[ShareVipID]=@ShareVipID,");
            if (pIsUpdateNullField || pEntity.ShareOpenID!=null)
                strSql.Append( "[ShareOpenID]=@ShareOpenID,");
            if (pIsUpdateNullField || pEntity.ShareCount!=null)
                strSql.Append( "[ShareCount]=@ShareCount,");
            if (pIsUpdateNullField || pEntity.BeShareVipID!=null)
                strSql.Append( "[BeShareVipID]=@BeShareVipID,");
            if (pIsUpdateNullField || pEntity.BeShareOpenID!=null)
                strSql.Append( "[BeShareOpenID]=@BeShareOpenID,");
            if (pIsUpdateNullField || pEntity.ShareURL!=null)
                strSql.Append( "[ShareURL]=@ShareURL,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.ShareVipType!=null)
                strSql.Append( "[ShareVipType]=@ShareVipType");
            strSql.Append(" where SharePersonLogId=@SharePersonLogId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@BusTypeCode",SqlDbType.VarChar),
					new SqlParameter("@ObjectId",SqlDbType.NVarChar),
					new SqlParameter("@ShareVipID",SqlDbType.NVarChar),
					new SqlParameter("@ShareOpenID",SqlDbType.NVarChar),
					new SqlParameter("@ShareCount",SqlDbType.Int),
					new SqlParameter("@BeShareVipID",SqlDbType.NVarChar),
					new SqlParameter("@BeShareOpenID",SqlDbType.NVarChar),
					new SqlParameter("@ShareURL",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerId",SqlDbType.VarChar),
					new SqlParameter("@ShareVipType",SqlDbType.Int),
					new SqlParameter("@SharePersonLogId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.BusTypeCode;
			parameters[1].Value = pEntity.ObjectId;
			parameters[2].Value = pEntity.ShareVipID;
			parameters[3].Value = pEntity.ShareOpenID;
			parameters[4].Value = pEntity.ShareCount;
			parameters[5].Value = pEntity.BeShareVipID;
			parameters[6].Value = pEntity.BeShareOpenID;
			parameters[7].Value = pEntity.ShareURL;
			parameters[8].Value = pEntity.LastUpdateBy;
			parameters[9].Value = pEntity.LastUpdateTime;
			parameters[10].Value = pEntity.CustomerId;
			parameters[11].Value = pEntity.ShareVipType;
			parameters[12].Value = pEntity.SharePersonLogId;

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
        public void Update(T_LEventsSharePersonLogEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(T_LEventsSharePersonLogEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(T_LEventsSharePersonLogEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.SharePersonLogId.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.SharePersonLogId.Value, pTran);           
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
            sql.AppendLine("update [T_LEventsSharePersonLog] set  isdelete=1 where SharePersonLogId=@SharePersonLogId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@SharePersonLogId",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(T_LEventsSharePersonLogEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.SharePersonLogId.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.SharePersonLogId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(T_LEventsSharePersonLogEntity[] pEntities)
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
            sql.AppendLine("update [T_LEventsSharePersonLog] set  isdelete=1 where SharePersonLogId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public T_LEventsSharePersonLogEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_LEventsSharePersonLog] where 1=1  and isdelete=0 ");
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
            List<T_LEventsSharePersonLogEntity> list = new List<T_LEventsSharePersonLogEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_LEventsSharePersonLogEntity m;
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
        public PagedQueryResult<T_LEventsSharePersonLogEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [SharePersonLogId] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [T_LEventsSharePersonLog] where 1=1  and isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [T_LEventsSharePersonLog] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<T_LEventsSharePersonLogEntity> result = new PagedQueryResult<T_LEventsSharePersonLogEntity>();
            List<T_LEventsSharePersonLogEntity> list = new List<T_LEventsSharePersonLogEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    T_LEventsSharePersonLogEntity m;
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
        public T_LEventsSharePersonLogEntity[] QueryByEntity(T_LEventsSharePersonLogEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<T_LEventsSharePersonLogEntity> PagedQueryByEntity(T_LEventsSharePersonLogEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(T_LEventsSharePersonLogEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.SharePersonLogId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SharePersonLogId", Value = pQueryEntity.SharePersonLogId });
            if (pQueryEntity.BusTypeCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BusTypeCode", Value = pQueryEntity.BusTypeCode });
            if (pQueryEntity.ObjectId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ObjectId", Value = pQueryEntity.ObjectId });
            if (pQueryEntity.ShareVipID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ShareVipID", Value = pQueryEntity.ShareVipID });
            if (pQueryEntity.ShareOpenID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ShareOpenID", Value = pQueryEntity.ShareOpenID });
            if (pQueryEntity.ShareCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ShareCount", Value = pQueryEntity.ShareCount });
            if (pQueryEntity.BeShareVipID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BeShareVipID", Value = pQueryEntity.BeShareVipID });
            if (pQueryEntity.BeShareOpenID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BeShareOpenID", Value = pQueryEntity.BeShareOpenID });
            if (pQueryEntity.ShareURL!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ShareURL", Value = pQueryEntity.ShareURL });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.LastUpdateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.LastUpdateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
            if (pQueryEntity.IsDelete!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });
            if (pQueryEntity.ShareVipType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ShareVipType", Value = pQueryEntity.ShareVipType });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out T_LEventsSharePersonLogEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new T_LEventsSharePersonLogEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["SharePersonLogId"] != DBNull.Value)
			{
				pInstance.SharePersonLogId =  (Guid)pReader["SharePersonLogId"];
			}
			if (pReader["BusTypeCode"] != DBNull.Value)
			{
				pInstance.BusTypeCode =  Convert.ToString(pReader["BusTypeCode"]);
			}
			if (pReader["ObjectId"] != DBNull.Value)
			{
				pInstance.ObjectId =  Convert.ToString(pReader["ObjectId"]);
			}
			if (pReader["ShareVipID"] != DBNull.Value)
			{
				pInstance.ShareVipID =  Convert.ToString(pReader["ShareVipID"]);
			}
			if (pReader["ShareOpenID"] != DBNull.Value)
			{
				pInstance.ShareOpenID =  Convert.ToString(pReader["ShareOpenID"]);
			}
			if (pReader["ShareCount"] != DBNull.Value)
			{
				pInstance.ShareCount =   Convert.ToInt32(pReader["ShareCount"]);
			}
			if (pReader["BeShareVipID"] != DBNull.Value)
			{
				pInstance.BeShareVipID =  Convert.ToString(pReader["BeShareVipID"]);
			}
			if (pReader["BeShareOpenID"] != DBNull.Value)
			{
				pInstance.BeShareOpenID =  Convert.ToString(pReader["BeShareOpenID"]);
			}
			if (pReader["ShareURL"] != DBNull.Value)
			{
				pInstance.ShareURL =  Convert.ToString(pReader["ShareURL"]);
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
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}
			if (pReader["IsDelete"] != DBNull.Value)
			{
				pInstance.IsDelete =   Convert.ToInt32(pReader["IsDelete"]);
			}
			if (pReader["ShareVipType"] != DBNull.Value)
			{
				pInstance.ShareVipType =   Convert.ToInt32(pReader["ShareVipType"]);
			}

        }
        #endregion
    }
}
