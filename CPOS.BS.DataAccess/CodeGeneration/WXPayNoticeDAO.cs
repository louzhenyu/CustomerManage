/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/7 13:41:42
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
    /// 表WXPayNotice的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class WXPayNoticeDAO : BaseCPOSDAO, ICRUDable<WXPayNoticeEntity>, IQueryable<WXPayNoticeEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public WXPayNoticeDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(WXPayNoticeEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(WXPayNoticeEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [WXPayNotice](");
            strSql.Append("[SignType],[ServiceVersion],[InputCharset],[Sign],[SignKeyIndex],[TradeMode],[TradeState],[PayInfo],[Partner],[BankType],[BankBillno],[TotalFee],[FeeType],[NotifyId],[TransactionId],[OutTradeNo],[Attach],[TimeEnd],[TransportFee],[ProductFee],[Discount],[BuyerAlias],[AppId],[TimeStamp],[NonceStr],[OpenId],[AppSignature],[IsSubscribe],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[CustomerId],[PayNoticeId])");
            strSql.Append(" values (");
            strSql.Append("@SignType,@ServiceVersion,@InputCharset,@Sign,@SignKeyIndex,@TradeMode,@TradeState,@PayInfo,@Partner,@BankType,@BankBillno,@TotalFee,@FeeType,@NotifyId,@TransactionId,@OutTradeNo,@Attach,@TimeEnd,@TransportFee,@ProductFee,@Discount,@BuyerAlias,@AppId,@TimeStamp,@NonceStr,@OpenId,@AppSignature,@IsSubscribe,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@CustomerId,@PayNoticeId)");            

			Guid? pkGuid;
			if (pEntity.PayNoticeId == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.PayNoticeId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@SignType",SqlDbType.NVarChar),
					new SqlParameter("@ServiceVersion",SqlDbType.NVarChar),
					new SqlParameter("@InputCharset",SqlDbType.NVarChar),
					new SqlParameter("@Sign",SqlDbType.NVarChar),
					new SqlParameter("@SignKeyIndex",SqlDbType.Int),
					new SqlParameter("@TradeMode",SqlDbType.Int),
					new SqlParameter("@TradeState",SqlDbType.Int),
					new SqlParameter("@PayInfo",SqlDbType.NVarChar),
					new SqlParameter("@Partner",SqlDbType.NVarChar),
					new SqlParameter("@BankType",SqlDbType.NVarChar),
					new SqlParameter("@BankBillno",SqlDbType.NVarChar),
					new SqlParameter("@TotalFee",SqlDbType.Int),
					new SqlParameter("@FeeType",SqlDbType.Int),
					new SqlParameter("@NotifyId",SqlDbType.NVarChar),
					new SqlParameter("@TransactionId",SqlDbType.NVarChar),
					new SqlParameter("@OutTradeNo",SqlDbType.NVarChar),
					new SqlParameter("@Attach",SqlDbType.NVarChar),
					new SqlParameter("@TimeEnd",SqlDbType.NVarChar),
					new SqlParameter("@TransportFee",SqlDbType.Int),
					new SqlParameter("@ProductFee",SqlDbType.Int),
					new SqlParameter("@Discount",SqlDbType.Int),
					new SqlParameter("@BuyerAlias",SqlDbType.NVarChar),
					new SqlParameter("@AppId",SqlDbType.NVarChar),
					new SqlParameter("@TimeStamp",SqlDbType.Int),
					new SqlParameter("@NonceStr",SqlDbType.NVarChar),
					new SqlParameter("@OpenId",SqlDbType.NVarChar),
					new SqlParameter("@AppSignature",SqlDbType.NVarChar),
					new SqlParameter("@IsSubscribe",SqlDbType.Int),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@PayNoticeId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.SignType;
			parameters[1].Value = pEntity.ServiceVersion;
			parameters[2].Value = pEntity.InputCharset;
			parameters[3].Value = pEntity.Sign;
			parameters[4].Value = pEntity.SignKeyIndex;
			parameters[5].Value = pEntity.TradeMode;
			parameters[6].Value = pEntity.TradeState;
			parameters[7].Value = pEntity.PayInfo;
			parameters[8].Value = pEntity.Partner;
			parameters[9].Value = pEntity.BankType;
			parameters[10].Value = pEntity.BankBillno;
			parameters[11].Value = pEntity.TotalFee;
			parameters[12].Value = pEntity.FeeType;
			parameters[13].Value = pEntity.NotifyId;
			parameters[14].Value = pEntity.TransactionId;
			parameters[15].Value = pEntity.OutTradeNo;
			parameters[16].Value = pEntity.Attach;
			parameters[17].Value = pEntity.TimeEnd;
			parameters[18].Value = pEntity.TransportFee;
			parameters[19].Value = pEntity.ProductFee;
			parameters[20].Value = pEntity.Discount;
			parameters[21].Value = pEntity.BuyerAlias;
			parameters[22].Value = pEntity.AppId;
			parameters[23].Value = pEntity.TimeStamp;
			parameters[24].Value = pEntity.NonceStr;
			parameters[25].Value = pEntity.OpenId;
			parameters[26].Value = pEntity.AppSignature;
			parameters[27].Value = pEntity.IsSubscribe;
			parameters[28].Value = pEntity.CreateBy;
			parameters[29].Value = pEntity.CreateTime;
			parameters[30].Value = pEntity.LastUpdateBy;
			parameters[31].Value = pEntity.LastUpdateTime;
			parameters[32].Value = pEntity.IsDelete;
			parameters[33].Value = pEntity.CustomerId;
			parameters[34].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.PayNoticeId = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public WXPayNoticeEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXPayNotice] where PayNoticeId='{0}'  and isdelete=0 ", id.ToString());
            //读取数据
            WXPayNoticeEntity m = null;
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
        public WXPayNoticeEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXPayNotice] where 1=1  and isdelete=0");
            //读取数据
            List<WXPayNoticeEntity> list = new List<WXPayNoticeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WXPayNoticeEntity m;
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
        public void Update(WXPayNoticeEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(WXPayNoticeEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.PayNoticeId.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [WXPayNotice] set ");
                        if (pIsUpdateNullField || pEntity.SignType!=null)
                strSql.Append( "[SignType]=@SignType,");
            if (pIsUpdateNullField || pEntity.ServiceVersion!=null)
                strSql.Append( "[ServiceVersion]=@ServiceVersion,");
            if (pIsUpdateNullField || pEntity.InputCharset!=null)
                strSql.Append( "[InputCharset]=@InputCharset,");
            if (pIsUpdateNullField || pEntity.Sign!=null)
                strSql.Append( "[Sign]=@Sign,");
            if (pIsUpdateNullField || pEntity.SignKeyIndex!=null)
                strSql.Append( "[SignKeyIndex]=@SignKeyIndex,");
            if (pIsUpdateNullField || pEntity.TradeMode!=null)
                strSql.Append( "[TradeMode]=@TradeMode,");
            if (pIsUpdateNullField || pEntity.TradeState!=null)
                strSql.Append( "[TradeState]=@TradeState,");
            if (pIsUpdateNullField || pEntity.PayInfo!=null)
                strSql.Append( "[PayInfo]=@PayInfo,");
            if (pIsUpdateNullField || pEntity.Partner!=null)
                strSql.Append( "[Partner]=@Partner,");
            if (pIsUpdateNullField || pEntity.BankType!=null)
                strSql.Append( "[BankType]=@BankType,");
            if (pIsUpdateNullField || pEntity.BankBillno!=null)
                strSql.Append( "[BankBillno]=@BankBillno,");
            if (pIsUpdateNullField || pEntity.TotalFee!=null)
                strSql.Append( "[TotalFee]=@TotalFee,");
            if (pIsUpdateNullField || pEntity.FeeType!=null)
                strSql.Append( "[FeeType]=@FeeType,");
            if (pIsUpdateNullField || pEntity.NotifyId!=null)
                strSql.Append( "[NotifyId]=@NotifyId,");
            if (pIsUpdateNullField || pEntity.TransactionId!=null)
                strSql.Append( "[TransactionId]=@TransactionId,");
            if (pIsUpdateNullField || pEntity.OutTradeNo!=null)
                strSql.Append( "[OutTradeNo]=@OutTradeNo,");
            if (pIsUpdateNullField || pEntity.Attach!=null)
                strSql.Append( "[Attach]=@Attach,");
            if (pIsUpdateNullField || pEntity.TimeEnd!=null)
                strSql.Append( "[TimeEnd]=@TimeEnd,");
            if (pIsUpdateNullField || pEntity.TransportFee!=null)
                strSql.Append( "[TransportFee]=@TransportFee,");
            if (pIsUpdateNullField || pEntity.ProductFee!=null)
                strSql.Append( "[ProductFee]=@ProductFee,");
            if (pIsUpdateNullField || pEntity.Discount!=null)
                strSql.Append( "[Discount]=@Discount,");
            if (pIsUpdateNullField || pEntity.BuyerAlias!=null)
                strSql.Append( "[BuyerAlias]=@BuyerAlias,");
            if (pIsUpdateNullField || pEntity.AppId!=null)
                strSql.Append( "[AppId]=@AppId,");
            if (pIsUpdateNullField || pEntity.TimeStamp!=null)
                strSql.Append( "[TimeStamp]=@TimeStamp,");
            if (pIsUpdateNullField || pEntity.NonceStr!=null)
                strSql.Append( "[NonceStr]=@NonceStr,");
            if (pIsUpdateNullField || pEntity.OpenId!=null)
                strSql.Append( "[OpenId]=@OpenId,");
            if (pIsUpdateNullField || pEntity.AppSignature!=null)
                strSql.Append( "[AppSignature]=@AppSignature,");
            if (pIsUpdateNullField || pEntity.IsSubscribe!=null)
                strSql.Append( "[IsSubscribe]=@IsSubscribe,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId");
            strSql.Append(" where PayNoticeId=@PayNoticeId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@SignType",SqlDbType.NVarChar),
					new SqlParameter("@ServiceVersion",SqlDbType.NVarChar),
					new SqlParameter("@InputCharset",SqlDbType.NVarChar),
					new SqlParameter("@Sign",SqlDbType.NVarChar),
					new SqlParameter("@SignKeyIndex",SqlDbType.Int),
					new SqlParameter("@TradeMode",SqlDbType.Int),
					new SqlParameter("@TradeState",SqlDbType.Int),
					new SqlParameter("@PayInfo",SqlDbType.NVarChar),
					new SqlParameter("@Partner",SqlDbType.NVarChar),
					new SqlParameter("@BankType",SqlDbType.NVarChar),
					new SqlParameter("@BankBillno",SqlDbType.NVarChar),
					new SqlParameter("@TotalFee",SqlDbType.Int),
					new SqlParameter("@FeeType",SqlDbType.Int),
					new SqlParameter("@NotifyId",SqlDbType.NVarChar),
					new SqlParameter("@TransactionId",SqlDbType.NVarChar),
					new SqlParameter("@OutTradeNo",SqlDbType.NVarChar),
					new SqlParameter("@Attach",SqlDbType.NVarChar),
					new SqlParameter("@TimeEnd",SqlDbType.NVarChar),
					new SqlParameter("@TransportFee",SqlDbType.Int),
					new SqlParameter("@ProductFee",SqlDbType.Int),
					new SqlParameter("@Discount",SqlDbType.Int),
					new SqlParameter("@BuyerAlias",SqlDbType.NVarChar),
					new SqlParameter("@AppId",SqlDbType.NVarChar),
					new SqlParameter("@TimeStamp",SqlDbType.Int),
					new SqlParameter("@NonceStr",SqlDbType.NVarChar),
					new SqlParameter("@OpenId",SqlDbType.NVarChar),
					new SqlParameter("@AppSignature",SqlDbType.NVarChar),
					new SqlParameter("@IsSubscribe",SqlDbType.Int),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@PayNoticeId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.SignType;
			parameters[1].Value = pEntity.ServiceVersion;
			parameters[2].Value = pEntity.InputCharset;
			parameters[3].Value = pEntity.Sign;
			parameters[4].Value = pEntity.SignKeyIndex;
			parameters[5].Value = pEntity.TradeMode;
			parameters[6].Value = pEntity.TradeState;
			parameters[7].Value = pEntity.PayInfo;
			parameters[8].Value = pEntity.Partner;
			parameters[9].Value = pEntity.BankType;
			parameters[10].Value = pEntity.BankBillno;
			parameters[11].Value = pEntity.TotalFee;
			parameters[12].Value = pEntity.FeeType;
			parameters[13].Value = pEntity.NotifyId;
			parameters[14].Value = pEntity.TransactionId;
			parameters[15].Value = pEntity.OutTradeNo;
			parameters[16].Value = pEntity.Attach;
			parameters[17].Value = pEntity.TimeEnd;
			parameters[18].Value = pEntity.TransportFee;
			parameters[19].Value = pEntity.ProductFee;
			parameters[20].Value = pEntity.Discount;
			parameters[21].Value = pEntity.BuyerAlias;
			parameters[22].Value = pEntity.AppId;
			parameters[23].Value = pEntity.TimeStamp;
			parameters[24].Value = pEntity.NonceStr;
			parameters[25].Value = pEntity.OpenId;
			parameters[26].Value = pEntity.AppSignature;
			parameters[27].Value = pEntity.IsSubscribe;
			parameters[28].Value = pEntity.LastUpdateBy;
			parameters[29].Value = pEntity.LastUpdateTime;
			parameters[30].Value = pEntity.CustomerId;
			parameters[31].Value = pEntity.PayNoticeId;

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
        public void Update(WXPayNoticeEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(WXPayNoticeEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(WXPayNoticeEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.PayNoticeId.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.PayNoticeId.Value, pTran);           
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
            sql.AppendLine("update [WXPayNotice] set  isdelete=1 where PayNoticeId=@PayNoticeId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@PayNoticeId",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(WXPayNoticeEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.PayNoticeId.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.PayNoticeId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(WXPayNoticeEntity[] pEntities)
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
            sql.AppendLine("update [WXPayNotice] set  isdelete=1 where PayNoticeId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public WXPayNoticeEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXPayNotice] where 1=1  and isdelete=0 ");
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
            List<WXPayNoticeEntity> list = new List<WXPayNoticeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WXPayNoticeEntity m;
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
        public PagedQueryResult<WXPayNoticeEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [PayNoticeId] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [WXPayNotice] where 1=1  and isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [WXPayNotice] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<WXPayNoticeEntity> result = new PagedQueryResult<WXPayNoticeEntity>();
            List<WXPayNoticeEntity> list = new List<WXPayNoticeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    WXPayNoticeEntity m;
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
        public WXPayNoticeEntity[] QueryByEntity(WXPayNoticeEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<WXPayNoticeEntity> PagedQueryByEntity(WXPayNoticeEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(WXPayNoticeEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.PayNoticeId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PayNoticeId", Value = pQueryEntity.PayNoticeId });
            if (pQueryEntity.SignType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SignType", Value = pQueryEntity.SignType });
            if (pQueryEntity.ServiceVersion!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ServiceVersion", Value = pQueryEntity.ServiceVersion });
            if (pQueryEntity.InputCharset!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "InputCharset", Value = pQueryEntity.InputCharset });
            if (pQueryEntity.Sign!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Sign", Value = pQueryEntity.Sign });
            if (pQueryEntity.SignKeyIndex!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SignKeyIndex", Value = pQueryEntity.SignKeyIndex });
            if (pQueryEntity.TradeMode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TradeMode", Value = pQueryEntity.TradeMode });
            if (pQueryEntity.TradeState!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TradeState", Value = pQueryEntity.TradeState });
            if (pQueryEntity.PayInfo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PayInfo", Value = pQueryEntity.PayInfo });
            if (pQueryEntity.Partner!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Partner", Value = pQueryEntity.Partner });
            if (pQueryEntity.BankType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BankType", Value = pQueryEntity.BankType });
            if (pQueryEntity.BankBillno!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BankBillno", Value = pQueryEntity.BankBillno });
            if (pQueryEntity.TotalFee!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TotalFee", Value = pQueryEntity.TotalFee });
            if (pQueryEntity.FeeType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FeeType", Value = pQueryEntity.FeeType });
            if (pQueryEntity.NotifyId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NotifyId", Value = pQueryEntity.NotifyId });
            if (pQueryEntity.TransactionId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TransactionId", Value = pQueryEntity.TransactionId });
            if (pQueryEntity.OutTradeNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OutTradeNo", Value = pQueryEntity.OutTradeNo });
            if (pQueryEntity.Attach!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Attach", Value = pQueryEntity.Attach });
            if (pQueryEntity.TimeEnd!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TimeEnd", Value = pQueryEntity.TimeEnd });
            if (pQueryEntity.TransportFee!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TransportFee", Value = pQueryEntity.TransportFee });
            if (pQueryEntity.ProductFee!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ProductFee", Value = pQueryEntity.ProductFee });
            if (pQueryEntity.Discount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Discount", Value = pQueryEntity.Discount });
            if (pQueryEntity.BuyerAlias!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BuyerAlias", Value = pQueryEntity.BuyerAlias });
            if (pQueryEntity.AppId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AppId", Value = pQueryEntity.AppId });
            if (pQueryEntity.TimeStamp!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TimeStamp", Value = pQueryEntity.TimeStamp });
            if (pQueryEntity.NonceStr!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NonceStr", Value = pQueryEntity.NonceStr });
            if (pQueryEntity.OpenId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OpenId", Value = pQueryEntity.OpenId });
            if (pQueryEntity.AppSignature!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AppSignature", Value = pQueryEntity.AppSignature });
            if (pQueryEntity.IsSubscribe!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsSubscribe", Value = pQueryEntity.IsSubscribe });
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
        protected void Load(IDataReader pReader, out WXPayNoticeEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new WXPayNoticeEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["PayNoticeId"] != DBNull.Value)
			{
				pInstance.PayNoticeId =  (Guid)pReader["PayNoticeId"];
			}
			if (pReader["SignType"] != DBNull.Value)
			{
				pInstance.SignType =  Convert.ToString(pReader["SignType"]);
			}
			if (pReader["ServiceVersion"] != DBNull.Value)
			{
				pInstance.ServiceVersion =  Convert.ToString(pReader["ServiceVersion"]);
			}
			if (pReader["InputCharset"] != DBNull.Value)
			{
				pInstance.InputCharset =  Convert.ToString(pReader["InputCharset"]);
			}
			if (pReader["Sign"] != DBNull.Value)
			{
				pInstance.Sign =  Convert.ToString(pReader["Sign"]);
			}
			if (pReader["SignKeyIndex"] != DBNull.Value)
			{
				pInstance.SignKeyIndex =   Convert.ToInt32(pReader["SignKeyIndex"]);
			}
			if (pReader["TradeMode"] != DBNull.Value)
			{
				pInstance.TradeMode =   Convert.ToInt32(pReader["TradeMode"]);
			}
			if (pReader["TradeState"] != DBNull.Value)
			{
				pInstance.TradeState =   Convert.ToInt32(pReader["TradeState"]);
			}
			if (pReader["PayInfo"] != DBNull.Value)
			{
				pInstance.PayInfo =  Convert.ToString(pReader["PayInfo"]);
			}
			if (pReader["Partner"] != DBNull.Value)
			{
				pInstance.Partner =  Convert.ToString(pReader["Partner"]);
			}
			if (pReader["BankType"] != DBNull.Value)
			{
				pInstance.BankType =  Convert.ToString(pReader["BankType"]);
			}
			if (pReader["BankBillno"] != DBNull.Value)
			{
				pInstance.BankBillno =  Convert.ToString(pReader["BankBillno"]);
			}
			if (pReader["TotalFee"] != DBNull.Value)
			{
				pInstance.TotalFee =   Convert.ToInt32(pReader["TotalFee"]);
			}
			if (pReader["FeeType"] != DBNull.Value)
			{
				pInstance.FeeType =   Convert.ToInt32(pReader["FeeType"]);
			}
			if (pReader["NotifyId"] != DBNull.Value)
			{
				pInstance.NotifyId =  Convert.ToString(pReader["NotifyId"]);
			}
			if (pReader["TransactionId"] != DBNull.Value)
			{
				pInstance.TransactionId =  Convert.ToString(pReader["TransactionId"]);
			}
			if (pReader["OutTradeNo"] != DBNull.Value)
			{
				pInstance.OutTradeNo =  Convert.ToString(pReader["OutTradeNo"]);
			}
			if (pReader["Attach"] != DBNull.Value)
			{
				pInstance.Attach =  Convert.ToString(pReader["Attach"]);
			}
			if (pReader["TimeEnd"] != DBNull.Value)
			{
				pInstance.TimeEnd =  Convert.ToString(pReader["TimeEnd"]);
			}
			if (pReader["TransportFee"] != DBNull.Value)
			{
				pInstance.TransportFee =   Convert.ToInt32(pReader["TransportFee"]);
			}
			if (pReader["ProductFee"] != DBNull.Value)
			{
				pInstance.ProductFee =   Convert.ToInt32(pReader["ProductFee"]);
			}
			if (pReader["Discount"] != DBNull.Value)
			{
				pInstance.Discount =   Convert.ToInt32(pReader["Discount"]);
			}
			if (pReader["BuyerAlias"] != DBNull.Value)
			{
				pInstance.BuyerAlias =  Convert.ToString(pReader["BuyerAlias"]);
			}
			if (pReader["AppId"] != DBNull.Value)
			{
				pInstance.AppId =  Convert.ToString(pReader["AppId"]);
			}
			if (pReader["TimeStamp"] != DBNull.Value)
			{
				pInstance.TimeStamp =   Convert.ToInt32(pReader["TimeStamp"]);
			}
			if (pReader["NonceStr"] != DBNull.Value)
			{
				pInstance.NonceStr =  Convert.ToString(pReader["NonceStr"]);
			}
			if (pReader["OpenId"] != DBNull.Value)
			{
				pInstance.OpenId =  Convert.ToString(pReader["OpenId"]);
			}
			if (pReader["AppSignature"] != DBNull.Value)
			{
				pInstance.AppSignature =  Convert.ToString(pReader["AppSignature"]);
			}
			if (pReader["IsSubscribe"] != DBNull.Value)
			{
				pInstance.IsSubscribe =   Convert.ToInt32(pReader["IsSubscribe"]);
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
