/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-7-8 15:47:38
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
    /// ��T_RefundOrder�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class T_RefundOrderDAO : Base.BaseCPOSDAO, ICRUDable<T_RefundOrderEntity>, IQueryable<T_RefundOrderEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public T_RefundOrderDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(T_RefundOrderEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(T_RefundOrderEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [T_RefundOrder](");
            strSql.Append("[SalesReturnID],[RefundNo],[VipID],[DeliveryType],[OrderID],[ItemID],[SkuID],[Qty],[ActualQty],[UnitID],[UnitName],[UnitTel],[Address],[Contacts],[Phone],[RefundAmount],[ConfirmAmount],[ActualRefundAmount],[Points],[PointsAmount],[ReturnAmount],[Amount],[CouponID],[PayOrderID],[Status],[CustomerID],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[RefundID])");
            strSql.Append(" values (");
            strSql.Append("@SalesReturnID,@RefundNo,@VipID,@DeliveryType,@OrderID,@ItemID,@SkuID,@Qty,@ActualQty,@UnitID,@UnitName,@UnitTel,@Address,@Contacts,@Phone,@RefundAmount,@ConfirmAmount,@ActualRefundAmount,@Points,@PointsAmount,@ReturnAmount,@Amount,@CouponID,@PayOrderID,@Status,@CustomerID,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@RefundID)");            

			Guid? pkGuid;
			if (pEntity.RefundID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.RefundID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@SalesReturnID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@RefundNo",SqlDbType.NVarChar),
					new SqlParameter("@VipID",SqlDbType.NVarChar),
					new SqlParameter("@DeliveryType",SqlDbType.Int),
					new SqlParameter("@OrderID",SqlDbType.NVarChar),
					new SqlParameter("@ItemID",SqlDbType.NVarChar),
					new SqlParameter("@SkuID",SqlDbType.NVarChar),
					new SqlParameter("@Qty",SqlDbType.Int),
					new SqlParameter("@ActualQty",SqlDbType.Int),
					new SqlParameter("@UnitID",SqlDbType.NVarChar),
					new SqlParameter("@UnitName",SqlDbType.NVarChar),
					new SqlParameter("@UnitTel",SqlDbType.NVarChar),
					new SqlParameter("@Address",SqlDbType.NVarChar),
					new SqlParameter("@Contacts",SqlDbType.NVarChar),
					new SqlParameter("@Phone",SqlDbType.NVarChar),
					new SqlParameter("@RefundAmount",SqlDbType.Decimal),
					new SqlParameter("@ConfirmAmount",SqlDbType.Decimal),
					new SqlParameter("@ActualRefundAmount",SqlDbType.Decimal),
					new SqlParameter("@Points",SqlDbType.Int),
					new SqlParameter("@PointsAmount",SqlDbType.Decimal),
					new SqlParameter("@ReturnAmount",SqlDbType.Decimal),
					new SqlParameter("@Amount",SqlDbType.Decimal),
					new SqlParameter("@CouponID",SqlDbType.NVarChar),
					new SqlParameter("@PayOrderID",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@RefundID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.SalesReturnID;
			parameters[1].Value = pEntity.RefundNo;
			parameters[2].Value = pEntity.VipID;
			parameters[3].Value = pEntity.DeliveryType;
			parameters[4].Value = pEntity.OrderID;
			parameters[5].Value = pEntity.ItemID;
			parameters[6].Value = pEntity.SkuID;
			parameters[7].Value = pEntity.Qty;
			parameters[8].Value = pEntity.ActualQty;
			parameters[9].Value = pEntity.UnitID;
			parameters[10].Value = pEntity.UnitName;
			parameters[11].Value = pEntity.UnitTel;
			parameters[12].Value = pEntity.Address;
			parameters[13].Value = pEntity.Contacts;
			parameters[14].Value = pEntity.Phone;
			parameters[15].Value = pEntity.RefundAmount;
			parameters[16].Value = pEntity.ConfirmAmount;
			parameters[17].Value = pEntity.ActualRefundAmount;
			parameters[18].Value = pEntity.Points;
			parameters[19].Value = pEntity.PointsAmount;
			parameters[20].Value = pEntity.ReturnAmount;
			parameters[21].Value = pEntity.Amount;
			parameters[22].Value = pEntity.CouponID;
			parameters[23].Value = pEntity.PayOrderID;
			parameters[24].Value = pEntity.Status;
			parameters[25].Value = pEntity.CustomerID;
			parameters[26].Value = pEntity.CreateTime;
			parameters[27].Value = pEntity.CreateBy;
			parameters[28].Value = pEntity.LastUpdateTime;
			parameters[29].Value = pEntity.LastUpdateBy;
			parameters[30].Value = pEntity.IsDelete;
			parameters[31].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.RefundID = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public T_RefundOrderEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_RefundOrder] where RefundID='{0}'  and isdelete=0 ", id.ToString());
            //��ȡ����
            T_RefundOrderEntity m = null;
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
        public T_RefundOrderEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_RefundOrder] where 1=1  and isdelete=0");
            //��ȡ����
            List<T_RefundOrderEntity> list = new List<T_RefundOrderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_RefundOrderEntity m;
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
        public void Update(T_RefundOrderEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(T_RefundOrderEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.RefundID.HasValue)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_RefundOrder] set ");
                        if (pIsUpdateNullField || pEntity.SalesReturnID!=null)
                strSql.Append( "[SalesReturnID]=@SalesReturnID,");
            if (pIsUpdateNullField || pEntity.RefundNo!=null)
                strSql.Append( "[RefundNo]=@RefundNo,");
            if (pIsUpdateNullField || pEntity.VipID!=null)
                strSql.Append( "[VipID]=@VipID,");
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
            if (pIsUpdateNullField || pEntity.RefundAmount!=null)
                strSql.Append( "[RefundAmount]=@RefundAmount,");
            if (pIsUpdateNullField || pEntity.ConfirmAmount!=null)
                strSql.Append( "[ConfirmAmount]=@ConfirmAmount,");
            if (pIsUpdateNullField || pEntity.ActualRefundAmount!=null)
                strSql.Append( "[ActualRefundAmount]=@ActualRefundAmount,");
            if (pIsUpdateNullField || pEntity.Points!=null)
                strSql.Append( "[Points]=@Points,");
            if (pIsUpdateNullField || pEntity.PointsAmount!=null)
                strSql.Append( "[PointsAmount]=@PointsAmount,");
            if (pIsUpdateNullField || pEntity.ReturnAmount!=null)
                strSql.Append( "[ReturnAmount]=@ReturnAmount,");
            if (pIsUpdateNullField || pEntity.Amount!=null)
                strSql.Append( "[Amount]=@Amount,");
            if (pIsUpdateNullField || pEntity.CouponID!=null)
                strSql.Append( "[CouponID]=@CouponID,");
            if (pIsUpdateNullField || pEntity.PayOrderID!=null)
                strSql.Append( "[PayOrderID]=@PayOrderID,");
            if (pIsUpdateNullField || pEntity.Status!=null)
                strSql.Append( "[Status]=@Status,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy");
            strSql.Append(" where RefundID=@RefundID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@SalesReturnID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@RefundNo",SqlDbType.NVarChar),
					new SqlParameter("@VipID",SqlDbType.NVarChar),
					new SqlParameter("@DeliveryType",SqlDbType.Int),
					new SqlParameter("@OrderID",SqlDbType.NVarChar),
					new SqlParameter("@ItemID",SqlDbType.NVarChar),
					new SqlParameter("@SkuID",SqlDbType.NVarChar),
					new SqlParameter("@Qty",SqlDbType.Int),
					new SqlParameter("@ActualQty",SqlDbType.Int),
					new SqlParameter("@UnitID",SqlDbType.NVarChar),
					new SqlParameter("@UnitName",SqlDbType.NVarChar),
					new SqlParameter("@UnitTel",SqlDbType.NVarChar),
					new SqlParameter("@Address",SqlDbType.NVarChar),
					new SqlParameter("@Contacts",SqlDbType.NVarChar),
					new SqlParameter("@Phone",SqlDbType.NVarChar),
					new SqlParameter("@RefundAmount",SqlDbType.Decimal),
					new SqlParameter("@ConfirmAmount",SqlDbType.Decimal),
					new SqlParameter("@ActualRefundAmount",SqlDbType.Decimal),
					new SqlParameter("@Points",SqlDbType.Int),
					new SqlParameter("@PointsAmount",SqlDbType.Decimal),
					new SqlParameter("@ReturnAmount",SqlDbType.Decimal),
					new SqlParameter("@Amount",SqlDbType.Decimal),
					new SqlParameter("@CouponID",SqlDbType.NVarChar),
					new SqlParameter("@PayOrderID",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@RefundID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.SalesReturnID;
			parameters[1].Value = pEntity.RefundNo;
			parameters[2].Value = pEntity.VipID;
			parameters[3].Value = pEntity.DeliveryType;
			parameters[4].Value = pEntity.OrderID;
			parameters[5].Value = pEntity.ItemID;
			parameters[6].Value = pEntity.SkuID;
			parameters[7].Value = pEntity.Qty;
			parameters[8].Value = pEntity.ActualQty;
			parameters[9].Value = pEntity.UnitID;
			parameters[10].Value = pEntity.UnitName;
			parameters[11].Value = pEntity.UnitTel;
			parameters[12].Value = pEntity.Address;
			parameters[13].Value = pEntity.Contacts;
			parameters[14].Value = pEntity.Phone;
			parameters[15].Value = pEntity.RefundAmount;
			parameters[16].Value = pEntity.ConfirmAmount;
			parameters[17].Value = pEntity.ActualRefundAmount;
			parameters[18].Value = pEntity.Points;
			parameters[19].Value = pEntity.PointsAmount;
			parameters[20].Value = pEntity.ReturnAmount;
			parameters[21].Value = pEntity.Amount;
			parameters[22].Value = pEntity.CouponID;
			parameters[23].Value = pEntity.PayOrderID;
			parameters[24].Value = pEntity.Status;
			parameters[25].Value = pEntity.CustomerID;
			parameters[26].Value = pEntity.LastUpdateTime;
			parameters[27].Value = pEntity.LastUpdateBy;
			parameters[28].Value = pEntity.RefundID;

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
        public void Update(T_RefundOrderEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(T_RefundOrderEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(T_RefundOrderEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.RefundID.HasValue)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.RefundID.Value, pTran);           
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
            sql.AppendLine("update [T_RefundOrder] set  isdelete=1 where RefundID=@RefundID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@RefundID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(T_RefundOrderEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.RefundID.HasValue)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.RefundID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(T_RefundOrderEntity[] pEntities)
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
            sql.AppendLine("update [T_RefundOrder] set  isdelete=1 where RefundID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public T_RefundOrderEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_RefundOrder] where 1=1  and isdelete=0 ");
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
            List<T_RefundOrderEntity> list = new List<T_RefundOrderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_RefundOrderEntity m;
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
        public PagedQueryResult<T_RefundOrderEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [RefundID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [T_RefundOrder] where 1=1  and isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [T_RefundOrder] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<T_RefundOrderEntity> result = new PagedQueryResult<T_RefundOrderEntity>();
            List<T_RefundOrderEntity> list = new List<T_RefundOrderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    T_RefundOrderEntity m;
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
        public T_RefundOrderEntity[] QueryByEntity(T_RefundOrderEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<T_RefundOrderEntity> PagedQueryByEntity(T_RefundOrderEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(T_RefundOrderEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.RefundID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RefundID", Value = pQueryEntity.RefundID });
            if (pQueryEntity.SalesReturnID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesReturnID", Value = pQueryEntity.SalesReturnID });
            if (pQueryEntity.RefundNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RefundNo", Value = pQueryEntity.RefundNo });
            if (pQueryEntity.VipID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipID", Value = pQueryEntity.VipID });
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
            if (pQueryEntity.RefundAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RefundAmount", Value = pQueryEntity.RefundAmount });
            if (pQueryEntity.ConfirmAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ConfirmAmount", Value = pQueryEntity.ConfirmAmount });
            if (pQueryEntity.ActualRefundAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ActualRefundAmount", Value = pQueryEntity.ActualRefundAmount });
            if (pQueryEntity.Points!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Points", Value = pQueryEntity.Points });
            if (pQueryEntity.PointsAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PointsAmount", Value = pQueryEntity.PointsAmount });
            if (pQueryEntity.ReturnAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReturnAmount", Value = pQueryEntity.ReturnAmount });
            if (pQueryEntity.Amount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Amount", Value = pQueryEntity.Amount });
            if (pQueryEntity.CouponID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CouponID", Value = pQueryEntity.CouponID });
            if (pQueryEntity.PayOrderID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PayOrderID", Value = pQueryEntity.PayOrderID });
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
        protected void Load(IDataReader pReader, out T_RefundOrderEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new T_RefundOrderEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["RefundID"] != DBNull.Value)
			{
				pInstance.RefundID =  (Guid)pReader["RefundID"];
			}
			if (pReader["SalesReturnID"] != DBNull.Value)
			{
				pInstance.SalesReturnID =  (Guid)pReader["SalesReturnID"];
			}
			if (pReader["RefundNo"] != DBNull.Value)
			{
				pInstance.RefundNo =  Convert.ToString(pReader["RefundNo"]);
			}
			if (pReader["VipID"] != DBNull.Value)
			{
				pInstance.VipID =  Convert.ToString(pReader["VipID"]);
                var vipDao = new VipDAO(this.CurrentUserInfo);
                var vipInfo = vipDao.GetByID(pInstance.VipID);
                if (vipInfo != null)
                    pInstance.VipName = vipInfo.VipName;
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
                    pInstance.OrderNo = inoutInfo.order_no;//������
                    pInstance.PayOrderID = inoutInfo.paymentcenter_id;//֧���ص���ʶ

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
			if (pReader["RefundAmount"] != DBNull.Value)
			{
				pInstance.RefundAmount =  Convert.ToDecimal(pReader["RefundAmount"]);
			}
			if (pReader["ConfirmAmount"] != DBNull.Value)
			{
				pInstance.ConfirmAmount =  Convert.ToDecimal(pReader["ConfirmAmount"]);
			}
			if (pReader["ActualRefundAmount"] != DBNull.Value)
			{
				pInstance.ActualRefundAmount =  Convert.ToDecimal(pReader["ActualRefundAmount"]);
			}
			if (pReader["Points"] != DBNull.Value)
			{
				pInstance.Points =   Convert.ToInt32(pReader["Points"]);
			}
			if (pReader["PointsAmount"] != DBNull.Value)
			{
				pInstance.PointsAmount =  Convert.ToDecimal(pReader["PointsAmount"]);
			}
			if (pReader["ReturnAmount"] != DBNull.Value)
			{
				pInstance.ReturnAmount =  Convert.ToDecimal(pReader["ReturnAmount"]);
			}
			if (pReader["Amount"] != DBNull.Value)
			{
				pInstance.Amount =  Convert.ToDecimal(pReader["Amount"]);
			}
			if (pReader["CouponID"] != DBNull.Value)
			{
				pInstance.CouponID =  Convert.ToString(pReader["CouponID"]);
			}
			if (pReader["PayOrderID"] != DBNull.Value)
			{
				pInstance.PayOrderID =  Convert.ToString(pReader["PayOrderID"]);
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
