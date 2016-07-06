/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/24 14:53:08
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
    /// ��T_SuperRetailTraderProfitDetail�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class T_SuperRetailTraderProfitDetailDAO : Base.BaseCPOSDAO, ICRUDable<T_SuperRetailTraderProfitDetailEntity>, IQueryable<T_SuperRetailTraderProfitDetailEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public T_SuperRetailTraderProfitDetailDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(T_SuperRetailTraderProfitDetailEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(T_SuperRetailTraderProfitDetailEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [T_SuperRetailTraderProfitDetail](");
            strSql.Append("[SuperRetailTraderProfitConfigId],[SuperRetailTraderID],[Level],[ProfitType],[Profit],[OrderType],[OrderId],[OrderNo],[OrderDate],[OrderActualAmount],[SalesId],[VipId],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[CustomerId],[IsDelete],[Id])");
            strSql.Append(" values (");
            strSql.Append("@SuperRetailTraderProfitConfigId,@SuperRetailTraderID,@Level,@ProfitType,@Profit,@OrderType,@OrderId,@OrderNo,@OrderDate,@OrderActualAmount,@SalesId,@VipId,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@CustomerId,@IsDelete,@Id)");            

			Guid? pkGuid;
			if (pEntity.Id == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.Id;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@SuperRetailTraderProfitConfigId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@SuperRetailTraderID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@Level",SqlDbType.Int),
					new SqlParameter("@ProfitType",SqlDbType.VarChar),
					new SqlParameter("@Profit",SqlDbType.Decimal),
					new SqlParameter("@OrderType",SqlDbType.VarChar),
					new SqlParameter("@OrderId",SqlDbType.NVarChar),
					new SqlParameter("@OrderNo",SqlDbType.NVarChar),
					new SqlParameter("@OrderDate",SqlDbType.DateTime),
					new SqlParameter("@OrderActualAmount",SqlDbType.Decimal),
					new SqlParameter("@SalesId",SqlDbType.NVarChar),
					new SqlParameter("@VipId",SqlDbType.NVarChar),
					new SqlParameter("@CreateBy",SqlDbType.VarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerId",SqlDbType.VarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@Id",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.SuperRetailTraderProfitConfigId;
			parameters[1].Value = pEntity.SuperRetailTraderID;
			parameters[2].Value = pEntity.Level;
			parameters[3].Value = pEntity.ProfitType;
			parameters[4].Value = pEntity.Profit;
			parameters[5].Value = pEntity.OrderType;
			parameters[6].Value = pEntity.OrderId;
			parameters[7].Value = pEntity.OrderNo;
			parameters[8].Value = pEntity.OrderDate;
			parameters[9].Value = pEntity.OrderActualAmount;
			parameters[10].Value = pEntity.SalesId;
			parameters[11].Value = pEntity.VipId;
			parameters[12].Value = pEntity.CreateBy;
			parameters[13].Value = pEntity.CreateTime;
			parameters[14].Value = pEntity.LastUpdateBy;
			parameters[15].Value = pEntity.LastUpdateTime;
			parameters[16].Value = pEntity.CustomerId;
			parameters[17].Value = pEntity.IsDelete;
			parameters[18].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.Id = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public T_SuperRetailTraderProfitDetailEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_SuperRetailTraderProfitDetail] where Id='{0}'  and isdelete=0 ", id.ToString());
            //��ȡ����
            T_SuperRetailTraderProfitDetailEntity m = null;
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
        public T_SuperRetailTraderProfitDetailEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_SuperRetailTraderProfitDetail] where 1=1  and isdelete=0");
            //��ȡ����
            List<T_SuperRetailTraderProfitDetailEntity> list = new List<T_SuperRetailTraderProfitDetailEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_SuperRetailTraderProfitDetailEntity m;
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
        public void Update(T_SuperRetailTraderProfitDetailEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(T_SuperRetailTraderProfitDetailEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.Id.HasValue)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_SuperRetailTraderProfitDetail] set ");
                        if (pIsUpdateNullField || pEntity.SuperRetailTraderProfitConfigId!=null)
                strSql.Append( "[SuperRetailTraderProfitConfigId]=@SuperRetailTraderProfitConfigId,");
            if (pIsUpdateNullField || pEntity.SuperRetailTraderID!=null)
                strSql.Append( "[SuperRetailTraderID]=@SuperRetailTraderID,");
            if (pIsUpdateNullField || pEntity.Level!=null)
                strSql.Append( "[Level]=@Level,");
            if (pIsUpdateNullField || pEntity.ProfitType!=null)
                strSql.Append( "[ProfitType]=@ProfitType,");
            if (pIsUpdateNullField || pEntity.Profit!=null)
                strSql.Append( "[Profit]=@Profit,");
            if (pIsUpdateNullField || pEntity.OrderType!=null)
                strSql.Append( "[OrderType]=@OrderType,");
            if (pIsUpdateNullField || pEntity.OrderId!=null)
                strSql.Append( "[OrderId]=@OrderId,");
            if (pIsUpdateNullField || pEntity.OrderNo!=null)
                strSql.Append( "[OrderNo]=@OrderNo,");
            if (pIsUpdateNullField || pEntity.OrderDate!=null)
                strSql.Append( "[OrderDate]=@OrderDate,");
            if (pIsUpdateNullField || pEntity.OrderActualAmount!=null)
                strSql.Append( "[OrderActualAmount]=@OrderActualAmount,");
            if (pIsUpdateNullField || pEntity.SalesId!=null)
                strSql.Append( "[SalesId]=@SalesId,");
            if (pIsUpdateNullField || pEntity.VipId!=null)
                strSql.Append( "[VipId]=@VipId,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@SuperRetailTraderProfitConfigId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@SuperRetailTraderID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@Level",SqlDbType.Int),
					new SqlParameter("@ProfitType",SqlDbType.VarChar),
					new SqlParameter("@Profit",SqlDbType.Decimal),
					new SqlParameter("@OrderType",SqlDbType.VarChar),
					new SqlParameter("@OrderId",SqlDbType.NVarChar),
					new SqlParameter("@OrderNo",SqlDbType.NVarChar),
					new SqlParameter("@OrderDate",SqlDbType.DateTime),
					new SqlParameter("@OrderActualAmount",SqlDbType.Decimal),
					new SqlParameter("@SalesId",SqlDbType.NVarChar),
					new SqlParameter("@VipId",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerId",SqlDbType.VarChar),
					new SqlParameter("@Id",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.SuperRetailTraderProfitConfigId;
			parameters[1].Value = pEntity.SuperRetailTraderID;
			parameters[2].Value = pEntity.Level;
			parameters[3].Value = pEntity.ProfitType;
			parameters[4].Value = pEntity.Profit;
			parameters[5].Value = pEntity.OrderType;
			parameters[6].Value = pEntity.OrderId;
			parameters[7].Value = pEntity.OrderNo;
			parameters[8].Value = pEntity.OrderDate;
			parameters[9].Value = pEntity.OrderActualAmount;
			parameters[10].Value = pEntity.SalesId;
			parameters[11].Value = pEntity.VipId;
			parameters[12].Value = pEntity.LastUpdateBy;
			parameters[13].Value = pEntity.LastUpdateTime;
			parameters[14].Value = pEntity.CustomerId;
			parameters[15].Value = pEntity.Id;

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
        public void Update(T_SuperRetailTraderProfitDetailEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(T_SuperRetailTraderProfitDetailEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(T_SuperRetailTraderProfitDetailEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.Id.HasValue)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.Id.Value, pTran);           
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
            sql.AppendLine("update [T_SuperRetailTraderProfitDetail] set  isdelete=1 where Id=@Id;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@Id",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(T_SuperRetailTraderProfitDetailEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.Id.HasValue)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.Id;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(T_SuperRetailTraderProfitDetailEntity[] pEntities)
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
            sql.AppendLine("update [T_SuperRetailTraderProfitDetail] set  isdelete=1 where Id in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public T_SuperRetailTraderProfitDetailEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_SuperRetailTraderProfitDetail] where 1=1  and isdelete=0 ");
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
            List<T_SuperRetailTraderProfitDetailEntity> list = new List<T_SuperRetailTraderProfitDetailEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_SuperRetailTraderProfitDetailEntity m;
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
        public PagedQueryResult<T_SuperRetailTraderProfitDetailEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [Id] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [T_SuperRetailTraderProfitDetail] where 1=1  and isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [T_SuperRetailTraderProfitDetail] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<T_SuperRetailTraderProfitDetailEntity> result = new PagedQueryResult<T_SuperRetailTraderProfitDetailEntity>();
            List<T_SuperRetailTraderProfitDetailEntity> list = new List<T_SuperRetailTraderProfitDetailEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    T_SuperRetailTraderProfitDetailEntity m;
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
        public T_SuperRetailTraderProfitDetailEntity[] QueryByEntity(T_SuperRetailTraderProfitDetailEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<T_SuperRetailTraderProfitDetailEntity> PagedQueryByEntity(T_SuperRetailTraderProfitDetailEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(T_SuperRetailTraderProfitDetailEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.Id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Id", Value = pQueryEntity.Id });
            if (pQueryEntity.SuperRetailTraderProfitConfigId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SuperRetailTraderProfitConfigId", Value = pQueryEntity.SuperRetailTraderProfitConfigId });
            if (pQueryEntity.SuperRetailTraderID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SuperRetailTraderID", Value = pQueryEntity.SuperRetailTraderID });
            if (pQueryEntity.Level!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Level", Value = pQueryEntity.Level });
            if (pQueryEntity.ProfitType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ProfitType", Value = pQueryEntity.ProfitType });
            if (pQueryEntity.Profit!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Profit", Value = pQueryEntity.Profit });
            if (pQueryEntity.OrderType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderType", Value = pQueryEntity.OrderType });
            if (pQueryEntity.OrderId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderId", Value = pQueryEntity.OrderId });
            if (pQueryEntity.OrderNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderNo", Value = pQueryEntity.OrderNo });
            if (pQueryEntity.OrderDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderDate", Value = pQueryEntity.OrderDate });
            if (pQueryEntity.OrderActualAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderActualAmount", Value = pQueryEntity.OrderActualAmount });
            if (pQueryEntity.SalesId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesId", Value = pQueryEntity.SalesId });
            if (pQueryEntity.VipId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipId", Value = pQueryEntity.VipId });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.LastUpdateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.LastUpdateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
            if (pQueryEntity.IsDelete!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(IDataReader pReader, out T_SuperRetailTraderProfitDetailEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new T_SuperRetailTraderProfitDetailEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["Id"] != DBNull.Value)
			{
				pInstance.Id =  (Guid)pReader["Id"];
			}
			if (pReader["SuperRetailTraderProfitConfigId"] != DBNull.Value)
			{
				pInstance.SuperRetailTraderProfitConfigId =  (Guid)pReader["SuperRetailTraderProfitConfigId"];
			}
			if (pReader["SuperRetailTraderID"] != DBNull.Value)
			{
				pInstance.SuperRetailTraderID =  (Guid)pReader["SuperRetailTraderID"];
			}
			if (pReader["Level"] != DBNull.Value)
			{
				pInstance.Level =   Convert.ToInt32(pReader["Level"]);
			}
			if (pReader["ProfitType"] != DBNull.Value)
			{
				pInstance.ProfitType =  Convert.ToString(pReader["ProfitType"]);
			}
			if (pReader["Profit"] != DBNull.Value)
			{
				pInstance.Profit =  Convert.ToDecimal(pReader["Profit"]);
			}
			if (pReader["OrderType"] != DBNull.Value)
			{
				pInstance.OrderType =  Convert.ToString(pReader["OrderType"]);
			}
			if (pReader["OrderId"] != DBNull.Value)
			{
				pInstance.OrderId =  Convert.ToString(pReader["OrderId"]);
			}
			if (pReader["OrderNo"] != DBNull.Value)
			{
				pInstance.OrderNo =  Convert.ToString(pReader["OrderNo"]);
			}
			if (pReader["OrderDate"] != DBNull.Value)
			{
				pInstance.OrderDate =  Convert.ToDateTime(pReader["OrderDate"]);
			}
			if (pReader["OrderActualAmount"] != DBNull.Value)
			{
				pInstance.OrderActualAmount =  Convert.ToDecimal(pReader["OrderActualAmount"]);
			}
			if (pReader["SalesId"] != DBNull.Value)
			{
				pInstance.SalesId =  Convert.ToString(pReader["SalesId"]);
			}
			if (pReader["VipId"] != DBNull.Value)
			{
				pInstance.VipId =  Convert.ToString(pReader["VipId"]);
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
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}
			if (pReader["IsDelete"] != DBNull.Value)
			{
				pInstance.IsDelete =   Convert.ToInt32(pReader["IsDelete"]);
			}

        }
        #endregion
    }
}
