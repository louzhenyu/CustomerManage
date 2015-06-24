/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/5/24 21:31:17
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
    /// 数据访问： 分销商包含门店和个人 
    /// 表RetailTrader的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class RetailTraderDAO : Base.BaseCPOSDAO, ICRUDable<RetailTraderEntity>, IQueryable<RetailTraderEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public RetailTraderDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(RetailTraderEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(RetailTraderEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [RetailTrader](");
            strSql.Append("[RetailTraderCode],[RetailTraderName],[RetailTraderLogin],[RetailTraderPass],[RetailTraderType],[RetailTraderMan],[RetailTraderPhone],[RetailTraderAddress],[CooperateType],[SellUserID],[UnitID],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[CustomerId],[Status],[RetailTraderID])");
            strSql.Append(" values (");
            strSql.Append("@RetailTraderCode,@RetailTraderName,@RetailTraderLogin,@RetailTraderPass,@RetailTraderType,@RetailTraderMan,@RetailTraderPhone,@RetailTraderAddress,@CooperateType,@SellUserID,@UnitID,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@CustomerId,@Status,@RetailTraderID)");            

			string pkString = pEntity.RetailTraderID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@RetailTraderCode",SqlDbType.Int),
					new SqlParameter("@RetailTraderName",SqlDbType.NVarChar),
					new SqlParameter("@RetailTraderLogin",SqlDbType.NVarChar),
					new SqlParameter("@RetailTraderPass",SqlDbType.NVarChar),
					new SqlParameter("@RetailTraderType",SqlDbType.NVarChar),
					new SqlParameter("@RetailTraderMan",SqlDbType.NVarChar),
					new SqlParameter("@RetailTraderPhone",SqlDbType.NVarChar),
					new SqlParameter("@RetailTraderAddress",SqlDbType.NVarChar),
					new SqlParameter("@CooperateType",SqlDbType.NVarChar),
					new SqlParameter("@SellUserID",SqlDbType.NVarChar),
					new SqlParameter("@UnitID",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.NVarChar),
					new SqlParameter("@RetailTraderID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.RetailTraderCode;
			parameters[1].Value = pEntity.RetailTraderName;
			parameters[2].Value = pEntity.RetailTraderLogin;
			parameters[3].Value = pEntity.RetailTraderPass;
			parameters[4].Value = pEntity.RetailTraderType;
			parameters[5].Value = pEntity.RetailTraderMan;
			parameters[6].Value = pEntity.RetailTraderPhone;
			parameters[7].Value = pEntity.RetailTraderAddress;
			parameters[8].Value = pEntity.CooperateType;
			parameters[9].Value = pEntity.SellUserID;
			parameters[10].Value = pEntity.UnitID;
			parameters[11].Value = pEntity.CreateTime;
			parameters[12].Value = pEntity.CreateBy;
			parameters[13].Value = pEntity.LastUpdateBy;
			parameters[14].Value = pEntity.LastUpdateTime;
			parameters[15].Value = pEntity.IsDelete;
			parameters[16].Value = pEntity.CustomerId;
			parameters[17].Value = pEntity.Status;
			parameters[18].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.RetailTraderID = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public RetailTraderEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [RetailTrader] where RetailTraderID='{0}'  and isdelete=0 ", id.ToString());
            //读取数据
            RetailTraderEntity m = null;
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
        public RetailTraderEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [RetailTrader] where 1=1  and isdelete=0");
            //读取数据
            List<RetailTraderEntity> list = new List<RetailTraderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    RetailTraderEntity m;
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
        public void Update(RetailTraderEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(RetailTraderEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.RetailTraderID == null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [RetailTrader] set ");
                        if (pIsUpdateNullField || pEntity.RetailTraderCode!=null)
                strSql.Append( "[RetailTraderCode]=@RetailTraderCode,");
            if (pIsUpdateNullField || pEntity.RetailTraderName!=null)
                strSql.Append( "[RetailTraderName]=@RetailTraderName,");
            if (pIsUpdateNullField || pEntity.RetailTraderLogin!=null)
                strSql.Append( "[RetailTraderLogin]=@RetailTraderLogin,");
            if (pIsUpdateNullField || pEntity.RetailTraderPass!=null)
                strSql.Append( "[RetailTraderPass]=@RetailTraderPass,");
            if (pIsUpdateNullField || pEntity.RetailTraderType!=null)
                strSql.Append( "[RetailTraderType]=@RetailTraderType,");
            if (pIsUpdateNullField || pEntity.RetailTraderMan!=null)
                strSql.Append( "[RetailTraderMan]=@RetailTraderMan,");
            if (pIsUpdateNullField || pEntity.RetailTraderPhone!=null)
                strSql.Append( "[RetailTraderPhone]=@RetailTraderPhone,");
            if (pIsUpdateNullField || pEntity.RetailTraderAddress!=null)
                strSql.Append( "[RetailTraderAddress]=@RetailTraderAddress,");
            if (pIsUpdateNullField || pEntity.CooperateType!=null)
                strSql.Append( "[CooperateType]=@CooperateType,");
            if (pIsUpdateNullField || pEntity.SellUserID!=null)
                strSql.Append( "[SellUserID]=@SellUserID,");
            if (pIsUpdateNullField || pEntity.UnitID!=null)
                strSql.Append( "[UnitID]=@UnitID,");
         
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.Status != null)
                strSql.Append("[Status]=@Status,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime");

            strSql.Append(" where RetailTraderID=@RetailTraderID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@RetailTraderCode",SqlDbType.Int),
					new SqlParameter("@RetailTraderName",SqlDbType.NVarChar),
					new SqlParameter("@RetailTraderLogin",SqlDbType.NVarChar),
					new SqlParameter("@RetailTraderPass",SqlDbType.NVarChar),
					new SqlParameter("@RetailTraderType",SqlDbType.NVarChar),
					new SqlParameter("@RetailTraderMan",SqlDbType.NVarChar),
					new SqlParameter("@RetailTraderPhone",SqlDbType.NVarChar),
					new SqlParameter("@RetailTraderAddress",SqlDbType.NVarChar),
					new SqlParameter("@CooperateType",SqlDbType.NVarChar),
					new SqlParameter("@SellUserID",SqlDbType.NVarChar),
					new SqlParameter("@UnitID",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.NVarChar),
					new SqlParameter("@RetailTraderID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.RetailTraderCode;
			parameters[1].Value = pEntity.RetailTraderName;
			parameters[2].Value = pEntity.RetailTraderLogin;
			parameters[3].Value = pEntity.RetailTraderPass;
			parameters[4].Value = pEntity.RetailTraderType;
			parameters[5].Value = pEntity.RetailTraderMan;
			parameters[6].Value = pEntity.RetailTraderPhone;
			parameters[7].Value = pEntity.RetailTraderAddress;
			parameters[8].Value = pEntity.CooperateType;
			parameters[9].Value = pEntity.SellUserID;
			parameters[10].Value = pEntity.UnitID;
			parameters[11].Value = pEntity.LastUpdateBy;
			parameters[12].Value = pEntity.LastUpdateTime;
			parameters[13].Value = pEntity.CustomerId;
			parameters[14].Value = pEntity.Status;
			parameters[15].Value = pEntity.RetailTraderID;

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
        public void Update(RetailTraderEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(RetailTraderEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(RetailTraderEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.RetailTraderID == null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.RetailTraderID, pTran);           
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
            sql.AppendLine("update [RetailTrader] set  isdelete=1 where RetailTraderID=@RetailTraderID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@RetailTraderID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(RetailTraderEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (pEntity.RetailTraderID == null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.RetailTraderID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(RetailTraderEntity[] pEntities)
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
            sql.AppendLine("update [RetailTrader] set  isdelete=1 where RetailTraderID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public RetailTraderEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [RetailTrader] where 1=1  and isdelete=0 ");
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
            List<RetailTraderEntity> list = new List<RetailTraderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    RetailTraderEntity m;
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
        public PagedQueryResult<RetailTraderEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [RetailTraderID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [RetailTrader] where 1=1  and isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [RetailTrader] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<RetailTraderEntity> result = new PagedQueryResult<RetailTraderEntity>();
            List<RetailTraderEntity> list = new List<RetailTraderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    RetailTraderEntity m;
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
        public RetailTraderEntity[] QueryByEntity(RetailTraderEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<RetailTraderEntity> PagedQueryByEntity(RetailTraderEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(RetailTraderEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.RetailTraderID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RetailTraderID", Value = pQueryEntity.RetailTraderID });
            if (pQueryEntity.RetailTraderCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RetailTraderCode", Value = pQueryEntity.RetailTraderCode });
            if (pQueryEntity.RetailTraderName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RetailTraderName", Value = pQueryEntity.RetailTraderName });
            if (pQueryEntity.RetailTraderLogin!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RetailTraderLogin", Value = pQueryEntity.RetailTraderLogin });
            if (pQueryEntity.RetailTraderPass!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RetailTraderPass", Value = pQueryEntity.RetailTraderPass });
            if (pQueryEntity.RetailTraderType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RetailTraderType", Value = pQueryEntity.RetailTraderType });
            if (pQueryEntity.RetailTraderMan!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RetailTraderMan", Value = pQueryEntity.RetailTraderMan });
            if (pQueryEntity.RetailTraderPhone!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RetailTraderPhone", Value = pQueryEntity.RetailTraderPhone });
            if (pQueryEntity.RetailTraderAddress!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RetailTraderAddress", Value = pQueryEntity.RetailTraderAddress });
            if (pQueryEntity.CooperateType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CooperateType", Value = pQueryEntity.CooperateType });
            if (pQueryEntity.SellUserID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SellUserID", Value = pQueryEntity.SellUserID });
            if (pQueryEntity.UnitID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitID", Value = pQueryEntity.UnitID });
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
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
            if (pQueryEntity.Status!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Status", Value = pQueryEntity.Status });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out RetailTraderEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new RetailTraderEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["RetailTraderID"] != DBNull.Value)
			{
				pInstance.RetailTraderID =  Convert.ToString(pReader["RetailTraderID"]);
			}
			if (pReader["RetailTraderCode"] != DBNull.Value)
			{
				pInstance.RetailTraderCode =   Convert.ToInt32(pReader["RetailTraderCode"]);
			}
			if (pReader["RetailTraderName"] != DBNull.Value)
			{
				pInstance.RetailTraderName =  Convert.ToString(pReader["RetailTraderName"]);
			}
			if (pReader["RetailTraderLogin"] != DBNull.Value)
			{
				pInstance.RetailTraderLogin =  Convert.ToString(pReader["RetailTraderLogin"]);
			}
			if (pReader["RetailTraderPass"] != DBNull.Value)
			{
				pInstance.RetailTraderPass =  Convert.ToString(pReader["RetailTraderPass"]);
			}
			if (pReader["RetailTraderType"] != DBNull.Value)
			{
				pInstance.RetailTraderType =  Convert.ToString(pReader["RetailTraderType"]);
			}
			if (pReader["RetailTraderMan"] != DBNull.Value)
			{
				pInstance.RetailTraderMan =  Convert.ToString(pReader["RetailTraderMan"]);
			}
			if (pReader["RetailTraderPhone"] != DBNull.Value)
			{
				pInstance.RetailTraderPhone =  Convert.ToString(pReader["RetailTraderPhone"]);
			}
			if (pReader["RetailTraderAddress"] != DBNull.Value)
			{
				pInstance.RetailTraderAddress =  Convert.ToString(pReader["RetailTraderAddress"]);
			}
			if (pReader["CooperateType"] != DBNull.Value)
			{
				pInstance.CooperateType =  Convert.ToString(pReader["CooperateType"]);
			}
			if (pReader["SellUserID"] != DBNull.Value)
			{
				pInstance.SellUserID =  Convert.ToString(pReader["SellUserID"]);
			}
			if (pReader["UnitID"] != DBNull.Value)
			{
				pInstance.UnitID =  Convert.ToString(pReader["UnitID"]);
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
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}
			if (pReader["Status"] != DBNull.Value)
			{
				pInstance.Status =  Convert.ToString(pReader["Status"]);
			}

        }
        #endregion
    }
}
