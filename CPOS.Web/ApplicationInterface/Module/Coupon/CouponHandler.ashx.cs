using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using JIT.CPOS.BLL;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.BLL.WX;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.Eliya;
using JIT.CPOS.BS.Entity.Interface;
using JIT.CPOS.BS.Entity.User;
using JIT.CPOS.Common;
using JIT.CPOS.DTO.Module.Order.Order.Response;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using JIT.Utility.Notification;
using JIT.Utility.Web;
using ThoughtWorks.QRCode.Codec;
using System.Drawing.Drawing2D;


namespace JIT.CPOS.Web.ApplicationInterface.Module.Coupon
{
    /// <summary>
    /// CouponHandler 的摘要说明
    /// </summary>
    public class CouponHandler : BaseGateway
    {
        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string contion = string.Empty;
            switch (pAction)
            {
                case "getCouponType":
                    contion = GetCouponType(pRequest);
                    break;
                case "getCouponList":
                    contion = GetCouponList(pRequest);
                    break;
                case "getCouponDetail":
                    contion = GetCouponDetail(pRequest);
                    break;
                case "bestowCoupon":
                    contion = BestowCoupon(pRequest);
                    break;
                default:
                    throw new Exception("未定义的接口:" + pAction);
            }
            return contion;
        }

        #region getCouponType

