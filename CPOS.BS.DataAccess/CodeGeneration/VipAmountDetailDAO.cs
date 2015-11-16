/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/10/30 15:25:27
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
    /// ��VipAmountDetail�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class VipAmountDetailDAO : Base.BaseCPOSDAO, ICRUDable<VipAmountDetailEntity>, IQueryable<VipAmountDetailEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VipAmountDetailDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(VipAmountDetailEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(VipAmountDetailEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [VipAmountDetail](");
            strSql.Append("[VipId],[VipCardCode],[UnitID],[UnitName],[SalesAmount],[Amount],[UsedReturnAmount],[Reason],[EffectiveDate],[DeadlineDate],[AmountSourceId],[ObjectId],[Remark],[IsValid],[IsWithdrawCash],[CustomerID],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[VipAmountDetailId])");
            strSql.Append(" values (");
            strSql.Append("@VipId,@VipCardCode,@UnitID,@UnitName,@SalesAmount,@Amount,@UsedReturnAmount,@Reason,@EffectiveDate,@DeadlineDate,@AmountSourceId,@ObjectId,@Remark,@IsValid,@IsWithdrawCash,@CustomerID,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@VipAmountDetailId)");

            Guid? pkGuid;
            if (pEntity.VipAmountDetailId == null)
                pkGuid = Guid.NewGuid();
            else
                pkGuid = pEntity.VipAmountDetailId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@VipId",SqlDbType.VarChar),
					new SqlParameter("@VipCardCode",SqlDbType.VarChar),
					new SqlParameter("@UnitID",SqlDbType.VarChar),
					new SqlParameter("@UnitName",SqlDbType.VarChar),
					new SqlParameter("@SalesAmount",SqlDbType.Decimal),
					new SqlParameter("@Amount",SqlDbType.Decimal),
					new SqlParameter("@UsedReturnAmount",SqlDbType.Decimal),
					new SqlParameter("@Reason",SqlDbType.NVarChar),
					new SqlParameter("@EffectiveDate",SqlDbType.DateTime),
					new SqlParameter("@DeadlineDate",SqlDbType.DateTime),
					new SqlParameter("@AmountSourceId",SqlDbType.NVarChar),
					new SqlParameter("@ObjectId",SqlDbType.NVarChar),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@IsValid",SqlDbType.Int),
					new SqlParameter("@IsWithdrawCash",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@VipAmountDetailId",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.VipId;
            parameters[1].Value = pEntity.VipCardCode;
            parameters[2].Value = pEntity.UnitID;
            parameters[3].Value = pEntity.UnitName;
            parameters[4].Value = pEntity.SalesAmount;
            parameters[5].Value = pEntity.Amount;
            parameters[6].Value = pEntity.UsedReturnAmount;
            parameters[7].Value = pEntity.Reason;
            parameters[8].Value = pEntity.EffectiveDate;
            parameters[9].Value = pEntity.DeadlineDate;
            parameters[10].Value = pEntity.AmountSourceId;
            parameters[11].Value = pEntity.ObjectId;
            parameters[12].Value = pEntity.Remark;
            parameters[13].Value = pEntity.IsValid;
            parameters[14].Value = pEntity.IsWithdrawCash;
            parameters[15].Value = pEntity.CustomerID;
            parameters[16].Value = pEntity.CreateTime;
            parameters[17].Value = pEntity.CreateBy;
            parameters[18].Value = pEntity.LastUpdateTime;
            parameters[19].Value = pEntity.LastUpdateBy;
            parameters[20].Value = pEntity.IsDelete;
            parameters[21].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            pEntity.VipAmountDetailId = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public VipAmountDetailEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipAmountDetail] where VipAmountDetailId='{0}'  and isdelete=0 ", id.ToString());
            //��ȡ����
            VipAmountDetailEntity m = null;
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
        public VipAmountDetailEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipAmountDetail] where 1=1  and isdelete=0");
            //��ȡ����
            List<VipAmountDetailEntity> list = new List<VipAmountDetailEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipAmountDetailEntity m;
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
        public void Update(VipAmountDetailEntity pEntity, IDbTransaction pTran)
        {
            Update(pEntity, pTran, true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(VipAmountDetailEntity pEntity, IDbTransaction pTran, bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.VipAmountDetailId.HasValue)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [VipAmountDetail] set ");
            if (pIsUpdateNullField || pEntity.VipId != null)
                strSql.Append("[VipId]=@VipId,");
            if (pIsUpdateNullField || pEntity.VipCardCode != null)
                strSql.Append("[VipCardCode]=@VipCardCode,");
            if (pIsUpdateNullField || pEntity.UnitID != null)
                strSql.Append("[UnitID]=@UnitID,");
            if (pIsUpdateNullField || pEntity.UnitName != null)
                strSql.Append("[UnitName]=@UnitName,");
            if (pIsUpdateNullField || pEntity.SalesAmount != null)
                strSql.Append("[SalesAmount]=@SalesAmount,");
            if (pIsUpdateNullField || pEntity.Amount != null)
                strSql.Append("[Amount]=@Amount,");
            if (pIsUpdateNullField || pEntity.UsedReturnAmount != null)
                strSql.Append("[UsedReturnAmount]=@UsedReturnAmount,");
            if (pIsUpdateNullField || pEntity.Reason != null)
                strSql.Append("[Reason]=@Reason,");
            if (pIsUpdateNullField || pEntity.EffectiveDate != null)
                strSql.Append("[EffectiveDate]=@EffectiveDate,");
            if (pIsUpdateNullField || pEntity.DeadlineDate != null)
                strSql.Append("[DeadlineDate]=@DeadlineDate,");
            if (pIsUpdateNullField || pEntity.AmountSourceId != null)
                strSql.Append("[AmountSourceId]=@AmountSourceId,");
            if (pIsUpdateNullField || pEntity.ObjectId != null)
                strSql.Append("[ObjectId]=@ObjectId,");
            if (pIsUpdateNullField || pEntity.Remark != null)
                strSql.Append("[Remark]=@Remark,");
            if (pIsUpdateNullField || pEntity.IsValid != null)
                strSql.Append("[IsValid]=@IsValid,");
            if (pIsUpdateNullField || pEntity.IsWithdrawCash != null)
                strSql.Append("[IsWithdrawCash]=@IsWithdrawCash,");
            if (pIsUpdateNullField || pEntity.CustomerID != null)
                strSql.Append("[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime != null)
                strSql.Append("[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy != null)
                strSql.Append("[LastUpdateBy]=@LastUpdateBy");
            strSql.Append(" where VipAmountDetailId=@VipAmountDetailId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@VipId",SqlDbType.VarChar),
					new SqlParameter("@VipCardCode",SqlDbType.VarChar),
					new SqlParameter("@UnitID",SqlDbType.VarChar),
					new SqlParameter("@UnitName",SqlDbType.VarChar),
					new SqlParameter("@SalesAmount",SqlDbType.Decimal),
					new SqlParameter("@Amount",SqlDbType.Decimal),
					new SqlParameter("@UsedReturnAmount",SqlDbType.Decimal),
					new SqlParameter("@Reason",SqlDbType.NVarChar),
					new SqlParameter("@EffectiveDate",SqlDbType.DateTime),
					new SqlParameter("@DeadlineDate",SqlDbType.DateTime),
					new SqlParameter("@AmountSourceId",SqlDbType.NVarChar),
					new SqlParameter("@ObjectId",SqlDbType.NVarChar),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@IsValid",SqlDbType.Int),
					new SqlParameter("@IsWithdrawCash",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@VipAmountDetailId",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.VipId;
            parameters[1].Value = pEntity.VipCardCode;
            parameters[2].Value = pEntity.UnitID;
            parameters[3].Value = pEntity.UnitName;
            parameters[4].Value = pEntity.SalesAmount;
            parameters[5].Value = pEntity.Amount;
            parameters[6].Value = pEntity.UsedReturnAmount;
            parameters[7].Value = pEntity.Reason;
            parameters[8].Value = pEntity.EffectiveDate;
            parameters[9].Value = pEntity.DeadlineDate;
            parameters[10].Value = pEntity.AmountSourceId;
            parameters[11].Value = pEntity.ObjectId;
            parameters[12].Value = pEntity.Remark;
            parameters[13].Value = pEntity.IsValid;
            parameters[14].Value = pEntity.IsWithdrawCash;
            parameters[15].Value = pEntity.CustomerID;
            parameters[16].Value = pEntity.LastUpdateTime;
            parameters[17].Value = pEntity.LastUpdateBy;
            parameters[18].Value = pEntity.VipAmountDetailId;

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
        public void Update(VipAmountDetailEntity pEntity)
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(VipAmountDetailEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(VipAmountDetailEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.VipAmountDetailId.HasValue)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.VipAmountDetailId.Value, pTran);
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
            sql.AppendLine("update [VipAmountDetail] set  isdelete=1 where VipAmountDetailId=@VipAmountDetailId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@VipAmountDetailId",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(VipAmountDetailEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.VipAmountDetailId.HasValue)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.VipAmountDetailId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(VipAmountDetailEntity[] pEntities)
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
            sql.AppendLine("update [VipAmountDetail] set  isdelete=1 where VipAmountDetailId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public VipAmountDetailEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipAmountDetail] where 1=1  and isdelete=0 ");
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
            List<VipAmountDetailEntity> list = new List<VipAmountDetailEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipAmountDetailEntity m;
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
        public PagedQueryResult<VipAmountDetailEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [VipAmountDetailId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [VipAmountDetail] where 1=1  and isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [VipAmountDetail] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<VipAmountDetailEntity> result = new PagedQueryResult<VipAmountDetailEntity>();
            List<VipAmountDetailEntity> list = new List<VipAmountDetailEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    VipAmountDetailEntity m;
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
        public VipAmountDetailEntity[] QueryByEntity(VipAmountDetailEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<VipAmountDetailEntity> PagedQueryByEntity(VipAmountDetailEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(VipAmountDetailEntity pQueryEntity)
        {
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.VipAmountDetailId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipAmountDetailId", Value = pQueryEntity.VipAmountDetailId });
            if (pQueryEntity.VipId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipId", Value = pQueryEntity.VipId });
            if (pQueryEntity.VipCardCode != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardCode", Value = pQueryEntity.VipCardCode });
            if (pQueryEntity.UnitID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitID", Value = pQueryEntity.UnitID });
            if (pQueryEntity.UnitName != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitName", Value = pQueryEntity.UnitName });
            if (pQueryEntity.SalesAmount != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesAmount", Value = pQueryEntity.SalesAmount });
            if (pQueryEntity.Amount != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Amount", Value = pQueryEntity.Amount });
            if (pQueryEntity.UsedReturnAmount != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UsedReturnAmount", Value = pQueryEntity.UsedReturnAmount });
            if (pQueryEntity.Reason != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Reason", Value = pQueryEntity.Reason });
            if (pQueryEntity.EffectiveDate != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EffectiveDate", Value = pQueryEntity.EffectiveDate });
            if (pQueryEntity.DeadlineDate != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DeadlineDate", Value = pQueryEntity.DeadlineDate });
            if (pQueryEntity.AmountSourceId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AmountSourceId", Value = pQueryEntity.AmountSourceId });
            if (pQueryEntity.ObjectId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ObjectId", Value = pQueryEntity.ObjectId });
            if (pQueryEntity.Remark != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Remark", Value = pQueryEntity.Remark });
            if (pQueryEntity.IsValid != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsValid", Value = pQueryEntity.IsValid });
            if (pQueryEntity.IsWithdrawCash != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsWithdrawCash", Value = pQueryEntity.IsWithdrawCash });
            if (pQueryEntity.CustomerID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });
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
        protected void Load(IDataReader pReader, out VipAmountDetailEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new VipAmountDetailEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["VipAmountDetailId"] != DBNull.Value)
            {
                pInstance.VipAmountDetailId = (Guid)pReader["VipAmountDetailId"];
            }
            if (pReader["VipId"] != DBNull.Value)
            {
                pInstance.VipId = Convert.ToString(pReader["VipId"]);
            }
            if (pReader["VipCardCode"] != DBNull.Value)
            {
                pInstance.VipCardCode = Convert.ToString(pReader["VipCardCode"]);
            }
            if (pReader["UnitID"] != DBNull.Value)
            {
                pInstance.UnitID = Convert.ToString(pReader["UnitID"]);
            }
            if (pReader["UnitName"] != DBNull.Value)
            {
                pInstance.UnitName = Convert.ToString(pReader["UnitName"]);
            }
            if (pReader["SalesAmount"] != DBNull.Value)
            {
                pInstance.SalesAmount = Convert.ToDecimal(pReader["SalesAmount"]);
            }
            if (pReader["Amount"] != DBNull.Value)
            {
                pInstance.Amount = Convert.ToDecimal(pReader["Amount"]);
            }
            if (pReader["UsedReturnAmount"] != DBNull.Value)
            {
                pInstance.UsedReturnAmount = Convert.ToDecimal(pReader["UsedReturnAmount"]);
            }
            if (pReader["Reason"] != DBNull.Value)
            {
                pInstance.Reason = Convert.ToString(pReader["Reason"]);
            }
            if (pReader["EffectiveDate"] != DBNull.Value)
            {
                pInstance.EffectiveDate = Convert.ToDateTime(pReader["EffectiveDate"]);
            }
            if (pReader["DeadlineDate"] != DBNull.Value)
            {
                pInstance.DeadlineDate = Convert.ToDateTime(pReader["DeadlineDate"]);
            }
            if (pReader["AmountSourceId"] != DBNull.Value)
            {
                pInstance.AmountSourceId = Convert.ToString(pReader["AmountSourceId"]);
            }
            if (pReader["ObjectId"] != DBNull.Value)
            {
                pInstance.ObjectId = Convert.ToString(pReader["ObjectId"]);
            }
            if (pReader["Remark"] != DBNull.Value)
            {
                pInstance.Remark = Convert.ToString(pReader["Remark"]);
            }
            if (pReader["IsValid"] != DBNull.Value)
            {
                pInstance.IsValid = Convert.ToInt32(pReader["IsValid"]);
            }
            if (pReader["IsWithdrawCash"] != DBNull.Value)
            {
                pInstance.IsWithdrawCash = Convert.ToInt32(pReader["IsWithdrawCash"]);
            }
            if (pReader["CustomerID"] != DBNull.Value)
            {
                pInstance.CustomerID = Convert.ToString(pReader["CustomerID"]);
            }
            if (pReader["CreateTime"] != DBNull.Value)
            {
                pInstance.CreateTime = Convert.ToDateTime(pReader["CreateTime"]);
            }
            if (pReader["CreateBy"] != DBNull.Value)
            {
                pInstance.CreateBy = Convert.ToString(pReader["CreateBy"]);
                if (pInstance.AmountSourceId == "23" || pInstance.AmountSourceId == "24")
                {
                    var userDao = new T_UserDAO(CurrentUserInfo);
                    var userInfo = userDao.GetByID(pInstance.CreateBy);
                    pInstance.CreateByName = userInfo != null ? userInfo.user_name : "";
                }
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
