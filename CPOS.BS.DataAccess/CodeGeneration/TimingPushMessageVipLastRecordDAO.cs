/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/2/27 14:13:26
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
    /// ���ݷ��ʣ� ����ͼ�¼ 
    /// ��TimingPushMessageVipLastRecord�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class TimingPushMessageVipLastRecordDAO : Base.BaseCPOSDAO, ICRUDable<TimingPushMessageVipLastRecordEntity>, IQueryable<TimingPushMessageVipLastRecordEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public TimingPushMessageVipLastRecordDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(TimingPushMessageVipLastRecordEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(TimingPushMessageVipLastRecordEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [TimingPushMessageVipLastRecord](");
            strSql.Append("[TimingPushMessageRuleID],[CSPipelineID],[VipID],[LastModelID],[LastContentID],[LastContentIndex],[ClientID],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[TimingPushMessageVipLastRecordID])");
            strSql.Append(" values (");
            strSql.Append("@TimingPushMessageRuleID,@CSPipelineID,@VipID,@LastModelID,@LastContentID,@LastContentIndex,@ClientID,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@TimingPushMessageVipLastRecordID)");            

			Guid? pkGuid;
			if (pEntity.TimingPushMessageVipLastRecordID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.TimingPushMessageVipLastRecordID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@TimingPushMessageRuleID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@CSPipelineID",SqlDbType.Int),
					new SqlParameter("@VipID",SqlDbType.VarChar),
					new SqlParameter("@LastModelID",SqlDbType.VarChar),
					new SqlParameter("@LastContentID",SqlDbType.VarChar),
					new SqlParameter("@LastContentIndex",SqlDbType.Int),
					new SqlParameter("@ClientID",SqlDbType.NVarChar),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@TimingPushMessageVipLastRecordID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.TimingPushMessageRuleID;
			parameters[1].Value = pEntity.CSPipelineID;
			parameters[2].Value = pEntity.VipID;
			parameters[3].Value = pEntity.LastModelID;
			parameters[4].Value = pEntity.LastContentID;
			parameters[5].Value = pEntity.LastContentIndex;
			parameters[6].Value = pEntity.ClientID;
			parameters[7].Value = pEntity.CreateBy;
			parameters[8].Value = pEntity.CreateTime;
			parameters[9].Value = pEntity.LastUpdateBy;
			parameters[10].Value = pEntity.LastUpdateTime;
			parameters[11].Value = pEntity.IsDelete;
			parameters[12].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.TimingPushMessageVipLastRecordID = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public TimingPushMessageVipLastRecordEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [TimingPushMessageVipLastRecord] where TimingPushMessageVipLastRecordID='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            TimingPushMessageVipLastRecordEntity m = null;
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
        public TimingPushMessageVipLastRecordEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [TimingPushMessageVipLastRecord] where isdelete=0");
            //��ȡ����
            List<TimingPushMessageVipLastRecordEntity> list = new List<TimingPushMessageVipLastRecordEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    TimingPushMessageVipLastRecordEntity m;
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
        public void Update(TimingPushMessageVipLastRecordEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(TimingPushMessageVipLastRecordEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.TimingPushMessageVipLastRecordID==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [TimingPushMessageVipLastRecord] set ");
            if (pIsUpdateNullField || pEntity.TimingPushMessageRuleID!=null)
                strSql.Append( "[TimingPushMessageRuleID]=@TimingPushMessageRuleID,");
            if (pIsUpdateNullField || pEntity.CSPipelineID!=null)
                strSql.Append( "[CSPipelineID]=@CSPipelineID,");
            if (pIsUpdateNullField || pEntity.VipID!=null)
                strSql.Append( "[VipID]=@VipID,");
            if (pIsUpdateNullField || pEntity.LastModelID!=null)
                strSql.Append( "[LastModelID]=@LastModelID,");
            if (pIsUpdateNullField || pEntity.LastContentID!=null)
                strSql.Append( "[LastContentID]=@LastContentID,");
            if (pIsUpdateNullField || pEntity.LastContentIndex!=null)
                strSql.Append( "[LastContentIndex]=@LastContentIndex,");
            if (pIsUpdateNullField || pEntity.ClientID!=null)
                strSql.Append( "[ClientID]=@ClientID,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where TimingPushMessageVipLastRecordID=@TimingPushMessageVipLastRecordID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@TimingPushMessageRuleID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@CSPipelineID",SqlDbType.Int),
					new SqlParameter("@VipID",SqlDbType.VarChar),
					new SqlParameter("@LastModelID",SqlDbType.VarChar),
					new SqlParameter("@LastContentID",SqlDbType.VarChar),
					new SqlParameter("@LastContentIndex",SqlDbType.Int),
					new SqlParameter("@ClientID",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@TimingPushMessageVipLastRecordID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.TimingPushMessageRuleID;
			parameters[1].Value = pEntity.CSPipelineID;
			parameters[2].Value = pEntity.VipID;
			parameters[3].Value = pEntity.LastModelID;
			parameters[4].Value = pEntity.LastContentID;
			parameters[5].Value = pEntity.LastContentIndex;
			parameters[6].Value = pEntity.ClientID;
			parameters[7].Value = pEntity.LastUpdateBy;
			parameters[8].Value = pEntity.LastUpdateTime;
			parameters[9].Value = pEntity.TimingPushMessageVipLastRecordID;

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
        public void Update(TimingPushMessageVipLastRecordEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(TimingPushMessageVipLastRecordEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(TimingPushMessageVipLastRecordEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(TimingPushMessageVipLastRecordEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.TimingPushMessageVipLastRecordID==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.TimingPushMessageVipLastRecordID, pTran);           
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
            sql.AppendLine("update [TimingPushMessageVipLastRecord] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where TimingPushMessageVipLastRecordID=@TimingPushMessageVipLastRecordID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@TimingPushMessageVipLastRecordID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(TimingPushMessageVipLastRecordEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.TimingPushMessageVipLastRecordID==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.TimingPushMessageVipLastRecordID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(TimingPushMessageVipLastRecordEntity[] pEntities)
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
            sql.AppendLine("update [TimingPushMessageVipLastRecord] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where TimingPushMessageVipLastRecordID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public TimingPushMessageVipLastRecordEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [TimingPushMessageVipLastRecord] where isdelete=0 ");
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
            List<TimingPushMessageVipLastRecordEntity> list = new List<TimingPushMessageVipLastRecordEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    TimingPushMessageVipLastRecordEntity m;
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
        public PagedQueryResult<TimingPushMessageVipLastRecordEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [TimingPushMessageVipLastRecordID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [TimingPushMessageVipLastRecord] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [TimingPushMessageVipLastRecord] where isdelete=0 ");
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
            PagedQueryResult<TimingPushMessageVipLastRecordEntity> result = new PagedQueryResult<TimingPushMessageVipLastRecordEntity>();
            List<TimingPushMessageVipLastRecordEntity> list = new List<TimingPushMessageVipLastRecordEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    TimingPushMessageVipLastRecordEntity m;
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
        public TimingPushMessageVipLastRecordEntity[] QueryByEntity(TimingPushMessageVipLastRecordEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<TimingPushMessageVipLastRecordEntity> PagedQueryByEntity(TimingPushMessageVipLastRecordEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(TimingPushMessageVipLastRecordEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.TimingPushMessageVipLastRecordID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TimingPushMessageVipLastRecordID", Value = pQueryEntity.TimingPushMessageVipLastRecordID });
            if (pQueryEntity.TimingPushMessageRuleID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TimingPushMessageRuleID", Value = pQueryEntity.TimingPushMessageRuleID });
            if (pQueryEntity.CSPipelineID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CSPipelineID", Value = pQueryEntity.CSPipelineID });
            if (pQueryEntity.VipID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipID", Value = pQueryEntity.VipID });
            if (pQueryEntity.LastModelID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastModelID", Value = pQueryEntity.LastModelID });
            if (pQueryEntity.LastContentID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastContentID", Value = pQueryEntity.LastContentID });
            if (pQueryEntity.LastContentIndex!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastContentIndex", Value = pQueryEntity.LastContentIndex });
            if (pQueryEntity.ClientID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ClientID", Value = pQueryEntity.ClientID });
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
        protected void Load(SqlDataReader pReader, out TimingPushMessageVipLastRecordEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new TimingPushMessageVipLastRecordEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["TimingPushMessageVipLastRecordID"] != DBNull.Value)
			{
				pInstance.TimingPushMessageVipLastRecordID =  (Guid)pReader["TimingPushMessageVipLastRecordID"];
			}
			if (pReader["TimingPushMessageRuleID"] != DBNull.Value)
			{
				pInstance.TimingPushMessageRuleID =  (Guid)pReader["TimingPushMessageRuleID"];
			}
			if (pReader["CSPipelineID"] != DBNull.Value)
			{
				pInstance.CSPipelineID =   Convert.ToInt32(pReader["CSPipelineID"]);
			}
			if (pReader["VipID"] != DBNull.Value)
			{
				pInstance.VipID =  Convert.ToString(pReader["VipID"]);
			}
			if (pReader["LastModelID"] != DBNull.Value)
			{
				pInstance.LastModelID =  Convert.ToString(pReader["LastModelID"]);
			}
			if (pReader["LastContentID"] != DBNull.Value)
			{
				pInstance.LastContentID =  Convert.ToString(pReader["LastContentID"]);
			}
			if (pReader["LastContentIndex"] != DBNull.Value)
			{
				pInstance.LastContentIndex =   Convert.ToInt32(pReader["LastContentIndex"]);
			}
			if (pReader["ClientID"] != DBNull.Value)
			{
				pInstance.ClientID =  Convert.ToString(pReader["ClientID"]);
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

        }
        #endregion
    }
}
