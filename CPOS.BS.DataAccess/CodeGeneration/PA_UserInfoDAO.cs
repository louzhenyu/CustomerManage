/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/17 11:49:55
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
using CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess.Base;
using JIT.CPOS.BS.Entity;

namespace CPOS.BS.DataAccess
{
    /// <summary>
    /// ���ݷ��ʣ�  
    /// ��PA_UserInfo�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class PA_UserInfoDAO : BaseCPOSDAO, ICRUDable<PA_UserInfoEntity>, IQueryable<PA_UserInfoEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public PA_UserInfoDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(PA_UserInfoEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(PA_UserInfoEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //��ʼ���̶��ֶ�
			pEntity.CreateTime=DateTime.Now;


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [PA_UserInfo](");
            strSql.Append("[AgentNo],[AgentType],[CreateTime],[CustomerId],[EmpType],[alias],[Field1],[Field2],[Field3],[Field4],[Field5],[OpenId])");
            strSql.Append(" values (");
            strSql.Append("@AgentNo,@AgentType,@CreateTime,@CustomerId,@EmpType,@alias,@Field1,@Field2,@Field3,@Field4,@Field5,@OpenId)");            

			string pkString = pEntity.OpenId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@AgentNo",SqlDbType.NVarChar),
					new SqlParameter("@AgentType",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@EmpType",SqlDbType.NVarChar),
					new SqlParameter("@alias",SqlDbType.NVarChar),
					new SqlParameter("@Field1",SqlDbType.NVarChar),
					new SqlParameter("@Field2",SqlDbType.NVarChar),
					new SqlParameter("@Field3",SqlDbType.NVarChar),
					new SqlParameter("@Field4",SqlDbType.NVarChar),
					new SqlParameter("@Field5",SqlDbType.NVarChar),
					new SqlParameter("@OpenId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.AgentNo;
			parameters[1].Value = pEntity.AgentType;
			parameters[2].Value = pEntity.CreateTime;
			parameters[3].Value = pEntity.CustomerId;
			parameters[4].Value = pEntity.EmpType;
			parameters[5].Value = pEntity.alias;
			parameters[6].Value = pEntity.Field1;
			parameters[7].Value = pEntity.Field2;
			parameters[8].Value = pEntity.Field3;
			parameters[9].Value = pEntity.Field4;
			parameters[10].Value = pEntity.Field5;
			parameters[11].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.OpenId = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public PA_UserInfoEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [PA_UserInfo] where OpenId='{0}'  ", id.ToString());
            //��ȡ����
            PA_UserInfoEntity m = null;
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
        public PA_UserInfoEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [PA_UserInfo] where 1=1 ");
            //��ȡ����
            List<PA_UserInfoEntity> list = new List<PA_UserInfoEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    PA_UserInfoEntity m;
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
        public void Update(PA_UserInfoEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(PA_UserInfoEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.OpenId == null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [PA_UserInfo] set ");
                        if (pIsUpdateNullField || pEntity.AgentNo!=null)
                strSql.Append( "[AgentNo]=@AgentNo,");
            if (pIsUpdateNullField || pEntity.AgentType!=null)
                strSql.Append( "[AgentType]=@AgentType,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.EmpType!=null)
                strSql.Append( "[EmpType]=@EmpType,");
            if (pIsUpdateNullField || pEntity.alias!=null)
                strSql.Append( "[alias]=@alias,");
            if (pIsUpdateNullField || pEntity.Field1!=null)
                strSql.Append( "[Field1]=@Field1,");
            if (pIsUpdateNullField || pEntity.Field2!=null)
                strSql.Append( "[Field2]=@Field2,");
            if (pIsUpdateNullField || pEntity.Field3!=null)
                strSql.Append( "[Field3]=@Field3,");
            if (pIsUpdateNullField || pEntity.Field4!=null)
                strSql.Append( "[Field4]=@Field4,");
            if (pIsUpdateNullField || pEntity.Field5!=null)
                strSql.Append( "[Field5]=@Field5");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where OpenId=@OpenId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@AgentNo",SqlDbType.NVarChar),
					new SqlParameter("@AgentType",SqlDbType.NVarChar),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@EmpType",SqlDbType.NVarChar),
					new SqlParameter("@alias",SqlDbType.NVarChar),
					new SqlParameter("@Field1",SqlDbType.NVarChar),
					new SqlParameter("@Field2",SqlDbType.NVarChar),
					new SqlParameter("@Field3",SqlDbType.NVarChar),
					new SqlParameter("@Field4",SqlDbType.NVarChar),
					new SqlParameter("@Field5",SqlDbType.NVarChar),
					new SqlParameter("@OpenId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.AgentNo;
			parameters[1].Value = pEntity.AgentType;
			parameters[2].Value = pEntity.CustomerId;
			parameters[3].Value = pEntity.EmpType;
			parameters[4].Value = pEntity.alias;
			parameters[5].Value = pEntity.Field1;
			parameters[6].Value = pEntity.Field2;
			parameters[7].Value = pEntity.Field3;
			parameters[8].Value = pEntity.Field4;
			parameters[9].Value = pEntity.Field5;
			parameters[10].Value = pEntity.OpenId;

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
        public void Update(PA_UserInfoEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(PA_UserInfoEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(PA_UserInfoEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.OpenId == null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.OpenId, pTran);           
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
            sql.AppendLine("update [PA_UserInfo] set  where OpenId=@OpenId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@OpenId",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(PA_UserInfoEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (pEntity.OpenId == null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.OpenId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(PA_UserInfoEntity[] pEntities)
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
            sql.AppendLine("update [PA_UserInfo] set  where OpenId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public PA_UserInfoEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [PA_UserInfo] where 1=1  ");
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
            List<PA_UserInfoEntity> list = new List<PA_UserInfoEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    PA_UserInfoEntity m;
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
        public PagedQueryResult<PA_UserInfoEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [OpenId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [PA_UserInfo] where 1=1  ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [PA_UserInfo] where 1=1  ");
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
            PagedQueryResult<PA_UserInfoEntity> result = new PagedQueryResult<PA_UserInfoEntity>();
            List<PA_UserInfoEntity> list = new List<PA_UserInfoEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    PA_UserInfoEntity m;
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
        public PA_UserInfoEntity[] QueryByEntity(PA_UserInfoEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<PA_UserInfoEntity> PagedQueryByEntity(PA_UserInfoEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(PA_UserInfoEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.OpenId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OpenId", Value = pQueryEntity.OpenId });
            if (pQueryEntity.AgentNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AgentNo", Value = pQueryEntity.AgentNo });
            if (pQueryEntity.AgentType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AgentType", Value = pQueryEntity.AgentType });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
            if (pQueryEntity.EmpType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EmpType", Value = pQueryEntity.EmpType });
            if (pQueryEntity.alias!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "alias", Value = pQueryEntity.alias });
            if (pQueryEntity.Field1!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field1", Value = pQueryEntity.Field1 });
            if (pQueryEntity.Field2!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field2", Value = pQueryEntity.Field2 });
            if (pQueryEntity.Field3!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field3", Value = pQueryEntity.Field3 });
            if (pQueryEntity.Field4!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field4", Value = pQueryEntity.Field4 });
            if (pQueryEntity.Field5!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field5", Value = pQueryEntity.Field5 });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(IDataReader pReader, out PA_UserInfoEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new PA_UserInfoEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["OpenId"] != DBNull.Value)
			{
				pInstance.OpenId =  Convert.ToString(pReader["OpenId"]);
			}
			if (pReader["AgentNo"] != DBNull.Value)
			{
				pInstance.AgentNo =  Convert.ToString(pReader["AgentNo"]);
			}
			if (pReader["AgentType"] != DBNull.Value)
			{
				pInstance.AgentType =  Convert.ToString(pReader["AgentType"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}
			if (pReader["EmpType"] != DBNull.Value)
			{
				pInstance.EmpType =  Convert.ToString(pReader["EmpType"]);
			}
			if (pReader["alias"] != DBNull.Value)
			{
				pInstance.alias =  Convert.ToString(pReader["alias"]);
			}
			if (pReader["Field1"] != DBNull.Value)
			{
				pInstance.Field1 =  Convert.ToString(pReader["Field1"]);
			}
			if (pReader["Field2"] != DBNull.Value)
			{
				pInstance.Field2 =  Convert.ToString(pReader["Field2"]);
			}
			if (pReader["Field3"] != DBNull.Value)
			{
				pInstance.Field3 =  Convert.ToString(pReader["Field3"]);
			}
			if (pReader["Field4"] != DBNull.Value)
			{
				pInstance.Field4 =  Convert.ToString(pReader["Field4"]);
			}
			if (pReader["Field5"] != DBNull.Value)
			{
				pInstance.Field5 =  Convert.ToString(pReader["Field5"]);
			}

        }
        #endregion
    }
}
