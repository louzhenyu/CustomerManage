/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/9 14:43:03
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
    /// 表MarketEventResponse的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class MarketEventResponseDAO : Base.BaseCPOSDAO, ICRUDable<MarketEventResponseEntity>, IQueryable<MarketEventResponseEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public MarketEventResponseDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(MarketEventResponseEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(MarketEventResponseEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [MarketEventResponse](");
            strSql.Append("[MarketEventID],[VIPID],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[CustomerPrice],[UnitPrice],[PurchaseNumber],[SalesIntegral],[PurchaseAmount],[PurchaseCount],[OpenID],[ProductName],[IsSales],[ReponseID])");
            strSql.Append(" values (");
            strSql.Append("@MarketEventID,@VIPID,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@CustomerPrice,@UnitPrice,@PurchaseNumber,@SalesIntegral,@PurchaseAmount,@PurchaseCount,@OpenID,@ProductName,@IsSales,@ReponseID)");            

			string pkString = pEntity.ReponseID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@MarketEventID",SqlDbType.NVarChar),
					new SqlParameter("@VIPID",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerPrice",SqlDbType.Decimal),
					new SqlParameter("@UnitPrice",SqlDbType.Decimal),
					new SqlParameter("@PurchaseNumber",SqlDbType.Int),
					new SqlParameter("@SalesIntegral",SqlDbType.Int),
					new SqlParameter("@PurchaseAmount",SqlDbType.Decimal),
					new SqlParameter("@PurchaseCount",SqlDbType.Int),
					new SqlParameter("@OpenID",SqlDbType.NVarChar),
					new SqlParameter("@ProductName",SqlDbType.NVarChar),
					new SqlParameter("@IsSales",SqlDbType.Int),
					new SqlParameter("@ReponseID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.MarketEventID;
			parameters[1].Value = pEntity.VIPID;
			parameters[2].Value = pEntity.CreateTime;
			parameters[3].Value = pEntity.CreateBy;
			parameters[4].Value = pEntity.LastUpdateBy;
			parameters[5].Value = pEntity.LastUpdateTime;
			parameters[6].Value = pEntity.IsDelete;
			parameters[7].Value = pEntity.CustomerPrice;
			parameters[8].Value = pEntity.UnitPrice;
			parameters[9].Value = pEntity.PurchaseNumber;
			parameters[10].Value = pEntity.SalesIntegral;
			parameters[11].Value = pEntity.PurchaseAmount;
			parameters[12].Value = pEntity.PurchaseCount;
			parameters[13].Value = pEntity.OpenID;
			parameters[14].Value = pEntity.ProductName;
			parameters[15].Value = pEntity.IsSales;
			parameters[16].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.ReponseID = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public MarketEventResponseEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [MarketEventResponse] where ReponseID='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            MarketEventResponseEntity m = null;
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
        public MarketEventResponseEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [MarketEventResponse] where isdelete=0");
            //读取数据
            List<MarketEventResponseEntity> list = new List<MarketEventResponseEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    MarketEventResponseEntity m;
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
        public void Update(MarketEventResponseEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(MarketEventResponseEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ReponseID==null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [MarketEventResponse] set ");
            if (pIsUpdateNullField || pEntity.MarketEventID!=null)
                strSql.Append( "[MarketEventID]=@MarketEventID,");
            if (pIsUpdateNullField || pEntity.VIPID!=null)
                strSql.Append( "[VIPID]=@VIPID,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.CustomerPrice!=null)
                strSql.Append( "[CustomerPrice]=@CustomerPrice,");
            if (pIsUpdateNullField || pEntity.UnitPrice!=null)
                strSql.Append( "[UnitPrice]=@UnitPrice,");
            if (pIsUpdateNullField || pEntity.PurchaseNumber!=null)
                strSql.Append( "[PurchaseNumber]=@PurchaseNumber,");
            if (pIsUpdateNullField || pEntity.SalesIntegral!=null)
                strSql.Append( "[SalesIntegral]=@SalesIntegral,");
            if (pIsUpdateNullField || pEntity.PurchaseAmount!=null)
                strSql.Append( "[PurchaseAmount]=@PurchaseAmount,");
            if (pIsUpdateNullField || pEntity.PurchaseCount!=null)
                strSql.Append( "[PurchaseCount]=@PurchaseCount,");
            if (pIsUpdateNullField || pEntity.OpenID!=null)
                strSql.Append( "[OpenID]=@OpenID,");
            if (pIsUpdateNullField || pEntity.ProductName!=null)
                strSql.Append( "[ProductName]=@ProductName,");
            if (pIsUpdateNullField || pEntity.IsSales!=null)
                strSql.Append( "[IsSales]=@IsSales");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where ReponseID=@ReponseID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@MarketEventID",SqlDbType.NVarChar),
					new SqlParameter("@VIPID",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerPrice",SqlDbType.Decimal),
					new SqlParameter("@UnitPrice",SqlDbType.Decimal),
					new SqlParameter("@PurchaseNumber",SqlDbType.Int),
					new SqlParameter("@SalesIntegral",SqlDbType.Int),
					new SqlParameter("@PurchaseAmount",SqlDbType.Decimal),
					new SqlParameter("@PurchaseCount",SqlDbType.Int),
					new SqlParameter("@OpenID",SqlDbType.NVarChar),
					new SqlParameter("@ProductName",SqlDbType.NVarChar),
					new SqlParameter("@IsSales",SqlDbType.Int),
					new SqlParameter("@ReponseID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.MarketEventID;
			parameters[1].Value = pEntity.VIPID;
			parameters[2].Value = pEntity.LastUpdateBy;
			parameters[3].Value = pEntity.LastUpdateTime;
			parameters[4].Value = pEntity.CustomerPrice;
			parameters[5].Value = pEntity.UnitPrice;
			parameters[6].Value = pEntity.PurchaseNumber;
			parameters[7].Value = pEntity.SalesIntegral;
			parameters[8].Value = pEntity.PurchaseAmount;
			parameters[9].Value = pEntity.PurchaseCount;
			parameters[10].Value = pEntity.OpenID;
			parameters[11].Value = pEntity.ProductName;
			parameters[12].Value = pEntity.IsSales;
			parameters[13].Value = pEntity.ReponseID;

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
        public void Update(MarketEventResponseEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(MarketEventResponseEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(MarketEventResponseEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(MarketEventResponseEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ReponseID==null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.ReponseID, pTran);           
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
            sql.AppendLine("update [MarketEventResponse] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where ReponseID=@ReponseID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@ReponseID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(MarketEventResponseEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.ReponseID==null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.ReponseID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(MarketEventResponseEntity[] pEntities)
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
            sql.AppendLine("update [MarketEventResponse] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where ReponseID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public MarketEventResponseEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [MarketEventResponse] where isdelete=0 ");
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
            List<MarketEventResponseEntity> list = new List<MarketEventResponseEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    MarketEventResponseEntity m;
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
        public PagedQueryResult<MarketEventResponseEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [ReponseID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [MarketEventResponse] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [MarketEventResponse] where isdelete=0 ");
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
            PagedQueryResult<MarketEventResponseEntity> result = new PagedQueryResult<MarketEventResponseEntity>();
            List<MarketEventResponseEntity> list = new List<MarketEventResponseEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    MarketEventResponseEntity m;
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
        public MarketEventResponseEntity[] QueryByEntity(MarketEventResponseEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<MarketEventResponseEntity> PagedQueryByEntity(MarketEventResponseEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(MarketEventResponseEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.ReponseID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReponseID", Value = pQueryEntity.ReponseID });
            if (pQueryEntity.MarketEventID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MarketEventID", Value = pQueryEntity.MarketEventID });
            if (pQueryEntity.VIPID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VIPID", Value = pQueryEntity.VIPID });
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
            if (pQueryEntity.CustomerPrice!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerPrice", Value = pQueryEntity.CustomerPrice });
            if (pQueryEntity.UnitPrice!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitPrice", Value = pQueryEntity.UnitPrice });
            if (pQueryEntity.PurchaseNumber!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PurchaseNumber", Value = pQueryEntity.PurchaseNumber });
            if (pQueryEntity.SalesIntegral!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesIntegral", Value = pQueryEntity.SalesIntegral });
            if (pQueryEntity.PurchaseAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PurchaseAmount", Value = pQueryEntity.PurchaseAmount });
            if (pQueryEntity.PurchaseCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PurchaseCount", Value = pQueryEntity.PurchaseCount });
            if (pQueryEntity.OpenID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OpenID", Value = pQueryEntity.OpenID });
            if (pQueryEntity.ProductName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ProductName", Value = pQueryEntity.ProductName });
            if (pQueryEntity.IsSales!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsSales", Value = pQueryEntity.IsSales });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out MarketEventResponseEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new MarketEventResponseEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["ReponseID"] != DBNull.Value)
			{
				pInstance.ReponseID =  Convert.ToString(pReader["ReponseID"]);
			}
			if (pReader["MarketEventID"] != DBNull.Value)
			{
				pInstance.MarketEventID =  Convert.ToString(pReader["MarketEventID"]);
			}
			if (pReader["VIPID"] != DBNull.Value)
			{
				pInstance.VIPID =  Convert.ToString(pReader["VIPID"]);
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
			if (pReader["CustomerPrice"] != DBNull.Value)
			{
				pInstance.CustomerPrice =  Convert.ToDecimal(pReader["CustomerPrice"]);
			}
			if (pReader["UnitPrice"] != DBNull.Value)
			{
				pInstance.UnitPrice =  Convert.ToDecimal(pReader["UnitPrice"]);
			}
			if (pReader["PurchaseNumber"] != DBNull.Value)
			{
				pInstance.PurchaseNumber =   Convert.ToInt32(pReader["PurchaseNumber"]);
			}
			if (pReader["SalesIntegral"] != DBNull.Value)
			{
				pInstance.SalesIntegral =   Convert.ToInt32(pReader["SalesIntegral"]);
			}
			if (pReader["PurchaseAmount"] != DBNull.Value)
			{
				pInstance.PurchaseAmount =  Convert.ToDecimal(pReader["PurchaseAmount"]);
			}
			if (pReader["PurchaseCount"] != DBNull.Value)
			{
				pInstance.PurchaseCount =   Convert.ToInt32(pReader["PurchaseCount"]);
			}
			if (pReader["OpenID"] != DBNull.Value)
			{
				pInstance.OpenID =  Convert.ToString(pReader["OpenID"]);
			}
			if (pReader["ProductName"] != DBNull.Value)
			{
				pInstance.ProductName =  Convert.ToString(pReader["ProductName"]);
			}
			if (pReader["IsSales"] != DBNull.Value)
			{
				pInstance.IsSales =   Convert.ToInt32(pReader["IsSales"]);
			}

        }
        #endregion
    }
}
