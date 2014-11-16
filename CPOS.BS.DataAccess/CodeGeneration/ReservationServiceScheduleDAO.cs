/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/25 9:57:34
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
    /// ��ReservationServiceSchedule�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class ReservationServiceScheduleDAO : Base.BaseCPOSDAO, ICRUDable<ReservationServiceScheduleEntity>, IQueryable<ReservationServiceScheduleEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public ReservationServiceScheduleDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(ReservationServiceScheduleEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(ReservationServiceScheduleEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [ReservationServiceSchedule](");
            strSql.Append("[ReservationServiceID],[ReservationServiceBigClassTermID],[ReservationServiceSmallClassTermID],[VIPID],[ReserveDate],[Remark],[PositionID],[ReserveTypeID],[StatusID],[ReservationStoreID],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[ReservationServiceScheduleID])");
            strSql.Append(" values (");
            strSql.Append("@ReservationServiceID,@ReservationServiceBigClassTermID,@ReservationServiceSmallClassTermID,@VIPID,@ReserveDate,@Remark,@PositionID,@ReserveTypeID,@StatusID,@ReservationStoreID,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@ReservationServiceScheduleID)");            

			string pkString = pEntity.ReservationServiceScheduleID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@ReservationServiceID",SqlDbType.Int),
					new SqlParameter("@ReservationServiceBigClassTermID",SqlDbType.Decimal),
					new SqlParameter("@ReservationServiceSmallClassTermID",SqlDbType.Decimal),
					new SqlParameter("@VIPID",SqlDbType.VarChar),
					new SqlParameter("@ReserveDate",SqlDbType.DateTime),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@PositionID",SqlDbType.Int),
					new SqlParameter("@ReserveTypeID",SqlDbType.Int),
					new SqlParameter("@StatusID",SqlDbType.Int),
					new SqlParameter("@ReservationStoreID",SqlDbType.Int),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@ReservationServiceScheduleID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.ReservationServiceID;
			parameters[1].Value = pEntity.ReservationServiceBigClassTermID;
			parameters[2].Value = pEntity.ReservationServiceSmallClassTermID;
			parameters[3].Value = pEntity.VIPID;
			parameters[4].Value = pEntity.ReserveDate;
			parameters[5].Value = pEntity.Remark;
			parameters[6].Value = pEntity.PositionID;
			parameters[7].Value = pEntity.ReserveTypeID;
			parameters[8].Value = pEntity.StatusID;
			parameters[9].Value = pEntity.ReservationStoreID;
			parameters[10].Value = pEntity.CreateTime;
			parameters[11].Value = pEntity.CreateBy;
			parameters[12].Value = pEntity.LastUpdateTime;
			parameters[13].Value = pEntity.LastUpdateBy;
			parameters[14].Value = pEntity.IsDelete;
			parameters[15].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.ReservationServiceScheduleID = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public ReservationServiceScheduleEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ReservationServiceSchedule] where ReservationServiceScheduleID='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            ReservationServiceScheduleEntity m = null;
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
        public ReservationServiceScheduleEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ReservationServiceSchedule] where isdelete=0");
            //��ȡ����
            List<ReservationServiceScheduleEntity> list = new List<ReservationServiceScheduleEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    ReservationServiceScheduleEntity m;
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
        public void Update(ReservationServiceScheduleEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(ReservationServiceScheduleEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ReservationServiceScheduleID==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [ReservationServiceSchedule] set ");
            if (pIsUpdateNullField || pEntity.ReservationServiceID!=null)
                strSql.Append( "[ReservationServiceID]=@ReservationServiceID,");
            if (pIsUpdateNullField || pEntity.ReservationServiceBigClassTermID!=null)
                strSql.Append( "[ReservationServiceBigClassTermID]=@ReservationServiceBigClassTermID,");
            if (pIsUpdateNullField || pEntity.ReservationServiceSmallClassTermID!=null)
                strSql.Append( "[ReservationServiceSmallClassTermID]=@ReservationServiceSmallClassTermID,");
            if (pIsUpdateNullField || pEntity.VIPID!=null)
                strSql.Append( "[VIPID]=@VIPID,");
            if (pIsUpdateNullField || pEntity.ReserveDate!=null)
                strSql.Append( "[ReserveDate]=@ReserveDate,");
            if (pIsUpdateNullField || pEntity.Remark!=null)
                strSql.Append( "[Remark]=@Remark,");
            if (pIsUpdateNullField || pEntity.PositionID!=null)
                strSql.Append( "[PositionID]=@PositionID,");
            if (pIsUpdateNullField || pEntity.ReserveTypeID!=null)
                strSql.Append( "[ReserveTypeID]=@ReserveTypeID,");
            if (pIsUpdateNullField || pEntity.StatusID!=null)
                strSql.Append( "[StatusID]=@StatusID,");
            if (pIsUpdateNullField || pEntity.ReservationStoreID!=null)
                strSql.Append( "[ReservationStoreID]=@ReservationStoreID,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where ReservationServiceScheduleID=@ReservationServiceScheduleID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@ReservationServiceID",SqlDbType.Int),
					new SqlParameter("@ReservationServiceBigClassTermID",SqlDbType.Decimal),
					new SqlParameter("@ReservationServiceSmallClassTermID",SqlDbType.Decimal),
					new SqlParameter("@VIPID",SqlDbType.VarChar),
					new SqlParameter("@ReserveDate",SqlDbType.DateTime),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@PositionID",SqlDbType.Int),
					new SqlParameter("@ReserveTypeID",SqlDbType.Int),
					new SqlParameter("@StatusID",SqlDbType.Int),
					new SqlParameter("@ReservationStoreID",SqlDbType.Int),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@ReservationServiceScheduleID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.ReservationServiceID;
			parameters[1].Value = pEntity.ReservationServiceBigClassTermID;
			parameters[2].Value = pEntity.ReservationServiceSmallClassTermID;
			parameters[3].Value = pEntity.VIPID;
			parameters[4].Value = pEntity.ReserveDate;
			parameters[5].Value = pEntity.Remark;
			parameters[6].Value = pEntity.PositionID;
			parameters[7].Value = pEntity.ReserveTypeID;
			parameters[8].Value = pEntity.StatusID;
			parameters[9].Value = pEntity.ReservationStoreID;
			parameters[10].Value = pEntity.LastUpdateTime;
			parameters[11].Value = pEntity.LastUpdateBy;
			parameters[12].Value = pEntity.ReservationServiceScheduleID;

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
        public void Update(ReservationServiceScheduleEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(ReservationServiceScheduleEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(ReservationServiceScheduleEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(ReservationServiceScheduleEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ReservationServiceScheduleID==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.ReservationServiceScheduleID, pTran);           
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
            sql.AppendLine("update [ReservationServiceSchedule] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where ReservationServiceScheduleID=@ReservationServiceScheduleID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@ReservationServiceScheduleID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(ReservationServiceScheduleEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.ReservationServiceScheduleID==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.ReservationServiceScheduleID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(ReservationServiceScheduleEntity[] pEntities)
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
            sql.AppendLine("update [ReservationServiceSchedule] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where ReservationServiceScheduleID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public ReservationServiceScheduleEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ReservationServiceSchedule] where isdelete=0 ");
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
            List<ReservationServiceScheduleEntity> list = new List<ReservationServiceScheduleEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    ReservationServiceScheduleEntity m;
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
        public PagedQueryResult<ReservationServiceScheduleEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [ReservationServiceScheduleID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [ReservationServiceSchedule] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [ReservationServiceSchedule] where isdelete=0 ");
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
            PagedQueryResult<ReservationServiceScheduleEntity> result = new PagedQueryResult<ReservationServiceScheduleEntity>();
            List<ReservationServiceScheduleEntity> list = new List<ReservationServiceScheduleEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    ReservationServiceScheduleEntity m;
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
        public ReservationServiceScheduleEntity[] QueryByEntity(ReservationServiceScheduleEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<ReservationServiceScheduleEntity> PagedQueryByEntity(ReservationServiceScheduleEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(ReservationServiceScheduleEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.ReservationServiceScheduleID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReservationServiceScheduleID", Value = pQueryEntity.ReservationServiceScheduleID });
            if (pQueryEntity.ReservationServiceID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReservationServiceID", Value = pQueryEntity.ReservationServiceID });
            if (pQueryEntity.ReservationServiceBigClassTermID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReservationServiceBigClassTermID", Value = pQueryEntity.ReservationServiceBigClassTermID });
            if (pQueryEntity.ReservationServiceSmallClassTermID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReservationServiceSmallClassTermID", Value = pQueryEntity.ReservationServiceSmallClassTermID });
            if (pQueryEntity.VIPID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VIPID", Value = pQueryEntity.VIPID });
            if (pQueryEntity.ReserveDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReserveDate", Value = pQueryEntity.ReserveDate });
            if (pQueryEntity.Remark!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Remark", Value = pQueryEntity.Remark });
            if (pQueryEntity.PositionID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PositionID", Value = pQueryEntity.PositionID });
            if (pQueryEntity.ReserveTypeID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReserveTypeID", Value = pQueryEntity.ReserveTypeID });
            if (pQueryEntity.StatusID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StatusID", Value = pQueryEntity.StatusID });
            if (pQueryEntity.ReservationStoreID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReservationStoreID", Value = pQueryEntity.ReservationStoreID });
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
        protected void Load(SqlDataReader pReader, out ReservationServiceScheduleEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new ReservationServiceScheduleEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["ReservationServiceScheduleID"] != DBNull.Value)
			{
				pInstance.ReservationServiceScheduleID =  Convert.ToString(pReader["ReservationServiceScheduleID"]);
			}
			if (pReader["ReservationServiceID"] != DBNull.Value)
			{
				pInstance.ReservationServiceID =   Convert.ToInt32(pReader["ReservationServiceID"]);
			}
			if (pReader["ReservationServiceBigClassTermID"] != DBNull.Value)
			{
				pInstance.ReservationServiceBigClassTermID =  Convert.ToInt32(pReader["ReservationServiceBigClassTermID"]);
			}
			if (pReader["ReservationServiceSmallClassTermID"] != DBNull.Value)
			{
                pInstance.ReservationServiceSmallClassTermID = Convert.ToInt32(pReader["ReservationServiceSmallClassTermID"]);
			}
			if (pReader["VIPID"] != DBNull.Value)
			{
				pInstance.VIPID =  Convert.ToString(pReader["VIPID"]);
			}
			if (pReader["ReserveDate"] != DBNull.Value)
			{
				pInstance.ReserveDate = Convert.ToDateTime(pReader["ReserveDate"]);
			}
			if (pReader["Remark"] != DBNull.Value)
			{
				pInstance.Remark =  Convert.ToString(pReader["Remark"]);
			}
			if (pReader["PositionID"] != DBNull.Value)
			{
				pInstance.PositionID =   Convert.ToInt32(pReader["PositionID"]);
			}
			if (pReader["ReserveTypeID"] != DBNull.Value)
			{
				pInstance.ReserveTypeID =   Convert.ToInt32(pReader["ReserveTypeID"]);
			}
			if (pReader["StatusID"] != DBNull.Value)
			{
				pInstance.StatusID =   Convert.ToInt32(pReader["StatusID"]);
			}
			if (pReader["ReservationStoreID"] != DBNull.Value)
			{
				pInstance.ReservationStoreID =   Convert.ToInt32(pReader["ReservationStoreID"]);
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
