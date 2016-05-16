/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/4/27 20:28:30
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
    public partial class PanicbuyingKJEventItemMappingDAO : BaseCPOSDAO, ICRUDable<PanicbuyingKJEventItemMappingEntity>, IQueryable<PanicbuyingKJEventItemMappingEntity>
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
            pEntity.IsDelete = 0;
            pEntity.CreateTime = DateTime.Now;
            pEntity.LastUpdateTime = pEntity.CreateTime;
            pEntity.CreateBy = CurrentUserInfo.UserID;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [PanicbuyingKJEventItemMapping](");
            strSql.Append("[EventId],[ItemID],[MinPrice],[MinBasePrice],[SoldQty],[Qty],[KeepQty],[SinglePurchaseQty],[DiscountRate],[PromotePersonCount],[BargainPersonCount],[PurchasePersonCount],[Status],[StatusReason],[DisplayIndex],[customerId],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[BargaingingInterval],[EventItemMappingID])");
            strSql.Append(" values (");

            strSql.Append("@EventId,@ItemID,@MinPrice,@MinBasePrice,@SoldQty,@Qty,@KeepQty,@SinglePurchaseQty,@DiscountRate,@PromotePersonCount,@BargainPersonCount,@PurchasePersonCount,@Status,@StatusReason,@DisplayIndex,@customerId,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@BargaingingInterval,@EventItemMappingID)");            


            Guid? pkGuid;
            if (pEntity.EventItemMappingID == null)
                pkGuid = Guid.NewGuid();
            else
                pkGuid = pEntity.EventItemMappingID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@EventId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@ItemID",SqlDbType.VarChar),
					new SqlParameter("@MinPrice",SqlDbType.Decimal),
					new SqlParameter("@MinBasePrice",SqlDbType.Decimal),
					new SqlParameter("@SoldQty",SqlDbType.Int),
					new SqlParameter("@Qty",SqlDbType.Int),
					new SqlParameter("@KeepQty",SqlDbType.Int),
					new SqlParameter("@SinglePurchaseQty",SqlDbType.Int),
					new SqlParameter("@DiscountRate",SqlDbType.Decimal),
					new SqlParameter("@PromotePersonCount",SqlDbType.Int),
					new SqlParameter("@BargainPersonCount",SqlDbType.Int),
					new SqlParameter("@PurchasePersonCount",SqlDbType.Int),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@StatusReason",SqlDbType.NVarChar),
					new SqlParameter("@DisplayIndex",SqlDbType.Int),
					new SqlParameter("@customerId",SqlDbType.VarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@BargaingingInterval",SqlDbType.Decimal),
					new SqlParameter("@EventItemMappingID",SqlDbType.UniqueIdentifier)
            };

			parameters[0].Value = pEntity.EventId;
			parameters[1].Value = pEntity.ItemID;
			parameters[2].Value = pEntity.MinPrice;
			parameters[3].Value = pEntity.MinBasePrice;
			parameters[4].Value = pEntity.SoldQty;
			parameters[5].Value = pEntity.Qty;
			parameters[6].Value = pEntity.KeepQty;
			parameters[7].Value = pEntity.SinglePurchaseQty;
			parameters[8].Value = pEntity.DiscountRate;
			parameters[9].Value = pEntity.PromotePersonCount;
			parameters[10].Value = pEntity.BargainPersonCount;
			parameters[11].Value = pEntity.PurchasePersonCount;
			parameters[12].Value = pEntity.Status;
			parameters[13].Value = pEntity.StatusReason;
			parameters[14].Value = pEntity.DisplayIndex;
			parameters[15].Value = pEntity.customerId;
			parameters[16].Value = pEntity.CreateTime;
			parameters[17].Value = pEntity.CreateBy;
			parameters[18].Value = pEntity.LastUpdateBy;
			parameters[19].Value = pEntity.LastUpdateTime;
			parameters[20].Value = pEntity.IsDelete;
			parameters[21].Value = pEntity.BargaingingInterval;
			parameters[22].Value = pkGuid;


            //ִ�в��������д
            int result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            pEntity.EventItemMappingID = pkGuid;
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
            sql.AppendFormat("select * from [PanicbuyingKJEventItemMapping] where EventItemMappingID='{0}'  and isdelete=0 ", id.ToString());
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
        public void Update(PanicbuyingKJEventItemMappingEntity pEntity, IDbTransaction pTran)
        {
            Update(pEntity, pTran, true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(PanicbuyingKJEventItemMappingEntity pEntity, IDbTransaction pTran, bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.EventItemMappingID.HasValue)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [PanicbuyingKJEventItemMapping] set ");

                        if (pIsUpdateNullField || pEntity.EventId!=null)
                strSql.Append( "[EventId]=@EventId,");
            if (pIsUpdateNullField || pEntity.ItemID!=null)
                strSql.Append( "[ItemID]=@ItemID,");
            if (pIsUpdateNullField || pEntity.MinPrice!=null)
                strSql.Append( "[MinPrice]=@MinPrice,");
            if (pIsUpdateNullField || pEntity.MinBasePrice!=null)
                strSql.Append( "[MinBasePrice]=@MinBasePrice,");
            if (pIsUpdateNullField || pEntity.SoldQty!=null)
                strSql.Append( "[SoldQty]=@SoldQty,");
            if (pIsUpdateNullField || pEntity.Qty!=null)
                strSql.Append( "[Qty]=@Qty,");
            if (pIsUpdateNullField || pEntity.KeepQty!=null)
                strSql.Append( "[KeepQty]=@KeepQty,");
            if (pIsUpdateNullField || pEntity.SinglePurchaseQty!=null)
                strSql.Append( "[SinglePurchaseQty]=@SinglePurchaseQty,");
            if (pIsUpdateNullField || pEntity.DiscountRate!=null)
                strSql.Append( "[DiscountRate]=@DiscountRate,");
            if (pIsUpdateNullField || pEntity.PromotePersonCount!=null)
                strSql.Append( "[PromotePersonCount]=@PromotePersonCount,");
            if (pIsUpdateNullField || pEntity.BargainPersonCount!=null)
                strSql.Append( "[BargainPersonCount]=@BargainPersonCount,");
            if (pIsUpdateNullField || pEntity.PurchasePersonCount!=null)
                strSql.Append( "[PurchasePersonCount]=@PurchasePersonCount,");
            if (pIsUpdateNullField || pEntity.Status!=null)
                strSql.Append( "[Status]=@Status,");
            if (pIsUpdateNullField || pEntity.StatusReason!=null)
                strSql.Append( "[StatusReason]=@StatusReason,");
            if (pIsUpdateNullField || pEntity.DisplayIndex!=null)
                strSql.Append( "[DisplayIndex]=@DisplayIndex,");
            if (pIsUpdateNullField || pEntity.customerId!=null)
                strSql.Append( "[customerId]=@customerId,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.BargaingingInterval!=null)
                strSql.Append( "[BargaingingInterval]=@BargaingingInterval");

            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where EventItemMappingID=@EventItemMappingID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@EventId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@ItemID",SqlDbType.VarChar),
					new SqlParameter("@MinPrice",SqlDbType.Decimal),
					new SqlParameter("@MinBasePrice",SqlDbType.Decimal),
					new SqlParameter("@SoldQty",SqlDbType.Int),
					new SqlParameter("@Qty",SqlDbType.Int),
					new SqlParameter("@KeepQty",SqlDbType.Int),
					new SqlParameter("@SinglePurchaseQty",SqlDbType.Int),
					new SqlParameter("@DiscountRate",SqlDbType.Decimal),
					new SqlParameter("@PromotePersonCount",SqlDbType.Int),
					new SqlParameter("@BargainPersonCount",SqlDbType.Int),
					new SqlParameter("@PurchasePersonCount",SqlDbType.Int),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@StatusReason",SqlDbType.NVarChar),
					new SqlParameter("@DisplayIndex",SqlDbType.Int),
					new SqlParameter("@customerId",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@BargaingingInterval",SqlDbType.Decimal),
					new SqlParameter("@EventItemMappingID",SqlDbType.UniqueIdentifier)
            };

			parameters[0].Value = pEntity.EventId;
			parameters[1].Value = pEntity.ItemID;
			parameters[2].Value = pEntity.MinPrice;
			parameters[3].Value = pEntity.MinBasePrice;
			parameters[4].Value = pEntity.SoldQty;
			parameters[5].Value = pEntity.Qty;
			parameters[6].Value = pEntity.KeepQty;
			parameters[7].Value = pEntity.SinglePurchaseQty;
			parameters[8].Value = pEntity.DiscountRate;
			parameters[9].Value = pEntity.PromotePersonCount;
			parameters[10].Value = pEntity.BargainPersonCount;
			parameters[11].Value = pEntity.PurchasePersonCount;
			parameters[12].Value = pEntity.Status;
			parameters[13].Value = pEntity.StatusReason;
			parameters[14].Value = pEntity.DisplayIndex;
			parameters[15].Value = pEntity.customerId;
			parameters[16].Value = pEntity.LastUpdateBy;
			parameters[17].Value = pEntity.LastUpdateTime;
			parameters[18].Value = pEntity.BargaingingInterval;
			parameters[19].Value = pEntity.EventItemMappingID;


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
        public void Update(PanicbuyingKJEventItemMappingEntity pEntity)
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
            if (!pEntity.EventItemMappingID.HasValue)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.EventItemMappingID.Value, pTran);
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
            sql.AppendLine("update [PanicbuyingKJEventItemMapping] set  isdelete=1 where EventItemMappingID=@EventItemMappingID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@EventItemMappingID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
                if (!pEntity.EventItemMappingID.HasValue)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.EventItemMappingID;
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
            sql.AppendLine("update [PanicbuyingKJEventItemMapping] set  isdelete=1 where EventItemMappingID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
                    if (item != null)
                    {
                        pagedSql.AppendFormat(" {0} {1},", StringUtils.WrapperSQLServerObject(item.FieldName), item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                    }
                }
                pagedSql.Remove(pagedSql.Length - 1, 1);
            }
            else
            {
                pagedSql.AppendFormat(" [EventItemMappingID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [PanicbuyingKJEventItemMapping] where 1=1  and isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [PanicbuyingKJEventItemMapping] where 1=1  and isdelete=0 ");
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
            return Query(queryWhereCondition, pOrderBys);
        }

        /// <summary>
        /// ��ҳ����ʵ��������ѯʵ��
        /// </summary>
        /// <param name="pQueryEntity">��ʵ����ʽ����Ĳ���</param>
        /// <param name="pOrderBys">�������</param>
        /// <returns>����������ʵ�弯</returns>
        public PagedQueryResult<PanicbuyingKJEventItemMappingEntity> PagedQueryByEntity(PanicbuyingKJEventItemMappingEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(PanicbuyingKJEventItemMappingEntity pQueryEntity)
        {
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.EventItemMappingID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventItemMappingID", Value = pQueryEntity.EventItemMappingID });
            if (pQueryEntity.EventId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventId", Value = pQueryEntity.EventId });
            if (pQueryEntity.ItemID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemID", Value = pQueryEntity.ItemID });
            if (pQueryEntity.MinPrice != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MinPrice", Value = pQueryEntity.MinPrice });
            if (pQueryEntity.MinBasePrice != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MinBasePrice", Value = pQueryEntity.MinBasePrice });
            if (pQueryEntity.SoldQty != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SoldQty", Value = pQueryEntity.SoldQty });
            if (pQueryEntity.Qty != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Qty", Value = pQueryEntity.Qty });
            if (pQueryEntity.KeepQty != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "KeepQty", Value = pQueryEntity.KeepQty });
            if (pQueryEntity.SinglePurchaseQty != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SinglePurchaseQty", Value = pQueryEntity.SinglePurchaseQty });
            if (pQueryEntity.DiscountRate != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DiscountRate", Value = pQueryEntity.DiscountRate });
            if (pQueryEntity.PromotePersonCount != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PromotePersonCount", Value = pQueryEntity.PromotePersonCount });
            if (pQueryEntity.BargainPersonCount != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BargainPersonCount", Value = pQueryEntity.BargainPersonCount });
            if (pQueryEntity.PurchasePersonCount != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PurchasePersonCount", Value = pQueryEntity.PurchasePersonCount });
            if (pQueryEntity.Status != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Status", Value = pQueryEntity.Status });
            if (pQueryEntity.StatusReason != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StatusReason", Value = pQueryEntity.StatusReason });
            if (pQueryEntity.DisplayIndex != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DisplayIndex", Value = pQueryEntity.DisplayIndex });
            if (pQueryEntity.customerId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customerId", Value = pQueryEntity.customerId });
            if (pQueryEntity.CreateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.LastUpdateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.LastUpdateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.IsDelete != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });
            if (pQueryEntity.BargaingingInterval!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BargaingingInterval", Value = pQueryEntity.BargaingingInterval });

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

            if (pReader["EventItemMappingID"] != DBNull.Value)
            {
                pInstance.EventItemMappingID = (Guid)pReader["EventItemMappingID"];
            }
            if (pReader["EventId"] != DBNull.Value)
            {
                pInstance.EventId = (Guid)pReader["EventId"];
            }
            if (pReader["ItemID"] != DBNull.Value)
            {
                pInstance.ItemID = Convert.ToString(pReader["ItemID"]);
                var ItemData = new T_ItemDAO(this.CurrentUserInfo).GetByID(pInstance.ItemID);
                if (ItemData != null)
                    pInstance.ItemName = ItemData.item_name;
            }
            if (pReader["MinPrice"] != DBNull.Value)
            {
                pInstance.MinPrice = Convert.ToDecimal(pReader["MinPrice"]);
            }
            if (pReader["MinBasePrice"] != DBNull.Value)
            {
                pInstance.MinBasePrice = Convert.ToDecimal(pReader["MinBasePrice"]);
            }
            if (pReader["SoldQty"] != DBNull.Value)
            {
                pInstance.SoldQty = Convert.ToInt32(pReader["SoldQty"]);
            }
            if (pReader["Qty"] != DBNull.Value)
            {
                pInstance.Qty = Convert.ToInt32(pReader["Qty"]);
            }
            if (pReader["KeepQty"] != DBNull.Value)
            {
                pInstance.KeepQty = Convert.ToInt32(pReader["KeepQty"]);
            }
            if (pReader["SinglePurchaseQty"] != DBNull.Value)
            {
                pInstance.SinglePurchaseQty = Convert.ToInt32(pReader["SinglePurchaseQty"]);
            }
            if (pReader["DiscountRate"] != DBNull.Value)
            {
                pInstance.DiscountRate = Convert.ToDecimal(pReader["DiscountRate"]);
            }
            if (pReader["PromotePersonCount"] != DBNull.Value)
            {
                pInstance.PromotePersonCount = Convert.ToInt32(pReader["PromotePersonCount"]);
            }
            if (pReader["BargainPersonCount"] != DBNull.Value)
            {
                pInstance.BargainPersonCount = Convert.ToInt32(pReader["BargainPersonCount"]);
            }
            if (pReader["PurchasePersonCount"] != DBNull.Value)
            {
                pInstance.PurchasePersonCount = Convert.ToInt32(pReader["PurchasePersonCount"]);
            }
            if (pReader["Status"] != DBNull.Value)
            {
                pInstance.Status = Convert.ToInt32(pReader["Status"]);
            }
            if (pReader["StatusReason"] != DBNull.Value)
            {
                pInstance.StatusReason = Convert.ToString(pReader["StatusReason"]);
            }
            if (pReader["DisplayIndex"] != DBNull.Value)
            {
                pInstance.DisplayIndex = Convert.ToInt32(pReader["DisplayIndex"]);
            }
            if (pReader["customerId"] != DBNull.Value)
            {
                pInstance.customerId = Convert.ToString(pReader["customerId"]);
            }
            if (pReader["CreateTime"] != DBNull.Value)
            {
                pInstance.CreateTime = Convert.ToDateTime(pReader["CreateTime"]);
            }
            if (pReader["CreateBy"] != DBNull.Value)
            {
                pInstance.CreateBy = Convert.ToString(pReader["CreateBy"]);
            }
            if (pReader["LastUpdateBy"] != DBNull.Value)
            {
                pInstance.LastUpdateBy = Convert.ToString(pReader["LastUpdateBy"]);
            }
            if (pReader["LastUpdateTime"] != DBNull.Value)
            {
                pInstance.LastUpdateTime = Convert.ToDateTime(pReader["LastUpdateTime"]);
            }
            if (pReader["IsDelete"] != DBNull.Value)
            {
                pInstance.IsDelete = Convert.ToInt32(pReader["IsDelete"]);
            }
			if (pReader["BargaingingInterval"] != DBNull.Value)
			{
				pInstance.BargaingingInterval =  Convert.ToDecimal(pReader["BargaingingInterval"]);
			}

        }
        #endregion
    }
}
