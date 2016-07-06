/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/17 13:43:21
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
using JIT.CPOS.BS.Entity.SAP;

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// ���ݷ��ʣ� ���б�����֪ͨ���������ң�֧�����Զ�� 
    /// ��DataQueue�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class DataQueueDAO : Base.BaseCPOSDAO, ICRUDable<DataQueueEntity>, IQueryable<DataQueueEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public DataQueueDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(DataQueueEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(DataQueueEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");

            //��ʼ���̶��ֶ�


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [DataQueue](");
            strSql.Append("[IsComplete],[ObjectType],[CustomerId],[TransType],[FieldNames],[FieldValues],[FieldsInKey],[ConsumptionCount],[PrevTime],[LastTime],[ErrorMsg],[NextTime],[Value],[Flied1],[Flied2],[Flied3],[Flied4],[Flied5])");
            strSql.Append(" values (");
            strSql.Append("@IsComplete,@ObjectType,@CustomerId,@TransType,@FieldNames,@FieldValues,@FieldsInKey,@ConsumptionCount,@PrevTime,@LastTime,@ErrorMsg,@NextTime,@Value,@Flied1,@Flied2,@Flied3,@Flied4,@Flied5)");
            strSql.AppendFormat("{0}select SCOPE_IDENTITY();", Environment.NewLine);


            SqlParameter[] parameters =
            {
                    new SqlParameter("@IsComplete",SqlDbType.Int),
                    new SqlParameter("@ObjectType",SqlDbType.VarChar),
                    new SqlParameter("@CustomerId",SqlDbType.VarChar),
                    new SqlParameter("@TransType",SqlDbType.VarChar),
                    new SqlParameter("@FieldNames",SqlDbType.VarChar),
                    new SqlParameter("@FieldValues",SqlDbType.VarChar),
                    new SqlParameter("@FieldsInKey",SqlDbType.Int),
                    new SqlParameter("@ConsumptionCount",SqlDbType.Int),
                    new SqlParameter("@PrevTime",SqlDbType.VarChar),
                    new SqlParameter("@LastTime",SqlDbType.VarChar),
                    new SqlParameter("@ErrorMsg",SqlDbType.VarChar),
                    new SqlParameter("@NextTime",SqlDbType.DateTime),
                    new SqlParameter("@Value",SqlDbType.VarChar),
                    new SqlParameter("@Flied1",SqlDbType.VarChar),
                    new SqlParameter("@Flied2",SqlDbType.VarChar),
                    new SqlParameter("@Flied3",SqlDbType.VarChar),
                    new SqlParameter("@Flied4",SqlDbType.VarChar),
                    new SqlParameter("@Flied5",SqlDbType.VarChar)
            };
            parameters[0].Value = pEntity.IsComplete;
            parameters[1].Value = pEntity.ObjectType;
            parameters[2].Value = pEntity.CustomerId;
            parameters[3].Value = pEntity.TransType;
            parameters[4].Value = pEntity.FieldNames;
            parameters[5].Value = pEntity.FieldValues;
            parameters[6].Value = pEntity.FieldsInKey;
            parameters[7].Value = pEntity.ConsumptionCount;
            parameters[8].Value = pEntity.PrevTime;
            parameters[9].Value = pEntity.LastTime;
            parameters[10].Value = pEntity.ErrorMsg;
            parameters[11].Value = pEntity.NextTime;
            parameters[12].Value = pEntity.Value;
            parameters[13].Value = pEntity.Flied1;
            parameters[14].Value = pEntity.Flied2;
            parameters[15].Value = pEntity.Flied3;
            parameters[16].Value = pEntity.Flied4;
            parameters[17].Value = pEntity.Flied5;

            //ִ�в��������д
            object result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteScalar((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters);
            pEntity.Id = Convert.ToInt32(result);

        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public bool Insert(DataQueueEntity pEntity)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");

            //��ʼ���̶��ֶ�


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [DataQueue](");
            strSql.Append("[IsComplete],[ObjectType],[CustomerId],[TransType],[FieldNames],[FieldValues],[FieldsInKey],[ConsumptionCount],[PrevTime],[LastTime],[ErrorMsg],[NextTime],[Value],[Flied1],[Flied2],[Flied3],[Flied4],[Flied5])");
            strSql.Append(" values (");
            strSql.Append("@IsComplete,@ObjectType,@CustomerId,@TransType,@FieldNames,@FieldValues,@FieldsInKey,@ConsumptionCount,@PrevTime,@LastTime,@ErrorMsg,@NextTime,@Value,@Flied1,@Flied2,@Flied3,@Flied4,@Flied5)");
            strSql.AppendFormat("{0}select SCOPE_IDENTITY();", Environment.NewLine);


            SqlParameter[] parameters =
            {
                    new SqlParameter("@IsComplete",SqlDbType.Int),
                    new SqlParameter("@ObjectType",SqlDbType.VarChar),
                    new SqlParameter("@CustomerId",SqlDbType.VarChar),
                    new SqlParameter("@TransType",SqlDbType.VarChar),
                    new SqlParameter("@FieldNames",SqlDbType.VarChar),
                    new SqlParameter("@FieldValues",SqlDbType.VarChar),
                    new SqlParameter("@FieldsInKey",SqlDbType.Int),
                    new SqlParameter("@ConsumptionCount",SqlDbType.Int),
                    new SqlParameter("@PrevTime",SqlDbType.VarChar),
                    new SqlParameter("@LastTime",SqlDbType.VarChar),
                    new SqlParameter("@ErrorMsg",SqlDbType.VarChar),
                    new SqlParameter("@NextTime",SqlDbType.DateTime),
                    new SqlParameter("@Value",SqlDbType.VarChar),
                    new SqlParameter("@Flied1",SqlDbType.VarChar),
                    new SqlParameter("@Flied2",SqlDbType.VarChar),
                    new SqlParameter("@Flied3",SqlDbType.VarChar),
                    new SqlParameter("@Flied4",SqlDbType.VarChar),
                    new SqlParameter("@Flied5",SqlDbType.VarChar)
            };
            parameters[0].Value = pEntity.IsComplete;
            parameters[1].Value = pEntity.ObjectType;
            parameters[2].Value = pEntity.CustomerId;
            parameters[3].Value = pEntity.TransType;
            parameters[4].Value = pEntity.FieldNames;
            parameters[5].Value = pEntity.FieldValues;
            parameters[6].Value = pEntity.FieldsInKey;
            parameters[7].Value = pEntity.ConsumptionCount;
            parameters[8].Value = pEntity.PrevTime;
            parameters[9].Value = pEntity.LastTime;
            parameters[10].Value = pEntity.ErrorMsg;
            parameters[11].Value = pEntity.NextTime;
            parameters[12].Value = pEntity.Value;
            parameters[13].Value = pEntity.Flied1;
            parameters[14].Value = pEntity.Flied2;
            parameters[15].Value = pEntity.Flied3;
            parameters[16].Value = pEntity.Flied4;
            parameters[17].Value = pEntity.Flied5;

            //
            int flag = 0;
            flag = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            return flag > 0;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public DataQueueEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [DataQueue] where Id='{0}'  ", id.ToString());
            //��ȡ����
            DataQueueEntity m = null;
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
        public DataQueueEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [DataQueue] where 1=1 ");
            //��ȡ����
            List<DataQueueEntity> list = new List<DataQueueEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    DataQueueEntity m;
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
        public void Update(DataQueueEntity pEntity, IDbTransaction pTran)
        {
            Update(pEntity, pTran, true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(DataQueueEntity pEntity, IDbTransaction pTran, bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            //��ʼ���̶��ֶ�


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [DataQueue] set ");
            if (pIsUpdateNullField || pEntity.IsComplete != null)
                strSql.Append("[IsComplete]=@IsComplete,");
            if (pIsUpdateNullField || pEntity.ObjectType != null)
                strSql.Append("[ObjectType]=@ObjectType,");
            if (pIsUpdateNullField || pEntity.CustomerId != null)
                strSql.Append("[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.TransType != null)
                strSql.Append("[TransType]=@TransType,");
            if (pIsUpdateNullField || pEntity.FieldNames != null)
                strSql.Append("[FieldNames]=@FieldNames,");
            if (pIsUpdateNullField || pEntity.FieldValues != null)
                strSql.Append("[FieldValues]=@FieldValues,");
            if (pIsUpdateNullField || pEntity.FieldsInKey != null)
                strSql.Append("[FieldsInKey]=@FieldsInKey,");
            if (pIsUpdateNullField || pEntity.ConsumptionCount != null)
                strSql.Append("[ConsumptionCount]=@ConsumptionCount,");
            if (pIsUpdateNullField || pEntity.PrevTime != null)
                strSql.Append("[PrevTime]=@PrevTime,");
            if (pIsUpdateNullField || pEntity.LastTime != null)
                strSql.Append("[LastTime]=@LastTime,");
            if (pIsUpdateNullField || pEntity.ErrorMsg != null)
                strSql.Append("[ErrorMsg]=@ErrorMsg,");
            if (pIsUpdateNullField || pEntity.NextTime != null)
                strSql.Append("[NextTime]=@NextTime,");
            if (pIsUpdateNullField || pEntity.Value != null)
                strSql.Append("[Value]=@Value,");
            if (pIsUpdateNullField || pEntity.Flied1 != null)
                strSql.Append("[Flied1]=@Flied1,");
            if (pIsUpdateNullField || pEntity.Flied2 != null)
                strSql.Append("[Flied2]=@Flied2,");
            if (pIsUpdateNullField || pEntity.Flied3 != null)
                strSql.Append("[Flied3]=@Flied3,");
            if (pIsUpdateNullField || pEntity.Flied4 != null)
                strSql.Append("[Flied4]=@Flied4,");
            if (pIsUpdateNullField || pEntity.Flied5 != null)
                strSql.Append("[Flied5]=@Flied5");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters =
            {
                    new SqlParameter("@IsComplete",SqlDbType.Int),
                    new SqlParameter("@ObjectType",SqlDbType.VarChar),
                    new SqlParameter("@CustomerId",SqlDbType.VarChar),
                    new SqlParameter("@TransType",SqlDbType.VarChar),
                    new SqlParameter("@FieldNames",SqlDbType.VarChar),
                    new SqlParameter("@FieldValues",SqlDbType.VarChar),
                    new SqlParameter("@FieldsInKey",SqlDbType.Int),
                    new SqlParameter("@ConsumptionCount",SqlDbType.Int),
                    new SqlParameter("@PrevTime",SqlDbType.VarChar),
                    new SqlParameter("@LastTime",SqlDbType.VarChar),
                    new SqlParameter("@ErrorMsg",SqlDbType.VarChar),
                    new SqlParameter("@NextTime",SqlDbType.DateTime),
                    new SqlParameter("@Value",SqlDbType.VarChar),
                    new SqlParameter("@Flied1",SqlDbType.VarChar),
                    new SqlParameter("@Flied2",SqlDbType.VarChar),
                    new SqlParameter("@Flied3",SqlDbType.VarChar),
                    new SqlParameter("@Flied4",SqlDbType.VarChar),
                    new SqlParameter("@Flied5",SqlDbType.VarChar),
                    new SqlParameter("@Id",SqlDbType.Int)
            };
            parameters[0].Value = pEntity.IsComplete;
            parameters[1].Value = pEntity.ObjectType;
            parameters[2].Value = pEntity.CustomerId;
            parameters[3].Value = pEntity.TransType;
            parameters[4].Value = pEntity.FieldNames;
            parameters[5].Value = pEntity.FieldValues;
            parameters[6].Value = pEntity.FieldsInKey;
            parameters[7].Value = pEntity.ConsumptionCount;
            parameters[8].Value = pEntity.PrevTime;
            parameters[9].Value = pEntity.LastTime;
            parameters[10].Value = pEntity.ErrorMsg;
            parameters[11].Value = pEntity.NextTime;
            parameters[12].Value = pEntity.Value;
            parameters[13].Value = pEntity.Flied1;
            parameters[14].Value = pEntity.Flied2;
            parameters[15].Value = pEntity.Flied3;
            parameters[16].Value = pEntity.Flied4;
            parameters[17].Value = pEntity.Flied5;
            parameters[18].Value = pEntity.Id;

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
        public void Update(DataQueueEntity pEntity)
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(DataQueueEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(DataQueueEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            //ִ�� 
            this.Delete(pEntity.Id, pTran);
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
            sql.AppendLine("update [DataQueue] set  where Id=@Id;");
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter{ParameterName="@Id",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(DataQueueEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                entityIDs[i] = pEntity.Id;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(DataQueueEntity[] pEntities)
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
                primaryKeys.AppendFormat("{0},", item.ToString());
            }
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [DataQueue] set  where Id in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public DataQueueEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [DataQueue] where 1=1  ");
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
            List<DataQueueEntity> list = new List<DataQueueEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    DataQueueEntity m;
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
        public PagedQueryResult<DataQueueEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [Id] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [DataQueue] where 1=1  ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [DataQueue] where 1=1  ");
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
            PagedQueryResult<DataQueueEntity> result = new PagedQueryResult<DataQueueEntity>();
            List<DataQueueEntity> list = new List<DataQueueEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    DataQueueEntity m;
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
        public DataQueueEntity[] QueryByEntity(DataQueueEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<DataQueueEntity> PagedQueryByEntity(DataQueueEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(DataQueueEntity pQueryEntity)
        {
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.Id != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Id", Value = pQueryEntity.Id });
            if (pQueryEntity.IsComplete != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsComplete", Value = pQueryEntity.IsComplete });
            if (pQueryEntity.ObjectType != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ObjectType", Value = pQueryEntity.ObjectType });
            if (pQueryEntity.CustomerId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
            if (pQueryEntity.TransType != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TransType", Value = pQueryEntity.TransType });
            if (pQueryEntity.FieldNames != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FieldNames", Value = pQueryEntity.FieldNames });
            if (pQueryEntity.FieldValues != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FieldValues", Value = pQueryEntity.FieldValues });
            if (pQueryEntity.FieldsInKey != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FieldsInKey", Value = pQueryEntity.FieldsInKey });
            if (pQueryEntity.ConsumptionCount != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ConsumptionCount", Value = pQueryEntity.ConsumptionCount });
            if (pQueryEntity.PrevTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PrevTime", Value = pQueryEntity.PrevTime });
            if (pQueryEntity.LastTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastTime", Value = pQueryEntity.LastTime });
            if (pQueryEntity.ErrorMsg != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ErrorMsg", Value = pQueryEntity.ErrorMsg });
            if (pQueryEntity.NextTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NextTime", Value = pQueryEntity.NextTime });
            if (pQueryEntity.Value != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Value", Value = pQueryEntity.Value });
            if (pQueryEntity.Flied1 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Flied1", Value = pQueryEntity.Flied1 });
            if (pQueryEntity.Flied2 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Flied2", Value = pQueryEntity.Flied2 });
            if (pQueryEntity.Flied3 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Flied3", Value = pQueryEntity.Flied3 });
            if (pQueryEntity.Flied4 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Flied4", Value = pQueryEntity.Flied4 });
            if (pQueryEntity.Flied5 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Flied5", Value = pQueryEntity.Flied5 });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(IDataReader pReader, out DataQueueEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new DataQueueEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["Id"] != DBNull.Value)
            {
                pInstance.Id = Convert.ToInt32(pReader["Id"]);
            }
            if (pReader["IsComplete"] != DBNull.Value)
            {
                pInstance.IsComplete = Convert.ToInt32(pReader["IsComplete"]);
            }
            if (pReader["ObjectType"] != DBNull.Value)
            {
                pInstance.ObjectType = Convert.ToString(pReader["ObjectType"]);
            }
            if (pReader["CustomerId"] != DBNull.Value)
            {
                pInstance.CustomerId = Convert.ToString(pReader["CustomerId"]);
            }
            if (pReader["TransType"] != DBNull.Value)
            {
                pInstance.TransType = Convert.ToString(pReader["TransType"]);
            }
            if (pReader["FieldNames"] != DBNull.Value)
            {
                pInstance.FieldNames = Convert.ToString(pReader["FieldNames"]);
            }
            if (pReader["FieldValues"] != DBNull.Value)
            {
                pInstance.FieldValues = Convert.ToString(pReader["FieldValues"]);
            }
            if (pReader["FieldsInKey"] != DBNull.Value)
            {
                pInstance.FieldsInKey = Convert.ToInt32(pReader["FieldsInKey"]);
            }
            if (pReader["ConsumptionCount"] != DBNull.Value)
            {
                pInstance.ConsumptionCount = Convert.ToInt32(pReader["ConsumptionCount"]);
            }
            if (pReader["PrevTime"] != DBNull.Value)
            {
                pInstance.PrevTime = Convert.ToString(pReader["PrevTime"]);
            }
            if (pReader["LastTime"] != DBNull.Value)
            {
                pInstance.LastTime = Convert.ToString(pReader["LastTime"]);
            }
            if (pReader["ErrorMsg"] != DBNull.Value)
            {
                pInstance.ErrorMsg = Convert.ToString(pReader["ErrorMsg"]);
            }
            if (pReader["NextTime"] != DBNull.Value)
            {
                pInstance.NextTime = Convert.ToDateTime(pReader["NextTime"]);
            }
            if (pReader["Value"] != DBNull.Value)
            {
                pInstance.Value = Convert.ToString(pReader["Value"]);
            }
            if (pReader["Flied1"] != DBNull.Value)
            {
                pInstance.Flied1 = Convert.ToString(pReader["Flied1"]);
            }
            if (pReader["Flied2"] != DBNull.Value)
            {
                pInstance.Flied2 = Convert.ToString(pReader["Flied2"]);
            }
            if (pReader["Flied3"] != DBNull.Value)
            {
                pInstance.Flied3 = Convert.ToString(pReader["Flied3"]);
            }
            if (pReader["Flied4"] != DBNull.Value)
            {
                pInstance.Flied4 = Convert.ToString(pReader["Flied4"]);
            }
            if (pReader["Flied5"] != DBNull.Value)
            {
                pInstance.Flied5 = Convert.ToString(pReader["Flied5"]);
            }

        }
        #endregion
    }
}
