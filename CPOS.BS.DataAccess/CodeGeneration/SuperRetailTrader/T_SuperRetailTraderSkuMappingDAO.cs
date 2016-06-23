/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/28 10:12:43
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
    /// ��T_SuperRetailTraderSkuMapping�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class T_SuperRetailTraderSkuMappingDAO : BaseCPOSDAO, ICRUDable<T_SuperRetailTraderSkuMappingEntity>, IQueryable<T_SuperRetailTraderSkuMappingEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public T_SuperRetailTraderSkuMappingDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(T_SuperRetailTraderSkuMappingEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(T_SuperRetailTraderSkuMappingEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [T_SuperRetailTraderSkuMapping](");
            strSql.Append("[SuperRetailTraderItemMappingId],[ItemId],[SkuId],[DistributerStock],[SalesQty],[DistributerCostPrice],[DistributerPrice],[Status],[OnShelfDatetime],[OffShelfDatetime],[RefId],[CustomerID],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[Id])");
            strSql.Append(" values (");
            strSql.Append("@SuperRetailTraderItemMappingId,@ItemId,@SkuId,@DistributerStock,@SalesQty,@DistributerCostPrice,@DistributerPrice,@Status,@OnShelfDatetime,@OffShelfDatetime,@RefId,@CustomerID,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@Id)");            

			Guid? pkGuid;
			if (pEntity.Id == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.Id;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@SuperRetailTraderItemMappingId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@ItemId",SqlDbType.NVarChar),
					new SqlParameter("@SkuId",SqlDbType.NVarChar),
					new SqlParameter("@DistributerStock",SqlDbType.Int),
					new SqlParameter("@SalesQty",SqlDbType.Int),
					new SqlParameter("@DistributerCostPrice",SqlDbType.Decimal),
					new SqlParameter("@DistributerPrice",SqlDbType.Decimal),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@OnShelfDatetime",SqlDbType.DateTime),
					new SqlParameter("@OffShelfDatetime",SqlDbType.DateTime),
					new SqlParameter("@RefId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@Id",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.SuperRetailTraderItemMappingId;
			parameters[1].Value = pEntity.ItemId;
			parameters[2].Value = pEntity.SkuId;
			parameters[3].Value = pEntity.DistributerStock;
			parameters[4].Value = pEntity.SalesQty;
			parameters[5].Value = pEntity.DistributerCostPrice;
			parameters[6].Value = pEntity.DistributerPrice;
			parameters[7].Value = pEntity.Status;
			parameters[8].Value = pEntity.OnShelfDatetime;
			parameters[9].Value = pEntity.OffShelfDatetime;
			parameters[10].Value = pEntity.RefId;
			parameters[11].Value = pEntity.CustomerID;
			parameters[12].Value = pEntity.CreateTime;
			parameters[13].Value = pEntity.CreateBy;
			parameters[14].Value = pEntity.LastUpdateTime;
			parameters[15].Value = pEntity.LastUpdateBy;
			parameters[16].Value = pEntity.IsDelete;
			parameters[17].Value = pkGuid;

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
        public T_SuperRetailTraderSkuMappingEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_SuperRetailTraderSkuMapping] where Id='{0}'  and isdelete=0 ", id.ToString());
            //��ȡ����
            T_SuperRetailTraderSkuMappingEntity m = null;
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
        public T_SuperRetailTraderSkuMappingEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_SuperRetailTraderSkuMapping] where 1=1  and isdelete=0");
            //��ȡ����
            List<T_SuperRetailTraderSkuMappingEntity> list = new List<T_SuperRetailTraderSkuMappingEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_SuperRetailTraderSkuMappingEntity m;
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
        public void Update(T_SuperRetailTraderSkuMappingEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(T_SuperRetailTraderSkuMappingEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
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
            strSql.Append("update [T_SuperRetailTraderSkuMapping] set ");
                        if (pIsUpdateNullField || pEntity.SuperRetailTraderItemMappingId!=null)
                strSql.Append( "[SuperRetailTraderItemMappingId]=@SuperRetailTraderItemMappingId,");
            if (pIsUpdateNullField || pEntity.ItemId!=null)
                strSql.Append( "[ItemId]=@ItemId,");
            if (pIsUpdateNullField || pEntity.SkuId!=null)
                strSql.Append( "[SkuId]=@SkuId,");
            if (pIsUpdateNullField || pEntity.DistributerStock!=null)
                strSql.Append( "[DistributerStock]=@DistributerStock,");
            if (pIsUpdateNullField || pEntity.SalesQty!=null)
                strSql.Append( "[SalesQty]=@SalesQty,");
            if (pIsUpdateNullField || pEntity.DistributerCostPrice!=null)
                strSql.Append( "[DistributerCostPrice]=@DistributerCostPrice,");
            if (pIsUpdateNullField || pEntity.DistributerPrice!=null)
                strSql.Append( "[DistributerPrice]=@DistributerPrice,");
            if (pIsUpdateNullField || pEntity.Status!=null)
                strSql.Append( "[Status]=@Status,");
            if (pIsUpdateNullField || pEntity.OnShelfDatetime!=null)
                strSql.Append( "[OnShelfDatetime]=@OnShelfDatetime,");
            if (pIsUpdateNullField || pEntity.OffShelfDatetime!=null)
                strSql.Append( "[OffShelfDatetime]=@OffShelfDatetime,");
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
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@SuperRetailTraderItemMappingId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@ItemId",SqlDbType.NVarChar),
					new SqlParameter("@SkuId",SqlDbType.NVarChar),
					new SqlParameter("@DistributerStock",SqlDbType.Int),
					new SqlParameter("@SalesQty",SqlDbType.Int),
					new SqlParameter("@DistributerCostPrice",SqlDbType.Decimal),
					new SqlParameter("@DistributerPrice",SqlDbType.Decimal),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@OnShelfDatetime",SqlDbType.DateTime),
					new SqlParameter("@OffShelfDatetime",SqlDbType.DateTime),
					new SqlParameter("@RefId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@Id",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.SuperRetailTraderItemMappingId;
			parameters[1].Value = pEntity.ItemId;
			parameters[2].Value = pEntity.SkuId;
			parameters[3].Value = pEntity.DistributerStock;
			parameters[4].Value = pEntity.SalesQty;
			parameters[5].Value = pEntity.DistributerCostPrice;
			parameters[6].Value = pEntity.DistributerPrice;
			parameters[7].Value = pEntity.Status;
			parameters[8].Value = pEntity.OnShelfDatetime;
			parameters[9].Value = pEntity.OffShelfDatetime;
			parameters[10].Value = pEntity.RefId;
			parameters[11].Value = pEntity.CustomerID;
			parameters[12].Value = pEntity.LastUpdateTime;
			parameters[13].Value = pEntity.LastUpdateBy;
			parameters[14].Value = pEntity.Id;

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
        public void Update(T_SuperRetailTraderSkuMappingEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(T_SuperRetailTraderSkuMappingEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(T_SuperRetailTraderSkuMappingEntity pEntity, IDbTransaction pTran)
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
            sql.AppendLine("update [T_SuperRetailTraderSkuMapping] set  isdelete=1 where Id=@Id;");
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
        public void Delete(T_SuperRetailTraderSkuMappingEntity[] pEntities, IDbTransaction pTran)
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
        public void Delete(T_SuperRetailTraderSkuMappingEntity[] pEntities)
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
            sql.AppendLine("update [T_SuperRetailTraderSkuMapping] set  isdelete=1 where Id in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public T_SuperRetailTraderSkuMappingEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_SuperRetailTraderSkuMapping] where 1=1  and isdelete=0 ");
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
            List<T_SuperRetailTraderSkuMappingEntity> list = new List<T_SuperRetailTraderSkuMappingEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_SuperRetailTraderSkuMappingEntity m;
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
        public PagedQueryResult<T_SuperRetailTraderSkuMappingEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
            pagedSql.AppendFormat(") as ___rn,* from [T_SuperRetailTraderSkuMapping] where 1=1  and isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [T_SuperRetailTraderSkuMapping] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<T_SuperRetailTraderSkuMappingEntity> result = new PagedQueryResult<T_SuperRetailTraderSkuMappingEntity>();
            List<T_SuperRetailTraderSkuMappingEntity> list = new List<T_SuperRetailTraderSkuMappingEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    T_SuperRetailTraderSkuMappingEntity m;
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
        public T_SuperRetailTraderSkuMappingEntity[] QueryByEntity(T_SuperRetailTraderSkuMappingEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<T_SuperRetailTraderSkuMappingEntity> PagedQueryByEntity(T_SuperRetailTraderSkuMappingEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(T_SuperRetailTraderSkuMappingEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.Id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Id", Value = pQueryEntity.Id });
            if (pQueryEntity.SuperRetailTraderItemMappingId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SuperRetailTraderItemMappingId", Value = pQueryEntity.SuperRetailTraderItemMappingId });
            if (pQueryEntity.ItemId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemId", Value = pQueryEntity.ItemId });
            if (pQueryEntity.SkuId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SkuId", Value = pQueryEntity.SkuId });
            if (pQueryEntity.DistributerStock!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DistributerStock", Value = pQueryEntity.DistributerStock });
            if (pQueryEntity.SalesQty!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesQty", Value = pQueryEntity.SalesQty });
            if (pQueryEntity.DistributerCostPrice!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DistributerCostPrice", Value = pQueryEntity.DistributerCostPrice });
            if (pQueryEntity.DistributerPrice!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DistributerPrice", Value = pQueryEntity.DistributerPrice });
            if (pQueryEntity.Status!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Status", Value = pQueryEntity.Status });
            if (pQueryEntity.OnShelfDatetime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OnShelfDatetime", Value = pQueryEntity.OnShelfDatetime });
            if (pQueryEntity.OffShelfDatetime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OffShelfDatetime", Value = pQueryEntity.OffShelfDatetime });
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
        protected void Load(IDataReader pReader, out T_SuperRetailTraderSkuMappingEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new T_SuperRetailTraderSkuMappingEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["Id"] != DBNull.Value)
			{
				pInstance.Id =  (Guid)pReader["Id"];
			}
			if (pReader["SuperRetailTraderItemMappingId"] != DBNull.Value)
			{
				pInstance.SuperRetailTraderItemMappingId =  (Guid)pReader["SuperRetailTraderItemMappingId"];
			}
			if (pReader["ItemId"] != DBNull.Value)
			{
				pInstance.ItemId =  Convert.ToString(pReader["ItemId"]);
			}
			if (pReader["SkuId"] != DBNull.Value)
			{
				pInstance.SkuId =  Convert.ToString(pReader["SkuId"]);
			}
			if (pReader["DistributerStock"] != DBNull.Value)
			{
				pInstance.DistributerStock =   Convert.ToInt32(pReader["DistributerStock"]);
			}
			if (pReader["SalesQty"] != DBNull.Value)
			{
				pInstance.SalesQty =   Convert.ToInt32(pReader["SalesQty"]);
			}
			if (pReader["DistributerCostPrice"] != DBNull.Value)
			{
				pInstance.DistributerCostPrice =  Convert.ToDecimal(pReader["DistributerCostPrice"]);
			}
			if (pReader["DistributerPrice"] != DBNull.Value)
			{
				pInstance.DistributerPrice =  Convert.ToDecimal(pReader["DistributerPrice"]);
			}
			if (pReader["Status"] != DBNull.Value)
			{
				pInstance.Status =   Convert.ToInt32(pReader["Status"]);
			}
			if (pReader["OnShelfDatetime"] != DBNull.Value)
			{
				pInstance.OnShelfDatetime =  Convert.ToDateTime(pReader["OnShelfDatetime"]);
			}
			if (pReader["OffShelfDatetime"] != DBNull.Value)
			{
				pInstance.OffShelfDatetime =  Convert.ToDateTime(pReader["OffShelfDatetime"]);
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
