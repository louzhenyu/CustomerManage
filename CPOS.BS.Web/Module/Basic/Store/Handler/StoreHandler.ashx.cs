
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.Entity;
using JIT.TenantPlatform.Entity;
using JIT.TenantPlatform.Web;
using JIT.TenantPlatform.BLL;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Entity;
using JIT.TenantPlatform.Web.Extension;
using JIT.TenantPlatform.Web.Session;
using JIT.Utility.Reflection;
using Aspose.Cells;
using System.IO;


namespace JIT.CPOS.BS.Web.Module.Basic.Store.Handler
{
    /// <summary>
    /// StoreHandler 的摘要说明
    /// </summary>


    public class StoreHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
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
        /// <summary>
        /// 页面入口
        /// </summary>
        /// <param name="pContext"></param>
        protected override void AjaxRequest(HttpContext pContext)
        {

            string res = "";
            switch (pContext.Request["btncode"])
            {
                case "create": //添加
                       switch (pContext.Request["method"])
                        {

                           case "EditView": //得到页面控件
                                res = GetStoreEditControls(pContext).ToJSON();
                                break;
                           case "create": //添加程序逻辑
                                res = Create(pContext).ToJSON();
                               break;
                           case "CheckIsRepeat": //检查重复
                               res = GetRepeatRowCount(pContext);
                               break;
         
                        }
                    break;

                case "update": //修改
                    switch (pContext.Request["method"])
                    {

                        case "EditView": //得到修改控件
                            res = GetStoreEditControls(pContext).ToJSON();
                            break;
                        case "update": //更新逻辑
                            res = Update(pContext).ToJSON();
                            break;
                        case "EditViewData": //得到表单数据
                            res = GetStoreEditData(pContext).ToJSON();
                            break;
                        case "CheckIsRepeat": //检查重复
                            res = GetRepeatRowCount(pContext);
                            break;

                    }
                    break;
                case "search":
                    switch (pContext.Request["method"])
                    {
                       
                      case "InitGridData": //初使化终端编辑列表的数据
                            res = GetInitGridData(pContext).ToJSON();
                            break;
                        case "PageGridData": //查询数据分页查询
                            res = GetStorePageData(pContext).ToJSON();
                            break;
                        case "GridData": //获取全部数据
                            res = GetStoreData(pContext).ToJSON();
                            break;
                         case "QueryView": //得到查询控件
                            res = GetStoreQueryConditionControls(pContext).ToJSON();
                            break;
                         case "EditViewData": //得到表单数据
                            res = GetStoreEditData(pContext).ToJSON();
                            break;
                         case "EditView": //得到页面控件
                            res = GetStoreEditControls(pContext).ToJSON();
                            break;

                    }
                    break;
                case "delete":
                    switch (pContext.Request["method"])
                    {
                        case "delete":
                         res = Delete(pContext);
                         break;
                      }
                    break;
                case "export":
                    switch (pContext.Request["method"])
                    {
                        case "export":
                            export(pContext);
                            break;
                    }
                    break;
              
            }


            pContext.Response.Write(res);
            pContext.Response.End();
        }
        #region 获取定义
        /// <summary>
        /// 初使化表格数据时候用
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private GridInitEntity GetInitGridData(HttpContext pContext)
        {
            GridInitEntity g = new GridInitEntity();
            g.GridDataDefinds = GetStoreGridDataModels(pContext);
            g.GridColumnDefinds = GetStoreGridColumns(pContext);
          //  g.GridDatas = GetStorePageData(pContext);
            return g;

        }
     
       
        
        /// <summary>
        /// 获取终端查询控件
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        public List<DefindControlEntity> GetStoreQueryConditionControls(HttpContext pContext)
        {
            StoreDefindModuleBLL b = new StoreDefindModuleBLL(CurrentUserInfo, "Store");
            return b.GetQueryConditionControls();

        }

