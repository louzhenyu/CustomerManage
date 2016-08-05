/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/25 14:47:15
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
    /// ��VipCardProfitRule�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class VipCardProfitRuleDAO : Base.BaseCPOSDAO, ICRUDable<VipCardProfitRuleEntity>, IQueryable<VipCardProfitRuleEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VipCardProfitRuleDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(VipCardProfitRuleEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(VipCardProfitRuleEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [VipCardProfitRule](");
            strSql.Append("[VipCardTypeID],[ProfitOwner],[FirstCardSalesProfitPct],[FirstRechargeProfitPct],[IsApplyAllUnits],[IsConsumeRule],[CardSalesProfitPct],[RechargeProfitPct],[UnitCostRebateProfitPct],[RefId],[CustomerID],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[CardBuyToProfitRuleId])");
            strSql.Append(" values (");
            strSql.Append("@VipCardTypeID,@ProfitOwner,@FirstCardSalesProfitPct,@FirstRechargeProfitPct,@IsApplyAllUnits,@IsConsumeRule,@CardSalesProfitPct,@RechargeProfitPct,@UnitCostRebateProfitPct,@RefId,@CustomerID,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@CardBuyToProfitRuleId)");

            Guid? pkGuid;
            if (pEntity.CardBuyToProfitRuleId == null)
                pkGuid = Guid.NewGuid();
            else
                pkGuid = pEntity.CardBuyToProfitRuleId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@VipCardTypeID",SqlDbType.Int),
					new SqlParameter("@ProfitOwner",SqlDbType.VarChar),
					new SqlParameter("@FirstCardSalesProfitPct",SqlDbType.Decimal),
					new SqlParameter("@FirstRechargeProfitPct",SqlDbType.Decimal),
					new SqlParameter("@IsApplyAllUnits",SqlDbType.Int),
					new SqlParameter("@IsConsumeRule",SqlDbType.Int),
					new SqlParameter("@CardSalesProfitPct",SqlDbType.Decimal),
					new SqlParameter("@RechargeProfitPct",SqlDbType.Decimal),
					new SqlParameter("@UnitCostRebateProfitPct",SqlDbType.Decimal),
					new SqlParameter("@RefId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CardBuyToProfitRuleId",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.VipCardTypeID;
            parameters[1].Value = pEntity.ProfitOwner;
            parameters[2].Value = pEntity.FirstCardSalesProfitPct;
            parameters[3].Value = pEntity.FirstRechargeProfitPct;
            parameters[4].Value = pEntity.IsApplyAllUnits;
            parameters[5].Value = pEntity.IsConsumeRule;
            parameters[6].Value = pEntity.CardSalesProfitPct;
            parameters[7].Value = pEntity.RechargeProfitPct;
            parameters[8].Value = pEntity.UnitCostRebateProfitPct;
            parameters[9].Value = pEntity.RefId;
            parameters[10].Value = pEntity.CustomerID;
            parameters[11].Value = pEntity.CreateTime;
            parameters[12].Value = pEntity.CreateBy;
            parameters[13].Value = pEntity.LastUpdateTime;
            parameters[14].Value = pEntity.LastUpdateBy;
            parameters[15].Value = pEntity.IsDelete;
            parameters[16].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            pEntity.CardBuyToProfitRuleId = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public VipCardProfitRuleEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCardProfitRule] where CardBuyToProfitRuleId='{0}'  and isdelete=0 ", id.ToString());
            //��ȡ����
            VipCardProfitRuleEntity m = null;
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
        public VipCardProfitRuleEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCardProfitRule] where 1=1  and isdelete=0");
            //��ȡ����
            List<VipCardProfitRuleEntity> list = new List<VipCardProfitRuleEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardProfitRuleEntity m;
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
        public void Update(VipCardProfitRuleEntity pEntity, IDbTransaction pTran)
        {
            Update(pEntity, pTran, true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(VipCardProfitRuleEntity pEntity, IDbTransaction pTran, bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.CardBuyToProfitRuleId.HasValue)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [VipCardProfitRule] set ");
            if (pIsUpdateNullField || pEntity.VipCardTypeID != null)
                strSql.Append("[VipCardTypeID]=@VipCardTypeID,");
            if (pIsUpdateNullField || pEntity.ProfitOwner != null)
                strSql.Append("[ProfitOwner]=@ProfitOwner,");
            if (pIsUpdateNullField || pEntity.FirstCardSalesProfitPct != null)
                strSql.Append("[FirstCardSalesProfitPct]=@FirstCardSalesProfitPct,");
            if (pIsUpdateNullField || pEntity.FirstRechargeProfitPct != null)
                strSql.Append("[FirstRechargeProfitPct]=@FirstRechargeProfitPct,");
            if (pIsUpdateNullField || pEntity.IsApplyAllUnits != null)
                strSql.Append("[IsApplyAllUnits]=@IsApplyAllUnits,");
            if (pIsUpdateNullField || pEntity.IsConsumeRule != null)
                strSql.Append("[IsConsumeRule]=@IsConsumeRule,");
            if (pIsUpdateNullField || pEntity.CardSalesProfitPct != null)
                strSql.Append("[CardSalesProfitPct]=@CardSalesProfitPct,");
            if (pIsUpdateNullField || pEntity.RechargeProfitPct != null)
                strSql.Append("[RechargeProfitPct]=@RechargeProfitPct,");
            if (pIsUpdateNullField || pEntity.UnitCostRebateProfitPct != null)
                strSql.Append("[UnitCostRebateProfitPct]=@UnitCostRebateProfitPct,");
            if (pIsUpdateNullField || pEntity.RefId != null)
                strSql.Append("[RefId]=@RefId,");
            if (pIsUpdateNullField || pEntity.CustomerID != null)
                strSql.Append("[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime != null)
                strSql.Append("[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy != null)
                strSql.Append("[LastUpdateBy]=@LastUpdateBy , ");
            if (pIsUpdateNullField || pEntity.IsDelete != null)
                strSql.Append("[IsDelete]=@IsDelete");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where CardBuyToProfitRuleId=@CardBuyToProfitRuleId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@VipCardTypeID",SqlDbType.Int),
					new SqlParameter("@ProfitOwner",SqlDbType.VarChar),
					new SqlParameter("@FirstCardSalesProfitPct",SqlDbType.Decimal),
					new SqlParameter("@FirstRechargeProfitPct",SqlDbType.Decimal),
					new SqlParameter("@IsApplyAllUnits",SqlDbType.Int),
					new SqlParameter("@IsConsumeRule",SqlDbType.Int),
					new SqlParameter("@CardSalesProfitPct",SqlDbType.Decimal),
					new SqlParameter("@RechargeProfitPct",SqlDbType.Decimal),
					new SqlParameter("@UnitCostRebateProfitPct",SqlDbType.Decimal),
					new SqlParameter("@RefId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
                    new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CardBuyToProfitRuleId",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.VipCardTypeID;
            parameters[1].Value = pEntity.ProfitOwner;
            parameters[2].Value = pEntity.FirstCardSalesProfitPct;
            parameters[3].Value = pEntity.FirstRechargeProfitPct;
            parameters[4].Value = pEntity.IsApplyAllUnits;
            parameters[5].Value = pEntity.IsConsumeRule;
            parameters[6].Value = pEntity.CardSalesProfitPct;
            parameters[7].Value = pEntity.RechargeProfitPct;
            parameters[8].Value = pEntity.UnitCostRebateProfitPct;
            parameters[9].Value = pEntity.RefId;
            parameters[10].Value = pEntity.CustomerID;
            parameters[11].Value = pEntity.LastUpdateTime;
            parameters[12].Value = pEntity.LastUpdateBy;
            parameters[13].Value = pEntity.IsDelete;
            parameters[14].Value = pEntity.CardBuyToProfitRuleId;

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
        public void Update(VipCardProfitRuleEntity pEntity)
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(VipCardProfitRuleEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(VipCardProfitRuleEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.CardBuyToProfitRuleId.HasValue)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.CardBuyToProfitRuleId.Value, pTran);
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
            sql.AppendLine("update [VipCardProfitRule] set  isdelete=1 where CardBuyToProfitRuleId=@CardBuyToProfitRuleId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@CardBuyToProfitRuleId",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(VipCardProfitRuleEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.CardBuyToProfitRuleId.HasValue)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.CardBuyToProfitRuleId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(VipCardProfitRuleEntity[] pEntities)
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
            sql.AppendLine("update [VipCardProfitRule] set  isdelete=1 where CardBuyToProfitRuleId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public VipCardProfitRuleEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCardProfitRule] where 1=1  and isdelete=0 ");
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
            List<VipCardProfitRuleEntity> list = new List<VipCardProfitRuleEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardProfitRuleEntity m;
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
        public PagedQueryResult<VipCardProfitRuleEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [CreateTime] desc"); //Ĭ��Ϊ����ʱ�併��
            }
            pagedSql.AppendFormat(") as ___rn,* from [VipCardProfitRule] where 1=1  and isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [VipCardProfitRule] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<VipCardProfitRuleEntity> result = new PagedQueryResult<VipCardProfitRuleEntity>();
            List<VipCardProfitRuleEntity> list = new List<VipCardProfitRuleEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardProfitRuleEntity m;
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
        public VipCardProfitRuleEntity[] QueryByEntity(VipCardProfitRuleEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<VipCardProfitRuleEntity> PagedQueryByEntity(VipCardProfitRuleEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(VipCardProfitRuleEntity pQueryEntity)
        {
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.CardBuyToProfitRuleId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CardBuyToProfitRuleId", Value = pQueryEntity.CardBuyToProfitRuleId });
            if (pQueryEntity.VipCardTypeID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardTypeID", Value = pQueryEntity.VipCardTypeID });
            if (pQueryEntity.ProfitOwner != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ProfitOwner", Value = pQueryEntity.ProfitOwner });
            if (pQueryEntity.FirstCardSalesProfitPct != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FirstCardSalesProfitPct", Value = pQueryEntity.FirstCardSalesProfitPct });
            if (pQueryEntity.FirstRechargeProfitPct != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FirstRechargeProfitPct", Value = pQueryEntity.FirstRechargeProfitPct });
            if (pQueryEntity.IsApplyAllUnits != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsApplyAllUnits", Value = pQueryEntity.IsApplyAllUnits });
            if (pQueryEntity.IsConsumeRule != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsConsumeRule", Value = pQueryEntity.IsConsumeRule });
            if (pQueryEntity.CardSalesProfitPct != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CardSalesProfitPct", Value = pQueryEntity.CardSalesProfitPct });
            if (pQueryEntity.RechargeProfitPct != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RechargeProfitPct", Value = pQueryEntity.RechargeProfitPct });
            if (pQueryEntity.UnitCostRebateProfitPct != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitCostRebateProfitPct", Value = pQueryEntity.UnitCostRebateProfitPct });
            if (pQueryEntity.RefId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RefId", Value = pQueryEntity.RefId });
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
        protected void Load(IDataReader pReader, out VipCardProfitRuleEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new VipCardProfitRuleEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["CardBuyToProfitRuleId"] != DBNull.Value)
            {
                pInstance.CardBuyToProfitRuleId = (Guid)pReader["CardBuyToProfitRuleId"];
            }
            if (pReader["VipCardTypeID"] != DBNull.Value)
            {
                pInstance.VipCardTypeID = Convert.ToInt32(pReader["VipCardTypeID"]);
            }
            if (pReader["ProfitOwner"] != DBNull.Value)
            {
                pInstance.ProfitOwner = Convert.ToString(pReader["ProfitOwner"]);
            }
            if (pReader["FirstCardSalesProfitPct"] != DBNull.Value)
            {
                pInstance.FirstCardSalesProfitPct = Convert.ToDecimal(pReader["FirstCardSalesProfitPct"]);
            }
            if (pReader["FirstRechargeProfitPct"] != DBNull.Value)
            {
                pInstance.FirstRechargeProfitPct = Convert.ToDecimal(pReader["FirstRechargeProfitPct"]);
            }
            if (pReader["IsApplyAllUnits"] != DBNull.Value)
            {
                pInstance.IsApplyAllUnits = Convert.ToInt32(pReader["IsApplyAllUnits"]);
            }
            if (pReader["IsConsumeRule"] != DBNull.Value)
            {
                pInstance.IsConsumeRule = Convert.ToInt32(pReader["IsConsumeRule"]);
            }
            if (pReader["CardSalesProfitPct"] != DBNull.Value)
            {
                pInstance.CardSalesProfitPct = Convert.ToDecimal(pReader["CardSalesProfitPct"]);
            }
            if (pReader["RechargeProfitPct"] != DBNull.Value)
            {
                pInstance.RechargeProfitPct = Convert.ToDecimal(pReader["RechargeProfitPct"]);
            }
            if (pReader["UnitCostRebateProfitPct"] != DBNull.Value)
            {
                pInstance.UnitCostRebateProfitPct = Convert.ToDecimal(pReader["UnitCostRebateProfitPct"]);
            }
            if (pReader["RefId"] != DBNull.Value)
            {
                pInstance.RefId = (Guid)pReader["RefId"];
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
