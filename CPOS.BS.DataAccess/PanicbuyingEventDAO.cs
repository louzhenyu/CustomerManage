/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/25 13:51:19
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
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{

    /// <summary>
    /// ���ݷ��ʣ�  
    /// ��PanicbuyingEvent�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class PanicbuyingEventDAO : Base.BaseCPOSDAO, ICRUDable<PanicbuyingEventEntity>, IQueryable<PanicbuyingEventEntity>
    {
        /// <summary>
        /// �����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public string AddPanicbuyingEvent(PanicbuyingEventEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");

            //��ʼ���̶��ֶ�
            pEntity.CreateTime = DateTime.Now;
            pEntity.CreateBy = CurrentUserInfo.UserID;
            pEntity.LastUpdateTime = pEntity.CreateTime;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;
            pEntity.IsDelete = 0;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [PanicbuyingEvent](");
            strSql.Append("[EventName],[EventTypeId],[BeginTime],[EndTime],[EventRemark],[CustomerID],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[EventStatus],[EventId])");
            strSql.Append(" values (");
            strSql.Append("@EventName,@EventTypeId,@BeginTime,@EndTime,@EventRemark,@CustomerID,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@EventStatus,@EventId)");

            Guid? pkGuid;
            if (pEntity.EventId == null)
                pkGuid = Guid.NewGuid();
            else
                pkGuid = pEntity.EventId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@EventName",SqlDbType.NVarChar),
					new SqlParameter("@EventTypeId",SqlDbType.Int),
					new SqlParameter("@BeginTime",SqlDbType.DateTime),
					new SqlParameter("@EndTime",SqlDbType.DateTime),
					new SqlParameter("@EventRemark",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@EventStatus",SqlDbType.Int),
					new SqlParameter("@EventId",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.EventName;
            parameters[1].Value = pEntity.EventTypeId;
            parameters[2].Value = pEntity.BeginTime;
            parameters[3].Value = pEntity.EndTime;
            parameters[4].Value = pEntity.EventRemark;
            parameters[5].Value = pEntity.CustomerID;
            parameters[6].Value = pEntity.CreateTime;
            parameters[7].Value = pEntity.CreateBy;
            parameters[8].Value = pEntity.LastUpdateBy;
            parameters[9].Value = pEntity.LastUpdateTime;
            parameters[10].Value = pEntity.IsDelete;
            parameters[11].Value = pEntity.EventStatus;
            parameters[12].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            pEntity.EventId = pkGuid;
            return pkGuid.ToString();
        }

        /// <summary>
        /// ��ȡ��б�
        /// </summary>
        /// <param name="pWhereConditions">ɸѡ����</param>
        /// <param name="pOrderBys">����</param>
        /// <param name="pPageSize">ÿҳ�ļ�¼��</param>
        /// <param name="pCurrentPageIndex">��0��ʼ�ĵ�ǰҳ��</param>
        /// <returns></returns>
        public PagedQueryResult<PanicbuyingEventEntity> GetPanicbuyingEvent(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [EventId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from VwPanicBuyingEvent where 1=1 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from VwPanicBuyingEvent where 1=1 ");
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
            PagedQueryResult<PanicbuyingEventEntity> result = new PagedQueryResult<PanicbuyingEventEntity>();
            List<PanicbuyingEventEntity> list = new List<PanicbuyingEventEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    PanicbuyingEventEntity m;
                    this.LoadEvent(rdr, out m);
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

        public PagedQueryResult<PanicbuyingEventEntity> GetPanicbuyingEventList(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [EventId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [VwPanicBuyingEvent] WITH(NOLOCK) where 1=1 and EventStatus='���ϼ�'");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [VwPanicBuyingEvent] WITH(NOLOCK) where 1=1  and EventStatus='���ϼ�' ");
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
            PagedQueryResult<PanicbuyingEventEntity> result = new PagedQueryResult<PanicbuyingEventEntity>();
            List<PanicbuyingEventEntity> list = new List<PanicbuyingEventEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    PanicbuyingEventEntity m;
                    this.LoadPanicbuyingEvent(rdr, out m);
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
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void LoadEvent(SqlDataReader pReader, out PanicbuyingEventEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new PanicbuyingEventEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["EventId"] != DBNull.Value)
            {
                pInstance.EventId = (Guid)pReader["EventId"];
            }
            if (pReader["EventName"] != DBNull.Value)
            {
                pInstance.EventName = Convert.ToString(pReader["EventName"]);
            }
            if (pReader["EventTypeId"] != DBNull.Value)
            {
                pInstance.EventTypeId = Convert.ToInt32(pReader["EventTypeId"]);
            }
            if (pReader["BeginTime"] != DBNull.Value)
            {
                pInstance.BeginTime = Convert.ToDateTime(pReader["BeginTime"]);
            }
            if (pReader["EndTime"] != DBNull.Value)
            {
                pInstance.EndTime = Convert.ToDateTime(pReader["EndTime"]);
            }
            if (pReader["CustomerID"] != DBNull.Value)
            {
                pInstance.CustomerID = Convert.ToString(pReader["CustomerID"]);
            }

            if (pReader["EventStatus"] != DBNull.Value)
            {
                pInstance.EventStatusStr = pReader["EventStatus"].ToString();
            }
            if (pReader["Qty"] != DBNull.Value)
            {
                pInstance.Qty = Convert.ToInt32(pReader["Qty"].ToString());
            }
            if (pReader["RemainQty"] != DBNull.Value)
            {
                pInstance.RemainQty = Convert.ToInt32(pReader["RemainQty"].ToString());
            }
        }
        protected void LoadPanicbuyingEvent(SqlDataReader pReader, out PanicbuyingEventEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new PanicbuyingEventEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["EventId"] != DBNull.Value)
            {
                pInstance.EventId = (Guid)pReader["EventId"];
            }
            if (pReader["EventName"] != DBNull.Value)
            {
                pInstance.EventName = Convert.ToString(pReader["EventName"]);
            }
            if (pReader["EventTypeId"] != DBNull.Value)
            {
                pInstance.EventTypeId = Convert.ToInt32(pReader["EventTypeId"]);
            }
            if (pReader["BeginTime"] != DBNull.Value)
            {
                pInstance.BeginTime = Convert.ToDateTime(pReader["BeginTime"]);
            }
            if (pReader["EndTime"] != DBNull.Value)
            {
                pInstance.EndTime = Convert.ToDateTime(pReader["EndTime"]);
            }
            if (pReader["CustomerID"] != DBNull.Value)
            {
                pInstance.CustomerID = Convert.ToString(pReader["CustomerID"]);
            }

            if (pReader["EventStatus"] != DBNull.Value)
            {
                pInstance.EventStatusStr = pReader["EventStatus"].ToString();
            }
  
        }
         public DataSet GetPanicbuyingEvent(string pEvenid)
        {
            string str = "select BeginTime,EndTime  from PanicbuyingEvent where EventId='" + pEvenid + "'";
            return this.SQLHelper.ExecuteDataset(str);
        
        }

        /// <summary>
        /// ��ȡ�����
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public PanicbuyingEventEntity GetPanicbuyingEventDetails(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"select * from [PanicbuyingEvent] p
                                        inner join VwPanicBuyingEvent vwp on vwp.EventId=p.EventId
                                        where p.EventId='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            PanicbuyingEventEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                    if (rdr["Qty"] != DBNull.Value)
                    {
                        m.Qty = Convert.ToInt32(rdr["Qty"]);
                    }
                    if (rdr["RemainQty"] != DBNull.Value)
                    {
                        m.RemainQty = Convert.ToInt32(rdr["RemainQty"]);
                    }
                    break;
                }
            }
            //����
            return m;
        }
    }
}
