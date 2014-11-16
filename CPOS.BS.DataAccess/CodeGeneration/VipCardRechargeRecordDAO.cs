/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/20 11:22:28
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
    /// ��VipCardRechargeRecord�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class VipCardRechargeRecordDAO : Base.BaseCPOSDAO, ICRUDable<VipCardRechargeRecordEntity>, IQueryable<VipCardRechargeRecordEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VipCardRechargeRecordDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(VipCardRechargeRecordEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(VipCardRechargeRecordEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [VipCardRechargeRecord](");
            strSql.Append("[VipCardID],[RechargeAmount],[BalanceBeforeAmount],[BalanceAfterAmount],[RechargeNo],[PaymentTypeID],[RechargeTime],[RechargeUserID],[UnitID],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[CustomerID],[RechargeRecordID])");
            strSql.Append(" values (");
            strSql.Append("@VipCardID,@RechargeAmount,@BalanceBeforeAmount,@BalanceAfterAmount,@RechargeNo,@PaymentTypeID,@RechargeTime,@RechargeUserID,@UnitID,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@CustomerID,@RechargeRecordID)");            

			string pkString = pEntity.RechargeRecordID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@VipCardID",SqlDbType.NVarChar),
					new SqlParameter("@RechargeAmount",SqlDbType.Decimal),
					new SqlParameter("@BalanceBeforeAmount",SqlDbType.Decimal),
					new SqlParameter("@BalanceAfterAmount",SqlDbType.Decimal),
					new SqlParameter("@RechargeNo",SqlDbType.NVarChar),
					new SqlParameter("@PaymentTypeID",SqlDbType.NVarChar),
					new SqlParameter("@RechargeTime",SqlDbType.DateTime),
					new SqlParameter("@RechargeUserID",SqlDbType.NVarChar),
					new SqlParameter("@UnitID",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@RechargeRecordID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.VipCardID;
			parameters[1].Value = pEntity.RechargeAmount;
			parameters[2].Value = pEntity.BalanceBeforeAmount;
			parameters[3].Value = pEntity.BalanceAfterAmount;
			parameters[4].Value = pEntity.RechargeNo;
			parameters[5].Value = pEntity.PaymentTypeID;
			parameters[6].Value = pEntity.RechargeTime;
			parameters[7].Value = pEntity.RechargeUserID;
			parameters[8].Value = pEntity.UnitID;
			parameters[9].Value = pEntity.CreateTime;
			parameters[10].Value = pEntity.CreateBy;
			parameters[11].Value = pEntity.LastUpdateTime;
			parameters[12].Value = pEntity.LastUpdateBy;
			parameters[13].Value = pEntity.IsDelete;
			parameters[14].Value = pEntity.CustomerID;
			parameters[15].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.RechargeRecordID = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public VipCardRechargeRecordEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCardRechargeRecord] where RechargeRecordID='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            VipCardRechargeRecordEntity m = null;
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
        public VipCardRechargeRecordEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCardRechargeRecord] where isdelete=0");
            //��ȡ����
            List<VipCardRechargeRecordEntity> list = new List<VipCardRechargeRecordEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardRechargeRecordEntity m;
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
        public void Update(VipCardRechargeRecordEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(VipCardRechargeRecordEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.RechargeRecordID==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [VipCardRechargeRecord] set ");
            if (pIsUpdateNullField || pEntity.VipCardID!=null)
                strSql.Append( "[VipCardID]=@VipCardID,");
            if (pIsUpdateNullField || pEntity.RechargeAmount!=null)
                strSql.Append( "[RechargeAmount]=@RechargeAmount,");
            if (pIsUpdateNullField || pEntity.BalanceBeforeAmount!=null)
                strSql.Append( "[BalanceBeforeAmount]=@BalanceBeforeAmount,");
            if (pIsUpdateNullField || pEntity.BalanceAfterAmount!=null)
                strSql.Append( "[BalanceAfterAmount]=@BalanceAfterAmount,");
            if (pIsUpdateNullField || pEntity.RechargeNo!=null)
                strSql.Append( "[RechargeNo]=@RechargeNo,");
            if (pIsUpdateNullField || pEntity.PaymentTypeID!=null)
                strSql.Append( "[PaymentTypeID]=@PaymentTypeID,");
            if (pIsUpdateNullField || pEntity.RechargeTime!=null)
                strSql.Append( "[RechargeTime]=@RechargeTime,");
            if (pIsUpdateNullField || pEntity.RechargeUserID!=null)
                strSql.Append( "[RechargeUserID]=@RechargeUserID,");
            if (pIsUpdateNullField || pEntity.UnitID!=null)
                strSql.Append( "[UnitID]=@UnitID,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where RechargeRecordID=@RechargeRecordID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@VipCardID",SqlDbType.NVarChar),
					new SqlParameter("@RechargeAmount",SqlDbType.Decimal),
					new SqlParameter("@BalanceBeforeAmount",SqlDbType.Decimal),
					new SqlParameter("@BalanceAfterAmount",SqlDbType.Decimal),
					new SqlParameter("@RechargeNo",SqlDbType.NVarChar),
					new SqlParameter("@PaymentTypeID",SqlDbType.NVarChar),
					new SqlParameter("@RechargeTime",SqlDbType.DateTime),
					new SqlParameter("@RechargeUserID",SqlDbType.NVarChar),
					new SqlParameter("@UnitID",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@RechargeRecordID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.VipCardID;
			parameters[1].Value = pEntity.RechargeAmount;
			parameters[2].Value = pEntity.BalanceBeforeAmount;
			parameters[3].Value = pEntity.BalanceAfterAmount;
			parameters[4].Value = pEntity.RechargeNo;
			parameters[5].Value = pEntity.PaymentTypeID;
			parameters[6].Value = pEntity.RechargeTime;
			parameters[7].Value = pEntity.RechargeUserID;
			parameters[8].Value = pEntity.UnitID;
			parameters[9].Value = pEntity.LastUpdateTime;
			parameters[10].Value = pEntity.LastUpdateBy;
			parameters[11].Value = pEntity.CustomerID;
			parameters[12].Value = pEntity.RechargeRecordID;

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
        public void Update(VipCardRechargeRecordEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(VipCardRechargeRecordEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(VipCardRechargeRecordEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(VipCardRechargeRecordEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.RechargeRecordID==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.RechargeRecordID, pTran);           
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
            sql.AppendLine("update [VipCardRechargeRecord] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where RechargeRecordID=@RechargeRecordID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@RechargeRecordID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(VipCardRechargeRecordEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.RechargeRecordID==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.RechargeRecordID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(VipCardRechargeRecordEntity[] pEntities)
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
            sql.AppendLine("update [VipCardRechargeRecord] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where RechargeRecordID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public VipCardRechargeRecordEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCardRechargeRecord] where isdelete=0 ");
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
            List<VipCardRechargeRecordEntity> list = new List<VipCardRechargeRecordEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardRechargeRecordEntity m;
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
        public PagedQueryResult<VipCardRechargeRecordEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [RechargeRecordID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [VipCardRechargeRecord] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [VipCardRechargeRecord] where isdelete=0 ");
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
            PagedQueryResult<VipCardRechargeRecordEntity> result = new PagedQueryResult<VipCardRechargeRecordEntity>();
            List<VipCardRechargeRecordEntity> list = new List<VipCardRechargeRecordEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardRechargeRecordEntity m;
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
        public VipCardRechargeRecordEntity[] QueryByEntity(VipCardRechargeRecordEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<VipCardRechargeRecordEntity> PagedQueryByEntity(VipCardRechargeRecordEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(VipCardRechargeRecordEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.RechargeRecordID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RechargeRecordID", Value = pQueryEntity.RechargeRecordID });
            if (pQueryEntity.VipCardID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardID", Value = pQueryEntity.VipCardID });
            if (pQueryEntity.RechargeAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RechargeAmount", Value = pQueryEntity.RechargeAmount });
            if (pQueryEntity.BalanceBeforeAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BalanceBeforeAmount", Value = pQueryEntity.BalanceBeforeAmount });
            if (pQueryEntity.BalanceAfterAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BalanceAfterAmount", Value = pQueryEntity.BalanceAfterAmount });
            if (pQueryEntity.RechargeNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RechargeNo", Value = pQueryEntity.RechargeNo });
            if (pQueryEntity.PaymentTypeID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PaymentTypeID", Value = pQueryEntity.PaymentTypeID });
            if (pQueryEntity.RechargeTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RechargeTime", Value = pQueryEntity.RechargeTime });
            if (pQueryEntity.RechargeUserID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RechargeUserID", Value = pQueryEntity.RechargeUserID });
            if (pQueryEntity.UnitID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitID", Value = pQueryEntity.UnitID });
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
            if (pQueryEntity.CustomerID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out VipCardRechargeRecordEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new VipCardRechargeRecordEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["RechargeRecordID"] != DBNull.Value)
			{
				pInstance.RechargeRecordID =  Convert.ToString(pReader["RechargeRecordID"]);
			}
			if (pReader["VipCardID"] != DBNull.Value)
			{
				pInstance.VipCardID =  Convert.ToString(pReader["VipCardID"]);
			}
			if (pReader["RechargeAmount"] != DBNull.Value)
			{
				pInstance.RechargeAmount =  Convert.ToDecimal(pReader["RechargeAmount"]);
			}
			if (pReader["BalanceBeforeAmount"] != DBNull.Value)
			{
				pInstance.BalanceBeforeAmount =  Convert.ToDecimal(pReader["BalanceBeforeAmount"]);
			}
			if (pReader["BalanceAfterAmount"] != DBNull.Value)
			{
				pInstance.BalanceAfterAmount =  Convert.ToDecimal(pReader["BalanceAfterAmount"]);
			}
			if (pReader["RechargeNo"] != DBNull.Value)
			{
				pInstance.RechargeNo =  Convert.ToString(pReader["RechargeNo"]);
			}
			if (pReader["PaymentTypeID"] != DBNull.Value)
			{
				pInstance.PaymentTypeID =  Convert.ToString(pReader["PaymentTypeID"]);
			}
			if (pReader["RechargeTime"] != DBNull.Value)
			{
				pInstance.RechargeTime =  Convert.ToDateTime(pReader["RechargeTime"]);
			}
			if (pReader["RechargeUserID"] != DBNull.Value)
			{
				pInstance.RechargeUserID =  Convert.ToString(pReader["RechargeUserID"]);
			}
			if (pReader["UnitID"] != DBNull.Value)
			{
				pInstance.UnitID =  Convert.ToString(pReader["UnitID"]);
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
			if (pReader["CustomerID"] != DBNull.Value)
			{
				pInstance.CustomerID =  Convert.ToString(pReader["CustomerID"]);
			}

        }
        #endregion
    }
}
