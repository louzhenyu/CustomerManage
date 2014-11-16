/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/8 16:41:23
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
    /// 表WKeywordReply的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class WKeywordReplyDAO : Base.BaseCPOSDAO, ICRUDable<WKeywordReplyEntity>, IQueryable<WKeywordReplyEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public WKeywordReplyDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(WKeywordReplyEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(WKeywordReplyEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [WKeywordReply](");
            strSql.Append("[Keyword],[ReplyType],[Text],[TextId],[VoiceId],[VideoId],[ImageId],[ApplicationId],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[ModelId],[DisplayIndex],[PageId],[PageUrlJson],[PageParamJson],[IsAuth],[BeLinkedType],[KeywordType],[ReplyId])");
            strSql.Append(" values (");
            strSql.Append("@Keyword,@ReplyType,@Text,@TextId,@VoiceId,@VideoId,@ImageId,@ApplicationId,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@ModelId,@DisplayIndex,@PageId,@PageUrlJson,@PageParamJson,@IsAuth,@BeLinkedType,@KeywordType,@ReplyId)");            

			string pkString = pEntity.ReplyId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@Keyword",SqlDbType.NVarChar),
					new SqlParameter("@ReplyType",SqlDbType.Int),
					new SqlParameter("@Text",SqlDbType.NVarChar),
					new SqlParameter("@TextId",SqlDbType.NVarChar),
					new SqlParameter("@VoiceId",SqlDbType.NVarChar),
					new SqlParameter("@VideoId",SqlDbType.NVarChar),
					new SqlParameter("@ImageId",SqlDbType.NVarChar),
					new SqlParameter("@ApplicationId",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@ModelId",SqlDbType.NVarChar),
					new SqlParameter("@DisplayIndex",SqlDbType.Int),
					new SqlParameter("@PageId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@PageUrlJson",SqlDbType.NVarChar),
					new SqlParameter("@PageParamJson",SqlDbType.NVarChar),
					new SqlParameter("@IsAuth",SqlDbType.Int),
					new SqlParameter("@BeLinkedType",SqlDbType.Int),
					new SqlParameter("@KeywordType",SqlDbType.Int),
					new SqlParameter("@ReplyId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.Keyword;
			parameters[1].Value = pEntity.ReplyType;
			parameters[2].Value = pEntity.Text;
			parameters[3].Value = pEntity.TextId;
			parameters[4].Value = pEntity.VoiceId;
			parameters[5].Value = pEntity.VideoId;
			parameters[6].Value = pEntity.ImageId;
			parameters[7].Value = pEntity.ApplicationId;
			parameters[8].Value = pEntity.CreateTime;
			parameters[9].Value = pEntity.CreateBy;
			parameters[10].Value = pEntity.LastUpdateBy;
			parameters[11].Value = pEntity.LastUpdateTime;
			parameters[12].Value = pEntity.IsDelete;
			parameters[13].Value = pEntity.ModelId;
			parameters[14].Value = pEntity.DisplayIndex;
			parameters[15].Value = pEntity.PageId;
			parameters[16].Value = pEntity.PageUrlJson;
			parameters[17].Value = pEntity.PageParamJson;
			parameters[18].Value = pEntity.IsAuth;
			parameters[19].Value = pEntity.BeLinkedType;
			parameters[20].Value = pEntity.KeywordType;
			parameters[21].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.ReplyId = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public WKeywordReplyEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WKeywordReply] where ReplyId='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            WKeywordReplyEntity m = null;
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
        public WKeywordReplyEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WKeywordReply] where isdelete=0");
            //读取数据
            List<WKeywordReplyEntity> list = new List<WKeywordReplyEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WKeywordReplyEntity m;
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
        public void Update(WKeywordReplyEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(WKeywordReplyEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ReplyId==null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [WKeywordReply] set ");
            if (pIsUpdateNullField || pEntity.Keyword!=null)
                strSql.Append( "[Keyword]=@Keyword,");
            if (pIsUpdateNullField || pEntity.ReplyType!=null)
                strSql.Append( "[ReplyType]=@ReplyType,");
            if (pIsUpdateNullField || pEntity.Text!=null)
                strSql.Append( "[Text]=@Text,");
            if (pIsUpdateNullField || pEntity.TextId!=null)
                strSql.Append( "[TextId]=@TextId,");
            if (pIsUpdateNullField || pEntity.VoiceId!=null)
                strSql.Append( "[VoiceId]=@VoiceId,");
            if (pIsUpdateNullField || pEntity.VideoId!=null)
                strSql.Append( "[VideoId]=@VideoId,");
            if (pIsUpdateNullField || pEntity.ImageId!=null)
                strSql.Append( "[ImageId]=@ImageId,");
            if (pIsUpdateNullField || pEntity.ApplicationId!=null)
                strSql.Append( "[ApplicationId]=@ApplicationId,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.ModelId!=null)
                strSql.Append( "[ModelId]=@ModelId,");
            if (pIsUpdateNullField || pEntity.DisplayIndex!=null)
                strSql.Append( "[DisplayIndex]=@DisplayIndex,");
            if (pIsUpdateNullField || pEntity.PageId!=null)
                strSql.Append( "[PageId]=@PageId,");
            if (pIsUpdateNullField || pEntity.PageUrlJson!=null)
                strSql.Append( "[PageUrlJson]=@PageUrlJson,");
            if (pIsUpdateNullField || pEntity.PageParamJson!=null)
                strSql.Append( "[PageParamJson]=@PageParamJson,");
            if (pIsUpdateNullField || pEntity.IsAuth!=null)
                strSql.Append( "[IsAuth]=@IsAuth,");
            if (pIsUpdateNullField || pEntity.BeLinkedType!=null)
                strSql.Append( "[BeLinkedType]=@BeLinkedType,");
            if (pIsUpdateNullField || pEntity.KeywordType!=null)
                strSql.Append( "[KeywordType]=@KeywordType");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where ReplyId=@ReplyId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@Keyword",SqlDbType.NVarChar),
					new SqlParameter("@ReplyType",SqlDbType.Int),
					new SqlParameter("@Text",SqlDbType.NVarChar),
					new SqlParameter("@TextId",SqlDbType.NVarChar),
					new SqlParameter("@VoiceId",SqlDbType.NVarChar),
					new SqlParameter("@VideoId",SqlDbType.NVarChar),
					new SqlParameter("@ImageId",SqlDbType.NVarChar),
					new SqlParameter("@ApplicationId",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@ModelId",SqlDbType.NVarChar),
					new SqlParameter("@DisplayIndex",SqlDbType.Int),
					new SqlParameter("@PageId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@PageUrlJson",SqlDbType.NVarChar),
					new SqlParameter("@PageParamJson",SqlDbType.NVarChar),
					new SqlParameter("@IsAuth",SqlDbType.Int),
					new SqlParameter("@BeLinkedType",SqlDbType.Int),
					new SqlParameter("@KeywordType",SqlDbType.Int),
					new SqlParameter("@ReplyId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.Keyword;
			parameters[1].Value = pEntity.ReplyType;
			parameters[2].Value = pEntity.Text;
			parameters[3].Value = pEntity.TextId;
			parameters[4].Value = pEntity.VoiceId;
			parameters[5].Value = pEntity.VideoId;
			parameters[6].Value = pEntity.ImageId;
			parameters[7].Value = pEntity.ApplicationId;
			parameters[8].Value = pEntity.LastUpdateBy;
			parameters[9].Value = pEntity.LastUpdateTime;
			parameters[10].Value = pEntity.ModelId;
			parameters[11].Value = pEntity.DisplayIndex;
			parameters[12].Value = pEntity.PageId;
			parameters[13].Value = pEntity.PageUrlJson;
			parameters[14].Value = pEntity.PageParamJson;
			parameters[15].Value = pEntity.IsAuth;
			parameters[16].Value = pEntity.BeLinkedType;
			parameters[17].Value = pEntity.KeywordType;
			parameters[18].Value = pEntity.ReplyId;

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
        public void Update(WKeywordReplyEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(WKeywordReplyEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(WKeywordReplyEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(WKeywordReplyEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ReplyId==null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.ReplyId, pTran);           
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
            sql.AppendLine("update [WKeywordReply] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where ReplyId=@ReplyId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@ReplyId",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(WKeywordReplyEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.ReplyId==null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.ReplyId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(WKeywordReplyEntity[] pEntities)
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
            sql.AppendLine("update [WKeywordReply] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where ReplyId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public WKeywordReplyEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WKeywordReply] where isdelete=0 ");
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
            List<WKeywordReplyEntity> list = new List<WKeywordReplyEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WKeywordReplyEntity m;
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
        public PagedQueryResult<WKeywordReplyEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [ReplyId] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [WKeywordReply] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [WKeywordReply] where isdelete=0 ");
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
            PagedQueryResult<WKeywordReplyEntity> result = new PagedQueryResult<WKeywordReplyEntity>();
            List<WKeywordReplyEntity> list = new List<WKeywordReplyEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    WKeywordReplyEntity m;
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
        public WKeywordReplyEntity[] QueryByEntity(WKeywordReplyEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<WKeywordReplyEntity> PagedQueryByEntity(WKeywordReplyEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(WKeywordReplyEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.ReplyId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReplyId", Value = pQueryEntity.ReplyId });
            if (pQueryEntity.Keyword!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Keyword", Value = pQueryEntity.Keyword });
            if (pQueryEntity.ReplyType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReplyType", Value = pQueryEntity.ReplyType });
            if (pQueryEntity.Text!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Text", Value = pQueryEntity.Text });
            if (pQueryEntity.TextId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TextId", Value = pQueryEntity.TextId });
            if (pQueryEntity.VoiceId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VoiceId", Value = pQueryEntity.VoiceId });
            if (pQueryEntity.VideoId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VideoId", Value = pQueryEntity.VideoId });
            if (pQueryEntity.ImageId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ImageId", Value = pQueryEntity.ImageId });
            if (pQueryEntity.ApplicationId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ApplicationId", Value = pQueryEntity.ApplicationId });
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
            if (pQueryEntity.ModelId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ModelId", Value = pQueryEntity.ModelId });
            if (pQueryEntity.DisplayIndex!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DisplayIndex", Value = pQueryEntity.DisplayIndex });
            if (pQueryEntity.PageId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PageId", Value = pQueryEntity.PageId });
            if (pQueryEntity.PageUrlJson!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PageUrlJson", Value = pQueryEntity.PageUrlJson });
            if (pQueryEntity.PageParamJson!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PageParamJson", Value = pQueryEntity.PageParamJson });
            if (pQueryEntity.IsAuth!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsAuth", Value = pQueryEntity.IsAuth });
            if (pQueryEntity.BeLinkedType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BeLinkedType", Value = pQueryEntity.BeLinkedType });
            if (pQueryEntity.KeywordType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "KeywordType", Value = pQueryEntity.KeywordType });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out WKeywordReplyEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new WKeywordReplyEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["ReplyId"] != DBNull.Value)
			{
				pInstance.ReplyId =  Convert.ToString(pReader["ReplyId"]);
			}
			if (pReader["Keyword"] != DBNull.Value)
			{
				pInstance.Keyword =  Convert.ToString(pReader["Keyword"]);
			}
			if (pReader["ReplyType"] != DBNull.Value)
			{
				pInstance.ReplyType =   Convert.ToInt32(pReader["ReplyType"]);
			}
			if (pReader["Text"] != DBNull.Value)
			{
				pInstance.Text =  Convert.ToString(pReader["Text"]);
			}
			if (pReader["TextId"] != DBNull.Value)
			{
				pInstance.TextId =  Convert.ToString(pReader["TextId"]);
			}
			if (pReader["VoiceId"] != DBNull.Value)
			{
				pInstance.VoiceId =  Convert.ToString(pReader["VoiceId"]);
			}
			if (pReader["VideoId"] != DBNull.Value)
			{
				pInstance.VideoId =  Convert.ToString(pReader["VideoId"]);
			}
			if (pReader["ImageId"] != DBNull.Value)
			{
				pInstance.ImageId =  Convert.ToString(pReader["ImageId"]);
			}
			if (pReader["ApplicationId"] != DBNull.Value)
			{
				pInstance.ApplicationId =  Convert.ToString(pReader["ApplicationId"]);
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
			if (pReader["ModelId"] != DBNull.Value)
			{
				pInstance.ModelId =  Convert.ToString(pReader["ModelId"]);
			}
			if (pReader["DisplayIndex"] != DBNull.Value)
			{
				pInstance.DisplayIndex =   Convert.ToInt32(pReader["DisplayIndex"]);
			}
			if (pReader["PageId"] != DBNull.Value)
			{
				pInstance.PageId =  (Guid)pReader["PageId"];
			}
			if (pReader["PageUrlJson"] != DBNull.Value)
			{
				pInstance.PageUrlJson =  Convert.ToString(pReader["PageUrlJson"]);
			}
			if (pReader["PageParamJson"] != DBNull.Value)
			{
				pInstance.PageParamJson =  Convert.ToString(pReader["PageParamJson"]);
			}
			if (pReader["IsAuth"] != DBNull.Value)
			{
				pInstance.IsAuth =   Convert.ToInt32(pReader["IsAuth"]);
			}
			if (pReader["BeLinkedType"] != DBNull.Value)
			{
				pInstance.BeLinkedType =   Convert.ToInt32(pReader["BeLinkedType"]);
			}
			if (pReader["KeywordType"] != DBNull.Value)
			{
				pInstance.KeywordType =   Convert.ToInt32(pReader["KeywordType"]);
			}

        }
        #endregion
    }
}
