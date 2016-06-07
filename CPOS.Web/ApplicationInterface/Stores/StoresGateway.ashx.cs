/*
 * Author		:Alex.Tian
 * EMail		:changjian.tian@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/7/14 17:00
 * Description	:门店接口
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
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.BLL.WX;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Common;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.Web.OnlineShopping.data;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using System.Drawing;
using System.Globalization;

namespace JIT.CPOS.Web.ApplicationInterface.Stores
{
    /// <summary>
    /// StoresGateway 的摘要说明
    /// </summary>
    public class StoresGateway : BaseGateway
    {

        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst;
            switch (pAction)
            {
                case "getDimensionalCode":
                    rst = getDimensionalCode(pRequest);  //获取二维码(动态/静态)
                    break;
                case "getDimensionalCodeByVipInfo":
                    rst = getDimensionalCodeByVipInfo(pRequest);  //根据动态二维码获取用户信息
                    break;
                case "setVirtualOrderInfo":   //提交虚拟订单 并修改动态二维码中的消费金额。订单ID
                    rst = setVirtualOrderInfo(pRequest);
                    break;
                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                    {
                        ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                    };
            }
            return rst;
        }
        #region 获取动态/静态二维码
        public string getDimensionalCode(string pRequest)
        {
            string content = string.Empty;
            string customerId = string.Empty;
            var RD = new DimensionalCodeRD();
            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            try
            {
                #region 解析传入参数
                //解析请求字符串
                var RP = pRequest.DeserializeJSONTo<APIRequest<DimensionalCodeRP>>();

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(RP.CustomerID))
                {
                    customerId = RP.CustomerID;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, RP.UserID);

                #endregion
                var imageUrl = string.Empty;
                Random ro = new Random();
                var iUp = 100000000;
                var iDown = 50000000;

                if (string.IsNullOrEmpty(RP.Parameters.VipDCode.ToString("")))
                {
                    rsp.ResultCode = 302;
                    rsp.Message = "参数 VipDCode 不能为空";
                    return rsp.ToJSON();
                }
                var rpVipDCode = 0;                 //临时二维码
                var iResult = ro.Next(iDown, iUp);  //随机数

                string objectid = string.IsNullOrEmpty(RP.Parameters.ObjectID) ? RP.UserID : RP.Parameters.ObjectID;
                if (RP.Parameters.VipDCode == 9)    //永久二维码
                {
                    var userQrCodeBll = new WQRCodeManagerBLL(loggingSessionInfo);
                    //if (!string.IsNullOrWhiteSpace(RP.Parameters.QRCodeId)) //如果是分享二维码，QRCodeId是有值的
                    //{
                        
                    //    var userQrCode = userQrCodeBll.GetByID(RP.Parameters.QRCodeId);

                    //    if (userQrCode != null )
                    //    {
                    //        RD.imageUrl = userQrCode.ImageUrl;
                    //        RD.paraTmp = userQrCode.QRCode;
                    //        return rsp.ToJSON();
                    //    }
                    //}
                    //else
                    //{
                        var userQrCode = userQrCodeBll.QueryByEntity(new WQRCodeManagerEntity() { ObjectId = objectid }, null);

                        if (userQrCode != null && userQrCode.Length > 0)
                        {
                            RD.imageUrl = userQrCode[0].ImageUrl;
                            RD.paraTmp = userQrCode[0].QRCode;
                            return rsp.ToJSON();
                        }
                    //}

                    //获取当前二维码 最大值
                    iResult = new WQRCodeManagerBLL(loggingSessionInfo).GetMaxWQRCod() + 1;
                    rpVipDCode = 1;                 //永久
                }


                #region 获取微信帐号
                //门店关联的公众号
                var tuBll = new t_unitBLL(loggingSessionInfo);
                var tuEntity = new t_unitEntity();
                if (!string.IsNullOrEmpty(RP.Parameters.unitId))
                {
                    tuEntity = tuBll.QueryByEntity(new t_unitEntity() { unit_id = RP.Parameters.unitId }, null).FirstOrDefault();
                }

                var server = new WApplicationInterfaceBLL(loggingSessionInfo);
                var wxObj = new WApplicationInterfaceEntity();
                if (tuEntity != null && !string.IsNullOrEmpty(tuEntity.weiXinId))
                {
                    wxObj = server.QueryByEntity(new WApplicationInterfaceEntity { WeiXinID = tuEntity.weiXinId, CustomerId = customerId }, null).FirstOrDefault();
                }
                else
                {
                    wxObj = server.QueryByEntity(new WApplicationInterfaceEntity { CustomerId = customerId }, null).FirstOrDefault();
                }

                if (wxObj == null)
                {
                    rsp.ResultCode = 302;
                    rsp.Message = "不存在对应的微信帐号";
                    return rsp.ToJSON().ToString();
                }
                else
                {
                    var commonServer = new CommonBLL();

                    imageUrl = commonServer.GetQrcodeUrl(wxObj.AppID
                        , wxObj.AppSecret
                        , rpVipDCode.ToString("")//二维码类型  0： 临时二维码  1：永久二维码
                        , iResult, loggingSessionInfo);//iResult作为场景值ID，临时二维码时为32位整型，永久二维码时只支持1--100000
                        //"https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=gQGN7zoAAAAAAAAAASxodHRwOi8vd2VpeGluLnFxLmNvbS9xL1dreENCS1htX0xxQk94SEJ6MktIAAIEUk88VwMECAcAAA==";
                        //"https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=gQGN7zoAAAAAAAAAASxodHRwOi8vd2VpeGluLnFxLmNvbS9xL1dreENCS1htX0xxQk94SEJ6MktIAAIEUk88VwMECAcAAA==";
                    if (imageUrl != null && !imageUrl.Equals(""))
                    {
                        CPOS.Common.DownloadImage downloadServer = new DownloadImage();
                        string downloadImageUrl = ConfigurationManager.AppSettings["website_WWW"];
                        imageUrl = downloadServer.DownloadFile(imageUrl, downloadImageUrl);
                        //如果名称不为空，就把图片放在一定的背景下面
                        if (!string.IsNullOrEmpty(RP.Parameters.RetailTraderName))
                        {
                            string apiDomain = ConfigurationManager.AppSettings["website_url"];

                            imageUrl = CombinImage(apiDomain + @"/HeadImage/qrcodeBack.jpg", imageUrl, RP.Parameters.RetailTraderName + "合作二维码");
                        }


                    }
                }

                #endregion


                if (RP.Parameters.VipDCode == 9 && !string.IsNullOrEmpty(imageUrl))    //永久二维码
                {
                    #region 创建店员永久二维码匹配表
                    var userQrTypeBll = new WQRCodeTypeBLL(loggingSessionInfo);
                    var userQrType = userQrTypeBll.QueryByEntity(new WQRCodeTypeEntity() { TypeCode = RP.Parameters.TypeCode }, null);//"UserQrCode"
                    if (userQrType != null && userQrType.Length > 0)
                    {
                        var userQrcodeBll = new WQRCodeManagerBLL(loggingSessionInfo);
                        var userQrCode = new WQRCodeManagerEntity();
                        userQrCode.QRCode = iResult.ToString();//记录传过去的二维码场景值
                        userQrCode.QRCodeTypeId = userQrType[0].QRCodeTypeId;
                        userQrCode.IsUse = 1;
                        userQrCode.CustomerId = loggingSessionInfo.ClientID;
                        userQrCode.ImageUrl = imageUrl;
                        userQrCode.ApplicationId = wxObj.ApplicationId;
                        //objectId 为店员ID
                        userQrCode.ObjectId = objectid;
                        userQrcodeBll.Create(userQrCode);
                    }
                    #endregion
                }
                else
                {
                    #region 创建临时匹配表
                    VipDCodeBLL vipDCodeServer = new VipDCodeBLL(loggingSessionInfo);
                    VipDCodeEntity info = new VipDCodeEntity();
                    info.DCodeId = iResult.ToString();//记录传过去的二维码场景值****（保存到数据库时没加空格）
                    info.CustomerId = customerId;
                    VipBLL vipBll = new VipBLL(loggingSessionInfo);
                    info.UnitId = string.IsNullOrEmpty(RP.Parameters.unitId) ? vipBll.GetUnitByUserId(RP.UserID) : RP.Parameters.unitId; //获取会集店
                    info.Status = "0";
                    info.IsReturn = 0;
                    info.DCodeType = RP.Parameters.VipDCode; // add by donal 2014-9-22 10:02:08
                    loggingSessionInfo.UserID = ToStr(RP.UserID);
                    info.CreateBy = RP.UserID;
                    info.ImageUrl = imageUrl;
                    //info.VipId = RP.UserID;
                    info.ObjectId = RP.Parameters.ShareUserId;//分享经销商的vipid
                    vipDCodeServer.Create(info);
                    #endregion
                }


                RD.imageUrl = imageUrl;
                if (RP.Parameters.VipDCode == 9)
                    RD.paraTmp = iResult.ToString("");
                else
                    RD.paraTmp = iResult.ToString().Insert(4, " "); //加空格，加空格有什么作用？

                if (RP.Parameters.VipDCode == 3)
                {
                    VipBLL vipBll = new VipBLL(loggingSessionInfo);
                    var VipData = vipBll.GetByID(RP.UserID);
                    if (VipData != null)
                    {
                        RD.VipName = VipData.VipName;
                        RD.HeadImgUrl = VipData.HeadImgUrl;
                    }
                }
            }
            catch (Exception ex)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("获取二维码出错:{0}", ex.Message)
                });
                rsp.ResultCode = 303;
                rsp.Message = "数据库操作错误";

            }

            content = rsp.ToJSON();

            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("setSignUp content: {0}", content)
            });
            return content;
        }
        public string ToStr(string obj)
        {
            if (obj == null) return string.Empty;
            return obj.ToString();
        }

        public class DimensionalCodeRD : IAPIResponseData
        {
            public string imageUrl { get; set; }
            public string paraTmp { get; set; }

            public string VipName { get; set; }
            public string HeadImgUrl { get; set; }
        }


        public class DimensionalCodeRP : IAPIRequestParameter
        {
            public string unitId { get; set; }

            /// <summary>
            /// 二维码类型
            /// </summary>
            public int VipDCode { get; set; } // add by donal 2014-9-22 09:57:46

            public string TypeCode { get; set; }
            public string ObjectID { get; set; }

            public string QRCodeId { get; set; }//二维码ID（分享）
            public string RetailTraderName { get; set; }//分销商名称

            public string ShareUserId { get; set; }//分享经销商的vipid
            public void Validate()
            {

            }
        }
        #endregion

        #region  根据动态二维码获取用户信息，只有 一个参数paraTmp，没有会员的标识
        public string getDimensionalCodeByVipInfo(string pRequest)
        {
            string content = string.Empty;
            string customerId = string.Empty;
            var RD = new getPollRespData();
            getPollRespContentData temp = new getPollRespContentData();
            var rsp = new SuccessResponse<IAPIResponseData>();
            try
            {
                #region

                //解析请求字符串
                var RP = pRequest.DeserializeJSONTo<APIRequest<getPollReqData>>();

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(RP.CustomerID))
                {
                    customerId = RP.CustomerID;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                if (string.IsNullOrWhiteSpace(RP.Parameters.special.paraTmp))
                {
                    rsp.ResultCode = 302;
                    rsp.Message = "paraTmp不能为空";
                    return rsp.ToJSON().ToString();
                }

                #endregion

                #region 创建临时匹配表

                VipDCodeBLL vipDCodeServer = new VipDCodeBLL(loggingSessionInfo);
                VipDCodeEntity info = new VipDCodeEntity();
                //由于CodeId有重复的概率，因此只取出最新的一条记录
                info = vipDCodeServer.QueryByEntity(
                    new VipDCodeEntity() { DCodeId = ToStr(RP.Parameters.special.paraTmp.Replace(" ", "")) }//又去掉了中间的空格
                    , new OrderBy[] { new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc } }
                    )[0];
                string status = string.Empty;
                string vipId = string.Empty;
                string openId = string.Empty;
                if (info == null || info.DCodeId == null)
                {
                    rsp.ResultCode = 303;
                    rsp.Message = "不存在对应的记录";
                    return rsp.ToJSON().ToString();
                }
                else
                {
                    status = info.Status;
                    openId = info.OpenId;
                    vipId = info.VipId;
                    //VipBLL vipServer = new VipBLL(loggingSessionInfo);
                    //var vipObj = vipServer.QueryByEntity(new VipEntity
                    //{
                    //    WeiXinUserId = info.OpenId
                    //    ,
                    //    IsDelete = 0
                    //}, null);
                    //if (vipObj == null && vipObj.Length == 0)
                    //{
                    //}
                    //else
                    //{
                    //    vipId = ToStr(vipObj[0].VIPID);
                    //}
                }
                temp.status = status;
                temp.userId = vipId;
                temp.openId = openId;
                RD.content = temp;
                rsp = new SuccessResponse<IAPIResponseData>(RD);
                if ((RP.Parameters.special.Mode == null || (!string.IsNullOrEmpty(RP.Parameters.special.Mode) && RP.Parameters.special.Mode.Equals("Inbound"))) && !string.IsNullOrEmpty(info.VipId))
                {
                    VipBLL vipBll = new VipBLL(loggingSessionInfo);
                    var unitBll = new t_unitBLL(loggingSessionInfo);
                    var UserBll = new T_UserBLL(loggingSessionInfo);

                    var vipInfo = vipBll.GetByID(info.VipId);
                    var tt = vipBll.GetUnitByUserId(RP.UserID);//获取员工的会集店****
                    var UserEntity = UserBll.GetByID(vipInfo.SetoffUserId);//当前会员集客员工
                    var tempUnit = unitBll.GetByID(vipInfo.CouponInfo);//获取会员目前的会籍店
                    //UnitService unitServer = new UnitService(loggingSessionInfo);

                    //
                    string UserStatus = "";
                    if (UserEntity != null)
                        UserStatus = UserEntity.user_status;
                    
                    #region 会员会籍店、集客员工变动处理
                    //string.IsNullOrWhiteSpace(vipInfo.CouponInfo) || string.IsNullOrWhiteSpace(vipInfo.SetoffUserId) 目前未用到
                    if (string.IsNullOrWhiteSpace(vipInfo.HigherVipID) && string.IsNullOrWhiteSpace(vipInfo.SetoffUserId) && string.IsNullOrWhiteSpace(vipInfo.Col20))
                    {//当会员会籍店、集客员工为空时
                        if (!string.IsNullOrEmpty(tt))
                        {
                            vipInfo.CouponInfo = tt;//设为门店
                            vipInfo.SetoffUserId = RP.UserID;//设为门店员工
                            vipInfo.Col21 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//集客时间*****
                        }
                        if (vipInfo != null && vipInfo.SetoffUserId == RP.UserID)
                        {
                            rsp.Message = "恭喜你集客成功。会员需要用心经营才会有订单哦！";
                        }
                    }
                    else
                    {
                        if (vipInfo != null && !string.IsNullOrEmpty(vipInfo.CouponInfo) && vipInfo.SetoffUserId != RP.UserID)
                        {
                            //rsp.Message = "此客户已是会员，无需再集客。老会员更要服务好哦！";
                        }
                        else if (vipInfo != null && vipInfo.SetoffUserId == RP.UserID && !string.IsNullOrEmpty(vipInfo.Col21) && Convert.ToDateTime(vipInfo.Col21).AddSeconds(3) < DateTime.Now)  //col21：员工集客/或者分销商集客时间
                        {
                            //rsp.Message = "此客户此前已经被您集客，无需重复集客。！";
                        } 
                    }
                    if (UserStatus.Trim().Equals("-1"))
                    {// 当前会员的集客员工离职时
                        if (!string.IsNullOrEmpty(tt))
                        {
                            vipInfo.CouponInfo = tt;//设为门店
                            vipInfo.SetoffUserId = RP.UserID;//设为门店员工
                            vipInfo.Col21 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//集客时间*****
                        }
                    }
                    else if (tempUnit!=null&&tempUnit.type_id == "2F35F85CF7FF4DF087188A7FB05DED1D")//是总部标识
                    {// 总部会员可以被门店集客
                        if (!string.IsNullOrEmpty(tt))
                        {
                            vipInfo.CouponInfo = tt;//设为门店
                            vipInfo.SetoffUserId = RP.UserID;//设为门店员工
                            vipInfo.Col21 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//集客时间*****
                        }
                    }
                    #endregion

                    #region 橙果财商
                    if (info.DCodeType == 3)
                    {
                        if (string.IsNullOrWhiteSpace(vipInfo.HigherVipID))
                        {
                            //if (!vipInfo.HigherVipID.Equals(RP.UserID))
                            //{
                            vipInfo.HigherVipID = RP.UserID;
                            vipInfo.Col21 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//集客时间*****
                            //}
                        }
                    }
                    #endregion



                    //if (vipInfo != null && !string.IsNullOrEmpty(vipInfo.CouponInfo) && vipInfo.SetoffUserId != RP.UserID)
                    //{
                    //    rsp.Message = "此客户已是会员，无需再集客。老会员更要服务好哦！";
                    //}
                    //if (vipInfo != null && vipInfo.SetoffUserId == RP.UserID && !string.IsNullOrEmpty(vipInfo.Col21) && Convert.ToDateTime(vipInfo.Col21).AddMinutes(3) < DateTime.Now)  //col21：员工集客/或者分销商集客时间
                    //{
                    //    rsp.Message = "此客户此前已经被您集客，无需重复集客。！";
                    //}
                    //else if (vipInfo != null && vipInfo.SetoffUserId == RP.UserID)
                    //{
                    //    rsp.Message = "恭喜你集客成功。会员需要用心经营才会有订单哦！";
                    //}
                      
                    
                    vipBll.Update(vipInfo);

                }
                //处理面对面、优惠券、海报的集客处理
                if (!string.IsNullOrEmpty(RP.Parameters.special.Mode) && (RP.Parameters.special.Mode.Equals("Coupon") || RP.Parameters.special.Mode.Equals("SetOffPoster") || RP.Parameters.special.Mode.Equals("ToFace")) && !string.IsNullOrEmpty(info.VipId))//如果通过优惠券或海报扫码
                {
                    VipBLL vipBll = new VipBLL(loggingSessionInfo);
                    var unitBll = new t_unitBLL(loggingSessionInfo);
                    var UserBll = new T_UserBLL(loggingSessionInfo);

                    var vipInfo = vipBll.GetByID(info.VipId);
                    var tt = vipBll.GetUnitByUserId(RP.UserID);//获取员工的会集店****
                    var UserEntity = UserBll.GetByID(vipInfo.SetoffUserId);//当前会员集客员工
                    var tempUnit = unitBll.GetByID(vipInfo.CouponInfo);//获取会员目前的会籍店
                    //UnitService unitServer = new UnitService(loggingSessionInfo);

                    //
                    string UserStatus = "";
                    if (UserEntity != null)
                        UserStatus = UserEntity.user_status;
                    string PlatformType = RP.Parameters.special.PlatformType;
                    #region 会员会籍店、集客员工变动处理
                    //当会员的HigherVipID、SetoffUserId、Col20都为空时可以进行处理
                    if (string.IsNullOrWhiteSpace(vipInfo.HigherVipID) && string.IsNullOrWhiteSpace(vipInfo.SetoffUserId) && string.IsNullOrWhiteSpace(vipInfo.Col20))
                    {
                        var HigherVipInfo = vipBll.QueryByEntity(new VipEntity() { ClientID=loggingSessionInfo.CurrentUser.customer_id,VIPID=RP.UserID},null).FirstOrDefault();
                        if (PlatformType != "" && PlatformType != null)//当为员工或会员时
                        {
                            switch (PlatformType)
                            {
                                case "1":
                                    vipInfo.SetoffUserId = RP.UserID;//设为门店员工
                                    vipInfo.Col23 = "1";
                                    if (!string.IsNullOrEmpty(tt))
                                    {
                                        vipInfo.CouponInfo = tt;//设为门店  1=员工;2=客服;3=会员;
                                    }
                                    break;
                                case "2":
                                    vipInfo.SetoffUserId = RP.UserID;//设为2=客服
                                    vipInfo.Col23 = "2";
                                    if (!string.IsNullOrEmpty(tt))
                                    {

                                        vipInfo.CouponInfo = tt;//设为门店  1=员工;2=客服;3=会员;
                                    }
                                    break;
                                case "3":
                                    vipInfo.HigherVipID = RP.UserID;//设为3=会员
                                    if (HigherVipInfo != null&&!string.IsNullOrEmpty(HigherVipInfo.CouponInfo))
                                    {
                                        vipInfo.CouponInfo = HigherVipInfo.CouponInfo;
                                    }
                                    vipInfo.Col23 = "3";
                                    break;
                                default:
                                    if (HigherVipInfo != null && !string.IsNullOrEmpty(HigherVipInfo.CouponInfo))
                                    {
                                        vipInfo.CouponInfo = HigherVipInfo.CouponInfo;
                                    }
                                    vipInfo.SetoffUserId = RP.UserID;
                                    break;
                            }
                        }
                        if (RP.Parameters.special.Mode.Equals("Coupon"))//如果是优惠券根据给定的VipSourceId=27给定
                        {
                            vipInfo.VipSourceId = "27";
                            vipInfo.VipSourceName = "优惠券二维码";
                        }
                        if (RP.Parameters.special.Mode.Equals("SetoffPoster"))//如果是海报根据给定的VipSourceId=29给定
                        {
                            vipInfo.VipSourceId = "29";
                            vipInfo.VipSourceName = "集客海报二维码";
                        }
                        vipInfo.Col24 = RP.Parameters.special.ObjectID;
                        vipInfo.Col21 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//集客时间*****         
                        //如果集客成功给出提示
                        if (vipInfo != null && ((!string.IsNullOrEmpty(vipInfo.CouponInfo) && vipInfo.SetoffUserId == RP.UserID) || (!string.IsNullOrEmpty(vipInfo.HigherVipID) && vipInfo.HigherVipID == RP.UserID)))
                        {
                            if (PlatformType == "1" || PlatformType == "2")
                            {
                                rsp.ResultCode = 306;
                                rsp.Message = "恭喜你集客成功。会员需要用心经营才会有订单哦！";
                            }
                            else
                            {
                                rsp.ResultCode = 307;
                                rsp.Message = "恭喜您集客成功，集客注册成功后才能获得奖励哦!";
                            }

                        }
                    }
                    else
                    {
                        //如果已经集客给出提示
                        if (vipInfo != null && ((!string.IsNullOrEmpty(vipInfo.CouponInfo) && vipInfo.SetoffUserId != RP.UserID) || (!string.IsNullOrEmpty(vipInfo.HigherVipID) && vipInfo.HigherVipID != RP.UserID)))
                        {
                            rsp.ResultCode = 312;
                            rsp.Message = "此客户已是会员，无需再集客。老会员更要服务好哦！";
                        }
                        else if (vipInfo != null && ((!string.IsNullOrEmpty(vipInfo.CouponInfo) && vipInfo.SetoffUserId == RP.UserID) || (!string.IsNullOrEmpty(vipInfo.HigherVipID) && vipInfo.HigherVipID == RP.UserID)) && !string.IsNullOrEmpty(vipInfo.Col21) && Convert.ToDateTime(vipInfo.Col21).AddSeconds(3) < DateTime.Now)  //col21：员工集客/或者分销商集客时间
                        {
                            rsp.ResultCode = 313;
                            rsp.Message = "此客户此前已经被您集客，无需重复集客。！";
                        }
                    }
                    if (UserStatus.Trim().Equals("-1"))//判断员工状态
                    {// 当前会员的集客员工离职时

                        if (PlatformType != "" && PlatformType != null)//1=员工;2=客服;3=会员;
                        {
                            switch (PlatformType)
                            {
                                case "1":
                                    vipInfo.SetoffUserId = RP.UserID;//设为门店员工
                                    vipInfo.Col23 = "1";
                                    if (!string.IsNullOrEmpty(tt))
                                    {
                                        vipInfo.CouponInfo = tt;//设为门店  
                                    }
                                    break;
                                case "2":
                                    vipInfo.SetoffUserId = RP.UserID;//设为2=客服
                                    vipInfo.Col23 = "2";
                                    if (!string.IsNullOrEmpty(tt))
                                    {
                                        vipInfo.CouponInfo = tt;//设为门店  1=员工;2=客服;3=会员;
                                    }
                                    break;
                                case "3":
                                    vipInfo.HigherVipID = RP.UserID;//设为3=会员
                                    vipInfo.Col23 = "3";
                                    break;
                                default:
                                    vipInfo.SetoffUserId = RP.UserID;
                                    break;
                            }
                        }
                        if (RP.Parameters.special.Mode.Equals("Coupon"))
                        {
                            vipInfo.VipSourceId = "27";
                            vipInfo.VipSourceName = "优惠券二维码";
                        }
                        if (RP.Parameters.special.Mode.Equals("SetOffPoster"))
                        {
                            vipInfo.VipSourceId = "29";
                            vipInfo.VipSourceName = "集客海报二维码";
                        }
                        if (RP.Parameters.special.Mode.Equals("ToFace"))
                        {
                            vipInfo.VipSourceId = "13";
                            vipInfo.VipSourceName = "员工集客";
                        }
                        vipInfo.Col24 = RP.Parameters.special.ObjectID;
                        vipInfo.Col21 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//集客时间*****

                    }
                    else if (tempUnit != null && tempUnit.type_id == "2F35F85CF7FF4DF087188A7FB05DED1D")//是总部标识
                    {
                        if (PlatformType != "" && PlatformType != null)
                        {
                            switch (PlatformType)
                            {
                                case "1":
                                    if (!string.IsNullOrEmpty(tt))//需判断当前员工或客服所属门店是否为空
                                    {
                                        vipInfo.CouponInfo = tt;//设为门店  1=员工;2=客服;3=会员;
                                    }
                                    vipInfo.SetoffUserId = RP.UserID;//设为门店员工
                                    vipInfo.Col23 = "1";
                                    break;
                                case "2":
                                    if (!string.IsNullOrEmpty(tt))
                                    {
                                        vipInfo.CouponInfo = tt;//设为门店  1=员工;2=客服;3=会员;
                                    }
                                    vipInfo.SetoffUserId = RP.UserID;//设为2=客服
                                    vipInfo.Col23 = "2";
                                    break;
                                case "3":
                                    vipInfo.HigherVipID = RP.UserID;//设为3=会员
                                    vipInfo.Col23 = "3";
                                    break;
                                default:
                                    vipInfo.SetoffUserId = RP.UserID;
                                    break;
                            }
                        }
                        if (RP.Parameters.special.Mode.Equals("Coupon"))
                        {
                            vipInfo.VipSourceId = "27";
                            vipInfo.VipSourceName = "优惠券二维码";
                        }
                        if (RP.Parameters.special.Mode.Equals("SetoffPoster"))
                        {
                            vipInfo.VipSourceId = "29";
                            vipInfo.VipSourceName = "集客海报二维码";
                        }
                        if (RP.Parameters.special.Mode.Equals("ToFace"))
                        {
                            vipInfo.VipSourceId = "13";
                            vipInfo.VipSourceName = "员工集客";
                        }
                        vipInfo.Col24 = RP.Parameters.special.ObjectID;
                        vipInfo.Col21 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//集客时间*****

                    }
                    #endregion                  
                    
                    vipBll.Update(vipInfo);
                }
                #endregion
            }
            catch (Exception ex)
            {
                rsp.ResultCode = 103;
                rsp.Message = "数据库操作错误";
                rsp.Message = ex.ToString();
            }
            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("setSignUp content: {0}", temp)
            });
            content = rsp.ToJSON();
            return content;
        }
        public class getPollRespData : IAPIResponseData
        {
            public getPollRespContentData content { get; set; }
        }

        public class getPollRespContentData
        {
            public string status { get; set; }
            public string userId { get; set; }
            public string openId { get; set; }
        }

        public class getPollReqData : IAPIRequestParameter
        {
            public getPollReqSpecialData special;

            public void Validate()
            {

            }
        }

        public class getPollReqSpecialData
        {
            public string unitId { get; set; }
            public string paraTmp { get; set; }
            public string Mode { get; set; }
            public string PlatformType { get; set; }//1=员工;2=客服;3=会员;类型
            public string ObjectID { get; set; }//优惠券或集客海报对象ID

        }
        #endregion

        #region 提交订单
        /// <summary>
        /// 提交虚拟订单 并且修改微信动态二维码中的对应该客户的消费金额。是否返现
        /// </summary>
        /// <returns></returns>
        public string setVirtualOrderInfo(string pRequest)
        {
            string content = string.Empty;
            string customerId = string.Empty;
            var RD = new setVirtualOrderInfoRespData();
            var rsp = new SuccessResponse<IAPIResponseData>();
            try
            {

                #region 解析请求字符串
                var RP = pRequest.DeserializeJSONTo<APIRequest<setVirtualOrderInfoReqData>>();
                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(RP.CustomerID))
                {
                    customerId = RP.CustomerID;
                }
                if (string.IsNullOrWhiteSpace(RP.CustomerID))
                {
                    rsp.ResultCode = 301;
                    rsp.Message = "客户标识不能为空";
                    return rsp.ToJSON().ToString();
                }
                if (string.IsNullOrWhiteSpace(RP.UserID))
                {
                    rsp.ResultCode = 302;
                    rsp.Message = "用户标识不能为空";
                    return rsp.ToJSON().ToString();
                }

                if (string.IsNullOrWhiteSpace(RP.Parameters.amount))
                {
                    rsp.ResultCode = 303;
                    rsp.Message = "金额不能为空";
                    return rsp.ToJSON().ToString();
                }

                if (string.IsNullOrWhiteSpace(RP.Parameters.dataFromId))
                {
                    rsp.ResultCode = 304;
                    rsp.Message = "来源不能为空 2=Pad，3=微信";
                    return rsp.ToJSON().ToString();
                }
                if (!string.IsNullOrWhiteSpace(RP.Parameters.DcodeId))
                {
                    RP.Parameters.DcodeId = RP.Parameters.DcodeId.Replace(" ", "").Trim();
                }
                if (string.IsNullOrEmpty(RP.Parameters.VipId))
                {
                    rsp.ResultCode = 305;
                    rsp.Message = "会员信息不能为空";
                    return rsp.ToJSON().ToString();
                }

                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                //判断订单号是否重复
                var InoutBLL = new T_InoutBLL(loggingSessionInfo);
                var Result = InoutBLL.QueryByEntity(new T_InoutEntity() { order_no = RP.Parameters.OffOrderNo }, null).ToList();
                if (Result.Count() > 0)
                {
                    rsp.ResultCode = 305;
                    rsp.Message = "订单号重复，请重新输入！";
                    return rsp.ToJSON().ToString();
                }

                #endregion
                #region
                string strError = string.Empty;
                InoutService server = new InoutService(loggingSessionInfo);
                var tran = server.GetTran();
                //  System.Data.SqlClient.SqlTransaction tran = new System.Data.SqlClient.SqlTransaction();

                using (tran.Connection)//事务
                {
                    try
                    {
                        WXSalesPolicyRateBLL SalesPolicybll = new WXSalesPolicyRateBLL(loggingSessionInfo);
                        if (string.IsNullOrWhiteSpace(RP.Parameters.orderId))
                        {
                            RP.Parameters.orderId = Common.Utils.NewGuid();//生成虚拟订单
                            string unitId = SalesPolicybll.GetUnitIDByUserId(RP.UserID, customerId);
                            var result = server.SetVirtualOrderInfo(ToStr(RP.Parameters.orderId)
                                                          , ToStr(RP.CustomerID)
                                                          , ToStr(unitId)
                                                          , ToStr(RP.Parameters.VipId)
                                                          , ToStr(RP.Parameters.dataFromId)
                                                          , ToStr(RP.Parameters.amount)
                                                          , ToStr(RP.Parameters.OffOrderNo), ToStr(RP.Parameters.remark), ToStr(RP.UserID));
                            if (result.Equals("1"))
                            {
                                VipDCodeBLL bll = new VipDCodeBLL(loggingSessionInfo);

                                if (!string.IsNullOrWhiteSpace(RP.Parameters.DcodeId))
                                {
                                    var temp = bll.GetByID(RP.Parameters.DcodeId);
                                    if (temp != null)
                                    {
                                        decimal? ReturnAmount = 0;
                                        string PushInfo = string.Empty;
                                        VipDCodeEntity entity = new VipDCodeEntity();
                                        entity = temp;
                                        entity.SalesAmount = Convert.ToDecimal(RP.Parameters.amount);
                                        entity.ObjectId = RP.Parameters.orderId;
                                        entity.DCodeId = RP.Parameters.DcodeId;
                                        if (RP.Parameters.Isscan.Trim().ToString().Equals("1"))
                                        {
                                            string strErrormessage = "";
                                            DataSet ds = SalesPolicybll.getReturnAmount(Convert.ToDecimal(RP.Parameters.amount.ToString()), customerId);

                                            VipCardVipMappingBLL vipCardVipMappingBll = new VipCardVipMappingBLL(loggingSessionInfo);
                                            VipCardBLL vipCardBll = new VipCardBLL(loggingSessionInfo);
                                            VipCardRuleBLL vipCardRuleBll = new VipCardRuleBLL(loggingSessionInfo);
                                            var vipCardVipMapping = vipCardVipMappingBll.QueryByEntity(new VipCardVipMappingEntity() { VIPID = RP.Parameters.VipId }, new OrderBy[] { new OrderBy(){ Direction = OrderByDirections.Desc,FieldName = "CreateTime"}});
                                            if (vipCardVipMapping.Length != 0)
                                            {
                                                var vipCard = vipCardBll.GetByID(vipCardVipMapping[0].VipCardID);
                                                var vipCardRule = vipCardRuleBll.QueryByEntity(new VipCardRuleEntity() { VipCardTypeID = vipCard.VipCardTypeID }, null);
                                                if (vipCardRule.Length != 0)
                                                {
                                                    double returnAmountPer = Convert.ToDouble(vipCardRule[0].ReturnAmountPer) * 0.01;
                                                    ReturnAmount = entity.ReturnAmount = entity.SalesAmount * Convert.ToDecimal(returnAmountPer);
                                                }
                                                else
                                                {
                                                    ReturnAmount = 0;
                                                } 
                                            }
            
                                            PushInfo = ds.Tables[1].Rows[0]["PushInfo"].ToString();
                                            //if (ds != null && ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
                                            //{
                                            //    throw new APIException("该客户没有配置策略信息") { ErrorCode = 250 };
                                            //}
                                            //if (ds != null && ds.Tables[0].Rows.Count > 0)
                                            //{
                                            //    string m_ReturnAmount = ds.Tables[0].Rows[0]["ReturnAmount"].ToString();
                                            //    if (!string.IsNullOrWhiteSpace(m_ReturnAmount))
                                            //    {
                                            //        //返现金额
                                            //        ReturnAmount = entity.ReturnAmount = Convert.ToDecimal(ds.Tables[0].Rows[0]["ReturnAmount"].ToString());
                                            //    }
                                            //    else
                                            //    {
                                            //        //返现金额
                                            //        ReturnAmount = entity.ReturnAmount = 0;
                                            //    }
                                            //    //返现消息内容
                                            //    PushInfo = ds.Tables[0].Rows[0]["PushInfo"].ToString();
                                            //}
                                            //else
                                            //{
                                            //    string m_ReturnAmountTwo = ds.Tables[1].Rows[0]["ReturnAmount"].ToString();
                                            //    if (!string.IsNullOrWhiteSpace(m_ReturnAmountTwo))
                                            //    {
                                            //        //返现金额
                                            //        ReturnAmount = entity.ReturnAmount = Convert.ToDecimal(ds.Tables[0].Rows[0]["ReturnAmount"].ToString());
                                            //    }
                                            //    else
                                            //    {
                                            //        //返现金额
                                            //        ReturnAmount = entity.ReturnAmount = 0;

                                            //    }
                                            //    //返现消息内容
                                            //    PushInfo = ds.Tables[1].Rows[0]["PushInfo"].ToString();
                                            //}
                                            VipAmountBLL Amountbll = new VipAmountBLL(loggingSessionInfo);
                                            if (temp.IsReturn != 1)  //当未返现的时候金额变更
                                            {
                                                if (Amountbll.SetVipAmountChange(RP.CustomerID, 2, RP.Parameters.VipId, ReturnAmount ?? 0, RP.Parameters.orderId, "门店返现", "IN", out strErrormessage, null))//tran,因为ExecuteScalar会把事务关掉 ，导致 后面无法执行
                                                {
                                                    entity.IsReturn = 1;
                                                }
                                            }
                                            else
                                            {
                                                ReturnAmount = temp.ReturnAmount;
                                            }
                                         
                                            var vipBll = new VipBLL(loggingSessionInfo);

                                            var vipEntity = vipBll.GetByID(RP.Parameters.VipId);


                                            JIT.CPOS.BS.BLL.WX.CommonBLL.StoreRebate(RP.Parameters.DcodeId.Trim(), RP.Parameters.amount, PushInfo,
                                              ReturnAmount ?? 0, RP.Parameters.VipId, vipEntity.WeiXinUserId, tran, loggingSessionInfo);//tran

                                            #region 增加抽奖信息
                                            //var rateList = SalesPolicybll.QueryByEntity(new WXSalesPolicyRateEntity { CustomerId = RP.CustomerID }, null);
                                            //if (rateList == null || rateList.Length == 0)
                                            //{
                                            //    //rateList = SalesPolicybll.QueryByEntity(new WXSalesPolicyRateEntity{CustomerId =null},null);
                                            //    rateList = SalesPolicybll.GetWxSalesPolicyRateList().ToArray();
                                            //}

                                            //if (rateList != null && rateList.Length > 0)
                                            //{

                                            //    var wxSalespolicyRateMapBll = new WXSalesPolicyRateObjectMappingBLL(loggingSessionInfo);

                                            //    var rateMappingEntity =
                                            //        wxSalespolicyRateMapBll.QueryByEntity(new WXSalesPolicyRateObjectMappingEntity { RateId = rateList[0].RateId }, null);
                                            //    if (rateMappingEntity != null && rateMappingEntity.Length > 0)
                                            //    {
                                            //        if (Convert.ToDecimal(RP.Parameters.amount) >= rateMappingEntity[0].CoefficientAmount)
                                            //        {
                                            //            if (rateMappingEntity[0].PushInfo != null)
                                            //            {
                                            //                var message = rateMappingEntity[0].PushInfo.Replace("#CustomerId#", RP.CustomerID).Replace("#EventId#", rateMappingEntity[0].ObjectId).Replace("#VipId#", RP.Parameters.VipId).Replace("#OpenId#", vipEntity.WeiXinUserId);

                                            //                WXSalesPushLogBLL PushLogbll = new WXSalesPushLogBLL(loggingSessionInfo);
                                            //                WXSalesPushLogEntity qrLog = new WXSalesPushLogEntity();
                                            //                qrLog.LogId = Guid.NewGuid();

                                            //                qrLog.OpenId = RP.OpenID;
                                            //                qrLog.VipId = RP.Parameters.VipId;
                                            //                qrLog.PushInfo = message;
                                            //                qrLog.DCodeId = content;
                                            //                qrLog.RateId = Guid.NewGuid();
                                            //                PushLogbll.Create(qrLog);

                                            //                #region 增加抽奖机会
                                            //                LEventsVipObjectBLL eventbll = new LEventsVipObjectBLL(loggingSessionInfo);
                                            //                LEventsVipObjectEntity evententity = new LEventsVipObjectEntity();
                                            //                evententity.MappingId = Guid.NewGuid().ToString("N");
                                            //                evententity.ObjectId = rateMappingEntity[0].ObjectId;
                                            //                evententity.VipId = RP.Parameters.VipId;
                                            //                evententity.EventId = rateMappingEntity[0].ObjectId;
                                            //                evententity.ObjectId = RP.Parameters.orderId;
                                            //                evententity.IsCheck = 0;
                                            //                evententity.LotteryCode = "0";
                                            //                evententity.IsLottery = 0;
                                            //                eventbll.Create(evententity);
                                            //                #endregion

                                            //                JIT.CPOS.BS.BLL.WX.CommonBLL.SendWeixinMessage(message, "1", loggingSessionInfo, vipEntity);//发送信息
                                            //            }
                                            //        }
                                            //    }


                                            //}
                                            #endregion

                                            //发送抽奖信息

                                        }
                                        bll.Update(entity, tran);
                                    }
                                }
                                rsp.ResultCode = 200;
                                rsp.Message = "操作成功";
                            }
                            else
                            {
                                rsp.ResultCode = 111;
                                rsp.Message = "后台业务错误，请联系管理员.";
                            }
                        }
                        tran.Commit();
                    }
                    catch (Exception)
                    {
                        tran.Rollback();
                        throw;
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                rsp.ResultCode = 103;
                rsp.Message = ex.ToString();
            }
            content = rsp.ToJSON();
            return content;
        }

        public class setVirtualOrderInfoRespData : IAPIResponseData
        {

        }
        public class setVirtualOrderInfoReqData : IAPIRequestParameter
        {
            public string orderId { get; set; }
            public string amount { get; set; }
            public string dataFromId { get; set; }
            public string unitId { get; set; }
            public string DcodeId { get; set; }
            public string Isscan { get; set; }  //是否扫描 0.不扫描。1.扫描
            public string VipId { get; set; }
            public string OffOrderNo { get; set; }
            public string remark { get; set; }

            public void Validate()
            {
                if (string.IsNullOrWhiteSpace(this.Isscan))
                {
                    throw new APIException("请传入参数[Isscan]是否扫描？0不扫描。1扫描") { ErrorCode = 308 };
                }
                if (this.Isscan.ToString().Trim().Equals("1"))
                {
                    if (string.IsNullOrWhiteSpace(this.VipId))
                    {
                        throw new APIException("您选择的是扫描，请传入会员ID") { ErrorCode = 309 };
                    }
                }
            }
        }
        #endregion



        public static string CombinImage(string imgBack, string destImg, string strData)
        {
            //1、上面的图片部分
            HttpWebRequest request_qrcode = (HttpWebRequest)WebRequest.Create(destImg);
            WebResponse response_qrcode = null;
            Stream qrcode_stream = null;
            response_qrcode = request_qrcode.GetResponse();
            qrcode_stream = response_qrcode.GetResponseStream();//把要嵌进去的图片转换成流


            Bitmap _bmpQrcode1 = new Bitmap(qrcode_stream);//把流转换成Bitmap
            Bitmap _bmpQrcode = new Bitmap(_bmpQrcode1, 327, 327);//缩放图片           
            //把二维码由八位的格式转为24位的
            Bitmap bmpQrcode = new Bitmap(_bmpQrcode.Width, _bmpQrcode.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb); //并用上面图片的尺寸做了一个位图
            //用上面空的位图生成了一个空的画板
            Graphics g3 = Graphics.FromImage(bmpQrcode);
            g3.DrawImageUnscaled(_bmpQrcode, 0, 0);//把原来的图片画了上去


            //2、背景部分
            HttpWebRequest request_backgroup = (HttpWebRequest)WebRequest.Create(imgBack);
            WebResponse response_keleyi = null;
            Stream backgroup_stream = null;
            response_keleyi = request_backgroup.GetResponse();
            backgroup_stream = response_keleyi.GetResponseStream();//把背景图片转换成流

            Bitmap bmp = new Bitmap(backgroup_stream);
            Graphics g = Graphics.FromImage(bmp);//生成背景图片的画板

            //3、画上文字
            //  String str = "文峰美容";
            Font font = new Font("黑体", 25);
            SolidBrush sbrush = new SolidBrush(Color.White);
            SizeF sizeText = g.MeasureString(strData, font);

            g.DrawString(strData, font, sbrush, (bmp.Width - sizeText.Width) / 2, 490);


            // g.DrawString(str, font, sbrush, new PointF(82, 490));


            g.DrawImage(bmp, 0, 0, bmp.Width, bmp.Height);//又把背景图片的位图画在了背景画布上。必须要这个，否则无法处理阴影

            //4.合并图片
            g.DrawImage(bmpQrcode, 130, 118, bmpQrcode.Width, bmpQrcode.Height);

            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            System.Drawing.Image newImg = Image.FromStream(ms);//生成的新的图片
            //把新图片保存下来
            string DownloadUrl = ConfigurationManager.AppSettings["website_WWW"];
            string host = DownloadUrl + "/HeadImage/";
            //创建下载根文件夹
            //var dirPath = @"C:\DownloadFile\";
            var dirPath = System.AppDomain.CurrentDomain.BaseDirectory + "HeadImage\\";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            //根据年月日创建下载子文件夹
            var ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
            dirPath += ymd + @"\";
            host += ymd + "/";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            //下载到本地文件
            var fileExt = Path.GetExtension(destImg).ToLower();
            var newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + ".jpg";//+ fileExt;
            var filePath = dirPath + newFileName;
            host += newFileName;

            newImg.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);

            return host;
        }
    }
}