/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/6 16:48:56
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
    /// ��MLCourseWare�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class MLCourseWareDAO : Base.BaseCPOSDAO, ICRUDable<MLCourseWareEntity>, IQueryable<MLCourseWareEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public MLCourseWareDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(MLCourseWareEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(MLCourseWareEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [MLCourseWare](");
            strSql.Append("[OnlineCourseId],[OriginalName],[CourseWareFile],[ExtName],[Icon],[Downloadable],[ContentId],[CourseWareIndex],[CustomerID],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[CourseWareSize],[CourseWareId])");
            strSql.Append(" values (");
            strSql.Append("@OnlineCourseId,@OriginalName,@CourseWareFile,@ExtName,@Icon,@Downloadable,@ContentId,@CourseWareIndex,@CustomerID,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@CourseWareSize,@CourseWareId)");            

			string pkString = pEntity.CourseWareId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@OnlineCourseId",SqlDbType.NVarChar),
					new SqlParameter("@OriginalName",SqlDbType.NVarChar),
					new SqlParameter("@CourseWareFile",SqlDbType.NVarChar),
					new SqlParameter("@ExtName",SqlDbType.NVarChar),
					new SqlParameter("@Icon",SqlDbType.NVarChar),
					new SqlParameter("@Downloadable",SqlDbType.Int),
					new SqlParameter("@ContentId",SqlDbType.NVarChar),
					new SqlParameter("@CourseWareIndex",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CourseWareSize",SqlDbType.NVarChar),
					new SqlParameter("@CourseWareId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.OnlineCourseId;
			parameters[1].Value = pEntity.OriginalName;
			parameters[2].Value = pEntity.CourseWareFile;
			parameters[3].Value = pEntity.ExtName;
			parameters[4].Value = pEntity.Icon;
			parameters[5].Value = pEntity.Downloadable;
			parameters[6].Value = pEntity.ContentId;
			parameters[7].Value = pEntity.CourseWareIndex;
			parameters[8].Value = pEntity.CustomerID;
			parameters[9].Value = pEntity.CreateBy;
			parameters[10].Value = pEntity.CreateTime;
			parameters[11].Value = pEntity.LastUpdateBy;
			parameters[12].Value = pEntity.LastUpdateTime;
			parameters[13].Value = pEntity.IsDelete;
			parameters[14].Value = pEntity.CourseWareSize;
			parameters[15].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.CourseWareId = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public MLCourseWareEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [MLCourseWare] where CourseWareId='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            MLCourseWareEntity m = null;
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
        public MLCourseWareEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [MLCourseWare] where isdelete=0");
            //��ȡ����
            List<MLCourseWareEntity> list = new List<MLCourseWareEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    MLCourseWareEntity m;
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
        public void Update(MLCourseWareEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(MLCourseWareEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.CourseWareId==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [MLCourseWare] set ");
            if (pIsUpdateNullField || pEntity.OnlineCourseId!=null)
                strSql.Append( "[OnlineCourseId]=@OnlineCourseId,");
            if (pIsUpdateNullField || pEntity.OriginalName!=null)
                strSql.Append( "[OriginalName]=@OriginalName,");
            if (pIsUpdateNullField || pEntity.CourseWareFile!=null)
                strSql.Append( "[CourseWareFile]=@CourseWareFile,");
            if (pIsUpdateNullField || pEntity.ExtName!=null)
                strSql.Append( "[ExtName]=@ExtName,");
            if (pIsUpdateNullField || pEntity.Icon!=null)
                strSql.Append( "[Icon]=@Icon,");
            if (pIsUpdateNullField || pEntity.Downloadable!=null)
                strSql.Append( "[Downloadable]=@Downloadable,");
            if (pIsUpdateNullField || pEntity.ContentId!=null)
                strSql.Append( "[ContentId]=@ContentId,");
            if (pIsUpdateNullField || pEntity.CourseWareIndex!=null)
                strSql.Append( "[CourseWareIndex]=@CourseWareIndex,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.CourseWareSize!=null)
                strSql.Append( "[CourseWareSize]=@CourseWareSize");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where CourseWareId=@CourseWareId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@OnlineCourseId",SqlDbType.NVarChar),
					new SqlParameter("@OriginalName",SqlDbType.NVarChar),
					new SqlParameter("@CourseWareFile",SqlDbType.NVarChar),
					new SqlParameter("@ExtName",SqlDbType.NVarChar),
					new SqlParameter("@Icon",SqlDbType.NVarChar),
					new SqlParameter("@Downloadable",SqlDbType.Int),
					new SqlParameter("@ContentId",SqlDbType.NVarChar),
					new SqlParameter("@CourseWareIndex",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CourseWareSize",SqlDbType.NVarChar),
					new SqlParameter("@CourseWareId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.OnlineCourseId;
			parameters[1].Value = pEntity.OriginalName;
			parameters[2].Value = pEntity.CourseWareFile;
			parameters[3].Value = pEntity.ExtName;
			parameters[4].Value = pEntity.Icon;
			parameters[5].Value = pEntity.Downloadable;
			parameters[6].Value = pEntity.ContentId;
			parameters[7].Value = pEntity.CourseWareIndex;
			parameters[8].Value = pEntity.CustomerID;
			parameters[9].Value = pEntity.LastUpdateBy;
			parameters[10].Value = pEntity.LastUpdateTime;
			parameters[11].Value = pEntity.CourseWareSize;
			parameters[12].Value = pEntity.CourseWareId;

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
        public void Update(MLCourseWareEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(MLCourseWareEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(MLCourseWareEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(MLCourseWareEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.CourseWareId==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.CourseWareId, pTran);           
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
            sql.AppendLine("update [MLCourseWare] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where CourseWareId=@CourseWareId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@CourseWareId",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(MLCourseWareEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.CourseWareId==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.CourseWareId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(MLCourseWareEntity[] pEntities)
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
            sql.AppendLine("update [MLCourseWare] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where CourseWareId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public MLCourseWareEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [MLCourseWare] where isdelete=0 ");
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
            List<MLCourseWareEntity> list = new List<MLCourseWareEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    MLCourseWareEntity m;
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
        public PagedQueryResult<MLCourseWareEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [CourseWareId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [MLCourseWare] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [MLCourseWare] where isdelete=0 ");
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
            PagedQueryResult<MLCourseWareEntity> result = new PagedQueryResult<MLCourseWareEntity>();
            List<MLCourseWareEntity> list = new List<MLCourseWareEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    MLCourseWareEntity m;
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
        public MLCourseWareEntity[] QueryByEntity(MLCourseWareEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<MLCourseWareEntity> PagedQueryByEntity(MLCourseWareEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(MLCourseWareEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.CourseWareId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CourseWareId", Value = pQueryEntity.CourseWareId });
            if (pQueryEntity.OnlineCourseId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OnlineCourseId", Value = pQueryEntity.OnlineCourseId });
            if (pQueryEntity.OriginalName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OriginalName", Value = pQueryEntity.OriginalName });
            if (pQueryEntity.CourseWareFile!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CourseWareFile", Value = pQueryEntity.CourseWareFile });
            if (pQueryEntity.ExtName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ExtName", Value = pQueryEntity.ExtName });
            if (pQueryEntity.Icon!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Icon", Value = pQueryEntity.Icon });
            if (pQueryEntity.Downloadable!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Downloadable", Value = pQueryEntity.Downloadable });
            if (pQueryEntity.ContentId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ContentId", Value = pQueryEntity.ContentId });
            if (pQueryEntity.CourseWareIndex!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CourseWareIndex", Value = pQueryEntity.CourseWareIndex });
            if (pQueryEntity.CustomerID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });
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
            if (pQueryEntity.CourseWareSize!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CourseWareSize", Value = pQueryEntity.CourseWareSize });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out MLCourseWareEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new MLCourseWareEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["CourseWareId"] != DBNull.Value)
			{
				pInstance.CourseWareId =  Convert.ToString(pReader["CourseWareId"]);
			}
			if (pReader["OnlineCourseId"] != DBNull.Value)
			{
				pInstance.OnlineCourseId =  Convert.ToString(pReader["OnlineCourseId"]);
			}
			if (pReader["OriginalName"] != DBNull.Value)
			{
				pInstance.OriginalName =  Convert.ToString(pReader["OriginalName"]);
			}
			if (pReader["CourseWareFile"] != DBNull.Value)
			{
				pInstance.CourseWareFile =  Convert.ToString(pReader["CourseWareFile"]);
			}
			if (pReader["ExtName"] != DBNull.Value)
			{
				pInstance.ExtName =  Convert.ToString(pReader["ExtName"]);
			}
			if (pReader["Icon"] != DBNull.Value)
			{
				pInstance.Icon =  Convert.ToString(pReader["Icon"]);
			}
			if (pReader["Downloadable"] != DBNull.Value)
			{
				pInstance.Downloadable =   Convert.ToInt32(pReader["Downloadable"]);
			}
			if (pReader["ContentId"] != DBNull.Value)
			{
				pInstance.ContentId =  Convert.ToString(pReader["ContentId"]);
			}
			if (pReader["CourseWareIndex"] != DBNull.Value)
			{
				pInstance.CourseWareIndex =   Convert.ToInt32(pReader["CourseWareIndex"]);
			}
			if (pReader["CustomerID"] != DBNull.Value)
			{
				pInstance.CustomerID =  Convert.ToString(pReader["CustomerID"]);
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
			if (pReader["CourseWareSize"] != DBNull.Value)
			{
				pInstance.CourseWareSize =  Convert.ToString(pReader["CourseWareSize"]);
			}

        }
        #endregion
    }
}
