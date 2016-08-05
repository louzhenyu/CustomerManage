/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/25 15:10:56
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
    /// ��VipCardUpgradeRule�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class VipCardUpgradeRuleDAO : Base.BaseCPOSDAO, ICRUDable<VipCardUpgradeRuleEntity>, IQueryable<VipCardUpgradeRuleEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VipCardUpgradeRuleDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(VipCardUpgradeRuleEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(VipCardUpgradeRuleEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [VipCardUpgradeRule](");
            strSql.Append("[VipCardTypeID],[IsFormVerify],[IsPurchaseUpgrade],[IsExchangeIntegral],[IsRecharge],[OnceRechargeAmount],[IsBuyUpgrade],[BuyAmount],[OnceBuyAmount],[IsPointUpgrade],[TotalPoint],[IsMustDeductPoint],[RefId],[CustomerID],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[VipCardUpgradeRuleId])");
            strSql.Append(" values (");
            strSql.Append("@VipCardTypeID,@IsFormVerify,@IsPurchaseUpgrade,@IsExchangeIntegral,@IsRecharge,@OnceRechargeAmount,@IsBuyUpgrade,@BuyAmount,@OnceBuyAmount,@IsPointUpgrade,@TotalPoint,@IsMustDeductPoint,@RefId,@CustomerID,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@VipCardUpgradeRuleId)");            

			Guid? pkGuid;
			if (pEntity.VipCardUpgradeRuleId == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.VipCardUpgradeRuleId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@VipCardTypeID",SqlDbType.Int),
					new SqlParameter("@IsFormVerify",SqlDbType.Int),
					new SqlParameter("@IsPurchaseUpgrade",SqlDbType.Int),
					new SqlParameter("@IsExchangeIntegral",SqlDbType.Int),
					new SqlParameter("@IsRecharge",SqlDbType.Int),
					new SqlParameter("@OnceRechargeAmount",SqlDbType.Decimal),
					new SqlParameter("@IsBuyUpgrade",SqlDbType.Int),
					new SqlParameter("@BuyAmount",SqlDbType.Decimal),
					new SqlParameter("@OnceBuyAmount",SqlDbType.Decimal),
					new SqlParameter("@IsPointUpgrade",SqlDbType.Int),
					new SqlParameter("@TotalPoint",SqlDbType.Decimal),
					new SqlParameter("@IsMustDeductPoint",SqlDbType.Int),
					new SqlParameter("@RefId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@VipCardUpgradeRuleId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.VipCardTypeID;
			parameters[1].Value = pEntity.IsFormVerify;
			parameters[2].Value = pEntity.IsPurchaseUpgrade;
			parameters[3].Value = pEntity.IsExchangeIntegral;
			parameters[4].Value = pEntity.IsRecharge;
			parameters[5].Value = pEntity.OnceRechargeAmount;
			parameters[6].Value = pEntity.IsBuyUpgrade;
			parameters[7].Value = pEntity.BuyAmount;
			parameters[8].Value = pEntity.OnceBuyAmount;
			parameters[9].Value = pEntity.IsPointUpgrade;
			parameters[10].Value = pEntity.TotalPoint;
			parameters[11].Value = pEntity.IsMustDeductPoint;
			parameters[12].Value = pEntity.RefId;
			parameters[13].Value = pEntity.CustomerID;
			parameters[14].Value = pEntity.CreateTime;
			parameters[15].Value = pEntity.CreateBy;
			parameters[16].Value = pEntity.LastUpdateTime;
			parameters[17].Value = pEntity.LastUpdateBy;
			parameters[18].Value = pEntity.IsDelete;
			parameters[19].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.VipCardUpgradeRuleId = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public VipCardUpgradeRuleEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCardUpgradeRule] where VipCardUpgradeRuleId='{0}'  and isdelete=0 ", id.ToString());
            //��ȡ����
            VipCardUpgradeRuleEntity m = null;
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
        public VipCardUpgradeRuleEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCardUpgradeRule] where 1=1  and isdelete=0");
            //��ȡ����
            List<VipCardUpgradeRuleEntity> list = new List<VipCardUpgradeRuleEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardUpgradeRuleEntity m;
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
        public void Update(VipCardUpgradeRuleEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(VipCardUpgradeRuleEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.VipCardUpgradeRuleId.HasValue)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [VipCardUpgradeRule] set ");
                        if (pIsUpdateNullField || pEntity.VipCardTypeID!=null)
                strSql.Append( "[VipCardTypeID]=@VipCardTypeID,");
            if (pIsUpdateNullField || pEntity.IsFormVerify!=null)
                strSql.Append( "[IsFormVerify]=@IsFormVerify,");
            if (pIsUpdateNullField || pEntity.IsPurchaseUpgrade!=null)
                strSql.Append( "[IsPurchaseUpgrade]=@IsPurchaseUpgrade,");
            if (pIsUpdateNullField || pEntity.IsExchangeIntegral!=null)
                strSql.Append( "[IsExchangeIntegral]=@IsExchangeIntegral,");
            if (pIsUpdateNullField || pEntity.IsRecharge!=null)
                strSql.Append( "[IsRecharge]=@IsRecharge,");
            if (pIsUpdateNullField || pEntity.OnceRechargeAmount!=null)
                strSql.Append( "[OnceRechargeAmount]=@OnceRechargeAmount,");
            if (pIsUpdateNullField || pEntity.IsBuyUpgrade!=null)
                strSql.Append( "[IsBuyUpgrade]=@IsBuyUpgrade,");
            if (pIsUpdateNullField || pEntity.BuyAmount!=null)
                strSql.Append( "[BuyAmount]=@BuyAmount,");
            if (pIsUpdateNullField || pEntity.OnceBuyAmount!=null)
                strSql.Append( "[OnceBuyAmount]=@OnceBuyAmount,");
            if (pIsUpdateNullField || pEntity.IsPointUpgrade!=null)
                strSql.Append( "[IsPointUpgrade]=@IsPointUpgrade,");
            if (pIsUpdateNullField || pEntity.TotalPoint!=null)
                strSql.Append( "[TotalPoint]=@TotalPoint,");
            if (pIsUpdateNullField || pEntity.IsMustDeductPoint!=null)
                strSql.Append( "[IsMustDeductPoint]=@IsMustDeductPoint,");
            if (pIsUpdateNullField || pEntity.RefId!=null)
                strSql.Append( "[RefId]=@RefId,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where VipCardUpgradeRuleId=@VipCardUpgradeRuleId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@VipCardTypeID",SqlDbType.Int),
					new SqlParameter("@IsFormVerify",SqlDbType.Int),
					new SqlParameter("@IsPurchaseUpgrade",SqlDbType.Int),
					new SqlParameter("@IsExchangeIntegral",SqlDbType.Int),
					new SqlParameter("@IsRecharge",SqlDbType.Int),
					new SqlParameter("@OnceRechargeAmount",SqlDbType.Decimal),
					new SqlParameter("@IsBuyUpgrade",SqlDbType.Int),
					new SqlParameter("@BuyAmount",SqlDbType.Decimal),
					new SqlParameter("@OnceBuyAmount",SqlDbType.Decimal),
					new SqlParameter("@IsPointUpgrade",SqlDbType.Int),
					new SqlParameter("@TotalPoint",SqlDbType.Decimal),
					new SqlParameter("@IsMustDeductPoint",SqlDbType.Int),
					new SqlParameter("@RefId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@VipCardUpgradeRuleId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.VipCardTypeID;
			parameters[1].Value = pEntity.IsFormVerify;
			parameters[2].Value = pEntity.IsPurchaseUpgrade;
			parameters[3].Value = pEntity.IsExchangeIntegral;
			parameters[4].Value = pEntity.IsRecharge;
			parameters[5].Value = pEntity.OnceRechargeAmount;
			parameters[6].Value = pEntity.IsBuyUpgrade;
			parameters[7].Value = pEntity.BuyAmount;
			parameters[8].Value = pEntity.OnceBuyAmount;
			parameters[9].Value = pEntity.IsPointUpgrade;
			parameters[10].Value = pEntity.TotalPoint;
			parameters[11].Value = pEntity.IsMustDeductPoint;
			parameters[12].Value = pEntity.RefId;
			parameters[13].Value = pEntity.CustomerID;
			parameters[14].Value = pEntity.LastUpdateTime;
			parameters[15].Value = pEntity.LastUpdateBy;
			parameters[16].Value = pEntity.VipCardUpgradeRuleId;

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
        public void Update(VipCardUpgradeRuleEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(VipCardUpgradeRuleEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(VipCardUpgradeRuleEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.VipCardUpgradeRuleId.HasValue)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.VipCardUpgradeRuleId.Value, pTran);           
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
            sql.AppendLine("update [VipCardUpgradeRule] set  isdelete=1 where VipCardUpgradeRuleId=@VipCardUpgradeRuleId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@VipCardUpgradeRuleId",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(VipCardUpgradeRuleEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.VipCardUpgradeRuleId.HasValue)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.VipCardUpgradeRuleId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(VipCardUpgradeRuleEntity[] pEntities)
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
            sql.AppendLine("update [VipCardUpgradeRule] set  isdelete=1 where VipCardUpgradeRuleId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public VipCardUpgradeRuleEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCardUpgradeRule] where 1=1  and isdelete=0 ");
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
            List<VipCardUpgradeRuleEntity> list = new List<VipCardUpgradeRuleEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardUpgradeRuleEntity m;
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
        public PagedQueryResult<VipCardUpgradeRuleEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [VipCardUpgradeRuleId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [VipCardUpgradeRule] where 1=1  and isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [VipCardUpgradeRule] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<VipCardUpgradeRuleEntity> result = new PagedQueryResult<VipCardUpgradeRuleEntity>();
            List<VipCardUpgradeRuleEntity> list = new List<VipCardUpgradeRuleEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardUpgradeRuleEntity m;
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
        public VipCardUpgradeRuleEntity[] QueryByEntity(VipCardUpgradeRuleEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<VipCardUpgradeRuleEntity> PagedQueryByEntity(VipCardUpgradeRuleEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(VipCardUpgradeRuleEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.VipCardUpgradeRuleId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardUpgradeRuleId", Value = pQueryEntity.VipCardUpgradeRuleId });
            if (pQueryEntity.VipCardTypeID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardTypeID", Value = pQueryEntity.VipCardTypeID });
            if (pQueryEntity.IsFormVerify!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsFormVerify", Value = pQueryEntity.IsFormVerify });
            if (pQueryEntity.IsPurchaseUpgrade!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsPurchaseUpgrade", Value = pQueryEntity.IsPurchaseUpgrade });
            if (pQueryEntity.IsExchangeIntegral!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsExchangeIntegral", Value = pQueryEntity.IsExchangeIntegral });
            if (pQueryEntity.IsRecharge!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsRecharge", Value = pQueryEntity.IsRecharge });
            if (pQueryEntity.OnceRechargeAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OnceRechargeAmount", Value = pQueryEntity.OnceRechargeAmount });
            if (pQueryEntity.IsBuyUpgrade!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsBuyUpgrade", Value = pQueryEntity.IsBuyUpgrade });
            if (pQueryEntity.BuyAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BuyAmount", Value = pQueryEntity.BuyAmount });
            if (pQueryEntity.OnceBuyAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OnceBuyAmount", Value = pQueryEntity.OnceBuyAmount });
            if (pQueryEntity.IsPointUpgrade!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsPointUpgrade", Value = pQueryEntity.IsPointUpgrade });
            if (pQueryEntity.TotalPoint!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TotalPoint", Value = pQueryEntity.TotalPoint });
            if (pQueryEntity.IsMustDeductPoint!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsMustDeductPoint", Value = pQueryEntity.IsMustDeductPoint });
            if (pQueryEntity.RefId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RefId", Value = pQueryEntity.RefId });
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
        protected void Load(IDataReader pReader, out VipCardUpgradeRuleEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new VipCardUpgradeRuleEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["VipCardUpgradeRuleId"] != DBNull.Value)
			{
				pInstance.VipCardUpgradeRuleId =  (Guid)pReader["VipCardUpgradeRuleId"];
			}
			if (pReader["VipCardTypeID"] != DBNull.Value)
			{
				pInstance.VipCardTypeID =   Convert.ToInt32(pReader["VipCardTypeID"]);
			}
			if (pReader["IsFormVerify"] != DBNull.Value)
			{
				pInstance.IsFormVerify =   Convert.ToInt32(pReader["IsFormVerify"]);
			}
			if (pReader["IsPurchaseUpgrade"] != DBNull.Value)
			{
				pInstance.IsPurchaseUpgrade =   Convert.ToInt32(pReader["IsPurchaseUpgrade"]);
			}
			if (pReader["IsExchangeIntegral"] != DBNull.Value)
			{
				pInstance.IsExchangeIntegral =   Convert.ToInt32(pReader["IsExchangeIntegral"]);
			}
			if (pReader["IsRecharge"] != DBNull.Value)
			{
				pInstance.IsRecharge =   Convert.ToInt32(pReader["IsRecharge"]);
			}
			if (pReader["OnceRechargeAmount"] != DBNull.Value)
			{
				pInstance.OnceRechargeAmount =  Convert.ToDecimal(pReader["OnceRechargeAmount"]);
			}
			if (pReader["IsBuyUpgrade"] != DBNull.Value)
			{
				pInstance.IsBuyUpgrade =   Convert.ToInt32(pReader["IsBuyUpgrade"]);
			}
			if (pReader["BuyAmount"] != DBNull.Value)
			{
				pInstance.BuyAmount =  Convert.ToDecimal(pReader["BuyAmount"]);
			}
			if (pReader["OnceBuyAmount"] != DBNull.Value)
			{
				pInstance.OnceBuyAmount =  Convert.ToDecimal(pReader["OnceBuyAmount"]);
			}
			if (pReader["IsPointUpgrade"] != DBNull.Value)
			{
				pInstance.IsPointUpgrade =   Convert.ToInt32(pReader["IsPointUpgrade"]);
			}
			if (pReader["TotalPoint"] != DBNull.Value)
			{
				pInstance.TotalPoint =  Convert.ToDecimal(pReader["TotalPoint"]);
			}
			if (pReader["IsMustDeductPoint"] != DBNull.Value)
			{
				pInstance.IsMustDeductPoint =   Convert.ToInt32(pReader["IsMustDeductPoint"]);
			}
			if (pReader["RefId"] != DBNull.Value)
			{
				pInstance.RefId =  (Guid)pReader["RefId"];
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
