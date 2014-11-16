using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;
using JIT.Utility.Log;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using JIT.Utility.ExtensionMethod;
using JIT.Utility;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.Web.wap
{
    public partial class wapData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //http://alumniapp.ceibs.edu:8080/ceibs/publicMark?
            //action=getShopDetail&
            //ReqContent=%7B%22common%22%3A%7B%22locale%22%3A%22zh%22%2C%22userId%22%3A%22%22%2C%22openId%22%3A%22%22%7D%2C%22special%22%3A%7B%22serviceId%22%3A%221%22%7D%7D&_=1369729582745
            //http://alumniapp.ceibs.edu:8080/ceibs/publicMark?
            //action=getShopDetail&ReqContent={"common":{"locale":"zh","userId":"238869","openId":"666666"},
            //"special":{"serviceId":"1"}}
            
            Response.Clear();
            string content = string.Empty;
            try
            {
                string dataType = Request["action"].ToString().Trim();
                switch (dataType)
                {
                    case "getShopDetail":
                        content = getShopDetail();
                        break;
                    case "SetCheckReal":
                        content = SetCheckReal();
                        break;
                    case "GetImageComment":
                        content = GetImageComment();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                content = ex.Message;
            }
            Response.ContentEncoding = Encoding.UTF8;
            Response.Write(content);
            Response.End();
        }

        #region
        public string getShopDetail() {
            string ReqContent = string.Empty;
            string content = string.Empty;
            var respObj = new getShopInfoRespData();
            string respStr = string.Empty;
            try
            {
                ReqContent = Request["ReqContent"];
                ReqContent = HttpUtility.HtmlDecode(ReqContent);
                var reqContentObj = ReqContent.DeserializeJSONTo<getShopInfoReqData>();
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format(
                        "getShopDetail ReqContent:{0}",
                        ReqContent)
                });
                string shopId = reqContentObj.special.serviceId;
                //shopId = "5";
                var contentObj = new getShopInfoContent();
                //contentObj.address = "云南省丽江市古城区光义街忠义巷174号";
                //contentObj.description = "花间堂•青尘院位于丽江市古城区光义街忠义巷174号，毗邻丽江古城最具当地生活气息的集市——忠义市场，交通出行十分便利，步行至著名景点木府也仅需3分钟.青尘院由主院、辅院、后院三个院落组成，主院宽敞大气，辅院精巧别致，后院宁静优雅。18间精致、唯美的客房与自助餐厅、书房、影音室等公共空间分布其间，各种花卉树木错落环绕，郁郁葱葱，移步易景，景景相透。客房内均配有地暖系统、高档卫浴设施、卫星电视及无线网络，在古朴典雅的纳西院落中，尽享清闲舒适的丽江慢生活。青尘院的另一大特色便是她的自助厨房，借助临近忠义市场这一得天独厚的优势，采买最新鲜地道的当地食材，亲自下厨一展身手，绝对乐在其中。";
                //contentObj.imageUrl = "http://img1.qunarzz.com/wugc/p207/201305/15/743dcc658d3d878093835fbb.jpg_r_535x400x90_5a16d9ad.jpg";
                //contentObj.name = "花间堂丽江青尘院体验券";
                //contentObj.price = "780元/一晚";
                //contentObj.priceNow = "1元/一晚";
                //contentObj.telephone = "18988015030";
                if (!shopId.Equals(""))
                {
                    contentObj = GetShopProductInfoById(shopId);
                }
                respObj.content = contentObj;

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format(
                        "getShopDetail RespContent:{0}",
                        respObj.ToJSON())
                });
            }
            catch (Exception ex) {
                respObj.code = "103";
                respObj.description = "数据库操作错误";
                respObj.exception = ex.ToString();
            }
            content = respObj.ToJSON();
            //content = "{\"code\":\"200\",\"content\":{\"address\":\"云南省丽江市古城区光义街忠义巷174号\",\"description\":\"花间堂•青尘院位于丽江市古城区光义街忠义巷174号，毗邻丽江古城最具当地生活气息的集市——忠义市场，交通出行十分便利，步行至著名景点木府也仅需3分钟.青尘院由主院、辅院、后院三个院落组成，主院宽敞大气，辅院精巧别致，后院宁静优雅。18间精致、唯美的客房与自助餐厅、书房、影音室等公共空间分布其间，各种花卉树木错落环绕，郁郁葱葱，移步易景，景景相透。客房内均配有地暖系统、高档卫浴设施、卫星电视及无线网络，在古朴典雅的纳西院落中，尽享清闲舒适的丽江慢生活。青尘院的另一大特色便是她的自助厨房，借助临近忠义市场这一得天独厚的优势，采买最新鲜地道的当地食材，亲自下厨一展身手，绝对乐在其中。\",\"imageUrl\":\"http://img1.qunarzz.com/wugc/p207/201305/15/743dcc658d3d878093835fbb.jpg_r_535x400x90_5a16d9ad.jpg\",\"name\":\"花间堂丽江青尘院体验券\",\"price\":\"780元/一晚\",\"priceNow\":\"1元/一晚\",\"telephone\":\"18988015030\"},\"description\":\"操作成功\"}; ";
            //content = "{\"code\":\"200\",
            //\"content\":{\"address\":\"云南省丽江市古城区光义街忠义巷174号\",
            //\"description\":\"花间堂•青尘院位于丽江市古城区光义街忠义巷174号，毗邻丽江古城最具当地生活气息的集市——忠义市场，交通出行十分便利，步行至著名景点木府也仅需3分钟.青尘院由主院、辅院、后院三个院落组成，主院宽敞大气，辅院精巧别致，后院宁静优雅。18间精致、唯美的客房与自助餐厅、书房、影音室等公共空间分布其间，各种花卉树木错落环绕，郁郁葱葱，移步易景，景景相透。客房内均配有地暖系统、高档卫浴设施、卫星电视及无线网络，在古朴典雅的纳西院落中，尽享清闲舒适的丽江慢生活。青尘院的另一大特色便是她的自助厨房，借助临近忠义市场这一得天独厚的优势，采买最新鲜地道的当地食材，亲自下厨一展身手，绝对乐在其中。\",
            //\"imageUrl\":\"http://img1.qunarzz.com/wugc/p207/201305/15/743dcc658d3d878093835fbb.jpg_r_535x400x90_5a16d9ad.jpg\",
            //\"name\":\"花间堂丽江青尘院体验券\",\"price\":\"780元/一晚\",
            //\"priceNow\":\"1元/一晚\",\"telephone\":\"18988015030\"},\"description\":\"操作成功\"}";
            return content;
        }
        #endregion

        #region 获取商品信息
        public getShopInfoContent GetShopProductInfoById(string shopId)
        {
            var contentObj = new getShopInfoContent();
            ShopProductBLL bll = new ShopProductBLL(Default.GetLoggingSession());
            ShopProductEntity shopInfo = new ShopProductEntity();
            shopInfo = bll.GetByID(shopId);
            if (shopInfo != null)
            {
                contentObj.address = shopInfo.Address;
                contentObj.description = shopInfo.Description;
                contentObj.imageUrl = shopInfo.ImageURL;
                contentObj.name = shopInfo.ProductName;
                contentObj.price = Convert.ToString(shopInfo.Price) + shopInfo.ProductUnit;
                contentObj.priceNow = Convert.ToString(shopInfo.PriceNow) + shopInfo.ProductUnit;
                contentObj.telephone = shopInfo.Telephone;
            }
            return contentObj;
        }
        #endregion

        #region 验证
        public class getShopInfoReqData 
        {
            public baseCommon common;
            public baseSpecial special;
        }

        public class baseCommon {
            public string locale;
            public string userId;
            public string openId;
        }
        public class baseSpecial
        {
            public string serviceId;
        }

        public class getShopInfoRespData 
        {
            public string code = "200";
            public string description = "操作成功";
            public string exception;
            public getShopInfoContent content;
        }
        public class getShopInfoContent
        {
            public string address;
            public string imageUrl;
            public string description;
            public string name;
            public string price;
            public string priceNow;
            public string telephone;
        }
        #endregion

        #region 验证真伪得积分
        /// <summary>
        /// 验证真伪得积分
        /// </summary>
        public string SetCheckReal()
        {
            string content = string.Empty;
            var respData = new SetCheckRealRespData();
            try
            {
                string openId = Request["openID"];
                //openId = "o8Y7Ejl1zl5RHXDvPONCNqoC5Md8";

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format(
                        "OpenID:{0}", openId)
                });

                var currentUser = Default.GetLoggingSession();

                VipBLL vipBLL = new VipBLL(currentUser);
                IntegralRuleBLL integralRuleBLL = new IntegralRuleBLL(currentUser);
                VipIntegralDetailBLL vipIntegralDetailBLL = new VipIntegralDetailBLL(currentUser);
                VipIntegralBLL vipIntegralBLL = new VipIntegralBLL(currentUser);

                string vipId = null;
                if (true)
                {
                    VipEntity vipIdData = null;
                    var vipIdDataList = vipBLL.QueryByEntity(new VipEntity()
                    {
                        WeiXinUserId = openId
                    }, null);
                    if (vipIdDataList == null || vipIdDataList.Length == 0 || vipIdDataList[0] == null ||
                        vipIdDataList[0].VIPID == null)
                    {
                        respData.Code = "103";
                        respData.Description = "数据库操作错误";
                        respData.Exception = "未查询到Vip会员";
                        return respData.ToJSON();
                    }
                    else
                    {
                        vipIdData = vipIdDataList[0];
                        vipId = vipIdData.VIPID;
                    }
                }

                // SysIntegralSource: 10
                string integralSourceId = "10";
                decimal integralValue = 0;
                if (true)
                {
                    IntegralRuleEntity integralRuleData = null;
                    var integralRuleDataList = integralRuleBLL.QueryByEntity(new IntegralRuleEntity()
                    {
                        IntegralSourceID = integralSourceId
                    }, null);
                    if (integralRuleDataList == null || integralRuleDataList.Length == 0 || integralRuleDataList[0] == null)
                    {
                        respData.Code = "103";
                        respData.Description = "数据库操作错误";
                        respData.Exception = "未查询到积分规则";
                        return respData.ToJSON();
                    }
                    else
                    {
                        integralRuleData = integralRuleDataList[0];
                        integralValue = CPOS.Common.Utils.GetDecimalVal(integralRuleData.Integral);
                    }
                }

                #region 保存积分
                if (true)
                {
                    string tmpVipId = vipId;
                    decimal tmpIntegralValue = integralValue;
                    string tmpIntegralSourceId = integralSourceId;
                    string tmpOpenId = openId;
                    string msgModel = "【验真品，送积分】活动，您本次验证赢得{0}点积分。您当前的总积分是{1}点。";

                    // 插入积分明细
                    VipIntegralDetailEntity vipIntegralDetailEntity = new VipIntegralDetailEntity();
                    vipIntegralDetailEntity.VipIntegralDetailID = CPOS.Common.Utils.NewGuid();
                    vipIntegralDetailEntity.VIPID = tmpVipId;
                    vipIntegralDetailEntity.FromVipID = tmpVipId;
                    vipIntegralDetailEntity.SalesAmount = 0;
                    vipIntegralDetailEntity.Integral = tmpIntegralValue;
                    vipIntegralDetailEntity.IntegralSourceID = tmpIntegralSourceId;
                    vipIntegralDetailEntity.IsAdd = 1;
                    //vipIntegralDetailBLL.Create(vipIntegralDetailEntity);

                    // 更新积分
                    VipIntegralEntity vipIntegralEntity = new VipIntegralEntity();
                    var vipIntegralDataList = vipIntegralBLL.QueryByEntity(
                        new VipIntegralEntity() { VipID = vipId }, null);
                    if (vipIntegralDataList == null || vipIntegralDataList.Length == 0 || vipIntegralDataList[0] == null)
                    {
                        vipIntegralEntity.VipID = tmpVipId;
                        vipIntegralEntity.BeginIntegral = 0; // 期初积分
                        vipIntegralEntity.InIntegral = tmpIntegralValue; // 增加积分
                        vipIntegralEntity.OutIntegral = 0; //消费积分
                        vipIntegralEntity.EndIntegral = tmpIntegralValue; //积分余额
                        vipIntegralEntity.InvalidIntegral = 0; // 累计失效积分
                        vipIntegralEntity.ValidIntegral = tmpIntegralValue; // 当前有效积分
                        //vipIntegralBLL.Create(vipIntegralEntity);
                    }
                    else
                    {
                        vipIntegralEntity.VipID = tmpVipId;
                        vipIntegralEntity.InIntegral = Common.Utils.GetDecimalVal(
                            vipIntegralDataList[0].InIntegral) + tmpIntegralValue; ; // 增加积分
                        //vipIntegralEntity.OutIntegral = 0; //消费积分
                        vipIntegralEntity.EndIntegral = Common.Utils.GetDecimalVal(
                            vipIntegralDataList[0].EndIntegral) + tmpIntegralValue; //积分余额
                        //vipIntegralEntity.InvalidIntegral = 0; // 累计失效积分
                        vipIntegralEntity.ValidIntegral = Common.Utils.GetDecimalVal(
                            vipIntegralDataList[0].ValidIntegral) + tmpIntegralValue; // 当前有效积分
                        //vipIntegralBLL.Update(vipIntegralEntity, false);
                    }

                    // 更新VIP
                    VipEntity vipEntity = new VipEntity();
                    var vipEntityDataList = vipBLL.QueryByEntity(
                        new VipEntity() { VIPID = tmpVipId }, null);
                    if (vipEntityDataList == null || vipEntityDataList.Length == 0 || vipEntityDataList[0] == null)
                    {
                        vipEntity.VIPID = tmpVipId;
                        vipEntity.Integration = vipIntegralEntity.ValidIntegral;
                        //vipEntity.HigherVipID = highOpenId;
                        vipEntity.ClientID = currentUser.CurrentUser.customer_id;
                        vipEntity.Status = 1;
                        vipBLL.Create(vipEntity);
                    }
                    else
                    {
                        vipEntity.VIPID = tmpVipId;
                        vipEntity.Integration = vipIntegralEntity.ValidIntegral;
                        //vipEntity.HigherVipID = highOpenId;
                        vipEntity.ClientID = currentUser.CurrentUser.customer_id;
                        vipBLL.Update(vipEntity, false);
                    }

                    // 推送消息
                    string msgUrl = ConfigurationManager.AppSettings["push_weixin_msg_url"].Trim();
                    string msgText = string.Format(msgModel, tmpIntegralValue, vipEntity.Integration);
                    string msgData = "<xml><OpenID><![CDATA[" + tmpOpenId + "]]></OpenID><Content><![CDATA[" + msgText + "]]></Content></xml>";

                    var msgResult = Common.Utils.GetRemoteData(msgUrl, "POST", msgData);
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("PushMsgResult:{0}", msgResult)
                    });

                    respData.Data = tmpIntegralValue.ToString();
                }
                #endregion
            }
            catch (Exception ex)
            {
                respData.Code = "103";
                respData.Description = "数据库操作错误";
                respData.Exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class SetCheckRealRespData : Default.RespData
        {
            public string Msg;
        }
        #endregion

        #region 获取图片与评论信息
        /// <summary>
        /// 获取图片与评论信息
        /// </summary>
        public string GetImageComment()
        {
            string content = string.Empty;
            var respData = new GetImageCommentRespData();
            try
            {
                string openId = Request["openID"];
                string id = Request["id"];

                //openId = "o8Y7Ejl1zl5RHXDvPONCNqoC5Md8";
                //id = "1";

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format(
                        "openId:{0}, id:{1}", openId, id)
                });

                var currentUser = Default.GetLoggingSession();

                VipBLL vipBLL = new VipBLL(currentUser);
                VIPImageCommentBLL vipImageCommentBLL = new VIPImageCommentBLL(currentUser);

                string vipId = null;
                if (true)
                {
                    VipEntity vipIdData = null;
                    var vipIdDataList = vipBLL.QueryByEntity(new VipEntity()
                    {
                        WeiXinUserId = openId
                    }, null);
                    if (vipIdDataList == null || vipIdDataList.Length == 0 || vipIdDataList[0] == null ||
                        vipIdDataList[0].VIPID == null)
                    {
                        respData.Code = "103";
                        respData.Description = "数据库操作错误";
                        respData.Exception = "未查询到Vip会员";
                        return respData.ToJSON();
                    }
                    else
                    {
                        vipIdData = vipIdDataList[0];
                        vipId = vipIdData.VIPID;
                    }
                }

                VIPImageCommentEntity vipImageCommentData = null;
                var vipImageCommentDataList = vipImageCommentBLL.QueryByEntity(new VIPImageCommentEntity()
                {
                    VipImageCommentID = id
                }, null);
                if (vipImageCommentDataList == null || vipImageCommentDataList.Length == 0 || vipImageCommentDataList[0] == null)
                {
                    respData.Code = "103";
                    respData.Description = "数据库操作错误";
                    respData.Exception = "未查询到数据";
                    return respData.ToJSON();
                }
                else
                {
                    vipImageCommentData = vipImageCommentDataList[0];

                    respData.Content = new GetImageCommentContentRespData();
                    respData.Content.ImageUrl = vipImageCommentData.ImageURL;
                    respData.Content.Comment = vipImageCommentData.Comment;
                }
                
            }
            catch (Exception ex)
            {
                respData.Code = "103";
                respData.Description = "数据库操作错误";
                respData.Exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class GetImageCommentRespData : Default.RespData
        {
            public GetImageCommentContentRespData Content;
        }
        public class GetImageCommentContentRespData
        {
            public string ImageUrl;
            public string Comment;
        }
        #endregion
    }
}