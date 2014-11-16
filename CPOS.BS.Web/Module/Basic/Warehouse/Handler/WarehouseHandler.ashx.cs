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
using JIT.CPOS.BS.Entity.User;
using JIT.CPOS.BS.Entity.Pos;
using System.Collections;

namespace JIT.CPOS.BS.Web.Module.Basic.Warehouse.Handler
{
    /// <summary>
    /// WarehouseHandler
    /// </summary>
    public class WarehouseHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
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
                case "search_warehouse":
                    content = GetWarehouseListData();
                    break;
                case "get_warehouse_by_id":
                    content = GetWarehouseInfoById();
                    break;
                case "warehouse_save":
                    content = SaveWarehouseData();
                    break;
                case "warehouse_delete":
                    content = DeleteData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }


        #region GetWarehouseListData
        /// <summary>
        /// 查询仓库
        /// </summary>
        public string GetWarehouseListData()
        {
            var form = Request("form").DeserializeJSONTo<WarehouseQueryEntity>();

            var posService = new PosService(CurrentUserInfo);
            IList<WarehouseInfo> warehouseList;
            string content = string.Empty;

            Hashtable hashtable = new Hashtable();
            hashtable.Add("Code", FormatParamValue(form.warehouse_code));
            hashtable.Add("Name", FormatParamValue(form.warehouse_name));
            hashtable.Add("Contacter", FormatParamValue(form.warehouse_contacter));
            hashtable.Add("Tel", FormatParamValue(form.warehouse_tel));
            hashtable.Add("Status", FormatParamValue(form.warehouse_status));
            hashtable.Add("UnitName", FormatParamValue(form.unit_name));

            int maxRowCount = PageSize;
            int startRowIndex = Utils.GetIntVal(Request("start"));

            warehouseList = posService.SelectWarehouseList(hashtable, maxRowCount, startRowIndex);

            var jsonData = new JsonData();
            jsonData.totalCount = posService.SelectWarehouseListCount(hashtable).ToString();
            jsonData.data = warehouseList;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                jsonData.data.ToJSON(),
                jsonData.totalCount);
            return content;
        }
        #endregion

        #region GetWarehouseInfoById
        /// <summary>
        /// 根据仓库ID获取仓库信息
        /// </summary>
        public string GetWarehouseInfoById()
        {
            var posService = new PosService(CurrentUserInfo);
            WarehouseInfo warehouse = new WarehouseInfo();
            string content = string.Empty;

            string key = string.Empty;
            if (Request("warehouse_id") != null && Request("warehouse_id") != string.Empty)
            {
                key = Request("warehouse_id").ToString().Trim();
            }

            warehouse = posService.GetWarehouseByID(CurrentUserInfo, key);

            var jsonData = new JsonData();
            jsonData.totalCount = warehouse == null ? "0" : "1";
            jsonData.data = warehouse;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region SaveWarehouseData
        /// <summary>
        /// 保存仓库
        /// </summary>
        public string SaveWarehouseData()
        {
            var posService = new PosService(CurrentUserInfo);
            WarehouseInfo warehouse = new WarehouseInfo();
            string content = string.Empty;
            var responseData = new ResponseData();

            string key = string.Empty;
            string warehouse_id = string.Empty;
            if (Request("warehouse") != null && Request("warehouse") != string.Empty)
            {
                key = Request("warehouse").ToString().Trim();
            }
            if (Request("warehouse_id") != null && Request("warehouse_id") != string.Empty)
            {
                warehouse_id = Request("warehouse_id").ToString().Trim();
            }

            warehouse = key.DeserializeJSONTo<WarehouseInfo>();

            if (warehouse.wh_code == null || warehouse.wh_code.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "仓库编码不能为空";
                return responseData.ToJSON();
            }
            if (warehouse.wh_name == null || warehouse.wh_name.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "仓库名称不能为空";
                return responseData.ToJSON();
            }
            if (warehouse.unit_id == null || warehouse.unit_id.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "所属单位不能为空";
                return responseData.ToJSON();
            }
            if (warehouse.wh_tel == null || warehouse.wh_tel.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "电话不能为空";
                return responseData.ToJSON();
            }

            warehouse.CreateUserID = CurrentUserInfo.CurrentUser.User_Id;
            warehouse.CreateUserName = CurrentUserInfo.CurrentUser.User_Name;
            warehouse.Unit.Id = warehouse.unit_id;

            bool status = true;
            string message = "保存成功";
            try
            {
                if (warehouse_id.Trim().Length == 0)
                {
                    posService.InsertWarehouse(warehouse);  //新增
                }
                else
                {
                    warehouse.warehouse_id = warehouse_id;
                    posService.ModifyWarehouse(warehouse);  //修改
                }
            }
            catch (Exception ex)
            {
                status = false;
                message = ex.Message;
            }

            responseData.success = status;
            responseData.msg = message;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region DeleteData
        /// <summary>
        /// 删除
        /// </summary>
        public string DeleteData()
        {
            var service = new WarehouseService(CurrentUserInfo);

            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            try
            {
                string key = string.Empty;
                if (FormatParamValue(Request("ids")) != null && FormatParamValue(Request("ids")) != string.Empty)
                {
                    key = FormatParamValue(Request("ids")).ToString().Trim();
                }

                if (key == null || key.Trim().Length == 0)
                {
                    responseData.success = false;
                    responseData.msg = "ID不能为空";
                    return responseData.ToJSON();
                }

                var status = "-1";
                if (FormatParamValue(Request("status")) != null && FormatParamValue(Request("status")) != string.Empty)
                {
                    status = FormatParamValue(Request("status")).ToString().Trim();
                }

                string[] ids = key.Split(',');
                foreach (var id in ids)
                {
                    //service.SetUnitStatus(key, status);
                }

                responseData.success = true;
                responseData.msg = error;
            }
            catch (Exception ex)
            {
                responseData.success = false;
                responseData.msg = ex.Message;
            }

            content = responseData.ToJSON();
            return content;
        }
        #endregion
    }

    #region QueryEntity
    public class WarehouseQueryEntity
    {
        public string unit_name;
        public string warehouse_code;
        public string warehouse_name;
        public string warehouse_contacter;
        public string warehouse_tel;
        public string warehouse_status;
    }
    #endregion

}