/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-7-17 11:24:52
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
    /// 表AgentCustomer的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class AgentCustomerDAO : Base.BaseCPOSDAO, ICRUDable<AgentCustomerEntity>, IQueryable<AgentCustomerEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public AgentCustomerDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(AgentCustomerEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(AgentCustomerEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [AgentCustomer](");
            strSql.Append("[AgentName],[AgentPhone],[AgentMail],[AgentPost],[CompanyName],[CompanyScale],[StoreNumber],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[CustomerID],[TryOrAgent],[AgentID])");
            strSql.Append(" values (");
            strSql.Append("@AgentName,@AgentPhone,@AgentMail,@AgentPost,@CompanyName,@CompanyScale,@StoreNumber,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@CustomerID,@TryOrAgent,@AgentID)");            

			string pkString = pEntity.AgentID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@AgentName",SqlDbType.NVarChar),
					new SqlParameter("@AgentPhone",SqlDbType.NVarChar),
					new SqlParameter("@AgentMail",SqlDbType.NVarChar),
					new SqlParameter("@AgentPost",SqlDbType.NVarChar),
					new SqlParameter("@CompanyName",SqlDbType.NVarChar),
					new SqlParameter("@CompanyScale",SqlDbType.NVarChar),
					new SqlParameter("@StoreNumber",SqlDbType.Int),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@TryOrAgent",SqlDbType.Int),
					new SqlParameter("@AgentID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.AgentName;
			parameters[1].Value = pEntity.AgentPhone;
			parameters[2].Value = pEntity.AgentMail;
			parameters[3].Value = pEntity.AgentPost;
			parameters[4].Value = pEntity.CompanyName;
			parameters[5].Value = pEntity.CompanyScale;
			parameters[6].Value = pEntity.StoreNumber;
			parameters[7].Value = pEntity.CreateTime;
			parameters[8].Value = pEntity.CreateBy;
			parameters[9].Value = pEntity.LastUpdateBy;
			parameters[10].Value = pEntity.LastUpdateTime;
			parameters[11].Value = pEntity.IsDelete;
			parameters[12].Value = pEntity.CustomerID;
			parameters[13].Value = pEntity.TryOrAgent;
			parameters[14].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.AgentID = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public AgentCustomerEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [AgentCustomer] where AgentID='{0}'  and isdelete=0 ", id.ToString());
            //读取数据
            AgentCustomerEntity m = null;
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
        public AgentCustomerEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [AgentCustomer] where 1=1  and isdelete=0");
            //读取数据
            List<AgentCustomerEntity> list = new List<AgentCustomerEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    AgentCustomerEntity m;
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
        public void Update(AgentCustomerEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(AgentCustomerEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.AgentID == null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [AgentCustomer] set ");
                        if (pIsUpdateNullField || pEntity.AgentName!=null)
                strSql.Append( "[AgentName]=@AgentName,");
            if (pIsUpdateNullField || pEntity.AgentPhone!=null)
                strSql.Append( "[AgentPhone]=@AgentPhone,");
            if (pIsUpdateNullField || pEntity.AgentMail!=null)
                strSql.Append( "[AgentMail]=@AgentMail,");
            if (pIsUpdateNullField || pEntity.AgentPost!=null)
                strSql.Append( "[AgentPost]=@AgentPost,");
            if (pIsUpdateNullField || pEntity.CompanyName!=null)
                strSql.Append( "[CompanyName]=@CompanyName,");
            if (pIsUpdateNullField || pEntity.CompanyScale!=null)
                strSql.Append( "[CompanyScale]=@CompanyScale,");
            if (pIsUpdateNullField || pEntity.StoreNumber!=null)
                strSql.Append( "[StoreNumber]=@StoreNumber,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.TryOrAgent!=null)
                strSql.Append( "[TryOrAgent]=@TryOrAgent");
            strSql.Append(" where AgentID=@AgentID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@AgentName",SqlDbType.NVarChar),
					new SqlParameter("@AgentPhone",SqlDbType.NVarChar),
					new SqlParameter("@AgentMail",SqlDbType.NVarChar),
					new SqlParameter("@AgentPost",SqlDbType.NVarChar),
					new SqlParameter("@CompanyName",SqlDbType.NVarChar),
					new SqlParameter("@CompanyScale",SqlDbType.NVarChar),
					new SqlParameter("@StoreNumber",SqlDbType.Int),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@TryOrAgent",SqlDbType.Int),
					new SqlParameter("@AgentID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.AgentName;
			parameters[1].Value = pEntity.AgentPhone;
			parameters[2].Value = pEntity.AgentMail;
			parameters[3].Value = pEntity.AgentPost;
			parameters[4].Value = pEntity.CompanyName;
			parameters[5].Value = pEntity.CompanyScale;
			parameters[6].Value = pEntity.StoreNumber;
			parameters[7].Value = pEntity.LastUpdateBy;
			parameters[8].Value = pEntity.LastUpdateTime;
			parameters[9].Value = pEntity.CustomerID;
			parameters[10].Value = pEntity.TryOrAgent;
			parameters[11].Value = pEntity.AgentID;

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
        public void Update(AgentCustomerEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(AgentCustomerEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(AgentCustomerEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.AgentID == null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.AgentID, pTran);           
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
            sql.AppendLine("update [AgentCustomer] set  isdelete=1 where AgentID=@AgentID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@AgentID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(AgentCustomerEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (pEntity.AgentID == null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.AgentID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(AgentCustomerEntity[] pEntities)
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
            sql.AppendLine("update [AgentCustomer] set  isdelete=1 where AgentID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public AgentCustomerEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [AgentCustomer] where 1=1  and isdelete=0 ");
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
            List<AgentCustomerEntity> list = new List<AgentCustomerEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    AgentCustomerEntity m;
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
        public PagedQueryResult<AgentCustomerEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [AgentID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [AgentCustomer] where 1=1  and isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [AgentCustomer] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<AgentCustomerEntity> result = new PagedQueryResult<AgentCustomerEntity>();
            List<AgentCustomerEntity> list = new List<AgentCustomerEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    AgentCustomerEntity m;
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
        public AgentCustomerEntity[] QueryByEntity(AgentCustomerEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<AgentCustomerEntity> PagedQueryByEntity(AgentCustomerEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(AgentCustomerEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.AgentID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AgentID", Value = pQueryEntity.AgentID });
            if (pQueryEntity.AgentName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AgentName", Value = pQueryEntity.AgentName });
            if (pQueryEntity.AgentPhone!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AgentPhone", Value = pQueryEntity.AgentPhone });
            if (pQueryEntity.AgentMail!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AgentMail", Value = pQueryEntity.AgentMail });
            if (pQueryEntity.AgentPost!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AgentPost", Value = pQueryEntity.AgentPost });
            if (pQueryEntity.CompanyName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CompanyName", Value = pQueryEntity.CompanyName });
            if (pQueryEntity.CompanyScale!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CompanyScale", Value = pQueryEntity.CompanyScale });
            if (pQueryEntity.StoreNumber!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StoreNumber", Value = pQueryEntity.StoreNumber });
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
            if (pQueryEntity.CustomerID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });
            if (pQueryEntity.TryOrAgent!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TryOrAgent", Value = pQueryEntity.TryOrAgent });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out AgentCustomerEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new AgentCustomerEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["AgentID"] != DBNull.Value)
			{
				pInstance.AgentID =  Convert.ToString(pReader["AgentID"]);
			}
			if (pReader["AgentName"] != DBNull.Value)
			{
				pInstance.AgentName =  Convert.ToString(pReader["AgentName"]);
			}
			if (pReader["AgentPhone"] != DBNull.Value)
			{
				pInstance.AgentPhone =  Convert.ToString(pReader["AgentPhone"]);
			}
			if (pReader["AgentMail"] != DBNull.Value)
			{
				pInstance.AgentMail =  Convert.ToString(pReader["AgentMail"]);
			}
			if (pReader["AgentPost"] != DBNull.Value)
			{
				pInstance.AgentPost =  Convert.ToString(pReader["AgentPost"]);
			}
			if (pReader["CompanyName"] != DBNull.Value)
			{
				pInstance.CompanyName =  Convert.ToString(pReader["CompanyName"]);
			}
			if (pReader["CompanyScale"] != DBNull.Value)
			{
				pInstance.CompanyScale =  Convert.ToString(pReader["CompanyScale"]);
			}
			if (pReader["StoreNumber"] != DBNull.Value)
			{
				pInstance.StoreNumber =   Convert.ToInt32(pReader["StoreNumber"]);
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
			if (pReader["CustomerID"] != DBNull.Value)
			{
				pInstance.CustomerID =  Convert.ToString(pReader["CustomerID"]);
			}
			if (pReader["TryOrAgent"] != DBNull.Value)
			{
				pInstance.TryOrAgent =   Convert.ToInt32(pReader["TryOrAgent"]);
			}

        }
        #endregion
    }
}
