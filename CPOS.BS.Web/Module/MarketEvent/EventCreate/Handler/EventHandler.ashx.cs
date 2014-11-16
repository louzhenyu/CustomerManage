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

namespace JIT.CPOS.BS.Web.Module.MarketEvent.Handler
{
    /// <summary>
    /// EventHandler
    /// </summary>
    public class EventHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
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
                case "search_role":
                    content = QueryRoleListData();
                    break;
                case "get_sys_menus_by_role_id":
                    content = GetSysMenuListByRoleIdData();
                    break;
                case "get_event_by_id":
                    content = GetEventInfoByIdData();
                    break;
                case "event_save":
                    content = SaveEventData();
                    break;
                case "event_time_save":
                    content = SaveEventTimeData();
                    break;
                case "get_waveband_list":
                    content = GetWaveBandListData();
                    break;
                case "get_store_list":
                    content = GetStoreListData();
                    break;
                case "get_default_store_list":
                    content = GetDefaultStoreListData();
                    break;
                case "event_store_save":
                    content = SaveEventStoreData();
                    break;
                case "get_template_list":
                    content = GetTemplateListData();
                    break;
                case "get_template_by_id":
                    content = GetTemplateInfoByIdData();
                    break;
                case "event_person_save":
                    content = SaveEventPersonData();
                    break;
                case "get_vip_list":
                    content = GetVipListData();
                    break;
                case "get_person_list":
                    content = GetPersonListData();
                    break;
                case "event_template_save":
                    content = SaveEventTemplateData();
                    break;
                case "event_send":
                    content = SaveEventSendData();
                    break;
                case "get_unit_property":
                    content = GetUnitPropertyData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region QueryRoleListData
        /// <summary>
        /// 查询角色列表
        /// </summary>
        public string QueryRoleListData()
        {
            var form = Request("form").DeserializeJSONTo<RoleQueryEntity>();

            var appSysService = new AppSysService(CurrentUserInfo);
            RoleModel list = new RoleModel();

            string content = string.Empty;
            string key = string.Empty;
            if (form.app_sys_id != null && form.app_sys_id != string.Empty)
            {
                key = form.app_sys_id.Trim();
            }
            list = appSysService.GetRolesByAppSysId(key, 1000, 0);

            var jsonData = new JsonData();
            jsonData.totalCount = list.RoleInfoList.Count.ToString();
            jsonData.data = list.RoleInfoList;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                list.RoleInfoList.ToJSON(),
                list.RoleInfoList.Count);
            return content;
        }
        #endregion

        #region GetSysMenuListByRoleIdData
        /// <summary>
        /// 通过RoleID获取菜单列表
        /// </summary>
        public string GetSysMenuListByRoleIdData()
        {
            var appSysService = new AppSysService(CurrentUserInfo);
            IList<MenuModel> data = new List<MenuModel>();
            string content = string.Empty;

            IList<MenuModel> src = new List<MenuModel>();

            string appSysId = "";
            string key = string.Empty;
            if (Request("role_id") != null && Request("role_id") != string.Empty)
            {
                key = Request("role_id").ToString().Trim();
            }

            if (Request("app_sys_id") != null && Request("app_sys_id") != string.Empty)
            {
                appSysId = Request("app_sys_id").ToString().Trim();
            }

            src = appSysService.GetAllMenusByAppSysId(appSysId);

            IList<MenuModel> roleMenuList = new List<MenuModel>();
            roleMenuList = appSysService.GetRoleMenus(CurrentUserInfo, key);

            foreach (var tmpSrcMenuObj in src)
            {
                foreach (var tmpRoleMenuObj in roleMenuList)
                {
                    if (tmpRoleMenuObj.Menu_Id == tmpSrcMenuObj.Menu_Id)
                    {
                        tmpSrcMenuObj.check_flag = "true";
                        break;
                    }
                    else
                    {
                        tmpSrcMenuObj.check_flag = "false";
                    }
                }
            }

            data = src.Where(c => c.Menu_Level == 1).ToList();
            foreach (var tmpMenuObj in data)
            {
                tmpMenuObj.leaf_flag = tmpMenuObj.Menu_Level == 1 ? "false" : "true";
                tmpMenuObj.expanded_flag = tmpMenuObj.Menu_Level == 1 ? "true" : "false";
                //tmpMenuObj.cls_flag = tmpMenuObj.Menu_Level == 1 ? "folder" : "";

                foreach (var tmpSrcMenuObj in src)
                {
                    if (tmpSrcMenuObj.Parent_Menu_Id == tmpMenuObj.Menu_Id)
                    {
                        if (tmpMenuObj.children == null)
                            tmpMenuObj.children = new List<MenuModel>();
                        tmpSrcMenuObj.leaf_flag = "true";
                        tmpSrcMenuObj.cls_flag = "";
                        tmpMenuObj.children.Add(tmpSrcMenuObj);
                    }
                }
            }

            var jsonData = new JsonData();
            jsonData.totalCount = data.Count.ToString();
            jsonData.data = data;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region GetEventInfoByIdData
        /// <summary>
        /// 通过ID获取Event信息
        /// </summary>
        public string GetEventInfoByIdData()
        {
            var service = new MarketEventBLL(CurrentUserInfo);
            var marketTemplateBLL = new MarketTemplateBLL(CurrentUserInfo);
            MarketEventEntity data;
            string content = string.Empty;

            string key = string.Empty;
            if (Request("MarketEventID") != null && Request("MarketEventID") != string.Empty)
            {
                key = Request("MarketEventID").ToString().Trim();
            }

            data = service.GetByID(key);

            //var templateObj = marketTemplateBLL.GetByID(data.TemplateID);
            //data.TemplateContent = templateObj != null ? templateObj.TemplateContent : string.Empty;

            var jsonData = new JsonData();
            jsonData.totalCount = "1";
            jsonData.data = data;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region SaveEventData
        /// <summary>
        /// 保存Event
        /// </summary>
        public string SaveEventData()
        {
            var service = new MarketEventBLL(CurrentUserInfo);
            var obj = new MarketEventEntity();
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string MarketEventID = string.Empty;
            if (Request("data") != null && Request("data") != string.Empty)
            {
                key = Request("data").ToString().Trim();
            }
            if (Request("MarketEventID") != null && Request("MarketEventID") != string.Empty)
            {
                MarketEventID = Request("MarketEventID").ToString().Trim();
            }

            obj = key.DeserializeJSONTo<MarketEventEntity>();

            //if (obj.BrandID == null || obj.BrandID.Trim().Length == 0)
            //{
            //    responseData.success = false;
            //    responseData.msg = "品牌不能为空";
            //    return responseData.ToJSON();
            //}
            //if (obj.EventType == null || obj.EventType.Trim().Length == 0)
            //{
            //    responseData.success = false;
            //    responseData.msg = "活动类型不能为空";
            //    return responseData.ToJSON();
            //}
            //if (obj.EventMode == null || obj.EventMode.Trim().Length == 0)
            //{
            //    responseData.success = false;
            //    responseData.msg = "活动方式不能为空";
            //    return responseData.ToJSON();
            //}

            if (MarketEventID.Trim().Length == 0 || MarketEventID == "null" || MarketEventID == "undefined")
            {
                obj.MarketEventID = Utils.NewGuid();
                obj.CustomerId = CurrentUserInfo.CurrentUser.customer_id;
                obj.EventStatus = 1;
                obj.StoreCount = 0;
                obj.PersonCount = 0;
                service.Create(obj);
            }
            else
            {
                obj.MarketEventID = MarketEventID;
                service.Update(obj, false);
            }

            responseData.success = true;
            responseData.msg = error;
            responseData.data = obj.MarketEventID;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region GetWaveBandListData
        /// <summary>
        /// 获取波段列表
        /// </summary>
        public string GetWaveBandListData()
        {
            var service = new MarketWaveBandBLL(CurrentUserInfo);
            string content = string.Empty;

            string key = string.Empty;
            if (Request("MarketEventID") != null && Request("MarketEventID") != string.Empty)
            {
                key = Request("MarketEventID").ToString().Trim();
            }

            var queryEntity = new MarketWaveBandEntity();
            //queryEntity.MarketEventID = key;
            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;

            var data = service.GetList(queryEntity, pageIndex, PageSize);
            var dataTotalCount = service.GetListCount(queryEntity);

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                dataTotalCount);
            return content;
        }
        #endregion

        #region SaveEventTimeData
        /// <summary>
        /// 保存EventTime
        /// </summary>
        public string SaveEventTimeData()
        {
            var service = new MarketEventBLL(CurrentUserInfo);
            var marketWaveBandBLL = new MarketWaveBandBLL(CurrentUserInfo);
            var obj = new MarketEventEntity();
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string MarketEventID = string.Empty;
            if (Request("data") != null && Request("data") != string.Empty)
            {
                key = Request("data").ToString().Trim();
            }
            if (Request("MarketEventID") != null && Request("MarketEventID") != string.Empty)
            {
                MarketEventID = Request("MarketEventID").ToString().Trim();
            }

            obj = key.DeserializeJSONTo<MarketEventEntity>();

            if (obj.MarketEventID == null || obj.MarketEventID.Trim().Length == 0 ||
                obj.MarketEventID == "null" || obj.MarketEventID == "undefined")
            {
                responseData.success = false;
                responseData.msg = "活动标示不能为空";
                return responseData.ToJSON();
            }

            service.Update(obj, false);

            //if (obj.MarketWaveBandList != null)
            //{
            //    foreach (var item in obj.MarketWaveBandList)
            //    {
            //        item.BeginTime = Convert.ToDateTime(item.BeginTime).ToString("yyyy-MM-dd");
            //        item.EndTime = Convert.ToDateTime(item.EndTime).ToString("yyyy-MM-dd");

            //        var exsitObj = marketWaveBandBLL.GetByID(item.WaveBandID);
            //        if (exsitObj != null && exsitObj.WaveBandID.ToString().Length > 0)
            //        {
            //            marketWaveBandBLL.Update(item, false);
            //        }
            //        else
            //        {
            //            marketWaveBandBLL.Create(item);
            //        }
            //    }
            //}

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region GetStoreListData
        /// <summary>
        /// 获取门店列表
        /// </summary>
        public string GetStoreListData()
        {
            var service = new MarketStoreBLL(CurrentUserInfo);
            string content = string.Empty;

            string key = string.Empty;
            if (Request("MarketEventID") != null && Request("MarketEventID") != string.Empty)
            {
                key = Request("MarketEventID").ToString().Trim();
            }
            else
            {
                key = "-99";
            }

            var queryEntity = new MarketStoreEntity();
            queryEntity.MarketEventID = key;
            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;

            var data = service.GetList(queryEntity, pageIndex, PageSize);
            var dataTotalCount = service.GetListCount(queryEntity);

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                dataTotalCount);
            return content;
        }
        #endregion

        #region GetDefaultStoreListData
        /// <summary>
        /// 获取门店列表
        /// </summary>
        public string GetDefaultStoreListData()
        {
            var service = new StoreBLL(CurrentUserInfo);
            string content = string.Empty;

            string key = string.Empty;
            if (Request("StoreID") != null && Request("StoreID") != string.Empty)
            {
                key = Request("StoreID").ToString().Trim();
            }

            var queryEntity = new StoreEntity();
            queryEntity.StoreID = key;
            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;

            var data = service.GetList(queryEntity, pageIndex, 1000);
            var dataTotalCount = service.GetListCount(queryEntity);

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                dataTotalCount);
            return content;
        }
        #endregion

        #region SaveEventStoreData
        /// <summary>
        /// 保存EventStore
        /// </summary>
        public string SaveEventStoreData()
        {
            var service = new MarketEventBLL(CurrentUserInfo);
            var marketWaveBandBLL = new MarketWaveBandBLL(CurrentUserInfo);
            var marketStoreBLL = new MarketStoreBLL(CurrentUserInfo);
            var obj = new MarketEventEntity();
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string MarketEventID = string.Empty;
            if (Request("data") != null && Request("data") != string.Empty)
            {
                key = Request("data").ToString().Trim();
            }
            if (Request("MarketEventID") != null && Request("MarketEventID") != string.Empty)
            {
                MarketEventID = Request("MarketEventID").ToString().Trim();
            }

            obj = key.DeserializeJSONTo<MarketEventEntity>();

            if (obj.MarketEventID == null || obj.MarketEventID.Trim().Length == 0 ||
                obj.MarketEventID == "null" || obj.MarketEventID == "undefined")
            {
                responseData.success = false;
                responseData.msg = "活动标示不能为空";
                return responseData.ToJSON();
            }

            marketStoreBLL.WebDelete(new MarketStoreEntity()
            {
                MarketEventID = obj.MarketEventID
            });
            if (obj.MarketStoreInfoList != null)
            {
                foreach (var item in obj.MarketStoreInfoList)
                {
                    item.MarketStoreID = Utils.NewGuid();
                    marketStoreBLL.Create(item);
                }
            }

            obj.StoreCount = obj.MarketStoreInfoList.Count;
            service.Update(obj, false);

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region GetTemplateListData
        /// <summary>
        /// 获取模板列表
        /// </summary>
        public string GetTemplateListData()
        {
            var service = new MarketTemplateBLL(CurrentUserInfo);
            string content = string.Empty;

            string key = string.Empty;
            if (Request("type") != null && Request("type") != string.Empty)
            {
                key = Request("type").ToString().Trim();
            }

            var queryEntity = new MarketTemplateEntity();
            queryEntity.TemplateType = key;

            var data = service.QueryByEntity(queryEntity, null);

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                data.Length);
            return content;
        }
        #endregion

        #region GetTemplateInfoByIdData
        /// <summary>
        /// 通过ID获取Template信息
        /// </summary>
        public string GetTemplateInfoByIdData()
        {
            var service = new MarketTemplateBLL(CurrentUserInfo);
            MarketTemplateEntity data;
            string content = string.Empty;

            string key = string.Empty;
            if (Request("TemplateID") != null && Request("TemplateID") != string.Empty)
            {
                key = Request("TemplateID").ToString().Trim();
            }

            data = service.GetByID(key);

            var jsonData = new JsonData();
            jsonData.totalCount = "1";
            jsonData.data = data;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region SaveEventPersonData
        /// <summary>
        /// 保存EventPerson
        /// </summary>
        public string SaveEventPersonData()
        {
            var service = new MarketEventBLL(CurrentUserInfo);
            var marketWaveBandBLL = new MarketWaveBandBLL(CurrentUserInfo);
            var marketPersonBLL = new MarketPersonBLL(CurrentUserInfo);
            var obj = new MarketPersonEntity();
            var eventObj = new MarketEventEntity();
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string MarketEventID = string.Empty;
            if (Request("data") != null && Request("data") != string.Empty)
            {
                key = Request("data").ToString().Trim();
            }
            if (Request("MarketEventID") != null && Request("MarketEventID") != string.Empty)
            {
                MarketEventID = Request("MarketEventID").ToString().Trim();
            }

            obj = key.DeserializeJSONTo<MarketPersonEntity>();

            if (obj.MarketEventID == null || obj.MarketEventID.Trim().Length == 0 ||
                obj.MarketEventID == "null" || obj.MarketEventID == "undefined")
            {
                responseData.success = false;
                responseData.msg = "活动标示不能为空";
                return responseData.ToJSON();
            }

            marketPersonBLL.WebDelete(new MarketPersonEntity()
            {
                MarketEventID = obj.MarketEventID
            });
            if (obj.MarketPersonInfoList != null)
            {
                foreach (var item in obj.MarketPersonInfoList)
                {
                    //item.MarketPersonID = Utils.NewGuid();
                    item.MarketEventID = MarketEventID;
                    marketPersonBLL.Create(item);
                }
            }

            eventObj.MarketEventID = MarketEventID;
            eventObj.PersonCount = obj.MarketPersonInfoList.Count;
            service.Update(eventObj, false);

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region GetVipListData
        /// <summary>
        /// 获取VIP列表
        /// </summary>
        public string GetVipListData()
        {
            var service = new VipBLL(CurrentUserInfo);
            var marketSignInBLL = new MarketSignInBLL(CurrentUserInfo);
            string content = string.Empty;

            string key = string.Empty;
            if (Request("VIPID") != null && Request("VIPID") != string.Empty)
            {
                key = Request("VIPID").ToString().Trim();
            }

            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page")));
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            var queryEntity = new VipSearchEntity();
            //queryEntity.VIPID = key;
            queryEntity.Page = pageIndex;
            queryEntity.PageSize = PageSize;

            if (Request("Gender") != "" && Request("Gender") != "--请选择--")
            {
                queryEntity.Gender = FormatParamValue(Request("Gender"));
            }
            if (Request("UserName") != "")
            {
                queryEntity.UserName = FormatParamValue(Request("UserName"));
            }
            if (Request("Enterprice") != "")
            {
                queryEntity.Enterprice = FormatParamValue(Request("Enterprice"));
            }
            if (Request("IsChainStores") != "")
            {
                queryEntity.IsChainStores = FormatParamValue(Request("IsChainStores"));
            }
            if (Request("IsWeiXinMarketing") != "" && Request("IsWeiXinMarketing") != "--请选择--")
            {
                queryEntity.IsWeiXinMarketing = FormatParamValue(Request("IsWeiXinMarketing"));
            }
            if (Request("EventId") != "")
            {
                queryEntity.EventId = FormatParamValue(Request("EventId"));
            }
            if (Request("tags") != "") // 标签及组合关系
            {
                queryEntity.Tags = FormatParamValue(Request("tags"));
            }

            //var data = service.SearchVipInfo(queryEntity);
            var data = marketSignInBLL.WebGetListAdd(queryEntity);

            var dataTotalCount = data.ICount;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.vipInfoList.ToJSON(),
                dataTotalCount);
            return content;
        }
        #endregion

        #region GetPersonListData
        /// <summary>
        /// 获取Person列表
        /// </summary>
        public string GetPersonListData()
        {
            var service = new MarketPersonBLL(CurrentUserInfo);
            string content = string.Empty;

            string key = string.Empty;
            if (Request("MarketEventID") != null && Request("MarketEventID") != string.Empty)
            {
                key = Request("MarketEventID").ToString().Trim();
            }

            var queryEntity = new MarketPersonEntity();
            queryEntity.MarketEventID = key;
            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;

            var data = service.GetList(queryEntity, pageIndex, 1000);
            var dataTotalCount = service.GetListCount(queryEntity);

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                dataTotalCount);
            return content;
        }
        #endregion

        #region SaveEventTemplateData
        /// <summary>
        /// 保存EventTemplate
        /// </summary>
        public string SaveEventTemplateData()
        {
            var service = new MarketEventBLL(CurrentUserInfo);
            var obj = new MarketEventEntity();
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string MarketEventID = string.Empty;
            if (Request("data") != null && Request("data") != string.Empty)
            {
                key = Request("data").ToString().Trim();
            }
            if (Request("MarketEventID") != null && Request("MarketEventID") != string.Empty)
            {
                MarketEventID = Request("MarketEventID").ToString().Trim();
            }

            obj = key.DeserializeJSONTo<MarketEventEntity>();

            if (MarketEventID.Trim().Length == 0 || MarketEventID == "null" || MarketEventID == "undefined")
            {
                obj.MarketEventID = Utils.NewGuid();
                obj.EventStatus = 1;
                service.Create(obj);
            }
            else
            {
                obj.MarketEventID = MarketEventID;
                service.Update(obj, false);
            }

            responseData.success = true;
            responseData.msg = error;
            responseData.data = obj.MarketEventID;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region SaveEventSendData
        /// <summary>
        /// 保存EventSend
        /// </summary>
        public string SaveEventSendData()
        {
            var service = new MarketEventBLL(CurrentUserInfo);
            var obj = new MarketEventEntity();
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string MarketEventID = string.Empty;
            bool chk = true;
            bool chkSMS = true;
            bool chkAPP = true;
            if (Request("data") != null && Request("data") != string.Empty)
            {
                key = Request("data").ToString().Trim();
            }
            if (Request("MarketEventID") != null && Request("MarketEventID") != string.Empty)
            {
                MarketEventID = Request("MarketEventID").ToString().Trim();
            }

            if (Request("chk") != null && Request("chk") != string.Empty)
            {
                chk = Convert.ToBoolean(Request("chk").ToString().Trim());
            }
            if (Request("chkSMS") != null && Request("chkSMS") != string.Empty)
            {
                chkSMS = Convert.ToBoolean(Request("chkSMS").ToString().Trim());
            }
            if (Request("chkAPP") != null && Request("chkAPP") != string.Empty)
            {
                chkAPP = Convert.ToBoolean(Request("chkAPP").ToString().Trim());
            }

            obj = key.DeserializeJSONTo<MarketEventEntity>();

            var eventObj = service.GetByID(MarketEventID);
            string msgUrl = ConfigurationManager.AppSettings["push_weixin_msg_url"].Trim();
            MarketPersonBLL marketPersonBLL = new MarketPersonBLL(this.CurrentUserInfo);
            var sendFlag = marketPersonBLL.SetEventPush(MarketEventID, msgUrl, eventObj.SendTypeId, chk, chkSMS, chkAPP);

            // Update
            obj.MarketEventID = MarketEventID;
            service.Update(obj, false);

            responseData.success = sendFlag;
            responseData.msg = error;
            responseData.data = obj.MarketEventID;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region GetUnitPropertyByIdData
        /// <summary>
        /// 通过ID获取UnitProperty信息
        /// </summary>
        public string GetUnitPropertyData()
        {
            var service = new VwUnitPropertyBLL(CurrentUserInfo);
            var marketPersonBLL = new MarketPersonBLL(CurrentUserInfo);
            VwUnitPropertyEntity data = new VwUnitPropertyEntity();
            string content = string.Empty;

            string key = CurrentUserInfo.CurrentUserRole.UnitId;
            //if (Request("UnitId") != null && Request("UnitId") != string.Empty)
            //{
            //    key = Request("UnitId").ToString().Trim();
            //}
            var eventId = Request("MarketEventID").ToString().Trim();

            var list = service.QueryByEntity(new VwUnitPropertyEntity() { UnitId = key }, null);
            if (list != null && list.Length > 0)
            {
                data = list[0];
            }

            var count1 = marketPersonBLL.GetMarketPersonSendCount(eventId, 1);
            var count2 = marketPersonBLL.GetMarketPersonSendCount(eventId, 2);
            var count3 = marketPersonBLL.GetMarketPersonSendCount(eventId, 3);

            var jsonData = new JsonData();
            jsonData.totalCount = list.Length.ToString();
            jsonData.data = data;
            jsonData.topics = count1 + "," + count2 + "," + count3;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion
    }

    #region QueryEntity
    public class RoleQueryEntity
    {
        public string app_sys_id;
    }
    #endregion

}