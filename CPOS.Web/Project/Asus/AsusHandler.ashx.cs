using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;

using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;

using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess.Query;
using System.Text;
using ThoughtWorks.QRCode.Codec;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Collections;
using JIT.Utility;
using System.Security.Cryptography;
using System.Web.Security;
using JIT.Utility.Notification;

namespace JIT.CPOS.Web.Project
{
    /// <summary>
    /// AsusHandler 的摘要说明
    /// </summary>
    public class AsusHandler : JIT.CPOS.Web.Base.PageBase.JITAjaxHandler
    {
        string customerId = "a6c351d17bf5482a807f1780a83b8239";

        #region AmbassadorLoginIn
        /// <summary>
        /// 华硕校园 专家登录
        /// </summary>
        /// <returns></returns>
        public string AmbassadorLoginIn()
        {
            string content = string.Empty;
            var respData = new ambassadorLoginInRespData();
            try
            {
                //接收参数
                string reqContent = HttpContext.Current.Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<ambassadorLoginInReqData>();
                reqObj = reqObj == null ? new ambassadorLoginInReqData() : reqObj;

                //获取客户ID
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }

                //初始化返回对象
                respData.content = new ambassadorLoginInRespContentData();
                respData.content.vipList = new List<ambassadorLoginInRespContentDataItem>();

                //用户登录信息
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                OnlineShoppingItemBLL itemService = new OnlineShoppingItemBLL(loggingSessionInfo);

                #region //组装参数
                Dictionary<string, string> pParams = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(reqObj.special.code))
                {
                    pParams.Add("pCode", reqObj.special.code);
                }
                //是否有此人
                var vip = new VipBLL(loggingSessionInfo).Query(new IWhereCondition[] {
                    new EqualsCondition() { FieldName = "VipCode", Value = reqObj.special.code },
                    new EqualsCondition() { FieldName = "ClientID", Value = customerId }
                }, null).FirstOrDefault();

                if (vip != null)
                {
                    pParams.Add("pPass", MD5Helper.Encryption(MD5Helper.Encryption(reqObj.special.pass) + vip.Col4));
                }
                else
                {
                    respData.code = "111";
                    respData.description = "专家编号或密码错误";

                    content = respData.ToJSON();
                    return content;
                }
                #endregion


                var vipInfo = itemService.AmbassadorLoginIn(pParams);
                if (vipInfo != null && vipInfo.Count > 0)
                {
                    List<ambassadorLoginInRespContentDataItem> list = new List<ambassadorLoginInRespContentDataItem>();
                    foreach (var item in vipInfo)
                    {
                        ambassadorLoginInRespContentDataItem info = new ambassadorLoginInRespContentDataItem();
                        info.VipID = item.VIPID;
                        info.VipName = item.VipName;
                        info.VipRealName = item.VipRealName;
                        info.VipCode = item.VipCode;
                        info.Phone = item.Phone;
                        info.City = item.City;
                        info.DeliveryAddress = item.DeliveryAddress;
                        info.Province = item.Province;
                        info.CityCode = item.CityCode;
                        info.RoleName = item.RoleName;
                        info.Code = item.VipCode;
                        list.Add(info);
                    }
                    respData.content.vipList = list;
                }
                else
                {
                    respData.code = "111";
                    respData.description = "专家编号或密码错误";
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }
        #endregion

        #region GetUserList
        /// <summary>
        /// 华硕校园 获取会员列表
        /// </summary>
        /// <returns></returns>
        public string GetUserList()
        {
            string content = string.Empty;
            var respData = new ambassadorLoginInRespData();
            try
            {
                //接收参数
                string reqContent = HttpContext.Current.Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<ambassadorLoginInReqData>();
                reqObj = reqObj == null ? new ambassadorLoginInReqData() : reqObj;

                //获取客户ID
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }

                //初始化返回对象
                respData.content = new ambassadorLoginInRespContentData();
                respData.content.vipList = new List<ambassadorLoginInRespContentDataItem>();

                //用户登录信息
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                OnlineShoppingItemBLL itemService = new OnlineShoppingItemBLL(loggingSessionInfo);

                #region //组装参数
                Dictionary<string, string> pParams = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(reqObj.special.param))
                {
                    pParams.Add("pParam", reqObj.special.param);
                }

