using System;
using System.Collections.Generic;
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
using JIT.CPOS.BS.Entity.User;
using System.Configuration;
using System.IO;
using JIT.Utility.Log;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Web.Module.WEvents.Handler;
using JIT.Utility.DataAccess.Query;
using Aspose.Cells;
using JIT.CPOS.DTO.Module.Questionnaire.ActivityQuestionnaireMapping.Response;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Questionnaire.Questionnaire
{
    /// <summary>
    /// DownLoadQuestionnaireInfor 的摘要说明
    /// </summary>
    public class DownLoadQuestionnaireInfor : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = "";
            switch (pContext.Request.QueryString["method"])
            {
                case "DownLoadQuestionnaireInfor":
                    DownLoad_QuestionnaireInfor(pContext);
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        private void DownLoad_QuestionnaireInfor(HttpContext pContext)
        {
            try
            {
                #region 获取信息
                string ActivityID = FormatParamValue(Request("ActivityID"));//活动id
                string QuestionnaireID = FormatParamValue(Request("QuestionnaireID"));//问卷id
                string filename = FormatParamValue(Request("filename"));//导出的文件名

                TitleName[] TitleData;
               System.Data.DataTable list_QuestionnaireInforEntity =GetQuestionnaireInforAH.GetQuestionnaireInfor(ActivityID, QuestionnaireID, out TitleData);

                var couponBLL = new CouponBLL(CurrentUserInfo);

                if(TitleData==null)
                {
                    return;
                }

                #endregion

                if (filename == null || filename == "")
                {
                    filename = "优惠券";
                }

                string filesrc = @"~/Framework/Upload/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                if (!System.IO.Directory.Exists(pContext.Server.MapPath(filesrc)))
                {
                    System.IO.Directory.CreateDirectory(pContext.Server.MapPath(filesrc));
                }


                string MapUrl = pContext.Server.MapPath(filesrc + filename + ".xls");
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
                cells.Merge(0, 0, 1, TitleData.Length);//合并单元格

                cells[0, 0].PutValue(filename);//填写内容
                cells[0, 0].SetStyle(styleTitle);
                cells.SetRowHeight(0, 38);

                #region 列名

                for (int i = 0; i < TitleData.Length; i++)
                {
                    cells[1, i].PutValue(TitleData[i].Name);
                    cells[1, i].SetStyle(style2);

                    cells.SetRowHeight(1, 30);
                }
                #endregion


                #region 生成数据行
                for (int i = 0; i < list_QuestionnaireInforEntity.Rows.Count; i++)
                {
                    for (int k = 0; k < TitleData.Length; k++)
                    {
                        if (TitleData[k].NameID != "ID")
                        {
                            cells[2 + i, k].PutValue(list_QuestionnaireInforEntity.Rows[i][TitleData[k].NameID]);
                            cells[2 + i, k].SetStyle(style3);
                        }
                    }
                    

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