        /// <summary>
        /// 得到编辑页面控件
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private List<DefindControlEntity> GetStoreEditControls(HttpContext pContext)
        {
            StoreDefindModuleBLL b = new StoreDefindModuleBLL(CurrentUserInfo, "Store");
            return b.GetEditControls();
        }
        /// <summary>
        /// 获取终端列表数据定义模型
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        public List<GridColumnModelEntity> GetStoreGridDataModels(HttpContext pContext)
        {
            StoreDefindModuleBLL b = new StoreDefindModuleBLL(CurrentUserInfo, "Store");
            return b.GetGridDataModels();

        }
        /// <summary>
        /// 获取终端列表表头定义
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        public List<GridColumnEntity> GetStoreGridColumns(HttpContext pContext)
        {
            StoreDefindModuleBLL b = new StoreDefindModuleBLL(CurrentUserInfo, "Store");
            return b.GetGridColumns();

        }
        #endregion
        #region 查询终端数据
        #region 重复数据查询
        private string GetRepeatRowCount(HttpContext pContext) 
        {
            StoreDefindModuleBLL b = new StoreDefindModuleBLL(CurrentUserInfo, "Store");
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
         /// <summary>
        /// 返回编辑页面的值
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private List<DefindControlEntity> GetStoreEditData(HttpContext pContext)
        {
            StoreDefindModuleBLL b = new StoreDefindModuleBLL(CurrentUserInfo, "Store");
            string pKeyValue = pContext.Request["pKeyValue"];
            return b.GetStoreEditData(pKeyValue);

        }
        /// <summary>
        /// 终端管理分页查询
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        public PageResultEntity GetStorePageData(HttpContext pContext)
        {
            StoreDefindModuleBLL b = new StoreDefindModuleBLL(CurrentUserInfo, "Store");
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
            return b.GetStorePageData(l, pPageSize, pPageIndex);


        }

        /// <summary>
        /// 不分页查询全部数据
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        public DataTable GetStoreData(HttpContext pContext)
        {
            StoreDefindModuleBLL b = new StoreDefindModuleBLL(CurrentUserInfo, "Store");
            string pSearch = pContext.Request["pSearch"];
            List<DefindControlEntity> l = new List<DefindControlEntity>();
            if (!string.IsNullOrEmpty(pSearch))
            {
                l = pSearch.DeserializeJSONTo<List<DefindControlEntity>>();

            }
             return b.GetStoreData(l);
         }
        #endregion
        #region 增删改
        /// <summary>
        /// 添加终端
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private string Create(HttpContext pContext)
        {
            StoreDefindModuleBLL b = new StoreDefindModuleBLL(CurrentUserInfo, "Store");
            string pEditValue = pContext.Request["pEditValue"];
            List<DefindControlEntity> l = new List<DefindControlEntity>();
            if (!string.IsNullOrEmpty(pEditValue))
            {
                l = pEditValue.DeserializeJSONTo<List<DefindControlEntity>>();

            }
            bool res = b.Create(l);

            if (res == true)
            {
                return "[{success:true}]";
            }
            else
            {
                return "[{success:false}]";
            }


        }
        /// <summary>
        /// 修改终端
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private string Update(HttpContext pContext)
        {
            StoreDefindModuleBLL b = new StoreDefindModuleBLL(CurrentUserInfo, "Store");
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
                return "[{success:true}]";
            }
            else
            {
                return "[{success:false}]";
            }

        }
        /// <summary>
        /// 删除终端
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private string Delete(HttpContext pContext)
        {
            StoreDefindModuleBLL b = new StoreDefindModuleBLL(CurrentUserInfo, "Store");
            string pKeyValue = pContext.Request["pKeyValue"];
            string checkRes = "";
            bool res = b.Delete(pKeyValue, out checkRes);
             if (res == true)
            {
                return "{success:true}";
            }
            else
            {
                return "{success:false,msg:\"" + checkRes + "\"}";
            }
        }
        #endregion


        #region 导出Excel文件 export  tiansheng.zhu
        /// <summary>
        /// 导出Excel数据功能 
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private string export(HttpContext pContext)
        {
            //获取所有的符合条件的数据
            DataTable dtTest = GetStoreData(pContext);
            GridInitEntity g = GetInitGridData(pContext);

            #region 替换标题信息
            if (g != null && g.GridColumnDefinds != null)
            {
                for (int i = 0; i < dtTest.Columns.Count; i++)
                {
                    if (dtTest.Columns[i].ColumnName == "StoreID")
                    {
                        dtTest.Columns[i].ColumnName = "终端标识ID";
                        continue;
                    }
                    for (int j = 0; j < g.GridColumnDefinds.Count; j++)
                    {
                        if (dtTest.Columns[i].ColumnName == g.GridColumnDefinds[j].DataIndex)
                        {
                            dtTest.Columns[i].ColumnName = g.GridColumnDefinds[j].ColumnText;
                            break;
                        }
                    }
                }
            }
            #endregion
            Workbook wbTest = JIT.Utility.DataTableExporter.WriteXLS(dtTest, 0);
            string MapUrl = pContext.Server.MapPath(@"~/File/Excel/" + DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss.ms") + ".xls");
            wbTest.Save(MapUrl);//保存Excel文件
            OutPutExcel(pContext, MapUrl);//输出Excel文件
            return "{success:true}";
        }
        #region OutPutExcel tiansheng.zhu
        protected void OutPutExcel(HttpContext p, string filePath)
        {
            p.Response.Clear();
            p.Response.Buffer = true;
            p.Response.Charset = "GB2312";
            p.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(Path.GetFileName(filePath)));
            p.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");//设置输出流为简体中文
            p.Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。          
            p.Response.WriteFile(filePath);
            p.ApplicationInstance.CompleteRequest();
        }
        #endregion
        #endregion
    }
}