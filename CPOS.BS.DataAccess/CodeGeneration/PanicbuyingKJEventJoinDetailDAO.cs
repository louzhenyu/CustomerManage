/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/2/16 14:37:47
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
    /// ��PanicbuyingKJEventJoinDetail�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class PanicbuyingKJEventJoinDetailDAO : Base.BaseCPOSDAO, ICRUDable<PanicbuyingKJEventJoinDetailEntity>, IQueryable<PanicbuyingKJEventJoinDetailEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public PanicbuyingKJEventJoinDetailDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(PanicbuyingKJEventJoinDetailEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(PanicbuyingKJEventJoinDetailEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [PanicbuyingKJEventJoinDetail](");
            strSql.Append("[EventId],[ItemId],[SkuId],[VipId],[WXName],[OpenId],[Price],[BasePrice],[BargainPrice],[MomentSalesPrice],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[CustomerId],[KJEventJoinId],[KJEventJoinDetailId])");
            strSql.Append(" values (");
            strSql.Append("@EventId,@ItemId,@SkuId,@VipId,@WXName,@OpenId,@Price,@BasePrice,@BargainPrice,@MomentSalesPrice,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@CustomerId,@KJEventJoinId,@KJEventJoinDetailId)");            

			Guid? pkGuid;
			if (pEntity.KJEventJoinDetailId == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.KJEventJoinDetailId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@EventId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@ItemId",SqlDbType.NVarChar),
					new SqlParameter("@SkuId",SqlDbType.NVarChar),
					new SqlParameter("@VipId",SqlDbType.NVarChar),
					new SqlParameter("@WXName",SqlDbType.NVarChar),
					new SqlParameter("@OpenId",SqlDbType.VarChar),
					new SqlParameter("@Price",SqlDbType.Decimal),
					new SqlParameter("@BasePrice",SqlDbType.Decimal),
					new SqlParameter("@BargainPrice",SqlDbType.Decimal),
					new SqlParameter("@MomentSalesPrice",SqlDbType.Decimal),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@KJEventJoinId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@KJEventJoinDetailId",SqlDbType.UniqueIdentifier)
            };
			parameters[-1].Value = pEntity.EventId;
			parameters[0].Value = pEntity.ItemId;
			parameters[1].Value = pEntity.SkuId;
			parameters[2].Value = pEntity.VipId;
			parameters[3].Value = pEntity.WXName;
			parameters[4].Value = pEntity.OpenId;
			parameters[5].Value = pEntity.Price;
			parameters[6].Value = pEntity.BasePrice;
			parameters[7].Value = pEntity.BargainPrice;
			parameters[8].Value = pEntity.MomentSalesPrice;
			parameters[9].Value = pEntity.CreateTime;
			parameters[10].Value = pEntity.CreateBy;
			parameters[11].Value = pEntity.LastUpdateBy;
			parameters[12].Value = pEntity.LastUpdateTime;
			parameters[13].Value = pEntity.IsDelete;
			parameters[14].Value = pEntity.CustomerId;
			parameters[15].Value = pEntity.KJEventJoinId;
			parameters[17].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.KJEventJoinDetailId = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public PanicbuyingKJEventJoinDetailEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [PanicbuyingKJEventJoinDetail] where KJEventJoinDetailId='{0}'  and isdelete=0 ", id.ToString());
            //��ȡ����
            PanicbuyingKJEventJoinDetailEntity m = null;
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
        public PanicbuyingKJEventJoinDetailEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [PanicbuyingKJEventJoinDetail] where 1=1  and isdelete=0");
            //��ȡ����
            List<PanicbuyingKJEventJoinDetailEntity> list = new List<PanicbuyingKJEventJoinDetailEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    PanicbuyingKJEventJoinDetailEntity m;
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
        public void Update(PanicbuyingKJEventJoinDetailEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(PanicbuyingKJEventJoinDetailEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.KJEventJoinDetailId.HasValue)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [PanicbuyingKJEventJoinDetail] set ");
                        if (pIsUpdateNullField || pEntity.EventId!=null)
                strSql.Append( "[EventId]=@EventId,");
            if (pIsUpdateNullField || pEntity.ItemId!=null)
                strSql.Append( "[ItemId]=@ItemId,");
            if (pIsUpdateNullField || pEntity.SkuId!=null)
                strSql.Append( "[SkuId]=@SkuId,");
            if (pIsUpdateNullField || pEntity.VipId!=null)
                strSql.Append( "[VipId]=@VipId,");
            if (pIsUpdateNullField || pEntity.WXName!=null)
                strSql.Append( "[WXName]=@WXName,");
            if (pIsUpdateNullField || pEntity.OpenId!=null)
                strSql.Append( "[OpenId]=@OpenId,");
            if (pIsUpdateNullField || pEntity.Price!=null)
                strSql.Append( "[Price]=@Price,");
            if (pIsUpdateNullField || pEntity.BasePrice!=null)
                strSql.Append( "[BasePrice]=@BasePrice,");
            if (pIsUpdateNullField || pEntity.BargainPrice!=null)
                strSql.Append( "[BargainPrice]=@BargainPrice,");
            if (pIsUpdateNullField || pEntity.MomentSalesPrice!=null)
                strSql.Append( "[MomentSalesPrice]=@MomentSalesPrice,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.KJEventJoinId!=null)
                strSql.Append( "[KJEventJoinId]=@KJEventJoinId");
            strSql.Append(" where KJEventJoinDetailId=@KJEventJoinDetailId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@EventId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@ItemId",SqlDbType.NVarChar),
					new SqlParameter("@SkuId",SqlDbType.NVarChar),
					new SqlParameter("@VipId",SqlDbType.NVarChar),
					new SqlParameter("@WXName",SqlDbType.NVarChar),
					new SqlParameter("@OpenId",SqlDbType.VarChar),
					new SqlParameter("@Price",SqlDbType.Decimal),
					new SqlParameter("@BasePrice",SqlDbType.Decimal),
					new SqlParameter("@BargainPrice",SqlDbType.Decimal),
					new SqlParameter("@MomentSalesPrice",SqlDbType.Decimal),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@KJEventJoinId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@KJEventJoinDetailId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.EventId;
			parameters[1].Value = pEntity.ItemId;
			parameters[2].Value = pEntity.SkuId;
			parameters[3].Value = pEntity.VipId;
			parameters[4].Value = pEntity.WXName;
			parameters[5].Value = pEntity.OpenId;
			parameters[6].Value = pEntity.Price;
			parameters[7].Value = pEntity.BasePrice;
			parameters[8].Value = pEntity.BargainPrice;
			parameters[9].Value = pEntity.MomentSalesPrice;
			parameters[10].Value = pEntity.LastUpdateBy;
			parameters[11].Value = pEntity.LastUpdateTime;
			parameters[12].Value = pEntity.CustomerId;
			parameters[13].Value = pEntity.KJEventJoinId;
			parameters[14].Value = pEntity.KJEventJoinDetailId;

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
        public void Update(PanicbuyingKJEventJoinDetailEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(PanicbuyingKJEventJoinDetailEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(PanicbuyingKJEventJoinDetailEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.KJEventJoinDetailId.HasValue)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.KJEventJoinDetailId.Value, pTran);           
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
            sql.AppendLine("update [PanicbuyingKJEventJoinDetail] set  isdelete=1 where KJEventJoinDetailId=@KJEventJoinDetailId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@KJEventJoinDetailId",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(PanicbuyingKJEventJoinDetailEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.KJEventJoinDetailId.HasValue)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.KJEventJoinDetailId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(PanicbuyingKJEventJoinDetailEntity[] pEntities)
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
            sql.AppendLine("update [PanicbuyingKJEventJoinDetail] set  isdelete=1 where KJEventJoinDetailId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public PanicbuyingKJEventJoinDetailEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [PanicbuyingKJEventJoinDetail] where 1=1  and isdelete=0 ");
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
            List<PanicbuyingKJEventJoinDetailEntity> list = new List<PanicbuyingKJEventJoinDetailEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    PanicbuyingKJEventJoinDetailEntity m;
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
        public PagedQueryResult<PanicbuyingKJEventJoinDetailEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [KJEventJoinDetailId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [PanicbuyingKJEventJoinDetail] where 1=1  and isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [PanicbuyingKJEventJoinDetail] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<PanicbuyingKJEventJoinDetailEntity> result = new PagedQueryResult<PanicbuyingKJEventJoinDetailEntity>();
            List<PanicbuyingKJEventJoinDetailEntity> list = new List<PanicbuyingKJEventJoinDetailEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    PanicbuyingKJEventJoinDetailEntity m;
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
        public PanicbuyingKJEventJoinDetailEntity[] QueryByEntity(PanicbuyingKJEventJoinDetailEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<PanicbuyingKJEventJoinDetailEntity> PagedQueryByEntity(PanicbuyingKJEventJoinDetailEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(PanicbuyingKJEventJoinDetailEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.EventId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventId", Value = pQueryEntity.EventId });
            if (pQueryEntity.ItemId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemId", Value = pQueryEntity.ItemId });
            if (pQueryEntity.SkuId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SkuId", Value = pQueryEntity.SkuId });
            if (pQueryEntity.VipId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipId", Value = pQueryEntity.VipId });
            if (pQueryEntity.WXName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WXName", Value = pQueryEntity.WXName });
            if (pQueryEntity.OpenId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OpenId", Value = pQueryEntity.OpenId });
            if (pQueryEntity.Price!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Price", Value = pQueryEntity.Price });
            if (pQueryEntity.BasePrice!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BasePrice", Value = pQueryEntity.BasePrice });
            if (pQueryEntity.BargainPrice!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BargainPrice", Value = pQueryEntity.BargainPrice });
            if (pQueryEntity.MomentSalesPrice!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MomentSalesPrice", Value = pQueryEntity.MomentSalesPrice });
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
            if (pQueryEntity.KJEventJoinId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "KJEventJoinId", Value = pQueryEntity.KJEventJoinId });
            if (pQueryEntity.KJEventJoinDetailId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "KJEventJoinDetailId", Value = pQueryEntity.KJEventJoinDetailId });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(IDataReader pReader, out PanicbuyingKJEventJoinDetailEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new PanicbuyingKJEventJoinDetailEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["EventId"] != DBNull.Value)
			{
				pInstance.EventId =  (Guid)pReader["EventId"];
			}
			if (pReader["ItemId"] != DBNull.Value)
			{
				pInstance.ItemId =  Convert.ToString(pReader["ItemId"]);
			}
			if (pReader["SkuId"] != DBNull.Value)
			{
				pInstance.SkuId =  Convert.ToString(pReader["SkuId"]);
			}
			if (pReader["VipId"] != DBNull.Value)
			{
				pInstance.VipId =  Convert.ToString(pReader["VipId"]);
			}
			if (pReader["WXName"] != DBNull.Value)
			{
				pInstance.WXName =  Convert.ToString(pReader["WXName"]);
			}
			if (pReader["OpenId"] != DBNull.Value)
			{
				pInstance.OpenId =  Convert.ToString(pReader["OpenId"]);
			}
			if (pReader["Price"] != DBNull.Value)
			{
				pInstance.Price =  Convert.ToDecimal(pReader["Price"]);
			}
			if (pReader["BasePrice"] != DBNull.Value)
			{
				pInstance.BasePrice =  Convert.ToDecimal(pReader["BasePrice"]);
			}
			if (pReader["BargainPrice"] != DBNull.Value)
			{
				pInstance.BargainPrice =  Convert.ToDecimal(pReader["BargainPrice"]);
			}
			if (pReader["MomentSalesPrice"] != DBNull.Value)
			{
				pInstance.MomentSalesPrice =  Convert.ToDecimal(pReader["MomentSalesPrice"]);
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
			if (pReader["KJEventJoinId"] != DBNull.Value)
			{
				pInstance.KJEventJoinId =  (Guid)pReader["KJEventJoinId"];
			}
			if (pReader["KJEventJoinDetailId"] != DBNull.Value)
			{
				pInstance.KJEventJoinDetailId =  (Guid)pReader["KJEventJoinDetailId"];
			}

        }
        #endregion
    }
}
