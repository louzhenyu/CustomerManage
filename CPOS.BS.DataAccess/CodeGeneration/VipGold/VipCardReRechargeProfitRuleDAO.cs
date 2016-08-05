/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/25 14:47:15
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
    /// 表VipCardReRechargeProfitRule的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VipCardReRechargeProfitRuleDAO : Base.BaseCPOSDAO, ICRUDable<VipCardReRechargeProfitRuleEntity>, IQueryable<VipCardReRechargeProfitRuleEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VipCardReRechargeProfitRuleDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(VipCardReRechargeProfitRuleEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(VipCardReRechargeProfitRuleEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [VipCardReRechargeProfitRule](");
            strSql.Append("[CardBuyToProfitRuleId],[VipCardTypeID],[ProfitType],[LimitAmount],[ProfitPct],[CustomerID],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[ReRechargeProfitRuleId])");
            strSql.Append(" values (");
            strSql.Append("@CardBuyToProfitRuleId,@VipCardTypeID,@ProfitType,@LimitAmount,@ProfitPct,@CustomerID,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@ReRechargeProfitRuleId)");            

			Guid? pkGuid;
			if (pEntity.ReRechargeProfitRuleId == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.ReRechargeProfitRuleId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@CardBuyToProfitRuleId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@VipCardTypeID",SqlDbType.Int),
					new SqlParameter("@ProfitType",SqlDbType.VarChar),
					new SqlParameter("@LimitAmount",SqlDbType.Decimal),
					new SqlParameter("@ProfitPct",SqlDbType.Decimal),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@ReRechargeProfitRuleId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.CardBuyToProfitRuleId;
			parameters[1].Value = pEntity.VipCardTypeID;
			parameters[2].Value = pEntity.ProfitType;
			parameters[3].Value = pEntity.LimitAmount;
			parameters[4].Value = pEntity.ProfitPct;
			parameters[5].Value = pEntity.CustomerID;
			parameters[6].Value = pEntity.CreateTime;
			parameters[7].Value = pEntity.CreateBy;
			parameters[8].Value = pEntity.LastUpdateTime;
			parameters[9].Value = pEntity.LastUpdateBy;
			parameters[10].Value = pEntity.IsDelete;
			parameters[11].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.ReRechargeProfitRuleId = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public VipCardReRechargeProfitRuleEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCardReRechargeProfitRule] where ReRechargeProfitRuleId='{0}'  and isdelete=0 ", id.ToString());
            //读取数据
            VipCardReRechargeProfitRuleEntity m = null;
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
        public VipCardReRechargeProfitRuleEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCardReRechargeProfitRule] where 1=1  and isdelete=0");
            //读取数据
            List<VipCardReRechargeProfitRuleEntity> list = new List<VipCardReRechargeProfitRuleEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardReRechargeProfitRuleEntity m;
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
        public void Update(VipCardReRechargeProfitRuleEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(VipCardReRechargeProfitRuleEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.ReRechargeProfitRuleId.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [VipCardReRechargeProfitRule] set ");
                        if (pIsUpdateNullField || pEntity.CardBuyToProfitRuleId!=null)
                strSql.Append( "[CardBuyToProfitRuleId]=@CardBuyToProfitRuleId,");
            if (pIsUpdateNullField || pEntity.VipCardTypeID!=null)
                strSql.Append( "[VipCardTypeID]=@VipCardTypeID,");
            if (pIsUpdateNullField || pEntity.ProfitType!=null)
                strSql.Append( "[ProfitType]=@ProfitType,");
            if (pIsUpdateNullField || pEntity.LimitAmount!=null)
                strSql.Append( "[LimitAmount]=@LimitAmount,");
            if (pIsUpdateNullField || pEntity.ProfitPct!=null)
                strSql.Append( "[ProfitPct]=@ProfitPct,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.IsDelete != null)
                strSql.Append("[IsDelete]=@IsDelete");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where ReRechargeProfitRuleId=@ReRechargeProfitRuleId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@CardBuyToProfitRuleId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@VipCardTypeID",SqlDbType.Int),
					new SqlParameter("@ProfitType",SqlDbType.VarChar),
					new SqlParameter("@LimitAmount",SqlDbType.Decimal),
					new SqlParameter("@ProfitPct",SqlDbType.Decimal),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
                    new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@ReRechargeProfitRuleId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.CardBuyToProfitRuleId;
			parameters[1].Value = pEntity.VipCardTypeID;
			parameters[2].Value = pEntity.ProfitType;
			parameters[3].Value = pEntity.LimitAmount;
			parameters[4].Value = pEntity.ProfitPct;
			parameters[5].Value = pEntity.CustomerID;
			parameters[6].Value = pEntity.LastUpdateTime;
			parameters[7].Value = pEntity.LastUpdateBy;
            parameters[8].Value = pEntity.IsDelete;
			parameters[9].Value = pEntity.ReRechargeProfitRuleId;

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
        public void Update(VipCardReRechargeProfitRuleEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(VipCardReRechargeProfitRuleEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(VipCardReRechargeProfitRuleEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.ReRechargeProfitRuleId.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.ReRechargeProfitRuleId.Value, pTran);           
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
            sql.AppendLine("update [VipCardReRechargeProfitRule] set  isdelete=1 where ReRechargeProfitRuleId=@ReRechargeProfitRuleId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@ReRechargeProfitRuleId",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(VipCardReRechargeProfitRuleEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.ReRechargeProfitRuleId.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.ReRechargeProfitRuleId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(VipCardReRechargeProfitRuleEntity[] pEntities)
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
            sql.AppendLine("update [VipCardReRechargeProfitRule] set  isdelete=1 where ReRechargeProfitRuleId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public VipCardReRechargeProfitRuleEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCardReRechargeProfitRule] where 1=1  and isdelete=0 ");
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
            List<VipCardReRechargeProfitRuleEntity> list = new List<VipCardReRechargeProfitRuleEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardReRechargeProfitRuleEntity m;
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
        public PagedQueryResult<VipCardReRechargeProfitRuleEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [ReRechargeProfitRuleId] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [VipCardReRechargeProfitRule] where 1=1  and isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [VipCardReRechargeProfitRule] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<VipCardReRechargeProfitRuleEntity> result = new PagedQueryResult<VipCardReRechargeProfitRuleEntity>();
            List<VipCardReRechargeProfitRuleEntity> list = new List<VipCardReRechargeProfitRuleEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardReRechargeProfitRuleEntity m;
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
        public VipCardReRechargeProfitRuleEntity[] QueryByEntity(VipCardReRechargeProfitRuleEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<VipCardReRechargeProfitRuleEntity> PagedQueryByEntity(VipCardReRechargeProfitRuleEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(VipCardReRechargeProfitRuleEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.ReRechargeProfitRuleId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReRechargeProfitRuleId", Value = pQueryEntity.ReRechargeProfitRuleId });
            if (pQueryEntity.CardBuyToProfitRuleId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CardBuyToProfitRuleId", Value = pQueryEntity.CardBuyToProfitRuleId });
            if (pQueryEntity.VipCardTypeID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardTypeID", Value = pQueryEntity.VipCardTypeID });
            if (pQueryEntity.ProfitType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ProfitType", Value = pQueryEntity.ProfitType });
            if (pQueryEntity.LimitAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LimitAmount", Value = pQueryEntity.LimitAmount });
            if (pQueryEntity.ProfitPct!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ProfitPct", Value = pQueryEntity.ProfitPct });
            if (pQueryEntity.CustomerID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.LastUpdateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.LastUpdateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.IsDelete!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out VipCardReRechargeProfitRuleEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new VipCardReRechargeProfitRuleEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["ReRechargeProfitRuleId"] != DBNull.Value)
			{
				pInstance.ReRechargeProfitRuleId =  (Guid)pReader["ReRechargeProfitRuleId"];
			}
			if (pReader["CardBuyToProfitRuleId"] != DBNull.Value)
			{
				pInstance.CardBuyToProfitRuleId =  (Guid)pReader["CardBuyToProfitRuleId"];
			}
			if (pReader["VipCardTypeID"] != DBNull.Value)
			{
				pInstance.VipCardTypeID =   Convert.ToInt32(pReader["VipCardTypeID"]);
			}
			if (pReader["ProfitType"] != DBNull.Value)
			{
				pInstance.ProfitType =  Convert.ToString(pReader["ProfitType"]);
			}
			if (pReader["LimitAmount"] != DBNull.Value)
			{
				pInstance.LimitAmount =  Convert.ToDecimal(pReader["LimitAmount"]);
			}
			if (pReader["ProfitPct"] != DBNull.Value)
			{
				pInstance.ProfitPct =  Convert.ToDecimal(pReader["ProfitPct"]);
			}
			if (pReader["CustomerID"] != DBNull.Value)
			{
				pInstance.CustomerID =  Convert.ToString(pReader["CustomerID"]);
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
			if (pReader["IsDelete"] != DBNull.Value)
			{
				pInstance.IsDelete =   Convert.ToInt32(pReader["IsDelete"]);
			}

        }
        #endregion
    }
}
