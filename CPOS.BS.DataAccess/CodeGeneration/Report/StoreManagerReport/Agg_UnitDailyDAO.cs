/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/3/23 14:10:40
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
    /// 表Agg_UnitDaily的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class Agg_UnitDailyDAO : Base.BaseCPOSDAO, ICRUDable<Agg_UnitDailyEntity>, IQueryable<Agg_UnitDailyEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public Agg_UnitDailyDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(Agg_UnitDailyEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(Agg_UnitDailyEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //初始化固定字段
			pEntity.CreateTime=DateTime.Now;


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [Agg_UnitDaily](");
            strSql.Append("[DateCode],[UnitId],[UnitCode],[UnitName],[StoreType],[CustomerId],[SalesAmount],[VipSalesAmount],[SalesAmountPlan],[AchievementRate],[CompleteSalesAmount],[NoCompleteSalesAmount],[DaysRemaining],[DayTargetSalesAmount],[OrderCount],[VipOrderCount],[SetoffVipCount],[SetoffCount],[NewVipCount],[OldVipBackCount],[VipCount],[ActiveVipCount],[HighValueVipCount],[NewPotentialVipCount],[New3MonthNoBackVipCount],[UseCouponCount],[CreateTime],[ID])");
            strSql.Append(" values (");
            strSql.Append("@DateCode,@UnitId,@UnitCode,@UnitName,@StoreType,@CustomerId,@SalesAmount,@VipSalesAmount,@SalesAmountPlan,@AchievementRate,@CompleteSalesAmount,@NoCompleteSalesAmount,@DaysRemaining,@DayTargetSalesAmount,@OrderCount,@VipOrderCount,@SetoffVipCount,@SetoffCount,@NewVipCount,@OldVipBackCount,@VipCount,@ActiveVipCount,@HighValueVipCount,@NewPotentialVipCount,@New3MonthNoBackVipCount,@UseCouponCount,@CreateTime,@ID)");            

			string pkString = pEntity.ID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@DateCode",SqlDbType.Date),
					new SqlParameter("@UnitId",SqlDbType.NVarChar),
					new SqlParameter("@UnitCode",SqlDbType.NVarChar),
					new SqlParameter("@UnitName",SqlDbType.NVarChar),
					new SqlParameter("@StoreType",SqlDbType.NVarChar),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@SalesAmount",SqlDbType.Decimal),
					new SqlParameter("@VipSalesAmount",SqlDbType.Decimal),
					new SqlParameter("@SalesAmountPlan",SqlDbType.Decimal),
					new SqlParameter("@AchievementRate",SqlDbType.Decimal),
					new SqlParameter("@CompleteSalesAmount",SqlDbType.Decimal),
					new SqlParameter("@NoCompleteSalesAmount",SqlDbType.Decimal),
					new SqlParameter("@DaysRemaining",SqlDbType.Int),
					new SqlParameter("@DayTargetSalesAmount",SqlDbType.Decimal),
					new SqlParameter("@OrderCount",SqlDbType.Int),
					new SqlParameter("@VipOrderCount",SqlDbType.Int),
					new SqlParameter("@SetoffVipCount",SqlDbType.Int),
					new SqlParameter("@SetoffCount",SqlDbType.Int),
					new SqlParameter("@NewVipCount",SqlDbType.Int),
					new SqlParameter("@OldVipBackCount",SqlDbType.Int),
					new SqlParameter("@VipCount",SqlDbType.Int),
					new SqlParameter("@ActiveVipCount",SqlDbType.Int),
					new SqlParameter("@HighValueVipCount",SqlDbType.Int),
					new SqlParameter("@NewPotentialVipCount",SqlDbType.Int),
					new SqlParameter("@New3MonthNoBackVipCount",SqlDbType.Int),
					new SqlParameter("@UseCouponCount",SqlDbType.Int),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@ID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.DateCode;
			parameters[1].Value = pEntity.UnitId;
			parameters[2].Value = pEntity.UnitCode;
			parameters[3].Value = pEntity.UnitName;
			parameters[4].Value = pEntity.StoreType;
			parameters[5].Value = pEntity.CustomerId;
			parameters[6].Value = pEntity.SalesAmount;
			parameters[7].Value = pEntity.VipSalesAmount;
			parameters[8].Value = pEntity.SalesAmountPlan;
			parameters[9].Value = pEntity.AchievementRate;
			parameters[10].Value = pEntity.CompleteSalesAmount;
			parameters[11].Value = pEntity.NoCompleteSalesAmount;
			parameters[12].Value = pEntity.DaysRemaining;
			parameters[13].Value = pEntity.DayTargetSalesAmount;
			parameters[14].Value = pEntity.OrderCount;
			parameters[15].Value = pEntity.VipOrderCount;
			parameters[16].Value = pEntity.SetoffVipCount;
			parameters[17].Value = pEntity.SetoffCount;
			parameters[18].Value = pEntity.NewVipCount;
			parameters[19].Value = pEntity.OldVipBackCount;
			parameters[20].Value = pEntity.VipCount;
			parameters[21].Value = pEntity.ActiveVipCount;
			parameters[22].Value = pEntity.HighValueVipCount;
			parameters[23].Value = pEntity.NewPotentialVipCount;
			parameters[24].Value = pEntity.New3MonthNoBackVipCount;
			parameters[25].Value = pEntity.UseCouponCount;
			parameters[26].Value = pEntity.CreateTime;
			parameters[27].Value = pkString;

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
        public Agg_UnitDailyEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [Agg_UnitDaily] where ID='{0}'  ", id.ToString());
            //读取数据
            Agg_UnitDailyEntity m = null;
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
        public Agg_UnitDailyEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [Agg_UnitDaily] where 1=1 ");
            //读取数据
            List<Agg_UnitDailyEntity> list = new List<Agg_UnitDailyEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    Agg_UnitDailyEntity m;
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
        public void Update(Agg_UnitDailyEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(Agg_UnitDailyEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
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
            strSql.Append("update [Agg_UnitDaily] set ");
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
            if (pIsUpdateNullField || pEntity.SalesAmount!=null)
                strSql.Append( "[SalesAmount]=@SalesAmount,");
            if (pIsUpdateNullField || pEntity.VipSalesAmount!=null)
                strSql.Append( "[VipSalesAmount]=@VipSalesAmount,");
            if (pIsUpdateNullField || pEntity.SalesAmountPlan!=null)
                strSql.Append( "[SalesAmountPlan]=@SalesAmountPlan,");
            if (pIsUpdateNullField || pEntity.AchievementRate!=null)
                strSql.Append( "[AchievementRate]=@AchievementRate,");
            if (pIsUpdateNullField || pEntity.CompleteSalesAmount!=null)
                strSql.Append( "[CompleteSalesAmount]=@CompleteSalesAmount,");
            if (pIsUpdateNullField || pEntity.NoCompleteSalesAmount!=null)
                strSql.Append( "[NoCompleteSalesAmount]=@NoCompleteSalesAmount,");
            if (pIsUpdateNullField || pEntity.DaysRemaining!=null)
                strSql.Append( "[DaysRemaining]=@DaysRemaining,");
            if (pIsUpdateNullField || pEntity.DayTargetSalesAmount!=null)
                strSql.Append( "[DayTargetSalesAmount]=@DayTargetSalesAmount,");
            if (pIsUpdateNullField || pEntity.OrderCount!=null)
                strSql.Append( "[OrderCount]=@OrderCount,");
            if (pIsUpdateNullField || pEntity.VipOrderCount!=null)
                strSql.Append( "[VipOrderCount]=@VipOrderCount,");
            if (pIsUpdateNullField || pEntity.SetoffVipCount!=null)
                strSql.Append( "[SetoffVipCount]=@SetoffVipCount,");
            if (pIsUpdateNullField || pEntity.SetoffCount!=null)
                strSql.Append( "[SetoffCount]=@SetoffCount,");
            if (pIsUpdateNullField || pEntity.NewVipCount!=null)
                strSql.Append( "[NewVipCount]=@NewVipCount,");
            if (pIsUpdateNullField || pEntity.OldVipBackCount!=null)
                strSql.Append( "[OldVipBackCount]=@OldVipBackCount,");
            if (pIsUpdateNullField || pEntity.VipCount!=null)
                strSql.Append( "[VipCount]=@VipCount,");
            if (pIsUpdateNullField || pEntity.ActiveVipCount!=null)
                strSql.Append( "[ActiveVipCount]=@ActiveVipCount,");
            if (pIsUpdateNullField || pEntity.HighValueVipCount!=null)
                strSql.Append( "[HighValueVipCount]=@HighValueVipCount,");
            if (pIsUpdateNullField || pEntity.NewPotentialVipCount!=null)
                strSql.Append( "[NewPotentialVipCount]=@NewPotentialVipCount,");
            if (pIsUpdateNullField || pEntity.New3MonthNoBackVipCount!=null)
                strSql.Append( "[New3MonthNoBackVipCount]=@New3MonthNoBackVipCount,");
            if (pIsUpdateNullField || pEntity.UseCouponCount!=null)
                strSql.Append( "[UseCouponCount]=@UseCouponCount");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@DateCode",SqlDbType.Date),
					new SqlParameter("@UnitId",SqlDbType.NVarChar),
					new SqlParameter("@UnitCode",SqlDbType.NVarChar),
					new SqlParameter("@UnitName",SqlDbType.NVarChar),
					new SqlParameter("@StoreType",SqlDbType.NVarChar),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@SalesAmount",SqlDbType.Decimal),
					new SqlParameter("@VipSalesAmount",SqlDbType.Decimal),
					new SqlParameter("@SalesAmountPlan",SqlDbType.Decimal),
					new SqlParameter("@AchievementRate",SqlDbType.Decimal),
					new SqlParameter("@CompleteSalesAmount",SqlDbType.Decimal),
					new SqlParameter("@NoCompleteSalesAmount",SqlDbType.Decimal),
					new SqlParameter("@DaysRemaining",SqlDbType.Int),
					new SqlParameter("@DayTargetSalesAmount",SqlDbType.Decimal),
					new SqlParameter("@OrderCount",SqlDbType.Int),
					new SqlParameter("@VipOrderCount",SqlDbType.Int),
					new SqlParameter("@SetoffVipCount",SqlDbType.Int),
					new SqlParameter("@SetoffCount",SqlDbType.Int),
					new SqlParameter("@NewVipCount",SqlDbType.Int),
					new SqlParameter("@OldVipBackCount",SqlDbType.Int),
					new SqlParameter("@VipCount",SqlDbType.Int),
					new SqlParameter("@ActiveVipCount",SqlDbType.Int),
					new SqlParameter("@HighValueVipCount",SqlDbType.Int),
					new SqlParameter("@NewPotentialVipCount",SqlDbType.Int),
					new SqlParameter("@New3MonthNoBackVipCount",SqlDbType.Int),
					new SqlParameter("@UseCouponCount",SqlDbType.Int),
					new SqlParameter("@ID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.DateCode;
			parameters[1].Value = pEntity.UnitId;
			parameters[2].Value = pEntity.UnitCode;
			parameters[3].Value = pEntity.UnitName;
			parameters[4].Value = pEntity.StoreType;
			parameters[5].Value = pEntity.CustomerId;
			parameters[6].Value = pEntity.SalesAmount;
			parameters[7].Value = pEntity.VipSalesAmount;
			parameters[8].Value = pEntity.SalesAmountPlan;
			parameters[9].Value = pEntity.AchievementRate;
			parameters[10].Value = pEntity.CompleteSalesAmount;
			parameters[11].Value = pEntity.NoCompleteSalesAmount;
			parameters[12].Value = pEntity.DaysRemaining;
			parameters[13].Value = pEntity.DayTargetSalesAmount;
			parameters[14].Value = pEntity.OrderCount;
			parameters[15].Value = pEntity.VipOrderCount;
			parameters[16].Value = pEntity.SetoffVipCount;
			parameters[17].Value = pEntity.SetoffCount;
			parameters[18].Value = pEntity.NewVipCount;
			parameters[19].Value = pEntity.OldVipBackCount;
			parameters[20].Value = pEntity.VipCount;
			parameters[21].Value = pEntity.ActiveVipCount;
			parameters[22].Value = pEntity.HighValueVipCount;
			parameters[23].Value = pEntity.NewPotentialVipCount;
			parameters[24].Value = pEntity.New3MonthNoBackVipCount;
			parameters[25].Value = pEntity.UseCouponCount;
			parameters[26].Value = pEntity.ID;

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
        public void Update(Agg_UnitDailyEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(Agg_UnitDailyEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(Agg_UnitDailyEntity pEntity, IDbTransaction pTran)
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
            sql.AppendLine("update [Agg_UnitDaily] set  where ID=@ID;");
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
        public void Delete(Agg_UnitDailyEntity[] pEntities, IDbTransaction pTran)
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
        public void Delete(Agg_UnitDailyEntity[] pEntities)
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
            sql.AppendLine("update [Agg_UnitDaily] set  where ID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public Agg_UnitDailyEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [Agg_UnitDaily] where 1=1  ");
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
            List<Agg_UnitDailyEntity> list = new List<Agg_UnitDailyEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    Agg_UnitDailyEntity m;
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
        public PagedQueryResult<Agg_UnitDailyEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
            pagedSql.AppendFormat(") as ___rn,* from [Agg_UnitDaily] where 1=1  ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [Agg_UnitDaily] where 1=1  ");
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
            PagedQueryResult<Agg_UnitDailyEntity> result = new PagedQueryResult<Agg_UnitDailyEntity>();
            List<Agg_UnitDailyEntity> list = new List<Agg_UnitDailyEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    Agg_UnitDailyEntity m;
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
        public Agg_UnitDailyEntity[] QueryByEntity(Agg_UnitDailyEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<Agg_UnitDailyEntity> PagedQueryByEntity(Agg_UnitDailyEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(Agg_UnitDailyEntity pQueryEntity)
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
            if (pQueryEntity.SalesAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesAmount", Value = pQueryEntity.SalesAmount });
            if (pQueryEntity.VipSalesAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipSalesAmount", Value = pQueryEntity.VipSalesAmount });
            if (pQueryEntity.SalesAmountPlan!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesAmountPlan", Value = pQueryEntity.SalesAmountPlan });
            if (pQueryEntity.AchievementRate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AchievementRate", Value = pQueryEntity.AchievementRate });
            if (pQueryEntity.CompleteSalesAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CompleteSalesAmount", Value = pQueryEntity.CompleteSalesAmount });
            if (pQueryEntity.NoCompleteSalesAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NoCompleteSalesAmount", Value = pQueryEntity.NoCompleteSalesAmount });
            if (pQueryEntity.DaysRemaining!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DaysRemaining", Value = pQueryEntity.DaysRemaining });
            if (pQueryEntity.DayTargetSalesAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DayTargetSalesAmount", Value = pQueryEntity.DayTargetSalesAmount });
            if (pQueryEntity.OrderCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderCount", Value = pQueryEntity.OrderCount });
            if (pQueryEntity.VipOrderCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipOrderCount", Value = pQueryEntity.VipOrderCount });
            if (pQueryEntity.SetoffVipCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SetoffVipCount", Value = pQueryEntity.SetoffVipCount });
            if (pQueryEntity.SetoffCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SetoffCount", Value = pQueryEntity.SetoffCount });
            if (pQueryEntity.NewVipCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NewVipCount", Value = pQueryEntity.NewVipCount });
            if (pQueryEntity.OldVipBackCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OldVipBackCount", Value = pQueryEntity.OldVipBackCount });
            if (pQueryEntity.VipCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCount", Value = pQueryEntity.VipCount });
            if (pQueryEntity.ActiveVipCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ActiveVipCount", Value = pQueryEntity.ActiveVipCount });
            if (pQueryEntity.HighValueVipCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "HighValueVipCount", Value = pQueryEntity.HighValueVipCount });
            if (pQueryEntity.NewPotentialVipCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NewPotentialVipCount", Value = pQueryEntity.NewPotentialVipCount });
            if (pQueryEntity.New3MonthNoBackVipCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "New3MonthNoBackVipCount", Value = pQueryEntity.New3MonthNoBackVipCount });
            if (pQueryEntity.UseCouponCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UseCouponCount", Value = pQueryEntity.UseCouponCount });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out Agg_UnitDailyEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new Agg_UnitDailyEntity();
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
			if (pReader["SalesAmount"] != DBNull.Value)
			{
				pInstance.SalesAmount =  Convert.ToDecimal(pReader["SalesAmount"]);
			}
			if (pReader["VipSalesAmount"] != DBNull.Value)
			{
				pInstance.VipSalesAmount =  Convert.ToDecimal(pReader["VipSalesAmount"]);
			}
			if (pReader["SalesAmountPlan"] != DBNull.Value)
			{
				pInstance.SalesAmountPlan =  Convert.ToDecimal(pReader["SalesAmountPlan"]);
			}
			if (pReader["AchievementRate"] != DBNull.Value)
			{
				pInstance.AchievementRate =  Convert.ToDecimal(pReader["AchievementRate"]);
			}
			if (pReader["CompleteSalesAmount"] != DBNull.Value)
			{
				pInstance.CompleteSalesAmount =  Convert.ToDecimal(pReader["CompleteSalesAmount"]);
			}
			if (pReader["NoCompleteSalesAmount"] != DBNull.Value)
			{
				pInstance.NoCompleteSalesAmount =  Convert.ToDecimal(pReader["NoCompleteSalesAmount"]);
			}
			if (pReader["DaysRemaining"] != DBNull.Value)
			{
				pInstance.DaysRemaining =   Convert.ToInt32(pReader["DaysRemaining"]);
			}
			if (pReader["DayTargetSalesAmount"] != DBNull.Value)
			{
				pInstance.DayTargetSalesAmount =  Convert.ToDecimal(pReader["DayTargetSalesAmount"]);
			}
			if (pReader["OrderCount"] != DBNull.Value)
			{
				pInstance.OrderCount =   Convert.ToInt32(pReader["OrderCount"]);
			}
			if (pReader["VipOrderCount"] != DBNull.Value)
			{
				pInstance.VipOrderCount =   Convert.ToInt32(pReader["VipOrderCount"]);
			}
			if (pReader["SetoffVipCount"] != DBNull.Value)
			{
				pInstance.SetoffVipCount =   Convert.ToInt32(pReader["SetoffVipCount"]);
			}
			if (pReader["SetoffCount"] != DBNull.Value)
			{
				pInstance.SetoffCount =   Convert.ToInt32(pReader["SetoffCount"]);
			}
			if (pReader["NewVipCount"] != DBNull.Value)
			{
				pInstance.NewVipCount =   Convert.ToInt32(pReader["NewVipCount"]);
			}
			if (pReader["OldVipBackCount"] != DBNull.Value)
			{
				pInstance.OldVipBackCount =   Convert.ToInt32(pReader["OldVipBackCount"]);
			}
			if (pReader["VipCount"] != DBNull.Value)
			{
				pInstance.VipCount =   Convert.ToInt32(pReader["VipCount"]);
			}
			if (pReader["ActiveVipCount"] != DBNull.Value)
			{
				pInstance.ActiveVipCount =   Convert.ToInt32(pReader["ActiveVipCount"]);
			}
			if (pReader["HighValueVipCount"] != DBNull.Value)
			{
				pInstance.HighValueVipCount =   Convert.ToInt32(pReader["HighValueVipCount"]);
			}
			if (pReader["NewPotentialVipCount"] != DBNull.Value)
			{
				pInstance.NewPotentialVipCount =   Convert.ToInt32(pReader["NewPotentialVipCount"]);
			}
			if (pReader["New3MonthNoBackVipCount"] != DBNull.Value)
			{
				pInstance.New3MonthNoBackVipCount =   Convert.ToInt32(pReader["New3MonthNoBackVipCount"]);
			}
			if (pReader["UseCouponCount"] != DBNull.Value)
			{
				pInstance.UseCouponCount =   Convert.ToInt32(pReader["UseCouponCount"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}

        }
        #endregion
    }
}
