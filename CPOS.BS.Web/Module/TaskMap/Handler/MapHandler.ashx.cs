using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.Extension;
using JIT.CPOS.Common;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Reflection;
using JIT.Utility.Web;
using JIT.CPOS.BS.Entity.User;
using JIT.CPOS.BS.Entity.Pos;
using System.Collections;
using System.Text;
using System.Configuration;

namespace JIT.CPOS.BS.Web.Module.TaskMap.Handler
{
    /// <summary>
    /// MapHandler 的摘要说明
    /// </summary>
    public class MapHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = "";
            switch (pContext.Request.QueryString["method"])
            {
                case "GetMapList":
                    content = GetMapList();
                    break;
                case "GetOrder":
                    content = GetOrder();
                    break;
                case "GetOrderTable":
                    content = GetOrderTable();
                    break;
                case "SendOrder":
                    content = SendOrder();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetMapNode 动态获取节点信息
        public MapEntity GetMapNode()
        {
            string content = string.Empty;
            GOrderBLL orderServer = new GOrderBLL(CurrentUserInfo);
            MapEntity mapInfo = new MapEntity();
            mapInfo = orderServer.GetMapNodeInfo();
            //content = mapInfo.ToJSON();
            return mapInfo;
        }
        #endregion

        #region 获取各种状态的单据数量
        public GOrderEntity GetOrderStatusCount()
        {
            string content = string.Empty;
            GOrderBLL orderServer = new GOrderBLL(CurrentUserInfo);
            GOrderEntity orderInfo = new GOrderEntity();
            orderInfo = orderServer.GetOrderStatusCount();
            //content = "{ \"num1\":\"" + orderInfo.status1Count + "\", \"num2\":\"" + orderInfo.status2Count + "\", \"num3\":\"" + orderInfo.status3Count + "\",\"data\":" + content + ",  }";
            return orderInfo;
        }
        #endregion

        #region GetMapList
        /// <summary>
        /// 查询
        /// </summary>
        public string GetMapList()
        {
            string content = string.Empty;
            var type = FormatParamValue(Request("type"));
            var name = FormatParamValue(Request("name"));
            GOrderBLL gOrderBLL = new GOrderBLL(CurrentUserInfo);

            var list = new List<MapEntity>();
            list.Add(new MapEntity()
            {
                StoreID = "1",
                Lng = "121.44115",
                Lat = "31.22852",
                //Icon = "o.png",
                Icon = "",
                LabelID = "",
                Menu = "0",
                LabelContent = "",
                Tips = "",
                IsAssigned = "false",
                IsEdit = "false"
            });

            var mapN = GetMapNode();
            var mapNodeList = mapN.MapList;
            var orderCountInfo = GetOrderStatusCount();
            if (mapNodeList != null)
            {
                foreach (var mapNode in mapNodeList)
                {
                    switch (mapNode.NodeTypeId)
                    {
                        case "1": // 门店
                            list.Add(mapNode);
                            mapNode.Tips = "";
                            mapNode.PopInfo = null;
                            break;
                        case "2": // 会员
                            list.Add(mapNode);
                            mapNode.Tips = "";
                            mapNode.PopInfo = null;
                            break;
                        case "3": // 订单
                            mapNode.Tips = "";
                            var popInfoList = new List<PopInfo>();
                            var popInfo = new PopInfo();
                            popInfo.type = 4;
                            //popInfo.title = "";
                            popInfo.value = "MapTaskInfo.aspx?id=" + mapNode.StoreID;
                            //mapNode.PopInfoHeight = "250";
                            //mapNode.PopInfoWidth = "500";
                            mapNode.PopInfoHeight = "160";
                            mapNode.PopInfoWidth = "200";
                            popInfoList.Add(popInfo);
                            mapNode.PopInfo = popInfoList;
                            //mapNode.SendUserList = gOrderBLL.GetSendUserListByOrderId(mapNode.StoreID, 1);

                            var userList = gOrderBLL.GetSendUserListByOrderId(mapNode.StoreID, 1);
                            if (userList != null)
                            {
                                foreach (var userItem in userList)
                                {
                                    mapNode.SendUserList += userItem.VIPID + ",";
                                }
                                if (mapNode.SendUserList.EndsWith(","))
                                    mapNode.SendUserList = mapNode.SendUserList.Substring(0, mapNode.SendUserList.Length - 1);
                            }

                            //mapNode.SendUserList2 = gOrderBLL.GetSendUserListByOrderId(mapNode.StoreID, 200);
                            list.Add(mapNode);
                            break;
                    }
                }
            }
            content = list.ToJSON();

            var num1 = orderCountInfo.status1Count;
            var num2 = orderCountInfo.status2Count;
            var num3 = orderCountInfo.status3Count;
            var amount = orderCountInfo.TotalAmount;
            content = "{ \"num1\":\"" + num1 + "\", \"num2\":\"" + num2 + "\", \"num3\":\"" + num3 + "\", \"amount\":\"" + amount + "\", \"data\":" + content + "  }";
            return content;
        }

        private string GetMapInfo()
        {
            string content = string.Empty;
            content = "[{\"StoreID\":\"1\",\"LabelID\":\"1\",\"Menu\":\"0\",\"LabelContent\":\"123232323\",\"Lng\":\"121.5209\",\"Lat\":\"31.2103\",\"Icon\":\"g.png\",\"IsAssigned\":\"false\",\"IsEdit\":\"true\",\"Tips\":\"这是一个测试点位1\",\"PopInfoWidth\":\"200\",\"PopInfoHeight\":\"\",\"PopInfo\":[{\"type\":\"1\",\"title\":\"客户名称\",\"value\":\"坂田医院\"},{\"type\":\"2\",\"title\":\"图片\",\"value\":\"http://www.mobanwang.com/icon/UploadFiles_8971/200806/20080602162129496.png\"},{\"type\":\"3\",\"title\":\"按钮1\",\"value\":\"test1\"}]}"
                    + ",{\"StoreID\":\"2\",\"LabelID\":\"2\",\"LabelContent\":\"12\",\"Menu\":\"2\",\"Lng\":\"121.4309\",\"Lat\":\"31.231\",\"Icon\":\"o.png\",\"IsAssigned\":\"true\",\"IsEdit\":\"false\",\"Tips\":\"这是一个测试点位2\",\"PopInfoWidth\":\"200\",\"PopInfo\":[{\"type\":\"1\",\"tilte\":\"客户名称\",\"value\":\"坂田医院\"}]}"
                    + ",{\"StoreID\":\"3\",\"LabelID\":\"3\",\"LabelContent\":\"others\",\"Lng\":\"121.4330\",\"Lat\":\"31.3\",\"Icon\":\"o.png\",\"IsAssigned\":\"true\",\"IsEdit\":\"false\",\"Tips\":\"这是一个测试点位2\",\"PopInfo\":[{\"type\":\"1\",\"tilte\":\"客户名称\",\"value\":\"坂田医院\"}]}"
                    + ",{\"StoreID\":\"5\",\"LabelID\":\"5\",\"Lng\":\"121.4\",\"Lat\":\"31.2\",\"Icon\":\"o.png\",\"IsAssigned\":\"true\",\"IsEdit\":\"false\",\"Tips\":\"frametest\",\"PopInfoWidth\":\"200\",\"PopInfoHeight\":\"200\",\"PopInfo\":[{\"type\":\"4\",\"tilte\":\"frame\",\"value\":\"http://www.baidu.com\"}]}"
                    + ",{\"StoreID\":\"4\",\"LabelID\":\"4\",\"Lng\":\"121.3\",\"Lat\":\"31.2\",\"Icon\":\"o.png\",\"IsAssigned\":\"true\",\"IsEdit\":\"false\",\"Tips\":\"frametest\",\"PopInfoWidth\":\"200\",\"PopInfoHeight\":\"200\",\"PopInfo\":[{\"type\":\"4\",\"tilte\":\"frame\",\"value\":\"http://www.baidu.com\"}]}"
                    + ",{\"PopInfoWidth\":\"350\",\"PopInfoHeight\":\"250\" ,\"StoreID\": \"8788\", \"LabelID\": \"\",\"LabelContent\":\"LT\", \"Lng\": \"113.9185962\", \"Lat\": \"22.4912462\", \"Icon\": \"o.png\", \"IsAssigned\": \"false\", \"Tips\": \"客惠隆百货-草围\", \"PopInfo\": [{ \"type\": \"4\", \"tilte\": \"frame\", \"value\": \"../Basic/Message.htm\"}]}"
                    + "]";
            return content;
        }

        private string GetMapEntity()
        {
            string content = string.Empty;
            MapEntity mapInfo = new MapEntity();
            #region
            mapInfo.StoreID = "B8DA8A6AE4B44737AA608D2F9C445B99";
            mapInfo.LabelID = "1";
            mapInfo.Menu = "0";
            mapInfo.LabelContent = "123232323";
            mapInfo.Lng = "121.44117";
            mapInfo.Lat = "31.22913";
            mapInfo.Icon = "g.png";
            //{\"type\":\"2\",\"title\":\"图片\",\"value\":\"\"},{\"type\":\"3\",\"title\":\"按钮1\",\"value\":\"test1\"}]}"
            mapInfo.IsAssigned = "false";
            mapInfo.IsEdit = "true";
            mapInfo.Tips = "这是一个测试点位1";
            //mapInfo.PopInfoHeight = "";
            mapInfo.PopInfoWidth = "200";
            #endregion
            IList<PopInfo> popInfoList = new List<PopInfo>();
            #region
            PopInfo popInfo1 = new PopInfo();
            popInfo1.type = 1;
            popInfo1.title = "客户名称";
            popInfo1.value = "坂田医院";
            popInfoList.Add(popInfo1);
            PopInfo popInfo2 = new PopInfo();
            popInfo2.type = 2;
            popInfo2.title = "图片";
            popInfo2.value = "http://www.mobanwang.com/icon/UploadFiles_8971/200806/20080602162129496.png";
            popInfoList.Add(popInfo2);
            PopInfo popInfo3 = new PopInfo();
            popInfo3.type = 3;
            popInfo3.title = "按钮1";
            popInfo3.value = "test1";
            popInfoList.Add(popInfo3);
            #endregion
            mapInfo.PopInfo = popInfoList;
            IList<MapEntity> MapInfoList = new List<MapEntity>();

            MapInfoList.Add(mapInfo);
            content = MapInfoList.ToJSON();
            var num1 = 16;
            var num2 = 10;
            var num3 = 12;
            content = "{ \"num1\":\"" + num1 + "\", \"num2\":\"" + num2 + "\", \"num3\":\"" + num3 + "\",\"data\":" + content + ",  }";

            return content;
        }
        #endregion

        #region GetOrder
        /// <summary>
        /// GetOrder
        /// </summary>
        public string GetOrder()
        {
            var service = new GOrderBLL(CurrentUserInfo);
            string content = string.Empty;

            string key = string.Empty;
            if (Request("id") != null && Request("id") != string.Empty)
            {
                key = Request("id").ToString().Trim();
            }

            var data = new GOrderEntity();

            var list = service.QueryByEntity(new GOrderEntity() { OrderId = key }, null);
            if (list != null && list.Length > 0)
            {
                data = list[0];
            }

            content = data.ToJSON();
            return content;
        }
        #endregion

        #region GetOrderTable
        /// <summary>
        /// GetOrderTable
        /// </summary>
        public string GetOrderTable()
        {
            var service = new GOrderBLL(CurrentUserInfo);
            var content = new StringBuilder();

            string key = string.Empty;
            if (Request("id") != null && Request("id") != string.Empty)
            {
                key = Request("id").ToString().Trim();
            }

            var data = new GOrderEntity();
            var list = service.GetReceiptByOrderId(key);
            content.AppendFormat("<table class=\"z_tk_2\" style=\"width:100%;\">");
            content.AppendFormat("<tr>");
            content.AppendFormat("<td class=\"z_tk_3\" style=\"width:10%;font-weight:bold;\">&nbsp;</td>");
            content.AppendFormat("<td class=\"z_tk_3\" style=\"width:30%;font-weight:bold;\">店员/工号</td>");
            content.AppendFormat("<td class=\"z_tk_3\" style=\"width:30%;font-weight:bold;\">当前复合(%)</td>");
            content.AppendFormat("<td class=\"z_tk_3\" style=\"width:30%;font-weight:bold;\">距离订单地址(km)</td>");
            content.AppendFormat("</tr>");
            content.AppendFormat("");
            if (list != null && list.Count > 0)
            {
                for (var i = 0; i < list.Count; i++)
                {
                    content.AppendFormat("<tr>");
                    content.AppendFormat(" <td class=\"z_tk_3\" style=\"text-align:center;\"><input type=\"radio\" name=\"order\" value=\"{0}\" /></td>", list[i].UserId);
                    content.AppendFormat(" <td class=\"z_tk_3\" style=\"\">{0}</td>", list[i].UserName);
                    content.AppendFormat(" <td class=\"z_tk_3\" style=\"\">{0}</td>", list[i].GoodnessFit);
                    content.AppendFormat(" <td class=\"z_tk_3\" style=\"\">{0}</td>", list[i].Distance);
                    content.AppendFormat("</tr>");
                }
            }
            content.AppendFormat("</table>");
            return content.ToString();
        }
        #endregion

        #region SendOrder
        /// <summary>
        /// SendOrder
        /// </summary>
        public string SendOrder()
        {
            var service = new GOrderBLL(CurrentUserInfo);
            string content = string.Empty;
            string key = string.Empty;
            if (Request("id") != null && Request("id") != string.Empty)
            {
                key = Request("id").ToString().Trim();
            }

            var data = new GOrderEntity();
            var error = "";
            var userId = Request("userId").ToString().Trim();
            var msgUrl = ConfigurationManager.AppSettings["push_weixin_msg_url"];
            var flag = true;
            try
            {
                flag = service.GetReceiptConfirm(key, userId, msgUrl, out error);
            }
            catch (Exception ex)
            {
                flag = false;
                error = ex.ToString();
            }
            if (flag)
            {
                content = "{\"success\":true, \"msg\":\"\"}";
            }
            else
            {
                content = "{\"success\":false, \"msg\":\"" + error + "\"}";
            }
            return content;
        }
        #endregion

    }
}