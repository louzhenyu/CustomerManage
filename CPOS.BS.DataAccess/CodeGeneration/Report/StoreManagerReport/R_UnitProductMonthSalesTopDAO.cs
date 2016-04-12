/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/3/23 14:10:42
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
    /// 表R_UnitProductMonthSalesTop的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class R_UnitProductMonthSalesTopDAO : Base.BaseCPOSDAO, ICRUDable<R_UnitProductMonthSalesTopEntity>, IQueryable<R_UnitProductMonthSalesTopEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public R_UnitProductMonthSalesTopDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(R_UnitProductMonthSalesTopEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(R_UnitProductMonthSalesTopEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //初始化固定字段
			pEntity.CreateTime=DateTime.Now;


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [R_UnitProductMonthSalesTop](");
            strSql.Append("[DateCode],[UnitId],[UnitCode],[UnitName],[StoreType],[CustomerId],[TopIndex],[SkuID],[item_id],[item_code],[item_name],[SkuName],[SalesQty],[SalesAmount],[CreateTime],[ID])");
            strSql.Append(" values (");
            strSql.Append("@DateCode,@UnitId,@UnitCode,@UnitName,@StoreType,@CustomerId,@TopIndex,@SkuID,@item_id,@item_code,@item_name,@SkuName,@SalesQty,@SalesAmount,@CreateTime,@ID)");            

			string pkString = pEntity.ID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@DateCode",SqlDbType.Date),
					new SqlParameter("@UnitId",SqlDbType.NVarChar),
					new SqlParameter("@UnitCode",SqlDbType.NVarChar),
					new SqlParameter("@UnitName",SqlDbType.NVarChar),
					new SqlParameter("@StoreType",SqlDbType.NVarChar),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@TopIndex",SqlDbType.Int),
					new SqlParameter("@SkuID",SqlDbType.VarChar),
					new SqlParameter("@item_id",SqlDbType.NVarChar),
					new SqlParameter("@item_code",SqlDbType.NVarChar),
					new SqlParameter("@item_name",SqlDbType.NVarChar),
					new SqlParameter("@SkuName",SqlDbType.VarChar),
					new SqlParameter("@SalesQty",SqlDbType.Decimal),
					new SqlParameter("@SalesAmount",SqlDbType.Decimal),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@ID",SqlDbType.VarChar)
            };
			parameters[0].Value = pEntity.DateCode;
			parameters[1].Value = pEntity.UnitId;
			parameters[2].Value = pEntity.UnitCode;
			parameters[3].Value = pEntity.UnitName;
			parameters[4].Value = pEntity.StoreType;
			parameters[5].Value = pEntity.CustomerId;
			parameters[6].Value = pEntity.TopIndex;
			parameters[7].Value = pEntity.SkuID;
			parameters[8].Value = pEntity.item_id;
			parameters[9].Value = pEntity.item_code;
			parameters[10].Value = pEntity.item_name;
			parameters[11].Value = pEntity.SkuName;
			parameters[12].Value = pEntity.SalesQty;
			parameters[13].Value = pEntity.SalesAmount;
			parameters[14].Value = pEntity.CreateTime;
			parameters[15].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.ID = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public R_UnitProductMonthSalesTopEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [R_UnitProductMonthSalesTop] where ID='{0}'  ", id.ToString());
            //读取数据
            R_UnitProductMonthSalesTopEntity m = null;
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
        public R_UnitProductMonthSalesTopEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [R_UnitProductMonthSalesTop] where 1=1 ");
            //读取数据
            List<R_UnitProductMonthSalesTopEntity> list = new List<R_UnitProductMonthSalesTopEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    R_UnitProductMonthSalesTopEntity m;
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
        public void Update(R_UnitProductMonthSalesTopEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(R_UnitProductMonthSalesTopEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ID == null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [R_UnitProductMonthSalesTop] set ");
                        if (pIsUpdateNullField || pEntity.DateCode!=null)
                strSql.Append( "[DateCode]=@DateCode,");
            if (pIsUpdateNullField || pEntity.UnitId!=null)
                strSql.Append( "[UnitId]=@UnitId,");
            if (pIsUpdateNullField || pEntity.UnitCode!=null)
                strSql.Append( "[UnitCode]=@UnitCode,");
            if (pIsUpdateNullField || pEntity.UnitName!=null)
                strSql.Append( "[UnitName]=@UnitName,");
            if (pIsUpdateNullField || pEntity.StoreType!=null)
                strSql.Append( "[StoreType]=@StoreType,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.TopIndex!=null)
                strSql.Append( "[TopIndex]=@TopIndex,");
            if (pIsUpdateNullField || pEntity.SkuID!=null)
                strSql.Append( "[SkuID]=@SkuID,");
            if (pIsUpdateNullField || pEntity.item_id!=null)
                strSql.Append( "[item_id]=@item_id,");
            if (pIsUpdateNullField || pEntity.item_code!=null)
                strSql.Append( "[item_code]=@item_code,");
            if (pIsUpdateNullField || pEntity.item_name!=null)
                strSql.Append( "[item_name]=@item_name,");
            if (pIsUpdateNullField || pEntity.SkuName!=null)
                strSql.Append( "[SkuName]=@SkuName,");
            if (pIsUpdateNullField || pEntity.SalesQty!=null)
                strSql.Append( "[SalesQty]=@SalesQty,");
            if (pIsUpdateNullField || pEntity.SalesAmount!=null)
                strSql.Append( "[SalesAmount]=@SalesAmount");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@DateCode",SqlDbType.Date),
					new SqlParameter("@UnitId",SqlDbType.NVarChar),
					new SqlParameter("@UnitCode",SqlDbType.NVarChar),
					new SqlParameter("@UnitName",SqlDbType.NVarChar),
					new SqlParameter("@StoreType",SqlDbType.NVarChar),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@TopIndex",SqlDbType.Int),
					new SqlParameter("@SkuID",SqlDbType.VarChar),
					new SqlParameter("@item_id",SqlDbType.NVarChar),
					new SqlParameter("@item_code",SqlDbType.NVarChar),
					new SqlParameter("@item_name",SqlDbType.NVarChar),
					new SqlParameter("@SkuName",SqlDbType.VarChar),
					new SqlParameter("@SalesQty",SqlDbType.Decimal),
					new SqlParameter("@SalesAmount",SqlDbType.Decimal),
					new SqlParameter("@ID",SqlDbType.VarChar)
            };
			parameters[0].Value = pEntity.DateCode;
			parameters[1].Value = pEntity.UnitId;
			parameters[2].Value = pEntity.UnitCode;
			parameters[3].Value = pEntity.UnitName;
			parameters[4].Value = pEntity.StoreType;
			parameters[5].Value = pEntity.CustomerId;
			parameters[6].Value = pEntity.TopIndex;
			parameters[7].Value = pEntity.SkuID;
			parameters[8].Value = pEntity.item_id;
			parameters[9].Value = pEntity.item_code;
			parameters[10].Value = pEntity.item_name;
			parameters[11].Value = pEntity.SkuName;
			parameters[12].Value = pEntity.SalesQty;
			parameters[13].Value = pEntity.SalesAmount;
			parameters[14].Value = pEntity.ID;

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
        public void Update(R_UnitProductMonthSalesTopEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(R_UnitProductMonthSalesTopEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(R_UnitProductMonthSalesTopEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ID == null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.ID, pTran);           
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
            sql.AppendLine("update [R_UnitProductMonthSalesTop] set  where ID=@ID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@ID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(R_UnitProductMonthSalesTopEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (pEntity.ID == null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.ID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(R_UnitProductMonthSalesTopEntity[] pEntities)
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
            sql.AppendLine("update [R_UnitProductMonthSalesTop] set  where ID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public R_UnitProductMonthSalesTopEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [R_UnitProductMonthSalesTop] where 1=1  ");
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
            List<R_UnitProductMonthSalesTopEntity> list = new List<R_UnitProductMonthSalesTopEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    R_UnitProductMonthSalesTopEntity m;
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
        public PagedQueryResult<R_UnitProductMonthSalesTopEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [ID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [R_UnitProductMonthSalesTop] where 1=1  ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [R_UnitProductMonthSalesTop] where 1=1  ");
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
            PagedQueryResult<R_UnitProductMonthSalesTopEntity> result = new PagedQueryResult<R_UnitProductMonthSalesTopEntity>();
            List<R_UnitProductMonthSalesTopEntity> list = new List<R_UnitProductMonthSalesTopEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    R_UnitProductMonthSalesTopEntity m;
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
        public R_UnitProductMonthSalesTopEntity[] QueryByEntity(R_UnitProductMonthSalesTopEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<R_UnitProductMonthSalesTopEntity> PagedQueryByEntity(R_UnitProductMonthSalesTopEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(R_UnitProductMonthSalesTopEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.ID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ID", Value = pQueryEntity.ID });
            if (pQueryEntity.DateCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DateCode", Value = pQueryEntity.DateCode });
            if (pQueryEntity.UnitId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitId", Value = pQueryEntity.UnitId });
            if (pQueryEntity.UnitCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitCode", Value = pQueryEntity.UnitCode });
            if (pQueryEntity.UnitName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitName", Value = pQueryEntity.UnitName });
            if (pQueryEntity.StoreType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StoreType", Value = pQueryEntity.StoreType });
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
            if (pQueryEntity.TopIndex!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TopIndex", Value = pQueryEntity.TopIndex });
            if (pQueryEntity.SkuID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SkuID", Value = pQueryEntity.SkuID });
            if (pQueryEntity.item_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "item_id", Value = pQueryEntity.item_id });
            if (pQueryEntity.item_code!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "item_code", Value = pQueryEntity.item_code });
            if (pQueryEntity.item_name!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "item_name", Value = pQueryEntity.item_name });
            if (pQueryEntity.SkuName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SkuName", Value = pQueryEntity.SkuName });
            if (pQueryEntity.SalesQty!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesQty", Value = pQueryEntity.SalesQty });
            if (pQueryEntity.SalesAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesAmount", Value = pQueryEntity.SalesAmount });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out R_UnitProductMonthSalesTopEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new R_UnitProductMonthSalesTopEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["ID"] != DBNull.Value)
			{
				pInstance.ID =  Convert.ToString(pReader["ID"]);
			}
			if (pReader["DateCode"] != DBNull.Value)
			{
				pInstance.DateCode = Convert.ToDateTime(pReader["DateCode"]);
			}
			if (pReader["UnitId"] != DBNull.Value)
			{
				pInstance.UnitId =  Convert.ToString(pReader["UnitId"]);
			}
			if (pReader["UnitCode"] != DBNull.Value)
			{
				pInstance.UnitCode =  Convert.ToString(pReader["UnitCode"]);
			}
			if (pReader["UnitName"] != DBNull.Value)
			{
				pInstance.UnitName =  Convert.ToString(pReader["UnitName"]);
			}
			if (pReader["StoreType"] != DBNull.Value)
			{
				pInstance.StoreType =  Convert.ToString(pReader["StoreType"]);
			}
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}
			if (pReader["TopIndex"] != DBNull.Value)
			{
				pInstance.TopIndex =   Convert.ToInt32(pReader["TopIndex"]);
			}
			if (pReader["SkuID"] != DBNull.Value)
			{
				pInstance.SkuID =  Convert.ToString(pReader["SkuID"]);
			}
			if (pReader["item_id"] != DBNull.Value)
			{
				pInstance.item_id =  Convert.ToString(pReader["item_id"]);
			}
			if (pReader["item_code"] != DBNull.Value)
			{
				pInstance.item_code =  Convert.ToString(pReader["item_code"]);
			}
			if (pReader["item_name"] != DBNull.Value)
			{
				pInstance.item_name =  Convert.ToString(pReader["item_name"]);
			}
			if (pReader["SkuName"] != DBNull.Value)
			{
				pInstance.SkuName =  Convert.ToString(pReader["SkuName"]);
			}
			if (pReader["SalesQty"] != DBNull.Value)
			{
				pInstance.SalesQty =  Convert.ToDecimal(pReader["SalesQty"]);
			}
			if (pReader["SalesAmount"] != DBNull.Value)
			{
				pInstance.SalesAmount =  Convert.ToDecimal(pReader["SalesAmount"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}

        }
        #endregion
    }
}
