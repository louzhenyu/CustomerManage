/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-19 17:56:45
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
    /// 表T_Payment_detail的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_Payment_detailDAO : Base.BaseCPOSDAO, ICRUDable<T_Payment_detailEntity>, IQueryable<T_Payment_detailEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_Payment_detailDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(T_Payment_detailEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(T_Payment_detailEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [T_Payment_detail](");
            strSql.Append("[Inout_Id],[UnitCode],[Payment_Type_Id],[Payment_Type_Code],[Payment_Type_Name],[Price],[Total_Amount],[If_Flag],[Pay_Points],[CreateTime],[LastUpdateTime],[CreateBy],[LastUpdateBy],[IsDelete],[CustomerId],[Payment_Id])");
            strSql.Append(" values (");
            strSql.Append("@Inout_Id,@UnitCode,@Payment_Type_Id,@Payment_Type_Code,@Payment_Type_Name,@Price,@Total_Amount,@If_Flag,@Pay_Points,@CreateTime,@LastUpdateTime,@CreateBy,@LastUpdateBy,@IsDelete,@CustomerId,@Payment_Id)");            

			string pkString = pEntity.Payment_Id;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@Inout_Id",SqlDbType.NVarChar),
					new SqlParameter("@UnitCode",SqlDbType.VarChar),
					new SqlParameter("@Payment_Type_Id",SqlDbType.NVarChar),
					new SqlParameter("@Payment_Type_Code",SqlDbType.NVarChar),
					new SqlParameter("@Payment_Type_Name",SqlDbType.NVarChar),
					new SqlParameter("@Price",SqlDbType.Decimal),
					new SqlParameter("@Total_Amount",SqlDbType.Decimal),
					new SqlParameter("@If_Flag",SqlDbType.Int),
					new SqlParameter("@Pay_Points",SqlDbType.Decimal),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@Payment_Id",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.Inout_Id;
			parameters[1].Value = pEntity.UnitCode;
			parameters[2].Value = pEntity.Payment_Type_Id;
			parameters[3].Value = pEntity.Payment_Type_Code;
			parameters[4].Value = pEntity.Payment_Type_Name;
			parameters[5].Value = pEntity.Price;
			parameters[6].Value = pEntity.Total_Amount;
			parameters[7].Value = pEntity.If_Flag;
			parameters[8].Value = pEntity.Pay_Points;
			parameters[9].Value = pEntity.CreateTime;
			parameters[10].Value = pEntity.LastUpdateTime;
			parameters[11].Value = pEntity.CreateBy;
			parameters[12].Value = pEntity.LastUpdateBy;
			parameters[13].Value = pEntity.IsDelete;
			parameters[14].Value = pEntity.CustomerId;
			parameters[15].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.Payment_Id = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public T_Payment_detailEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Payment_detail] where Payment_Id='{0}'  and isdelete=0 ", id.ToString());
            //读取数据
            T_Payment_detailEntity m = null;
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
        public T_Payment_detailEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Payment_detail] where 1=1  and isdelete=0");
            //读取数据
            List<T_Payment_detailEntity> list = new List<T_Payment_detailEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_Payment_detailEntity m;
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
        public void Update(T_Payment_detailEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(T_Payment_detailEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.Payment_Id == null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_Payment_detail] set ");
                        if (pIsUpdateNullField || pEntity.Inout_Id!=null)
                strSql.Append( "[Inout_Id]=@Inout_Id,");
            if (pIsUpdateNullField || pEntity.UnitCode!=null)
                strSql.Append( "[UnitCode]=@UnitCode,");
            if (pIsUpdateNullField || pEntity.Payment_Type_Id!=null)
                strSql.Append( "[Payment_Type_Id]=@Payment_Type_Id,");
            if (pIsUpdateNullField || pEntity.Payment_Type_Code!=null)
                strSql.Append( "[Payment_Type_Code]=@Payment_Type_Code,");
            if (pIsUpdateNullField || pEntity.Payment_Type_Name!=null)
                strSql.Append( "[Payment_Type_Name]=@Payment_Type_Name,");
            if (pIsUpdateNullField || pEntity.Price!=null)
                strSql.Append( "[Price]=@Price,");
            if (pIsUpdateNullField || pEntity.Total_Amount!=null)
                strSql.Append( "[Total_Amount]=@Total_Amount,");
            if (pIsUpdateNullField || pEntity.If_Flag!=null)
                strSql.Append( "[If_Flag]=@If_Flag,");
            if (pIsUpdateNullField || pEntity.Pay_Points!=null)
                strSql.Append( "[Pay_Points]=@Pay_Points,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId");
            strSql.Append(" where Payment_Id=@Payment_Id ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@Inout_Id",SqlDbType.NVarChar),
					new SqlParameter("@UnitCode",SqlDbType.VarChar),
					new SqlParameter("@Payment_Type_Id",SqlDbType.NVarChar),
					new SqlParameter("@Payment_Type_Code",SqlDbType.NVarChar),
					new SqlParameter("@Payment_Type_Name",SqlDbType.NVarChar),
					new SqlParameter("@Price",SqlDbType.Decimal),
					new SqlParameter("@Total_Amount",SqlDbType.Decimal),
					new SqlParameter("@If_Flag",SqlDbType.Int),
					new SqlParameter("@Pay_Points",SqlDbType.Decimal),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@Payment_Id",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.Inout_Id;
			parameters[1].Value = pEntity.UnitCode;
			parameters[2].Value = pEntity.Payment_Type_Id;
			parameters[3].Value = pEntity.Payment_Type_Code;
			parameters[4].Value = pEntity.Payment_Type_Name;
			parameters[5].Value = pEntity.Price;
			parameters[6].Value = pEntity.Total_Amount;
			parameters[7].Value = pEntity.If_Flag;
			parameters[8].Value = pEntity.Pay_Points;
			parameters[9].Value = pEntity.LastUpdateTime;
			parameters[10].Value = pEntity.LastUpdateBy;
			parameters[11].Value = pEntity.CustomerId;
			parameters[12].Value = pEntity.Payment_Id;

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
        public void Update(T_Payment_detailEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(T_Payment_detailEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(T_Payment_detailEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.Payment_Id == null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.Payment_Id, pTran);           
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
            sql.AppendLine("update [T_Payment_detail] set  isdelete=1 where Payment_Id=@Payment_Id;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@Payment_Id",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(T_Payment_detailEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (pEntity.Payment_Id == null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.Payment_Id;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(T_Payment_detailEntity[] pEntities)
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
            sql.AppendLine("update [T_Payment_detail] set  isdelete=1 where Payment_Id in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public T_Payment_detailEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Payment_detail] where 1=1  and isdelete=0 ");
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
            List<T_Payment_detailEntity> list = new List<T_Payment_detailEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_Payment_detailEntity m;
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
        public PagedQueryResult<T_Payment_detailEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [Payment_Id] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [T_Payment_detail] where 1=1  and isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [T_Payment_detail] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<T_Payment_detailEntity> result = new PagedQueryResult<T_Payment_detailEntity>();
            List<T_Payment_detailEntity> list = new List<T_Payment_detailEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    T_Payment_detailEntity m;
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
        public T_Payment_detailEntity[] QueryByEntity(T_Payment_detailEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<T_Payment_detailEntity> PagedQueryByEntity(T_Payment_detailEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(T_Payment_detailEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.Payment_Id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Payment_Id", Value = pQueryEntity.Payment_Id });
            if (pQueryEntity.Inout_Id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Inout_Id", Value = pQueryEntity.Inout_Id });
            if (pQueryEntity.UnitCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitCode", Value = pQueryEntity.UnitCode });
            if (pQueryEntity.Payment_Type_Id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Payment_Type_Id", Value = pQueryEntity.Payment_Type_Id });
            if (pQueryEntity.Payment_Type_Code!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Payment_Type_Code", Value = pQueryEntity.Payment_Type_Code });
            if (pQueryEntity.Payment_Type_Name!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Payment_Type_Name", Value = pQueryEntity.Payment_Type_Name });
            if (pQueryEntity.Price!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Price", Value = pQueryEntity.Price });
            if (pQueryEntity.Total_Amount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Total_Amount", Value = pQueryEntity.Total_Amount });
            if (pQueryEntity.If_Flag!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "If_Flag", Value = pQueryEntity.If_Flag });
            if (pQueryEntity.Pay_Points!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Pay_Points", Value = pQueryEntity.Pay_Points });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.LastUpdateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.LastUpdateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.IsDelete!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out T_Payment_detailEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new T_Payment_detailEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["Payment_Id"] != DBNull.Value)
			{
				pInstance.Payment_Id =  Convert.ToString(pReader["Payment_Id"]);
			}
			if (pReader["Inout_Id"] != DBNull.Value)
			{
				pInstance.Inout_Id =  Convert.ToString(pReader["Inout_Id"]);
			}
			if (pReader["UnitCode"] != DBNull.Value)
			{
				pInstance.UnitCode =  Convert.ToString(pReader["UnitCode"]);
			}
			if (pReader["Payment_Type_Id"] != DBNull.Value)
			{
				pInstance.Payment_Type_Id =  Convert.ToString(pReader["Payment_Type_Id"]);
			}
			if (pReader["Payment_Type_Code"] != DBNull.Value)
			{
				pInstance.Payment_Type_Code =  Convert.ToString(pReader["Payment_Type_Code"]);
			}
			if (pReader["Payment_Type_Name"] != DBNull.Value)
			{
				pInstance.Payment_Type_Name =  Convert.ToString(pReader["Payment_Type_Name"]);
			}
			if (pReader["Price"] != DBNull.Value)
			{
				pInstance.Price =  Convert.ToDecimal(pReader["Price"]);
			}
			if (pReader["Total_Amount"] != DBNull.Value)
			{
				pInstance.Total_Amount =  Convert.ToDecimal(pReader["Total_Amount"]);
			}
			if (pReader["If_Flag"] != DBNull.Value)
			{
				pInstance.If_Flag =   Convert.ToInt32(pReader["If_Flag"]);
			}
			if (pReader["Pay_Points"] != DBNull.Value)
			{
				pInstance.Pay_Points =  Convert.ToDecimal(pReader["Pay_Points"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}
			if (pReader["LastUpdateTime"] != DBNull.Value)
			{
				pInstance.LastUpdateTime =  Convert.ToDateTime(pReader["LastUpdateTime"]);
			}
			if (pReader["CreateBy"] != DBNull.Value)
			{
				pInstance.CreateBy =  Convert.ToString(pReader["CreateBy"]);
			}
			if (pReader["LastUpdateBy"] != DBNull.Value)
			{
				pInstance.LastUpdateBy =  Convert.ToString(pReader["LastUpdateBy"]);
			}
			if (pReader["IsDelete"] != DBNull.Value)
			{
				pInstance.IsDelete =   Convert.ToInt32(pReader["IsDelete"]);
			}
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}

        }
        #endregion
    }
}
