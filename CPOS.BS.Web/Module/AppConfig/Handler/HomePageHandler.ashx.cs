using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Common;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using JIT.CPOS.DTO.Base;
using Newtonsoft.Json;

namespace JIT.CPOS.BS.Web.Module.AppConfig.Handler
{
    /// <summary>
    /// HomePageHandler
    /// </summary>
    public class HomePageHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        /// <summary>
        /// 页面入口
        /// </summary>
        /// <param name="pContext"></param>
        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = string.Empty;
            switch (pContext.Request.QueryString["method"])
            {
                case "GetLevel1ItemCategory":   //获取第一级商品分类
                    content = GetLevel1ItemCategory();
                    break;
                case "GetItemCategory":         //商品分类查询
                    content = GetItemCategory();
                    break;
                case "GetItemGroup":         //商品分组查询
                    content = GetItemGroup();
                    break;
                case "GetItemList":             //商品查询
                    content = GetItemList();
                    break;
                case "GetHomePageConfigInfo":   //获取客户端首页所有配置信息
                    content = GetHomePageConfigInfo();
                    break;
                case "GetTemplateConfigInfo":        //获取模板配置信息
                    content = GetTemplateConfigInfo();
                    break;
                case "SaveItemCategory":        //保存商品分类
                    content = SaveItemCategory();
                    break;
                case "SaveAds":
                    content = SaveAds();
                    break;
                case "SaveEventItemArea":
                    content = SaveEventItemArea();
                    break;
                case "DeleteItemCategoryArea":
                    content = DeleteItemCategoryArea();
                    break;
                case "GetPanicbuyingEventList"://获取活动列表
                    content = GetPanicbuyingEventList();
                    break;
                case "GetItemAreaByEventTypeID":
                    content = GetItemAreaByEventTypeID();
                    break;
                case "GetItemAreaByEventID":
                    content = GetItemAreaByEventID();
                    break;
                case "UpdateMHCategoryAreaByGroupId":
                    content = UpdateMHCategoryAreaByGroupId();
                    break;
                case "UpdateMobileHomeSort":
                    content = UpdateMobileHomeSort();
                    break;
                case "UpdateModelSort":
                    content = UpdateModelSort();
                    break;
                case "SaveSeach":
                    content = SaveSeach();
                    break;
                case "GetHomePageTemplate":
                    content = GetHomePageTemplate();
                    break;
                case "GetHomePageListByCustomer":
                    content = GetHomePageListByCustomer();
                    break;
                case "SaveHomePage":
                    content = SaveHomePage();
                    break;
                case "SaveFollowInfo":
                    content = SaveFollowInfo();
                    break;
                case "GetFollowInfo":
                    content = GetFollowInfo();
                    break;
                case "ChangeStatus":
                    content = ChangeStatus();
                    break;
                case "DeleteHomePage":
                    content = DeleteHomePage();
                    break;

            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region 激活操作
        public string ChangeStatus()
        {
            var responseData = new ResponseData();
            try
            {
                string strHomeId = FormatParamValue(Request("HomeId"));//模板id
                var homeEntity = new MobileHomeEntity();
                var homeBll = new MobileHomeBLL(this.CurrentUserInfo);
                homeEntity.HomeId = new Guid(strHomeId);
                homeEntity.CustomerId = this.CurrentUserInfo.ClientID;
                homeEntity.IsActivate = 1;

                homeBll.UpdateIsActivate(homeEntity);//先把当前商户下的所有主页状态设为未激活状态
                homeBll.Update(homeEntity);//把当前主页设为激活状态

                responseData.success = true;
                return responseData.ToJSON();
            }
            catch (Exception ex)
            {
                responseData.success = false;
                responseData.msg = ex.Message.ToString();
                return responseData.ToJSON();
            }
        }
        #endregion
        /// <summary>
        /// 删除操作
        /// </summary>
        /// <returns></returns>
        public string DeleteHomePage()
        {
            var responseData = new ResponseData();
            try
            {


                string strHomeId = FormatParamValue(Request("HomeId"));//模板id

                var homeEntity = new MobileHomeEntity();
                var homeBll = new MobileHomeBLL(this.CurrentUserInfo);

                homeEntity.HomeId = new Guid(strHomeId);
                homeEntity.CustomerId = this.CurrentUserInfo.ClientID;
                homeEntity.IsDelete = 1;
                homeBll.Delete(homeEntity);

                responseData.success = true;
                return responseData.ToJSON();
            }
            catch (Exception ex)
            {
                responseData.success = false;
                responseData.msg = ex.Message.ToString();
                return responseData.ToJSON();
            }
        }

        #region 主页信息
        public class GetTemplateRespContentData
        {

            public string HomeId { get; set; }
            public IList<MobileHomeEntity> TemplateList { get; set; }             //主页
        }
        public class GetMobileHomeRespContentData
        {

            public int totalCount { get; set; }     // 总数量
            public int pageCount { get; set; }      //总页数
            public string HomeId { get; set; }
            public IList<HomePage> moblieHomeList { get; set; }             //主页
        }
        public class HomePage
        {
 
            public string HomeId { get; set; }
            public string Title { get; set; }
            public string sortActionJson { get; set; }
            public int IsActivate { get; set; }
            public string CreateTime { get; set; }
        }
        /// <summary>
        /// 获取模板信息
        /// </summary>
        /// <returns></returns>
        public string GetHomePageTemplate()
        {
            var responseData = new ResponseData();
            try
            {
                var homeBll = new MobileHomeBLL(this.CurrentUserInfo);
                var homeList = homeBll.QueryByEntity(new MobileHomeEntity { IsTemplate = 1 }, null);
                if (homeList != null && homeList.Length > 0)
                {

                    var content = new GetTemplateRespContentData
                    {
                        HomeId = new Guid().ToString(),
                        TemplateList = new List<MobileHomeEntity>()

                    };
                    content.TemplateList = homeList.OrderBy(a => a.DisplayIndex).ToArray();

                    responseData.success = true;
                    responseData.data = content;
                }

                return responseData.ToJSON();

            }
            catch (Exception ex)
            {

                responseData.success = false;
                responseData.msg = ex.ToString();
                return responseData.ToJSON();
            }
        }
        /// <summary>
        /// 获取商户所有主页列表
        /// </summary>
        /// <returns></returns>
        public string GetHomePageListByCustomer()
        {
            var responseData = new ResponseData();
            try
            {
                var homeBll = new MobileHomeBLL(this.CurrentUserInfo);
                int pPageSize = Convert.ToInt32(Request("PageSize"));//分页数量
                int pCurrentPageIndex = Convert.ToInt32(Request("PageIndex"));//页码


                OrderBy[] pOrderBys = new OrderBy[]{
                             new OrderBy(){ FieldName="CreateTime", Direction= OrderByDirections.Desc}
                            };
                var homeList = homeBll.PagedQueryByEntity(new MobileHomeEntity { CustomerId = this.CurrentUserInfo.ClientID, IsTemplate = 0 }, pOrderBys, pPageSize, pCurrentPageIndex);

                var content = new GetMobileHomeRespContentData
                {
                    totalCount = 0,
                    pageCount = 0,
                    moblieHomeList = new List<HomePage>()

                };
                if (homeList != null && homeList.RowCount > 0)
                {

                    content.totalCount = homeList.RowCount;
                    content.pageCount = homeList.PageCount;
                    content.moblieHomeList = homeList.Entities
                        .Select(a => new HomePage
                        {
                            HomeId = a.HomeId.ToString(),
                            Title = a.Title,
                            sortActionJson = a.sortActionJson,
                            IsActivate = a.IsActivate.HasValue ? (int)a.IsActivate : 0,
                            CreateTime =Convert.ToDateTime(a.CreateTime.ToString()).ToString("yyyy-MM-dd HH:mm:ss")

                        }).OrderByDescending(a=>a.CreateTime).ToArray();
                    responseData.success = true;
                    responseData.data = content;
                }
                else
                {
                    responseData.success = true;
                    responseData.data = content;
                    responseData.msg = "还未添加主页";
                }

                return responseData.ToJSON();

            }
            catch (Exception ex)
            {

                responseData.success = false;
                responseData.msg = ex.ToString();
                return responseData.ToJSON();
            }

        }
        /// <summary>
        /// 保存当前主页并设为激活状态
        /// </summary>
        /// <returns></returns>
        public string SaveHomePage()
        {
            var responseData = new ResponseData();
            try
            {


                string strHomeId = FormatParamValue(Request("HomeId"));//主页id
                //string strTemplateId = FormatParamValue(Request("TemplateId"));//模板id
                string strTitle = FormatParamValue(Request("Title"));//模板名称

                var homeEntity = new MobileHomeEntity();
                var homeBll = new MobileHomeBLL(this.CurrentUserInfo);

                var homeList = homeBll.QueryByEntity(new MobileHomeEntity { CustomerId = this.CurrentUserInfo.ClientID, HomeId = new Guid(strHomeId) }, null);

                if (homeList != null && homeList.Length > 0)
                {

                    homeEntity.Title = strTitle;
                    homeEntity.CustomerId = this.CurrentUserInfo.ClientID;
                    homeEntity.HomeId = new Guid(strHomeId);
                    homeBll.Update(homeEntity);//把当前主页设为激活状态
                }
              
                responseData.success = true;
                responseData.data = homeEntity.HomeId;

                return responseData.ToJSON();
            }
            catch (Exception ex)
            {
                responseData.success = false;
                responseData.msg = ex.Message.ToString();
                return responseData.ToJSON();
            }
        }
        #endregion

        #region 立即关注
        public class MaterialTextIdInfo
        {
            public string TextId { get; set; }

            public string Title { get; set; }
            public string ImageUrl { get; set; }

            public string Text { get; set; }
            public string OriginalUrl { get; set; }
            public string Author { get; set; }
        }
        public class followInfo
        {
            public string HomeId { get; set; }

            public string FollowId { get; set; }
            public string Title { get; set; }
            /// <summary>
            /// 类型 3 链接 35图文信息
            /// </summary>
            public int? TypeId { get; set; }
            /// <summary>
            /// 图文信息Id 关联 WMaterialText
            /// </summary>
            public string TextId { get; set; }
            /// <summary>
            /// 图文信息Title 关联 WMaterialText
            /// </summary>
            public string TextTitle { get; set; }
            /// <summary>
            /// 自定义链接
            /// </summary>
            public string Url { get; set; }
        }

        public string GetFollowInfo()
        {
            var responseData = new ResponseData();
            string homeId = FormatParamValue(Request("homeId"));

            var bllFollow = new MHFollowBLL(CurrentUserInfo);

            var ds = bllFollow.GetFollowInfo(homeId);


            //var bll = new WKeywordReplyBLL(CurrentUserInfo);
            //var ds = bll.GetKeyWordListByReplyId(replyId);
            //var textDs = bll.GetWMaterialTextByReplyId(replyId);

            if (ds.Tables[0].Rows.Count > 0)
            {



                //var temp = ds.Tables[0].AsEnumerable().Select(t => new followInfo()
                //{

                //    HomeId = string.IsNullOrEmpty(t["ReplyId"].ToString()) ? "" : t["ReplyId"].ToString(),
                //    Text = string.IsNullOrEmpty(t["text"].ToString()) ? "" : t["text"].ToString(),
                //    MaterialText = textDs.Tables[0].AsEnumerable().Select(tt => new MaterialTextIdInfo()
                //    {
                //        TextId = string.IsNullOrEmpty(tt["TextId"].ToString()) ? "" : tt["TextId"].ToString(),
                //        ImageUrl =
                //            string.IsNullOrEmpty(tt["CoverImageUrl"].ToString()) ? "" : tt["CoverImageUrl"].ToString(),
                //        Title = string.IsNullOrEmpty(tt["Title"].ToString()) ? "" : tt["Title"].ToString(),
                //        Author = string.IsNullOrEmpty(tt["Author"].ToString()) ? "" : tt["Author"].ToString(),
                //        Text = string.IsNullOrEmpty(tt["Text"].ToString()) ? "" : tt["Text"].ToString(),
                //        OriginalUrl = string.IsNullOrEmpty(tt["OriginalUrl"].ToString()) ? "" : tt["OriginalUrl"].ToString()
                //    }).FirstOrDefault()
                //});
                //responseData.success = true;
                //responseData.data = temp.FirstOrDefault();
            }
            else
            {
                responseData.success = false;
                responseData.msg = "没有设置关注信息";
            }
            return responseData.ToJSON();
        }
        public string SaveFollowInfo()
        {
            string strHomeId = FormatParamValue(Request("homeId"));//模板id
            string strTitle = FormatParamValue(Request("title"));//欢迎语
            string strTypeId = FormatParamValue(Request("typeId"));//类型 3:链接,35图文
            string strTextId = FormatParamValue(Request("textId"));//图文Id
            string strUrl = FormatParamValue(Request("url"));//链接地址
            string strFollowId = FormatParamValue(Request("followId"));
            string strTextTitle = FormatParamValue(Request("textTitle"));
            var responseData = new ResponseData();

            if (this.CurrentContext.Request["homeId"] == null || this.CurrentContext.Request["homeId"].ToString() == "")
            {
                responseData.success = false;
                responseData.msg = "参数homeId有误";
                return responseData.ToJSON();
            }

            try
            {

                var bllFollow = new MHFollowBLL(CurrentUserInfo);
                var entityFollow = new MHFollowEntity();
                entityFollow.Title = strTitle;
                entityFollow.CustomerID = CurrentUserInfo.ClientID;
                entityFollow.HomeId = strHomeId;

                if (strTypeId == "3")
                {
                    entityFollow.TypeId = Convert.ToInt32(strTypeId);
                    entityFollow.Url = strUrl;

                }
                if (strTypeId == "35")
                {
                    entityFollow.TypeId = Convert.ToInt32(strTypeId);
                    entityFollow.TextId = strTextId;
                    entityFollow.TextTitle = strTextTitle;


                }
                if (string.IsNullOrEmpty(strFollowId))
                {
                    bllFollow.Create(entityFollow);
                }
                else
                {
                    entityFollow.FollowId = new Guid(strFollowId);
                    bllFollow.Update(entityFollow, null);
                }
                responseData.success = true;

            }
            catch (Exception ex)
            {
                responseData.success = false;
                responseData.msg = ex.Message.ToString();
            }
            return responseData.ToJSON();

        }
        #endregion

        #region  更新首页各个模块的排列顺序
        public string UpdateMobileHomeSort()
        {
            var responseData = new ResponseData();
            var sortActionJson = this.CurrentContext.Request["sortActionJson"].ToString();//转换成

            if (string.IsNullOrEmpty(sortActionJson))
            {
                responseData.success = false;
                responseData.msg = "排序信息不能为空";
                return responseData.ToJSON();
            }
            try
            {


                var homeBll = new MobileHomeBLL(this.CurrentUserInfo);
                var homeList = homeBll.QueryByEntity(new MobileHomeEntity { CustomerId = this.CurrentUserInfo.ClientID, HomeId = new Guid(this.CurrentContext.Request["homeId"].ToString()) }, null);

                string customerId = this.CurrentUserInfo.CurrentUser.customer_id;
                if (homeList == null && homeList.Length == 0)
                {
                    responseData.success = false;
                    responseData.msg = "没有查询到对应首页主表的数据";
                    return responseData.ToJSON();
                }
                //var homeId = homeList.FirstOrDefault().HomeId;
                var currentHome = homeList.FirstOrDefault();
                currentHome.sortActionJson = sortActionJson;
                homeBll.Update(currentHome);
                if (this.CurrentContext.Request["displayIndexList"] != null && this.CurrentContext.Request["displayIndexList"].ToString() != "")
                {
                    JsonSerializer serializer = new JsonSerializer();

                    var bllCategoryAreaGroup = new MHCategoryAreaGroupBLL(this.CurrentUserInfo);
                    StringReader sr = new StringReader(this.CurrentContext.Request["displayIndexList"].ToString());
                    object obj = serializer.Deserialize(new JsonTextReader(sr), typeof(List<ModelSort>));
                    List<ModelSort> listModelSort = obj as List<ModelSort>;
                    if (listModelSort != null)
                    {
                        foreach (var item in listModelSort)
                        {
                            bllCategoryAreaGroup.UpdateCategoryAreaGroupDisplayIndex(item.index, item.groupId);
                        }
                    }
                }

                responseData.success = true;
                responseData.msg = "更新成功";
            }
            catch (Exception ex)
            {
                responseData.success = false;
                responseData.msg = "更新失败：" + ex.Message.ToString();
            }
            return responseData.ToJSON();
        }
        /// <summary>
        /// 更新单个模块的排序
        /// </summary>
        /// <returns></returns>
        public string UpdateModelSort()
        {
            var categoryAreaGroupBll = new MHCategoryAreaGroupBLL(this.CurrentUserInfo);
            var responseData = new ResponseData();

            var entityCategoryAreaGroup = new MHCategoryAreaGroupEntity();
            if (this.CurrentContext.Request["homeId"] != null && this.CurrentContext.Request["homeId"].ToString() != "")
            {
                responseData.success = false;
                responseData.msg = "homeId参数有误";
                return responseData.ToJSON();
            }
            if (this.CurrentContext.Request["groupId"] != null && this.CurrentContext.Request["groupId"].ToString() != "")
            {
                responseData.success = false;
                responseData.msg = "groupId参数有误";
                return responseData.ToJSON();
            }
            if (this.CurrentContext.Request["index"] != null && this.CurrentContext.Request["index"].ToString() != "")
            {
                responseData.success = false;
                responseData.msg = "index参数有误";
                return responseData.ToJSON();
            }
            try
            {
                entityCategoryAreaGroup.DisplayIndex = Convert.ToInt32(this.CurrentContext.Request["index"].ToString()); ;
                entityCategoryAreaGroup.GroupId = Convert.ToInt32(this.CurrentContext.Request["groupId"].ToString());
                categoryAreaGroupBll.Update(entityCategoryAreaGroup);

                responseData.success = true;
            }
            catch (Exception ex)
            {
                responseData.success = false;
                responseData.msg = ex.Message.ToString();
            }

            return responseData.ToJSON();

        }

        public class ModelSort
        {
            public string groupId { get; set; }
            public int index { get; set; }
        }

        #endregion

        #region GetLevel1ItemCategory 获取第一级商品分类

        /// <summary>
        /// 获取第一级商品分类
        /// </summary>
        public string GetLevel1ItemCategory()
        {
            var responseData = new ResponseData();

            var categoryService = new ItemCategoryService(this.CurrentUserInfo);
            var content = new GetLevel1ItemCategoryRespContentData();  //商品类别集合

            try
            {
                var ds = categoryService.GetLevel1ItemCategory(this.CurrentUserInfo.ClientID);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    content.categoryList = DataTableToObject.ConvertToList<GetLevel1ItemCategoryEntity>(ds.Tables[0]);
                }

                responseData.success = true;
                responseData.data = content;
                return responseData.ToJSON();
                //responseData.success = true;
                //responseData.msg = "asd".ToString();
                //return responseData.ToJSON();
            }
            catch (Exception ex)
            {
                responseData.success = false;
                responseData.msg = ex.ToString();
                return responseData.ToJSON();
            }
        }

