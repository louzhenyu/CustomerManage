/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/2/27 14:13:24
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
    /// ���ݷ��ʣ� �ͷ���Ϣ 
    /// ��CSMessage�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class CSMessageDAO : Base.BaseCPOSDAO, ICRUDable<CSMessageEntity>, IQueryable<CSMessageEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public CSMessageDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(CSMessageEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(CSMessageEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [CSMessage](");
            strSql.Append("[CSServiceTypeID],[CSPipelineID],[PipelineAccount],[Content],[MemberID],[MemberName],[CSObjectID],[DeviceToken],[CurrentCSID],[ConnectionTime],[ClientID],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[CSMessageID])");
            strSql.Append(" values (");
            strSql.Append("@CSServiceTypeID,@CSPipelineID,@PipelineAccount,@Content,@MemberID,@MemberName,@CSObjectID,@DeviceToken,@CurrentCSID,@ConnectionTime,@ClientID,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@CSMessageID)");            

			Guid? pkGuid;
			if (pEntity.CSMessageID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.CSMessageID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@CSServiceTypeID",SqlDbType.Int),
					new SqlParameter("@CSPipelineID",SqlDbType.Int),
					new SqlParameter("@PipelineAccount",SqlDbType.NVarChar),
					new SqlParameter("@Content",SqlDbType.NVarChar),
					new SqlParameter("@MemberID",SqlDbType.VarChar),
					new SqlParameter("@MemberName",SqlDbType.NVarChar),
					new SqlParameter("@CSObjectID",SqlDbType.NVarChar),
					new SqlParameter("@DeviceToken",SqlDbType.NVarChar),
					new SqlParameter("@CurrentCSID",SqlDbType.VarChar),
					new SqlParameter("@ConnectionTime",SqlDbType.DateTime),
					new SqlParameter("@ClientID",SqlDbType.NVarChar),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CSMessageID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.CSServiceTypeID;
			parameters[1].Value = pEntity.CSPipelineID;
			parameters[2].Value = pEntity.PipelineAccount;
			parameters[3].Value = pEntity.Content;
			parameters[4].Value = pEntity.MemberID;
			parameters[5].Value = pEntity.MemberName;
			parameters[6].Value = pEntity.CSObjectID;
			parameters[7].Value = pEntity.DeviceToken;
			parameters[8].Value = pEntity.CurrentCSID;
			parameters[9].Value = pEntity.ConnectionTime;
			parameters[10].Value = pEntity.ClientID;
			parameters[11].Value = pEntity.CreateBy;
			parameters[12].Value = pEntity.CreateTime;
			parameters[13].Value = pEntity.LastUpdateBy;
			parameters[14].Value = pEntity.LastUpdateTime;
			parameters[15].Value = pEntity.IsDelete;
			parameters[16].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.CSMessageID = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public CSMessageEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [CSMessage] where CSMessageID='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            CSMessageEntity m = null;
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
        public CSMessageEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [CSMessage] where isdelete=0");
            //��ȡ����
            List<CSMessageEntity> list = new List<CSMessageEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    CSMessageEntity m;
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
        public void Update(CSMessageEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(CSMessageEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.CSMessageID==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [CSMessage] set ");
            if (pIsUpdateNullField || pEntity.CSServiceTypeID!=null)
                strSql.Append( "[CSServiceTypeID]=@CSServiceTypeID,");
            if (pIsUpdateNullField || pEntity.CSPipelineID!=null)
                strSql.Append( "[CSPipelineID]=@CSPipelineID,");
            if (pIsUpdateNullField || pEntity.PipelineAccount!=null)
                strSql.Append( "[PipelineAccount]=@PipelineAccount,");
            if (pIsUpdateNullField || pEntity.Content!=null)
                strSql.Append( "[Content]=@Content,");
            if (pIsUpdateNullField || pEntity.MemberID!=null)
                strSql.Append( "[MemberID]=@MemberID,");
            if (pIsUpdateNullField || pEntity.MemberName!=null)
                strSql.Append( "[MemberName]=@MemberName,");
            if (pIsUpdateNullField || pEntity.CSObjectID!=null)
                strSql.Append( "[CSObjectID]=@CSObjectID,");
            if (pIsUpdateNullField || pEntity.DeviceToken!=null)
                strSql.Append( "[DeviceToken]=@DeviceToken,");
            if (pIsUpdateNullField || pEntity.CurrentCSID!=null)
                strSql.Append( "[CurrentCSID]=@CurrentCSID,");
            if (pIsUpdateNullField || pEntity.ConnectionTime!=null)
                strSql.Append( "[ConnectionTime]=@ConnectionTime,");
            if (pIsUpdateNullField || pEntity.ClientID!=null)
                strSql.Append( "[ClientID]=@ClientID,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where CSMessageID=@CSMessageID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@CSServiceTypeID",SqlDbType.Int),
					new SqlParameter("@CSPipelineID",SqlDbType.Int),
					new SqlParameter("@PipelineAccount",SqlDbType.NVarChar),
					new SqlParameter("@Content",SqlDbType.NVarChar),
					new SqlParameter("@MemberID",SqlDbType.VarChar),
					new SqlParameter("@MemberName",SqlDbType.NVarChar),
					new SqlParameter("@CSObjectID",SqlDbType.NVarChar),
					new SqlParameter("@DeviceToken",SqlDbType.NVarChar),
					new SqlParameter("@CurrentCSID",SqlDbType.VarChar),
					new SqlParameter("@ConnectionTime",SqlDbType.DateTime),
					new SqlParameter("@ClientID",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CSMessageID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.CSServiceTypeID;
			parameters[1].Value = pEntity.CSPipelineID;
			parameters[2].Value = pEntity.PipelineAccount;
			parameters[3].Value = pEntity.Content;
			parameters[4].Value = pEntity.MemberID;
			parameters[5].Value = pEntity.MemberName;
			parameters[6].Value = pEntity.CSObjectID;
			parameters[7].Value = pEntity.DeviceToken;
			parameters[8].Value = pEntity.CurrentCSID;
			parameters[9].Value = pEntity.ConnectionTime;
			parameters[10].Value = pEntity.ClientID;
			parameters[11].Value = pEntity.LastUpdateBy;
			parameters[12].Value = pEntity.LastUpdateTime;
			parameters[13].Value = pEntity.CSMessageID;

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
        public void Update(CSMessageEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(CSMessageEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(CSMessageEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(CSMessageEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.CSMessageID==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.CSMessageID, pTran);           
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
            sql.AppendLine("update [CSMessage] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where CSMessageID=@CSMessageID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@CSMessageID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(CSMessageEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.CSMessageID==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.CSMessageID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(CSMessageEntity[] pEntities)
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
            sql.AppendLine("update [CSMessage] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where CSMessageID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public CSMessageEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [CSMessage] where isdelete=0 ");
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
            List<CSMessageEntity> list = new List<CSMessageEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    CSMessageEntity m;
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
        public PagedQueryResult<CSMessageEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [CSMessageID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [CSMessage] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [CSMessage] where isdelete=0 ");
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
            PagedQueryResult<CSMessageEntity> result = new PagedQueryResult<CSMessageEntity>();
            List<CSMessageEntity> list = new List<CSMessageEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    CSMessageEntity m;
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
        public CSMessageEntity[] QueryByEntity(CSMessageEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<CSMessageEntity> PagedQueryByEntity(CSMessageEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(CSMessageEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.CSMessageID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CSMessageID", Value = pQueryEntity.CSMessageID });
            if (pQueryEntity.CSServiceTypeID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CSServiceTypeID", Value = pQueryEntity.CSServiceTypeID });
            if (pQueryEntity.CSPipelineID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CSPipelineID", Value = pQueryEntity.CSPipelineID });
            if (pQueryEntity.PipelineAccount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PipelineAccount", Value = pQueryEntity.PipelineAccount });
            if (pQueryEntity.Content!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Content", Value = pQueryEntity.Content });
            if (pQueryEntity.MemberID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MemberID", Value = pQueryEntity.MemberID });
            if (pQueryEntity.MemberName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MemberName", Value = pQueryEntity.MemberName });
            if (pQueryEntity.CSObjectID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CSObjectID", Value = pQueryEntity.CSObjectID });
            if (pQueryEntity.DeviceToken!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DeviceToken", Value = pQueryEntity.DeviceToken });
            if (pQueryEntity.CurrentCSID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CurrentCSID", Value = pQueryEntity.CurrentCSID });
            if (pQueryEntity.ConnectionTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ConnectionTime", Value = pQueryEntity.ConnectionTime });
            if (pQueryEntity.ClientID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ClientID", Value = pQueryEntity.ClientID });
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

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out CSMessageEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new CSMessageEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["CSMessageID"] != DBNull.Value)
			{
				pInstance.CSMessageID =  (Guid)pReader["CSMessageID"];
			}
			if (pReader["CSServiceTypeID"] != DBNull.Value)
			{
				pInstance.CSServiceTypeID =   Convert.ToInt32(pReader["CSServiceTypeID"]);
			}
			if (pReader["CSPipelineID"] != DBNull.Value)
			{
				pInstance.CSPipelineID =   Convert.ToInt32(pReader["CSPipelineID"]);
			}
			if (pReader["PipelineAccount"] != DBNull.Value)
			{
				pInstance.PipelineAccount =  Convert.ToString(pReader["PipelineAccount"]);
			}
			if (pReader["Content"] != DBNull.Value)
			{
				pInstance.Content =  Convert.ToString(pReader["Content"]);
			}
			if (pReader["MemberID"] != DBNull.Value)
			{
				pInstance.MemberID =  Convert.ToString(pReader["MemberID"]);
			}
			if (pReader["MemberName"] != DBNull.Value)
			{
				pInstance.MemberName =  Convert.ToString(pReader["MemberName"]);
			}
			if (pReader["CSObjectID"] != DBNull.Value)
			{
				pInstance.CSObjectID =  Convert.ToString(pReader["CSObjectID"]);
			}
			if (pReader["DeviceToken"] != DBNull.Value)
			{
				pInstance.DeviceToken =  Convert.ToString(pReader["DeviceToken"]);
			}
			if (pReader["CurrentCSID"] != DBNull.Value)
			{
				pInstance.CurrentCSID =  Convert.ToString(pReader["CurrentCSID"]);
			}
			if (pReader["ConnectionTime"] != DBNull.Value)
			{
				pInstance.ConnectionTime =  Convert.ToDateTime(pReader["ConnectionTime"]);
			}
			if (pReader["ClientID"] != DBNull.Value)
			{
				pInstance.ClientID =  Convert.ToString(pReader["ClientID"]);
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

        }
        #endregion
    }
}
