/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/4/22 18:54:19
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
    /// 表LNews的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class LNewsDAO : Base.BaseCPOSDAO, ICRUDable<LNewsEntity>, IQueryable<LNewsEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public LNewsDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(LNewsEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(LNewsEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [LNews](");
            strSql.Append("[NewsType],[NewsTitle],[NewsSubTitle],[Intro],[Content],[PublishTime],[ContentUrl],[ImageUrl],[ThumbnailImageUrl],[APPId],[NewsLevel],[ParentNewsId],[IsDefault],[IsTop],[Author],[BrowseCount],[PraiseCount],[CollCount],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[CustomerId],[DisplayIndex],[NewsId])");
            strSql.Append(" values (");
            strSql.Append("@NewsType,@NewsTitle,@NewsSubTitle,@Intro,@Content,@PublishTime,@ContentUrl,@ImageUrl,@ThumbnailImageUrl,@APPId,@NewsLevel,@ParentNewsId,@IsDefault,@IsTop,@Author,@BrowseCount,@PraiseCount,@CollCount,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@CustomerId,@DisplayIndex,@NewsId)");            

			string pkString = pEntity.NewsId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@NewsType",SqlDbType.NVarChar),
					new SqlParameter("@NewsTitle",SqlDbType.NVarChar),
					new SqlParameter("@NewsSubTitle",SqlDbType.NVarChar),
					new SqlParameter("@Intro",SqlDbType.NVarChar),
					new SqlParameter("@Content",SqlDbType.NVarChar),
					new SqlParameter("@PublishTime",SqlDbType.DateTime),
					new SqlParameter("@ContentUrl",SqlDbType.NVarChar),
					new SqlParameter("@ImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@ThumbnailImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@APPId",SqlDbType.NVarChar),
					new SqlParameter("@NewsLevel",SqlDbType.Int),
					new SqlParameter("@ParentNewsId",SqlDbType.NVarChar),
					new SqlParameter("@IsDefault",SqlDbType.Int),
					new SqlParameter("@IsTop",SqlDbType.Int),
					new SqlParameter("@Author",SqlDbType.NVarChar),
					new SqlParameter("@BrowseCount",SqlDbType.Int),
					new SqlParameter("@PraiseCount",SqlDbType.Int),
					new SqlParameter("@CollCount",SqlDbType.Int),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@DisplayIndex",SqlDbType.Int),
					new SqlParameter("@NewsId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.NewsType;
			parameters[1].Value = pEntity.NewsTitle;
			parameters[2].Value = pEntity.NewsSubTitle;
			parameters[3].Value = pEntity.Intro;
			parameters[4].Value = pEntity.Content;
			parameters[5].Value = pEntity.PublishTime;
			parameters[6].Value = pEntity.ContentUrl;
			parameters[7].Value = pEntity.ImageUrl;
			parameters[8].Value = pEntity.ThumbnailImageUrl;
			parameters[9].Value = pEntity.APPId;
			parameters[10].Value = pEntity.NewsLevel;
			parameters[11].Value = pEntity.ParentNewsId;
			parameters[12].Value = pEntity.IsDefault;
			parameters[13].Value = pEntity.IsTop;
			parameters[14].Value = pEntity.Author;
			parameters[15].Value = pEntity.BrowseCount;
			parameters[16].Value = pEntity.PraiseCount;
			parameters[17].Value = pEntity.CollCount;
			parameters[18].Value = pEntity.CreateTime;
			parameters[19].Value = pEntity.CreateBy;
			parameters[20].Value = pEntity.LastUpdateBy;
			parameters[21].Value = pEntity.LastUpdateTime;
			parameters[22].Value = pEntity.IsDelete;
			parameters[23].Value = pEntity.CustomerId;
			parameters[24].Value = pEntity.DisplayIndex;
			parameters[25].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.NewsId = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public LNewsEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [LNews] where NewsId='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            LNewsEntity m = null;
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
        public LNewsEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [LNews] where isdelete=0");
            //读取数据
            List<LNewsEntity> list = new List<LNewsEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    LNewsEntity m;
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
        public void Update(LNewsEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(LNewsEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.NewsId==null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [LNews] set ");
            if (pIsUpdateNullField || pEntity.NewsType!=null)
                strSql.Append( "[NewsType]=@NewsType,");
            if (pIsUpdateNullField || pEntity.NewsTitle!=null)
                strSql.Append( "[NewsTitle]=@NewsTitle,");
            if (pIsUpdateNullField || pEntity.NewsSubTitle!=null)
                strSql.Append( "[NewsSubTitle]=@NewsSubTitle,");
            if (pIsUpdateNullField || pEntity.Intro!=null)
                strSql.Append( "[Intro]=@Intro,");
            if (pIsUpdateNullField || pEntity.Content!=null)
                strSql.Append( "[Content]=@Content,");
            if (pIsUpdateNullField || pEntity.PublishTime!=null)
                strSql.Append( "[PublishTime]=@PublishTime,");
            if (pIsUpdateNullField || pEntity.ContentUrl!=null)
                strSql.Append( "[ContentUrl]=@ContentUrl,");
            if (pIsUpdateNullField || pEntity.ImageUrl!=null)
                strSql.Append( "[ImageUrl]=@ImageUrl,");
            if (pIsUpdateNullField || pEntity.ThumbnailImageUrl!=null)
                strSql.Append( "[ThumbnailImageUrl]=@ThumbnailImageUrl,");
            if (pIsUpdateNullField || pEntity.APPId!=null)
                strSql.Append( "[APPId]=@APPId,");
            if (pIsUpdateNullField || pEntity.NewsLevel!=null)
                strSql.Append( "[NewsLevel]=@NewsLevel,");
            if (pIsUpdateNullField || pEntity.ParentNewsId!=null)
                strSql.Append( "[ParentNewsId]=@ParentNewsId,");
            if (pIsUpdateNullField || pEntity.IsDefault!=null)
                strSql.Append( "[IsDefault]=@IsDefault,");
            if (pIsUpdateNullField || pEntity.IsTop!=null)
                strSql.Append( "[IsTop]=@IsTop,");
            if (pIsUpdateNullField || pEntity.Author!=null)
                strSql.Append( "[Author]=@Author,");
            if (pIsUpdateNullField || pEntity.BrowseCount!=null)
                strSql.Append( "[BrowseCount]=@BrowseCount,");
            if (pIsUpdateNullField || pEntity.PraiseCount!=null)
                strSql.Append( "[PraiseCount]=@PraiseCount,");
            if (pIsUpdateNullField || pEntity.CollCount!=null)
                strSql.Append( "[CollCount]=@CollCount,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.DisplayIndex!=null)
                strSql.Append( "[DisplayIndex]=@DisplayIndex");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where NewsId=@NewsId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@NewsType",SqlDbType.NVarChar),
					new SqlParameter("@NewsTitle",SqlDbType.NVarChar),
					new SqlParameter("@NewsSubTitle",SqlDbType.NVarChar),
					new SqlParameter("@Intro",SqlDbType.NVarChar),
					new SqlParameter("@Content",SqlDbType.NVarChar),
					new SqlParameter("@PublishTime",SqlDbType.DateTime),
					new SqlParameter("@ContentUrl",SqlDbType.NVarChar),
					new SqlParameter("@ImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@ThumbnailImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@APPId",SqlDbType.NVarChar),
					new SqlParameter("@NewsLevel",SqlDbType.Int),
					new SqlParameter("@ParentNewsId",SqlDbType.NVarChar),
					new SqlParameter("@IsDefault",SqlDbType.Int),
					new SqlParameter("@IsTop",SqlDbType.Int),
					new SqlParameter("@Author",SqlDbType.NVarChar),
					new SqlParameter("@BrowseCount",SqlDbType.Int),
					new SqlParameter("@PraiseCount",SqlDbType.Int),
					new SqlParameter("@CollCount",SqlDbType.Int),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@DisplayIndex",SqlDbType.Int),
					new SqlParameter("@NewsId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.NewsType;
			parameters[1].Value = pEntity.NewsTitle;
			parameters[2].Value = pEntity.NewsSubTitle;
			parameters[3].Value = pEntity.Intro;
			parameters[4].Value = pEntity.Content;
			parameters[5].Value = pEntity.PublishTime;
			parameters[6].Value = pEntity.ContentUrl;
			parameters[7].Value = pEntity.ImageUrl;
			parameters[8].Value = pEntity.ThumbnailImageUrl;
			parameters[9].Value = pEntity.APPId;
			parameters[10].Value = pEntity.NewsLevel;
			parameters[11].Value = pEntity.ParentNewsId;
			parameters[12].Value = pEntity.IsDefault;
			parameters[13].Value = pEntity.IsTop;
			parameters[14].Value = pEntity.Author;
			parameters[15].Value = pEntity.BrowseCount;
			parameters[16].Value = pEntity.PraiseCount;
			parameters[17].Value = pEntity.CollCount;
			parameters[18].Value = pEntity.LastUpdateBy;
			parameters[19].Value = pEntity.LastUpdateTime;
			parameters[20].Value = pEntity.CustomerId;
			parameters[21].Value = pEntity.DisplayIndex;
			parameters[22].Value = pEntity.NewsId;

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
        public void Update(LNewsEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(LNewsEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(LNewsEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(LNewsEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.NewsId==null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.NewsId, pTran);           
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
            sql.AppendLine("update [LNews] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where NewsId=@NewsId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@NewsId",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(LNewsEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.NewsId==null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.NewsId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(LNewsEntity[] pEntities)
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
            sql.AppendLine("update [LNews] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where NewsId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public LNewsEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [LNews] where isdelete=0 ");
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
            List<LNewsEntity> list = new List<LNewsEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    LNewsEntity m;
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
        public PagedQueryResult<LNewsEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [NewsId] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [LNews] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [LNews] where isdelete=0 ");
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
            PagedQueryResult<LNewsEntity> result = new PagedQueryResult<LNewsEntity>();
            List<LNewsEntity> list = new List<LNewsEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    LNewsEntity m;
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
        public LNewsEntity[] QueryByEntity(LNewsEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<LNewsEntity> PagedQueryByEntity(LNewsEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(LNewsEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.NewsId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NewsId", Value = pQueryEntity.NewsId });
            if (pQueryEntity.NewsType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NewsType", Value = pQueryEntity.NewsType });
            if (pQueryEntity.NewsTitle!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NewsTitle", Value = pQueryEntity.NewsTitle });
            if (pQueryEntity.NewsSubTitle!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NewsSubTitle", Value = pQueryEntity.NewsSubTitle });
            if (pQueryEntity.Intro!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Intro", Value = pQueryEntity.Intro });
            if (pQueryEntity.Content!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Content", Value = pQueryEntity.Content });
            if (pQueryEntity.PublishTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PublishTime", Value = pQueryEntity.PublishTime });
            if (pQueryEntity.ContentUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ContentUrl", Value = pQueryEntity.ContentUrl });
            if (pQueryEntity.ImageUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ImageUrl", Value = pQueryEntity.ImageUrl });
            if (pQueryEntity.ThumbnailImageUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ThumbnailImageUrl", Value = pQueryEntity.ThumbnailImageUrl });
            if (pQueryEntity.APPId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "APPId", Value = pQueryEntity.APPId });
            if (pQueryEntity.NewsLevel!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NewsLevel", Value = pQueryEntity.NewsLevel });
            if (pQueryEntity.ParentNewsId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ParentNewsId", Value = pQueryEntity.ParentNewsId });
            if (pQueryEntity.IsDefault!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDefault", Value = pQueryEntity.IsDefault });
            if (pQueryEntity.IsTop!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsTop", Value = pQueryEntity.IsTop });
            if (pQueryEntity.Author!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Author", Value = pQueryEntity.Author });
            if (pQueryEntity.BrowseCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BrowseCount", Value = pQueryEntity.BrowseCount });
            if (pQueryEntity.PraiseCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PraiseCount", Value = pQueryEntity.PraiseCount });
            if (pQueryEntity.CollCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CollCount", Value = pQueryEntity.CollCount });
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
            if (pQueryEntity.DisplayIndex!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DisplayIndex", Value = pQueryEntity.DisplayIndex });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out LNewsEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new LNewsEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["NewsId"] != DBNull.Value)
			{
				pInstance.NewsId =  Convert.ToString(pReader["NewsId"]);
			}
			if (pReader["NewsType"] != DBNull.Value)
			{
				pInstance.NewsType =  Convert.ToString(pReader["NewsType"]);
			}
			if (pReader["NewsTitle"] != DBNull.Value)
			{
				pInstance.NewsTitle =  Convert.ToString(pReader["NewsTitle"]);
			}
			if (pReader["NewsSubTitle"] != DBNull.Value)
			{
				pInstance.NewsSubTitle =  Convert.ToString(pReader["NewsSubTitle"]);
			}
			if (pReader["Intro"] != DBNull.Value)
			{
				pInstance.Intro =  Convert.ToString(pReader["Intro"]);
			}
			if (pReader["Content"] != DBNull.Value)
			{
				pInstance.Content =  Convert.ToString(pReader["Content"]);
			}
			if (pReader["PublishTime"] != DBNull.Value)
			{
				pInstance.PublishTime =  Convert.ToDateTime(pReader["PublishTime"]);
			}
			if (pReader["ContentUrl"] != DBNull.Value)
			{
				pInstance.ContentUrl =  Convert.ToString(pReader["ContentUrl"]);
			}
			if (pReader["ImageUrl"] != DBNull.Value)
			{
				pInstance.ImageUrl =  Convert.ToString(pReader["ImageUrl"]);
			}
			if (pReader["ThumbnailImageUrl"] != DBNull.Value)
			{
				pInstance.ThumbnailImageUrl =  Convert.ToString(pReader["ThumbnailImageUrl"]);
			}
			if (pReader["APPId"] != DBNull.Value)
			{
				pInstance.APPId =  Convert.ToString(pReader["APPId"]);
			}
			if (pReader["NewsLevel"] != DBNull.Value)
			{
				pInstance.NewsLevel =   Convert.ToInt32(pReader["NewsLevel"]);
			}
			if (pReader["ParentNewsId"] != DBNull.Value)
			{
				pInstance.ParentNewsId =  Convert.ToString(pReader["ParentNewsId"]);
			}
			if (pReader["IsDefault"] != DBNull.Value)
			{
				pInstance.IsDefault =   Convert.ToInt32(pReader["IsDefault"]);
			}
			if (pReader["IsTop"] != DBNull.Value)
			{
				pInstance.IsTop =   Convert.ToInt32(pReader["IsTop"]);
			}
			if (pReader["Author"] != DBNull.Value)
			{
				pInstance.Author =  Convert.ToString(pReader["Author"]);
			}
			if (pReader["BrowseCount"] != DBNull.Value)
			{
				pInstance.BrowseCount =   Convert.ToInt32(pReader["BrowseCount"]);
			}
			if (pReader["PraiseCount"] != DBNull.Value)
			{
				pInstance.PraiseCount =   Convert.ToInt32(pReader["PraiseCount"]);
			}
			if (pReader["CollCount"] != DBNull.Value)
			{
				pInstance.CollCount =   Convert.ToInt32(pReader["CollCount"]);
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
			if (pReader["DisplayIndex"] != DBNull.Value)
			{
				pInstance.DisplayIndex =   Convert.ToInt32(pReader["DisplayIndex"]);
			}

        }
        #endregion
    }
}
