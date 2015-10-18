/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-19 11:35:13
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
    /// ��T_Payment_Type�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class T_Payment_TypeDAO : Base.BaseCPOSDAO, ICRUDable<T_Payment_TypeEntity>, IQueryable<T_Payment_TypeEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public T_Payment_TypeDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(T_Payment_TypeEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(T_Payment_TypeEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [T_Payment_Type](");
            strSql.Append("[Payment_Type_Name],[Payment_Type_Code],[Status],[PaymentCompany],[PaymentItemType],[LogoURL],[PaymentDesc],[CreateTime],[LastUpdateTime],[CreateBy],[LastUpdateBy],[IsDelete],[IsJSPay],[IsNativePay],[IsCSPay],[CustomerId],[Payment_Type_Id])");
            strSql.Append(" values (");
            strSql.Append("@Payment_Type_Name,@Payment_Type_Code,@Status,@PaymentCompany,@PaymentItemType,@LogoURL,@PaymentDesc,@CreateTime,@LastUpdateTime,@CreateBy,@LastUpdateBy,@IsDelete,@IsJSPay,@IsNativePay,@IsCSPay,@CustomerId,@Payment_Type_Id)");            

			string pkString = pEntity.Payment_Type_Id;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@Payment_Type_Name",SqlDbType.NVarChar),
					new SqlParameter("@Payment_Type_Code",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.NVarChar),
					new SqlParameter("@PaymentCompany",SqlDbType.NVarChar),
					new SqlParameter("@PaymentItemType",SqlDbType.NVarChar),
					new SqlParameter("@LogoURL",SqlDbType.NVarChar),
					new SqlParameter("@PaymentDesc",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@IsJSPay",SqlDbType.Int),
					new SqlParameter("@IsNativePay",SqlDbType.Int),
					new SqlParameter("@IsCSPay",SqlDbType.Int),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@Payment_Type_Id",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.Payment_Type_Name;
			parameters[1].Value = pEntity.Payment_Type_Code;
			parameters[2].Value = pEntity.Status;
			parameters[3].Value = pEntity.PaymentCompany;
			parameters[4].Value = pEntity.PaymentItemType;
			parameters[5].Value = pEntity.LogoURL;
			parameters[6].Value = pEntity.PaymentDesc;
			parameters[7].Value = pEntity.CreateTime;
			parameters[8].Value = pEntity.LastUpdateTime;
			parameters[9].Value = pEntity.CreateBy;
			parameters[10].Value = pEntity.LastUpdateBy;
			parameters[11].Value = pEntity.IsDelete;
			parameters[12].Value = pEntity.IsJSPay;
			parameters[13].Value = pEntity.IsNativePay;
			parameters[14].Value = pEntity.IsCSPay;
			parameters[15].Value = pEntity.CustomerId;
			parameters[16].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.Payment_Type_Id = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public T_Payment_TypeEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Payment_Type] where Payment_Type_Id='{0}'  and isdelete=0 ", id.ToString());
            //��ȡ����
            T_Payment_TypeEntity m = null;
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
        public T_Payment_TypeEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Payment_Type] where 1=1  and isdelete=0");
            //��ȡ����
            List<T_Payment_TypeEntity> list = new List<T_Payment_TypeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_Payment_TypeEntity m;
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
        public void Update(T_Payment_TypeEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(T_Payment_TypeEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.Payment_Type_Id == null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_Payment_Type] set ");
                        if (pIsUpdateNullField || pEntity.Payment_Type_Name!=null)
                strSql.Append( "[Payment_Type_Name]=@Payment_Type_Name,");
            if (pIsUpdateNullField || pEntity.Payment_Type_Code!=null)
                strSql.Append( "[Payment_Type_Code]=@Payment_Type_Code,");
            if (pIsUpdateNullField || pEntity.Status!=null)
                strSql.Append( "[Status]=@Status,");
            if (pIsUpdateNullField || pEntity.PaymentCompany!=null)
                strSql.Append( "[PaymentCompany]=@PaymentCompany,");
            if (pIsUpdateNullField || pEntity.PaymentItemType!=null)
                strSql.Append( "[PaymentItemType]=@PaymentItemType,");
            if (pIsUpdateNullField || pEntity.LogoURL!=null)
                strSql.Append( "[LogoURL]=@LogoURL,");
            if (pIsUpdateNullField || pEntity.PaymentDesc!=null)
                strSql.Append( "[PaymentDesc]=@PaymentDesc,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.IsJSPay!=null)
                strSql.Append( "[IsJSPay]=@IsJSPay,");
            if (pIsUpdateNullField || pEntity.IsNativePay!=null)
                strSql.Append( "[IsNativePay]=@IsNativePay,");
            if (pIsUpdateNullField || pEntity.IsCSPay!=null)
                strSql.Append( "[IsCSPay]=@IsCSPay,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId");
            strSql.Append(" where Payment_Type_Id=@Payment_Type_Id ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@Payment_Type_Name",SqlDbType.NVarChar),
					new SqlParameter("@Payment_Type_Code",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.NVarChar),
					new SqlParameter("@PaymentCompany",SqlDbType.NVarChar),
					new SqlParameter("@PaymentItemType",SqlDbType.NVarChar),
					new SqlParameter("@LogoURL",SqlDbType.NVarChar),
					new SqlParameter("@PaymentDesc",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@IsJSPay",SqlDbType.Int),
					new SqlParameter("@IsNativePay",SqlDbType.Int),
					new SqlParameter("@IsCSPay",SqlDbType.Int),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@Payment_Type_Id",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.Payment_Type_Name;
			parameters[1].Value = pEntity.Payment_Type_Code;
			parameters[2].Value = pEntity.Status;
			parameters[3].Value = pEntity.PaymentCompany;
			parameters[4].Value = pEntity.PaymentItemType;
			parameters[5].Value = pEntity.LogoURL;
			parameters[6].Value = pEntity.PaymentDesc;
			parameters[7].Value = pEntity.LastUpdateTime;
			parameters[8].Value = pEntity.LastUpdateBy;
			parameters[9].Value = pEntity.IsJSPay;
			parameters[10].Value = pEntity.IsNativePay;
			parameters[11].Value = pEntity.IsCSPay;
			parameters[12].Value = pEntity.CustomerId;
			parameters[13].Value = pEntity.Payment_Type_Id;

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
        public void Update(T_Payment_TypeEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(T_Payment_TypeEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(T_Payment_TypeEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.Payment_Type_Id == null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.Payment_Type_Id, pTran);           
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
            sql.AppendLine("update [T_Payment_Type] set  isdelete=1 where Payment_Type_Id=@Payment_Type_Id;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@Payment_Type_Id",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(T_Payment_TypeEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (pEntity.Payment_Type_Id == null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.Payment_Type_Id;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(T_Payment_TypeEntity[] pEntities)
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
            sql.AppendLine("update [T_Payment_Type] set  isdelete=1 where Payment_Type_Id in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public T_Payment_TypeEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Payment_Type] where 1=1  and isdelete=0 ");
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
            List<T_Payment_TypeEntity> list = new List<T_Payment_TypeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_Payment_TypeEntity m;
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
        public PagedQueryResult<T_Payment_TypeEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [Payment_Type_Id] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [T_Payment_Type] where 1=1  and isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [T_Payment_Type] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<T_Payment_TypeEntity> result = new PagedQueryResult<T_Payment_TypeEntity>();
            List<T_Payment_TypeEntity> list = new List<T_Payment_TypeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    T_Payment_TypeEntity m;
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
        public T_Payment_TypeEntity[] QueryByEntity(T_Payment_TypeEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<T_Payment_TypeEntity> PagedQueryByEntity(T_Payment_TypeEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(T_Payment_TypeEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.Payment_Type_Id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Payment_Type_Id", Value = pQueryEntity.Payment_Type_Id });
            if (pQueryEntity.Payment_Type_Name!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Payment_Type_Name", Value = pQueryEntity.Payment_Type_Name });
            if (pQueryEntity.Payment_Type_Code!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Payment_Type_Code", Value = pQueryEntity.Payment_Type_Code });
            if (pQueryEntity.Status!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Status", Value = pQueryEntity.Status });
            if (pQueryEntity.PaymentCompany!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PaymentCompany", Value = pQueryEntity.PaymentCompany });
            if (pQueryEntity.PaymentItemType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PaymentItemType", Value = pQueryEntity.PaymentItemType });
            if (pQueryEntity.LogoURL!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LogoURL", Value = pQueryEntity.LogoURL });
            if (pQueryEntity.PaymentDesc!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PaymentDesc", Value = pQueryEntity.PaymentDesc });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.LastUpdateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.LastUpdateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.IsDelete!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });
            if (pQueryEntity.IsJSPay!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsJSPay", Value = pQueryEntity.IsJSPay });
            if (pQueryEntity.IsNativePay!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsNativePay", Value = pQueryEntity.IsNativePay });
            if (pQueryEntity.IsCSPay!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsCSPay", Value = pQueryEntity.IsCSPay });
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(IDataReader pReader, out T_Payment_TypeEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new T_Payment_TypeEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["Payment_Type_Id"] != DBNull.Value)
			{
				pInstance.Payment_Type_Id =  Convert.ToString(pReader["Payment_Type_Id"]);
			}
			if (pReader["Payment_Type_Name"] != DBNull.Value)
			{
				pInstance.Payment_Type_Name =  Convert.ToString(pReader["Payment_Type_Name"]);
			}
			if (pReader["Payment_Type_Code"] != DBNull.Value)
			{
				pInstance.Payment_Type_Code =  Convert.ToString(pReader["Payment_Type_Code"]);
			}
			if (pReader["Status"] != DBNull.Value)
			{
				pInstance.Status =  Convert.ToString(pReader["Status"]);
			}
			if (pReader["PaymentCompany"] != DBNull.Value)
			{
				pInstance.PaymentCompany =  Convert.ToString(pReader["PaymentCompany"]);
			}
			if (pReader["PaymentItemType"] != DBNull.Value)
			{
				pInstance.PaymentItemType =  Convert.ToString(pReader["PaymentItemType"]);
			}
			if (pReader["LogoURL"] != DBNull.Value)
			{
				pInstance.LogoURL =  Convert.ToString(pReader["LogoURL"]);
			}
			if (pReader["PaymentDesc"] != DBNull.Value)
			{
				pInstance.PaymentDesc =  Convert.ToString(pReader["PaymentDesc"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}
			if (pReader["LastUpdateTime"] != DBNull.Value)
			{
				pInstance.LastUpdateTime =  Convert.ToDateTime(pReader["LastUpdateTime"]);
			}
			if (pReader["CreateBy"] != DBNull.Value)
			{
				pInstance.CreateBy =  Convert.ToString(pReader["CreateBy"]);
			}
			if (pReader["LastUpdateBy"] != DBNull.Value)
			{
				pInstance.LastUpdateBy =  Convert.ToString(pReader["LastUpdateBy"]);
			}
			if (pReader["IsDelete"] != DBNull.Value)
			{
				pInstance.IsDelete =   Convert.ToInt32(pReader["IsDelete"]);
			}
			if (pReader["IsJSPay"] != DBNull.Value)
			{
				pInstance.IsJSPay =   Convert.ToInt32(pReader["IsJSPay"]);
			}
			if (pReader["IsNativePay"] != DBNull.Value)
			{
				pInstance.IsNativePay =   Convert.ToInt32(pReader["IsNativePay"]);
			}
			if (pReader["IsCSPay"] != DBNull.Value)
			{
				pInstance.IsCSPay =   Convert.ToInt32(pReader["IsCSPay"]);
			}
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}

        }
        #endregion
    }
}
