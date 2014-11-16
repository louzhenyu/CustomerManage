/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/14 10:15:25
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
    /// 表TPaymentOrderLog的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class TPaymentOrderLogDAO : Base.BaseCPOSDAO, ICRUDable<TPaymentOrderLogEntity>, IQueryable<TPaymentOrderLogEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TPaymentOrderLogDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(TPaymentOrderLogEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(TPaymentOrderLogEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [TPaymentOrderLog](");
            strSql.Append("[OrderId],[VipId],[ChannelID],[AppOrderTime],[AppOrderAmount],[AppOrderDesc],[Currency],[MobileNo],[ReturnUrl],[DynamicID],[DynamicIDType],[ResultCode],[PayUrl],[QrCodeUrl],[Message],[IsDelete],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[PaymentOrderId])");
            strSql.Append(" values (");
            strSql.Append("@OrderId,@VipId,@ChannelID,@AppOrderTime,@AppOrderAmount,@AppOrderDesc,@Currency,@MobileNo,@ReturnUrl,@DynamicID,@DynamicIDType,@ResultCode,@PayUrl,@QrCodeUrl,@Message,@IsDelete,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@PaymentOrderId)");            

			Guid? pkGuid;
			if (pEntity.PaymentOrderId == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.PaymentOrderId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@OrderId",SqlDbType.NVarChar),
					new SqlParameter("@VipId",SqlDbType.NVarChar),
					new SqlParameter("@ChannelID",SqlDbType.NVarChar),
					new SqlParameter("@AppOrderTime",SqlDbType.DateTime),
					new SqlParameter("@AppOrderAmount",SqlDbType.Int),
					new SqlParameter("@AppOrderDesc",SqlDbType.NVarChar),
					new SqlParameter("@Currency",SqlDbType.Int),
					new SqlParameter("@MobileNo",SqlDbType.NVarChar),
					new SqlParameter("@ReturnUrl",SqlDbType.NVarChar),
					new SqlParameter("@DynamicID",SqlDbType.NVarChar),
					new SqlParameter("@DynamicIDType",SqlDbType.NVarChar),
					new SqlParameter("@ResultCode",SqlDbType.Int),
					new SqlParameter("@PayUrl",SqlDbType.NVarChar),
					new SqlParameter("@QrCodeUrl",SqlDbType.NVarChar),
					new SqlParameter("@Message",SqlDbType.NVarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@PaymentOrderId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.OrderId;
			parameters[1].Value = pEntity.VipId;
			parameters[2].Value = pEntity.ChannelID;
			parameters[3].Value = pEntity.AppOrderTime;
			parameters[4].Value = pEntity.AppOrderAmount;
			parameters[5].Value = pEntity.AppOrderDesc;
			parameters[6].Value = pEntity.Currency;
			parameters[7].Value = pEntity.MobileNo;
			parameters[8].Value = pEntity.ReturnUrl;
			parameters[9].Value = pEntity.DynamicID;
			parameters[10].Value = pEntity.DynamicIDType;
			parameters[11].Value = pEntity.ResultCode;
			parameters[12].Value = pEntity.PayUrl;
			parameters[13].Value = pEntity.QrCodeUrl;
			parameters[14].Value = pEntity.Message;
			parameters[15].Value = pEntity.IsDelete;
			parameters[16].Value = pEntity.CreateTime;
			parameters[17].Value = pEntity.CreateBy;
			parameters[18].Value = pEntity.LastUpdateBy;
			parameters[19].Value = pEntity.LastUpdateTime;
			parameters[20].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.PaymentOrderId = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public TPaymentOrderLogEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [TPaymentOrderLog] where PaymentOrderId='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            TPaymentOrderLogEntity m = null;
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
        public TPaymentOrderLogEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [TPaymentOrderLog] where isdelete=0");
            //读取数据
            List<TPaymentOrderLogEntity> list = new List<TPaymentOrderLogEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    TPaymentOrderLogEntity m;
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
        public void Update(TPaymentOrderLogEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(TPaymentOrderLogEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.PaymentOrderId==null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [TPaymentOrderLog] set ");
            if (pIsUpdateNullField || pEntity.OrderId!=null)
                strSql.Append( "[OrderId]=@OrderId,");
            if (pIsUpdateNullField || pEntity.VipId!=null)
                strSql.Append( "[VipId]=@VipId,");
            if (pIsUpdateNullField || pEntity.ChannelID!=null)
                strSql.Append( "[ChannelID]=@ChannelID,");
            if (pIsUpdateNullField || pEntity.AppOrderTime!=null)
                strSql.Append( "[AppOrderTime]=@AppOrderTime,");
            if (pIsUpdateNullField || pEntity.AppOrderAmount!=null)
                strSql.Append( "[AppOrderAmount]=@AppOrderAmount,");
            if (pIsUpdateNullField || pEntity.AppOrderDesc!=null)
                strSql.Append( "[AppOrderDesc]=@AppOrderDesc,");
            if (pIsUpdateNullField || pEntity.Currency!=null)
                strSql.Append( "[Currency]=@Currency,");
            if (pIsUpdateNullField || pEntity.MobileNo!=null)
                strSql.Append( "[MobileNo]=@MobileNo,");
            if (pIsUpdateNullField || pEntity.ReturnUrl!=null)
                strSql.Append( "[ReturnUrl]=@ReturnUrl,");
            if (pIsUpdateNullField || pEntity.DynamicID!=null)
                strSql.Append( "[DynamicID]=@DynamicID,");
            if (pIsUpdateNullField || pEntity.DynamicIDType!=null)
                strSql.Append( "[DynamicIDType]=@DynamicIDType,");
            if (pIsUpdateNullField || pEntity.ResultCode!=null)
                strSql.Append( "[ResultCode]=@ResultCode,");
            if (pIsUpdateNullField || pEntity.PayUrl!=null)
                strSql.Append( "[PayUrl]=@PayUrl,");
            if (pIsUpdateNullField || pEntity.QrCodeUrl!=null)
                strSql.Append( "[QrCodeUrl]=@QrCodeUrl,");
            if (pIsUpdateNullField || pEntity.Message!=null)
                strSql.Append( "[Message]=@Message,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where PaymentOrderId=@PaymentOrderId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@OrderId",SqlDbType.NVarChar),
					new SqlParameter("@VipId",SqlDbType.NVarChar),
					new SqlParameter("@ChannelID",SqlDbType.NVarChar),
					new SqlParameter("@AppOrderTime",SqlDbType.DateTime),
					new SqlParameter("@AppOrderAmount",SqlDbType.Int),
					new SqlParameter("@AppOrderDesc",SqlDbType.NVarChar),
					new SqlParameter("@Currency",SqlDbType.Int),
					new SqlParameter("@MobileNo",SqlDbType.NVarChar),
					new SqlParameter("@ReturnUrl",SqlDbType.NVarChar),
					new SqlParameter("@DynamicID",SqlDbType.NVarChar),
					new SqlParameter("@DynamicIDType",SqlDbType.NVarChar),
					new SqlParameter("@ResultCode",SqlDbType.Int),
					new SqlParameter("@PayUrl",SqlDbType.NVarChar),
					new SqlParameter("@QrCodeUrl",SqlDbType.NVarChar),
					new SqlParameter("@Message",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@PaymentOrderId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.OrderId;
			parameters[1].Value = pEntity.VipId;
			parameters[2].Value = pEntity.ChannelID;
			parameters[3].Value = pEntity.AppOrderTime;
			parameters[4].Value = pEntity.AppOrderAmount;
			parameters[5].Value = pEntity.AppOrderDesc;
			parameters[6].Value = pEntity.Currency;
			parameters[7].Value = pEntity.MobileNo;
			parameters[8].Value = pEntity.ReturnUrl;
			parameters[9].Value = pEntity.DynamicID;
			parameters[10].Value = pEntity.DynamicIDType;
			parameters[11].Value = pEntity.ResultCode;
			parameters[12].Value = pEntity.PayUrl;
			parameters[13].Value = pEntity.QrCodeUrl;
			parameters[14].Value = pEntity.Message;
			parameters[15].Value = pEntity.LastUpdateBy;
			parameters[16].Value = pEntity.LastUpdateTime;
			parameters[17].Value = pEntity.PaymentOrderId;

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
        public void Update(TPaymentOrderLogEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(TPaymentOrderLogEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(TPaymentOrderLogEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(TPaymentOrderLogEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.PaymentOrderId==null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.PaymentOrderId, pTran);           
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
            sql.AppendLine("update [TPaymentOrderLog] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where PaymentOrderId=@PaymentOrderId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@PaymentOrderId",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(TPaymentOrderLogEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.PaymentOrderId==null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.PaymentOrderId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(TPaymentOrderLogEntity[] pEntities)
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
            sql.AppendLine("update [TPaymentOrderLog] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where PaymentOrderId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public TPaymentOrderLogEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [TPaymentOrderLog] where isdelete=0 ");
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
            List<TPaymentOrderLogEntity> list = new List<TPaymentOrderLogEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    TPaymentOrderLogEntity m;
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
        public PagedQueryResult<TPaymentOrderLogEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [PaymentOrderId] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [TPaymentOrderLog] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [TPaymentOrderLog] where isdelete=0 ");
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
            PagedQueryResult<TPaymentOrderLogEntity> result = new PagedQueryResult<TPaymentOrderLogEntity>();
            List<TPaymentOrderLogEntity> list = new List<TPaymentOrderLogEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    TPaymentOrderLogEntity m;
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
        public TPaymentOrderLogEntity[] QueryByEntity(TPaymentOrderLogEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<TPaymentOrderLogEntity> PagedQueryByEntity(TPaymentOrderLogEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(TPaymentOrderLogEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.PaymentOrderId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PaymentOrderId", Value = pQueryEntity.PaymentOrderId });
            if (pQueryEntity.OrderId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderId", Value = pQueryEntity.OrderId });
            if (pQueryEntity.VipId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipId", Value = pQueryEntity.VipId });
            if (pQueryEntity.ChannelID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ChannelID", Value = pQueryEntity.ChannelID });
            if (pQueryEntity.AppOrderTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AppOrderTime", Value = pQueryEntity.AppOrderTime });
            if (pQueryEntity.AppOrderAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AppOrderAmount", Value = pQueryEntity.AppOrderAmount });
            if (pQueryEntity.AppOrderDesc!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AppOrderDesc", Value = pQueryEntity.AppOrderDesc });
            if (pQueryEntity.Currency!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Currency", Value = pQueryEntity.Currency });
            if (pQueryEntity.MobileNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MobileNo", Value = pQueryEntity.MobileNo });
            if (pQueryEntity.ReturnUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReturnUrl", Value = pQueryEntity.ReturnUrl });
            if (pQueryEntity.DynamicID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DynamicID", Value = pQueryEntity.DynamicID });
            if (pQueryEntity.DynamicIDType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DynamicIDType", Value = pQueryEntity.DynamicIDType });
            if (pQueryEntity.ResultCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ResultCode", Value = pQueryEntity.ResultCode });
            if (pQueryEntity.PayUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PayUrl", Value = pQueryEntity.PayUrl });
            if (pQueryEntity.QrCodeUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "QrCodeUrl", Value = pQueryEntity.QrCodeUrl });
            if (pQueryEntity.Message!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Message", Value = pQueryEntity.Message });
            if (pQueryEntity.IsDelete!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.LastUpdateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.LastUpdateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out TPaymentOrderLogEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new TPaymentOrderLogEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["PaymentOrderId"] != DBNull.Value)
			{
				pInstance.PaymentOrderId =  (Guid)pReader["PaymentOrderId"];
			}
			if (pReader["OrderId"] != DBNull.Value)
			{
				pInstance.OrderId =  Convert.ToString(pReader["OrderId"]);
			}
			if (pReader["VipId"] != DBNull.Value)
			{
				pInstance.VipId =  Convert.ToString(pReader["VipId"]);
			}
			if (pReader["ChannelID"] != DBNull.Value)
			{
				pInstance.ChannelID =  Convert.ToString(pReader["ChannelID"]);
			}
			if (pReader["AppOrderTime"] != DBNull.Value)
			{
				pInstance.AppOrderTime =  Convert.ToDateTime(pReader["AppOrderTime"]);
			}
			if (pReader["AppOrderAmount"] != DBNull.Value)
			{
				pInstance.AppOrderAmount =   Convert.ToInt32(pReader["AppOrderAmount"]);
			}
			if (pReader["AppOrderDesc"] != DBNull.Value)
			{
				pInstance.AppOrderDesc =  Convert.ToString(pReader["AppOrderDesc"]);
			}
			if (pReader["Currency"] != DBNull.Value)
			{
				pInstance.Currency =   Convert.ToInt32(pReader["Currency"]);
			}
			if (pReader["MobileNo"] != DBNull.Value)
			{
				pInstance.MobileNo =  Convert.ToString(pReader["MobileNo"]);
			}
			if (pReader["ReturnUrl"] != DBNull.Value)
			{
				pInstance.ReturnUrl =  Convert.ToString(pReader["ReturnUrl"]);
			}
			if (pReader["DynamicID"] != DBNull.Value)
			{
				pInstance.DynamicID =  Convert.ToString(pReader["DynamicID"]);
			}
			if (pReader["DynamicIDType"] != DBNull.Value)
			{
				pInstance.DynamicIDType =  Convert.ToString(pReader["DynamicIDType"]);
			}
			if (pReader["ResultCode"] != DBNull.Value)
			{
				pInstance.ResultCode =   Convert.ToInt32(pReader["ResultCode"]);
			}
			if (pReader["PayUrl"] != DBNull.Value)
			{
				pInstance.PayUrl =  Convert.ToString(pReader["PayUrl"]);
			}
			if (pReader["QrCodeUrl"] != DBNull.Value)
			{
				pInstance.QrCodeUrl =  Convert.ToString(pReader["QrCodeUrl"]);
			}
			if (pReader["Message"] != DBNull.Value)
			{
				pInstance.Message =  Convert.ToString(pReader["Message"]);
			}
			if (pReader["IsDelete"] != DBNull.Value)
			{
				pInstance.IsDelete =   Convert.ToInt32(pReader["IsDelete"]);
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

        }
        #endregion
    }
}