        private string GetCouponType(string reqContent)
        {
            getCouponTypeListRespData respData = new getCouponTypeListRespData();
            try
            {
                var reqObj = reqContent.DeserializeJSONTo<reqConunbondata>();
                var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.customerId, "1");
                CouponTypeBLL bll = new CouponTypeBLL(loggingSessionInfo);
                DataSet ds = bll.GetCouponTypeList();
                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    respData.couponTypeList = DataTableToObject.ConvertToList<CouponTypeEntity>(ds.Tables[0]);
                }
            }
            catch (Exception)
            {
                respData.ResultCode = "103";
                respData.Message = "数据库操作失败";
            }
            return respData.ToJSON();

        }
        #endregion

        #region getCouponList
        /// <summary>
        /// 获取优惠劵列表
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string GetCouponList(string pRequest)
        {
            getCouponListRespData respData = new getCouponListRespData();
            try
            {
                var reqObj = pRequest.DeserializeJSONTo<reqConunbondata>();
                if (string.IsNullOrEmpty(reqObj.userId))
                {
                    respData.ResultCode = "103";
                    respData.Message = "登陆用户不能为空";
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.customerId, "1");
                CouponBLL bll = new CouponBLL(loggingSessionInfo);
                DataSet ds = bll.GetCouponList(reqObj.userId, reqObj.Parameters.couponTypeID);

                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    respData.couponList = DataTableToObject.ConvertToList<CouponEntity>(ds.Tables[0]);
                }
            }
            catch (Exception)
            {
                respData.ResultCode = "103";
                respData.Message = "数据库操作失败";
            }

            return respData.ToJSON();

        }
        #endregion

        #region GetCouponDetail
        /// <summary>
        /// 获取优惠劵详情
        /// </summary>
        /// <param name="pReques"></param>
        /// <returns></returns>
        public string GetCouponDetail(string pReques)
        {
            getCouponDetailData respData = new getCouponDetailData();
            try
            {
                var reqObj = pReques.DeserializeJSONTo<reqConunbondata>();
                if (string.IsNullOrEmpty(reqObj.userId))
                {
                    respData.ResultCode = "103";
                    respData.Message = "登陆用户不能为空";
                    return respData.ToJSON();
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.customerId, reqObj.userId);
                CouponBLL bll = new CouponBLL(loggingSessionInfo);
                DataSet ds = bll.GetCouponDetail(reqObj.Parameters.cuponID,reqObj.userId);

                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    respData.couponDetail = DataTableToObject.ConvertToObject<CouponEntity>(ds.Tables[0].Rows[0]);
                    respData.couponDetail.QRUrl = GeneratedQR(reqObj.Parameters.cuponID); //生成这优惠券的二维码
                }else
                {
                    respData.ResultCode = "103";
                    respData.Message = "无效的优惠券";
                    return respData.ToJSON();
                }
               

            }
            catch (Exception)
            {

                respData.ResultCode = "103";
                respData.Message = "数据库操作失败";
            }
            return respData.ToJSON();
        }
        #endregion

        #region BestowCoupon
        /// <summary>
        /// 核销优惠劵
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string BestowCoupon(string pRequest)
        {
            RespData respData = new RespData();
            try
            {
                var reqObj = pRequest.DeserializeJSONTo<reqConunbondata>();
                if (string.IsNullOrEmpty(reqObj.userId))
                {
                    respData.ResultCode = "103";
                    respData.Message = "登陆用户不能为空";
                }
                if (string.IsNullOrEmpty(reqObj.Parameters.doorID))
                {
                    respData.ResultCode = "103";
                    respData.Message = "此APP版本无法核销，请升级到新版本。";
                    return respData.ToJSON();
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.customerId, reqObj.userId);
                var couponUseBll = new CouponUseBLL(loggingSessionInfo);          //优惠券使用BLL实例化
                var vcmBll = new VipCouponMappingBLL(loggingSessionInfo);                //优惠券BLL实例化
                var couponTypeBll = new CouponTypeBLL(loggingSessionInfo);      //优惠券类型
                var mappingBll = new CouponTypeUnitMappingBLL(loggingSessionInfo);       //优惠券和门店映射
                //var vcmEntity = new VipCouponMappingEntity();
                CouponBLL bll = new CouponBLL(loggingSessionInfo);

                List<IWhereCondition> wheresOrderNo = new List<IWhereCondition>();
                    wheresOrderNo.Add(new EqualsCondition() { FieldName = "CouponID", Value = reqObj.Parameters.cuponID });
                    var resultCouponVipID = vcmBll.Query(wheresOrderNo.ToArray(), null);

                //判断是否有权限核销优惠券
                var couponEntity = bll.GetByID(reqObj.Parameters.cuponID);
                if (couponEntity != null)
                {
                    var couponTypeInfo = couponTypeBll.GetByID(couponEntity.CouponTypeID);
                    if (couponTypeInfo != null)
                    {
                        if (couponTypeInfo.SuitableForStore == 2)//下面的doorid传的是门店的id，如果等于1所有门店都能用，如果等于3所有分销商都能用 
                        {
                            var couponTypeUnitMapping = mappingBll.QueryByEntity(new CouponTypeUnitMappingEntity() { CouponTypeID = new Guid(couponEntity.CouponTypeID.ToString()), ObjectID = reqObj.Parameters.doorID }, null).FirstOrDefault();

                            if (couponTypeUnitMapping == null)
                            {
                                respData.ResultCode = "104";
                                respData.Message = "请到指定门店/分销商使用";
                                return respData.ToJSON();
                            }
                        }
                        if (couponTypeInfo.SuitableForStore == 3)//下面的doorid传的是门店的id，如果等于1所有门店都能用，如果等于3所有分销商都能用 
                        { 
                            //doorid必须是获取，分销商如果没数据，就报错。
                            RetailTraderBLL _RetailTraderBLL = new RetailTraderBLL(loggingSessionInfo);
                            RetailTraderEntity en = _RetailTraderBLL.GetByID(reqObj.Parameters.doorID);
                            if (en == null)
                            {
                                respData.ResultCode = "104";
                                respData.Message = "请到指定分销商使用";
                                return respData.ToJSON();
                            }

                        }
                        if (couponTypeInfo.SuitableForStore == 1)//下面的doorid传的是门店的id，如果等于1所有门店都能用，如果等于3所有分销商都能用 
                        {
                            //doorid必须是获取，门店如果没有数据，就报错。
                            TUnitBLL _TUnitBLL = new TUnitBLL(loggingSessionInfo);
                            TUnitEntity en = _TUnitBLL.GetByID(reqObj.Parameters.doorID);
                            if (en == null)
                            {
                                respData.ResultCode = "104";
                                respData.Message = "请到指定门店使用";
                                return respData.ToJSON();
                            }
                        }

                    }
                }
                int res = bll.BestowCoupon(reqObj.Parameters.cuponID, reqObj.Parameters.doorID);
                if (res > 0) //如果没有影响一行，所以Coupon表里这条记录的status=1了，不能被使用了。
                {
                #region 优惠券使用记录
                    var couponUseEntity = new CouponUseEntity()
                    {
                        CouponID = reqObj.Parameters.cuponID,
                        VipID = resultCouponVipID == null ? "" : resultCouponVipID[0].VIPID,
                        UnitID = reqObj.Parameters.doorID,
                        //OrderID = orderEntity.OrderID.ToString(),
                        //CreateBy = reqObj.userId,
                        Comment = "核销电子券",
                        CustomerID = reqObj.customerId
                    };
                    couponUseBll.Create(couponUseEntity);//生成优惠券使用记录
                #endregion

                    respData.ResultCode = "200";
                    respData.Message = "优惠劵使用成功";
                }
                else
                {
                    respData.ResultCode = "103";
                    respData.Message = "优惠劵已使用";
                }
            }
            catch (Exception)
            {
                respData.ResultCode = "103";
                respData.Message = "数据库操作失败";
            }
            return respData.ToJSON();
        }
        #endregion

        #region 获取二维码
        public string GeneratedQR(string CouponID)
        {

            string res = "";
            var qrcode = new StringBuilder();
            qrcode.AppendFormat("{0}", "YHQ/" + CouponID);
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeScale = 5;
            qrCodeEncoder.QRCodeVersion = 0;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            Image qrImage = qrCodeEncoder.Encode(qrcode.ToString(), Encoding.UTF8);
            Image bitmap = new System.Drawing.Bitmap(215, 215);
            Graphics g2 = System.Drawing.Graphics.FromImage(bitmap);
            g2.InterpolationMode = InterpolationMode.High;
            g2.SmoothingMode = SmoothingMode.HighQuality;
            g2.Clear(System.Drawing.Color.Transparent);
            g2.DrawImage(qrImage, new System.Drawing.Rectangle(0, 0, 215, 215), new System.Drawing.Rectangle(0, 0, qrImage.Width, qrImage.Height), System.Drawing.GraphicsUnit.Pixel);

            string fileName = CouponID.ToLower() + ".jpg";
            string host = ConfigurationManager.AppSettings["website_WWW"].ToString();

            if (!host.EndsWith("/")) host += "/";
            string fileUrl = host + "File/images/" + fileName;

            string newFilePath = string.Empty;
            string newFilename = string.Empty;
            string path = HttpContext.Current.Server.MapPath("/images/qrcode.jpg");
            System.Drawing.Image imgSrc = System.Drawing.Image.FromFile(path);
            System.Drawing.Image imgWarter = bitmap;
            using (Graphics g = Graphics.FromImage(imgSrc))
            {
                g.DrawImage(imgWarter, new Rectangle(0, 0, imgWarter.Width, imgWarter.Height), 0, 0, imgWarter.Width, imgWarter.Height, GraphicsUnit.Pixel);
            }
            newFilePath = string.Format("/File/images/{0}", fileName);
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

    }

    #region 返回及接受参数
    public class ReqConunbonData
    {
        public string locale;
        public string userId;
        public string openId;
        public string signUpId;
        public string customerId;
        public string businessZoneId;//商图ID
        public string channelId;//渠道ID
        public string eventId;
        public string isALD; //1-是
        public string plat;
    }

    public class reqConunbondata
    {
        public string locale;
        public string userId;
        public string openId;
        public string signUpId;
        public string customerId;
        public string businessZoneId;//商图ID
        public string channelId;//渠道ID
        public string eventId;
        public string isALD; //1-是
        public string plat;

        public ReqlistCommonData Parameters;
    }

    public class ReqlistCommonData
    {
        public string couponTypeID;
        public string cuponID;
        public string doorID;//门店ID
    }
    public class getCouponListRespData : RespData
    {
        public List<CouponEntity> couponList { set; get; }
    }

    public class getCouponTypeListRespData : RespData
    {
        public List<CouponTypeEntity> couponTypeList { set; get; }
    }
    public class getCouponDetailData : RespData
    {
        public CouponEntity couponDetail { set; get; }
    }
    public class RespData
    {
        public string ResultCode = "200";
        public string Message = "操作成功";
        public string exception = null;
        public string data;
        public int searchCount;
    }
    #endregion
}