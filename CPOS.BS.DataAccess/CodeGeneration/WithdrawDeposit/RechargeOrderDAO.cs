/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-19 17:43:07
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
    /// 表RechargeOrder的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class RechargeOrderDAO : Base.BaseCPOSDAO, ICRUDable<RechargeOrderEntity>, IQueryable<RechargeOrderEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public RechargeOrderDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(RechargeOrderEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(RechargeOrderEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [RechargeOrder](");
            strSql.Append("[OrderNo],[OrderDesc],[VipID],[VipCardNo],[UnitCode],[TotalAmount],[ActuallyPaid],[ReturnAmount],[PayPoints],[ReceivePoints],[PayerID],[PayID],[Status],[CustomerID],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[OrderID])");
            strSql.Append(" values (");
            strSql.Append("@OrderNo,@OrderDesc,@VipID,@VipCardNo,@UnitCode,@TotalAmount,@ActuallyPaid,@ReturnAmount,@PayPoints,@ReceivePoints,@PayerID,@PayID,@Status,@CustomerID,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@OrderID)");            

			Guid? pkGuid;
			if (pEntity.OrderID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.OrderID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@OrderNo",SqlDbType.VarChar),
					new SqlParameter("@OrderDesc",SqlDbType.NVarChar),
					new SqlParameter("@VipID",SqlDbType.VarChar),
					new SqlParameter("@VipCardNo",SqlDbType.VarChar),
					new SqlParameter("@UnitCode",SqlDbType.VarChar),
					new SqlParameter("@TotalAmount",SqlDbType.Decimal),
					new SqlParameter("@ActuallyPaid",SqlDbType.Decimal),
					new SqlParameter("@ReturnAmount",SqlDbType.Decimal),
					new SqlParameter("@PayPoints",SqlDbType.Decimal),
					new SqlParameter("@ReceivePoints",SqlDbType.Decimal),
					new SqlParameter("@PayerID",SqlDbType.VarChar),
					new SqlParameter("@PayID",SqlDbType.VarChar),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@OrderID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.OrderNo;
			parameters[1].Value = pEntity.OrderDesc;
			parameters[2].Value = pEntity.VipID;
			parameters[3].Value = pEntity.VipCardNo;
			parameters[4].Value = pEntity.UnitCode;
			parameters[5].Value = pEntity.TotalAmount;
			parameters[6].Value = pEntity.ActuallyPaid;
			parameters[7].Value = pEntity.ReturnAmount;
			parameters[8].Value = pEntity.PayPoints;
			parameters[9].Value = pEntity.ReceivePoints;
			parameters[10].Value = pEntity.PayerID;
			parameters[11].Value = pEntity.PayID;
			parameters[12].Value = pEntity.Status;
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
            pEntity.OrderID = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public RechargeOrderEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [RechargeOrder] where OrderID='{0}'  and isdelete=0 ", id.ToString());
            //读取数据
            RechargeOrderEntity m = null;
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
        public RechargeOrderEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [RechargeOrder] where 1=1  and isdelete=0");
            //读取数据
            List<RechargeOrderEntity> list = new List<RechargeOrderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    RechargeOrderEntity m;
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
        public void Update(RechargeOrderEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(RechargeOrderEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.OrderID.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [RechargeOrder] set ");
                        if (pIsUpdateNullField || pEntity.OrderNo!=null)
                strSql.Append( "[OrderNo]=@OrderNo,");
            if (pIsUpdateNullField || pEntity.OrderDesc!=null)
                strSql.Append( "[OrderDesc]=@OrderDesc,");
            if (pIsUpdateNullField || pEntity.VipID!=null)
                strSql.Append( "[VipID]=@VipID,");
            if (pIsUpdateNullField || pEntity.VipCardNo!=null)
                strSql.Append( "[VipCardNo]=@VipCardNo,");
            if (pIsUpdateNullField || pEntity.UnitCode!=null)
                strSql.Append( "[UnitCode]=@UnitCode,");
            if (pIsUpdateNullField || pEntity.TotalAmount!=null)
                strSql.Append( "[TotalAmount]=@TotalAmount,");
            if (pIsUpdateNullField || pEntity.ActuallyPaid!=null)
                strSql.Append( "[ActuallyPaid]=@ActuallyPaid,");
            if (pIsUpdateNullField || pEntity.ReturnAmount!=null)
                strSql.Append( "[ReturnAmount]=@ReturnAmount,");
            if (pIsUpdateNullField || pEntity.PayPoints!=null)
                strSql.Append( "[PayPoints]=@PayPoints,");
            if (pIsUpdateNullField || pEntity.ReceivePoints!=null)
                strSql.Append( "[ReceivePoints]=@ReceivePoints,");
            if (pIsUpdateNullField || pEntity.PayerID!=null)
                strSql.Append( "[PayerID]=@PayerID,");
            if (pIsUpdateNullField || pEntity.PayID!=null)
                strSql.Append( "[PayID]=@PayID,");
            if (pIsUpdateNullField || pEntity.Status!=null)
                strSql.Append( "[Status]=@Status,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy");
            strSql.Append(" where OrderID=@OrderID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@OrderNo",SqlDbType.VarChar),
					new SqlParameter("@OrderDesc",SqlDbType.NVarChar),
					new SqlParameter("@VipID",SqlDbType.VarChar),
					new SqlParameter("@VipCardNo",SqlDbType.VarChar),
					new SqlParameter("@UnitCode",SqlDbType.VarChar),
					new SqlParameter("@TotalAmount",SqlDbType.Decimal),
					new SqlParameter("@ActuallyPaid",SqlDbType.Decimal),
					new SqlParameter("@ReturnAmount",SqlDbType.Decimal),
					new SqlParameter("@PayPoints",SqlDbType.Decimal),
					new SqlParameter("@ReceivePoints",SqlDbType.Decimal),
					new SqlParameter("@PayerID",SqlDbType.VarChar),
					new SqlParameter("@PayID",SqlDbType.VarChar),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@OrderID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.OrderNo;
			parameters[1].Value = pEntity.OrderDesc;
			parameters[2].Value = pEntity.VipID;
			parameters[3].Value = pEntity.VipCardNo;
			parameters[4].Value = pEntity.UnitCode;
			parameters[5].Value = pEntity.TotalAmount;
			parameters[6].Value = pEntity.ActuallyPaid;
			parameters[7].Value = pEntity.ReturnAmount;
			parameters[8].Value = pEntity.PayPoints;
			parameters[9].Value = pEntity.ReceivePoints;
			parameters[10].Value = pEntity.PayerID;
			parameters[11].Value = pEntity.PayID;
			parameters[12].Value = pEntity.Status;
			parameters[13].Value = pEntity.CustomerID;
			parameters[14].Value = pEntity.LastUpdateTime;
			parameters[15].Value = pEntity.LastUpdateBy;
			parameters[16].Value = pEntity.OrderID;

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
        public void Update(RechargeOrderEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(RechargeOrderEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(RechargeOrderEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.OrderID.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.OrderID.Value, pTran);           
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
            sql.AppendLine("update [RechargeOrder] set  isdelete=1 where OrderID=@OrderID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@OrderID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(RechargeOrderEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.OrderID.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.OrderID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(RechargeOrderEntity[] pEntities)
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
            sql.AppendLine("update [RechargeOrder] set  isdelete=1 where OrderID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public RechargeOrderEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [RechargeOrder] where 1=1  and isdelete=0 ");
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
            List<RechargeOrderEntity> list = new List<RechargeOrderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    RechargeOrderEntity m;
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
        public PagedQueryResult<RechargeOrderEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [OrderID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [RechargeOrder] where 1=1  and isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [RechargeOrder] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<RechargeOrderEntity> result = new PagedQueryResult<RechargeOrderEntity>();
            List<RechargeOrderEntity> list = new List<RechargeOrderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    RechargeOrderEntity m;
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
        public RechargeOrderEntity[] QueryByEntity(RechargeOrderEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<RechargeOrderEntity> PagedQueryByEntity(RechargeOrderEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(RechargeOrderEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.OrderID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderID", Value = pQueryEntity.OrderID });
            if (pQueryEntity.OrderNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderNo", Value = pQueryEntity.OrderNo });
            if (pQueryEntity.OrderDesc!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderDesc", Value = pQueryEntity.OrderDesc });
            if (pQueryEntity.VipID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipID", Value = pQueryEntity.VipID });
            if (pQueryEntity.VipCardNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardNo", Value = pQueryEntity.VipCardNo });
            if (pQueryEntity.UnitCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitCode", Value = pQueryEntity.UnitCode });
            if (pQueryEntity.TotalAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TotalAmount", Value = pQueryEntity.TotalAmount });
            if (pQueryEntity.ActuallyPaid!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ActuallyPaid", Value = pQueryEntity.ActuallyPaid });
            if (pQueryEntity.ReturnAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReturnAmount", Value = pQueryEntity.ReturnAmount });
            if (pQueryEntity.PayPoints!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PayPoints", Value = pQueryEntity.PayPoints });
            if (pQueryEntity.ReceivePoints!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReceivePoints", Value = pQueryEntity.ReceivePoints });
            if (pQueryEntity.PayerID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PayerID", Value = pQueryEntity.PayerID });
            if (pQueryEntity.PayID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PayID", Value = pQueryEntity.PayID });
            if (pQueryEntity.Status!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Status", Value = pQueryEntity.Status });
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
        protected void Load(IDataReader pReader, out RechargeOrderEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new RechargeOrderEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["OrderID"] != DBNull.Value)
			{
				pInstance.OrderID =  (Guid)pReader["OrderID"];
			}
			if (pReader["OrderNo"] != DBNull.Value)
			{
				pInstance.OrderNo =  Convert.ToString(pReader["OrderNo"]);
			}
			if (pReader["OrderDesc"] != DBNull.Value)
			{
				pInstance.OrderDesc =  Convert.ToString(pReader["OrderDesc"]);
			}
			if (pReader["VipID"] != DBNull.Value)
			{
				pInstance.VipID =  Convert.ToString(pReader["VipID"]);
			}
			if (pReader["VipCardNo"] != DBNull.Value)
			{
				pInstance.VipCardNo =  Convert.ToString(pReader["VipCardNo"]);
			}
			if (pReader["UnitCode"] != DBNull.Value)
			{
				pInstance.UnitCode =  Convert.ToString(pReader["UnitCode"]);
			}
			if (pReader["TotalAmount"] != DBNull.Value)
			{
				pInstance.TotalAmount =  Convert.ToDecimal(pReader["TotalAmount"]);
			}
			if (pReader["ActuallyPaid"] != DBNull.Value)
			{
				pInstance.ActuallyPaid =  Convert.ToDecimal(pReader["ActuallyPaid"]);
			}
			if (pReader["ReturnAmount"] != DBNull.Value)
			{
				pInstance.ReturnAmount =  Convert.ToDecimal(pReader["ReturnAmount"]);
			}
			if (pReader["PayPoints"] != DBNull.Value)
			{
				pInstance.PayPoints =  Convert.ToDecimal(pReader["PayPoints"]);
			}
			if (pReader["ReceivePoints"] != DBNull.Value)
			{
				pInstance.ReceivePoints =  Convert.ToDecimal(pReader["ReceivePoints"]);
			}
			if (pReader["PayerID"] != DBNull.Value)
			{
				pInstance.PayerID =  Convert.ToString(pReader["PayerID"]);
			}
			if (pReader["PayID"] != DBNull.Value)
			{
				pInstance.PayID =  Convert.ToString(pReader["PayID"]);
			}
			if (pReader["Status"] != DBNull.Value)
			{
				pInstance.Status =   Convert.ToInt32(pReader["Status"]);
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
