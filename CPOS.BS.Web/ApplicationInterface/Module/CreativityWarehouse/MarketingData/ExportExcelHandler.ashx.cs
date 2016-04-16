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
using JIT.CPOS.BS.Web.Session;
using System.Data;
using System.Threading;
using System.Configuration;
using JIT.CPOS.BS.BLL.WX;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingData.Request;
using JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingData.Response;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.CreativityWarehouse.MarketingData
{
    /// <summary>
    /// ExportExcelHandler 的摘要说明
    /// </summary>
    public class ExportExcelHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {

        protected override void AjaxRequest(HttpContext pContext)
        {
            switch (pContext.Request.QueryString["method"])
            { 
                case "GivingOutAwardsListExport":
                    GivingOutAwardsListExport(pContext);
                    break; 
                case "SalesListExport":
                    SalesListExport(pContext);
                    break;
                case "GameAwardsListExport":
                    GameAwardsListExport(pContext);
                    break;
                case "SalesItemsListExport":
                    SalesItemsListExport(pContext);
                    break;
                case "OrderMoneyExport"://订单金额5天排行  导出Excel
                    OrderMoneyExport(pContext);
                    break;
                case "OrderCountExport"://订单数5天排行 导出Excel
                    OrderCountExport(pContext);
                    break;
                case "GameVipAddExport"://游戏会员增长排行 导出Excel
                    GameVipAddExport(pContext);
                    break;
                case "PromotionVipAddExport"://促销会员增长排行 导出Excel
                    PromotionVipAddExport(pContext);
                    break;
            }
            pContext.Response.End();
        }

        public void GivingOutAwardsListExport(HttpContext pContext)
        {
            GetEventPrizeDetailListRP form = Request("param").DeserializeJSONTo<GetEventPrizeDetailListRP>();
            T_CTW_LEventBLL _T_CTW_LEventBLL = new JIT.CPOS.BS.BLL.T_CTW_LEventBLL(CurrentUserInfo);

            DataSet ds = _T_CTW_LEventBLL.GetEventPrizeDetailList(form.LeventId, 60000, 0, CurrentUserInfo.ClientID);//订单导出，记录数0-60000
            List<EventPrizeDetailInfo> eventPrizeDetailInfo = new List<EventPrizeDetailInfo>();

            if (ds.Tables.Count > 0)
            {
               if (ds.Tables[1] != null && ds.Tables[1].Columns.Count > 0)
               {
                  eventPrizeDetailInfo = DataTableToObject.ConvertToList<EventPrizeDetailInfo>(ds.Tables[1]);
               }
            }

            if(eventPrizeDetailInfo != null)
            {
                string MapUrl = pContext.Server.MapPath(@"~/Framework/Upload/" + DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss.ms") + ".xls");
                Aspose.Cells.License lic = new Aspose.Cells.License();
                lic.SetLicense("Aspose.Total.lic");
                Workbook workbook = new Workbook();
                Worksheet sheetOne = workbook.Worksheets[0];
                Cells sheetOneCells = sheetOne.Cells;//单元格

                #region excel初始化
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

                sheetOneCells.Merge(0, 0, 1, 5);//合并单元格
                sheetOneCells[0, 0].PutValue("奖品发放清单");//填写内容
                sheetOneCells[0, 0].SetStyle(styleTitle);
                sheetOneCells.SetRowHeight(0, 38);
                for (int i = 0; i < 5; i++)
                {
                    sheetOneCells[1, i].SetStyle(style2);
                    sheetOneCells.SetColumnWidth(i, 30);
                }
                sheetOneCells.SetRowHeight(1, 25);

                sheetOneCells[1, 0].PutValue("奖品名称");
                sheetOneCells[1, 1].PutValue("中奖人");
                sheetOneCells[1, 2].PutValue("中奖时间");
                sheetOneCells[1, 3].PutValue("是否使用");
                sheetOneCells[1, 4].PutValue("是否关注");

                for (int i = 0;i < eventPrizeDetailInfo.Count;i++)
                {
                    sheetOneCells[i + 2, 0].PutValue(eventPrizeDetailInfo[i].Name);
                    sheetOneCells[i + 2, 0].SetStyle(style3);

                    sheetOneCells[i + 2, 1].PutValue(eventPrizeDetailInfo[i].vipname);
                    sheetOneCells[i + 2, 1].SetStyle(style3);

                    sheetOneCells[i + 2, 2].PutValue(eventPrizeDetailInfo[i].winTime);
                    sheetOneCells[i + 2, 2].SetStyle(style3);

                    sheetOneCells[i + 2, 3].PutValue(eventPrizeDetailInfo[i].PrizeUsed);
                    sheetOneCells[i + 2, 3].SetStyle(style3);

                    sheetOneCells[i + 2, 4].PutValue(eventPrizeDetailInfo[i].subscribe);
                    sheetOneCells[i + 2, 4].SetStyle(style3);

                    sheetOneCells.SetRowHeight(2 + i, 24);
                }

                workbook.Save(MapUrl);
                Utils.OutputExcel(pContext, MapUrl);//输出Excel文件
            }


        }
        public void SalesListExport(HttpContext pContext)
        {
            GeEventItemDetailListRP form = Request("param").DeserializeJSONTo<GeEventItemDetailListRP>();
            T_CTW_LEventBLL _T_CTW_LEventBLL = new JIT.CPOS.BS.BLL.T_CTW_LEventBLL(CurrentUserInfo);

            DataSet ds = _T_CTW_LEventBLL.GeEventItemDetailList(form.LeventId,60000,0,CurrentUserInfo.ClientID);
            List<EventItemDetailInfo> eventItemInfo = new List<EventItemDetailInfo>();

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[1] != null && ds.Tables[1].Columns.Count > 0)
                {
                    eventItemInfo = DataTableToObject.ConvertToList<EventItemDetailInfo>(ds.Tables[1]);
                }

            }

            if (eventItemInfo != null)
            {
                string MapUrl = pContext.Server.MapPath(@"~/Framework/Upload/" + DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss.ms") + ".xls");
                Aspose.Cells.License lic = new Aspose.Cells.License();
                lic.SetLicense("Aspose.Total.lic");
                Workbook workbook = new Workbook();
                Worksheet sheetOne = workbook.Worksheets[0];
                Cells sheetOneCells = sheetOne.Cells;//单元格

                #region excel初始化
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

                sheetOneCells.Merge(0, 0, 1, 7);//合并单元格
                sheetOneCells[0, 0].PutValue("销售清单");//填写内容
                sheetOneCells[0, 0].SetStyle(styleTitle);
                sheetOneCells.SetRowHeight(0, 38);
                for (int i = 0; i < 7; i++)
                {
                    sheetOneCells[1, i].SetStyle(style2);
                    sheetOneCells.SetColumnWidth(i, 30);
                }
                sheetOneCells.SetRowHeight(1, 25);

                sheetOneCells[1, 0].PutValue("订单号");
                sheetOneCells[1, 1].PutValue("商品名称");
                sheetOneCells[1, 2].PutValue("原价");
                sheetOneCells[1, 3].PutValue("团购价");
                sheetOneCells[1, 4].PutValue("订购人");
                sheetOneCells[1, 5].PutValue("配送方式");
                sheetOneCells[1, 6].PutValue("成交日期");

                for (int i = 0; i < eventItemInfo.Count; i++)
                {
                    sheetOneCells[i + 2, 0].PutValue(eventItemInfo[i].order_no);
                    sheetOneCells[i + 2, 0].SetStyle(style3);

                    sheetOneCells[i + 2, 1].PutValue(eventItemInfo[i].item_name);
                    sheetOneCells[i + 2, 1].SetStyle(style3);

                    sheetOneCells[i + 2, 1].PutValue(eventItemInfo[i].price);
                    sheetOneCells[i + 2, 1].SetStyle(style3);

                    sheetOneCells[i + 2, 2].PutValue(eventItemInfo[i].SalesPrice);
                    sheetOneCells[i + 2, 2].SetStyle(style3);

                    sheetOneCells[i + 2, 3].PutValue(eventItemInfo[i].vipname);
                    sheetOneCells[i + 2, 3].SetStyle(style3);

                    sheetOneCells[i + 2, 4].PutValue(eventItemInfo[i].DeliveryName);
                    sheetOneCells[i + 2, 4].SetStyle(style3);

                    sheetOneCells[i + 2, 5].PutValue(eventItemInfo[i].create_time);
                    sheetOneCells[i + 2, 5].SetStyle(style3);

                    sheetOneCells[i + 2, 6].PutValue(eventItemInfo[i].create_time);
                    sheetOneCells[i + 2, 6].SetStyle(style3);

                    sheetOneCells.SetRowHeight(2 + i, 24);
                }

                workbook.Save(MapUrl);
                Utils.OutputExcel(pContext, MapUrl);//输出Excel文件
            }

        }
        public void GameAwardsListExport(HttpContext pContext)
        {
            GetEventPrizeListRP form = Request("param").DeserializeJSONTo<GetEventPrizeListRP>();
            T_CTW_LEventBLL _T_CTW_LEventBLL = new JIT.CPOS.BS.BLL.T_CTW_LEventBLL(CurrentUserInfo);

            DataSet ds = _T_CTW_LEventBLL.GetEventPrizeList(form.LeventId, 60000, 0, CurrentUserInfo.ClientID);
            List<EventPrizeInfo> eventPrizeInfo = new List<EventPrizeInfo>();

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[1] != null && ds.Tables[1].Columns.Count > 0)
                {
                    eventPrizeInfo = DataTableToObject.ConvertToList<EventPrizeInfo>(ds.Tables[1]);
                }

            }
            if (eventPrizeInfo != null)
            {
                string MapUrl = pContext.Server.MapPath(@"~/Framework/Upload/" + DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss.ms") + ".xls");
                Aspose.Cells.License lic = new Aspose.Cells.License();
                lic.SetLicense("Aspose.Total.lic");
                Workbook workbook = new Workbook();
                Worksheet sheetOne = workbook.Worksheets[0];
                Cells sheetOneCells = sheetOne.Cells;//单元格

                #region excel初始化
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

                sheetOneCells.Merge(0, 0, 1, 7);//合并单元格
                sheetOneCells[0, 0].PutValue("游戏奖品清单");//填写内容
                sheetOneCells[0, 0].SetStyle(styleTitle);
                sheetOneCells.SetRowHeight(0, 38);
                for (int i = 0; i < 7; i++)
                {
                    sheetOneCells[1, i].SetStyle(style2);
                    sheetOneCells.SetColumnWidth(i, 30);
                }
                sheetOneCells.SetRowHeight(1, 25);

                sheetOneCells[1, 0].PutValue("奖品名称");
                sheetOneCells[1, 1].PutValue("已发放");
                sheetOneCells[1, 2].PutValue("剩余");
                sheetOneCells[1, 3].PutValue("已发放未使用");
                sheetOneCells[1, 4].PutValue("已使用");
                sheetOneCells[1, 5].PutValue("代动销量");
                sheetOneCells[1, 6].PutValue("奖品标识");

                for (int i = 0; i < eventPrizeInfo.Count; i++)
                {
                    sheetOneCells[i + 2, 0].PutValue(eventPrizeInfo[i].PrizeName);
                    sheetOneCells[i + 2, 0].SetStyle(style3);

                    sheetOneCells[i + 2, 1].PutValue(eventPrizeInfo[i].winnerCount);
                    sheetOneCells[i + 2, 1].SetStyle(style3);

                    sheetOneCells[i + 2, 2].PutValue(eventPrizeInfo[i].RemindCount);
                    sheetOneCells[i + 2, 2].SetStyle(style3);

                    sheetOneCells[i + 2, 3].PutValue(eventPrizeInfo[i].NotUsedCount);
                    sheetOneCells[i + 2, 3].SetStyle(style3);

                    sheetOneCells[i + 2, 4].PutValue(eventPrizeInfo[i].UsedCount);
                    sheetOneCells[i + 2, 4].SetStyle(style3);

                    sheetOneCells[i + 2, 5].PutValue(eventPrizeInfo[i].prizeSale);
                    sheetOneCells[i + 2, 5].SetStyle(style3);

                    sheetOneCells[i + 2, 6].PutValue(eventPrizeInfo[i].PrizesID);
                    sheetOneCells[i + 2, 6].SetStyle(style3);

                    sheetOneCells.SetRowHeight(2 + i, 24);
                }

                workbook.Save(MapUrl);
                Utils.OutputExcel(pContext, MapUrl);//输出Excel文件
            }
        }
        public void SalesItemsListExport(HttpContext pContext)
        {
            GeEventItemListRP form = Request("param").DeserializeJSONTo<GeEventItemListRP>();
            T_CTW_LEventBLL _T_CTW_LEventBLL = new JIT.CPOS.BS.BLL.T_CTW_LEventBLL(CurrentUserInfo);

            DataSet ds = _T_CTW_LEventBLL.GetEventPrizeList(form.LeventId, 60000, 0, CurrentUserInfo.ClientID);
            List<EventItemInfo> eventItemInfo = new List<EventItemInfo>();

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[1] != null && ds.Tables[1].Columns.Count > 0)
                {
                    eventItemInfo = DataTableToObject.ConvertToList<EventItemInfo>(ds.Tables[1]);
                }

            }
            if (eventItemInfo != null)
            {
                string MapUrl = pContext.Server.MapPath(@"~/Framework/Upload/" + DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss.ms") + ".xls");
                Aspose.Cells.License lic = new Aspose.Cells.License();
                lic.SetLicense("Aspose.Total.lic");
                Workbook workbook = new Workbook();
                Worksheet sheetOne = workbook.Worksheets[0];
                Cells sheetOneCells = sheetOne.Cells;//单元格

                #region excel初始化
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

                sheetOneCells.Merge(0, 0, 1, 9);//合并单元格
                sheetOneCells[0, 0].PutValue("销售商品清单");//填写内容
                sheetOneCells[0, 0].SetStyle(styleTitle);
                sheetOneCells.SetRowHeight(0, 38);
                for (int i = 0; i < 9; i++)
                {
                    sheetOneCells[1, i].SetStyle(style2);
                    sheetOneCells.SetColumnWidth(i, 30);
                }
                sheetOneCells.SetRowHeight(1, 25);

                sheetOneCells[1, 0].PutValue("商品名称");
                sheetOneCells[1, 1].PutValue("原价");
                sheetOneCells[1, 2].PutValue("团购价");
                sheetOneCells[1, 3].PutValue("商品数量");
                sheetOneCells[1, 4].PutValue("已售数量基数");
                sheetOneCells[1, 5].PutValue("真实销售");
                sheetOneCells[1, 6].PutValue("当前库存");
                sheetOneCells[1, 7].PutValue("总销售额");
                sheetOneCells[1, 8].PutValue("成交率");
             

                for (int i = 0; i < eventItemInfo.Count; i++)
                {
                    sheetOneCells[i + 2, 0].PutValue(eventItemInfo[i].item_name);
                    sheetOneCells[i + 2, 0].SetStyle(style3);

                    sheetOneCells[i + 2, 1].PutValue(eventItemInfo[i].price);
                    sheetOneCells[i + 2, 1].SetStyle(style3);

                    sheetOneCells[i + 2, 2].PutValue(eventItemInfo[i].SalesPrice);
                    sheetOneCells[i + 2, 2].SetStyle(style3);

                    sheetOneCells[i + 2, 3].PutValue(eventItemInfo[i].Qty);
                    sheetOneCells[i + 2, 3].SetStyle(style3);

                    sheetOneCells[i + 2, 4].PutValue(eventItemInfo[i].KeepQty);
                    sheetOneCells[i + 2, 4].SetStyle(style3);

                    sheetOneCells[i + 2, 5].PutValue(eventItemInfo[i].SoldQty);
                    sheetOneCells[i + 2, 5].SetStyle(style3);

                    sheetOneCells[i + 2, 6].PutValue(eventItemInfo[i].InverTory);
                    sheetOneCells[i + 2, 6].SetStyle(style3);

                    sheetOneCells[i + 2, 7].PutValue(eventItemInfo[i].TotalSales);
                    sheetOneCells[i + 2, 7].SetStyle(style3);

                    sheetOneCells[i + 2, 8].PutValue(eventItemInfo[i].TurnoverRate);
                    sheetOneCells[i + 2, 8].SetStyle(style3);


                    sheetOneCells.SetRowHeight(2 + i, 24);
                }

                workbook.Save(MapUrl);
                Utils.OutputExcel(pContext, MapUrl);//输出Excel文件
            }
        }


        public void OrderMoneyExport(HttpContext pContext)
        {
            try
            {

                var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

                T_CTW_LEventBLL bllLEvent = new T_CTW_LEventBLL(loggingSessionInfo);
                DataSet ds = null;
                if (FormatParamValue(Request("ctweventId")) != null && FormatParamValue(Request("ctweventId")).ToString() != "")
                {
                    ds = bllLEvent.GetCTW_PanicbuyingEventRankingStats(Request("ctweventId").ToString());
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            //rd.OrderMoneyRankList = DataTableToObject.ConvertToList<OrderMoneyRank>(ds.Tables[0]);                            

                            string MapUrl = pContext.Server.MapPath(@"~/Framework/Upload/订单金额5天排行" + DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss.ms") + ".xls");
                            Aspose.Cells.License lic = new Aspose.Cells.License();
                            lic.SetLicense("Aspose.Total.lic");
                            Workbook workbook = new Workbook();
                            Worksheet sheetOne = workbook.Worksheets[0];
                            workbook.Worksheets.Add();
                            Cells sheetOneCells = sheetOne.Cells;//单元格

                            #region excel初始化
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

                            sheetOneCells.Merge(0, 0, 1, 2);//合并单元格
                            sheetOneCells[0, 0].PutValue("订单数5天排行");//填写内容
                            sheetOneCells[0, 0].SetStyle(styleTitle);
                            sheetOneCells.SetRowHeight(0, 38);
                            //生成行2 列名行
                            for (int i = 0; i < 21; i++)
                            {
                                sheetOneCells.SetColumnWidth(i, 30);
                            }

                            sheetOneCells[1, 0].PutValue("订单日期");
                            sheetOneCells[1, 0].SetStyle(style2);

                            sheetOneCells[1, 1].PutValue("订单金额");
                            sheetOneCells[1, 1].SetStyle(style2);
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                sheetOneCells[2 + i, 0].PutValue(ds.Tables[0].Rows[i]["DateStr"]);
                                sheetOneCells[2 + i, 0].SetStyle(style3);

                                sheetOneCells[2 + i, 1].PutValue(ds.Tables[0].Rows[i]["OrderActualAmount"]);
                                sheetOneCells[2 + i, 1].SetStyle(style3);
                                sheetOneCells.SetRowHeight(2 + i, 24);
                            }
                            workbook.Save(MapUrl);
                            Utils.OutputExcel(pContext, MapUrl);//输出Excel文件
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void OrderCountExport(HttpContext pContext)
        {
            try
            {

                var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

                T_CTW_LEventBLL bllLEvent = new T_CTW_LEventBLL(loggingSessionInfo);
                DataSet ds = null;
                if (FormatParamValue(Request("ctweventId")) != null && FormatParamValue(Request("ctweventId")).ToString() != "")
                {
                    ds = bllLEvent.GetCTW_PanicbuyingEventRankingStats(Request("ctweventId").ToString());
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            //rd.OrderMoneyRankList = DataTableToObject.ConvertToList<OrderMoneyRank>(ds.Tables[0]);                            

                            string MapUrl = pContext.Server.MapPath(@"~/Framework/Upload/订单数5天排行" + DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss.ms") + ".xls");
                            Aspose.Cells.License lic = new Aspose.Cells.License();
                            lic.SetLicense("Aspose.Total.lic");
                            Workbook workbook = new Workbook();
                            Worksheet sheetOne = workbook.Worksheets[0];
                            workbook.Worksheets.Add();
                            Cells sheetOneCells = sheetOne.Cells;//单元格

                            #region excel初始化
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

                            sheetOneCells.Merge(0, 0, 1, 2);//合并单元格
                            sheetOneCells[0, 0].PutValue("订单数5天排行");//填写内容
                            sheetOneCells[0, 0].SetStyle(styleTitle);
                            sheetOneCells.SetRowHeight(0, 38);
                            //生成行2 列名行
                            for (int i = 0; i < 21; i++)
                            {
                                sheetOneCells.SetColumnWidth(i, 30);
                            }

                            sheetOneCells[1, 0].PutValue("订单日期");
                            sheetOneCells[1, 0].SetStyle(style2);

                            sheetOneCells[1, 1].PutValue("订单数量");
                            sheetOneCells[1, 1].SetStyle(style2);
                            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                            {
                                sheetOneCells[2 + i, 0].PutValue(ds.Tables[1].Rows[i]["DateStr"]);
                                sheetOneCells[2 + i, 0].SetStyle(style3);

                                sheetOneCells[2 + i, 1].PutValue(ds.Tables[1].Rows[i]["OrderCount"]);
                                sheetOneCells[2 + i, 1].SetStyle(style3);
                                sheetOneCells.SetRowHeight(2 + i, 24);
                            }
                            workbook.Save(MapUrl);
                            Utils.OutputExcel(pContext, MapUrl);//输出Excel文件
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void GameVipAddExport(HttpContext pContext)
        {
            try
            {


                var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

                T_CTW_LEventBLL bllLEvent = new T_CTW_LEventBLL(loggingSessionInfo);
                //获取游戏与促销会员增长排行
                DataSet dsVipAddRankingStats = null;
                if (FormatParamValue(Request("ctweventId")) != null && FormatParamValue(Request("ctweventId")).ToString() != "")
                {
                    dsVipAddRankingStats = bllLEvent.GetVipAddRankingStats(Request("ctweventId").ToString());
                    if (dsVipAddRankingStats != null && dsVipAddRankingStats.Tables.Count > 0)
                    {
                        if (dsVipAddRankingStats.Tables[0].Rows.Count > 0)
                        {
                            string MapUrl = pContext.Server.MapPath(@"~/Framework/Upload/游戏会员增长排行" + DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss.ms") + ".xls");
                            Aspose.Cells.License lic = new Aspose.Cells.License();
                            lic.SetLicense("Aspose.Total.lic");
                            Workbook workbook = new Workbook();
                            Worksheet sheetOne = workbook.Worksheets[0];
                            workbook.Worksheets.Add();
                            Cells sheetOneCells = sheetOne.Cells;//单元格

                            #region excel初始化
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

                            sheetOneCells.Merge(0, 0, 1, 2);//合并单元格
                            sheetOneCells[0, 0].PutValue("游戏会员增长排行");//填写内容
                            sheetOneCells[0, 0].SetStyle(styleTitle);
                            sheetOneCells.SetRowHeight(0, 38);
                            //生成行2 列名行
                            for (int i = 0; i < 21; i++)
                            {
                                sheetOneCells.SetColumnWidth(i, 30);
                            }

                            sheetOneCells[1, 0].PutValue("日期");
                            sheetOneCells[1, 0].SetStyle(style2);

                            sheetOneCells[1, 1].PutValue("关注会员数");
                            sheetOneCells[1, 1].SetStyle(style2);
                            for (int i = 0; i < dsVipAddRankingStats.Tables[0].Rows.Count; i++)
                            {
                                sheetOneCells[2 + i, 0].PutValue(dsVipAddRankingStats.Tables[0].Rows[i]["DateStr"]);
                                sheetOneCells[2 + i, 0].SetStyle(style3);

                                sheetOneCells[2 + i, 1].PutValue(dsVipAddRankingStats.Tables[0].Rows[i]["FocusVipCount"]);
                                sheetOneCells[2 + i, 1].SetStyle(style3);
                                sheetOneCells.SetRowHeight(2 + i, 24);
                            }
                            workbook.Save(MapUrl);
                            Utils.OutputExcel(pContext, MapUrl);//输出Excel文件
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void PromotionVipAddExport(HttpContext pContext)
        {
            try
            {


                var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

                T_CTW_LEventBLL bllLEvent = new T_CTW_LEventBLL(loggingSessionInfo);
                //获取游戏与促销会员增长排行
                DataSet dsVipAddRankingStats = null;
                if (FormatParamValue(Request("ctweventId")) != null && FormatParamValue(Request("ctweventId")).ToString() != "")
                {
                    dsVipAddRankingStats = bllLEvent.GetVipAddRankingStats(Request("ctweventId").ToString());
                    if (dsVipAddRankingStats != null && dsVipAddRankingStats.Tables.Count > 0)
                    {
                        if (dsVipAddRankingStats.Tables[1].Rows.Count > 0)
                        {
                            string MapUrl = pContext.Server.MapPath(@"~/Framework/Upload/促销会员增长排行" + DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss.ms") + ".xls");
                            Aspose.Cells.License lic = new Aspose.Cells.License();
                            lic.SetLicense("Aspose.Total.lic");
                            Workbook workbook = new Workbook();
                            Worksheet sheetOne = workbook.Worksheets[0];
                            workbook.Worksheets.Add();
                            Cells sheetOneCells = sheetOne.Cells;//单元格

                            #region excel初始化
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

                            sheetOneCells.Merge(0, 0, 1, 2);//合并单元格
                            sheetOneCells[0, 0].PutValue("促销会员增长排行");//填写内容
                            sheetOneCells[0, 0].SetStyle(styleTitle);
                            sheetOneCells.SetRowHeight(0, 38);
                            //生成行2 列名行
                            for (int i = 0; i < 21; i++)
                            {
                                sheetOneCells.SetColumnWidth(i, 30);
                            }

                            sheetOneCells[1, 0].PutValue("日期");
                            sheetOneCells[1, 0].SetStyle(style2);

                            sheetOneCells[1, 1].PutValue("注册会员数");
                            sheetOneCells[1, 1].SetStyle(style2);
                            for (int i = 0; i < dsVipAddRankingStats.Tables[1].Rows.Count; i++)
                            {
                                sheetOneCells[2 + i, 0].PutValue(dsVipAddRankingStats.Tables[1].Rows[i]["DateStr"]);
                                sheetOneCells[2 + i, 0].SetStyle(style3);

                                sheetOneCells[2 + i, 1].PutValue(dsVipAddRankingStats.Tables[1].Rows[i]["RegVipCount"]);
                                sheetOneCells[2 + i, 1].SetStyle(style3);
                                sheetOneCells.SetRowHeight(2 + i, 24);
                            }
                            workbook.Save(MapUrl);
                            Utils.OutputExcel(pContext, MapUrl);//输出Excel文件
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}