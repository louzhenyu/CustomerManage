/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/7/6 16:12:43
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
    /// ��T_Prop�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class T_PropDAO : Base.BaseCPOSDAO, ICRUDable<T_PropEntity>, IQueryable<T_PropEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public T_PropDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(T_PropEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(T_PropEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //��ʼ���̶��ֶ�


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [T_Prop](");
            strSql.Append("[prop_code],[prop_name],[prop_eng_name],[prop_type],[parent_prop_id],[prop_level],[prop_domain],[prop_input_flag],[prop_max_lenth],[prop_default_value],[status],[display_index],[create_user_id],[create_time],[modify_user_id],[modify_time],[prop_id])");
            strSql.Append(" values (");
            strSql.Append("@prop_code,@prop_name,@prop_eng_name,@prop_type,@parent_prop_id,@prop_level,@prop_domain,@prop_input_flag,@prop_max_lenth,@prop_default_value,@status,@display_index,@create_user_id,@create_time,@modify_user_id,@modify_time,@prop_id)");            

			string pkString = pEntity.prop_id;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@prop_code",SqlDbType.NVarChar),
					new SqlParameter("@prop_name",SqlDbType.NVarChar),
					new SqlParameter("@prop_eng_name",SqlDbType.NVarChar),
					new SqlParameter("@prop_type",SqlDbType.NVarChar),
					new SqlParameter("@parent_prop_id",SqlDbType.NVarChar),
					new SqlParameter("@prop_level",SqlDbType.Int),
					new SqlParameter("@prop_domain",SqlDbType.NVarChar),
					new SqlParameter("@prop_input_flag",SqlDbType.NVarChar),
					new SqlParameter("@prop_max_lenth",SqlDbType.Int),
					new SqlParameter("@prop_default_value",SqlDbType.NVarChar),
					new SqlParameter("@status",SqlDbType.Int),
					new SqlParameter("@display_index",SqlDbType.Int),
					new SqlParameter("@create_user_id",SqlDbType.NVarChar),
					new SqlParameter("@create_time",SqlDbType.NVarChar),
					new SqlParameter("@modify_user_id",SqlDbType.NVarChar),
					new SqlParameter("@modify_time",SqlDbType.NVarChar),
					new SqlParameter("@prop_id",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.prop_code;
			parameters[1].Value = pEntity.prop_name;
			parameters[2].Value = pEntity.prop_eng_name;
			parameters[3].Value = pEntity.prop_type;
			parameters[4].Value = pEntity.parent_prop_id;
			parameters[5].Value = pEntity.prop_level;
			parameters[6].Value = pEntity.prop_domain;
			parameters[7].Value = pEntity.prop_input_flag;
			parameters[8].Value = pEntity.prop_max_lenth;
			parameters[9].Value = pEntity.prop_default_value;
			parameters[10].Value = pEntity.status;
			parameters[11].Value = pEntity.display_index;
			parameters[12].Value = pEntity.create_user_id;
			parameters[13].Value = pEntity.create_time;
			parameters[14].Value = pEntity.modify_user_id;
			parameters[15].Value = pEntity.modify_time;
			parameters[16].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.prop_id = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public T_PropEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Prop] where prop_id='{0}'  and status<>'-1' ", id.ToString());
            //��ȡ����
            T_PropEntity m = null;
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
        public T_PropEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Prop] where 1=1  and status<>'-1'");
            //��ȡ����
            List<T_PropEntity> list = new List<T_PropEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_PropEntity m;
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
        public void Update(T_PropEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(T_PropEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.prop_id == null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_Prop] set ");
                        if (pIsUpdateNullField || pEntity.prop_code!=null)
                strSql.Append( "[prop_code]=@prop_code,");
            if (pIsUpdateNullField || pEntity.prop_name!=null)
                strSql.Append( "[prop_name]=@prop_name,");
            if (pIsUpdateNullField || pEntity.prop_eng_name!=null)
                strSql.Append( "[prop_eng_name]=@prop_eng_name,");
            if (pIsUpdateNullField || pEntity.prop_type!=null)
                strSql.Append( "[prop_type]=@prop_type,");
            if (pIsUpdateNullField || pEntity.parent_prop_id!=null)
                strSql.Append( "[parent_prop_id]=@parent_prop_id,");
            if (pIsUpdateNullField || pEntity.prop_level!=null)
                strSql.Append( "[prop_level]=@prop_level,");
            if (pIsUpdateNullField || pEntity.prop_domain!=null)
                strSql.Append( "[prop_domain]=@prop_domain,");
            if (pIsUpdateNullField || pEntity.prop_input_flag!=null)
                strSql.Append( "[prop_input_flag]=@prop_input_flag,");
            if (pIsUpdateNullField || pEntity.prop_max_lenth!=null)
                strSql.Append( "[prop_max_lenth]=@prop_max_lenth,");
            if (pIsUpdateNullField || pEntity.prop_default_value!=null)
                strSql.Append( "[prop_default_value]=@prop_default_value,");
            if (pIsUpdateNullField || pEntity.status!=null)
                strSql.Append( "[status]=@status,");
            if (pIsUpdateNullField || pEntity.display_index!=null)
                strSql.Append( "[display_index]=@display_index,");
            if (pIsUpdateNullField || pEntity.create_user_id!=null)
                strSql.Append( "[create_user_id]=@create_user_id,");
            if (pIsUpdateNullField || pEntity.create_time!=null)
                strSql.Append( "[create_time]=@create_time,");
            if (pIsUpdateNullField || pEntity.modify_user_id!=null)
                strSql.Append( "[modify_user_id]=@modify_user_id,");
            if (pIsUpdateNullField || pEntity.modify_time!=null)
                strSql.Append( "[modify_time]=@modify_time");
            strSql.Append(" where prop_id=@prop_id ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@prop_code",SqlDbType.NVarChar),
					new SqlParameter("@prop_name",SqlDbType.NVarChar),
					new SqlParameter("@prop_eng_name",SqlDbType.NVarChar),
					new SqlParameter("@prop_type",SqlDbType.NVarChar),
					new SqlParameter("@parent_prop_id",SqlDbType.NVarChar),
					new SqlParameter("@prop_level",SqlDbType.Int),
					new SqlParameter("@prop_domain",SqlDbType.NVarChar),
					new SqlParameter("@prop_input_flag",SqlDbType.NVarChar),
					new SqlParameter("@prop_max_lenth",SqlDbType.Int),
					new SqlParameter("@prop_default_value",SqlDbType.NVarChar),
					new SqlParameter("@status",SqlDbType.Int),
					new SqlParameter("@display_index",SqlDbType.Int),
					new SqlParameter("@create_user_id",SqlDbType.NVarChar),
					new SqlParameter("@create_time",SqlDbType.NVarChar),
					new SqlParameter("@modify_user_id",SqlDbType.NVarChar),
					new SqlParameter("@modify_time",SqlDbType.NVarChar),
					new SqlParameter("@prop_id",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.prop_code;
			parameters[1].Value = pEntity.prop_name;
			parameters[2].Value = pEntity.prop_eng_name;
			parameters[3].Value = pEntity.prop_type;
			parameters[4].Value = pEntity.parent_prop_id;
			parameters[5].Value = pEntity.prop_level;
			parameters[6].Value = pEntity.prop_domain;
			parameters[7].Value = pEntity.prop_input_flag;
			parameters[8].Value = pEntity.prop_max_lenth;
			parameters[9].Value = pEntity.prop_default_value;
			parameters[10].Value = pEntity.status;
			parameters[11].Value = pEntity.display_index;
			parameters[12].Value = pEntity.create_user_id;
			parameters[13].Value = pEntity.create_time;
			parameters[14].Value = pEntity.modify_user_id;
			parameters[15].Value = pEntity.modify_time;
			parameters[16].Value = pEntity.prop_id;

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
        public void Update(T_PropEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(T_PropEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(T_PropEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.prop_id == null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.prop_id, pTran);           
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
            sql.AppendLine("update [T_Prop] set status='-1' where prop_id=@prop_id;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@prop_id",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(T_PropEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (pEntity.prop_id == null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.prop_id;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(T_PropEntity[] pEntities)
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
            sql.AppendLine("update [T_Prop] set status='-1' where prop_id in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public T_PropEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Prop] where 1=1  and status<>'-1' ");
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
            List<T_PropEntity> list = new List<T_PropEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_PropEntity m;
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
        public PagedQueryResult<T_PropEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [prop_id] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [T_Prop] where 1=1  and status<>'-1' ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [T_Prop] where 1=1  and status<>'-1' ");
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
            PagedQueryResult<T_PropEntity> result = new PagedQueryResult<T_PropEntity>();
            List<T_PropEntity> list = new List<T_PropEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    T_PropEntity m;
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
        public T_PropEntity[] QueryByEntity(T_PropEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<T_PropEntity> PagedQueryByEntity(T_PropEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(T_PropEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.prop_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "prop_id", Value = pQueryEntity.prop_id });
            if (pQueryEntity.prop_code!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "prop_code", Value = pQueryEntity.prop_code });
            if (pQueryEntity.prop_name!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "prop_name", Value = pQueryEntity.prop_name });
            if (pQueryEntity.prop_eng_name!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "prop_eng_name", Value = pQueryEntity.prop_eng_name });
            if (pQueryEntity.prop_type!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "prop_type", Value = pQueryEntity.prop_type });
            if (pQueryEntity.parent_prop_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "parent_prop_id", Value = pQueryEntity.parent_prop_id });
            if (pQueryEntity.prop_level!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "prop_level", Value = pQueryEntity.prop_level });
            if (pQueryEntity.prop_domain!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "prop_domain", Value = pQueryEntity.prop_domain });
            if (pQueryEntity.prop_input_flag!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "prop_input_flag", Value = pQueryEntity.prop_input_flag });
            if (pQueryEntity.prop_max_lenth!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "prop_max_lenth", Value = pQueryEntity.prop_max_lenth });
            if (pQueryEntity.prop_default_value!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "prop_default_value", Value = pQueryEntity.prop_default_value });
            if (pQueryEntity.status!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "status", Value = pQueryEntity.status });
            if (pQueryEntity.display_index!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "display_index", Value = pQueryEntity.display_index });
            if (pQueryEntity.create_user_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "create_user_id", Value = pQueryEntity.create_user_id });
            if (pQueryEntity.create_time!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "create_time", Value = pQueryEntity.create_time });
            if (pQueryEntity.modify_user_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "modify_user_id", Value = pQueryEntity.modify_user_id });
            if (pQueryEntity.modify_time!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "modify_time", Value = pQueryEntity.modify_time });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(IDataReader pReader, out T_PropEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new T_PropEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["prop_id"] != DBNull.Value)
			{
				pInstance.prop_id =  Convert.ToString(pReader["prop_id"]);
			}
			if (pReader["prop_code"] != DBNull.Value)
			{
				pInstance.prop_code =  Convert.ToString(pReader["prop_code"]);
			}
			if (pReader["prop_name"] != DBNull.Value)
			{
				pInstance.prop_name =  Convert.ToString(pReader["prop_name"]);
			}
			if (pReader["prop_eng_name"] != DBNull.Value)
			{
				pInstance.prop_eng_name =  Convert.ToString(pReader["prop_eng_name"]);
			}
			if (pReader["prop_type"] != DBNull.Value)
			{
				pInstance.prop_type =  Convert.ToString(pReader["prop_type"]);
			}
			if (pReader["parent_prop_id"] != DBNull.Value)
			{
				pInstance.parent_prop_id =  Convert.ToString(pReader["parent_prop_id"]);
			}
			if (pReader["prop_level"] != DBNull.Value)
			{
				pInstance.prop_level =   Convert.ToInt32(pReader["prop_level"]);
			}
			if (pReader["prop_domain"] != DBNull.Value)
			{
				pInstance.prop_domain =  Convert.ToString(pReader["prop_domain"]);
			}
			if (pReader["prop_input_flag"] != DBNull.Value)
			{
				pInstance.prop_input_flag =  Convert.ToString(pReader["prop_input_flag"]);
			}
			if (pReader["prop_max_lenth"] != DBNull.Value)
			{
				pInstance.prop_max_lenth =   Convert.ToInt32(pReader["prop_max_lenth"]);
			}
			if (pReader["prop_default_value"] != DBNull.Value)
			{
				pInstance.prop_default_value =  Convert.ToString(pReader["prop_default_value"]);
			}
			if (pReader["status"] != DBNull.Value)
			{
				pInstance.status =   Convert.ToInt32(pReader["status"]);
			}
			if (pReader["display_index"] != DBNull.Value)
			{
				pInstance.display_index =   Convert.ToInt32(pReader["display_index"]);
			}
			if (pReader["create_user_id"] != DBNull.Value)
			{
				pInstance.create_user_id =  Convert.ToString(pReader["create_user_id"]);
			}
			if (pReader["create_time"] != DBNull.Value)
			{
				pInstance.create_time =  Convert.ToString(pReader["create_time"]);
			}
			if (pReader["modify_user_id"] != DBNull.Value)
			{
				pInstance.modify_user_id =  Convert.ToString(pReader["modify_user_id"]);
			}
			if (pReader["modify_time"] != DBNull.Value)
			{
				pInstance.modify_time =  Convert.ToString(pReader["modify_time"]);
			}

        }
        #endregion
    }
}
