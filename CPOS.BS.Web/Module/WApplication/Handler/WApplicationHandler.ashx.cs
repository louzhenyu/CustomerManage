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
using System.Text;
using System.Configuration;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.BLL.WX;

namespace JIT.CPOS.BS.Web.Module.Basic.WApplication.Handler
{
    /// <summary>
    /// WApplicationHandler
    /// </summary>
    public class WApplicationHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
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
                case "SetWeChatTemplate":
                    content = SetWeChatTemplate();
                    break;
                case "search_wapplication":
                    content = GetWApplicationListData();
                    break;
                case "get_wapplication_by_id":
                    content = GetWApplicationInfoById();
                    break;
                case "wapplication_save":
                    content = SaveWApplicationData();
                    break;
                case "wapplication_delete":
                    content = WApplicationDeleteData();
                    break;
                case "search_wmenu":
                    content = GetWMenuListData();
                    break;
                case "search_wmenu_tree":
                    content = GetWMenuTreeListData();
                    break;
                case "get_wmenu_by_id":
                    content = GetWMenuInfoById();
                    break;
                case "wmenu_save":
                    content = SaveWMenuData();
                    break;
                case "wmenu_delete":
                    content = WMenuDeleteData();
                    break;
                case "search_wmenu_items_2":
                    content = GetWMenuItems2Data();
                    break;
                case "search_wmenu_items_3":
                    content = GetWMenuItems3Data();
                    break;
                case "search_wmenu_items_4":
                    content = GetWMenuItems4Data();
                    break;

                case "WMaterialText_save":
                    content = SaveWMaterialTextData();
                    break;
                case "get_WMaterialText_by_id":
                    content = GetWMaterialTextById();
                    break;
                case "WMaterialText_delete":
                    content = WMaterialTextDeleteData();
                    break;

                case "WMaterialImage_save":
                    content = SaveWMaterialImageData();
                    break;
                case "get_WMaterialImage_by_id":
                    content = GetWMaterialImageById();
                    break;
                case "WMaterialImage_delete":
                    content = WMaterialImageDeleteData();
                    break;

                case "WMaterialVoice_save":
                    content = SaveWMaterialVoiceData();
                    break;
                case "get_WMaterialVoice_by_id":
                    content = GetWMaterialVoiceById();
                    break;
                case "WMaterialVoice_delete":
                    content = WMaterialVoiceDeleteData();
                    break;

                case "WMaterialWriting_save":
                    content = SaveWMaterialWritingData();
                    break;
                case "get_WMaterialWriting_by_id":
                    content = GetWMaterialWritingById();
                    break;
                case "WMaterialWriting_delete":
                    content = WMaterialWritingDeleteData();
                    break;

                case "get_items1":
                    content = GetWModelItems1Data();
                    break;
                case "get_items2":
                    content = GetWModelItems2Data();
                    break;
                case "get_items3":
                    content = GetWModelItems3Data();
                    break;
                case "get_items4":
                    content = GetWModelItems4Data();
                    break;

                case "wmodel_save":
                    content = SaveWModelData();
                    break;
                case "get_wmodel_by_id":
                    content = GetWModelInfoById();
                    break;

                case "search_WKeywordReply":
                    content = GetWKeywordReplyListData();
                    break;
                case "get_WKeywordReply_by_id":
                    content = GetWKeywordReplyInfoById();
                    break;
                case "WKeywordReply_save":
                    content = SaveWKeywordReplyData();
                    break;
                case "WKeywordReply_delete":
                    content = WKeywordReplyDeleteData();
                    break;

                case "get_WAutoReply_by_id":
                    content = GetWAutoReplyInfoById();
                    break;
                case "WAutoReply_save":
                    content = SaveWAutoReplyData();
                    break;

                case "WMenu_publish":
                    content = WMenuPublishData();
                    break;
                case "get_ZCourse_by_id":
                    content = GetZCourseInfoById();
                    break;
                case "get_ZCourse_news":
                    content = GetZCourseNewsData();
                    break;
                case "get_ZCourse_Apply":
                    content = GetZCourseApplyData();
                    break;
                case "get_ZCourse_Reflections":
                    content = GetZCourseReflectionsData();
                    break;
                case "news_delete":     //新闻删除
                    content = NewsDeleteData();
                    break;
                case "get_news_by_id":  //根据ID获取新闻信息
                    content = GetNewsById();
                    break;
                case "news_save":       //保存新闻信息
                    content = SaveNews();
                    break;
                case "get_Course_Reflections_by_id":
                    content = GetCourseReflectionsById();
                    break;
                case "Course_Reflections_save":
                    content = SaveCourseReflections();
                    break;
                case "Course_Reflections_delete":
                    content = CourseReflectionsDeleteData();
                    break;
                case "Course_save":
                    content = SaveCourse();
                    break;

                case "search_WModelList":
                    content = GetWModelListData();
                    break;
                case "get_WModelList_by_id":
                    content = GetWModelListInfoById();
                    break;
                case "WModelList_save":
                    content = SaveWModelListData();
                    break;
                case "WModelList_delete":
                    content = WModelListDeleteData();
                    break;
                case "checkdlete":
                    break;
                case "RemoveSessionById":
                    content = RemoveSessionById();
                    break;
                case "import":
                    content = Bulkimport();
                    break;
                case "ImportWXUser":
                    content = ImportWXUser();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region 设置微信消息模板
        public string SetWeChatTemplate()
        {
            var CommonBLL = new CommonBLL();
            var WXTMConfigBLL = new WXTMConfigBLL(CurrentUserInfo);
            string content = string.Empty;
            bool status = true;
            string message = string.Empty;
            var responseData = new ResponseData();
            string ApplicationId = Request("ApplicationId").ToString();

            //var Result = WXTMConfigBLL.QueryByEntity(new WXTMConfigEntity() { CustomerId = CurrentUserInfo.ClientID }, null).ToList();
            //if (Result.Count > 0)
            //{
            //    responseData.success = status;
            //    responseData.msg = "已设置微信消息模板！";
            //    content = responseData.ToJSON();
            //    return content;
            //}     


            if (!string.IsNullOrWhiteSpace(ApplicationId))
            {
                string FirstText = "连锁掌柜欢迎你！";
                string RemarkText = "连锁掌柜谢谢你！";

                var ArrayList = (new string[8] { "OPENTM200565259", "TM00211", "TM00398", "TM00230", "OPENTM205454780", "OPENTM207444083", "TM00701", "OPENTM206623166" }).ToList();

                foreach (var item in ArrayList)
                {
                    #region 匹配标题，备注内容
                    switch (item)
                    {
                        case "OPENTM200565259"://发货通知
                            FirstText = "亲，宝贝已经启程了，好想快点来到你身边";
                            RemarkText = "如有问题请直接在微信里留言，我们将在第一时间为您服务！";
                            break;
                        case "TM00211"://返现通知
                            FirstText = "您的返现到账啦~";
                            RemarkText = "到期时间：";
                            break;
                        case "TM00398"://付款成功通知
                            FirstText = "您的订单已完成付款，商家将即时为您发货。";
                            RemarkText = ">>查看订单详情";
                            break;
                        case "TM00230"://积分变动通知
                            FirstText = "您的积分账户变更如下";
                            RemarkText = "立即查看我的账户";
                            break;
                        case "OPENTM205454780"://账户余额变动通知
                            FirstText = "您的账户余额发生变动如下。";
                            RemarkText = "如对上述余额变动有异议，请联系客服人员协助处理。";
                            break;
                        case "OPENTM207444083"://电子券到账提醒 
                            FirstText = "您有一张新优惠券到账啦";
                            RemarkText = "如此厚爱给特别的你，还不快去享用~";
                            break;
                        case "TM00701":
                            FirstText = "您有一笔订单未支付，即将关闭。";
                            RemarkText = "未付款订单24时内将关闭，请及时付款。点击查看详情。";
                            break;
                        case "OPENTM206623166":
                            FirstText = "您的优惠券即将过期。";
                            RemarkText = "请合理使用优惠券";
                            break;
                    }
                    #endregion

                    CommonBLL.AddWXTemplateID(ApplicationId, item, FirstText, RemarkText, CurrentUserInfo);
                    FirstText = "连锁掌柜欢迎你！";
                    RemarkText = "连锁掌柜谢谢你！";
                }
                message = "设置成功！";
            }
            else
            {
                message = "已设置微信消息模板！";
            }
            responseData.success = status;
            responseData.msg = message;
            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region GetWApplicationListData
        /// <summary>
        /// 查询申请接口
        /// </summary>
        public string GetWApplicationListData()
        {
            var form = Request("form").DeserializeJSONTo<WApplicationQueryEntity>();

            var wApplicationInterfaceBLL = new WApplicationInterfaceBLL(CurrentUserInfo);
            IList<WApplicationInterfaceEntity> tagsList;
            string content = string.Empty;

            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;

            WApplicationInterfaceEntity queryEntity = new WApplicationInterfaceEntity();
            queryEntity.WeiXinName = FormatParamValue(form.WeiXinName);
            queryEntity.WeiXinID = FormatParamValue(form.WeiXinID);
            queryEntity.CustomerId = CurrentUserInfo.CurrentUser.customer_id;

            tagsList = wApplicationInterfaceBLL.GetWebWApplicationInterface(queryEntity, pageIndex, PageSize);
            var dataTotalCount = wApplicationInterfaceBLL.GetWebWApplicationInterfaceCount(queryEntity);

            var jsonData = new JsonData();
            jsonData.totalCount = dataTotalCount.ToString();
            jsonData.data = tagsList;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                jsonData.data.ToJSON(),
                jsonData.totalCount);
            return content;
        }
        #endregion

