/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/26 14:59:01
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
    /// ��IMGroup�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class IMGroupDAO : Base.BaseCPOSDAO, ICRUDable<IMGroupEntity>, IQueryable<IMGroupEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public IMGroupDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(IMGroupEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(IMGroupEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [IMGroup](");
            strSql.Append("[GroupName],[LogoUrl],[Description],[CustomerID],[Telephone],[UserCount],[GroupLevel],[IsPublic],[ApproveNeededLevel],[InvitationLevel],[ChatLevel],[QuitLevel],[BindGroupID],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[ChatGroupID])");
            strSql.Append(" values (");
            strSql.Append("@GroupName,@LogoUrl,@Description,@CustomerID,@Telephone,@UserCount,@GroupLevel,@IsPublic,@ApproveNeededLevel,@InvitationLevel,@ChatLevel,@QuitLevel,@BindGroupID,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@ChatGroupID)");

            Guid? pkGuid;
            if (pEntity.ChatGroupID == null)
                pkGuid = Guid.NewGuid();
            else
                pkGuid = pEntity.ChatGroupID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@GroupName",SqlDbType.NVarChar),
					new SqlParameter("@LogoUrl",SqlDbType.VarChar),
					new SqlParameter("@Description",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@Telephone",SqlDbType.Char),
					new SqlParameter("@UserCount",SqlDbType.Int),
					new SqlParameter("@GroupLevel",SqlDbType.Int),
					new SqlParameter("@IsPublic",SqlDbType.Int),
					new SqlParameter("@ApproveNeededLevel",SqlDbType.Int),
					new SqlParameter("@InvitationLevel",SqlDbType.Int),
					new SqlParameter("@ChatLevel",SqlDbType.Int),
					new SqlParameter("@QuitLevel",SqlDbType.Int),
					new SqlParameter("@BindGroupID",SqlDbType.VarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@ChatGroupID",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.GroupName;
            parameters[1].Value = pEntity.LogoUrl;
            parameters[2].Value = pEntity.Description;
            parameters[3].Value = pEntity.CustomerID;
            parameters[4].Value = pEntity.Telephone;
            parameters[5].Value = pEntity.UserCount;
            parameters[6].Value = pEntity.GroupLevel;
            parameters[7].Value = pEntity.IsPublic;
            parameters[8].Value = pEntity.ApproveNeededLevel;
            parameters[9].Value = pEntity.InvitationLevel;
            parameters[10].Value = pEntity.ChatLevel;
            parameters[11].Value = pEntity.QuitLevel;
            parameters[12].Value = pEntity.BindGroupID;
            parameters[13].Value = pEntity.CreateTime;
            parameters[14].Value = pEntity.CreateBy;
            parameters[15].Value = pEntity.LastUpdateTime;
            parameters[16].Value = pEntity.LastUpdateBy;
            parameters[17].Value = pEntity.IsDelete;
            parameters[18].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            pEntity.ChatGroupID = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public IMGroupEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [IMGroup] where ChatGroupID='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            IMGroupEntity m = null;
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
        public IMGroupEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [IMGroup] where isdelete=0");
            //��ȡ����
            List<IMGroupEntity> list = new List<IMGroupEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    IMGroupEntity m;
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
        public void Update(IMGroupEntity pEntity, IDbTransaction pTran)
        {
            Update(pEntity, true, pTran);
        }
        public void Update(IMGroupEntity pEntity, bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ChatGroupID == null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [IMGroup] set ");
            if (pIsUpdateNullField || pEntity.GroupName != null)
                strSql.Append("[GroupName]=@GroupName,");
            if (pIsUpdateNullField || pEntity.LogoUrl != null)
                strSql.Append("[LogoUrl]=@LogoUrl,");
            if (pIsUpdateNullField || pEntity.Description != null)
                strSql.Append("[Description]=@Description,");
            if (pIsUpdateNullField || pEntity.CustomerID != null)
                strSql.Append("[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.Telephone != null)
                strSql.Append("[Telephone]=@Telephone,");
            if (pIsUpdateNullField || pEntity.UserCount != null)
                strSql.Append("[UserCount]=@UserCount,");
            if (pIsUpdateNullField || pEntity.GroupLevel != null)
                strSql.Append("[GroupLevel]=@GroupLevel,");
            if (pIsUpdateNullField || pEntity.IsPublic != null)
                strSql.Append("[IsPublic]=@IsPublic,");
            if (pIsUpdateNullField || pEntity.ApproveNeededLevel != null)
                strSql.Append("[ApproveNeededLevel]=@ApproveNeededLevel,");
            if (pIsUpdateNullField || pEntity.InvitationLevel != null)
                strSql.Append("[InvitationLevel]=@InvitationLevel,");
            if (pIsUpdateNullField || pEntity.ChatLevel != null)
                strSql.Append("[ChatLevel]=@ChatLevel,");
            if (pIsUpdateNullField || pEntity.QuitLevel != null)
                strSql.Append("[QuitLevel]=@QuitLevel,");
            if (pIsUpdateNullField || pEntity.BindGroupID != null)
                strSql.Append("[BindGroupID]=@BindGroupID,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime != null)
                strSql.Append("[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy != null)
                strSql.Append("[LastUpdateBy]=@LastUpdateBy");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where ChatGroupID=@ChatGroupID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@GroupName",SqlDbType.NVarChar),
					new SqlParameter("@LogoUrl",SqlDbType.VarChar),
					new SqlParameter("@Description",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@Telephone",SqlDbType.Char),
					new SqlParameter("@UserCount",SqlDbType.Int),
					new SqlParameter("@GroupLevel",SqlDbType.Int),
					new SqlParameter("@IsPublic",SqlDbType.Int),
					new SqlParameter("@ApproveNeededLevel",SqlDbType.Int),
					new SqlParameter("@InvitationLevel",SqlDbType.Int),
					new SqlParameter("@ChatLevel",SqlDbType.Int),
					new SqlParameter("@QuitLevel",SqlDbType.Int),
					new SqlParameter("@BindGroupID",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@ChatGroupID",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.GroupName;
            parameters[1].Value = pEntity.LogoUrl;
            parameters[2].Value = pEntity.Description;
            parameters[3].Value = pEntity.CustomerID;
            parameters[4].Value = pEntity.Telephone;
            parameters[5].Value = pEntity.UserCount;
            parameters[6].Value = pEntity.GroupLevel;
            parameters[7].Value = pEntity.IsPublic;
            parameters[8].Value = pEntity.ApproveNeededLevel;
            parameters[9].Value = pEntity.InvitationLevel;
            parameters[10].Value = pEntity.ChatLevel;
            parameters[11].Value = pEntity.QuitLevel;
            parameters[12].Value = pEntity.BindGroupID;
            parameters[13].Value = pEntity.LastUpdateTime;
            parameters[14].Value = pEntity.LastUpdateBy;
            parameters[15].Value = pEntity.ChatGroupID;

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
        public void Update(IMGroupEntity pEntity)
        {
            Update(pEntity, true);
        }
        public void Update(IMGroupEntity pEntity, bool pIsUpdateNullField)
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(IMGroupEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(IMGroupEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ChatGroupID == null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.ChatGroupID, pTran);
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
            sql.AppendLine("update [IMGroup] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where ChatGroupID=@ChatGroupID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@ChatGroupID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(IMGroupEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.ChatGroupID == null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.ChatGroupID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(IMGroupEntity[] pEntities)
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
            sql.AppendLine("update [IMGroup] set LastUpdateTime='" + DateTime.Now.ToString() + "',LastUpdateBy='" + CurrentUserInfo.UserID + "',IsDelete=1 where ChatGroupID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public IMGroupEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [IMGroup] where isdelete=0 ");
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
            List<IMGroupEntity> list = new List<IMGroupEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    IMGroupEntity m;
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
        public PagedQueryResult<IMGroupEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [ChatGroupID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [IMGroup] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [IMGroup] where isdelete=0 ");
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
            PagedQueryResult<IMGroupEntity> result = new PagedQueryResult<IMGroupEntity>();
            List<IMGroupEntity> list = new List<IMGroupEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    IMGroupEntity m;
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
        public IMGroupEntity[] QueryByEntity(IMGroupEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<IMGroupEntity> PagedQueryByEntity(IMGroupEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(IMGroupEntity pQueryEntity)
        {
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.ChatGroupID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ChatGroupID", Value = pQueryEntity.ChatGroupID });
            if (pQueryEntity.GroupName != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "GroupName", Value = pQueryEntity.GroupName });
            if (pQueryEntity.LogoUrl != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LogoUrl", Value = pQueryEntity.LogoUrl });
            if (pQueryEntity.Description != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Description", Value = pQueryEntity.Description });
            if (pQueryEntity.CustomerID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });
            if (pQueryEntity.Telephone != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Telephone", Value = pQueryEntity.Telephone });
            if (pQueryEntity.UserCount != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UserCount", Value = pQueryEntity.UserCount });
            if (pQueryEntity.GroupLevel != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "GroupLevel", Value = pQueryEntity.GroupLevel });
            if (pQueryEntity.IsPublic != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsPublic", Value = pQueryEntity.IsPublic });
            if (pQueryEntity.ApproveNeededLevel != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ApproveNeededLevel", Value = pQueryEntity.ApproveNeededLevel });
            if (pQueryEntity.InvitationLevel != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "InvitationLevel", Value = pQueryEntity.InvitationLevel });
            if (pQueryEntity.ChatLevel != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ChatLevel", Value = pQueryEntity.ChatLevel });
            if (pQueryEntity.QuitLevel != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "QuitLevel", Value = pQueryEntity.QuitLevel });
            if (pQueryEntity.BindGroupID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BindGroupID", Value = pQueryEntity.BindGroupID });
            if (pQueryEntity.CreateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.LastUpdateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.LastUpdateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.IsDelete != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out IMGroupEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new IMGroupEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["ChatGroupID"] != DBNull.Value)
            {
                pInstance.ChatGroupID = (Guid)pReader["ChatGroupID"];
            }
            if (pReader["GroupName"] != DBNull.Value)
            {
                pInstance.GroupName = Convert.ToString(pReader["GroupName"]);
            }
            if (pReader["LogoUrl"] != DBNull.Value)
            {
                pInstance.LogoUrl = Convert.ToString(pReader["LogoUrl"]);
            }
            if (pReader["Description"] != DBNull.Value)
            {
                pInstance.Description = Convert.ToString(pReader["Description"]);
            }
            if (pReader["CustomerID"] != DBNull.Value)
            {
                pInstance.CustomerID = Convert.ToString(pReader["CustomerID"]);
            }
            if (pReader["Telephone"] != DBNull.Value)
            {
                pInstance.Telephone = Convert.ToString(pReader["Telephone"]);
            }
            if (pReader["UserCount"] != DBNull.Value)
            {
                pInstance.UserCount = Convert.ToInt32(pReader["UserCount"]);
            }
            if (pReader["GroupLevel"] != DBNull.Value)
            {
                pInstance.GroupLevel = Convert.ToInt32(pReader["GroupLevel"]);
            }
            if (pReader["IsPublic"] != DBNull.Value)
            {
                pInstance.IsPublic = Convert.ToInt32(pReader["IsPublic"]);
            }
            if (pReader["ApproveNeededLevel"] != DBNull.Value)
            {
                pInstance.ApproveNeededLevel = Convert.ToInt32(pReader["ApproveNeededLevel"]);
            }
            if (pReader["InvitationLevel"] != DBNull.Value)
            {
                pInstance.InvitationLevel = Convert.ToInt32(pReader["InvitationLevel"]);
            }
            if (pReader["ChatLevel"] != DBNull.Value)
            {
                pInstance.ChatLevel = Convert.ToInt32(pReader["ChatLevel"]);
            }
            if (pReader["QuitLevel"] != DBNull.Value)
            {
                pInstance.QuitLevel = Convert.ToInt32(pReader["QuitLevel"]);
            }
            if (pReader["BindGroupID"] != DBNull.Value)
            {
                pInstance.BindGroupID = pReader["BindGroupID"].ToString();
            }
            if (pReader["CreateTime"] != DBNull.Value)
            {
                pInstance.CreateTime = Convert.ToDateTime(pReader["CreateTime"]);
            }
            if (pReader["CreateBy"] != DBNull.Value)
            {
                pInstance.CreateBy = Convert.ToString(pReader["CreateBy"]);
            }
            if (pReader["LastUpdateTime"] != DBNull.Value)
            {
                pInstance.LastUpdateTime = Convert.ToDateTime(pReader["LastUpdateTime"]);
            }
            if (pReader["LastUpdateBy"] != DBNull.Value)
            {
                pInstance.LastUpdateBy = Convert.ToString(pReader["LastUpdateBy"]);
            }
            if (pReader["IsDelete"] != DBNull.Value)
            {
                pInstance.IsDelete = Convert.ToInt32(pReader["IsDelete"]);
            }

        }
        #endregion
    }
}
