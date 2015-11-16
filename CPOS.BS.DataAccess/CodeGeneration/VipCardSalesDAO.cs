/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-14 20:18:28
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
    /// ��VipCardSales�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class VipCardSalesDAO : Base.BaseCPOSDAO, ICRUDable<VipCardSalesEntity>, IQueryable<VipCardSalesEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VipCardSalesDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(VipCardSalesEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(VipCardSalesEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [VipCardSales](");
            strSql.Append("[VipCardID],[SalesAmount],[SalesBeforeAmount],[SalesAfterAmount],[OrderNo],[UnitID],[SalesTime],[OperationUserID],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[CustomerID],[SalesID])");
            strSql.Append(" values (");
            strSql.Append("@VipCardID,@SalesAmount,@SalesBeforeAmount,@SalesAfterAmount,@OrderNo,@UnitID,@SalesTime,@OperationUserID,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@CustomerID,@SalesID)");            

			string pkString = pEntity.SalesID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@VipCardID",SqlDbType.NVarChar),
					new SqlParameter("@SalesAmount",SqlDbType.Decimal),
					new SqlParameter("@SalesBeforeAmount",SqlDbType.Decimal),
					new SqlParameter("@SalesAfterAmount",SqlDbType.Decimal),
					new SqlParameter("@OrderNo",SqlDbType.NVarChar),
					new SqlParameter("@UnitID",SqlDbType.NVarChar),
					new SqlParameter("@SalesTime",SqlDbType.DateTime),
					new SqlParameter("@OperationUserID",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@SalesID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.VipCardID;
			parameters[1].Value = pEntity.SalesAmount;
			parameters[2].Value = pEntity.SalesBeforeAmount;
			parameters[3].Value = pEntity.SalesAfterAmount;
			parameters[4].Value = pEntity.OrderNo;
			parameters[5].Value = pEntity.UnitID;
			parameters[6].Value = pEntity.SalesTime;
			parameters[7].Value = pEntity.OperationUserID;
			parameters[8].Value = pEntity.CreateTime;
			parameters[9].Value = pEntity.CreateBy;
			parameters[10].Value = pEntity.LastUpdateTime;
			parameters[11].Value = pEntity.LastUpdateBy;
			parameters[12].Value = pEntity.IsDelete;
			parameters[13].Value = pEntity.CustomerID;
			parameters[14].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.SalesID = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public VipCardSalesEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCardSales] where SalesID='{0}'  and isdelete=0 ", id.ToString());
            //��ȡ����
            VipCardSalesEntity m = null;
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
        public VipCardSalesEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCardSales] where 1=1  and isdelete=0");
            //��ȡ����
            List<VipCardSalesEntity> list = new List<VipCardSalesEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardSalesEntity m;
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
        public void Update(VipCardSalesEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(VipCardSalesEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.SalesID == null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [VipCardSales] set ");
                        if (pIsUpdateNullField || pEntity.VipCardID!=null)
                strSql.Append( "[VipCardID]=@VipCardID,");
            if (pIsUpdateNullField || pEntity.SalesAmount!=null)
                strSql.Append( "[SalesAmount]=@SalesAmount,");
            if (pIsUpdateNullField || pEntity.SalesBeforeAmount!=null)
                strSql.Append( "[SalesBeforeAmount]=@SalesBeforeAmount,");
            if (pIsUpdateNullField || pEntity.SalesAfterAmount!=null)
                strSql.Append( "[SalesAfterAmount]=@SalesAfterAmount,");
            if (pIsUpdateNullField || pEntity.OrderNo!=null)
                strSql.Append( "[OrderNo]=@OrderNo,");
            if (pIsUpdateNullField || pEntity.UnitID!=null)
                strSql.Append( "[UnitID]=@UnitID,");
            if (pIsUpdateNullField || pEntity.SalesTime!=null)
                strSql.Append( "[SalesTime]=@SalesTime,");
            if (pIsUpdateNullField || pEntity.OperationUserID!=null)
                strSql.Append( "[OperationUserID]=@OperationUserID,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID");
            strSql.Append(" where SalesID=@SalesID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@VipCardID",SqlDbType.NVarChar),
					new SqlParameter("@SalesAmount",SqlDbType.Decimal),
					new SqlParameter("@SalesBeforeAmount",SqlDbType.Decimal),
					new SqlParameter("@SalesAfterAmount",SqlDbType.Decimal),
					new SqlParameter("@OrderNo",SqlDbType.NVarChar),
					new SqlParameter("@UnitID",SqlDbType.NVarChar),
					new SqlParameter("@SalesTime",SqlDbType.DateTime),
					new SqlParameter("@OperationUserID",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@SalesID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.VipCardID;
			parameters[1].Value = pEntity.SalesAmount;
			parameters[2].Value = pEntity.SalesBeforeAmount;
			parameters[3].Value = pEntity.SalesAfterAmount;
			parameters[4].Value = pEntity.OrderNo;
			parameters[5].Value = pEntity.UnitID;
			parameters[6].Value = pEntity.SalesTime;
			parameters[7].Value = pEntity.OperationUserID;
			parameters[8].Value = pEntity.LastUpdateTime;
			parameters[9].Value = pEntity.LastUpdateBy;
			parameters[10].Value = pEntity.CustomerID;
			parameters[11].Value = pEntity.SalesID;

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
        public void Update(VipCardSalesEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(VipCardSalesEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(VipCardSalesEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(VipCardSalesEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.SalesID == null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.SalesID, pTran);           
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
            sql.AppendLine("update [VipCardSales] set  isdelete=1 where SalesID=@SalesID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@SalesID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(VipCardSalesEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (pEntity.SalesID == null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.SalesID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(VipCardSalesEntity[] pEntities)
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
            sql.AppendLine("update [VipCardSales] set  isdelete=1 where SalesID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public VipCardSalesEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCardSales] where 1=1  and isdelete=0 ");
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
            List<VipCardSalesEntity> list = new List<VipCardSalesEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardSalesEntity m;
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
        public PagedQueryResult<VipCardSalesEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [SalesID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [VipCardSales] where 1=1  and isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [VipCardSales] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<VipCardSalesEntity> result = new PagedQueryResult<VipCardSalesEntity>();
            List<VipCardSalesEntity> list = new List<VipCardSalesEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardSalesEntity m;
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
        public VipCardSalesEntity[] QueryByEntity(VipCardSalesEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<VipCardSalesEntity> PagedQueryByEntity(VipCardSalesEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(VipCardSalesEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.SalesID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesID", Value = pQueryEntity.SalesID });
            if (pQueryEntity.VipCardID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardID", Value = pQueryEntity.VipCardID });
            if (pQueryEntity.SalesAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesAmount", Value = pQueryEntity.SalesAmount });
            if (pQueryEntity.SalesBeforeAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesBeforeAmount", Value = pQueryEntity.SalesBeforeAmount });
            if (pQueryEntity.SalesAfterAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesAfterAmount", Value = pQueryEntity.SalesAfterAmount });
            if (pQueryEntity.OrderNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderNo", Value = pQueryEntity.OrderNo });
            if (pQueryEntity.UnitID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitID", Value = pQueryEntity.UnitID });
            if (pQueryEntity.SalesTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesTime", Value = pQueryEntity.SalesTime });
            if (pQueryEntity.OperationUserID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OperationUserID", Value = pQueryEntity.OperationUserID });
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
        protected void Load(IDataReader pReader, out VipCardSalesEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new VipCardSalesEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["SalesID"] != DBNull.Value)
			{
				pInstance.SalesID =  Convert.ToString(pReader["SalesID"]);
			}
			if (pReader["VipCardID"] != DBNull.Value)
			{
				pInstance.VipCardID =  Convert.ToString(pReader["VipCardID"]);
			}
			if (pReader["SalesAmount"] != DBNull.Value)
			{
				pInstance.SalesAmount =  Convert.ToDecimal(pReader["SalesAmount"]);
			}
			if (pReader["SalesBeforeAmount"] != DBNull.Value)
			{
				pInstance.SalesBeforeAmount =  Convert.ToDecimal(pReader["SalesBeforeAmount"]);
			}
			if (pReader["SalesAfterAmount"] != DBNull.Value)
			{
				pInstance.SalesAfterAmount =  Convert.ToDecimal(pReader["SalesAfterAmount"]);
			}
			if (pReader["OrderNo"] != DBNull.Value)
			{
				pInstance.OrderNo =  Convert.ToString(pReader["OrderNo"]);
			}
			if (pReader["UnitID"] != DBNull.Value)
			{
				pInstance.UnitID =  Convert.ToString(pReader["UnitID"]);
			}
			if (pReader["SalesTime"] != DBNull.Value)
			{
				pInstance.SalesTime =  Convert.ToDateTime(pReader["SalesTime"]);
			}
			if (pReader["OperationUserID"] != DBNull.Value)
			{
				pInstance.OperationUserID =  Convert.ToString(pReader["OperationUserID"]);
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
