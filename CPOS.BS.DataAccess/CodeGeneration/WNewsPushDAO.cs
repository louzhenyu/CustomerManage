/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/15 18:13:03
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
    /// 表WNewsPush的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class WNewsPushDAO : Base.BaseCPOSDAO, ICRUDable<WNewsPushEntity>, IQueryable<WNewsPushEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public WNewsPushDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(WNewsPushEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(WNewsPushEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [WNewsPush](");
            strSql.Append("[WeiXinID],[MsgType],[Content],[PicUrl],[Location_X],[Location_Y],[Scale],[Title],[Description],[Url],[AnswerMsgType],[AnswerContent],[AnswerMusicUrl],[AnswerHQMusicUrl],[AnswerArticleCount],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[NewsPushID])");
            strSql.Append(" values (");
            strSql.Append("@WeiXinID,@MsgType,@Content,@PicUrl,@LocationX,@LocationY,@Scale,@Title,@Description,@Url,@AnswerMsgType,@AnswerContent,@AnswerMusicUrl,@AnswerHQMusicUrl,@AnswerArticleCount,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@NewsPushID)");            

			string pkString = pEntity.NewsPushID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@WeiXinID",SqlDbType.NVarChar),
					new SqlParameter("@MsgType",SqlDbType.NVarChar),
					new SqlParameter("@Content",SqlDbType.NVarChar),
					new SqlParameter("@PicUrl",SqlDbType.NVarChar),
					new SqlParameter("@LocationX",SqlDbType.NVarChar),
					new SqlParameter("@LocationY",SqlDbType.NVarChar),
					new SqlParameter("@Scale",SqlDbType.NVarChar),
					new SqlParameter("@Title",SqlDbType.NVarChar),
					new SqlParameter("@Description",SqlDbType.NVarChar),
					new SqlParameter("@Url",SqlDbType.NVarChar),
					new SqlParameter("@AnswerMsgType",SqlDbType.NVarChar),
					new SqlParameter("@AnswerContent",SqlDbType.NVarChar),
					new SqlParameter("@AnswerMusicUrl",SqlDbType.NVarChar),
					new SqlParameter("@AnswerHQMusicUrl",SqlDbType.NVarChar),
					new SqlParameter("@AnswerArticleCount",SqlDbType.Int),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@NewsPushID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.WeiXinID;
			parameters[1].Value = pEntity.MsgType;
			parameters[2].Value = pEntity.Content;
			parameters[3].Value = pEntity.PicUrl;
			parameters[4].Value = pEntity.LocationX;
			parameters[5].Value = pEntity.LocationY;
			parameters[6].Value = pEntity.Scale;
			parameters[7].Value = pEntity.Title;
			parameters[8].Value = pEntity.Description;
			parameters[9].Value = pEntity.Url;
			parameters[10].Value = pEntity.AnswerMsgType;
			parameters[11].Value = pEntity.AnswerContent;
			parameters[12].Value = pEntity.AnswerMusicUrl;
			parameters[13].Value = pEntity.AnswerHQMusicUrl;
			parameters[14].Value = pEntity.AnswerArticleCount;
			parameters[15].Value = pEntity.CreateTime;
			parameters[16].Value = pEntity.CreateBy;
			parameters[17].Value = pEntity.LastUpdateBy;
			parameters[18].Value = pEntity.LastUpdateTime;
			parameters[19].Value = pEntity.IsDelete;
			parameters[20].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.NewsPushID = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public WNewsPushEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WNewsPush] where NewsPushID='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            WNewsPushEntity m = null;
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
        public WNewsPushEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WNewsPush] where isdelete=0");
            //读取数据
            List<WNewsPushEntity> list = new List<WNewsPushEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WNewsPushEntity m;
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
        public void Update(WNewsPushEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(WNewsPushEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.NewsPushID==null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [WNewsPush] set ");
            if (pIsUpdateNullField || pEntity.WeiXinID!=null)
                strSql.Append( "[WeiXinID]=@WeiXinID,");
            if (pIsUpdateNullField || pEntity.MsgType!=null)
                strSql.Append( "[MsgType]=@MsgType,");
            if (pIsUpdateNullField || pEntity.Content!=null)
                strSql.Append( "[Content]=@Content,");
            if (pIsUpdateNullField || pEntity.PicUrl!=null)
                strSql.Append( "[PicUrl]=@PicUrl,");
            if (pIsUpdateNullField || pEntity.LocationX!=null)
                strSql.Append( "[Location_X]=@LocationX,");
            if (pIsUpdateNullField || pEntity.LocationY!=null)
                strSql.Append( "[Location_Y]=@LocationY,");
            if (pIsUpdateNullField || pEntity.Scale!=null)
                strSql.Append( "[Scale]=@Scale,");
            if (pIsUpdateNullField || pEntity.Title!=null)
                strSql.Append( "[Title]=@Title,");
            if (pIsUpdateNullField || pEntity.Description!=null)
                strSql.Append( "[Description]=@Description,");
            if (pIsUpdateNullField || pEntity.Url!=null)
                strSql.Append( "[Url]=@Url,");
            if (pIsUpdateNullField || pEntity.AnswerMsgType!=null)
                strSql.Append( "[AnswerMsgType]=@AnswerMsgType,");
            if (pIsUpdateNullField || pEntity.AnswerContent!=null)
                strSql.Append( "[AnswerContent]=@AnswerContent,");
            if (pIsUpdateNullField || pEntity.AnswerMusicUrl!=null)
                strSql.Append( "[AnswerMusicUrl]=@AnswerMusicUrl,");
            if (pIsUpdateNullField || pEntity.AnswerHQMusicUrl!=null)
                strSql.Append( "[AnswerHQMusicUrl]=@AnswerHQMusicUrl,");
            if (pIsUpdateNullField || pEntity.AnswerArticleCount!=null)
                strSql.Append( "[AnswerArticleCount]=@AnswerArticleCount,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where NewsPushID=@NewsPushID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@WeiXinID",SqlDbType.NVarChar),
					new SqlParameter("@MsgType",SqlDbType.NVarChar),
					new SqlParameter("@Content",SqlDbType.NVarChar),
					new SqlParameter("@PicUrl",SqlDbType.NVarChar),
					new SqlParameter("@LocationX",SqlDbType.NVarChar),
					new SqlParameter("@LocationY",SqlDbType.NVarChar),
					new SqlParameter("@Scale",SqlDbType.NVarChar),
					new SqlParameter("@Title",SqlDbType.NVarChar),
					new SqlParameter("@Description",SqlDbType.NVarChar),
					new SqlParameter("@Url",SqlDbType.NVarChar),
					new SqlParameter("@AnswerMsgType",SqlDbType.NVarChar),
					new SqlParameter("@AnswerContent",SqlDbType.NVarChar),
					new SqlParameter("@AnswerMusicUrl",SqlDbType.NVarChar),
					new SqlParameter("@AnswerHQMusicUrl",SqlDbType.NVarChar),
					new SqlParameter("@AnswerArticleCount",SqlDbType.Int),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@NewsPushID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.WeiXinID;
			parameters[1].Value = pEntity.MsgType;
			parameters[2].Value = pEntity.Content;
			parameters[3].Value = pEntity.PicUrl;
			parameters[4].Value = pEntity.LocationX;
			parameters[5].Value = pEntity.LocationY;
			parameters[6].Value = pEntity.Scale;
			parameters[7].Value = pEntity.Title;
			parameters[8].Value = pEntity.Description;
			parameters[9].Value = pEntity.Url;
			parameters[10].Value = pEntity.AnswerMsgType;
			parameters[11].Value = pEntity.AnswerContent;
			parameters[12].Value = pEntity.AnswerMusicUrl;
			parameters[13].Value = pEntity.AnswerHQMusicUrl;
			parameters[14].Value = pEntity.AnswerArticleCount;
			parameters[15].Value = pEntity.LastUpdateBy;
			parameters[16].Value = pEntity.LastUpdateTime;
			parameters[17].Value = pEntity.NewsPushID;

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
        public void Update(WNewsPushEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(WNewsPushEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(WNewsPushEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(WNewsPushEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.NewsPushID==null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.NewsPushID, pTran);           
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
            sql.AppendLine("update [WNewsPush] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where NewsPushID=@NewsPushID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@NewsPushID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(WNewsPushEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.NewsPushID==null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.NewsPushID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(WNewsPushEntity[] pEntities)
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
            sql.AppendLine("update [WNewsPush] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where NewsPushID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public WNewsPushEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WNewsPush] where isdelete=0 ");
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
            List<WNewsPushEntity> list = new List<WNewsPushEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WNewsPushEntity m;
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
        public PagedQueryResult<WNewsPushEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [NewsPushID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [WNewsPush] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [WNewsPush] where isdelete=0 ");
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
            PagedQueryResult<WNewsPushEntity> result = new PagedQueryResult<WNewsPushEntity>();
            List<WNewsPushEntity> list = new List<WNewsPushEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    WNewsPushEntity m;
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
        public WNewsPushEntity[] QueryByEntity(WNewsPushEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<WNewsPushEntity> PagedQueryByEntity(WNewsPushEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(WNewsPushEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.NewsPushID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NewsPushID", Value = pQueryEntity.NewsPushID });
            if (pQueryEntity.WeiXinID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WeiXinID", Value = pQueryEntity.WeiXinID });
            if (pQueryEntity.MsgType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MsgType", Value = pQueryEntity.MsgType });
            if (pQueryEntity.Content!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Content", Value = pQueryEntity.Content });
            if (pQueryEntity.PicUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PicUrl", Value = pQueryEntity.PicUrl });
            if (pQueryEntity.LocationX!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LocationX", Value = pQueryEntity.LocationX });
            if (pQueryEntity.LocationY!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LocationY", Value = pQueryEntity.LocationY });
            if (pQueryEntity.Scale!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Scale", Value = pQueryEntity.Scale });
            if (pQueryEntity.Title!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Title", Value = pQueryEntity.Title });
            if (pQueryEntity.Description!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Description", Value = pQueryEntity.Description });
            if (pQueryEntity.Url!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Url", Value = pQueryEntity.Url });
            if (pQueryEntity.AnswerMsgType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AnswerMsgType", Value = pQueryEntity.AnswerMsgType });
            if (pQueryEntity.AnswerContent!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AnswerContent", Value = pQueryEntity.AnswerContent });
            if (pQueryEntity.AnswerMusicUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AnswerMusicUrl", Value = pQueryEntity.AnswerMusicUrl });
            if (pQueryEntity.AnswerHQMusicUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AnswerHQMusicUrl", Value = pQueryEntity.AnswerHQMusicUrl });
            if (pQueryEntity.AnswerArticleCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AnswerArticleCount", Value = pQueryEntity.AnswerArticleCount });
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

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out WNewsPushEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new WNewsPushEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["NewsPushID"] != DBNull.Value)
			{
				pInstance.NewsPushID =  Convert.ToString(pReader["NewsPushID"]);
			}
			if (pReader["WeiXinID"] != DBNull.Value)
			{
				pInstance.WeiXinID =  Convert.ToString(pReader["WeiXinID"]);
			}
			if (pReader["MsgType"] != DBNull.Value)
			{
				pInstance.MsgType =  Convert.ToString(pReader["MsgType"]);
			}
			if (pReader["Content"] != DBNull.Value)
			{
				pInstance.Content =  Convert.ToString(pReader["Content"]);
			}
			if (pReader["PicUrl"] != DBNull.Value)
			{
				pInstance.PicUrl =  Convert.ToString(pReader["PicUrl"]);
			}
			if (pReader["Location_X"] != DBNull.Value)
			{
				pInstance.LocationX =  Convert.ToString(pReader["Location_X"]);
			}
			if (pReader["Location_Y"] != DBNull.Value)
			{
				pInstance.LocationY =  Convert.ToString(pReader["Location_Y"]);
			}
			if (pReader["Scale"] != DBNull.Value)
			{
				pInstance.Scale =  Convert.ToString(pReader["Scale"]);
			}
			if (pReader["Title"] != DBNull.Value)
			{
				pInstance.Title =  Convert.ToString(pReader["Title"]);
			}
			if (pReader["Description"] != DBNull.Value)
			{
				pInstance.Description =  Convert.ToString(pReader["Description"]);
			}
			if (pReader["Url"] != DBNull.Value)
			{
				pInstance.Url =  Convert.ToString(pReader["Url"]);
			}
			if (pReader["AnswerMsgType"] != DBNull.Value)
			{
				pInstance.AnswerMsgType =  Convert.ToString(pReader["AnswerMsgType"]);
			}
			if (pReader["AnswerContent"] != DBNull.Value)
			{
				pInstance.AnswerContent =  Convert.ToString(pReader["AnswerContent"]);
			}
			if (pReader["AnswerMusicUrl"] != DBNull.Value)
			{
				pInstance.AnswerMusicUrl =  Convert.ToString(pReader["AnswerMusicUrl"]);
			}
			if (pReader["AnswerHQMusicUrl"] != DBNull.Value)
			{
				pInstance.AnswerHQMusicUrl =  Convert.ToString(pReader["AnswerHQMusicUrl"]);
			}
			if (pReader["AnswerArticleCount"] != DBNull.Value)
			{
				pInstance.AnswerArticleCount =   Convert.ToInt32(pReader["AnswerArticleCount"]);
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

        }
        #endregion
    }
}
