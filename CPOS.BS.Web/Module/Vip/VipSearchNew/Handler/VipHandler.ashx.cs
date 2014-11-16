using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Script.Serialization;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.Extension;
using JIT.CPOS.Common;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Reflection;
using JIT.Utility.Web;
using JIT.CPOS.BS.Entity.User;
using JIT.CPOS.BS.Entity.Pos;
using System.Collections;
using JIT.CPOS.BS.BLL.Module.BasicData;
using JIT.CPOS.BS.Web.Base.Extension;
using Aspose.Cells;
using JIT.CPOS.BS.Web.Base.Excel;
using System.IO;
using System.Drawing;
using JIT.Utility.Log;

namespace JIT.CPOS.BS.Web.Module.Vip.VipSearchNew.Handler
{
    /// <summary>
    /// VipHandler 的摘要说明
    /// </summary>
    public class VipHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        public class GridInitEntity
        {
            /// <summary>
            /// 表格数据定义
            /// </summary>
            public List<GridColumnModelEntity> GridDataDefinds { get; set; }
            /// <summary>
            /// 表格表头定义
            /// </summary>
            public List<GridColumnEntity> GridColumnDefinds { get; set; }
            /// <summary>
            /// 表格数据
            /// </summary>
            public PageResultEntity GridDatas { get; set; }

        }
        protected override void AjaxRequest(HttpContext pContext)
        {
            pContext.Response.ContentType = "text/plain";

            string res = "";
            switch (pContext.Request["method"])
            {
                case "InitGridData": //初始化表格
                    res = GetInitGridData(pContext).ToJSON();
                    break;
                case "PageGridData":
                    res = GetPageData(pContext).ToJSON();
                    break;
                case "QueryView": //得到查询控件
                    res = GetQueryConditionControls(pContext).ToJSON();
                    break;
                case "EditViewData": //得到表单数据
                    res = GetEditData(pContext).ToJSON();
                    break;
                case "EditView": //得到页面控件
                    res = GetEditControls(pContext).ToJSON();
                    break;
                case "update": //更新逻辑
                    res = Update(pContext);
                    break;
                case "create": //创建
                    res = Create(pContext);
                    break;
                case "delete": //删除
                    res = Delete(pContext);
                    break;
                case "export":
                    res = Export(pContext);
                    break;

                case "GetRoleList":
                    res = GetRoleList();
                    break;

                case "GetImportFile":
                    res = GetImportFile(pContext);
                    break;
                case "ImportFile":
                    res = ImportFile(pContext);
                    break;

                case "CheckIsRepeat": //检查重复
                    res = GetRepeatRowCount(pContext);
                    break;
            }

            pContext.Response.Write(res);
            pContext.Response.End();

        }

        /// <summary>
        /// 初始化会员列表的定义数据模型与列表定义
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private GridInitEntity GetInitGridData(HttpContext pContext)
        {
            GridInitEntity g = new GridInitEntity();
            g.GridDataDefinds = GetGridDataModels(pContext);
            g.GridColumnDefinds = GetGridColumns(pContext);
            return g;

        }

        /// <summary>
        /// 获取查询控件定议
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private List<DefindControlEntity> GetQueryConditionControls(HttpContext pContext)
        {
            VIPDefindModuleBLL b = new VIPDefindModuleBLL(CurrentUserInfo, GetTableName(pContext));
            return b.GetQueryConditionControls();
        }

        /// <summary>
        /// 获取终端列表数据定义模型
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private List<GridColumnModelEntity> GetGridDataModels(HttpContext pContext)
        {
            VipBLLByNew b = new VipBLLByNew(CurrentUserInfo, GetTableName(pContext));
            return b.GetGridDataModels();

        }
        /// <summary>
        /// 获取表头定义
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private List<GridColumnEntity> GetGridColumns(HttpContext pContext)
        {
            VipBLLByNew b = new VipBLLByNew(CurrentUserInfo, GetTableName(pContext));
            return b.GetGridColumns();

        }
        /// <summary>
        /// 编辑控件
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private List<DefindControlEntity> GetEditControls(HttpContext pContext)
        {
            StoreDefindModuleBLL b = new StoreDefindModuleBLL(CurrentUserInfo, GetTableName(pContext));
            return b.GetEditControls();
        }

        #region GetEditData
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private List<DefindControlEntity> GetEditData(HttpContext pContext)
        {
            VipBLLByNew b = new VipBLLByNew(CurrentUserInfo, GetTableName(pContext));
            string pKeyValue = pContext.Request["pKeyValue"];
            return b.GetEditData(pKeyValue);
            // return null;

        }
        #endregion

