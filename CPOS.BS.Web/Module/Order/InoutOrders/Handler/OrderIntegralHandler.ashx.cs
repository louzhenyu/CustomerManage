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

namespace JIT.CPOS.BS.Web.Module.Order.InoutOrders.Handler
{
    /// <summary>
    /// OrderIntegralHandler 的摘要说明
    /// </summary>
    public class OrderIntegralHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        /// <summary>
        /// 页面入口
        /// </summary>
        /// <param name="pContext"></param>
        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = "";
            switch (pContext.Request.QueryString["method"])
            {

                case "GetOrderIntegralList":  //积分订单查询
                    content = GetOrderIntegralList(pContext.Request.Form);
                    break;

                case "Export":  //导出数据
                    Export(pContext);
                    break;

            }
            pContext.Response.Write(content);
            pContext.Response.End();

        }

        #region 积分订单查询
        public string GetOrderIntegralList(NameValueCollection rParams)
        {

            OrderIntegralEntity entity = Request("form").DeserializeJSONTo<OrderIntegralEntity>();

            int pageSize = rParams["limit"].ToInt();
            int pageIndex = rParams["page"].ToInt();

            int rowCount = 0;
            return string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
               new OrderIntegralBLL(CurrentUserInfo).GetList(entity, pageIndex, pageSize, out rowCount).ToJSON(),
                rowCount);
        }
        #endregion

        #region 导出Excel文件
        /// <summary>
        /// 导出Excel数据功能 
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private void Export(HttpContext pContext)
        {
            try
            {
                #region 获取信息

                OrderIntegralEntity entity = Request("param").DeserializeJSONTo<OrderIntegralEntity>();
                OrderIntegralEntity[] data = new OrderIntegralBLL(CurrentUserInfo).GetAllList(entity);

                #endregion
                string MapUrl = pContext.Server.MapPath(@"~/Framework/Upload/" + DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss.ms") + ".xls");
                Aspose.Cells.License lic = new Aspose.Cells.License();
                lic.SetLicense("Aspose.Total.lic");
                Workbook workbook = new Workbook();
                Worksheet sheet = workbook.Worksheets[0];
                Cells cells = sheet.Cells;//单元格
                #region
                //为标题设置样式    
                Style styleTitle = workbook.Styles[workbook.Styles.Add()];//新增样式
                styleTitle.HorizontalAlignment = TextAlignmentType.Center;//文字居中
                styleTitle.Font.Name = "宋体";//文字字体
                styleTitle.Font.Size = 18;//文字大小
                styleTitle.Font.IsBold = true;//粗体

                //样式2
                Style style2 = workbook.Styles[workbook.Styles.Add()];//新增样式
                style2.HorizontalAlignment = TextAlignmentType.Center;//文字居中
                style2.Font.Name = "宋体";//文字字体
                style2.Font.Size = 14;//文字大小
                style2.Font.IsBold = true;//粗体
                style2.IsTextWrapped = true;//单元格内容自动换行
                style2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                style2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                style2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                style2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

                //样式3
                Style style3 = workbook.Styles[workbook.Styles.Add()];//新增样式
                style3.HorizontalAlignment = TextAlignmentType.Center;//文字居中
                style3.Font.Name = "宋体";//文字字体
                style3.Font.Size = 12;//文字大小
                style3.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                style3.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                style3.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                style3.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                #endregion
                //生成行1 标题行  
                cells.Merge(0, 0, 1, 12);//合并单元格

                cells[0, 0].PutValue("积分订单");//填写内容
                cells[0, 0].SetStyle(styleTitle);
                cells.SetRowHeight(0, 38);

                //生成行2 列名行
                for (int i = 0; i < 12; i++)
                {
                    cells.SetColumnWidth(i, 30);
                }
                #region 列明
                cells[1, 0].PutValue("订单编号");
                cells[1, 0].SetStyle(style2);
                cells.SetRowHeight(1, 25);
                cells[1, 1].PutValue("下单时间");
                cells[1, 1].SetStyle(style2);
                cells.SetRowHeight(1, 25);
                cells[1, 2].PutValue("商品名称");
                cells[1, 2].SetStyle(style2);
                cells.SetRowHeight(1, 25);
                cells[1, 3].PutValue("商品编号");
                cells[1, 3].SetStyle(style2);
                cells.SetRowHeight(1, 25);
                cells[1, 4].PutValue("商品积分");
                cells[1, 4].SetStyle(style2);
                cells[1, 5].PutValue("兑换数量");
                cells[1, 5].SetStyle(style2);
                cells[1, 6].PutValue("总积分");
                cells[1, 6].SetStyle(style2);
                cells[1, 7].PutValue("会员");
                cells[1, 7].SetStyle(style2);
                cells[1, 8].PutValue("会员编号");
                cells[1, 8].SetStyle(style2);
                cells[1, 9].PutValue("收货人");
                cells[1, 9].SetStyle(style2);
                cells[1, 10].PutValue("收货电话");
                cells[1, 10].SetStyle(style2);
                cells[1, 11].PutValue("收货地址");
                cells[1, 11].SetStyle(style2);
                cells.SetRowHeight(1, 25);
                #endregion

                #region 生成数据行
                for (int i = 0; i < data.Length; i++)
                {
                    cells[2 + i, 0].PutValue(data[i].OrderIntegralID);
                    cells[2 + i, 0].SetStyle(style3);

                    cells[2 + i, 1].PutValue(data[i].CreateTimeFormat);
                    cells[2 + i, 1].SetStyle(style3);

                    cells[2 + i, 2].PutValue(data[i].item_name);
                    cells[2 + i, 2].SetStyle(style3);

                    cells[2 + i, 3].PutValue(data[i].item_code);
                    cells[2 + i, 3].SetStyle(style3);

                    cells[2 + i, 4].PutValue(data[i].Integral);
                    cells[2 + i, 4].SetStyle(style3);

                    cells[2 + i, 5].PutValue(data[i].Quantity);
                    cells[2 + i, 5].SetStyle(style3);

                    cells[2 + i, 6].PutValue(data[i].IntegralAmmount);
                    cells[2 + i, 6].SetStyle(style3);

                    cells[2 + i, 7].PutValue(data[i].VipName);
                    cells[2 + i, 7].SetStyle(style3);

                    cells[2 + i, 8].PutValue(data[i].VipCode);
                    cells[2 + i, 8].SetStyle(style3);

                    cells[2 + i, 9].PutValue(data[i].LinkMan);
                    cells[2 + i, 9].SetStyle(style3);

                    cells[2 + i, 10].PutValue(data[i].LinkTel);
                    cells[2 + i, 10].SetStyle(style3);

                    cells[2 + i, 11].PutValue(data[i].Address);
                    cells[2 + i, 11].SetStyle(style3);

                    cells.SetRowHeight(2 + i, 24);
                }
                #endregion
                workbook.Save(MapUrl);

                Utils.OutputExcel(pContext, MapUrl);//输出Excel文件

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion
        

    }

}