        public class GetLevel1ItemCategoryRespContentData
        {
            public IList<GetLevel1ItemCategoryEntity> categoryList { get; set; }     //商品类别集合
        }
        public class GetLevel1ItemCategoryEntity
        {
            public string categoryId { get; set; }      //商品类别ID
            public string categoryName { get; set; }    //商品类别名称
        }

        #endregion

        #region GetItemCategory 商品分类/分组查询
        /// <summary>
        /// 商品分组
        /// </summary>
        /// <returns></returns>
        public string GetItemGroup()
        {
            var responseData = new ResponseData();

            //请求参数
            var categoryName = FormatParamValue(Request("groupName"));
            var pageIndex = Utils.GetIntVal(FormatParamValue(Request("pageIndex")));
            var pageSize = Utils.GetIntVal(FormatParamValue(Request("pageSize")));

            if (pageSize <= 0) pageSize = 15;

            var categoryService = new ItemCategoryService(this.CurrentUserInfo);
            var content = new GetItemGroupRespContentData();  //商品分组集合

            try
            {
                var ds = categoryService.GetItemGroupList(this.CurrentUserInfo.ClientID, categoryName, pageIndex, pageSize);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    content.groupList = DataTableToObject.ConvertToList<GetItemCategoryEntity>(ds.Tables[0]);
                    content.totalCount = categoryService.GetItemGroupCount(this.CurrentUserInfo.ClientID, categoryName);
                }

                responseData.success = true;
                responseData.data = content;
                return responseData.ToJSON();
            }
            catch (Exception ex)
            {
                responseData.success = false;
                responseData.msg = ex.ToString();
                return responseData.ToJSON();
            }
        }
        /// <summary>
        /// 商品分类查询
        /// </summary>
        public string GetItemCategory()
        {
            var responseData = new ResponseData();

            //请求参数
            var categoryName = FormatParamValue(Request("categoryName"));
            var pageIndex = Utils.GetIntVal(FormatParamValue(Request("pageIndex")));
            var pageSize = Utils.GetIntVal(FormatParamValue(Request("pageSize")));

            if (pageSize <= 0) pageSize = 15;

            var categoryService = new ItemCategoryService(this.CurrentUserInfo);
            var content = new GetItemCategoryRespContentData();  //商品类别集合

            try
            {
                var ds = categoryService.GetItemCategoryList(this.CurrentUserInfo.ClientID, categoryName, pageIndex, pageSize);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    content.categoryList = DataTableToObject.ConvertToList<GetItemCategoryEntity>(ds.Tables[0]);
                    content.totalCount = categoryService.GetItemCategoryCount(this.CurrentUserInfo.ClientID, categoryName);
                }

                responseData.success = true;
                responseData.data = content;
                return responseData.ToJSON();
            }
            catch (Exception ex)
            {
                responseData.success = false;
                responseData.msg = ex.ToString();
                return responseData.ToJSON();
            }
        }
        public class GetItemGroupRespContentData
        {
            public int totalCount { get; set; }     // 总数量
            public IList<GetItemCategoryEntity> groupList { get; set; }     //商品类别集合
        }
        public class GetItemCategoryRespContentData
        {
            public int totalCount { get; set; }     // 总数量
            public IList<GetItemCategoryEntity> categoryList { get; set; }     //商品类别集合
        }
        public class GetItemCategoryEntity
        {
            public string categoryId { get; set; }      //商品类别ID
            public string categoryName { get; set; }    //商品类别名称
            public Int64 displayIndex { get; set; }	    //序号
        }

        #endregion

        #region GetItemList 商品查询

        /// <summary>
        /// 商品查询
        /// </summary>
        public string GetItemList()
        {
            var responseData = new ResponseData();

            //请求参数
            var categoryId = FormatParamValue(Request("categoryId"));
            var itemName = FormatParamValue(Request("itemName"));
            var pageIndex = Utils.GetIntVal(FormatParamValue(Request("pageIndex")));
            var pageSize = Utils.GetIntVal(FormatParamValue(Request("pageSize")));

            if (pageSize <= 0) pageSize = 15;

            var itemService = new ItemService(this.CurrentUserInfo);
            var content = new GetItemListRespContentData();  //商品集合

            try
            {
                var ds = itemService.GetItemList(this.CurrentUserInfo.ClientID, categoryId, itemName, pageIndex, pageSize);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    content.itemList = DataTableToObject.ConvertToList<GetItemListEntity>(ds.Tables[0]);
                    content.totalCount = itemService.GetItemListCount(this.CurrentUserInfo.ClientID, categoryId, itemName);
                }

                responseData.success = true;
                responseData.data = content;
                return responseData.ToJSON();
            }
            catch (Exception ex)
            {
                responseData.success = false;
                responseData.msg = ex.ToString();
                return responseData.ToJSON();
            }
        }

        public class GetItemListRespContentData
        {
            public int totalCount { get; set; }     // 总数量
            public IList<GetItemListEntity> itemList { get; set; }  //商品集合
        }
        public class GetItemListEntity
        {
            public string categoryName { get; set; }    //商品类别名称
            public string itemId { get; set; }          //商品ID
            public string itemName { get; set; }        //商品名称
            public Int64 displayIndex { get; set; }	    //序号
        }

        #endregion
        /// <summary>
        /// 获取模板的默认配置
        /// </summary>
        /// <param name="strTemplateId"></param>
        /// <returns></returns>
        #region GetHomePageConfigInfo 获取客户端首页所有配置信息

        public string GetTemplateConfigInfo()
        {
            var responseData = new ResponseData();
            try
            {
                if (this.CurrentContext.Request["homeId"] == null || this.CurrentContext.Request["homeId"].ToString() == "")
                {
                    responseData.success = false;
                    responseData.msg = "参数homeId有误";
                    return responseData.ToJSON();
                }
        
                string strHomeId = this.CurrentContext.Request["homeId"].ToString();

                var homeBll = new MobileHomeBLL(this.CurrentUserInfo);
                var homeList = homeBll.QueryByEntity(new MobileHomeEntity { CustomerId = this.CurrentUserInfo.ClientID, HomeId = new Guid(strHomeId),IsTemplate=1}, null);
                if (homeList != null && homeList.Length > 0)
                {
                    var homeEntity = homeList.FirstOrDefault();
                    var adAreaBll = new MHAdAreaBLL(this.CurrentUserInfo);
                    var content = new GetHomePageConfigInfoRespContentData
                    {
                        homeId = strHomeId,
                        title = homeEntity.Title,
                        adList = new List<AdEntity>(),
                        eventList = new List<EventListEntity>(),
                        secondKill = new List<EventListEntity>(),
                        groupBuy = new List<EventListEntity>(),
                        hotBuy = new List<EventListEntity>(),
                        originalityList = new List<CategoryEntity>(),//创意组合老版本的商品列表
                        productList = new List<CategoryEntity>(),//商品列表
                        categoryEntrance = new CategoryEntity(),//分类组合
                        navList = new CategoryEntity(),
                        search = new MHSearchAreaEntity(),
                        follow = new followInfo(),
                        sortActionJson = ""
                    };  //客户端首页所有配置信息
        
          
                   
                    content.sortActionJson = homeEntity.sortActionJson == null ? "" : homeEntity.sortActionJson;//返回排序数据
                    #region 广告集合 A 图片广告 B活动集合 C 商品分类和商品
                    var dsAd = adAreaBll.GetAdList(homeEntity.HomeId.ToString());//获取广告集合
                    if (dsAd != null && dsAd.Tables.Count > 0 && dsAd.Tables[0].Rows.Count > 0)
                    {
                        content.adList = DataTableToObject.ConvertToList<AdEntity>(dsAd.Tables[0]);
                    }
                    #endregion

                    #region 搜索框
                    var dsSearch = adAreaBll.GetMHSearchArea(homeEntity.HomeId.ToString());//获取搜索框
                    if (dsSearch != null && dsSearch.Tables.Count > 0 && dsSearch.Tables[0].Rows.Count > 0)
                    {
                        content.search = DataTableToObject.ConvertToObject<MHSearchAreaEntity>(dsSearch.Tables[0].Rows[0]);//转换成一个对象时，里面的参数不能是一个表，而是一行数据
                    }
                    #endregion

                    #region 活动集合
                    var bllCategoryAreaGroup = new MHCategoryAreaGroupBLL(this.CurrentUserInfo);

                    var allGroup = bllCategoryAreaGroup.QueryByEntity(new MHCategoryAreaGroupEntity { CustomerID = this.CurrentUserInfo.ClientID, HomeId = strHomeId }, null);

                    var eventGroup = allGroup.Where(a => a.ModelTypeId == 5 || a.ModelTypeId == 6 || a.ModelTypeId == 7 || a.ModelTypeId == 8);
                    foreach (var item in eventGroup)
                    {
                        var dsEvent = adAreaBll.GetEventInfoByGroupId(homeEntity.HomeId.ToString(), item.GroupId.ToString());//获取

                        if (dsEvent != null && dsEvent.Tables.Count > 0 && dsEvent.Tables[0].Rows.Count > 0)
                        {
                            var category = new EventListEntity();
                            var dsEventList = DataTableToObject.ConvertToList<EventAreaEntity>(dsEvent.Tables[0]);

                            category.groupId = item.GroupId;
                            category.showStyle = dsEventList.FirstOrDefault().showStyle;
                            category.shopType = dsEventList.FirstOrDefault().typeId;
                            category.displayIndex = item.DisplayIndex;
                            category.arrayList = dsEventList;


                            if (dsEventList.FirstOrDefault().areaFlag == "eventList")
                            {
                                category.areaFlag = "eventList";
                                content.eventList.Add(category);
                            }
                            if (dsEventList.FirstOrDefault().areaFlag == "secondKill")
                            {
                                category.areaFlag = "secondKill";
                                content.secondKill.Add(category);
                            }
                            if (dsEventList.FirstOrDefault().areaFlag == "groupBuy")
                            {
                                category.areaFlag = "groupBuy";
                                content.groupBuy.Add(category);
                            }
                            if (dsEventList.FirstOrDefault().areaFlag == "hotBuy")
                            {
                                category.areaFlag = "hotBuy";
                                content.hotBuy.Add(category);
                            }
                        }

                    }
                    #endregion

                    #region 分类集合

                    List<CategoryEntity> allList = new List<CategoryEntity>();
                    eventGroup = allGroup.Where(a => a.ModelTypeId != 5 && a.ModelTypeId != 6 && a.ModelTypeId != 7 && a.ModelTypeId != 8);

                    if (eventGroup != null)
                    {
                        foreach (var groupItem in eventGroup)
                        {
                            var category = new CategoryEntity
                            {
                                groupId = groupItem.GroupId.ToString(),//有个groupId
                                itemList = new List<ItemEntity>()
                            };
                            //根据groupId和HomeId来取MHCategoryArea
                            var dsItem = adAreaBll.GetItemList(category.groupId, homeEntity.HomeId.ToString());
                            if (dsItem != null && dsItem.Tables.Count > 0 && dsItem.Tables[0].Rows.Count > 0)
                            {

                                category.modelTypeId = groupItem.ModelTypeId;
                                category.modelTypeName = groupItem.ModelName;
                                category.styleType = groupItem.StyleType;
                                category.titleName = groupItem.TitleName;
                                category.titleStyle = groupItem.TitleStyle;
                                category.showCount = (int)groupItem.ShowCount;
                                category.showName = (int)groupItem.ShowName;
                                category.showPrice = (int)groupItem.ShowPrice;
                                category.showSalesPrice = (int)groupItem.ShowSalesPrice;
                                category.showDiscount = (int)groupItem.ShowDiscount;
                                category.showSalesQty = (int)groupItem.ShowSalesQty;
                                category.displayIndex = (int)groupItem.DisplayIndex;
                                category.CategoryAreaGroupId = (int)groupItem.GroupId;

                                category.itemList = DataTableToObject.ConvertToList<ItemEntity>(dsItem.Tables[0]);
                            }
                            allList.Add(category);
                        }
                    }
                    //分类组合
                    List<CategoryEntity> lc = allList.Where(p => p.modelTypeId == 1).ToList();
                    if (lc != null && lc.Count != 0)
                    {
                        content.categoryEntrance = lc.OrderByDescending(p => p.groupId).ToList()[0]; ;
                    }
                    //商品列表
                    List<CategoryEntity> pList = allList.Where(p => p.modelTypeId == 2).ToList();
                    if (pList != null && pList.Count != 0)
                    {
                        content.productList = pList.OrderByDescending(p => p.groupId).ToList(); ;
                    }

                    //创意组合
                    if (allList.Where(p => p.modelTypeId == 3).ToList() != null && allList.Where(p => p.modelTypeId == 3).ToList().Count != 0)
                    {
                        content.originalityList = allList.Where(p => p.modelTypeId == 3).ToList();

                    }
                    //导航
                    List<CategoryEntity> lc4 = allList.Where(p => p.modelTypeId == 4).ToList();
                    if (lc4 != null && lc4.Count != 0)
                    {
                        content.navList = lc4.OrderByDescending(p => p.groupId).ToList()[0]; ;//(获取唯一的)
                    }

                    #endregion

                    #region 关注信息

                    var bllFollow = new MHFollowBLL(CurrentUserInfo);
                    var entity = new MHFollowEntity();

                    var dsFollow = bllFollow.QueryByEntity(new MHFollowEntity() { HomeId = strHomeId }, null);
                    followInfo follow = new followInfo();
                    follow = dsFollow.Select(f => new followInfo()
                    {
                        HomeId = f.HomeId,
                        FollowId = f.FollowId.ToString(),
                        Title = f.Title,
                        TextId = f.TextId,
                        TextTitle = f.TextTitle,
                        Url = f.Url,
                        TypeId = f.TypeId
                    }).FirstOrDefault();
                    content.follow = follow;
                    #endregion
                    responseData.success = true;
                    responseData.data = content;
                    return responseData.ToJSON();
                }
                else
                {
                    responseData.success = false;
                    responseData.msg = "没有查询到对应首页主表的数据";
                    return responseData.ToJSON();
                }

            }
            catch (Exception ex)
            {
                responseData.success = false;
                responseData.msg = "没有查询到对应首页主表的数据_" + ex.ToString();
                return responseData.ToJSON();
            }
            #region

            //var responseData = new ResponseData();

            //string strTemplateId = this.CurrentContext.Request["homeId"].ToString();
            //var content = new GetHomePageConfigInfoRespContentData
            //{
            //    homeId = new Guid().ToString(),
            //    adList = new List<AdEntity>(),
            //    eventList = new List<EventListEntity>(),
            //    secondKill = new List<EventListEntity>(),
            //    groupBuy = new List<EventListEntity>(),
            //    hotBuy = new List<EventListEntity>(),
            //    originalityList = new List<CategoryEntity>(),//创意组合老版本的商品列表
            //    productList = new List<CategoryEntity>(),//商品列表
            //    categoryEntrance = new CategoryEntity(),//分类组合
            //    navList = new CategoryEntity(),
            //    search = new MHSearchAreaEntity(),
            //    follow = new followInfo(),
            //    sortActionJson = ""
            //};  //客户端首页所有配置信息
            //try
            //{
            //    var homeBll = new MobileHomeBLL(this.CurrentUserInfo);
            //    var homeList = homeBll.QueryByEntity(new MobileHomeEntity { CustomerId = this.CurrentUserInfo.ClientID, HomeId = new Guid(strTemplateId) }, null);
            //    if (homeList != null && homeList.Length > 0)
            //    {
            //        var homeEntity = homeList.FirstOrDefault();

            //        var adAreaBll = new MHAdAreaBLL(this.CurrentUserInfo);

            //        content.sortActionJson = homeEntity.sortActionJson == null ? "" : homeEntity.sortActionJson;//返回排序数据

            //        #region 广告集合 A 图片广告 B活动集合 C 商品分类和商品

            //        var dsAd = adAreaBll.GetAdList(homeEntity.HomeId.ToString());//获取广告集合
            //        if (dsAd != null && dsAd.Tables.Count > 0 && dsAd.Tables[0].Rows.Count > 0)
            //        {
            //            content.adList = DataTableToObject.ConvertToList<AdEntity>(dsAd.Tables[0]);
            //        }

            //        #endregion

            //        #region 搜索框

            //        var dsSearch = adAreaBll.GetMHSearchArea(homeEntity.HomeId.ToString());//获取搜索框
            //        if (dsSearch != null && dsSearch.Tables.Count > 0 && dsSearch.Tables[0].Rows.Count > 0)
            //        {
            //            content.search = DataTableToObject.ConvertToObject<MHSearchAreaEntity>(dsSearch.Tables[0].Rows[0]);//转换成一个对象时，里面的参数不能是一个表，而是一行数据
            //        }

            //        #endregion



            //        #region 活动集合
            //        var bllCategoryAreaGroup = new MHCategoryAreaGroupBLL(this.CurrentUserInfo);

            //        var eventGroup = bllCategoryAreaGroup.QueryByEntity(new MHCategoryAreaGroupEntity { CustomerID = this.CurrentUserInfo.ClientID, HomeId = this.CurrentContext.Request["homeId"].ToString() }, null)
            //                            .Where(a => a.ModelTypeId == 5 || a.ModelTypeId == 6 || a.ModelTypeId == 7 || a.ModelTypeId == 8);
            //        foreach (var item in eventGroup)
            //        {
            //            var dsEvent = adAreaBll.GetEventInfoByGroupId(homeEntity.HomeId.ToString(), item.GroupId.ToString());//获取

            //            if (dsEvent != null && dsEvent.Tables.Count > 0 && dsEvent.Tables[0].Rows.Count > 0)
            //            {
            //                var category = new EventListEntity();
            //                var dsEventList = DataTableToObject.ConvertToList<EventAreaEntity>(dsEvent.Tables[0]);

            //                category.groupId = item.GroupId;
            //                category.showStyle = dsEventList.FirstOrDefault().showStyle;
            //                category.shopType = dsEventList.FirstOrDefault().typeId;
            //                category.arrayList = dsEventList;

            //                if (dsEventList.FirstOrDefault().areaFlag == "eventList")
            //                {
            //                    category.areaFlag = "eventList";
            //                    content.eventList.Add(category);
            //                }
            //                if (dsEventList.FirstOrDefault().areaFlag == "secondKill")
            //                {
            //                    category.areaFlag = "secondKill";
            //                    content.secondKill.Add(category);
            //                }
            //                if (dsEventList.FirstOrDefault().areaFlag == "groupBuy")
            //                {
            //                    category.areaFlag = "groupBuy";
            //                    content.groupBuy.Add(category);
            //                }
            //                if (dsEventList.FirstOrDefault().areaFlag == "hotBuy")
            //                {
            //                    category.areaFlag = "hotBuy";
            //                    content.hotBuy.Add(category);
            //                }
            //            }

            //        }
            //        #region
            //        //var dsEvent = adAreaBll.GetEventInfo(homeEntity.HomeId.ToString());//获取
            //        //if (dsEvent != null && dsEvent.Tables.Count > 0 && dsEvent.Tables[0].Rows.Count > 0)
            //        //{
            //        //    var dsEventList = DataTableToObject.ConvertToList<EventAreaEntity>(dsEvent.Tables[0]);
            //        //    if (dsEventList != null && dsEventList.Where(p => p.areaFlag == "eventList").ToList<EventAreaEntity>().Count != 0)
            //        //    {
            //        //        //原来的团购部分，三块分别是抢购、团购、热销的
            //        //        content.eventList.arrayList = dsEventList.Where(p => p.areaFlag == "eventList").ToList<EventAreaEntity>();
            //        //        // content.eventList.shopType =-1;//不是任何的一个值,不赋值
            //        //        content.eventList.areaFlag = "eventList";//不是任何的一个值
            //        //    }
            //        //    //  secondKill
            //        //    content.secondKill.arrayList = dsEventList.Where(p => p.areaFlag == "secondKill").ToList<EventAreaEntity>();
            //        //    if (content.secondKill.arrayList != null && content.secondKill.arrayList.Count != 0)
            //        //    {
            //        //        content.secondKill.shopType = content.secondKill.arrayList[0].typeId;//不是任何的一个值
            //        //        content.secondKill.ShowStyle = content.secondKill.arrayList[0].showStyle;
            //        //    }
            //        //    content.secondKill.areaFlag = "secondKill";//不是任何的一个值

            //        //    content.groupBuy.arrayList = dsEventList.Where(p => p.areaFlag == "groupBuy").ToList<EventAreaEntity>();
            //        //    if (content.groupBuy.arrayList != null && content.groupBuy.arrayList.Count != 0)
            //        //    {
            //        //        content.groupBuy.shopType = content.groupBuy.arrayList[0].typeId;//不是任何的一个值
            //        //        content.groupBuy.ShowStyle = content.groupBuy.arrayList[0].showStyle;

            //        //    }

            //        //    content.groupBuy.areaFlag = "groupBuy";//不是任何的一个值
            //        //    content.hotBuy.arrayList = dsEventList.Where(p => p.areaFlag == "hotBuy").ToList<EventAreaEntity>();
            //        //    if (content.hotBuy.arrayList != null && content.hotBuy.arrayList.Count != 0)
            //        //    {
            //        //        content.hotBuy.shopType = content.hotBuy.arrayList[0].typeId;//不是任何的一个值
            //        //        content.hotBuy.ShowStyle = content.hotBuy.arrayList[0].showStyle;
            //        //    }

            //        //    content.hotBuy.areaFlag = "hotBuy";//不是任何的一个值
            //        //}
            //        #endregion



            //        #endregion

            //        #region 分类集合

            //        List<CategoryEntity> allList = new List<CategoryEntity>();
            //        //获取分组ID
            //        var dsGroup = adAreaBll.GetCategoryGroupId(homeEntity.HomeId.ToString());
            //        if (dsGroup != null && dsGroup.Tables.Count > 0 && dsGroup.Tables[0].Rows.Count > 0)
            //        {
            //            foreach (DataRow dr in dsGroup.Tables[0].Rows)
            //            {
            //                var category = new CategoryEntity
            //                {
            //                    groupId = dr[0].ToString(),//有个groupId
            //                    itemList = new List<ItemEntity>()
            //                };
            //                //根据groupId和HomeId来取MHCategoryArea
            //                var dsItem = adAreaBll.GetItemList(category.groupId, homeEntity.HomeId.ToString());
            //                if (dsItem != null && dsItem.Tables.Count > 0 && dsItem.Tables[0].Rows.Count > 0)
            //                {
            //                    //获取MHCategoryAreaGroup的modelTypeId和modelname，先取子元素，再取父元素的原因是防止冗余数据。
            //                    DataSet modelDs = adAreaBll.GetModelTypeIdByGroupId(category.groupId, homeEntity.HomeId.ToString());//传category的GroupID
            //                    if (modelDs.Tables[0].Rows.Count > 0)
            //                    {
            //                        category.modelTypeId = Convert.ToInt32(modelDs.Tables[0].Rows[0]["modelTypeId"]);
            //                        category.modelTypeName = Convert.ToString(modelDs.Tables[0].Rows[0]["modelTypeName"]);
            //                        category.styleType = Convert.ToString(modelDs.Tables[0].Rows[0]["StyleType"]);  //直接Convert.ToString会把null值变为“”
            //                        category.titleName = Convert.ToString(modelDs.Tables[0].Rows[0]["TitleName"]);
            //                        category.titleStyle = Convert.ToString(modelDs.Tables[0].Rows[0]["TitleStyle"]);
            //                        category.showCount = Convert.ToInt32(modelDs.Tables[0].Rows[0]["ShowCount"]);
            //                        category.showName = Convert.ToInt32(modelDs.Tables[0].Rows[0]["ShowName"]);
            //                        category.showPrice = Convert.ToInt32(modelDs.Tables[0].Rows[0]["ShowPrice"]);
            //                        category.showDiscount = Convert.ToInt32(modelDs.Tables[0].Rows[0]["ShowDiscount"]);
            //                        category.displayIndex = Convert.ToInt32(modelDs.Tables[0].Rows[0]["DisplayIndex"]);
            //                        category.CategoryAreaGroupId = Convert.ToInt32(modelDs.Tables[0].Rows[0]["CategoryAreaGroupId"]);

            //                    }

            //                    category.itemList = DataTableToObject.ConvertToList<ItemEntity>(dsItem.Tables[0]);
            //                }
            //                allList.Add(category);
            //            }
            //        }
            //        // 过滤分类集合，把ModelTypeID=1的取出来(获取唯一的)
            //        List<CategoryEntity> lc = allList.Where(p => p.modelTypeId == 1).ToList();
            //        if (lc != null && lc.Count != 0)
            //        {
            //            content.categoryEntrance = lc.OrderByDescending(p => p.groupId).ToList()[0]; ;
            //        }

            //        //else
            //        //{
            //        //    content.categoryEntrance.itemList = new List<ItemEntity>();
            //        //}
            //        List<CategoryEntity> pList = allList.Where(p => p.modelTypeId == 2).ToList();
            //        if (pList != null && pList.Count != 0)
            //        {
            //            content.productList = pList.OrderByDescending(p => p.groupId).ToList(); ;
            //        }

            //        // 过滤分类集合，把ModelTypeID=4的取出来(导航部门)
            //        List<CategoryEntity> lc4 = allList.Where(p => p.modelTypeId == 4).ToList();
            //        if (lc4 != null && lc4.Count != 0)
            //        {
            //            content.navList = lc4.OrderByDescending(p => p.groupId).ToList()[0]; ;//(获取唯一的)
            //        }
            //        //过滤分类集合，把ModelTypeID<>1的和不等于8的取出来
            //        if (allList.Where(p => p.modelTypeId == 3).ToList() != null && allList.Where(p => p.modelTypeId == 3).ToList().Count != 0)
            //        {
            //            content.originalityList = allList.Where(p => p.modelTypeId == 3).ToList();

            //        }
            //        #endregion

            //        #region 关注信息
            //        var bllFollow = new MHFollowBLL(CurrentUserInfo);
            //        var entity = new MHFollowEntity();

            //        var dsFollow = bllFollow.QueryByEntity(new MHFollowEntity() { HomeId = CurrentUserInfo.ClientID }, null);
            //        followInfo follow = new followInfo();
            //        follow = dsFollow.Select(f => new followInfo()
            //        {
            //            HomeId = f.HomeId,
            //            Title = f.Title,
            //            TextId = f.TextId,
            //            Url = f.Url,
            //            TypeId = f.TypeId
            //        }).FirstOrDefault();
            //        content.follow = follow;
            //        //var bll = new WKeywordReplyBLL(CurrentUserInfo);
            //        //var ds = bll.GetKeyWordListByReplyId(this.CurrentContext.Request["homeId"].ToString());
            //        //var textDs = bll.GetWMaterialTextByReplyId(this.CurrentContext.Request["homeId"].ToString());

            //        //if (ds.Tables[0].Rows.Count > 0)
            //        //{
            //        //    var temp = ds.Tables[0].AsEnumerable().Select(t => new followInfo()
            //        //    {

            //        //        HomeId = string.IsNullOrEmpty(t["ReplyId"].ToString()) ? "" : t["ReplyId"].ToString(),
            //        //        Text = string.IsNullOrEmpty(t["text"].ToString()) ? "" : t["text"].ToString(),
            //        //        MaterialText = textDs.Tables[0].AsEnumerable().Select(tt => new MaterialTextIdInfo()
            //        //        {
            //        //            TextId = string.IsNullOrEmpty(tt["TextId"].ToString()) ? "" : tt["TextId"].ToString(),
            //        //            ImageUrl = string.IsNullOrEmpty(tt["CoverImageUrl"].ToString()) ? "" : tt["CoverImageUrl"].ToString(),
            //        //            Title = string.IsNullOrEmpty(tt["Title"].ToString()) ? "" : tt["Title"].ToString(),
            //        //            Author = string.IsNullOrEmpty(tt["Author"].ToString()) ? "" : tt["Author"].ToString(),
            //        //            Text = string.IsNullOrEmpty(tt["Text"].ToString()) ? "" : tt["Text"].ToString(),
            //        //            OriginalUrl = string.IsNullOrEmpty(tt["OriginalUrl"].ToString()) ? "" : tt["OriginalUrl"].ToString()
            //        //        }).FirstOrDefault()
            //        //    });
            //        //    content.follow = temp.FirstOrDefault();
            //        //}
            //        #endregion


            //        responseData.success = true;
            //        responseData.data = content;
            //        return responseData.ToJSON();

            //    }
            //}
            //catch
            //{

            //}
            //return responseData.ToJSON();
            #endregion
        }
        /// <summary>
        /// 获取客户端在首次添加首页时的配置信息
        /// </summary>
        public string GetHomePageConfigInfo()
        {
            var responseData = new ResponseData();
            try
            {
                if (this.CurrentContext.Request["homeId"] == null || this.CurrentContext.Request["homeId"].ToString() == "")
                {
                    responseData.success = false;
                    responseData.msg = "参数homeId有误";
                    return responseData.ToJSON();
                }
                if (this.CurrentContext.Request["type"] == null || this.CurrentContext.Request["type"].ToString() == "")
                {
                    responseData.success = false;
                    responseData.msg = "参数type有误";
                    return responseData.ToJSON();
                }
                string strHomeId = this.CurrentContext.Request["homeId"].ToString();
                string strType = this.CurrentContext.Request["type"].ToString();

                var homeBll = new MobileHomeBLL(this.CurrentUserInfo);
                MobileHomeEntity[] homeList = null;
                if (strType == "Add")
                    homeList = homeBll.QueryByEntity(new MobileHomeEntity { HomeId = new Guid(strHomeId) ,IsTemplate=1}, null);
                else
                    homeList = homeBll.QueryByEntity(new MobileHomeEntity { CustomerId = this.CurrentUserInfo.ClientID, HomeId = new Guid(strHomeId) }, null);

                if (homeList != null && homeList.Length > 0)
                {
                    var homeEntity = homeList.FirstOrDefault();
                    var adAreaBll = new MHAdAreaBLL(this.CurrentUserInfo);
                    var content = new GetHomePageConfigInfoRespContentData
                    {
                        homeId = new Guid().ToString(),
                        title = homeEntity.Title,
                        adList = new List<AdEntity>(),
                        eventList = new List<EventListEntity>(),
                        secondKill = new List<EventListEntity>(),
                        groupBuy = new List<EventListEntity>(),
                        hotBuy = new List<EventListEntity>(),
                        originalityList = new List<CategoryEntity>(),//创意组合老版本的商品列表
                        productList = new List<CategoryEntity>(),//商品列表
                        categoryEntrance = new CategoryEntity(),//分类组合
                        navList = new CategoryEntity(),
                        search = new MHSearchAreaEntity(),
                        follow = new followInfo(),
                        sortActionJson = ""
                    };  //客户端首页所有配置信息
                    if (strType == "Add" && homeEntity.IsTemplate == 1)
                    {
                        string strNewHomeId = Guid.NewGuid().ToString();

                        MobileHomeEntity newHomeEntity = new MobileHomeEntity();

                        content.homeId = strNewHomeId;
                        newHomeEntity.Title = homeEntity.Title;
                        newHomeEntity.HomeId = new Guid(strNewHomeId);
                        newHomeEntity.CustomerId = this.CurrentUserInfo.ClientID;
                        newHomeEntity.IsDelete = 0;
                        newHomeEntity.IsTemplate = 0;
                        newHomeEntity.TemplateId = new Guid(strHomeId);
                        newHomeEntity.sortActionJson = homeEntity.sortActionJson;
                        homeBll.Create(newHomeEntity);
                        homeBll.CreateStoreDataFromTemplate(strNewHomeId, strHomeId);
                        homeEntity = newHomeEntity;
                    }
                    else if (strType == "Edit" && homeEntity.IsTemplate == 0)
                    {
                        content.homeId = homeEntity.HomeId.ToString();
                        content.title = homeEntity.Title;
                    }
                    else
                    {
                        responseData.success = false;
                        responseData.msg = "该HomeId对应的数据有误";
                        return responseData.ToJSON();
                    }
                    content.sortActionJson = homeEntity.sortActionJson == null ? "" : homeEntity.sortActionJson;//返回排序数据
                    #region 广告集合 A 图片广告 B活动集合 C 商品分类和商品
                    var dsAd = adAreaBll.GetAdList(homeEntity.HomeId.ToString());//获取广告集合
                    if (dsAd != null && dsAd.Tables.Count > 0 && dsAd.Tables[0].Rows.Count > 0)
                    {
                        content.adList = DataTableToObject.ConvertToList<AdEntity>(dsAd.Tables[0]);
                    }
                    #endregion

                    #region 搜索框
                    var dsSearch = adAreaBll.GetMHSearchArea(homeEntity.HomeId.ToString());//获取搜索框
                    if (dsSearch != null && dsSearch.Tables.Count > 0 && dsSearch.Tables[0].Rows.Count > 0)
                    {
                        content.search = DataTableToObject.ConvertToObject<MHSearchAreaEntity>(dsSearch.Tables[0].Rows[0]);//转换成一个对象时，里面的参数不能是一个表，而是一行数据
                    }
                    #endregion

                    #region 活动集合
                    var bllCategoryAreaGroup = new MHCategoryAreaGroupBLL(this.CurrentUserInfo);

                    var allGroup = bllCategoryAreaGroup.QueryByEntity(new MHCategoryAreaGroupEntity { CustomerID = this.CurrentUserInfo.ClientID, HomeId = homeEntity.HomeId.ToString() }, null);

                    var eventGroup = allGroup.Where(a => a.ModelTypeId == 5 || a.ModelTypeId == 6 || a.ModelTypeId == 7 || a.ModelTypeId == 8);
                    foreach (var item in eventGroup)
                    {
                        var dsEvent = adAreaBll.GetEventInfoByGroupId(homeEntity.HomeId.ToString(), item.GroupId.ToString());//获取

                        if (dsEvent != null && dsEvent.Tables.Count > 0 && dsEvent.Tables[0].Rows.Count > 0)
                        {
                            var category = new EventListEntity();
                            var dsEventList = DataTableToObject.ConvertToList<EventAreaEntity>(dsEvent.Tables[0]);

                            category.groupId = item.GroupId;
                            category.showStyle = dsEventList.FirstOrDefault().showStyle;
                            category.shopType = dsEventList.FirstOrDefault().typeId;
                            category.displayIndex = item.DisplayIndex;
                            category.arrayList = dsEventList;


                            if (dsEventList.FirstOrDefault().areaFlag == "eventList")
                            {
                                category.areaFlag = "eventList";
                                content.eventList.Add(category);
                            }
                            if (dsEventList.FirstOrDefault().areaFlag == "secondKill")
                            {
                                category.areaFlag = "secondKill";
                                content.secondKill.Add(category);
                            }
                            if (dsEventList.FirstOrDefault().areaFlag == "groupBuy")
                            {
                                category.areaFlag = "groupBuy";
                                content.groupBuy.Add(category);
                            }
                            if (dsEventList.FirstOrDefault().areaFlag == "hotBuy")
                            {
                                category.areaFlag = "hotBuy";
                                content.hotBuy.Add(category);
                            }
                        }

                    }
                    #endregion

                    #region 分类集合

                    List<CategoryEntity> allList = new List<CategoryEntity>();
                    eventGroup = allGroup.Where(a => a.ModelTypeId != 5 && a.ModelTypeId != 6 && a.ModelTypeId != 7 && a.ModelTypeId != 8);

                    if (eventGroup != null )
                    {
                        foreach (var groupItem in eventGroup)
                        {
                            var category = new CategoryEntity
                            {
                                groupId = groupItem.GroupId.ToString(),//有个groupId
                                itemList = new List<ItemEntity>()
                            };
                            category.modelTypeId = groupItem.ModelTypeId;
                            category.modelTypeName = groupItem.ModelName;
                            category.styleType = groupItem.StyleType;
                            category.titleName = groupItem.TitleName;
                            category.titleStyle = groupItem.TitleStyle;
                            category.showCount = (int)groupItem.ShowCount;
                            category.showName = (int)groupItem.ShowName;
                            category.showPrice = (int)groupItem.ShowPrice;
                            category.showSalesPrice = (int)groupItem.ShowSalesPrice;
                            category.showDiscount = (int)groupItem.ShowDiscount;
                            category.showSalesQty = (int)groupItem.ShowSalesQty;
                            category.displayIndex = (int)groupItem.DisplayIndex;
                            category.CategoryAreaGroupId = (int)groupItem.GroupId;
                                
                            //根据groupId和HomeId来取MHCategoryArea
                            var dsItem = adAreaBll.GetItemList(category.groupId, homeEntity.HomeId.ToString());
                            if (dsItem != null && dsItem.Tables.Count > 0 && dsItem.Tables[0].Rows.Count > 0)
                            {
                        
                           
                                category.itemList = DataTableToObject.ConvertToList<ItemEntity>(dsItem.Tables[0]);
                            }
                            allList.Add(category);
                        }
                    }
                    //分类组合
                    List<CategoryEntity> lc = allList.Where(p => p.modelTypeId == 1).ToList();
                    if (lc != null && lc.Count != 0)
                    {
                        content.categoryEntrance = lc.OrderByDescending(p => p.groupId).ToList()[0]; ;
                    }
                    //商品列表
                    List<CategoryEntity> pList = allList.Where(p => p.modelTypeId == 2).ToList();
                    if (pList != null && pList.Count != 0)
                    {
                        content.productList = pList.OrderByDescending(p => p.groupId).ToList(); ;
                    }
                    
                    //创意组合
                    if (allList.Where(p => p.modelTypeId == 3).ToList() != null && allList.Where(p => p.modelTypeId == 3).ToList().Count != 0)
                    {
                        content.originalityList = allList.Where(p => p.modelTypeId == 3).ToList();

                    }
                    //导航
                    List<CategoryEntity> lc4 = allList.Where(p => p.modelTypeId == 4).ToList();
                    if (lc4 != null && lc4.Count != 0)
                    {
                        content.navList = lc4.OrderByDescending(p => p.groupId).ToList()[0]; ;//(获取唯一的)
                    }
                
                    #endregion

                    #region 关注信息

                    var bllFollow = new MHFollowBLL(CurrentUserInfo);
                    var entity = new MHFollowEntity();

                    var dsFollow = bllFollow.QueryByEntity(new MHFollowEntity() { HomeId = homeEntity.HomeId.ToString() }, null);
                    followInfo follow = new followInfo();
                    follow = dsFollow.Select(f => new followInfo()
                    {
                        HomeId = f.HomeId,
                        FollowId = f.FollowId.ToString(),
                        Title = f.Title,
                        TextId = f.TextId,
                        TextTitle=f.TextTitle,
                        Url = f.Url,
                        TypeId = f.TypeId
                    }).FirstOrDefault();
                    content.follow = follow;
                    #endregion
                    responseData.success = true;
                    responseData.data = content;
                    return responseData.ToJSON();
                }
                else
                {
                    responseData.success = false;
                    responseData.msg = "没有查询到对应首页主表的数据";
                    return responseData.ToJSON();
                }

            }
            catch (Exception ex)
            {
                responseData.success = false;
                responseData.msg = "没有查询到对应首页主表的数据_" + ex.ToString();
                return responseData.ToJSON();
            }

        }
        public class GetHomePageConfigInfoRespContentData
        {
            public string homeId { get; set; }
            public string title { get; set; }
            public IList<AdEntity> adList { get; set; }             //广告集合

            public IList<CategoryEntity> originalityList { get; set; } // 创意组合(老版本中的商品列表)  首页分类分组信息(分组8以外的)

            public IList<CategoryEntity> productList { get; set; }  //商品列表
            public CategoryEntity categoryEntrance { get; set; } // 分类组合   C8区分类分组信息(新增)
            public CategoryEntity navList { get; set; } //导航区域，c区模块4
            public MHSearchAreaEntity search { get; set; } //搜索框
            public IList<EventListEntity> eventList { get; set; }   //活动集合
            public IList<EventListEntity> secondKill { get; set; }   //秒杀区/抢购
            public IList<EventListEntity> groupBuy { get; set; }   //团购
            public IList<EventListEntity> hotBuy { get; set; }   //热销

            public followInfo follow { get; set; }//关注
            public string sortActionJson { get; set; } //整体排序字段

        }
        public class EventListEntity
        {
            public int? groupId { get; set; }
            public string areaFlag { get; set; }  //区域标识，eventList,secondKill
            public int shopType { get; set; }  //用与存放秒杀区的整体类型数据，便于前端获取。
            public int showStyle { get; set; }  //展示的样式
            public int? displayIndex { get; set; }
            public IList<EventAreaEntity> arrayList { get; set; }   //活动集合
        }

        public class MHSearchAreaEntity
        {
            public Guid? MHSearchAreaID { get; set; }

            public string imageUrl { get; set; }        //图片链接
            public string url { get; set; }
            public string styleType { get; set; }
            public string show { get; set; }
            public string titleName { get; set; }
            public string titleStyle { get; set; }




        }
        public class AdEntity
        {
            public Guid adId { get; set; }          //广告ID
            public string imageUrl { get; set; }    //图片链接
            public string url { get; set; }         //链接
            public int displayIndex { get; set; }   //序号
            public string objectId { get; set; }    //对象ID
            public string objectName { get; set; }  //对象名称
            public int typeId { get; set; }         //类型ID： 1=活动 2=资讯 3=商品 4=门店
        }
        public class EventAreaEntity
        {
            public Guid eventAreaItemId { get; set; }   //活动区域项ID
            public string areaFlag { get; set; }             //eventList,secondKill
            public int typeId { get; set; }             //1=疯狂团购  2=限时抢购  3=热销榜单
            public int showStyle { get; set; }             //显示方式 1=大图  2=2小图  3=1大图2小图
            public Guid eventId { get; set; }           //活动ID
            public string eventName { get; set; }           //活动名称
            public string itemId { get; set; }          //商品ID
            public int displayIndex { get; set; }       //序号
            public string itemName { get; set; }        //商品名称
            public string imageUrl { get; set; }        //图片链接
            public int qty { get; set; }                //抢购数量
            public decimal price { get; set; }          //商品价格
            public decimal salesPrice { get; set; }     //抢购价格
            public decimal discountRate { get; set; }   //折扣
            public int remainingSec { get; set; }       //剩余时间(秒)
        }
        public class CategoryEntity
        {
            public int? CategoryAreaGroupId { get; set; }
            public int? modelTypeId { get; set; }
            public string modelTypeName { get; set; }
            public string groupId { get; set; }          //分组ID
            public IList<ItemEntity> itemList { get; set; }   //商品集合

            public String styleType { get; set; }//显示方式 1=大图  2=2小图  3=1大图2小图  4=详细列表

            public String titleName { get; set; }

            public String titleStyle { get; set; }

            public int showCount { get; set; }
            public int showName { get; set; }
            public int showPrice { get; set; }
            public int showSalesPrice { get; set; }
            public int showSalesQty { get; set; }
            public int showDiscount { get; set; }
            public int displayIndex { get; set; }

        }
        public class ItemEntity
        {
            public Guid categoryAreaId { get; set; }    //商品类别区域ID
            public int displayIndex { get; set; }       //序号
            public string imageUrl { get; set; }        //图片链接
            public string objectId { get; set; }        //对象ID
            public string objectName { get; set; }      //对象名称
            public int typeId { get; set; }             //类型ID： 1=商品分类 2=商品
            public string navName { get; set; }        //导航里各个小图片下面的文字
            public string url { get; set; }
            public int GroupId  { get; set; }
        }

        public class GetMHCategoryAreaInfoRespContentData
        {
            public IList<CategoryEntity> categoryList { get; set; } //分类集合
        }
        #endregion

        #region SaveItemCategory 保存商品分类

        /// <summary>
        /// 保存商品分类
        /// </summary>
        public string SaveItemCategory()
        {
            var responseData = new ResponseData();

            int modelTypeId = (Int32)Utils.GetIntVal(FormatParamValue(Request("modelTypeId")));//模块ID，含有1,3,8,4这样的1：分类导航2：商品列表3：创意组合4：导航
            string modelTypeName = FormatParamValue(Request("modelTypeName"));//模块名称
            string _styleType = FormatParamValue(Request("styleType"));//样式
            string _titleName = FormatParamValue(Request("titleName"));//标题名称
            string _titleStyle = FormatParamValue(Request("titleStyle"));//标题样式
            int _showCount = (Int32)Utils.GetIntVal(FormatParamValue(Request("ShowCount")));//标题样式	
            int _showName = (Int32)Utils.GetIntVal(FormatParamValue(Request("ShowName")));//是否显示商品名称	
            int _showPrice = (Int32)Utils.GetIntVal(FormatParamValue(Request("ShowPrice")));//是否显示商品价格	
            int _showDiscount = (Int32)Utils.GetIntVal(FormatParamValue(Request("ShowDiscount")));//是否显示商品折扣
            int _displayIndex = (Int32)Utils.GetIntVal(FormatParamValue(Request("DisplayIndex")));//排序
            int _showSalesPrice = (Int32)Utils.GetIntVal(FormatParamValue(Request("ShowSalesPrice")));//是否显示售价
            int _showSalesQty = (Int32)Utils.GetIntVal(FormatParamValue(Request("ShowSalesQty")));//是否显示销量
            int? groupId = (Int32)Utils.GetIntVal(Request("CategoryAreaGroupId"));//分组Id



            if (modelTypeId == 0)
            {
                responseData.success = false;
                responseData.msg = "模板ID不能为空";
                return responseData.ToJSON();
            }

            if (string.IsNullOrEmpty(modelTypeName))
            {
                responseData.success = false;
                responseData.msg = "模板名称不能为空";
                return responseData.ToJSON();
            }

            //集合数据
            var itemCategory = this.CurrentContext.Request["categoryList"].DeserializeJSONTo<List<ItemCategoryEntity>>();
            var adAreaBll = new MHAdAreaBLL(this.CurrentUserInfo);
            var categoryAreaBll = new MHCategoryAreaBLL(this.CurrentUserInfo);
            var homeBll = new MobileHomeBLL(this.CurrentUserInfo);
            var homeList = homeBll.QueryByEntity(new MobileHomeEntity { CustomerId = this.CurrentUserInfo.ClientID, HomeId = new Guid(this.CurrentContext.Request["homeId"].ToString()) }, null);
            var homeId = homeList.FirstOrDefault().HomeId;//HomeId
            if (homeList == null && homeList.Length == 0)
            {
                responseData.success = false;
                responseData.msg = "没有查询到对应首页主表的数据";
                return responseData.ToJSON();
            }
            if (itemCategory != null && itemCategory.Count > 0)//防错判断
            {
                //查找大块的信息
                var mHCategoryAreaGroup = new MHCategoryAreaGroupBLL(this.CurrentUserInfo);
                var categoryAreaGroup = mHCategoryAreaGroup.QueryByEntity(new MHCategoryAreaGroupEntity()   //QueryByEntity取出来的是一个集合
                {
                    GroupId = groupId,
                    CustomerID = this.CurrentUserInfo.ClientID,
                    HomeId = this.CurrentContext.Request["homeId"].ToString()
                }, null);//根据GroupValue取值，查找出相关信息


                var categoryAreaGroupEntity = new MHCategoryAreaGroupEntity()  //这里直接用传request穿过来的参数了
                {
                    ModelTypeId = modelTypeId,
                    ModelName = modelTypeName,
                    GroupValue = groupId,
                    CustomerID = this.CurrentUserInfo.ClientID,
                    TitleName = _titleName,
                    TitleStyle = _titleStyle,
                    StyleType = _styleType,
                    ShowCount = _showCount,
                    ShowName = _showName,
                    ShowPrice = _showPrice,
                    ShowDiscount = _showDiscount,
                    ShowSalesPrice = _showSalesPrice,
                    ShowSalesQty = _showSalesQty,
                    DisplayIndex = _displayIndex,
                    HomeId = homeId.ToString()

                };
                if (categoryAreaGroup == null || !categoryAreaGroup.Any())
                {
                    mHCategoryAreaGroup.Create(categoryAreaGroupEntity);//创建
                }
                else
                {
                    categoryAreaGroupEntity.GroupId = categoryAreaGroup[0].GroupId;
                    mHCategoryAreaGroup.Update(categoryAreaGroupEntity);//修改
                }


                //分组ID

                
                groupId = categoryAreaGroupEntity.GroupId;
                var entityMHCategoryArea = new MHCategoryAreaEntity();
                var bllMHCategoryArea=new MHCategoryAreaBLL(CurrentUserInfo);
                entityMHCategoryArea = bllMHCategoryArea.QueryByEntity(new MHCategoryAreaEntity() { GroupID = groupId }, null).FirstOrDefault();
                #region
                if (entityMHCategoryArea==null)
                {

                    InsertItemCategory(itemCategory, groupId, homeId.ToString());
                }
                else
                {
                    UpdateItemCategory(itemCategory, homeId.ToString(),groupId);
                }


                #endregion

                #region 返回商品分类数据

                var content = new CategoryEntity();  //商品分类

                //获取分组ID
                content.modelTypeId = modelTypeId;
                content.modelTypeName = modelTypeName;
                content.groupId = groupId.ToString();   
                content.styleType = _styleType;
                content.titleName = _titleName;
                content.titleStyle = _titleStyle;
                content.showCount = _showCount;
                content.showName = _showName;
                content.showPrice = _showPrice;
                content.showSalesPrice = _showSalesPrice;
                content.showSalesQty = _showSalesQty;
                content.showDiscount = _showDiscount;

                content.itemList = new List<ItemEntity>();

                var dsItem = adAreaBll.GetItemList(content.groupId, homeId.ToString());
                if (dsItem != null && dsItem.Tables.Count > 0 && dsItem.Tables[0].Rows.Count > 0)
                {
                    content.itemList = DataTableToObject.ConvertToList<ItemEntity>(dsItem.Tables[0]);
                }
                else
                {
                    content.itemList = new List<ItemEntity>();//表里没有数据就返回一个空
                }

                #endregion

                responseData.success = true;
                responseData.data = content;
                return responseData.ToJSON();


            }
            //else
            //{

            //    DeleteItemCategoryByGroupIdandHomeID((int)groupId, (Guid)homeId);//删除小块的内容
            //    //删除大块的内容
            //    DeleteCategoryGroupByGroupIdandCustomerId((int)groupId, this.CurrentUserInfo.ClientID,homeId.ToString());//删除大块的内容,,CustomerId = this.CurrentUserInfo.ClientID

            //    // responseData.success = false;
            //    responseData.success = true;
            //    responseData.data = "查无数据";
            //    return responseData.ToJSON();
            //}


            return responseData.ToJSON();


        }

        public void InsertItemCategory(List<ItemCategoryEntity> itemCategory, int? groupId, string strHomeId)
        {
            var categoryAreaBll = new MHCategoryAreaBLL(this.CurrentUserInfo);
            //var homeBll = new MobileHomeBLL(this.CurrentUserInfo);
            //var homeList = homeBll.QueryByEntity(new MobileHomeEntity { CustomerId = this.CurrentUserInfo.ClientID }, null);
            //var homeId = homeList.FirstOrDefault().HomeId;
            foreach (var item in itemCategory)
            {
                //新增
                var entity = new MHCategoryAreaEntity()
                {
                    CategoryAreaId = Guid.NewGuid(),//创建guid
                    HomeId = new Guid(strHomeId),
                    ObjectId = item.objectId,
                    ObjectTypeId = item.typeId,
                    GroupID = groupId,
                    ObjectName = item.objectName,
                    ImageUrlObject = item.imageUrl,
                    DisplayIndex = item.displayIndex,
                    navName = item.navName,
                    url = item.url
                };
                categoryAreaBll.Create(entity);

            }
        }

        public void UpdateItemCategory(List<ItemCategoryEntity> itemCategory, string strHomeId, int? intGroupId)
        {
            var adAreaBll = new MHAdAreaBLL(this.CurrentUserInfo);
            foreach (var item in itemCategory)
            {
                //更新
                var entity = new MHCategoryAreaEntity()
                {
                    CategoryAreaId = Guid.Parse(item.categoryAreaId),
                    HomeId = new Guid(strHomeId),
                    ObjectId = item.objectId,
                    ObjectTypeId = item.typeId,
                    GroupID = intGroupId,
                    ObjectName = item.objectName,
                    ImageUrlObject = item.imageUrl,
                    DisplayIndex = item.displayIndex,
                    navName = item.navName,
                    url = item.url
                };
                adAreaBll.UpdateMHCategoryArea(entity);
            }
        }

        public void DeleteItemCategoryByGroupIdandHomeID(int GroupID, Guid HomeId)
        {
            var adAreaBll = new MHAdAreaBLL(this.CurrentUserInfo);
            adAreaBll.DeleteItemCategoryByGroupIdandHomeID(GroupID, HomeId); ;

        }
        public void DeleteCategoryGroupByGroupIdandCustomerId(int GroupID, string customerId, string strHomeId)
        {
            var adAreaBll = new MHAdAreaBLL(this.CurrentUserInfo);
            adAreaBll.DeleteCategoryGroupByGroupIdandCustomerId(GroupID, customerId, strHomeId); //删除大块
        }

        public class SaveItemCategoryReqContentData
        {
            public int? modelTypeId { get; set; }
            public string modelTypeName { get; set; }
            public IList<ItemCategoryEntity> categoryList { get; set; } //分类集合

        }
        public class ItemCategoryEntity
        {
            public int typeId { get; set; }             //类型ID： 1=商品分类 2=商品  3 =自定义链接（资讯） ，8=全部分类 
            public string categoryAreaId { get; set; }  //商品类别区域ID
            public string objectId { get; set; }        //对象ID
            public string objectName { get; set; }      //对象名称
            public int? groupId { get; set; }            //分组ID（1、2、3…….）
            public int displayIndex { get; set; }       //序号（1、2、3）
            public string imageUrl { get; set; }        //图片链接
            public string navName { get; set; }        //导航里各个小图片下面的文字 
            public string url { get; set; }        //导航里各个小图片下面的文字 
        }

        #endregion

        #region 保存首页图片广告

        public class SaveMHAdArea
        {
            public Guid? adId { get; set; }
            public int? typeId { get; set; }  //类型ID： 1=活动 2=资讯 3=商品 4=门店
            public Guid? homeId { get; set; }
            public string objectId { get; set; }        //对象ID

            //  public int? objectTypeId { get; set; }
            public int displayIndex { get; set; }       //序号（1、2、3）
            public string imageUrl { get; set; }        //图片链接
            public string url { get; set; }
        }

        public string SaveAds()
        {
            var responseData = new ResponseData();
            var itemAds = this.CurrentContext.Request["adList"].DeserializeJSONTo<List<SaveMHAdArea>>();

            #region
            if (itemAds != null)
            {
                var adAreaBll = new MHAdAreaBLL(this.CurrentUserInfo);
                var homeBll = new MobileHomeBLL(this.CurrentUserInfo);
                var homeList = homeBll.QueryByEntity(new MobileHomeEntity { CustomerId = this.CurrentUserInfo.ClientID, HomeId = new Guid(this.CurrentContext.Request["homeId"].ToString()) }, null);

                string customerId = this.CurrentUserInfo.CurrentUser.customer_id;
                if (homeList != null && homeList.Length > 0)
                {
                    var homeId = homeList.FirstOrDefault().HomeId;

                    string adsIdList = itemAds.Where(item => !string.IsNullOrEmpty(item.adId.ToString())).Aggregate("", (current, item) => current + "'" + item.adId.ToString() + "',");
                    //根据AdAreaId删除MHAdArea中旧数据（not in adsList）
                    var itemCategoryService = new ItemCategoryService(this.CurrentUserInfo);
                    if (adsIdList != "")
                    {
                        itemCategoryService.UpdateMHAdAreaData(adsIdList, customerId);//其实是把id不在这个列表里面的给更新删除掉
                    }
                    else
                    {
                        itemCategoryService.DeleteMHAdAreaData(customerId);
                    }

                    //根据AdAreaId判断是新增还是更新MHAdArea数据
                    foreach (var item in itemAds)
                    {
                        if (string.IsNullOrEmpty(item.adId.ToString()))
                        {
                            var entity = new MHAdAreaEntity()
                            {
                                AdAreaId = Guid.NewGuid(),
                                HomeId = homeId,
                                ImageUrl = item.imageUrl,
                                ObjectId = item.objectId,
                                ObjectTypeId = item.typeId,
                                DisplayIndex = item.displayIndex,
                                Url = item.url
                            };
                            adAreaBll.Create(entity);
                        }
                        else
                        {
                            var entity = new MHAdAreaEntity()
                            {
                                AdAreaId = item.adId,
                                HomeId = homeId,
                                ImageUrl = item.imageUrl,
                                ObjectId = item.objectId,
                                ObjectTypeId = item.typeId,
                                DisplayIndex = item.displayIndex,
                                Url = item.url
                            };
                            adAreaBll.Update(entity);
                        }
                    }
                }
            }
            #endregion
            responseData.success = true;
            responseData.msg = "更新成功";
            return responseData.ToJSON();
        }
        #endregion

        #region 保存搜索区域

        public class SaveMHSearchArea
        {
            public Guid? MHSearchAreaID { get; set; }
            public Guid? homeId { get; set; }
            public string imageUrl { get; set; }        //图片链接
            public string url { get; set; }
            public string styleType { get; set; }
            public string show { get; set; }
            public string titleName { get; set; }
            public string titleStyle { get; set; }

        }

        public string SaveSeach()
        {
            var responseData = new ResponseData();
            var itemMHSearchArea = this.CurrentContext.Request["seach"].DeserializeJSONTo<SaveMHSearchArea>();//转换成

            #region
            if (itemMHSearchArea != null)
            {
                var mHSeachAreaBLL = new MHSeachAreaBLL(this.CurrentUserInfo);
                var homeBll = new MobileHomeBLL(this.CurrentUserInfo);
                var homeList = homeBll.QueryByEntity(new MobileHomeEntity { CustomerId = this.CurrentUserInfo.ClientID, HomeId = new Guid(this.CurrentContext.Request["homeId"].ToString()) }, null);

                string customerId = this.CurrentUserInfo.CurrentUser.customer_id;
                if (homeList != null && homeList.Length > 0)
                {
                    var homeId = homeList.FirstOrDefault().HomeId;
                    //比较底层的求和方式
                    //string adsIdList = itemAds.Where(item => !string.IsNullOrEmpty(item.adId.ToString())).Aggregate("", (current, item) => current + "'" + item.adId.ToString() + "',");
                    //根据AdAreaId删除MHAdArea中旧数据（not in adsList） 
                    //var itemCategoryService = new ItemCategoryService(this.CurrentUserInfo);
                    //if (itemMHSearchArea.MHSearchAreaID != null)//不为null
                    //{
                    //    itemCategoryService.UpdateMHSearchAreaData((Guid)itemMHSearchArea.MHSearchAreaID, customerId);
                    //}
                    //else
                    //{
                    //    itemCategoryService.DeleteMHSearchAreaData(customerId);
                    //}

                    //根据AdAreaId判断是新增还是更新MHAdArea数据
                    if (string.IsNullOrEmpty(itemMHSearchArea.MHSearchAreaID.ToString()))
                    {
                        var entity = new MHSeachAreaEntity()
                        {
                            MHSearchAreaID = Guid.NewGuid(),
                            HomeId = homeId,
                            ImageUrl = itemMHSearchArea.imageUrl,
                            Url = itemMHSearchArea.url,
                            styleType = itemMHSearchArea.styleType,
                            show = itemMHSearchArea.show,
                            titleName = itemMHSearchArea.titleName,
                            titleStyle = itemMHSearchArea.titleStyle
                        };
                        mHSeachAreaBLL.Create(entity);
                    }
                    else
                    {
                        var entity = new MHSeachAreaEntity()
                        {
                            MHSearchAreaID = itemMHSearchArea.MHSearchAreaID,
                            HomeId = homeId,
                            ImageUrl = itemMHSearchArea.imageUrl,
                            Url = itemMHSearchArea.url,
                            styleType = itemMHSearchArea.styleType,
                            show = itemMHSearchArea.show,
                            titleName = itemMHSearchArea.titleName,
                            titleStyle = itemMHSearchArea.titleStyle
                        };
                        mHSeachAreaBLL.Update(entity);
                    }
                }

            }
            #endregion
            responseData.success = true;
            responseData.msg = "操作成功";
            return responseData.ToJSON();
        }
        #endregion

        #region  保存商品分类（团购）区域

        public class SaveItemArea
        {
            public Guid? itemAreaId { get; set; }
            public string itemId { get; set; }
            public Guid? eventId { get; set; }
            public int? isUrl { get; set; }
            public int? displayIndex { get; set; }
            public string imageUrl { get; set; }
        }
        public string SaveEventItemArea()
        {
            var responseData = new ResponseData();

            try
            {
                if (this.CurrentContext.Request["areaFlag"] == null)
                {
                    responseData.success = false;
                    responseData.msg = "areaFlag参数有误";

                    return responseData.ToJSON();
                }
                if (this.CurrentContext.Request["ShowStyle"] == null)
                {
                    responseData.success = false;
                    responseData.msg = "ShowStyle参数有误";

                    return responseData.ToJSON();
                }
                if (this.CurrentContext.Request["DisplayIndex"] == null)
                {
                    responseData.success = false;
                    responseData.msg = "DisplayIndex参数有误";

                    return responseData.ToJSON();
                }
                var itemArea = this.CurrentContext.Request["eventItemList"].DeserializeJSONTo<List<SaveItemArea>>();


                var _areaFlag = this.CurrentContext.Request["areaFlag"].ToString().Trim();
                string strShowStyle = this.CurrentContext.Request["ShowStyle"].ToString().Trim();

                #region
                if (itemArea != null)
                {
                    var itemAreaBll = new MHItemAreaBLL(this.CurrentUserInfo);
                    var homeBll = new MobileHomeBLL(this.CurrentUserInfo);
                    string customerId = this.CurrentUserInfo.CurrentUser.customer_id;
                    var homeId = new Guid(this.CurrentContext.Request["homeId"].ToString());
                    var bllCategoryAreaGroup = new MHCategoryAreaGroupBLL(this.CurrentUserInfo);

                    //var categoryAreaGroup = bllCategoryAreaGroup.QueryByEntity(new MHCategoryAreaGroupEntity()   //QueryByEntity取出来的是一个集合
                    // {

                    //     GroupId =(string.IsNullOrEmpty(this.CurrentContext.Request["groupId"].ToString()) == true ? 0 : Convert.ToInt32(this.CurrentContext.Request["groupId"].ToString())),
                    //     CustomerID = this.CurrentUserInfo.ClientID,
                    //     HomeId=this.CurrentContext.Request["homeId"].ToString()
                    // }, null);

                    var categoryAreaGroupEntity = new MHCategoryAreaGroupEntity()
                    {
                        ModelTypeId = Convert.ToInt32(this.CurrentContext.Request["modelTypeId"].ToString()),
                        ModelName = this.CurrentContext.Request["modelTypeName"].ToString(),
                        CustomerID = this.CurrentUserInfo.ClientID,
                        DisplayIndex = Convert.ToInt32(this.CurrentContext.Request["DisplayIndex"].ToString()),
                        HomeId = homeId.ToString()

                    };
                    if (this.CurrentContext.Request["groupId"] == null || this.CurrentContext.Request["groupId"].ToString() == "")
                    {
                        bllCategoryAreaGroup.Create(categoryAreaGroupEntity);//创建
                    }
                    else
                    {
                        var categoryAreaGroup = bllCategoryAreaGroup.QueryByEntity(new MHCategoryAreaGroupEntity()   //QueryByEntity取出来的是一个集合
                        {

                            GroupId = Convert.ToInt32(this.CurrentContext.Request["groupId"].ToString()),
                            CustomerID = this.CurrentUserInfo.ClientID,
                            HomeId = this.CurrentContext.Request["homeId"].ToString()
                        }, null);

                        categoryAreaGroupEntity.GroupId = categoryAreaGroup[0].GroupId;
                        bllCategoryAreaGroup.Update(categoryAreaGroupEntity);//修改
                    }


                    //根据ItemAreaId判断是新增还是更新MHItemArea数据
                    foreach (var item in itemArea)
                    {
                        var entity = new MHItemAreaEntity()
                        {

                            HomeId = homeId,
                            IsUrl = item.isUrl,
                            EventId = item.eventId,
                            ItemId = item.itemId,
                            areaFlag = _areaFlag,//所属区域
                            DisplayIndex = item.displayIndex,
                            ShowStyle = Convert.ToInt32(strShowStyle),
                            ItemImageUrl = item.imageUrl == null ? "" : item.imageUrl


                        };
                        if (string.IsNullOrEmpty(item.itemAreaId.ToString()))
                        {
                            entity.GroupId = categoryAreaGroupEntity.GroupId;
                            entity.ItemAreaId = Guid.NewGuid();
                            itemAreaBll.Create(entity);
                            responseData.msg = "操作成功";

                        }
                        else
                        {
                            entity.ItemAreaId = item.itemAreaId;
                            itemAreaBll.Update(entity);
                            responseData.msg = "更新成功";
                        }
                    }
                }
                #endregion

                responseData.success = true;
            }
            catch (Exception ex)
            {
                responseData.success = true;
                responseData.msg = ex.Message.ToString();
                throw;
            }
            return responseData.ToJSON();
        }
        #endregion

        #region 删除模块

        public string DeleteItemCategoryArea()
        {
            var responseData = new ResponseData();

            var groupId = FormatParamValue(Request("groupId"));
            var homeId = FormatParamValue(Request("homeId"));

            var itemCategoryService = new ItemCategoryService(this.CurrentUserInfo);
            itemCategoryService.DeleteItemCategoryAreaData(groupId, homeId);
            itemCategoryService.DeleteItemCategoryAreaGroupData(groupId, homeId);
            itemCategoryService.DeleteItemAreaData(groupId, homeId);
            responseData.success = true;
            responseData.msg = "删除成功";
            return responseData.ToJSON();
        }

        #endregion




        #region 获取活动分组/活动商品列表
        /// <summary>
        /// 获取活动列表
        /// </summary>
        /// <returns></returns>
        public string GetPanicbuyingEventList()
        {
            var responseData = new ResponseData();

            //请求参数
            var eventTypeID = FormatParamValue(Request("eventTypeId"));
            var pageIndex = Utils.GetIntVal(FormatParamValue(Request("pageIndex")));
            var pageSize = Utils.GetIntVal(FormatParamValue(Request("pageSize")));


            if (pageSize <= 0) pageSize = 15;

            var eventBll = new PanicbuyingEventBLL(this.CurrentUserInfo);

            var content = new PanicbuyingEventList();  //活动商品集合

            try
            {
                List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
                if (Request("eventTypeId") != null && Request("eventTypeId").ToString() != "")
                {
                    complexCondition.Add(new EqualsCondition() { FieldName = "EventTypeId", Value = eventTypeID });
                    complexCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = this.CurrentUserInfo.ClientID });
                }
                else
                {
                    responseData.success = false;
                    responseData.msg = "参数eventTypeId有误";
                    return responseData.ToJSON();
                }
                //排序参数
                List<OrderBy> lstOrder = new List<OrderBy> { };
                lstOrder.Add(new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc });

                var tempEvent = eventBll.GetPanicbuyingEventList(complexCondition.ToArray(), lstOrder.ToArray(), pageSize == 0 ? 5 : pageSize, pageIndex == 0 ? pageIndex + 1 : pageIndex);

                List<PanicbuyingEvent> eventList = new List<PanicbuyingEvent> { };
                eventList.AddRange(tempEvent.Entities.Select(e => new PanicbuyingEvent()
                {
                    EventName = e.EventName,
                    EventId = e.EventId.ToString(),
                    EventTypeId = e.EventTypeId
                }));

                content.eventList = eventList;
                content.totalCount = eventList.Count;

                responseData.success = true;
                responseData.data = content;
                return responseData.ToJSON();
            }
            catch (Exception ex)
            {
                responseData.success = false;
                responseData.msg = ex.ToString();
                return responseData.ToJSON();
            }
        }
        /// <summary>
        /// 根据EventId获取商品
        /// </summary>
        /// <returns></returns>
        public string GetItemAreaByEventID()
        {
            var responseData = new ResponseData();

            //请求参数
            var eventId = FormatParamValue(Request("eventId"));
            var pageIndex = Utils.GetIntVal(FormatParamValue(Request("pageIndex")));
            var pageSize = Utils.GetIntVal(FormatParamValue(Request("pageSize")));


            if (pageSize <= 0) pageSize = 15;

            var itemService = new ItemService(this.CurrentUserInfo);
            var content = new GetItemAreaListRespContentData();  //活动商品集合

            try
            {
                var ds = itemService.GetItemListByEventId(this.CurrentUserInfo.ClientID, eventId, pageIndex, pageSize);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    content.itemList = DataTableToObject.ConvertToList<GetItemAreaListEntity>(ds.Tables[0]);
                    content.totalCount = Convert.ToInt32(ds.Tables[1].Rows[0]["TotalCount"].ToString());
                }

                responseData.success = true;
                responseData.data = content;
                return responseData.ToJSON();
            }
            catch (Exception ex)
            {
                responseData.success = false;
                responseData.msg = ex.ToString();
                return responseData.ToJSON();
            }
        }
        public string GetItemAreaByEventTypeID()
        {
            var responseData = new ResponseData();

            //请求参数
            var eventTypeID = FormatParamValue(Request("eventTypeID"));
            var pageIndex = Utils.GetIntVal(FormatParamValue(Request("pageIndex")));
            var pageSize = Utils.GetIntVal(FormatParamValue(Request("pageSize")));


            if (pageSize <= 0) pageSize = 15;

            var itemService = new ItemService(this.CurrentUserInfo);
            var content = new GetItemAreaListRespContentData();  //活动商品集合

            try
            {
                var ds = itemService.GetItemAreaList(this.CurrentUserInfo.ClientID, eventTypeID, pageIndex, pageSize);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    content.itemList = DataTableToObject.ConvertToList<GetItemAreaListEntity>(ds.Tables[0]);
                    content.totalCount = itemService.GetItemAreaListCount(this.CurrentUserInfo.ClientID, eventTypeID);
                }

                responseData.success = true;
                responseData.data = content;
                return responseData.ToJSON();
            }
            catch (Exception ex)
            {
                responseData.success = false;
                responseData.msg = ex.ToString();
                return responseData.ToJSON();
            }
        }
        public class PanicbuyingEventList
        {
            public int totalCount { get; set; }     // 总数量
            public IList<PanicbuyingEvent> eventList { get; set; }  //商品集合
        }
        public class PanicbuyingEvent
        {
            public string EventId { get; set; }
            public string EventName { get; set; }

            public int? EventTypeId { get; set; }

        }
        public class GetItemAreaListRespContentData
        {
            public int totalCount { get; set; }     // 总数量
            public IList<GetItemAreaListEntity> itemList { get; set; }  //商品集合
        }
        public class GetItemAreaListEntity
        {
            public string itemId { get; set; }    //商品类别名称
            public string itemName { get; set; }          //
            public string imageUrl { get; set; }
            public Guid? eventId { get; set; }
            public decimal salesPrice { get; set; }
            public decimal Price { get; set; }
            public Int64 displayIndex { get; set; }	    //序号
        }
        #endregion

        #region 调整商品分类活动的区域
        public string UpdateMHCategoryAreaByGroupId()
        {
            var responseData = new ResponseData();

            //请求参数
            var groupIDFrom = FormatParamValue(Request("groupIDFrom"));
            var groupIDTo = Utils.GetIntVal(FormatParamValue(Request("groupIDTo")));
            var itemService = new ItemService(this.CurrentUserInfo);

            if (Convert.ToInt32(groupIDFrom) == Convert.ToInt32(groupIDTo))
            {
                responseData.success = false;
                responseData.msg = "位置相同，不能移动";
                return responseData.ToJSON();
            }
            else
            {
                //update MHItemArea groupid
                itemService.UpdateMHCategoryAreaByGroupId(this.CurrentUserInfo.ClientID, Convert.ToInt32(groupIDFrom), Convert.ToInt32(groupIDTo));
                //get MHItemArea info 

                var content = new GetMHCategoryAreaInfoRespContentData { categoryList = new List<CategoryEntity>() };
                try
                {
                    var homeBll = new MobileHomeBLL(this.CurrentUserInfo);
                    var homeList = homeBll.QueryByEntity(new MobileHomeEntity { CustomerId = this.CurrentUserInfo.ClientID, HomeId = new Guid(this.CurrentContext.Request["homeId"].ToString()) }, null);
                    if (homeList != null && homeList.Length > 0)
                    {
                        var homeEntity = homeList.FirstOrDefault();
                        var adAreaBll = new MHAdAreaBLL(this.CurrentUserInfo);
                        var dsGroup = adAreaBll.GetCategoryGroupId(homeEntity.HomeId.ToString());

                        if (dsGroup != null && dsGroup.Tables.Count > 0 && dsGroup.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in dsGroup.Tables[0].Rows)
                            {
                                var category = new CategoryEntity
                                {
                                    groupId = dr[0].ToString(),
                                    itemList = new List<ItemEntity>()
                                };

                                var dsItem = adAreaBll.GetItemList(category.groupId, homeEntity.HomeId.ToString());
                                if (dsItem != null && dsItem.Tables.Count > 0 && dsItem.Tables[0].Rows.Count > 0)
                                {
                                    category.itemList = DataTableToObject.ConvertToList<ItemEntity>(dsItem.Tables[0]);
                                }
                                content.categoryList.Add(category);
                            }
                        }
                    }



                    responseData.success = true;
                    responseData.data = content;
                    return responseData.ToJSON();
                }
                catch (Exception ex)
                {
                    responseData.success = false;
                    responseData.msg = ex.ToString();
                    return responseData.ToJSON();
                }
            }
        }
        #endregion

        #region 获取资讯类别列表

        public class GetLNewsTypeListRespContentData
        {
            public List<GetLNewsTypeListEntity> newsTypeList { get; set; }
        }
        public class GetLNewsTypeListEntity
        {
            public Guid? newsTypeId { get; set; }
            public string newsTypeName { get; set; }
        }
        public string GetLNewsTypeList()
        {
            var responseData = new ResponseData();

            var itemservice = new ItemService(this.CurrentUserInfo);
            var content = new GetLNewsTypeListRespContentData();  //咨询类别集合

            try
            {
                var ds = itemservice.GetLNewsTypeList(this.CurrentUserInfo.ClientID);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    content.newsTypeList = DataTableToObject.ConvertToList<GetLNewsTypeListEntity>(ds.Tables[0]);
                }

                responseData.success = true;
                responseData.data = content;
                return responseData.ToJSON();
            }
            catch (Exception ex)
            {
                responseData.success = false;
                responseData.msg = ex.ToString();
                return responseData.ToJSON();
            }
        }
        #endregion

        #region 获取资讯列表

        public class GetLNewsListRespContentData
        {
            public List<GetLNewsListEntity> newsList { get; set; }
        }
        public class GetLNewsListEntity
        {
            public string newTitle { get; set; }
            public string publishTime { get; set; }
            public string createName { get; set; }
        }
        public string GetLNewsList()
        {
            var responseData = new ResponseData();

            //请求参数
            var newsTypeId = FormatParamValue(Request("newsTypeId"));
            var publishTimeFrom = Utils.GetIntVal(FormatParamValue(Request("publishTimeFrom")));
            var publishTimeTo = Utils.GetIntVal(FormatParamValue(Request("publishTimeTo")));
            var newsTitle = Utils.GetIntVal(FormatParamValue(Request("newsTitle")));

            var itemservice = new ItemService(this.CurrentUserInfo);
            var content = new GetLNewsListRespContentData();  //咨询类别集合

            try
            {
                var ds = itemservice.GetLNewsList(this.CurrentUserInfo.ClientID, newsTypeId.ToString(), publishTimeFrom.ToString(), publishTimeTo.ToString(), newsTitle.ToString());
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    content.newsList = DataTableToObject.ConvertToList<GetLNewsListEntity>(ds.Tables[0]);
                }

                responseData.success = true;
                responseData.data = content;
                return responseData.ToJSON();
            }
            catch (Exception ex)
            {
                responseData.success = false;
                responseData.msg = ex.ToString();
                return responseData.ToJSON();
            }
        }
        #endregion

        #region 获取活动列表
        public class GetLEventsListRespContentData
        {
            public List<GetLEventsListEntity> newsList { get; set; }
        }
        public class GetLEventsListEntity
        {
            public Guid? eventId { get; set; }
            public string title { get; set; }
            public string eventTypeId { get; set; }
            public string eventBeginTime { get; set; }
            public string eventEndTime { get; set; }
            public Guid? cityId { get; set; }
            public string cityName { get; set; }
            public int? displayIndex { get; set; }
        }
        public string GetEventList()
        {
            var responseData = new ResponseData();

            var eventTypeId = FormatParamValue(Request("eventTypeId"));
            var title = Utils.GetIntVal(FormatParamValue(Request("title")));
            var eventBeginTime = Utils.GetIntVal(FormatParamValue(Request("eventBeginTime")));
            var eventEndTime = Utils.GetIntVal(FormatParamValue(Request("eventEndTime")));

            var itemservice = new ItemService(this.CurrentUserInfo);
            var content = new GetLEventsListRespContentData();

            try
            {
                var ds = itemservice.GetLEventsList(this.CurrentUserInfo.ClientID, eventTypeId.ToString(), title.ToString(), eventBeginTime.ToString(), eventEndTime.ToString());
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    content.newsList = DataTableToObject.ConvertToList<GetLEventsListEntity>(ds.Tables[0]);
                }

                responseData.success = true;
                responseData.data = content;
                return responseData.ToJSON();
            }
            catch (Exception ex)
            {
                responseData.success = false;
                responseData.msg = ex.ToString();
                return responseData.ToJSON();
            }
        }
        #endregion

    }
}