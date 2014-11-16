/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/9/17 14:26:19
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
    /// ��t_customer�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class t_customerDAO : Base.BaseCPOSDAO, ICRUDable<t_customerEntity>, IQueryable<t_customerEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public t_customerDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(t_customerEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(t_customerEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //��ʼ���̶��ֶ�


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [t_customer](");
            strSql.Append("[customer_code],[customer_name],[customer_address],[customer_post_code],[customer_contacter],[customer_tel],[customer_fax],[customer_email],[customer_cell],[customer_memo],[customer_status],[create_user_id],[create_time],[modify_user_id],[modify_time],[sys_modify_stamp],[customer_name_en],[create_user_name],[modify_user_name],[customer_start_date],[is_approve],[customer_id])");
            strSql.Append(" values (");
            strSql.Append("@customer_code,@customer_name,@customer_address,@customer_post_code,@customer_contacter,@customer_tel,@customer_fax,@customer_email,@customer_cell,@customer_memo,@customer_status,@create_user_id,@create_time,@modify_user_id,@modify_time,@sys_modify_stamp,@customer_name_en,@create_user_name,@modify_user_name,@customer_start_date,@is_approve,@customer_id)");            

			string pkString = pEntity.customer_id;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@customer_code",SqlDbType.VarChar),
					new SqlParameter("@customer_name",SqlDbType.VarChar),
					new SqlParameter("@customer_address",SqlDbType.VarChar),
					new SqlParameter("@customer_post_code",SqlDbType.VarChar),
					new SqlParameter("@customer_contacter",SqlDbType.VarChar),
					new SqlParameter("@customer_tel",SqlDbType.VarChar),
					new SqlParameter("@customer_fax",SqlDbType.VarChar),
					new SqlParameter("@customer_email",SqlDbType.VarChar),
					new SqlParameter("@customer_cell",SqlDbType.VarChar),
					new SqlParameter("@customer_memo",SqlDbType.VarChar),
					new SqlParameter("@customer_status",SqlDbType.Int),
					new SqlParameter("@create_user_id",SqlDbType.VarChar),
					new SqlParameter("@create_time",SqlDbType.DateTime),
					new SqlParameter("@modify_user_id",SqlDbType.VarChar),
					new SqlParameter("@modify_time",SqlDbType.DateTime),
					new SqlParameter("@sys_modify_stamp",SqlDbType.DateTime),
					new SqlParameter("@customer_name_en",SqlDbType.VarChar),
					new SqlParameter("@create_user_name",SqlDbType.VarChar),
					new SqlParameter("@modify_user_name",SqlDbType.VarChar),
					new SqlParameter("@customer_start_date",SqlDbType.VarChar),
					new SqlParameter("@is_approve",SqlDbType.NVarChar),
					new SqlParameter("@customer_id",SqlDbType.VarChar)
            };
			parameters[0].Value = pEntity.customer_code;
			parameters[1].Value = pEntity.customer_name;
			parameters[2].Value = pEntity.customer_address;
			parameters[3].Value = pEntity.customer_post_code;
			parameters[4].Value = pEntity.customer_contacter;
			parameters[5].Value = pEntity.customer_tel;
			parameters[6].Value = pEntity.customer_fax;
			parameters[7].Value = pEntity.customer_email;
			parameters[8].Value = pEntity.customer_cell;
			parameters[9].Value = pEntity.customer_memo;
			parameters[10].Value = pEntity.customer_status;
			parameters[11].Value = pEntity.create_user_id;
			parameters[12].Value = pEntity.create_time;
			parameters[13].Value = pEntity.modify_user_id;
			parameters[14].Value = pEntity.modify_time;
			parameters[15].Value = pEntity.sys_modify_stamp;
			parameters[16].Value = pEntity.customer_name_en;
			parameters[17].Value = pEntity.create_user_name;
			parameters[18].Value = pEntity.modify_user_name;
			parameters[19].Value = pEntity.customer_start_date;
			parameters[20].Value = pEntity.is_approve;
			parameters[21].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.customer_id = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public t_customerEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [t_customer] where customer_id='{0}'  ", id.ToString());
            //��ȡ����
            t_customerEntity m = null;
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
        public t_customerEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [t_customer] where 1=1 ");
            //��ȡ����
            List<t_customerEntity> list = new List<t_customerEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    t_customerEntity m;
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
        public void Update(t_customerEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(t_customerEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.customer_id == null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [t_customer] set ");
                        if (pIsUpdateNullField || pEntity.customer_code!=null)
                strSql.Append( "[customer_code]=@customer_code,");
            if (pIsUpdateNullField || pEntity.customer_name!=null)
                strSql.Append( "[customer_name]=@customer_name,");
            if (pIsUpdateNullField || pEntity.customer_address!=null)
                strSql.Append( "[customer_address]=@customer_address,");
            if (pIsUpdateNullField || pEntity.customer_post_code!=null)
                strSql.Append( "[customer_post_code]=@customer_post_code,");
            if (pIsUpdateNullField || pEntity.customer_contacter!=null)
                strSql.Append( "[customer_contacter]=@customer_contacter,");
            if (pIsUpdateNullField || pEntity.customer_tel!=null)
                strSql.Append( "[customer_tel]=@customer_tel,");
            if (pIsUpdateNullField || pEntity.customer_fax!=null)
                strSql.Append( "[customer_fax]=@customer_fax,");
            if (pIsUpdateNullField || pEntity.customer_email!=null)
                strSql.Append( "[customer_email]=@customer_email,");
            if (pIsUpdateNullField || pEntity.customer_cell!=null)
                strSql.Append( "[customer_cell]=@customer_cell,");
            if (pIsUpdateNullField || pEntity.customer_memo!=null)
                strSql.Append( "[customer_memo]=@customer_memo,");
            if (pIsUpdateNullField || pEntity.customer_status!=null)
                strSql.Append( "[customer_status]=@customer_status,");
            if (pIsUpdateNullField || pEntity.create_user_id!=null)
                strSql.Append( "[create_user_id]=@create_user_id,");
            if (pIsUpdateNullField || pEntity.create_time!=null)
                strSql.Append( "[create_time]=@create_time,");
            if (pIsUpdateNullField || pEntity.modify_user_id!=null)
                strSql.Append( "[modify_user_id]=@modify_user_id,");
            if (pIsUpdateNullField || pEntity.modify_time!=null)
                strSql.Append( "[modify_time]=@modify_time,");
            if (pIsUpdateNullField || pEntity.sys_modify_stamp!=null)
                strSql.Append( "[sys_modify_stamp]=@sys_modify_stamp,");
            if (pIsUpdateNullField || pEntity.customer_name_en!=null)
                strSql.Append( "[customer_name_en]=@customer_name_en,");
            if (pIsUpdateNullField || pEntity.create_user_name!=null)
                strSql.Append( "[create_user_name]=@create_user_name,");
            if (pIsUpdateNullField || pEntity.modify_user_name!=null)
                strSql.Append( "[modify_user_name]=@modify_user_name,");
            if (pIsUpdateNullField || pEntity.customer_start_date!=null)
                strSql.Append( "[customer_start_date]=@customer_start_date,");
            if (pIsUpdateNullField || pEntity.is_approve!=null)
                strSql.Append( "[is_approve]=@is_approve");
            strSql.Append(" where customer_id=@customer_id ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@customer_code",SqlDbType.VarChar),
					new SqlParameter("@customer_name",SqlDbType.VarChar),
					new SqlParameter("@customer_address",SqlDbType.VarChar),
					new SqlParameter("@customer_post_code",SqlDbType.VarChar),
					new SqlParameter("@customer_contacter",SqlDbType.VarChar),
					new SqlParameter("@customer_tel",SqlDbType.VarChar),
					new SqlParameter("@customer_fax",SqlDbType.VarChar),
					new SqlParameter("@customer_email",SqlDbType.VarChar),
					new SqlParameter("@customer_cell",SqlDbType.VarChar),
					new SqlParameter("@customer_memo",SqlDbType.VarChar),
					new SqlParameter("@customer_status",SqlDbType.Int),
					new SqlParameter("@create_user_id",SqlDbType.VarChar),
					new SqlParameter("@create_time",SqlDbType.DateTime),
					new SqlParameter("@modify_user_id",SqlDbType.VarChar),
					new SqlParameter("@modify_time",SqlDbType.DateTime),
					new SqlParameter("@sys_modify_stamp",SqlDbType.DateTime),
					new SqlParameter("@customer_name_en",SqlDbType.VarChar),
					new SqlParameter("@create_user_name",SqlDbType.VarChar),
					new SqlParameter("@modify_user_name",SqlDbType.VarChar),
					new SqlParameter("@customer_start_date",SqlDbType.VarChar),
					new SqlParameter("@is_approve",SqlDbType.NVarChar),
					new SqlParameter("@customer_id",SqlDbType.VarChar)
            };
			parameters[0].Value = pEntity.customer_code;
			parameters[1].Value = pEntity.customer_name;
			parameters[2].Value = pEntity.customer_address;
			parameters[3].Value = pEntity.customer_post_code;
			parameters[4].Value = pEntity.customer_contacter;
			parameters[5].Value = pEntity.customer_tel;
			parameters[6].Value = pEntity.customer_fax;
			parameters[7].Value = pEntity.customer_email;
			parameters[8].Value = pEntity.customer_cell;
			parameters[9].Value = pEntity.customer_memo;
			parameters[10].Value = pEntity.customer_status;
			parameters[11].Value = pEntity.create_user_id;
			parameters[12].Value = pEntity.create_time;
			parameters[13].Value = pEntity.modify_user_id;
			parameters[14].Value = pEntity.modify_time;
			parameters[15].Value = pEntity.sys_modify_stamp;
			parameters[16].Value = pEntity.customer_name_en;
			parameters[17].Value = pEntity.create_user_name;
			parameters[18].Value = pEntity.modify_user_name;
			parameters[19].Value = pEntity.customer_start_date;
			parameters[20].Value = pEntity.is_approve;
			parameters[21].Value = pEntity.customer_id;

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
        public void Update(t_customerEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(t_customerEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(t_customerEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.customer_id == null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.customer_id, pTran);           
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
            sql.AppendLine("update [t_customer] set  where customer_id=@customer_id;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@customer_id",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(t_customerEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (pEntity.customer_id == null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.customer_id;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(t_customerEntity[] pEntities)
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
            sql.AppendLine("update [t_customer] set  where customer_id in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public t_customerEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [t_customer] where 1=1  ");
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
            List<t_customerEntity> list = new List<t_customerEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    t_customerEntity m;
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
        public PagedQueryResult<t_customerEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [customer_id] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [t_customer] where 1=1  ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [t_customer] where 1=1  ");
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
            PagedQueryResult<t_customerEntity> result = new PagedQueryResult<t_customerEntity>();
            List<t_customerEntity> list = new List<t_customerEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    t_customerEntity m;
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
        public t_customerEntity[] QueryByEntity(t_customerEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<t_customerEntity> PagedQueryByEntity(t_customerEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(t_customerEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.customer_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customer_id", Value = pQueryEntity.customer_id });
            if (pQueryEntity.customer_code!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customer_code", Value = pQueryEntity.customer_code });
            if (pQueryEntity.customer_name!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customer_name", Value = pQueryEntity.customer_name });
            if (pQueryEntity.customer_address!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customer_address", Value = pQueryEntity.customer_address });
            if (pQueryEntity.customer_post_code!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customer_post_code", Value = pQueryEntity.customer_post_code });
            if (pQueryEntity.customer_contacter!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customer_contacter", Value = pQueryEntity.customer_contacter });
            if (pQueryEntity.customer_tel!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customer_tel", Value = pQueryEntity.customer_tel });
            if (pQueryEntity.customer_fax!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customer_fax", Value = pQueryEntity.customer_fax });
            if (pQueryEntity.customer_email!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customer_email", Value = pQueryEntity.customer_email });
            if (pQueryEntity.customer_cell!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customer_cell", Value = pQueryEntity.customer_cell });
            if (pQueryEntity.customer_memo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customer_memo", Value = pQueryEntity.customer_memo });
            if (pQueryEntity.customer_status!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customer_status", Value = pQueryEntity.customer_status });
            if (pQueryEntity.create_user_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "create_user_id", Value = pQueryEntity.create_user_id });
            if (pQueryEntity.create_time!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "create_time", Value = pQueryEntity.create_time });
            if (pQueryEntity.modify_user_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "modify_user_id", Value = pQueryEntity.modify_user_id });
            if (pQueryEntity.modify_time!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "modify_time", Value = pQueryEntity.modify_time });
            if (pQueryEntity.sys_modify_stamp!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "sys_modify_stamp", Value = pQueryEntity.sys_modify_stamp });
            if (pQueryEntity.customer_name_en!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customer_name_en", Value = pQueryEntity.customer_name_en });
            if (pQueryEntity.create_user_name!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "create_user_name", Value = pQueryEntity.create_user_name });
            if (pQueryEntity.modify_user_name!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "modify_user_name", Value = pQueryEntity.modify_user_name });
            if (pQueryEntity.customer_start_date!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customer_start_date", Value = pQueryEntity.customer_start_date });
            if (pQueryEntity.is_approve!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "is_approve", Value = pQueryEntity.is_approve });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(IDataReader pReader, out t_customerEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new t_customerEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["customer_id"] != DBNull.Value)
			{
				pInstance.customer_id =  Convert.ToString(pReader["customer_id"]);
			}
			if (pReader["customer_code"] != DBNull.Value)
			{
				pInstance.customer_code =  Convert.ToString(pReader["customer_code"]);
			}
			if (pReader["customer_name"] != DBNull.Value)
			{
				pInstance.customer_name =  Convert.ToString(pReader["customer_name"]);
			}
			if (pReader["customer_address"] != DBNull.Value)
			{
				pInstance.customer_address =  Convert.ToString(pReader["customer_address"]);
			}
			if (pReader["customer_post_code"] != DBNull.Value)
			{
				pInstance.customer_post_code =  Convert.ToString(pReader["customer_post_code"]);
			}
			if (pReader["customer_contacter"] != DBNull.Value)
			{
				pInstance.customer_contacter =  Convert.ToString(pReader["customer_contacter"]);
			}
			if (pReader["customer_tel"] != DBNull.Value)
			{
				pInstance.customer_tel =  Convert.ToString(pReader["customer_tel"]);
			}
			if (pReader["customer_fax"] != DBNull.Value)
			{
				pInstance.customer_fax =  Convert.ToString(pReader["customer_fax"]);
			}
			if (pReader["customer_email"] != DBNull.Value)
			{
				pInstance.customer_email =  Convert.ToString(pReader["customer_email"]);
			}
			if (pReader["customer_cell"] != DBNull.Value)
			{
				pInstance.customer_cell =  Convert.ToString(pReader["customer_cell"]);
			}
			if (pReader["customer_memo"] != DBNull.Value)
			{
				pInstance.customer_memo =  Convert.ToString(pReader["customer_memo"]);
			}
			if (pReader["customer_status"] != DBNull.Value)
			{
				pInstance.customer_status =   Convert.ToInt32(pReader["customer_status"]);
			}
			if (pReader["create_user_id"] != DBNull.Value)
			{
				pInstance.create_user_id =  Convert.ToString(pReader["create_user_id"]);
			}
			if (pReader["create_time"] != DBNull.Value)
			{
				pInstance.create_time =  Convert.ToDateTime(pReader["create_time"]);
			}
			if (pReader["modify_user_id"] != DBNull.Value)
			{
				pInstance.modify_user_id =  Convert.ToString(pReader["modify_user_id"]);
			}
			if (pReader["modify_time"] != DBNull.Value)
			{
				pInstance.modify_time =  Convert.ToDateTime(pReader["modify_time"]);
			}
			if (pReader["sys_modify_stamp"] != DBNull.Value)
			{
				pInstance.sys_modify_stamp =  Convert.ToDateTime(pReader["sys_modify_stamp"]);
			}
			if (pReader["customer_name_en"] != DBNull.Value)
			{
				pInstance.customer_name_en =  Convert.ToString(pReader["customer_name_en"]);
			}
			if (pReader["create_user_name"] != DBNull.Value)
			{
				pInstance.create_user_name =  Convert.ToString(pReader["create_user_name"]);
			}
			if (pReader["modify_user_name"] != DBNull.Value)
			{
				pInstance.modify_user_name =  Convert.ToString(pReader["modify_user_name"]);
			}
			if (pReader["customer_start_date"] != DBNull.Value)
			{
				pInstance.customer_start_date =  Convert.ToString(pReader["customer_start_date"]);
			}
			if (pReader["is_approve"] != DBNull.Value)
			{
				pInstance.is_approve =  Convert.ToString(pReader["is_approve"]);
			}

        }
        #endregion
    }
}
