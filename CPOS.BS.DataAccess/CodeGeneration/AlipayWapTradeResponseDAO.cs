/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013-05-31 20:42
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
    /// ���ݷ��ʣ�  
    /// ��AlipayWapTradeResponse�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class AlipayWapTradeResponseDAO : BaseDAO<BasicUserInfo>, ICRUDable<AlipayWapTradeResponseEntity>, IQueryable<AlipayWapTradeResponseEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public AlipayWapTradeResponseDAO(BasicUserInfo pUserInfo)
            : base(pUserInfo, ConfigInfo.CURRENT_CONNECTION_STRING_MANAGER)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(AlipayWapTradeResponseEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(AlipayWapTradeResponseEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");

            //��ʼ���̶��ֶ�
            pEntity.CreateTime = DateTime.Now;
            pEntity.CreateBy = CurrentUserInfo.UserID;
            pEntity.LastUpdateTime = pEntity.CreateTime;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;
            pEntity.IsDelete = 0;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [AlipayWapTradeResponse](");
            strSql.Append("[order_id],[out_trade_no],[subject],[total_fee],[payment_type],[trade_no],[buyer_email],[gmt_create],[notify_type],[quantity],[notify_time],[seller_id],[trade_status],[is_total_fee_adjust],[gmt_payment],[seller_email],[gmt_close],[price],[buyer_id],[notify_id],[use_coupon],[merchant_url],[call_back_url],[status],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[response_id])");
            strSql.Append(" values (");
            strSql.Append("@OrderID,@OutTradeNo,@Subject,@TotalFee,@PaymentType,@TradeNo,@BuyerEmail,@GmtCreate,@NotifyType,@Quantity,@NotifyTime,@SellerID,@TradeStatus,@IsTotalFeeAdjust,@GmtPayment,@SellerEmail,@GmtClose,@Price,@BuyerID,@NotifyID,@UseCoupon,@MerchantUrl,@CallBackUrl,@Status,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@ResponseID)");

            string pkString = pEntity.ResponseID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@OrderID",SqlDbType.NVarChar),
					new SqlParameter("@OutTradeNo",SqlDbType.NVarChar),
					new SqlParameter("@Subject",SqlDbType.NVarChar),
					new SqlParameter("@TotalFee",SqlDbType.NVarChar),
					new SqlParameter("@PaymentType",SqlDbType.NVarChar),
					new SqlParameter("@TradeNo",SqlDbType.NVarChar),
					new SqlParameter("@BuyerEmail",SqlDbType.NVarChar),
					new SqlParameter("@GmtCreate",SqlDbType.NVarChar),
					new SqlParameter("@NotifyType",SqlDbType.NVarChar),
					new SqlParameter("@Quantity",SqlDbType.NVarChar),
					new SqlParameter("@NotifyTime",SqlDbType.NVarChar),
					new SqlParameter("@SellerID",SqlDbType.NVarChar),
					new SqlParameter("@TradeStatus",SqlDbType.NVarChar),
					new SqlParameter("@IsTotalFeeAdjust",SqlDbType.NVarChar),
					new SqlParameter("@GmtPayment",SqlDbType.NVarChar),
					new SqlParameter("@SellerEmail",SqlDbType.NVarChar),
					new SqlParameter("@GmtClose",SqlDbType.NVarChar),
					new SqlParameter("@Price",SqlDbType.NVarChar),
					new SqlParameter("@BuyerID",SqlDbType.NVarChar),
					new SqlParameter("@NotifyID",SqlDbType.NVarChar),
					new SqlParameter("@UseCoupon",SqlDbType.NVarChar),
					new SqlParameter("@MerchantUrl",SqlDbType.NVarChar),
					new SqlParameter("@CallBackUrl",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@ResponseID",SqlDbType.NVarChar)
            };
            parameters[0].Value = pEntity.OrderID;
            parameters[1].Value = pEntity.OutTradeNo;
            parameters[2].Value = pEntity.Subject;
            parameters[3].Value = pEntity.TotalFee;
            parameters[4].Value = pEntity.PaymentType;
            parameters[5].Value = pEntity.TradeNo;
            parameters[6].Value = pEntity.BuyerEmail;
            parameters[7].Value = pEntity.GmtCreate;
            parameters[8].Value = pEntity.NotifyType;
            parameters[9].Value = pEntity.Quantity;
            parameters[10].Value = pEntity.NotifyTime;
            parameters[11].Value = pEntity.SellerID;
            parameters[12].Value = pEntity.TradeStatus;
            parameters[13].Value = pEntity.IsTotalFeeAdjust;
            parameters[14].Value = pEntity.GmtPayment;
            parameters[15].Value = pEntity.SellerEmail;
            parameters[16].Value = pEntity.GmtClose;
            parameters[17].Value = pEntity.Price;
            parameters[18].Value = pEntity.BuyerID;
            parameters[19].Value = pEntity.NotifyID;
            parameters[20].Value = pEntity.UseCoupon;
            parameters[21].Value = pEntity.MerchantUrl;
            parameters[22].Value = pEntity.CallBackUrl;
            parameters[23].Value = pEntity.Status;
            parameters[24].Value = pEntity.CreateTime;
            parameters[25].Value = pEntity.CreateBy;
            parameters[26].Value = pEntity.LastUpdateTime;
            parameters[27].Value = pEntity.LastUpdateBy;
            parameters[28].Value = pEntity.IsDelete;
            parameters[29].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            pEntity.ResponseID = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public AlipayWapTradeResponseEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [AlipayWapTradeResponse] where response_id='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            AlipayWapTradeResponseEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                    break;
                }
            }
            //����
            return m;
        }

        /// <summary>
        /// ��ȡ����ʵ��
        /// </summary>
        /// <returns></returns>
        public AlipayWapTradeResponseEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [AlipayWapTradeResponse] where isdelete=0");
            //��ȡ����
            List<AlipayWapTradeResponseEntity> list = new List<AlipayWapTradeResponseEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    AlipayWapTradeResponseEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //����
            return list.ToArray();
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(AlipayWapTradeResponseEntity pEntity, IDbTransaction pTran)
        {
            Update(pEntity, true, pTran);
        }
        public void Update(AlipayWapTradeResponseEntity pEntity, bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ResponseID == null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [AlipayWapTradeResponse] set ");
            if (pIsUpdateNullField || pEntity.OrderID != null)
                strSql.Append("[order_id]=@OrderID,");
            if (pIsUpdateNullField || pEntity.OutTradeNo != null)
                strSql.Append("[out_trade_no]=@OutTradeNo,");
            if (pIsUpdateNullField || pEntity.Subject != null)
                strSql.Append("[subject]=@Subject,");
            if (pIsUpdateNullField || pEntity.TotalFee != null)
                strSql.Append("[total_fee]=@TotalFee,");
            if (pIsUpdateNullField || pEntity.PaymentType != null)
                strSql.Append("[payment_type]=@PaymentType,");
            if (pIsUpdateNullField || pEntity.TradeNo != null)
                strSql.Append("[trade_no]=@TradeNo,");
            if (pIsUpdateNullField || pEntity.BuyerEmail != null)
                strSql.Append("[buyer_email]=@BuyerEmail,");
            if (pIsUpdateNullField || pEntity.GmtCreate != null)
                strSql.Append("[gmt_create]=@GmtCreate,");
            if (pIsUpdateNullField || pEntity.NotifyType != null)
                strSql.Append("[notify_type]=@NotifyType,");
            if (pIsUpdateNullField || pEntity.Quantity != null)
                strSql.Append("[quantity]=@Quantity,");
            if (pIsUpdateNullField || pEntity.NotifyTime != null)
                strSql.Append("[notify_time]=@NotifyTime,");
            if (pIsUpdateNullField || pEntity.SellerID != null)
                strSql.Append("[seller_id]=@SellerID,");
            if (pIsUpdateNullField || pEntity.TradeStatus != null)
                strSql.Append("[trade_status]=@TradeStatus,");
            if (pIsUpdateNullField || pEntity.IsTotalFeeAdjust != null)
                strSql.Append("[is_total_fee_adjust]=@IsTotalFeeAdjust,");
            if (pIsUpdateNullField || pEntity.GmtPayment != null)
                strSql.Append("[gmt_payment]=@GmtPayment,");
            if (pIsUpdateNullField || pEntity.SellerEmail != null)
                strSql.Append("[seller_email]=@SellerEmail,");
            if (pIsUpdateNullField || pEntity.GmtClose != null)
                strSql.Append("[gmt_close]=@GmtClose,");
            if (pIsUpdateNullField || pEntity.Price != null)
                strSql.Append("[price]=@Price,");
            if (pIsUpdateNullField || pEntity.BuyerID != null)
                strSql.Append("[buyer_id]=@BuyerID,");
            if (pIsUpdateNullField || pEntity.NotifyID != null)
                strSql.Append("[notify_id]=@NotifyID,");
            if (pIsUpdateNullField || pEntity.UseCoupon != null)
                strSql.Append("[use_coupon]=@UseCoupon,");
            if (pIsUpdateNullField || pEntity.MerchantUrl != null)
                strSql.Append("[merchant_url]=@MerchantUrl,");
            if (pIsUpdateNullField || pEntity.CallBackUrl != null)
                strSql.Append("[call_back_url]=@CallBackUrl,");
            if (pIsUpdateNullField || pEntity.Status != null)
                strSql.Append("[status]=@Status,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime != null)
                strSql.Append("[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy != null)
                strSql.Append("[LastUpdateBy]=@LastUpdateBy");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where response_id=@ResponseID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@OrderID",SqlDbType.NVarChar),
					new SqlParameter("@OutTradeNo",SqlDbType.NVarChar),
					new SqlParameter("@Subject",SqlDbType.NVarChar),
					new SqlParameter("@TotalFee",SqlDbType.NVarChar),
					new SqlParameter("@PaymentType",SqlDbType.NVarChar),
					new SqlParameter("@TradeNo",SqlDbType.NVarChar),
					new SqlParameter("@BuyerEmail",SqlDbType.NVarChar),
					new SqlParameter("@GmtCreate",SqlDbType.NVarChar),
					new SqlParameter("@NotifyType",SqlDbType.NVarChar),
					new SqlParameter("@Quantity",SqlDbType.NVarChar),
					new SqlParameter("@NotifyTime",SqlDbType.NVarChar),
					new SqlParameter("@SellerID",SqlDbType.NVarChar),
					new SqlParameter("@TradeStatus",SqlDbType.NVarChar),
					new SqlParameter("@IsTotalFeeAdjust",SqlDbType.NVarChar),
					new SqlParameter("@GmtPayment",SqlDbType.NVarChar),
					new SqlParameter("@SellerEmail",SqlDbType.NVarChar),
					new SqlParameter("@GmtClose",SqlDbType.NVarChar),
					new SqlParameter("@Price",SqlDbType.NVarChar),
					new SqlParameter("@BuyerID",SqlDbType.NVarChar),
					new SqlParameter("@NotifyID",SqlDbType.NVarChar),
					new SqlParameter("@UseCoupon",SqlDbType.NVarChar),
					new SqlParameter("@MerchantUrl",SqlDbType.NVarChar),
					new SqlParameter("@CallBackUrl",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@ResponseID",SqlDbType.NVarChar)
            };
            parameters[0].Value = pEntity.OrderID;
            parameters[1].Value = pEntity.OutTradeNo;
            parameters[2].Value = pEntity.Subject;
            parameters[3].Value = pEntity.TotalFee;
            parameters[4].Value = pEntity.PaymentType;
            parameters[5].Value = pEntity.TradeNo;
            parameters[6].Value = pEntity.BuyerEmail;
            parameters[7].Value = pEntity.GmtCreate;
            parameters[8].Value = pEntity.NotifyType;
            parameters[9].Value = pEntity.Quantity;
            parameters[10].Value = pEntity.NotifyTime;
            parameters[11].Value = pEntity.SellerID;
            parameters[12].Value = pEntity.TradeStatus;
            parameters[13].Value = pEntity.IsTotalFeeAdjust;
            parameters[14].Value = pEntity.GmtPayment;
            parameters[15].Value = pEntity.SellerEmail;
            parameters[16].Value = pEntity.GmtClose;
            parameters[17].Value = pEntity.Price;
            parameters[18].Value = pEntity.BuyerID;
            parameters[19].Value = pEntity.NotifyID;
            parameters[20].Value = pEntity.UseCoupon;
            parameters[21].Value = pEntity.MerchantUrl;
            parameters[22].Value = pEntity.CallBackUrl;
            parameters[23].Value = pEntity.Status;
            parameters[24].Value = pEntity.LastUpdateTime;
            parameters[25].Value = pEntity.LastUpdateBy;
            parameters[26].Value = pEntity.ResponseID;

            //ִ�����
            int result = 0;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Update(AlipayWapTradeResponseEntity pEntity)
        {
            Update(pEntity, true);
        }
        public void Update(AlipayWapTradeResponseEntity pEntity, bool pIsUpdateNullField)
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(AlipayWapTradeResponseEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(AlipayWapTradeResponseEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ResponseID == null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.ResponseID, pTran);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(object pID, IDbTransaction pTran)
        {
            if (pID == null)
                return;
            //��֯������SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [AlipayWapTradeResponse] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where response_id=@ResponseID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@ResponseID",SqlDbType=SqlDbType.VarChar,Value=pID}
            };
            //ִ�����
            int result = 0;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), parameters);
            return;
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(AlipayWapTradeResponseEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.ResponseID == null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.ResponseID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(AlipayWapTradeResponseEntity[] pEntities)
        {
            Delete(pEntities, null);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pIDs">��ʶ��ֵ����</param>
        public void Delete(object[] pIDs)
        {
            Delete(pIDs, null);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pIDs">��ʶ��ֵ����</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(object[] pIDs, IDbTransaction pTran)
        {
            if (pIDs == null || pIDs.Length == 0)
                return;
            //��֯������SQL
            StringBuilder primaryKeys = new StringBuilder();
            foreach (object item in pIDs)
            {
                primaryKeys.AppendFormat("'{0}',", item.ToString());
            }
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [AlipayWapTradeResponse] set LastUpdateTime='" + DateTime.Now.ToString() + "',LastUpdateBy=" + CurrentUserInfo.UserID + ",IsDelete=1 where response_id in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
            //ִ�����
            int result = 0;
            if (pTran == null)
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), null);
            else
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString());
        }
        #endregion

        #region IQueryable ��Ա
        /// <summary>
        /// ִ�в�ѯ
        /// </summary>
        /// <param name="pWhereConditions">ɸѡ����</param>
        /// <param name="pOrderBys">����</param>
        /// <returns></returns>
        public AlipayWapTradeResponseEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [AlipayWapTradeResponse] where isdelete=0 ");
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
            //ִ��SQL
            List<AlipayWapTradeResponseEntity> list = new List<AlipayWapTradeResponseEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    AlipayWapTradeResponseEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //���ؽ��
            return list.ToArray();
        }
        /// <summary>
        /// ִ�з�ҳ��ѯ
        /// </summary>
        /// <param name="pWhereConditions">ɸѡ����</param>
        /// <param name="pOrderBys">����</param>
        /// <param name="pPageSize">ÿҳ�ļ�¼��</param>
        /// <param name="pCurrentPageIndex">��0��ʼ�ĵ�ǰҳ��</param>
        /// <returns></returns>
        public PagedQueryResult<AlipayWapTradeResponseEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            //��֯SQL
            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();
            //��ҳSQL
            pagedSql.AppendFormat("select * from (select row_number()over( order by ");
            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                foreach (var item in pOrderBys)
                {
                    if (item != null)
                    {
                        pagedSql.AppendFormat(" {0} {1},", StringUtils.WrapperSQLServerObject(item.FieldName), item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                    }
                }
                pagedSql.Remove(pagedSql.Length - 1, 1);
            }
            else
            {
                pagedSql.AppendFormat(" [ResponseID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [AlipayWapTradeResponse] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [AlipayWapTradeResponse] where isdelete=0 ");
            //��������
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    if (item != null)
                    {
                        pagedSql.AppendFormat(" and {0}", item.GetExpression());
                        totalCountSql.AppendFormat(" and {0}", item.GetExpression());
                    }
                }
            }
            pagedSql.AppendFormat(") as A ");
            //ȡָ��ҳ������
            pagedSql.AppendFormat(" where ___rn >{0} and ___rn <={1}", pPageSize * (pCurrentPageIndex - 1), pPageSize * (pCurrentPageIndex));
            //ִ����䲢���ؽ��
            PagedQueryResult<AlipayWapTradeResponseEntity> result = new PagedQueryResult<AlipayWapTradeResponseEntity>();
            List<AlipayWapTradeResponseEntity> list = new List<AlipayWapTradeResponseEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    AlipayWapTradeResponseEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            result.Entities = list.ToArray();
            int totalCount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(totalCountSql.ToString()));    //����������
            result.RowCount = totalCount;
            int remainder = 0;
            result.PageCount = Math.DivRem(totalCount, pPageSize, out remainder);
            if (remainder > 0)
                result.PageCount++;
            return result;
        }

        /// <summary>
        /// ����ʵ��������ѯʵ��
        /// </summary>
        /// <param name="pQueryEntity">��ʵ����ʽ����Ĳ���</param>
        /// <param name="pOrderBys">�������</param>
        /// <returns>����������ʵ�弯</returns>
        public AlipayWapTradeResponseEntity[] QueryByEntity(AlipayWapTradeResponseEntity pQueryEntity, OrderBy[] pOrderBys)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity(pQueryEntity);
            return Query(queryWhereCondition, pOrderBys);
        }

        /// <summary>
        /// ��ҳ����ʵ��������ѯʵ��
        /// </summary>
        /// <param name="pQueryEntity">��ʵ����ʽ����Ĳ���</param>
        /// <param name="pOrderBys">�������</param>
        /// <returns>����������ʵ�弯</returns>
        public PagedQueryResult<AlipayWapTradeResponseEntity> PagedQueryByEntity(AlipayWapTradeResponseEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity(pQueryEntity);
            return PagedQuery(queryWhereCondition, pOrderBys, pPageSize, pCurrentPageIndex);
        }

        #endregion

        #region ���߷���
        /// <summary>
        /// ����ʵ���Null�������ɲ�ѯ������
        /// </summary>
        /// <returns></returns>
        protected IWhereCondition[] GetWhereConditionByEntity(AlipayWapTradeResponseEntity pQueryEntity)
        {
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.ResponseID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ResponseID", Value = pQueryEntity.ResponseID });
            if (pQueryEntity.OrderID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderID", Value = pQueryEntity.OrderID });
            if (pQueryEntity.OutTradeNo != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OutTradeNo", Value = pQueryEntity.OutTradeNo });
            if (pQueryEntity.Subject != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Subject", Value = pQueryEntity.Subject });
            if (pQueryEntity.TotalFee != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TotalFee", Value = pQueryEntity.TotalFee });
            if (pQueryEntity.PaymentType != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PaymentType", Value = pQueryEntity.PaymentType });
            if (pQueryEntity.TradeNo != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TradeNo", Value = pQueryEntity.TradeNo });
            if (pQueryEntity.BuyerEmail != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BuyerEmail", Value = pQueryEntity.BuyerEmail });
            if (pQueryEntity.GmtCreate != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "GmtCreate", Value = pQueryEntity.GmtCreate });
            if (pQueryEntity.NotifyType != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NotifyType", Value = pQueryEntity.NotifyType });
            if (pQueryEntity.Quantity != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Quantity", Value = pQueryEntity.Quantity });
            if (pQueryEntity.NotifyTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NotifyTime", Value = pQueryEntity.NotifyTime });
            if (pQueryEntity.SellerID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SellerID", Value = pQueryEntity.SellerID });
            if (pQueryEntity.TradeStatus != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TradeStatus", Value = pQueryEntity.TradeStatus });
            if (pQueryEntity.IsTotalFeeAdjust != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsTotalFeeAdjust", Value = pQueryEntity.IsTotalFeeAdjust });
            if (pQueryEntity.GmtPayment != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "GmtPayment", Value = pQueryEntity.GmtPayment });
            if (pQueryEntity.SellerEmail != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SellerEmail", Value = pQueryEntity.SellerEmail });
            if (pQueryEntity.GmtClose != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "GmtClose", Value = pQueryEntity.GmtClose });
            if (pQueryEntity.Price != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Price", Value = pQueryEntity.Price });
            if (pQueryEntity.BuyerID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BuyerID", Value = pQueryEntity.BuyerID });
            if (pQueryEntity.NotifyID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NotifyID", Value = pQueryEntity.NotifyID });
            if (pQueryEntity.UseCoupon != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UseCoupon", Value = pQueryEntity.UseCoupon });
            if (pQueryEntity.MerchantUrl != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MerchantUrl", Value = pQueryEntity.MerchantUrl });
            if (pQueryEntity.CallBackUrl != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CallBackUrl", Value = pQueryEntity.CallBackUrl });
            if (pQueryEntity.Status != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Status", Value = pQueryEntity.Status });
            if (pQueryEntity.CreateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.LastUpdateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.LastUpdateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.IsDelete != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out AlipayWapTradeResponseEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new AlipayWapTradeResponseEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["response_id"] != DBNull.Value)
            {
                pInstance.ResponseID = Convert.ToString(pReader["response_id"]);
            }
            if (pReader["order_id"] != DBNull.Value)
            {
                pInstance.OrderID = Convert.ToString(pReader["order_id"]);
            }
            if (pReader["out_trade_no"] != DBNull.Value)
            {
                pInstance.OutTradeNo = Convert.ToString(pReader["out_trade_no"]);
            }
            if (pReader["subject"] != DBNull.Value)
            {
                pInstance.Subject = Convert.ToString(pReader["subject"]);
            }
            if (pReader["total_fee"] != DBNull.Value)
            {
                pInstance.TotalFee = Convert.ToString(pReader["total_fee"]);
            }
            if (pReader["payment_type"] != DBNull.Value)
            {
                pInstance.PaymentType = Convert.ToString(pReader["payment_type"]);
            }
            if (pReader["trade_no"] != DBNull.Value)
            {
                pInstance.TradeNo = Convert.ToString(pReader["trade_no"]);
            }
            if (pReader["buyer_email"] != DBNull.Value)
            {
                pInstance.BuyerEmail = Convert.ToString(pReader["buyer_email"]);
            }
            if (pReader["gmt_create"] != DBNull.Value)
            {
                pInstance.GmtCreate = Convert.ToString(pReader["gmt_create"]);
            }
            if (pReader["notify_type"] != DBNull.Value)
            {
                pInstance.NotifyType = Convert.ToString(pReader["notify_type"]);
            }
            if (pReader["quantity"] != DBNull.Value)
            {
                pInstance.Quantity = Convert.ToString(pReader["quantity"]);
            }
            if (pReader["notify_time"] != DBNull.Value)
            {
                pInstance.NotifyTime = Convert.ToString(pReader["notify_time"]);
            }
            if (pReader["seller_id"] != DBNull.Value)
            {
                pInstance.SellerID = Convert.ToString(pReader["seller_id"]);
            }
            if (pReader["trade_status"] != DBNull.Value)
            {
                pInstance.TradeStatus = Convert.ToString(pReader["trade_status"]);
            }
            if (pReader["is_total_fee_adjust"] != DBNull.Value)
            {
                pInstance.IsTotalFeeAdjust = Convert.ToString(pReader["is_total_fee_adjust"]);
            }
            if (pReader["gmt_payment"] != DBNull.Value)
            {
                pInstance.GmtPayment = Convert.ToString(pReader["gmt_payment"]);
            }
            if (pReader["seller_email"] != DBNull.Value)
            {
                pInstance.SellerEmail = Convert.ToString(pReader["seller_email"]);
            }
            if (pReader["gmt_close"] != DBNull.Value)
            {
                pInstance.GmtClose = Convert.ToString(pReader["gmt_close"]);
            }
            if (pReader["price"] != DBNull.Value)
            {
                pInstance.Price = Convert.ToString(pReader["price"]);
            }
            if (pReader["buyer_id"] != DBNull.Value)
            {
                pInstance.BuyerID = Convert.ToString(pReader["buyer_id"]);
            }
            if (pReader["notify_id"] != DBNull.Value)
            {
                pInstance.NotifyID = Convert.ToString(pReader["notify_id"]);
            }
            if (pReader["use_coupon"] != DBNull.Value)
            {
                pInstance.UseCoupon = Convert.ToString(pReader["use_coupon"]);
            }
            if (pReader["merchant_url"] != DBNull.Value)
            {
                pInstance.MerchantUrl = Convert.ToString(pReader["merchant_url"]);
            }
            if (pReader["call_back_url"] != DBNull.Value)
            {
                pInstance.CallBackUrl = Convert.ToString(pReader["call_back_url"]);
            }
            if (pReader["status"] != DBNull.Value)
            {
                pInstance.Status = Convert.ToString(pReader["status"]);
            }
            if (pReader["CreateTime"] != DBNull.Value)
            {
                pInstance.CreateTime = Convert.ToDateTime(pReader["CreateTime"]);
            }
            if (pReader["CreateBy"] != DBNull.Value)
            {
                pInstance.CreateBy = Convert.ToString(pReader["CreateBy"]);
            }
            if (pReader["LastUpdateTime"] != DBNull.Value)
            {
                pInstance.LastUpdateTime = Convert.ToDateTime(pReader["LastUpdateTime"]);
            }
            if (pReader["LastUpdateBy"] != DBNull.Value)
            {
                pInstance.LastUpdateBy = Convert.ToString(pReader["LastUpdateBy"]);
            }
            if (pReader["IsDelete"] != DBNull.Value)
            {
                pInstance.IsDelete = Convert.ToInt32(pReader["IsDelete"]);
            }

        }
        #endregion
    }
}
