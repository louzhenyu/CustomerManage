/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/18 14:40:25
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
    /// ��R_VipGoldHome�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class R_VipGoldHomeDAO : Base.BaseCPOSDAO, ICRUDable<R_VipGoldHomeEntity>, IQueryable<R_VipGoldHomeEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public R_VipGoldHomeDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(R_VipGoldHomeEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(R_VipGoldHomeEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //��ʼ���̶��ֶ�
			pEntity.CreateTime=DateTime.Now;


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [R_VipGoldHome](");
            strSql.Append("[DateCode],[CustomerId],[OnlineOnlyFansCount],[OnlineVipCount],[OnlineFansCount],[OfflineVipCount],[VipCount],[OnlineVipCountFor30DayOrder],[OnlineVipCountForLast30DayOrder],[OnlineVipCountFor30DayOrderM2M],[OnlineVipCountPerFor30DayOrder],[OnlineVipCountPerForLast30DayOrder],[OnlineVipCountPerFor30DayOrderM2M],[OnlineVipSalesFor30Day],[OnlineVipSalesForLast30Day],[OnlineVipSalesFor30DayM2M],[VipSalesFor30Day],[OnlineVipSalesPerFor30Day],[VipSalesForLast30Day],[OnlineVipSalesPerForLast30Day],[OnlineVipSalesPerFor30DayM2M],[SalesFor30Day],[SalesForLast30Day],[CreateTime],[ID])");
            strSql.Append(" values (");
            strSql.Append("@DateCode,@CustomerId,@OnlineOnlyFansCount,@OnlineVipCount,@OnlineFansCount,@OfflineVipCount,@VipCount,@OnlineVipCountFor30DayOrder,@OnlineVipCountForLast30DayOrder,@OnlineVipCountFor30DayOrderM2M,@OnlineVipCountPerFor30DayOrder,@OnlineVipCountPerForLast30DayOrder,@OnlineVipCountPerFor30DayOrderM2M,@OnlineVipSalesFor30Day,@OnlineVipSalesForLast30Day,@OnlineVipSalesFor30DayM2M,@VipSalesFor30Day,@OnlineVipSalesPerFor30Day,@VipSalesForLast30Day,@OnlineVipSalesPerForLast30Day,@OnlineVipSalesPerFor30DayM2M,@SalesFor30Day,@SalesForLast30Day,@CreateTime,@ID)");            

			Guid? pkGuid;
			if (pEntity.ID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.ID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@DateCode",SqlDbType.Date),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@OnlineOnlyFansCount",SqlDbType.Int),
					new SqlParameter("@OnlineVipCount",SqlDbType.Int),
					new SqlParameter("@OnlineFansCount",SqlDbType.Int),
					new SqlParameter("@OfflineVipCount",SqlDbType.Int),
					new SqlParameter("@VipCount",SqlDbType.Int),
					new SqlParameter("@OnlineVipCountFor30DayOrder",SqlDbType.Int),
					new SqlParameter("@OnlineVipCountForLast30DayOrder",SqlDbType.Int),
					new SqlParameter("@OnlineVipCountFor30DayOrderM2M",SqlDbType.Decimal),
					new SqlParameter("@OnlineVipCountPerFor30DayOrder",SqlDbType.Decimal),
					new SqlParameter("@OnlineVipCountPerForLast30DayOrder",SqlDbType.Decimal),
					new SqlParameter("@OnlineVipCountPerFor30DayOrderM2M",SqlDbType.Decimal),
					new SqlParameter("@OnlineVipSalesFor30Day",SqlDbType.Decimal),
					new SqlParameter("@OnlineVipSalesForLast30Day",SqlDbType.Decimal),
					new SqlParameter("@OnlineVipSalesFor30DayM2M",SqlDbType.Decimal),
					new SqlParameter("@VipSalesFor30Day",SqlDbType.Decimal),
					new SqlParameter("@OnlineVipSalesPerFor30Day",SqlDbType.Decimal),
					new SqlParameter("@VipSalesForLast30Day",SqlDbType.Decimal),
					new SqlParameter("@OnlineVipSalesPerForLast30Day",SqlDbType.Decimal),
					new SqlParameter("@OnlineVipSalesPerFor30DayM2M",SqlDbType.Decimal),
					new SqlParameter("@SalesFor30Day",SqlDbType.Decimal),
					new SqlParameter("@SalesForLast30Day",SqlDbType.Decimal),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@ID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.DateCode;
			parameters[1].Value = pEntity.CustomerId;
			parameters[2].Value = pEntity.OnlineOnlyFansCount;
			parameters[3].Value = pEntity.OnlineVipCount;
			parameters[4].Value = pEntity.OnlineFansCount;
			parameters[5].Value = pEntity.OfflineVipCount;
			parameters[6].Value = pEntity.VipCount;
			parameters[7].Value = pEntity.OnlineVipCountFor30DayOrder;
			parameters[8].Value = pEntity.OnlineVipCountForLast30DayOrder;
			parameters[9].Value = pEntity.OnlineVipCountFor30DayOrderM2M;
			parameters[10].Value = pEntity.OnlineVipCountPerFor30DayOrder;
			parameters[11].Value = pEntity.OnlineVipCountPerForLast30DayOrder;
			parameters[12].Value = pEntity.OnlineVipCountPerFor30DayOrderM2M;
			parameters[13].Value = pEntity.OnlineVipSalesFor30Day;
			parameters[14].Value = pEntity.OnlineVipSalesForLast30Day;
			parameters[15].Value = pEntity.OnlineVipSalesFor30DayM2M;
			parameters[16].Value = pEntity.VipSalesFor30Day;
			parameters[17].Value = pEntity.OnlineVipSalesPerFor30Day;
			parameters[18].Value = pEntity.VipSalesForLast30Day;
			parameters[19].Value = pEntity.OnlineVipSalesPerForLast30Day;
			parameters[20].Value = pEntity.OnlineVipSalesPerFor30DayM2M;
			parameters[21].Value = pEntity.SalesFor30Day;
			parameters[22].Value = pEntity.SalesForLast30Day;
			parameters[23].Value = pEntity.CreateTime;
			parameters[24].Value = pkGuid;

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
        public R_VipGoldHomeEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [R_VipGoldHome] where ID='{0}'  ", id.ToString());
            //��ȡ����
            R_VipGoldHomeEntity m = null;
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
        public R_VipGoldHomeEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [R_VipGoldHome] where 1=1 ");
            //��ȡ����
            List<R_VipGoldHomeEntity> list = new List<R_VipGoldHomeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    R_VipGoldHomeEntity m;
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
        public void Update(R_VipGoldHomeEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(R_VipGoldHomeEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
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
            strSql.Append("update [R_VipGoldHome] set ");
                        if (pIsUpdateNullField || pEntity.DateCode!=null)
                strSql.Append( "[DateCode]=@DateCode,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.OnlineOnlyFansCount!=null)
                strSql.Append( "[OnlineOnlyFansCount]=@OnlineOnlyFansCount,");
            if (pIsUpdateNullField || pEntity.OnlineVipCount!=null)
                strSql.Append( "[OnlineVipCount]=@OnlineVipCount,");
            if (pIsUpdateNullField || pEntity.OnlineFansCount!=null)
                strSql.Append( "[OnlineFansCount]=@OnlineFansCount,");
            if (pIsUpdateNullField || pEntity.OfflineVipCount!=null)
                strSql.Append( "[OfflineVipCount]=@OfflineVipCount,");
            if (pIsUpdateNullField || pEntity.VipCount!=null)
                strSql.Append( "[VipCount]=@VipCount,");
            if (pIsUpdateNullField || pEntity.OnlineVipCountFor30DayOrder!=null)
                strSql.Append( "[OnlineVipCountFor30DayOrder]=@OnlineVipCountFor30DayOrder,");
            if (pIsUpdateNullField || pEntity.OnlineVipCountForLast30DayOrder!=null)
                strSql.Append( "[OnlineVipCountForLast30DayOrder]=@OnlineVipCountForLast30DayOrder,");
            if (pIsUpdateNullField || pEntity.OnlineVipCountFor30DayOrderM2M!=null)
                strSql.Append( "[OnlineVipCountFor30DayOrderM2M]=@OnlineVipCountFor30DayOrderM2M,");
            if (pIsUpdateNullField || pEntity.OnlineVipCountPerFor30DayOrder!=null)
                strSql.Append( "[OnlineVipCountPerFor30DayOrder]=@OnlineVipCountPerFor30DayOrder,");
            if (pIsUpdateNullField || pEntity.OnlineVipCountPerForLast30DayOrder!=null)
                strSql.Append( "[OnlineVipCountPerForLast30DayOrder]=@OnlineVipCountPerForLast30DayOrder,");
            if (pIsUpdateNullField || pEntity.OnlineVipCountPerFor30DayOrderM2M!=null)
                strSql.Append( "[OnlineVipCountPerFor30DayOrderM2M]=@OnlineVipCountPerFor30DayOrderM2M,");
            if (pIsUpdateNullField || pEntity.OnlineVipSalesFor30Day!=null)
                strSql.Append( "[OnlineVipSalesFor30Day]=@OnlineVipSalesFor30Day,");
            if (pIsUpdateNullField || pEntity.OnlineVipSalesForLast30Day!=null)
                strSql.Append( "[OnlineVipSalesForLast30Day]=@OnlineVipSalesForLast30Day,");
            if (pIsUpdateNullField || pEntity.OnlineVipSalesFor30DayM2M!=null)
                strSql.Append( "[OnlineVipSalesFor30DayM2M]=@OnlineVipSalesFor30DayM2M,");
            if (pIsUpdateNullField || pEntity.VipSalesFor30Day!=null)
                strSql.Append( "[VipSalesFor30Day]=@VipSalesFor30Day,");
            if (pIsUpdateNullField || pEntity.OnlineVipSalesPerFor30Day!=null)
                strSql.Append( "[OnlineVipSalesPerFor30Day]=@OnlineVipSalesPerFor30Day,");
            if (pIsUpdateNullField || pEntity.VipSalesForLast30Day!=null)
                strSql.Append( "[VipSalesForLast30Day]=@VipSalesForLast30Day,");
            if (pIsUpdateNullField || pEntity.OnlineVipSalesPerForLast30Day!=null)
                strSql.Append( "[OnlineVipSalesPerForLast30Day]=@OnlineVipSalesPerForLast30Day,");
            if (pIsUpdateNullField || pEntity.OnlineVipSalesPerFor30DayM2M!=null)
                strSql.Append( "[OnlineVipSalesPerFor30DayM2M]=@OnlineVipSalesPerFor30DayM2M,");
            if (pIsUpdateNullField || pEntity.SalesFor30Day!=null)
                strSql.Append( "[SalesFor30Day]=@SalesFor30Day,");
            if (pIsUpdateNullField || pEntity.SalesForLast30Day!=null)
                strSql.Append( "[SalesForLast30Day]=@SalesForLast30Day");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@DateCode",SqlDbType.Date),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@OnlineOnlyFansCount",SqlDbType.Int),
					new SqlParameter("@OnlineVipCount",SqlDbType.Int),
					new SqlParameter("@OnlineFansCount",SqlDbType.Int),
					new SqlParameter("@OfflineVipCount",SqlDbType.Int),
					new SqlParameter("@VipCount",SqlDbType.Int),
					new SqlParameter("@OnlineVipCountFor30DayOrder",SqlDbType.Int),
					new SqlParameter("@OnlineVipCountForLast30DayOrder",SqlDbType.Int),
					new SqlParameter("@OnlineVipCountFor30DayOrderM2M",SqlDbType.Decimal),
					new SqlParameter("@OnlineVipCountPerFor30DayOrder",SqlDbType.Decimal),
					new SqlParameter("@OnlineVipCountPerForLast30DayOrder",SqlDbType.Decimal),
					new SqlParameter("@OnlineVipCountPerFor30DayOrderM2M",SqlDbType.Decimal),
					new SqlParameter("@OnlineVipSalesFor30Day",SqlDbType.Decimal),
					new SqlParameter("@OnlineVipSalesForLast30Day",SqlDbType.Decimal),
					new SqlParameter("@OnlineVipSalesFor30DayM2M",SqlDbType.Decimal),
					new SqlParameter("@VipSalesFor30Day",SqlDbType.Decimal),
					new SqlParameter("@OnlineVipSalesPerFor30Day",SqlDbType.Decimal),
					new SqlParameter("@VipSalesForLast30Day",SqlDbType.Decimal),
					new SqlParameter("@OnlineVipSalesPerForLast30Day",SqlDbType.Decimal),
					new SqlParameter("@OnlineVipSalesPerFor30DayM2M",SqlDbType.Decimal),
					new SqlParameter("@SalesFor30Day",SqlDbType.Decimal),
					new SqlParameter("@SalesForLast30Day",SqlDbType.Decimal),
					new SqlParameter("@ID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.DateCode;
			parameters[1].Value = pEntity.CustomerId;
			parameters[2].Value = pEntity.OnlineOnlyFansCount;
			parameters[3].Value = pEntity.OnlineVipCount;
			parameters[4].Value = pEntity.OnlineFansCount;
			parameters[5].Value = pEntity.OfflineVipCount;
			parameters[6].Value = pEntity.VipCount;
			parameters[7].Value = pEntity.OnlineVipCountFor30DayOrder;
			parameters[8].Value = pEntity.OnlineVipCountForLast30DayOrder;
			parameters[9].Value = pEntity.OnlineVipCountFor30DayOrderM2M;
			parameters[10].Value = pEntity.OnlineVipCountPerFor30DayOrder;
			parameters[11].Value = pEntity.OnlineVipCountPerForLast30DayOrder;
			parameters[12].Value = pEntity.OnlineVipCountPerFor30DayOrderM2M;
			parameters[13].Value = pEntity.OnlineVipSalesFor30Day;
			parameters[14].Value = pEntity.OnlineVipSalesForLast30Day;
			parameters[15].Value = pEntity.OnlineVipSalesFor30DayM2M;
			parameters[16].Value = pEntity.VipSalesFor30Day;
			parameters[17].Value = pEntity.OnlineVipSalesPerFor30Day;
			parameters[18].Value = pEntity.VipSalesForLast30Day;
			parameters[19].Value = pEntity.OnlineVipSalesPerForLast30Day;
			parameters[20].Value = pEntity.OnlineVipSalesPerFor30DayM2M;
			parameters[21].Value = pEntity.SalesFor30Day;
			parameters[22].Value = pEntity.SalesForLast30Day;
			parameters[23].Value = pEntity.ID;

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
        public void Update(R_VipGoldHomeEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(R_VipGoldHomeEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(R_VipGoldHomeEntity pEntity, IDbTransaction pTran)
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
            sql.AppendLine("update [R_VipGoldHome] set  where ID=@ID;");
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
        public void Delete(R_VipGoldHomeEntity[] pEntities, IDbTransaction pTran)
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
        public void Delete(R_VipGoldHomeEntity[] pEntities)
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
            sql.AppendLine("update [R_VipGoldHome] set  where ID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public R_VipGoldHomeEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [R_VipGoldHome] where 1=1  ");
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
            List<R_VipGoldHomeEntity> list = new List<R_VipGoldHomeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    R_VipGoldHomeEntity m;
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
        public PagedQueryResult<R_VipGoldHomeEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
            pagedSql.AppendFormat(") as ___rn,* from [R_VipGoldHome] where 1=1  ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [R_VipGoldHome] where 1=1  ");
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
            PagedQueryResult<R_VipGoldHomeEntity> result = new PagedQueryResult<R_VipGoldHomeEntity>();
            List<R_VipGoldHomeEntity> list = new List<R_VipGoldHomeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    R_VipGoldHomeEntity m;
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
        public R_VipGoldHomeEntity[] QueryByEntity(R_VipGoldHomeEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<R_VipGoldHomeEntity> PagedQueryByEntity(R_VipGoldHomeEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(R_VipGoldHomeEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.ID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ID", Value = pQueryEntity.ID });
            if (pQueryEntity.DateCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DateCode", Value = pQueryEntity.DateCode });
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
            if (pQueryEntity.OnlineOnlyFansCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OnlineOnlyFansCount", Value = pQueryEntity.OnlineOnlyFansCount });
            if (pQueryEntity.OnlineVipCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OnlineVipCount", Value = pQueryEntity.OnlineVipCount });
            if (pQueryEntity.OnlineFansCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OnlineFansCount", Value = pQueryEntity.OnlineFansCount });
            if (pQueryEntity.OfflineVipCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OfflineVipCount", Value = pQueryEntity.OfflineVipCount });
            if (pQueryEntity.VipCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCount", Value = pQueryEntity.VipCount });
            if (pQueryEntity.OnlineVipCountFor30DayOrder!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OnlineVipCountFor30DayOrder", Value = pQueryEntity.OnlineVipCountFor30DayOrder });
            if (pQueryEntity.OnlineVipCountForLast30DayOrder!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OnlineVipCountForLast30DayOrder", Value = pQueryEntity.OnlineVipCountForLast30DayOrder });
            if (pQueryEntity.OnlineVipCountFor30DayOrderM2M!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OnlineVipCountFor30DayOrderM2M", Value = pQueryEntity.OnlineVipCountFor30DayOrderM2M });
            if (pQueryEntity.OnlineVipCountPerFor30DayOrder!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OnlineVipCountPerFor30DayOrder", Value = pQueryEntity.OnlineVipCountPerFor30DayOrder });
            if (pQueryEntity.OnlineVipCountPerForLast30DayOrder!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OnlineVipCountPerForLast30DayOrder", Value = pQueryEntity.OnlineVipCountPerForLast30DayOrder });
            if (pQueryEntity.OnlineVipCountPerFor30DayOrderM2M!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OnlineVipCountPerFor30DayOrderM2M", Value = pQueryEntity.OnlineVipCountPerFor30DayOrderM2M });
            if (pQueryEntity.OnlineVipSalesFor30Day!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OnlineVipSalesFor30Day", Value = pQueryEntity.OnlineVipSalesFor30Day });
            if (pQueryEntity.OnlineVipSalesForLast30Day!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OnlineVipSalesForLast30Day", Value = pQueryEntity.OnlineVipSalesForLast30Day });
            if (pQueryEntity.OnlineVipSalesFor30DayM2M!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OnlineVipSalesFor30DayM2M", Value = pQueryEntity.OnlineVipSalesFor30DayM2M });
            if (pQueryEntity.VipSalesFor30Day!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipSalesFor30Day", Value = pQueryEntity.VipSalesFor30Day });
            if (pQueryEntity.OnlineVipSalesPerFor30Day!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OnlineVipSalesPerFor30Day", Value = pQueryEntity.OnlineVipSalesPerFor30Day });
            if (pQueryEntity.VipSalesForLast30Day!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipSalesForLast30Day", Value = pQueryEntity.VipSalesForLast30Day });
            if (pQueryEntity.OnlineVipSalesPerForLast30Day!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OnlineVipSalesPerForLast30Day", Value = pQueryEntity.OnlineVipSalesPerForLast30Day });
            if (pQueryEntity.OnlineVipSalesPerFor30DayM2M!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OnlineVipSalesPerFor30DayM2M", Value = pQueryEntity.OnlineVipSalesPerFor30DayM2M });
            if (pQueryEntity.SalesFor30Day!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesFor30Day", Value = pQueryEntity.SalesFor30Day });
            if (pQueryEntity.SalesForLast30Day!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesForLast30Day", Value = pQueryEntity.SalesForLast30Day });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(IDataReader pReader, out R_VipGoldHomeEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new R_VipGoldHomeEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["ID"] != DBNull.Value)
			{
				pInstance.ID =  (Guid)pReader["ID"];
			}
			if (pReader["DateCode"] != DBNull.Value)
			{
				pInstance.DateCode =Convert.ToDateTime(pReader["DateCode"]);
			}
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}
			if (pReader["OnlineOnlyFansCount"] != DBNull.Value)
			{
				pInstance.OnlineOnlyFansCount =   Convert.ToInt32(pReader["OnlineOnlyFansCount"]);
			}
			if (pReader["OnlineVipCount"] != DBNull.Value)
			{
				pInstance.OnlineVipCount =   Convert.ToInt32(pReader["OnlineVipCount"]);
			}
			if (pReader["OnlineFansCount"] != DBNull.Value)
			{
				pInstance.OnlineFansCount =   Convert.ToInt32(pReader["OnlineFansCount"]);
			}
			if (pReader["OfflineVipCount"] != DBNull.Value)
			{
				pInstance.OfflineVipCount =   Convert.ToInt32(pReader["OfflineVipCount"]);
			}
			if (pReader["VipCount"] != DBNull.Value)
			{
				pInstance.VipCount =   Convert.ToInt32(pReader["VipCount"]);
			}
			if (pReader["OnlineVipCountFor30DayOrder"] != DBNull.Value)
			{
				pInstance.OnlineVipCountFor30DayOrder =   Convert.ToInt32(pReader["OnlineVipCountFor30DayOrder"]);
			}
			if (pReader["OnlineVipCountForLast30DayOrder"] != DBNull.Value)
			{
				pInstance.OnlineVipCountForLast30DayOrder =   Convert.ToInt32(pReader["OnlineVipCountForLast30DayOrder"]);
			}
			if (pReader["OnlineVipCountFor30DayOrderM2M"] != DBNull.Value)
			{
				pInstance.OnlineVipCountFor30DayOrderM2M =  Convert.ToDecimal(pReader["OnlineVipCountFor30DayOrderM2M"]);
			}
			if (pReader["OnlineVipCountPerFor30DayOrder"] != DBNull.Value)
			{
				pInstance.OnlineVipCountPerFor30DayOrder =  Convert.ToDecimal(pReader["OnlineVipCountPerFor30DayOrder"]);
			}
			if (pReader["OnlineVipCountPerForLast30DayOrder"] != DBNull.Value)
			{
				pInstance.OnlineVipCountPerForLast30DayOrder =  Convert.ToDecimal(pReader["OnlineVipCountPerForLast30DayOrder"]);
			}
			if (pReader["OnlineVipCountPerFor30DayOrderM2M"] != DBNull.Value)
			{
				pInstance.OnlineVipCountPerFor30DayOrderM2M =  Convert.ToDecimal(pReader["OnlineVipCountPerFor30DayOrderM2M"]);
			}
			if (pReader["OnlineVipSalesFor30Day"] != DBNull.Value)
			{
				pInstance.OnlineVipSalesFor30Day =  Convert.ToDecimal(pReader["OnlineVipSalesFor30Day"]);
			}
			if (pReader["OnlineVipSalesForLast30Day"] != DBNull.Value)
			{
				pInstance.OnlineVipSalesForLast30Day =  Convert.ToDecimal(pReader["OnlineVipSalesForLast30Day"]);
			}
			if (pReader["OnlineVipSalesFor30DayM2M"] != DBNull.Value)
			{
				pInstance.OnlineVipSalesFor30DayM2M =  Convert.ToDecimal(pReader["OnlineVipSalesFor30DayM2M"]);
			}
			if (pReader["VipSalesFor30Day"] != DBNull.Value)
			{
				pInstance.VipSalesFor30Day =  Convert.ToDecimal(pReader["VipSalesFor30Day"]);
			}
			if (pReader["OnlineVipSalesPerFor30Day"] != DBNull.Value)
			{
				pInstance.OnlineVipSalesPerFor30Day =  Convert.ToDecimal(pReader["OnlineVipSalesPerFor30Day"]);
			}
			if (pReader["VipSalesForLast30Day"] != DBNull.Value)
			{
				pInstance.VipSalesForLast30Day =  Convert.ToDecimal(pReader["VipSalesForLast30Day"]);
			}
			if (pReader["OnlineVipSalesPerForLast30Day"] != DBNull.Value)
			{
				pInstance.OnlineVipSalesPerForLast30Day =  Convert.ToDecimal(pReader["OnlineVipSalesPerForLast30Day"]);
			}
			if (pReader["OnlineVipSalesPerFor30DayM2M"] != DBNull.Value)
			{
				pInstance.OnlineVipSalesPerFor30DayM2M =  Convert.ToDecimal(pReader["OnlineVipSalesPerFor30DayM2M"]);
			}
			if (pReader["SalesFor30Day"] != DBNull.Value)
			{
				pInstance.SalesFor30Day =  Convert.ToDecimal(pReader["SalesFor30Day"]);
			}
			if (pReader["SalesForLast30Day"] != DBNull.Value)
			{
				pInstance.SalesForLast30Day =  Convert.ToDecimal(pReader["SalesForLast30Day"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}

        }
        #endregion
    }
}
