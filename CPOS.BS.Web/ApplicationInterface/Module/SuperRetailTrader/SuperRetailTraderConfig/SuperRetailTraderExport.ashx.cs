using Aspose.Cells;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.Common;
using JIT.CPOS.DTO.Module.RetailTrader.Request;
using JIT.CPOS.DTO.Module.RetailTrader.Response;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.SuperRetailTrader.SuperRetailTraderConfig
{
    /// <summary>
    /// SuperRetailTraderExport 的摘要说明
    /// </summary>
    public class SuperRetailTraderExport : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        protected override void AjaxRequest(HttpContext pContext)
        {

            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo; //登录状态信息


            //var form = Request("param").DeserializeJSONTo<GetTSuperRetailTraderConfigRP>();

            int PageIndex = Convert.ToInt32(pContext.Request.QueryString["PageIndex"]);
            int PageSize = Convert.ToInt32(pContext.Request.QueryString["PageSizes"]);
            string SuperRetailTraderName = pContext.Request.QueryString["SuperRetailTraderName"] + "";
            string SuperRetailTraderFrom = pContext.Request.QueryString["SuperRetailTraderFrom"] + "";
            string JoinSatrtTime = pContext.Request.QueryString["JoinSatrtTime"] + "";
            string JoinEndTime = pContext.Request.QueryString["JoinEndTime"] + "";

            List<IWhereCondition> pWhereConditions = new List<IWhereCondition>() {
            new EqualsCondition() { FieldName = "a.CustomerId", Value = loggingSessionInfo .ClientID},
            new EqualsCondition() { FieldName = "a.IsDelete", Value =0},
            new EqualsCondition(){ FieldName="a.Status", Value="10"}
            };

            if (!String.IsNullOrEmpty(SuperRetailTraderName))
                pWhereConditions.Add(new EqualsCondition() { FieldName = "a.SuperRetailTraderName", Value = SuperRetailTraderName });

            if (!String.IsNullOrEmpty(SuperRetailTraderFrom))
                pWhereConditions.Add(new EqualsCondition() { FieldName = "a.SuperRetailTraderFrom", Value = SuperRetailTraderFrom });

            if (!String.IsNullOrEmpty(JoinSatrtTime))
                pWhereConditions.Add(new EqualsCondition() { FieldName = "a.JoinSatrtTime", Value = JoinSatrtTime });

            if (!String.IsNullOrEmpty(JoinEndTime))
                pWhereConditions.Add(new EqualsCondition() { FieldName = "a.JoinEndTime", Value = JoinEndTime });

            T_SuperRetailTraderBLL bll = new T_SuperRetailTraderBLL(loggingSessionInfo);
            PagedQueryResult<T_SuperRetailTraderEntity> models = bll.FindListByCustomerId(pWhereConditions.ToArray(), null, PageIndex, PageSize, loggingSessionInfo.ClientID);
            try
            {
                #region 获取信息

                //var param = Request("param").DeserializeJSONTo<dynamic>();
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
                cells.Merge(0, 0, 1, 9);//合并单元格
                cells[0, 0].PutValue("分销商列表");//填写内容
                cells[0, 0].SetStyle(styleTitle);
                cells.SetRowHeight(0, 38);


                List<SuperRetailTraderListInfo> lst = models.Entities.Select(m => new SuperRetailTraderListInfo()
                {
                    JoinTime = Convert.ToDateTime(m.JoinTime).ToString("yyyy-MM-dd"),
                    NumberOffline = m.NumberOffline,
                    OrderCount = m.OrderCount,
                    SuperRetailTraderFrom = m.SuperRetailTraderFrom,
                    SuperRetailTraderName = m.SuperRetailTraderName,
                    SuperRetailTraderPhone = m.SuperRetailTraderPhone,
                    WithdrawCount = m.WithdrawCount,
                    WithdrawTotalMoney = m.WithdrawTotalMoney
                }).ToList();

                //生成行2 列名行
                for (int i = 0; i < 8; i++)
                {
                    cells.SetColumnWidth(i, 30);
                }
                #region 列明
                cells[1, 0].PutValue("分销商姓名");
                cells[1, 0].SetStyle(style2);
                cells.SetRowHeight(1, 25);

                cells[1, 1].PutValue("分销商手机号码");
                cells[1, 1].SetStyle(style2);
                cells.SetRowHeight(1, 25);
                cells[1, 2].PutValue("分销商来源");
                cells[1, 2].SetStyle(style2);
                cells.SetRowHeight(1, 25);
                cells[1, 3].PutValue("分销商下线人数");
                cells[1, 3].SetStyle(style2);
                cells.SetRowHeight(1, 25);
                cells[1, 4].PutValue("分销商订单总数");
                cells[1, 4].SetStyle(style2);
                cells[1, 5].PutValue("分销商提现次数");
                cells[1, 5].SetStyle(style2);
                cells[1, 6].PutValue("分销商提现总金额");
                cells[1, 6].SetStyle(style2);
                cells[1, 7].PutValue("加盟时间");
                cells.SetRowHeight(1, 25);
                #endregion

                #region 生成数据行
                for (int i = 0; i < lst.Count; i++)
                {
                    cells[2 + i, 0].PutValue(lst[i].SuperRetailTraderName);
                    cells[2 + i, 0].SetStyle(style3);

                    cells[2 + i, 1].PutValue(lst[i].SuperRetailTraderPhone);
                    cells[2 + i, 1].SetStyle(style3);

                    cells[2 + i, 2].PutValue(lst[i].SuperRetailTraderFrom);
                    cells[2 + i, 2].SetStyle(style3);

                    cells[2 + i, 3].PutValue(lst[i].NumberOffline);
                    cells[2 + i, 3].SetStyle(style3);

                    cells[2 + i, 4].PutValue(lst[i].OrderCount);
                    cells[2 + i, 4].SetStyle(style3);

                    cells[2 + i, 5].PutValue(lst[i].WithdrawCount);
                    cells[2 + i, 5].SetStyle(style3);

                    cells[2 + i, 6].PutValue(lst[i].WithdrawTotalMoney);
                    cells[2 + i, 6].SetStyle(style3);

                    cells[2 + i, 7].PutValue(lst[i].JoinTime);
                    cells[2 + i, 7].SetStyle(style3);

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
    }
}