                if (!string.IsNullOrEmpty(reqObj.special.pageSize))
                {
                    pParams.Add("pageSize", reqObj.special.pageSize);
                }
                if (!string.IsNullOrEmpty(reqObj.special.pageIndex))
                {
                    pParams.Add("pageIndex", reqObj.special.pageIndex);
                }

                if (!string.IsNullOrEmpty(reqObj.common.userId))
                {
                    pParams.Add("pUserID", reqObj.common.userId);
                }

                #endregion
                int rowCount = 0;
                var vipInfo = itemService.GetUserList(pParams, out rowCount);
                if (vipInfo != null && vipInfo.Count > 0)
                {
                    List<ambassadorLoginInRespContentDataItem> list = new List<ambassadorLoginInRespContentDataItem>();
                    foreach (var item in vipInfo)
                    {
                        ambassadorLoginInRespContentDataItem info = new ambassadorLoginInRespContentDataItem();
                        info.VipID = item.VIPID;
                        info.VipName = item.VipName;
                        info.Phone = item.Phone;
                        info.DeliveryAddress = item.DeliveryAddress;
                        info.VipRealName = item.VipRealName;
                        info.RoleName = item.RoleName;
                        info.OrdersStatus = item.OrdersStatus;
                        info.Remark = item.Remark;
                        info.Code = item.VipCode;

                        list.Add(info);
                    }
                    respData.content.vipList = list;
                    respData.content.rowCount = rowCount;

                    int pageSize = 15;
                    if (!string.IsNullOrEmpty(reqObj.special.pageSize))
                    {
                        int.TryParse(reqObj.special.pageSize, out pageSize);
                    }
                    respData.content.pageCount = (int)(Math.Ceiling((decimal)rowCount / pageSize));
                }
                else
                {
                    respData.code = "111";
                    respData.description = "没有获取到数据";
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        #endregion

        #region ForgetPassword
        /// <summary>
        /// 校园专家 重置密码
        /// </summary>
        /// <returns></returns>
        public string ForgetPassword()
        {
            string content = string.Empty;
            var respData = new ambassadorLoginInRespData();
            try
            {
                //接收参数
                string reqContent = HttpContext.Current.Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<ambassadorLoginInReqData>();
                reqObj = reqObj == null ? new ambassadorLoginInReqData() : reqObj;

                //获取客户ID
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }

                //初始化返回对象
                respData.content = new ambassadorLoginInRespContentData();
                respData.content.vipList = new List<ambassadorLoginInRespContentDataItem>();

                //用户登录信息
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                OnlineShoppingItemBLL itemService = new OnlineShoppingItemBLL(loggingSessionInfo);

                var vipInfo = itemService.ForgetPassword(reqObj.special.email);
                if (vipInfo != null && vipInfo.Count > 0)
                {
                    //生成随机数 6位
                    Random rd = new Random();
                    string code = rd.Next(100000, 999999).ToString();

                    VipBLL vipBLL = new VipBLL(loggingSessionInfo);
                    VipEntity entity = new VipEntity();

                    entity = vipBLL.GetByID(vipInfo[0].VIPID);
                    entity.VipPasswrod = MD5Helper.Encryption(MD5Helper.Encryption(code) + entity.Col4);
                    entity.LastUpdateTime = DateTime.Now;
                    vipBLL.Update(entity);

                    #region 邮件发送
                    try
                    {
                        XmlManager xml = new XmlManager(ConfigurationManager.AppSettings["xmlFile"]);

                        FromSetting fs = new FromSetting();
                        fs.SMTPServer = xml.SelectNodeText("//Root/AsusMail//SMTPServer", 0);
                        fs.SendFrom = xml.SelectNodeText("//Root/AsusMail//MailSendFrom", 0);
                        fs.UserName = xml.SelectNodeText("//Root/AsusMail//MailUserName", 0);
                        fs.Password = xml.SelectNodeText("//Root/AsusMail//MailUserPassword", 0);
                        Mail.SendMail(fs, entity.Col7 + "," + xml.SelectNodeText("//Root/AsusMail//MailTo", 0), xml.SelectNodeText("//Root/AsusMail//MailTitle", 0), entity.VipRealName + "：你好，你的新密码为：" + code, null);
                    }
                    catch
                    {
                        respData.code = "111";
                        respData.description = "邮箱发送失败,请稍后重试";
                        content = respData.ToJSON();
                        return content;
                    }
                    #endregion

                    List<ambassadorLoginInRespContentDataItem> list = new List<ambassadorLoginInRespContentDataItem>();
                    foreach (var item in vipInfo)
                    {
                        ambassadorLoginInRespContentDataItem info = new ambassadorLoginInRespContentDataItem();
                        info.VipID = item.VIPID;
                        info.VipPassword = code;
                        info.Email = item.Email;

                        list.Add(info);
                    }
                    respData.content.vipList = list;
                }
                else
                {
                    respData.code = "111";
                    respData.description = "邮箱不存在";
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        #endregion

        #region UpdateUserInfo
        /// <summary>
        /// 更新会员信息
        /// </summary>
        /// <returns></returns>
        public string UpdateUserInfo()
        {
            string content = string.Empty;
            var respData = new ambassadorLoginInRespData();
            try
            {
                //接收参数
                string reqContent = HttpContext.Current.Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<ambassadorLoginInReqData>();
                reqObj = reqObj == null ? new ambassadorLoginInReqData() : reqObj;

                //获取客户ID
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }

                //初始化返回对象
                respData.content = new ambassadorLoginInRespContentData();
                respData.content.vipList = new List<ambassadorLoginInRespContentDataItem>();

                //用户登录信息
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                VipBLL vipBLL = new VipBLL(loggingSessionInfo);
                VipEntity entity = new VipEntity();

                //是否有此人
                var vip = vipBLL.Query(new IWhereCondition[] {
                    new EqualsCondition() { FieldName = "VipID", Value = reqObj.special.vipID },
                    new EqualsCondition() { FieldName = "ClientID", Value = customerId }
                }, null).FirstOrDefault();

                if (vip != null)
                {
                    entity = vipBLL.GetByID(vip.VIPID);
                    entity.Col16 = reqObj.special.remark;
                    entity.LastUpdateTime = DateTime.Now;
                    vipBLL.Update(entity);
                }
                respData.code = "200";
                respData.description = "操作成功";
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }
        #endregion

        #region GenerateQR
        /// <summary>
        /// 添加会员
        /// </summary>
        /// <returns></returns>
        public string GenerateQR()
        {
            string content = string.Empty;
            var respData = new ambassadorLoginInRespData();
            try
            {
                //接收参数
                string reqContent = HttpContext.Current.Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<ambassadorLoginInReqData>();
                reqObj = reqObj == null ? new ambassadorLoginInReqData() : reqObj;

                //获取客户ID
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }

                //初始化返回对象
                respData.content = new ambassadorLoginInRespContentData();
                respData.content.vipList = new List<ambassadorLoginInRespContentDataItem>();

                //用户登录信息
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                VipBLL vipBLL = new VipBLL(loggingSessionInfo);
                VipEntity entity = new VipEntity();

                //是否有此人
                var vip = vipBLL.Query(new IWhereCondition[] {
                    new EqualsCondition() { FieldName = "VipID", Value = reqObj.common.userId },
                    new EqualsCondition() { FieldName = "ClientID", Value = customerId }
                }, null).FirstOrDefault();

                if (vip != null)
                {
                    string QRCode = null;
                    if (!string.IsNullOrEmpty(vip.Col6))
                    {
                        QRCode = vip.Col6;
                    }
                    if (reqObj.special.generater)
                    {
                        QRCode = GeneratedQR(reqObj.special.url, vip.VIPID);
                        entity = vipBLL.GetByID(vip.VIPID);
                        entity.Col6 = QRCode;
                        entity.LastUpdateTime = DateTime.Now;
                        vipBLL.Update(entity);
                    }
                    else
                    {
                        QRCode = GeneratedQR(reqObj.special.url, vip.VIPID);
                        entity = vipBLL.GetByID(vip.VIPID);
                        entity.Col6 = QRCode;
                        entity.LastUpdateTime = DateTime.Now;
                        vipBLL.Update(entity);
                    }
                    List<ambassadorLoginInRespContentDataItem> list = new List<ambassadorLoginInRespContentDataItem>();
                    ambassadorLoginInRespContentDataItem info = new ambassadorLoginInRespContentDataItem();
                    info.VipID = vip.VIPID;
                    info.VipName = vip.VipName;
                    info.Phone = vip.Phone;
                    info.DeliveryAddress = vip.DeliveryAddress;
                    info.VipRealName = vip.VipRealName;
                    info.QRCode = QRCode;
                    info.Code = vip.Col4;

                    list.Add(info);

                    respData.content.vipList = list;
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }
        #endregion

        #region GetOrderList
        /// <summary>
        /// 华硕校园 获取订单列表
        /// </summary>
        /// <returns></returns>
        public string GetOrderList()
        {
            string content = string.Empty;
            var respData = new OrdersInfoRespData();
            try
            {
                //接收参数
                string reqContent = HttpContext.Current.Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<OrdersInfoReqData>();
                reqObj = reqObj == null ? new OrdersInfoReqData() : reqObj;

                //获取客户ID
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }

                //初始化返回对象
                respData.content = new OrdersInfoRespContentData();
                respData.content.orderList = new List<OrdersInfoRespContentDataItem>();

                //用户登录信息
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                OnlineShoppingItemBLL itemService = new OnlineShoppingItemBLL(loggingSessionInfo);

                int pageSize = 15, pageIndex = 1;
                if (!string.IsNullOrEmpty(reqObj.special.pageSize))
                {
                    pageSize = int.Parse(reqObj.special.pageSize);
                }
                if (!string.IsNullOrEmpty(reqObj.special.pageIndex))
                {
                    pageIndex = int.Parse(reqObj.special.pageIndex);
                }
                int rowCount = 0;
                var orderInfo = itemService.GetOrderList(reqObj.common.userId, pageSize, pageIndex, out rowCount);
                if (orderInfo != null && orderInfo.Count > 0)
                {
                    List<OrdersInfoRespContentDataItem> list = new List<OrdersInfoRespContentDataItem>();
                    foreach (var item in orderInfo)
                    {
                        OrdersInfoRespContentDataItem info = new OrdersInfoRespContentDataItem();
                        info.OrdersID = item.OrdersID;
                        info.OrdersNo = item.OrdersNo;
                        info.VipName = item.VipName;
                        info.Phone = item.Phone;
                        info.Model = item.Model;
                        info.OrderDate = item.OrderDate;
                        info.BuyWay = item.BuyWay;
                        info.GetWay = item.GetWay;
                        info.Price = item.Price;
                        info.OrdersStatusText = item.OrdersStatusText;

                        list.Add(info);
                    }
                    respData.content.orderList = list;
                    respData.content.rowCount = rowCount;

                    pageSize = 15;
                    if (!string.IsNullOrEmpty(reqObj.special.pageSize))
                    {
                        int.TryParse(reqObj.special.pageSize, out pageSize);
                    }
                    respData.content.pageCount = (int)(Math.Ceiling((decimal)rowCount / pageSize));
                }
                else
                {
                    respData.code = "111";
                    respData.description = "没有获取到数据";
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        #endregion

        #region AddOrders
        /// <summary>
        /// 订单提交
        /// </summary>
        /// <returns></returns>
        public string AddOrders()
        {
            string content = string.Empty;
            var respData = new OrdersInfoRespData();
            try
            {
                //接收参数
                string reqContent = HttpContext.Current.Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<OrdersInfoReqData>();
                reqObj = reqObj == null ? new OrdersInfoReqData() : reqObj;

                //获取客户ID
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }

                //初始化返回对象
                respData.content = new OrdersInfoRespContentData();
                respData.content.orderList = new List<OrdersInfoRespContentDataItem>();

                //用户登录信息
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                TInoutBLL inoutBLL = new TInoutBLL(loggingSessionInfo);
                TInoutEntity inoutEntity = new TInoutEntity();



                //添加订单
                inoutEntity.OrderID = Guid.NewGuid().ToString();
                inoutEntity.OrderTypeID = "1F0A100C42484454BAEA211D4C14B80F";
                inoutEntity.OrderReasonID = "2F6891A2194A4BBAB6F17B4C99A6C6F5";
                inoutEntity.OrderNo = GetOrdersNo();
                inoutEntity.VipNo = reqObj.common.userId;
                inoutEntity.OrderDate = DateTime.Now.ToString("yyyy-MM-dd");
                inoutEntity.RedFlag = "1";
                inoutEntity.Status = "100";
                inoutEntity.StatusDesc = "未审核";
                inoutEntity.PurchaseUnitID = ConfigurationManager.AppSettings["HS_PurchaseUnitID"].ToString();
                inoutEntity.CreateTime = DateTime.Now.ToString();

                if (reqObj.special.photo.Length > 0)
                {
                    var length = reqObj.special.photo.Length;
                    inoutEntity.Field1 = length >= 1 ? reqObj.special.photo[0].ToString() : "";
                    inoutEntity.Field2 = length >= 2 ? reqObj.special.photo[1].ToString() : "";
                    inoutEntity.Field3 = length >= 3 ? reqObj.special.photo[2].ToString() : "";
                    inoutEntity.Field4 = length >= 4 ? reqObj.special.photo[3].ToString() : "";
                    inoutEntity.Field5 = length >= 5 ? reqObj.special.photo[4].ToString() : "";
                    inoutEntity.Field6 = length >= 6 ? reqObj.special.photo[5].ToString() : "";
                }
                inoutEntity.Field7 = "100";//后台查询订单用
                inoutEntity.Field10 = "";//订单状态

                inoutEntity.Field8 = reqObj.special.model;  //产品型号
                inoutEntity.Field9 = reqObj.special.serial; //产品序列号
                
                inoutEntity.Field11 = reqObj.special.buyWay;//购买方式 关联Option
                inoutEntity.Field12 = reqObj.special.name;//客户姓名
                inoutEntity.Field13 = reqObj.special.mobile;//电话
                inoutEntity.Field14 = reqObj.special.email;//Email
                inoutEntity.Field15 = reqObj.special.getWay;//客户获取方式 关联Option
                inoutEntity.Field16 = reqObj.special.date;//订单日期
                inoutEntity.Field17 = reqObj.special.price;//订单价格

                inoutEntity.Field18 = reqObj.special.school;//学校
                inoutEntity.Field19 = reqObj.special.specialt;//专业
                inoutEntity.Field20 = reqObj.special.intent;//购买前意向




                inoutEntity.CustomerID = reqObj.common.customerId;

                inoutBLL.Create(inoutEntity);

                respData.code = "200";
                respData.description = "操作成功";
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }
        #endregion

        #region FilePhoto
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public string FileUpload()
        {
            string res = "";
            HttpPostedFile files = HttpContext.Current.Request.Files["fileUp"];
            UpLoadAttachment(files, out res);
            return res;
        }
        #endregion

        #region GetBuyWay
        /// <summary>
        /// 华硕校园 获取购买方式
        /// </summary>
        /// <returns></returns>
        public string GetBuyWay()
        {
            string content = string.Empty;
            var respData = new OptionsRespData();
            try
            {
                //接收参数
                string reqContent = HttpContext.Current.Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<OptionsReqData>();
                reqObj = reqObj == null ? new OptionsReqData() : reqObj;

                //获取客户ID
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }

                //初始化返回对象
                respData.content = new OptionsRespContentData();
                respData.content.optionList = new List<OptionsRespContentDataItem>();

                //用户登录信息
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                OnlineShoppingItemBLL itemService = new OnlineShoppingItemBLL(loggingSessionInfo);

                var optionsInfo = itemService.GetBuyWay();
                if (optionsInfo != null && optionsInfo.Count > 0)
                {
                    List<OptionsRespContentDataItem> list = new List<OptionsRespContentDataItem>();
                    foreach (var item in optionsInfo)
                    {
                        OptionsRespContentDataItem info = new OptionsRespContentDataItem();
                        info.OptionName = item.OptionName;
                        info.OptionValue = int.Parse(item.OptionValue.ToString());
                        info.OptionText = item.OptionText;

                        list.Add(info);
                    }
                    respData.content.optionList = list;
                }
                else
                {
                    respData.code = "111";
                    respData.description = "没有获取到数据.";
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        #endregion

        #region GetWay
        /// <summary>
        /// 华硕校园 获取购买方式
        /// </summary>
        /// <returns></returns>
        public string GetWay()
        {
            string content = string.Empty;
            var respData = new OptionsRespData();
            try
            {
                //接收参数
                string reqContent = HttpContext.Current.Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<OptionsReqData>();
                reqObj = reqObj == null ? new OptionsReqData() : reqObj;

                //获取客户ID
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }

                //初始化返回对象
                respData.content = new OptionsRespContentData();
                respData.content.optionList = new List<OptionsRespContentDataItem>();

                //用户登录信息
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                OnlineShoppingItemBLL itemService = new OnlineShoppingItemBLL(loggingSessionInfo);

                var optionsInfo = itemService.GetWay();
                if (optionsInfo != null && optionsInfo.Count > 0)
                {
                    List<OptionsRespContentDataItem> list = new List<OptionsRespContentDataItem>();
                    foreach (var item in optionsInfo)
                    {
                        OptionsRespContentDataItem info = new OptionsRespContentDataItem();
                        info.OptionName = item.OptionName;
                        info.OptionValue = int.Parse(item.OptionValue.ToString());
                        info.OptionText = item.OptionText;

                        list.Add(info);
                    }
                    respData.content.optionList = list;
                }
                else
                {
                    respData.code = "111";
                    respData.description = "没有获取到数据.";
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        #endregion

        #region 会员相关参数及返回值信息
        public class ambassadorLoginInRespData : Default.LowerRespData
        {
            public ambassadorLoginInRespContentData content { get; set; }
        }
        public class ambassadorLoginInRespContentData
        {
            public IList<ambassadorLoginInRespContentDataItem> vipList { get; set; }
            public int rowCount { get; set; }
            public int pageCount { get; set; }
        }

        public class ambassadorLoginInRespContentDataItem
        {
            public string VipID { get; set; }
            public string VipName { get; set; }
            public string VipPassword { get; set; }
            public string VipRealName { get; set; }
            public string VipCode { get; set; }
            public string Phone { get; set; }
            public string City { get; set; }
            public string DeliveryAddress { get; set; }
            public string Province { get; set; }
            public string CityCode { get; set; }
            public string RoleName { get; set; }
            public string OrdersStatus { get; set; }
            public string Remark { get; set; }
            public string QRCode { get; set; }
            public string Email { get; set; }
            public string Code { get; set; }//专家编号（供会员推荐码）
        }

        public class ambassadorLoginInReqData : ReqData
        {
            public ambassadorLoginInReqSpecialData special;
        }
        public class ambassadorLoginInReqSpecialData
        {
            public string vipID { get; set; }//会员ID
            public string code { get; set; } //专家编号
            public string pass { get; set; } //专家密码
            public string param { get; set; }//参数值
            public string remark { get; set; }//备注信息
            public string url { get; set; }
            public bool generater { get; set; }
            public string email { get; set; }

            public string pageSize { get; set; }
            public string pageIndex { get; set; }
        }

        #endregion

        #region 订单相关参数及返回值信息
        public class OrdersInfoRespData : Default.LowerRespData
        {
            public OrdersInfoRespContentData content { get; set; }
        }
        public class OrdersInfoRespContentData
        {
            public IList<OrdersInfoRespContentDataItem> orderList { get; set; }
            public int rowCount { get; set; }
            public int pageCount { get; set; }
        }

        public class OrdersInfoRespContentDataItem
        {
            public string OrdersID { get; set; }
            public string OrdersNo { get; set; }
            public string VipName { get; set; }
            public string Phone { get; set; }
            public string Model { get; set; }
            public string OrderDate { get; set; }
            public string BuyWay { get; set; }
            public string GetWay { get; set; }
            public string Price { get; set; }
            public string OrdersStatusText { get; set; }
        }

        public class OrdersInfoReqData : ReqData
        {
            public OrdersInfoReqSpecialData special;
        }
        public class OrdersInfoReqSpecialData
        {
            public string model { get; set; }//产品型号
            public string serial { get; set; }//序列号
            public string buyWay { get; set; }//购买方式
            public string date { get; set; }//购买日期
            public string name { get; set; }//客户姓名
            public string mobile { get; set; }//电话
            public string email { get; set; }//Email
            public string getWay { get; set; }//客户获取方式
            public string price { get; set; }//商品价格
            public string[] photo { get; set; }

            public string school { set; get; }//学校
            public string specialt { set;get; }//专业
            public string intent { set; get; }//客户购买前意向



            public string pageSize { get; set; }
            public string pageIndex { get; set; }
        }

        #endregion

        #region Options相关参数及返回值信息
        public class OptionsRespData : Default.LowerRespData
        {
            public OptionsRespContentData content { get; set; }
        }
        public class OptionsRespContentData
        {
            public IList<OptionsRespContentDataItem> optionList { get; set; }
        }

        public class OptionsRespContentDataItem
        {
            public string OptionName { get; set; }
            public int OptionValue { get; set; }
            public string OptionText { get; set; }
        }

        public class OptionsReqData : ReqData
        {
            public OptionsReqSpecialData special;
        }
        public class OptionsReqSpecialData
        {

        }

        #endregion

        #region 公共请求参数
        public class ReqData
        {
            public ReqCommonData common { get; set; }
        }
        public class ReqCommonData
        {
            public string locale;
            public string userId;
            public string openId;
            public string signUpId;
            public string customerId;
            public string businessZoneId;//商图ID
            public string channelId;//渠道ID
            public string eventId;
        }
        #endregion

        #region 生成二维码
        public string GeneratedQR(string pUrl, string pVipID)
        {
            string res = "";

            var qrcode = new StringBuilder();
            qrcode.AppendFormat("{0}", pUrl);
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeScale = 5;
            qrCodeEncoder.QRCodeVersion = 0;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            Image qrImage = qrCodeEncoder.Encode(qrcode.ToString(), Encoding.UTF8);
            Image bitmap = new System.Drawing.Bitmap(256, 256);
            Graphics g2 = System.Drawing.Graphics.FromImage(bitmap);
            g2.InterpolationMode = InterpolationMode.High;
            g2.SmoothingMode = SmoothingMode.HighQuality;
            g2.Clear(System.Drawing.Color.Transparent);
            g2.DrawImage(qrImage, new System.Drawing.Rectangle(0, 0, 256, 256), new System.Drawing.Rectangle(0, 0, qrImage.Width, qrImage.Height), System.Drawing.GraphicsUnit.Pixel);
            string fileName = pVipID.ToLower() + ".jpg";
            string host = ConfigurationManager.AppSettings["website_WWW"].ToString();
            if (!host.EndsWith("/")) host += "/";
            string fileUrl = host + "File/hs/images/" + fileName;
            string newFilePath = string.Empty;
            string newFilename = string.Empty;
            string path = HttpContext.Current.Server.MapPath("../../images/qrcode.jpg");
            System.Drawing.Image imgSrc = System.Drawing.Image.FromFile(path);
            System.Drawing.Image imgWarter = bitmap;
            using (Graphics g = Graphics.FromImage(imgSrc))
            {
                g.DrawImage(imgWarter, new Rectangle(0, 0, imgWarter.Width, imgWarter.Height), 0, 0, imgWarter.Width, imgWarter.Height, GraphicsUnit.Pixel);
            }
            newFilePath = string.Format("../../File/hs/images/{0}", fileName);
            newFilename = HttpContext.Current.Server.MapPath(newFilePath);
            imgSrc.Save(newFilename, System.Drawing.Imaging.ImageFormat.Jpeg);
            imgWarter.Dispose();
            imgSrc.Dispose();
            qrImage.Dispose();
            bitmap.Dispose();
            g2.Dispose();

            res = fileUrl;
            return res;
        }
        #endregion

        #region 根据日期生成订单号
        public string GetOrdersNo()
        {
            Random ro = new Random();//得到随机数 
            string OdNo = "HS" + DateTime.Now.Year.ToString()
            + DateTime.Now.Month.ToString()
            + DateTime.Now.Day.ToString()
            + DateTime.Now.Hour.ToString()
            + DateTime.Now.Minute.ToString()
            + DateTime.Now.Second.ToString()
            + ro.Next(10000).ToString();//订单号
            return OdNo;
        }
        #endregion

        #region UpLoadAttachment
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="files"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string UpLoadAttachment(HttpPostedFile files, out string msg)
        {
            msg = "";
            string filename = "";
            string fileName = "";
            HttpPostedFile postedFile = files;
            if (postedFile != null && postedFile.FileName != null && postedFile.FileName != "")
            {
                filename = postedFile.FileName;
                string suffixname = "";
                if (filename != null)
                {
                    suffixname = filename.Substring(filename.LastIndexOf(".")).ToLower();
                }
                string tempPath = "/File/hs/images/";
                fileName = DateTime.Now.ToString("yyyy.MM.dd.mm.ss.ffffff") + suffixname;
                string savepath = HttpContext.Current.Server.MapPath(tempPath);
                if (!Directory.Exists(savepath))
                {
                    Directory.CreateDirectory(savepath);
                }
                postedFile.SaveAs(savepath + @"/" + fileName);//保存
                msg = "{code:200,content:'" + ConfigurationManager.AppSettings["customer_service_url"].ToString().TrimEnd('/') + tempPath + fileName + "'}";
            }
            else
            {
                msg = "{code:300,content:'请上传.jpg,.png,.gif文件'}";
            }
            return fileName;
        }
        #endregion
    }
}