/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/25 15:10:56
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
    /// 表VipCardUpgradeRule的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VipCardUpgradeRuleDAO : Base.BaseCPOSDAO, ICRUDable<VipCardUpgradeRuleEntity>, IQueryable<VipCardUpgradeRuleEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VipCardUpgradeRuleDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(VipCardUpgradeRuleEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(VipCardUpgradeRuleEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [VipCardUpgradeRule](");
            strSql.Append("[VipCardTypeID],[IsFormVerify],[IsPurchaseUpgrade],[IsExchangeIntegral],[IsRecharge],[OnceRechargeAmount],[IsBuyUpgrade],[BuyAmount],[OnceBuyAmount],[IsPointUpgrade],[TotalPoint],[IsMustDeductPoint],[RefId],[CustomerID],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[VipCardUpgradeRuleId])");
            strSql.Append(" values (");
            strSql.Append("@VipCardTypeID,@IsFormVerify,@IsPurchaseUpgrade,@IsExchangeIntegral,@IsRecharge,@OnceRechargeAmount,@IsBuyUpgrade,@BuyAmount,@OnceBuyAmount,@IsPointUpgrade,@TotalPoint,@IsMustDeductPoint,@RefId,@CustomerID,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@VipCardUpgradeRuleId)");            

			Guid? pkGuid;
			if (pEntity.VipCardUpgradeRuleId == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.VipCardUpgradeRuleId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@VipCardTypeID",SqlDbType.Int),
					new SqlParameter("@IsFormVerify",SqlDbType.Int),
					new SqlParameter("@IsPurchaseUpgrade",SqlDbType.Int),
					new SqlParameter("@IsExchangeIntegral",SqlDbType.Int),
					new SqlParameter("@IsRecharge",SqlDbType.Int),
					new SqlParameter("@OnceRechargeAmount",SqlDbType.Decimal),
					new SqlParameter("@IsBuyUpgrade",SqlDbType.Int),
					new SqlParameter("@BuyAmount",SqlDbType.Decimal),
					new SqlParameter("@OnceBuyAmount",SqlDbType.Decimal),
					new SqlParameter("@IsPointUpgrade",SqlDbType.Int),
					new SqlParameter("@TotalPoint",SqlDbType.Decimal),
					new SqlParameter("@IsMustDeductPoint",SqlDbType.Int),
					new SqlParameter("@RefId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@VipCardUpgradeRuleId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.VipCardTypeID;
			parameters[1].Value = pEntity.IsFormVerify;
			parameters[2].Value = pEntity.IsPurchaseUpgrade;
			parameters[3].Value = pEntity.IsExchangeIntegral;
			parameters[4].Value = pEntity.IsRecharge;
			parameters[5].Value = pEntity.OnceRechargeAmount;
			parameters[6].Value = pEntity.IsBuyUpgrade;
			parameters[7].Value = pEntity.BuyAmount;
			parameters[8].Value = pEntity.OnceBuyAmount;
			parameters[9].Value = pEntity.IsPointUpgrade;
			parameters[10].Value = pEntity.TotalPoint;
			parameters[11].Value = pEntity.IsMustDeductPoint;
			parameters[12].Value = pEntity.RefId;
			parameters[13].Value = pEntity.CustomerID;
			parameters[14].Value = pEntity.CreateTime;
			parameters[15].Value = pEntity.CreateBy;
			parameters[16].Value = pEntity.LastUpdateTime;
			parameters[17].Value = pEntity.LastUpdateBy;
			parameters[18].Value = pEntity.IsDelete;
			parameters[19].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.VipCardUpgradeRuleId = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public VipCardUpgradeRuleEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCardUpgradeRule] where VipCardUpgradeRuleId='{0}'  and isdelete=0 ", id.ToString());
            //读取数据
            VipCardUpgradeRuleEntity m = null;
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
        public VipCardUpgradeRuleEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCardUpgradeRule] where 1=1  and isdelete=0");
            //读取数据
            List<VipCardUpgradeRuleEntity> list = new List<VipCardUpgradeRuleEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardUpgradeRuleEntity m;
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
        public void Update(VipCardUpgradeRuleEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(VipCardUpgradeRuleEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.VipCardUpgradeRuleId.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [VipCardUpgradeRule] set ");
                        if (pIsUpdateNullField || pEntity.VipCardTypeID!=null)
                strSql.Append( "[VipCardTypeID]=@VipCardTypeID,");
            if (pIsUpdateNullField || pEntity.IsFormVerify!=null)
                strSql.Append( "[IsFormVerify]=@IsFormVerify,");
            if (pIsUpdateNullField || pEntity.IsPurchaseUpgrade!=null)
                strSql.Append( "[IsPurchaseUpgrade]=@IsPurchaseUpgrade,");
            if (pIsUpdateNullField || pEntity.IsExchangeIntegral!=null)
                strSql.Append( "[IsExchangeIntegral]=@IsExchangeIntegral,");
            if (pIsUpdateNullField || pEntity.IsRecharge!=null)
                strSql.Append( "[IsRecharge]=@IsRecharge,");
            if (pIsUpdateNullField || pEntity.OnceRechargeAmount!=null)
                strSql.Append( "[OnceRechargeAmount]=@OnceRechargeAmount,");
            if (pIsUpdateNullField || pEntity.IsBuyUpgrade!=null)
                strSql.Append( "[IsBuyUpgrade]=@IsBuyUpgrade,");
            if (pIsUpdateNullField || pEntity.BuyAmount!=null)
                strSql.Append( "[BuyAmount]=@BuyAmount,");
            if (pIsUpdateNullField || pEntity.OnceBuyAmount!=null)
                strSql.Append( "[OnceBuyAmount]=@OnceBuyAmount,");
            if (pIsUpdateNullField || pEntity.IsPointUpgrade!=null)
                strSql.Append( "[IsPointUpgrade]=@IsPointUpgrade,");
            if (pIsUpdateNullField || pEntity.TotalPoint!=null)
                strSql.Append( "[TotalPoint]=@TotalPoint,");
            if (pIsUpdateNullField || pEntity.IsMustDeductPoint!=null)
                strSql.Append( "[IsMustDeductPoint]=@IsMustDeductPoint,");
            if (pIsUpdateNullField || pEntity.RefId!=null)
                strSql.Append( "[RefId]=@RefId,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where VipCardUpgradeRuleId=@VipCardUpgradeRuleId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@VipCardTypeID",SqlDbType.Int),
					new SqlParameter("@IsFormVerify",SqlDbType.Int),
					new SqlParameter("@IsPurchaseUpgrade",SqlDbType.Int),
					new SqlParameter("@IsExchangeIntegral",SqlDbType.Int),
					new SqlParameter("@IsRecharge",SqlDbType.Int),
					new SqlParameter("@OnceRechargeAmount",SqlDbType.Decimal),
					new SqlParameter("@IsBuyUpgrade",SqlDbType.Int),
					new SqlParameter("@BuyAmount",SqlDbType.Decimal),
					new SqlParameter("@OnceBuyAmount",SqlDbType.Decimal),
					new SqlParameter("@IsPointUpgrade",SqlDbType.Int),
					new SqlParameter("@TotalPoint",SqlDbType.Decimal),
					new SqlParameter("@IsMustDeductPoint",SqlDbType.Int),
					new SqlParameter("@RefId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@VipCardUpgradeRuleId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.VipCardTypeID;
			parameters[1].Value = pEntity.IsFormVerify;
			parameters[2].Value = pEntity.IsPurchaseUpgrade;
			parameters[3].Value = pEntity.IsExchangeIntegral;
			parameters[4].Value = pEntity.IsRecharge;
			parameters[5].Value = pEntity.OnceRechargeAmount;
			parameters[6].Value = pEntity.IsBuyUpgrade;
			parameters[7].Value = pEntity.BuyAmount;
			parameters[8].Value = pEntity.OnceBuyAmount;
			parameters[9].Value = pEntity.IsPointUpgrade;
			parameters[10].Value = pEntity.TotalPoint;
			parameters[11].Value = pEntity.IsMustDeductPoint;
			parameters[12].Value = pEntity.RefId;
			parameters[13].Value = pEntity.CustomerID;
			parameters[14].Value = pEntity.LastUpdateTime;
			parameters[15].Value = pEntity.LastUpdateBy;
			parameters[16].Value = pEntity.VipCardUpgradeRuleId;

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
        public void Update(VipCardUpgradeRuleEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(VipCardUpgradeRuleEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(VipCardUpgradeRuleEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.VipCardUpgradeRuleId.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.VipCardUpgradeRuleId.Value, pTran);           
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
            sql.AppendLine("update [VipCardUpgradeRule] set  isdelete=1 where VipCardUpgradeRuleId=@VipCardUpgradeRuleId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@VipCardUpgradeRuleId",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(VipCardUpgradeRuleEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.VipCardUpgradeRuleId.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.VipCardUpgradeRuleId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(VipCardUpgradeRuleEntity[] pEntities)
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
            sql.AppendLine("update [VipCardUpgradeRule] set  isdelete=1 where VipCardUpgradeRuleId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public VipCardUpgradeRuleEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCardUpgradeRule] where 1=1  and isdelete=0 ");
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
            List<VipCardUpgradeRuleEntity> list = new List<VipCardUpgradeRuleEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardUpgradeRuleEntity m;
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
        public PagedQueryResult<VipCardUpgradeRuleEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [VipCardUpgradeRuleId] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [VipCardUpgradeRule] where 1=1  and isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [VipCardUpgradeRule] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<VipCardUpgradeRuleEntity> result = new PagedQueryResult<VipCardUpgradeRuleEntity>();
            List<VipCardUpgradeRuleEntity> list = new List<VipCardUpgradeRuleEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardUpgradeRuleEntity m;
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
        public VipCardUpgradeRuleEntity[] QueryByEntity(VipCardUpgradeRuleEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<VipCardUpgradeRuleEntity> PagedQueryByEntity(VipCardUpgradeRuleEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(VipCardUpgradeRuleEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.VipCardUpgradeRuleId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardUpgradeRuleId", Value = pQueryEntity.VipCardUpgradeRuleId });
            if (pQueryEntity.VipCardTypeID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardTypeID", Value = pQueryEntity.VipCardTypeID });
            if (pQueryEntity.IsFormVerify!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsFormVerify", Value = pQueryEntity.IsFormVerify });
            if (pQueryEntity.IsPurchaseUpgrade!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsPurchaseUpgrade", Value = pQueryEntity.IsPurchaseUpgrade });
            if (pQueryEntity.IsExchangeIntegral!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsExchangeIntegral", Value = pQueryEntity.IsExchangeIntegral });
            if (pQueryEntity.IsRecharge!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsRecharge", Value = pQueryEntity.IsRecharge });
            if (pQueryEntity.OnceRechargeAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OnceRechargeAmount", Value = pQueryEntity.OnceRechargeAmount });
            if (pQueryEntity.IsBuyUpgrade!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsBuyUpgrade", Value = pQueryEntity.IsBuyUpgrade });
            if (pQueryEntity.BuyAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BuyAmount", Value = pQueryEntity.BuyAmount });
            if (pQueryEntity.OnceBuyAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OnceBuyAmount", Value = pQueryEntity.OnceBuyAmount });
            if (pQueryEntity.IsPointUpgrade!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsPointUpgrade", Value = pQueryEntity.IsPointUpgrade });
            if (pQueryEntity.TotalPoint!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TotalPoint", Value = pQueryEntity.TotalPoint });
            if (pQueryEntity.IsMustDeductPoint!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsMustDeductPoint", Value = pQueryEntity.IsMustDeductPoint });
            if (pQueryEntity.RefId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RefId", Value = pQueryEntity.RefId });
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
        protected void Load(IDataReader pReader, out VipCardUpgradeRuleEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new VipCardUpgradeRuleEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["VipCardUpgradeRuleId"] != DBNull.Value)
			{
				pInstance.VipCardUpgradeRuleId =  (Guid)pReader["VipCardUpgradeRuleId"];
			}
			if (pReader["VipCardTypeID"] != DBNull.Value)
			{
				pInstance.VipCardTypeID =   Convert.ToInt32(pReader["VipCardTypeID"]);
			}
			if (pReader["IsFormVerify"] != DBNull.Value)
			{
				pInstance.IsFormVerify =   Convert.ToInt32(pReader["IsFormVerify"]);
			}
			if (pReader["IsPurchaseUpgrade"] != DBNull.Value)
			{
				pInstance.IsPurchaseUpgrade =   Convert.ToInt32(pReader["IsPurchaseUpgrade"]);
			}
			if (pReader["IsExchangeIntegral"] != DBNull.Value)
			{
				pInstance.IsExchangeIntegral =   Convert.ToInt32(pReader["IsExchangeIntegral"]);
			}
			if (pReader["IsRecharge"] != DBNull.Value)
			{
				pInstance.IsRecharge =   Convert.ToInt32(pReader["IsRecharge"]);
			}
			if (pReader["OnceRechargeAmount"] != DBNull.Value)
			{
				pInstance.OnceRechargeAmount =  Convert.ToDecimal(pReader["OnceRechargeAmount"]);
			}
			if (pReader["IsBuyUpgrade"] != DBNull.Value)
			{
				pInstance.IsBuyUpgrade =   Convert.ToInt32(pReader["IsBuyUpgrade"]);
			}
			if (pReader["BuyAmount"] != DBNull.Value)
			{
				pInstance.BuyAmount =  Convert.ToDecimal(pReader["BuyAmount"]);
			}
			if (pReader["OnceBuyAmount"] != DBNull.Value)
			{
				pInstance.OnceBuyAmount =  Convert.ToDecimal(pReader["OnceBuyAmount"]);
			}
			if (pReader["IsPointUpgrade"] != DBNull.Value)
			{
				pInstance.IsPointUpgrade =   Convert.ToInt32(pReader["IsPointUpgrade"]);
			}
			if (pReader["TotalPoint"] != DBNull.Value)
			{
				pInstance.TotalPoint =  Convert.ToDecimal(pReader["TotalPoint"]);
			}
			if (pReader["IsMustDeductPoint"] != DBNull.Value)
			{
				pInstance.IsMustDeductPoint =   Convert.ToInt32(pReader["IsMustDeductPoint"]);
			}
			if (pReader["RefId"] != DBNull.Value)
			{
				pInstance.RefId =  (Guid)pReader["RefId"];
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
