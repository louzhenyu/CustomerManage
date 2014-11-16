/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/11/19 15:40:35
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
    /// ��ZCourse�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class ZCourseDAO : Base.BaseCPOSDAO, ICRUDable<ZCourseEntity>, IQueryable<ZCourseEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public ZCourseDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(ZCourseEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(ZCourseEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [ZCourse](");
            strSql.Append("[CouseDesc],[CourseTypeId],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[CourseName],[CourseSummary],[CourseFee],[CourseStartTime],[CouseCapital],[CouseContact],[Email],[EmailTitle],[ParentId],[CourseLevel],[CourseId])");
            strSql.Append(" values (");
            strSql.Append("@CouseDesc,@CourseTypeId,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@CourseName,@CourseSummary,@CourseFee,@CourseStartTime,@CouseCapital,@CouseContact,@Email,@EmailTitle,@ParentId,@CourseLevel,@CourseId)");            

			string pkString = pEntity.CourseId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@CouseDesc",SqlDbType.NVarChar),
					new SqlParameter("@CourseTypeId",SqlDbType.Int),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CourseName",SqlDbType.NVarChar),
					new SqlParameter("@CourseSummary",SqlDbType.NVarChar),
					new SqlParameter("@CourseFee",SqlDbType.NVarChar),
					new SqlParameter("@CourseStartTime",SqlDbType.NVarChar),
					new SqlParameter("@CouseCapital",SqlDbType.NVarChar),
					new SqlParameter("@CouseContact",SqlDbType.NVarChar),
					new SqlParameter("@Email",SqlDbType.NVarChar),
					new SqlParameter("@EmailTitle",SqlDbType.NVarChar),
					new SqlParameter("@ParentId",SqlDbType.NVarChar),
					new SqlParameter("@CourseLevel",SqlDbType.Int),
					new SqlParameter("@CourseId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.CouseDesc;
			parameters[1].Value = pEntity.CourseTypeId;
			parameters[2].Value = pEntity.CreateTime;
			parameters[3].Value = pEntity.CreateBy;
			parameters[4].Value = pEntity.LastUpdateBy;
			parameters[5].Value = pEntity.LastUpdateTime;
			parameters[6].Value = pEntity.IsDelete;
			parameters[7].Value = pEntity.CourseName;
			parameters[8].Value = pEntity.CourseSummary;
			parameters[9].Value = pEntity.CourseFee;
			parameters[10].Value = pEntity.CourseStartTime;
			parameters[11].Value = pEntity.CouseCapital;
			parameters[12].Value = pEntity.CouseContact;
			parameters[13].Value = pEntity.Email;
			parameters[14].Value = pEntity.EmailTitle;
			parameters[15].Value = pEntity.ParentId;
			parameters[16].Value = pEntity.CourseLevel;
			parameters[17].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.CourseId = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public ZCourseEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ZCourse] where CourseId='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            ZCourseEntity m = null;
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
        public ZCourseEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ZCourse] where isdelete=0");
            //��ȡ����
            List<ZCourseEntity> list = new List<ZCourseEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    ZCourseEntity m;
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
        public void Update(ZCourseEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(ZCourseEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.CourseId==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [ZCourse] set ");
            if (pIsUpdateNullField || pEntity.CouseDesc!=null)
                strSql.Append( "[CouseDesc]=@CouseDesc,");
            if (pIsUpdateNullField || pEntity.CourseTypeId!=null)
                strSql.Append( "[CourseTypeId]=@CourseTypeId,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.CourseName!=null)
                strSql.Append( "[CourseName]=@CourseName,");
            if (pIsUpdateNullField || pEntity.CourseSummary!=null)
                strSql.Append( "[CourseSummary]=@CourseSummary,");
            if (pIsUpdateNullField || pEntity.CourseFee!=null)
                strSql.Append( "[CourseFee]=@CourseFee,");
            if (pIsUpdateNullField || pEntity.CourseStartTime!=null)
                strSql.Append( "[CourseStartTime]=@CourseStartTime,");
            if (pIsUpdateNullField || pEntity.CouseCapital!=null)
                strSql.Append( "[CouseCapital]=@CouseCapital,");
            if (pIsUpdateNullField || pEntity.CouseContact!=null)
                strSql.Append( "[CouseContact]=@CouseContact,");
            if (pIsUpdateNullField || pEntity.Email!=null)
                strSql.Append( "[Email]=@Email,");
            if (pIsUpdateNullField || pEntity.EmailTitle!=null)
                strSql.Append( "[EmailTitle]=@EmailTitle,");
            if (pIsUpdateNullField || pEntity.ParentId!=null)
                strSql.Append( "[ParentId]=@ParentId,");
            if (pIsUpdateNullField || pEntity.CourseLevel!=null)
                strSql.Append( "[CourseLevel]=@CourseLevel");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where CourseId=@CourseId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@CouseDesc",SqlDbType.NVarChar),
					new SqlParameter("@CourseTypeId",SqlDbType.Int),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CourseName",SqlDbType.NVarChar),
					new SqlParameter("@CourseSummary",SqlDbType.NVarChar),
					new SqlParameter("@CourseFee",SqlDbType.NVarChar),
					new SqlParameter("@CourseStartTime",SqlDbType.NVarChar),
					new SqlParameter("@CouseCapital",SqlDbType.NVarChar),
					new SqlParameter("@CouseContact",SqlDbType.NVarChar),
					new SqlParameter("@Email",SqlDbType.NVarChar),
					new SqlParameter("@EmailTitle",SqlDbType.NVarChar),
					new SqlParameter("@ParentId",SqlDbType.NVarChar),
					new SqlParameter("@CourseLevel",SqlDbType.Int),
					new SqlParameter("@CourseId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.CouseDesc;
			parameters[1].Value = pEntity.CourseTypeId;
			parameters[2].Value = pEntity.LastUpdateBy;
			parameters[3].Value = pEntity.LastUpdateTime;
			parameters[4].Value = pEntity.CourseName;
			parameters[5].Value = pEntity.CourseSummary;
			parameters[6].Value = pEntity.CourseFee;
			parameters[7].Value = pEntity.CourseStartTime;
			parameters[8].Value = pEntity.CouseCapital;
			parameters[9].Value = pEntity.CouseContact;
			parameters[10].Value = pEntity.Email;
			parameters[11].Value = pEntity.EmailTitle;
			parameters[12].Value = pEntity.ParentId;
			parameters[13].Value = pEntity.CourseLevel;
			parameters[14].Value = pEntity.CourseId;

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
        public void Update(ZCourseEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(ZCourseEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(ZCourseEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(ZCourseEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.CourseId==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.CourseId, pTran);           
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
            sql.AppendLine("update [ZCourse] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where CourseId=@CourseId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@CourseId",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(ZCourseEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.CourseId==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.CourseId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(ZCourseEntity[] pEntities)
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
            sql.AppendLine("update [ZCourse] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where CourseId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public ZCourseEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ZCourse] where isdelete=0 ");
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
            List<ZCourseEntity> list = new List<ZCourseEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    ZCourseEntity m;
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
        public PagedQueryResult<ZCourseEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [CourseId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [ZCourse] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [ZCourse] where isdelete=0 ");
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
            PagedQueryResult<ZCourseEntity> result = new PagedQueryResult<ZCourseEntity>();
            List<ZCourseEntity> list = new List<ZCourseEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    ZCourseEntity m;
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
        public ZCourseEntity[] QueryByEntity(ZCourseEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<ZCourseEntity> PagedQueryByEntity(ZCourseEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(ZCourseEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.CourseId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CourseId", Value = pQueryEntity.CourseId });
            if (pQueryEntity.CouseDesc!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CouseDesc", Value = pQueryEntity.CouseDesc });
            if (pQueryEntity.CourseTypeId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CourseTypeId", Value = pQueryEntity.CourseTypeId });
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
            if (pQueryEntity.CourseName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CourseName", Value = pQueryEntity.CourseName });
            if (pQueryEntity.CourseSummary!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CourseSummary", Value = pQueryEntity.CourseSummary });
            if (pQueryEntity.CourseFee!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CourseFee", Value = pQueryEntity.CourseFee });
            if (pQueryEntity.CourseStartTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CourseStartTime", Value = pQueryEntity.CourseStartTime });
            if (pQueryEntity.CouseCapital!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CouseCapital", Value = pQueryEntity.CouseCapital });
            if (pQueryEntity.CouseContact!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CouseContact", Value = pQueryEntity.CouseContact });
            if (pQueryEntity.Email!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Email", Value = pQueryEntity.Email });
            if (pQueryEntity.EmailTitle!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EmailTitle", Value = pQueryEntity.EmailTitle });
            if (pQueryEntity.ParentId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ParentId", Value = pQueryEntity.ParentId });
            if (pQueryEntity.CourseLevel!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CourseLevel", Value = pQueryEntity.CourseLevel });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out ZCourseEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new ZCourseEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["CourseId"] != DBNull.Value)
			{
				pInstance.CourseId =  Convert.ToString(pReader["CourseId"]);
			}
			if (pReader["CouseDesc"] != DBNull.Value)
			{
				pInstance.CouseDesc =  Convert.ToString(pReader["CouseDesc"]);
			}
			if (pReader["CourseTypeId"] != DBNull.Value)
			{
				pInstance.CourseTypeId =   Convert.ToInt32(pReader["CourseTypeId"]);
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
			if (pReader["CourseName"] != DBNull.Value)
			{
				pInstance.CourseName =  Convert.ToString(pReader["CourseName"]);
			}
			if (pReader["CourseSummary"] != DBNull.Value)
			{
				pInstance.CourseSummary =  Convert.ToString(pReader["CourseSummary"]);
			}
			if (pReader["CourseFee"] != DBNull.Value)
			{
				pInstance.CourseFee =  Convert.ToString(pReader["CourseFee"]);
			}
			if (pReader["CourseStartTime"] != DBNull.Value)
			{
				pInstance.CourseStartTime =  Convert.ToString(pReader["CourseStartTime"]);
			}
			if (pReader["CouseCapital"] != DBNull.Value)
			{
				pInstance.CouseCapital =  Convert.ToString(pReader["CouseCapital"]);
			}
			if (pReader["CouseContact"] != DBNull.Value)
			{
				pInstance.CouseContact =  Convert.ToString(pReader["CouseContact"]);
			}
			if (pReader["Email"] != DBNull.Value)
			{
				pInstance.Email =  Convert.ToString(pReader["Email"]);
			}
			if (pReader["EmailTitle"] != DBNull.Value)
			{
				pInstance.EmailTitle =  Convert.ToString(pReader["EmailTitle"]);
			}
			if (pReader["ParentId"] != DBNull.Value)
			{
				pInstance.ParentId =  Convert.ToString(pReader["ParentId"]);
			}
			if (pReader["CourseLevel"] != DBNull.Value)
			{
				pInstance.CourseLevel =   Convert.ToInt32(pReader["CourseLevel"]);
			}

        }
        #endregion
    }
}
