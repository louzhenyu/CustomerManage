/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/27 10:35:51
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
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using System.Web;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class GOrderBLL
    {
        #region 获取地图上的所有节点
        public MapEntity GetMapNodeInfo()
        {
            MapEntity mapInfo = new MapEntity();
            IList<MapEntity> mapInfoList = new List<MapEntity>();
            DataSet ds = _currentDAO.GetMapNodeInfo();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                mapInfoList = DataTableToObject.ConvertToList<MapEntity>(ds.Tables[0]);
                foreach (var map in mapInfoList)
                {
                    PopInfo popInfo = new PopInfo();
                    switch (map.NodeTypeId)
                    { 
                        case "1":
                            popInfo.type = 1;
                            popInfo.title = "Tips";
                            popInfo.value = "门店";
                            break;
                        case "2":
                            popInfo.type = 1;
                            popInfo.title = "Tips";
                            popInfo.value = "门店";
                            break;
                        case "3":
                            popInfo.type = 4;
                            popInfo.title = "Tips";
                            popInfo.value = "http://bs.aladingyidong.com:";
                            break;
                        default:
                            break;
                    }
                    IList<PopInfo> popListInfo = new List<PopInfo>();
                    popListInfo.Add(popInfo);
                    map.PopInfo = popListInfo;
                }
            }

            mapInfo.MapList = mapInfoList;
            return mapInfo;
        }
        #endregion

        #region 获取订单各类型数量
        public GOrderEntity GetOrderStatusCount()
        {
            GOrderEntity orderInfo = new GOrderEntity();
            orderInfo.status1Count = 0;
            orderInfo.status2Count = 0;
            orderInfo.status3Count = 0;
            orderInfo.TotalAmount = 0;
            DataSet ds = new DataSet();
            ds = _currentDAO.GetOrderStatusCount();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    switch (dr["status"].ToString())
                    { 
                        case "1":
                            orderInfo.status1Count = Convert.ToInt32(dr["icount"].ToString());
                            break;
                        case "2":
                            orderInfo.status2Count += Convert.ToInt32(dr["icount"].ToString());
                            break;
                        case "3":
                            orderInfo.status2Count += Convert.ToInt32(dr["icount"].ToString());
                            break;
                        case "4":
                            int qty = Convert.ToInt32(dr["qty"].ToString());
                            orderInfo.TotalAmount = qty * 12; 
                            break;
                        default:
                            break;
                    }
                }
            }
            return orderInfo;
        }
        #endregion

        #region 微信推送
        /// <summary>
        /// 客户下完订单之后，系统处理微信推送
        /// </summary>
        /// <param name="OrderId">订单标识</param>
        /// <param name="msgUrl">微信推送地址</param>
        /// <param name="distance">距离</param>
        /// <param name="strError">错误提示</param>
        /// <returns></returns>
        public bool SetPushOrder(string OrderId, string msgUrl,float distance, out string strError)
        {
            string content = string.Empty;
            try
            {
                DataSet ds = new DataSet();
                IList<VipEntity> vipListInfo = new List<VipEntity>();
                ds = _currentDAO.GetFieldwork(distance, OrderId);
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    vipListInfo = DataTableToObject.ConvertToList<VipEntity>(ds.Tables[0]);
                    foreach (VipEntity vipInfo in vipListInfo)
                    {
                        var orderInfo = GetByID(OrderId);
                        //string msgUrl = ConfigurationManager.AppSettings["push_weixin_msg_url"].Trim();
                        //string msgText = string.Format("信息提示用户。{0}分钟内，工号为{1}的员工将上门收取衣物，该员工的手机号为{2}",
                        //   "5", vipInfo.VipCode, vipInfo.Phone);
                        Random rad = new Random();
                        //string msgText = "您有一个订单，<a href='http://o2oapi.aladingyidong.com/wap/xiyi/sureOrder.html?rid="+rad.Next(1000,100000)+"&customerId=" + this.CurrentUserInfo..Customer_Id + "&orderId=" + OrderId + "&userId=" + vipInfo.VIPID + "&openId=" + vipInfo.WeiXinUserId + "&address=" + HttpUtility.UrlEncode(orderInfo.Address) + "&phone=" + orderInfo.Phone + "&Cnumber=" + orderInfo.Qty + "&status="+orderInfo.Status+"'>点击确认订单.</a>";
                        string msgText = "您有一个订单，<a href='http://o2oapi.aladingyidong.com/wap/xiyi/sureOrder.html?rid=" + rad.Next(1000, 100000) + "&customerId=" + this.CurrentUserInfo.CurrentUser.customer_id + "&orderId=" + OrderId + "&userId=" + vipInfo.VIPID + "&openId=" + vipInfo.WeiXinUserId + "'>点击确认订单.</a>";
                        string msgData = "<xml><OpenID><![CDATA[" + vipInfo.WeiXinUserId + "]]></OpenID><Content><![CDATA[" + msgText + "]]></Content></xml>";
                   
                        var msgResult = Common.Utils.GetRemoteData(msgUrl, "POST", msgData);
                        #region 发送日志
                        MarketSendLogBLL logServer = new MarketSendLogBLL(CurrentUserInfo);
                        MarketSendLogEntity logInfo = new MarketSendLogEntity();
                        logInfo.LogId = BaseService.NewGuidPub();
                        logInfo.IsSuccess = 1;
                        logInfo.MarketEventId = OrderId;
                        logInfo.SendTypeId = "2";
                        logInfo.TemplateContent = msgData;
                        logInfo.VipId = vipInfo.VIPID;
                        logInfo.WeiXinUserId = vipInfo.WeiXinUserId;
                        logInfo.CreateTime = System.DateTime.Now;
                        logServer.Create(logInfo);
                        #endregion
                    }
                }
                strError = "成功";
                return true;
            }
            catch (Exception ex) {

                strError = ex.ToString();
                return false;
            }
            
        }
        #endregion

        #region 更新订单状态
        public bool SetGOrderStatus(string orderId,  out string strError)
        {
            try
            {
                string nextStatusDesc = string.Empty;
                GOrderEntity orderInfo = new GOrderEntity();
                orderInfo = _currentDAO.GetByID(orderId);
                if (orderInfo != null && !orderInfo.OrderId.Equals(""))
                {
                    switch (orderInfo.Status)
                    { 
                        case "1":
                            orderInfo.Status = "2";
                            orderInfo.StatusDesc = "1公里之内已推送";
                            orderInfo.FirstPushTime = System.DateTime.Now;
                            break;
                        case "2":
                            orderInfo.Status = "3";
                            orderInfo.StatusDesc = "2公里之内已推送";
                            orderInfo.SecondPushTime = System.DateTime.Now;
                            break;
                        default:
                            break;
                    }
                    Update(orderInfo,false);
                    strError = "更新状态成功";
                    return true;
                }
                else
                {
                    strError = "更新状态，没找到匹配的订单";
                    return false;
                }
            }
            catch (Exception ex) {
                strError = ex.ToString();
                return false;
            }
        }
        #endregion

        #region 获取所有要推送的订单
        public bool SetGOrderPushAll(string msgUrl,out string strError)
        {
            try
            {
                bool bReturn = true;
                GOrderEntity orderQuery = new GOrderEntity();
                #region 处理2公里的
                orderQuery.Status = "2";
                var orderList2 = _currentDAO.QueryByEntity(orderQuery, null);
                if (orderList2 != null && orderList2.Length > 0 )
                {
                    foreach (var orderInfo in orderList2)
                    {
                        bReturn = SetPushOrder(orderInfo.OrderId, msgUrl, 10.0f, out  strError);
                        if (bReturn)
                        {
                            bReturn = SetGOrderStatus(orderInfo.OrderId, out strError);
                            if (!bReturn) { break; }
                        }
                        else {
                            break;
                        }
                    }
                }
                #endregion
                #region 处理1公里的
                orderQuery.Status = "1";
                var orderList1 = _currentDAO.QueryByEntity(orderQuery, null);
                if (orderList1 != null && orderList1.Length > 0)
                {
                    foreach (var orderInfo in orderList1)
                    {
                        bReturn = SetPushOrder(orderInfo.OrderId, msgUrl, 5.0f, out  strError);
                        if (bReturn)
                        {
                            bReturn = SetGOrderStatus(orderInfo.OrderId, out strError);
                            if (!bReturn) { break; }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                #endregion
                strError = "推送成功";
                return true;
            }
            catch (Exception ex) {
                strError = ex.ToString();
                return false;
            }
        }
        #endregion

        #region 确认收单，并且推送消息告知客户
        /// <summary>
        /// 确认收单，并且推送消息告知客户
        /// </summary>
        /// <param name="orderId">确认单据</param>
        /// <param name="userId">收单确认人</param>
        /// <returns></returns>
        public bool GetReceiptConfirm(string orderId, string userId, string msgUrl, out string strError)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = _currentDAO.GetReceiptConfirm(orderId, userId);
                VipEntity vipInfo = new VipEntity();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    vipInfo = DataTableToObject.ConvertToObject<VipEntity>(ds.Tables[0].Rows[0]);
                    string msgText = string.Format("信息提示用户。{0}分钟内，工号为{1}的员工将上门收取衣物，该员工的手机号为{2}", "30", vipInfo.VipCode, vipInfo.Phone);
                    string msgData = "<xml><OpenID><![CDATA[" + vipInfo.WeiXinUserId + "]]></OpenID><Content><![CDATA[" + msgText + "]]></Content></xml>";

                    var msgResult = Common.Utils.GetRemoteData(msgUrl, "POST", msgData);
                    #region 发送日志
                    MarketSendLogBLL logServer = new MarketSendLogBLL(CurrentUserInfo);
                    MarketSendLogEntity logInfo = new MarketSendLogEntity();
                    logInfo.LogId = BaseService.NewGuidPub();
                    logInfo.IsSuccess = 1;
                    logInfo.MarketEventId = orderId;
                    logInfo.SendTypeId = "2";
                    logInfo.TemplateContent = msgData;
                    logInfo.VipId = vipInfo.VIPID;
                    logInfo.WeiXinUserId = vipInfo.WeiXinUserId;
                    logInfo.CreateTime = System.DateTime.Now;
                    logServer.Create(logInfo);
                    #endregion
                }
                strError = "";
                return true;
            }
            catch (Exception ex) {
                strError = ex.ToString();
                return false;
            }
        }
        #endregion

        #region 获取所有收货人员信息
        public IList<GOrderEntity> GetReceiptByOrderId(string OrderId)
        {
            IList<GOrderEntity> orderInfoList = new List<GOrderEntity>();
            DataSet ds = _currentDAO.GetReceiptByOrderId(OrderId);
              if (ds != null && ds.Tables[0].Rows.Count > 0)
              {
                  orderInfoList = DataTableToObject.ConvertToList<GOrderEntity>(ds.Tables[0]);
              }
            return orderInfoList;
        }

        #endregion

        #region 获取订单号码
        /// <summary>
        /// 获取干洗衣服的订单号
        /// </summary>
        /// <returns></returns>
        public string GetGOrderCode()
        {
            string orderCode = string.Empty;
            int icount = 0;
            icount = _currentDAO.GetGOrderCode();
            switch (icount.ToString().Length) { 
                case 1:
                    orderCode = "DO000" + icount.ToString();
                    break;
                case 2:
                    orderCode = "DO00" + icount.ToString();
                    break;
                case 3:
                    orderCode = "DO0" + icount.ToString();
                    break;
                case 4:
                    orderCode = "DO" + icount.ToString();
                    break;
                default:
                    orderCode = "DO" + icount.ToString();
                    break;
            }
            return orderCode;
        }
        #endregion

        #region 获取订单送货人员列表
        /// <summary>
        /// 获取订单送货人员列表
        /// </summary>
        public IList<VipEntity> GetSendUserListByOrderId(string OrderId, float distance)
        {
            DataSet ds = new DataSet();
            IList<VipEntity> vipListInfo = null;
            ds = _currentDAO.GetFieldwork2(distance, OrderId);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                vipListInfo = DataTableToObject.ConvertToList<VipEntity>(ds.Tables[0]);
            }
            var ds2 = _currentDAO.GetFieldwork4(distance, OrderId);
            if (ds2 != null && ds2.Tables[0] != null && ds2.Tables[0].Rows.Count > 0)
            {
                vipListInfo = DataTableToObject.ConvertToList<VipEntity>(ds2.Tables[0]);
            }
            return vipListInfo;
        }
        #endregion
    }
}