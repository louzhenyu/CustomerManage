/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/21 18:19:53
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
    /// ��HotelsOrder�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class HotelsOrderDAO : Base.BaseCPOSDAO, ICRUDable<HotelsOrderEntity>, IQueryable<HotelsOrderEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public HotelsOrderDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(HotelsOrderEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(HotelsOrderEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [HotelsOrder](");
            strSql.Append("[OrderNo],[OrderTypeId],[OrderDate],[CustomerId],[UnitId],[ReservationsVipId],[ReservationsTime],[Contact],[ContactPhone],[PaymentOrderNo],[PaymentTypeId],[PaymentTime],[PaymentStatus],[BeginDate],[EndDate],[ShopTime],[CheckDaysQty],[RoomQty],[StdTotalAmount],[SalesTotalAmount],[RetailTotalAmount],[DiscountRate],[RetailNeedTotalAmount],[PointsDAmount],[CouponDAmount],[OverDAmount],[CashBackAmount],[OrderSource],[ChannelSource],[OrderStatus],[OrderStatusDesc],[Remark],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[OrderId])");
            strSql.Append(" values (");
            strSql.Append("@OrderNo,@OrderTypeId,@OrderDate,@CustomerId,@UnitId,@ReservationsVipId,@ReservationsTime,@Contact,@ContactPhone,@PaymentOrderNo,@PaymentTypeId,@PaymentTime,@PaymentStatus,@BeginDate,@EndDate,@ShopTime,@CheckDaysQty,@RoomQty,@StdTotalAmount,@SalesTotalAmount,@RetailTotalAmount,@DiscountRate,@RetailNeedTotalAmount,@PointsDAmount,@CouponDAmount,@OverDAmount,@CashBackAmount,@OrderSource,@ChannelSource,@OrderStatus,@OrderStatusDesc,@Remark,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@OrderId)");            

			string pkString = pEntity.OrderId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@OrderNo",SqlDbType.NVarChar),
					new SqlParameter("@OrderTypeId",SqlDbType.Int),
					new SqlParameter("@OrderDate",SqlDbType.NVarChar),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@UnitId",SqlDbType.NVarChar),
					new SqlParameter("@ReservationsVipId",SqlDbType.NVarChar),
					new SqlParameter("@ReservationsTime",SqlDbType.DateTime),
					new SqlParameter("@Contact",SqlDbType.NVarChar),
					new SqlParameter("@ContactPhone",SqlDbType.NVarChar),
					new SqlParameter("@PaymentOrderNo",SqlDbType.NVarChar),
					new SqlParameter("@PaymentTypeId",SqlDbType.NVarChar),
					new SqlParameter("@PaymentTime",SqlDbType.DateTime),
					new SqlParameter("@PaymentStatus",SqlDbType.Int),
					new SqlParameter("@BeginDate",SqlDbType.DateTime),
					new SqlParameter("@EndDate",SqlDbType.DateTime),
					new SqlParameter("@ShopTime",SqlDbType.NVarChar),
					new SqlParameter("@CheckDaysQty",SqlDbType.Int),
					new SqlParameter("@RoomQty",SqlDbType.Int),
					new SqlParameter("@StdTotalAmount",SqlDbType.Decimal),
					new SqlParameter("@SalesTotalAmount",SqlDbType.Decimal),
					new SqlParameter("@RetailTotalAmount",SqlDbType.Decimal),
					new SqlParameter("@DiscountRate",SqlDbType.Decimal),
					new SqlParameter("@RetailNeedTotalAmount",SqlDbType.Decimal),
					new SqlParameter("@PointsDAmount",SqlDbType.Decimal),
					new SqlParameter("@CouponDAmount",SqlDbType.Decimal),
					new SqlParameter("@OverDAmount",SqlDbType.Decimal),
					new SqlParameter("@CashBackAmount",SqlDbType.Decimal),
					new SqlParameter("@OrderSource",SqlDbType.Decimal),
					new SqlParameter("@ChannelSource",SqlDbType.NVarChar),
					new SqlParameter("@OrderStatus",SqlDbType.Int),
					new SqlParameter("@OrderStatusDesc",SqlDbType.NVarChar),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@OrderId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.OrderNo;
			parameters[1].Value = pEntity.OrderTypeId;
			parameters[2].Value = pEntity.OrderDate;
			parameters[3].Value = pEntity.CustomerId;
			parameters[4].Value = pEntity.UnitId;
			parameters[5].Value = pEntity.ReservationsVipId;
			parameters[6].Value = pEntity.ReservationsTime;
			parameters[7].Value = pEntity.Contact;
			parameters[8].Value = pEntity.ContactPhone;
			parameters[9].Value = pEntity.PaymentOrderNo;
			parameters[10].Value = pEntity.PaymentTypeId;
			parameters[11].Value = pEntity.PaymentTime;
			parameters[12].Value = pEntity.PaymentStatus;
			parameters[13].Value = pEntity.BeginDate;
			parameters[14].Value = pEntity.EndDate;
			parameters[15].Value = pEntity.ShopTime;
			parameters[16].Value = pEntity.CheckDaysQty;
			parameters[17].Value = pEntity.RoomQty;
			parameters[18].Value = pEntity.StdTotalAmount;
			parameters[19].Value = pEntity.SalesTotalAmount;
			parameters[20].Value = pEntity.RetailTotalAmount;
			parameters[21].Value = pEntity.DiscountRate;
			parameters[22].Value = pEntity.RetailNeedTotalAmount;
			parameters[23].Value = pEntity.PointsDAmount;
			parameters[24].Value = pEntity.CouponDAmount;
			parameters[25].Value = pEntity.OverDAmount;
			parameters[26].Value = pEntity.CashBackAmount;
			parameters[27].Value = pEntity.OrderSource;
			parameters[28].Value = pEntity.ChannelSource;
			parameters[29].Value = pEntity.OrderStatus;
			parameters[30].Value = pEntity.OrderStatusDesc;
			parameters[31].Value = pEntity.Remark;
			parameters[32].Value = pEntity.CreateBy;
			parameters[33].Value = pEntity.CreateTime;
			parameters[34].Value = pEntity.LastUpdateBy;
			parameters[35].Value = pEntity.LastUpdateTime;
			parameters[36].Value = pEntity.IsDelete;
			parameters[37].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.OrderId = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public HotelsOrderEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [HotelsOrder] where OrderId='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            HotelsOrderEntity m = null;
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
        public HotelsOrderEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [HotelsOrder] where isdelete=0");
            //��ȡ����
            List<HotelsOrderEntity> list = new List<HotelsOrderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    HotelsOrderEntity m;
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
        public void Update(HotelsOrderEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(HotelsOrderEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.OrderId==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [HotelsOrder] set ");
            if (pIsUpdateNullField || pEntity.OrderNo!=null)
                strSql.Append( "[OrderNo]=@OrderNo,");
            if (pIsUpdateNullField || pEntity.OrderTypeId!=null)
                strSql.Append( "[OrderTypeId]=@OrderTypeId,");
            if (pIsUpdateNullField || pEntity.OrderDate!=null)
                strSql.Append( "[OrderDate]=@OrderDate,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.UnitId!=null)
                strSql.Append( "[UnitId]=@UnitId,");
            if (pIsUpdateNullField || pEntity.ReservationsVipId!=null)
                strSql.Append( "[ReservationsVipId]=@ReservationsVipId,");
            if (pIsUpdateNullField || pEntity.ReservationsTime!=null)
                strSql.Append( "[ReservationsTime]=@ReservationsTime,");
            if (pIsUpdateNullField || pEntity.Contact!=null)
                strSql.Append( "[Contact]=@Contact,");
            if (pIsUpdateNullField || pEntity.ContactPhone!=null)
                strSql.Append( "[ContactPhone]=@ContactPhone,");
            if (pIsUpdateNullField || pEntity.PaymentOrderNo!=null)
                strSql.Append( "[PaymentOrderNo]=@PaymentOrderNo,");
            if (pIsUpdateNullField || pEntity.PaymentTypeId!=null)
                strSql.Append( "[PaymentTypeId]=@PaymentTypeId,");
            if (pIsUpdateNullField || pEntity.PaymentTime!=null)
                strSql.Append( "[PaymentTime]=@PaymentTime,");
            if (pIsUpdateNullField || pEntity.PaymentStatus!=null)
                strSql.Append( "[PaymentStatus]=@PaymentStatus,");
            if (pIsUpdateNullField || pEntity.BeginDate!=null)
                strSql.Append( "[BeginDate]=@BeginDate,");
            if (pIsUpdateNullField || pEntity.EndDate!=null)
                strSql.Append( "[EndDate]=@EndDate,");
            if (pIsUpdateNullField || pEntity.ShopTime!=null)
                strSql.Append( "[ShopTime]=@ShopTime,");
            if (pIsUpdateNullField || pEntity.CheckDaysQty!=null)
                strSql.Append( "[CheckDaysQty]=@CheckDaysQty,");
            if (pIsUpdateNullField || pEntity.RoomQty!=null)
                strSql.Append( "[RoomQty]=@RoomQty,");
            if (pIsUpdateNullField || pEntity.StdTotalAmount!=null)
                strSql.Append( "[StdTotalAmount]=@StdTotalAmount,");
            if (pIsUpdateNullField || pEntity.SalesTotalAmount!=null)
                strSql.Append( "[SalesTotalAmount]=@SalesTotalAmount,");
            if (pIsUpdateNullField || pEntity.RetailTotalAmount!=null)
                strSql.Append( "[RetailTotalAmount]=@RetailTotalAmount,");
            if (pIsUpdateNullField || pEntity.DiscountRate!=null)
                strSql.Append( "[DiscountRate]=@DiscountRate,");
            if (pIsUpdateNullField || pEntity.RetailNeedTotalAmount!=null)
                strSql.Append( "[RetailNeedTotalAmount]=@RetailNeedTotalAmount,");
            if (pIsUpdateNullField || pEntity.PointsDAmount!=null)
                strSql.Append( "[PointsDAmount]=@PointsDAmount,");
            if (pIsUpdateNullField || pEntity.CouponDAmount!=null)
                strSql.Append( "[CouponDAmount]=@CouponDAmount,");
            if (pIsUpdateNullField || pEntity.OverDAmount!=null)
                strSql.Append( "[OverDAmount]=@OverDAmount,");
            if (pIsUpdateNullField || pEntity.CashBackAmount!=null)
                strSql.Append( "[CashBackAmount]=@CashBackAmount,");
            if (pIsUpdateNullField || pEntity.OrderSource!=null)
                strSql.Append( "[OrderSource]=@OrderSource,");
            if (pIsUpdateNullField || pEntity.ChannelSource!=null)
                strSql.Append( "[ChannelSource]=@ChannelSource,");
            if (pIsUpdateNullField || pEntity.OrderStatus!=null)
                strSql.Append( "[OrderStatus]=@OrderStatus,");
            if (pIsUpdateNullField || pEntity.OrderStatusDesc!=null)
                strSql.Append( "[OrderStatusDesc]=@OrderStatusDesc,");
            if (pIsUpdateNullField || pEntity.Remark!=null)
                strSql.Append( "[Remark]=@Remark,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where OrderId=@OrderId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@OrderNo",SqlDbType.NVarChar),
					new SqlParameter("@OrderTypeId",SqlDbType.Int),
					new SqlParameter("@OrderDate",SqlDbType.NVarChar),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@UnitId",SqlDbType.NVarChar),
					new SqlParameter("@ReservationsVipId",SqlDbType.NVarChar),
					new SqlParameter("@ReservationsTime",SqlDbType.DateTime),
					new SqlParameter("@Contact",SqlDbType.NVarChar),
					new SqlParameter("@ContactPhone",SqlDbType.NVarChar),
					new SqlParameter("@PaymentOrderNo",SqlDbType.NVarChar),
					new SqlParameter("@PaymentTypeId",SqlDbType.NVarChar),
					new SqlParameter("@PaymentTime",SqlDbType.DateTime),
					new SqlParameter("@PaymentStatus",SqlDbType.Int),
					new SqlParameter("@BeginDate",SqlDbType.DateTime),
					new SqlParameter("@EndDate",SqlDbType.DateTime),
					new SqlParameter("@ShopTime",SqlDbType.NVarChar),
					new SqlParameter("@CheckDaysQty",SqlDbType.Int),
					new SqlParameter("@RoomQty",SqlDbType.Int),
					new SqlParameter("@StdTotalAmount",SqlDbType.Decimal),
					new SqlParameter("@SalesTotalAmount",SqlDbType.Decimal),
					new SqlParameter("@RetailTotalAmount",SqlDbType.Decimal),
					new SqlParameter("@DiscountRate",SqlDbType.Decimal),
					new SqlParameter("@RetailNeedTotalAmount",SqlDbType.Decimal),
					new SqlParameter("@PointsDAmount",SqlDbType.Decimal),
					new SqlParameter("@CouponDAmount",SqlDbType.Decimal),
					new SqlParameter("@OverDAmount",SqlDbType.Decimal),
					new SqlParameter("@CashBackAmount",SqlDbType.Decimal),
					new SqlParameter("@OrderSource",SqlDbType.Decimal),
					new SqlParameter("@ChannelSource",SqlDbType.NVarChar),
					new SqlParameter("@OrderStatus",SqlDbType.Int),
					new SqlParameter("@OrderStatusDesc",SqlDbType.NVarChar),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@OrderId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.OrderNo;
			parameters[1].Value = pEntity.OrderTypeId;
			parameters[2].Value = pEntity.OrderDate;
			parameters[3].Value = pEntity.CustomerId;
			parameters[4].Value = pEntity.UnitId;
			parameters[5].Value = pEntity.ReservationsVipId;
			parameters[6].Value = pEntity.ReservationsTime;
			parameters[7].Value = pEntity.Contact;
			parameters[8].Value = pEntity.ContactPhone;
			parameters[9].Value = pEntity.PaymentOrderNo;
			parameters[10].Value = pEntity.PaymentTypeId;
			parameters[11].Value = pEntity.PaymentTime;
			parameters[12].Value = pEntity.PaymentStatus;
			parameters[13].Value = pEntity.BeginDate;
			parameters[14].Value = pEntity.EndDate;
			parameters[15].Value = pEntity.ShopTime;
			parameters[16].Value = pEntity.CheckDaysQty;
			parameters[17].Value = pEntity.RoomQty;
			parameters[18].Value = pEntity.StdTotalAmount;
			parameters[19].Value = pEntity.SalesTotalAmount;
			parameters[20].Value = pEntity.RetailTotalAmount;
			parameters[21].Value = pEntity.DiscountRate;
			parameters[22].Value = pEntity.RetailNeedTotalAmount;
			parameters[23].Value = pEntity.PointsDAmount;
			parameters[24].Value = pEntity.CouponDAmount;
			parameters[25].Value = pEntity.OverDAmount;
			parameters[26].Value = pEntity.CashBackAmount;
			parameters[27].Value = pEntity.OrderSource;
			parameters[28].Value = pEntity.ChannelSource;
			parameters[29].Value = pEntity.OrderStatus;
			parameters[30].Value = pEntity.OrderStatusDesc;
			parameters[31].Value = pEntity.Remark;
			parameters[32].Value = pEntity.LastUpdateBy;
			parameters[33].Value = pEntity.LastUpdateTime;
			parameters[34].Value = pEntity.OrderId;

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
        public void Update(HotelsOrderEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(HotelsOrderEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(HotelsOrderEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(HotelsOrderEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.OrderId==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.OrderId, pTran);           
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
            sql.AppendLine("update [HotelsOrder] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where OrderId=@OrderId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@OrderId",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(HotelsOrderEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.OrderId==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.OrderId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(HotelsOrderEntity[] pEntities)
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
            sql.AppendLine("update [HotelsOrder] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where OrderId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public HotelsOrderEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [HotelsOrder] where isdelete=0 ");
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
            List<HotelsOrderEntity> list = new List<HotelsOrderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    HotelsOrderEntity m;
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
        public PagedQueryResult<HotelsOrderEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [OrderId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [HotelsOrder] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [HotelsOrder] where isdelete=0 ");
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
            PagedQueryResult<HotelsOrderEntity> result = new PagedQueryResult<HotelsOrderEntity>();
            List<HotelsOrderEntity> list = new List<HotelsOrderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    HotelsOrderEntity m;
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
        public HotelsOrderEntity[] QueryByEntity(HotelsOrderEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<HotelsOrderEntity> PagedQueryByEntity(HotelsOrderEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(HotelsOrderEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.OrderId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderId", Value = pQueryEntity.OrderId });
            if (pQueryEntity.OrderNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderNo", Value = pQueryEntity.OrderNo });
            if (pQueryEntity.OrderTypeId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderTypeId", Value = pQueryEntity.OrderTypeId });
            if (pQueryEntity.OrderDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderDate", Value = pQueryEntity.OrderDate });
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
            if (pQueryEntity.UnitId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitId", Value = pQueryEntity.UnitId });
            if (pQueryEntity.ReservationsVipId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReservationsVipId", Value = pQueryEntity.ReservationsVipId });
            if (pQueryEntity.ReservationsTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReservationsTime", Value = pQueryEntity.ReservationsTime });
            if (pQueryEntity.Contact!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Contact", Value = pQueryEntity.Contact });
            if (pQueryEntity.ContactPhone!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ContactPhone", Value = pQueryEntity.ContactPhone });
            if (pQueryEntity.PaymentOrderNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PaymentOrderNo", Value = pQueryEntity.PaymentOrderNo });
            if (pQueryEntity.PaymentTypeId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PaymentTypeId", Value = pQueryEntity.PaymentTypeId });
            if (pQueryEntity.PaymentTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PaymentTime", Value = pQueryEntity.PaymentTime });
            if (pQueryEntity.PaymentStatus!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PaymentStatus", Value = pQueryEntity.PaymentStatus });
            if (pQueryEntity.BeginDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BeginDate", Value = pQueryEntity.BeginDate });
            if (pQueryEntity.EndDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EndDate", Value = pQueryEntity.EndDate });
            if (pQueryEntity.ShopTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ShopTime", Value = pQueryEntity.ShopTime });
            if (pQueryEntity.CheckDaysQty!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CheckDaysQty", Value = pQueryEntity.CheckDaysQty });
            if (pQueryEntity.RoomQty!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RoomQty", Value = pQueryEntity.RoomQty });
            if (pQueryEntity.StdTotalAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StdTotalAmount", Value = pQueryEntity.StdTotalAmount });
            if (pQueryEntity.SalesTotalAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesTotalAmount", Value = pQueryEntity.SalesTotalAmount });
            if (pQueryEntity.RetailTotalAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RetailTotalAmount", Value = pQueryEntity.RetailTotalAmount });
            if (pQueryEntity.DiscountRate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DiscountRate", Value = pQueryEntity.DiscountRate });
            if (pQueryEntity.RetailNeedTotalAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RetailNeedTotalAmount", Value = pQueryEntity.RetailNeedTotalAmount });
            if (pQueryEntity.PointsDAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PointsDAmount", Value = pQueryEntity.PointsDAmount });
            if (pQueryEntity.CouponDAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CouponDAmount", Value = pQueryEntity.CouponDAmount });
            if (pQueryEntity.OverDAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OverDAmount", Value = pQueryEntity.OverDAmount });
            if (pQueryEntity.CashBackAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CashBackAmount", Value = pQueryEntity.CashBackAmount });
            if (pQueryEntity.OrderSource!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderSource", Value = pQueryEntity.OrderSource });
            if (pQueryEntity.ChannelSource!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ChannelSource", Value = pQueryEntity.ChannelSource });
            if (pQueryEntity.OrderStatus!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderStatus", Value = pQueryEntity.OrderStatus });
            if (pQueryEntity.OrderStatusDesc!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderStatusDesc", Value = pQueryEntity.OrderStatusDesc });
            if (pQueryEntity.Remark!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Remark", Value = pQueryEntity.Remark });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.LastUpdateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.LastUpdateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.IsDelete!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out HotelsOrderEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new HotelsOrderEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["OrderId"] != DBNull.Value)
			{
				pInstance.OrderId =  Convert.ToString(pReader["OrderId"]);
			}
			if (pReader["OrderNo"] != DBNull.Value)
			{
				pInstance.OrderNo =  Convert.ToString(pReader["OrderNo"]);
			}
			if (pReader["OrderTypeId"] != DBNull.Value)
			{
				pInstance.OrderTypeId =   Convert.ToInt32(pReader["OrderTypeId"]);
			}
			if (pReader["OrderDate"] != DBNull.Value)
			{
				pInstance.OrderDate =  Convert.ToString(pReader["OrderDate"]);
			}
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}
			if (pReader["UnitId"] != DBNull.Value)
			{
				pInstance.UnitId =  Convert.ToString(pReader["UnitId"]);
			}
			if (pReader["ReservationsVipId"] != DBNull.Value)
			{
				pInstance.ReservationsVipId =  Convert.ToString(pReader["ReservationsVipId"]);
			}
			if (pReader["ReservationsTime"] != DBNull.Value)
			{
				pInstance.ReservationsTime =  Convert.ToDateTime(pReader["ReservationsTime"]);
			}
			if (pReader["Contact"] != DBNull.Value)
			{
				pInstance.Contact =  Convert.ToString(pReader["Contact"]);
			}
			if (pReader["ContactPhone"] != DBNull.Value)
			{
				pInstance.ContactPhone =  Convert.ToString(pReader["ContactPhone"]);
			}
			if (pReader["PaymentOrderNo"] != DBNull.Value)
			{
				pInstance.PaymentOrderNo =  Convert.ToString(pReader["PaymentOrderNo"]);
			}
			if (pReader["PaymentTypeId"] != DBNull.Value)
			{
				pInstance.PaymentTypeId =  Convert.ToString(pReader["PaymentTypeId"]);
			}
			if (pReader["PaymentTime"] != DBNull.Value)
			{
				pInstance.PaymentTime =  Convert.ToDateTime(pReader["PaymentTime"]);
			}
			if (pReader["PaymentStatus"] != DBNull.Value)
			{
				pInstance.PaymentStatus =   Convert.ToInt32(pReader["PaymentStatus"]);
			}
			if (pReader["BeginDate"] != DBNull.Value)
			{
				pInstance.BeginDate =  Convert.ToDateTime(pReader["BeginDate"]);
			}
			if (pReader["EndDate"] != DBNull.Value)
			{
				pInstance.EndDate =  Convert.ToDateTime(pReader["EndDate"]);
			}
			if (pReader["ShopTime"] != DBNull.Value)
			{
				pInstance.ShopTime =  Convert.ToString(pReader["ShopTime"]);
			}
			if (pReader["CheckDaysQty"] != DBNull.Value)
			{
				pInstance.CheckDaysQty =   Convert.ToInt32(pReader["CheckDaysQty"]);
			}
			if (pReader["RoomQty"] != DBNull.Value)
			{
				pInstance.RoomQty =   Convert.ToInt32(pReader["RoomQty"]);
			}
			if (pReader["StdTotalAmount"] != DBNull.Value)
			{
				pInstance.StdTotalAmount =  Convert.ToDecimal(pReader["StdTotalAmount"]);
			}
			if (pReader["SalesTotalAmount"] != DBNull.Value)
			{
				pInstance.SalesTotalAmount =  Convert.ToDecimal(pReader["SalesTotalAmount"]);
			}
			if (pReader["RetailTotalAmount"] != DBNull.Value)
			{
				pInstance.RetailTotalAmount =  Convert.ToDecimal(pReader["RetailTotalAmount"]);
			}
			if (pReader["DiscountRate"] != DBNull.Value)
			{
				pInstance.DiscountRate =  Convert.ToDecimal(pReader["DiscountRate"]);
			}
			if (pReader["RetailNeedTotalAmount"] != DBNull.Value)
			{
				pInstance.RetailNeedTotalAmount =  Convert.ToDecimal(pReader["RetailNeedTotalAmount"]);
			}
			if (pReader["PointsDAmount"] != DBNull.Value)
			{
				pInstance.PointsDAmount =  Convert.ToDecimal(pReader["PointsDAmount"]);
			}
			if (pReader["CouponDAmount"] != DBNull.Value)
			{
				pInstance.CouponDAmount =  Convert.ToDecimal(pReader["CouponDAmount"]);
			}
			if (pReader["OverDAmount"] != DBNull.Value)
			{
				pInstance.OverDAmount =  Convert.ToDecimal(pReader["OverDAmount"]);
			}
			if (pReader["CashBackAmount"] != DBNull.Value)
			{
				pInstance.CashBackAmount =  Convert.ToDecimal(pReader["CashBackAmount"]);
			}
			if (pReader["OrderSource"] != DBNull.Value)
			{
				pInstance.OrderSource =  Convert.ToDecimal(pReader["OrderSource"]);
			}
			if (pReader["ChannelSource"] != DBNull.Value)
			{
				pInstance.ChannelSource =  Convert.ToString(pReader["ChannelSource"]);
			}
			if (pReader["OrderStatus"] != DBNull.Value)
			{
				pInstance.OrderStatus =   Convert.ToInt32(pReader["OrderStatus"]);
			}
			if (pReader["OrderStatusDesc"] != DBNull.Value)
			{
				pInstance.OrderStatusDesc =  Convert.ToString(pReader["OrderStatusDesc"]);
			}
			if (pReader["Remark"] != DBNull.Value)
			{
				pInstance.Remark =  Convert.ToString(pReader["Remark"]);
			}
			if (pReader["CreateBy"] != DBNull.Value)
			{
				pInstance.CreateBy =  Convert.ToString(pReader["CreateBy"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}
			if (pReader["LastUpdateBy"] != DBNull.Value)
			{
				pInstance.LastUpdateBy =  Convert.ToString(pReader["LastUpdateBy"]);
			}
			if (pReader["LastUpdateTime"] != DBNull.Value)
			{
				pInstance.LastUpdateTime =  Convert.ToDateTime(pReader["LastUpdateTime"]);
			}
			if (pReader["IsDelete"] != DBNull.Value)
			{
				pInstance.IsDelete =   Convert.ToInt32(pReader["IsDelete"]);
			}

        }
        #endregion
    }
}
