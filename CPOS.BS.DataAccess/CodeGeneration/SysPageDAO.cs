/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/12 14:35:53
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
    /// ��SysPage�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class SysPageDAO : BaseCPOSDAO, ICRUDable<SysPageEntity>, IQueryable<SysPageEntity>
    {
        #region ���캯��
        public string StaticConnectionString { get; set; }
        private ISQLHelper staticSqlHelper;
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public SysPageDAO(LoggingSessionInfo pUserInfo, string connectionString)
            : base(pUserInfo)
        {
            this.StaticConnectionString = connectionString;
            this.SQLHelper = StaticSqlHelper;
        }
        #endregion

        protected ISQLHelper StaticSqlHelper
        {
            get
            {
                if (null == staticSqlHelper)
                    staticSqlHelper = new DefaultSQLHelper(StaticConnectionString);
                return staticSqlHelper;
            }
        }

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(SysPageEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(SysPageEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");

            //��ʼ���̶��ֶ�
            pEntity.IsDelete = 0;
            pEntity.CreateTime = DateTime.Now;
            pEntity.LastUpdateTime = pEntity.CreateTime;
            pEntity.CreateBy = CurrentUserInfo.UserID;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [SysPage](");
            strSql.Append("[PageKey],[Title],[ModuleName],[IsEntrance],[URLTemplate],[JsonValue],[PageCode],[IsAuth],[IsRebuild],[Version],[Author],[DefaultHtml],[Remark],[CustomerID],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[CustomerVisible],[PageID])");
            strSql.Append(" values (");
            strSql.Append("@PageKey,@Title,@ModuleName,@IsEntrance,@URLTemplate,@JsonValue,@PageCode,@IsAuth,@IsRebuild,@Version,@Author,@DefaultHtml,@Remark,@CustomerID,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@CustomerVisible,@PageID)");            

            Guid? pkGuid;
            if (pEntity.PageID == null)
                pkGuid = Guid.NewGuid();
            else
                pkGuid = pEntity.PageID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@PageKey",SqlDbType.NVarChar),
					new SqlParameter("@Title",SqlDbType.NVarChar),
					new SqlParameter("@ModuleName",SqlDbType.NVarChar),
					new SqlParameter("@IsEntrance",SqlDbType.Int),
					new SqlParameter("@URLTemplate",SqlDbType.NVarChar),
					new SqlParameter("@JsonValue",SqlDbType.NVarChar),
					new SqlParameter("@PageCode",SqlDbType.NVarChar),
					new SqlParameter("@IsAuth",SqlDbType.Int),
					new SqlParameter("@IsRebuild",SqlDbType.Int),
					new SqlParameter("@Version",SqlDbType.NVarChar),
					new SqlParameter("@Author",SqlDbType.NVarChar),
					new SqlParameter("@DefaultHtml",SqlDbType.NVarChar),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerVisible",SqlDbType.Int),
					new SqlParameter("@PageID",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.PageKey;
            parameters[1].Value = pEntity.Title;
            parameters[2].Value = pEntity.ModuleName;
            parameters[3].Value = pEntity.IsEntrance;
            parameters[4].Value = pEntity.URLTemplate;
            parameters[5].Value = pEntity.JsonValue;
            parameters[6].Value = pEntity.PageCode;
            parameters[7].Value = pEntity.IsAuth;
            parameters[8].Value = pEntity.IsRebuild;
            parameters[9].Value = pEntity.Version;
            parameters[10].Value = pEntity.Author;
			parameters[11].Value = pEntity.DefaultHtml;
			parameters[12].Value = pEntity.Remark;
			parameters[13].Value = pEntity.CustomerID;
			parameters[14].Value = pEntity.CreateBy;
			parameters[15].Value = pEntity.CreateTime;
			parameters[16].Value = pEntity.LastUpdateBy;
			parameters[17].Value = pEntity.LastUpdateTime;
			parameters[18].Value = pEntity.IsDelete;
			parameters[19].Value = pEntity.CustomerVisible;
			parameters[20].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            pEntity.PageID = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public SysPageEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [SysPage] where PageID='{0}'  and isdelete=0 ", id.ToString());
            //��ȡ����
            SysPageEntity m = null;
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
        public SysPageEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [SysPage] where 1=1  and isdelete=0");
            //��ȡ����
            List<SysPageEntity> list = new List<SysPageEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    SysPageEntity m;
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
        public void Update(SysPageEntity pEntity, IDbTransaction pTran)
        {
            Update(pEntity, pTran, true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(SysPageEntity pEntity, IDbTransaction pTran, bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.PageID.HasValue)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [SysPage] set ");
            if (pIsUpdateNullField || pEntity.PageKey != null)
                strSql.Append("[PageKey]=@PageKey,");
            if (pIsUpdateNullField || pEntity.Title != null)
                strSql.Append("[Title]=@Title,");
            if (pIsUpdateNullField || pEntity.ModuleName != null)
                strSql.Append("[ModuleName]=@ModuleName,");
            if (pIsUpdateNullField || pEntity.IsEntrance != null)
                strSql.Append("[IsEntrance]=@IsEntrance,");
            if (pIsUpdateNullField || pEntity.URLTemplate != null)
                strSql.Append("[URLTemplate]=@URLTemplate,");
            if (pIsUpdateNullField || pEntity.JsonValue != null)
                strSql.Append("[JsonValue]=@JsonValue,");
            if (pIsUpdateNullField || pEntity.PageCode != null)
                strSql.Append("[PageCode]=@PageCode,");
            if (pIsUpdateNullField || pEntity.IsAuth != null)
                strSql.Append("[IsAuth]=@IsAuth,");
            if (pIsUpdateNullField || pEntity.IsRebuild != null)
                strSql.Append("[IsRebuild]=@IsRebuild,");
            if (pIsUpdateNullField || pEntity.Version != null)
                strSql.Append("[Version]=@Version,");
            if (pIsUpdateNullField || pEntity.Author != null)
                strSql.Append("[Author]=@Author,");
            if (pIsUpdateNullField || pEntity.DefaultHtml!=null)
                strSql.Append( "[DefaultHtml]=@DefaultHtml,");
            if (pIsUpdateNullField || pEntity.Remark != null)
                strSql.Append("[Remark]=@Remark,");
            if (pIsUpdateNullField || pEntity.CustomerID != null)
                strSql.Append("[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy != null)
                strSql.Append("[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime != null)
                strSql.Append("[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.CustomerVisible!=null)
                strSql.Append( "[CustomerVisible]=@CustomerVisible");
            strSql.Append(" where PageID=@PageID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@PageKey",SqlDbType.NVarChar),
					new SqlParameter("@Title",SqlDbType.NVarChar),
					new SqlParameter("@ModuleName",SqlDbType.NVarChar),
					new SqlParameter("@IsEntrance",SqlDbType.Int),
					new SqlParameter("@URLTemplate",SqlDbType.NVarChar),
					new SqlParameter("@JsonValue",SqlDbType.NVarChar),
					new SqlParameter("@PageCode",SqlDbType.NVarChar),
					new SqlParameter("@IsAuth",SqlDbType.Int),
					new SqlParameter("@IsRebuild",SqlDbType.Int),
					new SqlParameter("@Version",SqlDbType.NVarChar),
					new SqlParameter("@Author",SqlDbType.NVarChar),
					new SqlParameter("@DefaultHtml",SqlDbType.NVarChar),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerVisible",SqlDbType.Int),
					new SqlParameter("@PageID",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.PageKey;
            parameters[1].Value = pEntity.Title;
            parameters[2].Value = pEntity.ModuleName;
            parameters[3].Value = pEntity.IsEntrance;
            parameters[4].Value = pEntity.URLTemplate;
            parameters[5].Value = pEntity.JsonValue;
            parameters[6].Value = pEntity.PageCode;
            parameters[7].Value = pEntity.IsAuth;
            parameters[8].Value = pEntity.IsRebuild;
            parameters[9].Value = pEntity.Version;
            parameters[10].Value = pEntity.Author;
			parameters[11].Value = pEntity.DefaultHtml;
			parameters[12].Value = pEntity.Remark;
			parameters[13].Value = pEntity.CustomerID;
			parameters[14].Value = pEntity.LastUpdateBy;
			parameters[15].Value = pEntity.LastUpdateTime;
			parameters[16].Value = pEntity.CustomerVisible;
			parameters[17].Value = pEntity.PageID;

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
        public void Update(SysPageEntity pEntity)
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(SysPageEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(SysPageEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.PageID.HasValue)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.PageID.Value, pTran);
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
            sql.AppendLine("update [SysPage] set  isdelete=1 where PageID=@PageID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@PageID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(SysPageEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.PageID.HasValue)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.PageID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(SysPageEntity[] pEntities)
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
            sql.AppendLine("update [SysPage] set  isdelete=1 where PageID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public SysPageEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [SysPage] where 1=1  and isdelete=0 ");
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
            List<SysPageEntity> list = new List<SysPageEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    SysPageEntity m;
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
        public PagedQueryResult<SysPageEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [PageID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [SysPage] where 1=1  and isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [SysPage] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<SysPageEntity> result = new PagedQueryResult<SysPageEntity>();
            List<SysPageEntity> list = new List<SysPageEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    SysPageEntity m;
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
        public SysPageEntity[] QueryByEntity(SysPageEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<SysPageEntity> PagedQueryByEntity(SysPageEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(SysPageEntity pQueryEntity)
        {
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.PageID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PageID", Value = pQueryEntity.PageID });
            if (pQueryEntity.PageKey != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PageKey", Value = pQueryEntity.PageKey });
            if (pQueryEntity.Title != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Title", Value = pQueryEntity.Title });
            if (pQueryEntity.ModuleName != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ModuleName", Value = pQueryEntity.ModuleName });
            if (pQueryEntity.IsEntrance != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsEntrance", Value = pQueryEntity.IsEntrance });
            if (pQueryEntity.URLTemplate != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "URLTemplate", Value = pQueryEntity.URLTemplate });
            if (pQueryEntity.JsonValue != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "JsonValue", Value = pQueryEntity.JsonValue });
            if (pQueryEntity.PageCode != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PageCode", Value = pQueryEntity.PageCode });
            if (pQueryEntity.IsAuth != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsAuth", Value = pQueryEntity.IsAuth });
            if (pQueryEntity.IsRebuild != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsRebuild", Value = pQueryEntity.IsRebuild });
            if (pQueryEntity.Version != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Version", Value = pQueryEntity.Version });
            if (pQueryEntity.Author != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Author", Value = pQueryEntity.Author });
            if (pQueryEntity.DefaultHtml!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DefaultHtml", Value = pQueryEntity.DefaultHtml });
            if (pQueryEntity.Remark != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Remark", Value = pQueryEntity.Remark });
            if (pQueryEntity.CustomerID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });
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
            if (pQueryEntity.CustomerVisible!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerVisible", Value = pQueryEntity.CustomerVisible });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(IDataReader pReader, out SysPageEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new SysPageEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["PageID"] != DBNull.Value)
            {
                pInstance.PageID = (Guid)pReader["PageID"];
            }
            if (pReader["PageKey"] != DBNull.Value)
            {
                pInstance.PageKey = Convert.ToString(pReader["PageKey"]);
            }
            if (pReader["Title"] != DBNull.Value)
            {
                pInstance.Title = Convert.ToString(pReader["Title"]);
            }
            if (pReader["ModuleName"] != DBNull.Value)
            {
                pInstance.ModuleName = Convert.ToString(pReader["ModuleName"]);
            }
            if (pReader["IsEntrance"] != DBNull.Value)
            {
                pInstance.IsEntrance = Convert.ToInt32(pReader["IsEntrance"]);
            }
            if (pReader["URLTemplate"] != DBNull.Value)
            {
                pInstance.URLTemplate = Convert.ToString(pReader["URLTemplate"]);
            }
            if (pReader["JsonValue"] != DBNull.Value)
            {
                pInstance.JsonValue = Convert.ToString(pReader["JsonValue"]);
            }
            if (pReader["PageCode"] != DBNull.Value)
            {
                pInstance.PageCode = Convert.ToString(pReader["PageCode"]);
            }
            if (pReader["IsAuth"] != DBNull.Value)
            {
                pInstance.IsAuth = Convert.ToInt32(pReader["IsAuth"]);
            }
            if (pReader["IsRebuild"] != DBNull.Value)
            {
                pInstance.IsRebuild = Convert.ToInt32(pReader["IsRebuild"]);
            }
            if (pReader["Version"] != DBNull.Value)
            {
                pInstance.Version = Convert.ToString(pReader["Version"]);
            }
            if (pReader["Author"] != DBNull.Value)
            {
                pInstance.Author = Convert.ToString(pReader["Author"]);
            }
			if (pReader["DefaultHtml"] != DBNull.Value)
			{
				pInstance.DefaultHtml =  Convert.ToString(pReader["DefaultHtml"]);
			}
            if (pReader["Remark"] != DBNull.Value)
            {
                pInstance.Remark = Convert.ToString(pReader["Remark"]);
            }
            if (pReader["CustomerID"] != DBNull.Value)
            {
                pInstance.CustomerID = Convert.ToString(pReader["CustomerID"]);
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
			if (pReader["CustomerVisible"] != DBNull.Value)
			{
				pInstance.CustomerVisible =   Convert.ToInt32(pReader["CustomerVisible"]);
			}

        }
        #endregion
    }
}
