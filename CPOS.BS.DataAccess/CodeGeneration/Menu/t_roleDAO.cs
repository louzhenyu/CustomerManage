/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016-5-9 17:56:01
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
    /// ��T_Role�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class T_RoleDAO : Base.BaseCPOSDAO, ICRUDable<T_RoleEntity>, IQueryable<T_RoleEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public T_RoleDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(T_RoleEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(T_RoleEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //��ʼ���̶��ֶ�


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [T_Role](");
            strSql.Append("[def_app_id],[role_code],[role_name],[role_eng_name],[is_sys],[status],[create_time],[create_user_id],[modify_time],[modify_user_id],[customer_id],[table_name],[org_level],[type_id],[role_id])");
            strSql.Append(" values (");
            strSql.Append("@def_app_id,@role_code,@role_name,@role_eng_name,@is_sys,@status,@create_time,@create_user_id,@modify_time,@modify_user_id,@customer_id,@table_name,@org_level,@type_id,@role_id)");            

			string pkString = pEntity.role_id;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@def_app_id",SqlDbType.NVarChar),
					new SqlParameter("@role_code",SqlDbType.NVarChar),
					new SqlParameter("@role_name",SqlDbType.NVarChar),
					new SqlParameter("@role_eng_name",SqlDbType.NVarChar),
					new SqlParameter("@is_sys",SqlDbType.Int),
					new SqlParameter("@status",SqlDbType.Int),
					new SqlParameter("@create_time",SqlDbType.NVarChar),
					new SqlParameter("@create_user_id",SqlDbType.NVarChar),
					new SqlParameter("@modify_time",SqlDbType.NVarChar),
					new SqlParameter("@modify_user_id",SqlDbType.NVarChar),
					new SqlParameter("@customer_id",SqlDbType.NVarChar),
					new SqlParameter("@table_name",SqlDbType.NVarChar),
					new SqlParameter("@org_level",SqlDbType.Int),
					new SqlParameter("@type_id",SqlDbType.NVarChar),
					new SqlParameter("@role_id",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.def_app_id;
			parameters[1].Value = pEntity.role_code;
			parameters[2].Value = pEntity.role_name;
			parameters[3].Value = pEntity.role_eng_name;
			parameters[4].Value = pEntity.is_sys;
			parameters[5].Value = pEntity.status;
			parameters[6].Value = pEntity.create_time;
			parameters[7].Value = pEntity.create_user_id;
			parameters[8].Value = pEntity.modify_time;
			parameters[9].Value = pEntity.modify_user_id;
			parameters[10].Value = pEntity.customer_id;
			parameters[11].Value = pEntity.table_name;
			parameters[12].Value = pEntity.org_level;
			parameters[13].Value = pEntity.type_id;
			parameters[14].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.role_id = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public T_RoleEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Role] where role_id='{0}'  and status<>'-1' ", id.ToString());
            //��ȡ����
            T_RoleEntity m = null;
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
        public T_RoleEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Role] where 1=1  and status<>'-1'");
            //��ȡ����
            List<T_RoleEntity> list = new List<T_RoleEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_RoleEntity m;
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
        public void Update(T_RoleEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(T_RoleEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.role_id == null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_Role] set ");
                        if (pIsUpdateNullField || pEntity.def_app_id!=null)
                strSql.Append( "[def_app_id]=@def_app_id,");
            if (pIsUpdateNullField || pEntity.role_code!=null)
                strSql.Append( "[role_code]=@role_code,");
            if (pIsUpdateNullField || pEntity.role_name!=null)
                strSql.Append( "[role_name]=@role_name,");
            if (pIsUpdateNullField || pEntity.role_eng_name!=null)
                strSql.Append( "[role_eng_name]=@role_eng_name,");
            if (pIsUpdateNullField || pEntity.is_sys!=null)
                strSql.Append( "[is_sys]=@is_sys,");
            if (pIsUpdateNullField || pEntity.status!=null)
                strSql.Append( "[status]=@status,");
            if (pIsUpdateNullField || pEntity.create_time!=null)
                strSql.Append( "[create_time]=@create_time,");
            if (pIsUpdateNullField || pEntity.create_user_id!=null)
                strSql.Append( "[create_user_id]=@create_user_id,");
            if (pIsUpdateNullField || pEntity.modify_time!=null)
                strSql.Append( "[modify_time]=@modify_time,");
            if (pIsUpdateNullField || pEntity.modify_user_id!=null)
                strSql.Append( "[modify_user_id]=@modify_user_id,");
            if (pIsUpdateNullField || pEntity.customer_id!=null)
                strSql.Append( "[customer_id]=@customer_id,");
            if (pIsUpdateNullField || pEntity.table_name!=null)
                strSql.Append( "[table_name]=@table_name,");
            if (pIsUpdateNullField || pEntity.org_level!=null)
                strSql.Append( "[org_level]=@org_level,");
            if (pIsUpdateNullField || pEntity.type_id!=null)
                strSql.Append( "[type_id]=@type_id");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where role_id=@role_id ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@def_app_id",SqlDbType.NVarChar),
					new SqlParameter("@role_code",SqlDbType.NVarChar),
					new SqlParameter("@role_name",SqlDbType.NVarChar),
					new SqlParameter("@role_eng_name",SqlDbType.NVarChar),
					new SqlParameter("@is_sys",SqlDbType.Int),
					new SqlParameter("@status",SqlDbType.Int),
					new SqlParameter("@create_time",SqlDbType.NVarChar),
					new SqlParameter("@create_user_id",SqlDbType.NVarChar),
					new SqlParameter("@modify_time",SqlDbType.NVarChar),
					new SqlParameter("@modify_user_id",SqlDbType.NVarChar),
					new SqlParameter("@customer_id",SqlDbType.NVarChar),
					new SqlParameter("@table_name",SqlDbType.NVarChar),
					new SqlParameter("@org_level",SqlDbType.Int),
					new SqlParameter("@type_id",SqlDbType.NVarChar),
					new SqlParameter("@role_id",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.def_app_id;
			parameters[1].Value = pEntity.role_code;
			parameters[2].Value = pEntity.role_name;
			parameters[3].Value = pEntity.role_eng_name;
			parameters[4].Value = pEntity.is_sys;
			parameters[5].Value = pEntity.status;
			parameters[6].Value = pEntity.create_time;
			parameters[7].Value = pEntity.create_user_id;
			parameters[8].Value = pEntity.modify_time;
			parameters[9].Value = pEntity.modify_user_id;
			parameters[10].Value = pEntity.customer_id;
			parameters[11].Value = pEntity.table_name;
			parameters[12].Value = pEntity.org_level;
			parameters[13].Value = pEntity.type_id;
			parameters[14].Value = pEntity.role_id;

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
        public void Update(T_RoleEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(T_RoleEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(T_RoleEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.role_id == null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.role_id, pTran);           
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
            sql.AppendLine("update [T_Role] set status='-1' where role_id=@role_id;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@role_id",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(T_RoleEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (pEntity.role_id == null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.role_id;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(T_RoleEntity[] pEntities)
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
            sql.AppendLine("update [T_Role] set status='-1' where role_id in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public T_RoleEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Role] where 1=1  and status<>'-1' ");
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
            List<T_RoleEntity> list = new List<T_RoleEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_RoleEntity m;
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
        public PagedQueryResult<T_RoleEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [role_id] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [T_Role] where 1=1  and status<>'-1' ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [T_Role] where 1=1  and status<>'-1' ");
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
            PagedQueryResult<T_RoleEntity> result = new PagedQueryResult<T_RoleEntity>();
            List<T_RoleEntity> list = new List<T_RoleEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    T_RoleEntity m;
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
        public T_RoleEntity[] QueryByEntity(T_RoleEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<T_RoleEntity> PagedQueryByEntity(T_RoleEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(T_RoleEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.role_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "role_id", Value = pQueryEntity.role_id });
            if (pQueryEntity.def_app_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "def_app_id", Value = pQueryEntity.def_app_id });
            if (pQueryEntity.role_code!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "role_code", Value = pQueryEntity.role_code });
            if (pQueryEntity.role_name!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "role_name", Value = pQueryEntity.role_name });
            if (pQueryEntity.role_eng_name!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "role_eng_name", Value = pQueryEntity.role_eng_name });
            if (pQueryEntity.is_sys!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "is_sys", Value = pQueryEntity.is_sys });
            if (pQueryEntity.status!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "status", Value = pQueryEntity.status });
            if (pQueryEntity.create_time!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "create_time", Value = pQueryEntity.create_time });
            if (pQueryEntity.create_user_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "create_user_id", Value = pQueryEntity.create_user_id });
            if (pQueryEntity.modify_time!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "modify_time", Value = pQueryEntity.modify_time });
            if (pQueryEntity.modify_user_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "modify_user_id", Value = pQueryEntity.modify_user_id });
            if (pQueryEntity.customer_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customer_id", Value = pQueryEntity.customer_id });
            if (pQueryEntity.table_name!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "table_name", Value = pQueryEntity.table_name });
            if (pQueryEntity.org_level!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "org_level", Value = pQueryEntity.org_level });
            if (pQueryEntity.type_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "type_id", Value = pQueryEntity.type_id });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(IDataReader pReader, out T_RoleEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new T_RoleEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["role_id"] != DBNull.Value)
			{
				pInstance.role_id =  Convert.ToString(pReader["role_id"]);
			}
			if (pReader["def_app_id"] != DBNull.Value)
			{
				pInstance.def_app_id =  Convert.ToString(pReader["def_app_id"]);
			}
			if (pReader["role_code"] != DBNull.Value)
			{
				pInstance.role_code =  Convert.ToString(pReader["role_code"]);
			}
			if (pReader["role_name"] != DBNull.Value)
			{
				pInstance.role_name =  Convert.ToString(pReader["role_name"]);
			}
			if (pReader["role_eng_name"] != DBNull.Value)
			{
				pInstance.role_eng_name =  Convert.ToString(pReader["role_eng_name"]);
			}
			if (pReader["is_sys"] != DBNull.Value)
			{
				pInstance.is_sys =   Convert.ToInt32(pReader["is_sys"]);
			}
			if (pReader["status"] != DBNull.Value)
			{
				pInstance.status =   Convert.ToInt32(pReader["status"]);
			}
			if (pReader["create_time"] != DBNull.Value)
			{
				pInstance.create_time =  Convert.ToString(pReader["create_time"]);
			}
			if (pReader["create_user_id"] != DBNull.Value)
			{
				pInstance.create_user_id =  Convert.ToString(pReader["create_user_id"]);
			}
			if (pReader["modify_time"] != DBNull.Value)
			{
				pInstance.modify_time =  Convert.ToString(pReader["modify_time"]);
			}
			if (pReader["modify_user_id"] != DBNull.Value)
			{
				pInstance.modify_user_id =  Convert.ToString(pReader["modify_user_id"]);
			}
			if (pReader["customer_id"] != DBNull.Value)
			{
				pInstance.customer_id =  Convert.ToString(pReader["customer_id"]);
			}
			if (pReader["table_name"] != DBNull.Value)
			{
				pInstance.table_name =  Convert.ToString(pReader["table_name"]);
			}
			if (pReader["org_level"] != DBNull.Value)
			{
				pInstance.org_level =   Convert.ToInt32(pReader["org_level"]);
			}
			if (pReader["type_id"] != DBNull.Value)
			{
				pInstance.type_id =  Convert.ToString(pReader["type_id"]);
			}

        }
        #endregion
    }
}
