/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/4/16 13:49:49
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
    /// ���ݷ��ʣ� �ݷò���ѡ��ֵ 
    /// ��VisitingParameterOptions�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class VisitingParameterOptionsDAO : BaseCPOSDAO, ICRUDable<VisitingParameterOptionsEntity>, IQueryable<VisitingParameterOptionsEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VisitingParameterOptionsDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo, true)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(VisitingParameterOptionsEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(VisitingParameterOptionsEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [VisitingParameterOptions](");
            strSql.Append("[OptionName],[OptionValue],[OptionText],[OptionTextEn],[Sequence],[ClientID],[ClientDistributorID],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[VisitingParameterOptionsID])");
            strSql.Append(" values (");
            strSql.Append("@OptionName,@OptionValue,@OptionText,@OptionTextEn,@Sequence,@ClientID,@ClientDistributorID,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@VisitingParameterOptionsID)");

            Guid? pkGuid;
            if (pEntity.VisitingParameterOptionsID == null)
                pkGuid = Guid.NewGuid();
            else
                pkGuid = pEntity.VisitingParameterOptionsID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@OptionName",SqlDbType.NVarChar,100),
					new SqlParameter("@OptionValue",SqlDbType.Int),
					new SqlParameter("@OptionText",SqlDbType.NVarChar,100),
					new SqlParameter("@OptionTextEn",SqlDbType.NVarChar,100),
					new SqlParameter("@Sequence",SqlDbType.Int),
					new SqlParameter("@ClientID",SqlDbType.VarChar,36),
					new SqlParameter("@ClientDistributorID",SqlDbType.Int),
					new SqlParameter("@CreateBy",SqlDbType.VarChar,36),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar,36),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@VisitingParameterOptionsID",SqlDbType.UniqueIdentifier,16)
            };
            parameters[0].Value = pEntity.OptionName;
            parameters[1].Value = pEntity.OptionValue;
            parameters[2].Value = pEntity.OptionText;
            parameters[3].Value = pEntity.OptionTextEn;
            parameters[4].Value = pEntity.Sequence;
            parameters[5].Value = pEntity.ClientID;
            parameters[6].Value = pEntity.ClientDistributorID;
            parameters[7].Value = pEntity.CreateBy;
            parameters[8].Value = pEntity.CreateTime;
            parameters[9].Value = pEntity.LastUpdateBy;
            parameters[10].Value = pEntity.LastUpdateTime;
            parameters[11].Value = pEntity.IsDelete;
            parameters[12].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            pEntity.VisitingParameterOptionsID = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public VisitingParameterOptionsEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VisitingParameterOptions] where VisitingParameterOptionsID='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            VisitingParameterOptionsEntity m = null;
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
        public VisitingParameterOptionsEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VisitingParameterOptions] where isdelete=0");
            //��ȡ����
            List<VisitingParameterOptionsEntity> list = new List<VisitingParameterOptionsEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VisitingParameterOptionsEntity m;
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
        public void Update(VisitingParameterOptionsEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.VisitingParameterOptionsID.HasValue)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [VisitingParameterOptions] set ");
            strSql.Append("[OptionName]=@OptionName,[OptionValue]=@OptionValue,[OptionText]=@OptionText,[OptionTextEn]=@OptionTextEn,[Sequence]=@Sequence,[ClientID]=@ClientID,[ClientDistributorID]=@ClientDistributorID,[LastUpdateBy]=@LastUpdateBy,[LastUpdateTime]=@LastUpdateTime");
            strSql.Append(" where VisitingParameterOptionsID=@VisitingParameterOptionsID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@OptionName",SqlDbType.NVarChar,100),
					new SqlParameter("@OptionValue",SqlDbType.Int),
					new SqlParameter("@OptionText",SqlDbType.NVarChar,100),
					new SqlParameter("@OptionTextEn",SqlDbType.NVarChar,100),
					new SqlParameter("@Sequence",SqlDbType.Int),
					new SqlParameter("@ClientID",SqlDbType.VarChar,36),
					new SqlParameter("@ClientDistributorID",SqlDbType.Int),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar,36),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@VisitingParameterOptionsID",SqlDbType.UniqueIdentifier,16)
            };
            parameters[0].Value = pEntity.OptionName;
            parameters[1].Value = pEntity.OptionValue;
            parameters[2].Value = pEntity.OptionText;
            parameters[3].Value = pEntity.OptionTextEn;
            parameters[4].Value = pEntity.Sequence;
            parameters[5].Value = pEntity.ClientID;
            parameters[6].Value = pEntity.ClientDistributorID;
            parameters[7].Value = pEntity.LastUpdateBy;
            parameters[8].Value = pEntity.LastUpdateTime;
            parameters[9].Value = pEntity.VisitingParameterOptionsID;

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
        public void Update(VisitingParameterOptionsEntity pEntity)
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(VisitingParameterOptionsEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(VisitingParameterOptionsEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.VisitingParameterOptionsID.HasValue)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.VisitingParameterOptionsID.Value, pTran);
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
            sql.AppendLine("update [VisitingParameterOptions] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where VisitingParameterOptionsID=@VisitingParameterOptionsID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=CurrentUserInfo.UserID},
                new SqlParameter{ParameterName="@VisitingParameterOptionsID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(VisitingParameterOptionsEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (!item.VisitingParameterOptionsID.HasValue)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.VisitingParameterOptionsID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(VisitingParameterOptionsEntity[] pEntities)
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
            sql.AppendLine("update [VisitingParameterOptions] set LastUpdateTime='" + DateTime.Now.ToString() + "',LastUpdateBy='" + CurrentUserInfo.UserID + "',IsDelete=1 where VisitingParameterOptionsID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public VisitingParameterOptionsEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VisitingParameterOptions] where isdelete=0 ");
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
            List<VisitingParameterOptionsEntity> list = new List<VisitingParameterOptionsEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VisitingParameterOptionsEntity m;
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
        public PagedQueryResult<VisitingParameterOptionsEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [VisitingParameterOptionsID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [VisitingParameterOptions] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [VisitingParameterOptions] where isdelete=0 ");
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
            PagedQueryResult<VisitingParameterOptionsEntity> result = new PagedQueryResult<VisitingParameterOptionsEntity>();
            List<VisitingParameterOptionsEntity> list = new List<VisitingParameterOptionsEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    VisitingParameterOptionsEntity m;
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
        public VisitingParameterOptionsEntity[] QueryByEntity(VisitingParameterOptionsEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<VisitingParameterOptionsEntity> PagedQueryByEntity(VisitingParameterOptionsEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(VisitingParameterOptionsEntity pQueryEntity)
        {
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.VisitingParameterOptionsID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VisitingParameterOptionsID", Value = pQueryEntity.VisitingParameterOptionsID });
            if (pQueryEntity.OptionName != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OptionName", Value = pQueryEntity.OptionName });
            if (pQueryEntity.OptionValue != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OptionValue", Value = pQueryEntity.OptionValue });
            if (pQueryEntity.OptionText != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OptionText", Value = pQueryEntity.OptionText });
            if (pQueryEntity.OptionTextEn != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OptionTextEn", Value = pQueryEntity.OptionTextEn });
            if (pQueryEntity.Sequence != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Sequence", Value = pQueryEntity.Sequence });
            if (pQueryEntity.ClientID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ClientID", Value = pQueryEntity.ClientID });
            if (pQueryEntity.ClientDistributorID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ClientDistributorID", Value = pQueryEntity.ClientDistributorID });
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

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out VisitingParameterOptionsEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new VisitingParameterOptionsEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["VisitingParameterOptionsID"] != DBNull.Value)
            {
                pInstance.VisitingParameterOptionsID = (Guid)pReader["VisitingParameterOptionsID"];
            }
            if (pReader["OptionName"] != DBNull.Value)
            {
                pInstance.OptionName = Convert.ToString(pReader["OptionName"]);
            }
            if (pReader["OptionValue"] != DBNull.Value)
            {
                pInstance.OptionValue = Convert.ToInt32(pReader["OptionValue"]);
            }
            if (pReader["OptionText"] != DBNull.Value)
            {
                pInstance.OptionText = Convert.ToString(pReader["OptionText"]);
            }
            if (pReader["OptionTextEn"] != DBNull.Value)
            {
                pInstance.OptionTextEn = Convert.ToString(pReader["OptionTextEn"]);
            }
            if (pReader["Sequence"] != DBNull.Value)
            {
                pInstance.Sequence = Convert.ToInt32(pReader["Sequence"]);
            }
            if (pReader["ClientID"] != DBNull.Value)
            {
                pInstance.ClientID = pReader["ClientID"].ToString();
            }
            if (pReader["ClientDistributorID"] != DBNull.Value)
            {
                pInstance.ClientDistributorID = Convert.ToInt32(pReader["ClientDistributorID"]);
            }
            if (pReader["CreateBy"] != DBNull.Value)
            {
                pInstance.CreateBy = pReader["CreateBy"].ToString();
            }
            if (pReader["CreateTime"] != DBNull.Value)
            {
                pInstance.CreateTime = Convert.ToDateTime(pReader["CreateTime"]);
            }
            if (pReader["LastUpdateBy"] != DBNull.Value)
            {
                pInstance.LastUpdateBy = pReader["LastUpdateBy"].ToString();
            }
            if (pReader["LastUpdateTime"] != DBNull.Value)
            {
                pInstance.LastUpdateTime = Convert.ToDateTime(pReader["LastUpdateTime"]);
            }
            if (pReader["IsDelete"] != DBNull.Value)
            {
                pInstance.IsDelete = Convert.ToInt32(pReader["IsDelete"]);
            }

        }
        #endregion
    }
}
