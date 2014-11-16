using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.BLL;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.Web.ApplicationInterface.Hotle
{
    /// <summary>
    /// HotleGateway 的摘要说明
    /// </summary>
    public class HotleGateway : BaseGateway
    {
        #region 获取城市列表
        /// <summary>
        /// 获取城市列表
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetCityListByUnit(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetCityListByUnitRP>>();

            var cityCode = rp.Parameters.CityCode;
            var cityName = rp.Parameters.CityName;

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

            var storeBll = new StoreBLL(loggingSessionInfo);
            var ds = storeBll.GetCityDsByUnit(cityName, cityCode, rp.CustomerID);

            var rd = new GetCityListByUnitRD();

            if (ds.Tables[0].Rows.Count > 0)
            {
                var tmp = ds.Tables[0].AsEnumerable().Select(t => new GetCityListByUnitInfo()
                    {
                        CityId = t["ABC"].ToString(),
                        CityCode = t["CityCode"].ToString(),
                        CityName = t["CityName"].ToString(),
                        Type = t["type"].ToString()
                    });
                rd.CityList = tmp.ToArray();
            }

            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }

        #endregion

        public string GetHotleStarsAndPrice(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<EmptyRequestParameter>>();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");


            var storeBll = new StoreBLL(loggingSessionInfo);
            var ds = storeBll.GetHotleStarAndPrice();

            var rd = new GetHotleStarsAndPriceRD();

            if (ds.Tables[0].Rows.Count > 0)
            {
                var tmp = ds.Tables[0].AsEnumerable().Select(t => new HotleStarsList()
                    {
                        StrasCode = t["StarsCode"].ToString(),
                        StrasName = t["StarsName"].ToString()
                    });
                rd.HotleStarsList = tmp.ToArray();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                var tmp = ds.Tables[1].AsEnumerable().Select(t => new HotlePriceList()
                {
                    PriceFrom = Convert.ToDecimal(t["PriceBegin"].ToString()),
                    PriceTo = Convert.ToDecimal(t["PriceEnd"].ToString()),
                    PriceText = t["PriceName"].ToString()
                });
                rd.HotlePriceList = tmp.ToArray();
            }

            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }

        public string GetHotleList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetHotleListRP>>();

            if (rp.CustomerID == "" || string.IsNullOrEmpty(rp.CustomerID))
            {
                throw new APIException("请求参数中缺少CustomerID或值为空.") { ErrorCode = 121 };
            }

            //if (rp.Parameters.CityCode == "" || string.IsNullOrEmpty(rp.Parameters.CityCode))
            //{
            //    throw new APIException("请求参数中缺少CityCode或值为空.") { ErrorCode = 122 };
            //}
            if (rp.Parameters.DateFrom == "" || string.IsNullOrEmpty(rp.Parameters.DateFrom))
            {
                throw new APIException("请求参数中缺少DateFrom或值为空.") { ErrorCode = 123 };
            }

            if (rp.Parameters.DateTo == "" || string.IsNullOrEmpty(rp.Parameters.DateTo))
            {
                throw new APIException("请求参数中缺少DateTo或值为空.") { ErrorCode = 124 };
            }

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

            var rd = new GetHotleListRD();

            var storeBll = new StoreBLL(loggingSessionInfo);

            var ds = storeBll.GetHotleList(rp.Parameters.CityCode, rp.Parameters.DateFrom,
                rp.Parameters.DateTo, rp.Parameters.HotleName, rp.Parameters.PriceFrom,
                rp.Parameters.PriceTo, rp.Parameters.HotleStar,
                rp.Parameters.log, rp.Parameters.lat, rp.CustomerID,rp.Parameters.OrderItem,rp.Parameters.OrderType,rp.Parameters.Radius);

            if (ds.Tables[0].Rows.Count > 0)
            {
                var tmp = ds.Tables[0].AsEnumerable().Select(t => new GetHotleListInfo()
                    {
                        UnitId = t["UnitId"].ToString(),
                        UnitName = t["UnitName"].ToString(),
                        ImageUrl = t["ImageUrl"].ToString(),
                        Score = Convert.ToDecimal(t["Score"].ToString()),

                        IsWifi = Convert.ToInt32(t["IsWifi"].ToString()),
                        IsCarPart = Convert.ToInt32(t["IsCarPart"].ToString()),
                        Start = Convert.ToInt32(t["Star"].ToString()),
                        StartName = t["StarDec"].ToString(),
                        SalesAmount = Convert.ToDecimal(t["MinPrice"].ToString()),
                        Address = t["Address"].ToString(),
                        Length = Convert.ToDecimal(t["Distance"].ToString()),
                        IsCashBack = Convert.ToInt32(t["IsCashBack"].ToString()),
                        IsPhoneExclusive = Convert.ToInt32(t["IsPhoneExclusive"].ToString()),
                        IsCustomers = Convert.ToInt32(t["IsCustomers"].ToString()),
                        IsCoupon = Convert.ToInt32(t["IsCoupon"].ToString()),
                        Promotions = Convert.ToInt32(t["IsPromotions"].ToString()),
                        HotlesIntroduction = t["HotelsIntroduction"].ToString(),
                        HotleType = t["HotelType"].ToString(),


                        CityCode = t["City_Code"].ToString(),
                        CityName = t["City_Name"].ToString(),
                        Lat = Convert.ToDecimal(t["lat"].ToString()),
                        Lng = Convert.ToDecimal(t["log"].ToString())

                    });
                rd.GetHotleListInfo = tmp.ToArray();
            }
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }


        #region GetHotelReservation 获取预定酒店信息
        public string GetHotelReservation(string pRequest)
        {
            HotelReservationRD rd = new HotelReservationRD();
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<HotelReservationRP>>();
                LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                //var vipentity = new VipBLL(loggingSessionInfo).QueryByEntity(new VipEntity()
                //{
                //    Phone = rp.UserID
                //}, null).FirstOrDefault();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("VipID", rp.UserID);
                dic.Add("RoomId", rp.Parameters.RoomId);
                dic.Add("BeginDate", rp.Parameters.BeginDate);
                dic.Add("EndDate", rp.Parameters.EndDate);
                dic.Add("RoomQty", rp.Parameters.RoomQty);
                var bll = new HotelRoomBLL(loggingSessionInfo);
                DataSet ds=  bll.GetHotelReservation(dic);
                if (ds!=null&&ds.Tables.Count>0)
                {
                   DataRow dr=ds.Tables[0].Rows[0];
                   rd.RoomName = dr["RoomName"].ToString();
                   rd.SalesTotalAmount = Convert.ToDecimal(dr["SalesTotalAmount"]);
                   rd.UnitName = dr["unit_name"].ToString();
                   rd.CustomerName = dr["customer_name"].ToString();
                }

                var rsp = new SuccessResponse<IAPIResponseData>(rd);
                return rsp.ToJSON();

               // return "{\"ResultCode\": 0,\"Message\": \"OK\",\"Data\": {\"CustomerName\": \"酒店\",\"UnitName\": \"宜必思酒店(东大桥店)\",\"RoomName\": \"无敌海景房\", \"SalesTotalAmount\": \"1120\",\"VipRealName\": \"周擎罡\",\"Phone\": \"18917112278\" }}";
            }
            catch (Exception ex)
            {

                throw new APIException(ex.Message);
            }


        }
        #endregion

        #region 预定酒店 获取房费明细
        public string GetHotelReservationDetailAmount(string pRequest)
        {
            HotelReservationHotelReservationRD rd = new HotelReservationHotelReservationRD();
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<HotelReservationRP>>();
                LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                //var vipentity = new VipBLL(loggingSessionInfo).QueryByEntity(new VipEntity()
                //{
                //    Phone = rp.UserID
                //}, null).FirstOrDefault();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("VipID", rp.UserID);
                dic.Add("RoomId", rp.Parameters.RoomId);
                dic.Add("BeginDate", rp.Parameters.BeginDate);
                dic.Add("EndDate", rp.Parameters.EndDate);
                dic.Add("RoomQty", rp.Parameters.RoomQty);
                var bll = new HotelRoomBLL(loggingSessionInfo);
                DataTable dt = bll.GetHotelReservationDetailAmount(dic);
                rd.DetailAmountList=  DataTableToObject.ConvertToList<DetailAmount>(dt);
                 DataSet ds=bll.GetHotelReservation(dic);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    rd.SalesTotalAmount = Convert.ToDecimal(dr["SalesTotalAmount"]);
                }
                var rsp = new SuccessResponse<IAPIResponseData>(rd);
                return rsp.ToJSON();
            }
            catch (Exception)
            {
                
                throw;
            }

          // return "{\"ResultCode\": \"0\",\"Message\": \"OK\", \"Data\": {\"SalesTotalAmount\":\"1120\", \"DetailAmountList\":[{\"IsBreakfast\":\"true\", \"SameDay\":\"2014-07-21\", \"Price\":\"560\", \"RoomQty\":\"2\" }] }}";
        }
        #endregion

        #region AddHotelsCheckUser 添加用户信息
        public string AddHotelsCheckUser(string pRequest)
        {
            try
            {
                HotelReservationRD rd = new HotelReservationRD();
                var rp = pRequest.DeserializeJSONTo<APIRequest<HotelsCheckUserRP>>();
                LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                var bll = new HotelsCheckUserBLL(loggingSessionInfo);
                var list = rp.Parameters.CheckUserList;
                foreach (CheckUser item in list)
                {
                    bool bl = false;
                    string str = "";
                    if (item.CheckUserId == null)
                    {
                        var result = bll.QueryByEntity(new HotelsCheckUserEntity { IsDelete = 0, CheckUserName = item.CheckUserName, VipId = rp.UserID }, null).FirstOrDefault();
                        if (result != null)
                        {
                            bl = true;
                            str += item.CheckUserName + ",";
                        }
                    }
                    if (bl)
                    {
                        return "{ \"ResultCode\": 100,\"Message\": \"" + str.Trim(',') + "已添加\"}";
                    }
                }
                foreach (CheckUser item in list)
                {

                    if (item.CheckUserId == null)
                    {
                        bll.Create(new HotelsCheckUserEntity
                        {
                            VipId = rp.UserID,
                            CheckUserName = item.CheckUserName,
                            CheckUserId = Guid.NewGuid(),
                            IsDelete = 0
                        });
                    }

                }
                var rsp = new SuccessResponse<IAPIResponseData>();
                return rsp.ToJSON();
              //  return "{\"ResultCode\":\"0\",\"Message\": \"OK\"}";
            }
            catch (Exception ex)
            {

                throw new APIException(ex.Message);
            }

        }
        #endregion

        #region DelHotelsCheckUser 删除 入住用户信息
        public string DelHotelsCheckUser(string pRequest)
        {
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<HotelsDelCheckUserRP>>();
                LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                new HotelsCheckUserBLL(loggingSessionInfo).Delete(new HotelsCheckUserEntity
                {
                    CheckUserId = rp.Parameters.CheckUserId
                });
                var rsp = new SuccessResponse<IAPIResponseData>();
                return rsp.ToJSON();
               // return string.Format("{\"ResultCode\":\"0\",\"Message\": \"OK\"}");
            }
            catch (Exception ex)
            {

                throw new APIException(ex.Message);
            }
        }

        #endregion

        #region GetHotelsCheckUserByVipId 获取入住用户信息

        /// <summary>
        ///获取入住用户信息
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetHotelsCheckUserByVipId(string pRequest)
        {
            try
            {
                HotelReservationRD rd = new HotelReservationRD();
                var rp = pRequest.DeserializeJSONTo<APIRequest<HotelsCheckUserRP>>();
                LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);



                HotelsCheckUserEntity[] entitys = new HotelsCheckUserBLL(loggingSessionInfo).QueryByEntity(new HotelsCheckUserEntity() { IsDelete = 0, VipId = rp.UserID }, null);
                HotelsCheckUserRD RD = new HotelsCheckUserRD();
                if (entitys != null && entitys.Length > 0)
                {
                    List<CheckUser> CheckUserList = new List<CheckUser>();
                    foreach (HotelsCheckUserEntity item in entitys)
                    {
                        CheckUser user = new CheckUser
                        {
                            CheckUserId = item.CheckUserId,
                            CheckUserName = item.CheckUserName
                        };
                        CheckUserList.Add(user);
                    }
                    RD.CheckUserList = CheckUserList;

                }
                var rsp = new SuccessResponse<IAPIResponseData>(RD);
                return rsp.ToJSON();

              //  return "{\"ResultCode\":\"0\",\"Message\": \"OK\",\"Data\": {\"HotelsCheckUser \":[{\"CheckUserName\":\"曹希军\", \"CheckUserId\":\" DEA9286B-5127-4EDB-B4D7-1A2429A56E3F\"},{\"CheckUserName\":\"周擎罡\", \"CheckUserId\":\" DEA9286B-5127-4EDB-B4Q7-1A2429A56E3Q\"}] }}";
            }
            catch (Exception ex)
            {

                throw new APIException(ex.Message);
            }

        }

        #endregion

        #region AddHotelsOrder 预定酒店 提交订单

        public string AddHotelsOrder(string pRequest)
        {
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<HotelsOrderRP>>();
                LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("RoomId", rp.Parameters.RoomId);
                dic.Add("BeginDate", rp.Parameters.BeginDate);
                dic.Add("EndDate", rp.Parameters.EndDate);
                dic.Add("RoomQty", rp.Parameters.RoomQty);
                var roombll = new HotelRoomBLL(loggingSessionInfo);
                decimal StdTotalAmount = roombll.GetStdTotalAmount(dic);
                string OrderId = Guid.NewGuid().ToString();
                var bll = new HotelsOrderBLL(loggingSessionInfo);
                var service = new GOrderBLL(loggingSessionInfo);
   
          


                bll.Create(new HotelsOrderEntity
                {
                    OrderId = OrderId,
                    OrderNo = new TUnitExpandBLL(loggingSessionInfo).GetUnitOrderNo(loggingSessionInfo, rp.Parameters.UnitId),
                    OrderTypeId = rp.Parameters.OrderTypeId,
                    OrderDate = rp.Parameters.OrderDate,
                    CustomerId = rp.CustomerID,
                    UnitId = rp.Parameters.UnitId,
                    ReservationsVipId = rp.UserID,
                    ReservationsTime = DateTime.Now,
                    Contact = rp.Parameters.Contact,
                    ContactPhone = rp.Parameters.ContactPhone,
                    BeginDate = rp.Parameters.BeginDate,
                    EndDate = rp.Parameters.EndDate,
                    ShopTime = rp.Parameters.ShopTime,
                    CheckDaysQty = GetCheckDaysQty(rp.Parameters.BeginDate, rp.Parameters.EndDate),
                    RoomQty = rp.Parameters.RoomQty,
                    StdTotalAmount = StdTotalAmount,
                    SalesTotalAmount = rp.Parameters.SalesTotalAmount,
                    RetailTotalAmount = (rp.Parameters.SalesTotalAmount - rp.Parameters.PointsDAmount - rp.Parameters.CouponDAmount - rp.Parameters.OverDAmount),
                    RetailNeedTotalAmount = (rp.Parameters.SalesTotalAmount - rp.Parameters.PointsDAmount - rp.Parameters.CouponDAmount - rp.Parameters.OverDAmount),
                    PointsDAmount = rp.Parameters.SalesTotalAmount,
                    CouponDAmount = rp.Parameters.PointsDAmount,
                    OverDAmount = rp.Parameters.OverDAmount,
                    OrderSource = 1,
                    OrderStatus = 100,
                    OrderStatusDesc = "预订",
                    CreateBy = rp.UserID,
                    CreateTime = DateTime.Now,
                    IsDelete = 0,
                    DiscountRate = StdTotalAmount / rp.Parameters.SalesTotalAmount,
                    PaymentTypeId="1"
                }, null);
                var bllorderDetail = new HotelsOrderDetailBLL(loggingSessionInfo);
                for (int i = 0; i < 100000000; i++)
                {
                    DateTime newBeginEnd = rp.Parameters.BeginDate.AddDays(i);
                    if (DateTime.Compare(newBeginEnd, rp.Parameters.EndDate) == 0)
                    {
                        break;
                    }
                    Dictionary<string, object> detaildic = new Dictionary<string, object>();
                    detaildic.Add("RoomId", rp.Parameters.RoomId);
                    detaildic.Add("BeginDate", newBeginEnd);
                    detaildic.Add("EndDate", newBeginEnd.AddDays(1));
                    detaildic.Add("RoomQty", rp.Parameters.RoomQty);
                    decimal demailStdTotalAmount = roombll.GetStdTotalAmount(detaildic);
                    decimal demailSalesTotalAmount = roombll.GetSalesTotalAmount(dic);
                    bllorderDetail.Create(new HotelsOrderDetailEntity
                    {
                        OrderId = OrderId,
                        CurrencyType = "RMB",
                        RoomQty = rp.Parameters.RoomQty,
                        OrderDetailId = Guid.NewGuid().ToString(),
                        RoomId = rp.Parameters.RoomId,
                        CheckInDate = newBeginEnd,
                        CheckInPeople = rp.Parameters.CheckInPeople,
                        InStatus = 10,
                        StdPrice = demailStdTotalAmount / rp.Parameters.RoomQty,
                        SalesAmount = demailSalesTotalAmount / rp.Parameters.RoomQty,
                        SalesTotalAmount = demailSalesTotalAmount,
                        DiscountRate = (demailStdTotalAmount / rp.Parameters.RoomQty) / (demailSalesTotalAmount / rp.Parameters.RoomQty),
                        CreateBy = rp.UserID,
                        CreateTime = DateTime.Now,
                        IsDelete = 0
                    });


                }
                //var rsp = new SuccessResonse<IAPIResponseData>();
                //return rsp.ToJSON();
                return "{\"ResultCode\":\"0\",\"Message\": \"OK\",\"OrderId\":\""+OrderId+"\"}";
            }
            catch (Exception)
            {

                throw;
            }


            //return "{\"ResultCode\":\"0\",\"Message\": \"OK\"}";

        }

        private int GetCheckDaysQty(DateTime BeginDate, DateTime EndDate)
        {
            int days = 0;
            for (int i = 0; i < 100000000; i++)
            {
                DateTime newBeginEnd = BeginDate.AddDays(i);
                if (DateTime.Compare(newBeginEnd, EndDate) == 0)
                {
                    return days;
                }
                days += 1;
            }
            return days;
        }
        #endregion



        #region 获取酒店详情和房型列表
        /// <summary>
        /// 获取酒店详情
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetHotelDetails(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetHotelDetailsRP>>();

            if (string.IsNullOrEmpty(rp.Parameters.UnitID))
            {
                throw new APIException("请求参数中缺少UnitID或值为空.") { ErrorCode = 121 };
            }
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            var storeBll = new StoreBLL(loggingSessionInfo);
            HotelDetailsRD hotelDetails = null;

            var ds = storeBll.GetHotelDetails(rp.Parameters.UnitID);

            if (ds.Tables[0].Rows.Count > 0)
            {
                hotelDetails = new HotelDetailsRD();
                hotelDetails.UnitID = ds.Tables[0].Rows[0]["UnitID"].ToString();
                hotelDetails.UnitName = ds.Tables[0].Rows[0]["UnitName"].ToString();
                hotelDetails.HotelType = ds.Tables[0].Rows[0]["HotelType"].ToString();
                hotelDetails.Score = Convert.ToDecimal(ds.Tables[0].Rows[0]["Score"].ToString());
                hotelDetails.CommentsPersonCount = Convert.ToInt32(ds.Tables[0].Rows[0]["CommentsPersonCount"]);
                hotelDetails.ImageUrl = ds.Tables[0].Rows[0]["ImageUrl"].ToString();
                hotelDetails.ImageCount = Convert.ToInt32(ds.Tables[0].Rows[0]["ImageCount"]);
                hotelDetails.Environment = Convert.ToDecimal(ds.Tables[0].Rows[0]["Environment"]);
                hotelDetails.Facility = Convert.ToDecimal(ds.Tables[0].Rows[0]["Facility"]);
                hotelDetails.Service = Convert.ToDecimal(ds.Tables[0].Rows[0]["Service"]);
                hotelDetails.Health = Convert.ToDecimal(ds.Tables[0].Rows[0]["Health"]);
                hotelDetails.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                hotelDetails.Log = Convert.ToDouble(ds.Tables[0].Rows[0]["log"]);
                hotelDetails.Lat = Convert.ToDouble(ds.Tables[0].Rows[0]["lat"]);
                hotelDetails.OpenDate = ds.Tables[0].Rows[0]["OpenDate"].ToString();
                hotelDetails.IsWifi = Convert.ToInt32(ds.Tables[0].Rows[0]["IsWifi"]);
                hotelDetails.IsCarPart = Convert.ToInt32(ds.Tables[0].Rows[0]["IsCarPart"]);
            }
            var rsp = new SuccessResponse<IAPIResponseData>(hotelDetails);
            return rsp.ToJSON();
        }
        /// <summary>
        /// 获取房型列表
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetHotelRoomSku(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetHotelRoomRP>>();

            if (string.IsNullOrEmpty(rp.Parameters.UnitID))
            {
                throw new APIException("请求参数中缺少UnitID或值为空.") { ErrorCode = 121 };
            }
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            var storeBll = new StoreBLL(loggingSessionInfo);
 
            var rd =new HotelRoomListRD();

            var ds = storeBll.GetHotelRoomSku(rp.Parameters.UnitID,rp.Parameters.BeginDate,rp.Parameters.EndDate);

            if (ds.Tables[0].Rows.Count > 0)
            {
                var tmp = ds.Tables[0].AsEnumerable().Select(t => new HotelRoomListInfo()
                {
                    RoomID=t["RoomID"].ToString(),
                    ImageUrl = t["ImageUrl"].ToString(),
                    RoomName = t["RoomName"].ToString(),
                    IsBreakfast =Convert.ToInt32(t["IsBreakfast"]),
                    IsKing = Convert.ToInt32(t["IsKing"]),
                    IsWifi = Convert.ToInt32(t["IsWifi"]),
                    MinPrice = Convert.ToDecimal(t["FloatPrice"])
                });
                rd.HotelRoomList = tmp.ToArray();
            }
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }


        #endregion


        protected override string ProcessAction(string pType, string pAction, string pRequest)
        
        {
            string rst;
            switch (pAction)
            {
                case "GetCityListByUnit":
                    rst = this.GetCityListByUnit(pRequest);
                    break;
                case "GetHotleStarsAndPrice":
                    rst = this.GetHotleStarsAndPrice(pRequest);
                    break;
                case "GetHotleList":
                    rst = this.GetHotleList(pRequest);
                    break;
                case "GetHotelReservation":
                    rst = this.GetHotelReservation(pRequest);
                    break;
                case "AddHotelsCheckUser":
                    rst = this.AddHotelsCheckUser(pRequest);
                    break;
                case "DelHotelsCheckUser":
                    rst = this.DelHotelsCheckUser(pRequest);
                    break;
                case "AddHotelsOrder":
                    rst = this.AddHotelsOrder(pRequest);
                    break;
                case "GetHotelReservationDetailAmount":
                    rst = this.GetHotelReservationDetailAmount(pRequest);
                    break;
                case "GetHotelsCheckUserByVipId":
                    rst = this.GetHotelsCheckUserByVipId(pRequest);
                    break;
                case "GetHotelDetails"://获取酒店详情
                    rst = this.GetHotelDetails(pRequest);
                    break;
                case "GetHotelRoomSku"://获取房型列表
                    rst = this.GetHotelRoomSku(pRequest);
                    break;

                case "GetHotelsOrderList":  //我的酒店订单列表 add by changjian.tian 2014-07-22
                    rst = this.GetHotelsOrderList(pRequest);
                    break;
                case "GetHotelsOrderDetails": //我的酒店订单详情 add by changjian.tian 2014-07-22
                    rst = this.GetHotelsOrderDetails(pRequest);
                    break;

                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                    {
                        ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                    };
            }
            return rst;
        }



        #region 我的酒店订单列表
        /// <summary>
        /// 我的酒店订单列表
        /// </summary>
        /// <param name="pRequest">请求参数列表</param>
        /// <returns></returns>
        public string GetHotelsOrderList(string pRequest)
        {
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<HotelsOrderListRP>>();
                var rsp = new SuccessResponse<IAPIResponseData>();
                string pCustomerId=rp.CustomerID;  //客户ID
                string pVipId=rp.Parameters.VipId;  //会员ID
                int pOrderDateRange=rp.Parameters.OrderDateRange; //订单时间范围
                int pHotelOrH5orders=rp.Parameters.HotelOrH5orders;//订单类型.1.商城订单.2.酒店订单
                if (string.IsNullOrWhiteSpace(pCustomerId))
                {
                    throw new APIException("客户ID不能为空") { ErrorCode = 301 };
                }
                if (string.IsNullOrWhiteSpace(pVipId))
                {
                    throw new APIException("VipID不能为空") { ErrorCode = 302 };
                }
                if (string.IsNullOrWhiteSpace(pOrderDateRange.ToString()))
                {
                    throw new APIException("订单时间范围不能为空。1.近三个月。2.三个月前") { ErrorCode = 303 };
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
                HotelsOrderBLL bll = new HotelsOrderBLL(loggingSessionInfo);
                HotelsOrderListRD RD = new HotelsOrderListRD();
                DataSet MyHotelsOrder = bll.GetMyHotelsOrderList(pVipId, pOrderDateRange);
                if (MyHotelsOrder != null && MyHotelsOrder.Tables.Count > 0 && MyHotelsOrder.Tables[0].Rows.Count > 0)
                {
                    var temp = MyHotelsOrder.Tables[0].AsEnumerable().Select(t => new HotelsOrdersInfo
                    {
                        OrderId=t["OrderId"].ToString(),
                        UnitName = t["UnitName"].ToString(),
                        OrderStatus = t["OrderStatus"].ToString(),
                        RetailTotalAmount = t["RetailTotalAmount"] != DBNull.Value ? Convert.ToDecimal(t["RetailTotalAmount"].ToString()) : 0,
                        Address = t["Address"].ToString(),
                        CheckDaysQty = t["CheckDaysQty"] != DBNull.Value ? Convert.ToInt32(t["CheckDaysQty"].ToString()) : 0,
                        BeginDate = t["BeginDate"].ToString(), //开始时间
                        EndDate = t["EndDate"].ToString(), //结束时间
                        RoomQty = t["RoomQty"] != DBNull.Value ? Convert.ToInt32(t["RoomQty"].ToString()) : 0
                    });
                    RD.OrderList = temp.ToArray();
                }
                else
                {
                    rsp.ResultCode = 304;
                    rsp.Message = "没有VipID为"+pVipId+"的订单";
                    throw new APIException(string.Format("没有VipID为'{0}'的订单", pVipId)) { ErrorCode = 304 };
                  
                }
                rsp = new SuccessResponse<IAPIResponseData>(RD);
                return rsp.ToJSON();
            }
            catch (Exception ex)
            {
                throw new APIException("接口错误："+ex.ToString()) {ErrorCode=305 };
            }
        }
        public class HotelsOrderListRP : IAPIRequestParameter
        {
            /// <summary>
            /// 会员ID
            /// </summary>
            public string VipId { get; set; }

            /// <summary>
            /// 订单时间范围1.近三个月 2.三个月前
            /// </summary>
            public int OrderDateRange { get; set; }

            /// <summary>
            /// 订单类型 1.商城订单。2.酒店订单。
            /// </summary>
            public int HotelOrH5orders { get; set; }
            public void Validate()
            {

            }
        }
        public class HotelsOrderListRD:IAPIResponseData
        {
            public HotelsOrdersInfo[] OrderList { get; set; }
        }
        public class HotelsOrdersInfo
        {
            /// <summary>
            /// 订单号
            /// </summary>
            public string OrderId { get; set; }
            /// <summary>
            /// 酒店名称
            /// </summary>
            public string UnitName { get; set; }

            /// <summary>
            /// 订单状态
            /// </summary>
            public string OrderStatus { get; set; }
            /// <summary>
            /// 价格
            /// </summary>
            public decimal RetailTotalAmount { get; set; }

            /// <summary>
            /// 地址
            /// </summary>
            public string Address { get; set; }

            /// <summary>
            /// 住几晚
            /// </summary>
            public int CheckDaysQty { get; set; }
            /// <summary>
            /// 开始时间
            /// </summary>
            public string BeginDate { get; set; }
            /// <summary>
            /// 结束时间
            /// </summary>
            public string EndDate { get; set; }

            /// <summary>
            /// 房间数
            /// </summary>
            public int RoomQty { get; set; }

        }
        #endregion

        #region 我的酒店订单详情
        /// <summary>
        /// 我的酒店订单详情
        /// </summary>
        /// <param name="pRequest">请求参数列表</param>
        /// <returns></returns>
        public string GetHotelsOrderDetails(string pRequest)
        {
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<HotelsOrderDetailsRP>>();
                var rsp = new SuccessResponse<IAPIResponseData>();
                string pCustomerId = rp.CustomerID;  //客户ID
                string pVipId = rp.Parameters.VipId;  //会员ID
                string pOrderId = rp.Parameters.OrderId; //订单ID
               
                if (string.IsNullOrWhiteSpace(pCustomerId))
                {
                    throw new APIException("客户ID不能为空") { ErrorCode = 301 };
                }
                if (string.IsNullOrWhiteSpace(pVipId))
                {
                    throw new APIException("VipID不能为空") { ErrorCode = 302 };
                }
                if (string.IsNullOrWhiteSpace(pOrderId.ToString()))
                {
                    throw new APIException("订单ID不能为空") { ErrorCode = 303 };
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
                HotelsOrderBLL bll = new HotelsOrderBLL(loggingSessionInfo);
                HotelsOrderDetailsRD RD = new HotelsOrderDetailsRD();
                DataSet MyHotelsOrderDetails = bll.GetMyHotelsOrderListDetails(pVipId, pOrderId);
                if (MyHotelsOrderDetails != null && MyHotelsOrderDetails.Tables.Count > 0 && MyHotelsOrderDetails.Tables[0].Rows.Count > 0)
                {
                    var temp = MyHotelsOrderDetails.Tables[0].AsEnumerable().Select(t => new HotelsOrdersDetailInfo
                    {
                       SalesTotalAmount=Convert.ToDecimal(t["SalesTotalAmount"].ToString()), //订单总额
                       OrderStatus=t["OrderStatus"].ToString(),   //订单状态
                       OrderNo = t["OrderNo"].ToString(), //订单号
                       ReservationsTime=t["ReservationsTime"].ToString(), //预订日期
                       PaymentType=t["DefrayTypeName"].ToString(),  //支付方式
                       HotelName=t["HotelName"].ToString(),  //酒店名称
                       RoomName=t["RoomName"].ToString(),  //房型名称
                       RoomQty=t["RoomQty"]!=DBNull.Value?Convert.ToInt32(t["RoomQty"].ToString()):0,
                       HotelAddress=t["HotelAddress"].ToString(),  //酒店地址
                       Log=t["Log"]!=DBNull.Value?Convert.ToDecimal(t["Log"].ToString()):0, //经度
                       Lat=t["Lat"]!=DBNull.Value?Convert.ToDecimal(t["Lat"].ToString()):0, //维度
                       Unit_tel=t["unit_tel"].ToString(), //酒店电话
                       CheckInPeople=t["CheckInPeople"].ToString(),//入住人
                       Contact=t["Contact"].ToString(), //入住联系人
                       ContactPhone=t["ContactPhone"].ToString(),  //联系人电话
                       Remark = t["Remark"].ToString()  //备注信息
                    });
                    RD.MyOrderDetail = temp.ToArray();
                }
                else
                {
                    rsp.ResultCode = 304;
                    rsp.Message = "没有订单ID为" + pOrderId + "的订单明细";
                    throw new APIException(string.Format("没有订单ID为'{0}'的订单", pOrderId)) { ErrorCode = 304 };

                }
                rsp = new SuccessResponse<IAPIResponseData>(RD);
                return rsp.ToJSON();
            }
            catch (Exception ex)
            {
                throw new APIException("接口错误：" + ex.ToString()) { ErrorCode = 305 };
            }
        }
        public class HotelsOrderDetailsRD:IAPIResponseData
        {
            public HotelsOrdersDetailInfo[] MyOrderDetail { get; set; }
        }
        public class HotelsOrdersDetailInfo 
        {
            /// <summary>
            /// 订单总额
            /// </summary>
            public decimal SalesTotalAmount { get; set; }
            /// <summary>
            /// 订单状态
            /// </summary>
            public string OrderStatus { get; set; }
            /// <summary>
            /// 订单号
            /// </summary>
            public string OrderNo { get; set; }

            /// <summary>
            /// 预订日期
            /// </summary>
            public string ReservationsTime { get; set; }

            /// <summary>
            /// 支付方式
            /// </summary>
            public string PaymentType { get; set; }

            /// <summary>
            /// 酒店名称
            /// </summary>
            public string HotelName { get; set; }

            /// <summary>
            /// 房型名称
            /// </summary>
            public string RoomName { get; set; }

            /// <summary>
            /// 房间数量
            /// </summary>
            public int RoomQty { get; set; }

            /// <summary>
            /// 酒店地址
            /// </summary>
            public string HotelAddress { get; set; }

            /// <summary>
            /// 经度
            /// </summary>
            public decimal Log { get; set; }

            /// <summary>
            /// 维度
            /// </summary>
            public decimal Lat { get; set; }

            /// <summary>
            /// 酒店电话
            /// </summary>
            public string Unit_tel { get; set; }

            /// <summary>
            /// 入住人
            /// </summary>
            public string CheckInPeople { get; set; }

            /// <summary>
            /// 联系人
            /// </summary>
            public string Contact { get; set; }

            /// <summary>
            /// 联系人电话
            /// </summary>
            public string ContactPhone { get; set; }

            /// <summary>
            /// 订单备注
            /// </summary>
            public string Remark { get; set; }
        }
        public class HotelsOrderDetailsRP:IAPIRequestParameter
        {
            public string VipId { get; set; }
            public string OrderId { get; set; }
            public void Validate()
            {
              
            }
        }
        #endregion
    }



    public class GetCityListByUnitRP : IAPIRequestParameter
    {
        public string CityName { get; set; }
        public string CityCode { get; set; }
        public void Validate()
        {
        }
    }

    public class GetCityListByUnitRD : IAPIResponseData
    {
        public GetCityListByUnitInfo[] CityList { get; set; }
    }
    public class GetCityListByUnitInfo
    {
        public string CityId { get; set; }
        public string CityName { get; set; }
        public string CityCode { get; set; }
        public string Type { get; set; }
    }

    public class GetHotleStarsAndPriceRD : IAPIResponseData
    {
        public HotleStarsList[] HotleStarsList { get; set; }
        public HotlePriceList[] HotlePriceList { get; set; }
    }

    public class HotleStarsList
    {
        public string StrasCode { get; set; }
        public string StrasName { get; set; }
    }

    public class HotlePriceList
    {
        public string PriceText { get; set; }
        public decimal PriceFrom { get; set; }
        public decimal PriceTo { get; set; }
    }




    public class GetHotleListRP : IAPIRequestParameter
    {
        public string CityCode { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string HotleName { get; set; }

        public decimal PriceFrom { get; set; }

        public decimal PriceTo { get; set; }
        /// <summary>
        /// 酒店星级，多选 逗号分割
        /// </summary>
        public string HotleStar { get; set; }

        public decimal log { get; set; }
        public decimal lat { get; set; }
        /// <summary>
        /// 排序类型 SCORE , DISTANCE ,STAR  
        /// </summary>
        public string OrderItem { get; set; }
        /// <summary>
        /// 排序方式 DESC,ASC  
        /// </summary>
        public string OrderType { get; set; }

        public decimal Radius { get; set; }
        public void Validate()
        {
        }
    }

    public class GetHotleListRD : IAPIResponseData
    {
        public GetHotleListInfo[] GetHotleListInfo { get; set; }
    }

    public class GetHotleListInfo
    {
        public string UnitId { get; set; }
        public string UnitName { get; set; }
        public string ImageUrl { get; set; }
        public decimal Score { get; set; }
        public int IsWifi { get; set; }
        public int IsCarPart { get; set; }
        public int Start { get; set; }
        public string StartName { get; set; }
        public decimal SalesAmount { get; set; }
        public string Address { get; set; }
        public decimal Length { get; set; }

        public int IsCashBack { get; set; }
        public int IsPhoneExclusive { get; set; }
        public int IsCustomers { get; set; }
        public int IsCoupon { get; set; }
        public int Promotions { get; set; }
        public string HotlesIntroduction { get; set; }
        public string HotleType { get; set; }

        public string CityCode { get; set; }
        public string CityName { get; set; }
        public decimal Lng { get; set; }
        public decimal Lat { get; set; }
    }
    #region 酒店预定请求/返回参数

    #region 获取酒店预定 请求参数
    public class HotelReservationRP : IAPIRequestParameter
    {

        public string VipID { set; get; }

        public string RoomId { set; get; }

        public DateTime BeginDate { set; get; }

        public DateTime EndDate { set; get; }

        public int? RoomQty { set; get; }

        public void Validate()
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region 获取酒店预定 返回参数
    public class HotelReservationRD : IAPIResponseData
    {

        public string CustomerName { set; get; }
        public string UnitName { set; get; }
        public string RoomName { set; get; }
        public decimal SalesTotalAmount { set; get; }
        public string VipRealName { set; get; }
        public string Phone { set; get; }

    }
    #endregion

    #region 酒店预订 添加用户信息
    public class HotelsCheckUserRP : IAPIRequestParameter
    {
        public List<CheckUser> CheckUserList { set; get; }
        public void Validate()
        {
            throw new NotImplementedException();
        }
    }
    public class CheckUser
    {

        public Guid? CheckUserId { set; get; }
        public string CheckUserName { set; get; }
    }

    #endregion

    #region 酒店预定 删除用户信息
    public class HotelsDelCheckUserRP : IAPIRequestParameter
    {

        public Guid CheckUserId { set; get; }
        public void Validate()
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region 酒店预定  获取入住用户信息
    public class HotelsCheckUserRD : IAPIResponseData
    {
        public List<CheckUser> CheckUserList { set; get; }
    }


    #endregion

    #region 酒店预定 提交预定订单 请求参数

    public class HotelsOrderRP : IAPIRequestParameter
    {
        public int OrderTypeId { set; get; }
        public string OrderDate { set; get; }

        public string RoomId { set; get; }
        public string UnitId { set; get; }

        public string Contact { set; get; }

        public string ContactPhone { set; get; }

        public DateTime BeginDate { set; get; }

        public DateTime EndDate { set; get; }

        public string ShopTime { set; get; }

        public int RoomQty { set; get; }

        public decimal PointsDAmount { set; get; }

        public decimal CouponDAmount { set; get; }

        public decimal OverDAmount { set; get; }

        public decimal SalesTotalAmount { set; get; }

        public string CheckInPeople { set; get; }
        public void Validate()
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region 获取预定酒店 获取房费明细

    public class HotelReservationHotelReservationRD : IAPIResponseData
    {
        public decimal SalesTotalAmount { set; get; }
        public List<DetailAmount> DetailAmountList { set; get; }
    }

    public class DetailAmount {

        public string IsBreakfast { set; get; }
        public string   SameDay { set; get; }

        public decimal Price { set; get; }

        public int RoomQty { set; get; }
    }
    #endregion

    #endregion


    #region 酒店详情和房型列表参数对象
    /// <summary>
    /// 酒店详情请求参数
    /// </summary>
    public class GetHotelDetailsRP : IAPIRequestParameter
    {
        public string UnitID { get; set; }
        public void Validate()
        {
        }
    }
    /// <summary>
    /// 酒店房型请求参数
    /// </summary>
    public class GetHotelRoomRP : IAPIRequestParameter
    {
        public string UnitID { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public void Validate()
        {
        }
    }
    /// <summary>
    /// 酒店详情返回参数
    /// </summary>
    public class HotelDetailsRD : IAPIResponseData
    {
        public string UnitID { get; set; }
        public string UnitName { get; set; }

        public string HotelType { get; set; }

        public decimal Score { get; set; }

        public int CommentsPersonCount { get; set; }

        public string ImageUrl { get; set; }
        public int ImageCount { get; set; }

        public decimal Environment { get; set; }
        public decimal Facility { get; set; }

        public decimal Service { get; set; }

        public decimal Health { get; set; }

        public string Address { get; set; }

        public double Log { get; set; }

        public double Lat { get; set; }

        public string OpenDate { get; set; }
        public int IsWifi { get; set; }
        public int IsCarPart { get; set; }

    }
    /// <summary>
    /// 酒店房型列表返回参数
    /// </summary>
    public class HotelRoomListRD : IAPIResponseData
    {
        public HotelRoomListInfo[] HotelRoomList { get; set; }
    }
    /// <summary>
    /// 酒店房型列表信息
    /// </summary>
    public class HotelRoomListInfo
    {
        public string RoomID { get; set; }
        public string ImageUrl { get; set; }
        public string RoomName { get; set; }
        public int IsBreakfast { get; set; }
        public int IsKing { get; set; }
        public int IsWifi { get; set; }
        public decimal MinPrice { get; set; }
    }

    #endregion


}