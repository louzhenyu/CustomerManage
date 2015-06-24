/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/6/1 16:12:03
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
    /// ���ݷ��ʣ� ��Ϊ�����˫���� 
    /// ��SysRetailRewardRule�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class SysRetailRewardRuleDAO : Base.BaseCPOSDAO, ICRUDable<SysRetailRewardRuleEntity>, IQueryable<SysRetailRewardRuleEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public SysRetailRewardRuleDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(SysRetailRewardRuleEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(SysRetailRewardRuleEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [SysRetailRewardRule](");
            strSql.Append("[CooperateType],[RewardTypeName],[RewardTypeCode],[SellUserReward],[IsTemplate],[RetailTraderReward],[CreateTime],[AmountOrPercent],[CreateBy],[LastUpdateBy],[LastUpdateTime],[Status],[IsDelete],[CustomerId],[BeginTime],[EndTime],[RetailTraderID],[RetailRewardRuleID])");
            strSql.Append(" values (");
            strSql.Append("@CooperateType,@RewardTypeName,@RewardTypeCode,@SellUserReward,@IsTemplate,@RetailTraderReward,@CreateTime,@AmountOrPercent,@CreateBy,@LastUpdateBy,@LastUpdateTime,@Status,@IsDelete,@CustomerId,@BeginTime,@EndTime,@RetailTraderID,@RetailRewardRuleID)");            

			string pkString = pEntity.RetailRewardRuleID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@CooperateType",SqlDbType.NVarChar),
					new SqlParameter("@RewardTypeName",SqlDbType.NVarChar),
					new SqlParameter("@RewardTypeCode",SqlDbType.NVarChar),
					new SqlParameter("@SellUserReward",SqlDbType.Decimal),
					new SqlParameter("@IsTemplate",SqlDbType.Int),
					new SqlParameter("@RetailTraderReward",SqlDbType.Decimal),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@AmountOrPercent",SqlDbType.Int),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@Status",SqlDbType.NVarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@BeginTime",SqlDbType.DateTime),
					new SqlParameter("@EndTime",SqlDbType.DateTime),
					new SqlParameter("@RetailTraderID",SqlDbType.NVarChar),
					new SqlParameter("@RetailRewardRuleID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.CooperateType;
			parameters[1].Value = pEntity.RewardTypeName;
			parameters[2].Value = pEntity.RewardTypeCode;
			parameters[3].Value = pEntity.SellUserReward;
			parameters[4].Value = pEntity.IsTemplate;
			parameters[5].Value = pEntity.RetailTraderReward;
			parameters[6].Value = pEntity.CreateTime;
			parameters[7].Value = pEntity.AmountOrPercent;
			parameters[8].Value = pEntity.CreateBy;
			parameters[9].Value = pEntity.LastUpdateBy;
			parameters[10].Value = pEntity.LastUpdateTime;
			parameters[11].Value = pEntity.Status;
			parameters[12].Value = pEntity.IsDelete;
			parameters[13].Value = pEntity.CustomerId;
			parameters[14].Value = pEntity.BeginTime;
			parameters[15].Value = pEntity.EndTime;
			parameters[16].Value = pEntity.RetailTraderID;
			parameters[17].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.RetailRewardRuleID = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public SysRetailRewardRuleEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [SysRetailRewardRule] where RetailRewardRuleID='{0}'  and isdelete=0 ", id.ToString());
            //��ȡ����
            SysRetailRewardRuleEntity m = null;
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
        public SysRetailRewardRuleEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [SysRetailRewardRule] where 1=1  and isdelete=0");
            //��ȡ����
            List<SysRetailRewardRuleEntity> list = new List<SysRetailRewardRuleEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    SysRetailRewardRuleEntity m;
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
        public void Update(SysRetailRewardRuleEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(SysRetailRewardRuleEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.RetailRewardRuleID == null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [SysRetailRewardRule] set ");
                        if (pIsUpdateNullField || pEntity.CooperateType!=null)
                strSql.Append( "[CooperateType]=@CooperateType,");
            if (pIsUpdateNullField || pEntity.RewardTypeName!=null)
                strSql.Append( "[RewardTypeName]=@RewardTypeName,");
            if (pIsUpdateNullField || pEntity.RewardTypeCode!=null)
                strSql.Append( "[RewardTypeCode]=@RewardTypeCode,");
            if (pIsUpdateNullField || pEntity.SellUserReward!=null)
                strSql.Append( "[SellUserReward]=@SellUserReward,");
            if (pIsUpdateNullField || pEntity.IsTemplate!=null)
                strSql.Append( "[IsTemplate]=@IsTemplate,");
            if (pIsUpdateNullField || pEntity.RetailTraderReward!=null)
                strSql.Append( "[RetailTraderReward]=@RetailTraderReward,");
            if (pIsUpdateNullField || pEntity.AmountOrPercent!=null)
                strSql.Append( "[AmountOrPercent]=@AmountOrPercent,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.Status!=null)
                strSql.Append( "[Status]=@Status,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.BeginTime!=null)
                strSql.Append( "[BeginTime]=@BeginTime,");
            if (pIsUpdateNullField || pEntity.EndTime!=null)
                strSql.Append( "[EndTime]=@EndTime,");
            if (pIsUpdateNullField || pEntity.RetailTraderID!=null)
                strSql.Append( "[RetailTraderID]=@RetailTraderID");
            strSql.Append(" where RetailRewardRuleID=@RetailRewardRuleID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@CooperateType",SqlDbType.NVarChar),
					new SqlParameter("@RewardTypeName",SqlDbType.NVarChar),
					new SqlParameter("@RewardTypeCode",SqlDbType.NVarChar),
					new SqlParameter("@SellUserReward",SqlDbType.Decimal),
					new SqlParameter("@IsTemplate",SqlDbType.Int),
					new SqlParameter("@RetailTraderReward",SqlDbType.Decimal),
					new SqlParameter("@AmountOrPercent",SqlDbType.Int),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@Status",SqlDbType.NVarChar),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@BeginTime",SqlDbType.DateTime),
					new SqlParameter("@EndTime",SqlDbType.DateTime),
					new SqlParameter("@RetailTraderID",SqlDbType.NVarChar),
					new SqlParameter("@RetailRewardRuleID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.CooperateType;
			parameters[1].Value = pEntity.RewardTypeName;
			parameters[2].Value = pEntity.RewardTypeCode;
			parameters[3].Value = pEntity.SellUserReward;
			parameters[4].Value = pEntity.IsTemplate;
			parameters[5].Value = pEntity.RetailTraderReward;
			parameters[6].Value = pEntity.AmountOrPercent;
			parameters[7].Value = pEntity.LastUpdateBy;
			parameters[8].Value = pEntity.LastUpdateTime;
			parameters[9].Value = pEntity.Status;
			parameters[10].Value = pEntity.CustomerId;
			parameters[11].Value = pEntity.BeginTime;
			parameters[12].Value = pEntity.EndTime;
			parameters[13].Value = pEntity.RetailTraderID;
			parameters[14].Value = pEntity.RetailRewardRuleID;

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
        public void Update(SysRetailRewardRuleEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(SysRetailRewardRuleEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(SysRetailRewardRuleEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.RetailRewardRuleID == null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.RetailRewardRuleID, pTran);           
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
            sql.AppendLine("update [SysRetailRewardRule] set  isdelete=1 where RetailRewardRuleID=@RetailRewardRuleID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@RetailRewardRuleID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(SysRetailRewardRuleEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (pEntity.RetailRewardRuleID == null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.RetailRewardRuleID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(SysRetailRewardRuleEntity[] pEntities)
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
            sql.AppendLine("update [SysRetailRewardRule] set  isdelete=1 where RetailRewardRuleID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public SysRetailRewardRuleEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [SysRetailRewardRule] where 1=1  and isdelete=0 ");
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
            List<SysRetailRewardRuleEntity> list = new List<SysRetailRewardRuleEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    SysRetailRewardRuleEntity m;
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
        public PagedQueryResult<SysRetailRewardRuleEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [RetailRewardRuleID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [SysRetailRewardRule] where 1=1  and isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [SysRetailRewardRule] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<SysRetailRewardRuleEntity> result = new PagedQueryResult<SysRetailRewardRuleEntity>();
            List<SysRetailRewardRuleEntity> list = new List<SysRetailRewardRuleEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    SysRetailRewardRuleEntity m;
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
        public SysRetailRewardRuleEntity[] QueryByEntity(SysRetailRewardRuleEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<SysRetailRewardRuleEntity> PagedQueryByEntity(SysRetailRewardRuleEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(SysRetailRewardRuleEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.RetailRewardRuleID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RetailRewardRuleID", Value = pQueryEntity.RetailRewardRuleID });
            if (pQueryEntity.CooperateType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CooperateType", Value = pQueryEntity.CooperateType });
            if (pQueryEntity.RewardTypeName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RewardTypeName", Value = pQueryEntity.RewardTypeName });
            if (pQueryEntity.RewardTypeCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RewardTypeCode", Value = pQueryEntity.RewardTypeCode });
            if (pQueryEntity.SellUserReward!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SellUserReward", Value = pQueryEntity.SellUserReward });
            if (pQueryEntity.IsTemplate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsTemplate", Value = pQueryEntity.IsTemplate });
            if (pQueryEntity.RetailTraderReward!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RetailTraderReward", Value = pQueryEntity.RetailTraderReward });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.AmountOrPercent!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AmountOrPercent", Value = pQueryEntity.AmountOrPercent });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.LastUpdateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.LastUpdateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.Status!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Status", Value = pQueryEntity.Status });
            if (pQueryEntity.IsDelete!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
            if (pQueryEntity.BeginTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BeginTime", Value = pQueryEntity.BeginTime });
            if (pQueryEntity.EndTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EndTime", Value = pQueryEntity.EndTime });
            if (pQueryEntity.RetailTraderID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RetailTraderID", Value = pQueryEntity.RetailTraderID });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(IDataReader pReader, out SysRetailRewardRuleEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new SysRetailRewardRuleEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["RetailRewardRuleID"] != DBNull.Value)
			{
				pInstance.RetailRewardRuleID =  Convert.ToString(pReader["RetailRewardRuleID"]);
			}
			if (pReader["CooperateType"] != DBNull.Value)
			{
				pInstance.CooperateType =  Convert.ToString(pReader["CooperateType"]);
			}
			if (pReader["RewardTypeName"] != DBNull.Value)
			{
				pInstance.RewardTypeName =  Convert.ToString(pReader["RewardTypeName"]);
			}
			if (pReader["RewardTypeCode"] != DBNull.Value)
			{
				pInstance.RewardTypeCode =  Convert.ToString(pReader["RewardTypeCode"]);
			}
			if (pReader["SellUserReward"] != DBNull.Value)
			{
				pInstance.SellUserReward =  Convert.ToDecimal(pReader["SellUserReward"]);
			}
			if (pReader["IsTemplate"] != DBNull.Value)
			{
				pInstance.IsTemplate =   Convert.ToInt32(pReader["IsTemplate"]);
			}
			if (pReader["RetailTraderReward"] != DBNull.Value)
			{
				pInstance.RetailTraderReward =  Convert.ToDecimal(pReader["RetailTraderReward"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}
			if (pReader["AmountOrPercent"] != DBNull.Value)
			{
				pInstance.AmountOrPercent =   Convert.ToInt32(pReader["AmountOrPercent"]);
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
			if (pReader["Status"] != DBNull.Value)
			{
				pInstance.Status =  Convert.ToString(pReader["Status"]);
			}
			if (pReader["IsDelete"] != DBNull.Value)
			{
				pInstance.IsDelete =   Convert.ToInt32(pReader["IsDelete"]);
			}
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}
			if (pReader["BeginTime"] != DBNull.Value)
			{
				pInstance.BeginTime =  Convert.ToDateTime(pReader["BeginTime"]);
			}
			if (pReader["EndTime"] != DBNull.Value)
			{
				pInstance.EndTime =  Convert.ToDateTime(pReader["EndTime"]);
			}
			if (pReader["RetailTraderID"] != DBNull.Value)
			{
				pInstance.RetailTraderID =   Convert.ToString(pReader["RetailTraderID"]);
			}

        }
        #endregion
    }
}
