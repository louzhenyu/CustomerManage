/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/1 19:09:45
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
    /// ��R_SRT_RTHome�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class R_SRT_RTHomeDAO : Base.BaseCPOSDAO, ICRUDable<R_SRT_RTHomeEntity>, IQueryable<R_SRT_RTHomeEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public R_SRT_RTHomeDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(R_SRT_RTHomeEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(R_SRT_RTHomeEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //��ʼ���̶��ֶ�
			pEntity.CreateTime=DateTime.Now;


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [R_SRT_RTHome](");
            strSql.Append("[DateCode],[CustomerId],[Day30ActiveRTCount],[Day30NoActiveRTCount],[Day30SalesRTCount],[Day30SalesExpandRTCount],[Day30SalesNoExpandRTCount],[Day30ExpandRTCount],[Day30JoinSalesRTCount],[Day30JoinNoSalesRTCount],[CreateTime],[ID])");
            strSql.Append(" values (");
            strSql.Append("@DateCode,@CustomerId,@Day30ActiveRTCount,@Day30NoActiveRTCount,@Day30SalesRTCount,@Day30SalesExpandRTCount,@Day30SalesNoExpandRTCount,@Day30ExpandRTCount,@Day30JoinSalesRTCount,@Day30JoinNoSalesRTCount,@CreateTime,@ID)");            

			Guid? pkGuid;
			if (pEntity.ID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.ID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@DateCode",SqlDbType.Date),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@Day30ActiveRTCount",SqlDbType.Int),
					new SqlParameter("@Day30NoActiveRTCount",SqlDbType.Int),
					new SqlParameter("@Day30SalesRTCount",SqlDbType.Int),
					new SqlParameter("@Day30SalesExpandRTCount",SqlDbType.Int),
					new SqlParameter("@Day30SalesNoExpandRTCount",SqlDbType.Int),
					new SqlParameter("@Day30ExpandRTCount",SqlDbType.Int),
					new SqlParameter("@Day30JoinSalesRTCount",SqlDbType.Int),
					new SqlParameter("@Day30JoinNoSalesRTCount",SqlDbType.Int),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@ID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.DateCode;
			parameters[1].Value = pEntity.CustomerId;
			parameters[2].Value = pEntity.Day30ActiveRTCount;
			parameters[3].Value = pEntity.Day30NoActiveRTCount;
			parameters[4].Value = pEntity.Day30SalesRTCount;
			parameters[5].Value = pEntity.Day30SalesExpandRTCount;
			parameters[6].Value = pEntity.Day30SalesNoExpandRTCount;
			parameters[7].Value = pEntity.Day30ExpandRTCount;
			parameters[8].Value = pEntity.Day30JoinSalesRTCount;
			parameters[9].Value = pEntity.Day30JoinNoSalesRTCount;
			parameters[10].Value = pEntity.CreateTime;
			parameters[11].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.ID = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public R_SRT_RTHomeEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [R_SRT_RTHome] where ID='{0}'  ", id.ToString());
            //��ȡ����
            R_SRT_RTHomeEntity m = null;
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
        public R_SRT_RTHomeEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [R_SRT_RTHome] where 1=1 ");
            //��ȡ����
            List<R_SRT_RTHomeEntity> list = new List<R_SRT_RTHomeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    R_SRT_RTHomeEntity m;
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
        public void Update(R_SRT_RTHomeEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(R_SRT_RTHomeEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.ID.HasValue)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [R_SRT_RTHome] set ");
                        if (pIsUpdateNullField || pEntity.DateCode!=null)
                strSql.Append( "[DateCode]=@DateCode,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.Day30ActiveRTCount!=null)
                strSql.Append( "[Day30ActiveRTCount]=@Day30ActiveRTCount,");
            if (pIsUpdateNullField || pEntity.Day30NoActiveRTCount!=null)
                strSql.Append( "[Day30NoActiveRTCount]=@Day30NoActiveRTCount,");
            if (pIsUpdateNullField || pEntity.Day30SalesRTCount!=null)
                strSql.Append( "[Day30SalesRTCount]=@Day30SalesRTCount,");
            if (pIsUpdateNullField || pEntity.Day30SalesExpandRTCount!=null)
                strSql.Append( "[Day30SalesExpandRTCount]=@Day30SalesExpandRTCount,");
            if (pIsUpdateNullField || pEntity.Day30SalesNoExpandRTCount!=null)
                strSql.Append( "[Day30SalesNoExpandRTCount]=@Day30SalesNoExpandRTCount,");
            if (pIsUpdateNullField || pEntity.Day30ExpandRTCount!=null)
                strSql.Append( "[Day30ExpandRTCount]=@Day30ExpandRTCount,");
            if (pIsUpdateNullField || pEntity.Day30JoinSalesRTCount!=null)
                strSql.Append( "[Day30JoinSalesRTCount]=@Day30JoinSalesRTCount,");
            if (pIsUpdateNullField || pEntity.Day30JoinNoSalesRTCount!=null)
                strSql.Append( "[Day30JoinNoSalesRTCount]=@Day30JoinNoSalesRTCount");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@DateCode",SqlDbType.Date),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@Day30ActiveRTCount",SqlDbType.Int),
					new SqlParameter("@Day30NoActiveRTCount",SqlDbType.Int),
					new SqlParameter("@Day30SalesRTCount",SqlDbType.Int),
					new SqlParameter("@Day30SalesExpandRTCount",SqlDbType.Int),
					new SqlParameter("@Day30SalesNoExpandRTCount",SqlDbType.Int),
					new SqlParameter("@Day30ExpandRTCount",SqlDbType.Int),
					new SqlParameter("@Day30JoinSalesRTCount",SqlDbType.Int),
					new SqlParameter("@Day30JoinNoSalesRTCount",SqlDbType.Int),
					new SqlParameter("@ID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.DateCode;
			parameters[1].Value = pEntity.CustomerId;
			parameters[2].Value = pEntity.Day30ActiveRTCount;
			parameters[3].Value = pEntity.Day30NoActiveRTCount;
			parameters[4].Value = pEntity.Day30SalesRTCount;
			parameters[5].Value = pEntity.Day30SalesExpandRTCount;
			parameters[6].Value = pEntity.Day30SalesNoExpandRTCount;
			parameters[7].Value = pEntity.Day30ExpandRTCount;
			parameters[8].Value = pEntity.Day30JoinSalesRTCount;
			parameters[9].Value = pEntity.Day30JoinNoSalesRTCount;
			parameters[10].Value = pEntity.ID;

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
        public void Update(R_SRT_RTHomeEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(R_SRT_RTHomeEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(R_SRT_RTHomeEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.ID.HasValue)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.ID.Value, pTran);           
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
            sql.AppendLine("update [R_SRT_RTHome] set  where ID=@ID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@ID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(R_SRT_RTHomeEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.ID.HasValue)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.ID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(R_SRT_RTHomeEntity[] pEntities)
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
            sql.AppendLine("update [R_SRT_RTHome] set  where ID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public R_SRT_RTHomeEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [R_SRT_RTHome] where 1=1  ");
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
            List<R_SRT_RTHomeEntity> list = new List<R_SRT_RTHomeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    R_SRT_RTHomeEntity m;
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
        public PagedQueryResult<R_SRT_RTHomeEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [ID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [R_SRT_RTHome] where 1=1  ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [R_SRT_RTHome] where 1=1  ");
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
            PagedQueryResult<R_SRT_RTHomeEntity> result = new PagedQueryResult<R_SRT_RTHomeEntity>();
            List<R_SRT_RTHomeEntity> list = new List<R_SRT_RTHomeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    R_SRT_RTHomeEntity m;
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
        public R_SRT_RTHomeEntity[] QueryByEntity(R_SRT_RTHomeEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<R_SRT_RTHomeEntity> PagedQueryByEntity(R_SRT_RTHomeEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(R_SRT_RTHomeEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.DateCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DateCode", Value = pQueryEntity.DateCode });
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
            if (pQueryEntity.Day30ActiveRTCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Day30ActiveRTCount", Value = pQueryEntity.Day30ActiveRTCount });
            if (pQueryEntity.Day30NoActiveRTCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Day30NoActiveRTCount", Value = pQueryEntity.Day30NoActiveRTCount });
            if (pQueryEntity.Day30SalesRTCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Day30SalesRTCount", Value = pQueryEntity.Day30SalesRTCount });
            if (pQueryEntity.Day30SalesExpandRTCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Day30SalesExpandRTCount", Value = pQueryEntity.Day30SalesExpandRTCount });
            if (pQueryEntity.Day30SalesNoExpandRTCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Day30SalesNoExpandRTCount", Value = pQueryEntity.Day30SalesNoExpandRTCount });
            if (pQueryEntity.Day30ExpandRTCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Day30ExpandRTCount", Value = pQueryEntity.Day30ExpandRTCount });
            if (pQueryEntity.Day30JoinSalesRTCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Day30JoinSalesRTCount", Value = pQueryEntity.Day30JoinSalesRTCount });
            if (pQueryEntity.Day30JoinNoSalesRTCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Day30JoinNoSalesRTCount", Value = pQueryEntity.Day30JoinNoSalesRTCount });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.ID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ID", Value = pQueryEntity.ID });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(IDataReader pReader, out R_SRT_RTHomeEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new R_SRT_RTHomeEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["DateCode"] != DBNull.Value)
			{
				pInstance.DateCode =Convert.ToDateTime(pReader["DateCode"]);
			}
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}
			if (pReader["Day30ActiveRTCount"] != DBNull.Value)
			{
				pInstance.Day30ActiveRTCount =   Convert.ToInt32(pReader["Day30ActiveRTCount"]);
			}
			if (pReader["Day30NoActiveRTCount"] != DBNull.Value)
			{
				pInstance.Day30NoActiveRTCount =   Convert.ToInt32(pReader["Day30NoActiveRTCount"]);
			}
			if (pReader["Day30SalesRTCount"] != DBNull.Value)
			{
				pInstance.Day30SalesRTCount =   Convert.ToInt32(pReader["Day30SalesRTCount"]);
			}
			if (pReader["Day30SalesExpandRTCount"] != DBNull.Value)
			{
				pInstance.Day30SalesExpandRTCount =   Convert.ToInt32(pReader["Day30SalesExpandRTCount"]);
			}
			if (pReader["Day30SalesNoExpandRTCount"] != DBNull.Value)
			{
				pInstance.Day30SalesNoExpandRTCount =   Convert.ToInt32(pReader["Day30SalesNoExpandRTCount"]);
			}
			if (pReader["Day30ExpandRTCount"] != DBNull.Value)
			{
				pInstance.Day30ExpandRTCount =   Convert.ToInt32(pReader["Day30ExpandRTCount"]);
			}
			if (pReader["Day30JoinSalesRTCount"] != DBNull.Value)
			{
				pInstance.Day30JoinSalesRTCount =   Convert.ToInt32(pReader["Day30JoinSalesRTCount"]);
			}
			if (pReader["Day30JoinNoSalesRTCount"] != DBNull.Value)
			{
				pInstance.Day30JoinNoSalesRTCount =   Convert.ToInt32(pReader["Day30JoinNoSalesRTCount"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}
			if (pReader["ID"] != DBNull.Value)
			{
				pInstance.ID =  (Guid)pReader["ID"];
			}

        }
        #endregion
    }
}
