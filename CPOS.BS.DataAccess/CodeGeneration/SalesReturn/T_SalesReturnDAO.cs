/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-7-3 10:30:22
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
    /// ��T_SalesReturn�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class T_SalesReturnDAO : Base.BaseCPOSDAO, ICRUDable<T_SalesReturnEntity>, IQueryable<T_SalesReturnEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public T_SalesReturnDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(T_SalesReturnEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(T_SalesReturnEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [T_SalesReturn](");
            strSql.Append("[SalesReturnNo],[VipID],[ServicesType],[DeliveryType],[OrderID],[ItemID],[SkuID],[Qty],[ActualQty],[RefundAmount],[ConfirmAmount],[UnitID],[UnitName],[UnitTel],[Address],[Contacts],[Phone],[Reason],[Status],[CustomerID],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[SalesReturnID])");
            strSql.Append(" values (");
            strSql.Append("@SalesReturnNo,@VipID,@ServicesType,@DeliveryType,@OrderID,@ItemID,@SkuID,@Qty,@ActualQty,@RefundAmount,@ConfirmAmount,@UnitID,@UnitName,@UnitTel,@Address,@Contacts,@Phone,@Reason,@Status,@CustomerID,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@SalesReturnID)");            

			Guid? pkGuid;
			if (pEntity.SalesReturnID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.SalesReturnID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@SalesReturnNo",SqlDbType.NVarChar),
					new SqlParameter("@VipID",SqlDbType.NVarChar),
					new SqlParameter("@ServicesType",SqlDbType.Int),
					new SqlParameter("@DeliveryType",SqlDbType.Int),
					new SqlParameter("@OrderID",SqlDbType.NVarChar),
					new SqlParameter("@ItemID",SqlDbType.NVarChar),
					new SqlParameter("@SkuID",SqlDbType.NVarChar),
					new SqlParameter("@Qty",SqlDbType.Int),
					new SqlParameter("@ActualQty",SqlDbType.Int),
					new SqlParameter("@RefundAmount",SqlDbType.Decimal),
					new SqlParameter("@ConfirmAmount",SqlDbType.Decimal),
					new SqlParameter("@UnitID",SqlDbType.NVarChar),
					new SqlParameter("@UnitName",SqlDbType.NVarChar),
					new SqlParameter("@UnitTel",SqlDbType.NVarChar),
					new SqlParameter("@Address",SqlDbType.NVarChar),
					new SqlParameter("@Contacts",SqlDbType.NVarChar),
					new SqlParameter("@Phone",SqlDbType.NVarChar),
					new SqlParameter("@Reason",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@SalesReturnID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.SalesReturnNo;
			parameters[1].Value = pEntity.VipID;
			parameters[2].Value = pEntity.ServicesType;
			parameters[3].Value = pEntity.DeliveryType;
			parameters[4].Value = pEntity.OrderID;
			parameters[5].Value = pEntity.ItemID;
			parameters[6].Value = pEntity.SkuID;
			parameters[7].Value = pEntity.Qty;
			parameters[8].Value = pEntity.ActualQty;
			parameters[9].Value = pEntity.RefundAmount;
			parameters[10].Value = pEntity.ConfirmAmount;
			parameters[11].Value = pEntity.UnitID;
			parameters[12].Value = pEntity.UnitName;
			parameters[13].Value = pEntity.UnitTel;
			parameters[14].Value = pEntity.Address;
			parameters[15].Value = pEntity.Contacts;
			parameters[16].Value = pEntity.Phone;
			parameters[17].Value = pEntity.Reason;
			parameters[18].Value = pEntity.Status;
			parameters[19].Value = pEntity.CustomerID;
			parameters[20].Value = pEntity.CreateTime;
			parameters[21].Value = pEntity.CreateBy;
			parameters[22].Value = pEntity.LastUpdateTime;
			parameters[23].Value = pEntity.LastUpdateBy;
			parameters[24].Value = pEntity.IsDelete;
			parameters[25].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.SalesReturnID = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public T_SalesReturnEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_SalesReturn] where SalesReturnID='{0}'  and isdelete=0 ", id.ToString());
            //��ȡ����
            T_SalesReturnEntity m = null;
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
        public T_SalesReturnEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_SalesReturn] where 1=1  and isdelete=0");
            //��ȡ����
            List<T_SalesReturnEntity> list = new List<T_SalesReturnEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_SalesReturnEntity m;
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
        public void Update(T_SalesReturnEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(T_SalesReturnEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.SalesReturnID.HasValue)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_SalesReturn] set ");
                        if (pIsUpdateNullField || pEntity.SalesReturnNo!=null)
                strSql.Append( "[SalesReturnNo]=@SalesReturnNo,");
            if (pIsUpdateNullField || pEntity.VipID!=null)
                strSql.Append( "[VipID]=@VipID,");
            if (pIsUpdateNullField || pEntity.ServicesType!=null)
                strSql.Append( "[ServicesType]=@ServicesType,");
            if (pIsUpdateNullField || pEntity.DeliveryType!=null)
                strSql.Append( "[DeliveryType]=@DeliveryType,");
            if (pIsUpdateNullField || pEntity.OrderID!=null)
                strSql.Append( "[OrderID]=@OrderID,");
            if (pIsUpdateNullField || pEntity.ItemID!=null)
                strSql.Append( "[ItemID]=@ItemID,");
            if (pIsUpdateNullField || pEntity.SkuID!=null)
                strSql.Append( "[SkuID]=@SkuID,");
            if (pIsUpdateNullField || pEntity.Qty!=null)
                strSql.Append( "[Qty]=@Qty,");
            if (pIsUpdateNullField || pEntity.ActualQty!=null)
                strSql.Append( "[ActualQty]=@ActualQty,");
            if (pIsUpdateNullField || pEntity.RefundAmount!=null)
                strSql.Append( "[RefundAmount]=@RefundAmount,");
            if (pIsUpdateNullField || pEntity.ConfirmAmount!=null)
                strSql.Append( "[ConfirmAmount]=@ConfirmAmount,");
            if (pIsUpdateNullField || pEntity.UnitID!=null)
                strSql.Append( "[UnitID]=@UnitID,");
            if (pIsUpdateNullField || pEntity.UnitName!=null)
                strSql.Append( "[UnitName]=@UnitName,");
            if (pIsUpdateNullField || pEntity.UnitTel!=null)
                strSql.Append( "[UnitTel]=@UnitTel,");
            if (pIsUpdateNullField || pEntity.Address!=null)
                strSql.Append( "[Address]=@Address,");
            if (pIsUpdateNullField || pEntity.Contacts!=null)
                strSql.Append( "[Contacts]=@Contacts,");
            if (pIsUpdateNullField || pEntity.Phone!=null)
                strSql.Append( "[Phone]=@Phone,");
            if (pIsUpdateNullField || pEntity.Reason!=null)
                strSql.Append( "[Reason]=@Reason,");
            if (pIsUpdateNullField || pEntity.Status!=null)
                strSql.Append( "[Status]=@Status,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy");
            strSql.Append(" where SalesReturnID=@SalesReturnID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@SalesReturnNo",SqlDbType.NVarChar),
					new SqlParameter("@VipID",SqlDbType.NVarChar),
					new SqlParameter("@ServicesType",SqlDbType.Int),
					new SqlParameter("@DeliveryType",SqlDbType.Int),
					new SqlParameter("@OrderID",SqlDbType.NVarChar),
					new SqlParameter("@ItemID",SqlDbType.NVarChar),
					new SqlParameter("@SkuID",SqlDbType.NVarChar),
					new SqlParameter("@Qty",SqlDbType.Int),
					new SqlParameter("@ActualQty",SqlDbType.Int),
					new SqlParameter("@RefundAmount",SqlDbType.Decimal),
					new SqlParameter("@ConfirmAmount",SqlDbType.Decimal),
					new SqlParameter("@UnitID",SqlDbType.NVarChar),
					new SqlParameter("@UnitName",SqlDbType.NVarChar),
					new SqlParameter("@UnitTel",SqlDbType.NVarChar),
					new SqlParameter("@Address",SqlDbType.NVarChar),
					new SqlParameter("@Contacts",SqlDbType.NVarChar),
					new SqlParameter("@Phone",SqlDbType.NVarChar),
					new SqlParameter("@Reason",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@SalesReturnID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.SalesReturnNo;
			parameters[1].Value = pEntity.VipID;
			parameters[2].Value = pEntity.ServicesType;
			parameters[3].Value = pEntity.DeliveryType;
			parameters[4].Value = pEntity.OrderID;
			parameters[5].Value = pEntity.ItemID;
			parameters[6].Value = pEntity.SkuID;
			parameters[7].Value = pEntity.Qty;
			parameters[8].Value = pEntity.ActualQty;
			parameters[9].Value = pEntity.RefundAmount;
			parameters[10].Value = pEntity.ConfirmAmount;
			parameters[11].Value = pEntity.UnitID;
			parameters[12].Value = pEntity.UnitName;
			parameters[13].Value = pEntity.UnitTel;
			parameters[14].Value = pEntity.Address;
			parameters[15].Value = pEntity.Contacts;
			parameters[16].Value = pEntity.Phone;
			parameters[17].Value = pEntity.Reason;
			parameters[18].Value = pEntity.Status;
			parameters[19].Value = pEntity.CustomerID;
			parameters[20].Value = pEntity.LastUpdateTime;
			parameters[21].Value = pEntity.LastUpdateBy;
			parameters[22].Value = pEntity.SalesReturnID;

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
        public void Update(T_SalesReturnEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(T_SalesReturnEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(T_SalesReturnEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.SalesReturnID.HasValue)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.SalesReturnID.Value, pTran);           
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
            sql.AppendLine("update [T_SalesReturn] set  isdelete=1 where SalesReturnID=@SalesReturnID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@SalesReturnID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(T_SalesReturnEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.SalesReturnID.HasValue)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.SalesReturnID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(T_SalesReturnEntity[] pEntities)
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
            sql.AppendLine("update [T_SalesReturn] set  isdelete=1 where SalesReturnID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public T_SalesReturnEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_SalesReturn] where 1=1  and isdelete=0 ");
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
            List<T_SalesReturnEntity> list = new List<T_SalesReturnEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_SalesReturnEntity m;
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
        public PagedQueryResult<T_SalesReturnEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [SalesReturnID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [T_SalesReturn] where 1=1  and isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [T_SalesReturn] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<T_SalesReturnEntity> result = new PagedQueryResult<T_SalesReturnEntity>();
            List<T_SalesReturnEntity> list = new List<T_SalesReturnEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    T_SalesReturnEntity m;
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
        public T_SalesReturnEntity[] QueryByEntity(T_SalesReturnEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<T_SalesReturnEntity> PagedQueryByEntity(T_SalesReturnEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(T_SalesReturnEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.SalesReturnID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesReturnID", Value = pQueryEntity.SalesReturnID });
            if (pQueryEntity.SalesReturnNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesReturnNo", Value = pQueryEntity.SalesReturnNo });
            if (pQueryEntity.VipID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipID", Value = pQueryEntity.VipID });
            if (pQueryEntity.ServicesType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ServicesType", Value = pQueryEntity.ServicesType });
            if (pQueryEntity.DeliveryType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DeliveryType", Value = pQueryEntity.DeliveryType });
            if (pQueryEntity.OrderID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderID", Value = pQueryEntity.OrderID });
            if (pQueryEntity.ItemID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemID", Value = pQueryEntity.ItemID });
            if (pQueryEntity.SkuID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SkuID", Value = pQueryEntity.SkuID });
            if (pQueryEntity.Qty!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Qty", Value = pQueryEntity.Qty });
            if (pQueryEntity.ActualQty!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ActualQty", Value = pQueryEntity.ActualQty });
            if (pQueryEntity.RefundAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RefundAmount", Value = pQueryEntity.RefundAmount });
            if (pQueryEntity.ConfirmAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ConfirmAmount", Value = pQueryEntity.ConfirmAmount });
            if (pQueryEntity.UnitID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitID", Value = pQueryEntity.UnitID });
            if (pQueryEntity.UnitName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitName", Value = pQueryEntity.UnitName });
            if (pQueryEntity.UnitTel!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitTel", Value = pQueryEntity.UnitTel });
            if (pQueryEntity.Address!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Address", Value = pQueryEntity.Address });
            if (pQueryEntity.Contacts!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Contacts", Value = pQueryEntity.Contacts });
            if (pQueryEntity.Phone!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Phone", Value = pQueryEntity.Phone });
            if (pQueryEntity.Reason!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Reason", Value = pQueryEntity.Reason });
            if (pQueryEntity.Status!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Status", Value = pQueryEntity.Status });
            if (pQueryEntity.CustomerID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });
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
        protected void Load(IDataReader pReader, out T_SalesReturnEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new T_SalesReturnEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["SalesReturnID"] != DBNull.Value)
			{
				pInstance.SalesReturnID =  (Guid)pReader["SalesReturnID"];
			}
			if (pReader["SalesReturnNo"] != DBNull.Value)
			{
				pInstance.SalesReturnNo =  Convert.ToString(pReader["SalesReturnNo"]);
			}
			if (pReader["VipID"] != DBNull.Value)
			{
				pInstance.VipID =  Convert.ToString(pReader["VipID"]);
                var vipDao = new VipDAO(this.CurrentUserInfo);
                var vipInfo=vipDao.GetByID(pInstance.VipID);
                if (vipInfo != null)
                    pInstance.VipName = vipInfo.VipName;
			}
			if (pReader["ServicesType"] != DBNull.Value)
			{
				pInstance.ServicesType =   Convert.ToInt32(pReader["ServicesType"]);
			}
			if (pReader["DeliveryType"] != DBNull.Value)
			{
				pInstance.DeliveryType =   Convert.ToInt32(pReader["DeliveryType"]);
			}
			if (pReader["OrderID"] != DBNull.Value)
			{
				pInstance.OrderID =  Convert.ToString(pReader["OrderID"]);
                var inoutDAO = new T_InoutDAO(CurrentUserInfo);
                var inoutInfo = inoutDAO.GetByID(pInstance.OrderID);
                if (inoutInfo != null)
                {
                    var paymentTypeDAO = new TPaymentTypeDAO(CurrentUserInfo);
                    var paymentTypeInfo = paymentTypeDAO.GetByID(inoutInfo.pay_id);
                    if (paymentTypeInfo != null)
                    {
                        pInstance.PayTypeName = paymentTypeInfo.PaymentTypeName;//֧����ʽ����
                    }
                }
			}
			if (pReader["ItemID"] != DBNull.Value)
			{
				pInstance.ItemID =  Convert.ToString(pReader["ItemID"]);
			}
			if (pReader["SkuID"] != DBNull.Value)
			{
				pInstance.SkuID =  Convert.ToString(pReader["SkuID"]);
			}
			if (pReader["Qty"] != DBNull.Value)
			{
				pInstance.Qty =   Convert.ToInt32(pReader["Qty"]);
			}
			if (pReader["ActualQty"] != DBNull.Value)
			{
				pInstance.ActualQty =   Convert.ToInt32(pReader["ActualQty"]);
			}
			if (pReader["RefundAmount"] != DBNull.Value)
			{
				pInstance.RefundAmount =  Convert.ToDecimal(pReader["RefundAmount"]);
			}
			if (pReader["ConfirmAmount"] != DBNull.Value)
			{
				pInstance.ConfirmAmount =  Convert.ToDecimal(pReader["ConfirmAmount"]);
			}
			if (pReader["UnitID"] != DBNull.Value)
			{
				pInstance.UnitID =  Convert.ToString(pReader["UnitID"]);
			}
			if (pReader["UnitName"] != DBNull.Value)
			{
				pInstance.UnitName =  Convert.ToString(pReader["UnitName"]);
			}
			if (pReader["UnitTel"] != DBNull.Value)
			{
				pInstance.UnitTel =  Convert.ToString(pReader["UnitTel"]);
			}
			if (pReader["Address"] != DBNull.Value)
			{
				pInstance.Address =  Convert.ToString(pReader["Address"]);
			}
			if (pReader["Contacts"] != DBNull.Value)
			{
				pInstance.Contacts =  Convert.ToString(pReader["Contacts"]);
			}
			if (pReader["Phone"] != DBNull.Value)
			{
				pInstance.Phone =  Convert.ToString(pReader["Phone"]);
			}
			if (pReader["Reason"] != DBNull.Value)
			{
				pInstance.Reason =  Convert.ToString(pReader["Reason"]);
			}
			if (pReader["Status"] != DBNull.Value)
			{
				pInstance.Status =   Convert.ToInt32(pReader["Status"]);
			}
			if (pReader["CustomerID"] != DBNull.Value)
			{
				pInstance.CustomerID =  Convert.ToString(pReader["CustomerID"]);
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
