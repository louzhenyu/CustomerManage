/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/11/26 19:56:00
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
    /// 表WXTMConfig的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class WXTMConfigDAO : Base.BaseCPOSDAO, ICRUDable<WXTMConfigEntity>, IQueryable<WXTMConfigEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public WXTMConfigDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(WXTMConfigEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(WXTMConfigEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [WXTMConfig](");
            strSql.Append("[WeiXinId],[TemplateIdShort],[AppId],[FirstText],[RemarkText],[FirstColour],[RemarkColour],[AmountColour],[Colour1],[Colour2],[Colour3],[Colour4],[Colour5],[Colour6],[CustomerId],[IsDelete],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[Title],[TemplateID])");
            strSql.Append(" values (");
            strSql.Append("@WeiXinId,@TemplateIdShort,@AppId,@FirstText,@RemarkText,@FirstColour,@RemarkColour,@AmountColour,@Colour1,@Colour2,@Colour3,@Colour4,@Colour5,@Colour6,@CustomerId,@IsDelete,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@Title,@TemplateID)");            

			string pkString = pEntity.TemplateID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@WeiXinId",SqlDbType.VarChar),
					new SqlParameter("@TemplateIdShort",SqlDbType.VarChar),
					new SqlParameter("@AppId",SqlDbType.VarChar),
					new SqlParameter("@FirstText",SqlDbType.VarChar),
					new SqlParameter("@RemarkText",SqlDbType.VarChar),
					new SqlParameter("@FirstColour",SqlDbType.VarChar),
					new SqlParameter("@RemarkColour",SqlDbType.VarChar),
					new SqlParameter("@AmountColour",SqlDbType.VarChar),
					new SqlParameter("@Colour1",SqlDbType.VarChar),
					new SqlParameter("@Colour2",SqlDbType.VarChar),
					new SqlParameter("@Colour3",SqlDbType.VarChar),
					new SqlParameter("@Colour4",SqlDbType.VarChar),
					new SqlParameter("@Colour5",SqlDbType.VarChar),
					new SqlParameter("@Colour6",SqlDbType.VarChar),
					new SqlParameter("@CustomerId",SqlDbType.VarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@Title",SqlDbType.VarChar),
					new SqlParameter("@TemplateID",SqlDbType.VarChar)
            };
			parameters[0].Value = pEntity.WeiXinId;
			parameters[1].Value = pEntity.TemplateIdShort;
			parameters[2].Value = pEntity.AppId;
			parameters[3].Value = pEntity.FirstText;
			parameters[4].Value = pEntity.RemarkText;
			parameters[5].Value = pEntity.FirstColour;
			parameters[6].Value = pEntity.RemarkColour;
			parameters[7].Value = pEntity.AmountColour;
			parameters[8].Value = pEntity.Colour1;
			parameters[9].Value = pEntity.Colour2;
			parameters[10].Value = pEntity.Colour3;
			parameters[11].Value = pEntity.Colour4;
			parameters[12].Value = pEntity.Colour5;
			parameters[13].Value = pEntity.Colour6;
			parameters[14].Value = pEntity.CustomerId;
			parameters[15].Value = pEntity.IsDelete;
			parameters[16].Value = pEntity.CreateTime;
			parameters[17].Value = pEntity.CreateBy;
			parameters[18].Value = pEntity.LastUpdateTime;
			parameters[19].Value = pEntity.LastUpdateBy;
			parameters[20].Value = pEntity.Title;
			parameters[21].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.TemplateID = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public WXTMConfigEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXTMConfig] where TemplateID='{0}'  and isdelete=0 ", id.ToString());
            //读取数据
            WXTMConfigEntity m = null;
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
        public WXTMConfigEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXTMConfig] where 1=1  and isdelete=0");
            //读取数据
            List<WXTMConfigEntity> list = new List<WXTMConfigEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WXTMConfigEntity m;
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
        public void Update(WXTMConfigEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(WXTMConfigEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.TemplateID == null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [WXTMConfig] set ");
                        if (pIsUpdateNullField || pEntity.WeiXinId!=null)
                strSql.Append( "[WeiXinId]=@WeiXinId,");
            if (pIsUpdateNullField || pEntity.TemplateIdShort!=null)
                strSql.Append( "[TemplateIdShort]=@TemplateIdShort,");
            if (pIsUpdateNullField || pEntity.AppId!=null)
                strSql.Append( "[AppId]=@AppId,");
            if (pIsUpdateNullField || pEntity.FirstText!=null)
                strSql.Append( "[FirstText]=@FirstText,");
            if (pIsUpdateNullField || pEntity.RemarkText!=null)
                strSql.Append( "[RemarkText]=@RemarkText,");
            if (pIsUpdateNullField || pEntity.FirstColour!=null)
                strSql.Append( "[FirstColour]=@FirstColour,");
            if (pIsUpdateNullField || pEntity.RemarkColour!=null)
                strSql.Append( "[RemarkColour]=@RemarkColour,");
            if (pIsUpdateNullField || pEntity.AmountColour!=null)
                strSql.Append( "[AmountColour]=@AmountColour,");
            if (pIsUpdateNullField || pEntity.Colour1!=null)
                strSql.Append( "[Colour1]=@Colour1,");
            if (pIsUpdateNullField || pEntity.Colour2!=null)
                strSql.Append( "[Colour2]=@Colour2,");
            if (pIsUpdateNullField || pEntity.Colour3!=null)
                strSql.Append( "[Colour3]=@Colour3,");
            if (pIsUpdateNullField || pEntity.Colour4!=null)
                strSql.Append( "[Colour4]=@Colour4,");
            if (pIsUpdateNullField || pEntity.Colour5!=null)
                strSql.Append( "[Colour5]=@Colour5,");
            if (pIsUpdateNullField || pEntity.Colour6!=null)
                strSql.Append( "[Colour6]=@Colour6,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.Title!=null)
                strSql.Append( "[Title]=@Title");
            strSql.Append(" where TemplateID=@TemplateID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@WeiXinId",SqlDbType.VarChar),
					new SqlParameter("@TemplateIdShort",SqlDbType.VarChar),
					new SqlParameter("@AppId",SqlDbType.VarChar),
					new SqlParameter("@FirstText",SqlDbType.VarChar),
					new SqlParameter("@RemarkText",SqlDbType.VarChar),
					new SqlParameter("@FirstColour",SqlDbType.VarChar),
					new SqlParameter("@RemarkColour",SqlDbType.VarChar),
					new SqlParameter("@AmountColour",SqlDbType.VarChar),
					new SqlParameter("@Colour1",SqlDbType.VarChar),
					new SqlParameter("@Colour2",SqlDbType.VarChar),
					new SqlParameter("@Colour3",SqlDbType.VarChar),
					new SqlParameter("@Colour4",SqlDbType.VarChar),
					new SqlParameter("@Colour5",SqlDbType.VarChar),
					new SqlParameter("@Colour6",SqlDbType.VarChar),
					new SqlParameter("@CustomerId",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@Title",SqlDbType.VarChar),
					new SqlParameter("@TemplateID",SqlDbType.VarChar)
            };
			parameters[0].Value = pEntity.WeiXinId;
			parameters[1].Value = pEntity.TemplateIdShort;
			parameters[2].Value = pEntity.AppId;
			parameters[3].Value = pEntity.FirstText;
			parameters[4].Value = pEntity.RemarkText;
			parameters[5].Value = pEntity.FirstColour;
			parameters[6].Value = pEntity.RemarkColour;
			parameters[7].Value = pEntity.AmountColour;
			parameters[8].Value = pEntity.Colour1;
			parameters[9].Value = pEntity.Colour2;
			parameters[10].Value = pEntity.Colour3;
			parameters[11].Value = pEntity.Colour4;
			parameters[12].Value = pEntity.Colour5;
			parameters[13].Value = pEntity.Colour6;
			parameters[14].Value = pEntity.CustomerId;
			parameters[15].Value = pEntity.LastUpdateTime;
			parameters[16].Value = pEntity.LastUpdateBy;
			parameters[17].Value = pEntity.Title;
			parameters[18].Value = pEntity.TemplateID;

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
        public void Update(WXTMConfigEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(WXTMConfigEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(WXTMConfigEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.TemplateID == null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.TemplateID, pTran);           
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
            sql.AppendLine("update [WXTMConfig] set  isdelete=1 where TemplateID=@TemplateID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@TemplateID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(WXTMConfigEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (pEntity.TemplateID == null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.TemplateID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(WXTMConfigEntity[] pEntities)
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
            sql.AppendLine("update [WXTMConfig] set  isdelete=1 where TemplateID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public WXTMConfigEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXTMConfig] where 1=1  and isdelete=0 ");
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
            List<WXTMConfigEntity> list = new List<WXTMConfigEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WXTMConfigEntity m;
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
        public PagedQueryResult<WXTMConfigEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [TemplateID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [WXTMConfig] where 1=1  and isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [WXTMConfig] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<WXTMConfigEntity> result = new PagedQueryResult<WXTMConfigEntity>();
            List<WXTMConfigEntity> list = new List<WXTMConfigEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    WXTMConfigEntity m;
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
        public WXTMConfigEntity[] QueryByEntity(WXTMConfigEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<WXTMConfigEntity> PagedQueryByEntity(WXTMConfigEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(WXTMConfigEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.TemplateID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TemplateID", Value = pQueryEntity.TemplateID });
            if (pQueryEntity.WeiXinId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WeiXinId", Value = pQueryEntity.WeiXinId });
            if (pQueryEntity.TemplateIdShort!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TemplateIdShort", Value = pQueryEntity.TemplateIdShort });
            if (pQueryEntity.AppId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AppId", Value = pQueryEntity.AppId });
            if (pQueryEntity.FirstText!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FirstText", Value = pQueryEntity.FirstText });
            if (pQueryEntity.RemarkText!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RemarkText", Value = pQueryEntity.RemarkText });
            if (pQueryEntity.FirstColour!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FirstColour", Value = pQueryEntity.FirstColour });
            if (pQueryEntity.RemarkColour!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RemarkColour", Value = pQueryEntity.RemarkColour });
            if (pQueryEntity.AmountColour!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AmountColour", Value = pQueryEntity.AmountColour });
            if (pQueryEntity.Colour1!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Colour1", Value = pQueryEntity.Colour1 });
            if (pQueryEntity.Colour2!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Colour2", Value = pQueryEntity.Colour2 });
            if (pQueryEntity.Colour3!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Colour3", Value = pQueryEntity.Colour3 });
            if (pQueryEntity.Colour4!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Colour4", Value = pQueryEntity.Colour4 });
            if (pQueryEntity.Colour5!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Colour5", Value = pQueryEntity.Colour5 });
            if (pQueryEntity.Colour6!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Colour6", Value = pQueryEntity.Colour6 });
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
            if (pQueryEntity.IsDelete!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.LastUpdateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.LastUpdateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.Title!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Title", Value = pQueryEntity.Title });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out WXTMConfigEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new WXTMConfigEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["TemplateID"] != DBNull.Value)
			{
				pInstance.TemplateID =  Convert.ToString(pReader["TemplateID"]);
			}
			if (pReader["WeiXinId"] != DBNull.Value)
			{
				pInstance.WeiXinId =  Convert.ToString(pReader["WeiXinId"]);
			}
			if (pReader["TemplateIdShort"] != DBNull.Value)
			{
				pInstance.TemplateIdShort =  Convert.ToString(pReader["TemplateIdShort"]);
			}
			if (pReader["AppId"] != DBNull.Value)
			{
				pInstance.AppId =  Convert.ToString(pReader["AppId"]);
			}
			if (pReader["FirstText"] != DBNull.Value)
			{
				pInstance.FirstText =  Convert.ToString(pReader["FirstText"]);
			}
			if (pReader["RemarkText"] != DBNull.Value)
			{
				pInstance.RemarkText =  Convert.ToString(pReader["RemarkText"]);
			}
			if (pReader["FirstColour"] != DBNull.Value)
			{
				pInstance.FirstColour =  Convert.ToString(pReader["FirstColour"]);
			}
			if (pReader["RemarkColour"] != DBNull.Value)
			{
				pInstance.RemarkColour =  Convert.ToString(pReader["RemarkColour"]);
			}
			if (pReader["AmountColour"] != DBNull.Value)
			{
				pInstance.AmountColour =  Convert.ToString(pReader["AmountColour"]);
			}
			if (pReader["Colour1"] != DBNull.Value)
			{
				pInstance.Colour1 =  Convert.ToString(pReader["Colour1"]);
			}
			if (pReader["Colour2"] != DBNull.Value)
			{
				pInstance.Colour2 =  Convert.ToString(pReader["Colour2"]);
			}
			if (pReader["Colour3"] != DBNull.Value)
			{
				pInstance.Colour3 =  Convert.ToString(pReader["Colour3"]);
			}
			if (pReader["Colour4"] != DBNull.Value)
			{
				pInstance.Colour4 =  Convert.ToString(pReader["Colour4"]);
			}
			if (pReader["Colour5"] != DBNull.Value)
			{
				pInstance.Colour5 =  Convert.ToString(pReader["Colour5"]);
			}
			if (pReader["Colour6"] != DBNull.Value)
			{
				pInstance.Colour6 =  Convert.ToString(pReader["Colour6"]);
			}
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}
			if (pReader["IsDelete"] != DBNull.Value)
			{
				pInstance.IsDelete =   Convert.ToInt32(pReader["IsDelete"]);
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
			if (pReader["Title"] != DBNull.Value)
			{
				pInstance.Title =  Convert.ToString(pReader["Title"]);
			}

        }
        #endregion
    }
}
