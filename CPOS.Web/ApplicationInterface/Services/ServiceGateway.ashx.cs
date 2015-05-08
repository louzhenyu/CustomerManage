using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.BLL;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using System.Collections;
using System.Configuration;
using JIT.CPOS.Common;
using JIT.Utility.Log;
using System.IO;

namespace JIT.CPOS.Web.ApplicationInterface.Services
{
    /// <summary>
    /// 生活服务接口
    /// </summary>
    public class ServiceGateway : BaseGateway
    {
        /// <summary>
        /// ALD生活服务暂使用cpos_bs_alading数据库
        /// </summary>
        private string CUSTOMERID = ConfigurationManager.AppSettings["ALDServiceCustomerID"];
        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst;
            switch (pAction)
            {
                case "GetUnitList"://获取商家门店列表
                    rst = GetUnitList(pRequest);
                    break;
                case "GetNearbyAndMyUnit":  //查询附近商家和我的商家
                    rst = GetNearbyAndMyUnit(pRequest);
                    break;
                case "GetUnitDetail"://门店详情
                    rst = GetUnitDetail(pRequest);
                    break;
                case "GetVocationList"://获取行业列表
                    rst = GetVocationList(pRequest);
                    break;
                case "GetCouponList"://商家优惠券
                    rst = GetCouponList(pRequest);
                    break;
                case "GetCouponDetail"://获取优惠券二维码
                    rst = GetCouponDetail(pRequest);
                    break;
                case "UpdateCoupon"://使用优惠券
                    rst = UpdateCoupon(pRequest);
                    break;
                case "GetMyIntegral"://我的积分
                    rst = GetMyIntegral(pRequest);
                    break;
                case "GetMyAccount"://账户余额
                    rst = GetMyAccount(pRequest);
                    break;
                case "GetRechargeStrategy"://根据公司标识获取充值策略
                    rst = GetRechargeStrategy(pRequest);
                    break;
                case "GetCardInfo"://获取充值卡信息
                    rst = GetCardInfo(pRequest);
                    break;
                case "ActiveCard"://充值卡充值
                    rst = ActiveCard(pRequest);
                    break;
                default:
                    throw new APIException(string.Format("找不到名为：{0}的Action方法。", pAction));
            }
            return rst;
        }

        #region 生活服务方法 add by Henry 2014-8-18
        /// <summary>
        /// 获取商家门店列表
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected string GetUnitList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetUnitListRP>>();
            var loggingSessionInfo = Default.GetBSLoggingSession(CUSTOMERID, "1");
            var rd = new UnitListRD();

            var unitBll = new UnitBLL(loggingSessionInfo);

            Hashtable htPara = new Hashtable();
            #region 参数赋值
            if (rp.Parameters.IsMyUnit == 1)
                htPara["MemberID"] = rp.UserID;
            else
                htPara["MemberID"] = string.Empty;
            htPara["PageIndex"] = rp.Parameters.PageIndex + 1;
            htPara["PageSize"] = rp.Parameters.PageSize;
            htPara["Longitude"] = rp.Parameters.Longitude;
            htPara["Latitude"] = rp.Parameters.Latitude;
            htPara["Distance"] = rp.Parameters.Distance;
            htPara["SortField"] = rp.Parameters.SortField;
            htPara["SortType"] = rp.Parameters.SortType;
            htPara["IndustryID"] = rp.Parameters.IndustryID;
            htPara["UnitName"] = rp.Parameters.UnitName;
            #endregion
            //查询
            DataSet tempUnit = unitBll.GetUnitList(htPara);

            int totalPageCount = 0;
            UnitInfo[] unitList = GetUnitList(tempUnit, out totalPageCount);

