/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/9 15:19:33
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
    /// ��CardDeposit�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class CardDepositDAO : Base.BaseCPOSDAO, ICRUDable<CardDepositEntity>, IQueryable<CardDepositEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public CardDepositDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(CardDepositEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(CardDepositEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [CardDeposit](");
            strSql.Append("[CardNo],[CardPassword],[SerialNo],[VerifyCode],[CustomerId],[BatchId],[UnitId],[ChannelId],[DepositTime],[Amount],[Bonus],[ConsumedAmount],[VipId],[CouponQty],[CardStatus],[UseStatus],[IsDelete],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[CardDepositId])");
            strSql.Append(" values (");
            strSql.Append("@CardNo,@CardPassword,@SerialNo,@VerifyCode,@CustomerId,@BatchId,@UnitId,@ChannelId,@DepositTime,@Amount,@Bonus,@ConsumedAmount,@VipId,@CouponQty,@CardStatus,@UseStatus,@IsDelete,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@CardDepositId)");            

			Guid? pkGuid;
			if (pEntity.CardDepositId == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.CardDepositId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@CardNo",SqlDbType.NVarChar),
					new SqlParameter("@CardPassword",SqlDbType.VarChar),
					new SqlParameter("@SerialNo",SqlDbType.NVarChar),
					new SqlParameter("@VerifyCode",SqlDbType.NVarChar),
					new SqlParameter("@CustomerId",SqlDbType.VarChar),
					new SqlParameter("@BatchId",SqlDbType.VarChar),
					new SqlParameter("@UnitId",SqlDbType.VarChar),
					new SqlParameter("@ChannelId",SqlDbType.VarChar),
					new SqlParameter("@DepositTime",SqlDbType.DateTime),
					new SqlParameter("@Amount",SqlDbType.Decimal),
					new SqlParameter("@Bonus",SqlDbType.Decimal),
					new SqlParameter("@ConsumedAmount",SqlDbType.Decimal),
					new SqlParameter("@VipId",SqlDbType.VarChar),
					new SqlParameter("@CouponQty",SqlDbType.Int),
					new SqlParameter("@CardStatus",SqlDbType.Int),
					new SqlParameter("@UseStatus",SqlDbType.Int),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CardDepositId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.CardNo;
			parameters[1].Value = pEntity.CardPassword;
			parameters[2].Value = pEntity.SerialNo;
			parameters[3].Value = pEntity.VerifyCode;
			parameters[4].Value = pEntity.CustomerId;
			parameters[5].Value = pEntity.BatchId;
			parameters[6].Value = pEntity.UnitId;
			parameters[7].Value = pEntity.ChannelId;
			parameters[8].Value = pEntity.DepositTime;
			parameters[9].Value = pEntity.Amount;
			parameters[10].Value = pEntity.Bonus;
			parameters[11].Value = pEntity.ConsumedAmount;
			parameters[12].Value = pEntity.VipId;
			parameters[13].Value = pEntity.CouponQty;
			parameters[14].Value = pEntity.CardStatus;
			parameters[15].Value = pEntity.UseStatus;
			parameters[16].Value = pEntity.IsDelete;
			parameters[17].Value = pEntity.CreateBy;
			parameters[18].Value = pEntity.CreateTime;
			parameters[19].Value = pEntity.LastUpdateBy;
			parameters[20].Value = pEntity.LastUpdateTime;
			parameters[21].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.CardDepositId = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public CardDepositEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [CardDeposit] where CardDepositId='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            CardDepositEntity m = null;
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
        public CardDepositEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [CardDeposit] where isdelete=0");
            //��ȡ����
            List<CardDepositEntity> list = new List<CardDepositEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    CardDepositEntity m;
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
        public void Update(CardDepositEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(CardDepositEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.CardDepositId==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [CardDeposit] set ");
            if (pIsUpdateNullField || pEntity.CardNo!=null)
                strSql.Append( "[CardNo]=@CardNo,");
            if (pIsUpdateNullField || pEntity.CardPassword!=null)
                strSql.Append( "[CardPassword]=@CardPassword,");
            if (pIsUpdateNullField || pEntity.SerialNo!=null)
                strSql.Append( "[SerialNo]=@SerialNo,");
            if (pIsUpdateNullField || pEntity.VerifyCode!=null)
                strSql.Append( "[VerifyCode]=@VerifyCode,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.BatchId!=null)
                strSql.Append( "[BatchId]=@BatchId,");
            if (pIsUpdateNullField || pEntity.UnitId!=null)
                strSql.Append( "[UnitId]=@UnitId,");
            if (pIsUpdateNullField || pEntity.ChannelId!=null)
                strSql.Append( "[ChannelId]=@ChannelId,");
            if (pIsUpdateNullField || pEntity.DepositTime!=null)
                strSql.Append( "[DepositTime]=@DepositTime,");
            if (pIsUpdateNullField || pEntity.Amount!=null)
                strSql.Append( "[Amount]=@Amount,");
            if (pIsUpdateNullField || pEntity.Bonus!=null)
                strSql.Append( "[Bonus]=@Bonus,");
            if (pIsUpdateNullField || pEntity.ConsumedAmount!=null)
                strSql.Append( "[ConsumedAmount]=@ConsumedAmount,");
            if (pIsUpdateNullField || pEntity.VipId!=null)
                strSql.Append( "[VipId]=@VipId,");
            if (pIsUpdateNullField || pEntity.CouponQty!=null)
                strSql.Append( "[CouponQty]=@CouponQty,");
            if (pIsUpdateNullField || pEntity.CardStatus!=null)
                strSql.Append( "[CardStatus]=@CardStatus,");
            if (pIsUpdateNullField || pEntity.UseStatus!=null)
                strSql.Append( "[UseStatus]=@UseStatus,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where CardDepositId=@CardDepositId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@CardNo",SqlDbType.NVarChar),
					new SqlParameter("@CardPassword",SqlDbType.VarChar),
					new SqlParameter("@SerialNo",SqlDbType.NVarChar),
					new SqlParameter("@VerifyCode",SqlDbType.NVarChar),
					new SqlParameter("@CustomerId",SqlDbType.VarChar),
					new SqlParameter("@BatchId",SqlDbType.VarChar),
					new SqlParameter("@UnitId",SqlDbType.VarChar),
					new SqlParameter("@ChannelId",SqlDbType.VarChar),
					new SqlParameter("@DepositTime",SqlDbType.DateTime),
					new SqlParameter("@Amount",SqlDbType.Decimal),
					new SqlParameter("@Bonus",SqlDbType.Decimal),
					new SqlParameter("@ConsumedAmount",SqlDbType.Decimal),
					new SqlParameter("@VipId",SqlDbType.VarChar),
					new SqlParameter("@CouponQty",SqlDbType.Int),
					new SqlParameter("@CardStatus",SqlDbType.Int),
					new SqlParameter("@UseStatus",SqlDbType.Int),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CardDepositId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.CardNo;
			parameters[1].Value = pEntity.CardPassword;
			parameters[2].Value = pEntity.SerialNo;
			parameters[3].Value = pEntity.VerifyCode;
			parameters[4].Value = pEntity.CustomerId;
			parameters[5].Value = pEntity.BatchId;
			parameters[6].Value = pEntity.UnitId;
			parameters[7].Value = pEntity.ChannelId;
			parameters[8].Value = pEntity.DepositTime;
			parameters[9].Value = pEntity.Amount;
			parameters[10].Value = pEntity.Bonus;
			parameters[11].Value = pEntity.ConsumedAmount;
			parameters[12].Value = pEntity.VipId;
			parameters[13].Value = pEntity.CouponQty;
			parameters[14].Value = pEntity.CardStatus;
			parameters[15].Value = pEntity.UseStatus;
			parameters[16].Value = pEntity.LastUpdateBy;
			parameters[17].Value = pEntity.LastUpdateTime;
			parameters[18].Value = pEntity.CardDepositId;

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
        public void Update(CardDepositEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(CardDepositEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(CardDepositEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(CardDepositEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.CardDepositId==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.CardDepositId, pTran);           
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
            sql.AppendLine("update [CardDeposit] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where CardDepositId=@CardDepositId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@CardDepositId",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(CardDepositEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.CardDepositId==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.CardDepositId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(CardDepositEntity[] pEntities)
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
            sql.AppendLine("update [CardDeposit] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where CardDepositId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public CardDepositEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [CardDeposit] where isdelete=0 ");
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
            List<CardDepositEntity> list = new List<CardDepositEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    CardDepositEntity m;
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
        public PagedQueryResult<CardDepositEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [CardDepositId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [CardDeposit] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [CardDeposit] where isdelete=0 ");
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
            PagedQueryResult<CardDepositEntity> result = new PagedQueryResult<CardDepositEntity>();
            List<CardDepositEntity> list = new List<CardDepositEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    CardDepositEntity m;
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
        public CardDepositEntity[] QueryByEntity(CardDepositEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<CardDepositEntity> PagedQueryByEntity(CardDepositEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(CardDepositEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.CardDepositId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CardDepositId", Value = pQueryEntity.CardDepositId });
            if (pQueryEntity.CardNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CardNo", Value = pQueryEntity.CardNo });
            if (pQueryEntity.CardPassword!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CardPassword", Value = pQueryEntity.CardPassword });
            if (pQueryEntity.SerialNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SerialNo", Value = pQueryEntity.SerialNo });
            if (pQueryEntity.VerifyCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VerifyCode", Value = pQueryEntity.VerifyCode });
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
            if (pQueryEntity.BatchId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BatchId", Value = pQueryEntity.BatchId });
            if (pQueryEntity.UnitId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitId", Value = pQueryEntity.UnitId });
            if (pQueryEntity.ChannelId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ChannelId", Value = pQueryEntity.ChannelId });
            if (pQueryEntity.DepositTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DepositTime", Value = pQueryEntity.DepositTime });
            if (pQueryEntity.Amount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Amount", Value = pQueryEntity.Amount });
            if (pQueryEntity.Bonus!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Bonus", Value = pQueryEntity.Bonus });
            if (pQueryEntity.ConsumedAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ConsumedAmount", Value = pQueryEntity.ConsumedAmount });
            if (pQueryEntity.VipId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipId", Value = pQueryEntity.VipId });
            if (pQueryEntity.CouponQty!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CouponQty", Value = pQueryEntity.CouponQty });
            if (pQueryEntity.CardStatus!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CardStatus", Value = pQueryEntity.CardStatus });
            if (pQueryEntity.UseStatus!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UseStatus", Value = pQueryEntity.UseStatus });
            if (pQueryEntity.IsDelete!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.LastUpdateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.LastUpdateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out CardDepositEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new CardDepositEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["CardDepositId"] != DBNull.Value)
			{
				pInstance.CardDepositId =  (Guid)pReader["CardDepositId"];
			}
			if (pReader["CardNo"] != DBNull.Value)
			{
				pInstance.CardNo =  Convert.ToString(pReader["CardNo"]);
			}
			if (pReader["CardPassword"] != DBNull.Value)
			{
				pInstance.CardPassword =  Convert.ToString(pReader["CardPassword"]);
			}
			if (pReader["SerialNo"] != DBNull.Value)
			{
				pInstance.SerialNo =  Convert.ToString(pReader["SerialNo"]);
			}
			if (pReader["VerifyCode"] != DBNull.Value)
			{
				pInstance.VerifyCode =  Convert.ToString(pReader["VerifyCode"]);
			}
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}
			if (pReader["BatchId"] != DBNull.Value)
			{
				pInstance.BatchId =  Convert.ToString(pReader["BatchId"]);
			}
			if (pReader["UnitId"] != DBNull.Value)
			{
				pInstance.UnitId =  Convert.ToString(pReader["UnitId"]);
			}
			if (pReader["ChannelId"] != DBNull.Value)
			{
				pInstance.ChannelId =  Convert.ToString(pReader["ChannelId"]);
			}
			if (pReader["DepositTime"] != DBNull.Value)
			{
				pInstance.DepositTime =  Convert.ToDateTime(pReader["DepositTime"]);
			}
			if (pReader["Amount"] != DBNull.Value)
			{
				pInstance.Amount =  Convert.ToDecimal(pReader["Amount"]);
			}
			if (pReader["Bonus"] != DBNull.Value)
			{
				pInstance.Bonus =  Convert.ToDecimal(pReader["Bonus"]);
			}
			if (pReader["ConsumedAmount"] != DBNull.Value)
			{
				pInstance.ConsumedAmount =  Convert.ToDecimal(pReader["ConsumedAmount"]);
			}
			if (pReader["VipId"] != DBNull.Value)
			{
				pInstance.VipId =  Convert.ToString(pReader["VipId"]);
			}
			if (pReader["CouponQty"] != DBNull.Value)
			{
				pInstance.CouponQty =   Convert.ToInt32(pReader["CouponQty"]);
			}
			if (pReader["CardStatus"] != DBNull.Value)
			{
				pInstance.CardStatus =   Convert.ToInt32(pReader["CardStatus"]);
			}
			if (pReader["UseStatus"] != DBNull.Value)
			{
				pInstance.UseStatus =   Convert.ToInt32(pReader["UseStatus"]);
			}
			if (pReader["IsDelete"] != DBNull.Value)
			{
				pInstance.IsDelete =   Convert.ToInt32(pReader["IsDelete"]);
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

        }
        #endregion
    }
}
