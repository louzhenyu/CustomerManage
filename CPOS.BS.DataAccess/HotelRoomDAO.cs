/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/21 11:41:01
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
    /// 数据访问：  
    /// 表HotelRoom的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class HotelRoomDAO : Base.BaseCPOSDAO, ICRUDable<HotelRoomEntity>, IQueryable<HotelRoomEntity>
    {

        #region GetHotelReservation 获取酒店预定信息
        /// <summary>
        /// 获取酒店预定信息
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public DataSet GetHotelReservation(Dictionary<string, object> dic)
        {
            StringBuilder strb = new StringBuilder();
            strb.AppendFormat(@"
                        select  
                         ap_t.customer_name
                        ,h.RoomName
                        ,t.unit_name 
                        ,0 SalesTotalAmount
                        from  T_Unit as t 
                        inner join HotelRoom as h on t.unit_id=h.UnitId
                        left  join cpos_ap..t_customer as ap_t on ap_t.customer_id=h.CustomerId
                        where h.RoomId='{1}'
                        and h.IsDelete=0 and h.CustomerId='{2}'
                        ", dic["VipID"], dic["RoomId"], this.CurrentUserInfo.ClientID);
            decimal SalesTotalAmount = GetSalesTotalAmount(dic);
            DataSet ds = this.SQLHelper.ExecuteDataset(strb.ToString());
            ds.Tables[0].Rows[0]["SalesTotalAmount"] = SalesTotalAmount;
            return ds;

        }

        /// <summary>
        /// 获取预定信息的总金额
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public decimal GetSalesTotalAmount(Dictionary<string, object> dic)
        {
            string RoomId = dic["RoomId"].ToString();
            DateTime BeginDate = Convert.ToDateTime(dic["BeginDate"]);
            DateTime EndDate = Convert.ToDateTime(dic["EndDate"]);
            int RoomQty = Convert.ToInt32(dic["RoomQty"]);

            HotelRoomEntity entity = QueryByEntity(new HotelRoomEntity() { RoomId = RoomId, IsDelete = 0 }, null)[0];
            decimal StandardPrice = (decimal)entity.StandardPrice;//房型标准价格
            StringBuilder strb = null;

            string sql = @"
                            select top 1 FloatPrice
                                    from HotelRoom as hotel
                                    left join  HotelDynamicPrice as hotelDynamic on hotel.RoomId=hotelDynamic.RoomId 
                                    where
                                    hotel.IsDelete=0 
                                    and hotelDynamic.IsDelete=0
                                    and hotel.RoomId='{0}'   
                                    and hotel.CustomerId='{1}'
                                    and '{2}' between hotelDynamic.EffectiveBeginDate and EffectiveEndDate 
                          ";
            decimal SalesTotalAmount = 0;
            for (int i = 0; i < 100000000; i++)
            {
                DateTime newBeginEnd = BeginDate.AddDays(i);
                if (DateTime.Compare(newBeginEnd, EndDate) == 0)
                {
                    return SalesTotalAmount;
                }
                strb = new StringBuilder();
                strb.AppendFormat(sql, RoomId, this.CurrentUserInfo.ClientID, newBeginEnd);
                object obj = this.SQLHelper.ExecuteScalar(strb.ToString());
                if (obj == null)
                {
                    SalesTotalAmount += StandardPrice * RoomQty;
                }
                else
                {
                    SalesTotalAmount += (decimal)obj * RoomQty;
                }


            }
            return 0;
        }



        #endregion


        #region 获取酒店预定 房费明细

        public DataTable GetHotelReservationDetailAmount(Dictionary<string, object> dic)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("IsBreakfast"));
            dt.Columns.Add(new DataColumn("SameDay"));
            dt.Columns.Add(new DataColumn("Price", typeof(Decimal)));
            dt.Columns.Add(new DataColumn("RoomQty", typeof(int)));

            string RoomId = dic["RoomId"].ToString();
            DateTime BeginDate = Convert.ToDateTime(dic["BeginDate"]);
            DateTime EndDate = Convert.ToDateTime(dic["EndDate"]);
            int RoomQty = Convert.ToInt32(dic["RoomQty"]);

            HotelRoomEntity entity = QueryByEntity(new HotelRoomEntity() { RoomId = RoomId, IsDelete = 0 }, null)[0];
            decimal StandardPrice = (decimal)entity.StandardPrice;//房型标准价格

            string strSql = " select IsBreakfast from VwHotelRoomSku where RoomID='" + RoomId + "'";
            object IsBreakfast = this.SQLHelper.ExecuteScalar(strSql);


            string sql = @"
                            select top 1 FloatPrice
                                    from HotelRoom as hotel
                                    left join  HotelDynamicPrice as hotelDynamic on hotel.RoomId=hotelDynamic.RoomId 
                                    where
                                    hotel.IsDelete=0 
                                    and hotelDynamic.IsDelete=0
                                    and hotel.RoomId='{0}'   
                                    and hotel.CustomerId='{1}'
                                    and '{2}' between hotelDynamic.EffectiveBeginDate and EffectiveEndDate ";



            StringBuilder strb = null;
            for (int i = 0; i < 100000000; i++)
            {
                DateTime newBeginEnd = BeginDate.AddDays(i);
                strb = new StringBuilder();
                if (DateTime.Compare(newBeginEnd, EndDate) == 0)
                {
                    return dt;
                }
                strb.AppendFormat(sql, RoomId, this.CurrentUserInfo.ClientID, newBeginEnd);
                object obj = this.SQLHelper.ExecuteScalar(strb.ToString());

                DataRow dr = dt.NewRow();
                dr["IsBreakfast"] = IsBreakfast;
                dr["SameDay"] = newBeginEnd.ToString("yyyy-MM-dd");
                dr["RoomQty"] = dic["RoomQty"];

                if (obj == null)
                {
                    dr["Price"] = StandardPrice;
                }
                else
                {

                    dr["Price"] = obj;
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }


        #endregion


        #region AddHotelsOrder 提交预定订单
        public void AddHotelsOrder(Dictionary<String, object> dic)
        {
            string OrderTypeId = dic["OrderTypeId"].ToString();
            DateTime OrderDate = Convert.ToDateTime(dic["OrderDate"]);
            string UnitId = dic["UnitId"].ToString();
            string ReservationsVipId = dic["ReservationsVipId"].ToString();
            string ReservationsTime = dic["ReservationsTime"].ToString();
            string Contact = dic["Contact"].ToString();
            string ContactPhone = dic["ContactPhone"].ToString();
            DateTime BeginDate = Convert.ToDateTime(dic["BeginDate"]);
            DateTime EndDate = Convert.ToDateTime(dic["EndDate"]);
            String ShopTime = Convert.ToString(dic["ShopTime"]);
            int RoomQty = Convert.ToInt32(dic["RoomQty"]);
            decimal PointsDAmount = Convert.ToDecimal(dic["PointsDAmount"]);
            decimal CouponDAmount = Convert.ToDecimal(dic["CouponDAmount"]);
            decimal OverDAmount = Convert.ToDecimal(dic["OverDAmount"]);




        }
        #endregion


        #region 获取酒店预定 房费原价
        public decimal GetStdTotalAmount(Dictionary<string, object> dic)
        {

            string RoomId = dic["RoomId"].ToString();
            DateTime BeginDate = Convert.ToDateTime(dic["BeginDate"]);
            DateTime EndDate = Convert.ToDateTime(dic["EndDate"]);
            int RoomQty = Convert.ToInt32(dic["RoomQty"]);

            HotelRoomEntity entity = QueryByEntity(new HotelRoomEntity() { RoomId = RoomId, IsDelete = 0 }, null)[0];
            decimal StandardPrice = (decimal)entity.StandardPrice;//房型标准价格
            StringBuilder strb = null;

            decimal SalesTotalAmount = 0;
            for (int i = 0; i < 100000000; i++)
            {
                DateTime newBeginEnd = BeginDate.AddDays(i);
                if (DateTime.Compare(newBeginEnd, EndDate) == 0)
                {
                    return SalesTotalAmount;
                }
                SalesTotalAmount += StandardPrice * RoomQty;

            }
            return 0;
        }

        #endregion


    }
}