            rd.UnitList = unitList.ToArray();
            rd.TotalPageCount = totalPageCount;
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
            //return "{\"ResultCode\":0,\"Message\":null,\"Data\":{\"UnitList\":[{\"CustomerID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"UnitID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"UnitName\":\"\u77AC\u95F4\u8865\u6C34\u9759\u5B89\u5E97\",\"IsMyUnit\":1,\"CommentPoint\":\"9\",\"AvgPrice\":100,\"UnitTag\":\"\u9759\u5B89\u5BFA \u4E2D\u5C71\u516C\u56ED\",\"Distance\":\"1.2km\",\"EventList\":[{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"},{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"}]},{\"CustomerID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"UnitID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"UnitName\":\"\u77AC\u95F4\u8865\u6C34\u9759\u5B89\u5E97\",\"IsMyUnit\":1,\"CommentPoint\":\"9\",\"AvgPrice\":100,\"UnitTag\":\"\u9759\u5B89\u5BFA \u4E2D\u5C71\u516C\u56ED\",\"Distance\":\"1.2km\",\"EventList\":[{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"},{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"}]},{\"CustomerID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"UnitID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"UnitName\":\"\u77AC\u95F4\u8865\u6C34\u9759\u5B89\u5E97\",\"IsMyUnit\":1,\"CommentPoint\":\"9\",\"AvgPrice\":100,\"UnitTag\":\"\u9759\u5B89\u5BFA \u4E2D\u5C71\u516C\u56ED\",\"Distance\":\"1.2km\",\"EventList\":[{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"},{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"}]},{\"CustomerID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"UnitID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"UnitName\":\"\u77AC\u95F4\u8865\u6C34\u9759\u5B89\u5E97\",\"IsMyUnit\":1,\"CommentPoint\":\"9\",\"AvgPrice\":100,\"UnitTag\":\"\u9759\u5B89\u5BFA \u4E2D\u5C71\u516C\u56ED\",\"Distance\":\"1.2km\",\"EventList\":[{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"},{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"}]},{\"CustomerID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"UnitID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"UnitName\":\"\u77AC\u95F4\u8865\u6C34\u9759\u5B89\u5E97\",\"IsMyUnit\":1,\"CommentPoint\":\"9\",\"AvgPrice\":100,\"UnitTag\":\"\u9759\u5B89\u5BFA \u4E2D\u5C71\u516C\u56ED\",\"Distance\":\"1.2km\",\"EventList\":[{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"},{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"}]},{\"CustomerID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"UnitID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"UnitName\":\"\u77AC\u95F4\u8865\u6C34\u9759\u5B89\u5E97\",\"IsMyUnit\":1,\"CommentPoint\":\"9\",\"AvgPrice\":100,\"UnitTag\":\"\u9759\u5B89\u5BFA \u4E2D\u5C71\u516C\u56ED\",\"Distance\":\"1.2km\",\"EventList\":[{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"},{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"}]}],\"TotalPageCount\":1}}";
        }
        /// <summary>
        /// 通用封装门店数据
        /// </summary>
        /// <param name="tempUnit"></param>
        /// <param name="totalPageCount"></param>
        /// <returns></returns>
        private UnitInfo[] GetUnitList(DataSet tempUnit, out int totalPageCount)
        {
            UnitInfo unitInfo = null;
            List<UnitInfo> unitList = new List<UnitInfo> { };
            EventInfo eventInfo = null;
            totalPageCount = 0;
            string unitID = string.Empty;
            if (tempUnit.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < tempUnit.Tables[0].Rows.Count; i++)
                {
                    unitInfo = new UnitInfo();

                    //总页数
                    if (totalPageCount == 0)
                        totalPageCount = int.Parse(tempUnit.Tables[0].Rows[i]["PageCount"].ToString());
                    unitInfo.CustomerID = tempUnit.Tables[0].Rows[i]["CustomerID"].ToString();
                    unitID = tempUnit.Tables[0].Rows[i]["UnitID"].ToString();
                    unitInfo.UnitID = unitID;
                    unitInfo.ImageUrl = tempUnit.Tables[0].Rows[i]["ImageUrl"].ToString();
                    unitInfo.UnitName = tempUnit.Tables[0].Rows[i]["UnitName"].ToString();
                    unitInfo.IsMyUnit = int.Parse(tempUnit.Tables[0].Rows[i]["IsMyUnit"].ToString());
                    unitInfo.CommentPoint = decimal.Parse(tempUnit.Tables[0].Rows[i]["CommentPoint"].ToString());
                    if (!string.IsNullOrEmpty(tempUnit.Tables[0].Rows[i]["AvgPrice"].ToString()))
                        unitInfo.AvgPrice = int.Parse(tempUnit.Tables[0].Rows[i]["AvgPrice"].ToString());
                    unitInfo.UnitTag = tempUnit.Tables[0].Rows[i]["UnitTag"].ToString();
                    if (!string.IsNullOrEmpty(tempUnit.Tables[0].Rows[i]["longitude"].ToString()))
                        unitInfo.Longitude = Convert.ToDecimal(tempUnit.Tables[0].Rows[i]["longitude"].ToString());
                    if (!string.IsNullOrEmpty(tempUnit.Tables[0].Rows[i]["latitude"].ToString()))
                        unitInfo.Latitude = Convert.ToDecimal(tempUnit.Tables[0].Rows[i]["latitude"].ToString());
                    int distance = 0;//距离
                    //处理返回米
                    if (!string.IsNullOrEmpty(tempUnit.Tables[0].Rows[i]["Distance"].ToString()))
                        distance = Convert.ToInt32(tempUnit.Tables[0].Rows[i]["Distance"]);
                    if (distance < 1000)
                        unitInfo.Distance = distance + "m";
                    else
                        unitInfo.Distance = Math.Round(Convert.ToDecimal(distance) / 1000, 1) + "km";


                    List<EventInfo> eventList = new List<EventInfo> { };
                    //通过门店筛选
                    DataRow[] tempRow = tempUnit.Tables[1].Select("UnitID='" + unitID + "'");
                    if (tempRow.Count() > 0)
                    {
                        DataTable tempEvent = tempRow.CopyToDataTable();
                        List<EventInfo> tempEventList = DataTableToObject.ConvertToList<EventInfo>(tempEvent);
                        ///过滤掉重复记录
                         var EventItemIdList = tempEventList.GroupBy(k => new { k.ItemID })
                                .Select(k => new { ItemID = k.Key.ItemID});
                        foreach (var eventItem in EventItemIdList)
                        {
                            eventInfo = tempEventList.Where(m => m.ItemID ==eventItem.ItemID).FirstOrDefault();
                            eventList.Add(eventInfo);
                        }
                        unitInfo.EventList = eventList.ToArray();
                    }

                    #region 原处理
                    //筛选门店的活动
                    //DataRow[] tempEvent = tempUnit.Tables[1].Select("UnitID='" + unitID + "'");
                    //if (tempEvent.Count() > 0)
                    //{
                    //    for (int j = 0; j < tempEvent.Count(); j++)
                    //    {
                    //        eventInfo = new EventInfo();
                    //        eventInfo.ItemName = tempEvent[j]["ItemName"].ToString();
                    //        eventInfo.Price = Math.Round(Convert.ToDecimal(tempEvent[j]["Price"]), 2);
                    //        eventInfo.SoldPrice = Math.Round(Convert.ToDecimal(tempEvent[j]["SalePrice"]), 2);
                    //        eventList.Add(eventInfo);
                    //    }
                    //    unitInfo.EventList = eventList.ToArray();
                    //}
                    #endregion

                    unitList.Add(unitInfo);
                }
            }
            return unitList.ToArray();
        }
        /// <summary>
        /// 查询附近商家和我的商家
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected string GetNearbyAndMyUnit(string pRequest)
        {
            DateTime netStart = DateTime.Now;//测试程序响应时间-开始
            var rd = new GetNearbyAndMyUnitRD();

            var rp = pRequest.DeserializeJSONTo<APIRequest<GetUnitListRP>>();
            var loggingSessionInfo = Default.GetBSLoggingSession(CUSTOMERID, "1");
            var unitBll = new UnitBLL(loggingSessionInfo);

            #region 附近商家
            Hashtable htNearbyPara = new Hashtable();
            htNearbyPara["MemberID"] = string.Empty;
            htNearbyPara["PageIndex"] = rp.Parameters.PageIndex + 1;
            htNearbyPara["PageSize"] = rp.Parameters.PageSize;
            htNearbyPara["Longitude"] = rp.Parameters.Longitude;
            htNearbyPara["Latitude"] = rp.Parameters.Latitude;
            htNearbyPara["Distance"] = rp.Parameters.Distance;
            htNearbyPara["SortField"] = rp.Parameters.SortField;
            htNearbyPara["SortType"] = rp.Parameters.SortType;
            htNearbyPara["IndustryID"] = string.Empty;
            htNearbyPara["UnitName"] = rp.Parameters.UnitName;

            DateTime dtSqlStart = DateTime.Now;//测试sql加载时间-开始

            DataSet tempNearbyUnit = unitBll.GetUnitList(htNearbyPara);
            DateTime dtSqlEnd = DateTime.Now;
            TimeSpan ts1 = dtSqlEnd - dtSqlStart;//测试sql加载时间-结束

            int totalPageCount = 0;
            UnitInfo[] nearbyUnitList = GetUnitList(tempNearbyUnit, out totalPageCount);
            #endregion

            #region 我的商家
            //Hashtable htMyUnitPara = new Hashtable();
            //htMyUnitPara["MemberID"] = rp.UserID;
            //htMyUnitPara["PageIndex"] = rp.Parameters.PageIndex + 1;
            //htMyUnitPara["PageSize"] = rp.Parameters.PageSize;
            //htMyUnitPara["Longitude"] = 0;
            //htMyUnitPara["Latitude"] = 0;
            //htMyUnitPara["Distance"] = 0;
            //htMyUnitPara["SortField"] = rp.Parameters.SortField;
            //htMyUnitPara["SortType"] = rp.Parameters.SortType;
            //htMyUnitPara["IndustryID"] = string.Empty;


            //DataSet tempMyUnit = unitBll.GetUnitList(htMyUnitPara);
            //UnitInfo[] myUnitList = GetUnitList(tempMyUnit, out totalPageCount);
            #endregion

            #region 封装测试数据
            List<UnitInfo> myUnitList = new List<UnitInfo> { };
            UnitInfo unit = new UnitInfo();
            unit.CustomerID = "10960720a8544be585b5d176f1cfb8e3";
            unit.UnitID = "E944F242362242E7A7ACA26AB7E7657F";
            unit.ImageUrl = "";
            unit.UnitName = "薇薇新娘";
            unit.IsMyUnit = 0;
            unit.CommentPoint = 0;
            unit.AvgPrice = 0;
            unit.UnitTag = "0";
            unit.Distance = "2km";
            unit.Longitude =Convert.ToDecimal(537.976);
            unit.Latitude =Convert.ToDecimal(180.563);
            unit.EventList = null;
            myUnitList.Add(unit);
            #endregion

            rd.MyUnitList = myUnitList.ToArray();
            rd.NearbyUnitList = nearbyUnitList.ToArray();
            rd.sqlTime = ts1.Seconds.ToString();//测试sql加载时间-赋值
            DateTime netEnd = DateTime.Now;
            TimeSpan ts2 = netEnd - netStart;//测试程序加载时间-结束
            rd.netTime = ts2.Seconds.ToString();//测试程序加载时间-赋值

            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
            //return "{\"ResultCode\":200,\"Message\":null,\"Data\":{\"NearbyUnitList\":[{\"CustomerID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"UnitID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"UnitName\":\"\u77AC\u95F4\u8865\u6C34\u9759\u5B89\u5E97\",\"IsMyUnit\":1,\"CommentPoint\":\"9\",\"AvgPrice\":100,\"UnitTag\":\"\u9759\u5B89\u5BFA \u4E2D\u5C71\u516C\u56ED\",\"Distance\":\"1.2km\",\"EventList\":[{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"}]},{\"CustomerID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"UnitID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"UnitName\":\"\u77AC\u95F4\u8865\u6C34\u9759\u5B89\u5E97\",\"IsMyUnit\":1,\"CommentPoint\":\"9\",\"AvgPrice\":100,\"UnitTag\":\"\u9759\u5B89\u5BFA \u4E2D\u5C71\u516C\u56ED\",\"Distance\":\"1.2km\",\"EventList\":[{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"},{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"}]},{\"CustomerID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"UnitID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"UnitName\":\"\u77AC\u95F4\u8865\u6C34\u9759\u5B89\u5E97\",\"IsMyUnit\":1,\"CommentPoint\":\"9\",\"AvgPrice\":100,\"UnitTag\":\"\u9759\u5B89\u5BFA \u4E2D\u5C71\u516C\u56ED\",\"Distance\":\"1.2km\",\"EventList\":[{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"},{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"}]},{\"CustomerID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"UnitID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"UnitName\":\"\u77AC\u95F4\u8865\u6C34\u9759\u5B89\u5E97\",\"IsMyUnit\":1,\"CommentPoint\":\"9\",\"AvgPrice\":100,\"UnitTag\":\"\u9759\u5B89\u5BFA \u4E2D\u5C71\u516C\u56ED\",\"Distance\":\"1.2km\",\"EventList\":[{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"},{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"}]},{\"CustomerID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"UnitID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"UnitName\":\"\u77AC\u95F4\u8865\u6C34\u9759\u5B89\u5E97\",\"IsMyUnit\":1,\"CommentPoint\":\"9\",\"AvgPrice\":100,\"UnitTag\":\"\u9759\u5B89\u5BFA \u4E2D\u5C71\u516C\u56ED\",\"Distance\":\"1.2km\",\"EventList\":[{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"},{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"}]},{\"CustomerID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"UnitID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"UnitName\":\"\u77AC\u95F4\u8865\u6C34\u9759\u5B89\u5E97\",\"IsMyUnit\":1,\"CommentPoint\":\"9\",\"AvgPrice\":100,\"UnitTag\":\"\u9759\u5B89\u5BFA \u4E2D\u5C71\u516C\u56ED\",\"Distance\":\"1.2km\",\"EventList\":[{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"},{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"}]},{\"CustomerID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"UnitID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"UnitName\":\"\u77AC\u95F4\u8865\u6C34\u9759\u5B89\u5E97\",\"IsMyUnit\":1,\"CommentPoint\":\"9\",\"AvgPrice\":100,\"UnitTag\":\"\u9759\u5B89\u5BFA \u4E2D\u5C71\u516C\u56ED\",\"Distance\":\"1.2km\",\"EventList\":[{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"},{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"}]},{\"CustomerID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"UnitID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"UnitName\":\"\u77AC\u95F4\u8865\u6C34\u9759\u5B89\u5E97\",\"IsMyUnit\":1,\"CommentPoint\":\"9\",\"AvgPrice\":100,\"UnitTag\":\"\u9759\u5B89\u5BFA \u4E2D\u5C71\u516C\u56ED\",\"Distance\":\"1.2km\",\"EventList\":[{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"},{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"}]},{\"CustomerID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"UnitID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"UnitName\":\"\u77AC\u95F4\u8865\u6C34\u9759\u5B89\u5E97\",\"IsMyUnit\":1,\"CommentPoint\":\"9\",\"AvgPrice\":100,\"UnitTag\":\"\u9759\u5B89\u5BFA \u4E2D\u5C71\u516C\u56ED\",\"Distance\":\"1.2km\",\"EventList\":[{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"},{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"}]},{\"CustomerID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"UnitID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"UnitName\":\"\u77AC\u95F4\u8865\u6C34\u9759\u5B89\u5E97\",\"IsMyUnit\":1,\"CommentPoint\":\"9\",\"AvgPrice\":100,\"UnitTag\":\"\u9759\u5B89\u5BFA \u4E2D\u5C71\u516C\u56ED\",\"Distance\":\"1.2km\",\"EventList\":[{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"},{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"}]}],\"MyUnitList\":[{\"CustomerID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"UnitID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"UnitName\":\"\u77AC\u95F4\u8865\u6C34\u9759\u5B89\u5E97\",\"IsMyUnit\":1,\"CommentPoint\":\"9\",\"AvgPrice\":100,\"UnitTag\":\"\u9759\u5B89\u5BFA \u4E2D\u5C71\u516C\u56ED\",\"Distance\":\"1.2km\",\"EventList\":[{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"}]},{\"CustomerID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"UnitID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"UnitName\":\"\u77AC\u95F4\u8865\u6C34\u9759\u5B89\u5E97\",\"IsMyUnit\":1,\"CommentPoint\":\"9\",\"AvgPrice\":100,\"UnitTag\":\"\u9759\u5B89\u5BFA \u4E2D\u5C71\u516C\u56ED\",\"Distance\":\"1.2km\",\"EventList\":[{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"},{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"}]},{\"CustomerID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"UnitID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"UnitName\":\"\u77AC\u95F4\u8865\u6C34\u9759\u5B89\u5E97\",\"IsMyUnit\":1,\"CommentPoint\":\"9\",\"AvgPrice\":100,\"UnitTag\":\"\u9759\u5B89\u5BFA \u4E2D\u5C71\u516C\u56ED\",\"Distance\":\"1.2km\",\"EventList\":[{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"},{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"}]},{\"CustomerID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"UnitID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"UnitName\":\"\u77AC\u95F4\u8865\u6C34\u9759\u5B89\u5E97\",\"IsMyUnit\":1,\"CommentPoint\":\"9\",\"AvgPrice\":100,\"UnitTag\":\"\u9759\u5B89\u5BFA \u4E2D\u5C71\u516C\u56ED\",\"Distance\":\"1.2km\",\"EventList\":[{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"},{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"}]},{\"CustomerID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"UnitID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"UnitName\":\"\u77AC\u95F4\u8865\u6C34\u9759\u5B89\u5E97\",\"IsMyUnit\":1,\"CommentPoint\":\"9\",\"AvgPrice\":100,\"UnitTag\":\"\u9759\u5B89\u5BFA \u4E2D\u5C71\u516C\u56ED\",\"Distance\":\"1.2km\",\"EventList\":[{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"},{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"}]},{\"CustomerID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"UnitID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"UnitName\":\"\u77AC\u95F4\u8865\u6C34\u9759\u5B89\u5E97\",\"IsMyUnit\":1,\"CommentPoint\":\"9\",\"AvgPrice\":100,\"UnitTag\":\"\u9759\u5B89\u5BFA \u4E2D\u5C71\u516C\u56ED\",\"Distance\":\"1.2km\",\"EventList\":[{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"},{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"}]},{\"CustomerID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"UnitID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"UnitName\":\"\u77AC\u95F4\u8865\u6C34\u9759\u5B89\u5E97\",\"IsMyUnit\":1,\"CommentPoint\":\"9\",\"AvgPrice\":100,\"UnitTag\":\"\u9759\u5B89\u5BFA \u4E2D\u5C71\u516C\u56ED\",\"Distance\":\"1.2km\",\"EventList\":[{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"},{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"}]},{\"CustomerID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"UnitID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"UnitName\":\"\u77AC\u95F4\u8865\u6C34\u9759\u5B89\u5E97\",\"IsMyUnit\":1,\"CommentPoint\":\"9\",\"AvgPrice\":100,\"UnitTag\":\"\u9759\u5B89\u5BFA \u4E2D\u5C71\u516C\u56ED\",\"Distance\":\"1.2km\",\"EventList\":[{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"},{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"}]},{\"CustomerID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"UnitID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"UnitName\":\"\u77AC\u95F4\u8865\u6C34\u9759\u5B89\u5E97\",\"IsMyUnit\":1,\"CommentPoint\":\"9\",\"AvgPrice\":100,\"UnitTag\":\"\u9759\u5B89\u5BFA \u4E2D\u5C71\u516C\u56ED\",\"Distance\":\"1.2km\",\"EventList\":[{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"},{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"}]},{\"CustomerID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"UnitID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"UnitName\":\"\u77AC\u95F4\u8865\u6C34\u9759\u5B89\u5E97\",\"IsMyUnit\":1,\"CommentPoint\":\"9\",\"AvgPrice\":100,\"UnitTag\":\"\u9759\u5B89\u5BFA \u4E2D\u5C71\u516C\u56ED\",\"Distance\":\"1.2km\",\"EventList\":[{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"},{\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"}]}]}}";
        }
        /// <summary>
        /// 获取行业列表
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected string GetVocationList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<EmptyRequestParameter>>();
            var loggingSessionInfo = Default.GetBSLoggingSession(CUSTOMERID, "1");
            //var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var rd = new VocationListRD();
            var sysVocationBll = new SysVocationBLL(loggingSessionInfo);
            //查询参数
            //List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            //if (rp.Parameters.EventTypeId != 0)
            //    complexCondition.Add(new EqualsCondition() { FieldName = "EventTypeId", Value = rp.Parameters.EventTypeId });
            //排序参数
            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = "VocationID", Direction = OrderByDirections.Asc });
            //查询
            var tempVocation = sysVocationBll.GetVocationList(null, lstOrder.ToArray());

            List<VocationInfo> rdVocationList = new List<VocationInfo> { };
            VocationInfo rdVocation = null;
            List<VocationInfo> vocationList = null;
            VocationInfo vocation = null;

            foreach (var item in tempVocation.Where(t => t.ParentVocationID == 0))
            {
                rdVocation = new VocationInfo();
                rdVocation.VocationID = item.VocationID;
                rdVocation.VocationDesc = item.VocationDesc;
                vocationList = new List<VocationInfo> { };
                foreach (var sonItem in tempVocation.Where(t => t.ParentVocationID == item.VocationID))
                {
                    vocation = new VocationInfo();
                    vocation.VocationID = sonItem.VocationID;
                    vocation.VocationDesc = sonItem.VocationDesc;
                    vocationList.Add(vocation);
                }
                rdVocation.VocationList = vocationList.ToArray();
                rdVocationList.Add(rdVocation);
            }
            rd.VocationList = rdVocationList.ToArray();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
            //return "{\"ResultCode\":0,\"Message\":null,\"Data\":{\"VocationList\":[{\"VocationID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"VocationDesc\":\"\u9910\u996E\",\"VocationList\":[{\"VocationID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"VocationDesc\":\"\u4E0A\u6D77\u83DC\"},{\"VocationID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"VocationDesc\":\"\u7CA4\u83DC\"}]},{\"VocationID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"VocationDesc\":\"KTV\",\"VocationList\":[]},{\"VocationID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"VocationDesc\":\"\u4F11\u95F2\u517B\u751F\",\"VocationList\":[{\"VocationID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"VocationDesc\":\"\u8DB3\u7597\"},{\"VocationID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"VocationDesc\":\"spa\"}]}]}}";
        }
        /// <summary>
        /// 门店详情
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected string GetUnitDetail(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetUnitDetailRP>>();
            LoggingSessionInfo loggingSessionInfo = null;
            if (!string.IsNullOrEmpty(rp.UserID))
                loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID, rp.IsAToC);
            else
                loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            var rd = new UnitDetailRD();

            var unitBll = new UnitBLL(loggingSessionInfo);

            DataSet dsUnitDetail = unitBll.GetUnitDetail(loggingSessionInfo.UserID, rp.Parameters.UnitID);

            if (dsUnitDetail.Tables[0].Rows.Count > 0)
            {
                rd = DataTableToObject.ConvertToObject<UnitDetailRD>(dsUnitDetail.Tables[0].Rows[0]);
                if (dsUnitDetail.Tables[1].Rows.Count > 0)
                    rd.EventList = DataTableToObject.ConvertToList<EventInfo>(dsUnitDetail.Tables[1]);
                if (dsUnitDetail.Tables[2].Rows.Count > 0)
                    rd.ImageList = DataTableToObject.ConvertToList<ImageInfo>(dsUnitDetail.Tables[2]);

                #region 制造商户评论数据
                List<CustomerComment> commentList = new List<CustomerComment> { };
                CustomerComment customerComment = new CustomerComment();
                customerComment.CommentTitle = "口味";
                customerComment.CommentScore = "9.2";
                CustomerComment customerComment1 = new CustomerComment();
                customerComment1.CommentTitle = "环境";
                customerComment1.CommentScore = "8.5";
                CustomerComment customerComment2 = new CustomerComment();
                customerComment2.CommentTitle = "服务";
                customerComment2.CommentScore = "8.2";
                commentList.Add(customerComment);
                commentList.Add(customerComment1);
                commentList.Add(customerComment2);
                #endregion

                rd.CustomerComment = commentList;
            }
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
            //return "{\"ResultCode\":0,\"Message\":null,\"Data\":{\"UnitDetail\":{\"CustomerID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"UnitID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"UnitName\":\"\u77AC\u95F4\u8865\u6C34\u9759\u5B89\u5E97\",\"ImageCount\":10,\"UnitAddress\":\"\u4E0A\u6D77\u9759\u5B89\u5BFA\u9644\u8FD1\",\"AvgPrice\":100,\"Longitude\":\"121.449997\",\"Latitude\":\"31.23102\",\"UnitTel\":\"15856999999\",\"Integration\":100,\"CouponCount\":20,\"Amount\":\"150\",\"CustomerComment\":[{\"CommentTitle\":\"\u53E3\u5473\",\"CommentScore\":\"8.3\"},{\"CommentTitle\":\"\u73AF\u5883\",\"CommentScore\":\"8.3\"},{\"CommentTitle\":\"\u670D\u52A1\",\"CommentScore\":\"8.3\"}],\"ImageList\":[{\"ImageId\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\"},{\"ImageId\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\"},{\"ImageId\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\"}],\"EventList\":[{\"EventID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ItemID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":1,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"},{\"EventID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ItemID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":2,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"},{\"EventID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ItemID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"ItemName\":\"\u77AC\u95F4\u8865\u6C34\u54DF\",\"ImageUrl\":\"http://121.199.42.125:5001/images/20140530/20140530182026_2608.png\",\"ItemSortId\":3,\"Price\":\"180\",\"SoldPrice\":\"150\",\"Support\":\"\"}]}}}";
        }
        /// <summary>
        /// 商家优惠券
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected string GetCouponList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetMyInfoRP>>();
            rp.Parameters.Validate();//验证传值
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID, rp.IsAToC);
            var rd = new CouponRD();

            var unitBll = new UnitBLL(loggingSessionInfo);
            Hashtable htPara = new Hashtable();
            htPara["MemberID"] = loggingSessionInfo.UserID;
            htPara["CustomerID"] = rp.CustomerID;
            htPara["Status"] = rp.Parameters.Status;
            htPara["PageIndex"] = rp.Parameters.PageIndex + 1;
            htPara["PageSize"] = rp.Parameters.PageSize;
            DataSet dsCoupon = unitBll.GetCouponList(htPara);

            if (dsCoupon.Tables[0].Rows.Count > 0)
            {
                rd.Rule = "q/";
                rd.CouponList = DataTableToObject.ConvertToList<CouponInfo>(dsCoupon.Tables[0]);
                rd.Total = int.Parse(dsCoupon.Tables[0].Rows[0]["Total"].ToString());
                rd.TotalPageCount = int.Parse(dsCoupon.Tables[0].Rows[0]["PageCount"].ToString());
            }
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
            //return "{\"ResultCode\":0,\"Message\":null,\"Data\":{\"CouponList\":[{\"CouponCount\":1,\"CouponCode\":\"12365\",\"CouponPassword\":\"\",\"CouponDesc\":\"\u8BA2\u5355\u6EE1300\u5143\u53EF\u4F7F\u7528\u8BE5\u5238\",\"EndDate\":\"2014/05/31\",\"CouponTypeName\":\"\u62B5\u7528\u5238\",\"ParValue\":\"100\",\"Status\":0,\"Rule\":\"q/00848A1B9E284972AF84B13AA2A2AB78\"},{\"CouponCount\":1,\"CouponCode\":\"78256\",\"CouponPassword\":\"\",\"CouponDesc\":\"\u8BA2\u5355\u6EE1300\u5143\u53EF\u4F7F\u7528\u8BE5\u5238\",\"EndDate\":\"2014/05/31\",\"CouponTypeName\":\"\u4F18\u60E0\u5238\",\"ParValue\":\"100\",\"Status\":0,\"Rule\":\"q/00848A1B9E284972AF84B13AA2A2AB78\"},{\"UseType\":1,\"CouponCount\":1,\"CouponCode\":\"14796\",\"CouponPassword\":\"\",\"CouponDesc\":\"\u8BA2\u5355\u6EE1300\u5143\u53EF\u4F7F\u7528\u8BE5\u5238\",\"EndDate\":\"2014/05/31\",\"CouponTypeName\":\"\u62B5\u7528\u5238\",\"ParValue\":\"100\",\"Status\":0,\"Rule\":\"q/00848A1B9E284972AF84B13AA2A2AB78\"}]}}";
        }
        /// <summary>
        /// 使用优惠券
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected string UpdateCoupon(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetCouponDetailRP>>();
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            var couponBll=new CouponBLL(loggingSessionInfo);
            var couponInfo = couponBll.GetByID(rp.Parameters.CouponID);
            if (couponInfo != null)
            {
                couponInfo.Status = 1;      //置为已用；（0：未用，1：已使用，2：已过期，3：全部）
                couponBll.Update(couponInfo);   //修改

                #region 优惠券使用记录
                var couponUseBll = new CouponUseBLL(loggingSessionInfo);          //优惠券使用BLL实例化
                var vcmBll = new VipCouponMappingBLL(loggingSessionInfo);                //优惠券BLL实例化
                //var vcmEntity = new VipCouponMappingEntity();
                CouponBLL bll = new CouponBLL(loggingSessionInfo);
                List<IWhereCondition> wheresOrderNo = new List<IWhereCondition>();
                wheresOrderNo.Add(new EqualsCondition() { FieldName = "CouponID", Value = rp.Parameters.CouponID });
                var resultCouponVipID = vcmBll.Query(wheresOrderNo.ToArray(), null);
                List<IWhereCondition> wheresCouponUse = new List<IWhereCondition>();
                wheresCouponUse.Add(new EqualsCondition() { FieldName = "CouponID", Value = rp.Parameters.CouponID });
                var resultCouponUse = couponUseBll.Query(wheresCouponUse.ToArray(), null);
                foreach (var rcu in resultCouponUse)
                {
                    couponUseBll.Delete(rcu);
                }

                var couponUseEntity = new CouponUseEntity()
                {
                    CouponID = rp.Parameters.CouponID,
                    VipID = resultCouponVipID == null ? "" : resultCouponVipID[0].VIPID,
                    UnitID = rp.Parameters.UnitID,
                    //OrderID = orderEntity.OrderID.ToString(),
                    CreateBy = rp.UserID,
                    Comment = "核销电子券",
                    CustomerID = rp.CustomerID
                };
                couponUseBll.Create(couponUseEntity);//生成优惠券使用记录
                
                #endregion
            }
            var rd = new EmptyRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        /// <summary>
        /// 我的积分
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected string GetMyIntegral(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetMyInfoRP>>();
            rp.Parameters.Validate();//验证传值
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID, rp.IsAToC);
            var rd = new MyIntegralRD();

            var unitBll = new UnitBLL(loggingSessionInfo);
            Hashtable htPara = new Hashtable();
            htPara["MemberID"] = loggingSessionInfo.UserID;
            htPara["CustomerID"] = rp.CustomerID;
            htPara["PageIndex"] = rp.Parameters.PageIndex + 1;
            htPara["PageSize"] = rp.Parameters.PageSize;
            DataSet dsMyIntegral = unitBll.GetMyIntegral(htPara);

            if (dsMyIntegral.Tables[0].Rows.Count > 0)
            {
                rd.IntegralList = DataTableToObject.ConvertToList<MyIntegralInfo>(dsMyIntegral.Tables[0]);
                rd.Total = int.Parse(dsMyIntegral.Tables[0].Rows[0]["Total"].ToString());
                rd.TotalPageCount = int.Parse(dsMyIntegral.Tables[0].Rows[0]["PageCount"].ToString());
            }
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
            //return "{\"ResultCode\":200,\"Message\":null,\"Data\":{\"IntegralList\":[{\"UpdateReason\":\"\u63A8\u8350\u6709\u793C\",\"UpdateCount\":200,\"UpdateTime\":\"2014-2-6\"},{\"UpdateReason\":\"\u5151\u6362\u793C\u54C1\",\"UpdateCount\":-1000,\"UpdateTime\":\"2014-2-6\"},{\"UpdateReason\":\"\u63A8\u8350\u6709\u793C\",\"UpdateCount\":200,\"UpdateTime\":\"2014-2-6\"},{\"UpdateReason\":\"\u5151\u6362\u793C\u54C1\",\"UpdateCount\":-1000,\"UpdateTime\":\"2014-2-6\"},{\"UpdateReason\":\"\u63A8\u8350\u6709\u793C\",\"UpdateCount\":200,\"UpdateTime\":\"2014-2-6\"},{\"UpdateReason\":\"\u5151\u6362\u793C\u54C1\",\"UpdateCount\":-1000,\"UpdateTime\":\"2014-2-6\"}],\"TotalPageCount\":1}}";
        }
        /// <summary>
        /// 账户余额
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected string GetMyAccount(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetMyInfoRP>>();
            rp.Parameters.Validate();//验证传值
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID, rp.IsAToC);
            var rd = new MyAccountRD();

            var unitBll = new UnitBLL(loggingSessionInfo);
            Hashtable htPara = new Hashtable();
            htPara["MemberID"] = loggingSessionInfo.UserID;
            htPara["CustomerID"] = rp.CustomerID;
            htPara["PageIndex"] = rp.Parameters.PageIndex + 1;
            htPara["PageSize"] = rp.Parameters.PageSize;
            DataSet dsMyAccount = unitBll.GetMyAccount(htPara);

            if (dsMyAccount.Tables[0].Rows.Count > 0)
            {
                rd.AccountList = DataTableToObject.ConvertToList<AccountInfo>(dsMyAccount.Tables[0]);
                rd.Total = Convert.ToDecimal(dsMyAccount.Tables[0].Rows[0]["Total"].ToString());
                rd.TotalPageCount = int.Parse(dsMyAccount.Tables[0].Rows[0]["PageCount"].ToString());
            }
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
            //return "{\"ResultCode\":200,\"Message\":null,\"Data\":{\"AccountList\":[{\"UpdateReason\":\"\u8F6C\u8D26\",\"UpdateCount\":200,\"UpdateTime\":\"2014-2-6\"},{\"UpdateReason\":\"\u8F6C\u8D26\",\"UpdateCount\":-1000,\"UpdateTime\":\"2014-2-6\"},{\"UpdateReason\":\"\u8F6C\u8D26\",\"UpdateCount\":50,\"UpdateTime\":\"2014-2-6\"},{\"UpdateReason\":\"\u8F6C\u8D26\",\"UpdateCount\":-50,\"UpdateTime\":\"2014-2-6\"},{\"UpdateReason\":\"\u8F6C\u8D26\",\"UpdateCount\":200,\"UpdateTime\":\"2014-2-6\"},{\"UpdateReason\":\"\u8F6C\u8D26\",\"UpdateCount\":-50,\"UpdateTime\":\"2014-2-6\"}],\"TotalPageCount\":1}}";
        }
        /// <summary>
        /// 根据公司标识获取充值策略
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected string GetRechargeStrategy(string pRequest)
        {
            #region 原获取充值策略
            var rp = pRequest.DeserializeJSONTo<APIRequest<EmptyRequestParameter>>();
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            var rechargeBll = new RechargeStrategyBLL(loggingSessionInfo);
            var rd = new RechargeStrategyRD();

            //查询参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            if (!string.IsNullOrEmpty(rp.CustomerID))
                complexCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = rp.CustomerID });
            //排序参数
            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = "RechargeAmount", Direction = OrderByDirections.Asc });
            //查询
            RechargeStrategyEntity[] rechargeList = rechargeBll.Query(complexCondition.ToArray(), lstOrder.ToArray());
            if (rechargeList.Count() == 0)//根据customerID查询不到，就根据null查询
            {
                //查询参数
                List<IWhereCondition> complex = new List<IWhereCondition> { };
                complex.Add(new EqualsCondition() { FieldName = "CustomerID", Value = "" });
                rechargeList = rechargeBll.Query(complex.ToArray(), lstOrder.ToArray());
            }
            List<RechargeStrategyInfo> lstRecharge = new List<RechargeStrategyInfo> { };
            lstRecharge.AddRange(rechargeList.Select(t => new RechargeStrategyInfo()
            {
                RechargeStrategyId = t.RechargeStrategyId.ToString(),
                RechargeStrategyName = t.RechargeStrategyName,
                RechargeStrategyDesc = t.RechargeStrategyDesc,
                RechargeAmount = t.RechargeAmount,
                GiftAmount = t.GiftAmount,
                CustomerID = t.CustomerId
            }));
            rd.RechargeStrategyList = lstRecharge;
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
            #endregion 

            #region 获取充值类商品信息
            //var rp = pRequest.DeserializeJSONTo<APIRequest<EmptyRequestParameter>>();
            //var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
           
            //var rd = new RechargeStrategyRD();
            //var unitBll = new UnitBLL(loggingSessionInfo);
            //OnlineShoppingItemBLL itemService = new OnlineShoppingItemBLL(loggingSessionInfo);
            ////获取充值类商品信息
            //DataSet dsItem=  unitBll.GetItemBySortId(rp.CustomerID, 2);
            //if (dsItem.Tables[0].Rows.Count > 0)
            //{
            //    string itemId = dsItem.Tables[0].Rows[0]["ItemId"].ToString();
            //    var dsSkus = itemService.GetItemSkuList(itemId);
            //    List<RechargeStrategyInfo> lstRecharge = new List<RechargeStrategyInfo> { };
            //    RechargeStrategyInfo rechargeStrategy=null;
            //    foreach (DataRow row in dsSkus.Tables[0].Rows)
            //    {
            //        rechargeStrategy=new RechargeStrategyInfo ();
            //        rechargeStrategy.RechargeStrategyId = row["skuId"].ToString();
            //        rechargeStrategy.RechargeStrategyName = row["skuCode"].ToString();
            //        rechargeStrategy.RechargeStrategyDesc =string.Empty;
            //        rechargeStrategy.RechargeAmount =Convert.ToDouble(row["salesPrice"]).ToString("0.##");
            //        rechargeStrategy.GiftAmount =row["ReturnCash"] is DBNull?"0":Convert.ToDouble(row["ReturnCash"]).ToString("0.##");
            //        rechargeStrategy.CustomerID = rp.CustomerID;
            //        lstRecharge.Add(rechargeStrategy);
            //    }
            //    rd.RechargeStrategyList = lstRecharge;
            //    var rsp = new SuccessResponse<IAPIResponseData>(rd);
            //    return rsp.ToJSON();
            //}
            //else
            //{
            //    var rsp = new SuccessResponse<IAPIResponseData>();
            //    rsp.ResultCode = 0;
            //    rsp.Message = "没有配置充值类商品";
            //    return rsp.ToJSON();
            //}
            #endregion

            //return "{\"ResultCode\":0,\"Message\":null,\"Data\":{\"RechargeStrategyList\":[{\"RechargeStrategyId\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"RechargeStrategyName\":\"\u5145100\u5143\u900120\u5143\",\"RechargeStrategyDesc\":\"\u5145100\u5143\u900120\u5143\u54DF\",\"RechargeAmount\":\"100\",\"GiftAmount\":\"20\",\"CustomerID\":\"3906051a805c4192938d4fa307cb3b4e\"},{\"RechargeStrategyId\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"RechargeStrategyName\":\"\u5145200\u5143\u900150\u5143\",\"RechargeStrategyDesc\":\"\u5145200\u5143\u900150\u5143\u54DF\",\"RechargeAmount\":\"200\",\"GiftAmount\":\"50\",\"CustomerID\":\"3906051a805c4192938d4fa307cb3b4e\"},{\"RechargeStrategyId\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"RechargeStrategyName\":\"\u5145500\u5143\u9001200\u5143\",\"RechargeStrategyDesc\":\"\u5145500\u5143\u9001200\u5143\u54DF\",\"RechargeAmount\":\"500\",\"GiftAmount\":\"200\",\"CustomerID\":\"3906051a805c4192938d4fa307cb3b4e\"}]}}";
        }
        /// <summary>
        /// 获取充值卡信息(主体方法复用后台，返回对象重新封装了)
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected string GetCardInfo(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetCardRP>>();
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            var cardDepositBLL = new CardDepositBLL(loggingSessionInfo);

            var rd = new GetCardInfoRD();

            rp.Parameters.Validate();

            var ds = cardDepositBLL.PagedSearch(rp.Parameters, loggingSessionInfo.ClientID);

            Card cardInfo = null;
            if (ds.Tables.Count == 2 && ds.Tables[0].Rows.Count > 0)
            {
                cardInfo = new Card();
                cardInfo.CardID = ds.Tables[0].Rows[0]["CardDepositId"].ToString();
                cardInfo.CardNo = ds.Tables[0].Rows[0]["CardNo"].ToString();
                cardInfo.ChannelTitle = ds.Tables[0].Rows[0]["ChannelTitle"].ToString();
                cardInfo.Amount = decimal.Parse(ds.Tables[0].Rows[0]["Amount"].ToString());
                cardInfo.CardStatus = int.Parse(ds.Tables[0].Rows[0]["CardStatus"].ToString());
                cardInfo.UseStatus = int.Parse(ds.Tables[0].Rows[0]["UseStatus"].ToString());
            }
            rd.CardInfo = cardInfo;
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
            //return "{\"ResultCode\":0,\"Message\":null,\"Data\":{\"CardInfo\":{\"CardID\":\"DC7FD6AB116A4A42822B6C9FD35B6D9B\",\"CardNo\":\"25006\",\"Amount\":\"100\",\"CardStatus\":1,\"UseStatus\":0}}}";
        }
        /// <summary>
        /// 充值卡充值（复用后台方法）
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected string ActiveCard(string pRequest)
        {

            var rp = pRequest.DeserializeJSONTo<APIRequest<ActiveCardRP>>();
            rp.Parameters.Validate();
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.Parameters.VipID, rp.IsAToC);
            rp.Parameters.VipID = loggingSessionInfo.UserID;//获取商户VipId重新赋值
            var cardDepositBLL = new CardDepositBLL(loggingSessionInfo);
            var rd = new EmptyRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            int result = cardDepositBLL.ActiveCard(rp);

            if (result >= 1)
            {
                rsp.ResultCode = 0;
                rsp.Message = result.ToString();
            }
            else
            {
                rsp.ResultCode = 300;
                rsp.Message = "密码错误或卡已被使用!";
            }
            return rsp.ToJSON();
            //return "{\"ResultCode\":0,\"Message\":null,\"Data\":{}}";
        }
        /// <summary>
        /// 获取优惠券二维码
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected string GetCouponDetail(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetCouponDetailRP>>();
            rp.Parameters.Validate();//验证传值
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            var rd = new GetCouponDetailRD();

            string weixinDomain = ConfigurationManager.AppSettings["original_url"];
            string sourcePath = System.Web.HttpContext.Current.Server.MapPath("/ApplicationInterface/Services/QRCodeImage/qrcode.jpg");
            string targetPath = System.Web.HttpContext.Current.Server.MapPath("/qrcode_image/");
            string currentDomain = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port;
            string rule = rp.Parameters.Rule + rp.Parameters.CouponID; //生成规则
            string imageURL;

            ObjectImagesBLL objectImagesBLL = new ObjectImagesBLL(loggingSessionInfo);
            //查找是否已经生成了二维码
            ObjectImagesEntity[] objectImagesEntityArray = objectImagesBLL.QueryByEntity(new ObjectImagesEntity() { ObjectId = rp.Parameters.CouponID, Description = "自动生成的产品二维码" }, null);

            if (objectImagesEntityArray.Length == 0)
            {
                imageURL = Utils.GenerateQRCode(weixinDomain + rule, currentDomain, sourcePath, targetPath);
                //把下载下来的图片的地址存到ObjectImages
                objectImagesBLL.Create(new ObjectImagesEntity()
                {
                    ImageId = Utils.NewGuid(),
                    CustomerId = rp.CustomerID,
                    ImageURL = imageURL,
                    ObjectId = rp.Parameters.CouponID,
                    Title = "优惠券二维码",
                    Description = "自动生成的产品二维码"
                });

                Loggers.Debug(new DebugLogInfo() { Message = "二维码已生成，imageURL:" + imageURL });
            }
            else
            {
                imageURL = objectImagesEntityArray[0].ImageURL;
            }
            string imagePath = targetPath + imageURL.Substring(imageURL.LastIndexOf("/"));
            Loggers.Debug(new DebugLogInfo() { Message = "二维码路径，imagePath:" + imageURL });

            rd.ImageUrl = imageURL;
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        #endregion
    }
    #region 请求/返回参数 add by Henry 2014-8-18


    /// <summary>
    /// 商家门店列表请求参数
    /// </summary>
    public class GetUnitListRP : IAPIRequestParameter
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public int Distance { get; set; }
        /// <summary>
        /// 排序字段（距离：Distance评分：CommentPoint人均：AvgPrice）
        /// </summary>
        public string SortField { get; set; }
        /// <summary>
        /// 排序类别（ASC=升序；DESC=降序）
        /// </summary>
        public string SortType { get; set; }
        public string IndustryID { get; set; }
        /// <summary>
        /// 是否取我的商家信息 0=不是；1=是
        /// </summary>
        public int IsMyUnit { get; set; }
        /// <summary>
        /// 门店名称，可模糊查询 add by Henry 2014-10-21 
        /// </summary>
        public string UnitName { get; set; }

        public void Validate()
        {
        }
    }
    /// <summary>
    /// 商家门店列表
    /// </summary>
    public class UnitListRD : IAPIResponseData
    {
        public UnitInfo[] UnitList { get; set; }
        public int TotalPageCount { get; set; }
    }
    /// <summary>
    /// 附近商家和我的商家
    /// </summary>
    public class GetNearbyAndMyUnitRD : IAPIResponseData
    {
        public UnitInfo[] NearbyUnitList { get; set; }
        public UnitInfo[] MyUnitList { get; set; }

        public string sqlTime { get; set; }
        public string netTime { get; set; }

    }
    /// <summary>
    /// 商家门店信息
    /// </summary>
    public class UnitInfo
    {
        public string CustomerID { get; set; }
        public string UnitID { get; set; }
        public string ImageUrl { get; set; }
        public string UnitName { get; set; }
        public int IsMyUnit { get; set; }
        public decimal CommentPoint { get; set; }
        public int AvgPrice { get; set; }
        public string UnitTag { get; set; }
        public string Distance { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public EventInfo[] EventList { get; set; }
    }
    /// <summary>
    /// 门店活动信息
    /// </summary>
    public class EventInfo
    {
        public Guid EventID { get; set; }
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public string ImageUrl { get; set; }
        /// <summary>
        /// 商品业务分类标识(1=团；2=充；3=券)
        /// </summary>
        public int ItemSortId { get; set; }
        public decimal Price { get; set; }
        public decimal SoldPrice { get; set; }
        /// <summary>
        ///预留值:支持范围(部分门店、所有门店) 
        /// </summary>
        public string Support { get; set; }

    }


    /// <summary>
    /// 行业返回参数
    /// </summary>
    public class VocationListRD : IAPIResponseData
    {
        public VocationInfo[] VocationList { get; set; }
    }
    /// <summary>
    /// 行业对象
    /// </summary>
    public class VocationInfo
    {
        public int? VocationID { get; set; }
        public string VocationDesc { get; set; }
        public VocationInfo[] VocationList { get; set; }
    }
    /// <summary>
    /// 门店详情请求参数
    /// </summary>
    public class GetUnitDetailRP : IAPIRequestParameter
    {
        public string UnitID { get; set; }
        public void Validate()
        {
        }
    }

    public class UnitDetailRD : IAPIResponseData
    {
        public string CustomerID { get; set; }
        public string UnitID { get; set; }
        public string UnitName { get; set; }
        public string ImageUrl { get; set; }
        public string UnitAddress { get; set; }
        public int AvgPrice { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public string UnitTel { get; set; }
        public int Integration { get; set; }
        public int CouponCount { get; set; }
        public double Amount { get; set; }
        public List<CustomerComment> CustomerComment { get; set; }
        public List<ImageInfo> ImageList { get; set; }
        public List<EventInfo> EventList { get; set; }

    }
    /// <summary>
    /// 商户评论
    /// </summary>
    public class CustomerComment
    {
        public string CommentTitle { get; set; }
        public string CommentScore { get; set; }
    }
    /// <summary>
    /// 图片ID
    /// </summary>
    public class ImageInfo
    {
        public string ImageId { get; set; }
        public string ImageUrl { get; set; }
    }
    /// <summary>
    /// 我的积分、账户余额、优惠券通用请求参数
    /// </summary>
    public class GetMyInfoRP : IAPIRequestParameter
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        /// <summary>
        /// 指定优惠券状态（0：未用，1：已使用，2：已过期）[非比填]
        /// </summary>
        public int Status { get; set; }
        public void Validate()
        {
            //if (string.IsNullOrEmpty(MemberID))
            //    throw new APIException(201, "会员ID不能为空！");
            //if (string.IsNullOrEmpty(UnitID))
            //    throw new APIException(201, "门店ID不能为空！");
        }
    }
    /// <summary>
    /// 我的积分返回对象
    /// </summary>
    public class MyIntegralRD : IAPIResponseData
    {
        public List<MyIntegralInfo> IntegralList { get; set; }
        public int Total { get; set; }
        public int TotalPageCount { get; set; }
    }
    /// <summary>
    /// 我的积分
    /// </summary>
    public class MyIntegralInfo
    {
        public string UpdateReason { get; set; }
        public int UpdateCount { get; set; }
        public string UpdateTime { get; set; }
    }

    /// <summary>
    /// 账户余额返回对象
    /// </summary>
    public class MyAccountRD : IAPIResponseData
    {
        public List<AccountInfo> AccountList { get; set; }
        public decimal Total { get; set; }
        public int TotalPageCount { get; set; }
    }
    /// <summary>
    /// 获取优惠券详情参数
    /// </summary>
    public class GetCouponDetailRP : IAPIRequestParameter
    {
        public string CouponID { get; set; }
        public string UnitID { get; set; }
        public string Rule { get; set; }
        public void Validate()
        {
        }
    }
    public class GetCouponDetailRD : IAPIResponseData
    {
        public string ImageUrl { get; set; }
    }
    /// <summary>
    /// 账户余额
    /// </summary>
    public class AccountInfo
    {
        public string UpdateReason { get; set; }
        public decimal UpdateCount { get; set; }
        public string UpdateTime { get; set; }
    }
    /// <summary>
    /// 优惠券返回对象
    /// </summary>
    public class CouponRD : IAPIResponseData
    {
        public List<CouponInfo> CouponList { get; set; }
        public int TotalPageCount { get; set; }
        public int Total { get; set; }
        public string Rule { get; set; }
    }
    /// <summary>
    /// 优惠券对象
    /// </summary>
    public class CouponInfo
    {
        public string CouponID { get; set; }
        public int CouponCount { get; set; }
        public string CouponCode { get; set; }
        public string CouponPassword { get; set; }
        public string CouponDesc { get; set; }
        public string EndDate { get; set; }
        public string CouponTypeName { get; set; }
        public decimal ParValue { get; set; }
        public int Status { get; set; }
    }
    /// <summary>
    /// 充值策略返回对象
    /// </summary>
    public class RechargeStrategyRD : IAPIResponseData
    {
        public List<RechargeStrategyInfo> RechargeStrategyList { get; set; }
    }
    /// <summary>
    /// 充值策略对象
    /// </summary>
    public class RechargeStrategyInfo
    {
        public string RechargeStrategyId { get; set; }
        public string RechargeStrategyName { get; set; }
        public string RechargeStrategyDesc { get; set; }
        public decimal RechargeAmount { get; set; }
        public decimal? GiftAmount { get; set; }
        public string CustomerID { get; set; }
    }
    /// <summary>
    /// 获取充值卡信息
    /// </summary>
    public class GetCardInfoRD : IAPIResponseData
    {
        public Card CardInfo { get; set; }
    }

    #endregion
}