/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/3/19 11:39:44
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
    /// ��T_CTW_LEvents�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class T_CTW_LEventsDAO : Base.BaseCPOSDAO, ICRUDable<T_CTW_LEventsEntity>, IQueryable<T_CTW_LEventsEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public T_CTW_LEventsDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
            
        }
        #endregion
    

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(T_CTW_LEventsEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(T_CTW_LEventsEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [T_CTW_LEvents](");
            strSql.Append("[Title],[EventLevel],[ParentEventID],[Description],[ImageURL],[URL],[IsSubEvent],[EventStatus],[DisplayIndex],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[DrawMethodId],[EventFlag],[EventID])");
            strSql.Append(" values (");
            strSql.Append("@Title,@EventLevel,@ParentEventID,@Description,@ImageURL,@URL,@IsSubEvent,@EventStatus,@DisplayIndex,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@DrawMethodId,@EventFlag,@EventID)");            

			string pkString = pEntity.EventID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@Title",SqlDbType.NVarChar),
					new SqlParameter("@EventLevel",SqlDbType.Int),
					new SqlParameter("@ParentEventID",SqlDbType.NVarChar),
					new SqlParameter("@Description",SqlDbType.NVarChar),
					new SqlParameter("@ImageURL",SqlDbType.NVarChar),
					new SqlParameter("@URL",SqlDbType.NVarChar),
					new SqlParameter("@IsSubEvent",SqlDbType.Int),
					new SqlParameter("@EventStatus",SqlDbType.Int),
					new SqlParameter("@DisplayIndex",SqlDbType.Int),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@DrawMethodId",SqlDbType.Int),
					new SqlParameter("@EventFlag",SqlDbType.NVarChar),
					new SqlParameter("@EventID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.Title;
			parameters[1].Value = pEntity.EventLevel;
			parameters[2].Value = pEntity.ParentEventID;
			parameters[3].Value = pEntity.Description;
			parameters[4].Value = pEntity.ImageURL;
			parameters[5].Value = pEntity.URL;
			parameters[6].Value = pEntity.IsSubEvent;
			parameters[7].Value = pEntity.EventStatus;
			parameters[8].Value = pEntity.DisplayIndex;
			parameters[9].Value = pEntity.CreateTime;
			parameters[10].Value = pEntity.CreateBy;
			parameters[11].Value = pEntity.LastUpdateBy;
			parameters[12].Value = pEntity.LastUpdateTime;
			parameters[13].Value = pEntity.IsDelete;
			parameters[14].Value = pEntity.DrawMethodId;
			parameters[15].Value = pEntity.EventFlag;
			parameters[16].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.EventID = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public T_CTW_LEventsEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_CTW_LEvents] where EventID='{0}'  and isdelete=0 ", id.ToString());
            //��ȡ����
            T_CTW_LEventsEntity m = null;
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
        public T_CTW_LEventsEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_CTW_LEvents] where 1=1  and isdelete=0");
            //��ȡ����
            List<T_CTW_LEventsEntity> list = new List<T_CTW_LEventsEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_CTW_LEventsEntity m;
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
        public void Update(T_CTW_LEventsEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(T_CTW_LEventsEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.EventID == null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_CTW_LEvents] set ");
                        if (pIsUpdateNullField || pEntity.Title!=null)
                strSql.Append( "[Title]=@Title,");
            if (pIsUpdateNullField || pEntity.EventLevel!=null)
                strSql.Append( "[EventLevel]=@EventLevel,");
            if (pIsUpdateNullField || pEntity.ParentEventID!=null)
                strSql.Append( "[ParentEventID]=@ParentEventID,");
            if (pIsUpdateNullField || pEntity.Description!=null)
                strSql.Append( "[Description]=@Description,");
            if (pIsUpdateNullField || pEntity.ImageURL!=null)
                strSql.Append( "[ImageURL]=@ImageURL,");
            if (pIsUpdateNullField || pEntity.URL!=null)
                strSql.Append( "[URL]=@URL,");
            if (pIsUpdateNullField || pEntity.IsSubEvent!=null)
                strSql.Append( "[IsSubEvent]=@IsSubEvent,");
            if (pIsUpdateNullField || pEntity.EventStatus!=null)
                strSql.Append( "[EventStatus]=@EventStatus,");
            if (pIsUpdateNullField || pEntity.DisplayIndex!=null)
                strSql.Append( "[DisplayIndex]=@DisplayIndex,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.DrawMethodId!=null)
                strSql.Append( "[DrawMethodId]=@DrawMethodId,");
            if (pIsUpdateNullField || pEntity.EventFlag!=null)
                strSql.Append( "[EventFlag]=@EventFlag");
            strSql.Append(" where EventID=@EventID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@Title",SqlDbType.NVarChar),
					new SqlParameter("@EventLevel",SqlDbType.Int),
					new SqlParameter("@ParentEventID",SqlDbType.NVarChar),
					new SqlParameter("@Description",SqlDbType.NVarChar),
					new SqlParameter("@ImageURL",SqlDbType.NVarChar),
					new SqlParameter("@URL",SqlDbType.NVarChar),
					new SqlParameter("@IsSubEvent",SqlDbType.Int),
					new SqlParameter("@EventStatus",SqlDbType.Int),
					new SqlParameter("@DisplayIndex",SqlDbType.Int),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@DrawMethodId",SqlDbType.Int),
					new SqlParameter("@EventFlag",SqlDbType.NVarChar),
					new SqlParameter("@EventID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.Title;
			parameters[1].Value = pEntity.EventLevel;
			parameters[2].Value = pEntity.ParentEventID;
			parameters[3].Value = pEntity.Description;
			parameters[4].Value = pEntity.ImageURL;
			parameters[5].Value = pEntity.URL;
			parameters[6].Value = pEntity.IsSubEvent;
			parameters[7].Value = pEntity.EventStatus;
			parameters[8].Value = pEntity.DisplayIndex;
			parameters[9].Value = pEntity.LastUpdateBy;
			parameters[10].Value = pEntity.LastUpdateTime;
			parameters[11].Value = pEntity.DrawMethodId;
			parameters[12].Value = pEntity.EventFlag;
			parameters[13].Value = pEntity.EventID;

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
        public void Update(T_CTW_LEventsEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(T_CTW_LEventsEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(T_CTW_LEventsEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.EventID == null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.EventID, pTran);           
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
            sql.AppendLine("update [T_CTW_LEvents] set  isdelete=1 where EventID=@EventID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@EventID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(T_CTW_LEventsEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (pEntity.EventID == null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.EventID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(T_CTW_LEventsEntity[] pEntities)
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
            sql.AppendLine("update [T_CTW_LEvents] set  isdelete=1 where EventID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public T_CTW_LEventsEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_CTW_LEvents] where 1=1  and isdelete=0 ");
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
            List<T_CTW_LEventsEntity> list = new List<T_CTW_LEventsEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_CTW_LEventsEntity m;
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
        public PagedQueryResult<T_CTW_LEventsEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [EventID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [T_CTW_LEvents] where 1=1  and isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [T_CTW_LEvents] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<T_CTW_LEventsEntity> result = new PagedQueryResult<T_CTW_LEventsEntity>();
            List<T_CTW_LEventsEntity> list = new List<T_CTW_LEventsEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    T_CTW_LEventsEntity m;
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
        public T_CTW_LEventsEntity[] QueryByEntity(T_CTW_LEventsEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<T_CTW_LEventsEntity> PagedQueryByEntity(T_CTW_LEventsEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(T_CTW_LEventsEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.EventID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventID", Value = pQueryEntity.EventID });
            if (pQueryEntity.Title!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Title", Value = pQueryEntity.Title });
            if (pQueryEntity.EventLevel!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventLevel", Value = pQueryEntity.EventLevel });
            if (pQueryEntity.ParentEventID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ParentEventID", Value = pQueryEntity.ParentEventID });
            if (pQueryEntity.Description!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Description", Value = pQueryEntity.Description });
            if (pQueryEntity.ImageURL!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ImageURL", Value = pQueryEntity.ImageURL });
            if (pQueryEntity.URL!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "URL", Value = pQueryEntity.URL });
            if (pQueryEntity.IsSubEvent!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsSubEvent", Value = pQueryEntity.IsSubEvent });
            if (pQueryEntity.EventStatus!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventStatus", Value = pQueryEntity.EventStatus });
            if (pQueryEntity.DisplayIndex!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DisplayIndex", Value = pQueryEntity.DisplayIndex });
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
            if (pQueryEntity.DrawMethodId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DrawMethodId", Value = pQueryEntity.DrawMethodId });
            if (pQueryEntity.EventFlag!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventFlag", Value = pQueryEntity.EventFlag });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(IDataReader pReader, out T_CTW_LEventsEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new T_CTW_LEventsEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["EventID"] != DBNull.Value)
			{
				pInstance.EventID =  Convert.ToString(pReader["EventID"]);
			}
			if (pReader["Title"] != DBNull.Value)
			{
				pInstance.Title =  Convert.ToString(pReader["Title"]);
			}
			if (pReader["EventLevel"] != DBNull.Value)
			{
				pInstance.EventLevel =   Convert.ToInt32(pReader["EventLevel"]);
			}
			if (pReader["ParentEventID"] != DBNull.Value)
			{
				pInstance.ParentEventID =  Convert.ToString(pReader["ParentEventID"]);
			}
			if (pReader["Description"] != DBNull.Value)
			{
				pInstance.Description =  Convert.ToString(pReader["Description"]);
			}
			if (pReader["ImageURL"] != DBNull.Value)
			{
				pInstance.ImageURL =  Convert.ToString(pReader["ImageURL"]);
			}
			if (pReader["URL"] != DBNull.Value)
			{
				pInstance.URL =  Convert.ToString(pReader["URL"]);
			}
			if (pReader["IsSubEvent"] != DBNull.Value)
			{
				pInstance.IsSubEvent =   Convert.ToInt32(pReader["IsSubEvent"]);
			}
			if (pReader["EventStatus"] != DBNull.Value)
			{
				pInstance.EventStatus =   Convert.ToInt32(pReader["EventStatus"]);
			}
			if (pReader["DisplayIndex"] != DBNull.Value)
			{
				pInstance.DisplayIndex =   Convert.ToInt32(pReader["DisplayIndex"]);
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
			if (pReader["DrawMethodId"] != DBNull.Value)
			{
				pInstance.DrawMethodId =   Convert.ToInt32(pReader["DrawMethodId"]);
			}
			if (pReader["EventFlag"] != DBNull.Value)
			{
				pInstance.EventFlag =  Convert.ToString(pReader["EventFlag"]);
			}

        }
        #endregion
    }
}
