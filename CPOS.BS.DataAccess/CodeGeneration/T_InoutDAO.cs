/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/26 18:33:14
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
    /// 数据访问：  
    /// 表T_Inout的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_InoutDAO : Base.BaseCPOSDAO, ICRUDable<T_InoutEntity>, IQueryable<T_InoutEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_InoutDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(T_InoutEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(T_InoutEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //初始化固定字段


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [T_Inout](");
            strSql.Append("[order_no],[order_type_id],[order_reason_id],[red_flag],[ref_order_id],[ref_order_no],[warehouse_id],[order_date],[request_date],[complete_date],[create_unit_id],[unit_id],[related_unit_id],[related_unit_code],[pos_id],[shift_id],[sales_user],[total_amount],[discount_rate],[actual_amount],[receive_points],[pay_points],[pay_id],[print_times],[carrier_id],[remark],[status],[status_desc],[total_qty],[total_retail],[keep_the_change],[wiping_zero],[vip_no],[create_time],[create_user_id],[approve_time],[approve_user_id],[send_time],[send_user_id],[accpect_time],[accpect_user_id],[modify_time],[modify_user_id],[data_from_id],[sales_unit_id],[purchase_unit_id],[if_flag],[customer_id],[bat_id],[sales_warehouse_id],[purchase_warehouse_id],[Field1],[Field2],[Field3],[Field4],[Field5],[Field6],[Field7],[Field8],[Field9],[Field10],[Field11],[Field12],[Field13],[Field14],[Field15],[Field16],[Field17],[Field18],[Field19],[Field20],[paymentcenter_id],[order_id])");
            strSql.Append(" values (");
            strSql.Append("@order_no,@order_type_id,@order_reason_id,@red_flag,@ref_order_id,@ref_order_no,@warehouse_id,@order_date,@request_date,@complete_date,@create_unit_id,@unit_id,@related_unit_id,@related_unit_code,@pos_id,@shift_id,@sales_user,@total_amount,@discount_rate,@actual_amount,@receive_points,@pay_points,@pay_id,@print_times,@carrier_id,@remark,@status,@status_desc,@total_qty,@total_retail,@keep_the_change,@wiping_zero,@vip_no,@create_time,@create_user_id,@approve_time,@approve_user_id,@send_time,@send_user_id,@accpect_time,@accpect_user_id,@modify_time,@modify_user_id,@data_from_id,@sales_unit_id,@purchase_unit_id,@if_flag,@customer_id,@bat_id,@sales_warehouse_id,@purchase_warehouse_id,@Field1,@Field2,@Field3,@Field4,@Field5,@Field6,@Field7,@Field8,@Field9,@Field10,@Field11,@Field12,@Field13,@Field14,@Field15,@Field16,@Field17,@Field18,@Field19,@Field20,@paymentcenter_id,@order_id)");            

			string pkString = pEntity.order_id;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@order_no",SqlDbType.NVarChar),
					new SqlParameter("@order_type_id",SqlDbType.NVarChar),
					new SqlParameter("@order_reason_id",SqlDbType.NVarChar),
					new SqlParameter("@red_flag",SqlDbType.NVarChar),
					new SqlParameter("@ref_order_id",SqlDbType.NVarChar),
					new SqlParameter("@ref_order_no",SqlDbType.NVarChar),
					new SqlParameter("@warehouse_id",SqlDbType.NVarChar),
					new SqlParameter("@order_date",SqlDbType.NVarChar),
					new SqlParameter("@request_date",SqlDbType.NVarChar),
					new SqlParameter("@complete_date",SqlDbType.NVarChar),
					new SqlParameter("@create_unit_id",SqlDbType.NVarChar),
					new SqlParameter("@unit_id",SqlDbType.NVarChar),
					new SqlParameter("@related_unit_id",SqlDbType.NVarChar),
					new SqlParameter("@related_unit_code",SqlDbType.NVarChar),
					new SqlParameter("@pos_id",SqlDbType.NVarChar),
					new SqlParameter("@shift_id",SqlDbType.NVarChar),
					new SqlParameter("@sales_user",SqlDbType.NVarChar),
					new SqlParameter("@total_amount",SqlDbType.Decimal),
					new SqlParameter("@discount_rate",SqlDbType.Decimal),
					new SqlParameter("@actual_amount",SqlDbType.Decimal),
					new SqlParameter("@receive_points",SqlDbType.Decimal),
					new SqlParameter("@pay_points",SqlDbType.Decimal),
					new SqlParameter("@pay_id",SqlDbType.NVarChar),
					new SqlParameter("@print_times",SqlDbType.Int),
					new SqlParameter("@carrier_id",SqlDbType.NVarChar),
					new SqlParameter("@remark",SqlDbType.NVarChar),
					new SqlParameter("@status",SqlDbType.NVarChar),
					new SqlParameter("@status_desc",SqlDbType.NVarChar),
					new SqlParameter("@total_qty",SqlDbType.Decimal),
					new SqlParameter("@total_retail",SqlDbType.Decimal),
					new SqlParameter("@keep_the_change",SqlDbType.Decimal),
					new SqlParameter("@wiping_zero",SqlDbType.Decimal),
					new SqlParameter("@vip_no",SqlDbType.NVarChar),
					new SqlParameter("@create_time",SqlDbType.NVarChar),
					new SqlParameter("@create_user_id",SqlDbType.NVarChar),
					new SqlParameter("@approve_time",SqlDbType.NVarChar),
					new SqlParameter("@approve_user_id",SqlDbType.NVarChar),
					new SqlParameter("@send_time",SqlDbType.NVarChar),
					new SqlParameter("@send_user_id",SqlDbType.NVarChar),
					new SqlParameter("@accpect_time",SqlDbType.NVarChar),
					new SqlParameter("@accpect_user_id",SqlDbType.NVarChar),
					new SqlParameter("@modify_time",SqlDbType.NVarChar),
					new SqlParameter("@modify_user_id",SqlDbType.NVarChar),
					new SqlParameter("@data_from_id",SqlDbType.NVarChar),
					new SqlParameter("@sales_unit_id",SqlDbType.NVarChar),
					new SqlParameter("@purchase_unit_id",SqlDbType.NVarChar),
					new SqlParameter("@if_flag",SqlDbType.NVarChar),
					new SqlParameter("@customer_id",SqlDbType.NVarChar),
					new SqlParameter("@bat_id",SqlDbType.NVarChar),
					new SqlParameter("@sales_warehouse_id",SqlDbType.NVarChar),
					new SqlParameter("@purchase_warehouse_id",SqlDbType.NVarChar),
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
					new SqlParameter("@paymentcenter_id",SqlDbType.NVarChar),
					new SqlParameter("@order_id",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.order_no;
			parameters[1].Value = pEntity.order_type_id;
			parameters[2].Value = pEntity.order_reason_id;
			parameters[3].Value = pEntity.red_flag;
			parameters[4].Value = pEntity.ref_order_id;
			parameters[5].Value = pEntity.ref_order_no;
			parameters[6].Value = pEntity.warehouse_id;
			parameters[7].Value = pEntity.order_date;
			parameters[8].Value = pEntity.request_date;
			parameters[9].Value = pEntity.complete_date;
			parameters[10].Value = pEntity.create_unit_id;
			parameters[11].Value = pEntity.unit_id;
			parameters[12].Value = pEntity.related_unit_id;
			parameters[13].Value = pEntity.related_unit_code;
			parameters[14].Value = pEntity.pos_id;
			parameters[15].Value = pEntity.shift_id;
			parameters[16].Value = pEntity.sales_user;
			parameters[17].Value = pEntity.total_amount;
			parameters[18].Value = pEntity.discount_rate;
			parameters[19].Value = pEntity.actual_amount;
			parameters[20].Value = pEntity.receive_points;
			parameters[21].Value = pEntity.pay_points;
			parameters[22].Value = pEntity.pay_id;
			parameters[23].Value = pEntity.print_times;
			parameters[24].Value = pEntity.carrier_id;
			parameters[25].Value = pEntity.remark;
			parameters[26].Value = pEntity.status;
			parameters[27].Value = pEntity.status_desc;
			parameters[28].Value = pEntity.total_qty;
			parameters[29].Value = pEntity.total_retail;
			parameters[30].Value = pEntity.keep_the_change;
			parameters[31].Value = pEntity.wiping_zero;
			parameters[32].Value = pEntity.vip_no;
			parameters[33].Value = pEntity.create_time;
			parameters[34].Value = pEntity.create_user_id;
			parameters[35].Value = pEntity.approve_time;
			parameters[36].Value = pEntity.approve_user_id;
			parameters[37].Value = pEntity.send_time;
			parameters[38].Value = pEntity.send_user_id;
			parameters[39].Value = pEntity.accpect_time;
			parameters[40].Value = pEntity.accpect_user_id;
			parameters[41].Value = pEntity.modify_time;
			parameters[42].Value = pEntity.modify_user_id;
			parameters[43].Value = pEntity.data_from_id;
			parameters[44].Value = pEntity.sales_unit_id;
			parameters[45].Value = pEntity.purchase_unit_id;
			parameters[46].Value = pEntity.if_flag;
			parameters[47].Value = pEntity.customer_id;
			parameters[48].Value = pEntity.bat_id;
			parameters[49].Value = pEntity.sales_warehouse_id;
			parameters[50].Value = pEntity.purchase_warehouse_id;
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
			parameters[71].Value = pEntity.paymentcenter_id;
			parameters[72].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.order_id = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public T_InoutEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Inout] where order_id='{0}'  and status<>'-1' ", id.ToString());
            //读取数据
            T_InoutEntity m = null;
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
        public T_InoutEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Inout] where 1=1  and status<>'-1'");
            //读取数据
            List<T_InoutEntity> list = new List<T_InoutEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_InoutEntity m;
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
        public void Update(T_InoutEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(T_InoutEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.order_id == null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_Inout] set ");
                        if (pIsUpdateNullField || pEntity.order_no!=null)
                strSql.Append( "[order_no]=@order_no,");
            if (pIsUpdateNullField || pEntity.order_type_id!=null)
                strSql.Append( "[order_type_id]=@order_type_id,");
            if (pIsUpdateNullField || pEntity.order_reason_id!=null)
                strSql.Append( "[order_reason_id]=@order_reason_id,");
            if (pIsUpdateNullField || pEntity.red_flag!=null)
                strSql.Append( "[red_flag]=@red_flag,");
            if (pIsUpdateNullField || pEntity.ref_order_id!=null)
                strSql.Append( "[ref_order_id]=@ref_order_id,");
            if (pIsUpdateNullField || pEntity.ref_order_no!=null)
                strSql.Append( "[ref_order_no]=@ref_order_no,");
            if (pIsUpdateNullField || pEntity.warehouse_id!=null)
                strSql.Append( "[warehouse_id]=@warehouse_id,");
            if (pIsUpdateNullField || pEntity.order_date!=null)
                strSql.Append( "[order_date]=@order_date,");
            if (pIsUpdateNullField || pEntity.request_date!=null)
                strSql.Append( "[request_date]=@request_date,");
            if (pIsUpdateNullField || pEntity.complete_date!=null)
                strSql.Append( "[complete_date]=@complete_date,");
            if (pIsUpdateNullField || pEntity.create_unit_id!=null)
                strSql.Append( "[create_unit_id]=@create_unit_id,");
            if (pIsUpdateNullField || pEntity.unit_id!=null)
                strSql.Append( "[unit_id]=@unit_id,");
            if (pIsUpdateNullField || pEntity.related_unit_id!=null)
                strSql.Append( "[related_unit_id]=@related_unit_id,");
            if (pIsUpdateNullField || pEntity.related_unit_code!=null)
                strSql.Append( "[related_unit_code]=@related_unit_code,");
            if (pIsUpdateNullField || pEntity.pos_id!=null)
                strSql.Append( "[pos_id]=@pos_id,");
            if (pIsUpdateNullField || pEntity.shift_id!=null)
                strSql.Append( "[shift_id]=@shift_id,");
            if (pIsUpdateNullField || pEntity.sales_user!=null)
                strSql.Append( "[sales_user]=@sales_user,");
            if (pIsUpdateNullField || pEntity.total_amount!=null)
                strSql.Append( "[total_amount]=@total_amount,");
            if (pIsUpdateNullField || pEntity.discount_rate!=null)
                strSql.Append( "[discount_rate]=@discount_rate,");
            if (pIsUpdateNullField || pEntity.actual_amount!=null)
                strSql.Append( "[actual_amount]=@actual_amount,");
            if (pIsUpdateNullField || pEntity.receive_points!=null)
                strSql.Append( "[receive_points]=@receive_points,");
            if (pIsUpdateNullField || pEntity.pay_points!=null)
                strSql.Append( "[pay_points]=@pay_points,");
            if (pIsUpdateNullField || pEntity.pay_id!=null)
                strSql.Append( "[pay_id]=@pay_id,");
            if (pIsUpdateNullField || pEntity.print_times!=null)
                strSql.Append( "[print_times]=@print_times,");
            if (pIsUpdateNullField || pEntity.carrier_id!=null)
                strSql.Append( "[carrier_id]=@carrier_id,");
            if (pIsUpdateNullField || pEntity.remark!=null)
                strSql.Append( "[remark]=@remark,");
            if (pIsUpdateNullField || pEntity.status!=null)
                strSql.Append( "[status]=@status,");
            if (pIsUpdateNullField || pEntity.status_desc!=null)
                strSql.Append( "[status_desc]=@status_desc,");
            if (pIsUpdateNullField || pEntity.total_qty!=null)
                strSql.Append( "[total_qty]=@total_qty,");
            if (pIsUpdateNullField || pEntity.total_retail!=null)
                strSql.Append( "[total_retail]=@total_retail,");
            if (pIsUpdateNullField || pEntity.keep_the_change!=null)
                strSql.Append( "[keep_the_change]=@keep_the_change,");
            if (pIsUpdateNullField || pEntity.wiping_zero!=null)
                strSql.Append( "[wiping_zero]=@wiping_zero,");
            if (pIsUpdateNullField || pEntity.vip_no!=null)
                strSql.Append( "[vip_no]=@vip_no,");
            if (pIsUpdateNullField || pEntity.create_time!=null)
                strSql.Append( "[create_time]=@create_time,");
            if (pIsUpdateNullField || pEntity.create_user_id!=null)
                strSql.Append( "[create_user_id]=@create_user_id,");
            if (pIsUpdateNullField || pEntity.approve_time!=null)
                strSql.Append( "[approve_time]=@approve_time,");
            if (pIsUpdateNullField || pEntity.approve_user_id!=null)
                strSql.Append( "[approve_user_id]=@approve_user_id,");
            if (pIsUpdateNullField || pEntity.send_time!=null)
                strSql.Append( "[send_time]=@send_time,");
            if (pIsUpdateNullField || pEntity.send_user_id!=null)
                strSql.Append( "[send_user_id]=@send_user_id,");
            if (pIsUpdateNullField || pEntity.accpect_time!=null)
                strSql.Append( "[accpect_time]=@accpect_time,");
            if (pIsUpdateNullField || pEntity.accpect_user_id!=null)
                strSql.Append( "[accpect_user_id]=@accpect_user_id,");
            if (pIsUpdateNullField || pEntity.modify_time!=null)
                strSql.Append( "[modify_time]=@modify_time,");
            if (pIsUpdateNullField || pEntity.modify_user_id!=null)
                strSql.Append( "[modify_user_id]=@modify_user_id,");
            if (pIsUpdateNullField || pEntity.data_from_id!=null)
                strSql.Append( "[data_from_id]=@data_from_id,");
            if (pIsUpdateNullField || pEntity.sales_unit_id!=null)
                strSql.Append( "[sales_unit_id]=@sales_unit_id,");
            if (pIsUpdateNullField || pEntity.purchase_unit_id!=null)
                strSql.Append( "[purchase_unit_id]=@purchase_unit_id,");
            if (pIsUpdateNullField || pEntity.if_flag!=null)
                strSql.Append( "[if_flag]=@if_flag,");
            if (pIsUpdateNullField || pEntity.customer_id!=null)
                strSql.Append( "[customer_id]=@customer_id,");
            if (pIsUpdateNullField || pEntity.bat_id!=null)
                strSql.Append( "[bat_id]=@bat_id,");
            if (pIsUpdateNullField || pEntity.sales_warehouse_id!=null)
                strSql.Append( "[sales_warehouse_id]=@sales_warehouse_id,");
            if (pIsUpdateNullField || pEntity.purchase_warehouse_id!=null)
                strSql.Append( "[purchase_warehouse_id]=@purchase_warehouse_id,");
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
            if (pIsUpdateNullField || pEntity.paymentcenter_id!=null)
                strSql.Append( "[paymentcenter_id]=@paymentcenter_id");
            strSql.Append(" where order_id=@order_id ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@order_no",SqlDbType.NVarChar),
					new SqlParameter("@order_type_id",SqlDbType.NVarChar),
					new SqlParameter("@order_reason_id",SqlDbType.NVarChar),
					new SqlParameter("@red_flag",SqlDbType.NVarChar),
					new SqlParameter("@ref_order_id",SqlDbType.NVarChar),
					new SqlParameter("@ref_order_no",SqlDbType.NVarChar),
					new SqlParameter("@warehouse_id",SqlDbType.NVarChar),
					new SqlParameter("@order_date",SqlDbType.NVarChar),
					new SqlParameter("@request_date",SqlDbType.NVarChar),
					new SqlParameter("@complete_date",SqlDbType.NVarChar),
					new SqlParameter("@create_unit_id",SqlDbType.NVarChar),
					new SqlParameter("@unit_id",SqlDbType.NVarChar),
					new SqlParameter("@related_unit_id",SqlDbType.NVarChar),
					new SqlParameter("@related_unit_code",SqlDbType.NVarChar),
					new SqlParameter("@pos_id",SqlDbType.NVarChar),
					new SqlParameter("@shift_id",SqlDbType.NVarChar),
					new SqlParameter("@sales_user",SqlDbType.NVarChar),
					new SqlParameter("@total_amount",SqlDbType.Decimal),
					new SqlParameter("@discount_rate",SqlDbType.Decimal),
					new SqlParameter("@actual_amount",SqlDbType.Decimal),
					new SqlParameter("@receive_points",SqlDbType.Decimal),
					new SqlParameter("@pay_points",SqlDbType.Decimal),
					new SqlParameter("@pay_id",SqlDbType.NVarChar),
					new SqlParameter("@print_times",SqlDbType.Int),
					new SqlParameter("@carrier_id",SqlDbType.NVarChar),
					new SqlParameter("@remark",SqlDbType.NVarChar),
					new SqlParameter("@status",SqlDbType.NVarChar),
					new SqlParameter("@status_desc",SqlDbType.NVarChar),
					new SqlParameter("@total_qty",SqlDbType.Decimal),
					new SqlParameter("@total_retail",SqlDbType.Decimal),
					new SqlParameter("@keep_the_change",SqlDbType.Decimal),
					new SqlParameter("@wiping_zero",SqlDbType.Decimal),
					new SqlParameter("@vip_no",SqlDbType.NVarChar),
					new SqlParameter("@create_time",SqlDbType.NVarChar),
					new SqlParameter("@create_user_id",SqlDbType.NVarChar),
					new SqlParameter("@approve_time",SqlDbType.NVarChar),
					new SqlParameter("@approve_user_id",SqlDbType.NVarChar),
					new SqlParameter("@send_time",SqlDbType.NVarChar),
					new SqlParameter("@send_user_id",SqlDbType.NVarChar),
					new SqlParameter("@accpect_time",SqlDbType.NVarChar),
					new SqlParameter("@accpect_user_id",SqlDbType.NVarChar),
					new SqlParameter("@modify_time",SqlDbType.NVarChar),
					new SqlParameter("@modify_user_id",SqlDbType.NVarChar),
					new SqlParameter("@data_from_id",SqlDbType.NVarChar),
					new SqlParameter("@sales_unit_id",SqlDbType.NVarChar),
					new SqlParameter("@purchase_unit_id",SqlDbType.NVarChar),
					new SqlParameter("@if_flag",SqlDbType.NVarChar),
					new SqlParameter("@customer_id",SqlDbType.NVarChar),
					new SqlParameter("@bat_id",SqlDbType.NVarChar),
					new SqlParameter("@sales_warehouse_id",SqlDbType.NVarChar),
					new SqlParameter("@purchase_warehouse_id",SqlDbType.NVarChar),
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
					new SqlParameter("@paymentcenter_id",SqlDbType.NVarChar),
					new SqlParameter("@order_id",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.order_no;
			parameters[1].Value = pEntity.order_type_id;
			parameters[2].Value = pEntity.order_reason_id;
			parameters[3].Value = pEntity.red_flag;
			parameters[4].Value = pEntity.ref_order_id;
			parameters[5].Value = pEntity.ref_order_no;
			parameters[6].Value = pEntity.warehouse_id;
			parameters[7].Value = pEntity.order_date;
			parameters[8].Value = pEntity.request_date;
			parameters[9].Value = pEntity.complete_date;
			parameters[10].Value = pEntity.create_unit_id;
			parameters[11].Value = pEntity.unit_id;
			parameters[12].Value = pEntity.related_unit_id;
			parameters[13].Value = pEntity.related_unit_code;
			parameters[14].Value = pEntity.pos_id;
			parameters[15].Value = pEntity.shift_id;
			parameters[16].Value = pEntity.sales_user;
			parameters[17].Value = pEntity.total_amount;
			parameters[18].Value = pEntity.discount_rate;
			parameters[19].Value = pEntity.actual_amount;
			parameters[20].Value = pEntity.receive_points;
			parameters[21].Value = pEntity.pay_points;
			parameters[22].Value = pEntity.pay_id;
			parameters[23].Value = pEntity.print_times;
			parameters[24].Value = pEntity.carrier_id;
			parameters[25].Value = pEntity.remark;
			parameters[26].Value = pEntity.status;
			parameters[27].Value = pEntity.status_desc;
			parameters[28].Value = pEntity.total_qty;
			parameters[29].Value = pEntity.total_retail;
			parameters[30].Value = pEntity.keep_the_change;
			parameters[31].Value = pEntity.wiping_zero;
			parameters[32].Value = pEntity.vip_no;
			parameters[33].Value = pEntity.create_time;
			parameters[34].Value = pEntity.create_user_id;
			parameters[35].Value = pEntity.approve_time;
			parameters[36].Value = pEntity.approve_user_id;
			parameters[37].Value = pEntity.send_time;
			parameters[38].Value = pEntity.send_user_id;
			parameters[39].Value = pEntity.accpect_time;
			parameters[40].Value = pEntity.accpect_user_id;
			parameters[41].Value = pEntity.modify_time;
			parameters[42].Value = pEntity.modify_user_id;
			parameters[43].Value = pEntity.data_from_id;
			parameters[44].Value = pEntity.sales_unit_id;
			parameters[45].Value = pEntity.purchase_unit_id;
			parameters[46].Value = pEntity.if_flag;
			parameters[47].Value = pEntity.customer_id;
			parameters[48].Value = pEntity.bat_id;
			parameters[49].Value = pEntity.sales_warehouse_id;
			parameters[50].Value = pEntity.purchase_warehouse_id;
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
			parameters[71].Value = pEntity.paymentcenter_id;
			parameters[72].Value = pEntity.order_id;

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
        public void Update(T_InoutEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(T_InoutEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(T_InoutEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.order_id == null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.order_id, pTran);           
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
            sql.AppendLine("update [T_Inout] set {$deleteFeild$} where order_id=@order_id;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@order_id",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(T_InoutEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (pEntity.order_id == null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.order_id;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(T_InoutEntity[] pEntities)
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
            sql.AppendLine("update [T_Inout] set status='-1' where order_id in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public T_InoutEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select p.Payment_Type_Code,p.Payment_Type_Name,t.*  from [T_Inout] t  left join T_Payment_Type p on t.pay_id=p.Payment_Type_Id where 1=1  and t.status<>'-1' ");
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
            List<T_InoutEntity> list = new List<T_InoutEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_InoutEntity m;
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
        public PagedQueryResult<T_InoutEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [order_id] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [T_Inout] where 1=1  and status<>'-1' ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [T_Inout] where 1=1  and status<>'-1' ");
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
            PagedQueryResult<T_InoutEntity> result = new PagedQueryResult<T_InoutEntity>();
            List<T_InoutEntity> list = new List<T_InoutEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    T_InoutEntity m;
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
        public T_InoutEntity[] QueryByEntity(T_InoutEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<T_InoutEntity> PagedQueryByEntity(T_InoutEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(T_InoutEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.order_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "order_id", Value = pQueryEntity.order_id });
            if (pQueryEntity.order_no!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "order_no", Value = pQueryEntity.order_no });
            if (pQueryEntity.order_type_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "order_type_id", Value = pQueryEntity.order_type_id });
            if (pQueryEntity.order_reason_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "order_reason_id", Value = pQueryEntity.order_reason_id });
            if (pQueryEntity.red_flag!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "red_flag", Value = pQueryEntity.red_flag });
            if (pQueryEntity.ref_order_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ref_order_id", Value = pQueryEntity.ref_order_id });
            if (pQueryEntity.ref_order_no!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ref_order_no", Value = pQueryEntity.ref_order_no });
            if (pQueryEntity.warehouse_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "warehouse_id", Value = pQueryEntity.warehouse_id });
            if (pQueryEntity.order_date!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "order_date", Value = pQueryEntity.order_date });
            if (pQueryEntity.request_date!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "request_date", Value = pQueryEntity.request_date });
            if (pQueryEntity.complete_date!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "complete_date", Value = pQueryEntity.complete_date });
            if (pQueryEntity.create_unit_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "create_unit_id", Value = pQueryEntity.create_unit_id });
            if (pQueryEntity.unit_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "unit_id", Value = pQueryEntity.unit_id });
            if (pQueryEntity.related_unit_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "related_unit_id", Value = pQueryEntity.related_unit_id });
            if (pQueryEntity.related_unit_code!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "related_unit_code", Value = pQueryEntity.related_unit_code });
            if (pQueryEntity.pos_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "pos_id", Value = pQueryEntity.pos_id });
            if (pQueryEntity.shift_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "shift_id", Value = pQueryEntity.shift_id });
            if (pQueryEntity.sales_user!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "sales_user", Value = pQueryEntity.sales_user });
            if (pQueryEntity.total_amount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "total_amount", Value = pQueryEntity.total_amount });
            if (pQueryEntity.discount_rate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "discount_rate", Value = pQueryEntity.discount_rate });
            if (pQueryEntity.actual_amount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "actual_amount", Value = pQueryEntity.actual_amount });
            if (pQueryEntity.receive_points!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "receive_points", Value = pQueryEntity.receive_points });
            if (pQueryEntity.pay_points!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "pay_points", Value = pQueryEntity.pay_points });
            if (pQueryEntity.pay_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "pay_id", Value = pQueryEntity.pay_id });
            if (pQueryEntity.print_times!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "print_times", Value = pQueryEntity.print_times });
            if (pQueryEntity.carrier_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "carrier_id", Value = pQueryEntity.carrier_id });
            if (pQueryEntity.remark!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "remark", Value = pQueryEntity.remark });
            if (pQueryEntity.status!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "status", Value = pQueryEntity.status });
            if (pQueryEntity.status_desc!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "status_desc", Value = pQueryEntity.status_desc });
            if (pQueryEntity.total_qty!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "total_qty", Value = pQueryEntity.total_qty });
            if (pQueryEntity.total_retail!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "total_retail", Value = pQueryEntity.total_retail });
            if (pQueryEntity.keep_the_change!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "keep_the_change", Value = pQueryEntity.keep_the_change });
            if (pQueryEntity.wiping_zero!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "wiping_zero", Value = pQueryEntity.wiping_zero });
            if (pQueryEntity.vip_no!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "vip_no", Value = pQueryEntity.vip_no });
            if (pQueryEntity.create_time!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "create_time", Value = pQueryEntity.create_time });
            if (pQueryEntity.create_user_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "create_user_id", Value = pQueryEntity.create_user_id });
            if (pQueryEntity.approve_time!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "approve_time", Value = pQueryEntity.approve_time });
            if (pQueryEntity.approve_user_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "approve_user_id", Value = pQueryEntity.approve_user_id });
            if (pQueryEntity.send_time!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "send_time", Value = pQueryEntity.send_time });
            if (pQueryEntity.send_user_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "send_user_id", Value = pQueryEntity.send_user_id });
            if (pQueryEntity.accpect_time!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "accpect_time", Value = pQueryEntity.accpect_time });
            if (pQueryEntity.accpect_user_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "accpect_user_id", Value = pQueryEntity.accpect_user_id });
            if (pQueryEntity.modify_time!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "modify_time", Value = pQueryEntity.modify_time });
            if (pQueryEntity.modify_user_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "modify_user_id", Value = pQueryEntity.modify_user_id });
            if (pQueryEntity.data_from_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "data_from_id", Value = pQueryEntity.data_from_id });
            if (pQueryEntity.sales_unit_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "sales_unit_id", Value = pQueryEntity.sales_unit_id });
            if (pQueryEntity.purchase_unit_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "purchase_unit_id", Value = pQueryEntity.purchase_unit_id });
            if (pQueryEntity.if_flag!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "if_flag", Value = pQueryEntity.if_flag });
            if (pQueryEntity.customer_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "customer_id", Value = pQueryEntity.customer_id });
            if (pQueryEntity.bat_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "bat_id", Value = pQueryEntity.bat_id });
            if (pQueryEntity.sales_warehouse_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "sales_warehouse_id", Value = pQueryEntity.sales_warehouse_id });
            if (pQueryEntity.purchase_warehouse_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "purchase_warehouse_id", Value = pQueryEntity.purchase_warehouse_id });
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
            if (pQueryEntity.paymentcenter_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "paymentcenter_id", Value = pQueryEntity.paymentcenter_id });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out T_InoutEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new T_InoutEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["order_id"] != DBNull.Value)
			{
				pInstance.order_id =  Convert.ToString(pReader["order_id"]);
			}
			if (pReader["order_no"] != DBNull.Value)
			{
				pInstance.order_no =  Convert.ToString(pReader["order_no"]);
			}
			if (pReader["order_type_id"] != DBNull.Value)
			{
				pInstance.order_type_id =  Convert.ToString(pReader["order_type_id"]);
			}
			if (pReader["order_reason_id"] != DBNull.Value)
			{
				pInstance.order_reason_id =  Convert.ToString(pReader["order_reason_id"]);
			}
			if (pReader["red_flag"] != DBNull.Value)
			{
				pInstance.red_flag =  Convert.ToString(pReader["red_flag"]);
			}
			if (pReader["ref_order_id"] != DBNull.Value)
			{
				pInstance.ref_order_id =  Convert.ToString(pReader["ref_order_id"]);
			}
			if (pReader["ref_order_no"] != DBNull.Value)
			{
				pInstance.ref_order_no =  Convert.ToString(pReader["ref_order_no"]);
			}
			if (pReader["warehouse_id"] != DBNull.Value)
			{
				pInstance.warehouse_id =  Convert.ToString(pReader["warehouse_id"]);
			}
			if (pReader["order_date"] != DBNull.Value)
			{
				pInstance.order_date =  Convert.ToString(pReader["order_date"]);
			}
			if (pReader["request_date"] != DBNull.Value)
			{
				pInstance.request_date =  Convert.ToString(pReader["request_date"]);
			}
			if (pReader["complete_date"] != DBNull.Value)
			{
				pInstance.complete_date =  Convert.ToString(pReader["complete_date"]);
			}
			if (pReader["create_unit_id"] != DBNull.Value)
			{
				pInstance.create_unit_id =  Convert.ToString(pReader["create_unit_id"]);
			}
			if (pReader["unit_id"] != DBNull.Value)
			{
				pInstance.unit_id =  Convert.ToString(pReader["unit_id"]);
			}
			if (pReader["related_unit_id"] != DBNull.Value)
			{
				pInstance.related_unit_id =  Convert.ToString(pReader["related_unit_id"]);
			}
			if (pReader["related_unit_code"] != DBNull.Value)
			{
				pInstance.related_unit_code =  Convert.ToString(pReader["related_unit_code"]);
			}
			if (pReader["pos_id"] != DBNull.Value)
			{
				pInstance.pos_id =  Convert.ToString(pReader["pos_id"]);
			}
			if (pReader["shift_id"] != DBNull.Value)
			{
				pInstance.shift_id =  Convert.ToString(pReader["shift_id"]);
			}
			if (pReader["sales_user"] != DBNull.Value)
			{
				pInstance.sales_user =  Convert.ToString(pReader["sales_user"]);
			}
			if (pReader["total_amount"] != DBNull.Value)
			{
				pInstance.total_amount =  Convert.ToDecimal(pReader["total_amount"]);
			}
			if (pReader["discount_rate"] != DBNull.Value)
			{
				pInstance.discount_rate =  Convert.ToDecimal(pReader["discount_rate"]);
			}
			if (pReader["actual_amount"] != DBNull.Value)
			{
				pInstance.actual_amount =  Convert.ToDecimal(pReader["actual_amount"]);
			}
			if (pReader["receive_points"] != DBNull.Value)
			{
				pInstance.receive_points =  Convert.ToDecimal(pReader["receive_points"]);
			}
			if (pReader["pay_points"] != DBNull.Value)
			{
				pInstance.pay_points =  Convert.ToDecimal(pReader["pay_points"]);
			}
			if (pReader["pay_id"] != DBNull.Value)
			{
				pInstance.pay_id =  Convert.ToString(pReader["pay_id"]);
			}
			if (pReader["print_times"] != DBNull.Value)
			{
				pInstance.print_times =   Convert.ToInt32(pReader["print_times"]);
			}
			if (pReader["carrier_id"] != DBNull.Value)
			{
				pInstance.carrier_id =  Convert.ToString(pReader["carrier_id"]);
			}
			if (pReader["remark"] != DBNull.Value)
			{
				pInstance.remark =  Convert.ToString(pReader["remark"]);
			}
			if (pReader["status"] != DBNull.Value)
			{
				pInstance.status =  Convert.ToString(pReader["status"]);
			}
			if (pReader["status_desc"] != DBNull.Value)
			{
				pInstance.status_desc =  Convert.ToString(pReader["status_desc"]);
			}
			if (pReader["total_qty"] != DBNull.Value)
			{
				pInstance.total_qty =  Convert.ToDecimal(pReader["total_qty"]);
			}
			if (pReader["total_retail"] != DBNull.Value)
			{
				pInstance.total_retail =  Convert.ToDecimal(pReader["total_retail"]);
			}
			if (pReader["keep_the_change"] != DBNull.Value)
			{
				pInstance.keep_the_change =  Convert.ToDecimal(pReader["keep_the_change"]);
			}
			if (pReader["wiping_zero"] != DBNull.Value)
			{
				pInstance.wiping_zero =  Convert.ToDecimal(pReader["wiping_zero"]);
			}
			if (pReader["vip_no"] != DBNull.Value)
			{
				pInstance.vip_no =  Convert.ToString(pReader["vip_no"]);
			}
			if (pReader["create_time"] != DBNull.Value)
			{
				pInstance.create_time =  Convert.ToString(pReader["create_time"]);
			}
			if (pReader["create_user_id"] != DBNull.Value)
			{
				pInstance.create_user_id =  Convert.ToString(pReader["create_user_id"]);
			}
			if (pReader["approve_time"] != DBNull.Value)
			{
				pInstance.approve_time =  Convert.ToString(pReader["approve_time"]);
			}
			if (pReader["approve_user_id"] != DBNull.Value)
			{
				pInstance.approve_user_id =  Convert.ToString(pReader["approve_user_id"]);
			}
			if (pReader["send_time"] != DBNull.Value)
			{
				pInstance.send_time =  Convert.ToString(pReader["send_time"]);
			}
			if (pReader["send_user_id"] != DBNull.Value)
			{
				pInstance.send_user_id =  Convert.ToString(pReader["send_user_id"]);
			}
			if (pReader["accpect_time"] != DBNull.Value)
			{
				pInstance.accpect_time =  Convert.ToString(pReader["accpect_time"]);
			}
			if (pReader["accpect_user_id"] != DBNull.Value)
			{
				pInstance.accpect_user_id =  Convert.ToString(pReader["accpect_user_id"]);
			}
			if (pReader["modify_time"] != DBNull.Value)
			{
				pInstance.modify_time =  Convert.ToString(pReader["modify_time"]);
			}
			if (pReader["modify_user_id"] != DBNull.Value)
			{
				pInstance.modify_user_id =  Convert.ToString(pReader["modify_user_id"]);
			}
			if (pReader["data_from_id"] != DBNull.Value)
			{
				pInstance.data_from_id =  Convert.ToString(pReader["data_from_id"]);
			}
			if (pReader["sales_unit_id"] != DBNull.Value)
			{
				pInstance.sales_unit_id =  Convert.ToString(pReader["sales_unit_id"]);
			}
			if (pReader["purchase_unit_id"] != DBNull.Value)
			{
				pInstance.purchase_unit_id =  Convert.ToString(pReader["purchase_unit_id"]);
			}
			if (pReader["if_flag"] != DBNull.Value)
			{
				pInstance.if_flag =  Convert.ToString(pReader["if_flag"]);
			}
			if (pReader["customer_id"] != DBNull.Value)
			{
				pInstance.customer_id =  Convert.ToString(pReader["customer_id"]);
			}
			if (pReader["bat_id"] != DBNull.Value)
			{
				pInstance.bat_id =  Convert.ToString(pReader["bat_id"]);
			}
			if (pReader["sales_warehouse_id"] != DBNull.Value)
			{
				pInstance.sales_warehouse_id =  Convert.ToString(pReader["sales_warehouse_id"]);
			}
			if (pReader["purchase_warehouse_id"] != DBNull.Value)
			{
				pInstance.purchase_warehouse_id =  Convert.ToString(pReader["purchase_warehouse_id"]);
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
				pInstance.paymentcenter_id =  Convert.ToString(pReader["paymentcenter_id"]);
			}


            if (pReader.GetSchemaTable().Select("[ColumnName]='Payment_Type_Code'").Length == 1&& pReader["Payment_Type_Code"] != DBNull.Value)
            {
                pInstance.Payment_Type_Code = Convert.ToString(pReader["Payment_Type_Code"]);
            }
            if (pReader.GetSchemaTable().Select("[ColumnName]='Payment_Type_Name'").Length == 1 && pReader["Payment_Type_Name"] != DBNull.Value)
            {
                pInstance.Payment_Type_Name = Convert.ToString(pReader["Payment_Type_Name"]);
            }

            
        }
        #endregion
    }
}
