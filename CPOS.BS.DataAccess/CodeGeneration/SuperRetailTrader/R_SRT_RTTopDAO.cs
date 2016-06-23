/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/1 19:09:45
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
    /// 表R_SRT_RTTop的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class R_SRT_RTTopDAO : Base.BaseCPOSDAO, ICRUDable<R_SRT_RTTopEntity>, IQueryable<R_SRT_RTTopEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public R_SRT_RTTopDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(R_SRT_RTTopEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(R_SRT_RTTopEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //初始化固定字段
			pEntity.CreateTime=DateTime.Now;


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [R_SRT_RTTop](");
            strSql.Append("[DateCode],[CustomerId],[BusiType],[TopType],[SuperRetailTraderID],[SuperRetailTraderName],[SuperRetailJoinTime],[Idx],[SalesAmount],[AddRTCount],[CreateTime],[ID])");
            strSql.Append(" values (");
            strSql.Append("@DateCode,@CustomerId,@BusiType,@TopType,@SuperRetailTraderID,@SuperRetailTraderName,@SuperRetailJoinTime,@Idx,@SalesAmount,@AddRTCount,@CreateTime,@ID)");            

			Guid? pkGuid;
			if (pEntity.ID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.ID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@DateCode",SqlDbType.Date),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@BusiType",SqlDbType.Int),
					new SqlParameter("@TopType",SqlDbType.Int),
					new SqlParameter("@SuperRetailTraderID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@SuperRetailTraderName",SqlDbType.NVarChar),
					new SqlParameter("@SuperRetailJoinTime",SqlDbType.DateTime),
					new SqlParameter("@Idx",SqlDbType.Int),
					new SqlParameter("@SalesAmount",SqlDbType.Decimal),
					new SqlParameter("@AddRTCount",SqlDbType.Int),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@ID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.DateCode;
			parameters[1].Value = pEntity.CustomerId;
			parameters[2].Value = pEntity.BusiType;
			parameters[3].Value = pEntity.TopType;
			parameters[4].Value = pEntity.SuperRetailTraderID;
			parameters[5].Value = pEntity.SuperRetailTraderName;
			parameters[6].Value = pEntity.SuperRetailJoinTime;
			parameters[7].Value = pEntity.Idx;
			parameters[8].Value = pEntity.SalesAmount;
			parameters[9].Value = pEntity.AddRTCount;
			parameters[10].Value = pEntity.CreateTime;
			parameters[11].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.ID = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public R_SRT_RTTopEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [R_SRT_RTTop] where ID='{0}'  ", id.ToString());
            //读取数据
            R_SRT_RTTopEntity m = null;
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
        public R_SRT_RTTopEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [R_SRT_RTTop] where 1=1 ");
            //读取数据
            List<R_SRT_RTTopEntity> list = new List<R_SRT_RTTopEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    R_SRT_RTTopEntity m;
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
        public void Update(R_SRT_RTTopEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(R_SRT_RTTopEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.ID.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [R_SRT_RTTop] set ");
                        if (pIsUpdateNullField || pEntity.DateCode!=null)
                strSql.Append( "[DateCode]=@DateCode,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.BusiType!=null)
                strSql.Append( "[BusiType]=@BusiType,");
            if (pIsUpdateNullField || pEntity.TopType!=null)
                strSql.Append( "[TopType]=@TopType,");
            if (pIsUpdateNullField || pEntity.SuperRetailTraderID!=null)
                strSql.Append( "[SuperRetailTraderID]=@SuperRetailTraderID,");
            if (pIsUpdateNullField || pEntity.SuperRetailTraderName!=null)
                strSql.Append( "[SuperRetailTraderName]=@SuperRetailTraderName,");
            if (pIsUpdateNullField || pEntity.SuperRetailJoinTime!=null)
                strSql.Append( "[SuperRetailJoinTime]=@SuperRetailJoinTime,");
            if (pIsUpdateNullField || pEntity.Idx!=null)
                strSql.Append( "[Idx]=@Idx,");
            if (pIsUpdateNullField || pEntity.SalesAmount!=null)
                strSql.Append( "[SalesAmount]=@SalesAmount,");
            if (pIsUpdateNullField || pEntity.AddRTCount!=null)
                strSql.Append( "[AddRTCount]=@AddRTCount");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@DateCode",SqlDbType.Date),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@BusiType",SqlDbType.Int),
					new SqlParameter("@TopType",SqlDbType.Int),
					new SqlParameter("@SuperRetailTraderID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@SuperRetailTraderName",SqlDbType.NVarChar),
					new SqlParameter("@SuperRetailJoinTime",SqlDbType.DateTime),
					new SqlParameter("@Idx",SqlDbType.Int),
					new SqlParameter("@SalesAmount",SqlDbType.Decimal),
					new SqlParameter("@AddRTCount",SqlDbType.Int),
					new SqlParameter("@ID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.DateCode;
			parameters[1].Value = pEntity.CustomerId;
			parameters[2].Value = pEntity.BusiType;
			parameters[3].Value = pEntity.TopType;
			parameters[4].Value = pEntity.SuperRetailTraderID;
			parameters[5].Value = pEntity.SuperRetailTraderName;
			parameters[6].Value = pEntity.SuperRetailJoinTime;
			parameters[7].Value = pEntity.Idx;
			parameters[8].Value = pEntity.SalesAmount;
			parameters[9].Value = pEntity.AddRTCount;
			parameters[10].Value = pEntity.ID;

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
        public void Update(R_SRT_RTTopEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(R_SRT_RTTopEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(R_SRT_RTTopEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.ID.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.ID.Value, pTran);           
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
            sql.AppendLine("update [R_SRT_RTTop] set  where ID=@ID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@ID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(R_SRT_RTTopEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.ID.HasValue)
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
        public void Delete(R_SRT_RTTopEntity[] pEntities)
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
            sql.AppendLine("update [R_SRT_RTTop] set  where ID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public R_SRT_RTTopEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [R_SRT_RTTop] where 1=1  ");
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
            List<R_SRT_RTTopEntity> list = new List<R_SRT_RTTopEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    R_SRT_RTTopEntity m;
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
        public PagedQueryResult<R_SRT_RTTopEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
            pagedSql.AppendFormat(") as ___rn,* from [R_SRT_RTTop] where 1=1  ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [R_SRT_RTTop] where 1=1  ");
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
            PagedQueryResult<R_SRT_RTTopEntity> result = new PagedQueryResult<R_SRT_RTTopEntity>();
            List<R_SRT_RTTopEntity> list = new List<R_SRT_RTTopEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    R_SRT_RTTopEntity m;
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
        public R_SRT_RTTopEntity[] QueryByEntity(R_SRT_RTTopEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<R_SRT_RTTopEntity> PagedQueryByEntity(R_SRT_RTTopEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(R_SRT_RTTopEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.DateCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DateCode", Value = pQueryEntity.DateCode });
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
            if (pQueryEntity.BusiType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BusiType", Value = pQueryEntity.BusiType });
            if (pQueryEntity.TopType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TopType", Value = pQueryEntity.TopType });
            if (pQueryEntity.SuperRetailTraderID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SuperRetailTraderID", Value = pQueryEntity.SuperRetailTraderID });
            if (pQueryEntity.SuperRetailTraderName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SuperRetailTraderName", Value = pQueryEntity.SuperRetailTraderName });
            if (pQueryEntity.SuperRetailJoinTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SuperRetailJoinTime", Value = pQueryEntity.SuperRetailJoinTime });
            if (pQueryEntity.Idx!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Idx", Value = pQueryEntity.Idx });
            if (pQueryEntity.SalesAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesAmount", Value = pQueryEntity.SalesAmount });
            if (pQueryEntity.AddRTCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AddRTCount", Value = pQueryEntity.AddRTCount });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.ID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ID", Value = pQueryEntity.ID });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out R_SRT_RTTopEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new R_SRT_RTTopEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["DateCode"] != DBNull.Value)
			{
				pInstance.DateCode =Convert.ToDateTime(pReader["DateCode"]);
			}
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}
			if (pReader["BusiType"] != DBNull.Value)
			{
				pInstance.BusiType =   Convert.ToInt32(pReader["BusiType"]);
			}
			if (pReader["TopType"] != DBNull.Value)
			{
				pInstance.TopType =   Convert.ToInt32(pReader["TopType"]);
			}
			if (pReader["SuperRetailTraderID"] != DBNull.Value)
			{
				pInstance.SuperRetailTraderID =  (Guid)pReader["SuperRetailTraderID"];
			}
			if (pReader["SuperRetailTraderName"] != DBNull.Value)
			{
				pInstance.SuperRetailTraderName =  Convert.ToString(pReader["SuperRetailTraderName"]);
			}
			if (pReader["SuperRetailJoinTime"] != DBNull.Value)
			{
				pInstance.SuperRetailJoinTime =  Convert.ToDateTime(pReader["SuperRetailJoinTime"]);
			}
			if (pReader["Idx"] != DBNull.Value)
			{
				pInstance.Idx =   Convert.ToInt32(pReader["Idx"]);
			}
			if (pReader["SalesAmount"] != DBNull.Value)
			{
				pInstance.SalesAmount =  Convert.ToDecimal(pReader["SalesAmount"]);
			}
			if (pReader["AddRTCount"] != DBNull.Value)
			{
				pInstance.AddRTCount =   Convert.ToInt32(pReader["AddRTCount"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}
			if (pReader["ID"] != DBNull.Value)
			{
				pInstance.ID =  (Guid)pReader["ID"];
			}

        }
        #endregion
    }
}
