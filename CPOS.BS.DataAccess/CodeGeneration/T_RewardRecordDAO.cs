/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016-1-18 17:23:57
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
    /// ��T_RewardRecord�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class T_RewardRecordDAO : Base.BaseCPOSDAO, ICRUDable<T_RewardRecordEntity>, IQueryable<T_RewardRecordEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public T_RewardRecordDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(T_RewardRecordEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(T_RewardRecordEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [T_RewardRecord](");
            strSql.Append("[RewardCode],[RewardOPType],[RewardOP],[RewardedOPType],[RewardedOP],[RewardType],[RewardAmount],[Remark],[PayStatus],[PayDatetime],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[CustomerId],[RewardId])");
            strSql.Append(" values (");
            strSql.Append("@RewardCode,@RewardOPType,@RewardOP,@RewardedOPType,@RewardedOP,@RewardType,@RewardAmount,@Remark,@PayStatus,@PayDatetime,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@CustomerId,@RewardId)");            

			Guid? pkGuid;
			if (pEntity.RewardId == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.RewardId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@RewardCode",SqlDbType.VarChar),
					new SqlParameter("@RewardOPType",SqlDbType.Int),
					new SqlParameter("@RewardOP",SqlDbType.NVarChar),
					new SqlParameter("@RewardedOPType",SqlDbType.Int),
					new SqlParameter("@RewardedOP",SqlDbType.NVarChar),
					new SqlParameter("@RewardType",SqlDbType.Int),
					new SqlParameter("@RewardAmount",SqlDbType.Decimal),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@PayStatus",SqlDbType.Int),
					new SqlParameter("@PayDatetime",SqlDbType.DateTime),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerId",SqlDbType.VarChar),
					new SqlParameter("@RewardId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.RewardCode;
			parameters[1].Value = pEntity.RewardOPType;
			parameters[2].Value = pEntity.RewardOP;
			parameters[3].Value = pEntity.RewardedOPType;
			parameters[4].Value = pEntity.RewardedOP;
			parameters[5].Value = pEntity.RewardType;
			parameters[6].Value = pEntity.RewardAmount;
			parameters[7].Value = pEntity.Remark;
			parameters[8].Value = pEntity.PayStatus;
			parameters[9].Value = pEntity.PayDatetime;
			parameters[10].Value = pEntity.CreateTime;
			parameters[11].Value = pEntity.CreateBy;
			parameters[12].Value = pEntity.LastUpdateTime;
			parameters[13].Value = pEntity.LastUpdateBy;
			parameters[14].Value = pEntity.IsDelete;
			parameters[15].Value = pEntity.CustomerId;
			parameters[16].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.RewardId = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public T_RewardRecordEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_RewardRecord] where RewardId='{0}'  and isdelete=0 ", id.ToString());
            //��ȡ����
            T_RewardRecordEntity m = null;
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
        public T_RewardRecordEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_RewardRecord] where 1=1  and isdelete=0");
            //��ȡ����
            List<T_RewardRecordEntity> list = new List<T_RewardRecordEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_RewardRecordEntity m;
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
        public void Update(T_RewardRecordEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(T_RewardRecordEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.RewardId.HasValue)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_RewardRecord] set ");
                        if (pIsUpdateNullField || pEntity.RewardCode!=null)
                strSql.Append( "[RewardCode]=@RewardCode,");
            if (pIsUpdateNullField || pEntity.RewardOPType!=null)
                strSql.Append( "[RewardOPType]=@RewardOPType,");
            if (pIsUpdateNullField || pEntity.RewardOP!=null)
                strSql.Append( "[RewardOP]=@RewardOP,");
            if (pIsUpdateNullField || pEntity.RewardedOPType!=null)
                strSql.Append( "[RewardedOPType]=@RewardedOPType,");
            if (pIsUpdateNullField || pEntity.RewardedOP!=null)
                strSql.Append( "[RewardedOP]=@RewardedOP,");
            if (pIsUpdateNullField || pEntity.RewardType!=null)
                strSql.Append( "[RewardType]=@RewardType,");
            if (pIsUpdateNullField || pEntity.RewardAmount!=null)
                strSql.Append( "[RewardAmount]=@RewardAmount,");
            if (pIsUpdateNullField || pEntity.Remark!=null)
                strSql.Append( "[Remark]=@Remark,");
            if (pIsUpdateNullField || pEntity.PayStatus!=null)
                strSql.Append( "[PayStatus]=@PayStatus,");
            if (pIsUpdateNullField || pEntity.PayDatetime!=null)
                strSql.Append( "[PayDatetime]=@PayDatetime,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId");
            strSql.Append(" where RewardId=@RewardId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@RewardCode",SqlDbType.VarChar),
					new SqlParameter("@RewardOPType",SqlDbType.Int),
					new SqlParameter("@RewardOP",SqlDbType.NVarChar),
					new SqlParameter("@RewardedOPType",SqlDbType.Int),
					new SqlParameter("@RewardedOP",SqlDbType.NVarChar),
					new SqlParameter("@RewardType",SqlDbType.Int),
					new SqlParameter("@RewardAmount",SqlDbType.Decimal),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@PayStatus",SqlDbType.Int),
					new SqlParameter("@PayDatetime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@CustomerId",SqlDbType.VarChar),
					new SqlParameter("@RewardId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.RewardCode;
			parameters[1].Value = pEntity.RewardOPType;
			parameters[2].Value = pEntity.RewardOP;
			parameters[3].Value = pEntity.RewardedOPType;
			parameters[4].Value = pEntity.RewardedOP;
			parameters[5].Value = pEntity.RewardType;
			parameters[6].Value = pEntity.RewardAmount;
			parameters[7].Value = pEntity.Remark;
			parameters[8].Value = pEntity.PayStatus;
			parameters[9].Value = pEntity.PayDatetime;
			parameters[10].Value = pEntity.LastUpdateTime;
			parameters[11].Value = pEntity.LastUpdateBy;
			parameters[12].Value = pEntity.CustomerId;
			parameters[13].Value = pEntity.RewardId;

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
        public void Update(T_RewardRecordEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(T_RewardRecordEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(T_RewardRecordEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.RewardId.HasValue)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.RewardId.Value, pTran);           
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
            sql.AppendLine("update [T_RewardRecord] set  isdelete=1 where RewardId=@RewardId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@RewardId",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(T_RewardRecordEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.RewardId.HasValue)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.RewardId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(T_RewardRecordEntity[] pEntities)
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
            sql.AppendLine("update [T_RewardRecord] set  isdelete=1 where RewardId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public T_RewardRecordEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_RewardRecord] where 1=1  and isdelete=0 ");
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
            List<T_RewardRecordEntity> list = new List<T_RewardRecordEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_RewardRecordEntity m;
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
        public PagedQueryResult<T_RewardRecordEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [RewardId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [T_RewardRecord] where 1=1  and isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [T_RewardRecord] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<T_RewardRecordEntity> result = new PagedQueryResult<T_RewardRecordEntity>();
            List<T_RewardRecordEntity> list = new List<T_RewardRecordEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    T_RewardRecordEntity m;
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
        public T_RewardRecordEntity[] QueryByEntity(T_RewardRecordEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<T_RewardRecordEntity> PagedQueryByEntity(T_RewardRecordEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(T_RewardRecordEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.RewardId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RewardId", Value = pQueryEntity.RewardId });
            if (pQueryEntity.RewardCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RewardCode", Value = pQueryEntity.RewardCode });
            if (pQueryEntity.RewardOPType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RewardOPType", Value = pQueryEntity.RewardOPType });
            if (pQueryEntity.RewardOP!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RewardOP", Value = pQueryEntity.RewardOP });
            if (pQueryEntity.RewardedOPType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RewardedOPType", Value = pQueryEntity.RewardedOPType });
            if (pQueryEntity.RewardedOP!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RewardedOP", Value = pQueryEntity.RewardedOP });
            if (pQueryEntity.RewardType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RewardType", Value = pQueryEntity.RewardType });
            if (pQueryEntity.RewardAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RewardAmount", Value = pQueryEntity.RewardAmount });
            if (pQueryEntity.Remark!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Remark", Value = pQueryEntity.Remark });
            if (pQueryEntity.PayStatus!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PayStatus", Value = pQueryEntity.PayStatus });
            if (pQueryEntity.PayDatetime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PayDatetime", Value = pQueryEntity.PayDatetime });
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
        protected void Load(IDataReader pReader, out T_RewardRecordEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new T_RewardRecordEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["RewardId"] != DBNull.Value)
			{
				pInstance.RewardId =  (Guid)pReader["RewardId"];
			}
			if (pReader["RewardCode"] != DBNull.Value)
			{
				pInstance.RewardCode =  Convert.ToString(pReader["RewardCode"]);
			}
			if (pReader["RewardOPType"] != DBNull.Value)
			{
				pInstance.RewardOPType =   Convert.ToInt32(pReader["RewardOPType"]);
			}
			if (pReader["RewardOP"] != DBNull.Value)
			{
				pInstance.RewardOP =  Convert.ToString(pReader["RewardOP"]);
			}
			if (pReader["RewardedOPType"] != DBNull.Value)
			{
				pInstance.RewardedOPType =   Convert.ToInt32(pReader["RewardedOPType"]);
			}
			if (pReader["RewardedOP"] != DBNull.Value)
			{
				pInstance.RewardedOP =  Convert.ToString(pReader["RewardedOP"]);
			}
			if (pReader["RewardType"] != DBNull.Value)
			{
				pInstance.RewardType =   Convert.ToInt32(pReader["RewardType"]);
			}
			if (pReader["RewardAmount"] != DBNull.Value)
			{
				pInstance.RewardAmount =  Convert.ToDecimal(pReader["RewardAmount"]);
			}
			if (pReader["Remark"] != DBNull.Value)
			{
				pInstance.Remark =  Convert.ToString(pReader["Remark"]);
			}
			if (pReader["PayStatus"] != DBNull.Value)
			{
				pInstance.PayStatus =   Convert.ToInt32(pReader["PayStatus"]);
			}
			if (pReader["PayDatetime"] != DBNull.Value)
			{
				pInstance.PayDatetime =  Convert.ToDateTime(pReader["PayDatetime"]);
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
