/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/7/13 18:00:26
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
    /// ���ݷ��ʣ� 10��֧���ɹ�   90��֧��ʧ�� 
    /// ��ReceiveAmountOrder�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class ReceiveAmountOrderDAO : BaseCPOSDAO, ICRUDable<ReceiveAmountOrderEntity>, IQueryable<ReceiveAmountOrderEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public ReceiveAmountOrderDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(ReceiveAmountOrderEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(ReceiveAmountOrderEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //��ʼ���̶��ֶ�
			pEntity.IsDelete=0;
			pEntity.CreateTime=DateTime.Now;
			pEntity.LastUpdateTime=pEntity.CreateTime;
			pEntity.CreateBy=CurrentUserInfo.UserID;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [ReceiveAmountOrder](");
            strSql.Append("[OrderNo],[VipId],[VipCardId],[VipCardNo],[VipCardTypeId],[ServiceUnitId],[ServiceUserId],[TotalAmount],[VipDiscount],[TransAmount],[ReturnAmount],[PayPoints],[AmountFromPayPoints],[ReturnPoints],[CouponUsePay],[AmountAcctPay],[PayTypeId],[PayStatus],[PayDatetTime],[TimeStamp],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[CustomerId],[OrderId])");
            strSql.Append(" values (");
            strSql.Append("@OrderNo,@VipId,@VipCardId,@VipCardNo,@VipCardTypeId,@ServiceUnitId,@ServiceUserId,@TotalAmount,@VipDiscount,@TransAmount,@ReturnAmount,@PayPoints,@AmountFromPayPoints,@ReturnPoints,@CouponUsePay,@AmountAcctPay,@PayTypeId,@PayStatus,@PayDatetTime,@TimeStamp,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@CustomerId,@OrderId)");            

			Guid? pkGuid;
			if (pEntity.OrderId == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.OrderId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@OrderNo",SqlDbType.VarChar),
					new SqlParameter("@VipId",SqlDbType.NVarChar),
					new SqlParameter("@VipCardId",SqlDbType.NVarChar),
					new SqlParameter("@VipCardNo",SqlDbType.VarChar),
					new SqlParameter("@VipCardTypeId",SqlDbType.Int),
					new SqlParameter("@ServiceUnitId",SqlDbType.NVarChar),
					new SqlParameter("@ServiceUserId",SqlDbType.NVarChar),
					new SqlParameter("@TotalAmount",SqlDbType.Decimal),
					new SqlParameter("@VipDiscount",SqlDbType.Decimal),
					new SqlParameter("@TransAmount",SqlDbType.Decimal),
					new SqlParameter("@ReturnAmount",SqlDbType.Decimal),
					new SqlParameter("@PayPoints",SqlDbType.Decimal),
					new SqlParameter("@AmountFromPayPoints",SqlDbType.Decimal),
					new SqlParameter("@ReturnPoints",SqlDbType.Decimal),
					new SqlParameter("@CouponUsePay",SqlDbType.Decimal),
					new SqlParameter("@AmountAcctPay",SqlDbType.Decimal),
					new SqlParameter("@PayTypeId",SqlDbType.NVarChar),
					new SqlParameter("@PayStatus",SqlDbType.VarChar),
					new SqlParameter("@PayDatetTime",SqlDbType.DateTime),
					new SqlParameter("@TimeStamp",SqlDbType.VarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@OrderId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.OrderNo;
			parameters[1].Value = pEntity.VipId;
			parameters[2].Value = pEntity.VipCardId;
			parameters[3].Value = pEntity.VipCardNo;
			parameters[4].Value = pEntity.VipCardTypeId;
			parameters[5].Value = pEntity.ServiceUnitId;
			parameters[6].Value = pEntity.ServiceUserId;
			parameters[7].Value = pEntity.TotalAmount;
			parameters[8].Value = pEntity.VipDiscount;
			parameters[9].Value = pEntity.TransAmount;
			parameters[10].Value = pEntity.ReturnAmount;
			parameters[11].Value = pEntity.PayPoints;
			parameters[12].Value = pEntity.AmountFromPayPoints;
			parameters[13].Value = pEntity.ReturnPoints;
			parameters[14].Value = pEntity.CouponUsePay;
			parameters[15].Value = pEntity.AmountAcctPay;
			parameters[16].Value = pEntity.PayTypeId;
			parameters[17].Value = pEntity.PayStatus;
			parameters[18].Value = pEntity.PayDatetTime;
			parameters[19].Value = pEntity.TimeStamp;
			parameters[20].Value = pEntity.CreateTime;
			parameters[21].Value = pEntity.CreateBy;
			parameters[22].Value = pEntity.LastUpdateTime;
			parameters[23].Value = pEntity.LastUpdateBy;
			parameters[24].Value = pEntity.IsDelete;
			parameters[25].Value = pEntity.CustomerId;
			parameters[26].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.OrderId = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public ReceiveAmountOrderEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ReceiveAmountOrder] where OrderId='{0}'  and isdelete=0 ", id.ToString());
            //��ȡ����
            ReceiveAmountOrderEntity m = null;
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
        public ReceiveAmountOrderEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ReceiveAmountOrder] where 1=1  and isdelete=0");
            //��ȡ����
            List<ReceiveAmountOrderEntity> list = new List<ReceiveAmountOrderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    ReceiveAmountOrderEntity m;
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
        public void Update(ReceiveAmountOrderEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(ReceiveAmountOrderEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.OrderId.HasValue)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [ReceiveAmountOrder] set ");
                        if (pIsUpdateNullField || pEntity.OrderNo!=null)
                strSql.Append( "[OrderNo]=@OrderNo,");
            if (pIsUpdateNullField || pEntity.VipId!=null)
                strSql.Append( "[VipId]=@VipId,");
            if (pIsUpdateNullField || pEntity.VipCardId!=null)
                strSql.Append( "[VipCardId]=@VipCardId,");
            if (pIsUpdateNullField || pEntity.VipCardNo!=null)
                strSql.Append( "[VipCardNo]=@VipCardNo,");
            if (pIsUpdateNullField || pEntity.VipCardTypeId!=null)
                strSql.Append( "[VipCardTypeId]=@VipCardTypeId,");
            if (pIsUpdateNullField || pEntity.ServiceUnitId!=null)
                strSql.Append( "[ServiceUnitId]=@ServiceUnitId,");
            if (pIsUpdateNullField || pEntity.ServiceUserId!=null)
                strSql.Append( "[ServiceUserId]=@ServiceUserId,");
            if (pIsUpdateNullField || pEntity.TotalAmount!=null)
                strSql.Append( "[TotalAmount]=@TotalAmount,");
            if (pIsUpdateNullField || pEntity.VipDiscount!=null)
                strSql.Append( "[VipDiscount]=@VipDiscount,");
            if (pIsUpdateNullField || pEntity.TransAmount!=null)
                strSql.Append( "[TransAmount]=@TransAmount,");
            if (pIsUpdateNullField || pEntity.ReturnAmount!=null)
                strSql.Append( "[ReturnAmount]=@ReturnAmount,");
            if (pIsUpdateNullField || pEntity.PayPoints!=null)
                strSql.Append( "[PayPoints]=@PayPoints,");
            if (pIsUpdateNullField || pEntity.AmountFromPayPoints!=null)
                strSql.Append( "[AmountFromPayPoints]=@AmountFromPayPoints,");
            if (pIsUpdateNullField || pEntity.ReturnPoints!=null)
                strSql.Append( "[ReturnPoints]=@ReturnPoints,");
            if (pIsUpdateNullField || pEntity.CouponUsePay!=null)
                strSql.Append( "[CouponUsePay]=@CouponUsePay,");
            if (pIsUpdateNullField || pEntity.AmountAcctPay!=null)
                strSql.Append( "[AmountAcctPay]=@AmountAcctPay,");
            if (pIsUpdateNullField || pEntity.PayTypeId!=null)
                strSql.Append( "[PayTypeId]=@PayTypeId,");
            if (pIsUpdateNullField || pEntity.PayStatus!=null)
                strSql.Append( "[PayStatus]=@PayStatus,");
            if (pIsUpdateNullField || pEntity.PayDatetTime!=null)
                strSql.Append( "[PayDatetTime]=@PayDatetTime,");
            if (pIsUpdateNullField || pEntity.TimeStamp!=null)
                strSql.Append( "[TimeStamp]=@TimeStamp,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where OrderId=@OrderId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@OrderNo",SqlDbType.VarChar),
					new SqlParameter("@VipId",SqlDbType.NVarChar),
					new SqlParameter("@VipCardId",SqlDbType.NVarChar),
					new SqlParameter("@VipCardNo",SqlDbType.VarChar),
					new SqlParameter("@VipCardTypeId",SqlDbType.Int),
					new SqlParameter("@ServiceUnitId",SqlDbType.NVarChar),
					new SqlParameter("@ServiceUserId",SqlDbType.NVarChar),
					new SqlParameter("@TotalAmount",SqlDbType.Decimal),
					new SqlParameter("@VipDiscount",SqlDbType.Decimal),
					new SqlParameter("@TransAmount",SqlDbType.Decimal),
					new SqlParameter("@ReturnAmount",SqlDbType.Decimal),
					new SqlParameter("@PayPoints",SqlDbType.Decimal),
					new SqlParameter("@AmountFromPayPoints",SqlDbType.Decimal),
					new SqlParameter("@ReturnPoints",SqlDbType.Decimal),
					new SqlParameter("@CouponUsePay",SqlDbType.Decimal),
					new SqlParameter("@AmountAcctPay",SqlDbType.Decimal),
					new SqlParameter("@PayTypeId",SqlDbType.NVarChar),
					new SqlParameter("@PayStatus",SqlDbType.VarChar),
					new SqlParameter("@PayDatetTime",SqlDbType.DateTime),
					new SqlParameter("@TimeStamp",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@OrderId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.OrderNo;
			parameters[1].Value = pEntity.VipId;
			parameters[2].Value = pEntity.VipCardId;
			parameters[3].Value = pEntity.VipCardNo;
			parameters[4].Value = pEntity.VipCardTypeId;
			parameters[5].Value = pEntity.ServiceUnitId;
			parameters[6].Value = pEntity.ServiceUserId;
			parameters[7].Value = pEntity.TotalAmount;
			parameters[8].Value = pEntity.VipDiscount;
			parameters[9].Value = pEntity.TransAmount;
			parameters[10].Value = pEntity.ReturnAmount;
			parameters[11].Value = pEntity.PayPoints;
			parameters[12].Value = pEntity.AmountFromPayPoints;
			parameters[13].Value = pEntity.ReturnPoints;
			parameters[14].Value = pEntity.CouponUsePay;
			parameters[15].Value = pEntity.AmountAcctPay;
			parameters[16].Value = pEntity.PayTypeId;
			parameters[17].Value = pEntity.PayStatus;
			parameters[18].Value = pEntity.PayDatetTime;
			parameters[19].Value = pEntity.TimeStamp;
			parameters[20].Value = pEntity.LastUpdateTime;
			parameters[21].Value = pEntity.LastUpdateBy;
			parameters[22].Value = pEntity.CustomerId;
			parameters[23].Value = pEntity.OrderId;

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
        public void Update(ReceiveAmountOrderEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(ReceiveAmountOrderEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(ReceiveAmountOrderEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.OrderId.HasValue)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.OrderId.Value, pTran);           
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(object pID, IDbTransaction pTran)
        {
            if (pID == null)
                return ;   
            //��֯������SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [ReceiveAmountOrder] set  isdelete=1 where OrderId=@OrderId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@OrderId",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
            };
            //ִ�����
            int result = 0;
            if (pTran != null)
                result=this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), parameters);
            else
                result=this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), parameters);
            return ;
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(ReceiveAmountOrderEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.OrderId.HasValue)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.OrderId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(ReceiveAmountOrderEntity[] pEntities)
        { 
            Delete(pEntities, null);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pIDs">��ʶ��ֵ����</param>
        public void Delete(object[] pIDs)
        {
            Delete(pIDs,null);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pIDs">��ʶ��ֵ����</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(object[] pIDs, IDbTransaction pTran) 
        {
            if (pIDs == null || pIDs.Length==0)
                return ;
            //��֯������SQL
            StringBuilder primaryKeys = new StringBuilder();
            foreach (object item in pIDs)
            {
                primaryKeys.AppendFormat("'{0}',",item.ToString());
            }
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [ReceiveAmountOrder] set  isdelete=1 where OrderId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
            //ִ�����
            int result = 0;   
            if (pTran == null)
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), null);
            else
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran,CommandType.Text, sql.ToString());       
        }
        #endregion

        #region IQueryable ��Ա
        /// <summary>
        /// ִ�в�ѯ
        /// </summary>
        /// <param name="pWhereConditions">ɸѡ����</param>
        /// <param name="pOrderBys">����</param>
        /// <returns></returns>
        public ReceiveAmountOrderEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ReceiveAmountOrder] where 1=1  and isdelete=0 ");
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
            List<ReceiveAmountOrderEntity> list = new List<ReceiveAmountOrderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    ReceiveAmountOrderEntity m;
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
        public PagedQueryResult<ReceiveAmountOrderEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                    if(item!=null)
                    {
                        pagedSql.AppendFormat(" {0} {1},", StringUtils.WrapperSQLServerObject(item.FieldName), item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                    }
                }
                pagedSql.Remove(pagedSql.Length - 1, 1);
            }
            else
            {
                pagedSql.AppendFormat(" [OrderId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [ReceiveAmountOrder] where 1=1  and isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [ReceiveAmountOrder] where 1=1  and isdelete=0 ");
            //��������
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
            //ȡָ��ҳ������
            pagedSql.AppendFormat(" where ___rn >{0} and ___rn <={1}", pPageSize * (pCurrentPageIndex-1), pPageSize * (pCurrentPageIndex));
            //ִ����䲢���ؽ��
            PagedQueryResult<ReceiveAmountOrderEntity> result = new PagedQueryResult<ReceiveAmountOrderEntity>();
            List<ReceiveAmountOrderEntity> list = new List<ReceiveAmountOrderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    ReceiveAmountOrderEntity m;
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
        public ReceiveAmountOrderEntity[] QueryByEntity(ReceiveAmountOrderEntity pQueryEntity, OrderBy[] pOrderBys)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity(pQueryEntity);
            return Query(queryWhereCondition,  pOrderBys);            
        }

        /// <summary>
        /// ��ҳ����ʵ��������ѯʵ��
        /// </summary>
        /// <param name="pQueryEntity">��ʵ����ʽ����Ĳ���</param>
        /// <param name="pOrderBys">�������</param>
        /// <returns>����������ʵ�弯</returns>
        public PagedQueryResult<ReceiveAmountOrderEntity> PagedQueryByEntity(ReceiveAmountOrderEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity( pQueryEntity);
            return PagedQuery(queryWhereCondition, pOrderBys, pPageSize, pCurrentPageIndex);
        }

        #endregion

        #region ���߷���
        /// <summary>
        /// ����ʵ���Null�������ɲ�ѯ������
        /// </summary>
        /// <returns></returns>
        protected IWhereCondition[] GetWhereConditionByEntity(ReceiveAmountOrderEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.OrderId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderId", Value = pQueryEntity.OrderId });
            if (pQueryEntity.OrderNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderNo", Value = pQueryEntity.OrderNo });
            if (pQueryEntity.VipId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipId", Value = pQueryEntity.VipId });
            if (pQueryEntity.VipCardId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardId", Value = pQueryEntity.VipCardId });
            if (pQueryEntity.VipCardNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardNo", Value = pQueryEntity.VipCardNo });
            if (pQueryEntity.VipCardTypeId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardTypeId", Value = pQueryEntity.VipCardTypeId });
            if (pQueryEntity.ServiceUnitId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ServiceUnitId", Value = pQueryEntity.ServiceUnitId });
            if (pQueryEntity.ServiceUserId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ServiceUserId", Value = pQueryEntity.ServiceUserId });
            if (pQueryEntity.TotalAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TotalAmount", Value = pQueryEntity.TotalAmount });
            if (pQueryEntity.VipDiscount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipDiscount", Value = pQueryEntity.VipDiscount });
            if (pQueryEntity.TransAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TransAmount", Value = pQueryEntity.TransAmount });
            if (pQueryEntity.ReturnAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReturnAmount", Value = pQueryEntity.ReturnAmount });
            if (pQueryEntity.PayPoints!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PayPoints", Value = pQueryEntity.PayPoints });
            if (pQueryEntity.AmountFromPayPoints!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AmountFromPayPoints", Value = pQueryEntity.AmountFromPayPoints });
            if (pQueryEntity.ReturnPoints!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReturnPoints", Value = pQueryEntity.ReturnPoints });
            if (pQueryEntity.CouponUsePay!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CouponUsePay", Value = pQueryEntity.CouponUsePay });
            if (pQueryEntity.AmountAcctPay!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AmountAcctPay", Value = pQueryEntity.AmountAcctPay });
            if (pQueryEntity.PayTypeId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PayTypeId", Value = pQueryEntity.PayTypeId });
            if (pQueryEntity.PayStatus!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PayStatus", Value = pQueryEntity.PayStatus });
            if (pQueryEntity.PayDatetTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PayDatetTime", Value = pQueryEntity.PayDatetTime });
            if (pQueryEntity.TimeStamp!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TimeStamp", Value = pQueryEntity.TimeStamp });
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
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(IDataReader pReader, out ReceiveAmountOrderEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new ReceiveAmountOrderEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["OrderId"] != DBNull.Value)
			{
				pInstance.OrderId =  (Guid)pReader["OrderId"];
			}
			if (pReader["OrderNo"] != DBNull.Value)
			{
				pInstance.OrderNo =  Convert.ToString(pReader["OrderNo"]);
			}
			if (pReader["VipId"] != DBNull.Value)
			{
				pInstance.VipId =  Convert.ToString(pReader["VipId"]);
			}
			if (pReader["VipCardId"] != DBNull.Value)
			{
				pInstance.VipCardId =  Convert.ToString(pReader["VipCardId"]);
			}
			if (pReader["VipCardNo"] != DBNull.Value)
			{
				pInstance.VipCardNo =  Convert.ToString(pReader["VipCardNo"]);
			}
			if (pReader["VipCardTypeId"] != DBNull.Value)
			{
				pInstance.VipCardTypeId =   Convert.ToInt32(pReader["VipCardTypeId"]);
			}
			if (pReader["ServiceUnitId"] != DBNull.Value)
			{
				pInstance.ServiceUnitId =  Convert.ToString(pReader["ServiceUnitId"]);
			}
			if (pReader["ServiceUserId"] != DBNull.Value)
			{
				pInstance.ServiceUserId =  Convert.ToString(pReader["ServiceUserId"]);
			}
			if (pReader["TotalAmount"] != DBNull.Value)
			{
				pInstance.TotalAmount =  Convert.ToDecimal(pReader["TotalAmount"]);
			}
			if (pReader["VipDiscount"] != DBNull.Value)
			{
				pInstance.VipDiscount =  Convert.ToDecimal(pReader["VipDiscount"]);
			}
			if (pReader["TransAmount"] != DBNull.Value)
			{
				pInstance.TransAmount =  Convert.ToDecimal(pReader["TransAmount"]);
			}
			if (pReader["ReturnAmount"] != DBNull.Value)
			{
				pInstance.ReturnAmount =  Convert.ToDecimal(pReader["ReturnAmount"]);
			}
			if (pReader["PayPoints"] != DBNull.Value)
			{
				pInstance.PayPoints =  Convert.ToDecimal(pReader["PayPoints"]);
			}
			if (pReader["AmountFromPayPoints"] != DBNull.Value)
			{
				pInstance.AmountFromPayPoints =  Convert.ToDecimal(pReader["AmountFromPayPoints"]);
			}
			if (pReader["ReturnPoints"] != DBNull.Value)
			{
				pInstance.ReturnPoints =  Convert.ToDecimal(pReader["ReturnPoints"]);
			}
			if (pReader["CouponUsePay"] != DBNull.Value)
			{
				pInstance.CouponUsePay =  Convert.ToDecimal(pReader["CouponUsePay"]);
			}
			if (pReader["AmountAcctPay"] != DBNull.Value)
			{
				pInstance.AmountAcctPay =  Convert.ToDecimal(pReader["AmountAcctPay"]);
			}
			if (pReader["PayTypeId"] != DBNull.Value)
			{
				pInstance.PayTypeId =  Convert.ToString(pReader["PayTypeId"]);
			}
			if (pReader["PayStatus"] != DBNull.Value)
			{
				pInstance.PayStatus =  Convert.ToString(pReader["PayStatus"]);
			}
			if (pReader["PayDatetTime"] != DBNull.Value)
			{
				pInstance.PayDatetTime =  Convert.ToDateTime(pReader["PayDatetTime"]);
			}
			if (pReader["TimeStamp"] != DBNull.Value)
			{
				pInstance.TimeStamp =  Convert.ToString(pReader["TimeStamp"]);
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
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}

        }
        #endregion
    }
}
