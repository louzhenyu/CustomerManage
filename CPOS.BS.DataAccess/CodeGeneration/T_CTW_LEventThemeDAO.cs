/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/4/12 16:22:15
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
    /// 表T_CTW_LEventTheme的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_CTW_LEventThemeDAO : Base.BaseCPOSDAO, ICRUDable<T_CTW_LEventThemeEntity>, IQueryable<T_CTW_LEventThemeEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_CTW_LEventThemeDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(T_CTW_LEventThemeEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(T_CTW_LEventThemeEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [T_CTW_LEventTheme](");
            strSql.Append("[OriginalThemeId],[CTWEventId],[ThemeName],[H5Url],[H5TemplateId],[WorksId],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[CustomerId],[IsDelete],[ThemeId])");
            strSql.Append(" values (");
            strSql.Append("@OriginalThemeId,@CTWEventId,@ThemeName,@H5Url,@H5TemplateId,@WorksId,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@CustomerId,@IsDelete,@ThemeId)");            

			Guid? pkGuid;
			if (pEntity.ThemeId == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.ThemeId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@OriginalThemeId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@CTWEventId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@ThemeName",SqlDbType.NVarChar),
					new SqlParameter("@H5Url",SqlDbType.VarChar),
					new SqlParameter("@H5TemplateId",SqlDbType.VarChar),
					new SqlParameter("@WorksId",SqlDbType.VarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@CustomerId",SqlDbType.VarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@ThemeId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.OriginalThemeId;
			parameters[1].Value = pEntity.CTWEventId;
			parameters[2].Value = pEntity.ThemeName;
			parameters[3].Value = pEntity.H5Url;
			parameters[4].Value = pEntity.H5TemplateId;
			parameters[5].Value = pEntity.WorksId;
			parameters[6].Value = pEntity.CreateTime;
			parameters[7].Value = pEntity.CreateBy;
			parameters[8].Value = pEntity.LastUpdateTime;
			parameters[9].Value = pEntity.LastUpdateBy;
			parameters[10].Value = pEntity.CustomerId;
			parameters[11].Value = pEntity.IsDelete;
			parameters[12].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.ThemeId = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public T_CTW_LEventThemeEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_CTW_LEventTheme] where ThemeId='{0}'  and isdelete=0 ", id.ToString());
            //读取数据
            T_CTW_LEventThemeEntity m = null;
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
        public T_CTW_LEventThemeEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_CTW_LEventTheme] where 1=1  and isdelete=0");
            //读取数据
            List<T_CTW_LEventThemeEntity> list = new List<T_CTW_LEventThemeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_CTW_LEventThemeEntity m;
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
        public void Update(T_CTW_LEventThemeEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(T_CTW_LEventThemeEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.ThemeId.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_CTW_LEventTheme] set ");
                        if (pIsUpdateNullField || pEntity.OriginalThemeId!=null)
                strSql.Append( "[OriginalThemeId]=@OriginalThemeId,");
            if (pIsUpdateNullField || pEntity.CTWEventId!=null)
                strSql.Append( "[CTWEventId]=@CTWEventId,");
            if (pIsUpdateNullField || pEntity.ThemeName!=null)
                strSql.Append( "[ThemeName]=@ThemeName,");
            if (pIsUpdateNullField || pEntity.H5Url!=null)
                strSql.Append( "[H5Url]=@H5Url,");
            if (pIsUpdateNullField || pEntity.H5TemplateId!=null)
                strSql.Append( "[H5TemplateId]=@H5TemplateId,");
            if (pIsUpdateNullField || pEntity.WorksId!=null)
                strSql.Append( "[WorksId]=@WorksId,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId");
            strSql.Append(" where ThemeId=@ThemeId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@OriginalThemeId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@CTWEventId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@ThemeName",SqlDbType.NVarChar),
					new SqlParameter("@H5Url",SqlDbType.VarChar),
					new SqlParameter("@H5TemplateId",SqlDbType.VarChar),
					new SqlParameter("@WorksId",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@CustomerId",SqlDbType.VarChar),
					new SqlParameter("@ThemeId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.OriginalThemeId;
			parameters[1].Value = pEntity.CTWEventId;
			parameters[2].Value = pEntity.ThemeName;
			parameters[3].Value = pEntity.H5Url;
			parameters[4].Value = pEntity.H5TemplateId;
			parameters[5].Value = pEntity.WorksId;
			parameters[6].Value = pEntity.LastUpdateTime;
			parameters[7].Value = pEntity.LastUpdateBy;
			parameters[8].Value = pEntity.CustomerId;
			parameters[9].Value = pEntity.ThemeId;

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
        public void Update(T_CTW_LEventThemeEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(T_CTW_LEventThemeEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(T_CTW_LEventThemeEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.ThemeId.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.ThemeId.Value, pTran);           
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
            sql.AppendLine("update [T_CTW_LEventTheme] set  isdelete=1 where ThemeId=@ThemeId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@ThemeId",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(T_CTW_LEventThemeEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.ThemeId.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.ThemeId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(T_CTW_LEventThemeEntity[] pEntities)
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
            sql.AppendLine("update [T_CTW_LEventTheme] set  isdelete=1 where ThemeId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public T_CTW_LEventThemeEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_CTW_LEventTheme] where 1=1  and isdelete=0 ");
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
            List<T_CTW_LEventThemeEntity> list = new List<T_CTW_LEventThemeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_CTW_LEventThemeEntity m;
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
        public PagedQueryResult<T_CTW_LEventThemeEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [ThemeId] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [T_CTW_LEventTheme] where 1=1  and isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [T_CTW_LEventTheme] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<T_CTW_LEventThemeEntity> result = new PagedQueryResult<T_CTW_LEventThemeEntity>();
            List<T_CTW_LEventThemeEntity> list = new List<T_CTW_LEventThemeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    T_CTW_LEventThemeEntity m;
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
        public T_CTW_LEventThemeEntity[] QueryByEntity(T_CTW_LEventThemeEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<T_CTW_LEventThemeEntity> PagedQueryByEntity(T_CTW_LEventThemeEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(T_CTW_LEventThemeEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.ThemeId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ThemeId", Value = pQueryEntity.ThemeId });
            if (pQueryEntity.OriginalThemeId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OriginalThemeId", Value = pQueryEntity.OriginalThemeId });
            if (pQueryEntity.CTWEventId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CTWEventId", Value = pQueryEntity.CTWEventId });
            if (pQueryEntity.ThemeName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ThemeName", Value = pQueryEntity.ThemeName });
            if (pQueryEntity.H5Url!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "H5Url", Value = pQueryEntity.H5Url });
            if (pQueryEntity.H5TemplateId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "H5TemplateId", Value = pQueryEntity.H5TemplateId });
            if (pQueryEntity.WorksId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WorksId", Value = pQueryEntity.WorksId });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.LastUpdateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.LastUpdateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
            if (pQueryEntity.IsDelete!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out T_CTW_LEventThemeEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new T_CTW_LEventThemeEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["ThemeId"] != DBNull.Value)
			{
				pInstance.ThemeId =  (Guid)pReader["ThemeId"];
			}
			if (pReader["OriginalThemeId"] != DBNull.Value)
			{
				pInstance.OriginalThemeId =  (Guid)pReader["OriginalThemeId"];
			}
			if (pReader["CTWEventId"] != DBNull.Value)
			{
				pInstance.CTWEventId =  (Guid)pReader["CTWEventId"];
			}
			if (pReader["ThemeName"] != DBNull.Value)
			{
				pInstance.ThemeName =  Convert.ToString(pReader["ThemeName"]);
			}
			if (pReader["H5Url"] != DBNull.Value)
			{
				pInstance.H5Url =  Convert.ToString(pReader["H5Url"]);
			}
			if (pReader["H5TemplateId"] != DBNull.Value)
			{
				pInstance.H5TemplateId =  Convert.ToString(pReader["H5TemplateId"]);
			}
			if (pReader["WorksId"] != DBNull.Value)
			{
				pInstance.WorksId =  Convert.ToString(pReader["WorksId"]);
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
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}
			if (pReader["IsDelete"] != DBNull.Value)
			{
				pInstance.IsDelete =   Convert.ToInt32(pReader["IsDelete"]);
			}

        }
        #endregion
    }
}
