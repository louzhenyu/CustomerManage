/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/31 19:00:57
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
    /// ��MHCategoryArea�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class MHCategoryAreaDAO : BaseCPOSDAO, ICRUDable<MHCategoryAreaEntity>, IQueryable<MHCategoryAreaEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public MHCategoryAreaDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(MHCategoryAreaEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(MHCategoryAreaEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [MHCategoryArea](");
            strSql.Append("[HomeId],[ObjectId],[ObjectTypeId],[Level],[ObjectName],[ImageUrlObject],[DisplayIndex],[Remark],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[GroupID],[CategoryAreaId])");
            strSql.Append(" values (");
            strSql.Append("@HomeId,@ObjectId,@ObjectTypeId,@Level,@ObjectName,@ImageUrlObject,@DisplayIndex,@Remark,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@GroupID,@CategoryAreaId)");            

			Guid? pkGuid;
			if (pEntity.CategoryAreaId == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.CategoryAreaId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@HomeId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@ObjectId",SqlDbType.NVarChar),
					new SqlParameter("@ObjectTypeId",SqlDbType.Int),
					new SqlParameter("@Level",SqlDbType.Int),
					new SqlParameter("@ObjectName",SqlDbType.NVarChar),
					new SqlParameter("@ImageUrlObject",SqlDbType.NVarChar),
					new SqlParameter("@DisplayIndex",SqlDbType.Int),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@GroupID",SqlDbType.Int),
					new SqlParameter("@CategoryAreaId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.HomeId;
			parameters[1].Value = pEntity.ObjectId;
			parameters[2].Value = pEntity.ObjectTypeId;
			parameters[3].Value = pEntity.Level;
			parameters[4].Value = pEntity.ObjectName;
			parameters[5].Value = pEntity.ImageUrlObject;
			parameters[6].Value = pEntity.DisplayIndex;
			parameters[7].Value = pEntity.Remark;
			parameters[8].Value = pEntity.CreateTime;
			parameters[9].Value = pEntity.CreateBy;
			parameters[10].Value = pEntity.LastUpdateBy;
			parameters[11].Value = pEntity.LastUpdateTime;
			parameters[12].Value = pEntity.IsDelete;
			parameters[13].Value = pEntity.GroupID;
			parameters[14].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.CategoryAreaId = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public MHCategoryAreaEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [MHCategoryArea] where CategoryAreaId='{0}'  and isdelete=0 ", id.ToString());
            //��ȡ����
            MHCategoryAreaEntity m = null;
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
        public MHCategoryAreaEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [MHCategoryArea] where 1=1  and isdelete=0");
            //��ȡ����
            List<MHCategoryAreaEntity> list = new List<MHCategoryAreaEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    MHCategoryAreaEntity m;
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
        public void Update(MHCategoryAreaEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(MHCategoryAreaEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.CategoryAreaId.HasValue)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [MHCategoryArea] set ");
                        if (pIsUpdateNullField || pEntity.HomeId!=null)
                strSql.Append( "[HomeId]=@HomeId,");
            if (pIsUpdateNullField || pEntity.ObjectId!=null)
                strSql.Append( "[ObjectId]=@ObjectId,");
            if (pIsUpdateNullField || pEntity.ObjectTypeId!=null)
                strSql.Append( "[ObjectTypeId]=@ObjectTypeId,");
            if (pIsUpdateNullField || pEntity.Level!=null)
                strSql.Append( "[Level]=@Level,");
            if (pIsUpdateNullField || pEntity.ObjectName!=null)
                strSql.Append( "[ObjectName]=@ObjectName,");
            if (pIsUpdateNullField || pEntity.ImageUrlObject!=null)
                strSql.Append( "[ImageUrlObject]=@ImageUrlObject,");
            if (pIsUpdateNullField || pEntity.DisplayIndex!=null)
                strSql.Append( "[DisplayIndex]=@DisplayIndex,");
            if (pIsUpdateNullField || pEntity.Remark!=null)
                strSql.Append( "[Remark]=@Remark,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.GroupID!=null)
                strSql.Append( "[GroupID]=@GroupID");
            strSql.Append(" where CategoryAreaId=@CategoryAreaId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@HomeId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@ObjectId",SqlDbType.NVarChar),
					new SqlParameter("@ObjectTypeId",SqlDbType.Int),
					new SqlParameter("@Level",SqlDbType.Int),
					new SqlParameter("@ObjectName",SqlDbType.NVarChar),
					new SqlParameter("@ImageUrlObject",SqlDbType.NVarChar),
					new SqlParameter("@DisplayIndex",SqlDbType.Int),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@GroupID",SqlDbType.Int),
					new SqlParameter("@CategoryAreaId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.HomeId;
			parameters[1].Value = pEntity.ObjectId;
			parameters[2].Value = pEntity.ObjectTypeId;
			parameters[3].Value = pEntity.Level;
			parameters[4].Value = pEntity.ObjectName;
			parameters[5].Value = pEntity.ImageUrlObject;
			parameters[6].Value = pEntity.DisplayIndex;
			parameters[7].Value = pEntity.Remark;
			parameters[8].Value = pEntity.LastUpdateBy;
			parameters[9].Value = pEntity.LastUpdateTime;
			parameters[10].Value = pEntity.GroupID;
			parameters[11].Value = pEntity.CategoryAreaId;

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
        public void Update(MHCategoryAreaEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(MHCategoryAreaEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(MHCategoryAreaEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.CategoryAreaId.HasValue)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.CategoryAreaId.Value, pTran);           
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
            sql.AppendLine("update [MHCategoryArea] set  isdelete=1 where CategoryAreaId=@CategoryAreaId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@CategoryAreaId",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(MHCategoryAreaEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.CategoryAreaId.HasValue)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.CategoryAreaId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(MHCategoryAreaEntity[] pEntities)
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
            sql.AppendLine("update [MHCategoryArea] set  isdelete=1 where CategoryAreaId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public MHCategoryAreaEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [MHCategoryArea] where 1=1  and isdelete=0 ");
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
            List<MHCategoryAreaEntity> list = new List<MHCategoryAreaEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    MHCategoryAreaEntity m;
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
        public PagedQueryResult<MHCategoryAreaEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [CategoryAreaId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [MHCategoryArea] where 1=1  and isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [MHCategoryArea] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<MHCategoryAreaEntity> result = new PagedQueryResult<MHCategoryAreaEntity>();
            List<MHCategoryAreaEntity> list = new List<MHCategoryAreaEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    MHCategoryAreaEntity m;
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
        public MHCategoryAreaEntity[] QueryByEntity(MHCategoryAreaEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<MHCategoryAreaEntity> PagedQueryByEntity(MHCategoryAreaEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(MHCategoryAreaEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.CategoryAreaId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CategoryAreaId", Value = pQueryEntity.CategoryAreaId });
            if (pQueryEntity.HomeId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "HomeId", Value = pQueryEntity.HomeId });
            if (pQueryEntity.ObjectId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ObjectId", Value = pQueryEntity.ObjectId });
            if (pQueryEntity.ObjectTypeId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ObjectTypeId", Value = pQueryEntity.ObjectTypeId });
            if (pQueryEntity.Level!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Level", Value = pQueryEntity.Level });
            if (pQueryEntity.ObjectName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ObjectName", Value = pQueryEntity.ObjectName });
            if (pQueryEntity.ImageUrlObject!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ImageUrlObject", Value = pQueryEntity.ImageUrlObject });
            if (pQueryEntity.DisplayIndex!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DisplayIndex", Value = pQueryEntity.DisplayIndex });
            if (pQueryEntity.Remark!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Remark", Value = pQueryEntity.Remark });
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
            if (pQueryEntity.GroupID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "GroupID", Value = pQueryEntity.GroupID });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(IDataReader pReader, out MHCategoryAreaEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new MHCategoryAreaEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["CategoryAreaId"] != DBNull.Value)
			{
				pInstance.CategoryAreaId =  (Guid)pReader["CategoryAreaId"];
			}
			if (pReader["HomeId"] != DBNull.Value)
			{
				pInstance.HomeId =  (Guid)pReader["HomeId"];
			}
			if (pReader["ObjectId"] != DBNull.Value)
			{
				pInstance.ObjectId =  Convert.ToString(pReader["ObjectId"]);
			}
			if (pReader["ObjectTypeId"] != DBNull.Value)
			{
				pInstance.ObjectTypeId =   Convert.ToInt32(pReader["ObjectTypeId"]);
			}
			if (pReader["Level"] != DBNull.Value)
			{
				pInstance.Level =   Convert.ToInt32(pReader["Level"]);
			}
			if (pReader["ObjectName"] != DBNull.Value)
			{
				pInstance.ObjectName =  Convert.ToString(pReader["ObjectName"]);
			}
			if (pReader["ImageUrlObject"] != DBNull.Value)
			{
				pInstance.ImageUrlObject =  Convert.ToString(pReader["ImageUrlObject"]);
			}
			if (pReader["DisplayIndex"] != DBNull.Value)
			{
				pInstance.DisplayIndex =   Convert.ToInt32(pReader["DisplayIndex"]);
			}
			if (pReader["Remark"] != DBNull.Value)
			{
				pInstance.Remark =  Convert.ToString(pReader["Remark"]);
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
			if (pReader["GroupID"] != DBNull.Value)
			{
				pInstance.GroupID =   Convert.ToInt32(pReader["GroupID"]);
			}

        }
        #endregion
    }
}
