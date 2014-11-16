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
using System.Text;
using Aspose.Cells;
using System.IO;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Configuration;
using ThoughtWorks.QRCode.Codec;

namespace JIT.CPOS.BS.Web.Module.Order.Print.Handler
{
    /// <summary>
    /// PrintPicingHander 的摘要说明
    /// </summary>
    public class PrintPicingHander : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = string.Empty;
            switch (pContext.Request.QueryString["method"])
            {
                case "GetPrintPickingInfo":
                    content = GetPrintPickingInfo(pContext);
                    break;
                case "GetPrintPickingTypeInfo":
                    content = GetPrintPickingTypeInfo(pContext);
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }
        #region GetPrintPickingInfo
        /// <summary>
        /// 获取打印信息
        /// </summary>
        /// <returns></returns>
        public string GetPrintPickingInfo(HttpContext pContext)
        {
            string ret = string.Empty;
            string orderID = pContext.Request.QueryString["orderID"];
            if (string.IsNullOrEmpty(orderID))
            {
                orderID = pContext.Request.Form["orderID"].ToString();
            }
            if (string.IsNullOrEmpty(orderID))
            {
                ret = string.Format("{success:false,msg:'操作失败'}");
            }
            try
            {
                string str = this.GeneratedQR(orderID);
                DataSet ds = new TInOutStatusNodeBLL(CurrentUserInfo).GetPrintPickInfo(orderID);
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        ret = string.Format("{{\"success\":true,\"msg\":'操作成功',\"data\":{0},\"dataType\":{1},\"image\":'{2}'}}", ds.Tables[0].ToJSON(), ds.Tables[1].ToJSON(), str);//dataType存的是另外 
                    }
                    else
                    {
                        ret = string.Format("{{\"success\":true,\"msg\":'操作成功',\"data\":{0},\"dataType\":'null',\"image\":'{1}'}}", ds.Tables[0].ToJSON(), str);
                    }
                }
            }
            catch (Exception)
            {
                ret = string.Format("{{success:false,msg:'操作失败'}}");
                throw;
            }

            return ret;
        }
        #endregion

        #region GetPrintPickingTypeInfo
        /// <summary>
        /// 获取打印信息
        /// </summary>
        /// <returns></returns>
        public string GetPrintPickingTypeInfo(HttpContext pContext)
        {
            string ret = string.Empty;
            string orderID = pContext.Request.Form["orderID"].ToString();
            string itemTagID = pContext.Request.Form["itemTagID"].ToString();
            if (string.IsNullOrEmpty(orderID))
            {
                ret = string.Format("{success:false,msg:'操作失败'}");
            }
            try
            {
                DataSet ds = new TInOutStatusNodeBLL(CurrentUserInfo).GetPrintPickingTypeInfo(orderID, itemTagID);
                if (ds != null && ds.Tables.Count > 0)
                {
                    ret = string.Format("{{\"success\":true,\"msg\":'操作成功',\"data\":{0}}}", ds.Tables[0].ToJSON());
                }
            }
            catch (Exception)
            {
                ret = string.Format("{success:false,msg:'操作失败'}");
                throw;
            }

            return ret;
        }

        #endregion

        #region 获取二维码
        public string GeneratedQR(string orderID)
        {


            string res = "";
            var qrcode = new StringBuilder();
            qrcode.AppendFormat("{0}", "JHD/" + orderID);
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeScale = 5;
            qrCodeEncoder.QRCodeVersion = 0;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            Image qrImage = qrCodeEncoder.Encode(qrcode.ToString(), Encoding.UTF8);
            Image bitmap = new System.Drawing.Bitmap(100, 100);
            Graphics g2 = System.Drawing.Graphics.FromImage(bitmap);
            g2.InterpolationMode = InterpolationMode.High;
            g2.SmoothingMode = SmoothingMode.HighQuality;
            g2.Clear(System.Drawing.Color.Transparent);
            g2.DrawImage(qrImage, new System.Drawing.Rectangle(0, 0, 100, 100), new System.Drawing.Rectangle(0, 0, qrImage.Width, qrImage.Height), System.Drawing.GraphicsUnit.Pixel);

            string fileName = orderID.ToLower() + ".jpg";
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


}