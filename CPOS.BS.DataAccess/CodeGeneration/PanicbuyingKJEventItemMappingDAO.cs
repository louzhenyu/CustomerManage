/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/2/16 14:37:46
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
    /// ��PanicbuyingKJEventItemMapping�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class PanicbuyingKJEventItemMappingDAO : Base.BaseCPOSDAO, ICRUDable<PanicbuyingKJEventItemMappingEntity>, IQueryable<PanicbuyingKJEventItemMappingEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public PanicbuyingKJEventItemMappingDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(PanicbuyingKJEventItemMappingEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(PanicbuyingKJEventItemMappingEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [PanicbuyingKJEventItemMapping](");
            strSql.Append("[EventId],[ItemId],[SkuId],[AddedTime],[Qty],[KeepQty],[SoldQty],[SinglePurchaseQty],[Price],[BasePrice],[BargainStartPrice],[BargainEndPrice],[BargaingingInterval],[DisplayIndex],[IsFirst],[Status],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[CustomerId],[IsDelete],[EventItemMappingId])");
            strSql.Append(" values (");
            strSql.Append("@EventId,@ItemId,@SkuId,@AddedTime,@Qty,@KeepQty,@SoldQty,@SinglePurchaseQty,@Price,@BasePrice,@BargainStartPrice,@BargainEndPrice,@BargaingingInterval,@DisplayIndex,@IsFirst,@Status,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@CustomerId,@IsDelete,@EventItemMappingId)");            

			Guid? pkGuid;
			if (pEntity.EventItemMappingId == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.EventItemMappingId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@EventId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@ItemId",SqlDbType.NVarChar),
					new SqlParameter("@SkuId",SqlDbType.NVarChar),
					new SqlParameter("@AddedTime",SqlDbType.DateTime),
					new SqlParameter("@Qty",SqlDbType.Int),
					new SqlParameter("@KeepQty",SqlDbType.Int),
					new SqlParameter("@SoldQty",SqlDbType.Int),
					new SqlParameter("@SinglePurchaseQty",SqlDbType.Int),
					new SqlParameter("@Price",SqlDbType.Decimal),
					new SqlParameter("@BasePrice",SqlDbType.Decimal),
					new SqlParameter("@BargainStartPrice",SqlDbType.Decimal),
					new SqlParameter("@BargainEndPrice",SqlDbType.Decimal),
					new SqlParameter("@BargaingingInterval",SqlDbType.Decimal),
					new SqlParameter("@DisplayIndex",SqlDbType.Int),
					new SqlParameter("@IsFirst",SqlDbType.Int),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerId",SqlDbType.VarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@EventItemMappingId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.EventId;
			parameters[1].Value = pEntity.ItemId;
			parameters[2].Value = pEntity.SkuId;
			parameters[3].Value = pEntity.AddedTime;
			parameters[4].Value = pEntity.Qty;
			parameters[5].Value = pEntity.KeepQty;
			parameters[6].Value = pEntity.SoldQty;
			parameters[7].Value = pEntity.SinglePurchaseQty;
			parameters[8].Value = pEntity.Price;
			parameters[9].Value = pEntity.BasePrice;
			parameters[10].Value = pEntity.BargainStartPrice;
			parameters[11].Value = pEntity.BargainEndPrice;
			parameters[12].Value = pEntity.BargaingingInterval;
			parameters[13].Value = pEntity.DisplayIndex;
			parameters[14].Value = pEntity.IsFirst;
			parameters[15].Value = pEntity.Status;
			parameters[16].Value = pEntity.CreateTime;
			parameters[17].Value = pEntity.CreateBy;
			parameters[18].Value = pEntity.LastUpdateBy;
			parameters[19].Value = pEntity.LastUpdateTime;
			parameters[20].Value = pEntity.CustomerId;
			parameters[21].Value = pEntity.IsDelete;
			parameters[22].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.EventItemMappingId = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public PanicbuyingKJEventItemMappingEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [PanicbuyingKJEventItemMapping] where EventItemMappingId='{0}'  and isdelete=0 ", id.ToString());
            //��ȡ����
            PanicbuyingKJEventItemMappingEntity m = null;
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
        public PanicbuyingKJEventItemMappingEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [PanicbuyingKJEventItemMapping] where 1=1  and isdelete=0");
            //��ȡ����
            List<PanicbuyingKJEventItemMappingEntity> list = new List<PanicbuyingKJEventItemMappingEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    PanicbuyingKJEventItemMappingEntity m;
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
        public void Update(PanicbuyingKJEventItemMappingEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(PanicbuyingKJEventItemMappingEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.EventItemMappingId.HasValue)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [PanicbuyingKJEventItemMapping] set ");
                        if (pIsUpdateNullField || pEntity.EventId!=null)
                strSql.Append( "[EventId]=@EventId,");
            if (pIsUpdateNullField || pEntity.ItemId!=null)
                strSql.Append( "[ItemId]=@ItemId,");
            if (pIsUpdateNullField || pEntity.SkuId!=null)
                strSql.Append( "[SkuId]=@SkuId,");
            if (pIsUpdateNullField || pEntity.AddedTime!=null)
                strSql.Append( "[AddedTime]=@AddedTime,");
            if (pIsUpdateNullField || pEntity.Qty!=null)
                strSql.Append( "[Qty]=@Qty,");
            if (pIsUpdateNullField || pEntity.KeepQty!=null)
                strSql.Append( "[KeepQty]=@KeepQty,");
            if (pIsUpdateNullField || pEntity.SoldQty!=null)
                strSql.Append( "[SoldQty]=@SoldQty,");
            if (pIsUpdateNullField || pEntity.SinglePurchaseQty!=null)
                strSql.Append( "[SinglePurchaseQty]=@SinglePurchaseQty,");
            if (pIsUpdateNullField || pEntity.Price!=null)
                strSql.Append( "[Price]=@Price,");
            if (pIsUpdateNullField || pEntity.BasePrice!=null)
                strSql.Append( "[BasePrice]=@BasePrice,");
            if (pIsUpdateNullField || pEntity.BargainStartPrice!=null)
                strSql.Append( "[BargainStartPrice]=@BargainStartPrice,");
            if (pIsUpdateNullField || pEntity.BargainEndPrice!=null)
                strSql.Append( "[BargainEndPrice]=@BargainEndPrice,");
            if (pIsUpdateNullField || pEntity.BargaingingInterval!=null)
                strSql.Append( "[BargaingingInterval]=@BargaingingInterval,");
            if (pIsUpdateNullField || pEntity.DisplayIndex!=null)
                strSql.Append( "[DisplayIndex]=@DisplayIndex,");
            if (pIsUpdateNullField || pEntity.IsFirst!=null)
                strSql.Append( "[IsFirst]=@IsFirst,");
            if (pIsUpdateNullField || pEntity.Status!=null)
                strSql.Append( "[Status]=@Status,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId");
            strSql.Append(" where EventItemMappingId=@EventItemMappingId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@EventId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@ItemId",SqlDbType.NVarChar),
					new SqlParameter("@SkuId",SqlDbType.NVarChar),
					new SqlParameter("@AddedTime",SqlDbType.DateTime),
					new SqlParameter("@Qty",SqlDbType.Int),
					new SqlParameter("@KeepQty",SqlDbType.Int),
					new SqlParameter("@SoldQty",SqlDbType.Int),
					new SqlParameter("@SinglePurchaseQty",SqlDbType.Int),
					new SqlParameter("@Price",SqlDbType.Decimal),
					new SqlParameter("@BasePrice",SqlDbType.Decimal),
					new SqlParameter("@BargainStartPrice",SqlDbType.Decimal),
					new SqlParameter("@BargainEndPrice",SqlDbType.Decimal),
					new SqlParameter("@BargaingingInterval",SqlDbType.Decimal),
					new SqlParameter("@DisplayIndex",SqlDbType.Int),
					new SqlParameter("@IsFirst",SqlDbType.Int),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerId",SqlDbType.VarChar),
					new SqlParameter("@EventItemMappingId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.EventId;
			parameters[1].Value = pEntity.ItemId;
			parameters[2].Value = pEntity.SkuId;
			parameters[3].Value = pEntity.AddedTime;
			parameters[4].Value = pEntity.Qty;
			parameters[5].Value = pEntity.KeepQty;
			parameters[6].Value = pEntity.SoldQty;
			parameters[7].Value = pEntity.SinglePurchaseQty;
			parameters[8].Value = pEntity.Price;
			parameters[9].Value = pEntity.BasePrice;
			parameters[10].Value = pEntity.BargainStartPrice;
			parameters[11].Value = pEntity.BargainEndPrice;
			parameters[12].Value = pEntity.BargaingingInterval;
			parameters[13].Value = pEntity.DisplayIndex;
			parameters[14].Value = pEntity.IsFirst;
			parameters[15].Value = pEntity.Status;
			parameters[16].Value = pEntity.LastUpdateBy;
			parameters[17].Value = pEntity.LastUpdateTime;
			parameters[18].Value = pEntity.CustomerId;
			parameters[19].Value = pEntity.EventItemMappingId;

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
        public void Update(PanicbuyingKJEventItemMappingEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(PanicbuyingKJEventItemMappingEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(PanicbuyingKJEventItemMappingEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.EventItemMappingId.HasValue)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.EventItemMappingId.Value, pTran);           
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
            sql.AppendLine("update [PanicbuyingKJEventItemMapping] set  isdelete=1 where EventItemMappingId=@EventItemMappingId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@EventItemMappingId",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(PanicbuyingKJEventItemMappingEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.EventItemMappingId.HasValue)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.EventItemMappingId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(PanicbuyingKJEventItemMappingEntity[] pEntities)
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
            sql.AppendLine("update [PanicbuyingKJEventItemMapping] set  isdelete=1 where EventItemMappingId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public PanicbuyingKJEventItemMappingEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [PanicbuyingKJEventItemMapping] where 1=1  and isdelete=0 ");
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
            List<PanicbuyingKJEventItemMappingEntity> list = new List<PanicbuyingKJEventItemMappingEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    PanicbuyingKJEventItemMappingEntity m;
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
        public PagedQueryResult<PanicbuyingKJEventItemMappingEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [EventItemMappingId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [PanicbuyingKJEventItemMapping] where 1=1  and isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [PanicbuyingKJEventItemMapping] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<PanicbuyingKJEventItemMappingEntity> result = new PagedQueryResult<PanicbuyingKJEventItemMappingEntity>();
            List<PanicbuyingKJEventItemMappingEntity> list = new List<PanicbuyingKJEventItemMappingEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    PanicbuyingKJEventItemMappingEntity m;
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
        public PanicbuyingKJEventItemMappingEntity[] QueryByEntity(PanicbuyingKJEventItemMappingEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<PanicbuyingKJEventItemMappingEntity> PagedQueryByEntity(PanicbuyingKJEventItemMappingEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(PanicbuyingKJEventItemMappingEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.EventItemMappingId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventItemMappingId", Value = pQueryEntity.EventItemMappingId });
            if (pQueryEntity.EventId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventId", Value = pQueryEntity.EventId });
            if (pQueryEntity.ItemId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemId", Value = pQueryEntity.ItemId });
            if (pQueryEntity.SkuId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SkuId", Value = pQueryEntity.SkuId });
            if (pQueryEntity.AddedTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AddedTime", Value = pQueryEntity.AddedTime });
            if (pQueryEntity.Qty!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Qty", Value = pQueryEntity.Qty });
            if (pQueryEntity.KeepQty!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "KeepQty", Value = pQueryEntity.KeepQty });
            if (pQueryEntity.SoldQty!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SoldQty", Value = pQueryEntity.SoldQty });
            if (pQueryEntity.SinglePurchaseQty!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SinglePurchaseQty", Value = pQueryEntity.SinglePurchaseQty });
            if (pQueryEntity.Price!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Price", Value = pQueryEntity.Price });
            if (pQueryEntity.BasePrice!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BasePrice", Value = pQueryEntity.BasePrice });
            if (pQueryEntity.BargainStartPrice!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BargainStartPrice", Value = pQueryEntity.BargainStartPrice });
            if (pQueryEntity.BargainEndPrice!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BargainEndPrice", Value = pQueryEntity.BargainEndPrice });
            if (pQueryEntity.BargaingingInterval!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BargaingingInterval", Value = pQueryEntity.BargaingingInterval });
            if (pQueryEntity.DisplayIndex!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DisplayIndex", Value = pQueryEntity.DisplayIndex });
            if (pQueryEntity.IsFirst!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsFirst", Value = pQueryEntity.IsFirst });
            if (pQueryEntity.Status!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Status", Value = pQueryEntity.Status });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
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
        protected void Load(IDataReader pReader, out PanicbuyingKJEventItemMappingEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new PanicbuyingKJEventItemMappingEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["EventItemMappingId"] != DBNull.Value)
			{
				pInstance.EventItemMappingId =  (Guid)pReader["EventItemMappingId"];
			}
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
			if (pReader["AddedTime"] != DBNull.Value)
			{
				pInstance.AddedTime =  Convert.ToDateTime(pReader["AddedTime"]);
			}
			if (pReader["Qty"] != DBNull.Value)
			{
				pInstance.Qty =   Convert.ToInt32(pReader["Qty"]);
			}
			if (pReader["KeepQty"] != DBNull.Value)
			{
				pInstance.KeepQty =   Convert.ToInt32(pReader["KeepQty"]);
			}
			if (pReader["SoldQty"] != DBNull.Value)
			{
				pInstance.SoldQty =   Convert.ToInt32(pReader["SoldQty"]);
			}
			if (pReader["SinglePurchaseQty"] != DBNull.Value)
			{
				pInstance.SinglePurchaseQty =   Convert.ToInt32(pReader["SinglePurchaseQty"]);
			}
			if (pReader["Price"] != DBNull.Value)
			{
				pInstance.Price =  Convert.ToDecimal(pReader["Price"]);
			}
			if (pReader["BasePrice"] != DBNull.Value)
			{
				pInstance.BasePrice =  Convert.ToDecimal(pReader["BasePrice"]);
			}
			if (pReader["BargainStartPrice"] != DBNull.Value)
			{
				pInstance.BargainStartPrice =  Convert.ToDecimal(pReader["BargainStartPrice"]);
			}
			if (pReader["BargainEndPrice"] != DBNull.Value)
			{
				pInstance.BargainEndPrice =  Convert.ToDecimal(pReader["BargainEndPrice"]);
			}
			if (pReader["BargaingingInterval"] != DBNull.Value)
			{
				pInstance.BargaingingInterval =  Convert.ToDecimal(pReader["BargaingingInterval"]);
			}
			if (pReader["DisplayIndex"] != DBNull.Value)
			{
				pInstance.DisplayIndex =   Convert.ToInt32(pReader["DisplayIndex"]);
			}
			if (pReader["IsFirst"] != DBNull.Value)
			{
				pInstance.IsFirst =   Convert.ToInt32(pReader["IsFirst"]);
			}
			if (pReader["Status"] != DBNull.Value)
			{
				pInstance.Status =   Convert.ToInt32(pReader["Status"]);
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
