/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/8/22 15:24:19
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
    /// 表WMaterialText的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class WMaterialTextDAO : Base.BaseCPOSDAO, ICRUDable<WMaterialTextEntity>, IQueryable<WMaterialTextEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public WMaterialTextDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(WMaterialTextEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(WMaterialTextEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [WMaterialText](");
            strSql.Append("[ParentTextId],[Title],[Author],[CoverImageUrl],[Text],[OriginalUrl],[DisplayIndex],[ApplicationId],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[TypeId],[PageId],[PageUrlJson],[PageParamJson],[IsAuth],[TextId])");
            strSql.Append(" values (");
            strSql.Append("@ParentTextId,@Title,@Author,@CoverImageUrl,@Text,@OriginalUrl,@DisplayIndex,@ApplicationId,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@TypeId,@PageId,@PageUrlJson,@PageParamJson,@IsAuth,@TextId)");

            string pkString = pEntity.TextId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@ParentTextId",SqlDbType.NVarChar),
					new SqlParameter("@Title",SqlDbType.NVarChar),
					new SqlParameter("@Author",SqlDbType.NVarChar),
					new SqlParameter("@CoverImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@Text",SqlDbType.NVarChar),
					new SqlParameter("@OriginalUrl",SqlDbType.NVarChar),
					new SqlParameter("@DisplayIndex",SqlDbType.Int),
					new SqlParameter("@ApplicationId",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@TypeId",SqlDbType.NVarChar),
					new SqlParameter("@PageId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@PageUrlJson",SqlDbType.NVarChar),
					new SqlParameter("@PageParamJson",SqlDbType.NVarChar),
					new SqlParameter("@IsAuth",SqlDbType.Int),
					new SqlParameter("@TextId",SqlDbType.NVarChar)
            };
            parameters[0].Value = pEntity.ParentTextId;
            parameters[1].Value = pEntity.Title;
            parameters[2].Value = pEntity.Author;
            parameters[3].Value = pEntity.CoverImageUrl;
            parameters[4].Value = pEntity.Text;
            parameters[5].Value = pEntity.OriginalUrl;
            parameters[6].Value = pEntity.DisplayIndex;
            parameters[7].Value = pEntity.ApplicationId;
            parameters[8].Value = pEntity.CreateTime;
            parameters[9].Value = pEntity.CreateBy;
            parameters[10].Value = pEntity.LastUpdateBy;
            parameters[11].Value = pEntity.LastUpdateTime;
            parameters[12].Value = pEntity.IsDelete;
            parameters[13].Value = pEntity.TypeId;
            parameters[14].Value = pEntity.PageId;
            parameters[15].Value = pEntity.PageUrlJson;
            parameters[16].Value = pEntity.PageParamJson;
            parameters[17].Value = pEntity.IsAuth;
            parameters[18].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            pEntity.TextId = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public WMaterialTextEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WMaterialText] where TextId='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            WMaterialTextEntity m = null;
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
        public WMaterialTextEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WMaterialText] where isdelete=0");
            //读取数据
            List<WMaterialTextEntity> list = new List<WMaterialTextEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WMaterialTextEntity m;
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
        public void Update(WMaterialTextEntity pEntity, IDbTransaction pTran)
        {
            Update(pEntity, true, pTran);
        }
        public void Update(WMaterialTextEntity pEntity, bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.TextId == null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
            //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [WMaterialText] set ");
            if (pIsUpdateNullField || pEntity.ParentTextId != null)
                strSql.Append("[ParentTextId]=@ParentTextId,");
            if (pIsUpdateNullField || pEntity.Title != null)
                strSql.Append("[Title]=@Title,");
            if (pIsUpdateNullField || pEntity.Author != null)
                strSql.Append("[Author]=@Author,");
            if (pIsUpdateNullField || pEntity.CoverImageUrl != null)
                strSql.Append("[CoverImageUrl]=@CoverImageUrl,");
            if (pIsUpdateNullField || pEntity.Text != null)
                strSql.Append("[Text]=@Text,");
            if (pIsUpdateNullField || pEntity.OriginalUrl != null)
                strSql.Append("[OriginalUrl]=@OriginalUrl,");
            if (pIsUpdateNullField || pEntity.DisplayIndex != null)
                strSql.Append("[DisplayIndex]=@DisplayIndex,");
            if (pIsUpdateNullField || pEntity.ApplicationId != null)
                strSql.Append("[ApplicationId]=@ApplicationId,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy != null)
                strSql.Append("[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime != null)
                strSql.Append("[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.TypeId != null)
                strSql.Append("[TypeId]=@TypeId,");
            if (pIsUpdateNullField || pEntity.PageId != null)
                strSql.Append("[PageId]=@PageId,");
            if (pIsUpdateNullField || pEntity.PageUrlJson != null)
                strSql.Append("[PageUrlJson]=@PageUrlJson,");
            if (pIsUpdateNullField || pEntity.PageParamJson != null)
                strSql.Append("[PageParamJson]=@PageParamJson,");
            if (pIsUpdateNullField || pEntity.IsAuth != null)
                strSql.Append("[IsAuth]=@IsAuth");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where TextId=@TextId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@ParentTextId",SqlDbType.NVarChar),
					new SqlParameter("@Title",SqlDbType.NVarChar),
					new SqlParameter("@Author",SqlDbType.NVarChar),
					new SqlParameter("@CoverImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@Text",SqlDbType.NVarChar),
					new SqlParameter("@OriginalUrl",SqlDbType.NVarChar),
					new SqlParameter("@DisplayIndex",SqlDbType.Int),
					new SqlParameter("@ApplicationId",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@TypeId",SqlDbType.NVarChar),
					new SqlParameter("@PageId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@PageUrlJson",SqlDbType.NVarChar),
					new SqlParameter("@PageParamJson",SqlDbType.NVarChar),
					new SqlParameter("@IsAuth",SqlDbType.Int),
					new SqlParameter("@TextId",SqlDbType.NVarChar)
            };
            parameters[0].Value = pEntity.ParentTextId;
            parameters[1].Value = pEntity.Title;
            parameters[2].Value = pEntity.Author;
            parameters[3].Value = pEntity.CoverImageUrl;
            parameters[4].Value = pEntity.Text;
            parameters[5].Value = pEntity.OriginalUrl;
            parameters[6].Value = pEntity.DisplayIndex;
            parameters[7].Value = pEntity.ApplicationId;
            parameters[8].Value = pEntity.LastUpdateBy;
            parameters[9].Value = pEntity.LastUpdateTime;
            parameters[10].Value = pEntity.TypeId;
            parameters[11].Value = pEntity.PageId;
            parameters[12].Value = pEntity.PageUrlJson;
            parameters[13].Value = pEntity.PageParamJson;
            parameters[14].Value = pEntity.IsAuth;
            parameters[15].Value = pEntity.TextId;

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
        public void Update(WMaterialTextEntity pEntity)
        {
            Update(pEntity, true);
        }
        public void Update(WMaterialTextEntity pEntity, bool pIsUpdateNullField)
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(WMaterialTextEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(WMaterialTextEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.TextId == null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.TextId, pTran);
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
            sql.AppendLine("update [WMaterialText] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where TextId=@TextId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@TextId",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(WMaterialTextEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.TextId == null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.TextId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(WMaterialTextEntity[] pEntities)
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
            sql.AppendLine("update [WMaterialText] set LastUpdateTime='" + DateTime.Now.ToString() + "',LastUpdateBy='" + CurrentUserInfo.UserID + "',IsDelete=1 where TextId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public WMaterialTextEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WMaterialText] where isdelete=0 ");
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
            List<WMaterialTextEntity> list = new List<WMaterialTextEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WMaterialTextEntity m;
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
        public PagedQueryResult<WMaterialTextEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [TextId] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [WMaterialText] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [WMaterialText] where isdelete=0 ");
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
            PagedQueryResult<WMaterialTextEntity> result = new PagedQueryResult<WMaterialTextEntity>();
            List<WMaterialTextEntity> list = new List<WMaterialTextEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    WMaterialTextEntity m;
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
        public WMaterialTextEntity[] QueryByEntity(WMaterialTextEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<WMaterialTextEntity> PagedQueryByEntity(WMaterialTextEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(WMaterialTextEntity pQueryEntity)
        {
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.TextId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TextId", Value = pQueryEntity.TextId });
            if (pQueryEntity.ParentTextId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ParentTextId", Value = pQueryEntity.ParentTextId });
            if (pQueryEntity.Title != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Title", Value = pQueryEntity.Title });
            if (pQueryEntity.Author != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Author", Value = pQueryEntity.Author });
            if (pQueryEntity.CoverImageUrl != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CoverImageUrl", Value = pQueryEntity.CoverImageUrl });
            if (pQueryEntity.Text != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Text", Value = pQueryEntity.Text });
            if (pQueryEntity.OriginalUrl != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OriginalUrl", Value = pQueryEntity.OriginalUrl });
            if (pQueryEntity.DisplayIndex != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DisplayIndex", Value = pQueryEntity.DisplayIndex });
            if (pQueryEntity.ApplicationId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ApplicationId", Value = pQueryEntity.ApplicationId });
            if (pQueryEntity.CreateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.LastUpdateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.LastUpdateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.IsDelete != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });
            if (pQueryEntity.TypeId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TypeId", Value = pQueryEntity.TypeId });
            if (pQueryEntity.PageId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PageId", Value = pQueryEntity.PageId });
            if (pQueryEntity.PageUrlJson != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PageUrlJson", Value = pQueryEntity.PageUrlJson });
            if (pQueryEntity.PageParamJson != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PageParamJson", Value = pQueryEntity.PageParamJson });
            if (pQueryEntity.IsAuth != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsAuth", Value = pQueryEntity.IsAuth });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out WMaterialTextEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new WMaterialTextEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["TextId"] != DBNull.Value)
            {
                pInstance.TextId = Convert.ToString(pReader["TextId"]);
            }
            if (pReader["ParentTextId"] != DBNull.Value)
            {
                pInstance.ParentTextId = Convert.ToString(pReader["ParentTextId"]);
            }
            if (pReader["Title"] != DBNull.Value)
            {
                pInstance.Title = Convert.ToString(pReader["Title"]);
            }
            if (pReader["Author"] != DBNull.Value)
            {
                pInstance.Author = Convert.ToString(pReader["Author"]);
            }
            if (pReader["CoverImageUrl"] != DBNull.Value)
            {
                pInstance.CoverImageUrl = Convert.ToString(pReader["CoverImageUrl"]);
            }
            if (pReader["Text"] != DBNull.Value)
            {
                pInstance.Text = Convert.ToString(pReader["Text"]);
            }
            if (pReader["OriginalUrl"] != DBNull.Value)
            {
                pInstance.OriginalUrl = Convert.ToString(pReader["OriginalUrl"]);
            }
            if (pReader["DisplayIndex"] != DBNull.Value)
            {
                pInstance.DisplayIndex = Convert.ToInt32(pReader["DisplayIndex"]);
            }
            if (pReader["ApplicationId"] != DBNull.Value)
            {
                pInstance.ApplicationId = Convert.ToString(pReader["ApplicationId"]);
            }
            if (pReader["CreateTime"] != DBNull.Value)
            {
                pInstance.CreateTime = Convert.ToDateTime(pReader["CreateTime"]);
            }
            if (pReader["CreateBy"] != DBNull.Value)
            {
                pInstance.CreateBy = Convert.ToString(pReader["CreateBy"]);
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
            if (pReader["TypeId"] != DBNull.Value)
            {
                pInstance.TypeId = Convert.ToString(pReader["TypeId"]);
            }
            if (pReader["PageId"] != DBNull.Value)
            {
                pInstance.PageId = (Guid)pReader["PageId"];
            }
            if (pReader["PageUrlJson"] != DBNull.Value)
            {
                pInstance.PageUrlJson = Convert.ToString(pReader["PageUrlJson"]);
            }
            if (pReader["PageParamJson"] != DBNull.Value)
            {
                pInstance.PageParamJson = Convert.ToString(pReader["PageParamJson"]);
            }
            if (pReader["IsAuth"] != DBNull.Value)
            {
                pInstance.IsAuth = Convert.ToInt32(pReader["IsAuth"]);
            }

        }
        #endregion
    }
}
