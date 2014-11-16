/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/25 14:42:41
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
    /// ��WXHouseDayTransaction�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class WXHouseDayTransactionDAO : Base.BaseCPOSDAO, ICRUDable<WXHouseDayTransactionEntity>, IQueryable<WXHouseDayTransactionEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public WXHouseDayTransactionDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(WXHouseDayTransactionEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(WXHouseDayTransactionEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [WXHouseDayTransaction](");
            strSql.Append("[OrderDate],[Assignbuyer],[SeqNO],[HatradedDate],[ThirdOrderNo],[FundType],[EntrustPay],[ClearType],[FundState],[RetCode],[RetMsg],[CustomerID],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[TransactID])");
            strSql.Append(" values (");
            strSql.Append("@OrderDate,@Assignbuyer,@SeqNO,@HatradedDate,@ThirdOrderNo,@FundType,@EntrustPay,@ClearType,@FundState,@RetCode,@RetMsg,@CustomerID,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@TransactID)");            

			Guid? pkGuid;
			if (pEntity.TransactID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.TransactID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@OrderDate",SqlDbType.VarChar),
					new SqlParameter("@Assignbuyer",SqlDbType.NVarChar),
					new SqlParameter("@SeqNO",SqlDbType.VarChar),
					new SqlParameter("@HatradedDate",SqlDbType.VarChar),
					new SqlParameter("@ThirdOrderNo",SqlDbType.NVarChar),
					new SqlParameter("@FundType",SqlDbType.VarChar),
					new SqlParameter("@EntrustPay",SqlDbType.Decimal),
					new SqlParameter("@ClearType",SqlDbType.Int),
					new SqlParameter("@FundState",SqlDbType.VarChar),
					new SqlParameter("@RetCode",SqlDbType.VarChar),
					new SqlParameter("@RetMsg",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@TransactID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.OrderDate;
			parameters[1].Value = pEntity.Assignbuyer;
			parameters[2].Value = pEntity.SeqNO;
			parameters[3].Value = pEntity.HatradedDate;
			parameters[4].Value = pEntity.ThirdOrderNo;
			parameters[5].Value = pEntity.FundType;
			parameters[6].Value = pEntity.EntrustPay;
			parameters[7].Value = pEntity.ClearType;
			parameters[8].Value = pEntity.FundState;
			parameters[9].Value = pEntity.RetCode;
			parameters[10].Value = pEntity.RetMsg;
			parameters[11].Value = pEntity.CustomerID;
			parameters[12].Value = pEntity.CreateBy;
			parameters[13].Value = pEntity.CreateTime;
			parameters[14].Value = pEntity.LastUpdateBy;
			parameters[15].Value = pEntity.LastUpdateTime;
			parameters[16].Value = pEntity.IsDelete;
			parameters[17].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.TransactID = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public WXHouseDayTransactionEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXHouseDayTransaction] where TransactID='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            WXHouseDayTransactionEntity m = null;
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
        public WXHouseDayTransactionEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXHouseDayTransaction] where isdelete=0");
            //��ȡ����
            List<WXHouseDayTransactionEntity> list = new List<WXHouseDayTransactionEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WXHouseDayTransactionEntity m;
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
        public void Update(WXHouseDayTransactionEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(WXHouseDayTransactionEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.TransactID==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [WXHouseDayTransaction] set ");
            if (pIsUpdateNullField || pEntity.OrderDate!=null)
                strSql.Append( "[OrderDate]=@OrderDate,");
            if (pIsUpdateNullField || pEntity.Assignbuyer!=null)
                strSql.Append( "[Assignbuyer]=@Assignbuyer,");
            if (pIsUpdateNullField || pEntity.SeqNO!=null)
                strSql.Append( "[SeqNO]=@SeqNO,");
            if (pIsUpdateNullField || pEntity.HatradedDate!=null)
                strSql.Append( "[HatradedDate]=@HatradedDate,");
            if (pIsUpdateNullField || pEntity.ThirdOrderNo!=null)
                strSql.Append( "[ThirdOrderNo]=@ThirdOrderNo,");
            if (pIsUpdateNullField || pEntity.FundType!=null)
                strSql.Append( "[FundType]=@FundType,");
            if (pIsUpdateNullField || pEntity.EntrustPay!=null)
                strSql.Append( "[EntrustPay]=@EntrustPay,");
            if (pIsUpdateNullField || pEntity.ClearType!=null)
                strSql.Append( "[ClearType]=@ClearType,");
            if (pIsUpdateNullField || pEntity.FundState!=null)
                strSql.Append( "[FundState]=@FundState,");
            if (pIsUpdateNullField || pEntity.RetCode!=null)
                strSql.Append( "[RetCode]=@RetCode,");
            if (pIsUpdateNullField || pEntity.RetMsg!=null)
                strSql.Append( "[RetMsg]=@RetMsg,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where TransactID=@TransactID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@OrderDate",SqlDbType.VarChar),
					new SqlParameter("@Assignbuyer",SqlDbType.NVarChar),
					new SqlParameter("@SeqNO",SqlDbType.VarChar),
					new SqlParameter("@HatradedDate",SqlDbType.VarChar),
					new SqlParameter("@ThirdOrderNo",SqlDbType.NVarChar),
					new SqlParameter("@FundType",SqlDbType.VarChar),
					new SqlParameter("@EntrustPay",SqlDbType.Decimal),
					new SqlParameter("@ClearType",SqlDbType.Int),
					new SqlParameter("@FundState",SqlDbType.VarChar),
					new SqlParameter("@RetCode",SqlDbType.VarChar),
					new SqlParameter("@RetMsg",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@TransactID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.OrderDate;
			parameters[1].Value = pEntity.Assignbuyer;
			parameters[2].Value = pEntity.SeqNO;
			parameters[3].Value = pEntity.HatradedDate;
			parameters[4].Value = pEntity.ThirdOrderNo;
			parameters[5].Value = pEntity.FundType;
			parameters[6].Value = pEntity.EntrustPay;
			parameters[7].Value = pEntity.ClearType;
			parameters[8].Value = pEntity.FundState;
			parameters[9].Value = pEntity.RetCode;
			parameters[10].Value = pEntity.RetMsg;
			parameters[11].Value = pEntity.CustomerID;
			parameters[12].Value = pEntity.LastUpdateBy;
			parameters[13].Value = pEntity.LastUpdateTime;
			parameters[14].Value = pEntity.TransactID;

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
        public void Update(WXHouseDayTransactionEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(WXHouseDayTransactionEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(WXHouseDayTransactionEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(WXHouseDayTransactionEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.TransactID==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.TransactID, pTran);           
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
            sql.AppendLine("update [WXHouseDayTransaction] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where TransactID=@TransactID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@TransactID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(WXHouseDayTransactionEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.TransactID==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.TransactID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(WXHouseDayTransactionEntity[] pEntities)
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
            sql.AppendLine("update [WXHouseDayTransaction] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where TransactID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public WXHouseDayTransactionEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXHouseDayTransaction] where isdelete=0 ");
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
            List<WXHouseDayTransactionEntity> list = new List<WXHouseDayTransactionEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WXHouseDayTransactionEntity m;
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
        public PagedQueryResult<WXHouseDayTransactionEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [TransactID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [WXHouseDayTransaction] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [WXHouseDayTransaction] where isdelete=0 ");
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
            PagedQueryResult<WXHouseDayTransactionEntity> result = new PagedQueryResult<WXHouseDayTransactionEntity>();
            List<WXHouseDayTransactionEntity> list = new List<WXHouseDayTransactionEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    WXHouseDayTransactionEntity m;
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
        public WXHouseDayTransactionEntity[] QueryByEntity(WXHouseDayTransactionEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<WXHouseDayTransactionEntity> PagedQueryByEntity(WXHouseDayTransactionEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(WXHouseDayTransactionEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.TransactID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TransactID", Value = pQueryEntity.TransactID });
            if (pQueryEntity.OrderDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderDate", Value = pQueryEntity.OrderDate });
            if (pQueryEntity.Assignbuyer!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Assignbuyer", Value = pQueryEntity.Assignbuyer });
            if (pQueryEntity.SeqNO!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SeqNO", Value = pQueryEntity.SeqNO });
            if (pQueryEntity.HatradedDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "HatradedDate", Value = pQueryEntity.HatradedDate });
            if (pQueryEntity.ThirdOrderNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ThirdOrderNo", Value = pQueryEntity.ThirdOrderNo });
            if (pQueryEntity.FundType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FundType", Value = pQueryEntity.FundType });
            if (pQueryEntity.EntrustPay!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EntrustPay", Value = pQueryEntity.EntrustPay });
            if (pQueryEntity.ClearType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ClearType", Value = pQueryEntity.ClearType });
            if (pQueryEntity.FundState!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FundState", Value = pQueryEntity.FundState });
            if (pQueryEntity.RetCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RetCode", Value = pQueryEntity.RetCode });
            if (pQueryEntity.RetMsg!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RetMsg", Value = pQueryEntity.RetMsg });
            if (pQueryEntity.CustomerID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });
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
        protected void Load(SqlDataReader pReader, out WXHouseDayTransactionEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new WXHouseDayTransactionEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["TransactID"] != DBNull.Value)
			{
				pInstance.TransactID =  (Guid)pReader["TransactID"];
			}
			if (pReader["OrderDate"] != DBNull.Value)
			{
				pInstance.OrderDate =  Convert.ToString(pReader["OrderDate"]);
			}
			if (pReader["Assignbuyer"] != DBNull.Value)
			{
				pInstance.Assignbuyer =  Convert.ToString(pReader["Assignbuyer"]);
			}
			if (pReader["SeqNO"] != DBNull.Value)
			{
				pInstance.SeqNO =  Convert.ToString(pReader["SeqNO"]);
			}
			if (pReader["HatradedDate"] != DBNull.Value)
			{
				pInstance.HatradedDate =  Convert.ToString(pReader["HatradedDate"]);
			}
			if (pReader["ThirdOrderNo"] != DBNull.Value)
			{
				pInstance.ThirdOrderNo =  Convert.ToString(pReader["ThirdOrderNo"]);
			}
			if (pReader["FundType"] != DBNull.Value)
			{
				pInstance.FundType =  Convert.ToString(pReader["FundType"]);
			}
			if (pReader["EntrustPay"] != DBNull.Value)
			{
				pInstance.EntrustPay =  Convert.ToDecimal(pReader["EntrustPay"]);
			}
			if (pReader["ClearType"] != DBNull.Value)
			{
				pInstance.ClearType =   Convert.ToInt32(pReader["ClearType"]);
			}
			if (pReader["FundState"] != DBNull.Value)
			{
				pInstance.FundState =  Convert.ToString(pReader["FundState"]);
			}
			if (pReader["RetCode"] != DBNull.Value)
			{
				pInstance.RetCode =  Convert.ToString(pReader["RetCode"]);
			}
			if (pReader["RetMsg"] != DBNull.Value)
			{
				pInstance.RetMsg =  Convert.ToString(pReader["RetMsg"]);
			}
			if (pReader["CustomerID"] != DBNull.Value)
			{
				pInstance.CustomerID =  Convert.ToString(pReader["CustomerID"]);
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
