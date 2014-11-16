/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/5 18:00:39
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
    /// ���ݷ��ʣ� ������¼ 
    /// ��CSConversation�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class CSConversationDAO : Base.BaseCPOSDAO, ICRUDable<CSConversationEntity>, IQueryable<CSConversationEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public CSConversationDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(CSConversationEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(CSConversationEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [CSConversation](");
            strSql.Append("[CSMessageID],[CSQueueID],[MessageTypeID],[Content],[PersonID],[Person],[IsCS],[IsPush],[ClientID],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[ContentTypeID],[HeadImageUrl],[TimeStamp],[OpenId],[CSConversationID])");
            strSql.Append(" values (");
            strSql.Append("@CSMessageID,@CSQueueID,@MessageTypeID,@Content,@PersonID,@Person,@IsCS,@IsPush,@ClientID,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@ContentTypeID,@HeadImageUrl,@TimeStamp,@OpenId,@CSConversationID)");

            Guid? pkGuid;
            if (pEntity.CSConversationID == null)
                pkGuid = Guid.NewGuid();
            else
                pkGuid = pEntity.CSConversationID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@CSMessageID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@CSQueueID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@MessageTypeID",SqlDbType.Int),
					new SqlParameter("@Content",SqlDbType.NVarChar),
					new SqlParameter("@PersonID",SqlDbType.NVarChar),
					new SqlParameter("@Person",SqlDbType.NVarChar),
					new SqlParameter("@IsCS",SqlDbType.Int),
					new SqlParameter("@IsPush",SqlDbType.Int),
					new SqlParameter("@ClientID",SqlDbType.NVarChar),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@ContentTypeID",SqlDbType.Int),
					new SqlParameter("@HeadImageUrl",SqlDbType.VarChar),
					new SqlParameter("@TimeStamp",SqlDbType.BigInt),
					new SqlParameter("@OpenId",SqlDbType.VarChar),
					new SqlParameter("@CSConversationID",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.CSMessageID;
            parameters[1].Value = pEntity.CSQueueID;
            parameters[2].Value = pEntity.MessageTypeID;
            parameters[3].Value = pEntity.Content;
            parameters[4].Value = pEntity.PersonID;
            parameters[5].Value = pEntity.Person;
            parameters[6].Value = pEntity.IsCS;
            parameters[7].Value = pEntity.IsPush;
            parameters[8].Value = pEntity.ClientID;
            parameters[9].Value = pEntity.CreateBy;
            parameters[10].Value = pEntity.CreateTime;
            parameters[11].Value = pEntity.LastUpdateBy;
            parameters[12].Value = pEntity.LastUpdateTime;
            parameters[13].Value = pEntity.IsDelete;
            parameters[14].Value = pEntity.ContentTypeID;
            parameters[15].Value = pEntity.HeadImageUrl;
            parameters[16].Value = pEntity.TimeStamp;
            parameters[17].Value = pEntity.OpenId;
            parameters[18].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            pEntity.CSConversationID = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public CSConversationEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [CSConversation] where CSConversationID='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            CSConversationEntity m = null;
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
        public CSConversationEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [CSConversation] where isdelete=0");
            //��ȡ����
            List<CSConversationEntity> list = new List<CSConversationEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    CSConversationEntity m;
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
        public void Update(CSConversationEntity pEntity, IDbTransaction pTran)
        {
            Update(pEntity, true, pTran);
        }
        public void Update(CSConversationEntity pEntity, bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.CSConversationID == null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [CSConversation] set ");
            if (pIsUpdateNullField || pEntity.CSMessageID != null)
                strSql.Append("[CSMessageID]=@CSMessageID,");
            if (pIsUpdateNullField || pEntity.CSQueueID != null)
                strSql.Append("[CSQueueID]=@CSQueueID,");
            if (pIsUpdateNullField || pEntity.MessageTypeID != null)
                strSql.Append("[MessageTypeID]=@MessageTypeID,");
            if (pIsUpdateNullField || pEntity.Content != null)
                strSql.Append("[Content]=@Content,");
            if (pIsUpdateNullField || pEntity.PersonID != null)
                strSql.Append("[PersonID]=@PersonID,");
            if (pIsUpdateNullField || pEntity.Person != null)
                strSql.Append("[Person]=@Person,");
            if (pIsUpdateNullField || pEntity.IsCS != null)
                strSql.Append("[IsCS]=@IsCS,");
            if (pIsUpdateNullField || pEntity.IsPush != null)
                strSql.Append("[IsPush]=@IsPush,");
            if (pIsUpdateNullField || pEntity.ClientID != null)
                strSql.Append("[ClientID]=@ClientID,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy != null)
                strSql.Append("[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime != null)
                strSql.Append("[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.ContentTypeID != null)
                strSql.Append("[ContentTypeID]=@ContentTypeID,");
            if (pIsUpdateNullField || pEntity.HeadImageUrl != null)
                strSql.Append("[HeadImageUrl]=@HeadImageUrl,");
            if (pIsUpdateNullField || pEntity.TimeStamp != null)
                strSql.Append("[TimeStamp]=@TimeStamp,");
            if (pIsUpdateNullField || pEntity.OpenId != null)
                strSql.Append("[OpenId]=@OpenId");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where CSConversationID=@CSConversationID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@CSMessageID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@CSQueueID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@MessageTypeID",SqlDbType.Int),
					new SqlParameter("@Content",SqlDbType.NVarChar),
					new SqlParameter("@PersonID",SqlDbType.NVarChar),
					new SqlParameter("@Person",SqlDbType.NVarChar),
					new SqlParameter("@IsCS",SqlDbType.Int),
					new SqlParameter("@IsPush",SqlDbType.Int),
					new SqlParameter("@ClientID",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@ContentTypeID",SqlDbType.Int),
					new SqlParameter("@HeadImageUrl",SqlDbType.VarChar),
					new SqlParameter("@TimeStamp",SqlDbType.BigInt),
					new SqlParameter("@OpenId",SqlDbType.VarChar),
					new SqlParameter("@CSConversationID",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.CSMessageID;
            parameters[1].Value = pEntity.CSQueueID;
            parameters[2].Value = pEntity.MessageTypeID;
            parameters[3].Value = pEntity.Content;
            parameters[4].Value = pEntity.PersonID;
            parameters[5].Value = pEntity.Person;
            parameters[6].Value = pEntity.IsCS;
            parameters[7].Value = pEntity.IsPush;
            parameters[8].Value = pEntity.ClientID;
            parameters[9].Value = pEntity.LastUpdateBy;
            parameters[10].Value = pEntity.LastUpdateTime;
            parameters[11].Value = pEntity.ContentTypeID;
            parameters[12].Value = pEntity.HeadImageUrl;
            parameters[13].Value = pEntity.TimeStamp;
            parameters[14].Value = pEntity.OpenId;
            parameters[15].Value = pEntity.CSConversationID;

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
        public void Update(CSConversationEntity pEntity)
        {
            Update(pEntity, true);
        }
        public void Update(CSConversationEntity pEntity, bool pIsUpdateNullField)
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(CSConversationEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(CSConversationEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.CSConversationID == null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.CSConversationID, pTran);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(object pID, IDbTransaction pTran)
        {
            if (pID == null)
                return;
            //��֯������SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [CSConversation] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where CSConversationID=@CSConversationID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@CSConversationID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
            };
            //ִ�����
            int result = 0;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), parameters);
            return;
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(CSConversationEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.CSConversationID == null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.CSConversationID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(CSConversationEntity[] pEntities)
        {
            Delete(pEntities, null);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pIDs">��ʶ��ֵ����</param>
        public void Delete(object[] pIDs)
        {
            Delete(pIDs, null);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pIDs">��ʶ��ֵ����</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(object[] pIDs, IDbTransaction pTran)
        {
            if (pIDs == null || pIDs.Length == 0)
                return;
            //��֯������SQL
            StringBuilder primaryKeys = new StringBuilder();
            foreach (object item in pIDs)
            {
                primaryKeys.AppendFormat("'{0}',", item.ToString());
            }
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [CSConversation] set LastUpdateTime='" + DateTime.Now.ToString() + "',LastUpdateBy='" + CurrentUserInfo.UserID + "',IsDelete=1 where CSConversationID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
            //ִ�����
            int result = 0;
            if (pTran == null)
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), null);
            else
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString());
        }
        #endregion

        #region IQueryable ��Ա
        /// <summary>
        /// ִ�в�ѯ
        /// </summary>
        /// <param name="pWhereConditions">ɸѡ����</param>
        /// <param name="pOrderBys">����</param>
        /// <returns></returns>
        public CSConversationEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [CSConversation] where isdelete=0 ");
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
            List<CSConversationEntity> list = new List<CSConversationEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    CSConversationEntity m;
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
        public PagedQueryResult<CSConversationEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                    if (item != null)
                    {
                        pagedSql.AppendFormat(" {0} {1},", StringUtils.WrapperSQLServerObject(item.FieldName), item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                    }
                }
                pagedSql.Remove(pagedSql.Length - 1, 1);
            }
            else
            {
                pagedSql.AppendFormat(" [CSConversationID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [CSConversation] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [CSConversation] where isdelete=0 ");
            //��������
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    if (item != null)
                    {
                        pagedSql.AppendFormat(" and {0}", item.GetExpression());
                        totalCountSql.AppendFormat(" and {0}", item.GetExpression());
                    }
                }
            }
            pagedSql.AppendFormat(") as A ");
            //ȡָ��ҳ������
            pagedSql.AppendFormat(" where ___rn >{0} and ___rn <={1}", pPageSize * (pCurrentPageIndex - 1), pPageSize * (pCurrentPageIndex));
            //ִ����䲢���ؽ��
            PagedQueryResult<CSConversationEntity> result = new PagedQueryResult<CSConversationEntity>();
            List<CSConversationEntity> list = new List<CSConversationEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    CSConversationEntity m;
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
        public CSConversationEntity[] QueryByEntity(CSConversationEntity pQueryEntity, OrderBy[] pOrderBys)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity(pQueryEntity);
            return Query(queryWhereCondition, pOrderBys);
        }

        /// <summary>
        /// ��ҳ����ʵ��������ѯʵ��
        /// </summary>
        /// <param name="pQueryEntity">��ʵ����ʽ����Ĳ���</param>
        /// <param name="pOrderBys">�������</param>
        /// <returns>����������ʵ�弯</returns>
        public PagedQueryResult<CSConversationEntity> PagedQueryByEntity(CSConversationEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity(pQueryEntity);
            return PagedQuery(queryWhereCondition, pOrderBys, pPageSize, pCurrentPageIndex);
        }

        #endregion

        #region ���߷���
        /// <summary>
        /// ����ʵ���Null�������ɲ�ѯ������
        /// </summary>
        /// <returns></returns>
        protected IWhereCondition[] GetWhereConditionByEntity(CSConversationEntity pQueryEntity)
        {
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.CSConversationID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CSConversationID", Value = pQueryEntity.CSConversationID });
            if (pQueryEntity.CSMessageID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CSMessageID", Value = pQueryEntity.CSMessageID });
            if (pQueryEntity.CSQueueID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CSQueueID", Value = pQueryEntity.CSQueueID });
            if (pQueryEntity.MessageTypeID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MessageTypeID", Value = pQueryEntity.MessageTypeID });
            if (pQueryEntity.Content != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Content", Value = pQueryEntity.Content });
            if (pQueryEntity.PersonID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PersonID", Value = pQueryEntity.PersonID });
            if (pQueryEntity.Person != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Person", Value = pQueryEntity.Person });
            if (pQueryEntity.IsCS != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsCS", Value = pQueryEntity.IsCS });
            if (pQueryEntity.IsPush != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsPush", Value = pQueryEntity.IsPush });
            if (pQueryEntity.ClientID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ClientID", Value = pQueryEntity.ClientID });
            if (pQueryEntity.CreateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.CreateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.LastUpdateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.LastUpdateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.IsDelete != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });
            if (pQueryEntity.ContentTypeID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ContentTypeID", Value = pQueryEntity.ContentTypeID });
            if (pQueryEntity.HeadImageUrl != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "HeadImageUrl", Value = pQueryEntity.HeadImageUrl });
            if (pQueryEntity.TimeStamp != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TimeStamp", Value = pQueryEntity.TimeStamp });
            if (pQueryEntity.OpenId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OpenId", Value = pQueryEntity.OpenId });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out CSConversationEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new CSConversationEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["CSConversationID"] != DBNull.Value)
            {
                pInstance.CSConversationID = (Guid)pReader["CSConversationID"];
            }
            if (pReader["CSMessageID"] != DBNull.Value)
            {
                pInstance.CSMessageID = (Guid)pReader["CSMessageID"];
            }
            if (pReader["CSQueueID"] != DBNull.Value)
            {
                pInstance.CSQueueID = (Guid)pReader["CSQueueID"];
            }
            if (pReader["MessageTypeID"] != DBNull.Value)
            {
                pInstance.MessageTypeID = Convert.ToInt32(pReader["MessageTypeID"]);
            }
            if (pReader["Content"] != DBNull.Value)
            {
                pInstance.Content = Convert.ToString(pReader["Content"]);
            }
            if (pReader["PersonID"] != DBNull.Value)
            {
                pInstance.PersonID = Convert.ToString(pReader["PersonID"]);
            }
            if (pReader["Person"] != DBNull.Value)
            {
                pInstance.Person = Convert.ToString(pReader["Person"]);
            }
            if (pReader["IsCS"] != DBNull.Value)
            {
                pInstance.IsCS = Convert.ToInt32(pReader["IsCS"]);
            }
            if (pReader["IsPush"] != DBNull.Value)
            {
                pInstance.IsPush = Convert.ToInt32(pReader["IsPush"]);
            }
            if (pReader["ClientID"] != DBNull.Value)
            {
                pInstance.ClientID = Convert.ToString(pReader["ClientID"]);
            }
            if (pReader["CreateBy"] != DBNull.Value)
            {
                pInstance.CreateBy = Convert.ToString(pReader["CreateBy"]);
            }
            if (pReader["CreateTime"] != DBNull.Value)
            {
                pInstance.CreateTime = Convert.ToDateTime(pReader["CreateTime"]);
            }
            if (pReader["LastUpdateBy"] != DBNull.Value)
            {
                pInstance.LastUpdateBy = Convert.ToString(pReader["LastUpdateBy"]);
            }
            if (pReader["LastUpdateTime"] != DBNull.Value)
            {
                pInstance.LastUpdateTime = Convert.ToDateTime(pReader["LastUpdateTime"]);
            }
            if (pReader["IsDelete"] != DBNull.Value)
            {
                pInstance.IsDelete = Convert.ToInt32(pReader["IsDelete"]);
            }
            if (pReader["ContentTypeID"] != DBNull.Value)
            {
                pInstance.ContentTypeID = Convert.ToInt32(pReader["ContentTypeID"]);
            }
            if (pReader["HeadImageUrl"] != DBNull.Value)
            {
                pInstance.HeadImageUrl = Convert.ToString(pReader["HeadImageUrl"]);
            }
            if (pReader["TimeStamp"] != DBNull.Value)
            {
                pInstance.TimeStamp = Convert.ToInt64(pReader["TimeStamp"]);
            }
            if (pReader["OpenId"] != DBNull.Value)
            {
                pInstance.OpenId = Convert.ToString(pReader["OpenId"]);
            }

        }
        #endregion
    }
}
