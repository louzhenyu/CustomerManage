/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/18 10:10:21
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
    /// ��CustomerOrderPay�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class CustomerOrderPayDAO : Base.BaseCPOSDAO, ICRUDable<CustomerOrderPayEntity>, IQueryable<CustomerOrderPayEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public CustomerOrderPayDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(CustomerOrderPayEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(CustomerOrderPayEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [CustomerOrderPay](");
            strSql.Append("[OrderId],[ChannelId],[SerialPay],[PayAmount],[WithdrawalAmount],[ReceivablesAmount],[OrderSource],[OrderPayStatus],[PayTime],[ArriveTime],[WithdrawalTime],[PlayMoneyTime],[WithdrawalId],[FailureReason],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[CustomerId],[OrderPayId])");
            strSql.Append(" values (");
            strSql.Append("@OrderId,@ChannelId,@SerialPay,@PayAmount,@WithdrawalAmount,@ReceivablesAmount,@OrderSource,@OrderPayStatus,@PayTime,@ArriveTime,@WithdrawalTime,@PlayMoneyTime,@WithdrawalId,@FailureReason,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@CustomerId,@OrderPayId)");            

			Guid? pkGuid;
			if (pEntity.OrderPayId == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.OrderPayId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@OrderId",SqlDbType.NVarChar),
					new SqlParameter("@ChannelId",SqlDbType.Int),
					new SqlParameter("@SerialPay",SqlDbType.NVarChar),
					new SqlParameter("@PayAmount",SqlDbType.Decimal),
					new SqlParameter("@WithdrawalAmount",SqlDbType.Decimal),
					new SqlParameter("@ReceivablesAmount",SqlDbType.Decimal),
					new SqlParameter("@OrderSource",SqlDbType.Int),
					new SqlParameter("@OrderPayStatus",SqlDbType.Int),
					new SqlParameter("@PayTime",SqlDbType.DateTime),
					new SqlParameter("@ArriveTime",SqlDbType.DateTime),
					new SqlParameter("@WithdrawalTime",SqlDbType.DateTime),
					new SqlParameter("@PlayMoneyTime",SqlDbType.DateTime),
					new SqlParameter("@WithdrawalId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@FailureReason",SqlDbType.NVarChar),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@OrderPayId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.OrderId;
			parameters[1].Value = pEntity.ChannelId;
			parameters[2].Value = pEntity.SerialPay;
			parameters[3].Value = pEntity.PayAmount;
			parameters[4].Value = pEntity.WithdrawalAmount;
			parameters[5].Value = pEntity.ReceivablesAmount;
			parameters[6].Value = pEntity.OrderSource;
			parameters[7].Value = pEntity.OrderPayStatus;
			parameters[8].Value = pEntity.PayTime;
			parameters[9].Value = pEntity.ArriveTime;
			parameters[10].Value = pEntity.WithdrawalTime;
			parameters[11].Value = pEntity.PlayMoneyTime;
			parameters[12].Value = pEntity.WithdrawalId;
			parameters[13].Value = pEntity.FailureReason;
			parameters[14].Value = pEntity.CreateBy;
			parameters[15].Value = pEntity.CreateTime;
			parameters[16].Value = pEntity.LastUpdateBy;
			parameters[17].Value = pEntity.LastUpdateTime;
			parameters[18].Value = pEntity.IsDelete;
			parameters[19].Value = pEntity.CustomerId;
			parameters[20].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.OrderPayId = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public CustomerOrderPayEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [CustomerOrderPay] where OrderPayId='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            CustomerOrderPayEntity m = null;
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
        public CustomerOrderPayEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [CustomerOrderPay] where isdelete=0");
            //��ȡ����
            List<CustomerOrderPayEntity> list = new List<CustomerOrderPayEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    CustomerOrderPayEntity m;
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
        public void Update(CustomerOrderPayEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(CustomerOrderPayEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.OrderPayId==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [CustomerOrderPay] set ");
            if (pIsUpdateNullField || pEntity.OrderId!=null)
                strSql.Append( "[OrderId]=@OrderId,");
            if (pIsUpdateNullField || pEntity.ChannelId!=null)
                strSql.Append( "[ChannelId]=@ChannelId,");
            if (pIsUpdateNullField || pEntity.SerialPay!=null)
                strSql.Append( "[SerialPay]=@SerialPay,");
            if (pIsUpdateNullField || pEntity.PayAmount!=null)
                strSql.Append( "[PayAmount]=@PayAmount,");
            if (pIsUpdateNullField || pEntity.WithdrawalAmount!=null)
                strSql.Append( "[WithdrawalAmount]=@WithdrawalAmount,");
            if (pIsUpdateNullField || pEntity.ReceivablesAmount!=null)
                strSql.Append( "[ReceivablesAmount]=@ReceivablesAmount,");
            if (pIsUpdateNullField || pEntity.OrderSource!=null)
                strSql.Append( "[OrderSource]=@OrderSource,");
            if (pIsUpdateNullField || pEntity.OrderPayStatus!=null)
                strSql.Append( "[OrderPayStatus]=@OrderPayStatus,");
            if (pIsUpdateNullField || pEntity.PayTime!=null)
                strSql.Append( "[PayTime]=@PayTime,");
            if (pIsUpdateNullField || pEntity.ArriveTime!=null)
                strSql.Append( "[ArriveTime]=@ArriveTime,");
            if (pIsUpdateNullField || pEntity.WithdrawalTime!=null)
                strSql.Append( "[WithdrawalTime]=@WithdrawalTime,");
            if (pIsUpdateNullField || pEntity.PlayMoneyTime!=null)
                strSql.Append( "[PlayMoneyTime]=@PlayMoneyTime,");
            if (pIsUpdateNullField || pEntity.WithdrawalId!=null)
                strSql.Append( "[WithdrawalId]=@WithdrawalId,");
            if (pIsUpdateNullField || pEntity.FailureReason!=null)
                strSql.Append( "[FailureReason]=@FailureReason,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where OrderPayId=@OrderPayId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@OrderId",SqlDbType.NVarChar),
					new SqlParameter("@ChannelId",SqlDbType.Int),
					new SqlParameter("@SerialPay",SqlDbType.NVarChar),
					new SqlParameter("@PayAmount",SqlDbType.Decimal),
					new SqlParameter("@WithdrawalAmount",SqlDbType.Decimal),
					new SqlParameter("@ReceivablesAmount",SqlDbType.Decimal),
					new SqlParameter("@OrderSource",SqlDbType.Int),
					new SqlParameter("@OrderPayStatus",SqlDbType.Int),
					new SqlParameter("@PayTime",SqlDbType.DateTime),
					new SqlParameter("@ArriveTime",SqlDbType.DateTime),
					new SqlParameter("@WithdrawalTime",SqlDbType.DateTime),
					new SqlParameter("@PlayMoneyTime",SqlDbType.DateTime),
					new SqlParameter("@WithdrawalId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@FailureReason",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@OrderPayId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.OrderId;
			parameters[1].Value = pEntity.ChannelId;
			parameters[2].Value = pEntity.SerialPay;
			parameters[3].Value = pEntity.PayAmount;
			parameters[4].Value = pEntity.WithdrawalAmount;
			parameters[5].Value = pEntity.ReceivablesAmount;
			parameters[6].Value = pEntity.OrderSource;
			parameters[7].Value = pEntity.OrderPayStatus;
			parameters[8].Value = pEntity.PayTime;
			parameters[9].Value = pEntity.ArriveTime;
			parameters[10].Value = pEntity.WithdrawalTime;
			parameters[11].Value = pEntity.PlayMoneyTime;
			parameters[12].Value = pEntity.WithdrawalId;
			parameters[13].Value = pEntity.FailureReason;
			parameters[14].Value = pEntity.LastUpdateBy;
			parameters[15].Value = pEntity.LastUpdateTime;
			parameters[16].Value = pEntity.CustomerId;
			parameters[17].Value = pEntity.OrderPayId;

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
        public void Update(CustomerOrderPayEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(CustomerOrderPayEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(CustomerOrderPayEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(CustomerOrderPayEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.OrderPayId==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.OrderPayId, pTran);           
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
            sql.AppendLine("update [CustomerOrderPay] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where OrderPayId=@OrderPayId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@OrderPayId",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(CustomerOrderPayEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.OrderPayId==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.OrderPayId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(CustomerOrderPayEntity[] pEntities)
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
            sql.AppendLine("update [CustomerOrderPay] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where OrderPayId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public CustomerOrderPayEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [CustomerOrderPay] where isdelete=0 ");
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
            List<CustomerOrderPayEntity> list = new List<CustomerOrderPayEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    CustomerOrderPayEntity m;
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
        public PagedQueryResult<CustomerOrderPayEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [OrderPayId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [CustomerOrderPay] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [CustomerOrderPay] where isdelete=0 ");
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
            PagedQueryResult<CustomerOrderPayEntity> result = new PagedQueryResult<CustomerOrderPayEntity>();
            List<CustomerOrderPayEntity> list = new List<CustomerOrderPayEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    CustomerOrderPayEntity m;
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
        public CustomerOrderPayEntity[] QueryByEntity(CustomerOrderPayEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<CustomerOrderPayEntity> PagedQueryByEntity(CustomerOrderPayEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(CustomerOrderPayEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.OrderPayId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderPayId", Value = pQueryEntity.OrderPayId });
            if (pQueryEntity.OrderId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderId", Value = pQueryEntity.OrderId });
            if (pQueryEntity.ChannelId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ChannelId", Value = pQueryEntity.ChannelId });
            if (pQueryEntity.SerialPay!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SerialPay", Value = pQueryEntity.SerialPay });
            if (pQueryEntity.PayAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PayAmount", Value = pQueryEntity.PayAmount });
            if (pQueryEntity.WithdrawalAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WithdrawalAmount", Value = pQueryEntity.WithdrawalAmount });
            if (pQueryEntity.ReceivablesAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReceivablesAmount", Value = pQueryEntity.ReceivablesAmount });
            if (pQueryEntity.OrderSource!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderSource", Value = pQueryEntity.OrderSource });
            if (pQueryEntity.OrderPayStatus!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderPayStatus", Value = pQueryEntity.OrderPayStatus });
            if (pQueryEntity.PayTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PayTime", Value = pQueryEntity.PayTime });
            if (pQueryEntity.ArriveTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ArriveTime", Value = pQueryEntity.ArriveTime });
            if (pQueryEntity.WithdrawalTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WithdrawalTime", Value = pQueryEntity.WithdrawalTime });
            if (pQueryEntity.PlayMoneyTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PlayMoneyTime", Value = pQueryEntity.PlayMoneyTime });
            if (pQueryEntity.WithdrawalId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WithdrawalId", Value = pQueryEntity.WithdrawalId });
            if (pQueryEntity.FailureReason!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FailureReason", Value = pQueryEntity.FailureReason });
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
        protected void Load(SqlDataReader pReader, out CustomerOrderPayEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new CustomerOrderPayEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["OrderPayId"] != DBNull.Value)
			{
				pInstance.OrderPayId =  (Guid)pReader["OrderPayId"];
			}
			if (pReader["OrderId"] != DBNull.Value)
			{
				pInstance.OrderId =  Convert.ToString(pReader["OrderId"]);
			}
			if (pReader["ChannelId"] != DBNull.Value)
			{
				pInstance.ChannelId =   Convert.ToInt32(pReader["ChannelId"]);
			}
			if (pReader["SerialPay"] != DBNull.Value)
			{
				pInstance.SerialPay =  Convert.ToString(pReader["SerialPay"]);
			}
			if (pReader["PayAmount"] != DBNull.Value)
			{
				pInstance.PayAmount =  Convert.ToDecimal(pReader["PayAmount"]);
			}
			if (pReader["WithdrawalAmount"] != DBNull.Value)
			{
				pInstance.WithdrawalAmount =  Convert.ToDecimal(pReader["WithdrawalAmount"]);
			}
			if (pReader["ReceivablesAmount"] != DBNull.Value)
			{
				pInstance.ReceivablesAmount =  Convert.ToDecimal(pReader["ReceivablesAmount"]);
			}
			if (pReader["OrderSource"] != DBNull.Value)
			{
				pInstance.OrderSource =   Convert.ToInt32(pReader["OrderSource"]);
			}
			if (pReader["OrderPayStatus"] != DBNull.Value)
			{
				pInstance.OrderPayStatus =   Convert.ToInt32(pReader["OrderPayStatus"]);
			}
			if (pReader["PayTime"] != DBNull.Value)
			{
				pInstance.PayTime =  Convert.ToDateTime(pReader["PayTime"]);
			}
			if (pReader["ArriveTime"] != DBNull.Value)
			{
				pInstance.ArriveTime =  Convert.ToDateTime(pReader["ArriveTime"]);
			}
			if (pReader["WithdrawalTime"] != DBNull.Value)
			{
				pInstance.WithdrawalTime =  Convert.ToDateTime(pReader["WithdrawalTime"]);
			}
			if (pReader["PlayMoneyTime"] != DBNull.Value)
			{
				pInstance.PlayMoneyTime =  Convert.ToDateTime(pReader["PlayMoneyTime"]);
			}
			if (pReader["WithdrawalId"] != DBNull.Value)
			{
				pInstance.WithdrawalId =  (Guid)pReader["WithdrawalId"];
			}
			if (pReader["FailureReason"] != DBNull.Value)
			{
				pInstance.FailureReason =  Convert.ToString(pReader["FailureReason"]);
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
