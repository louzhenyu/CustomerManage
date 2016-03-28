using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Web;
using JIT.CPOS.BLL;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Common;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Base;
using Aspose.Cells;
using ThoughtWorks.QRCode.Codec;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Market.MarketNameApply
{
    /// <summary>
    /// GenerateMarketEventQR 的摘要说明
    /// </summary>
    public class GenerateMarketEventQR: JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {

        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = "";
           // string method = pContext.Request.QueryString["method"];
            MarketNamedApplyBLL marketNameApplyBll = new MarketNamedApplyBLL(CurrentUserInfo);
            EmptyResponseData emptyRD = new EmptyResponseData();
            MarketNamedApplyEntity[] marketNameApplyEntityArray = marketNameApplyBll.QueryByEntity(new MarketNamedApplyEntity() { Status = 20, CustomerId = CurrentUserInfo.CurrentUser.customer_id }, null);
            if(marketNameApplyEntityArray.Length > 0)
            {
                for(int i = 0 ;i < marketNameApplyEntityArray.Length ; i++)
                {
                    string url = this.GeneratedQR(marketNameApplyEntityArray[i].VipId, marketNameApplyEntityArray[0].MarketEventID);
                    marketNameApplyEntityArray[i].QRCodeUrl = url;
                    marketNameApplyBll.Update(marketNameApplyEntityArray[i]);
                }
                this.ExportExcel(pContext,CurrentUserInfo.CurrentUser.customer_id);
            }

            //pContext.Response.Write(content);
        }

        #region 获取二维码
        public string GeneratedQR(string VipID, string EventId)
        {

            string res = "";
            var qrcode = new StringBuilder();
            qrcode.AppendFormat("{0}", "MarketNameApply/" + VipID + "," + EventId);
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();  //这个类从哪来的？是一个生成二维码的组件
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeScale = 5;
            qrCodeEncoder.QRCodeVersion = 0;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            Image qrImage = qrCodeEncoder.Encode(qrcode.ToString(), Encoding.UTF8);//把couponid放到 了二维码 信息 里 
            Image bitmap = new System.Drawing.Bitmap(215, 215);
            Graphics g2 = System.Drawing.Graphics.FromImage(bitmap);
            g2.InterpolationMode = InterpolationMode.High;
            g2.SmoothingMode = SmoothingMode.HighQuality;
            g2.Clear(System.Drawing.Color.Transparent);
            g2.DrawImage(qrImage, new System.Drawing.Rectangle(0, 0, 215, 215), new System.Drawing.Rectangle(0, 0, qrImage.Width, qrImage.Height), System.Drawing.GraphicsUnit.Pixel);

            string fileName = VipID.ToLower() + ".jpg";
            string host = ConfigurationManager.AppSettings["website_WWW"].ToString();

            if (!host.EndsWith("/")) host += "/";
            string fileUrl = host + "File/images/" + fileName;

            string newFilePath = string.Empty;
            string newFilename = string.Empty;
            string path = HttpContext.Current.Server.MapPath("/images/qrcode2.jpg");
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

        #region 导出excel
        public void ExportExcel(HttpContext pContext,string CustomerID)
        {
            MarketNamedApplyBLL marketNamedApplyBll = new MarketNamedApplyBLL(CurrentUserInfo);
            VipBLL vip = new VipBLL(CurrentUserInfo);

            DataSet dsVip = marketNamedApplyBll.GetVipByMarketNamedApply(CustomerID);

            if (dsVip.Tables.Count > 0)
            {
                string MapUrl = pContext.Server.MapPath(@"~/Framework/Upload/" + DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss.ms") + ".xls");
                Aspose.Cells.License lic = new Aspose.Cells.License();
                lic.SetLicense("Aspose.Total.lic");
                Workbook workbook = new Workbook();
                Worksheet sheet = workbook.Worksheets[0];
                Cells cells = sheet.Cells;//单元格

                Style style1 = workbook.Styles[workbook.Styles.Add()];//新增样式
                style1.HorizontalAlignment = TextAlignmentType.Center;//文字居中
                style1.Font.Name = "宋体";//文字字体
                style1.Font.Size = 14;//文字大小
                style1.Font.IsBold = true;//粗体
                style1.IsTextWrapped = true;//单元格内容自动换行
                style1.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                style1.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                style1.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                style1.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

                Style style2 = workbook.Styles[workbook.Styles.Add()];//新增样式
                style2.HorizontalAlignment = TextAlignmentType.Center;//文字居中
                style2.Font.Name = "宋体";//文字字体
                style2.Font.Size = 12;//文字大小
                style2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                style2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                style2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                style2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

                //设置宽度
                cells.SetColumnWidth(1, 30);
                cells.SetColumnWidth(2, 30);
                cells.SetColumnWidth(3, 90);

                cells[0, 0].PutValue("会员姓名");
                cells[0, 0].SetStyle(style1);

                cells[0, 1].PutValue("会员电话");
                cells[0, 1].SetStyle(style1);

                cells[0, 2].PutValue("二维码路径");
                cells[0, 2].SetStyle(style1);


                cells.SetRowHeight(0, 30);
                cells.SetColumnWidth(0, 30);
                cells.SetRowHeight(1, 20);
                cells.SetColumnWidth(2, 90);

                #region 生成数据行

                for (int i = 0; i < dsVip.Tables[0].Rows.Count; i++)
                {
                    cells[i + 1, 0].PutValue(dsVip.Tables[0].Rows[i]["VipName"].ToString());
                    cells[i + 1, 0].SetStyle(style2);

                    cells[i + 1, 1].PutValue(dsVip.Tables[0].Rows[i]["Phone"].ToString());
                    cells[i + 1, 1].SetStyle(style2);

                    cells[i + 1, 2].PutValue(dsVip.Tables[0].Rows[i]["QRCodeUrl"].ToString());
                    cells[i + 1, 3].SetStyle(style2);
                }
                #endregion

                workbook.Save(MapUrl);

                Utils.OutputExcel(pContext, MapUrl);//输出Excel文件

            }
            else
            {
                throw new APIException("没有人报名");
            }
        }
        #endregion
    }
}