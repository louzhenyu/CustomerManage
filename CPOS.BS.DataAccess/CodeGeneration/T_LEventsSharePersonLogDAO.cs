/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/4/14 16:14:12
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
    /// ��T_LEventsSharePersonLog�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class T_LEventsSharePersonLogDAO : Base.BaseCPOSDAO, ICRUDable<T_LEventsSharePersonLogEntity>, IQueryable<T_LEventsSharePersonLogEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public T_LEventsSharePersonLogDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(T_LEventsSharePersonLogEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(T_LEventsSharePersonLogEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [T_LEventsSharePersonLog](");
            strSql.Append("[BusTypeCode],[ObjectId],[ShareVipID],[ShareOpenID],[ShareCount],[BeShareVipID],[BeShareOpenID],[ShareURL],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[CustomerId],[IsDelete],[ShareVipType],[SharePersonLogId])");
            strSql.Append(" values (");
            strSql.Append("@BusTypeCode,@ObjectId,@ShareVipID,@ShareOpenID,@ShareCount,@BeShareVipID,@BeShareOpenID,@ShareURL,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@CustomerId,@IsDelete,@ShareVipType,@SharePersonLogId)");            

			Guid? pkGuid;
			if (pEntity.SharePersonLogId == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.SharePersonLogId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@BusTypeCode",SqlDbType.VarChar),
					new SqlParameter("@ObjectId",SqlDbType.NVarChar),
					new SqlParameter("@ShareVipID",SqlDbType.NVarChar),
					new SqlParameter("@ShareOpenID",SqlDbType.NVarChar),
					new SqlParameter("@ShareCount",SqlDbType.Int),
					new SqlParameter("@BeShareVipID",SqlDbType.NVarChar),
					new SqlParameter("@BeShareOpenID",SqlDbType.NVarChar),
					new SqlParameter("@ShareURL",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerId",SqlDbType.VarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@ShareVipType",SqlDbType.Int),
					new SqlParameter("@SharePersonLogId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.BusTypeCode;
			parameters[1].Value = pEntity.ObjectId;
			parameters[2].Value = pEntity.ShareVipID;
			parameters[3].Value = pEntity.ShareOpenID;
			parameters[4].Value = pEntity.ShareCount;
			parameters[5].Value = pEntity.BeShareVipID;
			parameters[6].Value = pEntity.BeShareOpenID;
			parameters[7].Value = pEntity.ShareURL;
			parameters[8].Value = pEntity.CreateTime;
			parameters[9].Value = pEntity.CreateBy;
			parameters[10].Value = pEntity.LastUpdateBy;
			parameters[11].Value = pEntity.LastUpdateTime;
			parameters[12].Value = pEntity.CustomerId;
			parameters[13].Value = pEntity.IsDelete;
			parameters[14].Value = pEntity.ShareVipType;
			parameters[15].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.SharePersonLogId = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public T_LEventsSharePersonLogEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_LEventsSharePersonLog] where SharePersonLogId='{0}'  and isdelete=0 ", id.ToString());
            //��ȡ����
            T_LEventsSharePersonLogEntity m = null;
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
        public T_LEventsSharePersonLogEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_LEventsSharePersonLog] where 1=1  and isdelete=0");
            //��ȡ����
            List<T_LEventsSharePersonLogEntity> list = new List<T_LEventsSharePersonLogEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_LEventsSharePersonLogEntity m;
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
        public void Update(T_LEventsSharePersonLogEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(T_LEventsSharePersonLogEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.SharePersonLogId.HasValue)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_LEventsSharePersonLog] set ");
                        if (pIsUpdateNullField || pEntity.BusTypeCode!=null)
                strSql.Append( "[BusTypeCode]=@BusTypeCode,");
            if (pIsUpdateNullField || pEntity.ObjectId!=null)
                strSql.Append( "[ObjectId]=@ObjectId,");
            if (pIsUpdateNullField || pEntity.ShareVipID!=null)
                strSql.Append( "[ShareVipID]=@ShareVipID,");
            if (pIsUpdateNullField || pEntity.ShareOpenID!=null)
                strSql.Append( "[ShareOpenID]=@ShareOpenID,");
            if (pIsUpdateNullField || pEntity.ShareCount!=null)
                strSql.Append( "[ShareCount]=@ShareCount,");
            if (pIsUpdateNullField || pEntity.BeShareVipID!=null)
                strSql.Append( "[BeShareVipID]=@BeShareVipID,");
            if (pIsUpdateNullField || pEntity.BeShareOpenID!=null)
                strSql.Append( "[BeShareOpenID]=@BeShareOpenID,");
            if (pIsUpdateNullField || pEntity.ShareURL!=null)
                strSql.Append( "[ShareURL]=@ShareURL,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.ShareVipType!=null)
                strSql.Append( "[ShareVipType]=@ShareVipType");
            strSql.Append(" where SharePersonLogId=@SharePersonLogId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@BusTypeCode",SqlDbType.VarChar),
					new SqlParameter("@ObjectId",SqlDbType.NVarChar),
					new SqlParameter("@ShareVipID",SqlDbType.NVarChar),
					new SqlParameter("@ShareOpenID",SqlDbType.NVarChar),
					new SqlParameter("@ShareCount",SqlDbType.Int),
					new SqlParameter("@BeShareVipID",SqlDbType.NVarChar),
					new SqlParameter("@BeShareOpenID",SqlDbType.NVarChar),
					new SqlParameter("@ShareURL",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerId",SqlDbType.VarChar),
					new SqlParameter("@ShareVipType",SqlDbType.Int),
					new SqlParameter("@SharePersonLogId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.BusTypeCode;
			parameters[1].Value = pEntity.ObjectId;
			parameters[2].Value = pEntity.ShareVipID;
			parameters[3].Value = pEntity.ShareOpenID;
			parameters[4].Value = pEntity.ShareCount;
			parameters[5].Value = pEntity.BeShareVipID;
			parameters[6].Value = pEntity.BeShareOpenID;
			parameters[7].Value = pEntity.ShareURL;
			parameters[8].Value = pEntity.LastUpdateBy;
			parameters[9].Value = pEntity.LastUpdateTime;
			parameters[10].Value = pEntity.CustomerId;
			parameters[11].Value = pEntity.ShareVipType;
			parameters[12].Value = pEntity.SharePersonLogId;

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
        public void Update(T_LEventsSharePersonLogEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(T_LEventsSharePersonLogEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(T_LEventsSharePersonLogEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.SharePersonLogId.HasValue)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.SharePersonLogId.Value, pTran);           
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
            sql.AppendLine("update [T_LEventsSharePersonLog] set  isdelete=1 where SharePersonLogId=@SharePersonLogId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@SharePersonLogId",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(T_LEventsSharePersonLogEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.SharePersonLogId.HasValue)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.SharePersonLogId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(T_LEventsSharePersonLogEntity[] pEntities)
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
            sql.AppendLine("update [T_LEventsSharePersonLog] set  isdelete=1 where SharePersonLogId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public T_LEventsSharePersonLogEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_LEventsSharePersonLog] where 1=1  and isdelete=0 ");
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
            List<T_LEventsSharePersonLogEntity> list = new List<T_LEventsSharePersonLogEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_LEventsSharePersonLogEntity m;
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
        public PagedQueryResult<T_LEventsSharePersonLogEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [SharePersonLogId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [T_LEventsSharePersonLog] where 1=1  and isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [T_LEventsSharePersonLog] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<T_LEventsSharePersonLogEntity> result = new PagedQueryResult<T_LEventsSharePersonLogEntity>();
            List<T_LEventsSharePersonLogEntity> list = new List<T_LEventsSharePersonLogEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    T_LEventsSharePersonLogEntity m;
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
        public T_LEventsSharePersonLogEntity[] QueryByEntity(T_LEventsSharePersonLogEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<T_LEventsSharePersonLogEntity> PagedQueryByEntity(T_LEventsSharePersonLogEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(T_LEventsSharePersonLogEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.SharePersonLogId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SharePersonLogId", Value = pQueryEntity.SharePersonLogId });
            if (pQueryEntity.BusTypeCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BusTypeCode", Value = pQueryEntity.BusTypeCode });
            if (pQueryEntity.ObjectId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ObjectId", Value = pQueryEntity.ObjectId });
            if (pQueryEntity.ShareVipID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ShareVipID", Value = pQueryEntity.ShareVipID });
            if (pQueryEntity.ShareOpenID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ShareOpenID", Value = pQueryEntity.ShareOpenID });
            if (pQueryEntity.ShareCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ShareCount", Value = pQueryEntity.ShareCount });
            if (pQueryEntity.BeShareVipID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BeShareVipID", Value = pQueryEntity.BeShareVipID });
            if (pQueryEntity.BeShareOpenID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BeShareOpenID", Value = pQueryEntity.BeShareOpenID });
            if (pQueryEntity.ShareURL!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ShareURL", Value = pQueryEntity.ShareURL });
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
            if (pQueryEntity.ShareVipType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ShareVipType", Value = pQueryEntity.ShareVipType });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(IDataReader pReader, out T_LEventsSharePersonLogEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new T_LEventsSharePersonLogEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["SharePersonLogId"] != DBNull.Value)
			{
				pInstance.SharePersonLogId =  (Guid)pReader["SharePersonLogId"];
			}
			if (pReader["BusTypeCode"] != DBNull.Value)
			{
				pInstance.BusTypeCode =  Convert.ToString(pReader["BusTypeCode"]);
			}
			if (pReader["ObjectId"] != DBNull.Value)
			{
				pInstance.ObjectId =  Convert.ToString(pReader["ObjectId"]);
			}
			if (pReader["ShareVipID"] != DBNull.Value)
			{
				pInstance.ShareVipID =  Convert.ToString(pReader["ShareVipID"]);
			}
			if (pReader["ShareOpenID"] != DBNull.Value)
			{
				pInstance.ShareOpenID =  Convert.ToString(pReader["ShareOpenID"]);
			}
			if (pReader["ShareCount"] != DBNull.Value)
			{
				pInstance.ShareCount =   Convert.ToInt32(pReader["ShareCount"]);
			}
			if (pReader["BeShareVipID"] != DBNull.Value)
			{
				pInstance.BeShareVipID =  Convert.ToString(pReader["BeShareVipID"]);
			}
			if (pReader["BeShareOpenID"] != DBNull.Value)
			{
				pInstance.BeShareOpenID =  Convert.ToString(pReader["BeShareOpenID"]);
			}
			if (pReader["ShareURL"] != DBNull.Value)
			{
				pInstance.ShareURL =  Convert.ToString(pReader["ShareURL"]);
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
			if (pReader["ShareVipType"] != DBNull.Value)
			{
				pInstance.ShareVipType =   Convert.ToInt32(pReader["ShareVipType"]);
			}

        }
        #endregion
    }
}
