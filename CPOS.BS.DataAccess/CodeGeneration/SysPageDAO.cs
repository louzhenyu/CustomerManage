/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/12 14:35:53
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
    /// 表SysPage的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class SysPageDAO : BaseCPOSDAO, ICRUDable<SysPageEntity>, IQueryable<SysPageEntity>
    {
        #region 构造函数
        public string StaticConnectionString { get; set; }
        private ISQLHelper staticSqlHelper;
        /// <summary>
        /// 构造函数 
        /// </summary>
        public SysPageDAO(LoggingSessionInfo pUserInfo, string connectionString)
            : base(pUserInfo)
        {
            this.StaticConnectionString = connectionString;
            this.SQLHelper = StaticSqlHelper;
        }
        #endregion

        protected ISQLHelper StaticSqlHelper
        {
            get
            {
                if (null == staticSqlHelper)
                    staticSqlHelper = new DefaultSQLHelper(StaticConnectionString);
                return staticSqlHelper;
            }
        }

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(SysPageEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(SysPageEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");

            //初始化固定字段
            pEntity.IsDelete = 0;
            pEntity.CreateTime = DateTime.Now;
            pEntity.LastUpdateTime = pEntity.CreateTime;
            pEntity.CreateBy = CurrentUserInfo.UserID;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [SysPage](");
            strSql.Append("[PageKey],[Title],[ModuleName],[IsEntrance],[URLTemplate],[JsonValue],[PageCode],[IsAuth],[IsRebuild],[Version],[Author],[DefaultHtml],[Remark],[CustomerID],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[CustomerVisible],[PageID])");
            strSql.Append(" values (");
            strSql.Append("@PageKey,@Title,@ModuleName,@IsEntrance,@URLTemplate,@JsonValue,@PageCode,@IsAuth,@IsRebuild,@Version,@Author,@DefaultHtml,@Remark,@CustomerID,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@CustomerVisible,@PageID)");            

            Guid? pkGuid;
            if (pEntity.PageID == null)
                pkGuid = Guid.NewGuid();
            else
                pkGuid = pEntity.PageID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@PageKey",SqlDbType.NVarChar),
					new SqlParameter("@Title",SqlDbType.NVarChar),
					new SqlParameter("@ModuleName",SqlDbType.NVarChar),
					new SqlParameter("@IsEntrance",SqlDbType.Int),
					new SqlParameter("@URLTemplate",SqlDbType.NVarChar),
					new SqlParameter("@JsonValue",SqlDbType.NVarChar),
					new SqlParameter("@PageCode",SqlDbType.NVarChar),
					new SqlParameter("@IsAuth",SqlDbType.Int),
					new SqlParameter("@IsRebuild",SqlDbType.Int),
					new SqlParameter("@Version",SqlDbType.NVarChar),
					new SqlParameter("@Author",SqlDbType.NVarChar),
					new SqlParameter("@DefaultHtml",SqlDbType.NVarChar),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerVisible",SqlDbType.Int),
					new SqlParameter("@PageID",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.PageKey;
            parameters[1].Value = pEntity.Title;
            parameters[2].Value = pEntity.ModuleName;
            parameters[3].Value = pEntity.IsEntrance;
            parameters[4].Value = pEntity.URLTemplate;
            parameters[5].Value = pEntity.JsonValue;
            parameters[6].Value = pEntity.PageCode;
            parameters[7].Value = pEntity.IsAuth;
            parameters[8].Value = pEntity.IsRebuild;
            parameters[9].Value = pEntity.Version;
            parameters[10].Value = pEntity.Author;
			parameters[11].Value = pEntity.DefaultHtml;
			parameters[12].Value = pEntity.Remark;
			parameters[13].Value = pEntity.CustomerID;
			parameters[14].Value = pEntity.CreateBy;
			parameters[15].Value = pEntity.CreateTime;
			parameters[16].Value = pEntity.LastUpdateBy;
			parameters[17].Value = pEntity.LastUpdateTime;
			parameters[18].Value = pEntity.IsDelete;
			parameters[19].Value = pEntity.CustomerVisible;
			parameters[20].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            pEntity.PageID = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public SysPageEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [SysPage] where PageID='{0}'  and isdelete=0 ", id.ToString());
            //读取数据
            SysPageEntity m = null;
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
        public SysPageEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [SysPage] where 1=1  and isdelete=0");
            //读取数据
            List<SysPageEntity> list = new List<SysPageEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    SysPageEntity m;
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
        public void Update(SysPageEntity pEntity, IDbTransaction pTran)
        {
            Update(pEntity, pTran, true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(SysPageEntity pEntity, IDbTransaction pTran, bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.PageID.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
            //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [SysPage] set ");
            if (pIsUpdateNullField || pEntity.PageKey != null)
                strSql.Append("[PageKey]=@PageKey,");
            if (pIsUpdateNullField || pEntity.Title != null)
                strSql.Append("[Title]=@Title,");
            if (pIsUpdateNullField || pEntity.ModuleName != null)
                strSql.Append("[ModuleName]=@ModuleName,");
            if (pIsUpdateNullField || pEntity.IsEntrance != null)
                strSql.Append("[IsEntrance]=@IsEntrance,");
            if (pIsUpdateNullField || pEntity.URLTemplate != null)
                strSql.Append("[URLTemplate]=@URLTemplate,");
            if (pIsUpdateNullField || pEntity.JsonValue != null)
                strSql.Append("[JsonValue]=@JsonValue,");
            if (pIsUpdateNullField || pEntity.PageCode != null)
                strSql.Append("[PageCode]=@PageCode,");
            if (pIsUpdateNullField || pEntity.IsAuth != null)
                strSql.Append("[IsAuth]=@IsAuth,");
            if (pIsUpdateNullField || pEntity.IsRebuild != null)
                strSql.Append("[IsRebuild]=@IsRebuild,");
            if (pIsUpdateNullField || pEntity.Version != null)
                strSql.Append("[Version]=@Version,");
            if (pIsUpdateNullField || pEntity.Author != null)
                strSql.Append("[Author]=@Author,");
            if (pIsUpdateNullField || pEntity.DefaultHtml!=null)
                strSql.Append( "[DefaultHtml]=@DefaultHtml,");
            if (pIsUpdateNullField || pEntity.Remark != null)
                strSql.Append("[Remark]=@Remark,");
            if (pIsUpdateNullField || pEntity.CustomerID != null)
                strSql.Append("[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy != null)
                strSql.Append("[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime != null)
                strSql.Append("[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.CustomerVisible!=null)
                strSql.Append( "[CustomerVisible]=@CustomerVisible");
            strSql.Append(" where PageID=@PageID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@PageKey",SqlDbType.NVarChar),
					new SqlParameter("@Title",SqlDbType.NVarChar),
					new SqlParameter("@ModuleName",SqlDbType.NVarChar),
					new SqlParameter("@IsEntrance",SqlDbType.Int),
					new SqlParameter("@URLTemplate",SqlDbType.NVarChar),
					new SqlParameter("@JsonValue",SqlDbType.NVarChar),
					new SqlParameter("@PageCode",SqlDbType.NVarChar),
					new SqlParameter("@IsAuth",SqlDbType.Int),
					new SqlParameter("@IsRebuild",SqlDbType.Int),
					new SqlParameter("@Version",SqlDbType.NVarChar),
					new SqlParameter("@Author",SqlDbType.NVarChar),
					new SqlParameter("@DefaultHtml",SqlDbType.NVarChar),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerVisible",SqlDbType.Int),
					new SqlParameter("@PageID",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.PageKey;
            parameters[1].Value = pEntity.Title;
            parameters[2].Value = pEntity.ModuleName;
            parameters[3].Value = pEntity.IsEntrance;
            parameters[4].Value = pEntity.URLTemplate;
            parameters[5].Value = pEntity.JsonValue;
            parameters[6].Value = pEntity.PageCode;
            parameters[7].Value = pEntity.IsAuth;
            parameters[8].Value = pEntity.IsRebuild;
            parameters[9].Value = pEntity.Version;
            parameters[10].Value = pEntity.Author;
			parameters[11].Value = pEntity.DefaultHtml;
			parameters[12].Value = pEntity.Remark;
			parameters[13].Value = pEntity.CustomerID;
			parameters[14].Value = pEntity.LastUpdateBy;
			parameters[15].Value = pEntity.LastUpdateTime;
			parameters[16].Value = pEntity.CustomerVisible;
			parameters[17].Value = pEntity.PageID;

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
        public void Update(SysPageEntity pEntity)
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(SysPageEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(SysPageEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.PageID.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.PageID.Value, pTran);
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
            sql.AppendLine("update [SysPage] set  isdelete=1 where PageID=@PageID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@PageID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(SysPageEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.PageID.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.PageID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(SysPageEntity[] pEntities)
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
            sql.AppendLine("update [SysPage] set  isdelete=1 where PageID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public SysPageEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [SysPage] where 1=1  and isdelete=0 ");
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
            List<SysPageEntity> list = new List<SysPageEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    SysPageEntity m;
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
        public PagedQueryResult<SysPageEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [PageID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [SysPage] where 1=1  and isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [SysPage] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<SysPageEntity> result = new PagedQueryResult<SysPageEntity>();
            List<SysPageEntity> list = new List<SysPageEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    SysPageEntity m;
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
        public SysPageEntity[] QueryByEntity(SysPageEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<SysPageEntity> PagedQueryByEntity(SysPageEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(SysPageEntity pQueryEntity)
        {
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.PageID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PageID", Value = pQueryEntity.PageID });
            if (pQueryEntity.PageKey != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PageKey", Value = pQueryEntity.PageKey });
            if (pQueryEntity.Title != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Title", Value = pQueryEntity.Title });
            if (pQueryEntity.ModuleName != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ModuleName", Value = pQueryEntity.ModuleName });
            if (pQueryEntity.IsEntrance != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsEntrance", Value = pQueryEntity.IsEntrance });
            if (pQueryEntity.URLTemplate != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "URLTemplate", Value = pQueryEntity.URLTemplate });
            if (pQueryEntity.JsonValue != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "JsonValue", Value = pQueryEntity.JsonValue });
            if (pQueryEntity.PageCode != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PageCode", Value = pQueryEntity.PageCode });
            if (pQueryEntity.IsAuth != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsAuth", Value = pQueryEntity.IsAuth });
            if (pQueryEntity.IsRebuild != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsRebuild", Value = pQueryEntity.IsRebuild });
            if (pQueryEntity.Version != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Version", Value = pQueryEntity.Version });
            if (pQueryEntity.Author != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Author", Value = pQueryEntity.Author });
            if (pQueryEntity.DefaultHtml!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DefaultHtml", Value = pQueryEntity.DefaultHtml });
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
            if (pQueryEntity.CustomerVisible!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerVisible", Value = pQueryEntity.CustomerVisible });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out SysPageEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new SysPageEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["PageID"] != DBNull.Value)
            {
                pInstance.PageID = (Guid)pReader["PageID"];
            }
            if (pReader["PageKey"] != DBNull.Value)
            {
                pInstance.PageKey = Convert.ToString(pReader["PageKey"]);
            }
            if (pReader["Title"] != DBNull.Value)
            {
                pInstance.Title = Convert.ToString(pReader["Title"]);
            }
            if (pReader["ModuleName"] != DBNull.Value)
            {
                pInstance.ModuleName = Convert.ToString(pReader["ModuleName"]);
            }
            if (pReader["IsEntrance"] != DBNull.Value)
            {
                pInstance.IsEntrance = Convert.ToInt32(pReader["IsEntrance"]);
            }
            if (pReader["URLTemplate"] != DBNull.Value)
            {
                pInstance.URLTemplate = Convert.ToString(pReader["URLTemplate"]);
            }
            if (pReader["JsonValue"] != DBNull.Value)
            {
                pInstance.JsonValue = Convert.ToString(pReader["JsonValue"]);
            }
            if (pReader["PageCode"] != DBNull.Value)
            {
                pInstance.PageCode = Convert.ToString(pReader["PageCode"]);
            }
            if (pReader["IsAuth"] != DBNull.Value)
            {
                pInstance.IsAuth = Convert.ToInt32(pReader["IsAuth"]);
            }
            if (pReader["IsRebuild"] != DBNull.Value)
            {
                pInstance.IsRebuild = Convert.ToInt32(pReader["IsRebuild"]);
            }
            if (pReader["Version"] != DBNull.Value)
            {
                pInstance.Version = Convert.ToString(pReader["Version"]);
            }
            if (pReader["Author"] != DBNull.Value)
            {
                pInstance.Author = Convert.ToString(pReader["Author"]);
            }
			if (pReader["DefaultHtml"] != DBNull.Value)
			{
				pInstance.DefaultHtml =  Convert.ToString(pReader["DefaultHtml"]);
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
			if (pReader["CustomerVisible"] != DBNull.Value)
			{
				pInstance.CustomerVisible =   Convert.ToInt32(pReader["CustomerVisible"]);
			}

        }
        #endregion
    }
}
