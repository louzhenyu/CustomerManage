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
    /// ���ݷ��ʣ�  
    /// ��CustomerBack�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class CustomerBackDAO : BaseCPOSDAO, ICRUDable<CustomerBackEntity>, IQueryable<CustomerBackEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public CustomerBackDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(CustomerBackEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(CustomerBackEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [CustomerBack](");
            strSql.Append("[CustomerName],[ReceivingBank],[OpenBank],[BankAccount],[ChannelId],[WithdrawalPassword],[MD5Pwd],[BackStatus],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[CustomerId],[CustomerBackId])");
            strSql.Append(" values (");
            strSql.Append("@CustomerName,@ReceivingBank,@OpenBank,@BankAccount,@ChannelId,@WithdrawalPassword,@MD5Pwd,@BackStatus,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@CustomerId,@CustomerBackId)");            

			Guid? pkGuid;
			if (pEntity.CustomerBackId == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.CustomerBackId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@CustomerName",SqlDbType.NVarChar),
					new SqlParameter("@ReceivingBank",SqlDbType.NVarChar),
					new SqlParameter("@OpenBank",SqlDbType.NVarChar),
					new SqlParameter("@BankAccount",SqlDbType.NVarChar),
					new SqlParameter("@ChannelId",SqlDbType.Int),
					new SqlParameter("@WithdrawalPassword",SqlDbType.NVarChar),
					new SqlParameter("@MD5Pwd",SqlDbType.NVarChar),
					new SqlParameter("@BackStatus",SqlDbType.Int),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@CustomerBackId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.CustomerName;
			parameters[1].Value = pEntity.ReceivingBank;
			parameters[2].Value = pEntity.OpenBank;
			parameters[3].Value = pEntity.BankAccount;
			parameters[4].Value = pEntity.ChannelId;
			parameters[5].Value = pEntity.WithdrawalPassword;
			parameters[6].Value = pEntity.MD5Pwd;
			parameters[7].Value = pEntity.BackStatus;
			parameters[8].Value = pEntity.CreateBy;
			parameters[9].Value = pEntity.CreateTime;
			parameters[10].Value = pEntity.LastUpdateBy;
			parameters[11].Value = pEntity.LastUpdateTime;
			parameters[12].Value = pEntity.IsDelete;
			parameters[13].Value = pEntity.CustomerId;
			parameters[14].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.CustomerBackId = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public CustomerBackEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [CustomerBack] where CustomerBackId='{0}'  and isdelete=0 ", id.ToString());
            //��ȡ����
            CustomerBackEntity m = null;
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
        public CustomerBackEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [CustomerBack] where 1=1  and isdelete=0");
            //��ȡ����
            List<CustomerBackEntity> list = new List<CustomerBackEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    CustomerBackEntity m;
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
        public void Update(CustomerBackEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(CustomerBackEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.CustomerBackId.HasValue)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update cpos_ap.dbo.[CustomerBack] set ");
                        if (pIsUpdateNullField || pEntity.CustomerName!=null)
                strSql.Append( "[CustomerName]=@CustomerName,");
            if (pIsUpdateNullField || pEntity.ReceivingBank!=null)
                strSql.Append( "[ReceivingBank]=@ReceivingBank,");
            if (pIsUpdateNullField || pEntity.OpenBank!=null)
                strSql.Append( "[OpenBank]=@OpenBank,");
            if (pIsUpdateNullField || pEntity.BankAccount!=null)
                strSql.Append( "[BankAccount]=@BankAccount,");
            if (pIsUpdateNullField || pEntity.ChannelId!=null)
                strSql.Append( "[ChannelId]=@ChannelId,");
            if (pIsUpdateNullField || pEntity.WithdrawalPassword!=null)
                strSql.Append( "[WithdrawalPassword]=@WithdrawalPassword,");
            if (pIsUpdateNullField || pEntity.MD5Pwd!=null)
                strSql.Append( "[MD5Pwd]=@MD5Pwd,");
            if (pIsUpdateNullField || pEntity.BackStatus!=null)
                strSql.Append( "[BackStatus]=@BackStatus,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId");
            strSql.Append(" where CustomerBackId=@CustomerBackId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@CustomerName",SqlDbType.NVarChar),
					new SqlParameter("@ReceivingBank",SqlDbType.NVarChar),
					new SqlParameter("@OpenBank",SqlDbType.NVarChar),
					new SqlParameter("@BankAccount",SqlDbType.NVarChar),
					new SqlParameter("@ChannelId",SqlDbType.Int),
					new SqlParameter("@WithdrawalPassword",SqlDbType.NVarChar),
					new SqlParameter("@MD5Pwd",SqlDbType.NVarChar),
					new SqlParameter("@BackStatus",SqlDbType.Int),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@CustomerBackId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.CustomerName;
			parameters[1].Value = pEntity.ReceivingBank;
			parameters[2].Value = pEntity.OpenBank;
			parameters[3].Value = pEntity.BankAccount;
			parameters[4].Value = pEntity.ChannelId;
			parameters[5].Value = pEntity.WithdrawalPassword;
			parameters[6].Value = pEntity.MD5Pwd;
			parameters[7].Value = pEntity.BackStatus;
			parameters[8].Value = pEntity.LastUpdateBy;
			parameters[9].Value = pEntity.LastUpdateTime;
			parameters[10].Value = pEntity.CustomerId;
			parameters[11].Value = pEntity.CustomerBackId;

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
        public void Update(CustomerBackEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(CustomerBackEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(CustomerBackEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.CustomerBackId.HasValue)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.CustomerBackId.Value, pTran);           
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
            sql.AppendLine("update [CustomerBack] set  isdelete=1 where CustomerBackId=@CustomerBackId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@CustomerBackId",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(CustomerBackEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.CustomerBackId.HasValue)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.CustomerBackId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(CustomerBackEntity[] pEntities)
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
            sql.AppendLine("update [CustomerBack] set  isdelete=1 where CustomerBackId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public CustomerBackEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from cpos_ap.dbo.[CustomerBack] where 1=1  and isdelete=0 ");
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
            List<CustomerBackEntity> list = new List<CustomerBackEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    CustomerBackEntity m;
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
        public PagedQueryResult<CustomerBackEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [CustomerBackId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [CustomerBack] where 1=1  and isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [CustomerBack] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<CustomerBackEntity> result = new PagedQueryResult<CustomerBackEntity>();
            List<CustomerBackEntity> list = new List<CustomerBackEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    CustomerBackEntity m;
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
        public CustomerBackEntity[] QueryByEntity(CustomerBackEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<CustomerBackEntity> PagedQueryByEntity(CustomerBackEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(CustomerBackEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.CustomerBackId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerBackId", Value = pQueryEntity.CustomerBackId });
            if (pQueryEntity.CustomerName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerName", Value = pQueryEntity.CustomerName });
            if (pQueryEntity.ReceivingBank!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReceivingBank", Value = pQueryEntity.ReceivingBank });
            if (pQueryEntity.OpenBank!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OpenBank", Value = pQueryEntity.OpenBank });
            if (pQueryEntity.BankAccount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BankAccount", Value = pQueryEntity.BankAccount });
            if (pQueryEntity.ChannelId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ChannelId", Value = pQueryEntity.ChannelId });
            if (pQueryEntity.WithdrawalPassword!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WithdrawalPassword", Value = pQueryEntity.WithdrawalPassword });
            if (pQueryEntity.MD5Pwd!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MD5Pwd", Value = pQueryEntity.MD5Pwd });
            if (pQueryEntity.BackStatus!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BackStatus", Value = pQueryEntity.BackStatus });
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
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(IDataReader pReader, out CustomerBackEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new CustomerBackEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["CustomerBackId"] != DBNull.Value)
			{
				pInstance.CustomerBackId =  (Guid)pReader["CustomerBackId"];
			}
			if (pReader["CustomerName"] != DBNull.Value)
			{
				pInstance.CustomerName =  Convert.ToString(pReader["CustomerName"]);
			}
			if (pReader["ReceivingBank"] != DBNull.Value)
			{
				pInstance.ReceivingBank =  Convert.ToString(pReader["ReceivingBank"]);
			}
			if (pReader["OpenBank"] != DBNull.Value)
			{
				pInstance.OpenBank =  Convert.ToString(pReader["OpenBank"]);
			}
			if (pReader["BankAccount"] != DBNull.Value)
			{
				pInstance.BankAccount =  Convert.ToString(pReader["BankAccount"]);
			}
			if (pReader["ChannelId"] != DBNull.Value)
			{
				pInstance.ChannelId =   Convert.ToInt32(pReader["ChannelId"]);
			}
			if (pReader["WithdrawalPassword"] != DBNull.Value)
			{
				pInstance.WithdrawalPassword =  Convert.ToString(pReader["WithdrawalPassword"]);
			}
			if (pReader["MD5Pwd"] != DBNull.Value)
			{
				pInstance.MD5Pwd =  Convert.ToString(pReader["MD5Pwd"]);
			}
			if (pReader["BackStatus"] != DBNull.Value)
			{
				pInstance.BackStatus =   Convert.ToInt32(pReader["BackStatus"]);
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
