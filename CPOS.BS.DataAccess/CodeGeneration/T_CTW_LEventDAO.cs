/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/4/20 11:28:20
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
    /// ��T_CTW_LEvent�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class T_CTW_LEventDAO : Base.BaseCPOSDAO, ICRUDable<T_CTW_LEventEntity>, IQueryable<T_CTW_LEventEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public T_CTW_LEventDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(T_CTW_LEventEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(T_CTW_LEventEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [T_CTW_LEvent](");
            strSql.Append("[TemplateId],[Name],[Desc],[StartDate],[EndDate],[ActivityGroupId],[InteractionType],[ImageURL],[OnlineQRCodeId],[OfflineQRCodeId],[Status],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[CustomerId],[IsDelete],[OfflineRedirectUrl],[OnlineRedirectUrl],[CTWEventId])");
            strSql.Append(" values (");
            strSql.Append("@TemplateId,@Name,@Desc,@StartDate,@EndDate,@ActivityGroupId,@InteractionType,@ImageURL,@OnlineQRCodeId,@OfflineQRCodeId,@Status,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@CustomerId,@IsDelete,@OfflineRedirectUrl,@OnlineRedirectUrl,@CTWEventId)");            

			Guid? pkGuid;
			if (pEntity.CTWEventId == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.CTWEventId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@TemplateId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@Name",SqlDbType.NVarChar),
					new SqlParameter("@Desc",SqlDbType.NVarChar),
					new SqlParameter("@StartDate",SqlDbType.DateTime),
					new SqlParameter("@EndDate",SqlDbType.DateTime),
					new SqlParameter("@ActivityGroupId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@InteractionType",SqlDbType.Int),
					new SqlParameter("@ImageURL",SqlDbType.NVarChar),
					new SqlParameter("@OnlineQRCodeId",SqlDbType.VarChar),
					new SqlParameter("@OfflineQRCodeId",SqlDbType.VarChar),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@CustomerId",SqlDbType.VarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@OfflineRedirectUrl",SqlDbType.NVarChar),
					new SqlParameter("@OnlineRedirectUrl",SqlDbType.NVarChar),
					new SqlParameter("@CTWEventId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.TemplateId;
			parameters[1].Value = pEntity.Name;
			parameters[2].Value = pEntity.Desc;
			parameters[3].Value = pEntity.StartDate;
			parameters[4].Value = pEntity.EndDate;
			parameters[5].Value = pEntity.ActivityGroupId;
			parameters[6].Value = pEntity.InteractionType;
			parameters[7].Value = pEntity.ImageURL;
			parameters[8].Value = pEntity.OnlineQRCodeId;
			parameters[9].Value = pEntity.OfflineQRCodeId;
			parameters[10].Value = pEntity.Status;
			parameters[11].Value = pEntity.CreateTime;
			parameters[12].Value = pEntity.CreateBy;
			parameters[13].Value = pEntity.LastUpdateTime;
			parameters[14].Value = pEntity.LastUpdateBy;
			parameters[15].Value = pEntity.CustomerId;
			parameters[16].Value = pEntity.IsDelete;
			parameters[17].Value = pEntity.OffLineRedirectUrl;
			parameters[18].Value = pEntity.OnLineRedirectUrl;
			parameters[19].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.CTWEventId = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public T_CTW_LEventEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_CTW_LEvent] where CTWEventId='{0}'  and isdelete=0 ", id.ToString());
            //��ȡ����
            T_CTW_LEventEntity m = null;
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
        public T_CTW_LEventEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_CTW_LEvent] where 1=1  and isdelete=0");
            //��ȡ����
            List<T_CTW_LEventEntity> list = new List<T_CTW_LEventEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_CTW_LEventEntity m;
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
        public void Update(T_CTW_LEventEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(T_CTW_LEventEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.CTWEventId.HasValue)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_CTW_LEvent] set ");
                        if (pIsUpdateNullField || pEntity.TemplateId!=null)
                strSql.Append( "[TemplateId]=@TemplateId,");
            if (pIsUpdateNullField || pEntity.Name!=null)
                strSql.Append( "[Name]=@Name,");
            if (pIsUpdateNullField || pEntity.Desc!=null)
                strSql.Append( "[Desc]=@Desc,");
            if (pIsUpdateNullField || pEntity.StartDate!=null)
                strSql.Append( "[StartDate]=@StartDate,");
            if (pIsUpdateNullField || pEntity.EndDate!=null)
                strSql.Append( "[EndDate]=@EndDate,");
            if (pIsUpdateNullField || pEntity.ActivityGroupId!=null)
                strSql.Append( "[ActivityGroupId]=@ActivityGroupId,");
            if (pIsUpdateNullField || pEntity.InteractionType!=null)
                strSql.Append( "[InteractionType]=@InteractionType,");
            if (pIsUpdateNullField || pEntity.ImageURL!=null)
                strSql.Append( "[ImageURL]=@ImageURL,");
            if (pIsUpdateNullField || pEntity.OnlineQRCodeId!=null)
                strSql.Append( "[OnlineQRCodeId]=@OnlineQRCodeId,");
            if (pIsUpdateNullField || pEntity.OfflineQRCodeId!=null)
                strSql.Append( "[OfflineQRCodeId]=@OfflineQRCodeId,");
            if (pIsUpdateNullField || pEntity.Status!=null)
                strSql.Append( "[Status]=@Status,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.OffLineRedirectUrl!=null)
                strSql.Append( "[OfflineRedirectUrl]=@OfflineRedirectUrl,");
            if (pIsUpdateNullField || pEntity.OnLineRedirectUrl!=null)
                strSql.Append( "[OnlineRedirectUrl]=@OnlineRedirectUrl");
            strSql.Append(" where CTWEventId=@CTWEventId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@TemplateId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@Name",SqlDbType.NVarChar),
					new SqlParameter("@Desc",SqlDbType.NVarChar),
					new SqlParameter("@StartDate",SqlDbType.DateTime),
					new SqlParameter("@EndDate",SqlDbType.DateTime),
					new SqlParameter("@ActivityGroupId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@InteractionType",SqlDbType.Int),
					new SqlParameter("@ImageURL",SqlDbType.NVarChar),
					new SqlParameter("@OnlineQRCodeId",SqlDbType.VarChar),
					new SqlParameter("@OfflineQRCodeId",SqlDbType.VarChar),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@CustomerId",SqlDbType.VarChar),
					new SqlParameter("@OfflineRedirectUrl",SqlDbType.NVarChar),
					new SqlParameter("@OnlineRedirectUrl",SqlDbType.NVarChar),
					new SqlParameter("@CTWEventId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.TemplateId;
			parameters[1].Value = pEntity.Name;
			parameters[2].Value = pEntity.Desc;
			parameters[3].Value = pEntity.StartDate;
			parameters[4].Value = pEntity.EndDate;
			parameters[5].Value = pEntity.ActivityGroupId;
			parameters[6].Value = pEntity.InteractionType;
			parameters[7].Value = pEntity.ImageURL;
			parameters[8].Value = pEntity.OnlineQRCodeId;
			parameters[9].Value = pEntity.OfflineQRCodeId;
			parameters[10].Value = pEntity.Status;
			parameters[11].Value = pEntity.LastUpdateTime;
			parameters[12].Value = pEntity.LastUpdateBy;
			parameters[13].Value = pEntity.CustomerId;
			parameters[14].Value = pEntity.OffLineRedirectUrl;
			parameters[15].Value = pEntity.OnLineRedirectUrl;
			parameters[16].Value = pEntity.CTWEventId;

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
        public void Update(T_CTW_LEventEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(T_CTW_LEventEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(T_CTW_LEventEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.CTWEventId.HasValue)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.CTWEventId.Value, pTran);           
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
            sql.AppendLine("update [T_CTW_LEvent] set  isdelete=1 where CTWEventId=@CTWEventId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@CTWEventId",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(T_CTW_LEventEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.CTWEventId.HasValue)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.CTWEventId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(T_CTW_LEventEntity[] pEntities)
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
            sql.AppendLine("update [T_CTW_LEvent] set  isdelete=1 where CTWEventId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public T_CTW_LEventEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_CTW_LEvent] where 1=1  and isdelete=0 ");
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
            List<T_CTW_LEventEntity> list = new List<T_CTW_LEventEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_CTW_LEventEntity m;
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
        public PagedQueryResult<T_CTW_LEventEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [CTWEventId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [T_CTW_LEvent] where 1=1  and isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [T_CTW_LEvent] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<T_CTW_LEventEntity> result = new PagedQueryResult<T_CTW_LEventEntity>();
            List<T_CTW_LEventEntity> list = new List<T_CTW_LEventEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    T_CTW_LEventEntity m;
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
        public T_CTW_LEventEntity[] QueryByEntity(T_CTW_LEventEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<T_CTW_LEventEntity> PagedQueryByEntity(T_CTW_LEventEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(T_CTW_LEventEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.CTWEventId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CTWEventId", Value = pQueryEntity.CTWEventId });
            if (pQueryEntity.TemplateId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TemplateId", Value = pQueryEntity.TemplateId });
            if (pQueryEntity.Name!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Name", Value = pQueryEntity.Name });
            if (pQueryEntity.Desc!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Desc", Value = pQueryEntity.Desc });
            if (pQueryEntity.StartDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StartDate", Value = pQueryEntity.StartDate });
            if (pQueryEntity.EndDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EndDate", Value = pQueryEntity.EndDate });
            if (pQueryEntity.ActivityGroupId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ActivityGroupId", Value = pQueryEntity.ActivityGroupId });
            if (pQueryEntity.InteractionType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "InteractionType", Value = pQueryEntity.InteractionType });
            if (pQueryEntity.ImageURL!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ImageURL", Value = pQueryEntity.ImageURL });
            if (pQueryEntity.OnlineQRCodeId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OnlineQRCodeId", Value = pQueryEntity.OnlineQRCodeId });
            if (pQueryEntity.OfflineQRCodeId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OfflineQRCodeId", Value = pQueryEntity.OfflineQRCodeId });
            if (pQueryEntity.Status!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Status", Value = pQueryEntity.Status });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.LastUpdateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.LastUpdateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
            if (pQueryEntity.IsDelete!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });
            if (pQueryEntity.OffLineRedirectUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OfflineRedirectUrl", Value = pQueryEntity.OffLineRedirectUrl });
            if (pQueryEntity.OnLineRedirectUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OnlineRedirectUrl", Value = pQueryEntity.OnLineRedirectUrl });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(IDataReader pReader, out T_CTW_LEventEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new T_CTW_LEventEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["CTWEventId"] != DBNull.Value)
			{
				pInstance.CTWEventId =  (Guid)pReader["CTWEventId"];
			}
			if (pReader["TemplateId"] != DBNull.Value)
			{
				pInstance.TemplateId =  (Guid)pReader["TemplateId"];
			}
			if (pReader["Name"] != DBNull.Value)
			{
				pInstance.Name =  Convert.ToString(pReader["Name"]);
			}
			if (pReader["Desc"] != DBNull.Value)
			{
				pInstance.Desc =  Convert.ToString(pReader["Desc"]);
			}
			if (pReader["StartDate"] != DBNull.Value)
			{
				pInstance.StartDate =  Convert.ToDateTime(pReader["StartDate"]);
			}
			if (pReader["EndDate"] != DBNull.Value)
			{
				pInstance.EndDate =  Convert.ToDateTime(pReader["EndDate"]);
			}
			if (pReader["ActivityGroupId"] != DBNull.Value)
			{
				pInstance.ActivityGroupId =  (Guid)pReader["ActivityGroupId"];
			}
			if (pReader["InteractionType"] != DBNull.Value)
			{
				pInstance.InteractionType =   Convert.ToInt32(pReader["InteractionType"]);
			}
			if (pReader["ImageURL"] != DBNull.Value)
			{
				pInstance.ImageURL =  Convert.ToString(pReader["ImageURL"]);
			}
			if (pReader["OnlineQRCodeId"] != DBNull.Value)
			{
				pInstance.OnlineQRCodeId =  Convert.ToString(pReader["OnlineQRCodeId"]);
			}
			if (pReader["OfflineQRCodeId"] != DBNull.Value)
			{
				pInstance.OfflineQRCodeId =  Convert.ToString(pReader["OfflineQRCodeId"]);
			}
			if (pReader["Status"] != DBNull.Value)
			{
				pInstance.Status =   Convert.ToInt32(pReader["Status"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}
			if (pReader["CreateBy"] != DBNull.Value)
			{
				pInstance.CreateBy =  Convert.ToString(pReader["CreateBy"]);
			}
			if (pReader["LastUpdateTime"] != DBNull.Value)
			{
				pInstance.LastUpdateTime =  Convert.ToDateTime(pReader["LastUpdateTime"]);
			}
			if (pReader["LastUpdateBy"] != DBNull.Value)
			{
				pInstance.LastUpdateBy =  Convert.ToString(pReader["LastUpdateBy"]);
			}
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}
			if (pReader["IsDelete"] != DBNull.Value)
			{
				pInstance.IsDelete =   Convert.ToInt32(pReader["IsDelete"]);
			}
			if (pReader["OfflineRedirectUrl"] != DBNull.Value)
			{
				pInstance.OffLineRedirectUrl =  Convert.ToString(pReader["OfflineRedirectUrl"]);
			}
			if (pReader["OnlineRedirectUrl"] != DBNull.Value)
			{
				pInstance.OnLineRedirectUrl =  Convert.ToString(pReader["OnlineRedirectUrl"]);
			}

        }
        #endregion
    }
}
