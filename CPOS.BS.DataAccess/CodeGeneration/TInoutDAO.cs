/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/2/28 14:14:45
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
using JIT.CPOS.BS.DataAccess.Base;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Utility;

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// 数据访问：  
    /// 表T_Inout的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class TInoutDAO : BaseCPOSDAO, ICRUDable<TInoutEntity>, IQueryable<TInoutEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TInoutDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(TInoutEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(TInoutEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [T_Inout](");
            strSql.Append("[order_no],[order_type_id],[order_reason_id],[red_flag],[ref_order_id],[ref_order_no],[warehouse_id],[order_date],[request_date],[complete_date],[create_unit_id],[unit_id],[related_unit_id],[related_unit_code],[pos_id],[shift_id],[sales_user],[total_amount],[discount_rate],[actual_amount],[receive_points],[pay_points],[pay_id],[print_times],[carrier_id],[remark],[status],[status_desc],[total_qty],[total_retail],[keep_the_change],[wiping_zero],[vip_no],[create_time],[create_user_id],[approve_time],[approve_user_id],[send_time],[send_user_id],[accpect_time],[accpect_user_id],[modify_time],[modify_user_id],[data_from_id],[sales_unit_id],[purchase_unit_id],[if_flag],[customer_id],[bat_id],[sales_warehouse_id],[purchase_warehouse_id],[Field1],[Field2],[Field3],[Field4],[Field5],[Field6],[Field7],[Field8],[Field9],[Field10],[Field11],[Field12],[Field13],[Field14],[Field15],[Field16],[Field17],[Field18],[Field19],[Field20],[paymentcenter_id],[order_id])");
            strSql.Append(" values (");
            strSql.Append("@OrderNo,@OrderTypeID,@OrderReasonID,@RedFlag,@RefOrderID,@RefOrderNo,@WarehouseID,@OrderDate,@RequestDate,@CompleteDate,@CreateUnitID,@UnitID,@RelatedUnitID,@RelatedUnitCode,@PosID,@ShiftID,@SalesUser,@TotalAmount,@DiscountRate,@ActualAmount,@ReceivePoints,@PayPoints,@PayID,@PrintTimes,@CarrierID,@Remark,@Status,@StatusDesc,@TotalQty,@TotalRetail,@KeepTheChange,@WipingZero,@VipNo,@CreateTime,@CreateUserID,@ApproveTime,@ApproveUserID,@SendTime,@SendUserID,@AccpectTime,@AccpectUserID,@ModifyTime,@ModifyUserID,@DataFromID,@SalesUnitID,@PurchaseUnitID,@IfFlag,@CustomerID,@BatID,@SalesWarehouseID,@PurchaseWarehouseID,@Field1,@Field2,@Field3,@Field4,@Field5,@Field6,@Field7,@Field8,@Field9,@Field10,@Field11,@Field12,@Field13,@Field14,@Field15,@Field16,@Field17,@Field18,@Field19,@Field20,@PaymentcenterID,@OrderID)");            

			string pkString = pEntity.OrderID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@OrderNo",SqlDbType.NVarChar),
					new SqlParameter("@OrderTypeID",SqlDbType.NVarChar),
					new SqlParameter("@OrderReasonID",SqlDbType.NVarChar),
					new SqlParameter("@RedFlag",SqlDbType.NVarChar),
					new SqlParameter("@RefOrderID",SqlDbType.NVarChar),
					new SqlParameter("@RefOrderNo",SqlDbType.NVarChar),
					new SqlParameter("@WarehouseID",SqlDbType.NVarChar),
					new SqlParameter("@OrderDate",SqlDbType.NVarChar),
					new SqlParameter("@RequestDate",SqlDbType.NVarChar),
					new SqlParameter("@CompleteDate",SqlDbType.NVarChar),
					new SqlParameter("@CreateUnitID",SqlDbType.NVarChar),
					new SqlParameter("@UnitID",SqlDbType.NVarChar),
					new SqlParameter("@RelatedUnitID",SqlDbType.NVarChar),
					new SqlParameter("@RelatedUnitCode",SqlDbType.NVarChar),
					new SqlParameter("@PosID",SqlDbType.NVarChar),
					new SqlParameter("@ShiftID",SqlDbType.NVarChar),
					new SqlParameter("@SalesUser",SqlDbType.NVarChar),
					new SqlParameter("@TotalAmount",SqlDbType.Decimal),
					new SqlParameter("@DiscountRate",SqlDbType.Decimal),
					new SqlParameter("@ActualAmount",SqlDbType.Decimal),
					new SqlParameter("@ReceivePoints",SqlDbType.Decimal),
					new SqlParameter("@PayPoints",SqlDbType.Decimal),
					new SqlParameter("@PayID",SqlDbType.NVarChar),
					new SqlParameter("@PrintTimes",SqlDbType.Int),
					new SqlParameter("@CarrierID",SqlDbType.NVarChar),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.NVarChar),
					new SqlParameter("@StatusDesc",SqlDbType.NVarChar),
					new SqlParameter("@TotalQty",SqlDbType.Decimal),
					new SqlParameter("@TotalRetail",SqlDbType.Decimal),
					new SqlParameter("@KeepTheChange",SqlDbType.Decimal),
					new SqlParameter("@WipingZero",SqlDbType.Decimal),
					new SqlParameter("@VipNo",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.NVarChar),
					new SqlParameter("@CreateUserID",SqlDbType.NVarChar),
					new SqlParameter("@ApproveTime",SqlDbType.NVarChar),
					new SqlParameter("@ApproveUserID",SqlDbType.NVarChar),
					new SqlParameter("@SendTime",SqlDbType.NVarChar),
					new SqlParameter("@SendUserID",SqlDbType.NVarChar),
					new SqlParameter("@AccpectTime",SqlDbType.NVarChar),
					new SqlParameter("@AccpectUserID",SqlDbType.NVarChar),
					new SqlParameter("@ModifyTime",SqlDbType.NVarChar),
					new SqlParameter("@ModifyUserID",SqlDbType.NVarChar),
					new SqlParameter("@DataFromID",SqlDbType.NVarChar),
					new SqlParameter("@SalesUnitID",SqlDbType.NVarChar),
					new SqlParameter("@PurchaseUnitID",SqlDbType.NVarChar),
					new SqlParameter("@IfFlag",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@BatID",SqlDbType.NVarChar),
					new SqlParameter("@SalesWarehouseID",SqlDbType.NVarChar),
					new SqlParameter("@PurchaseWarehouseID",SqlDbType.NVarChar),
					new SqlParameter("@Field1",SqlDbType.NVarChar),
					new SqlParameter("@Field2",SqlDbType.NVarChar),
					new SqlParameter("@Field3",SqlDbType.NVarChar),
					new SqlParameter("@Field4",SqlDbType.NVarChar),
					new SqlParameter("@Field5",SqlDbType.NVarChar),
					new SqlParameter("@Field6",SqlDbType.NVarChar),
					new SqlParameter("@Field7",SqlDbType.NVarChar),
					new SqlParameter("@Field8",SqlDbType.NVarChar),
					new SqlParameter("@Field9",SqlDbType.NVarChar),
					new SqlParameter("@Field10",SqlDbType.NVarChar),
					new SqlParameter("@Field11",SqlDbType.NVarChar),
					new SqlParameter("@Field12",SqlDbType.NVarChar),
					new SqlParameter("@Field13",SqlDbType.NVarChar),
					new SqlParameter("@Field14",SqlDbType.NVarChar),
					new SqlParameter("@Field15",SqlDbType.NVarChar),
					new SqlParameter("@Field16",SqlDbType.NVarChar),
					new SqlParameter("@Field17",SqlDbType.NVarChar),
					new SqlParameter("@Field18",SqlDbType.NVarChar),
					new SqlParameter("@Field19",SqlDbType.NVarChar),
					new SqlParameter("@Field20",SqlDbType.NVarChar),
					new SqlParameter("@PaymentcenterID",SqlDbType.NVarChar),
					new SqlParameter("@OrderID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.OrderNo;
			parameters[1].Value = pEntity.OrderTypeID;
			parameters[2].Value = pEntity.OrderReasonID;
			parameters[3].Value = pEntity.RedFlag;
			parameters[4].Value = pEntity.RefOrderID;
			parameters[5].Value = pEntity.RefOrderNo;
			parameters[6].Value = pEntity.WarehouseID;
			parameters[7].Value = pEntity.OrderDate;
			parameters[8].Value = pEntity.RequestDate;
			parameters[9].Value = pEntity.CompleteDate;
			parameters[10].Value = pEntity.CreateUnitID;
			parameters[11].Value = pEntity.UnitID;
			parameters[12].Value = pEntity.RelatedUnitID;
			parameters[13].Value = pEntity.RelatedUnitCode;
			parameters[14].Value = pEntity.PosID;
			parameters[15].Value = pEntity.ShiftID;
			parameters[16].Value = pEntity.SalesUser;
			parameters[17].Value = pEntity.TotalAmount;
			parameters[18].Value = pEntity.DiscountRate;
			parameters[19].Value = pEntity.ActualAmount;
			parameters[20].Value = pEntity.ReceivePoints;
			parameters[21].Value = pEntity.PayPoints;
			parameters[22].Value = pEntity.PayID;
			parameters[23].Value = pEntity.PrintTimes;
			parameters[24].Value = pEntity.CarrierID;
			parameters[25].Value = pEntity.Remark;
			parameters[26].Value = pEntity.Status;
			parameters[27].Value = pEntity.StatusDesc;
			parameters[28].Value = pEntity.TotalQty;
			parameters[29].Value = pEntity.TotalRetail;
			parameters[30].Value = pEntity.KeepTheChange;
			parameters[31].Value = pEntity.WipingZero;
			parameters[32].Value = pEntity.VipNo;
			parameters[33].Value = pEntity.CreateTime;
			parameters[34].Value = pEntity.CreateUserID;
			parameters[35].Value = pEntity.ApproveTime;
			parameters[36].Value = pEntity.ApproveUserID;
			parameters[37].Value = pEntity.SendTime;
			parameters[38].Value = pEntity.SendUserID;
			parameters[39].Value = pEntity.AccpectTime;
			parameters[40].Value = pEntity.AccpectUserID;
			parameters[41].Value = pEntity.ModifyTime;
			parameters[42].Value = pEntity.ModifyUserID;
			parameters[43].Value = pEntity.DataFromID;
			parameters[44].Value = pEntity.SalesUnitID;
			parameters[45].Value = pEntity.PurchaseUnitID;
			parameters[46].Value = pEntity.IfFlag;
			parameters[47].Value = pEntity.CustomerID;
			parameters[48].Value = pEntity.BatID;
			parameters[49].Value = pEntity.SalesWarehouseID;
			parameters[50].Value = pEntity.PurchaseWarehouseID;
			parameters[51].Value = pEntity.Field1;
			parameters[52].Value = pEntity.Field2;
			parameters[53].Value = pEntity.Field3;
			parameters[54].Value = pEntity.Field4;
			parameters[55].Value = pEntity.Field5;
			parameters[56].Value = pEntity.Field6;
			parameters[57].Value = pEntity.Field7;
			parameters[58].Value = pEntity.Field8;
			parameters[59].Value = pEntity.Field9;
			parameters[60].Value = pEntity.Field10;
			parameters[61].Value = pEntity.Field11;
			parameters[62].Value = pEntity.Field12;
			parameters[63].Value = pEntity.Field13;
			parameters[64].Value = pEntity.Field14;
			parameters[65].Value = pEntity.Field15;
			parameters[66].Value = pEntity.Field16;
			parameters[67].Value = pEntity.Field17;
			parameters[68].Value = pEntity.Field18;
			parameters[69].Value = pEntity.Field19;
			parameters[70].Value = pEntity.Field20;
			parameters[71].Value = pEntity.PaymentcenterID;
			parameters[72].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.OrderID = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public TInoutEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Inout] where order_id='{0}' ", id.ToString());
            //读取数据
            TInoutEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                    break;
                }
            }
            //返回
            return m;
        }

        /// <summary>
        /// 获取所有实例
        /// </summary>
        /// <returns></returns>
        public TInoutEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Inout] where isdelete=0");
            //读取数据
            List<TInoutEntity> list = new List<TInoutEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    TInoutEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //返回
            return list.ToArray();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(TInoutEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(TInoutEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.OrderID == null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_Inout] set ");
                        if (pIsUpdateNullField || pEntity.OrderNo!=null)
                strSql.Append( "[order_no]=@OrderNo,");
            if (pIsUpdateNullField || pEntity.OrderTypeID!=null)
                strSql.Append( "[order_type_id]=@OrderTypeID,");
            if (pIsUpdateNullField || pEntity.OrderReasonID!=null)
                strSql.Append( "[order_reason_id]=@OrderReasonID,");
            if (pIsUpdateNullField || pEntity.RedFlag!=null)
                strSql.Append( "[red_flag]=@RedFlag,");
            if (pIsUpdateNullField || pEntity.RefOrderID!=null)
                strSql.Append( "[ref_order_id]=@RefOrderID,");
            if (pIsUpdateNullField || pEntity.RefOrderNo!=null)
                strSql.Append( "[ref_order_no]=@RefOrderNo,");
            if (pIsUpdateNullField || pEntity.WarehouseID!=null)
                strSql.Append( "[warehouse_id]=@WarehouseID,");
            if (pIsUpdateNullField || pEntity.OrderDate!=null)
                strSql.Append( "[order_date]=@OrderDate,");
            if (pIsUpdateNullField || pEntity.RequestDate!=null)
                strSql.Append( "[request_date]=@RequestDate,");
            if (pIsUpdateNullField || pEntity.CompleteDate!=null)
                strSql.Append( "[complete_date]=@CompleteDate,");
            if (pIsUpdateNullField || pEntity.CreateUnitID!=null)
                strSql.Append( "[create_unit_id]=@CreateUnitID,");
            if (pIsUpdateNullField || pEntity.UnitID!=null)
                strSql.Append( "[unit_id]=@UnitID,");
            if (pIsUpdateNullField || pEntity.RelatedUnitID!=null)
                strSql.Append( "[related_unit_id]=@RelatedUnitID,");
            if (pIsUpdateNullField || pEntity.RelatedUnitCode!=null)
                strSql.Append( "[related_unit_code]=@RelatedUnitCode,");
            if (pIsUpdateNullField || pEntity.PosID!=null)
                strSql.Append( "[pos_id]=@PosID,");
            if (pIsUpdateNullField || pEntity.ShiftID!=null)
                strSql.Append( "[shift_id]=@ShiftID,");
            if (pIsUpdateNullField || pEntity.SalesUser!=null)
                strSql.Append( "[sales_user]=@SalesUser,");
            if (pIsUpdateNullField || pEntity.TotalAmount!=null)
                strSql.Append( "[total_amount]=@TotalAmount,");
            if (pIsUpdateNullField || pEntity.DiscountRate!=null)
                strSql.Append( "[discount_rate]=@DiscountRate,");
            if (pIsUpdateNullField || pEntity.ActualAmount!=null)
                strSql.Append( "[actual_amount]=@ActualAmount,");
            if (pIsUpdateNullField || pEntity.ReceivePoints!=null)
                strSql.Append( "[receive_points]=@ReceivePoints,");
            if (pIsUpdateNullField || pEntity.PayPoints!=null)
                strSql.Append( "[pay_points]=@PayPoints,");
            if (pIsUpdateNullField || pEntity.PayID!=null)
                strSql.Append( "[pay_id]=@PayID,");
            if (pIsUpdateNullField || pEntity.PrintTimes!=null)
                strSql.Append( "[print_times]=@PrintTimes,");
            if (pIsUpdateNullField || pEntity.CarrierID!=null)
                strSql.Append( "[carrier_id]=@CarrierID,");
            if (pIsUpdateNullField || pEntity.Remark!=null)
                strSql.Append( "[remark]=@Remark,");
            if (pIsUpdateNullField || pEntity.Status!=null)
                strSql.Append( "[status]=@Status,");
            if (pIsUpdateNullField || pEntity.StatusDesc!=null)
                strSql.Append( "[status_desc]=@StatusDesc,");
            if (pIsUpdateNullField || pEntity.TotalQty!=null)
                strSql.Append( "[total_qty]=@TotalQty,");
            if (pIsUpdateNullField || pEntity.TotalRetail!=null)
                strSql.Append( "[total_retail]=@TotalRetail,");
            if (pIsUpdateNullField || pEntity.KeepTheChange!=null)
                strSql.Append( "[keep_the_change]=@KeepTheChange,");
            if (pIsUpdateNullField || pEntity.WipingZero!=null)
                strSql.Append( "[wiping_zero]=@WipingZero,");
            if (pIsUpdateNullField || pEntity.VipNo!=null)
                strSql.Append( "[vip_no]=@VipNo,");
            if (pIsUpdateNullField || pEntity.CreateUserID!=null)
                strSql.Append( "[create_user_id]=@CreateUserID,");
            if (pIsUpdateNullField || pEntity.ApproveTime!=null)
                strSql.Append( "[approve_time]=@ApproveTime,");
            if (pIsUpdateNullField || pEntity.ApproveUserID!=null)
                strSql.Append( "[approve_user_id]=@ApproveUserID,");
            if (pIsUpdateNullField || pEntity.SendTime!=null)
                strSql.Append( "[send_time]=@SendTime,");
            if (pIsUpdateNullField || pEntity.SendUserID!=null)
                strSql.Append( "[send_user_id]=@SendUserID,");
            if (pIsUpdateNullField || pEntity.AccpectTime!=null)
                strSql.Append( "[accpect_time]=@AccpectTime,");
            if (pIsUpdateNullField || pEntity.AccpectUserID!=null)
                strSql.Append( "[accpect_user_id]=@AccpectUserID,");
            if (pIsUpdateNullField || pEntity.ModifyTime!=null)
                strSql.Append( "[modify_time]=@ModifyTime,");
            if (pIsUpdateNullField || pEntity.ModifyUserID!=null)
                strSql.Append( "[modify_user_id]=@ModifyUserID,");
            if (pIsUpdateNullField || pEntity.DataFromID!=null)
                strSql.Append( "[data_from_id]=@DataFromID,");
            if (pIsUpdateNullField || pEntity.SalesUnitID!=null)
                strSql.Append( "[sales_unit_id]=@SalesUnitID,");
            if (pIsUpdateNullField || pEntity.PurchaseUnitID!=null)
                strSql.Append( "[purchase_unit_id]=@PurchaseUnitID,");
            if (pIsUpdateNullField || pEntity.IfFlag!=null)
                strSql.Append( "[if_flag]=@IfFlag,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[customer_id]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.BatID!=null)
                strSql.Append( "[bat_id]=@BatID,");
            if (pIsUpdateNullField || pEntity.SalesWarehouseID!=null)
                strSql.Append( "[sales_warehouse_id]=@SalesWarehouseID,");
            if (pIsUpdateNullField || pEntity.PurchaseWarehouseID!=null)
                strSql.Append( "[purchase_warehouse_id]=@PurchaseWarehouseID,");
            if (pIsUpdateNullField || pEntity.Field1!=null)
                strSql.Append( "[Field1]=@Field1,");
            if (pIsUpdateNullField || pEntity.Field2!=null)
                strSql.Append( "[Field2]=@Field2,");
            if (pIsUpdateNullField || pEntity.Field3!=null)
                strSql.Append( "[Field3]=@Field3,");
            if (pIsUpdateNullField || pEntity.Field4!=null)
                strSql.Append( "[Field4]=@Field4,");
            if (pIsUpdateNullField || pEntity.Field5!=null)
                strSql.Append( "[Field5]=@Field5,");
            if (pIsUpdateNullField || pEntity.Field6!=null)
                strSql.Append( "[Field6]=@Field6,");
            if (pIsUpdateNullField || pEntity.Field7!=null)
                strSql.Append( "[Field7]=@Field7,");
            if (pIsUpdateNullField || pEntity.Field8!=null)
                strSql.Append( "[Field8]=@Field8,");
            if (pIsUpdateNullField || pEntity.Field9!=null)
                strSql.Append( "[Field9]=@Field9,");
            if (pIsUpdateNullField || pEntity.Field10!=null)
                strSql.Append( "[Field10]=@Field10,");
            if (pIsUpdateNullField || pEntity.Field11!=null)
                strSql.Append( "[Field11]=@Field11,");
            if (pIsUpdateNullField || pEntity.Field12!=null)
                strSql.Append( "[Field12]=@Field12,");
            if (pIsUpdateNullField || pEntity.Field13!=null)
                strSql.Append( "[Field13]=@Field13,");
            if (pIsUpdateNullField || pEntity.Field14!=null)
                strSql.Append( "[Field14]=@Field14,");
            if (pIsUpdateNullField || pEntity.Field15!=null)
                strSql.Append( "[Field15]=@Field15,");
            if (pIsUpdateNullField || pEntity.Field16!=null)
                strSql.Append( "[Field16]=@Field16,");
            if (pIsUpdateNullField || pEntity.Field17!=null)
                strSql.Append( "[Field17]=@Field17,");
            if (pIsUpdateNullField || pEntity.Field18!=null)
                strSql.Append( "[Field18]=@Field18,");
            if (pIsUpdateNullField || pEntity.Field19!=null)
                strSql.Append( "[Field19]=@Field19,");
            if (pIsUpdateNullField || pEntity.Field20!=null)
                strSql.Append( "[Field20]=@Field20,");
            if (pIsUpdateNullField || pEntity.PaymentcenterID!=null)
                strSql.Append( "[paymentcenter_id]=@PaymentcenterID");
     
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where order_id=@OrderID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@OrderNo",SqlDbType.NVarChar),
					new SqlParameter("@OrderTypeID",SqlDbType.NVarChar),
					new SqlParameter("@OrderReasonID",SqlDbType.NVarChar),
					new SqlParameter("@RedFlag",SqlDbType.NVarChar),
					new SqlParameter("@RefOrderID",SqlDbType.NVarChar),
					new SqlParameter("@RefOrderNo",SqlDbType.NVarChar),
					new SqlParameter("@WarehouseID",SqlDbType.NVarChar),
					new SqlParameter("@OrderDate",SqlDbType.NVarChar),
					new SqlParameter("@RequestDate",SqlDbType.NVarChar),
					new SqlParameter("@CompleteDate",SqlDbType.NVarChar),
					new SqlParameter("@CreateUnitID",SqlDbType.NVarChar),
					new SqlParameter("@UnitID",SqlDbType.NVarChar),
					new SqlParameter("@RelatedUnitID",SqlDbType.NVarChar),
					new SqlParameter("@RelatedUnitCode",SqlDbType.NVarChar),
					new SqlParameter("@PosID",SqlDbType.NVarChar),
					new SqlParameter("@ShiftID",SqlDbType.NVarChar),
					new SqlParameter("@SalesUser",SqlDbType.NVarChar),
					new SqlParameter("@TotalAmount",SqlDbType.Decimal),
					new SqlParameter("@DiscountRate",SqlDbType.Decimal),
					new SqlParameter("@ActualAmount",SqlDbType.Decimal),
					new SqlParameter("@ReceivePoints",SqlDbType.Decimal),
					new SqlParameter("@PayPoints",SqlDbType.Decimal),
					new SqlParameter("@PayID",SqlDbType.NVarChar),
					new SqlParameter("@PrintTimes",SqlDbType.Int),
					new SqlParameter("@CarrierID",SqlDbType.NVarChar),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.NVarChar),
					new SqlParameter("@StatusDesc",SqlDbType.NVarChar),
					new SqlParameter("@TotalQty",SqlDbType.Decimal),
					new SqlParameter("@TotalRetail",SqlDbType.Decimal),
					new SqlParameter("@KeepTheChange",SqlDbType.Decimal),
					new SqlParameter("@WipingZero",SqlDbType.Decimal),
					new SqlParameter("@VipNo",SqlDbType.NVarChar),
					new SqlParameter("@CreateUserID",SqlDbType.NVarChar),
					new SqlParameter("@ApproveTime",SqlDbType.NVarChar),
					new SqlParameter("@ApproveUserID",SqlDbType.NVarChar),
					new SqlParameter("@SendTime",SqlDbType.NVarChar),
					new SqlParameter("@SendUserID",SqlDbType.NVarChar),
					new SqlParameter("@AccpectTime",SqlDbType.NVarChar),
					new SqlParameter("@AccpectUserID",SqlDbType.NVarChar),
					new SqlParameter("@ModifyTime",SqlDbType.NVarChar),
					new SqlParameter("@ModifyUserID",SqlDbType.NVarChar),
					new SqlParameter("@DataFromID",SqlDbType.NVarChar),
					new SqlParameter("@SalesUnitID",SqlDbType.NVarChar),
					new SqlParameter("@PurchaseUnitID",SqlDbType.NVarChar),
					new SqlParameter("@IfFlag",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@BatID",SqlDbType.NVarChar),
					new SqlParameter("@SalesWarehouseID",SqlDbType.NVarChar),
					new SqlParameter("@PurchaseWarehouseID",SqlDbType.NVarChar),
					new SqlParameter("@Field1",SqlDbType.NVarChar),
					new SqlParameter("@Field2",SqlDbType.NVarChar),
					new SqlParameter("@Field3",SqlDbType.NVarChar),
					new SqlParameter("@Field4",SqlDbType.NVarChar),
					new SqlParameter("@Field5",SqlDbType.NVarChar),
					new SqlParameter("@Field6",SqlDbType.NVarChar),
					new SqlParameter("@Field7",SqlDbType.NVarChar),
					new SqlParameter("@Field8",SqlDbType.NVarChar),
					new SqlParameter("@Field9",SqlDbType.NVarChar),
					new SqlParameter("@Field10",SqlDbType.NVarChar),
					new SqlParameter("@Field11",SqlDbType.NVarChar),
					new SqlParameter("@Field12",SqlDbType.NVarChar),
					new SqlParameter("@Field13",SqlDbType.NVarChar),
					new SqlParameter("@Field14",SqlDbType.NVarChar),
					new SqlParameter("@Field15",SqlDbType.NVarChar),
					new SqlParameter("@Field16",SqlDbType.NVarChar),
					new SqlParameter("@Field17",SqlDbType.NVarChar),
					new SqlParameter("@Field18",SqlDbType.NVarChar),
					new SqlParameter("@Field19",SqlDbType.NVarChar),
					new SqlParameter("@Field20",SqlDbType.NVarChar),
					new SqlParameter("@PaymentcenterID",SqlDbType.NVarChar),
					new SqlParameter("@OrderID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.OrderNo;
			parameters[1].Value = pEntity.OrderTypeID;
			parameters[2].Value = pEntity.OrderReasonID;
			parameters[3].Value = pEntity.RedFlag;
			parameters[4].Value = pEntity.RefOrderID;
			parameters[5].Value = pEntity.RefOrderNo;
			parameters[6].Value = pEntity.WarehouseID;
			parameters[7].Value = pEntity.OrderDate;
			parameters[8].Value = pEntity.RequestDate;
			parameters[9].Value = pEntity.CompleteDate;
			parameters[10].Value = pEntity.CreateUnitID;
			parameters[11].Value = pEntity.UnitID;
			parameters[12].Value = pEntity.RelatedUnitID;
			parameters[13].Value = pEntity.RelatedUnitCode;
			parameters[14].Value = pEntity.PosID;
			parameters[15].Value = pEntity.ShiftID;
			parameters[16].Value = pEntity.SalesUser;
			parameters[17].Value = pEntity.TotalAmount;
			parameters[18].Value = pEntity.DiscountRate;
			parameters[19].Value = pEntity.ActualAmount;
			parameters[20].Value = pEntity.ReceivePoints;
			parameters[21].Value = pEntity.PayPoints;
			parameters[22].Value = pEntity.PayID;
			parameters[23].Value = pEntity.PrintTimes;
			parameters[24].Value = pEntity.CarrierID;
			parameters[25].Value = pEntity.Remark;
			parameters[26].Value = pEntity.Status;
			parameters[27].Value = pEntity.StatusDesc;
			parameters[28].Value = pEntity.TotalQty;
			parameters[29].Value = pEntity.TotalRetail;
			parameters[30].Value = pEntity.KeepTheChange;
			parameters[31].Value = pEntity.WipingZero;
			parameters[32].Value = pEntity.VipNo;
			parameters[33].Value = pEntity.CreateUserID;
			parameters[34].Value = pEntity.ApproveTime;
			parameters[35].Value = pEntity.ApproveUserID;
			parameters[36].Value = pEntity.SendTime;
			parameters[37].Value = pEntity.SendUserID;
			parameters[38].Value = pEntity.AccpectTime;
			parameters[39].Value = pEntity.AccpectUserID;
			parameters[40].Value = pEntity.ModifyTime;
			parameters[41].Value = pEntity.ModifyUserID;
			parameters[42].Value = pEntity.DataFromID;
			parameters[43].Value = pEntity.SalesUnitID;
			parameters[44].Value = pEntity.PurchaseUnitID;
			parameters[45].Value = pEntity.IfFlag;
			parameters[46].Value = pEntity.CustomerID;
			parameters[47].Value = pEntity.BatID;
			parameters[48].Value = pEntity.SalesWarehouseID;
			parameters[49].Value = pEntity.PurchaseWarehouseID;
			parameters[50].Value = pEntity.Field1;
			parameters[51].Value = pEntity.Field2;
			parameters[52].Value = pEntity.Field3;
			parameters[53].Value = pEntity.Field4;
			parameters[54].Value = pEntity.Field5;
			parameters[55].Value = pEntity.Field6;
			parameters[56].Value = pEntity.Field7;
			parameters[57].Value = pEntity.Field8;
			parameters[58].Value = pEntity.Field9;
			parameters[59].Value = pEntity.Field10;
			parameters[60].Value = pEntity.Field11;
			parameters[61].Value = pEntity.Field12;
			parameters[62].Value = pEntity.Field13;
			parameters[63].Value = pEntity.Field14;
			parameters[64].Value = pEntity.Field15;
			parameters[65].Value = pEntity.Field16;
			parameters[66].Value = pEntity.Field17;
			parameters[67].Value = pEntity.Field18;
			parameters[68].Value = pEntity.Field19;
			parameters[69].Value = pEntity.Field20;
			parameters[70].Value = pEntity.PaymentcenterID;
			parameters[71].Value = pEntity.OrderID;

            //执行语句
            int result = 0;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Update(TInoutEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(TInoutEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(TInoutEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.OrderID == null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.OrderID, pTran);           
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pID">标识符的值</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(object pID, IDbTransaction pTran)
        {
            if (pID == null)
                return ;   
            //组织参数化SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [T_Inout] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy where order_id=@OrderID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.Int,Value=Convert.ToInt32(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@OrderID",SqlDbType=SqlDbType.VarChar,Value=pID}
            };
            //执行语句
            int result = 0;
            if (pTran != null)
                result=this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), parameters);
            else
                result=this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), parameters);
            return ;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(TInoutEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (pEntity.OrderID == null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.OrderID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(TInoutEntity[] pEntities)
        { 
            Delete(pEntities, null);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pIDs">标识符值数组</param>
        public void Delete(object[] pIDs)
        {
            Delete(pIDs,null);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pIDs">标识符值数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(object[] pIDs, IDbTransaction pTran) 
        {
            if (pIDs == null || pIDs.Length==0)
                return ;
            //组织参数化SQL
            StringBuilder primaryKeys = new StringBuilder();
            foreach (object item in pIDs)
            {
                primaryKeys.AppendFormat("'{0}',",item.ToString());
            }
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [T_Inout] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy="+CurrentUserInfo.UserID+" where order_id in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
            //执行语句
            int result = 0;   
            if (pTran == null)
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), null);
            else
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran,CommandType.Text, sql.ToString());       
        }
        #endregion

        #region IQueryable 成员
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <returns></returns>
        public TInoutEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Inout] where 1=1 ");
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
            //执行SQL
            List<TInoutEntity> list = new List<TInoutEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    TInoutEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //返回结果
            return list.ToArray();
        }
        /// <summary>
        /// 执行分页查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <param name="pPageSize">每页的记录数</param>
        /// <param name="pCurrentPageIndex">以0开始的当前页码</param>
        /// <returns></returns>
        public PagedQueryResult<TInoutEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            //组织SQL
            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();
            //分页SQL
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
                pagedSql.AppendFormat(" [OrderID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [TInout] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [TInout] where isdelete=0 ");
            //过滤条件
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
            //取指定页的数据
            pagedSql.AppendFormat(" where ___rn >{0} and ___rn <={1}", pPageSize * (pCurrentPageIndex-1), pPageSize * (pCurrentPageIndex));
            //执行语句并返回结果
            PagedQueryResult<TInoutEntity> result = new PagedQueryResult<TInoutEntity>();
            List<TInoutEntity> list = new List<TInoutEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    TInoutEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            result.Entities = list.ToArray();
            int totalCount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(totalCountSql.ToString()));    //计算总行数
            result.RowCount = totalCount;
            int remainder = 0;
            result.PageCount = Math.DivRem(totalCount, pPageSize, out remainder);
            if (remainder > 0)
                result.PageCount++;
            return result;
        }

        /// <summary>
        /// 根据实体条件查询实体
        /// </summary>
        /// <param name="pQueryEntity">以实体形式传入的参数</param>
        /// <param name="pOrderBys">排序组合</param>
        /// <returns>符合条件的实体集</returns>
        public TInoutEntity[] QueryByEntity(TInoutEntity pQueryEntity, OrderBy[] pOrderBys)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity(pQueryEntity);
            return Query(queryWhereCondition,  pOrderBys);            
        }

        /// <summary>
        /// 分页根据实体条件查询实体
        /// </summary>
        /// <param name="pQueryEntity">以实体形式传入的参数</param>
        /// <param name="pOrderBys">排序组合</param>
        /// <returns>符合条件的实体集</returns>
        public PagedQueryResult<TInoutEntity> PagedQueryByEntity(TInoutEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity( pQueryEntity);
            return PagedQuery(queryWhereCondition, pOrderBys, pPageSize, pCurrentPageIndex);
        }

        #endregion

        #region 工具方法
        /// <summary>
        /// 根据实体非Null属性生成查询条件。
        /// </summary>
        /// <returns></returns>
        protected IWhereCondition[] GetWhereConditionByEntity(TInoutEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.OrderID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderID", Value = pQueryEntity.OrderID });
            if (pQueryEntity.OrderNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderNo", Value = pQueryEntity.OrderNo });
            if (pQueryEntity.OrderTypeID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderTypeID", Value = pQueryEntity.OrderTypeID });
            if (pQueryEntity.OrderReasonID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderReasonID", Value = pQueryEntity.OrderReasonID });
            if (pQueryEntity.RedFlag!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RedFlag", Value = pQueryEntity.RedFlag });
            if (pQueryEntity.RefOrderID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RefOrderID", Value = pQueryEntity.RefOrderID });
            if (pQueryEntity.RefOrderNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RefOrderNo", Value = pQueryEntity.RefOrderNo });
            if (pQueryEntity.WarehouseID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WarehouseID", Value = pQueryEntity.WarehouseID });
            if (pQueryEntity.OrderDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderDate", Value = pQueryEntity.OrderDate });
            if (pQueryEntity.RequestDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RequestDate", Value = pQueryEntity.RequestDate });
            if (pQueryEntity.CompleteDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CompleteDate", Value = pQueryEntity.CompleteDate });
            if (pQueryEntity.CreateUnitID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateUnitID", Value = pQueryEntity.CreateUnitID });
            if (pQueryEntity.UnitID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitID", Value = pQueryEntity.UnitID });
            if (pQueryEntity.RelatedUnitID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RelatedUnitID", Value = pQueryEntity.RelatedUnitID });
            if (pQueryEntity.RelatedUnitCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RelatedUnitCode", Value = pQueryEntity.RelatedUnitCode });
            if (pQueryEntity.PosID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PosID", Value = pQueryEntity.PosID });
            if (pQueryEntity.ShiftID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ShiftID", Value = pQueryEntity.ShiftID });
            if (pQueryEntity.SalesUser!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesUser", Value = pQueryEntity.SalesUser });
            if (pQueryEntity.TotalAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TotalAmount", Value = pQueryEntity.TotalAmount });
            if (pQueryEntity.DiscountRate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DiscountRate", Value = pQueryEntity.DiscountRate });
            if (pQueryEntity.ActualAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ActualAmount", Value = pQueryEntity.ActualAmount });
            if (pQueryEntity.ReceivePoints!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReceivePoints", Value = pQueryEntity.ReceivePoints });
            if (pQueryEntity.PayPoints!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PayPoints", Value = pQueryEntity.PayPoints });
            if (pQueryEntity.PayID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PayID", Value = pQueryEntity.PayID });
            if (pQueryEntity.PrintTimes!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PrintTimes", Value = pQueryEntity.PrintTimes });
            if (pQueryEntity.CarrierID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CarrierID", Value = pQueryEntity.CarrierID });
            if (pQueryEntity.Remark!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Remark", Value = pQueryEntity.Remark });
            if (pQueryEntity.Status!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Status", Value = pQueryEntity.Status });
            if (pQueryEntity.StatusDesc!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StatusDesc", Value = pQueryEntity.StatusDesc });
            if (pQueryEntity.TotalQty!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TotalQty", Value = pQueryEntity.TotalQty });
            if (pQueryEntity.TotalRetail!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TotalRetail", Value = pQueryEntity.TotalRetail });
            if (pQueryEntity.KeepTheChange!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "KeepTheChange", Value = pQueryEntity.KeepTheChange });
            if (pQueryEntity.WipingZero!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WipingZero", Value = pQueryEntity.WipingZero });
            if (pQueryEntity.VipNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipNo", Value = pQueryEntity.VipNo });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateUserID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateUserID", Value = pQueryEntity.CreateUserID });
            if (pQueryEntity.ApproveTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ApproveTime", Value = pQueryEntity.ApproveTime });
            if (pQueryEntity.ApproveUserID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ApproveUserID", Value = pQueryEntity.ApproveUserID });
            if (pQueryEntity.SendTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SendTime", Value = pQueryEntity.SendTime });
            if (pQueryEntity.SendUserID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SendUserID", Value = pQueryEntity.SendUserID });
            if (pQueryEntity.AccpectTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AccpectTime", Value = pQueryEntity.AccpectTime });
            if (pQueryEntity.AccpectUserID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AccpectUserID", Value = pQueryEntity.AccpectUserID });
            if (pQueryEntity.ModifyTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ModifyTime", Value = pQueryEntity.ModifyTime });
            if (pQueryEntity.ModifyUserID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ModifyUserID", Value = pQueryEntity.ModifyUserID });
            if (pQueryEntity.DataFromID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DataFromID", Value = pQueryEntity.DataFromID });
            if (pQueryEntity.SalesUnitID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesUnitID", Value = pQueryEntity.SalesUnitID });
            if (pQueryEntity.PurchaseUnitID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PurchaseUnitID", Value = pQueryEntity.PurchaseUnitID });
            if (pQueryEntity.IfFlag!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IfFlag", Value = pQueryEntity.IfFlag });
            if (pQueryEntity.CustomerID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });
            if (pQueryEntity.BatID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BatID", Value = pQueryEntity.BatID });
            if (pQueryEntity.SalesWarehouseID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesWarehouseID", Value = pQueryEntity.SalesWarehouseID });
            if (pQueryEntity.PurchaseWarehouseID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PurchaseWarehouseID", Value = pQueryEntity.PurchaseWarehouseID });
            if (pQueryEntity.Field1!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field1", Value = pQueryEntity.Field1 });
            if (pQueryEntity.Field2!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field2", Value = pQueryEntity.Field2 });
            if (pQueryEntity.Field3!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field3", Value = pQueryEntity.Field3 });
            if (pQueryEntity.Field4!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field4", Value = pQueryEntity.Field4 });
            if (pQueryEntity.Field5!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field5", Value = pQueryEntity.Field5 });
            if (pQueryEntity.Field6!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field6", Value = pQueryEntity.Field6 });
            if (pQueryEntity.Field7!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field7", Value = pQueryEntity.Field7 });
            if (pQueryEntity.Field8!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field8", Value = pQueryEntity.Field8 });
            if (pQueryEntity.Field9!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field9", Value = pQueryEntity.Field9 });
            if (pQueryEntity.Field10!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field10", Value = pQueryEntity.Field10 });
            if (pQueryEntity.Field11!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field11", Value = pQueryEntity.Field11 });
            if (pQueryEntity.Field12!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field12", Value = pQueryEntity.Field12 });
            if (pQueryEntity.Field13!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field13", Value = pQueryEntity.Field13 });
            if (pQueryEntity.Field14!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field14", Value = pQueryEntity.Field14 });
            if (pQueryEntity.Field15!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field15", Value = pQueryEntity.Field15 });
            if (pQueryEntity.Field16!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field16", Value = pQueryEntity.Field16 });
            if (pQueryEntity.Field17!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field17", Value = pQueryEntity.Field17 });
            if (pQueryEntity.Field18!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field18", Value = pQueryEntity.Field18 });
            if (pQueryEntity.Field19!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field19", Value = pQueryEntity.Field19 });
            if (pQueryEntity.Field20!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Field20", Value = pQueryEntity.Field20 });
            if (pQueryEntity.PaymentcenterID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PaymentcenterID", Value = pQueryEntity.PaymentcenterID });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out TInoutEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new TInoutEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["order_id"] != DBNull.Value)
			{
				pInstance.OrderID =  Convert.ToString(pReader["order_id"]);
			}
			if (pReader["order_no"] != DBNull.Value)
			{
				pInstance.OrderNo =  Convert.ToString(pReader["order_no"]);
			}
			if (pReader["order_type_id"] != DBNull.Value)
			{
				pInstance.OrderTypeID =  Convert.ToString(pReader["order_type_id"]);
			}
			if (pReader["order_reason_id"] != DBNull.Value)
			{
				pInstance.OrderReasonID =  Convert.ToString(pReader["order_reason_id"]);
			}
			if (pReader["red_flag"] != DBNull.Value)
			{
				pInstance.RedFlag =  Convert.ToString(pReader["red_flag"]);
			}
			if (pReader["ref_order_id"] != DBNull.Value)
			{
				pInstance.RefOrderID =  Convert.ToString(pReader["ref_order_id"]);
			}
			if (pReader["ref_order_no"] != DBNull.Value)
			{
				pInstance.RefOrderNo =  Convert.ToString(pReader["ref_order_no"]);
			}
			if (pReader["warehouse_id"] != DBNull.Value)
			{
				pInstance.WarehouseID =  Convert.ToString(pReader["warehouse_id"]);
			}
			if (pReader["order_date"] != DBNull.Value)
			{
				pInstance.OrderDate =  Convert.ToString(pReader["order_date"]);
			}
			if (pReader["request_date"] != DBNull.Value)
			{
				pInstance.RequestDate =  Convert.ToString(pReader["request_date"]);
			}
			if (pReader["complete_date"] != DBNull.Value)
			{
				pInstance.CompleteDate =  Convert.ToString(pReader["complete_date"]);
			}
			if (pReader["create_unit_id"] != DBNull.Value)
			{
				pInstance.CreateUnitID =  Convert.ToString(pReader["create_unit_id"]);
			}
			if (pReader["unit_id"] != DBNull.Value)
			{
				pInstance.UnitID =  Convert.ToString(pReader["unit_id"]);
			}
			if (pReader["related_unit_id"] != DBNull.Value)
			{
				pInstance.RelatedUnitID =  Convert.ToString(pReader["related_unit_id"]);
			}
			if (pReader["related_unit_code"] != DBNull.Value)
			{
				pInstance.RelatedUnitCode =  Convert.ToString(pReader["related_unit_code"]);
			}
			if (pReader["pos_id"] != DBNull.Value)
			{
				pInstance.PosID =  Convert.ToString(pReader["pos_id"]);
			}
			if (pReader["shift_id"] != DBNull.Value)
			{
				pInstance.ShiftID =  Convert.ToString(pReader["shift_id"]);
			}
			if (pReader["sales_user"] != DBNull.Value)
			{
				pInstance.SalesUser =  Convert.ToString(pReader["sales_user"]);
			}
			if (pReader["total_amount"] != DBNull.Value)
			{
				pInstance.TotalAmount =  Convert.ToDecimal(pReader["total_amount"]);
			}
			if (pReader["discount_rate"] != DBNull.Value)
			{
				pInstance.DiscountRate =  Convert.ToDecimal(pReader["discount_rate"]);
			}
			if (pReader["actual_amount"] != DBNull.Value)
			{
				pInstance.ActualAmount =  Convert.ToDecimal(pReader["actual_amount"]);
			}
			if (pReader["receive_points"] != DBNull.Value)
			{
				pInstance.ReceivePoints =  Convert.ToDecimal(pReader["receive_points"]);
			}
			if (pReader["pay_points"] != DBNull.Value)
			{
				pInstance.PayPoints =  Convert.ToDecimal(pReader["pay_points"]);
			}
			if (pReader["pay_id"] != DBNull.Value)
			{
				pInstance.PayID =  Convert.ToString(pReader["pay_id"]);
			}
			if (pReader["print_times"] != DBNull.Value)
			{
				pInstance.PrintTimes =   Convert.ToInt32(pReader["print_times"]);
			}
			if (pReader["carrier_id"] != DBNull.Value)
			{
				pInstance.CarrierID =  Convert.ToString(pReader["carrier_id"]);
			}
			if (pReader["remark"] != DBNull.Value)
			{
				pInstance.Remark =  Convert.ToString(pReader["remark"]);
			}
			if (pReader["status"] != DBNull.Value)
			{
				pInstance.Status =  Convert.ToString(pReader["status"]);
			}
			if (pReader["status_desc"] != DBNull.Value)
			{
				pInstance.StatusDesc =  Convert.ToString(pReader["status_desc"]);
			}
			if (pReader["total_qty"] != DBNull.Value)
			{
				pInstance.TotalQty =  Convert.ToDecimal(pReader["total_qty"]);
			}
			if (pReader["total_retail"] != DBNull.Value)
			{
				pInstance.TotalRetail =  Convert.ToDecimal(pReader["total_retail"]);
			}
			if (pReader["keep_the_change"] != DBNull.Value)
			{
				pInstance.KeepTheChange =  Convert.ToDecimal(pReader["keep_the_change"]);
			}
			if (pReader["wiping_zero"] != DBNull.Value)
			{
				pInstance.WipingZero =  Convert.ToDecimal(pReader["wiping_zero"]);
			}
			if (pReader["vip_no"] != DBNull.Value)
			{
				pInstance.VipNo =  Convert.ToString(pReader["vip_no"]);
			}
			if (pReader["create_time"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToString(pReader["create_time"]);
			}
			if (pReader["create_user_id"] != DBNull.Value)
			{
				pInstance.CreateUserID =  Convert.ToString(pReader["create_user_id"]);
			}
			if (pReader["approve_time"] != DBNull.Value)
			{
				pInstance.ApproveTime =  Convert.ToString(pReader["approve_time"]);
			}
			if (pReader["approve_user_id"] != DBNull.Value)
			{
				pInstance.ApproveUserID =  Convert.ToString(pReader["approve_user_id"]);
			}
			if (pReader["send_time"] != DBNull.Value)
			{
				pInstance.SendTime =  Convert.ToString(pReader["send_time"]);
			}
			if (pReader["send_user_id"] != DBNull.Value)
			{
				pInstance.SendUserID =  Convert.ToString(pReader["send_user_id"]);
			}
			if (pReader["accpect_time"] != DBNull.Value)
			{
				pInstance.AccpectTime =  Convert.ToString(pReader["accpect_time"]);
			}
			if (pReader["accpect_user_id"] != DBNull.Value)
			{
				pInstance.AccpectUserID =  Convert.ToString(pReader["accpect_user_id"]);
			}
			if (pReader["modify_time"] != DBNull.Value)
			{
				pInstance.ModifyTime =  Convert.ToString(pReader["modify_time"]);
			}
			if (pReader["modify_user_id"] != DBNull.Value)
			{
				pInstance.ModifyUserID =  Convert.ToString(pReader["modify_user_id"]);
			}
			if (pReader["data_from_id"] != DBNull.Value)
			{
				pInstance.DataFromID =  Convert.ToString(pReader["data_from_id"]);
			}
			if (pReader["sales_unit_id"] != DBNull.Value)
			{
				pInstance.SalesUnitID =  Convert.ToString(pReader["sales_unit_id"]);
			}
			if (pReader["purchase_unit_id"] != DBNull.Value)
			{
				pInstance.PurchaseUnitID =  Convert.ToString(pReader["purchase_unit_id"]);
			}
			if (pReader["if_flag"] != DBNull.Value)
			{
				pInstance.IfFlag =  Convert.ToString(pReader["if_flag"]);
			}
			if (pReader["customer_id"] != DBNull.Value)
			{
				pInstance.CustomerID =  Convert.ToString(pReader["customer_id"]);
			}
			if (pReader["bat_id"] != DBNull.Value)
			{
				pInstance.BatID =  Convert.ToString(pReader["bat_id"]);
			}
			if (pReader["sales_warehouse_id"] != DBNull.Value)
			{
				pInstance.SalesWarehouseID =  Convert.ToString(pReader["sales_warehouse_id"]);
			}
			if (pReader["purchase_warehouse_id"] != DBNull.Value)
			{
				pInstance.PurchaseWarehouseID =  Convert.ToString(pReader["purchase_warehouse_id"]);
			}
			if (pReader["Field1"] != DBNull.Value)
			{
				pInstance.Field1 =  Convert.ToString(pReader["Field1"]);
			}
			if (pReader["Field2"] != DBNull.Value)
			{
				pInstance.Field2 =  Convert.ToString(pReader["Field2"]);
			}
			if (pReader["Field3"] != DBNull.Value)
			{
				pInstance.Field3 =  Convert.ToString(pReader["Field3"]);
			}
			if (pReader["Field4"] != DBNull.Value)
			{
				pInstance.Field4 =  Convert.ToString(pReader["Field4"]);
			}
			if (pReader["Field5"] != DBNull.Value)
			{
				pInstance.Field5 =  Convert.ToString(pReader["Field5"]);
			}
			if (pReader["Field6"] != DBNull.Value)
			{
				pInstance.Field6 =  Convert.ToString(pReader["Field6"]);
			}
			if (pReader["Field7"] != DBNull.Value)
			{
				pInstance.Field7 =  Convert.ToString(pReader["Field7"]);
			}
			if (pReader["Field8"] != DBNull.Value)
			{
				pInstance.Field8 =  Convert.ToString(pReader["Field8"]);
			}
			if (pReader["Field9"] != DBNull.Value)
			{
				pInstance.Field9 =  Convert.ToString(pReader["Field9"]);
			}
			if (pReader["Field10"] != DBNull.Value)
			{
				pInstance.Field10 =  Convert.ToString(pReader["Field10"]);
			}
			if (pReader["Field11"] != DBNull.Value)
			{
				pInstance.Field11 =  Convert.ToString(pReader["Field11"]);
			}
			if (pReader["Field12"] != DBNull.Value)
			{
				pInstance.Field12 =  Convert.ToString(pReader["Field12"]);
			}
			if (pReader["Field13"] != DBNull.Value)
			{
				pInstance.Field13 =  Convert.ToString(pReader["Field13"]);
			}
			if (pReader["Field14"] != DBNull.Value)
			{
				pInstance.Field14 =  Convert.ToString(pReader["Field14"]);
			}
			if (pReader["Field15"] != DBNull.Value)
			{
				pInstance.Field15 =  Convert.ToString(pReader["Field15"]);
			}
			if (pReader["Field16"] != DBNull.Value)
			{
				pInstance.Field16 =  Convert.ToString(pReader["Field16"]);
			}
			if (pReader["Field17"] != DBNull.Value)
			{
				pInstance.Field17 =  Convert.ToString(pReader["Field17"]);
			}
			if (pReader["Field18"] != DBNull.Value)
			{
				pInstance.Field18 =  Convert.ToString(pReader["Field18"]);
			}
			if (pReader["Field19"] != DBNull.Value)
			{
				pInstance.Field19 =  Convert.ToString(pReader["Field19"]);
			}
			if (pReader["Field20"] != DBNull.Value)
			{
				pInstance.Field20 =  Convert.ToString(pReader["Field20"]);
			}
			if (pReader["paymentcenter_id"] != DBNull.Value)
			{
				pInstance.PaymentcenterID =  Convert.ToString(pReader["paymentcenter_id"]);
			}

        }
        #endregion
    }
}