        #region GetWApplicationInfoById
        /// <summary>
        /// 根据申请接口ID获取申请接口信息
        /// </summary>
        public string GetWApplicationInfoById()
        {
            var service = new WApplicationInterfaceBLL(CurrentUserInfo);
            WApplicationInterfaceEntity obj = new WApplicationInterfaceEntity();
            string content = string.Empty;

            string key = string.Empty;
            if (Request("WApplicationID") != null && Request("WApplicationID") != string.Empty)
            {
                key = Request("WApplicationID").ToString().Trim();
            }

            obj = service.GetByID(key);

            var jsonData = new JsonData();
            jsonData.totalCount = obj == null ? "0" : "1";
            jsonData.data = obj;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region SaveWApplicationData
        /// <summary>
        /// 保存申请接口
        /// </summary>
        public string SaveWApplicationData()
        {
            var service = new WApplicationInterfaceBLL(CurrentUserInfo);
            WApplicationInterfaceEntity item = new WApplicationInterfaceEntity();
            string content = string.Empty;
            var responseData = new ResponseData();

            string key = string.Empty;
            string item_id = string.Empty;
            if (Request("item") != null && Request("item") != string.Empty)
            {
                key = Request("item").ToString().Trim();
            }
            if (Request("WApplicationId") != null && Request("WApplicationId") != string.Empty)
            {
                item_id = Request("WApplicationId").ToString().Trim();
            }

            item = key.DeserializeJSONTo<WApplicationInterfaceEntity>();

            if (item.WeiXinName == null || item.WeiXinName.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "微信账号名称不能为空";
                return responseData.ToJSON();
            }
            if (item.WeiXinID == null || item.WeiXinID.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "微信账号唯一码不能为空";
                return responseData.ToJSON();
            }

            bool status = true;
            string message = "保存成功";
            if (item.ApplicationId.Trim().Length == 0)
            {
                item.ApplicationId = Utils.NewGuid();
                service.Create(item);
            }
            else
            {
                item.ApplicationId = item_id;
                service.Update(item, false);
            }
            //Jermyn20140515 提交微信初级菜单
            service.setCreateWXMenu(item);
            //Jermyn20131120 提交管理平台
            service.setCposApMapping(item);

            responseData.success = status;
            responseData.msg = message;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region WApplicationDeleteData
        /// <summary>
        /// 删除
        /// </summary>
        public string WApplicationDeleteData()
        {
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

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

            string[] ids = key.Split(',');
            new WApplicationInterfaceBLL(this.CurrentUserInfo).Delete(new WApplicationInterfaceEntity()
            {
                ApplicationId = key
            });

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region GetWMenuListData
        /// <summary>
        /// 查询菜单
        /// </summary>
        public string GetWMenuListData()
        {
            var form = Request("form").DeserializeJSONTo<WMenuQueryEntity>();

            var wMenuBLL = new WMenuBLL(CurrentUserInfo);
            IList<WMenuEntity> list;
            string content = string.Empty;

            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;

            WMenuEntity queryEntity = new WMenuEntity();
            queryEntity.Name = FormatParamValue(form.Name);
            queryEntity.Key = FormatParamValue(form.Key);
            queryEntity.Type = FormatParamValue(form.Type);
            queryEntity.Level = FormatParamValue(form.Level);
            queryEntity.WeiXinID = FormatParamValue(form.WeiXinID);
            queryEntity.ApplicationId = FormatParamValue(Request("ApplicationId"));
            if (Request("parentId") != null)// && Request("parentId") != "ALL")
            {
                queryEntity.ParentId = FormatParamValue(Request("parentId"));
            }

            list = wMenuBLL.GetWebWMenu(queryEntity, pageIndex, PageSize);
            var dataTotalCount = wMenuBLL.GetWebWMenuCount(queryEntity);

            var jsonData = new JsonData();
            jsonData.totalCount = dataTotalCount.ToString();
            jsonData.data = list;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                jsonData.data.ToJSON(),
                jsonData.totalCount);
            return content;
        }
        #endregion

        #region GetWMenuTreeListData
        /// <summary>
        /// 查询菜单Tree
        /// </summary>
        public string GetWMenuTreeListData()
        {
            var wMenuBLL = new WMenuBLL(CurrentUserInfo);
            IList<WMenuEntity> list;
            WMenuEntity queryEntity = new WMenuEntity();
            queryEntity.ParentId = "";
            queryEntity.ApplicationId = FormatParamValue(Request("ApplicationId"));
            list = wMenuBLL.GetWebWMenu(queryEntity, 0, 1000);
            StringBuilder sb = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                sb.Append("{\"expanded\": true, \"text\":\"菜单\", \"children\": [");
                foreach (var item in list)
                {
                    //sb.Append("{\"expanded\": true, \"leaf\":true, ");
                    sb.Append("{\"expanded\": true, ");
                    sb.Append("\"ID\":\"" + item.ID + "\",");
                    sb.Append("\"ParentId\":\"" + item.ParentId + "\",");
                    sb.Append("\"WeiXinID\":\"" + item.WeiXinID + "\",");
                    sb.Append("\"Name\":\"" + item.Name + "\",");
                    sb.Append("\"Key\":\"" + item.Key + "\",");
                    sb.Append("\"Type\":\"" + item.Type + "\",");
                    sb.Append("\"Level\":\"" + item.Level + "\",");
                    sb.Append("\"DisplayColumn\":\"" + item.DisplayColumn + "\",");
                    sb.Append("\"CreateTime\":\"" + item.CreateTime + "\",");
                    sb.Append("\"CreateBy\":\"" + item.CreateBy + "\",");
                    sb.Append("\"LastUpdateBy\":\"" + item.LastUpdateBy + "\",");
                    sb.Append("\"LastUpdateTime\":\"" + item.LastUpdateTime + "\",");
                    sb.Append("\"IsDelete\":\"" + item.IsDelete + "\",");
                    sb.Append("\"MaterialTypeId\":\"" + item.MaterialTypeId + "\",");
                    sb.Append("\"Text\":\"" + item.Text + "\",");
                    sb.Append("\"ImageId\":\"" + item.ImageId + "\",");
                    sb.Append("\"TextId\":\"" + item.TextId + "\",");
                    sb.Append("\"VoiceId\":\"" + item.VoiceId + "\",");
                    sb.Append("\"VideoId\":\"" + item.VideoId + "\",");
                    //提取子菜单
                    GetWMenuTreeChildrenJson(wMenuBLL, item.ID, sb);
                    sb.Append("},");
                }
                sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("]}");
            return sb.ToString();
        }
        public void GetWMenuTreeChildrenJson(WMenuBLL service, string parentId, StringBuilder sb)
        {
            IList<WMenuEntity> list;
            WMenuEntity queryEntity = new WMenuEntity();
            queryEntity.ParentId = parentId;
            list = service.GetWebWMenu(queryEntity, 0, 1000);
            sb.Append("\"leaf\":false,\"expanded\": true, \"children\":[");
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    sb.Append("{");
                    sb.Append("\"ID\":\"" + item.ID + "\",");
                    sb.Append("\"ParentId\":\"" + item.ParentId + "\",");
                    sb.Append("\"WeiXinID\":\"" + item.WeiXinID + "\",");
                    sb.Append("\"Name\":\"" + item.Name + "\",");
                    sb.Append("\"Key\":\"" + item.Key + "\",");
                    sb.Append("\"Type\":\"" + item.Type + "\",");
                    sb.Append("\"Level\":\"" + item.Level + "\",");
                    sb.Append("\"DisplayColumn\":\"" + item.DisplayColumn + "\",");
                    sb.Append("\"CreateTime\":\"" + item.CreateTime + "\",");
                    sb.Append("\"CreateBy\":\"" + item.CreateBy + "\",");
                    sb.Append("\"LastUpdateBy\":\"" + item.LastUpdateBy + "\",");
                    sb.Append("\"LastUpdateTime\":\"" + item.LastUpdateTime + "\",");
                    sb.Append("\"IsDelete\":\"" + item.IsDelete + "\",");
                    sb.Append("\"MaterialTypeId\":\"" + item.MaterialTypeId + "\",");
                    sb.Append("\"Text\":\"" + item.Text + "\",");
                    sb.Append("\"ImageId\":\"" + item.ImageId + "\",");
                    sb.Append("\"TextId\":\"" + item.TextId + "\",");
                    sb.Append("\"VoiceId\":\"" + item.VoiceId + "\",");
                    sb.Append("\"VideoId\":\"" + item.VideoId + "\",");
                    //提取子菜单
                    GetWMenuTreeChildrenJson(service, item.ID, sb);
                    sb.Append("},");
                }
                sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("]");
        }
        #endregion

        #region GetWMenuInfoById
        /// <summary>
        /// 根据菜单ID获取菜单信息
        /// </summary>
        public string GetWMenuInfoById()
        {
            var service = new WMenuBLL(CurrentUserInfo);
            var wMaterialImageBLL = new WMaterialImageBLL(CurrentUserInfo);
            var wMaterialTextBLL = new WMaterialTextBLL(CurrentUserInfo);
            var wMaterialVoiceBLL = new WMaterialVoiceBLL(CurrentUserInfo);
            var wApplicationInterfaceBLL = new WApplicationInterfaceBLL(CurrentUserInfo);
            WMenuEntity obj = new WMenuEntity();
            string content = string.Empty;

            string key = string.Empty;
            if (Request("WMenuID") != null && Request("WMenuID") != string.Empty)
            {
                key = Request("WMenuID").ToString().Trim();
            }

            obj = service.GetByID(key);

            if (obj.MaterialTypeId == "2")
            {
                var tmpObj = wMaterialImageBLL.GetByID(obj.TextId);
                if (tmpObj != null) obj.WMaterialImageTitle = tmpObj.ImageName;
            }
            if (obj.MaterialTypeId == "3")
            {
                var tmpObj = wMaterialTextBLL.GetByID(obj.TextId);
                if (tmpObj != null) obj.WMaterialTextTitle = tmpObj.Title;
            }
            if (obj.MaterialTypeId == "4")
            {
                var tmpObj = wMaterialVoiceBLL.GetByID(obj.TextId);
                if (tmpObj != null) obj.WMaterialVoiceTitle = tmpObj.VoiceName;
            }

            if (obj.ParentId != null && obj.ParentId.Length > 0)
            {
                var parentObj = service.GetByID(obj.ParentId);
                obj.ParentName = parentObj == null ? string.Empty : parentObj.Name;
            }
            if (obj.WeiXinID != null && obj.WeiXinID.Length > 0)
            {
                var appObj = wApplicationInterfaceBLL.QueryByEntity(new WApplicationInterfaceEntity()
                {
                    WeiXinID = obj.WeiXinID
                }, null);
                if (appObj != null && appObj.Length > 0)
                {
                    obj.ApplicationId = appObj[0].ApplicationId;
                }
            }

            var jsonData = new JsonData();
            jsonData.totalCount = obj == null ? "0" : "1";
            jsonData.data = obj;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region SaveWMenuData
        /// <summary>
        /// 保存菜单
        /// </summary>
        public string SaveWMenuData()
        {
            var service = new WMenuBLL(CurrentUserInfo);
            var wApplicationInterfaceBLL = new WApplicationInterfaceBLL(CurrentUserInfo);
            WMenuEntity item = new WMenuEntity();
            string content = string.Empty;
            var responseData = new ResponseData();

            string key = string.Empty;
            string item_id = string.Empty;
            if (Request("item") != null && Request("item") != string.Empty)
            {
                key = Request("item").ToString().Trim();
            }
            if (Request("WMenuId") != null && Request("WMenuId") != string.Empty)
            {
                item_id = Request("WMenuId").ToString().Trim();
            }

            item = key.DeserializeJSONTo<WMenuEntity>();

            WMenuEntity parentObj = new WMenuEntity();
            parentObj.Level = "0";
            if (item.ParentId != null && item.ParentId.Trim().Length > 0)
            {
                parentObj = service.GetByID(item.ParentId);
            }

            if (item.ApplicationId == null || item.ApplicationId.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "微信账号不能为空";
                return responseData.ToJSON();
            }
            var appObj = wApplicationInterfaceBLL.GetByID(item.ApplicationId);
            if (appObj == null || appObj.WeiXinID == null || appObj.WeiXinID.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "微信账号唯一码不能为空";
                return responseData.ToJSON();
            }
            item.WeiXinID = appObj.WeiXinID;
            if (item.Name == null || item.Name.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "名称不能为空";
                return responseData.ToJSON();
            }
            item.Key = Utils.NewGuid().Substring(0, 7);
            if (item.Key == null || item.Key.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "菜单KEY值不能为空";
                return responseData.ToJSON();
            }
            if (item.Type == null || item.Type.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "类型不能为空";
                return responseData.ToJSON();
            }
            item.Level = (int.Parse(parentObj.Level) + 1).ToString();
            if (item.Level == null || item.Level.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "菜单级别不能为空";
                return responseData.ToJSON();
            }

            bool status = true;
            string message = "保存成功";
            if (item.ID.Trim().Length == 0)
            {
                item.ID = Utils.NewGuid();
                service.Create(item);
            }
            else
            {
                item.ID = item_id;
                service.Update(item, false);
            }

            responseData.success = status;
            responseData.msg = message;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region WMenuDeleteData
        /// <summary>
        /// 删除
        /// </summary>
        public string WMenuDeleteData()
        {
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

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

            string[] ids = key.Split(',');
            if (checkdlete(key))
            {
                responseData.success = false;
                responseData.msg = "模板已被引用不能删除";
                return responseData.ToJSON();
            }
            new WMenuBLL(this.CurrentUserInfo).Delete(new WMenuEntity()
            {
                ID = key
            });

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region GetWMenuItems2Data
        /// <summary>
        /// 查询
        /// </summary>
        public string GetWMenuItems2Data()
        {
            var service = new WMaterialImageBLL(CurrentUserInfo);
            IList<WMaterialImageEntity> list;
            string content = string.Empty;

            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;

            WMaterialImageEntity queryEntity = new WMaterialImageEntity();

            list = service.GetWebList(queryEntity, pageIndex, PageSize);
            var dataTotalCount = service.GetWebListCount(queryEntity);

            var jsonData = new JsonData();
            jsonData.totalCount = dataTotalCount.ToString();
            jsonData.data = list;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                jsonData.data.ToJSON(),
                jsonData.totalCount);
            return content;
        }
        #endregion

        #region GetWMenuItems3Data
        /// <summary>
        /// 查询
        /// </summary>
        public string GetWMenuItems3Data()
        {
            var service = new WMaterialTextBLL(CurrentUserInfo);
            IList<WMaterialTextEntity> list;
            string content = string.Empty;

            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;

            WMaterialTextEntity queryEntity = new WMaterialTextEntity();

            list = service.GetWebList(queryEntity, pageIndex, PageSize);
            var dataTotalCount = service.GetWebListCount(queryEntity);

            var jsonData = new JsonData();
            jsonData.totalCount = dataTotalCount.ToString();
            jsonData.data = list;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                jsonData.data.ToJSON(),
                jsonData.totalCount);
            return content;
        }
        #endregion

        #region GetWMenuItems4Data
        /// <summary>
        /// 查询
        /// </summary>
        public string GetWMenuItems4Data()
        {
            var service = new WMaterialVoiceBLL(CurrentUserInfo);
            IList<WMaterialVoiceEntity> list;
            string content = string.Empty;

            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;

            WMaterialVoiceEntity queryEntity = new WMaterialVoiceEntity();

            list = service.GetWebList(queryEntity, pageIndex, PageSize);
            var dataTotalCount = service.GetWebListCount(queryEntity);

            var jsonData = new JsonData();
            jsonData.totalCount = dataTotalCount.ToString();
            jsonData.data = list;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                jsonData.data.ToJSON(),
                jsonData.totalCount);
            return content;
        }
        #endregion

        #region SaveWMaterialTextData
        /// <summary>
        /// 保存WMaterialText
        /// </summary>
        public string SaveWMaterialTextData()
        {
            var service = new WMaterialTextBLL(CurrentUserInfo);
            var wModelTextMappingBLL = new WModelTextMappingBLL(CurrentUserInfo);
            WMaterialTextEntity item = new WMaterialTextEntity();
            string content = string.Empty;
            var responseData = new ResponseData();

            string key = string.Empty;
            string item_id = string.Empty;
            if (Request("item") != null && Request("item") != string.Empty)
            {
                key = Request("item").ToString().Trim();
            }
            if (Request("Id") != null && Request("Id") != string.Empty)
            {
                item_id = Request("Id").ToString().Trim();
            }

            item = key.DeserializeJSONTo<WMaterialTextEntity>();

            if (item.Title == null || item.Title.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "标题不能为空";
                return responseData.ToJSON();
            }
            if (item.DisplayIndex == null || item.DisplayIndex.ToString().Length == 0)
            {
                item.DisplayIndex = 0;
            }

            bool status = true;
            string message = "保存成功";
            if (item.TextId.Trim().Length == 0)
            {
                item.TextId = Utils.NewGuid();

                if (item.ModelId == null || item.ModelId.Trim().Length == 0)
                {
                    responseData.success = false;
                    responseData.msg = "模块ID不能为空";
                    return responseData.ToJSON();
                }

                //添加默认原文链接
                if (string.IsNullOrEmpty(item.OriginalUrl))
                {
                    var originalUrl = ConfigurationManager.AppSettings["original_url"].Trim();
                    item.OriginalUrl = originalUrl + "/WeiXin/TextImageWap.aspx?customerId=" + this.CurrentUserInfo.CurrentUser.customer_id + "&id=" + item.TextId;
                }

                service.Create(item);

                wModelTextMappingBLL.Create(new WModelTextMappingEntity()
                {
                    MappingId = Utils.NewGuid(),
                    ModelId = item.ModelId,
                    TextId = item.TextId
                });
            }
            else
            {
                item.TextId = item_id;
                service.Update(item, false);
            }

            responseData.success = status;
            responseData.msg = message;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region GetWMaterialTextById
        /// <summary>
        /// GetWMaterialTextById
        /// </summary>
        public string GetWMaterialTextById()
        {
            var service = new WMaterialTextBLL(CurrentUserInfo);
            WMaterialTextEntity obj = new WMaterialTextEntity();
            string content = string.Empty;

            string key = string.Empty;
            if (Request("Id") != null && Request("Id") != string.Empty)
            {
                key = Request("Id").ToString().Trim();
            }

            obj = service.GetByID(key);

            var jsonData = new JsonData();
            jsonData.totalCount = obj == null ? "0" : "1";
            jsonData.data = obj;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region WMaterialTextDeleteData
        /// <summary>
        /// 删除
        /// </summary>
        public string WMaterialTextDeleteData()
        {
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

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

            string[] ids = key.Split(',');
            new WMaterialTextBLL(this.CurrentUserInfo).Delete(new WMaterialTextEntity()
            {
                TextId = key
            });

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region SaveWMaterialImageData
        /// <summary>
        /// 保存WMaterialImage
        /// </summary>
        public string SaveWMaterialImageData()
        {
            var service = new WMaterialImageBLL(CurrentUserInfo);
            var wModelImageMappingBLL = new WModelImageMappingBLL(CurrentUserInfo);
            WMaterialImageEntity item = new WMaterialImageEntity();
            string content = string.Empty;
            var responseData = new ResponseData();

            string key = string.Empty;
            string item_id = string.Empty;
            if (Request("item") != null && Request("item") != string.Empty)
            {
                key = Request("item").ToString().Trim();
            }
            if (Request("Id") != null && Request("Id") != string.Empty)
            {
                item_id = Request("Id").ToString().Trim();
            }

            item = key.DeserializeJSONTo<WMaterialImageEntity>();

            if (item.ImageName == null || item.ImageName.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "名称不能为空";
                return responseData.ToJSON();
            }

            bool status = true;
            string message = "保存成功";
            if (item.ImageId.Trim().Length == 0)
            {
                item.ImageId = Utils.NewGuid();

                if (item.ModelId == null || item.ModelId.Trim().Length == 0)
                {
                    responseData.success = false;
                    responseData.msg = "模块ID不能为空";
                    return responseData.ToJSON();
                }

                service.Create(item);

                wModelImageMappingBLL.Create(new WModelImageMappingEntity()
                {
                    MappingId = Utils.NewGuid(),
                    ModelId = item.ModelId,
                    ImageId = item.ImageId
                });
            }
            else
            {
                item.ImageId = item_id;
                service.Update(item, false);
            }

            responseData.success = status;
            responseData.msg = message;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region GetWMaterialImageById
        /// <summary>
        /// GetWMaterialImageById
        /// </summary>
        public string GetWMaterialImageById()
        {
            var service = new WMaterialImageBLL(CurrentUserInfo);
            WMaterialImageEntity obj = new WMaterialImageEntity();
            string content = string.Empty;

            string key = string.Empty;
            if (Request("Id") != null && Request("Id") != string.Empty)
            {
                key = Request("Id").ToString().Trim();
            }

            obj = service.GetByID(key);

            var jsonData = new JsonData();
            jsonData.totalCount = obj == null ? "0" : "1";
            jsonData.data = obj;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region WMaterialImageDeleteData
        /// <summary>
        /// 删除
        /// </summary>
        public string WMaterialImageDeleteData()
        {
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

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

            string[] ids = key.Split(',');
            new WMaterialImageBLL(this.CurrentUserInfo).Delete(new WMaterialImageEntity()
            {
                ImageId = key
            });

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region SaveWMaterialVoiceData
        /// <summary>
        /// 保存WMaterialVoice
        /// </summary>
        public string SaveWMaterialVoiceData()
        {
            var service = new WMaterialVoiceBLL(CurrentUserInfo);
            var wModelVoiceMappingBLL = new WModelVoiceMappingBLL(CurrentUserInfo);
            WMaterialVoiceEntity item = new WMaterialVoiceEntity();
            string content = string.Empty;
            var responseData = new ResponseData();

            string key = string.Empty;
            string item_id = string.Empty;
            if (Request("item") != null && Request("item") != string.Empty)
            {
                key = Request("item").ToString().Trim();
            }
            if (Request("Id") != null && Request("Id") != string.Empty)
            {
                item_id = Request("Id").ToString().Trim();
            }

            item = key.DeserializeJSONTo<WMaterialVoiceEntity>();

            if (item.VoiceName == null || item.VoiceName.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "名称不能为空";
                return responseData.ToJSON();
            }

            bool status = true;
            string message = "保存成功";
            if (item.VoiceId.Trim().Length == 0)
            {
                item.VoiceId = Utils.NewGuid();

                if (item.ModelId == null || item.ModelId.Trim().Length == 0)
                {
                    responseData.success = false;
                    responseData.msg = "模块ID不能为空";
                    return responseData.ToJSON();
                }

                service.Create(item);

                wModelVoiceMappingBLL.Create(new WModelVoiceMappingEntity()
                {
                    MappingId = Utils.NewGuid(),
                    ModelId = item.ModelId,
                    VoiceId = item.VoiceId
                });
            }
            else
            {
                item.VoiceId = item_id;
                service.Update(item, false);
            }

            responseData.success = status;
            responseData.msg = message;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region GetWMaterialVoiceById
        /// <summary>
        /// GetWMaterialVoiceById
        /// </summary>
        public string GetWMaterialVoiceById()
        {
            var service = new WMaterialVoiceBLL(CurrentUserInfo);
            WMaterialVoiceEntity obj = new WMaterialVoiceEntity();
            string content = string.Empty;

            string key = string.Empty;
            if (Request("Id") != null && Request("Id") != string.Empty)
            {
                key = Request("Id").ToString().Trim();
            }

            obj = service.GetByID(key);

            var jsonData = new JsonData();
            jsonData.totalCount = obj == null ? "0" : "1";
            jsonData.data = obj;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region WMaterialVoiceDeleteData
        /// <summary>
        /// 删除
        /// </summary>
        public string WMaterialVoiceDeleteData()
        {
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

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

            string[] ids = key.Split(',');
            new WMaterialVoiceBLL(this.CurrentUserInfo).Delete(new WMaterialVoiceEntity()
            {
                VoiceId = key
            });

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region SaveWMaterialWritingData
        /// <summary>
        /// 保存WMaterialWriting
        /// </summary>
        public string SaveWMaterialWritingData()
        {
            var service = new WMaterialWritingBLL(CurrentUserInfo);
            var wModelWritingMappingBLL = new WModelWritingMappingBLL(CurrentUserInfo);
            WMaterialWritingEntity item = new WMaterialWritingEntity();
            string content = string.Empty;
            var responseData = new ResponseData();

            string key = string.Empty;
            string item_id = string.Empty;
            if (Request("item") != null && Request("item") != string.Empty)
            {
                key = Request("item").ToString().Trim();
            }
            if (Request("Id") != null && Request("Id") != string.Empty)
            {
                item_id = Request("Id").ToString().Trim();
            }

            item = key.DeserializeJSONTo<WMaterialWritingEntity>();

            if (item.Content == null || item.Content.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "文本不能为空";
                return responseData.ToJSON();
            }

            bool status = true;
            string message = "保存成功";
            if (item.WritingId.Trim().Length == 0)
            {
                item.WritingId = Utils.NewGuid();

                if (item.ModelId == null || item.ModelId.Trim().Length == 0)
                {
                    responseData.success = false;
                    responseData.msg = "模块ID不能为空";
                    return responseData.ToJSON();
                }

                service.Create(item);

                wModelWritingMappingBLL.Create(new WModelWritingMappingEntity()
                {
                    MappingId = Utils.NewGuid(),
                    ModelId = item.ModelId,
                    WritingId = item.WritingId
                });
            }
            else
            {
                item.WritingId = item_id;
                service.Update(item, false);
            }

            responseData.success = status;
            responseData.msg = message;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region GetWMaterialWritingById
        /// <summary>
        /// GetWMaterialWritingById
        /// </summary>
        public string GetWMaterialWritingById()
        {
            var service = new WMaterialWritingBLL(CurrentUserInfo);
            WMaterialWritingEntity obj = new WMaterialWritingEntity();
            string content = string.Empty;

            string key = string.Empty;
            if (Request("Id") != null && Request("Id") != string.Empty)
            {
                key = Request("Id").ToString().Trim();
            }

            obj = service.GetByID(key);

            var jsonData = new JsonData();
            jsonData.totalCount = obj == null ? "0" : "1";
            jsonData.data = obj;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region WMaterialWritingDeleteData
        /// <summary>
        /// 删除
        /// </summary>
        public string WMaterialWritingDeleteData()
        {
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

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

            string[] ids = key.Split(',');
            new WMaterialWritingBLL(this.CurrentUserInfo).Delete(new WMaterialWritingEntity()
            {
                WritingId = key
            });

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region GetWModelItems1Data
        /// <summary>
        /// 查询
        /// </summary>
        public string GetWModelItems1Data()
        {
            var service = new WMaterialWritingBLL(CurrentUserInfo);
            IList<WMaterialWritingEntity> list;
            string content = string.Empty;

            int pageIndex = 0;
            int page = Utils.GetIntVal(FormatParamValue(Request("page")));
            if (page > 0)
                pageIndex = page - 1;

            var modelId = FormatParamValue(Request("Id"));

            WMaterialWritingEntity queryEntity = new WMaterialWritingEntity();
            queryEntity.ModelId = modelId;

            list = service.GetWebList(queryEntity, pageIndex, PageSize);
            var dataTotalCount = service.GetWebListCount(queryEntity);

            var jsonData = new JsonData();
            jsonData.totalCount = dataTotalCount.ToString();
            jsonData.data = list;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                jsonData.data.ToJSON(),
                jsonData.totalCount);
            return content;
        }
        #endregion

        #region GetWModelItems2Data
        /// <summary>
        /// 查询
        /// </summary>
        public string GetWModelItems2Data()
        {
            var service = new WMaterialImageBLL(CurrentUserInfo);
            IList<WMaterialImageEntity> list;
            string content = string.Empty;

            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;
            var modelId = FormatParamValue(Request("Id"));

            WMaterialImageEntity queryEntity = new WMaterialImageEntity();
            queryEntity.ModelId = modelId;

            list = service.GetWebList(queryEntity, pageIndex, PageSize);
            var dataTotalCount = service.GetWebListCount(queryEntity);

            var jsonData = new JsonData();
            jsonData.totalCount = dataTotalCount.ToString();
            jsonData.data = list;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                jsonData.data.ToJSON(),
                jsonData.totalCount);
            return content;
        }
        #endregion

        #region GetWModelItems3Data
        /// <summary>
        /// 查询
        /// </summary>
        public string GetWModelItems3Data()
        {
            var service = new WMaterialTextBLL(CurrentUserInfo);
            IList<WMaterialTextEntity> list;
            string content = string.Empty;

            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;
            var modelId = FormatParamValue(Request("Id"));

            WMaterialTextEntity queryEntity = new WMaterialTextEntity();
            queryEntity.ModelId = modelId;

            list = service.GetWebList(queryEntity, pageIndex, PageSize);
            var dataTotalCount = service.GetWebListCount(queryEntity);

            var jsonData = new JsonData();
            jsonData.totalCount = dataTotalCount.ToString();
            jsonData.data = list;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                jsonData.data.ToJSON(),
                jsonData.totalCount);
            return content;
        }
        #endregion

        #region GetWModelItems4Data
        /// <summary>
        /// 查询
        /// </summary>
        public string GetWModelItems4Data()
        {
            var service = new WMaterialVoiceBLL(CurrentUserInfo);
            IList<WMaterialVoiceEntity> list;
            string content = string.Empty;

            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;
            var modelId = FormatParamValue(Request("Id"));

            WMaterialVoiceEntity queryEntity = new WMaterialVoiceEntity();
            queryEntity.ModelId = modelId;

            list = service.GetWebList(queryEntity, pageIndex, PageSize);
            var dataTotalCount = service.GetWebListCount(queryEntity);

            var jsonData = new JsonData();
            jsonData.totalCount = dataTotalCount.ToString();
            jsonData.data = list;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                jsonData.data.ToJSON(),
                jsonData.totalCount);
            return content;
        }
        #endregion

        #region SaveWModelData
        /// <summary>
        /// 保存
        /// </summary>
        public string SaveWModelData()
        {
            var service = new WModelBLL(CurrentUserInfo);
            WModelEntity item = new WModelEntity();
            string content = string.Empty;
            var responseData = new ResponseData();

            string key = string.Empty;
            string item_id = string.Empty;
            if (Request("item") != null && Request("item") != string.Empty)
            {
                key = Request("item").ToString().Trim();
            }
            if (Request("ModelId") != null && Request("ModelId") != string.Empty)
            {
                item_id = Request("ModelId").ToString().Trim();
            }

            item = key.DeserializeJSONTo<WModelEntity>();

            //if (item.ModelId == null || item.ModelId.Trim().Length == 0)
            //{
            //    responseData.success = false;
            //    responseData.msg = "ID不能为空";
            //    return responseData.ToJSON();
            //}

            bool status = true;
            string message = "保存成功";
            if (item.ModelId.Trim().Length == 0)
            {
                item.ModelId = Utils.NewGuid();
                service.Create(item);
            }
            else
            {
                item.ModelId = item_id;
                service.Update(item, false);
            }

            if (item.MaterialTextList != null && item.MaterialTextList.Count > 0)
            {
                WMaterialTextBLL materialService = new WMaterialTextBLL(CurrentUserInfo);

                for (int i = 0; i < item.MaterialTextList.Count; i++)
                {
                    item.MaterialTextList[i].ParentTextId = item.MaterialTextList.FirstOrDefault().TextId;
                    materialService.Update(item.MaterialTextList[i]);
                }
            }

            responseData.success = status;
            responseData.msg = message;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region GetWModelInfoById
        /// <summary>
        /// 获取信息
        /// </summary>
        public string GetWModelInfoById()
        {
            var service = new WModelBLL(CurrentUserInfo);
            WModelEntity obj = new WModelEntity();
            string content = string.Empty;

            string key = string.Empty;
            if (Request("ModelId") != null && Request("ModelId") != string.Empty)
            {
                key = Request("ModelId").ToString().Trim();
            }

            obj = service.GetByID(key);

            var jsonData = new JsonData();
            jsonData.totalCount = obj == null ? "0" : "1";
            jsonData.data = obj;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region GetWKeywordReplyListData
        /// <summary>
        /// 查询申请接口
        /// </summary>
        public string GetWKeywordReplyListData()
        {
            var form = Request("form").DeserializeJSONTo<WKeywordReplyQueryEntity>();

            var wKeywordReplyBLL = new WKeywordReplyBLL(CurrentUserInfo);
            IList<WKeywordReplyEntity> list;
            string content = string.Empty;

            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;

            WKeywordReplyEntity queryEntity = new WKeywordReplyEntity();
            queryEntity.Keyword = FormatParamValue(form.Keyword);
            queryEntity.ModelId = FormatParamValue(form.ModelId);
            queryEntity.ApplicationId = FormatParamValue(form.ApplicationId);

            list = wKeywordReplyBLL.GetWebWKeywordReply(queryEntity, pageIndex, PageSize);
            var dataTotalCount = wKeywordReplyBLL.GetWebWKeywordReplyCount(queryEntity);

            var jsonData = new JsonData();
            jsonData.totalCount = dataTotalCount.ToString();
            jsonData.data = list;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                jsonData.data.ToJSON(),
                jsonData.totalCount);
            return content;
        }
        #endregion

        #region GetWKeywordReplyInfoById
        /// <summary>
        /// 根据ID获取信息
        /// </summary>
        public string GetWKeywordReplyInfoById()
        {
            var service = new WKeywordReplyBLL(CurrentUserInfo);
            WKeywordReplyEntity obj = new WKeywordReplyEntity();
            string content = string.Empty;

            string key = string.Empty;
            if (Request("WKeywordReplyID") != null && Request("WKeywordReplyID") != string.Empty)
            {
                key = Request("WKeywordReplyID").ToString().Trim();
            }

            obj = service.GetByID(key);

            var jsonData = new JsonData();
            jsonData.totalCount = obj == null ? "0" : "1";
            jsonData.data = obj;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region SaveWKeywordReplyData
        /// <summary>
        /// 保存
        /// </summary>
        public string SaveWKeywordReplyData()
        {
            var service = new WKeywordReplyBLL(CurrentUserInfo);
            WKeywordReplyEntity item = new WKeywordReplyEntity();
            string content = string.Empty;
            var responseData = new ResponseData();

            string key = string.Empty;
            string item_id = string.Empty;
            if (Request("item") != null && Request("item") != string.Empty)
            {
                key = Request("item").ToString().Trim();
            }
            if (Request("WKeywordReplyId") != null && Request("WKeywordReplyId") != string.Empty)
            {
                item_id = Request("WKeywordReplyId").ToString().Trim();
            }

            item = key.DeserializeJSONTo<WKeywordReplyEntity>();

            if (item.Keyword == null || item.Keyword.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "关键字不能为空";
                return responseData.ToJSON();
            }

            var isExist = service.IsExistKeyword(item.ApplicationId, item.Keyword, item.ReplyId);
            if (isExist)
            {
                responseData.success = false;
                responseData.msg = "关键字已存在";
                return responseData.ToJSON();
            }

            bool status = true;
            string message = "保存成功";
            if (item.ReplyId.Trim().Length == 0)
            {
                item.ReplyId = Utils.NewGuid();
                service.Create(item);
            }
            else
            {
                item.ReplyId = item_id;
                service.Update(item, false);
            }

            responseData.success = status;
            responseData.msg = message;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region WKeywordReplyDeleteData
        /// <summary>
        /// 删除
        /// </summary>
        public string WKeywordReplyDeleteData()
        {
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

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

            string[] ids = key.Split(',');
            new WKeywordReplyBLL(this.CurrentUserInfo).Delete(new WKeywordReplyEntity()
            {
                ReplyId = key
            });

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region GetWAutoReplyInfoById
        /// <summary>
        /// 根据ID获取信息
        /// </summary>
        public string GetWAutoReplyInfoById()
        {
            var service = new WAutoReplyBLL(CurrentUserInfo);
            WAutoReplyEntity obj = new WAutoReplyEntity();
            string content = string.Empty;

            string key = string.Empty;
            if (Request("WAutoReplyID") != null && Request("WAutoReplyID") != string.Empty)
            {
                key = Request("WAutoReplyID").ToString().Trim();
            }

            obj = service.GetByID(key);

            var jsonData = new JsonData();
            jsonData.totalCount = obj == null ? "0" : "1";
            jsonData.data = obj;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region SaveWAutoReplyData
        /// <summary>
        /// 保存
        /// </summary>
        public string SaveWAutoReplyData()
        {
            var service = new WAutoReplyBLL(CurrentUserInfo);
            WAutoReplyEntity item = new WAutoReplyEntity();
            string content = string.Empty;
            var responseData = new ResponseData();

            string key = string.Empty;
            string item_id = string.Empty;
            if (Request("item") != null && Request("item") != string.Empty)
            {
                key = Request("item").ToString().Trim();
            }
            if (Request("WAutoReplyId") != null && Request("WAutoReplyId") != string.Empty)
            {
                item_id = Request("WAutoReplyId").ToString().Trim();
            }

            item = key.DeserializeJSONTo<WAutoReplyEntity>();

            if (item.ModelId == null || item.ModelId.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "模板不能为空";
                return responseData.ToJSON();
            }
            if (item.ApplicationId == null || item.ApplicationId.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "申请接口不能为空";
                return responseData.ToJSON();
            }

            bool status = true;
            string message = "保存成功";
            item.ReplyId = item_id;
            //if (item.ReplyId == null || item.ReplyId.Trim().Length == 0)
            //{
            //    item.ReplyId = Utils.NewGuid();
            //}
            if (service.GetByID(item.ReplyId) == null)
            {
                service.Create(item);
            }
            else
            {
                service.Update(item, false);
            }

            responseData.success = status;
            responseData.msg = message;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region WMenuPublishData
        /// <summary>
        /// 保存
        /// </summary>
        public string WMenuPublishData()
        {
            var service = new JIT.CPOS.BS.BLL.WX.CommonBLL();
            string content = string.Empty;
            var responseData = new ResponseData();

            string key = string.Empty;
            string item_id = string.Empty;
            if (Request("item") != null && Request("item") != string.Empty)
            {
                key = Request("item").ToString().Trim();
            }
            if (Request("ApplicationId") != null && Request("ApplicationId") != string.Empty)
            {
                item_id = Request("ApplicationId").ToString().Trim();
            }

            if (item_id == null || item_id.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "模板不能为空";
                return responseData.ToJSON();
            }

            try
            {
                var result = service.CreateMenu(CurrentUserInfo, item_id);

                if (result.errcode.Equals("0"))
                {
                    responseData.success = true;
                    responseData.msg = "发布成功";
                }
                else
                {
                    responseData.success = false;
                    responseData.msg = result.errcode + " " + result.errmsg;
                }
            }
            catch (Exception ex)
            {
                responseData.success = false;
                responseData.msg = ex.ToString();
            }

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region GetZCourseInfoById
        /// <summary>
        /// 根据ID获取ZCourse信息
        /// </summary>
        public string GetZCourseInfoById()
        {
            var service = new ZCourseBLL(CurrentUserInfo);
            ZCourseEntity obj = new ZCourseEntity();
            string content = string.Empty;

            string key = string.Empty;
            if (Request("id") != null && Request("id") != string.Empty)
            {
                key = Request("id").ToString().Trim();
            }

            obj = service.GetByID(key);

            var jsonData = new JsonData();
            jsonData.totalCount = obj == null ? "0" : "1";
            jsonData.data = obj;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region GetZCourseNewsData
        public string GetZCourseNewsData()
        {
            //var form = Request("form").DeserializeJSONTo<LNewsQueryEntity>();
            var newsService = new LNewsBLL(CurrentUserInfo);
            var zCourseNewsMappingBLL = new ZCourseNewsMappingBLL(CurrentUserInfo);

            string content = string.Empty;

            string CourseId = FormatParamValue(Request("CourseId"));
            //string NewsTitle = FormatParamValue(form.NewsTitle);
            //string PublishTimeBegin = FormatParamValue(form.PublishTimeBegin);
            //string PublishTimeEnd = FormatParamValue(form.PublishTimeEnd);
            int startRowIndex = Utils.GetIntVal(FormatParamValue(Request("page")));

            var condition = new List<IWhereCondition>();

            if (!CourseId.Equals(string.Empty))
            {
                condition.Add(new EqualsCondition() { FieldName = "CourseId", Value = CourseId });
            }
            //if (!NewsTitle.Equals(string.Empty))
            //{
            //    condition.Add(new LikeCondition() { FieldName = "NewsTitle", Value = NewsTitle, HasLeftFuzzMatching = true, HasRightFuzzMathing = true });
            //}
            //if (!PublishTimeBegin.Equals(string.Empty))
            //{
            //    condition.Add(new MoreThanCondition() { FieldName = "PublishTime", Value = PublishTimeBegin, IncludeEquals = true });
            //}
            //if (!PublishTimeEnd.Equals(string.Empty))
            //{
            //    condition.Add(new LessThanCondition() { FieldName = "PublishTime", Value = PublishTimeEnd, IncludeEquals = true });
            //}

            var orderBy = new OrderBy[]{
                new OrderBy{ FieldName = "CreateTime", Direction=OrderByDirections.Desc }
            };

            //var data = newsService.PagedQueryNews(condition.ToArray(), orderBy, PageSize, startRowIndex);

            var data = new List<LNewsEntity>();
            var mapList = zCourseNewsMappingBLL.QueryByEntity(new ZCourseNewsMappingEntity() { CourseId = CourseId }, orderBy);

            if (mapList != null)
            {
                foreach (var mapItem in mapList)
                {
                    var tmpNews = newsService.GetByID(mapItem.NewsId);
                    data.Add(tmpNews);
                }
            }

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                data.Count);

            return content;
        }

        #endregion

        #region GetNewsById 根据ID获取新闻信息

        /// <summary>
        /// 根据ID获取新闻信息
        /// </summary>
        public string GetNewsById()
        {
            var newsService = new LNewsBLL(CurrentUserInfo);
            string content = string.Empty;

            string key = string.Empty;
            if (FormatParamValue(Request("NewsId")) != null && FormatParamValue(Request("NewsId")) != string.Empty)
            {
                key = FormatParamValue(Request("NewsId")).ToString().Trim();
            }

            var condition = new List<IWhereCondition>();
            if (!key.Equals(string.Empty))
            {
                condition.Add(new EqualsCondition() { FieldName = "NewsId", Value = key });
            }

            LNewsEntity data = new LNewsEntity();
            var news = newsService.Query(condition.ToArray(), null);

            if (news != null && news.Length > 0)
            {
                data = news.ToList().FirstOrDefault();
                data.StrPublishTime = data.PublishTime.Value.ToString("yyyy-MM-dd");
            }

            var jsonData = new JsonData();
            jsonData.totalCount = (data == null) ? "0" : "1";
            jsonData.data = data;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                (data == null) ? "0" : "1");

            return content;
        }

        #endregion

        #region SaveNews 保存新闻信息

        /// <summary>
        /// 保存新闻信息
        /// </summary>
        public string SaveNews()
        {
            var newsService = new LNewsBLL(CurrentUserInfo);
            var zCourseNewsMappingBLL = new ZCourseNewsMappingBLL(CurrentUserInfo);

            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string NewsId = string.Empty;
            string CourseId = string.Empty;
            var news = Request("news");

            if (FormatParamValue(news) != null && FormatParamValue(news) != string.Empty)
            {
                key = FormatParamValue(news).ToString().Trim();
            }
            if (FormatParamValue(Request("NewsId")) != null && FormatParamValue(Request("NewsId")) != string.Empty)
            {
                NewsId = FormatParamValue(Request("NewsId")).ToString().Trim();
            }
            if (FormatParamValue(Request("CourseId")) != null && FormatParamValue(Request("CourseId")) != string.Empty)
            {
                CourseId = FormatParamValue(Request("CourseId")).ToString().Trim();
            }

            var newsEntity = key.DeserializeJSONTo<LNewsEntity>();

            //if (newsEntity.NewsType == null || newsEntity.NewsType.Trim().Length == 0)
            //{
            //    responseData.success = false;
            //    responseData.msg = "新闻类型不能为空";
            //    return responseData.ToJSON();
            //}
            if (newsEntity.NewsTitle == null || newsEntity.NewsTitle.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "新闻标题不能为空";
                return responseData.ToJSON();
            }
            if (newsEntity.PublishTime == null)
            {
                responseData.success = false;
                responseData.msg = "发布时间不能为空";
                return responseData.ToJSON();
            }
            if (newsEntity.Content == null || newsEntity.Content.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "新闻内容不能为空";
                return responseData.ToJSON();
            }

            string host = ConfigurationManager.AppSettings["host"].Trim();
            if (!host.EndsWith("/"))
            {
                host += "/";
            }

            if (NewsId.Trim().Length == 0)
            {
                newsEntity.NewsId = Utils.NewGuid();
                if (newsEntity.ContentUrl.Trim().Length == 0)
                {
                    newsEntity.ContentUrl = host + "newsdetail.aspx?news_id=" + newsEntity.NewsId;
                }
                zCourseNewsMappingBLL.Create(new ZCourseNewsMappingEntity()
                {
                    MappingId = Utils.NewGuid(),
                    NewsId = newsEntity.NewsId,
                    CourseId = CourseId
                });
                newsService.Create(newsEntity);
            }
            else
            {
                newsEntity.NewsId = NewsId;
                if (newsEntity.ContentUrl.Trim().Length == 0)
                {
                    newsEntity.ContentUrl = host + "newsdetail.aspx?news_id=" + newsEntity.NewsId;
                }
                newsService.Update(newsEntity);
            }

            //var mapList = zCourseNewsMappingBLL.QueryByEntity(new ZCourseNewsMappingEntity() { NewsId = NewsId }, null);
            //if (mapList != null && mapList.Length > 0)
            //{
            //    zCourseNewsMappingBLL.Delete(mapList[0]);
            //}


            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region NewsDeleteData 新闻删除

        /// <summary>
        /// 新闻删除
        /// </summary>
        public string NewsDeleteData()
        {
            var zCourseNewsMappingBLL = new ZCourseNewsMappingBLL(CurrentUserInfo);

            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            if (FormatParamValue(Request("ids")) != null && FormatParamValue(Request("ids")) != string.Empty)
            {
                key = FormatParamValue(Request("ids")).ToString().Trim();
            }

            if (key == null || key.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "新闻ID不能为空";
                return responseData.ToJSON();
            }

            string[] ids = key.Split(',');
            new LNewsBLL(CurrentUserInfo).Delete(ids);

            foreach (var id in ids)
            {
                var mapList = zCourseNewsMappingBLL.QueryByEntity(new ZCourseNewsMappingEntity() { NewsId = id }, null);
                if (mapList != null && mapList.Length > 0)
                {
                    zCourseNewsMappingBLL.Delete(mapList[0]);
                }
            }

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region GetZCourseApplyData
        public string GetZCourseApplyData()
        {
            var zCourseApplyBLL = new ZCourseApplyBLL(CurrentUserInfo);

            string content = string.Empty;

            string CourseId = FormatParamValue(Request("CourseId"));
            int page = Utils.GetIntVal(FormatParamValue(Request("page")));

            var totalCount = 0;
            ZCourseApplyEntity queryEntity = new ZCourseApplyEntity();
            queryEntity.CouseId = CourseId;
            var data = zCourseApplyBLL.GetList(queryEntity, page, PageSize);
            totalCount = zCourseApplyBLL.GetListCount(queryEntity);

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                totalCount);

            return content;
        }
        #endregion

        #region GetZCourseReflectionsData
        public string GetZCourseReflectionsData()
        {
            var zCourseReflectionsBLL = new ZCourseReflectionsBLL(CurrentUserInfo);

            string content = string.Empty;

            string CourseId = FormatParamValue(Request("CourseId"));
            int page = Utils.GetIntVal(FormatParamValue(Request("page")));

            var totalCount = 0;
            ZCourseReflectionsEntity queryEntity = new ZCourseReflectionsEntity();
            queryEntity.CourseId = CourseId;
            var data = zCourseReflectionsBLL.GetList(queryEntity, page - 1, PageSize);
            totalCount = zCourseReflectionsBLL.GetListCount(queryEntity);

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                totalCount);

            return content;
        }
        #endregion

        #region GetCourseReflectionsById
        public string GetCourseReflectionsById()
        {
            var zCourseReflectionsBLL = new ZCourseReflectionsBLL(CurrentUserInfo);
            string content = string.Empty;

            string key = string.Empty;
            if (FormatParamValue(Request("ReflectionsId")) != null && FormatParamValue(Request("ReflectionsId")) != string.Empty)
            {
                key = FormatParamValue(Request("ReflectionsId")).ToString().Trim();
            }

            var data = zCourseReflectionsBLL.GetByID(key);

            var jsonData = new JsonData();
            jsonData.totalCount = (data == null) ? "0" : "1";
            jsonData.data = data;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                (data == null) ? "0" : "1");

            return content;
        }
        #endregion

        #region SaveCourseReflections
        public string SaveCourseReflections()
        {
            var service = new ZCourseReflectionsBLL(CurrentUserInfo);

            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string ReflectionsId = string.Empty;
            string CourseId = string.Empty;
            var news = Request("news");

            if (FormatParamValue(news) != null && FormatParamValue(news) != string.Empty)
            {
                key = FormatParamValue(news).ToString().Trim();
            }
            if (FormatParamValue(Request("ReflectionsId")) != null && FormatParamValue(Request("ReflectionsId")) != string.Empty)
            {
                ReflectionsId = FormatParamValue(Request("ReflectionsId")).ToString().Trim();
            }
            if (FormatParamValue(Request("CourseId")) != null && FormatParamValue(Request("CourseId")) != string.Empty)
            {
                CourseId = FormatParamValue(Request("CourseId")).ToString().Trim();
            }

            var newsEntity = key.DeserializeJSONTo<ZCourseReflectionsEntity>();

            if (newsEntity.StudentName == null || newsEntity.StudentName.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "学员名称不能为空";
                return responseData.ToJSON();
            }
            if (newsEntity.Content == null || newsEntity.Content.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "感言不能为空";
                return responseData.ToJSON();
            }


            if (ReflectionsId.Trim().Length == 0)
            {
                newsEntity.ReflectionsId = Utils.NewGuid();
                service.Create(newsEntity);
            }
            else
            {
                service.Update(newsEntity, false);
            }

            //Jermyn20131015 处理图片
            if (newsEntity != null && newsEntity.ImageURL != null && !newsEntity.ImageURL.Equals(""))
            {
                ObjectImagesBLL objectServer = new ObjectImagesBLL(CurrentUserInfo);
                var imageObj = objectServer.GetObjectImagesByObjectId(newsEntity.ReflectionsId);
                if (imageObj == null || imageObj.Count == 0)
                {
                    ObjectImagesEntity objectInfo = new ObjectImagesEntity();
                    objectInfo.ImageId = Utils.NewGuid();
                    objectInfo.ObjectId = newsEntity.ReflectionsId;
                    objectInfo.ImageURL = newsEntity.ImageURL;
                    objectInfo.CustomerId = this.CurrentUserInfo.CurrentUser.customer_id;
                    objectServer.Create(objectInfo);
                }
                else
                {
                    var objectInfo = imageObj[0];
                    objectInfo.ImageURL = newsEntity.ImageURL;
                    objectInfo.CustomerId = this.CurrentUserInfo.CurrentUser.customer_id;
                    objectServer.Update(objectInfo);
                }
            }
            /////////////////////////////////////////////////////
            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region CourseReflectionsDeleteData
        public string CourseReflectionsDeleteData()
        {
            var zCourseReflectionsBLL = new ZCourseReflectionsBLL(CurrentUserInfo);

            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

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

            string[] ids = key.Split(',');
            new ZCourseReflectionsBLL(CurrentUserInfo).Delete(ids);

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region SaveCourse
        public string SaveCourse()
        {
            var service = new ZCourseBLL(CurrentUserInfo);

            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string CourseId = string.Empty;
            var item = Request("item");

            if (FormatParamValue(item) != null && FormatParamValue(item) != string.Empty)
            {
                key = FormatParamValue(item).ToString().Trim();
            }
            if (FormatParamValue(Request("CourseId")) != null && FormatParamValue(Request("CourseId")) != string.Empty)
            {
                CourseId = FormatParamValue(Request("CourseId")).ToString().Trim();
            }

            var itemEntity = key.DeserializeJSONTo<ZCourseEntity>();

            //if (itemEntity.CourseId == null || itemEntity.CourseId.Trim().Length == 0)
            //{
            //    responseData.success = false;
            //    responseData.msg = "ID不能为空";
            //    return responseData.ToJSON();
            //}


            if (CourseId.Trim().Length == 0)
            {
                itemEntity.CourseId = Utils.NewGuid();
                service.Create(itemEntity);
            }
            else
            {
                service.Update(itemEntity, false);
            }

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region GetWModelListData
        /// <summary>
        /// 查询申请接口
        /// </summary>
        public string GetWModelListData()
        {
            var form = Request("form").DeserializeJSONTo<WModelListQueryEntity>();

            var wModelBLL = new WModelBLL(CurrentUserInfo);
            IList<WModelEntity> list;
            string content = string.Empty;

            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;

            WModelEntity queryEntity = new WModelEntity();
            queryEntity.ModelName = FormatParamValue(form.ModelName);
            queryEntity.ModelId = FormatParamValue(form.ModelId);
            queryEntity.ApplicationId = FormatParamValue(form.ApplicationId);
            queryEntity.CustomerId = CurrentUserInfo.CurrentUser.customer_id;

            list = wModelBLL.GetList(queryEntity, pageIndex, PageSize);
            var dataTotalCount = wModelBLL.GetListCount(queryEntity);

            var jsonData = new JsonData();
            jsonData.totalCount = dataTotalCount.ToString();
            jsonData.data = list;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                jsonData.data.ToJSON(),
                jsonData.totalCount);
            return content;
        }
        #endregion

        #region GetWModelListInfoById
        /// <summary>
        /// 根据ID获取信息
        /// </summary>
        public string GetWModelListInfoById()
        {
            var service = new WModelBLL(CurrentUserInfo);
            var wQRCodeTypeModelMappingBLL = new WQRCodeTypeModelMappingBLL(CurrentUserInfo);
            WModelEntity obj = new WModelEntity();
            string content = string.Empty;

            string key = string.Empty;
            if (Request("ModelId") != null && Request("ModelId") != string.Empty)
            {
                key = Request("ModelId").ToString().Trim();
            }

            obj = service.GetByID(key);

            if (obj != null && obj.QRCodeTypeId == null)
            {
                var mappingList = wQRCodeTypeModelMappingBLL.QueryByEntity(new WQRCodeTypeModelMappingEntity()
                {
                    ModelId = obj.ModelId
                }, null);
                if (mappingList != null && mappingList.Length > 0)
                {
                    obj.QRCodeTypeId = mappingList[0].QRCodeTypeId.ToString();
                }
            }

            var jsonData = new JsonData();
            jsonData.totalCount = obj == null ? "0" : "1";
            jsonData.data = obj;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region SaveWModelListData
        /// <summary>
        /// 保存
        /// </summary>
        public string SaveWModelListData()
        {
            var service = new WModelBLL(CurrentUserInfo);
            var wQRCodeTypeModelMappingBLL = new WQRCodeTypeModelMappingBLL(CurrentUserInfo);
            WModelEntity item = new WModelEntity();
            string content = string.Empty;
            var responseData = new ResponseData();

            string key = string.Empty;
            string item_id = string.Empty;
            if (Request("item") != null && Request("item") != string.Empty)
            {
                key = Request("item").ToString().Trim();
            }
            if (Request("ModelId") != null && Request("ModelId") != string.Empty)
            {
                item_id = Request("ModelId").ToString().Trim();
            }

            item = key.DeserializeJSONTo<WModelEntity>();

            if (item.ModelName == null || item.ModelName.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "名称不能为空";
                return responseData.ToJSON();
            }

            var isExist = service.IsExist(item.ApplicationId, item.ModelName, item.ModelId);
            if (isExist)
            {
                responseData.success = false;
                responseData.msg = "名称已存在";
                return responseData.ToJSON();
            }

            bool status = true;
            string message = "保存成功";
            if (item.ModelId.Trim().Length == 0)
            {
                item.ModelId = Utils.NewGuid();
                item.CustomerId = CurrentUserInfo.CurrentUser.customer_id;
                service.Create(item);
            }
            else
            {
                item.ModelId = item_id;
                service.Update(item, false);
            }

            responseData.success = status;
            responseData.msg = message;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region WModelListDeleteData
        /// <summary>
        /// 删除
        /// </summary>
        public string WModelListDeleteData()
        {
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

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
            if (checkdlete(key))
            {
                responseData.success = false;
                responseData.msg = "模板已被引用不能删除";
                return responseData.ToJSON();
            }
            string[] ids = key.Split(',');
            new WModelBLL(this.CurrentUserInfo).Delete(new WModelEntity()
            {
                ModelId = key
            });

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region checkdlete
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool checkdlete(string key)
        {
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            bool bl = new WMenuBLL(this.CurrentUserInfo).CheckDelete("'" + key + "'");
            return bl;

        }
        #endregion

        #region MyRegion
        public string RemoveSessionById()
        {
            var responseData = new ResponseData();
            try
            {
                WApplicationInterfaceBLL server = new WApplicationInterfaceBLL(CurrentUserInfo);
                string ApplicationId = string.Empty;
                if (Request("ApplicationId") != null && Request("ApplicationId") != string.Empty)
                {
                    ApplicationId = Request("ApplicationId").ToString().Trim();
                }
                server.RemoveSession(ApplicationId);
                responseData.success = true;

            }
            catch (Exception)
            {
                responseData.success = false;

            }
            return responseData.ToJSON();
        }
        #endregion

        #region Bulkimport
        public string Bulkimport()
        {
            var responseData = new ResponseData();
            responseData.success = false;
            responseData.msg = "批量导入失败";
            try
            {
                string appId = string.Empty;
                string appSecret = string.Empty;
                string weixinId = string.Empty;
                if (Request("appId") != null && Request("appId") != string.Empty)
                {
                    appId = Request("appId").ToString().Trim();
                }
                if (Request("appSecret") != null && Request("appSecret") != string.Empty)
                {
                    appSecret = Request("appSecret").ToString().Trim();
                }
                if (Request("weixinId") != null && Request("weixinId") != string.Empty)
                {
                    weixinId = Request("weixinId").ToString().Trim();
                }
                VipBLL Vip = new VipBLL(this.CurrentUserInfo);
                string VipCode = Vip.GetMaxVipCode();
                JIT.CPOS.BS.BLL.WX.CommonBLL bll = new BLL.WX.CommonBLL();
                int Length = Convert.ToInt32(VipCode.Replace("Vip", ""));
                bool bl = bll.NewImportUserInfo(appId, appSecret, weixinId, this.CurrentUserInfo, Length);
                if (bl == true)
                {
                    responseData.success = true;
                    responseData.msg = "批量导入成功";
                }
            }
            catch (Exception)
            {
                responseData.success = false;
                responseData.msg = "批量导入失败";
                return responseData.ToJSON();
            }

            return responseData.ToJSON();
        }
        #endregion

        #region 导入和更新公众号会员信息
        public string ImportWXUser()
        {
            var appInterfaceBLL = new WApplicationInterfaceBLL(this.CurrentUserInfo);
            VipBLL Vip = new VipBLL(this.CurrentUserInfo);
            JIT.CPOS.BS.BLL.WX.CommonBLL bll = new BLL.WX.CommonBLL();

            var responseData = new ResponseData();
            responseData.success = false;
            responseData.msg = "批量导入失败";
            try
            {
                string appId = string.Empty;
                string appSecret = string.Empty;
                string weixinId = string.Empty;
                bool bl = false;

                var appInterfaceList = appInterfaceBLL.QueryByEntity(new WApplicationInterfaceEntity() { CustomerId = CurrentUserInfo.ClientID }, null);

                foreach (var item in appInterfaceList)
                {
                    string VipCode = Vip.GetMaxVipCode();
                    int Length = Convert.ToInt32(VipCode.Replace("Vip", ""));
                    bl = bll.ImportUserInfo(item.AppID, item.AppSecret, item.WeiXinID, this.CurrentUserInfo, Length, appInterfaceList.Count());
                }

                if (bl == true)
                {
                    responseData.success = true;
                    responseData.msg = "批量导入成功";
                }
            }
            catch (Exception)
            {
                responseData.success = false;
                responseData.msg = "批量导入失败";
                return responseData.ToJSON();
            }

            return responseData.ToJSON();
        }
        #endregion

    }

    #region QueryEntity
    public class WApplicationQueryEntity
    {
        public string WeiXinName;
        public string WeiXinID;
    }
    public class WMenuQueryEntity
    {
        public string Name;
        public string Key;
        public string Type;
        public string Level;
        public string WeiXinID;
    }
    public class WKeywordReplyQueryEntity
    {
        public string Keyword;
        public string ModelId;
        public string ApplicationId;
    }
    public class WModelListQueryEntity
    {
        public string ModelName;
        public string ModelId;
        public string ApplicationId;
    }
    #endregion

}