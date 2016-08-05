/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/27 14:28:44
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
    /// ��VipCardGradeChangeLog�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class VipCardGradeChangeLogDAO : Base.BaseCPOSDAO, ICRUDable<VipCardGradeChangeLogEntity>, IQueryable<VipCardGradeChangeLogEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VipCardGradeChangeLogDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(VipCardGradeChangeLogEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(VipCardGradeChangeLogEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [VipCardGradeChangeLog](");
            strSql.Append("[VipCardUpgradeRuleId],[OrderType],[OrderId],[VipCardID],[ChangeBeforeVipCardID],[ChangeBeforeGradeID],[NowGradeID],[ChangeReason],[OperationType],[ChangeTime],[UnitID],[OperationUserID],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[CustomerID],[ChangeLogID])");
            strSql.Append(" values (");
            strSql.Append("@VipCardUpgradeRuleId,@OrderType,@OrderId,@VipCardID,@ChangeBeforeVipCardID,@ChangeBeforeGradeID,@NowGradeID,@ChangeReason,@OperationType,@ChangeTime,@UnitID,@OperationUserID,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@CustomerID,@ChangeLogID)");

            string pkString = pEntity.ChangeLogID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@VipCardUpgradeRuleId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@OrderType",SqlDbType.VarChar),
					new SqlParameter("@OrderId",SqlDbType.NVarChar),
					new SqlParameter("@VipCardID",SqlDbType.NVarChar),
					new SqlParameter("@ChangeBeforeVipCardID",SqlDbType.NVarChar),
					new SqlParameter("@ChangeBeforeGradeID",SqlDbType.Int),
					new SqlParameter("@NowGradeID",SqlDbType.Int),
					new SqlParameter("@ChangeReason",SqlDbType.NVarChar),
					new SqlParameter("@OperationType",SqlDbType.Int),
					new SqlParameter("@ChangeTime",SqlDbType.DateTime),
					new SqlParameter("@UnitID",SqlDbType.NVarChar),
					new SqlParameter("@OperationUserID",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@ChangeLogID",SqlDbType.NVarChar)
            };
            parameters[0].Value = pEntity.VipCardUpgradeRuleId;
            parameters[1].Value = pEntity.OrderType;
            parameters[2].Value = pEntity.OrderId;
            parameters[3].Value = pEntity.VipCardID;
            parameters[4].Value = pEntity.ChangeBeforeVipCardID;
            parameters[5].Value = pEntity.ChangeBeforeGradeID;
            parameters[6].Value = pEntity.NowGradeID;
            parameters[7].Value = pEntity.ChangeReason;
            parameters[8].Value = pEntity.OperationType;
            parameters[9].Value = pEntity.ChangeTime;
            parameters[10].Value = pEntity.UnitID;
            parameters[11].Value = pEntity.OperationUserID;
            parameters[12].Value = pEntity.CreateTime;
            parameters[13].Value = pEntity.CreateBy;
            parameters[14].Value = pEntity.LastUpdateTime;
            parameters[15].Value = pEntity.LastUpdateBy;
            parameters[16].Value = pEntity.IsDelete;
            parameters[17].Value = pEntity.CustomerID;
            parameters[18].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            pEntity.ChangeLogID = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public VipCardGradeChangeLogEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCardGradeChangeLog] where ChangeLogID='{0}'  and isdelete=0 ", id.ToString());
            //��ȡ����
            VipCardGradeChangeLogEntity m = null;
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
        public VipCardGradeChangeLogEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCardGradeChangeLog] where 1=1  and isdelete=0");
            //��ȡ����
            List<VipCardGradeChangeLogEntity> list = new List<VipCardGradeChangeLogEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardGradeChangeLogEntity m;
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
        public void Update(VipCardGradeChangeLogEntity pEntity, IDbTransaction pTran)
        {
            Update(pEntity, pTran, false);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(VipCardGradeChangeLogEntity pEntity, IDbTransaction pTran, bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ChangeLogID == null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [VipCardGradeChangeLog] set ");
            if (pIsUpdateNullField || pEntity.VipCardUpgradeRuleId != null)
                strSql.Append("[VipCardUpgradeRuleId]=@VipCardUpgradeRuleId,");
            if (pIsUpdateNullField || pEntity.OrderType != null)
                strSql.Append("[OrderType]=@OrderType,");
            if (pIsUpdateNullField || pEntity.OrderId != null)
                strSql.Append("[OrderId]=@OrderId,");
            if (pIsUpdateNullField || pEntity.VipCardID != null)
                strSql.Append("[VipCardID]=@VipCardID,");
            if (pIsUpdateNullField || pEntity.ChangeBeforeVipCardID != null)
                strSql.Append("[ChangeBeforeVipCardID]=@ChangeBeforeVipCardID,");
            if (pIsUpdateNullField || pEntity.ChangeBeforeGradeID != null)
                strSql.Append("[ChangeBeforeGradeID]=@ChangeBeforeGradeID,");
            if (pIsUpdateNullField || pEntity.NowGradeID != null)
                strSql.Append("[NowGradeID]=@NowGradeID,");
            if (pIsUpdateNullField || pEntity.ChangeReason != null)
                strSql.Append("[ChangeReason]=@ChangeReason,");
            if (pIsUpdateNullField || pEntity.OperationType != null)
                strSql.Append("[OperationType]=@OperationType,");
            if (pIsUpdateNullField || pEntity.ChangeTime != null)
                strSql.Append("[ChangeTime]=@ChangeTime,");
            if (pIsUpdateNullField || pEntity.UnitID != null)
                strSql.Append("[UnitID]=@UnitID,");
            if (pIsUpdateNullField || pEntity.OperationUserID != null)
                strSql.Append("[OperationUserID]=@OperationUserID,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime != null)
                strSql.Append("[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy != null)
                strSql.Append("[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.CustomerID != null)
                strSql.Append("[CustomerID]=@CustomerID");
            strSql.Append(" where ChangeLogID=@ChangeLogID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@VipCardUpgradeRuleId",SqlDbType.UniqueIdentifier),
					new SqlParameter("@OrderType",SqlDbType.VarChar),
					new SqlParameter("@OrderId",SqlDbType.NVarChar),
					new SqlParameter("@VipCardID",SqlDbType.NVarChar),
					new SqlParameter("@ChangeBeforeVipCardID",SqlDbType.NVarChar),
					new SqlParameter("@ChangeBeforeGradeID",SqlDbType.Int),
					new SqlParameter("@NowGradeID",SqlDbType.Int),
					new SqlParameter("@ChangeReason",SqlDbType.NVarChar),
					new SqlParameter("@OperationType",SqlDbType.Int),
					new SqlParameter("@ChangeTime",SqlDbType.DateTime),
					new SqlParameter("@UnitID",SqlDbType.NVarChar),
					new SqlParameter("@OperationUserID",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@ChangeLogID",SqlDbType.NVarChar)
            };
            parameters[0].Value = pEntity.VipCardUpgradeRuleId;
            parameters[1].Value = pEntity.OrderType;
            parameters[2].Value = pEntity.OrderId;
            parameters[3].Value = pEntity.VipCardID;
            parameters[4].Value = pEntity.ChangeBeforeVipCardID;
            parameters[5].Value = pEntity.ChangeBeforeGradeID;
            parameters[6].Value = pEntity.NowGradeID;
            parameters[7].Value = pEntity.ChangeReason;
            parameters[8].Value = pEntity.OperationType;
            parameters[9].Value = pEntity.ChangeTime;
            parameters[10].Value = pEntity.UnitID;
            parameters[11].Value = pEntity.OperationUserID;
            parameters[12].Value = pEntity.LastUpdateTime;
            parameters[13].Value = pEntity.LastUpdateBy;
            parameters[14].Value = pEntity.CustomerID;
            parameters[15].Value = pEntity.ChangeLogID;

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
        public void Update(VipCardGradeChangeLogEntity pEntity)
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(VipCardGradeChangeLogEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(VipCardGradeChangeLogEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ChangeLogID == null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.ChangeLogID, pTran);
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
            sql.AppendLine("update [VipCardGradeChangeLog] set  isdelete=1 where ChangeLogID=@ChangeLogID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@ChangeLogID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(VipCardGradeChangeLogEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (pEntity.ChangeLogID == null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.ChangeLogID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(VipCardGradeChangeLogEntity[] pEntities)
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
            sql.AppendLine("update [VipCardGradeChangeLog] set  isdelete=1 where ChangeLogID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public VipCardGradeChangeLogEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCardGradeChangeLog] where 1=1  and isdelete=0 ");
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
            List<VipCardGradeChangeLogEntity> list = new List<VipCardGradeChangeLogEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardGradeChangeLogEntity m;
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
        public PagedQueryResult<VipCardGradeChangeLogEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [ChangeLogID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [VipCardGradeChangeLog] where 1=1  and isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [VipCardGradeChangeLog] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<VipCardGradeChangeLogEntity> result = new PagedQueryResult<VipCardGradeChangeLogEntity>();
            List<VipCardGradeChangeLogEntity> list = new List<VipCardGradeChangeLogEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardGradeChangeLogEntity m;
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
        public VipCardGradeChangeLogEntity[] QueryByEntity(VipCardGradeChangeLogEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<VipCardGradeChangeLogEntity> PagedQueryByEntity(VipCardGradeChangeLogEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(VipCardGradeChangeLogEntity pQueryEntity)
        {
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.ChangeLogID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ChangeLogID", Value = pQueryEntity.ChangeLogID });
            if (pQueryEntity.VipCardUpgradeRuleId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardUpgradeRuleId", Value = pQueryEntity.VipCardUpgradeRuleId });
            if (pQueryEntity.OrderType != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderType", Value = pQueryEntity.OrderType });
            if (pQueryEntity.OrderId != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderId", Value = pQueryEntity.OrderId });
            if (pQueryEntity.VipCardID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardID", Value = pQueryEntity.VipCardID });
            if (pQueryEntity.ChangeBeforeVipCardID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ChangeBeforeVipCardID", Value = pQueryEntity.ChangeBeforeVipCardID });
            if (pQueryEntity.ChangeBeforeGradeID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ChangeBeforeGradeID", Value = pQueryEntity.ChangeBeforeGradeID });
            if (pQueryEntity.NowGradeID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NowGradeID", Value = pQueryEntity.NowGradeID });
            if (pQueryEntity.ChangeReason != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ChangeReason", Value = pQueryEntity.ChangeReason });
            if (pQueryEntity.OperationType != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OperationType", Value = pQueryEntity.OperationType });
            if (pQueryEntity.ChangeTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ChangeTime", Value = pQueryEntity.ChangeTime });
            if (pQueryEntity.UnitID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitID", Value = pQueryEntity.UnitID });
            if (pQueryEntity.OperationUserID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OperationUserID", Value = pQueryEntity.OperationUserID });
            if (pQueryEntity.CreateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.LastUpdateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.LastUpdateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.IsDelete != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });
            if (pQueryEntity.CustomerID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(IDataReader pReader, out VipCardGradeChangeLogEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new VipCardGradeChangeLogEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["ChangeLogID"] != DBNull.Value)
            {
                pInstance.ChangeLogID = Convert.ToString(pReader["ChangeLogID"]);
            }
            if (pReader["VipCardUpgradeRuleId"] != DBNull.Value)
            {
                pInstance.VipCardUpgradeRuleId = (Guid)pReader["VipCardUpgradeRuleId"];
            }
            if (pReader["OrderType"] != DBNull.Value)
            {
                pInstance.OrderType = Convert.ToString(pReader["OrderType"]);
            }
            if (pReader["OrderId"] != DBNull.Value)
            {
                pInstance.OrderId = Convert.ToString(pReader["OrderId"]);
            }
            if (pReader["VipCardID"] != DBNull.Value)
            {
                pInstance.VipCardID = Convert.ToString(pReader["VipCardID"]);
            }
            if (pReader["ChangeBeforeVipCardID"] != DBNull.Value)
            {
                pInstance.ChangeBeforeVipCardID = Convert.ToString(pReader["ChangeBeforeVipCardID"]);
            }
            if (pReader["ChangeBeforeGradeID"] != DBNull.Value)
            {
                pInstance.ChangeBeforeGradeID = Convert.ToInt32(pReader["ChangeBeforeGradeID"]);
            }
            if (pReader["NowGradeID"] != DBNull.Value)
            {
                pInstance.NowGradeID = Convert.ToInt32(pReader["NowGradeID"]);
            }
            if (pReader["ChangeReason"] != DBNull.Value)
            {
                pInstance.ChangeReason = Convert.ToString(pReader["ChangeReason"]);
            }
            if (pReader["OperationType"] != DBNull.Value)
            {
                pInstance.OperationType = Convert.ToInt32(pReader["OperationType"]);
            }
            if (pReader["ChangeTime"] != DBNull.Value)
            {
                pInstance.ChangeTime = Convert.ToDateTime(pReader["ChangeTime"]);
            }
            if (pReader["UnitID"] != DBNull.Value)
            {
                pInstance.UnitID = Convert.ToString(pReader["UnitID"]);
            }
            if (pReader["OperationUserID"] != DBNull.Value)
            {
                pInstance.OperationUserID = Convert.ToString(pReader["OperationUserID"]);
            }
            if (pReader["CreateTime"] != DBNull.Value)
            {
                pInstance.CreateTime = Convert.ToDateTime(pReader["CreateTime"]);
            }
            if (pReader["CreateBy"] != DBNull.Value)
            {
                pInstance.CreateBy = Convert.ToString(pReader["CreateBy"]);
            }
            if (pReader["LastUpdateTime"] != DBNull.Value)
            {
                pInstance.LastUpdateTime = Convert.ToDateTime(pReader["LastUpdateTime"]);
            }
            if (pReader["LastUpdateBy"] != DBNull.Value)
            {
                pInstance.LastUpdateBy = Convert.ToString(pReader["LastUpdateBy"]);
            }
            if (pReader["IsDelete"] != DBNull.Value)
            {
                pInstance.IsDelete = Convert.ToInt32(pReader["IsDelete"]);
            }
            if (pReader["CustomerID"] != DBNull.Value)
            {
                pInstance.CustomerID = Convert.ToString(pReader["CustomerID"]);
            }

        }
        #endregion
    }
}
