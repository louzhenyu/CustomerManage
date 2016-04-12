/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/3/23 14:10:42
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
    /// ��R_UnitProductMonthSalesTop�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class R_UnitProductMonthSalesTopDAO : Base.BaseCPOSDAO, ICRUDable<R_UnitProductMonthSalesTopEntity>, IQueryable<R_UnitProductMonthSalesTopEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public R_UnitProductMonthSalesTopDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(R_UnitProductMonthSalesTopEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(R_UnitProductMonthSalesTopEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //��ʼ���̶��ֶ�
			pEntity.CreateTime=DateTime.Now;


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [R_UnitProductMonthSalesTop](");
            strSql.Append("[DateCode],[UnitId],[UnitCode],[UnitName],[StoreType],[CustomerId],[TopIndex],[SkuID],[item_id],[item_code],[item_name],[SkuName],[SalesQty],[SalesAmount],[CreateTime],[ID])");
            strSql.Append(" values (");
            strSql.Append("@DateCode,@UnitId,@UnitCode,@UnitName,@StoreType,@CustomerId,@TopIndex,@SkuID,@item_id,@item_code,@item_name,@SkuName,@SalesQty,@SalesAmount,@CreateTime,@ID)");            

			string pkString = pEntity.ID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@DateCode",SqlDbType.Date),
					new SqlParameter("@UnitId",SqlDbType.NVarChar),
					new SqlParameter("@UnitCode",SqlDbType.NVarChar),
					new SqlParameter("@UnitName",SqlDbType.NVarChar),
					new SqlParameter("@StoreType",SqlDbType.NVarChar),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@TopIndex",SqlDbType.Int),
					new SqlParameter("@SkuID",SqlDbType.VarChar),
					new SqlParameter("@item_id",SqlDbType.NVarChar),
					new SqlParameter("@item_code",SqlDbType.NVarChar),
					new SqlParameter("@item_name",SqlDbType.NVarChar),
					new SqlParameter("@SkuName",SqlDbType.VarChar),
					new SqlParameter("@SalesQty",SqlDbType.Decimal),
					new SqlParameter("@SalesAmount",SqlDbType.Decimal),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@ID",SqlDbType.VarChar)
            };
			parameters[0].Value = pEntity.DateCode;
			parameters[1].Value = pEntity.UnitId;
			parameters[2].Value = pEntity.UnitCode;
			parameters[3].Value = pEntity.UnitName;
			parameters[4].Value = pEntity.StoreType;
			parameters[5].Value = pEntity.CustomerId;
			parameters[6].Value = pEntity.TopIndex;
			parameters[7].Value = pEntity.SkuID;
			parameters[8].Value = pEntity.item_id;
			parameters[9].Value = pEntity.item_code;
			parameters[10].Value = pEntity.item_name;
			parameters[11].Value = pEntity.SkuName;
			parameters[12].Value = pEntity.SalesQty;
			parameters[13].Value = pEntity.SalesAmount;
			parameters[14].Value = pEntity.CreateTime;
			parameters[15].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.ID = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public R_UnitProductMonthSalesTopEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [R_UnitProductMonthSalesTop] where ID='{0}'  ", id.ToString());
            //��ȡ����
            R_UnitProductMonthSalesTopEntity m = null;
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
        public R_UnitProductMonthSalesTopEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [R_UnitProductMonthSalesTop] where 1=1 ");
            //��ȡ����
            List<R_UnitProductMonthSalesTopEntity> list = new List<R_UnitProductMonthSalesTopEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    R_UnitProductMonthSalesTopEntity m;
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
        public void Update(R_UnitProductMonthSalesTopEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(R_UnitProductMonthSalesTopEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ID == null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [R_UnitProductMonthSalesTop] set ");
                        if (pIsUpdateNullField || pEntity.DateCode!=null)
                strSql.Append( "[DateCode]=@DateCode,");
            if (pIsUpdateNullField || pEntity.UnitId!=null)
                strSql.Append( "[UnitId]=@UnitId,");
            if (pIsUpdateNullField || pEntity.UnitCode!=null)
                strSql.Append( "[UnitCode]=@UnitCode,");
            if (pIsUpdateNullField || pEntity.UnitName!=null)
                strSql.Append( "[UnitName]=@UnitName,");
            if (pIsUpdateNullField || pEntity.StoreType!=null)
                strSql.Append( "[StoreType]=@StoreType,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.TopIndex!=null)
                strSql.Append( "[TopIndex]=@TopIndex,");
            if (pIsUpdateNullField || pEntity.SkuID!=null)
                strSql.Append( "[SkuID]=@SkuID,");
            if (pIsUpdateNullField || pEntity.item_id!=null)
                strSql.Append( "[item_id]=@item_id,");
            if (pIsUpdateNullField || pEntity.item_code!=null)
                strSql.Append( "[item_code]=@item_code,");
            if (pIsUpdateNullField || pEntity.item_name!=null)
                strSql.Append( "[item_name]=@item_name,");
            if (pIsUpdateNullField || pEntity.SkuName!=null)
                strSql.Append( "[SkuName]=@SkuName,");
            if (pIsUpdateNullField || pEntity.SalesQty!=null)
                strSql.Append( "[SalesQty]=@SalesQty,");
            if (pIsUpdateNullField || pEntity.SalesAmount!=null)
                strSql.Append( "[SalesAmount]=@SalesAmount");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@DateCode",SqlDbType.Date),
					new SqlParameter("@UnitId",SqlDbType.NVarChar),
					new SqlParameter("@UnitCode",SqlDbType.NVarChar),
					new SqlParameter("@UnitName",SqlDbType.NVarChar),
					new SqlParameter("@StoreType",SqlDbType.NVarChar),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@TopIndex",SqlDbType.Int),
					new SqlParameter("@SkuID",SqlDbType.VarChar),
					new SqlParameter("@item_id",SqlDbType.NVarChar),
					new SqlParameter("@item_code",SqlDbType.NVarChar),
					new SqlParameter("@item_name",SqlDbType.NVarChar),
					new SqlParameter("@SkuName",SqlDbType.VarChar),
					new SqlParameter("@SalesQty",SqlDbType.Decimal),
					new SqlParameter("@SalesAmount",SqlDbType.Decimal),
					new SqlParameter("@ID",SqlDbType.VarChar)
            };
			parameters[0].Value = pEntity.DateCode;
			parameters[1].Value = pEntity.UnitId;
			parameters[2].Value = pEntity.UnitCode;
			parameters[3].Value = pEntity.UnitName;
			parameters[4].Value = pEntity.StoreType;
			parameters[5].Value = pEntity.CustomerId;
			parameters[6].Value = pEntity.TopIndex;
			parameters[7].Value = pEntity.SkuID;
			parameters[8].Value = pEntity.item_id;
			parameters[9].Value = pEntity.item_code;
			parameters[10].Value = pEntity.item_name;
			parameters[11].Value = pEntity.SkuName;
			parameters[12].Value = pEntity.SalesQty;
			parameters[13].Value = pEntity.SalesAmount;
			parameters[14].Value = pEntity.ID;

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
        public void Update(R_UnitProductMonthSalesTopEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(R_UnitProductMonthSalesTopEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(R_UnitProductMonthSalesTopEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ID == null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.ID, pTran);           
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
            sql.AppendLine("update [R_UnitProductMonthSalesTop] set  where ID=@ID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@ID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(R_UnitProductMonthSalesTopEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (pEntity.ID == null)
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
        public void Delete(R_UnitProductMonthSalesTopEntity[] pEntities)
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
            sql.AppendLine("update [R_UnitProductMonthSalesTop] set  where ID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public R_UnitProductMonthSalesTopEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [R_UnitProductMonthSalesTop] where 1=1  ");
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
            List<R_UnitProductMonthSalesTopEntity> list = new List<R_UnitProductMonthSalesTopEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    R_UnitProductMonthSalesTopEntity m;
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
        public PagedQueryResult<R_UnitProductMonthSalesTopEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [ID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [R_UnitProductMonthSalesTop] where 1=1  ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [R_UnitProductMonthSalesTop] where 1=1  ");
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
            PagedQueryResult<R_UnitProductMonthSalesTopEntity> result = new PagedQueryResult<R_UnitProductMonthSalesTopEntity>();
            List<R_UnitProductMonthSalesTopEntity> list = new List<R_UnitProductMonthSalesTopEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    R_UnitProductMonthSalesTopEntity m;
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
        public R_UnitProductMonthSalesTopEntity[] QueryByEntity(R_UnitProductMonthSalesTopEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<R_UnitProductMonthSalesTopEntity> PagedQueryByEntity(R_UnitProductMonthSalesTopEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(R_UnitProductMonthSalesTopEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.ID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ID", Value = pQueryEntity.ID });
            if (pQueryEntity.DateCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DateCode", Value = pQueryEntity.DateCode });
            if (pQueryEntity.UnitId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitId", Value = pQueryEntity.UnitId });
            if (pQueryEntity.UnitCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitCode", Value = pQueryEntity.UnitCode });
            if (pQueryEntity.UnitName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitName", Value = pQueryEntity.UnitName });
            if (pQueryEntity.StoreType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StoreType", Value = pQueryEntity.StoreType });
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
            if (pQueryEntity.TopIndex!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TopIndex", Value = pQueryEntity.TopIndex });
            if (pQueryEntity.SkuID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SkuID", Value = pQueryEntity.SkuID });
            if (pQueryEntity.item_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "item_id", Value = pQueryEntity.item_id });
            if (pQueryEntity.item_code!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "item_code", Value = pQueryEntity.item_code });
            if (pQueryEntity.item_name!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "item_name", Value = pQueryEntity.item_name });
            if (pQueryEntity.SkuName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SkuName", Value = pQueryEntity.SkuName });
            if (pQueryEntity.SalesQty!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesQty", Value = pQueryEntity.SalesQty });
            if (pQueryEntity.SalesAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesAmount", Value = pQueryEntity.SalesAmount });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(IDataReader pReader, out R_UnitProductMonthSalesTopEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new R_UnitProductMonthSalesTopEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["ID"] != DBNull.Value)
			{
				pInstance.ID =  Convert.ToString(pReader["ID"]);
			}
			if (pReader["DateCode"] != DBNull.Value)
			{
				pInstance.DateCode = Convert.ToDateTime(pReader["DateCode"]);
			}
			if (pReader["UnitId"] != DBNull.Value)
			{
				pInstance.UnitId =  Convert.ToString(pReader["UnitId"]);
			}
			if (pReader["UnitCode"] != DBNull.Value)
			{
				pInstance.UnitCode =  Convert.ToString(pReader["UnitCode"]);
			}
			if (pReader["UnitName"] != DBNull.Value)
			{
				pInstance.UnitName =  Convert.ToString(pReader["UnitName"]);
			}
			if (pReader["StoreType"] != DBNull.Value)
			{
				pInstance.StoreType =  Convert.ToString(pReader["StoreType"]);
			}
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}
			if (pReader["TopIndex"] != DBNull.Value)
			{
				pInstance.TopIndex =   Convert.ToInt32(pReader["TopIndex"]);
			}
			if (pReader["SkuID"] != DBNull.Value)
			{
				pInstance.SkuID =  Convert.ToString(pReader["SkuID"]);
			}
			if (pReader["item_id"] != DBNull.Value)
			{
				pInstance.item_id =  Convert.ToString(pReader["item_id"]);
			}
			if (pReader["item_code"] != DBNull.Value)
			{
				pInstance.item_code =  Convert.ToString(pReader["item_code"]);
			}
			if (pReader["item_name"] != DBNull.Value)
			{
				pInstance.item_name =  Convert.ToString(pReader["item_name"]);
			}
			if (pReader["SkuName"] != DBNull.Value)
			{
				pInstance.SkuName =  Convert.ToString(pReader["SkuName"]);
			}
			if (pReader["SalesQty"] != DBNull.Value)
			{
				pInstance.SalesQty =  Convert.ToDecimal(pReader["SalesQty"]);
			}
			if (pReader["SalesAmount"] != DBNull.Value)
			{
				pInstance.SalesAmount =  Convert.ToDecimal(pReader["SalesAmount"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}

        }
        #endregion
    }
}
