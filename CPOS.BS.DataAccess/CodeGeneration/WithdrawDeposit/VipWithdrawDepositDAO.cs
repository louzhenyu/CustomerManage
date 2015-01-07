/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014-12-28 11:40:41
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
    /// ��VipWithdrawDeposit�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class VipWithdrawDepositDAO : Base.BaseCPOSDAO, ICRUDable<VipWithdrawDepositEntity>, IQueryable<VipWithdrawDepositEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VipWithdrawDepositDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(VipWithdrawDepositEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(VipWithdrawDepositEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [VipWithdrawDeposit](");
            strSql.Append("[VIPID],[BeginAmount],[InAmount],[OutAmount],[EndAmount],[TransitAmount],[TotalAmount],[PayPassword],[IsLocking],[CustomerID],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[WithdrawDepositID])");
            strSql.Append(" values (");
            strSql.Append("@VIPID,@BeginAmount,@InAmount,@OutAmount,@EndAmount,@TransitAmount,@TotalAmount,@PayPassword,@IsLocking,@CustomerID,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@WithdrawDepositID)");            

			Guid? pkGuid;
			if (pEntity.WithdrawDepositID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.WithdrawDepositID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@VIPID",SqlDbType.NVarChar),
					new SqlParameter("@BeginAmount",SqlDbType.Decimal),
					new SqlParameter("@InAmount",SqlDbType.Decimal),
					new SqlParameter("@OutAmount",SqlDbType.Decimal),
					new SqlParameter("@EndAmount",SqlDbType.Decimal),
					new SqlParameter("@TransitAmount",SqlDbType.Decimal),
					new SqlParameter("@TotalAmount",SqlDbType.Decimal),
					new SqlParameter("@PayPassword",SqlDbType.NVarChar),
					new SqlParameter("@IsLocking",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@WithdrawDepositID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.VIPID;
			parameters[1].Value = pEntity.BeginAmount;
			parameters[2].Value = pEntity.InAmount;
			parameters[3].Value = pEntity.OutAmount;
			parameters[4].Value = pEntity.EndAmount;
			parameters[5].Value = pEntity.TransitAmount;
			parameters[6].Value = pEntity.TotalAmount;
			parameters[7].Value = pEntity.PayPassword;
			parameters[8].Value = pEntity.IsLocking;
			parameters[9].Value = pEntity.CustomerID;
			parameters[10].Value = pEntity.CreateTime;
			parameters[11].Value = pEntity.CreateBy;
			parameters[12].Value = pEntity.LastUpdateTime;
			parameters[13].Value = pEntity.LastUpdateBy;
			parameters[14].Value = pEntity.IsDelete;
			parameters[15].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.WithdrawDepositID = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public VipWithdrawDepositEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipWithdrawDeposit] where WithdrawDepositID='{0}'  and isdelete=0 ", id.ToString());
            //��ȡ����
            VipWithdrawDepositEntity m = null;
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
        public VipWithdrawDepositEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipWithdrawDeposit] where 1=1  and isdelete=0");
            //��ȡ����
            List<VipWithdrawDepositEntity> list = new List<VipWithdrawDepositEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipWithdrawDepositEntity m;
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
        public void Update(VipWithdrawDepositEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(VipWithdrawDepositEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.WithdrawDepositID.HasValue)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [VipWithdrawDeposit] set ");
                        if (pIsUpdateNullField || pEntity.VIPID!=null)
                strSql.Append( "[VIPID]=@VIPID,");
            if (pIsUpdateNullField || pEntity.BeginAmount!=null)
                strSql.Append( "[BeginAmount]=@BeginAmount,");
            if (pIsUpdateNullField || pEntity.InAmount!=null)
                strSql.Append( "[InAmount]=@InAmount,");
            if (pIsUpdateNullField || pEntity.OutAmount!=null)
                strSql.Append( "[OutAmount]=@OutAmount,");
            if (pIsUpdateNullField || pEntity.EndAmount!=null)
                strSql.Append( "[EndAmount]=@EndAmount,");
            if (pIsUpdateNullField || pEntity.TransitAmount!=null)
                strSql.Append( "[TransitAmount]=@TransitAmount,");
            if (pIsUpdateNullField || pEntity.TotalAmount!=null)
                strSql.Append( "[TotalAmount]=@TotalAmount,");
            if (pIsUpdateNullField || pEntity.PayPassword!=null)
                strSql.Append( "[PayPassword]=@PayPassword,");
            if (pIsUpdateNullField || pEntity.IsLocking!=null)
                strSql.Append( "[IsLocking]=@IsLocking,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy");
            strSql.Append(" where WithdrawDepositID=@WithdrawDepositID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@VIPID",SqlDbType.NVarChar),
					new SqlParameter("@BeginAmount",SqlDbType.Decimal),
					new SqlParameter("@InAmount",SqlDbType.Decimal),
					new SqlParameter("@OutAmount",SqlDbType.Decimal),
					new SqlParameter("@EndAmount",SqlDbType.Decimal),
					new SqlParameter("@TransitAmount",SqlDbType.Decimal),
					new SqlParameter("@TotalAmount",SqlDbType.Decimal),
					new SqlParameter("@PayPassword",SqlDbType.NVarChar),
					new SqlParameter("@IsLocking",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@WithdrawDepositID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.VIPID;
			parameters[1].Value = pEntity.BeginAmount;
			parameters[2].Value = pEntity.InAmount;
			parameters[3].Value = pEntity.OutAmount;
			parameters[4].Value = pEntity.EndAmount;
			parameters[5].Value = pEntity.TransitAmount;
			parameters[6].Value = pEntity.TotalAmount;
			parameters[7].Value = pEntity.PayPassword;
			parameters[8].Value = pEntity.IsLocking;
			parameters[9].Value = pEntity.CustomerID;
			parameters[10].Value = pEntity.LastUpdateTime;
			parameters[11].Value = pEntity.LastUpdateBy;
			parameters[12].Value = pEntity.WithdrawDepositID;

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
        public void Update(VipWithdrawDepositEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(VipWithdrawDepositEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(VipWithdrawDepositEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.WithdrawDepositID.HasValue)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.WithdrawDepositID.Value, pTran);           
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
            sql.AppendLine("update [VipWithdrawDeposit] set  isdelete=1 where WithdrawDepositID=@WithdrawDepositID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@WithdrawDepositID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(VipWithdrawDepositEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.WithdrawDepositID.HasValue)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.WithdrawDepositID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(VipWithdrawDepositEntity[] pEntities)
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
            sql.AppendLine("update [VipWithdrawDeposit] set  isdelete=1 where WithdrawDepositID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public VipWithdrawDepositEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipWithdrawDeposit] where 1=1  and isdelete=0 ");
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
            List<VipWithdrawDepositEntity> list = new List<VipWithdrawDepositEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipWithdrawDepositEntity m;
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
        public PagedQueryResult<VipWithdrawDepositEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [WithdrawDepositID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [VipWithdrawDeposit] where 1=1  and isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [VipWithdrawDeposit] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<VipWithdrawDepositEntity> result = new PagedQueryResult<VipWithdrawDepositEntity>();
            List<VipWithdrawDepositEntity> list = new List<VipWithdrawDepositEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    VipWithdrawDepositEntity m;
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
        public VipWithdrawDepositEntity[] QueryByEntity(VipWithdrawDepositEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<VipWithdrawDepositEntity> PagedQueryByEntity(VipWithdrawDepositEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(VipWithdrawDepositEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.WithdrawDepositID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WithdrawDepositID", Value = pQueryEntity.WithdrawDepositID });
            if (pQueryEntity.VIPID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VIPID", Value = pQueryEntity.VIPID });
            if (pQueryEntity.BeginAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BeginAmount", Value = pQueryEntity.BeginAmount });
            if (pQueryEntity.InAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "InAmount", Value = pQueryEntity.InAmount });
            if (pQueryEntity.OutAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OutAmount", Value = pQueryEntity.OutAmount });
            if (pQueryEntity.EndAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EndAmount", Value = pQueryEntity.EndAmount });
            if (pQueryEntity.TransitAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TransitAmount", Value = pQueryEntity.TransitAmount });
            if (pQueryEntity.TotalAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TotalAmount", Value = pQueryEntity.TotalAmount });
            if (pQueryEntity.PayPassword!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PayPassword", Value = pQueryEntity.PayPassword });
            if (pQueryEntity.IsLocking!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsLocking", Value = pQueryEntity.IsLocking });
            if (pQueryEntity.CustomerID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });
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

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(IDataReader pReader, out VipWithdrawDepositEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new VipWithdrawDepositEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["WithdrawDepositID"] != DBNull.Value)
			{
				pInstance.WithdrawDepositID =  (Guid)pReader["WithdrawDepositID"];
			}
			if (pReader["VIPID"] != DBNull.Value)
			{
				pInstance.VIPID =  Convert.ToString(pReader["VIPID"]);
			}
			if (pReader["BeginAmount"] != DBNull.Value)
			{
				pInstance.BeginAmount =  Convert.ToDecimal(pReader["BeginAmount"]);
			}
			if (pReader["InAmount"] != DBNull.Value)
			{
				pInstance.InAmount =  Convert.ToDecimal(pReader["InAmount"]);
			}
			if (pReader["OutAmount"] != DBNull.Value)
			{
				pInstance.OutAmount =  Convert.ToDecimal(pReader["OutAmount"]);
			}
			if (pReader["EndAmount"] != DBNull.Value)
			{
				pInstance.EndAmount =  Convert.ToDecimal(pReader["EndAmount"]);
			}
			if (pReader["TransitAmount"] != DBNull.Value)
			{
				pInstance.TransitAmount =  Convert.ToDecimal(pReader["TransitAmount"]);
			}
			if (pReader["TotalAmount"] != DBNull.Value)
			{
				pInstance.TotalAmount =  Convert.ToDecimal(pReader["TotalAmount"]);
			}
			if (pReader["PayPassword"] != DBNull.Value)
			{
				pInstance.PayPassword =  Convert.ToString(pReader["PayPassword"]);
			}
			if (pReader["IsLocking"] != DBNull.Value)
			{
				pInstance.IsLocking =   Convert.ToInt32(pReader["IsLocking"]);
			}
			if (pReader["CustomerID"] != DBNull.Value)
			{
				pInstance.CustomerID =  Convert.ToString(pReader["CustomerID"]);
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

        }
        #endregion
    }
}
