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

namespace JIT.CPOS.BS.Web.Module.Basic.WQRCodeType.Handler
{
    /// <summary>
    /// WQRCodeTypeHandler
    /// </summary>
    public class WQRCodeTypeHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
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
                case "search_wQRCodeType":
                    content = QueryWQRCodeTypeListData();
                    break;
                case "get_wQRCodeType_by_id":
                    content = GetWQRCodeTypeInfoByIdData();
                    break;
                case "wQRCodeType_save":
                    content = SaveWQRCodeTypeData();
                    break;
                case "wQRCodeType_delete":
                    content = DeleteData();
                    break;

            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region QueryWQRCodeTypeListData
        /// <summary>
        /// 查询角色列表
        /// </summary>
        public string QueryWQRCodeTypeListData()
        {
            var form = Request("form").DeserializeJSONTo<WQRCodeTypeQueryEntity>();

            var appSysService = new WQRCodeTypeBLL(CurrentUserInfo);
            IList<WQRCodeTypeEntity> list = new List<WQRCodeTypeEntity>();

            string content = string.Empty;
            string key = string.Empty;

            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;
            WQRCodeTypeEntity queryEntity = new WQRCodeTypeEntity();
            queryEntity.TypeName = form.wQRCodeType_name;
            queryEntity.TypeCode = form.wQRCodeType_code;
            list = appSysService.GetList(queryEntity, pageIndex, PageSize);

            var jsonData = new JsonData();
            jsonData.totalCount = appSysService.GetListCount(queryEntity).ToString();
            jsonData.data = list;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                list.ToJSON(),
                jsonData.totalCount);
            return content;
        }
        #endregion


        #region GetWQRCodeTypeInfoByIdData
        /// <summary>
        /// 通过ID获取角色信息
        /// </summary>
        public string GetWQRCodeTypeInfoByIdData()
        {
            var service = new WQRCodeTypeBLL(CurrentUserInfo);
            WQRCodeTypeEntity data = null;
            string content = string.Empty;

            string key = string.Empty;
            if (Request("wQRCodeType_id") != null && Request("wQRCodeType_id") != string.Empty)
            {
                key = Request("wQRCodeType_id").ToString().Trim();
            }

            WQRCodeTypeEntity queryEntity = new WQRCodeTypeEntity();
            queryEntity.QRCodeTypeId = Guid.Parse(key);
            var list = service.GetList(queryEntity, 0, 1);
            if (list != null && list.Count > 0)
            {
                data = list[0];
            }

            data.WModelIds = new List<string>();
            var wQRCodeTypeModelMappingBLL = new WQRCodeTypeModelMappingBLL(CurrentUserInfo);
            var wModelBLL = new WModelBLL(CurrentUserInfo);
            var mappingList = wQRCodeTypeModelMappingBLL.QueryByEntity(new WQRCodeTypeModelMappingEntity()
            {
                QRCodeTypeId = data.QRCodeTypeId
            }, null);
            if (mappingList != null && mappingList.Length > 0)
            {
                foreach (var mappingItem in mappingList)
                {
                    data.ApplicationId = wModelBLL.GetByID(mappingItem.ModelId).ApplicationId;
                    data.WModelIds.Add(mappingItem.ModelId);
                }
            }

            var jsonData = new JsonData();
            jsonData.totalCount = "1";
            jsonData.data = data;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region SaveWQRCodeTypeData
        /// <summary>
        /// 保存角色
        /// </summary>
        public string SaveWQRCodeTypeData()
        {
            var wQRCodeTypeService = new WQRCodeTypeBLL(CurrentUserInfo);
            WQRCodeTypeEntity obj = new WQRCodeTypeEntity();
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string wQRCodeType_id = string.Empty;
            if (Request("wQRCodeType") != null && Request("wQRCodeType") != string.Empty)
            {
                key = Request("wQRCodeType").ToString().Trim();
            }
            if (Request("wQRCodeType_id") != null && Request("wQRCodeType_id") != string.Empty)
            {
                wQRCodeType_id = Request("wQRCodeType_id").ToString().Trim();
            }

            obj = key.DeserializeJSONTo<WQRCodeTypeEntity>();

            //if (obj.TypeCode == null || obj.TypeCode.Trim().Length == 0)
            //{
            //    responseData.success = false;
            //    responseData.msg = "编码不能为空";
            //    return responseData.ToJSON();
            //}
            //if (obj.TypeName == null || obj.TypeName.Trim().Length == 0)
            //{
            //    responseData.success = false;
            //    responseData.msg = "名称不能为空";
            //    return responseData.ToJSON();
            //}

            if (wQRCodeType_id.Trim().Length == 0 || wQRCodeType_id == "null" || wQRCodeType_id == "undefined")
            {
                obj.QRCodeTypeId = Guid.NewGuid();
                wQRCodeTypeService.Create(obj);
            }
            else
            {
                obj.QRCodeTypeId = Guid.Parse(wQRCodeType_id);
                wQRCodeTypeService.Update(obj, false);
            }

            // WModelId
            var wQRCodeTypeModelMappingBLL = new WQRCodeTypeModelMappingBLL(CurrentUserInfo);
            var mappingList = wQRCodeTypeModelMappingBLL.QueryByEntity(new WQRCodeTypeModelMappingEntity()
            {
                QRCodeTypeId = obj.QRCodeTypeId
            }, null);
            if (mappingList != null && mappingList.Length > 0)
            {
                foreach (var mappingItem in mappingList)
                {
                    wQRCodeTypeModelMappingBLL.Delete(mappingItem);
                }
            }
            if (obj.WModelIds != null && obj.WModelIds.Count > 0)
            {
                foreach (var wModelIdItem in obj.WModelIds)
                {
                    if (wModelIdItem.Trim().Length == 0) continue;
                    wQRCodeTypeModelMappingBLL.Create(new WQRCodeTypeModelMappingEntity()
                    {
                        Mapping = Guid.NewGuid(),
                        ModelId = wModelIdItem,
                        QRCodeTypeId = obj.QRCodeTypeId
                    });
                }
            }

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region 删除
        public string DeleteData()
        {
            var service = new WQRCodeTypeBLL(CurrentUserInfo);
            string content = string.Empty;
            string error = "删除成功";
            var responseData = new ResponseData();
            string key = string.Empty;
            try
            {
                if (Request("ids") != null && Request("ids") != string.Empty)
                {
                    key = Request("ids").ToString().Trim();
                }
                if (key == null || key.Length == 0)
                {
                    responseData.success = false;
                    responseData.msg = "请选择分类";
                    return responseData.ToJSON(); ;
                }

                var status = "-1";
                if (FormatParamValue(Request("status")) != null && FormatParamValue(Request("status")) != string.Empty)
                {
                    status = FormatParamValue(Request("status")).ToString().Trim();
                }

                var idList = key.Split(',');
                foreach (var tmpId in idList)
                {
                    if (tmpId.Trim().Length > 0)
                    {
                        service.Delete(new WQRCodeTypeEntity() {
                            QRCodeTypeId = Guid.Parse(tmpId.Trim())
                        });
                    }
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
    public class WQRCodeTypeQueryEntity
    {
        public string wQRCodeType_name;
        public string wQRCodeType_code;
    }
    #endregion

}