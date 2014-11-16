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
using System.Configuration;
using System.Threading;

namespace JIT.CPOS.BS.Web.Module.Basic.WQRCodeManager.Handler
{
    /// <summary>
    /// WQRCodeManagerHandler
    /// </summary>
    public class WQRCodeManagerHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
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
                case "search_wQRCodeManager":
                    content = QueryWQRCodeManagerListData();
                    break;
                case "get_wQRCodeManager_by_id":
                    content = GetWQRCodeManagerInfoByIdData();
                    break;
                case "wQRCodeManager_save":
                    content = SaveWQRCodeManagerData();
                    break;
                case "wQRCodeManager_delete":
                    content = DeleteData();
                    break;
                case "SetUnitWXCode":
                    content = SetUnitWXCode();
                    break;

            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region QueryWQRCodeManagerListData
        /// <summary>
        /// 查询角色列表
        /// </summary>
        public string QueryWQRCodeManagerListData()
        {
            var form = Request("form").DeserializeJSONTo<WQRCodeManagerQueryEntity>();

            var appSysService = new WQRCodeManagerBLL(CurrentUserInfo);
            IList<WQRCodeManagerEntity> list = new List<WQRCodeManagerEntity>();

            string content = string.Empty;
            string key = string.Empty;

            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;
            WQRCodeManagerEntity queryEntity = new WQRCodeManagerEntity();
            queryEntity.QRCode = form.QRCode;
            if (form.QRCodeTypeId != null && form.QRCodeTypeId.Length > 0)
                queryEntity.QRCodeTypeId = Guid.Parse(form.QRCodeTypeId);
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

        #region GetWQRCodeManagerInfoByIdData
        /// <summary>
        /// 通过ID获取角色信息
        /// </summary>
        public string GetWQRCodeManagerInfoByIdData()
        {
            var service = new WQRCodeManagerBLL(CurrentUserInfo);
            WQRCodeManagerEntity data = null;
            string content = string.Empty;

            string key = string.Empty;
            if (Request("wQRCodeManager_id") != null && Request("wQRCodeManager_id") != string.Empty)
            {
                key = Request("wQRCodeManager_id").ToString().Trim();
            }

            WQRCodeManagerEntity queryEntity = new WQRCodeManagerEntity();
            queryEntity.QRCodeId = Guid.Parse(key);
            var list = service.GetList(queryEntity, 0, 1);
            if (list != null && list.Count > 0)
            {
                data = list[0];
            }

            var jsonData = new JsonData();
            jsonData.totalCount = "1";
            jsonData.data = data;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region SaveWQRCodeManagerData
        /// <summary>
        /// 保存角色
        /// </summary>
        public string SaveWQRCodeManagerData()
        {
            var wQRCodeManagerService = new WQRCodeManagerBLL(CurrentUserInfo);
            WQRCodeManagerEntity obj = new WQRCodeManagerEntity();
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string wQRCodeManager_id = string.Empty;
            if (Request("wQRCodeManager") != null && Request("wQRCodeManager") != string.Empty)
            {
                key = Request("wQRCodeManager").ToString().Trim();
            }
            if (Request("wQRCodeManager_id") != null && Request("wQRCodeManager_id") != string.Empty)
            {
                wQRCodeManager_id = Request("wQRCodeManager_id").ToString().Trim();
            }

            obj = key.DeserializeJSONTo<WQRCodeManagerEntity>();

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

            if (wQRCodeManager_id.Trim().Length == 0 || wQRCodeManager_id == "null" || wQRCodeManager_id == "undefined")
            {
                obj.QRCodeId = Guid.NewGuid();
                obj.CustomerId = CurrentUserInfo.CurrentUser.customer_id;
                wQRCodeManagerService.Create(obj);
            }
            else
            {
                obj.QRCodeId = Guid.Parse(wQRCodeManager_id);
                wQRCodeManagerService.Update(obj, false);
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
            var service = new WQRCodeManagerBLL(CurrentUserInfo);
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
                        service.Delete(new WQRCodeManagerEntity() {
                            QRCodeId = Guid.Parse(tmpId.Trim())
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

        #region 生成门店二维码
        /// <summary>
        /// 获取门店二维码
        /// </summary>
        /// <returns></returns>
        public string SetUnitWXCode()
        {
            #region 参数处理
            string WeiXinId = Request("WeiXinId");
            string UnitId = Request("UnitId");
            string WXCode = Request("WXCode");
            var responseData = new ResponseData();
            if (WeiXinId == null || WeiXinId.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "公众号不能为空";
                return responseData.ToJSON();
            }

            //if (UnitId == null || UnitId.Trim().Length == 0)
            //{
            //    responseData.success = false;
            //    responseData.msg = "门店标识不能为空";
            //    return responseData.ToJSON();
            //}
            //if (WXCode == null || WXCode.Equals(""))
            //{
            //    VwUnitPropertyBLL unitServer = new VwUnitPropertyBLL(CurrentUserInfo);
            //    WXCode = unitServer.GetUnitWXCode(UnitId).ToString();
            //}
            #endregion

            #region 获取微信公众号信息
            WApplicationInterfaceBLL server = new WApplicationInterfaceBLL(CurrentUserInfo);
            var wxObj = server.QueryByEntity(new WApplicationInterfaceEntity
            {
                CustomerId = CurrentUserInfo.CurrentUser.customer_id
                ,
                IsDelete = 0
                ,
                ApplicationId = WeiXinId
            }, null);
            if (wxObj == null || wxObj.Length == 0)
            {
                responseData.success = false;
                responseData.msg = "不存在对应的微信帐号";
                return responseData.ToJSON().ToString();
            }
            else
            {
                JIT.CPOS.BS.BLL.WX.CommonBLL commonServer = new JIT.CPOS.BS.BLL.WX.CommonBLL();
                string imageUrl = commonServer.GetQrcodeUrl(wxObj[0].AppID.ToString().Trim()
                                                        , wxObj[0].AppSecret.Trim()
                                                        , "1"
                                                        , Convert.ToInt32(WXCode), CurrentUserInfo);
                if (imageUrl != null && !imageUrl.Equals(""))
                {
                    string host = ConfigurationManager.AppSettings["DownloadImageUrl"];

                    try
                    {
                        CPOS.Common.DownloadImage downloadServer = new DownloadImage();
                        imageUrl = downloadServer.DownloadFile(imageUrl, host);
                        responseData.success = true;
                        responseData.msg = imageUrl;
                        responseData.status = WXCode;
                    }
                    catch (Exception ex)
                    {
                        responseData.success = false;
                        responseData.data = imageUrl;
                        responseData.msg = ex.ToString();
                        return responseData.ToJSON().ToString();
                    }
                    return responseData.ToJSON().ToString();
                }
                else
                {
                    responseData.success = false;
                    responseData.msg = "图片不存在";
                    return responseData.ToJSON().ToString();
                }
            }
            #endregion

        }
        #endregion

    }

    #region QueryEntity
    public class WQRCodeManagerQueryEntity
    {
        public string QRCode;
        public string QRCodeTypeId;
    }
    #endregion

}