/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/3/19 11:39:44
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
    /// 表T_CTW_LEventTemplate的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_CTW_LEventTemplateDAO : Base.BaseCPOSDAO, ICRUDable<T_CTW_LEventTemplateEntity>, IQueryable<T_CTW_LEventTemplateEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_CTW_LEventTemplateDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
           
        }
        #endregion


        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(T_CTW_LEventTemplateEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(T_CTW_LEventTemplateEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [T_CTW_LEventTemplate](");
            strSql.Append("[TemplateName],[TemplateDesc],[ActivityGroupId],[TemplateStatus],[ImageId],[StartDate],[EndDate],[IsLongTime],[DisplayIndex],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[TemplateId])");
            strSql.Append(" values (");
            strSql.Append("@TemplateName,@TemplateDesc,@ActivityGroupId,@TemplateStatus,@ImageId,@StartDate,@EndDate,@IsLongTime,@DisplayIndex,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@TemplateId)");            

			Guid? pkGuid;
			if (pEntity.TemplateId == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.TemplateId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@TemplateName",SqlDbType.NVarChar),
					new SqlParameter("@TemplateDesc",SqlDbType.NVarChar),
					new SqlParameter("@ActivityGroupId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@TemplateStatus",SqlDbType.Int),
					new SqlParameter("@ImageId",SqlDbType.VarChar),
					new SqlParameter("@StartDate",SqlDbType.DateTime),
					new SqlParameter("@EndDate",SqlDbType.DateTime),
					new SqlParameter("@IsLongTime",SqlDbType.Int),
					new SqlParameter("@DisplayIndex",SqlDbType.Int),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@TemplateId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.TemplateName;
			parameters[1].Value = pEntity.TemplateDesc;
			parameters[2].Value = pEntity.ActivityGroupId;
			parameters[3].Value = pEntity.TemplateStatus;
			parameters[4].Value = pEntity.ImageId;
			parameters[5].Value = pEntity.StartDate;
			parameters[6].Value = pEntity.EndDate;
			parameters[7].Value = pEntity.IsLongTime;
			parameters[8].Value = pEntity.DisplayIndex;
			parameters[9].Value = pEntity.CreateTime;
			parameters[10].Value = pEntity.CreateBy;
			parameters[11].Value = pEntity.LastUpdateTime;
			parameters[12].Value = pEntity.LastUpdateBy;
			parameters[13].Value = pEntity.IsDelete;
			parameters[14].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.TemplateId = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public T_CTW_LEventTemplateEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_CTW_LEventTemplate] where TemplateId='{0}'  and isdelete=0 ", id.ToString());
            //读取数据
            T_CTW_LEventTemplateEntity m = null;
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
        public T_CTW_LEventTemplateEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_CTW_LEventTemplate] where 1=1  and isdelete=0");
            //读取数据
            List<T_CTW_LEventTemplateEntity> list = new List<T_CTW_LEventTemplateEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_CTW_LEventTemplateEntity m;
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
        public void Update(T_CTW_LEventTemplateEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(T_CTW_LEventTemplateEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.TemplateId.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_CTW_LEventTemplate] set ");
                        if (pIsUpdateNullField || pEntity.TemplateName!=null)
                strSql.Append( "[TemplateName]=@TemplateName,");
            if (pIsUpdateNullField || pEntity.TemplateDesc!=null)
                strSql.Append( "[TemplateDesc]=@TemplateDesc,");
            if (pIsUpdateNullField || pEntity.ActivityGroupId!=null)
                strSql.Append( "[ActivityGroupId]=@ActivityGroupId,");
            if (pIsUpdateNullField || pEntity.TemplateStatus!=null)
                strSql.Append( "[TemplateStatus]=@TemplateStatus,");
            if (pIsUpdateNullField || pEntity.ImageId!=null)
                strSql.Append( "[ImageId]=@ImageId,");
            if (pIsUpdateNullField || pEntity.StartDate!=null)
                strSql.Append( "[StartDate]=@StartDate,");
            if (pIsUpdateNullField || pEntity.EndDate!=null)
                strSql.Append( "[EndDate]=@EndDate,");
            if (pIsUpdateNullField || pEntity.IsLongTime!=null)
                strSql.Append( "[IsLongTime]=@IsLongTime,");
            if (pIsUpdateNullField || pEntity.DisplayIndex!=null)
                strSql.Append( "[DisplayIndex]=@DisplayIndex,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy");
            strSql.Append(" where TemplateId=@TemplateId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@TemplateName",SqlDbType.NVarChar),
					new SqlParameter("@TemplateDesc",SqlDbType.NVarChar),
					new SqlParameter("@ActivityGroupId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@TemplateStatus",SqlDbType.Int),
					new SqlParameter("@ImageId",SqlDbType.VarChar),
					new SqlParameter("@StartDate",SqlDbType.DateTime),
					new SqlParameter("@EndDate",SqlDbType.DateTime),
					new SqlParameter("@IsLongTime",SqlDbType.Int),
					new SqlParameter("@DisplayIndex",SqlDbType.Int),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@TemplateId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.TemplateName;
			parameters[1].Value = pEntity.TemplateDesc;
			parameters[2].Value = pEntity.ActivityGroupId;
			parameters[3].Value = pEntity.TemplateStatus;
			parameters[4].Value = pEntity.ImageId;
			parameters[5].Value = pEntity.StartDate;
			parameters[6].Value = pEntity.EndDate;
			parameters[7].Value = pEntity.IsLongTime;
			parameters[8].Value = pEntity.DisplayIndex;
			parameters[9].Value = pEntity.LastUpdateTime;
			parameters[10].Value = pEntity.LastUpdateBy;
			parameters[11].Value = pEntity.TemplateId;

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
        public void Update(T_CTW_LEventTemplateEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(T_CTW_LEventTemplateEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(T_CTW_LEventTemplateEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.TemplateId.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.TemplateId.Value, pTran);           
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
            sql.AppendLine("update [T_CTW_LEventTemplate] set  isdelete=1 where TemplateId=@TemplateId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@TemplateId",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(T_CTW_LEventTemplateEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.TemplateId.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.TemplateId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(T_CTW_LEventTemplateEntity[] pEntities)
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
            sql.AppendLine("update [T_CTW_LEventTemplate] set  isdelete=1 where TemplateId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public T_CTW_LEventTemplateEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_CTW_LEventTemplate] where 1=1  and isdelete=0 ");
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
            List<T_CTW_LEventTemplateEntity> list = new List<T_CTW_LEventTemplateEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_CTW_LEventTemplateEntity m;
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
        public PagedQueryResult<T_CTW_LEventTemplateEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [TemplateId] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [T_CTW_LEventTemplate] where 1=1  and isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [T_CTW_LEventTemplate] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<T_CTW_LEventTemplateEntity> result = new PagedQueryResult<T_CTW_LEventTemplateEntity>();
            List<T_CTW_LEventTemplateEntity> list = new List<T_CTW_LEventTemplateEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    T_CTW_LEventTemplateEntity m;
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
        public T_CTW_LEventTemplateEntity[] QueryByEntity(T_CTW_LEventTemplateEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<T_CTW_LEventTemplateEntity> PagedQueryByEntity(T_CTW_LEventTemplateEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(T_CTW_LEventTemplateEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.TemplateId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TemplateId", Value = pQueryEntity.TemplateId });
            if (pQueryEntity.TemplateName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TemplateName", Value = pQueryEntity.TemplateName });
            if (pQueryEntity.TemplateDesc!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TemplateDesc", Value = pQueryEntity.TemplateDesc });
            if (pQueryEntity.ActivityGroupId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ActivityGroupId", Value = pQueryEntity.ActivityGroupId });
            if (pQueryEntity.TemplateStatus!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TemplateStatus", Value = pQueryEntity.TemplateStatus });
            if (pQueryEntity.ImageId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ImageId", Value = pQueryEntity.ImageId });
            if (pQueryEntity.StartDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StartDate", Value = pQueryEntity.StartDate });
            if (pQueryEntity.EndDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EndDate", Value = pQueryEntity.EndDate });
            if (pQueryEntity.IsLongTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsLongTime", Value = pQueryEntity.IsLongTime });
            if (pQueryEntity.DisplayIndex!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DisplayIndex", Value = pQueryEntity.DisplayIndex });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.LastUpdateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.LastUpdateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.IsDelete!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out T_CTW_LEventTemplateEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new T_CTW_LEventTemplateEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["TemplateId"] != DBNull.Value)
			{
				pInstance.TemplateId =  (Guid)pReader["TemplateId"];
			}
			if (pReader["TemplateName"] != DBNull.Value)
			{
				pInstance.TemplateName =  Convert.ToString(pReader["TemplateName"]);
			}
			if (pReader["TemplateDesc"] != DBNull.Value)
			{
				pInstance.TemplateDesc =  Convert.ToString(pReader["TemplateDesc"]);
			}
			if (pReader["ActivityGroupId"] != DBNull.Value)
			{
				pInstance.ActivityGroupId =  (Guid)pReader["ActivityGroupId"];
			}
			if (pReader["TemplateStatus"] != DBNull.Value)
			{
				pInstance.TemplateStatus =   Convert.ToInt32(pReader["TemplateStatus"]);
			}
			if (pReader["ImageId"] != DBNull.Value)
			{
				pInstance.ImageId =  Convert.ToString(pReader["ImageId"]);
			}
			if (pReader["StartDate"] != DBNull.Value)
			{
				pInstance.StartDate =  Convert.ToDateTime(pReader["StartDate"]);
			}
			if (pReader["EndDate"] != DBNull.Value)
			{
				pInstance.EndDate =  Convert.ToDateTime(pReader["EndDate"]);
			}
			if (pReader["IsLongTime"] != DBNull.Value)
			{
				pInstance.IsLongTime =   Convert.ToInt32(pReader["IsLongTime"]);
			}
			if (pReader["DisplayIndex"] != DBNull.Value)
			{
				pInstance.DisplayIndex =   Convert.ToInt32(pReader["DisplayIndex"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}
			if (pReader["CreateBy"] != DBNull.Value)
			{
				pInstance.CreateBy =  Convert.ToString(pReader["CreateBy"]);
			}
			if (pReader["LastUpdateTime"] != DBNull.Value)
			{
				pInstance.LastUpdateTime =  Convert.ToDateTime(pReader["LastUpdateTime"]);
			}
			if (pReader["LastUpdateBy"] != DBNull.Value)
			{
				pInstance.LastUpdateBy =  Convert.ToString(pReader["LastUpdateBy"]);
			}
			if (pReader["IsDelete"] != DBNull.Value)
			{
				pInstance.IsDelete =   Convert.ToInt32(pReader["IsDelete"]);
			}

        }
        #endregion
    }
}
