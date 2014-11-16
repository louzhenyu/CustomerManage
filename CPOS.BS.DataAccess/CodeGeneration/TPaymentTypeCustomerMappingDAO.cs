/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/17 9:41:56
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
    /// ��TPaymentTypeCustomerMapping�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class TPaymentTypeCustomerMappingDAO : Base.BaseCPOSDAO, ICRUDable<TPaymentTypeCustomerMappingEntity>, IQueryable<TPaymentTypeCustomerMappingEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public TPaymentTypeCustomerMappingDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(TPaymentTypeCustomerMappingEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(TPaymentTypeCustomerMappingEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [TPaymentTypeCustomerMapping](");
            strSql.Append("[PaymentTypeID],[CustomerId],[IsDelete],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[ChannelId],[APPId],[Token],[Currency],[PayDeplyType],[PayAccountNumber],[PayAccounPublic],[PayPrivate],[EncryptionCertificate],[EncryptionPwd],[DecryptionCertificate],[DecryptionPwd],[AccountIdentity],[PublicKey],[TenPayIdentity],[TenPayKey],[PayEncryptedPwd],[SalesTBAccess],[ApplyMD5Key],[DefaultName],[MappingId])");
            strSql.Append(" values (");
            strSql.Append("@PaymentTypeID,@CustomerId,@IsDelete,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@ChannelId,@APPId,@Token,@Currency,@PayDeplyType,@PayAccountNumber,@PayAccounPublic,@PayPrivate,@EncryptionCertificate,@EncryptionPwd,@DecryptionCertificate,@DecryptionPwd,@AccountIdentity,@PublicKey,@TenPayIdentity,@TenPayKey,@PayEncryptedPwd,@SalesTBAccess,@ApplyMD5Key,@DefaultName,@MappingId)");

            Guid? pkGuid;
            if (pEntity.MappingId == null)
                pkGuid = Guid.NewGuid();
            else
                pkGuid = pEntity.MappingId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@PaymentTypeID",SqlDbType.NVarChar),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@ChannelId",SqlDbType.NVarChar),
					new SqlParameter("@APPId",SqlDbType.NVarChar),
					new SqlParameter("@Token",SqlDbType.NVarChar),
					new SqlParameter("@Currency",SqlDbType.Int),
					new SqlParameter("@PayDeplyType",SqlDbType.Int),
					new SqlParameter("@PayAccountNumber",SqlDbType.NVarChar),
					new SqlParameter("@PayAccounPublic",SqlDbType.NVarChar),
					new SqlParameter("@PayPrivate",SqlDbType.NVarChar),
					new SqlParameter("@EncryptionCertificate",SqlDbType.NVarChar),
					new SqlParameter("@EncryptionPwd",SqlDbType.NVarChar),
					new SqlParameter("@DecryptionCertificate",SqlDbType.NVarChar),
					new SqlParameter("@DecryptionPwd",SqlDbType.NVarChar),
					new SqlParameter("@AccountIdentity",SqlDbType.NVarChar),
					new SqlParameter("@PublicKey",SqlDbType.NVarChar),
					new SqlParameter("@TenPayIdentity",SqlDbType.NVarChar),
					new SqlParameter("@TenPayKey",SqlDbType.NVarChar),
					new SqlParameter("@PayEncryptedPwd",SqlDbType.NVarChar),
					new SqlParameter("@SalesTBAccess",SqlDbType.NVarChar),
					new SqlParameter("@ApplyMD5Key",SqlDbType.NVarChar),
					new SqlParameter("@DefaultName",SqlDbType.NVarChar),
					new SqlParameter("@MappingId",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.PaymentTypeID;
            parameters[1].Value = pEntity.CustomerId;
            parameters[2].Value = pEntity.IsDelete;
            parameters[3].Value = pEntity.CreateTime;
            parameters[4].Value = pEntity.CreateBy;
            parameters[5].Value = pEntity.LastUpdateBy;
            parameters[6].Value = pEntity.LastUpdateTime;
            parameters[7].Value = pEntity.ChannelId;
            parameters[8].Value = pEntity.APPId;
            parameters[9].Value = pEntity.Token;
            parameters[10].Value = pEntity.Currency;
            parameters[11].Value = pEntity.PayDeplyType;
            parameters[12].Value = pEntity.PayAccountNumber;
            parameters[13].Value = pEntity.PayAccounPublic;
            parameters[14].Value = pEntity.PayPrivate;
            parameters[15].Value = pEntity.EncryptionCertificate;
            parameters[16].Value = pEntity.EncryptionPwd;
            parameters[17].Value = pEntity.DecryptionCertificate;
            parameters[18].Value = pEntity.DecryptionPwd;
            parameters[19].Value = pEntity.AccountIdentity;
            parameters[20].Value = pEntity.PublicKey;
            parameters[21].Value = pEntity.TenPayIdentity;
            parameters[22].Value = pEntity.TenPayKey;
            parameters[23].Value = pEntity.PayEncryptedPwd;
            parameters[24].Value = pEntity.SalesTBAccess;
            parameters[25].Value = pEntity.ApplyMD5Key;
            parameters[26].Value = pEntity.DefaultName;
            parameters[27].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            pEntity.MappingId = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public TPaymentTypeCustomerMappingEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [TPaymentTypeCustomerMapping] where MappingId='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            TPaymentTypeCustomerMappingEntity m = null;
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
        public TPaymentTypeCustomerMappingEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [TPaymentTypeCustomerMapping] where isdelete=0");
            //��ȡ����
            List<TPaymentTypeCustomerMappingEntity> list = new List<TPaymentTypeCustomerMappingEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    TPaymentTypeCustomerMappingEntity m;
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
        public void Update(TPaymentTypeCustomerMappingEntity pEntity, IDbTransaction pTran)
        {
            Update(pEntity, true, pTran);
        }
        public void Update(TPaymentTypeCustomerMappingEntity pEntity, bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.MappingId == null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [TPaymentTypeCustomerMapping] set ");
            if (pIsUpdateNullField || pEntity.PaymentTypeID != null)
                strSql.Append("[PaymentTypeID]=@PaymentTypeID,");
            if (pIsUpdateNullField || pEntity.CustomerId != null)
                strSql.Append("[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy != null)
                strSql.Append("[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime != null)
                strSql.Append("[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.ChannelId != null)
                strSql.Append("[ChannelId]=@ChannelId,");
            if (pIsUpdateNullField || pEntity.APPId != null)
                strSql.Append("[APPId]=@APPId,");
            if (pIsUpdateNullField || pEntity.Token != null)
                strSql.Append("[Token]=@Token,");
            if (pIsUpdateNullField || pEntity.Currency != null)
                strSql.Append("[Currency]=@Currency,");
            if (pIsUpdateNullField || pEntity.PayDeplyType != null)
                strSql.Append("[PayDeplyType]=@PayDeplyType,");
            if (pIsUpdateNullField || pEntity.PayAccountNumber != null)
                strSql.Append("[PayAccountNumber]=@PayAccountNumber,");
            if (pIsUpdateNullField || pEntity.PayAccounPublic != null)
                strSql.Append("[PayAccounPublic]=@PayAccounPublic,");
            if (pIsUpdateNullField || pEntity.PayPrivate != null)
                strSql.Append("[PayPrivate]=@PayPrivate,");
            if (pIsUpdateNullField || pEntity.EncryptionCertificate != null)
                strSql.Append("[EncryptionCertificate]=@EncryptionCertificate,");
            if (pIsUpdateNullField || pEntity.EncryptionPwd != null)
                strSql.Append("[EncryptionPwd]=@EncryptionPwd,");
            if (pIsUpdateNullField || pEntity.DecryptionCertificate != null)
                strSql.Append("[DecryptionCertificate]=@DecryptionCertificate,");
            if (pIsUpdateNullField || pEntity.DecryptionPwd != null)
                strSql.Append("[DecryptionPwd]=@DecryptionPwd,");
            if (pIsUpdateNullField || pEntity.AccountIdentity != null)
                strSql.Append("[AccountIdentity]=@AccountIdentity,");
            if (pIsUpdateNullField || pEntity.PublicKey != null)
                strSql.Append("[PublicKey]=@PublicKey,");
            if (pIsUpdateNullField || pEntity.TenPayIdentity != null)
                strSql.Append("[TenPayIdentity]=@TenPayIdentity,");
            if (pIsUpdateNullField || pEntity.TenPayKey != null)
                strSql.Append("[TenPayKey]=@TenPayKey,");
            if (pIsUpdateNullField || pEntity.PayEncryptedPwd != null)
                strSql.Append("[PayEncryptedPwd]=@PayEncryptedPwd,");
            if (pIsUpdateNullField || pEntity.SalesTBAccess != null)
                strSql.Append("[SalesTBAccess]=@SalesTBAccess,");
            if (pIsUpdateNullField || pEntity.ApplyMD5Key != null)
                strSql.Append("[ApplyMD5Key]=@ApplyMD5Key,");
            if (pIsUpdateNullField || pEntity.DefaultName != null)
                strSql.Append("[DefaultName]=@DefaultName");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where MappingId=@MappingId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@PaymentTypeID",SqlDbType.NVarChar),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@ChannelId",SqlDbType.NVarChar),
					new SqlParameter("@APPId",SqlDbType.NVarChar),
					new SqlParameter("@Token",SqlDbType.NVarChar),
					new SqlParameter("@Currency",SqlDbType.Int),
					new SqlParameter("@PayDeplyType",SqlDbType.Int),
					new SqlParameter("@PayAccountNumber",SqlDbType.NVarChar),
					new SqlParameter("@PayAccounPublic",SqlDbType.NVarChar),
					new SqlParameter("@PayPrivate",SqlDbType.NVarChar),
					new SqlParameter("@EncryptionCertificate",SqlDbType.NVarChar),
					new SqlParameter("@EncryptionPwd",SqlDbType.NVarChar),
					new SqlParameter("@DecryptionCertificate",SqlDbType.NVarChar),
					new SqlParameter("@DecryptionPwd",SqlDbType.NVarChar),
					new SqlParameter("@AccountIdentity",SqlDbType.NVarChar),
					new SqlParameter("@PublicKey",SqlDbType.NVarChar),
					new SqlParameter("@TenPayIdentity",SqlDbType.NVarChar),
					new SqlParameter("@TenPayKey",SqlDbType.NVarChar),
					new SqlParameter("@PayEncryptedPwd",SqlDbType.NVarChar),
					new SqlParameter("@SalesTBAccess",SqlDbType.NVarChar),
					new SqlParameter("@ApplyMD5Key",SqlDbType.NVarChar),
					new SqlParameter("@DefaultName",SqlDbType.NVarChar),
					new SqlParameter("@MappingId",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.PaymentTypeID;
            parameters[1].Value = pEntity.CustomerId;
            parameters[2].Value = pEntity.LastUpdateBy;
            parameters[3].Value = pEntity.LastUpdateTime;
            parameters[4].Value = pEntity.ChannelId;
            parameters[5].Value = pEntity.APPId;
            parameters[6].Value = pEntity.Token;
            parameters[7].Value = pEntity.Currency;
            parameters[8].Value = pEntity.PayDeplyType;
            parameters[9].Value = pEntity.PayAccountNumber;
            parameters[10].Value = pEntity.PayAccounPublic;
            parameters[11].Value = pEntity.PayPrivate;
            parameters[12].Value = pEntity.EncryptionCertificate;
            parameters[13].Value = pEntity.EncryptionPwd;
            parameters[14].Value = pEntity.DecryptionCertificate;
            parameters[15].Value = pEntity.DecryptionPwd;
            parameters[16].Value = pEntity.AccountIdentity;
            parameters[17].Value = pEntity.PublicKey;
            parameters[18].Value = pEntity.TenPayIdentity;
            parameters[19].Value = pEntity.TenPayKey;
            parameters[20].Value = pEntity.PayEncryptedPwd;
            parameters[21].Value = pEntity.SalesTBAccess;
            parameters[22].Value = pEntity.ApplyMD5Key;
            parameters[23].Value = pEntity.DefaultName;
            parameters[24].Value = pEntity.MappingId;

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
        public void Update(TPaymentTypeCustomerMappingEntity pEntity)
        {
            Update(pEntity, true);
        }
        public void Update(TPaymentTypeCustomerMappingEntity pEntity, bool pIsUpdateNullField)
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(TPaymentTypeCustomerMappingEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(TPaymentTypeCustomerMappingEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.MappingId == null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.MappingId, pTran);
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
            sql.AppendLine("update [TPaymentTypeCustomerMapping] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where MappingId=@MappingId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@MappingId",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(TPaymentTypeCustomerMappingEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.MappingId == null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.MappingId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(TPaymentTypeCustomerMappingEntity[] pEntities)
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
            sql.AppendLine("update [TPaymentTypeCustomerMapping] set LastUpdateTime='" + DateTime.Now.ToString() + "',LastUpdateBy='" + CurrentUserInfo.UserID + "',IsDelete=1 where MappingId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public TPaymentTypeCustomerMappingEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [TPaymentTypeCustomerMapping] where isdelete=0 ");
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
            List<TPaymentTypeCustomerMappingEntity> list = new List<TPaymentTypeCustomerMappingEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    TPaymentTypeCustomerMappingEntity m;
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
        public PagedQueryResult<TPaymentTypeCustomerMappingEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [MappingId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [TPaymentTypeCustomerMapping] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [TPaymentTypeCustomerMapping] where isdelete=0 ");
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
            PagedQueryResult<TPaymentTypeCustomerMappingEntity> result = new PagedQueryResult<TPaymentTypeCustomerMappingEntity>();
            List<TPaymentTypeCustomerMappingEntity> list = new List<TPaymentTypeCustomerMappingEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    TPaymentTypeCustomerMappingEntity m;
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
        public TPaymentTypeCustomerMappingEntity[] QueryByEntity(TPaymentTypeCustomerMappingEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<TPaymentTypeCustomerMappingEntity> PagedQueryByEntity(TPaymentTypeCustomerMappingEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(TPaymentTypeCustomerMappingEntity pQueryEntity)
        {
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.MappingId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MappingId", Value = pQueryEntity.MappingId });
            if (pQueryEntity.PaymentTypeID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PaymentTypeID", Value = pQueryEntity.PaymentTypeID });
            if (pQueryEntity.CustomerId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
            if (pQueryEntity.IsDelete != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });
            if (pQueryEntity.CreateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.LastUpdateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.LastUpdateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.ChannelId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ChannelId", Value = pQueryEntity.ChannelId });
            if (pQueryEntity.APPId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "APPId", Value = pQueryEntity.APPId });
            if (pQueryEntity.Token != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Token", Value = pQueryEntity.Token });
            if (pQueryEntity.Currency != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Currency", Value = pQueryEntity.Currency });
            if (pQueryEntity.PayDeplyType != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PayDeplyType", Value = pQueryEntity.PayDeplyType });
            if (pQueryEntity.PayAccountNumber != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PayAccountNumber", Value = pQueryEntity.PayAccountNumber });
            if (pQueryEntity.PayAccounPublic != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PayAccounPublic", Value = pQueryEntity.PayAccounPublic });
            if (pQueryEntity.PayPrivate != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PayPrivate", Value = pQueryEntity.PayPrivate });
            if (pQueryEntity.EncryptionCertificate != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EncryptionCertificate", Value = pQueryEntity.EncryptionCertificate });
            if (pQueryEntity.EncryptionPwd != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EncryptionPwd", Value = pQueryEntity.EncryptionPwd });
            if (pQueryEntity.DecryptionCertificate != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DecryptionCertificate", Value = pQueryEntity.DecryptionCertificate });
            if (pQueryEntity.DecryptionPwd != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DecryptionPwd", Value = pQueryEntity.DecryptionPwd });
            if (pQueryEntity.AccountIdentity != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AccountIdentity", Value = pQueryEntity.AccountIdentity });
            if (pQueryEntity.PublicKey != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PublicKey", Value = pQueryEntity.PublicKey });
            if (pQueryEntity.TenPayIdentity != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TenPayIdentity", Value = pQueryEntity.TenPayIdentity });
            if (pQueryEntity.TenPayKey != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TenPayKey", Value = pQueryEntity.TenPayKey });
            if (pQueryEntity.PayEncryptedPwd != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PayEncryptedPwd", Value = pQueryEntity.PayEncryptedPwd });
            if (pQueryEntity.SalesTBAccess != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesTBAccess", Value = pQueryEntity.SalesTBAccess });
            if (pQueryEntity.ApplyMD5Key != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ApplyMD5Key", Value = pQueryEntity.ApplyMD5Key });
            if (pQueryEntity.DefaultName != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DefaultName", Value = pQueryEntity.DefaultName });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out TPaymentTypeCustomerMappingEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new TPaymentTypeCustomerMappingEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["MappingId"] != DBNull.Value)
            {
                pInstance.MappingId = (Guid)pReader["MappingId"];
            }
            if (pReader["PaymentTypeID"] != DBNull.Value)
            {
                pInstance.PaymentTypeID = Convert.ToString(pReader["PaymentTypeID"]);
            }
            if (pReader["CustomerId"] != DBNull.Value)
            {
                pInstance.CustomerId = Convert.ToString(pReader["CustomerId"]);
            }
            if (pReader["IsDelete"] != DBNull.Value)
            {
                pInstance.IsDelete = Convert.ToInt32(pReader["IsDelete"]);
            }
            if (pReader["CreateTime"] != DBNull.Value)
            {
                pInstance.CreateTime = Convert.ToDateTime(pReader["CreateTime"]);
            }
            if (pReader["CreateBy"] != DBNull.Value)
            {
                pInstance.CreateBy = Convert.ToString(pReader["CreateBy"]);
            }
            if (pReader["LastUpdateBy"] != DBNull.Value)
            {
                pInstance.LastUpdateBy = Convert.ToString(pReader["LastUpdateBy"]);
            }
            if (pReader["LastUpdateTime"] != DBNull.Value)
            {
                pInstance.LastUpdateTime = Convert.ToDateTime(pReader["LastUpdateTime"]);
            }
            if (pReader["ChannelId"] != DBNull.Value)
            {
                pInstance.ChannelId = Convert.ToString(pReader["ChannelId"]);
            }
            if (pReader["APPId"] != DBNull.Value)
            {
                pInstance.APPId = Convert.ToString(pReader["APPId"]);
            }
            if (pReader["Token"] != DBNull.Value)
            {
                pInstance.Token = Convert.ToString(pReader["Token"]);
            }
            if (pReader["Currency"] != DBNull.Value)
            {
                pInstance.Currency = Convert.ToInt32(pReader["Currency"]);
            }
            if (pReader["PayDeplyType"] != DBNull.Value)
            {
                pInstance.PayDeplyType = Convert.ToInt32(pReader["PayDeplyType"]);
            }
            if (pReader["PayAccountNumber"] != DBNull.Value)
            {
                pInstance.PayAccountNumber = Convert.ToString(pReader["PayAccountNumber"]);
            }
            if (pReader["PayAccounPublic"] != DBNull.Value)
            {
                pInstance.PayAccounPublic = Convert.ToString(pReader["PayAccounPublic"]);
            }
            if (pReader["PayPrivate"] != DBNull.Value)
            {
                pInstance.PayPrivate = Convert.ToString(pReader["PayPrivate"]);
            }
            if (pReader["EncryptionCertificate"] != DBNull.Value)
            {
                pInstance.EncryptionCertificate = Convert.ToString(pReader["EncryptionCertificate"]);
            }
            if (pReader["EncryptionPwd"] != DBNull.Value)
            {
                pInstance.EncryptionPwd = Convert.ToString(pReader["EncryptionPwd"]);
            }
            if (pReader["DecryptionCertificate"] != DBNull.Value)
            {
                pInstance.DecryptionCertificate = Convert.ToString(pReader["DecryptionCertificate"]);
            }
            if (pReader["DecryptionPwd"] != DBNull.Value)
            {
                pInstance.DecryptionPwd = Convert.ToString(pReader["DecryptionPwd"]);
            }
            if (pReader["AccountIdentity"] != DBNull.Value)
            {
                pInstance.AccountIdentity = Convert.ToString(pReader["AccountIdentity"]);
            }
            if (pReader["PublicKey"] != DBNull.Value)
            {
                pInstance.PublicKey = Convert.ToString(pReader["PublicKey"]);
            }
            if (pReader["TenPayIdentity"] != DBNull.Value)
            {
                pInstance.TenPayIdentity = Convert.ToString(pReader["TenPayIdentity"]);
            }
            if (pReader["TenPayKey"] != DBNull.Value)
            {
                pInstance.TenPayKey = Convert.ToString(pReader["TenPayKey"]);
            }
            if (pReader["PayEncryptedPwd"] != DBNull.Value)
            {
                pInstance.PayEncryptedPwd = Convert.ToString(pReader["PayEncryptedPwd"]);
            }
            if (pReader["SalesTBAccess"] != DBNull.Value)
            {
                pInstance.SalesTBAccess = Convert.ToString(pReader["SalesTBAccess"]);
            }
            if (pReader["ApplyMD5Key"] != DBNull.Value)
            {
                pInstance.ApplyMD5Key = Convert.ToString(pReader["ApplyMD5Key"]);
            }
            if (pReader["DefaultName"] != DBNull.Value)
            {
                pInstance.DefaultName = Convert.ToString(pReader["DefaultName"]);
            }

        }
        #endregion
    }
}
