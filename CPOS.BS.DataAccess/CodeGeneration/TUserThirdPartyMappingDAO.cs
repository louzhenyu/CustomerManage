/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/23 12:26:34
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
    /// ��TUserThirdPartyMapping�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class TUserThirdPartyMappingDAO : Base.BaseCPOSDAO, ICRUDable<TUserThirdPartyMappingEntity>, IQueryable<TUserThirdPartyMappingEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public TUserThirdPartyMappingDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(TUserThirdPartyMappingEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(TUserThirdPartyMappingEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [TUserThirdPartyMapping](");
            strSql.Append("[StatusCode],[SubAccountSid],[SubToken],[DateCreated],[VoipAccount],[VoipPwd],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[UserId])");
            strSql.Append(" values (");
            strSql.Append("@StatusCode,@SubAccountSid,@SubToken,@DateCreated,@VoipAccount,@VoipPwd,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@UserId)");            

			string pkString = pEntity.UserId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@StatusCode",SqlDbType.NVarChar),
					new SqlParameter("@SubAccountSid",SqlDbType.NVarChar),
					new SqlParameter("@SubToken",SqlDbType.NVarChar),
					new SqlParameter("@DateCreated",SqlDbType.NVarChar),
					new SqlParameter("@VoipAccount",SqlDbType.NVarChar),
					new SqlParameter("@VoipPwd",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@UserId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.StatusCode;
			parameters[1].Value = pEntity.SubAccountSid;
			parameters[2].Value = pEntity.SubToken;
			parameters[3].Value = pEntity.DateCreated;
			parameters[4].Value = pEntity.VoipAccount;
			parameters[5].Value = pEntity.VoipPwd;
			parameters[6].Value = pEntity.CreateTime;
			parameters[7].Value = pEntity.CreateBy;
			parameters[8].Value = pEntity.LastUpdateBy;
			parameters[9].Value = pEntity.LastUpdateTime;
			parameters[10].Value = pEntity.IsDelete;
			parameters[11].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.UserId = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public TUserThirdPartyMappingEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [TUserThirdPartyMapping] where UserId='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            TUserThirdPartyMappingEntity m = null;
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
        public TUserThirdPartyMappingEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [TUserThirdPartyMapping] where isdelete=0");
            //��ȡ����
            List<TUserThirdPartyMappingEntity> list = new List<TUserThirdPartyMappingEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    TUserThirdPartyMappingEntity m;
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
        public void Update(TUserThirdPartyMappingEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(TUserThirdPartyMappingEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.UserId==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [TUserThirdPartyMapping] set ");
            if (pIsUpdateNullField || pEntity.StatusCode!=null)
                strSql.Append( "[StatusCode]=@StatusCode,");
            if (pIsUpdateNullField || pEntity.SubAccountSid!=null)
                strSql.Append( "[SubAccountSid]=@SubAccountSid,");
            if (pIsUpdateNullField || pEntity.SubToken!=null)
                strSql.Append( "[SubToken]=@SubToken,");
            if (pIsUpdateNullField || pEntity.DateCreated!=null)
                strSql.Append( "[DateCreated]=@DateCreated,");
            if (pIsUpdateNullField || pEntity.VoipAccount!=null)
                strSql.Append( "[VoipAccount]=@VoipAccount,");
            if (pIsUpdateNullField || pEntity.VoipPwd!=null)
                strSql.Append( "[VoipPwd]=@VoipPwd,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where UserId=@UserId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@StatusCode",SqlDbType.NVarChar),
					new SqlParameter("@SubAccountSid",SqlDbType.NVarChar),
					new SqlParameter("@SubToken",SqlDbType.NVarChar),
					new SqlParameter("@DateCreated",SqlDbType.NVarChar),
					new SqlParameter("@VoipAccount",SqlDbType.NVarChar),
					new SqlParameter("@VoipPwd",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@UserId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.StatusCode;
			parameters[1].Value = pEntity.SubAccountSid;
			parameters[2].Value = pEntity.SubToken;
			parameters[3].Value = pEntity.DateCreated;
			parameters[4].Value = pEntity.VoipAccount;
			parameters[5].Value = pEntity.VoipPwd;
			parameters[6].Value = pEntity.LastUpdateBy;
			parameters[7].Value = pEntity.LastUpdateTime;
			parameters[8].Value = pEntity.UserId;

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
        public void Update(TUserThirdPartyMappingEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(TUserThirdPartyMappingEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(TUserThirdPartyMappingEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(TUserThirdPartyMappingEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.UserId==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.UserId, pTran);           
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
            sql.AppendLine("update [TUserThirdPartyMapping] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where UserId=@UserId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@UserId",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(TUserThirdPartyMappingEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.UserId==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.UserId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(TUserThirdPartyMappingEntity[] pEntities)
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
            sql.AppendLine("update [TUserThirdPartyMapping] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where UserId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public TUserThirdPartyMappingEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [TUserThirdPartyMapping] where isdelete=0 ");
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
            List<TUserThirdPartyMappingEntity> list = new List<TUserThirdPartyMappingEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    TUserThirdPartyMappingEntity m;
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
        public PagedQueryResult<TUserThirdPartyMappingEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [UserId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [TUserThirdPartyMapping] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [TUserThirdPartyMapping] where isdelete=0 ");
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
            PagedQueryResult<TUserThirdPartyMappingEntity> result = new PagedQueryResult<TUserThirdPartyMappingEntity>();
            List<TUserThirdPartyMappingEntity> list = new List<TUserThirdPartyMappingEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    TUserThirdPartyMappingEntity m;
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
        public TUserThirdPartyMappingEntity[] QueryByEntity(TUserThirdPartyMappingEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<TUserThirdPartyMappingEntity> PagedQueryByEntity(TUserThirdPartyMappingEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(TUserThirdPartyMappingEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.UserId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UserId", Value = pQueryEntity.UserId });
            if (pQueryEntity.StatusCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StatusCode", Value = pQueryEntity.StatusCode });
            if (pQueryEntity.SubAccountSid!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SubAccountSid", Value = pQueryEntity.SubAccountSid });
            if (pQueryEntity.SubToken!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SubToken", Value = pQueryEntity.SubToken });
            if (pQueryEntity.DateCreated!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DateCreated", Value = pQueryEntity.DateCreated });
            if (pQueryEntity.VoipAccount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VoipAccount", Value = pQueryEntity.VoipAccount });
            if (pQueryEntity.VoipPwd!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VoipPwd", Value = pQueryEntity.VoipPwd });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.LastUpdateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.LastUpdateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.IsDelete!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out TUserThirdPartyMappingEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new TUserThirdPartyMappingEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["UserId"] != DBNull.Value)
			{
				pInstance.UserId =  Convert.ToString(pReader["UserId"]);
			}
			if (pReader["StatusCode"] != DBNull.Value)
			{
				pInstance.StatusCode =  Convert.ToString(pReader["StatusCode"]);
			}
			if (pReader["SubAccountSid"] != DBNull.Value)
			{
				pInstance.SubAccountSid =  Convert.ToString(pReader["SubAccountSid"]);
			}
			if (pReader["SubToken"] != DBNull.Value)
			{
				pInstance.SubToken =  Convert.ToString(pReader["SubToken"]);
			}
			if (pReader["DateCreated"] != DBNull.Value)
			{
				pInstance.DateCreated =  Convert.ToString(pReader["DateCreated"]);
			}
			if (pReader["VoipAccount"] != DBNull.Value)
			{
				pInstance.VoipAccount =  Convert.ToString(pReader["VoipAccount"]);
			}
			if (pReader["VoipPwd"] != DBNull.Value)
			{
				pInstance.VoipPwd =  Convert.ToString(pReader["VoipPwd"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}
			if (pReader["CreateBy"] != DBNull.Value)
			{
				pInstance.CreateBy =  Convert.ToString(pReader["CreateBy"]);
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

        }
        #endregion
    }
}
