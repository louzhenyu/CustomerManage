/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/4/22 16:57:28
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
using JIT.CPOS.BS.DataAccess.Base;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Utility;

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// ���ݷ��ʣ� �ݷ����� 
    /// ��VisitingTask�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class VisitingTaskDAO : BaseCPOSDAO, ICRUDable<VisitingTaskEntity>, IQueryable<VisitingTaskEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VisitingTaskDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo, true)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(VisitingTaskEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(VisitingTaskEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [VisitingTask](");
            strSql.Append("[VisitingTaskNo],[VisitingTaskName],[VisitingTaskNameEn],[VisitingTaskType],[ClientPositionID],[POPType],[POPGroupID],[StartDate],[EndDate],[StartGPSType],[EndGPSType],[StartPic],[EndPic],[TaskPriority],[IsCombin],[Remark],[ClientID],[ClientDistributorID],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[VisitingTaskID])");
            strSql.Append(" values (");
            strSql.Append("@VisitingTaskNo,@VisitingTaskName,@VisitingTaskNameEn,@VisitingTaskType,@ClientPositionID,@POPType,@POPGroupID,@StartDate,@EndDate,@StartGPSType,@EndGPSType,@StartPic,@EndPic,@TaskPriority,@IsCombin,@Remark,@ClientID,@ClientDistributorID,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@VisitingTaskID)");            

			Guid? pkGuid;
			if (pEntity.VisitingTaskID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.VisitingTaskID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@VisitingTaskNo",SqlDbType.NVarChar,100),
					new SqlParameter("@VisitingTaskName",SqlDbType.NVarChar,100),
					new SqlParameter("@VisitingTaskNameEn",SqlDbType.NVarChar,100),
					new SqlParameter("@VisitingTaskType",SqlDbType.Int),
					new SqlParameter("@ClientPositionID",SqlDbType.VarChar,36),
					new SqlParameter("@POPType",SqlDbType.Int),
					new SqlParameter("@POPGroupID",SqlDbType.Int),
					new SqlParameter("@StartDate",SqlDbType.DateTime),
					new SqlParameter("@EndDate",SqlDbType.DateTime),
					new SqlParameter("@StartGPSType",SqlDbType.Int),
					new SqlParameter("@EndGPSType",SqlDbType.Int),
					new SqlParameter("@StartPic",SqlDbType.Int),
					new SqlParameter("@EndPic",SqlDbType.Int),
					new SqlParameter("@TaskPriority",SqlDbType.Int),
					new SqlParameter("@IsCombin",SqlDbType.Int),
					new SqlParameter("@Remark",SqlDbType.NVarChar,400),
					new SqlParameter("@ClientID",SqlDbType.VarChar,36),
					new SqlParameter("@ClientDistributorID",SqlDbType.Int),
					new SqlParameter("@CreateBy",SqlDbType.VarChar,36),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar,36),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@VisitingTaskID",SqlDbType.UniqueIdentifier,16)
            };
			parameters[0].Value = pEntity.VisitingTaskNo;
			parameters[1].Value = pEntity.VisitingTaskName;
			parameters[2].Value = pEntity.VisitingTaskNameEn;
			parameters[3].Value = pEntity.VisitingTaskType;
			parameters[4].Value = pEntity.ClientPositionID;
			parameters[5].Value = pEntity.POPType;
			parameters[6].Value = pEntity.POPGroupID;
			parameters[7].Value = pEntity.StartDate;
			parameters[8].Value = pEntity.EndDate;
			parameters[9].Value = pEntity.StartGPSType;
			parameters[10].Value = pEntity.EndGPSType;
			parameters[11].Value = pEntity.StartPic;
			parameters[12].Value = pEntity.EndPic;
			parameters[13].Value = pEntity.TaskPriority;
			parameters[14].Value = pEntity.IsCombin;
			parameters[15].Value = pEntity.Remark;
			parameters[16].Value = pEntity.ClientID;
			parameters[17].Value = pEntity.ClientDistributorID;
			parameters[18].Value = pEntity.CreateBy;
			parameters[19].Value = pEntity.CreateTime;
			parameters[20].Value = pEntity.LastUpdateBy;
			parameters[21].Value = pEntity.LastUpdateTime;
			parameters[22].Value = pEntity.IsDelete;
			parameters[23].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.VisitingTaskID = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public VisitingTaskEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VisitingTask] where VisitingTaskID='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            VisitingTaskEntity m = null;
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
        public VisitingTaskEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VisitingTask] where isdelete=0");
            //��ȡ����
            List<VisitingTaskEntity> list = new List<VisitingTaskEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VisitingTaskEntity m;
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
        public void Update(VisitingTaskEntity pEntity , IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.VisitingTaskID.HasValue)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [VisitingTask] set ");
            strSql.Append("[VisitingTaskNo]=@VisitingTaskNo,[VisitingTaskName]=@VisitingTaskName,[VisitingTaskNameEn]=@VisitingTaskNameEn,[VisitingTaskType]=@VisitingTaskType,[ClientPositionID]=@ClientPositionID,[POPType]=@POPType,[POPGroupID]=@POPGroupID,[StartDate]=@StartDate,[EndDate]=@EndDate,[StartGPSType]=@StartGPSType,[EndGPSType]=@EndGPSType,[StartPic]=@StartPic,[EndPic]=@EndPic,[TaskPriority]=@TaskPriority,[IsCombin]=@IsCombin,[Remark]=@Remark,[ClientID]=@ClientID,[ClientDistributorID]=@ClientDistributorID,[LastUpdateBy]=@LastUpdateBy,[LastUpdateTime]=@LastUpdateTime");
            strSql.Append(" where VisitingTaskID=@VisitingTaskID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@VisitingTaskNo",SqlDbType.NVarChar,100),
					new SqlParameter("@VisitingTaskName",SqlDbType.NVarChar,100),
					new SqlParameter("@VisitingTaskNameEn",SqlDbType.NVarChar,100),
					new SqlParameter("@VisitingTaskType",SqlDbType.Int),
					new SqlParameter("@ClientPositionID",SqlDbType.VarChar,36),
					new SqlParameter("@POPType",SqlDbType.Int),
					new SqlParameter("@POPGroupID",SqlDbType.Int),
					new SqlParameter("@StartDate",SqlDbType.DateTime),
					new SqlParameter("@EndDate",SqlDbType.DateTime),
					new SqlParameter("@StartGPSType",SqlDbType.Int),
					new SqlParameter("@EndGPSType",SqlDbType.Int),
					new SqlParameter("@StartPic",SqlDbType.Int),
					new SqlParameter("@EndPic",SqlDbType.Int),
					new SqlParameter("@TaskPriority",SqlDbType.Int),
					new SqlParameter("@IsCombin",SqlDbType.Int),
					new SqlParameter("@Remark",SqlDbType.NVarChar,400),
					new SqlParameter("@ClientID",SqlDbType.VarChar,36),
					new SqlParameter("@ClientDistributorID",SqlDbType.Int),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar,36),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@VisitingTaskID",SqlDbType.UniqueIdentifier,16)
            };
			parameters[0].Value = pEntity.VisitingTaskNo;
			parameters[1].Value = pEntity.VisitingTaskName;
			parameters[2].Value = pEntity.VisitingTaskNameEn;
			parameters[3].Value = pEntity.VisitingTaskType;
			parameters[4].Value = pEntity.ClientPositionID;
			parameters[5].Value = pEntity.POPType;
			parameters[6].Value = pEntity.POPGroupID;
			parameters[7].Value = pEntity.StartDate;
			parameters[8].Value = pEntity.EndDate;
			parameters[9].Value = pEntity.StartGPSType;
			parameters[10].Value = pEntity.EndGPSType;
			parameters[11].Value = pEntity.StartPic;
			parameters[12].Value = pEntity.EndPic;
			parameters[13].Value = pEntity.TaskPriority;
			parameters[14].Value = pEntity.IsCombin;
			parameters[15].Value = pEntity.Remark;
			parameters[16].Value = pEntity.ClientID;
			parameters[17].Value = pEntity.ClientDistributorID;
			parameters[18].Value = pEntity.LastUpdateBy;
			parameters[19].Value = pEntity.LastUpdateTime;
			parameters[20].Value = pEntity.VisitingTaskID;

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
        public void Update(VisitingTaskEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(VisitingTaskEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(VisitingTaskEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.VisitingTaskID.HasValue)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.VisitingTaskID.Value, pTran);           
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
            sql.AppendLine("update [VisitingTask] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where VisitingTaskID=@VisitingTaskID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=CurrentUserInfo.UserID},
                new SqlParameter{ParameterName="@VisitingTaskID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(VisitingTaskEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (!item.VisitingTaskID.HasValue)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.VisitingTaskID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(VisitingTaskEntity[] pEntities)
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
            sql.AppendLine("update [VisitingTask] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='" + CurrentUserInfo.UserID + "',IsDelete=1 where VisitingTaskID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public VisitingTaskEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VisitingTask] where isdelete=0 ");
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
            List<VisitingTaskEntity> list = new List<VisitingTaskEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VisitingTaskEntity m;
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
        public PagedQueryResult<VisitingTaskEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [VisitingTaskID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [VisitingTask] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [VisitingTask] where isdelete=0 ");
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
            PagedQueryResult<VisitingTaskEntity> result = new PagedQueryResult<VisitingTaskEntity>();
            List<VisitingTaskEntity> list = new List<VisitingTaskEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    VisitingTaskEntity m;
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
        public VisitingTaskEntity[] QueryByEntity(VisitingTaskEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<VisitingTaskEntity> PagedQueryByEntity(VisitingTaskEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(VisitingTaskEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.VisitingTaskID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VisitingTaskID", Value = pQueryEntity.VisitingTaskID });
            if (pQueryEntity.VisitingTaskNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VisitingTaskNo", Value = pQueryEntity.VisitingTaskNo });
            if (pQueryEntity.VisitingTaskName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VisitingTaskName", Value = pQueryEntity.VisitingTaskName });
            if (pQueryEntity.VisitingTaskNameEn!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VisitingTaskNameEn", Value = pQueryEntity.VisitingTaskNameEn });
            if (pQueryEntity.VisitingTaskType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VisitingTaskType", Value = pQueryEntity.VisitingTaskType });
            if (pQueryEntity.ClientPositionID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ClientPositionID", Value = pQueryEntity.ClientPositionID });
            if (pQueryEntity.POPType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "POPType", Value = pQueryEntity.POPType });
            if (pQueryEntity.POPGroupID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "POPGroupID", Value = pQueryEntity.POPGroupID });
            if (pQueryEntity.StartDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StartDate", Value = pQueryEntity.StartDate });
            if (pQueryEntity.EndDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EndDate", Value = pQueryEntity.EndDate });
            if (pQueryEntity.StartGPSType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StartGPSType", Value = pQueryEntity.StartGPSType });
            if (pQueryEntity.EndGPSType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EndGPSType", Value = pQueryEntity.EndGPSType });
            if (pQueryEntity.StartPic!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StartPic", Value = pQueryEntity.StartPic });
            if (pQueryEntity.EndPic!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EndPic", Value = pQueryEntity.EndPic });
            if (pQueryEntity.TaskPriority!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TaskPriority", Value = pQueryEntity.TaskPriority });
            if (pQueryEntity.IsCombin!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsCombin", Value = pQueryEntity.IsCombin });
            if (pQueryEntity.Remark!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Remark", Value = pQueryEntity.Remark });
            if (pQueryEntity.ClientID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ClientID", Value = pQueryEntity.ClientID });
            if (pQueryEntity.ClientDistributorID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ClientDistributorID", Value = pQueryEntity.ClientDistributorID });
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
        protected void Load(SqlDataReader pReader, out VisitingTaskEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new VisitingTaskEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["VisitingTaskID"] != DBNull.Value)
			{
				pInstance.VisitingTaskID =  (Guid)pReader["VisitingTaskID"];
			}
			if (pReader["VisitingTaskNo"] != DBNull.Value)
			{
				pInstance.VisitingTaskNo =  Convert.ToString(pReader["VisitingTaskNo"]);
			}
			if (pReader["VisitingTaskName"] != DBNull.Value)
			{
				pInstance.VisitingTaskName =  Convert.ToString(pReader["VisitingTaskName"]);
			}
			if (pReader["VisitingTaskNameEn"] != DBNull.Value)
			{
				pInstance.VisitingTaskNameEn =  Convert.ToString(pReader["VisitingTaskNameEn"]);
			}
			if (pReader["VisitingTaskType"] != DBNull.Value)
			{
				pInstance.VisitingTaskType =   Convert.ToInt32(pReader["VisitingTaskType"]);
			}
			if (pReader["ClientPositionID"] != DBNull.Value)
			{
				pInstance.ClientPositionID =   pReader["ClientPositionID"].ToString();
			}
			if (pReader["POPType"] != DBNull.Value)
			{
				pInstance.POPType =   Convert.ToInt32(pReader["POPType"]);
			}
			if (pReader["POPGroupID"] != DBNull.Value)
			{
				pInstance.POPGroupID =   Convert.ToInt32(pReader["POPGroupID"]);
			}
			if (pReader["StartDate"] != DBNull.Value)
			{
				pInstance.StartDate =  Convert.ToDateTime(pReader["StartDate"]);
			}
			if (pReader["EndDate"] != DBNull.Value)
			{
				pInstance.EndDate =  Convert.ToDateTime(pReader["EndDate"]);
			}
			if (pReader["StartGPSType"] != DBNull.Value)
			{
				pInstance.StartGPSType =   Convert.ToInt32(pReader["StartGPSType"]);
			}
			if (pReader["EndGPSType"] != DBNull.Value)
			{
				pInstance.EndGPSType =   Convert.ToInt32(pReader["EndGPSType"]);
			}
			if (pReader["StartPic"] != DBNull.Value)
			{
				pInstance.StartPic =   Convert.ToInt32(pReader["StartPic"]);
			}
			if (pReader["EndPic"] != DBNull.Value)
			{
				pInstance.EndPic =   Convert.ToInt32(pReader["EndPic"]);
			}
			if (pReader["TaskPriority"] != DBNull.Value)
			{
				pInstance.TaskPriority =   Convert.ToInt32(pReader["TaskPriority"]);
			}
			if (pReader["IsCombin"] != DBNull.Value)
			{
				pInstance.IsCombin =   Convert.ToInt32(pReader["IsCombin"]);
			}
			if (pReader["Remark"] != DBNull.Value)
			{
				pInstance.Remark =  Convert.ToString(pReader["Remark"]);
			}
			if (pReader["ClientID"] != DBNull.Value)
			{
				pInstance.ClientID =   pReader["ClientID"].ToString();
			}
			if (pReader["ClientDistributorID"] != DBNull.Value)
			{
				pInstance.ClientDistributorID =   Convert.ToInt32(pReader["ClientDistributorID"]);
			}
			if (pReader["CreateBy"] != DBNull.Value)
			{
				pInstance.CreateBy =   pReader["CreateBy"].ToString();
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}
			if (pReader["LastUpdateBy"] != DBNull.Value)
			{
				pInstance.LastUpdateBy =   pReader["LastUpdateBy"].ToString();
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
