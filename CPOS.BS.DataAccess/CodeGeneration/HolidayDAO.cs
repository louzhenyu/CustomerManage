/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-14 21:54:10
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
    /// ��Holiday�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class HolidayDAO : Base.BaseCPOSDAO, ICRUDable<HolidayEntity>, IQueryable<HolidayEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public HolidayDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(HolidayEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(HolidayEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [Holiday](");
            strSql.Append("[HolidayName],[BeginDate],[EndDate],[Model],[Workdays],[Options],[Months],[Weeks],[Days],[Discount],[Desciption],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[CustomerID],[HolidayId])");
            strSql.Append(" values (");
            strSql.Append("@HolidayName,@BeginDate,@EndDate,@Model,@Workdays,@Options,@Months,@Weeks,@Days,@Discount,@Desciption,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@CustomerID,@HolidayId)");            

			Guid? pkGuid;
			if (pEntity.HolidayId == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.HolidayId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@HolidayName",SqlDbType.NVarChar),
					new SqlParameter("@BeginDate",SqlDbType.DateTime),
					new SqlParameter("@EndDate",SqlDbType.DateTime),
					new SqlParameter("@Model",SqlDbType.NVarChar),
					new SqlParameter("@Workdays",SqlDbType.NVarChar),
					new SqlParameter("@Options",SqlDbType.NVarChar),
					new SqlParameter("@Months",SqlDbType.NVarChar),
					new SqlParameter("@Weeks",SqlDbType.NVarChar),
					new SqlParameter("@Days",SqlDbType.NVarChar),
					new SqlParameter("@Discount",SqlDbType.Decimal),
					new SqlParameter("@Desciption",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@HolidayId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.HolidayName;
			parameters[1].Value = pEntity.BeginDate;
			parameters[2].Value = pEntity.EndDate;
			parameters[3].Value = pEntity.Model;
			parameters[4].Value = pEntity.Workdays;
			parameters[5].Value = pEntity.Options;
			parameters[6].Value = pEntity.Months;
			parameters[7].Value = pEntity.Weeks;
			parameters[8].Value = pEntity.Days;
			parameters[9].Value = pEntity.Discount;
			parameters[10].Value = pEntity.Desciption;
			parameters[11].Value = pEntity.CreateTime;
			parameters[12].Value = pEntity.CreateBy;
			parameters[13].Value = pEntity.LastUpdateTime;
			parameters[14].Value = pEntity.LastUpdateBy;
			parameters[15].Value = pEntity.IsDelete;
			parameters[16].Value = pEntity.CustomerID;
			parameters[17].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.HolidayId = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public HolidayEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [Holiday] where HolidayId='{0}'  and isdelete=0 ", id.ToString());
            //��ȡ����
            HolidayEntity m = null;
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
        public HolidayEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [Holiday] where 1=1  and isdelete=0");
            //��ȡ����
            List<HolidayEntity> list = new List<HolidayEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    HolidayEntity m;
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
        public void Update(HolidayEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(HolidayEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.HolidayId.HasValue)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [Holiday] set ");
                        if (pIsUpdateNullField || pEntity.HolidayName!=null)
                strSql.Append( "[HolidayName]=@HolidayName,");
            if (pIsUpdateNullField || pEntity.BeginDate!=null)
                strSql.Append( "[BeginDate]=@BeginDate,");
            if (pIsUpdateNullField || pEntity.EndDate!=null)
                strSql.Append( "[EndDate]=@EndDate,");
            if (pIsUpdateNullField || pEntity.Model!=null)
                strSql.Append( "[Model]=@Model,");
            if (pIsUpdateNullField || pEntity.Workdays!=null)
                strSql.Append( "[Workdays]=@Workdays,");
            if (pIsUpdateNullField || pEntity.Options!=null)
                strSql.Append( "[Options]=@Options,");
            if (pIsUpdateNullField || pEntity.Months!=null)
                strSql.Append( "[Months]=@Months,");
            if (pIsUpdateNullField || pEntity.Weeks!=null)
                strSql.Append( "[Weeks]=@Weeks,");
            if (pIsUpdateNullField || pEntity.Days!=null)
                strSql.Append( "[Days]=@Days,");
            if (pIsUpdateNullField || pEntity.Discount!=null)
                strSql.Append( "[Discount]=@Discount,");
            if (pIsUpdateNullField || pEntity.Desciption!=null)
                strSql.Append( "[Desciption]=@Desciption,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID");
            strSql.Append(" where HolidayId=@HolidayId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@HolidayName",SqlDbType.NVarChar),
					new SqlParameter("@BeginDate",SqlDbType.DateTime),
					new SqlParameter("@EndDate",SqlDbType.DateTime),
					new SqlParameter("@Model",SqlDbType.NVarChar),
					new SqlParameter("@Workdays",SqlDbType.NVarChar),
					new SqlParameter("@Options",SqlDbType.NVarChar),
					new SqlParameter("@Months",SqlDbType.NVarChar),
					new SqlParameter("@Weeks",SqlDbType.NVarChar),
					new SqlParameter("@Days",SqlDbType.NVarChar),
					new SqlParameter("@Discount",SqlDbType.Decimal),
					new SqlParameter("@Desciption",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@HolidayId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.HolidayName;
			parameters[1].Value = pEntity.BeginDate;
			parameters[2].Value = pEntity.EndDate;
			parameters[3].Value = pEntity.Model;
			parameters[4].Value = pEntity.Workdays;
			parameters[5].Value = pEntity.Options;
			parameters[6].Value = pEntity.Months;
			parameters[7].Value = pEntity.Weeks;
			parameters[8].Value = pEntity.Days;
			parameters[9].Value = pEntity.Discount;
			parameters[10].Value = pEntity.Desciption;
			parameters[11].Value = pEntity.LastUpdateTime;
			parameters[12].Value = pEntity.LastUpdateBy;
			parameters[13].Value = pEntity.CustomerID;
			parameters[14].Value = pEntity.HolidayId;

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
        public void Update(HolidayEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(HolidayEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(HolidayEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.HolidayId.HasValue)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.HolidayId.Value, pTran);           
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
            sql.AppendLine("update [Holiday] set  isdelete=1 where HolidayId=@HolidayId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@HolidayId",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(HolidayEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.HolidayId.HasValue)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.HolidayId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(HolidayEntity[] pEntities)
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
            sql.AppendLine("update [Holiday] set  isdelete=1 where HolidayId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public HolidayEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [Holiday] where 1=1  and isdelete=0 ");
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
            List<HolidayEntity> list = new List<HolidayEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    HolidayEntity m;
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
        public PagedQueryResult<HolidayEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [HolidayId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [Holiday] where 1=1  and isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [Holiday] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<HolidayEntity> result = new PagedQueryResult<HolidayEntity>();
            List<HolidayEntity> list = new List<HolidayEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    HolidayEntity m;
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
        public HolidayEntity[] QueryByEntity(HolidayEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<HolidayEntity> PagedQueryByEntity(HolidayEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(HolidayEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.HolidayId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "HolidayId", Value = pQueryEntity.HolidayId });
            if (pQueryEntity.HolidayName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "HolidayName", Value = pQueryEntity.HolidayName });
            if (pQueryEntity.BeginDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BeginDate", Value = pQueryEntity.BeginDate });
            if (pQueryEntity.EndDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EndDate", Value = pQueryEntity.EndDate });
            if (pQueryEntity.Model!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Model", Value = pQueryEntity.Model });
            if (pQueryEntity.Workdays!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Workdays", Value = pQueryEntity.Workdays });
            if (pQueryEntity.Options!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Options", Value = pQueryEntity.Options });
            if (pQueryEntity.Months!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Months", Value = pQueryEntity.Months });
            if (pQueryEntity.Weeks!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Weeks", Value = pQueryEntity.Weeks });
            if (pQueryEntity.Days!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Days", Value = pQueryEntity.Days });
            if (pQueryEntity.Discount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Discount", Value = pQueryEntity.Discount });
            if (pQueryEntity.Desciption!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Desciption", Value = pQueryEntity.Desciption });
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
            if (pQueryEntity.CustomerID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(IDataReader pReader, out HolidayEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new HolidayEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["HolidayId"] != DBNull.Value)
			{
				pInstance.HolidayId =  (Guid)pReader["HolidayId"];
			}
			if (pReader["HolidayName"] != DBNull.Value)
			{
				pInstance.HolidayName =  Convert.ToString(pReader["HolidayName"]);
			}
			if (pReader["BeginDate"] != DBNull.Value)
			{
				pInstance.BeginDate =  Convert.ToDateTime(pReader["BeginDate"]);
			}
			if (pReader["EndDate"] != DBNull.Value)
			{
				pInstance.EndDate =  Convert.ToDateTime(pReader["EndDate"]);
			}
			if (pReader["Model"] != DBNull.Value)
			{
				pInstance.Model =  Convert.ToString(pReader["Model"]);
			}
			if (pReader["Workdays"] != DBNull.Value)
			{
				pInstance.Workdays =  Convert.ToString(pReader["Workdays"]);
			}
			if (pReader["Options"] != DBNull.Value)
			{
				pInstance.Options =  Convert.ToString(pReader["Options"]);
			}
			if (pReader["Months"] != DBNull.Value)
			{
				pInstance.Months =  Convert.ToString(pReader["Months"]);
			}
			if (pReader["Weeks"] != DBNull.Value)
			{
				pInstance.Weeks =  Convert.ToString(pReader["Weeks"]);
			}
			if (pReader["Days"] != DBNull.Value)
			{
				pInstance.Days =  Convert.ToString(pReader["Days"]);
			}
			if (pReader["Discount"] != DBNull.Value)
			{
				pInstance.Discount =  Convert.ToDecimal(pReader["Discount"]);
			}
			if (pReader["Desciption"] != DBNull.Value)
			{
				pInstance.Desciption =  Convert.ToString(pReader["Desciption"]);
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
			if (pReader["CustomerID"] != DBNull.Value)
			{
				pInstance.CustomerID =  Convert.ToString(pReader["CustomerID"]);
			}

        }
        #endregion
    }
}
