using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess.Query;

using JIT.CPOS.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;

using JIT.CPOS.Web.Base.PageBase;
using JIT.CPOS.Web.SendSMSService;
using System.Data;
using System.IO;
using System.Configuration;
using JIT.CPOS.BS.Entity.Interface;
using JIT.CPOS.BS.BLL.Module.NoticeEmail;
using JIT.Utility.Notification;
using System.Text;
using ThoughtWorks.QRCode.Codec;
using System.Drawing.Drawing2D;
using System.Drawing;
namespace JIT.CPOS.Web.Module
{
    /// <summary>
    /// CouponHandler 的摘要说明
    /// </summary>
    public class CouponHandler : IHttpHandler
    {

        string customerId = "";
        string reqContent = "";
        string requestIP = "";
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.ServerVariables["HTTP_VIA"] != null)
            {
                requestIP = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else
            {
                requestIP = context.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }

            reqContent = context.Request["ReqContent"];
            string action = context.Request["action"].ToString().Trim();
            string content = string.Empty;
            JIT.CPOS.Web.Module.Log.InterfaceWebLog.Logger.Log(context, context.Request, action);
            switch (action)
            {
                case "getCouponType":      
                    content = GetCouponType(reqContent);
                    break;
                case "getCouponList":      
                    content = GetCouponList(reqContent);
                    break;
                case "getCouponDetail":
                    content = GetCouponDetail(reqContent);
                    break;
                case "bestowCoupon":
                    content = BestowCoupon(reqContent);
                    break;
                default:
                    throw new Exception("未定义的接口:" + action);
            }

            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            context.Response.Write(content);
            context.Response.End();

        }

        #region getCouponType

        private string GetCouponType(string reqContent)
        {
            getCouponTypeListRespData respData = new getCouponTypeListRespData();
            try
            {
                var reqObj = reqContent.DeserializeJSONTo<reqConunbondata>();
                var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
                CouponTypeBLL bll = new CouponTypeBLL(loggingSessionInfo);
                DataSet ds = bll.GetCouponType();
                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    respData.couponTypeList = DataTableToObject.ConvertToList<CouponTypeEntity>(ds.Tables[0]);
                }
            }
            catch (Exception)
            {
                respData.code = "103";
                respData.description = "数据库操作失败";
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
                if (string.IsNullOrEmpty(reqObj.common.userId))
                {
                    respData.code = "103";
                    respData.description = "登陆用户不能为空";
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
                CouponBLL bll = new CouponBLL(loggingSessionInfo);
                DataSet ds = bll.GetCouponList(reqObj.common.userId, reqObj.special.couponTypeID);

                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    respData.couponList = DataTableToObject.ConvertToList<CouponEntity>(ds.Tables[0]);
                }
            }
            catch (Exception)
            {
                respData.code = "103";
                respData.description = "数据库操作失败";
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
                if (string.IsNullOrEmpty(reqObj.common.userId))
                {
                    respData.code = "103";
                    respData.description = "登陆用户不能为空";
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
                CouponBLL bll = new CouponBLL(loggingSessionInfo);
                //DataSet ds = bll.GetCouponDetail(reqObj.special.cuponID,reqObj.user);

                //if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                //{
                //    respData.couponDetail = DataTableToObject.ConvertToObject<CouponEntity>(ds.Tables[0].Rows[0]);
                //}
                respData.couponDetail.QRUrl = GeneratedQR(reqObj.special.cuponID);
              
            }
            catch (Exception)
            {

                respData.code = "103";
                respData.description = "数据库操作失败";
            }
            return respData.ToJSON();
        }
        #endregion

        #region BestowCoupon
        /// <summary>
        /// 获取优惠劵列表
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string BestowCoupon(string pRequest)
        {
            Default.LowerRespData respData = new Default.LowerRespData();
            try
            {
                var reqObj = pRequest.DeserializeJSONTo<reqConunbondata>();
                if (string.IsNullOrEmpty(reqObj.common.userId))
                {
                    respData.code = "103";
                    respData.description = "登陆用户不能为空";
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
                CouponBLL bll = new CouponBLL(loggingSessionInfo);
                int res = bll.BestowCoupon(reqObj.special.cuponID, reqObj.special.doorID);
                if (res > 0)
                {
                    respData.code = "100";
                    respData.description = "优惠劵使用成功";
                }
                else
                {
                    respData.code = "103";
                    respData.description = "优惠劵不存在";
                }
            }
            catch (Exception)
            {
                respData.code = "103";
                respData.description = "数据库操作失败";
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


        public bool IsReusable
        {
            get { throw new NotImplementedException(); }
        }


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
        public ReqConunbonData common;
        public ReqlistCommonData special;
    }

    public class ReqlistCommonData
    {
        public string couponTypeID;
        public string cuponID;
        public string doorID;//门店ID
    }
    public class getCouponListRespData : Default.LowerRespData
    {
        public List<CouponEntity> couponList { set; get; }
    }

    public class getCouponTypeListRespData : Default.LowerRespData
    {
        public List<CouponTypeEntity> couponTypeList { set; get; }
    }
    public class getCouponDetailData : Default.LowerRespData
    {
        public CouponEntity couponDetail { set; get; }
    }
    #endregion
}