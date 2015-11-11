/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-10-18 9:39:00
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
    /// ��T_Type�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class T_TypeDAO : Base.BaseCPOSDAO, ICRUDable<T_TypeEntity>, IQueryable<T_TypeEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public T_TypeDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(T_TypeEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(T_TypeEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //��ʼ���̶��ֶ�


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [T_Type](");
            strSql.Append("[type_code],[type_name],[type_name_en],[type_domain],[type_system_flag],[status],[type_Level],[customer_id],[type_id])");
            strSql.Append(" values (");
            strSql.Append("@type_code,@type_name,@type_name_en,@type_domain,@type_system_flag,@status,@type_Level,@customer_id,@type_id)");            

			string pkString = pEntity.type_id;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@type_code",SqlDbType.NVarChar),
					new SqlParameter("@type_name",SqlDbType.NVarChar),
					new SqlParameter("@type_name_en",SqlDbType.NVarChar),
					new SqlParameter("@type_domain",SqlDbType.NVarChar),
					new SqlParameter("@type_system_flag",SqlDbType.Int),
					new SqlParameter("@status",SqlDbType.Int),
					new SqlParameter("@type_Level",SqlDbType.Int),
					new SqlParameter("@customer_id",SqlDbType.VarChar),
					new SqlParameter("@type_id",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.type_code;
			parameters[1].Value = pEntity.type_name;
			parameters[2].Value = pEntity.type_name_en;
			parameters[3].Value = pEntity.type_domain;
			parameters[4].Value = pEntity.type_system_flag;
			parameters[5].Value = pEntity.status;
			parameters[6].Value = pEntity.type_Level;
			parameters[7].Value = pEntity.customer_id;
			parameters[8].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.type_id = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public T_TypeEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Type] where type_id='{0}'  and status<>'-1' ", id.ToString());
            //��ȡ����
            T_TypeEntity m = null;
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
        public T_TypeEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Type] where 1=1  and status<>'-1'");
            //��ȡ����
            List<T_TypeEntity> list = new List<T_TypeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_TypeEntity m;
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
        public void Update(T_TypeEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(T_TypeEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.type_id == null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_Type] set ");
                        if (pIsUpdateNullField || pEntity.type_code!=null)
                strSql.Append( "[type_code]=@type_code,");
            if (pIsUpdateNullField || pEntity.type_name!=null)
                strSql.Append( "[type_name]=@type_name,");
            if (pIsUpdateNullField || pEntity.type_name_en!=null)
                strSql.Append( "[type_name_en]=@type_name_en,");
            if (pIsUpdateNullField || pEntity.type_domain!=null)
                strSql.Append( "[type_domain]=@type_domain,");
            if (pIsUpdateNullField || pEntity.type_system_flag!=null)
                strSql.Append( "[type_system_flag]=@type_system_flag,");
            if (pIsUpdateNullField || pEntity.status!=null)
                strSql.Append( "[status]=@status,");
            if (pIsUpdateNullField || pEntity.type_Level!=null)
                strSql.Append( "[type_Level]=@type_Level,");
            if (pIsUpdateNullField || pEntity.customer_id!=null)
                strSql.Append( "[customer_id]=@customer_id");
            strSql.Append(" where type_id=@type_id ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@type_code",SqlDbType.NVarChar),
					new SqlParameter("@type_name",SqlDbType.NVarChar),
					new SqlParameter("@type_name_en",SqlDbType.NVarChar),
					new SqlParameter("@type_domain",SqlDbType.NVarChar),
					new SqlParameter("@type_system_flag",SqlDbType.Int),
					new SqlParameter("@status",SqlDbType.Int),
					new SqlParameter("@type_Level",SqlDbType.Int),
					new SqlParameter("@customer_id",SqlDbType.VarChar),
					new SqlParameter("@type_id",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.type_code;
			parameters[1].Value = pEntity.type_name;
			parameters[2].Value = pEntity.type_name_en;
			parameters[3].Value = pEntity.type_domain;
			parameters[4].Value = pEntity.type_system_flag;
			parameters[5].Value = pEntity.status;
			parameters[6].Value = pEntity.type_Level;
			parameters[7].Value = pEntity.customer_id;
			parameters[8].Value = pEntity.type_id;

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
        public void Update(T_TypeEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(T_TypeEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(T_TypeEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.type_id == null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.type_id, pTran);           
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
            sql.AppendLine("update [T_Type] set status='-1' where type_id=@type_id;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@type_id",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(T_TypeEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (pEntity.type_id == null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.type_id;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(T_TypeEntity[] pEntities)
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
            sql.AppendLine("update [T_Type] set status='-1' where type_id in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public T_TypeEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Type] where 1=1  and status<>'-1' ");
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
            List<T_TypeEntity> list = new List<T_TypeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_TypeEntity m;
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
        public PagedQueryResult<T_TypeEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [type_id] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [T_Type] where 1=1  and status<>'-1' ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [T_Type] where 1=1  and status<>'-1' ");
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
            PagedQueryResult<T_TypeEntity> result = new PagedQueryResult<T_TypeEntity>();
            List<T_TypeEntity> list = new List<T_TypeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    T_TypeEntity m;
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
        public T_TypeEntity[] QueryByEntity(T_TypeEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<T_TypeEntity> PagedQueryByEntity(T_TypeEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(T_TypeEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.type_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "type_id", Value = pQueryEntity.type_id });
            if (pQueryEntity.type_code!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "type_code", Value = pQueryEntity.type_code });
            if (pQueryEntity.type_name!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "type_name", Value = pQueryEntity.type_name });
            if (pQueryEntity.type_name_en!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "type_name_en", Value = pQueryEntity.type_name_en });
            if (pQueryEntity.type_domain!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "type_domain", Value = pQueryEntity.type_domain });
            if (pQueryEntity.type_system_flag!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "type_system_flag", Value = pQueryEntity.type_system_flag });
            if (pQueryEntity.status!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "status", Value = pQueryEntity.status });
            if (pQueryEntity.type_Level!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "type_Level", Value = pQueryEntity.type_Level });
            if (pQueryEntity.customer_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customer_id", Value = pQueryEntity.customer_id });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(IDataReader pReader, out T_TypeEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new T_TypeEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["type_id"] != DBNull.Value)
			{
				pInstance.type_id =  Convert.ToString(pReader["type_id"]);
			}
			if (pReader["type_code"] != DBNull.Value)
			{
				pInstance.type_code =  Convert.ToString(pReader["type_code"]);
			}
			if (pReader["type_name"] != DBNull.Value)
			{
				pInstance.type_name =  Convert.ToString(pReader["type_name"]);
			}
			if (pReader["type_name_en"] != DBNull.Value)
			{
				pInstance.type_name_en =  Convert.ToString(pReader["type_name_en"]);
			}
			if (pReader["type_domain"] != DBNull.Value)
			{
				pInstance.type_domain =  Convert.ToString(pReader["type_domain"]);
			}
			if (pReader["type_system_flag"] != DBNull.Value)
			{
				pInstance.type_system_flag =   Convert.ToInt32(pReader["type_system_flag"]);
			}
			if (pReader["status"] != DBNull.Value)
			{
				pInstance.status =   Convert.ToInt32(pReader["status"]);
			}
			if (pReader["type_Level"] != DBNull.Value)
			{
				pInstance.type_Level =   Convert.ToInt32(pReader["type_Level"]);
			}
			if (pReader["customer_id"] != DBNull.Value)
			{
				pInstance.customer_id =  Convert.ToString(pReader["customer_id"]);
			}

        }
        #endregion
    }
}
