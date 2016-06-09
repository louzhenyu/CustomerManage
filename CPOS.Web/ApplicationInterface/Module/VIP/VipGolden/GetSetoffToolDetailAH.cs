using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.BLL.WX;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.VipGold.Request;
using JIT.CPOS.DTO.Module.VIP.VipGold.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using RedisOpenAPIClient.Models.CC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.VipGolden
{
    /// <summary>
    /// 获取集客海报、优惠券详细接口
    /// </summary>
    public class GetSetoffToolDetailAH : BaseActionHandler<GetSetoffToolDetailRP, GetSetoffToolDetailRD>
    {
        const int ERROR_NULL_WXOBJ = 300;//微信号为空返回值   
        const int ERROR_LOAD_QRCODE = 301;//获取二维码出错，重新打开
        const int ERROR_LOAD_WRONG = 302;//打开出错
        protected override GetSetoffToolDetailRD ProcessRequest(APIRequest<GetSetoffToolDetailRP> pRequest)
        {
            string content = string.Empty;
            string customerId = string.Empty;
            GetSetoffToolDetailRD setoffToolDetailRD = new GetSetoffToolDetailRD();
            var para = pRequest.Parameters;
            customerId = CurrentUserInfo.CurrentUser.customer_id;
            try
            {
                var imageUrl = string.Empty;
                Random ro = new Random();
                var iUp = 100000000;
                var iDown = 50000000;
                var rpVipDCode = 0;                 //临时二维码
                var iResult = ro.Next(iDown, iUp);  //随机数
                var userQrCodeBll = new WQRCodeManagerBLL(CurrentUserInfo);
                var imgBll = new ObjectImagesBLL(CurrentUserInfo);
                var setOffPosterBLL = new SetoffPosterBLL(CurrentUserInfo);
                var SetoffToolUserViewBLL = new SetoffToolUserViewBLL(CurrentUserInfo);
                var SetoffToolsBll = new SetoffToolsBLL(CurrentUserInfo);
                var SetOffPosterInfo = setOffPosterBLL.QueryByEntity(new SetoffPosterEntity() { SetoffPosterID = new Guid(para.ObjectId) }, null);//获取集客海报信息
                var setoffEventBLL = new SetoffEventBLL(CurrentUserInfo);
                var setoffEventInfo = setoffEventBLL.QueryByEntity(new SetoffEventEntity() { Status = "10", SetoffType = 1, CustomerId=customerId }, null);
                //var SetoffToolsInfo = SetoffToolsBll.QueryByEntity(new SetoffToolsEntity() { ObjectId = para.ObjectId, Status = "10", ToolType = para.ToolType, SetoffEventID = setoffEventInfo[0].SetoffEventID }, null);//获取工具信息
                if (para.VipDCode == 9)
                {
                    //根据分享人ID和对象ID获取永久二维码信息
                    var userQrCode = userQrCodeBll.QueryByEntity(new WQRCodeManagerEntity() { ObjectId = para.ObjectId }, null);
                    if (userQrCode != null && userQrCode.Length > 0)
                    {
                        if (para.ToolType == "Coupon")//如果类型为优惠券则返回二维码
                        {
                            setoffToolDetailRD.ToolType = para.ToolType;
                            setoffToolDetailRD.imageUrl = userQrCode[0].ImageUrl;
                            setoffToolDetailRD.paraTemp = userQrCode[0].QRCode;
                           
                        }
                        if (para.ToolType == "SetoffPoster")//如果为集客海报则返回背景图和二维码
                        {
                            if (SetOffPosterInfo != null)
                            {
                                var backImgInfo = imgBll.QueryByEntity(new ObjectImagesEntity() { ObjectId = SetOffPosterInfo[0].ImageId }, null);
                                setoffToolDetailRD.ToolType = para.ToolType;
                                setoffToolDetailRD.imageUrl = userQrCode[0].ImageUrl;
                                setoffToolDetailRD.paraTemp = userQrCode[0].QRCode;
                                setoffToolDetailRD.backImgUrl = backImgInfo[0].ImageURL;                                
                            }
                        }                        
                        return setoffToolDetailRD;
                    }
                    //获取当前二维码 最大值
                    iResult = new WQRCodeManagerBLL(CurrentUserInfo).GetMaxWQRCod() + 1;
                    rpVipDCode = 1;
                }

                #region 获取微信帐号
                var server = new WApplicationInterfaceBLL(CurrentUserInfo);
                var wxObj = new WApplicationInterfaceEntity();
                wxObj = server.QueryByEntity(new WApplicationInterfaceEntity { CustomerId = customerId }, null).FirstOrDefault();
                if (wxObj == null)
                {
                    throw new APIException("不存在对应的微信帐号.") { ErrorCode = ERROR_NULL_WXOBJ };
                }
                else
                {
                    var commonServer = new CommonBLL();
                    //rpVipDCode 二维码类型  0： 临时二维码  1：永久二维码
                    imageUrl = commonServer.GetQrcodeUrl(wxObj.AppID, wxObj.AppSecret, rpVipDCode.ToString(""), iResult, CurrentUserInfo);//iResult作为场景值ID，临时二维码时为32位整型，永久二维码时只支持1--100000     
                    //供本地测试时使用  "https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=gQGN7zoAAAAAAAAAASxodHRwOi8vd2VpeGluLnFxLmNvbS9xL1dreENCS1htX0xxQk94SEJ6MktIAAIEUk88VwMECAcAAA==";
                }
                #endregion

                if (para.VipDCode == 9 && !string.IsNullOrEmpty(imageUrl))    //永久二维码
                {
                    #region 创建店员永久二维码匹配表
                    var userQrTypeBll = new WQRCodeTypeBLL(CurrentUserInfo);
                    string pTypeCode = string.Empty;
                    if (para.ToolType.ToLower() == "coupon")
                    {
                        pTypeCode = "CouponQrCode";
                    }
                    if (para.ToolType.ToLower() == "setoffposter")
                    {
                        pTypeCode = "SetoffPosterQrCode";
                    }
                    var userQrType = userQrTypeBll.QueryByEntity(new WQRCodeTypeEntity() { TypeCode = pTypeCode }, null);//"CouponQrCode=优惠券二维码/SetoffPosterQrCode=集客海报二维码"
                    if (userQrType != null && userQrType.Length > 0)
                    {
                        var userQrcodeBll = new WQRCodeManagerBLL(CurrentUserInfo);
                        var userQrCode = new WQRCodeManagerEntity();
                        userQrCode.QRCode = iResult.ToString();//记录传过去的二维码场景值
                        userQrCode.QRCodeTypeId = userQrType[0].QRCodeTypeId;
                        userQrCode.IsUse = 1;
                        userQrCode.CustomerId = customerId;
                        userQrCode.ImageUrl = imageUrl;
                        userQrCode.ApplicationId = wxObj.ApplicationId;
                        //objectId 为集客海报ID或者优惠券ID
                        userQrCode.ObjectId = para.ObjectId;
                        userQrcodeBll.Create(userQrCode);
                    }
                    #endregion
                }
                else
                {
                    #region 创建临时匹配表
                    VipDCodeBLL vipDCodeServer = new VipDCodeBLL(CurrentUserInfo);
                    VipDCodeEntity info = new VipDCodeEntity();
                    info.DCodeId = iResult.ToString();//记录传过去的二维码场景值****（保存到数据库时没加空格）
                    info.CustomerId = customerId;
                    VipBLL vipBll = new VipBLL(CurrentUserInfo);
                    info.UnitId = "";
                    info.Status = "0";
                    info.IsReturn = 0;
                    info.DCodeType = para.VipDCode;
                    info.CreateBy = CurrentUserInfo.UserID;
                    info.ImageUrl = imageUrl;
                    info.VipId = "";
                    info.ObjectId = para.ObjectId;//工具对象ID（优惠券或集客海报对象ID）
                    info.OwnerUserId = para.ShareVipId;//分享人ID
                    vipDCodeServer.Create(info);
                    #endregion
                }
                int flag = 0;//定义是否需要新增打开数据，0=不需要新增，1=需要新增
                var UserViewData = SetoffToolUserViewBLL.QueryByEntity(new SetoffToolUserViewEntity() { ObjectId = para.ObjectId, UserID = CurrentUserInfo.UserID }, null);
                if (!string.IsNullOrEmpty(para.SetoffToolID))
                {
                    UserViewData = SetoffToolUserViewBLL.QueryByEntity(new SetoffToolUserViewEntity() { ObjectId = para.ObjectId, UserID = CurrentUserInfo.UserID, SetoffToolID = new Guid(para.SetoffToolID) }, null);
                }
                if (UserViewData.Length == 0 && !string.IsNullOrEmpty(para.SetoffToolID))
                {
                    flag = 1;//当为1时进行增加打开工具记录
                }
                if (para.ToolType == "Coupon")//如果类型为优惠券则返回二维码
                {
                    
                    #region 用户领优惠券过程
                    var CouponTypeBLL = new CouponTypeBLL(CurrentUserInfo);
                    var couponBLL = new CouponBLL(CurrentUserInfo);
                    var CouponSourceBLL = new CouponSourceBLL(CurrentUserInfo);
                    var VipCouponMappingBLL = new VipCouponMappingBLL(CurrentUserInfo);                    
                    
                    int? SurplusCount = 0;//先获得剩余张数
                    int hasCouponCount=0;//已领张数 如果>0表示已领取过就不让他领，=0就领取
                    var couponTypeInfo = CouponTypeBLL.QueryByEntity(new CouponTypeEntity() { CouponTypeID = new Guid(para.ObjectId) }, null);
                    
                    if (couponTypeInfo != null)
                    {
                        string couponIsVocher = string.Empty;
                        if (couponTypeInfo[0].IsVoucher == null)
                        {
                            SurplusCount = couponTypeInfo[0].IssuedQty - 0;
                        }
                        else
                        {
                            SurplusCount = couponTypeInfo[0].IssuedQty - couponTypeInfo[0].IsVoucher;
                        }       
                        if (SurplusCount != 0 && para.ShareVipId != null && para.ShareVipId != "")// 通过列表进详细自己不能领券
                        {
                            //当剩余数量不为0时，进行下一步判断是否存在已领券的信息
                            hasCouponCount = VipCouponMappingBLL.GetReceiveCouponCount(CurrentUserInfo.UserID,para.ObjectId,"Share");
                            if (hasCouponCount == 0 )//如果等于零，开始领券流程
                            {
                                //关联优惠券让用户领券
                                var redisVipMappingCouponBLL = new JIT.CPOS.BS.BLL.RedisOperationBLL.Coupon.RedisVipMappingCouponBLL();
                                try
                                {
                                    //执行领取操作
                                    redisVipMappingCouponBLL.SetVipMappingCoupon(new CC_Coupon()
                                    {
                                        CustomerId = this.CurrentUserInfo.ClientID,
                                        CouponTypeId = para.ObjectId
                                    }, "", CurrentUserInfo.UserID, "Share");                                    
                                    setoffToolDetailRD.Name = couponTypeInfo[0].CouponTypeName+",已飞入您的账户";
                                }
                                catch (Exception ex)
                                {
                                    setoffToolDetailRD.Name = "很遗憾此次优惠券" + couponTypeInfo[0].CouponTypeName + "的分享已失效,下次请及时领取.";
                                }
                                
                            }
                        }
                        else
                        {
                            //如果剩余数量为0时，给出已领完提示
                            if (para.ShareVipId != null && para.ShareVipId != "")
                            {
                                setoffToolDetailRD.Name = "很遗憾您来迟一步优惠券" + couponTypeInfo[0].CouponTypeName + "已被领完.";
                                //throw new APIException("很遗憾您来迟一步券已被领完.") { ErrorCode = ERROR_LOAD_WRONG };
                            }
                        }
                        if (setoffToolDetailRD.Name == null || setoffToolDetailRD.Name=="")
                        {
                            setoffToolDetailRD.Name = couponTypeInfo[0].CouponTypeName;
                        }                        
                        setoffToolDetailRD.StartTime = couponTypeInfo[0].BeginTime == null ? Convert.ToDateTime(couponTypeInfo[0].CreateTime).ToString("yyyy-MM-dd") : Convert.ToDateTime(couponTypeInfo[0].BeginTime).ToString("yyyy-MM-dd");
                        setoffToolDetailRD.EndTime = couponTypeInfo[0].EndTime == null ? "" : Convert.ToDateTime(couponTypeInfo[0].EndTime).ToString("yyyy-MM-dd");
                        setoffToolDetailRD.ServiceLife = couponTypeInfo[0].ServiceLife.ToString();
                    }
                    #endregion
                    setoffToolDetailRD.ToolType = para.ToolType;//返回工具类别
                    setoffToolDetailRD.imageUrl = imageUrl;//返回临时二维码
                    setoffToolDetailRD.paraTemp = iResult.ToString().Insert(4, " "); //加空格，加空格有什么作用？
                }
                if (para.ToolType == "SetoffPoster")//如果为集客海报则返回背景图和二维码
                {
                    if (SetOffPosterInfo != null)
                    {
                        var backImgInfo = imgBll.QueryByEntity(new ObjectImagesEntity() { ImageId = SetOffPosterInfo[0].ImageId }, null);
                        setoffToolDetailRD.ToolType = para.ToolType;
                        setoffToolDetailRD.imageUrl = imageUrl;
                        setoffToolDetailRD.paraTemp = iResult.ToString().Insert(4, " "); //加空格，加空格有什么作用？
                        setoffToolDetailRD.backImgUrl = backImgInfo[0].ImageURL;
                        setoffToolDetailRD.Name = SetOffPosterInfo[0].Name;                       
                    }
                }
                if (flag == 1)//当为1是需进行打开工具的记录
                {
                    var SetoffToolUserView = new SetoffToolUserViewEntity();
                    SetoffToolUserView.SetoffToolUserViewID = Guid.NewGuid();
                    SetoffToolUserView.SetoffEventID = setoffEventInfo[0].SetoffEventID;
                    SetoffToolUserView.SetoffToolID = new Guid(para.SetoffToolID);
                    SetoffToolUserView.ToolType = para.ToolType;
                    SetoffToolUserView.ObjectId = para.ObjectId;
                    SetoffToolUserView.NoticePlatformType = 1;
                    SetoffToolUserView.UserID = CurrentUserInfo.UserID;
                    SetoffToolUserView.IsOpen = 1;
                    SetoffToolUserView.OpenTime = System.DateTime.Now;
                    SetoffToolUserView.CustomerId = customerId;
                    SetoffToolUserView.CreateTime = System.DateTime.Now;
                    SetoffToolUserView.CreateBy = CurrentUserInfo.UserID;
                    SetoffToolUserView.LastUpdateTime = System.DateTime.Now;
                    SetoffToolUserView.LastUpdateBy = CurrentUserInfo.UserID;
                    SetoffToolUserView.IsDelete = 0;
                    SetoffToolUserViewBLL.Create(SetoffToolUserView);
                }
            }
            catch (Exception ex)
            {
                throw new APIException(ex.Message) { };
            }
            return setoffToolDetailRD;
        }
    }
}