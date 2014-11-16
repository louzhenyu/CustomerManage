/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/10/21 11:38:21
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
    /// 表MarketEvent的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class MarketEventDAO : Base.BaseCPOSDAO, ICRUDable<MarketEventEntity>, IQueryable<MarketEventEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public MarketEventDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(MarketEventEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(MarketEventEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [MarketEvent](");
            strSql.Append("[EventCode],[BrandID],[EventType],[EventMode],[EventStatus],[BudgetTotal],[PerCapita],[BeginTime],[EndTime],[EventDesc],[IsWaveBand],[StoreCount],[PersonCount],[TemplateID],[StatisticsID],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[TemplateContent],[TemplateContentSMS],[SendTypeId],[TemplateContentAPP],[CustomerId],[MarketEventID])");
            strSql.Append(" values (");
            strSql.Append("@EventCode,@BrandID,@EventType,@EventMode,@EventStatus,@BudgetTotal,@PerCapita,@BeginTime,@EndTime,@EventDesc,@IsWaveBand,@StoreCount,@PersonCount,@TemplateID,@StatisticsID,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@TemplateContent,@TemplateContentSMS,@SendTypeId,@TemplateContentAPP,@CustomerId,@MarketEventID)");            

			string pkString = pEntity.MarketEventID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@EventCode",SqlDbType.NVarChar),
					new SqlParameter("@BrandID",SqlDbType.NVarChar),
					new SqlParameter("@EventType",SqlDbType.NVarChar),
					new SqlParameter("@EventMode",SqlDbType.NVarChar),
					new SqlParameter("@EventStatus",SqlDbType.Int),
					new SqlParameter("@BudgetTotal",SqlDbType.Decimal),
					new SqlParameter("@PerCapita",SqlDbType.Decimal),
					new SqlParameter("@BeginTime",SqlDbType.NVarChar),
					new SqlParameter("@EndTime",SqlDbType.NVarChar),
					new SqlParameter("@EventDesc",SqlDbType.NVarChar),
					new SqlParameter("@IsWaveBand",SqlDbType.Int),
					new SqlParameter("@StoreCount",SqlDbType.Int),
					new SqlParameter("@PersonCount",SqlDbType.Int),
					new SqlParameter("@TemplateID",SqlDbType.NVarChar),
					new SqlParameter("@StatisticsID",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@TemplateContent",SqlDbType.NVarChar),
					new SqlParameter("@TemplateContentSMS",SqlDbType.NVarChar),
					new SqlParameter("@SendTypeId",SqlDbType.NVarChar),
					new SqlParameter("@TemplateContentAPP",SqlDbType.NVarChar),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@MarketEventID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.EventCode;
			parameters[1].Value = pEntity.BrandID;
			parameters[2].Value = pEntity.EventType;
			parameters[3].Value = pEntity.EventMode;
			parameters[4].Value = pEntity.EventStatus;
			parameters[5].Value = pEntity.BudgetTotal;
			parameters[6].Value = pEntity.PerCapita;
			parameters[7].Value = pEntity.BeginTime;
			parameters[8].Value = pEntity.EndTime;
			parameters[9].Value = pEntity.EventDesc;
			parameters[10].Value = pEntity.IsWaveBand;
			parameters[11].Value = pEntity.StoreCount;
			parameters[12].Value = pEntity.PersonCount;
			parameters[13].Value = pEntity.TemplateID;
			parameters[14].Value = pEntity.StatisticsID;
			parameters[15].Value = pEntity.CreateTime;
			parameters[16].Value = pEntity.CreateBy;
			parameters[17].Value = pEntity.LastUpdateBy;
			parameters[18].Value = pEntity.LastUpdateTime;
			parameters[19].Value = pEntity.IsDelete;
			parameters[20].Value = pEntity.TemplateContent;
			parameters[21].Value = pEntity.TemplateContentSMS;
			parameters[22].Value = pEntity.SendTypeId;
			parameters[23].Value = pEntity.TemplateContentAPP;
			parameters[24].Value = pEntity.CustomerId;
			parameters[25].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.MarketEventID = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public MarketEventEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [MarketEvent] where MarketEventID='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            MarketEventEntity m = null;
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
        public MarketEventEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [MarketEvent] where isdelete=0");
            //读取数据
            List<MarketEventEntity> list = new List<MarketEventEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    MarketEventEntity m;
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
        public void Update(MarketEventEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(MarketEventEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.MarketEventID==null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [MarketEvent] set ");
            if (pIsUpdateNullField || pEntity.EventCode!=null)
                strSql.Append( "[EventCode]=@EventCode,");
            if (pIsUpdateNullField || pEntity.BrandID!=null)
                strSql.Append( "[BrandID]=@BrandID,");
            if (pIsUpdateNullField || pEntity.EventType!=null)
                strSql.Append( "[EventType]=@EventType,");
            if (pIsUpdateNullField || pEntity.EventMode!=null)
                strSql.Append( "[EventMode]=@EventMode,");
            if (pIsUpdateNullField || pEntity.EventStatus!=null)
                strSql.Append( "[EventStatus]=@EventStatus,");
            if (pIsUpdateNullField || pEntity.BudgetTotal!=null)
                strSql.Append( "[BudgetTotal]=@BudgetTotal,");
            if (pIsUpdateNullField || pEntity.PerCapita!=null)
                strSql.Append( "[PerCapita]=@PerCapita,");
            if (pIsUpdateNullField || pEntity.BeginTime!=null)
                strSql.Append( "[BeginTime]=@BeginTime,");
            if (pIsUpdateNullField || pEntity.EndTime!=null)
                strSql.Append( "[EndTime]=@EndTime,");
            if (pIsUpdateNullField || pEntity.EventDesc!=null)
                strSql.Append( "[EventDesc]=@EventDesc,");
            if (pIsUpdateNullField || pEntity.IsWaveBand!=null)
                strSql.Append( "[IsWaveBand]=@IsWaveBand,");
            if (pIsUpdateNullField || pEntity.StoreCount!=null)
                strSql.Append( "[StoreCount]=@StoreCount,");
            if (pIsUpdateNullField || pEntity.PersonCount!=null)
                strSql.Append( "[PersonCount]=@PersonCount,");
            if (pIsUpdateNullField || pEntity.TemplateID!=null)
                strSql.Append( "[TemplateID]=@TemplateID,");
            if (pIsUpdateNullField || pEntity.StatisticsID!=null)
                strSql.Append( "[StatisticsID]=@StatisticsID,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.TemplateContent!=null)
                strSql.Append( "[TemplateContent]=@TemplateContent,");
            if (pIsUpdateNullField || pEntity.TemplateContentSMS!=null)
                strSql.Append( "[TemplateContentSMS]=@TemplateContentSMS,");
            if (pIsUpdateNullField || pEntity.SendTypeId!=null)
                strSql.Append( "[SendTypeId]=@SendTypeId,");
            if (pIsUpdateNullField || pEntity.TemplateContentAPP!=null)
                strSql.Append( "[TemplateContentAPP]=@TemplateContentAPP,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where MarketEventID=@MarketEventID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@EventCode",SqlDbType.NVarChar),
					new SqlParameter("@BrandID",SqlDbType.NVarChar),
					new SqlParameter("@EventType",SqlDbType.NVarChar),
					new SqlParameter("@EventMode",SqlDbType.NVarChar),
					new SqlParameter("@EventStatus",SqlDbType.Int),
					new SqlParameter("@BudgetTotal",SqlDbType.Decimal),
					new SqlParameter("@PerCapita",SqlDbType.Decimal),
					new SqlParameter("@BeginTime",SqlDbType.NVarChar),
					new SqlParameter("@EndTime",SqlDbType.NVarChar),
					new SqlParameter("@EventDesc",SqlDbType.NVarChar),
					new SqlParameter("@IsWaveBand",SqlDbType.Int),
					new SqlParameter("@StoreCount",SqlDbType.Int),
					new SqlParameter("@PersonCount",SqlDbType.Int),
					new SqlParameter("@TemplateID",SqlDbType.NVarChar),
					new SqlParameter("@StatisticsID",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@TemplateContent",SqlDbType.NVarChar),
					new SqlParameter("@TemplateContentSMS",SqlDbType.NVarChar),
					new SqlParameter("@SendTypeId",SqlDbType.NVarChar),
					new SqlParameter("@TemplateContentAPP",SqlDbType.NVarChar),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@MarketEventID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.EventCode;
			parameters[1].Value = pEntity.BrandID;
			parameters[2].Value = pEntity.EventType;
			parameters[3].Value = pEntity.EventMode;
			parameters[4].Value = pEntity.EventStatus;
			parameters[5].Value = pEntity.BudgetTotal;
			parameters[6].Value = pEntity.PerCapita;
			parameters[7].Value = pEntity.BeginTime;
			parameters[8].Value = pEntity.EndTime;
			parameters[9].Value = pEntity.EventDesc;
			parameters[10].Value = pEntity.IsWaveBand;
			parameters[11].Value = pEntity.StoreCount;
			parameters[12].Value = pEntity.PersonCount;
			parameters[13].Value = pEntity.TemplateID;
			parameters[14].Value = pEntity.StatisticsID;
			parameters[15].Value = pEntity.LastUpdateBy;
			parameters[16].Value = pEntity.LastUpdateTime;
			parameters[17].Value = pEntity.TemplateContent;
			parameters[18].Value = pEntity.TemplateContentSMS;
			parameters[19].Value = pEntity.SendTypeId;
			parameters[20].Value = pEntity.TemplateContentAPP;
			parameters[21].Value = pEntity.CustomerId;
			parameters[22].Value = pEntity.MarketEventID;

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
        public void Update(MarketEventEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(MarketEventEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(MarketEventEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(MarketEventEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.MarketEventID==null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.MarketEventID, pTran);           
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
            sql.AppendLine("update [MarketEvent] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where MarketEventID=@MarketEventID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@MarketEventID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(MarketEventEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.MarketEventID==null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.MarketEventID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(MarketEventEntity[] pEntities)
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
            sql.AppendLine("update [MarketEvent] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where MarketEventID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public MarketEventEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [MarketEvent] where isdelete=0 ");
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
            List<MarketEventEntity> list = new List<MarketEventEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    MarketEventEntity m;
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
        public PagedQueryResult<MarketEventEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [MarketEventID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [MarketEvent] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [MarketEvent] where isdelete=0 ");
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
            PagedQueryResult<MarketEventEntity> result = new PagedQueryResult<MarketEventEntity>();
            List<MarketEventEntity> list = new List<MarketEventEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    MarketEventEntity m;
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
        public MarketEventEntity[] QueryByEntity(MarketEventEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<MarketEventEntity> PagedQueryByEntity(MarketEventEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(MarketEventEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.MarketEventID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MarketEventID", Value = pQueryEntity.MarketEventID });
            if (pQueryEntity.EventCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventCode", Value = pQueryEntity.EventCode });
            if (pQueryEntity.BrandID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BrandID", Value = pQueryEntity.BrandID });
            if (pQueryEntity.EventType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventType", Value = pQueryEntity.EventType });
            if (pQueryEntity.EventMode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventMode", Value = pQueryEntity.EventMode });
            if (pQueryEntity.EventStatus!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventStatus", Value = pQueryEntity.EventStatus });
            if (pQueryEntity.BudgetTotal!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BudgetTotal", Value = pQueryEntity.BudgetTotal });
            if (pQueryEntity.PerCapita!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PerCapita", Value = pQueryEntity.PerCapita });
            if (pQueryEntity.BeginTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BeginTime", Value = pQueryEntity.BeginTime });
            if (pQueryEntity.EndTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EndTime", Value = pQueryEntity.EndTime });
            if (pQueryEntity.EventDesc!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventDesc", Value = pQueryEntity.EventDesc });
            if (pQueryEntity.IsWaveBand!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsWaveBand", Value = pQueryEntity.IsWaveBand });
            if (pQueryEntity.StoreCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StoreCount", Value = pQueryEntity.StoreCount });
            if (pQueryEntity.PersonCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PersonCount", Value = pQueryEntity.PersonCount });
            if (pQueryEntity.TemplateID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TemplateID", Value = pQueryEntity.TemplateID });
            if (pQueryEntity.StatisticsID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StatisticsID", Value = pQueryEntity.StatisticsID });
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
            if (pQueryEntity.TemplateContent!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TemplateContent", Value = pQueryEntity.TemplateContent });
            if (pQueryEntity.TemplateContentSMS!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TemplateContentSMS", Value = pQueryEntity.TemplateContentSMS });
            if (pQueryEntity.SendTypeId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SendTypeId", Value = pQueryEntity.SendTypeId });
            if (pQueryEntity.TemplateContentAPP!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TemplateContentAPP", Value = pQueryEntity.TemplateContentAPP });
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out MarketEventEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new MarketEventEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["MarketEventID"] != DBNull.Value)
			{
				pInstance.MarketEventID =  Convert.ToString(pReader["MarketEventID"]);
			}
			if (pReader["EventCode"] != DBNull.Value)
			{
				pInstance.EventCode =  Convert.ToString(pReader["EventCode"]);
			}
			if (pReader["BrandID"] != DBNull.Value)
			{
				pInstance.BrandID =  Convert.ToString(pReader["BrandID"]);
			}
			if (pReader["EventType"] != DBNull.Value)
			{
				pInstance.EventType =  Convert.ToString(pReader["EventType"]);
			}
			if (pReader["EventMode"] != DBNull.Value)
			{
				pInstance.EventMode =  Convert.ToString(pReader["EventMode"]);
			}
			if (pReader["EventStatus"] != DBNull.Value)
			{
				pInstance.EventStatus =   Convert.ToInt32(pReader["EventStatus"]);
			}
			if (pReader["BudgetTotal"] != DBNull.Value)
			{
				pInstance.BudgetTotal =  Convert.ToDecimal(pReader["BudgetTotal"]);
			}
			if (pReader["PerCapita"] != DBNull.Value)
			{
				pInstance.PerCapita =  Convert.ToDecimal(pReader["PerCapita"]);
			}
			if (pReader["BeginTime"] != DBNull.Value)
			{
				pInstance.BeginTime =  Convert.ToString(pReader["BeginTime"]);
			}
			if (pReader["EndTime"] != DBNull.Value)
			{
				pInstance.EndTime =  Convert.ToString(pReader["EndTime"]);
			}
			if (pReader["EventDesc"] != DBNull.Value)
			{
				pInstance.EventDesc =  Convert.ToString(pReader["EventDesc"]);
			}
			if (pReader["IsWaveBand"] != DBNull.Value)
			{
				pInstance.IsWaveBand =   Convert.ToInt32(pReader["IsWaveBand"]);
			}
			if (pReader["StoreCount"] != DBNull.Value)
			{
				pInstance.StoreCount =   Convert.ToInt32(pReader["StoreCount"]);
			}
			if (pReader["PersonCount"] != DBNull.Value)
			{
				pInstance.PersonCount =   Convert.ToInt32(pReader["PersonCount"]);
			}
			if (pReader["TemplateID"] != DBNull.Value)
			{
				pInstance.TemplateID =  Convert.ToString(pReader["TemplateID"]);
			}
			if (pReader["StatisticsID"] != DBNull.Value)
			{
				pInstance.StatisticsID =  Convert.ToString(pReader["StatisticsID"]);
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
			if (pReader["TemplateContent"] != DBNull.Value)
			{
				pInstance.TemplateContent =  Convert.ToString(pReader["TemplateContent"]);
			}
			if (pReader["TemplateContentSMS"] != DBNull.Value)
			{
				pInstance.TemplateContentSMS =  Convert.ToString(pReader["TemplateContentSMS"]);
			}
			if (pReader["SendTypeId"] != DBNull.Value)
			{
				pInstance.SendTypeId =  Convert.ToString(pReader["SendTypeId"]);
			}
			if (pReader["TemplateContentAPP"] != DBNull.Value)
			{
				pInstance.TemplateContentAPP =  Convert.ToString(pReader["TemplateContentAPP"]);
			}
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}

        }
        #endregion
    }
}
