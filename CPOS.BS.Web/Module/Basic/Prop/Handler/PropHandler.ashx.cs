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

namespace JIT.CPOS.BS.Web.Module.Basic.Prop.Handler
{
    /// <summary>
    /// PropHandler
    /// </summary>
    public class PropHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
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
                case "search_prop":
                    content = GetPropListData();
                    break;
                case "search_prop_tree":
                    content = GetPropTreeListData();
                    break;
                case "get_prop_by_id":
                    content = GetPropInfoById();
                    break;
                case "prop_save":
                    content = SavePropData();
                    break;
                case "prop_delete":
                    content = PropDeleteData();
                    break;
                case "search_BrandDetail":
                    content = GetBrandDetailListData();
                    break;
                case "get_BrandDetail_by_id":
                    content = GetBrandDetailInfoById();
                    break;
                case "get_item_image_info_by_item_id":
                    content = GetItemImageInfoByItemIdData();
                    break;
                case "save_BrandDetail":
                    content = SaveBrandDetailData();
                    break;
                case "BrandDetail_delete":
                    content = BrandDetailDeleteData();
                    break;
                case "createIndex":
                    content = CreateIndex(pContext);
                    break;
                  
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetPropListData
        /// <summary>
        /// 查询属性
        /// </summary>
        public string GetPropListData()
        {
            var form = Request("form").DeserializeJSONTo<PropQueryEntity>();

            var propService = new PropService(CurrentUserInfo);
            IList<PropInfo> list;
            string content = string.Empty;

            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;

            PropInfo queryEntity = new PropInfo();
            queryEntity.Prop_Name = FormatParamValue(form.prop_name);
            queryEntity.Prop_Domain = FormatParamValue(Request("ApplicationId"));
            if (Request("parentId") != null && Request("parentId").Length > 0)// && Request("parentId") != "ALL")
            {
                queryEntity.Parent_Prop_id = FormatParamValue(Request("parentId"));
            }
            else
            {
                queryEntity.Parent_Prop_id = "-99";
            }

            //(jifeng.cao 20140220)
            if (Request("propType") != null && Request("propType").Length > 0)
            {
                //如果是遍历SKU属性,则根据客户标识获取数据
                if (FormatParamValue(Request("propType")) == "1" && queryEntity.Prop_Domain == "SKU")
                {
                    queryEntity.CustomerId = this.CurrentUserInfo.CurrentLoggingManager.Customer_Id;
                }
                else
                {
                    queryEntity.CustomerId = null;
                }
            }

            list = propService.GetWebProp(queryEntity, pageIndex, PageSize);
            var dataTotalCount = propService.GetWebPropCount(queryEntity);

            var jsonData = new JsonData();
            jsonData.totalCount = dataTotalCount.ToString();
            jsonData.data = list;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                jsonData.data.ToJSON(),
                jsonData.totalCount);
            return content;
        }
        #endregion

        #region GetPropTreeListData
        /// <summary>
        /// 查询属性Tree
        /// </summary>
        public string GetPropTreeListData()
        {
            var propService = new PropService(CurrentUserInfo);
            IList<PropInfo> list;
            PropInfo queryEntity = new PropInfo();
            queryEntity.Parent_Prop_id = "-99";
            queryEntity.Prop_Domain = FormatParamValue(Request("ApplicationId"));
            if (queryEntity.Prop_Domain != null && queryEntity.Prop_Domain.Trim().ToUpper()=="SKU")
            {
                //查询sku的tree
                queryEntity.Parent_Prop_id = "-88";
                queryEntity.CustomerId = CurrentUserInfo.ClientID;
            }
            
            list = propService.GetWebProp(queryEntity, 0, 1000);
            StringBuilder sb = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                sb.Append("{\"expanded\": true, \"text\":\"属性\", \"children\": [");
                foreach (var item in list)
                {
                    //sb.Append("{\"expanded\": true, \"leaf\":true, ");
                    sb.Append("{\"expanded\": true, ");
                    sb.Append("\"ID\":\"" + item.Prop_Id + "\",");
                    sb.Append("\"ParentId\":\"" + item.Parent_Prop_id + "\",");
                    sb.Append("\"prop_id\":\"" + item.Prop_Id + "\",");
                    sb.Append("\"prop_code\":\"" + item.Prop_Code + "\",");
                    sb.Append("\"prop_name\":\"" + item.Prop_Name + "\",");
                    sb.Append("\"prop_eng_name\":\"" + item.Prop_Eng_Name + "\",");
                    sb.Append("\"prop_type\":\"" + item.Prop_Type + "\",");
                    sb.Append("\"parent_prop_id\":\"" + item.Parent_Prop_id + "\",");
                    sb.Append("\"prop_level\":\"" + item.Prop_Level + "\",");
                    sb.Append("\"prop_domain\":\"" + item.Prop_Domain + "\",");
                    sb.Append("\"prop_input_flag\":\"" + item.Prop_Input_Flag + "\",");
                    sb.Append("\"prop_max_length\":\"" + item.Prop_Max_Length + "\",");
                    sb.Append("\"prop_default_value\":\"" + item.Prop_Default_Value + "\",");
                    sb.Append("\"prop_status\":\"" + item.Prop_Status + "\",");
                    sb.Append("\"display_index\":\"" + item.Display_Index + "\",");
                    sb.Append("\"create_user_id\":\"" + item.Create_User_Id + "\",");
                    sb.Append("\"create_time\":\"" + item.Create_Time + "\",");
                    sb.Append("\"modify_user_id\":\"" + item.Modify_User_Id + "\",");
                    sb.Append("\"modify_time\":\"" + item.Modify_Time + "\",");
                    //提取子属性
                    GetPropTreeChildrenJson(propService, item.Prop_Id, sb, item.Prop_Type);
                    sb.Append("},");
                }
                sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("]}");
            return sb.ToString();
        }
        public void GetPropTreeChildrenJson(PropService service, string parentId, StringBuilder sb, string propType)
        {
            IList<PropInfo> list;
            PropInfo queryEntity = new PropInfo();
            queryEntity.Parent_Prop_id = parentId;

            //对应属性域(jifeng.cao 20140220)
            queryEntity.Prop_Domain = FormatParamValue(Request("ApplicationId"));
            //如果是遍历SKU属性,则根据客户标识获取数据
            if (propType == "1" && queryEntity.Prop_Domain == "SKU")
            {
                queryEntity.CustomerId = this.CurrentUserInfo.CurrentLoggingManager.Customer_Id;
            }
            else
            {
                queryEntity.CustomerId = null;
            }

            list = service.GetWebProp(queryEntity, 0, 1000);
            sb.Append("\"leaf\":false,\"expanded\": true, \"children\":[");
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    sb.Append("{");
                    sb.Append("\"ID\":\"" + item.Prop_Id + "\",");
                    sb.Append("\"ParentId\":\"" + item.Parent_Prop_id + "\",");
                    sb.Append("\"prop_id\":\"" + item.Prop_Id + "\",");
                    sb.Append("\"prop_code\":\"" + item.Prop_Code + "\",");
                    sb.Append("\"prop_name\":\"" + item.Prop_Name + "\",");
                    sb.Append("\"prop_eng_name\":\"" + item.Prop_Eng_Name + "\",");
                    sb.Append("\"prop_type\":\"" + item.Prop_Type + "\",");
                    sb.Append("\"parent_prop_id\":\"" + item.Parent_Prop_id + "\",");
                    sb.Append("\"prop_level\":\"" + item.Prop_Level + "\",");
                    sb.Append("\"prop_domain\":\"" + item.Prop_Domain + "\",");
                    sb.Append("\"prop_input_flag\":\"" + item.Prop_Input_Flag + "\",");
                    sb.Append("\"prop_max_length\":\"" + item.Prop_Max_Length + "\",");
                    sb.Append("\"prop_default_value\":\"" + item.Prop_Default_Value + "\",");
                    sb.Append("\"prop_status\":\"" + item.Prop_Status + "\",");
                    sb.Append("\"display_index\":\"" + item.Display_Index + "\",");
                    sb.Append("\"create_user_id\":\"" + item.Create_User_Id + "\",");
                    sb.Append("\"create_time\":\"" + item.Create_Time + "\",");
                    sb.Append("\"modify_user_id\":\"" + item.Modify_User_Id + "\",");
                    sb.Append("\"modify_time\":\"" + item.Modify_Time + "\",");
                    //提取子属性
                    GetPropTreeChildrenJson(service, item.Prop_Id, sb, item.Prop_Type);
                    sb.Append("},");
                }
                sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("]");
        }
        #endregion

        #region GetPropInfoById
        /// <summary>
        /// 根据属性ID获取属性信息
        /// </summary>
        public string GetPropInfoById()
        {
            var service = new PropService(CurrentUserInfo);
            PropInfo obj = new PropInfo();
            string content = string.Empty;

            string key = string.Empty;
            if (Request("PropID") != null && Request("PropID") != string.Empty)
            {
                key = Request("PropID").ToString().Trim();
            }

            obj = service.GetPropInfoById(key);
            var SKUDomain = "SKU";
            if (obj != null)
            {
                obj.Children = service.GetPropListByParentId(SKUDomain, obj.Prop_Id);
            }

            var jsonData = new JsonData();
            jsonData.totalCount = obj == null ? "0" : "1";
            jsonData.data = obj;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion
        /// <summary>
        ///获取生成的序号
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        public string CreateIndex(HttpContext pContext)
        {
            var service = new PropService(CurrentUserInfo);

            int index = service.CreateIndex(pContext.Request["partentPropID"].ToString(), pContext.Request["propID"].ToString());
            return string.Format("{{\"displayIndex\":'{0}'}}", index);

        }

        #region SavePropData
        /// <summary>
        /// 保存属性
        /// </summary>
        public string SavePropData()
        {
            var service = new PropService(CurrentUserInfo);
            var brandDetailBLL = new BrandDetailBLL(CurrentUserInfo);
            var skuPropServer = new SkuPropServer(this.CurrentUserInfo);
            PropInfo item = new PropInfo();
            string content = string.Empty;
            var responseData = new ResponseData();

            string key = string.Empty;
            string item_id = string.Empty;
            if (Request("item") != null && Request("item") != string.Empty)
            {
                key = Request("item").ToString().Trim();
            }
            if (Request("PropId") != null && Request("PropId") != string.Empty)
            {
                item_id = Request("PropId").ToString().Trim();
            }

            item = key.DeserializeJSONTo<PropInfo>();

            PropInfo parentObj = new PropInfo();
            parentObj.Prop_Level = 1;
            if (item.Parent_Prop_id != null && item.Parent_Prop_id.Trim().Length > 0)
            {
                parentObj = service.GetPropInfoById(item.Parent_Prop_id);
                if (parentObj != null)
                {
                    item.Prop_Level = parentObj.Prop_Level + 1;
                }
            }
            else
            {
                item.Parent_Prop_id = "-99";
                item.Prop_Status = 1;
            }

            if (item.Prop_Domain == "SKU" && item.Prop_Level == 2)
            {
                if (service.CheckSkuLast(item.Parent_Prop_id))
                {
                    responseData.success = false;
                    responseData.msg = "SKU组最多有5个子节点";
                    return responseData.ToJSON();
                }
            }

            if (item.Prop_Name == null || item.Prop_Name.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "名称不能为空";
                return responseData.ToJSON();
            }
            if (item.Prop_Code == null || item.Prop_Code.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "代码不能为空";
                return responseData.ToJSON();
            }


            #region 商品sku 属性时判断  Display_Index只能在1-5，且不能在
            if (item.Prop_Type == "2" && item.Prop_Domain == "SKU")
            {
                string skuMsg = string.Empty;
                if (item.Display_Index < 1 || item.Display_Index > 5)
                {
                    skuMsg = "序号必须在1~5范围内";
                }

                //是否存在sku

                if (skuPropServer.CheckSkuPropByDisplayindex(this.CurrentUserInfo.CurrentLoggingManager.Customer_Id, item.Display_Index))
                {
                    skuMsg = "序号被占用,请重新选择1~5的序号。";
                }
                if (!string.IsNullOrWhiteSpace(skuMsg))
                {
                    responseData.msg = skuMsg;
                    return responseData.ToJSON();
                }
            }
            #endregion


            bool status = true;
            string message = "保存成功";
            //------------------------------------------------
            //if (item.Prop_Domain.Equals("SKU") && item.Prop_Level.Equals("3"))
            //{
            //    item.Prop_Domain = "ITEM";
            //}
            //-----------------------------------------------------------------
            item.Prop_Status = 1;
            if (item.Prop_Id.Trim().Length == 0)
            {
                item.Prop_Id = Utils.NewGuid();
                service.SaveProp(item, ref message);
            }
            else
            {
                service.SaveProp(item, ref message);
            }

            if (message != "属性代码已存在")
            {
                if (item.Prop_Type == "2" && item.Prop_Domain == "SKU")
                {
                    string skuMsg = string.Empty;
                    if (item.Display_Index<1&&item.Display_Index>5)
                    {
                        skuMsg = "序号必须在1~5范围内";   
                    }

                    //是否存在sku

                    if (skuPropServer.CheckSkuPropByDisplayindex(this.CurrentUserInfo.CurrentLoggingManager.Customer_Id,item.Display_Index))
                    {
                        skuMsg = "序号被占用,请重新选择1~5的序号。";   
                    }
                    if (!string.IsNullOrWhiteSpace(skuMsg))
                    {
                        responseData.msg = skuMsg;
                        return responseData.ToJSON();
                    }

                    //如果不存在属性关系
                    if (!skuPropServer.CheckSkuProp(item.Prop_Id))
                    {
                        SkuPropInfo skuPropInfo = new SkuPropInfo();
                        skuPropInfo.sku_prop_id = Utils.NewGuid();
                        skuPropInfo.prop_id = item.Prop_Id;
                        skuPropInfo.CustomerId = this.CurrentUserInfo.CurrentLoggingManager.Customer_Id;
                        skuPropInfo.status = item.Prop_Status.ToString();
                        skuPropInfo.display_index = item.Display_Index;
                        skuPropServer.AddSkuProp(skuPropInfo);
                    }
                }
                else
                {
                    SkuPropInfo skuPropInfo = new SkuPropInfo();
                    skuPropInfo.prop_id = item.Prop_Id;
                    skuPropServer.DeleteSkuProp(skuPropInfo);
                }
            }
            else
            {
                status = false;
            }



            if (parentObj != null && (parentObj.Prop_Code == "品牌" || parentObj.Prop_Id == "F8823C2EBACF4965BA134D3B10BD0B9F"))
            {
                brandDetailBLL.SetBrandAndPropSyn(item.Prop_Id, item.Prop_Name, 2, 0, out message);
            }

            responseData.success = status;
            responseData.msg = message;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region PropDeleteData
        /// <summary>
        /// 删除
        /// </summary>
        public string PropDeleteData()
        {
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();
            var service = new PropService(this.CurrentUserInfo);
            var brandDetailBLL = new BrandDetailBLL(this.CurrentUserInfo);
            var skuPropServer = new SkuPropServer(this.CurrentUserInfo);

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

            if (ids.Length > 0)
            {
                foreach (var id in ids)
                {
                    if (skuPropServer.ISCheckSkuProp(id))
                    {
                        responseData.success = false;
                        responseData.msg = "属性已被引用";
                        return responseData.ToJSON();


                    }

                }
            }
            if (ids.Length > 0)
            {
                foreach (var id in ids)
                {
                    var propObj = new PropInfo();
                    propObj.Prop_Id = id;

                    var tmpPropObj = service.GetPropInfoById(id);
                    PropInfo parentObj = null;
                    if (tmpPropObj.Parent_Prop_id != null && tmpPropObj.Parent_Prop_id.Trim().Length > 0)
                    {
                        parentObj = service.GetPropInfoById(tmpPropObj.Parent_Prop_id);
                    }
                    if (parentObj != null && (parentObj.Prop_Code == "品牌" || parentObj.Prop_Id == "F8823C2EBACF4965BA134D3B10BD0B9F"))
                    {
                        brandDetailBLL.SetBrandAndPropSyn(propObj.Prop_Id, propObj.Prop_Name, 2, 1, out error);
                    }



                    service.DeleteProp(propObj);

                    SkuPropInfo skuPropInfo = new SkuPropInfo();
                    skuPropInfo.prop_id = propObj.Prop_Id;
                    skuPropServer.DeleteSkuProp(skuPropInfo);
                }
            }

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region GetBrandDetailListData
        /// <summary>
        /// 查询属性
        /// </summary>
        public string GetBrandDetailListData()
        {
            var form = Request("form").DeserializeJSONTo<BrandDetailQueryEntity>();

            var service = new BrandDetailBLL(CurrentUserInfo);
            IList<BrandDetailEntity> list;
            string content = string.Empty;

            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;

            BrandDetailEntity queryEntity = new BrandDetailEntity();
            queryEntity.BrandName = FormatParamValue(form.BrandName);
            queryEntity.BrandCode = FormatParamValue(form.BrandCode);
            //if (Request("parentId") != null && Request("parentId").Length > 0)// && Request("parentId") != "ALL")
            //{
            //    queryEntity.Parent_Prop_id = FormatParamValue(Request("parentId"));
            //}
            //else
            //{
            //    queryEntity.Parent_Prop_id = "-99";
            //}

            list = service.GetWebBrandDetail(queryEntity, pageIndex, PageSize);
            var dataTotalCount = service.GetWebBrandDetailCount(queryEntity);

            var jsonData = new JsonData();
            jsonData.totalCount = dataTotalCount.ToString();
            jsonData.data = list;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                jsonData.data.ToJSON(),
                jsonData.totalCount);
            return content;
        }
        #endregion

        #region GetBrandDetailInfoById
        /// <summary>
        /// 根据属性ID获取属性信息
        /// </summary>
        public string GetBrandDetailInfoById()
        {
            var service = new BrandDetailBLL(CurrentUserInfo);
            BrandDetailEntity obj = new BrandDetailEntity();
            string content = string.Empty;

            string key = string.Empty;
            if (Request("item_id") != null && Request("item_id") != string.Empty)
            {
                key = Request("item_id").ToString().Trim();
            }

            obj = service.GetByID(key);

            var jsonData = new JsonData();
            jsonData.totalCount = obj == null ? "0" : "1";
            jsonData.data = obj;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region GetItemImageInfoByItemIdData
        /// <summary>
        /// 通过商品ID获取商品图片信息
        /// </summary>
        public string GetItemImageInfoByItemIdData()
        {
            var imageService = new ObjectImagesBLL(CurrentUserInfo);
            IList<ObjectImagesEntity> data = new List<ObjectImagesEntity>();
            string content = string.Empty;

            string key = string.Empty;
            if (Request("item_id") != null && Request("item_id") != string.Empty)
            {
                key = Request("item_id").ToString().Trim();
            }

            var itemObj = imageService.QueryByEntity(new ObjectImagesEntity() { ObjectId = key }, null);
            if (itemObj != null && itemObj.Length > 0)
            {
                data = itemObj.OrderBy(item => item.DisplayIndex).ToList();
            }

            var jsonData = new JsonData();
            jsonData.totalCount = data.Count.ToString();
            jsonData.data = data;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region SaveBrandDetailData
        /// <summary>
        /// 保存
        /// </summary>
        public string SaveBrandDetailData()
        {
            var service = new BrandDetailBLL(CurrentUserInfo);
            var objectImagesBLL = new ObjectImagesBLL(CurrentUserInfo);
            var propService = new PropService(CurrentUserInfo);
            BrandDetailEntity obj = new BrandDetailEntity();
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string item_id = string.Empty;
            if (Request("item") != null && Request("item") != string.Empty)
            {
                key = Request("item").ToString().Trim();
            }
            if (Request("item_id") != null && Request("item_id") != string.Empty)
            {
                item_id = Request("item_id").ToString().Trim();
            }

            obj = key.DeserializeJSONTo<BrandDetailEntity>();

            if (obj.BrandName == null || obj.BrandName.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "名称不能为空";
                return responseData.ToJSON();
            }
            if (obj.BrandCode == null || obj.BrandCode.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "代码不能为空";
                return responseData.ToJSON();
            }

            if (item_id.Trim().Length == 0)
            {
                obj.BrandId = Utils.NewGuid();
                obj.CustomerId = this.CurrentUserInfo.CurrentUser.customer_id;
                service.Create(obj);

                var tmpImageList = objectImagesBLL.QueryByEntity(new ObjectImagesEntity() { ObjectId = obj.BrandId }, null);
                if (tmpImageList != null && tmpImageList.Length > 0)
                {
                    foreach (var tmpImageItem in tmpImageList)
                    {
                        objectImagesBLL.Delete(tmpImageItem);
                    }
                }
                if (obj.ItemImageList != null)
                {
                    foreach (var tmpImageItem in obj.ItemImageList)
                    {
                        tmpImageItem.ObjectId = obj.BrandId;
                        tmpImageItem.CustomerId = this.CurrentUserInfo.CurrentUser.customer_id;
                        objectImagesBLL.Create(tmpImageItem);
                    }
                }

                service.SetBrandAndPropSyn(obj.BrandId, obj.BrandName, 1, 0, out error);
            }
            else
            {
                obj.BrandId = item_id;
                obj.CustomerId = CurrentUserInfo.CurrentUser.customer_id;
                service.Update(obj, false);

                var tmpImageList = objectImagesBLL.QueryByEntity(
                    new ObjectImagesEntity() { ObjectId = obj.BrandId }, null);
                if (tmpImageList != null && tmpImageList.Length > 0)
                {
                    foreach (var tmpImageItem in tmpImageList)
                    {
                        objectImagesBLL.Delete(tmpImageItem);
                    }
                }
                if (obj.ItemImageList != null)
                {
                    foreach (var tmpImageItem in obj.ItemImageList)
                    {
                        tmpImageItem.ObjectId = obj.BrandId;
                        tmpImageItem.CustomerId = this.CurrentUserInfo.CurrentUser.customer_id;
                        objectImagesBLL.Create(tmpImageItem);
                    }
                }

                service.SetBrandAndPropSyn(obj.BrandId, obj.BrandName, 1, 0, out error);
            }

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region BrandDetailDeleteData
        /// <summary>
        /// 删除
        /// </summary>
        public string BrandDetailDeleteData()
        {
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();
            var service = new BrandDetailBLL(this.CurrentUserInfo);
            var propService = new PropService(this.CurrentUserInfo);

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
            if (ids.Length > 0)
            {
                foreach (var id in ids)
                {
                    var propObj = new BrandDetailEntity();
                    propObj.BrandId = id;
                    service.Delete(propObj);
                    //propService.DeleteProp(new PropInfo() { Prop_Id = id });

                    service.SetBrandAndPropSyn(propObj.BrandId, propObj.BrandName, 1, 1, out error);
                }
            }

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

    }

    #region QueryEntity
    public class PropQueryEntity
    {
        public string parent_prop_id;
        public string prop_name;
        public string prop_code;
        public string prop_type;
        public string prop_domain;
    }
    public class BrandDetailQueryEntity
    {
        public string BrandName;
        public string BrandCode;
    }
    #endregion

}