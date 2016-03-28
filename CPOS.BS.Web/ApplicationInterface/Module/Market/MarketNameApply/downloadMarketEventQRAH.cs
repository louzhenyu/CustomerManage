using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using Aspose.Cells;
using System.IO;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.Common;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Market.MarketNameApply
{
    public class DownloadMarketEventQRAH : BaseActionHandler<EmptyRequestParameter, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<EmptyRequestParameter> pRequest)
        {
            MarketNamedApplyBLL marketNamedApplyBll = new MarketNamedApplyBLL(CurrentUserInfo);
            VipBLL vip = new VipBLL(CurrentUserInfo);

            DataSet dsVip = marketNamedApplyBll.GetVipByMarketNamedApply(pRequest.CustomerID);

            if (dsVip.Tables.Count > 0)
            {
                string MapUrl = HttpContext.Current.Server.MapPath(@"~/Framework/Upload/" + DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss.ms") + ".xls");
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

                Utils.OutputExcel(HttpContext.Current, MapUrl);//输出Excel文件

            }
            else
            {
                throw new APIException("没有人报名");
            }

            return null;
        }
    }
}