/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/17 14:52:46
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
    /// 表LPrizePools的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class LPrizePoolsDAO : Base.BaseCPOSDAO, ICRUDable<LPrizePoolsEntity>, IQueryable<LPrizePoolsEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public LPrizePoolsDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(LPrizePoolsEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(LPrizePoolsEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [LPrizePools](");
            strSql.Append("[PrizeID],[StartTime],[EndTime],[AppearTime],[Status],[Longitude],[Latitude],[Distance],[EventId],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[PrizePoolsID])");
            strSql.Append(" values (");
            strSql.Append("@PrizeID,@StartTime,@EndTime,@AppearTime,@Status,@Longitude,@Latitude,@Distance,@EventId,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@PrizePoolsID)");            

			string pkString = pEntity.PrizePoolsID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@PrizeID",SqlDbType.NVarChar),
					new SqlParameter("@StartTime",SqlDbType.DateTime),
					new SqlParameter("@EndTime",SqlDbType.DateTime),
					new SqlParameter("@AppearTime",SqlDbType.DateTime),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@Longitude",SqlDbType.Float),
					new SqlParameter("@Latitude",SqlDbType.Float),
					new SqlParameter("@Distance",SqlDbType.Float),
					new SqlParameter("@EventId",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@PrizePoolsID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.PrizeID;
			parameters[1].Value = pEntity.StartTime;
			parameters[2].Value = pEntity.EndTime;
			parameters[3].Value = pEntity.AppearTime;
			parameters[4].Value = pEntity.Status;
			parameters[5].Value = pEntity.Longitude;
			parameters[6].Value = pEntity.Latitude;
			parameters[7].Value = pEntity.Distance;
			parameters[8].Value = pEntity.EventId;
			parameters[9].Value = pEntity.CreateTime;
			parameters[10].Value = pEntity.CreateBy;
			parameters[11].Value = pEntity.LastUpdateBy;
			parameters[12].Value = pEntity.LastUpdateTime;
			parameters[13].Value = pEntity.IsDelete;
			parameters[14].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.PrizePoolsID = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public LPrizePoolsEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [LPrizePools] where PrizePoolsID='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            LPrizePoolsEntity m = null;
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
        public LPrizePoolsEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [LPrizePools] where isdelete=0");
            //读取数据
            List<LPrizePoolsEntity> list = new List<LPrizePoolsEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    LPrizePoolsEntity m;
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
        public void Update(LPrizePoolsEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(LPrizePoolsEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.PrizePoolsID==null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [LPrizePools] set ");
            if (pIsUpdateNullField || pEntity.PrizeID!=null)
                strSql.Append( "[PrizeID]=@PrizeID,");
            if (pIsUpdateNullField || pEntity.StartTime!=null)
                strSql.Append( "[StartTime]=@StartTime,");
            if (pIsUpdateNullField || pEntity.EndTime!=null)
                strSql.Append( "[EndTime]=@EndTime,");
            if (pIsUpdateNullField || pEntity.AppearTime!=null)
                strSql.Append( "[AppearTime]=@AppearTime,");
            if (pIsUpdateNullField || pEntity.Status!=null)
                strSql.Append( "[Status]=@Status,");
            if (pIsUpdateNullField || pEntity.Longitude!=null)
                strSql.Append( "[Longitude]=@Longitude,");
            if (pIsUpdateNullField || pEntity.Latitude!=null)
                strSql.Append( "[Latitude]=@Latitude,");
            if (pIsUpdateNullField || pEntity.Distance!=null)
                strSql.Append( "[Distance]=@Distance,");
            if (pIsUpdateNullField || pEntity.EventId!=null)
                strSql.Append( "[EventId]=@EventId,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where PrizePoolsID=@PrizePoolsID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@PrizeID",SqlDbType.NVarChar),
					new SqlParameter("@StartTime",SqlDbType.DateTime),
					new SqlParameter("@EndTime",SqlDbType.DateTime),
					new SqlParameter("@AppearTime",SqlDbType.DateTime),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@Longitude",SqlDbType.Float),
					new SqlParameter("@Latitude",SqlDbType.Float),
					new SqlParameter("@Distance",SqlDbType.Float),
					new SqlParameter("@EventId",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@PrizePoolsID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.PrizeID;
			parameters[1].Value = pEntity.StartTime;
			parameters[2].Value = pEntity.EndTime;
			parameters[3].Value = pEntity.AppearTime;
			parameters[4].Value = pEntity.Status;
			parameters[5].Value = pEntity.Longitude;
			parameters[6].Value = pEntity.Latitude;
			parameters[7].Value = pEntity.Distance;
			parameters[8].Value = pEntity.EventId;
			parameters[9].Value = pEntity.LastUpdateBy;
			parameters[10].Value = pEntity.LastUpdateTime;
			parameters[11].Value = pEntity.PrizePoolsID;

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
        public void Update(LPrizePoolsEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(LPrizePoolsEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField,null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(LPrizePoolsEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(LPrizePoolsEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.PrizePoolsID==null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.PrizePoolsID, pTran);           
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
            sql.AppendLine("update [LPrizePools] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where PrizePoolsID=@PrizePoolsID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@PrizePoolsID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(LPrizePoolsEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.PrizePoolsID==null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.PrizePoolsID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(LPrizePoolsEntity[] pEntities)
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
            sql.AppendLine("update [LPrizePools] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where PrizePoolsID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public LPrizePoolsEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [LPrizePools] where isdelete=0 ");
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
            List<LPrizePoolsEntity> list = new List<LPrizePoolsEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    LPrizePoolsEntity m;
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
        public PagedQueryResult<LPrizePoolsEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [PrizePoolsID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [LPrizePools] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [LPrizePools] where isdelete=0 ");
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
            PagedQueryResult<LPrizePoolsEntity> result = new PagedQueryResult<LPrizePoolsEntity>();
            List<LPrizePoolsEntity> list = new List<LPrizePoolsEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    LPrizePoolsEntity m;
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
        public LPrizePoolsEntity[] QueryByEntity(LPrizePoolsEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<LPrizePoolsEntity> PagedQueryByEntity(LPrizePoolsEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(LPrizePoolsEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.PrizePoolsID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PrizePoolsID", Value = pQueryEntity.PrizePoolsID });
            if (pQueryEntity.PrizeID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PrizeID", Value = pQueryEntity.PrizeID });
            if (pQueryEntity.StartTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StartTime", Value = pQueryEntity.StartTime });
            if (pQueryEntity.EndTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EndTime", Value = pQueryEntity.EndTime });
            if (pQueryEntity.AppearTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AppearTime", Value = pQueryEntity.AppearTime });
            if (pQueryEntity.Status!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Status", Value = pQueryEntity.Status });
            if (pQueryEntity.Longitude!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Longitude", Value = pQueryEntity.Longitude });
            if (pQueryEntity.Latitude!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Latitude", Value = pQueryEntity.Latitude });
            if (pQueryEntity.Distance!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Distance", Value = pQueryEntity.Distance });
            if (pQueryEntity.EventId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventId", Value = pQueryEntity.EventId });
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
        protected void Load(SqlDataReader pReader, out LPrizePoolsEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new LPrizePoolsEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["PrizePoolsID"] != DBNull.Value)
			{
				pInstance.PrizePoolsID =  Convert.ToString(pReader["PrizePoolsID"]);
			}
			if (pReader["PrizeID"] != DBNull.Value)
			{
				pInstance.PrizeID =  Convert.ToString(pReader["PrizeID"]);
			}
			if (pReader["StartTime"] != DBNull.Value)
			{
				pInstance.StartTime =  Convert.ToDateTime(pReader["StartTime"]);
			}
			if (pReader["EndTime"] != DBNull.Value)
			{
				pInstance.EndTime =  Convert.ToDateTime(pReader["EndTime"]);
			}
			if (pReader["AppearTime"] != DBNull.Value)
			{
				pInstance.AppearTime =  Convert.ToDateTime(pReader["AppearTime"]);
			}
			if (pReader["Status"] != DBNull.Value)
			{
				pInstance.Status =   Convert.ToInt32(pReader["Status"]);
			}
			if (pReader["Longitude"] != DBNull.Value)
			{
				pInstance.Longitude =  Convert.ToSingle(pReader["Longitude"]);
			}
			if (pReader["Latitude"] != DBNull.Value)
			{
				pInstance.Latitude =  Convert.ToSingle(pReader["Latitude"]);
			}
			if (pReader["Distance"] != DBNull.Value)
			{
				pInstance.Distance =  Convert.ToSingle(pReader["Distance"]);
			}
			if (pReader["EventId"] != DBNull.Value)
			{
				pInstance.EventId =  Convert.ToString(pReader["EventId"]);
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
