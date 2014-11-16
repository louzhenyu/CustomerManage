/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/8/22 15:24:19
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
    /// ��WMaterialText�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class WMaterialTextDAO : Base.BaseCPOSDAO, ICRUDable<WMaterialTextEntity>, IQueryable<WMaterialTextEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public WMaterialTextDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(WMaterialTextEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(WMaterialTextEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [WMaterialText](");
            strSql.Append("[ParentTextId],[Title],[Author],[CoverImageUrl],[Text],[OriginalUrl],[DisplayIndex],[ApplicationId],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[TypeId],[PageId],[PageUrlJson],[PageParamJson],[IsAuth],[TextId])");
            strSql.Append(" values (");
            strSql.Append("@ParentTextId,@Title,@Author,@CoverImageUrl,@Text,@OriginalUrl,@DisplayIndex,@ApplicationId,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@TypeId,@PageId,@PageUrlJson,@PageParamJson,@IsAuth,@TextId)");

            string pkString = pEntity.TextId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@ParentTextId",SqlDbType.NVarChar),
					new SqlParameter("@Title",SqlDbType.NVarChar),
					new SqlParameter("@Author",SqlDbType.NVarChar),
					new SqlParameter("@CoverImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@Text",SqlDbType.NVarChar),
					new SqlParameter("@OriginalUrl",SqlDbType.NVarChar),
					new SqlParameter("@DisplayIndex",SqlDbType.Int),
					new SqlParameter("@ApplicationId",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@TypeId",SqlDbType.NVarChar),
					new SqlParameter("@PageId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@PageUrlJson",SqlDbType.NVarChar),
					new SqlParameter("@PageParamJson",SqlDbType.NVarChar),
					new SqlParameter("@IsAuth",SqlDbType.Int),
					new SqlParameter("@TextId",SqlDbType.NVarChar)
            };
            parameters[0].Value = pEntity.ParentTextId;
            parameters[1].Value = pEntity.Title;
            parameters[2].Value = pEntity.Author;
            parameters[3].Value = pEntity.CoverImageUrl;
            parameters[4].Value = pEntity.Text;
            parameters[5].Value = pEntity.OriginalUrl;
            parameters[6].Value = pEntity.DisplayIndex;
            parameters[7].Value = pEntity.ApplicationId;
            parameters[8].Value = pEntity.CreateTime;
            parameters[9].Value = pEntity.CreateBy;
            parameters[10].Value = pEntity.LastUpdateBy;
            parameters[11].Value = pEntity.LastUpdateTime;
            parameters[12].Value = pEntity.IsDelete;
            parameters[13].Value = pEntity.TypeId;
            parameters[14].Value = pEntity.PageId;
            parameters[15].Value = pEntity.PageUrlJson;
            parameters[16].Value = pEntity.PageParamJson;
            parameters[17].Value = pEntity.IsAuth;
            parameters[18].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            pEntity.TextId = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public WMaterialTextEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WMaterialText] where TextId='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            WMaterialTextEntity m = null;
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
        public WMaterialTextEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WMaterialText] where isdelete=0");
            //��ȡ����
            List<WMaterialTextEntity> list = new List<WMaterialTextEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WMaterialTextEntity m;
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
        public void Update(WMaterialTextEntity pEntity, IDbTransaction pTran)
        {
            Update(pEntity, true, pTran);
        }
        public void Update(WMaterialTextEntity pEntity, bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.TextId == null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [WMaterialText] set ");
            if (pIsUpdateNullField || pEntity.ParentTextId != null)
                strSql.Append("[ParentTextId]=@ParentTextId,");
            if (pIsUpdateNullField || pEntity.Title != null)
                strSql.Append("[Title]=@Title,");
            if (pIsUpdateNullField || pEntity.Author != null)
                strSql.Append("[Author]=@Author,");
            if (pIsUpdateNullField || pEntity.CoverImageUrl != null)
                strSql.Append("[CoverImageUrl]=@CoverImageUrl,");
            if (pIsUpdateNullField || pEntity.Text != null)
                strSql.Append("[Text]=@Text,");
            if (pIsUpdateNullField || pEntity.OriginalUrl != null)
                strSql.Append("[OriginalUrl]=@OriginalUrl,");
            if (pIsUpdateNullField || pEntity.DisplayIndex != null)
                strSql.Append("[DisplayIndex]=@DisplayIndex,");
            if (pIsUpdateNullField || pEntity.ApplicationId != null)
                strSql.Append("[ApplicationId]=@ApplicationId,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy != null)
                strSql.Append("[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime != null)
                strSql.Append("[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.TypeId != null)
                strSql.Append("[TypeId]=@TypeId,");
            if (pIsUpdateNullField || pEntity.PageId != null)
                strSql.Append("[PageId]=@PageId,");
            if (pIsUpdateNullField || pEntity.PageUrlJson != null)
                strSql.Append("[PageUrlJson]=@PageUrlJson,");
            if (pIsUpdateNullField || pEntity.PageParamJson != null)
                strSql.Append("[PageParamJson]=@PageParamJson,");
            if (pIsUpdateNullField || pEntity.IsAuth != null)
                strSql.Append("[IsAuth]=@IsAuth");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where TextId=@TextId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@ParentTextId",SqlDbType.NVarChar),
					new SqlParameter("@Title",SqlDbType.NVarChar),
					new SqlParameter("@Author",SqlDbType.NVarChar),
					new SqlParameter("@CoverImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@Text",SqlDbType.NVarChar),
					new SqlParameter("@OriginalUrl",SqlDbType.NVarChar),
					new SqlParameter("@DisplayIndex",SqlDbType.Int),
					new SqlParameter("@ApplicationId",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@TypeId",SqlDbType.NVarChar),
					new SqlParameter("@PageId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@PageUrlJson",SqlDbType.NVarChar),
					new SqlParameter("@PageParamJson",SqlDbType.NVarChar),
					new SqlParameter("@IsAuth",SqlDbType.Int),
					new SqlParameter("@TextId",SqlDbType.NVarChar)
            };
            parameters[0].Value = pEntity.ParentTextId;
            parameters[1].Value = pEntity.Title;
            parameters[2].Value = pEntity.Author;
            parameters[3].Value = pEntity.CoverImageUrl;
            parameters[4].Value = pEntity.Text;
            parameters[5].Value = pEntity.OriginalUrl;
            parameters[6].Value = pEntity.DisplayIndex;
            parameters[7].Value = pEntity.ApplicationId;
            parameters[8].Value = pEntity.LastUpdateBy;
            parameters[9].Value = pEntity.LastUpdateTime;
            parameters[10].Value = pEntity.TypeId;
            parameters[11].Value = pEntity.PageId;
            parameters[12].Value = pEntity.PageUrlJson;
            parameters[13].Value = pEntity.PageParamJson;
            parameters[14].Value = pEntity.IsAuth;
            parameters[15].Value = pEntity.TextId;

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
        public void Update(WMaterialTextEntity pEntity)
        {
            Update(pEntity, true);
        }
        public void Update(WMaterialTextEntity pEntity, bool pIsUpdateNullField)
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(WMaterialTextEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(WMaterialTextEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.TextId == null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.TextId, pTran);
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
            sql.AppendLine("update [WMaterialText] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where TextId=@TextId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@TextId",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(WMaterialTextEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.TextId == null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.TextId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(WMaterialTextEntity[] pEntities)
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
            sql.AppendLine("update [WMaterialText] set LastUpdateTime='" + DateTime.Now.ToString() + "',LastUpdateBy='" + CurrentUserInfo.UserID + "',IsDelete=1 where TextId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public WMaterialTextEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WMaterialText] where isdelete=0 ");
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
            List<WMaterialTextEntity> list = new List<WMaterialTextEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WMaterialTextEntity m;
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
        public PagedQueryResult<WMaterialTextEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [TextId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [WMaterialText] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [WMaterialText] where isdelete=0 ");
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
            PagedQueryResult<WMaterialTextEntity> result = new PagedQueryResult<WMaterialTextEntity>();
            List<WMaterialTextEntity> list = new List<WMaterialTextEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    WMaterialTextEntity m;
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
        public WMaterialTextEntity[] QueryByEntity(WMaterialTextEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<WMaterialTextEntity> PagedQueryByEntity(WMaterialTextEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(WMaterialTextEntity pQueryEntity)
        {
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.TextId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TextId", Value = pQueryEntity.TextId });
            if (pQueryEntity.ParentTextId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ParentTextId", Value = pQueryEntity.ParentTextId });
            if (pQueryEntity.Title != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Title", Value = pQueryEntity.Title });
            if (pQueryEntity.Author != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Author", Value = pQueryEntity.Author });
            if (pQueryEntity.CoverImageUrl != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CoverImageUrl", Value = pQueryEntity.CoverImageUrl });
            if (pQueryEntity.Text != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Text", Value = pQueryEntity.Text });
            if (pQueryEntity.OriginalUrl != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OriginalUrl", Value = pQueryEntity.OriginalUrl });
            if (pQueryEntity.DisplayIndex != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DisplayIndex", Value = pQueryEntity.DisplayIndex });
            if (pQueryEntity.ApplicationId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ApplicationId", Value = pQueryEntity.ApplicationId });
            if (pQueryEntity.CreateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.LastUpdateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.LastUpdateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.IsDelete != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });
            if (pQueryEntity.TypeId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TypeId", Value = pQueryEntity.TypeId });
            if (pQueryEntity.PageId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PageId", Value = pQueryEntity.PageId });
            if (pQueryEntity.PageUrlJson != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PageUrlJson", Value = pQueryEntity.PageUrlJson });
            if (pQueryEntity.PageParamJson != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PageParamJson", Value = pQueryEntity.PageParamJson });
            if (pQueryEntity.IsAuth != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsAuth", Value = pQueryEntity.IsAuth });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out WMaterialTextEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new WMaterialTextEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["TextId"] != DBNull.Value)
            {
                pInstance.TextId = Convert.ToString(pReader["TextId"]);
            }
            if (pReader["ParentTextId"] != DBNull.Value)
            {
                pInstance.ParentTextId = Convert.ToString(pReader["ParentTextId"]);
            }
            if (pReader["Title"] != DBNull.Value)
            {
                pInstance.Title = Convert.ToString(pReader["Title"]);
            }
            if (pReader["Author"] != DBNull.Value)
            {
                pInstance.Author = Convert.ToString(pReader["Author"]);
            }
            if (pReader["CoverImageUrl"] != DBNull.Value)
            {
                pInstance.CoverImageUrl = Convert.ToString(pReader["CoverImageUrl"]);
            }
            if (pReader["Text"] != DBNull.Value)
            {
                pInstance.Text = Convert.ToString(pReader["Text"]);
            }
            if (pReader["OriginalUrl"] != DBNull.Value)
            {
                pInstance.OriginalUrl = Convert.ToString(pReader["OriginalUrl"]);
            }
            if (pReader["DisplayIndex"] != DBNull.Value)
            {
                pInstance.DisplayIndex = Convert.ToInt32(pReader["DisplayIndex"]);
            }
            if (pReader["ApplicationId"] != DBNull.Value)
            {
                pInstance.ApplicationId = Convert.ToString(pReader["ApplicationId"]);
            }
            if (pReader["CreateTime"] != DBNull.Value)
            {
                pInstance.CreateTime = Convert.ToDateTime(pReader["CreateTime"]);
            }
            if (pReader["CreateBy"] != DBNull.Value)
            {
                pInstance.CreateBy = Convert.ToString(pReader["CreateBy"]);
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
            if (pReader["TypeId"] != DBNull.Value)
            {
                pInstance.TypeId = Convert.ToString(pReader["TypeId"]);
            }
            if (pReader["PageId"] != DBNull.Value)
            {
                pInstance.PageId = (Guid)pReader["PageId"];
            }
            if (pReader["PageUrlJson"] != DBNull.Value)
            {
                pInstance.PageUrlJson = Convert.ToString(pReader["PageUrlJson"]);
            }
            if (pReader["PageParamJson"] != DBNull.Value)
            {
                pInstance.PageParamJson = Convert.ToString(pReader["PageParamJson"]);
            }
            if (pReader["IsAuth"] != DBNull.Value)
            {
                pInstance.IsAuth = Convert.ToInt32(pReader["IsAuth"]);
            }

        }
        #endregion
    }
}
