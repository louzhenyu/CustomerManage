/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/2/18 16:24:00
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
    /// ��T_ZMBA_CourseDetail�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class T_ZMBA_CourseDetailDAO : Base.BaseCPOSDAO, ICRUDable<T_ZMBA_CourseDetailEntity>, IQueryable<T_ZMBA_CourseDetailEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public T_ZMBA_CourseDetailDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(T_ZMBA_CourseDetailEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(T_ZMBA_CourseDetailEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [T_ZMBA_CourseDetail](");
            strSql.Append("[CourseId],[CourseName],[Name],[Essentials],[IconURL],[Type],[VideoURL],[Content],[Link],[ReadCount],[DisplayIndex],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[CustomerId],[CourseDetailId])");
            strSql.Append(" values (");
            strSql.Append("@CourseId,@CourseName,@Name,@Essentials,@IconURL,@Type,@VideoURL,@Content,@Link,@ReadCount,@DisplayIndex,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@CustomerId,@CourseDetailId)");            

			Guid? pkGuid;
			if (pEntity.CourseDetailId == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.CourseDetailId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@CourseId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@CourseName",SqlDbType.NVarChar),
					new SqlParameter("@Name",SqlDbType.NVarChar),
					new SqlParameter("@Essentials",SqlDbType.NVarChar),
					new SqlParameter("@IconURL",SqlDbType.VarChar),
					new SqlParameter("@Type",SqlDbType.Int),
					new SqlParameter("@VideoURL",SqlDbType.VarChar),
					new SqlParameter("@Content",SqlDbType.NVarChar),
					new SqlParameter("@Link",SqlDbType.VarChar),
					new SqlParameter("@ReadCount",SqlDbType.Int),
					new SqlParameter("@DisplayIndex",SqlDbType.Int),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerId",SqlDbType.VarChar),
					new SqlParameter("@CourseDetailId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.CourseId;
			parameters[1].Value = pEntity.CourseName;
			parameters[2].Value = pEntity.Name;
			parameters[3].Value = pEntity.Essentials;
			parameters[4].Value = pEntity.IconURL;
			parameters[5].Value = pEntity.Type;
			parameters[6].Value = pEntity.VideoURL;
			parameters[7].Value = pEntity.Content;
			parameters[8].Value = pEntity.Link;
			parameters[9].Value = pEntity.ReadCount;
			parameters[10].Value = pEntity.DisplayIndex;
			parameters[11].Value = pEntity.CreateTime;
			parameters[12].Value = pEntity.CreateBy;
			parameters[13].Value = pEntity.LastUpdateTime;
			parameters[14].Value = pEntity.LastUpdateBy;
			parameters[15].Value = pEntity.IsDelete;
			parameters[16].Value = pEntity.CustomerId;
			parameters[17].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.CourseDetailId = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public T_ZMBA_CourseDetailEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_ZMBA_CourseDetail] where CourseDetailId='{0}'  and isdelete=0 ", id.ToString());
            //��ȡ����
            T_ZMBA_CourseDetailEntity m = null;
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
        public T_ZMBA_CourseDetailEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_ZMBA_CourseDetail] where 1=1  and isdelete=0");
            //��ȡ����
            List<T_ZMBA_CourseDetailEntity> list = new List<T_ZMBA_CourseDetailEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_ZMBA_CourseDetailEntity m;
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
        public void Update(T_ZMBA_CourseDetailEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(T_ZMBA_CourseDetailEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.CourseDetailId.HasValue)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_ZMBA_CourseDetail] set ");
                        if (pIsUpdateNullField || pEntity.CourseId!=null)
                strSql.Append( "[CourseId]=@CourseId,");
            if (pIsUpdateNullField || pEntity.CourseName!=null)
                strSql.Append( "[CourseName]=@CourseName,");
            if (pIsUpdateNullField || pEntity.Name!=null)
                strSql.Append( "[Name]=@Name,");
            if (pIsUpdateNullField || pEntity.Essentials!=null)
                strSql.Append( "[Essentials]=@Essentials,");
            if (pIsUpdateNullField || pEntity.IconURL!=null)
                strSql.Append( "[IconURL]=@IconURL,");
            if (pIsUpdateNullField || pEntity.Type!=null)
                strSql.Append( "[Type]=@Type,");
            if (pIsUpdateNullField || pEntity.VideoURL!=null)
                strSql.Append( "[VideoURL]=@VideoURL,");
            if (pIsUpdateNullField || pEntity.Content!=null)
                strSql.Append( "[Content]=@Content,");
            if (pIsUpdateNullField || pEntity.Link!=null)
                strSql.Append( "[Link]=@Link,");
            if (pIsUpdateNullField || pEntity.ReadCount!=null)
                strSql.Append( "[ReadCount]=@ReadCount,");
            if (pIsUpdateNullField || pEntity.DisplayIndex!=null)
                strSql.Append( "[DisplayIndex]=@DisplayIndex,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId");
            strSql.Append(" where CourseDetailId=@CourseDetailId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@CourseId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@CourseName",SqlDbType.NVarChar),
					new SqlParameter("@Name",SqlDbType.NVarChar),
					new SqlParameter("@Essentials",SqlDbType.NVarChar),
					new SqlParameter("@IconURL",SqlDbType.VarChar),
					new SqlParameter("@Type",SqlDbType.Int),
					new SqlParameter("@VideoURL",SqlDbType.VarChar),
					new SqlParameter("@Content",SqlDbType.NVarChar),
					new SqlParameter("@Link",SqlDbType.VarChar),
					new SqlParameter("@ReadCount",SqlDbType.Int),
					new SqlParameter("@DisplayIndex",SqlDbType.Int),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@CustomerId",SqlDbType.VarChar),
					new SqlParameter("@CourseDetailId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.CourseId;
			parameters[1].Value = pEntity.CourseName;
			parameters[2].Value = pEntity.Name;
			parameters[3].Value = pEntity.Essentials;
			parameters[4].Value = pEntity.IconURL;
			parameters[5].Value = pEntity.Type;
			parameters[6].Value = pEntity.VideoURL;
			parameters[7].Value = pEntity.Content;
			parameters[8].Value = pEntity.Link;
			parameters[9].Value = pEntity.ReadCount;
			parameters[10].Value = pEntity.DisplayIndex;
			parameters[11].Value = pEntity.LastUpdateTime;
			parameters[12].Value = pEntity.LastUpdateBy;
			parameters[13].Value = pEntity.CustomerId;
			parameters[14].Value = pEntity.CourseDetailId;

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
        public void Update(T_ZMBA_CourseDetailEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(T_ZMBA_CourseDetailEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(T_ZMBA_CourseDetailEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.CourseDetailId.HasValue)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.CourseDetailId.Value, pTran);           
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
            sql.AppendLine("update [T_ZMBA_CourseDetail] set  isdelete=1 where CourseDetailId=@CourseDetailId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@CourseDetailId",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(T_ZMBA_CourseDetailEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.CourseDetailId.HasValue)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.CourseDetailId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(T_ZMBA_CourseDetailEntity[] pEntities)
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
            sql.AppendLine("update [T_ZMBA_CourseDetail] set  isdelete=1 where CourseDetailId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public T_ZMBA_CourseDetailEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_ZMBA_CourseDetail] where 1=1  and isdelete=0 ");
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
            List<T_ZMBA_CourseDetailEntity> list = new List<T_ZMBA_CourseDetailEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_ZMBA_CourseDetailEntity m;
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
        public PagedQueryResult<T_ZMBA_CourseDetailEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [CourseDetailId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [T_ZMBA_CourseDetail] where 1=1  and isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [T_ZMBA_CourseDetail] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<T_ZMBA_CourseDetailEntity> result = new PagedQueryResult<T_ZMBA_CourseDetailEntity>();
            List<T_ZMBA_CourseDetailEntity> list = new List<T_ZMBA_CourseDetailEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    T_ZMBA_CourseDetailEntity m;
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
        public T_ZMBA_CourseDetailEntity[] QueryByEntity(T_ZMBA_CourseDetailEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<T_ZMBA_CourseDetailEntity> PagedQueryByEntity(T_ZMBA_CourseDetailEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(T_ZMBA_CourseDetailEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.CourseDetailId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CourseDetailId", Value = pQueryEntity.CourseDetailId });
            if (pQueryEntity.CourseId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CourseId", Value = pQueryEntity.CourseId });
            if (pQueryEntity.CourseName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CourseName", Value = pQueryEntity.CourseName });
            if (pQueryEntity.Name!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Name", Value = pQueryEntity.Name });
            if (pQueryEntity.Essentials!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Essentials", Value = pQueryEntity.Essentials });
            if (pQueryEntity.IconURL!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IconURL", Value = pQueryEntity.IconURL });
            if (pQueryEntity.Type!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Type", Value = pQueryEntity.Type });
            if (pQueryEntity.VideoURL!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VideoURL", Value = pQueryEntity.VideoURL });
            if (pQueryEntity.Content!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Content", Value = pQueryEntity.Content });
            if (pQueryEntity.Link!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Link", Value = pQueryEntity.Link });
            if (pQueryEntity.ReadCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReadCount", Value = pQueryEntity.ReadCount });
            if (pQueryEntity.DisplayIndex!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DisplayIndex", Value = pQueryEntity.DisplayIndex });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.LastUpdateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.LastUpdateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.IsDelete!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(IDataReader pReader, out T_ZMBA_CourseDetailEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new T_ZMBA_CourseDetailEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["CourseDetailId"] != DBNull.Value)
			{
				pInstance.CourseDetailId =  (Guid)pReader["CourseDetailId"];
			}
			if (pReader["CourseId"] != DBNull.Value)
			{
				pInstance.CourseId =  (Guid)pReader["CourseId"];
			}
			if (pReader["CourseName"] != DBNull.Value)
			{
				pInstance.CourseName =  Convert.ToString(pReader["CourseName"]);
			}
			if (pReader["Name"] != DBNull.Value)
			{
				pInstance.Name =  Convert.ToString(pReader["Name"]);
			}
			if (pReader["Essentials"] != DBNull.Value)
			{
				pInstance.Essentials =  Convert.ToString(pReader["Essentials"]);
			}
			if (pReader["IconURL"] != DBNull.Value)
			{
				pInstance.IconURL =  Convert.ToString(pReader["IconURL"]);
			}
			if (pReader["Type"] != DBNull.Value)
			{
				pInstance.Type =   Convert.ToInt32(pReader["Type"]);
			}
			if (pReader["VideoURL"] != DBNull.Value)
			{
				pInstance.VideoURL =  Convert.ToString(pReader["VideoURL"]);
			}
			if (pReader["Content"] != DBNull.Value)
			{
				pInstance.Content =  Convert.ToString(pReader["Content"]);
			}
			if (pReader["Link"] != DBNull.Value)
			{
				pInstance.Link =  Convert.ToString(pReader["Link"]);
			}
			if (pReader["ReadCount"] != DBNull.Value)
			{
				pInstance.ReadCount =   Convert.ToInt32(pReader["ReadCount"]);
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
			if (pReader["LastUpdateTime"] != DBNull.Value)
			{
				pInstance.LastUpdateTime =  Convert.ToDateTime(pReader["LastUpdateTime"]);
			}
			if (pReader["LastUpdateBy"] != DBNull.Value)
			{
				pInstance.LastUpdateBy =  Convert.ToString(pReader["LastUpdateBy"]);
			}
			if (pReader["IsDelete"] != DBNull.Value)
			{
				pInstance.IsDelete =   Convert.ToInt32(pReader["IsDelete"]);
			}
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}

        }
        #endregion
    }
}
