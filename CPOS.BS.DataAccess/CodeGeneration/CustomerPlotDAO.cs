/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/18 10:22:22
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
    /// 表CustomerPlot的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class CustomerPlotDAO : BaseCPOSDAO, ICRUDable<CustomerPlotEntity>, IQueryable<CustomerPlotEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public CustomerPlotDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(CustomerPlotEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(CustomerPlotEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [CustomerPlot](");
            strSql.Append("[TimeDiff],[OffPeriod],[MinAmount],[TheAmountDay],[FailureNo],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[CustomerId],[PaypalRate],[CUPRate],[PayRemark],[AladingRate],[PlotId])");
            strSql.Append(" values (");
            strSql.Append("@TimeDiff,@OffPeriod,@MinAmount,@TheAmountDay,@FailureNo,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@CustomerId,@PaypalRate,@CUPRate,@PayRemark,@AladingRate,@PlotId)");            

			Guid? pkGuid;
			if (pEntity.PlotId == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.PlotId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@TimeDiff",SqlDbType.Int),
					new SqlParameter("@OffPeriod",SqlDbType.Int),
					new SqlParameter("@MinAmount",SqlDbType.Int),
					new SqlParameter("@TheAmountDay",SqlDbType.Int),
					new SqlParameter("@FailureNo",SqlDbType.Int),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@PaypalRate",SqlDbType.Decimal),
					new SqlParameter("@CUPRate",SqlDbType.Decimal),
					new SqlParameter("@PayRemark",SqlDbType.NVarChar),
					new SqlParameter("@AladingRate",SqlDbType.Decimal),
					new SqlParameter("@PlotId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.TimeDiff;
			parameters[1].Value = pEntity.OffPeriod;
			parameters[2].Value = pEntity.MinAmount;
			parameters[3].Value = pEntity.TheAmountDay;
			parameters[4].Value = pEntity.FailureNo;
			parameters[5].Value = pEntity.CreateBy;
			parameters[6].Value = pEntity.CreateTime;
			parameters[7].Value = pEntity.LastUpdateBy;
			parameters[8].Value = pEntity.LastUpdateTime;
			parameters[9].Value = pEntity.IsDelete;
			parameters[10].Value = pEntity.CustomerId;
			parameters[11].Value = pEntity.PaypalRate;
			parameters[12].Value = pEntity.CUPRate;
			parameters[13].Value = pEntity.PayRemark;
			parameters[14].Value = pEntity.AladingRate;
			parameters[15].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.PlotId = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public CustomerPlotEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [CustomerPlot] where PlotId='{0}'  and isdelete=0 ", id.ToString());
            //读取数据
            CustomerPlotEntity m = null;
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
        public CustomerPlotEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [CustomerPlot] where 1=1  and isdelete=0");
            //读取数据
            List<CustomerPlotEntity> list = new List<CustomerPlotEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    CustomerPlotEntity m;
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
        public void Update(CustomerPlotEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(CustomerPlotEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.PlotId.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [CustomerPlot] set ");
                        if (pIsUpdateNullField || pEntity.TimeDiff!=null)
                strSql.Append( "[TimeDiff]=@TimeDiff,");
            if (pIsUpdateNullField || pEntity.OffPeriod!=null)
                strSql.Append( "[OffPeriod]=@OffPeriod,");
            if (pIsUpdateNullField || pEntity.MinAmount!=null)
                strSql.Append( "[MinAmount]=@MinAmount,");
            if (pIsUpdateNullField || pEntity.TheAmountDay!=null)
                strSql.Append( "[TheAmountDay]=@TheAmountDay,");
            if (pIsUpdateNullField || pEntity.FailureNo!=null)
                strSql.Append( "[FailureNo]=@FailureNo,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.PaypalRate!=null)
                strSql.Append( "[PaypalRate]=@PaypalRate,");
            if (pIsUpdateNullField || pEntity.CUPRate!=null)
                strSql.Append( "[CUPRate]=@CUPRate,");
            if (pIsUpdateNullField || pEntity.PayRemark!=null)
                strSql.Append( "[PayRemark]=@PayRemark,");
            if (pIsUpdateNullField || pEntity.AladingRate!=null)
                strSql.Append( "[AladingRate]=@AladingRate");
            strSql.Append(" where PlotId=@PlotId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@TimeDiff",SqlDbType.Int),
					new SqlParameter("@OffPeriod",SqlDbType.Int),
					new SqlParameter("@MinAmount",SqlDbType.Int),
					new SqlParameter("@TheAmountDay",SqlDbType.Int),
					new SqlParameter("@FailureNo",SqlDbType.Int),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@PaypalRate",SqlDbType.Decimal),
					new SqlParameter("@CUPRate",SqlDbType.Decimal),
					new SqlParameter("@PayRemark",SqlDbType.NVarChar),
					new SqlParameter("@AladingRate",SqlDbType.Decimal),
					new SqlParameter("@PlotId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.TimeDiff;
			parameters[1].Value = pEntity.OffPeriod;
			parameters[2].Value = pEntity.MinAmount;
			parameters[3].Value = pEntity.TheAmountDay;
			parameters[4].Value = pEntity.FailureNo;
			parameters[5].Value = pEntity.LastUpdateBy;
			parameters[6].Value = pEntity.LastUpdateTime;
			parameters[7].Value = pEntity.CustomerId;
			parameters[8].Value = pEntity.PaypalRate;
			parameters[9].Value = pEntity.CUPRate;
			parameters[10].Value = pEntity.PayRemark;
			parameters[11].Value = pEntity.AladingRate;
			parameters[12].Value = pEntity.PlotId;

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
        public void Update(CustomerPlotEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(CustomerPlotEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(CustomerPlotEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.PlotId.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.PlotId.Value, pTran);           
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
            sql.AppendLine("update [CustomerPlot] set  isdelete=1 where PlotId=@PlotId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@PlotId",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(CustomerPlotEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.PlotId.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.PlotId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(CustomerPlotEntity[] pEntities)
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
            sql.AppendLine("update [CustomerPlot] set  isdelete=1 where PlotId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public CustomerPlotEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [CustomerPlot] where 1=1  and isdelete=0 ");
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
            List<CustomerPlotEntity> list = new List<CustomerPlotEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    CustomerPlotEntity m;
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
        public PagedQueryResult<CustomerPlotEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [PlotId] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [CustomerPlot] where 1=1  and isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [CustomerPlot] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<CustomerPlotEntity> result = new PagedQueryResult<CustomerPlotEntity>();
            List<CustomerPlotEntity> list = new List<CustomerPlotEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    CustomerPlotEntity m;
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
        public CustomerPlotEntity[] QueryByEntity(CustomerPlotEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<CustomerPlotEntity> PagedQueryByEntity(CustomerPlotEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(CustomerPlotEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.PlotId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PlotId", Value = pQueryEntity.PlotId });
            if (pQueryEntity.TimeDiff!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TimeDiff", Value = pQueryEntity.TimeDiff });
            if (pQueryEntity.OffPeriod!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OffPeriod", Value = pQueryEntity.OffPeriod });
            if (pQueryEntity.MinAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MinAmount", Value = pQueryEntity.MinAmount });
            if (pQueryEntity.TheAmountDay!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TheAmountDay", Value = pQueryEntity.TheAmountDay });
            if (pQueryEntity.FailureNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FailureNo", Value = pQueryEntity.FailureNo });
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
            if (pQueryEntity.PaypalRate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PaypalRate", Value = pQueryEntity.PaypalRate });
            if (pQueryEntity.CUPRate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CUPRate", Value = pQueryEntity.CUPRate });
            if (pQueryEntity.PayRemark!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PayRemark", Value = pQueryEntity.PayRemark });
            if (pQueryEntity.AladingRate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AladingRate", Value = pQueryEntity.AladingRate });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out CustomerPlotEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new CustomerPlotEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["PlotId"] != DBNull.Value)
			{
				pInstance.PlotId =  (Guid)pReader["PlotId"];
			}
			if (pReader["TimeDiff"] != DBNull.Value)
			{
				pInstance.TimeDiff =   Convert.ToInt32(pReader["TimeDiff"]);
			}
			if (pReader["OffPeriod"] != DBNull.Value)
			{
				pInstance.OffPeriod =   Convert.ToInt32(pReader["OffPeriod"]);
			}
			if (pReader["MinAmount"] != DBNull.Value)
			{
				pInstance.MinAmount =   Convert.ToInt32(pReader["MinAmount"]);
			}
			if (pReader["TheAmountDay"] != DBNull.Value)
			{
				pInstance.TheAmountDay =   Convert.ToInt32(pReader["TheAmountDay"]);
			}
			if (pReader["FailureNo"] != DBNull.Value)
			{
				pInstance.FailureNo =   Convert.ToInt32(pReader["FailureNo"]);
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
			if (pReader["PaypalRate"] != DBNull.Value)
			{
				pInstance.PaypalRate =  Convert.ToDecimal(pReader["PaypalRate"]);
			}
			if (pReader["CUPRate"] != DBNull.Value)
			{
				pInstance.CUPRate =  Convert.ToDecimal(pReader["CUPRate"]);
			}
			if (pReader["PayRemark"] != DBNull.Value)
			{
				pInstance.PayRemark =  Convert.ToString(pReader["PayRemark"]);
			}
			if (pReader["AladingRate"] != DBNull.Value)
			{
				pInstance.AladingRate =  Convert.ToDecimal(pReader["AladingRate"]);
			}

        }
        #endregion
    }
}
