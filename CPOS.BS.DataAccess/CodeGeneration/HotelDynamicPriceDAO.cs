/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/21 11:41:01
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
    /// 表HotelDynamicPrice的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class HotelDynamicPriceDAO : Base.BaseCPOSDAO, ICRUDable<HotelDynamicPriceEntity>, IQueryable<HotelDynamicPriceEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public HotelDynamicPriceDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(HotelDynamicPriceEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(HotelDynamicPriceEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [HotelDynamicPrice](");
            strSql.Append("[RoomId],[FloatPrice],[MaxIntegralToCurrency],[DonateIntegral],[ReturnCurrency],[EffectiveBeginTime],[EffectiveEndTime],[EffectiveBeginDate],[EffectiveEndDate],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[CustomerId],[HotelDynamicPriceId])");
            strSql.Append(" values (");
            strSql.Append("@RoomId,@FloatPrice,@MaxIntegralToCurrency,@DonateIntegral,@ReturnCurrency,@EffectiveBeginTime,@EffectiveEndTime,@EffectiveBeginDate,@EffectiveEndDate,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@CustomerId,@HotelDynamicPriceId)");            

			Guid? pkGuid;
			if (pEntity.HotelDynamicPriceId == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.HotelDynamicPriceId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@RoomId",SqlDbType.NVarChar),
					new SqlParameter("@FloatPrice",SqlDbType.Decimal),
					new SqlParameter("@MaxIntegralToCurrency",SqlDbType.Int),
					new SqlParameter("@DonateIntegral",SqlDbType.Int),
					new SqlParameter("@ReturnCurrency",SqlDbType.Decimal),
					new SqlParameter("@EffectiveBeginTime",SqlDbType.DateTime),
					new SqlParameter("@EffectiveEndTime",SqlDbType.DateTime),
					new SqlParameter("@EffectiveBeginDate",SqlDbType.DateTime),
					new SqlParameter("@EffectiveEndDate",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@HotelDynamicPriceId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.RoomId;
			parameters[1].Value = pEntity.FloatPrice;
			parameters[2].Value = pEntity.MaxIntegralToCurrency;
			parameters[3].Value = pEntity.DonateIntegral;
			parameters[4].Value = pEntity.ReturnCurrency;
			parameters[5].Value = pEntity.EffectiveBeginTime;
			parameters[6].Value = pEntity.EffectiveEndTime;
			parameters[7].Value = pEntity.EffectiveBeginDate;
			parameters[8].Value = pEntity.EffectiveEndDate;
			parameters[9].Value = pEntity.CreateBy;
			parameters[10].Value = pEntity.CreateTime;
			parameters[11].Value = pEntity.LastUpdateBy;
			parameters[12].Value = pEntity.LastUpdateTime;
			parameters[13].Value = pEntity.IsDelete;
			parameters[14].Value = pEntity.CustomerId;
			parameters[15].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.HotelDynamicPriceId = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public HotelDynamicPriceEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [HotelDynamicPrice] where HotelDynamicPriceId='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            HotelDynamicPriceEntity m = null;
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
        public HotelDynamicPriceEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [HotelDynamicPrice] where isdelete=0");
            //读取数据
            List<HotelDynamicPriceEntity> list = new List<HotelDynamicPriceEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    HotelDynamicPriceEntity m;
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
        public void Update(HotelDynamicPriceEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(HotelDynamicPriceEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.HotelDynamicPriceId==null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [HotelDynamicPrice] set ");
            if (pIsUpdateNullField || pEntity.RoomId!=null)
                strSql.Append( "[RoomId]=@RoomId,");
            if (pIsUpdateNullField || pEntity.FloatPrice!=null)
                strSql.Append( "[FloatPrice]=@FloatPrice,");
            if (pIsUpdateNullField || pEntity.MaxIntegralToCurrency!=null)
                strSql.Append( "[MaxIntegralToCurrency]=@MaxIntegralToCurrency,");
            if (pIsUpdateNullField || pEntity.DonateIntegral!=null)
                strSql.Append( "[DonateIntegral]=@DonateIntegral,");
            if (pIsUpdateNullField || pEntity.ReturnCurrency!=null)
                strSql.Append( "[ReturnCurrency]=@ReturnCurrency,");
            if (pIsUpdateNullField || pEntity.EffectiveBeginTime!=null)
                strSql.Append( "[EffectiveBeginTime]=@EffectiveBeginTime,");
            if (pIsUpdateNullField || pEntity.EffectiveEndTime!=null)
                strSql.Append( "[EffectiveEndTime]=@EffectiveEndTime,");
            if (pIsUpdateNullField || pEntity.EffectiveBeginDate!=null)
                strSql.Append( "[EffectiveBeginDate]=@EffectiveBeginDate,");
            if (pIsUpdateNullField || pEntity.EffectiveEndDate!=null)
                strSql.Append( "[EffectiveEndDate]=@EffectiveEndDate,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where HotelDynamicPriceId=@HotelDynamicPriceId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@RoomId",SqlDbType.NVarChar),
					new SqlParameter("@FloatPrice",SqlDbType.Decimal),
					new SqlParameter("@MaxIntegralToCurrency",SqlDbType.Int),
					new SqlParameter("@DonateIntegral",SqlDbType.Int),
					new SqlParameter("@ReturnCurrency",SqlDbType.Decimal),
					new SqlParameter("@EffectiveBeginTime",SqlDbType.DateTime),
					new SqlParameter("@EffectiveEndTime",SqlDbType.DateTime),
					new SqlParameter("@EffectiveBeginDate",SqlDbType.DateTime),
					new SqlParameter("@EffectiveEndDate",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@HotelDynamicPriceId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.RoomId;
			parameters[1].Value = pEntity.FloatPrice;
			parameters[2].Value = pEntity.MaxIntegralToCurrency;
			parameters[3].Value = pEntity.DonateIntegral;
			parameters[4].Value = pEntity.ReturnCurrency;
			parameters[5].Value = pEntity.EffectiveBeginTime;
			parameters[6].Value = pEntity.EffectiveEndTime;
			parameters[7].Value = pEntity.EffectiveBeginDate;
			parameters[8].Value = pEntity.EffectiveEndDate;
			parameters[9].Value = pEntity.LastUpdateBy;
			parameters[10].Value = pEntity.LastUpdateTime;
			parameters[11].Value = pEntity.CustomerId;
			parameters[12].Value = pEntity.HotelDynamicPriceId;

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
        public void Update(HotelDynamicPriceEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(HotelDynamicPriceEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(HotelDynamicPriceEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(HotelDynamicPriceEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.HotelDynamicPriceId==null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.HotelDynamicPriceId, pTran);           
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
            sql.AppendLine("update [HotelDynamicPrice] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where HotelDynamicPriceId=@HotelDynamicPriceId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@HotelDynamicPriceId",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(HotelDynamicPriceEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.HotelDynamicPriceId==null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.HotelDynamicPriceId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(HotelDynamicPriceEntity[] pEntities)
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
            sql.AppendLine("update [HotelDynamicPrice] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where HotelDynamicPriceId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public HotelDynamicPriceEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [HotelDynamicPrice] where isdelete=0 ");
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
            List<HotelDynamicPriceEntity> list = new List<HotelDynamicPriceEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    HotelDynamicPriceEntity m;
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
        public PagedQueryResult<HotelDynamicPriceEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [HotelDynamicPriceId] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [HotelDynamicPrice] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [HotelDynamicPrice] where isdelete=0 ");
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
            PagedQueryResult<HotelDynamicPriceEntity> result = new PagedQueryResult<HotelDynamicPriceEntity>();
            List<HotelDynamicPriceEntity> list = new List<HotelDynamicPriceEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    HotelDynamicPriceEntity m;
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
        public HotelDynamicPriceEntity[] QueryByEntity(HotelDynamicPriceEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<HotelDynamicPriceEntity> PagedQueryByEntity(HotelDynamicPriceEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(HotelDynamicPriceEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.HotelDynamicPriceId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "HotelDynamicPriceId", Value = pQueryEntity.HotelDynamicPriceId });
            if (pQueryEntity.RoomId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RoomId", Value = pQueryEntity.RoomId });
            if (pQueryEntity.FloatPrice!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FloatPrice", Value = pQueryEntity.FloatPrice });
            if (pQueryEntity.MaxIntegralToCurrency!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MaxIntegralToCurrency", Value = pQueryEntity.MaxIntegralToCurrency });
            if (pQueryEntity.DonateIntegral!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DonateIntegral", Value = pQueryEntity.DonateIntegral });
            if (pQueryEntity.ReturnCurrency!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReturnCurrency", Value = pQueryEntity.ReturnCurrency });
            if (pQueryEntity.EffectiveBeginTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EffectiveBeginTime", Value = pQueryEntity.EffectiveBeginTime });
            if (pQueryEntity.EffectiveEndTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EffectiveEndTime", Value = pQueryEntity.EffectiveEndTime });
            if (pQueryEntity.EffectiveBeginDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EffectiveBeginDate", Value = pQueryEntity.EffectiveBeginDate });
            if (pQueryEntity.EffectiveEndDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EffectiveEndDate", Value = pQueryEntity.EffectiveEndDate });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.LastUpdateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.LastUpdateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
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
        protected void Load(SqlDataReader pReader, out HotelDynamicPriceEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new HotelDynamicPriceEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["HotelDynamicPriceId"] != DBNull.Value)
			{
				pInstance.HotelDynamicPriceId =  (Guid)pReader["HotelDynamicPriceId"];
			}
			if (pReader["RoomId"] != DBNull.Value)
			{
				pInstance.RoomId =  Convert.ToString(pReader["RoomId"]);
			}
			if (pReader["FloatPrice"] != DBNull.Value)
			{
				pInstance.FloatPrice =  Convert.ToDecimal(pReader["FloatPrice"]);
			}
			if (pReader["MaxIntegralToCurrency"] != DBNull.Value)
			{
				pInstance.MaxIntegralToCurrency =   Convert.ToInt32(pReader["MaxIntegralToCurrency"]);
			}
			if (pReader["DonateIntegral"] != DBNull.Value)
			{
				pInstance.DonateIntegral =   Convert.ToInt32(pReader["DonateIntegral"]);
			}
			if (pReader["ReturnCurrency"] != DBNull.Value)
			{
				pInstance.ReturnCurrency =  Convert.ToDecimal(pReader["ReturnCurrency"]);
			}
			if (pReader["EffectiveBeginTime"] != DBNull.Value)
			{
				pInstance.EffectiveBeginTime =  Convert.ToDateTime(pReader["EffectiveBeginTime"]);
			}
			if (pReader["EffectiveEndTime"] != DBNull.Value)
			{
				pInstance.EffectiveEndTime =  Convert.ToDateTime(pReader["EffectiveEndTime"]);
			}
			if (pReader["EffectiveBeginDate"] != DBNull.Value)
			{
				pInstance.EffectiveBeginDate =  Convert.ToDateTime(pReader["EffectiveBeginDate"]);
			}
			if (pReader["EffectiveEndDate"] != DBNull.Value)
			{
				pInstance.EffectiveEndDate =  Convert.ToDateTime(pReader["EffectiveEndDate"]);
			}
			if (pReader["CreateBy"] != DBNull.Value)
			{
				pInstance.CreateBy =  Convert.ToString(pReader["CreateBy"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
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

        }
        #endregion
    }
}
