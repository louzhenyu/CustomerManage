/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/19 15:54:11
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
    /// ��R_WxO2OPanel_7Days�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class R_WxO2OPanel_7DaysDAO : Base.BaseCPOSDAO, ICRUDable<R_WxO2OPanel_7DaysEntity>, IQueryable<R_WxO2OPanel_7DaysEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public R_WxO2OPanel_7DaysDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(R_WxO2OPanel_7DaysEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(R_WxO2OPanel_7DaysEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");

            //��ʼ���̶��ֶ�
            pEntity.CreateTime = DateTime.Now;


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [R_WxO2OPanel_7Days](");
            strSql.Append("[CustomerId],[DateCode],[WxUV],[OfflineUV],[WxOrderPayCount],[OfflineOrderPayCount],[WxOrderPayMoney],[OfflineOrderPayMoney],[WxOrderAVG],[OfflineOrderAVG],[CreateTime],[LogIDs],[ID])");
            strSql.Append(" values (");
            strSql.Append("@CustomerId,@DateCode,@WxUV,@OfflineUV,@WxOrderPayCount,@OfflineOrderPayCount,@WxOrderPayMoney,@OfflineOrderPayMoney,@WxOrderAVG,@OfflineOrderAVG,@CreateTime,@LogIDs,@ID)");

            Guid? pkGuid;
            if (pEntity.ID == null)
                pkGuid = Guid.NewGuid();
            else
                pkGuid = pEntity.ID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@DateCode",SqlDbType.Date),
					new SqlParameter("@WxUV",SqlDbType.Int),
					new SqlParameter("@OfflineUV",SqlDbType.Int),
					new SqlParameter("@WxOrderPayCount",SqlDbType.Int),
					new SqlParameter("@OfflineOrderPayCount",SqlDbType.Int),
					new SqlParameter("@WxOrderPayMoney",SqlDbType.Decimal),
					new SqlParameter("@OfflineOrderPayMoney",SqlDbType.Decimal),
					new SqlParameter("@WxOrderAVG",SqlDbType.Decimal),
					new SqlParameter("@OfflineOrderAVG",SqlDbType.Decimal),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LogIDs",SqlDbType.NVarChar),
					new SqlParameter("@ID",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.CustomerId;
            parameters[1].Value = pEntity.DateCode;
            parameters[2].Value = pEntity.WxUV;
            parameters[3].Value = pEntity.OfflineUV;
            parameters[4].Value = pEntity.WxOrderPayCount;
            parameters[5].Value = pEntity.OfflineOrderPayCount;
            parameters[6].Value = pEntity.WxOrderPayMoney;
            parameters[7].Value = pEntity.OfflineOrderPayMoney;
            parameters[8].Value = pEntity.WxOrderAVG;
            parameters[9].Value = pEntity.OfflineOrderAVG;
            parameters[10].Value = pEntity.CreateTime;
            parameters[11].Value = pEntity.LogIDs;
            parameters[12].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            pEntity.ID = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public R_WxO2OPanel_7DaysEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [R_WxO2OPanel_7Days] where ID='{0}'  ", id.ToString());
            //��ȡ����
            R_WxO2OPanel_7DaysEntity m = null;
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
        public R_WxO2OPanel_7DaysEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [R_WxO2OPanel_7Days] where 1=1 ");
            //��ȡ����
            List<R_WxO2OPanel_7DaysEntity> list = new List<R_WxO2OPanel_7DaysEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    R_WxO2OPanel_7DaysEntity m;
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
        public void Update(R_WxO2OPanel_7DaysEntity pEntity, IDbTransaction pTran)
        {
            Update(pEntity, pTran, true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(R_WxO2OPanel_7DaysEntity pEntity, IDbTransaction pTran, bool pIsUpdateNullField)
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
            strSql.Append("update [R_WxO2OPanel_7Days] set ");
            if (pIsUpdateNullField || pEntity.CustomerId != null)
                strSql.Append("[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.DateCode != null)
                strSql.Append("[DateCode]=@DateCode,");
            if (pIsUpdateNullField || pEntity.WxUV != null)
                strSql.Append("[WxUV]=@WxUV,");
            if (pIsUpdateNullField || pEntity.OfflineUV != null)
                strSql.Append("[OfflineUV]=@OfflineUV,");
            if (pIsUpdateNullField || pEntity.WxOrderPayCount != null)
                strSql.Append("[WxOrderPayCount]=@WxOrderPayCount,");
            if (pIsUpdateNullField || pEntity.OfflineOrderPayCount != null)
                strSql.Append("[OfflineOrderPayCount]=@OfflineOrderPayCount,");
            if (pIsUpdateNullField || pEntity.WxOrderPayMoney != null)
                strSql.Append("[WxOrderPayMoney]=@WxOrderPayMoney,");
            if (pIsUpdateNullField || pEntity.OfflineOrderPayMoney != null)
                strSql.Append("[OfflineOrderPayMoney]=@OfflineOrderPayMoney,");
            if (pIsUpdateNullField || pEntity.WxOrderAVG != null)
                strSql.Append("[WxOrderAVG]=@WxOrderAVG,");
            if (pIsUpdateNullField || pEntity.OfflineOrderAVG != null)
                strSql.Append("[OfflineOrderAVG]=@OfflineOrderAVG,");
            if (pIsUpdateNullField || pEntity.LogIDs != null)
                strSql.Append("[LogIDs]=@LogIDs");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@DateCode",SqlDbType.Date),
					new SqlParameter("@WxUV",SqlDbType.Int),
					new SqlParameter("@OfflineUV",SqlDbType.Int),
					new SqlParameter("@WxOrderPayCount",SqlDbType.Int),
					new SqlParameter("@OfflineOrderPayCount",SqlDbType.Int),
					new SqlParameter("@WxOrderPayMoney",SqlDbType.Decimal),
					new SqlParameter("@OfflineOrderPayMoney",SqlDbType.Decimal),
					new SqlParameter("@WxOrderAVG",SqlDbType.Decimal),
					new SqlParameter("@OfflineOrderAVG",SqlDbType.Decimal),
					new SqlParameter("@LogIDs",SqlDbType.NVarChar),
					new SqlParameter("@ID",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.CustomerId;
            parameters[1].Value = pEntity.DateCode;
            parameters[2].Value = pEntity.WxUV;
            parameters[3].Value = pEntity.OfflineUV;
            parameters[4].Value = pEntity.WxOrderPayCount;
            parameters[5].Value = pEntity.OfflineOrderPayCount;
            parameters[6].Value = pEntity.WxOrderPayMoney;
            parameters[7].Value = pEntity.OfflineOrderPayMoney;
            parameters[8].Value = pEntity.WxOrderAVG;
            parameters[9].Value = pEntity.OfflineOrderAVG;
            parameters[10].Value = pEntity.LogIDs;
            parameters[11].Value = pEntity.ID;

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
        public void Update(R_WxO2OPanel_7DaysEntity pEntity)
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(R_WxO2OPanel_7DaysEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(R_WxO2OPanel_7DaysEntity pEntity, IDbTransaction pTran)
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
                return;
            //��֯������SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [R_WxO2OPanel_7Days] set  where ID=@ID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@ID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(R_WxO2OPanel_7DaysEntity[] pEntities, IDbTransaction pTran)
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
        public void Delete(R_WxO2OPanel_7DaysEntity[] pEntities)
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
            sql.AppendLine("update [R_WxO2OPanel_7Days] set  where ID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public R_WxO2OPanel_7DaysEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [R_WxO2OPanel_7Days] where 1=1  ");
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
            List<R_WxO2OPanel_7DaysEntity> list = new List<R_WxO2OPanel_7DaysEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    R_WxO2OPanel_7DaysEntity m;
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
        public PagedQueryResult<R_WxO2OPanel_7DaysEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [ID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [R_WxO2OPanel_7Days] where 1=1  ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [R_WxO2OPanel_7Days] where 1=1  ");
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
            PagedQueryResult<R_WxO2OPanel_7DaysEntity> result = new PagedQueryResult<R_WxO2OPanel_7DaysEntity>();
            List<R_WxO2OPanel_7DaysEntity> list = new List<R_WxO2OPanel_7DaysEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    R_WxO2OPanel_7DaysEntity m;
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
        public R_WxO2OPanel_7DaysEntity[] QueryByEntity(R_WxO2OPanel_7DaysEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<R_WxO2OPanel_7DaysEntity> PagedQueryByEntity(R_WxO2OPanel_7DaysEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(R_WxO2OPanel_7DaysEntity pQueryEntity)
        {
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.ID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ID", Value = pQueryEntity.ID });
            if (pQueryEntity.CustomerId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
            if (pQueryEntity.DateCode != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DateCode", Value = pQueryEntity.DateCode });
            if (pQueryEntity.WxUV != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WxUV", Value = pQueryEntity.WxUV });
            if (pQueryEntity.OfflineUV != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OfflineUV", Value = pQueryEntity.OfflineUV });
            if (pQueryEntity.WxOrderPayCount != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WxOrderPayCount", Value = pQueryEntity.WxOrderPayCount });
            if (pQueryEntity.OfflineOrderPayCount != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OfflineOrderPayCount", Value = pQueryEntity.OfflineOrderPayCount });
            if (pQueryEntity.WxOrderPayMoney != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WxOrderPayMoney", Value = pQueryEntity.WxOrderPayMoney });
            if (pQueryEntity.OfflineOrderPayMoney != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OfflineOrderPayMoney", Value = pQueryEntity.OfflineOrderPayMoney });
            if (pQueryEntity.WxOrderAVG != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WxOrderAVG", Value = pQueryEntity.WxOrderAVG });
            if (pQueryEntity.OfflineOrderAVG != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OfflineOrderAVG", Value = pQueryEntity.OfflineOrderAVG });
            if (pQueryEntity.CreateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.LogIDs != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LogIDs", Value = pQueryEntity.LogIDs });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(IDataReader pReader, out R_WxO2OPanel_7DaysEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new R_WxO2OPanel_7DaysEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["ID"] != DBNull.Value)
            {
                pInstance.ID = (Guid)pReader["ID"];
            }
            if (pReader["CustomerId"] != DBNull.Value)
            {
                pInstance.CustomerId = Convert.ToString(pReader["CustomerId"]);
            }
            if (pReader["DateCode"] != DBNull.Value)
            {
                pInstance.DateCode = (DateTime?)pReader["DateCode"];
            }
            if (pReader["WxUV"] != DBNull.Value)
            {
                pInstance.WxUV = Convert.ToInt32(pReader["WxUV"]);
            }
            if (pReader["OfflineUV"] != DBNull.Value)
            {
                pInstance.OfflineUV = Convert.ToInt32(pReader["OfflineUV"]);
            }
            if (pReader["WxOrderPayCount"] != DBNull.Value)
            {
                pInstance.WxOrderPayCount = Convert.ToInt32(pReader["WxOrderPayCount"]);
            }
            if (pReader["OfflineOrderPayCount"] != DBNull.Value)
            {
                pInstance.OfflineOrderPayCount = Convert.ToInt32(pReader["OfflineOrderPayCount"]);
            }
            if (pReader["WxOrderPayMoney"] != DBNull.Value)
            {
                pInstance.WxOrderPayMoney = Convert.ToDecimal(pReader["WxOrderPayMoney"]);
            }
            if (pReader["OfflineOrderPayMoney"] != DBNull.Value)
            {
                pInstance.OfflineOrderPayMoney = Convert.ToDecimal(pReader["OfflineOrderPayMoney"]);
            }
            if (pReader["WxOrderAVG"] != DBNull.Value)
            {
                pInstance.WxOrderAVG = Convert.ToDecimal(pReader["WxOrderAVG"]);
            }
            if (pReader["OfflineOrderAVG"] != DBNull.Value)
            {
                pInstance.OfflineOrderAVG = Convert.ToDecimal(pReader["OfflineOrderAVG"]);
            }
            if (pReader["CreateTime"] != DBNull.Value)
            {
                pInstance.CreateTime = Convert.ToDateTime(pReader["CreateTime"]);
            }
            if (pReader["LogIDs"] != DBNull.Value)
            {
                pInstance.LogIDs = Convert.ToString(pReader["LogIDs"]);
            }

        }
        #endregion
    }
}
