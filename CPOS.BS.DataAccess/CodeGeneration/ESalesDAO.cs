/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/10/28 15:52:20
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
    /// ��ESales�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class ESalesDAO : Base.BaseCPOSDAO, ICRUDable<ESalesEntity>, IQueryable<ESalesEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public ESalesDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(ESalesEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(ESalesEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [ESales](");
            strSql.Append("[SalesName],[EnterpriseCustomerId],[SalesProductId],[SalesVipId],[EndDate],[ECSourceId],[StageId],[Possibility],[ForecastAmount],[Remark],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[CustomerId],[SalesId])");
            strSql.Append(" values (");
            strSql.Append("@SalesName,@EnterpriseCustomerId,@SalesProductId,@SalesVipId,@EndDate,@ECSourceId,@StageId,@Possibility,@ForecastAmount,@Remark,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@CustomerId,@SalesId)");            

			string pkString = pEntity.SalesId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@SalesName",SqlDbType.NVarChar),
					new SqlParameter("@EnterpriseCustomerId",SqlDbType.NVarChar),
					new SqlParameter("@SalesProductId",SqlDbType.NVarChar),
					new SqlParameter("@SalesVipId",SqlDbType.NVarChar),
					new SqlParameter("@EndDate",SqlDbType.NVarChar),
					new SqlParameter("@ECSourceId",SqlDbType.NVarChar),
					new SqlParameter("@StageId",SqlDbType.NVarChar),
					new SqlParameter("@Possibility",SqlDbType.NVarChar),
					new SqlParameter("@ForecastAmount",SqlDbType.NVarChar),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@SalesId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.SalesName;
			parameters[1].Value = pEntity.EnterpriseCustomerId;
			parameters[2].Value = pEntity.SalesProductId;
			parameters[3].Value = pEntity.SalesVipId;
			parameters[4].Value = pEntity.EndDate;
			parameters[5].Value = pEntity.ECSourceId;
			parameters[6].Value = pEntity.StageId;
			parameters[7].Value = pEntity.Possibility;
			parameters[8].Value = pEntity.ForecastAmount;
			parameters[9].Value = pEntity.Remark;
			parameters[10].Value = pEntity.CreateTime;
			parameters[11].Value = pEntity.CreateBy;
			parameters[12].Value = pEntity.LastUpdateBy;
			parameters[13].Value = pEntity.LastUpdateTime;
			parameters[14].Value = pEntity.IsDelete;
			parameters[15].Value = pEntity.CustomerId;
			parameters[16].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.SalesId = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public ESalesEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ESales] where SalesId='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            ESalesEntity m = null;
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
        public ESalesEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ESales] where isdelete=0");
            //��ȡ����
            List<ESalesEntity> list = new List<ESalesEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    ESalesEntity m;
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
        public void Update(ESalesEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(ESalesEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.SalesId==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [ESales] set ");
            if (pIsUpdateNullField || pEntity.SalesName!=null)
                strSql.Append( "[SalesName]=@SalesName,");
            if (pIsUpdateNullField || pEntity.EnterpriseCustomerId!=null)
                strSql.Append( "[EnterpriseCustomerId]=@EnterpriseCustomerId,");
            if (pIsUpdateNullField || pEntity.SalesProductId!=null)
                strSql.Append( "[SalesProductId]=@SalesProductId,");
            if (pIsUpdateNullField || pEntity.SalesVipId!=null)
                strSql.Append( "[SalesVipId]=@SalesVipId,");
            if (pIsUpdateNullField || pEntity.EndDate!=null)
                strSql.Append( "[EndDate]=@EndDate,");
            if (pIsUpdateNullField || pEntity.ECSourceId!=null)
                strSql.Append( "[ECSourceId]=@ECSourceId,");
            if (pIsUpdateNullField || pEntity.StageId!=null)
                strSql.Append( "[StageId]=@StageId,");
            if (pIsUpdateNullField || pEntity.Possibility!=null)
                strSql.Append( "[Possibility]=@Possibility,");
            if (pIsUpdateNullField || pEntity.ForecastAmount!=null)
                strSql.Append( "[ForecastAmount]=@ForecastAmount,");
            if (pIsUpdateNullField || pEntity.Remark!=null)
                strSql.Append( "[Remark]=@Remark,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where SalesId=@SalesId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@SalesName",SqlDbType.NVarChar),
					new SqlParameter("@EnterpriseCustomerId",SqlDbType.NVarChar),
					new SqlParameter("@SalesProductId",SqlDbType.NVarChar),
					new SqlParameter("@SalesVipId",SqlDbType.NVarChar),
					new SqlParameter("@EndDate",SqlDbType.NVarChar),
					new SqlParameter("@ECSourceId",SqlDbType.NVarChar),
					new SqlParameter("@StageId",SqlDbType.NVarChar),
					new SqlParameter("@Possibility",SqlDbType.NVarChar),
					new SqlParameter("@ForecastAmount",SqlDbType.NVarChar),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@SalesId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.SalesName;
			parameters[1].Value = pEntity.EnterpriseCustomerId;
			parameters[2].Value = pEntity.SalesProductId;
			parameters[3].Value = pEntity.SalesVipId;
			parameters[4].Value = pEntity.EndDate;
			parameters[5].Value = pEntity.ECSourceId;
			parameters[6].Value = pEntity.StageId;
			parameters[7].Value = pEntity.Possibility;
			parameters[8].Value = pEntity.ForecastAmount;
			parameters[9].Value = pEntity.Remark;
			parameters[10].Value = pEntity.LastUpdateBy;
			parameters[11].Value = pEntity.LastUpdateTime;
			parameters[12].Value = pEntity.CustomerId;
			parameters[13].Value = pEntity.SalesId;

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
        public void Update(ESalesEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(ESalesEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(ESalesEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(ESalesEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.SalesId==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.SalesId, pTran);           
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
            sql.AppendLine("update [ESales] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where SalesId=@SalesId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@SalesId",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(ESalesEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.SalesId==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.SalesId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(ESalesEntity[] pEntities)
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
            sql.AppendLine("update [ESales] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where SalesId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public ESalesEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ESales] where isdelete=0 ");
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
            List<ESalesEntity> list = new List<ESalesEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    ESalesEntity m;
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
        public PagedQueryResult<ESalesEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [SalesId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [ESales] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [ESales] where isdelete=0 ");
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
            PagedQueryResult<ESalesEntity> result = new PagedQueryResult<ESalesEntity>();
            List<ESalesEntity> list = new List<ESalesEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    ESalesEntity m;
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
        public ESalesEntity[] QueryByEntity(ESalesEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<ESalesEntity> PagedQueryByEntity(ESalesEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(ESalesEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.SalesId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesId", Value = pQueryEntity.SalesId });
            if (pQueryEntity.SalesName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesName", Value = pQueryEntity.SalesName });
            if (pQueryEntity.EnterpriseCustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EnterpriseCustomerId", Value = pQueryEntity.EnterpriseCustomerId });
            if (pQueryEntity.SalesProductId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesProductId", Value = pQueryEntity.SalesProductId });
            if (pQueryEntity.SalesVipId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesVipId", Value = pQueryEntity.SalesVipId });
            if (pQueryEntity.EndDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EndDate", Value = pQueryEntity.EndDate });
            if (pQueryEntity.ECSourceId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ECSourceId", Value = pQueryEntity.ECSourceId });
            if (pQueryEntity.StageId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StageId", Value = pQueryEntity.StageId });
            if (pQueryEntity.Possibility!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Possibility", Value = pQueryEntity.Possibility });
            if (pQueryEntity.ForecastAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ForecastAmount", Value = pQueryEntity.ForecastAmount });
            if (pQueryEntity.Remark!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Remark", Value = pQueryEntity.Remark });
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
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out ESalesEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new ESalesEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["SalesId"] != DBNull.Value)
			{
				pInstance.SalesId =  Convert.ToString(pReader["SalesId"]);
			}
			if (pReader["SalesName"] != DBNull.Value)
			{
				pInstance.SalesName =  Convert.ToString(pReader["SalesName"]);
			}
			if (pReader["EnterpriseCustomerId"] != DBNull.Value)
			{
				pInstance.EnterpriseCustomerId =  Convert.ToString(pReader["EnterpriseCustomerId"]);
			}
			if (pReader["SalesProductId"] != DBNull.Value)
			{
				pInstance.SalesProductId =  Convert.ToString(pReader["SalesProductId"]);
			}
			if (pReader["SalesVipId"] != DBNull.Value)
			{
				pInstance.SalesVipId =  Convert.ToString(pReader["SalesVipId"]);
			}
			if (pReader["EndDate"] != DBNull.Value)
			{
				pInstance.EndDate =  Convert.ToString(pReader["EndDate"]);
			}
			if (pReader["ECSourceId"] != DBNull.Value)
			{
				pInstance.ECSourceId =  Convert.ToString(pReader["ECSourceId"]);
			}
			if (pReader["StageId"] != DBNull.Value)
			{
				pInstance.StageId =  Convert.ToString(pReader["StageId"]);
			}
			if (pReader["Possibility"] != DBNull.Value)
			{
				pInstance.Possibility =  Convert.ToString(pReader["Possibility"]);
			}
			if (pReader["ForecastAmount"] != DBNull.Value)
			{
				pInstance.ForecastAmount =  Convert.ToString(pReader["ForecastAmount"]);
			}
			if (pReader["Remark"] != DBNull.Value)
			{
				pInstance.Remark =  Convert.ToString(pReader["Remark"]);
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
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}

        }
        #endregion
    }
}