        #region GetPageData
        /// <summary>
        /// 点选终端分页查询控件
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private PageResultEntity GetPageData(HttpContext pContext)
        {
            VipBLLByNew b = new VipBLLByNew(CurrentUserInfo, GetTableName(pContext));
            string pSearch = pContext.Request["pSearch"];
            List<DefindControlEntity> l = new List<DefindControlEntity>();
            if (!string.IsNullOrEmpty(pSearch))
            {
                l = pSearch.DeserializeJSONTo<List<DefindControlEntity>>();

            }

            int? pPageIndex = null;
            int? pPageSize = null;
            if (!string.IsNullOrEmpty(pContext.Request["pPageIndex"]))
            {
                pPageIndex = Convert.ToInt32(pContext.Request["pPageIndex"]);
            }
            if (!string.IsNullOrEmpty(pContext.Request["pPageSize"]))
            {
                pPageSize = Convert.ToInt32(pContext.Request["pPageSize"]);
            }
            //string pKeyValue = pContext.Request["pKeyValue"];
            //string pUserArray = pContext.Request["pUserArray"];
            string correlationValue = pContext.Request["CorrelationValue"];
            if (string.IsNullOrEmpty(correlationValue.Split('&')[0]) || string.IsNullOrEmpty(correlationValue.Split('&')[1]))
            {
                correlationValue = "";
            }
            return b.GetPageData(l, pPageSize, pPageIndex, correlationValue);
        }
        #endregion

        #region Create
        /// <summary>
        /// 添加终端
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private string Create(HttpContext pContext)
        {
            VipBLLByNew b = new VipBLLByNew(CurrentUserInfo, GetTableName(pContext));
            string pEditValue = pContext.Request["pEditValue"];
            List<DefindControlEntity> l = new List<DefindControlEntity>();
            if (!string.IsNullOrEmpty(pEditValue))
            {
                l = pEditValue.DeserializeJSONTo<List<DefindControlEntity>>();
            }
            bool res = b.Create(l);

            if (res == true)
            {
                return "{success:true,msg:''}";
            }
            else
            {
                return "{success:false}";
            }


        }
        #endregion

        #region Delete
        /// <summary>
        /// 删除终端
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private string Delete(HttpContext pContext)
        {
            VipBLLByNew b = new VipBLLByNew(CurrentUserInfo, GetTableName(pContext));
            string pKeyValue = pContext.Request["pKeyValue"];
            bool res = b.Delete(pKeyValue);
            if (res == true)
            {
                return "{success:true}";
            }
            else
            {
                return "{success:false}";
            }
        }
        #endregion

        #region Update
        private string Update(HttpContext pContext)
        {
            VipBLLByNew b = new VipBLLByNew(CurrentUserInfo, GetTableName(pContext));
            string pKeyValue = pContext.Request["pKeyValue"];
            string pEditValue = pContext.Request["pEditValue"];
            List<DefindControlEntity> l = new List<DefindControlEntity>();
            if (!string.IsNullOrEmpty(pEditValue))
            {
                l = pEditValue.DeserializeJSONTo<List<DefindControlEntity>>();

            }
            bool res = b.Update(l, pKeyValue);
            if (res == true)
            {
                return "{success:true,msg:''}";
            }
            else
            {
                return "{success:false}";
            }
        }
        #endregion

