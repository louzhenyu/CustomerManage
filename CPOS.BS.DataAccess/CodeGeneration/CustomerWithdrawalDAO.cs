/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/18 10:21:46
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
    /// ��CustomerWithdrawal�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class CustomerWithdrawalDAO : BaseCPOSDAO, ICRUDable<CustomerWithdrawalEntity>, IQueryable<CustomerWithdrawalEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public CustomerWithdrawalDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(CustomerWithdrawalEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(CustomerWithdrawalEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [CustomerWithdrawal](");
            strSql.Append("[CustomerBackId],[SerialNo],[WithdrawalAmount],[WithdrawalBy],[Remark],[WithdrawalTime],[WithdrawalStatus],[PlayMoneyTime],[FailureReason],[PlotId],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[CustomerId],[WithdrawalId])");
            strSql.Append(" values (");
            strSql.Append("@CustomerBackId,@SerialNo,@WithdrawalAmount,@WithdrawalBy,@Remark,@WithdrawalTime,@WithdrawalStatus,@PlayMoneyTime,@FailureReason,@PlotId,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@CustomerId,@WithdrawalId)");            

			Guid? pkGuid;
			if (pEntity.WithdrawalId == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.WithdrawalId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@CustomerBackId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@SerialNo",SqlDbType.NVarChar),
					new SqlParameter("@WithdrawalAmount",SqlDbType.Decimal),
					new SqlParameter("@WithdrawalBy",SqlDbType.NVarChar),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@WithdrawalTime",SqlDbType.DateTime),
					new SqlParameter("@WithdrawalStatus",SqlDbType.Int),
					new SqlParameter("@PlayMoneyTime",SqlDbType.DateTime),
					new SqlParameter("@FailureReason",SqlDbType.NVarChar),
					new SqlParameter("@PlotId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@WithdrawalId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.CustomerBackId;
			parameters[1].Value = pEntity.SerialNo;
			parameters[2].Value = pEntity.WithdrawalAmount;
			parameters[3].Value = pEntity.WithdrawalBy;
			parameters[4].Value = pEntity.Remark;
			parameters[5].Value = pEntity.WithdrawalTime;
			parameters[6].Value = pEntity.WithdrawalStatus;
			parameters[7].Value = pEntity.PlayMoneyTime;
			parameters[8].Value = pEntity.FailureReason;
			parameters[9].Value = pEntity.PlotId;
			parameters[10].Value = pEntity.CreateBy;
			parameters[11].Value = pEntity.CreateTime;
			parameters[12].Value = pEntity.LastUpdateBy;
			parameters[13].Value = pEntity.LastUpdateTime;
			parameters[14].Value = pEntity.IsDelete;
			parameters[15].Value = pEntity.CustomerId;
			parameters[16].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.WithdrawalId = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public CustomerWithdrawalEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [CustomerWithdrawal] where WithdrawalId='{0}'  and isdelete=0 ", id.ToString());
            //��ȡ����
            CustomerWithdrawalEntity m = null;
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
        public CustomerWithdrawalEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [CustomerWithdrawal] where 1=1  and isdelete=0");
            //��ȡ����
            List<CustomerWithdrawalEntity> list = new List<CustomerWithdrawalEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    CustomerWithdrawalEntity m;
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
        public void Update(CustomerWithdrawalEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(CustomerWithdrawalEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.WithdrawalId.HasValue)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [CustomerWithdrawal] set ");
                        if (pIsUpdateNullField || pEntity.CustomerBackId!=null)
                strSql.Append( "[CustomerBackId]=@CustomerBackId,");
            if (pIsUpdateNullField || pEntity.SerialNo!=null)
                strSql.Append( "[SerialNo]=@SerialNo,");
            if (pIsUpdateNullField || pEntity.WithdrawalAmount!=null)
                strSql.Append( "[WithdrawalAmount]=@WithdrawalAmount,");
            if (pIsUpdateNullField || pEntity.WithdrawalBy!=null)
                strSql.Append( "[WithdrawalBy]=@WithdrawalBy,");
            if (pIsUpdateNullField || pEntity.Remark!=null)
                strSql.Append( "[Remark]=@Remark,");
            if (pIsUpdateNullField || pEntity.WithdrawalTime!=null)
                strSql.Append( "[WithdrawalTime]=@WithdrawalTime,");
            if (pIsUpdateNullField || pEntity.WithdrawalStatus!=null)
                strSql.Append( "[WithdrawalStatus]=@WithdrawalStatus,");
            if (pIsUpdateNullField || pEntity.PlayMoneyTime!=null)
                strSql.Append( "[PlayMoneyTime]=@PlayMoneyTime,");
            if (pIsUpdateNullField || pEntity.FailureReason!=null)
                strSql.Append( "[FailureReason]=@FailureReason,");
            if (pIsUpdateNullField || pEntity.PlotId!=null)
                strSql.Append( "[PlotId]=@PlotId,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId");
            strSql.Append(" where WithdrawalId=@WithdrawalId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@CustomerBackId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@SerialNo",SqlDbType.NVarChar),
					new SqlParameter("@WithdrawalAmount",SqlDbType.Decimal),
					new SqlParameter("@WithdrawalBy",SqlDbType.NVarChar),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@WithdrawalTime",SqlDbType.DateTime),
					new SqlParameter("@WithdrawalStatus",SqlDbType.Int),
					new SqlParameter("@PlayMoneyTime",SqlDbType.DateTime),
					new SqlParameter("@FailureReason",SqlDbType.NVarChar),
					new SqlParameter("@PlotId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@WithdrawalId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.CustomerBackId;
			parameters[1].Value = pEntity.SerialNo;
			parameters[2].Value = pEntity.WithdrawalAmount;
			parameters[3].Value = pEntity.WithdrawalBy;
			parameters[4].Value = pEntity.Remark;
			parameters[5].Value = pEntity.WithdrawalTime;
			parameters[6].Value = pEntity.WithdrawalStatus;
			parameters[7].Value = pEntity.PlayMoneyTime;
			parameters[8].Value = pEntity.FailureReason;
			parameters[9].Value = pEntity.PlotId;
			parameters[10].Value = pEntity.LastUpdateBy;
			parameters[11].Value = pEntity.LastUpdateTime;
			parameters[12].Value = pEntity.CustomerId;
			parameters[13].Value = pEntity.WithdrawalId;

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
        public void Update(CustomerWithdrawalEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(CustomerWithdrawalEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(CustomerWithdrawalEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.WithdrawalId.HasValue)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.WithdrawalId.Value, pTran);           
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
            sql.AppendLine("update [CustomerWithdrawal] set  isdelete=1 where WithdrawalId=@WithdrawalId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@WithdrawalId",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(CustomerWithdrawalEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.WithdrawalId.HasValue)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.WithdrawalId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(CustomerWithdrawalEntity[] pEntities)
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
            sql.AppendLine("update [CustomerWithdrawal] set  isdelete=1 where WithdrawalId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public CustomerWithdrawalEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [CustomerWithdrawal] where 1=1  and isdelete=0 ");
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
            List<CustomerWithdrawalEntity> list = new List<CustomerWithdrawalEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    CustomerWithdrawalEntity m;
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
        public PagedQueryResult<CustomerWithdrawalEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [WithdrawalId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [CustomerWithdrawal] where 1=1  and isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [CustomerWithdrawal] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<CustomerWithdrawalEntity> result = new PagedQueryResult<CustomerWithdrawalEntity>();
            List<CustomerWithdrawalEntity> list = new List<CustomerWithdrawalEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    CustomerWithdrawalEntity m;
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
        public CustomerWithdrawalEntity[] QueryByEntity(CustomerWithdrawalEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<CustomerWithdrawalEntity> PagedQueryByEntity(CustomerWithdrawalEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(CustomerWithdrawalEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.WithdrawalId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WithdrawalId", Value = pQueryEntity.WithdrawalId });
            if (pQueryEntity.CustomerBackId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerBackId", Value = pQueryEntity.CustomerBackId });
            if (pQueryEntity.SerialNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SerialNo", Value = pQueryEntity.SerialNo });
            if (pQueryEntity.WithdrawalAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WithdrawalAmount", Value = pQueryEntity.WithdrawalAmount });
            if (pQueryEntity.WithdrawalBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WithdrawalBy", Value = pQueryEntity.WithdrawalBy });
            if (pQueryEntity.Remark!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Remark", Value = pQueryEntity.Remark });
            if (pQueryEntity.WithdrawalTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WithdrawalTime", Value = pQueryEntity.WithdrawalTime });
            if (pQueryEntity.WithdrawalStatus!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WithdrawalStatus", Value = pQueryEntity.WithdrawalStatus });
            if (pQueryEntity.PlayMoneyTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PlayMoneyTime", Value = pQueryEntity.PlayMoneyTime });
            if (pQueryEntity.FailureReason!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FailureReason", Value = pQueryEntity.FailureReason });
            if (pQueryEntity.PlotId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PlotId", Value = pQueryEntity.PlotId });
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
        protected void Load(IDataReader pReader, out CustomerWithdrawalEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new CustomerWithdrawalEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["WithdrawalId"] != DBNull.Value)
			{
				pInstance.WithdrawalId =  (Guid)pReader["WithdrawalId"];
			}
			if (pReader["CustomerBackId"] != DBNull.Value)
			{
				pInstance.CustomerBackId =  (Guid)pReader["CustomerBackId"];
			}
			if (pReader["SerialNo"] != DBNull.Value)
			{
				pInstance.SerialNo =  Convert.ToString(pReader["SerialNo"]);
			}
			if (pReader["WithdrawalAmount"] != DBNull.Value)
			{
				pInstance.WithdrawalAmount =  Convert.ToDecimal(pReader["WithdrawalAmount"]);
			}
			if (pReader["WithdrawalBy"] != DBNull.Value)
			{
				pInstance.WithdrawalBy =  Convert.ToString(pReader["WithdrawalBy"]);
			}
			if (pReader["Remark"] != DBNull.Value)
			{
				pInstance.Remark =  Convert.ToString(pReader["Remark"]);
			}
			if (pReader["WithdrawalTime"] != DBNull.Value)
			{
				pInstance.WithdrawalTime =  Convert.ToDateTime(pReader["WithdrawalTime"]);
			}
			if (pReader["WithdrawalStatus"] != DBNull.Value)
			{
				pInstance.WithdrawalStatus =   Convert.ToInt32(pReader["WithdrawalStatus"]);
			}
			if (pReader["PlayMoneyTime"] != DBNull.Value)
			{
				pInstance.PlayMoneyTime =  Convert.ToDateTime(pReader["PlayMoneyTime"]);
			}
			if (pReader["FailureReason"] != DBNull.Value)
			{
				pInstance.FailureReason =  Convert.ToString(pReader["FailureReason"]);
			}
			if (pReader["PlotId"] != DBNull.Value)
			{
				pInstance.PlotId =  (Guid)pReader["PlotId"];
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