        #region Export
        private string Export(HttpContext pContext)
        {
            //获取所有的符合条件的数据
            DataTable dtExport = GetStoreData(pContext);
            GridInitEntity g = GetInitGridData(pContext);

            #region 替换标题信息
            if (g != null && g.GridColumnDefinds != null)
            {
                if (dtExport.Columns.Contains("VIPID"))
                {
                    dtExport.Columns.Remove("VIPID");
                }
                for (int i = 0; i < dtExport.Columns.Count; i++)
                {
                    for (int j = 0; j < g.GridColumnDefinds.Count; j++)
                    {
                        if (dtExport.Columns[i].ColumnName == g.GridColumnDefinds[j].DataIndex)
                        {
                            dtExport.Columns[i].ColumnName = g.GridColumnDefinds[j].ColumnText;
                            break;
                        }
                    }
                }
            }
            #endregion
            Workbook wbExport = JIT.Utility.DataTableExporter.WriteXLS(dtExport, 0);
            wbExport.Worksheets[0].FreezePanes(1, 1, 1, 1);
            wbExport.Worksheets[0].AutoFitColumns();

            string mapPath = pContext.Server.MapPath(@"~/File/Excel/");
            if (!Directory.Exists(mapPath.Remove(mapPath.LastIndexOf(@"\"), mapPath.Length - mapPath.LastIndexOf(@"\"))))//创建目录
            {
                Directory.CreateDirectory(mapPath);
            }

            string filePath = mapPath + DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss.ms") + ".xls";
            wbExport.Save(filePath);//保存Excel文件
            new ExcelCommon().OutPutExcel(pContext, filePath);//输出Excel文件
            return "{success:true}";
        }
        #endregion

        #region GetStoreData
        public DataTable GetStoreData(HttpContext pContext)
        {
            VipBLLByNew b = new VipBLLByNew(CurrentUserInfo, GetTableName(pContext));
            string pSearch = pContext.Request["pSearch"];
            List<DefindControlEntity> l = new List<DefindControlEntity>();
            if (!string.IsNullOrEmpty(pSearch))
            {
                l = pSearch.DeserializeJSONTo<List<DefindControlEntity>>();

            }
            return b.GetStoreData(l);
        }
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #region GetTableName
        private string GetTableName(HttpContext pContext)
        {
            string tableName = "";
            if (!string.IsNullOrEmpty(pContext.Request.QueryString["tablename"]))
            {
                tableName = pContext.Request.QueryString["tablename"];
            }
            else
            {
                DataSet ds = new RoleService(CurrentUserInfo).GetRoleList();
                if (ds.CheckDataSet() && !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["table_name"].ToString()))
                {
                    tableName = ds.Tables[0].Rows[0]["table_name"].ToString();
                }
                else
                {
                    //考虑到很多还未配置会员角色的数据库，所以这里默认是初始状态
                    tableName = "VIP";
                    //throw new Exception("请给角色配置tablename");
                }
            }
            return tableName;
        }
        #endregion

        #region GetRoleList
        private string GetRoleList()
        {
            return new RoleService(CurrentUserInfo).GetRoleList().Tables[0].ToJSON();
        }
        #endregion

        #region GetRepeatRowCount
        private string GetRepeatRowCount(HttpContext pContext)
        {
            VipBLLByNew b = new VipBLLByNew(CurrentUserInfo, GetTableName(pContext));
            string pKeyValue = pContext.Request["pKeyValue"];
            string pSearch = pContext.Request["pSearch"];
            List<DefindControlEntity> l = new List<DefindControlEntity>();
            if (!string.IsNullOrEmpty(pSearch))
            {
                l = pSearch.DeserializeJSONTo<List<DefindControlEntity>>();

            }
            return b.GetRepeatRowCount(l, pKeyValue);

        }
        #endregion


        #region GetImportFile
        public string GetImportFile(HttpContext pContext)
        {
            DateTime dtStart = DateTime.Now;

            Aspose.Cells.License lic = new Aspose.Cells.License();
            lic.SetLicense("Aspose.Total.lic");

            var g = new GridInitEntity();

            Workbook workBook = new Workbook();
            Worksheet sheet = null;

            g.GridColumnDefinds = GetGridColumns(pContext);

            workBook.Open(HttpContext.Current.Server.MapPath("/File/Excel/DownLoad/VIPTemplate.xls"));

            sheet = workBook.Worksheets[1];
            sheet.Name = "会员";

            for (int i = 0; i < g.GridColumnDefinds.Count; i++)
            {
                sheet.Cells[0, i].PutValue(g.GridColumnDefinds[i].ColumnText);
                if (g.GridColumnDefinds[i].DataIndex.ToLower() == "storename")//冻结
                {
                    sheet.FreezePanes(1, 1, 1, 0);
                }

                Style s = sheet.Cells[0, i].GetStyle();//颜色设置
                s.Font.Color = Color.White;
                s.Font.IsBold = true;
                s.Pattern = BackgroundType.Solid;
                if (g.GridColumnDefinds[i].IsMustDo.HasValue
                    && g.GridColumnDefinds[i].IsMustDo.Value == 1)
                {
                    s.ForegroundColor = Color.Red;
                }
                else
                {
                    s.ForegroundColor = Color.Gray;
                }
                sheet.Cells[0, i].SetStyle(s);

            }
            sheet.AutoFitColumns();

            string savePath = pContext.Server.MapPath(string.Format("/File/Excel/DownLoad/temp/{0}_{1}_{2}.xls",
                CurrentUserInfo.ClientID,
                CurrentUserInfo.UserID,
                DateTime.Now.ToString("yyyyMMddHHmmssfff")));
            if (!Directory.Exists(savePath.Remove(savePath.LastIndexOf(@"\"), savePath.Length - savePath.LastIndexOf(@"\"))))//创建目录
            {
                Directory.CreateDirectory(savePath);
            }

            Loggers.Debug(new DebugLogInfo() { ClientID = this.CurrentUserInfo.ClientID, UserID = this.CurrentUserInfo.UserID, Message = "输出模板文件开始[" + DateTime.Now + "]" });

            workBook.Save(savePath);
            new ExcelCommon().OutPutExcel(pContext, savePath);

            DateTime dtFinish = DateTime.Now;
            Loggers.Debug(new DebugLogInfo() { ClientID = this.CurrentUserInfo.ClientID, UserID = this.CurrentUserInfo.UserID, Message = "输出模板文件结束[" + DateTime.Now + "].共花费时间[" + (dtFinish - dtStart).TotalSeconds.ToString() + "]s" });
            return "test";
        }
        #endregion

        #region ImportFile
        public string ImportFile(HttpContext pContext)
        {

            return "";
        }
        #endregion
    }
}