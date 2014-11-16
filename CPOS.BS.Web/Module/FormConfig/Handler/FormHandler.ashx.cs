using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Common;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;

namespace JIT.CPOS.BS.Web.Module.FormConfig.Handler
{
    /// <summary>
    /// FormHandler
    /// </summary>
    public class FormHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
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
                case "GetItemList":             //商品查询
                    content = GetItemList();
                    break;
                case "GetHomePageConfigInfo":   //获取客户端首页所有配置信息
                    content = GetHomePageConfigInfo();
                    break;
                case "SaveItemCategory":        //保存商品分类
                    content = SaveItemCategory();
                    break;
                case "SaveEventItemArea":
                    content = SaveEventItemArea();
                    break;
                case "DeleteItemCategoryArea":
                    content = DeleteItemCategoryArea();
                    break;
                case "UpdateMHCategoryAreaByGroupId":
                    content = UpdateMHCategoryAreaByGroupId();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

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

        #region GetItemCategory 商品分类查询

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

        #region GetHomePageConfigInfo 获取客户端首页所有配置信息

        /// <summary>
        /// 获取客户端首页所有配置信息
        /// </summary>
        public string GetHomePageConfigInfo()
        {
            var responseData = new ResponseData();

            try
            {
                var homeBll = new MobileHomeBLL(this.CurrentUserInfo);
                var homeList = homeBll.QueryByEntity(new MobileHomeEntity { CustomerId = this.CurrentUserInfo.ClientID }, null);

                if (homeList != null && homeList.Length > 0)
                {
                    var homeEntity = homeList.FirstOrDefault();

                    var adAreaBll = new MHAdAreaBLL(this.CurrentUserInfo);
                    var content = new GetHomePageConfigInfoRespContentData
                    {
                        adList = new List<AdEntity>(),
                        eventList = new List<EventAreaEntity>(),
                        categoryList = new List<CategoryEntity>()
                    };  //客户端首页所有配置信息

                    #region 广告集合 A 图片广告 B活动集合 C 商品分类和商品

                    var dsAd = adAreaBll.GetAdList(homeEntity.HomeId.ToString());
                    if (dsAd != null && dsAd.Tables.Count > 0 && dsAd.Tables[0].Rows.Count > 0)
                    {
                        content.adList = DataTableToObject.ConvertToList<AdEntity>(dsAd.Tables[0]);
                    }

                    #endregion

                    #region 活动集合

                    var dsEvent = adAreaBll.GetEventInfo(homeEntity.HomeId.ToString());
                    if (dsEvent != null && dsEvent.Tables.Count > 0 && dsEvent.Tables[0].Rows.Count > 0)
                    {
                        content.eventList = DataTableToObject.ConvertToList<EventAreaEntity>(dsEvent.Tables[0]);
                    }

                    #endregion

                    #region 分类集合

                    //获取分组ID
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
                                DataSet modelDs = adAreaBll.GetModelTypeIdByGroupId(category.groupId);
                                if (modelDs.Tables[0].Rows.Count > 0)
                                {
                                    category.modelTypeId = Convert.ToInt32(modelDs.Tables[0].Rows[0]["modelTypeId"]);
                                    category.modelTypeName = Convert.ToString(modelDs.Tables[0].Rows[0]["modelTypeName"]);
                                }
                                
                                category.itemList = DataTableToObject.ConvertToList<ItemEntity>(dsItem.Tables[0]);
                            }

                            content.categoryList.Add(category);
                        }
                    }

                    #endregion

                    responseData.success = true;
                    responseData.data = content;
                    return responseData.ToJSON();
                }
                else
                {
                    //添加移动终端首页主表MobileHome
                    homeBll.Create(new MobileHomeEntity
                    {
                        HomeId = Guid.NewGuid(),
                        Title = "客户端首页",
                        CustomerId = this.CurrentUserInfo.ClientID
                    });

                    responseData.success = true;
                    responseData.msg = string.Empty;
                    return responseData.ToJSON();
                }
            }
            catch (Exception ex)
            {
                responseData.success = false;
                responseData.msg = ex.ToString();
                return responseData.ToJSON();
            }
        }
        public class GetHomePageConfigInfoRespContentData
        {
            public IList<AdEntity> adList { get; set; }             //广告集合
            public IList<EventAreaEntity> eventList { get; set; }   //活动集合
            public IList<CategoryEntity> categoryList { get; set; } //分类集合
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
            public int typeId { get; set; }             //1=疯狂团购  2=限时抢购  3=热销榜单
            public Guid eventId { get; set; }           //活动ID
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
            public int? modelTypeId { get; set; }
            public string modelTypeName { get; set; }
            public string groupId { get; set; }          //分组ID
            public IList<ItemEntity> itemList { get; set; }   //商品集合
        }
        public class ItemEntity
        {
            public Guid categoryAreaId { get; set; }    //商品类别区域ID
            public int displayIndex { get; set; }       //序号
            public string imageUrl { get; set; }        //图片链接
            public string objectId { get; set; }        //对象ID
            public string objectName { get; set; }      //对象名称
            public int typeId { get; set; }             //类型ID： 1=商品分类 2=商品
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

            int modelTypeId = (Int32)Utils.GetIntVal(FormatParamValue(Request("modelTypeId")));
            string modelTypeName = FormatParamValue(Request("modelTypeName"));

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

            var itemCategory = this.CurrentContext.Request["categoryList"].DeserializeJSONTo<List<ItemCategoryEntity>>();

            if (itemCategory != null && itemCategory.Count > 0)
            {
                //分组ID
                var groupId = itemCategory.FirstOrDefault().groupId;
                var adAreaBll = new MHAdAreaBLL(this.CurrentUserInfo);
                var categoryAreaBll = new MHCategoryAreaBLL(this.CurrentUserInfo);
                var homeBll = new MobileHomeBLL(this.CurrentUserInfo);
                var homeList = homeBll.QueryByEntity(new MobileHomeEntity { CustomerId = this.CurrentUserInfo.ClientID }, null);
                var homeId = homeList.FirstOrDefault().HomeId;

                if (homeList != null && homeList.Length > 0)
                {
                    //判断groupId是否为空,为空则新增,不为空则更新
                    //如果新增，则传入 模板ID，修改则不需要传入模板ID
                    if (groupId == 0 || string.IsNullOrEmpty(groupId.ToString()))
                    {
                        //查找表中最大的groupid
                        groupId = categoryAreaBll.GetMaxGroupId();
                        //在此基础上对groupid+1
                        groupId++;
                        InsertItemCategory(itemCategory, groupId);                    
                    }
                    else
                    {
                        UpdateItemCategory(itemCategory);
                    }

                    var mHCategoryAreaGroup = new MHCategoryAreaGroupBLL(this.CurrentUserInfo);
                    var categoryAreaGroup = mHCategoryAreaGroup.QueryByEntity(new MHCategoryAreaGroupEntity()
                    {
                        GroupValue = groupId,
                        CustomerId =  this.CurrentUserInfo.ClientID
                    }, null);

                    var categoryAreaGroupEntity = new MHCategoryAreaGroupEntity()
                    {
                        ModelTypeId = modelTypeId,
                        ModelName = modelTypeName,
                        GroupValue = groupId,
                        CustomerId =  this.CurrentUserInfo.ClientID
                    };
                    if (categoryAreaGroup == null || !categoryAreaGroup.Any())
                    {
                        categoryAreaGroupEntity.GroupId = mHCategoryAreaGroup.GetMaxGroupId();
                        mHCategoryAreaGroup.Create(categoryAreaGroupEntity);
                    }
                    else
                    {
                        categoryAreaGroupEntity.GroupId = categoryAreaGroup[0].GroupId;
                        mHCategoryAreaGroup.Update(categoryAreaGroupEntity);
                    }
                    
                }
                else
                {
                    responseData.success = false;
                    responseData.msg = "没有查询到对应首页主表的数据";
                    return responseData.ToJSON();
                }
               
               
                
                #region 返回商品分类数据

                var content = new CategoryEntity();  //商品分类

                //获取分组ID
                content.modelTypeId = modelTypeId;
                content.modelTypeName = modelTypeName;
                content.groupId = groupId.ToString();
                content.itemList = new List<ItemEntity>();

                var dsItem = adAreaBll.GetItemList(content.groupId, homeId.ToString());
                if (dsItem != null && dsItem.Tables.Count > 0 && dsItem.Tables[0].Rows.Count > 0)
                {
                    content.itemList = DataTableToObject.ConvertToList<ItemEntity>(dsItem.Tables[0]);
                }

                #endregion

                responseData.success = true;
                responseData.data = content;
                return responseData.ToJSON();
            }
            else
            {
                responseData.success = false;
                responseData.data = "查无数据";
                return responseData.ToJSON();
            }



        }

        public void InsertItemCategory(List<ItemCategoryEntity> itemCategory, int groupId)
        {
            var categoryAreaBll = new MHCategoryAreaBLL(this.CurrentUserInfo);
            var homeBll = new MobileHomeBLL(this.CurrentUserInfo);
            var homeList = homeBll.QueryByEntity(new MobileHomeEntity { CustomerId = this.CurrentUserInfo.ClientID }, null);
            var homeId = homeList.FirstOrDefault().HomeId;
            foreach (var item in itemCategory)
            {
                //新增
                var entity = new MHCategoryAreaEntity()
                {
                    CategoryAreaId = Guid.NewGuid(),
                    HomeId = homeId,
                    ObjectId = item.objectId,
                    ObjectTypeId = item.typeId,
                    GroupID = groupId,
                    ObjectName = item.objectName,
                    ImageUrlObject = item.imageUrl,
                    DisplayIndex = item.displayIndex
                };
                categoryAreaBll.Create(entity);

            }
        }

        public void UpdateItemCategory(List<ItemCategoryEntity> itemCategory)
        {
            var adAreaBll = new MHAdAreaBLL(this.CurrentUserInfo);
            var categoryAreaBll = new MHCategoryAreaBLL(this.CurrentUserInfo);

            var homeBll = new MobileHomeBLL(this.CurrentUserInfo);
            var homeList = homeBll.QueryByEntity(new MobileHomeEntity { CustomerId = this.CurrentUserInfo.ClientID }, null);
            var homeId = homeList.FirstOrDefault().HomeId;
            foreach (var item in itemCategory)
            {
                //更新
                var entity = new MHCategoryAreaEntity()
                {
                    CategoryAreaId = Guid.Parse(item.categoryAreaId),
                    HomeId = homeId,
                    ObjectId = item.objectId,
                    ObjectTypeId = item.typeId,
                    GroupID = item.groupId,
                    ObjectName = item.objectName,
                    ImageUrlObject = item.imageUrl,
                    DisplayIndex = item.displayIndex
                };
                adAreaBll.UpdateMHCategoryArea(entity);
            }
        }
   
        public class SaveItemCategoryReqContentData
        {
            public int? modelTypeId { get; set; }
            public string modelTypeName { get; set; }
            public IList<ItemCategoryEntity> categoryList { get; set; } //分类集合
           
        }
        public class ItemCategoryEntity
        {
            public int typeId { get; set; }             //类型ID： 1=商品分类 2=商品
            public string categoryAreaId { get; set; }  //商品类别区域ID
            public string objectId { get; set; }        //对象ID
            public string objectName { get; set; }      //对象名称
            public int groupId { get; set; }            //分组ID（1、2、3…….）
            public int displayIndex { get; set; }       //序号（1、2、3）
            public string imageUrl { get; set; }        //图片链接
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
                var homeList = homeBll.QueryByEntity(new MobileHomeEntity { CustomerId = this.CurrentUserInfo.ClientID }, null);

                string customerId = this.CurrentUserInfo.CurrentUser.customer_id;
                if (homeList != null && homeList.Length > 0)
                {
                    var homeId = homeList.FirstOrDefault().HomeId;

                    string adsIdList = itemAds.Where(item => !string.IsNullOrEmpty(item.adId.ToString())).Aggregate("", (current, item) => current + "'" + item.adId.ToString() + "',");
                    //根据AdAreaId删除MHAdArea中旧数据（not in adsList）
                    var itemCategoryService = new ItemCategoryService(this.CurrentUserInfo);
                    if (adsIdList != "")
                    {
                        itemCategoryService.UpdateMHAdAreaData(adsIdList, customerId);
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

        #region  保存商品分类区域

        public class SaveItemArea
        {
            public Guid? itemAreaId { get; set; }
            public string itemId { get; set; }
            public Guid? eventId { get; set; }
            public int? isUrl { get; set; }
            public int? displayIndex { get; set; }
        }
        public string SaveEventItemArea()
        {
            var responseData = new ResponseData();

            var itemArea = this.CurrentContext.Request["eventItemList"].DeserializeJSONTo<List<SaveItemArea>>();

            #region
            if (itemArea != null)
            {
                var itemAreaBll = new MHItemAreaBLL(this.CurrentUserInfo);
                var homeBll = new MobileHomeBLL(this.CurrentUserInfo);
                string customerId = this.CurrentUserInfo.CurrentUser.customer_id;
                var homeList = homeBll.QueryByEntity(new MobileHomeEntity { CustomerId = this.CurrentUserInfo.ClientID }, null);

                if (homeList != null && homeList.Length > 0)
                {
                    var homeId = homeList.FirstOrDefault().HomeId;
                    string itemAreaIdList = itemArea.Where(item => !string.IsNullOrEmpty(item.itemAreaId.ToString())).Aggregate("", (current, item) => current + "'" + item.itemAreaId.ToString() + "',");
                    //根据ItemAreaId删除MHItemArea中旧数据（not in itemAreaIdList）                  
                    if (itemAreaIdList != "")
                    {
                        var itemCategoryService = new ItemCategoryService(this.CurrentUserInfo);
                        itemCategoryService.UpdateMHItemAreaData(itemAreaIdList,customerId);
                    }

                    //根据ItemAreaId判断是新增还是更新MHItemArea数据
                    foreach (var item in itemArea)
                    {
                        if (string.IsNullOrEmpty(item.itemAreaId.ToString()))
                        {
                            var entity = new MHItemAreaEntity()
                            {
                                ItemAreaId = Guid.NewGuid(),
                                HomeId = homeId,
                                IsUrl = item.isUrl,
                                EventId = item.eventId,
                                ItemId = item.itemId,
                                DisplayIndex = item.displayIndex,
                            };
                            itemAreaBll.Create(entity);
                        }
                        else
                        {
                            var entity = new MHItemAreaEntity()
                            {
                                ItemAreaId = item.itemAreaId,
                                HomeId = homeId,
                                IsUrl = item.isUrl,
                                EventId = item.eventId,
                                ItemId = item.itemId,
                                DisplayIndex = item.displayIndex,
                            };
                            itemAreaBll.Update(entity);
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

        #region 删除商品分类区域

        public string DeleteItemCategoryArea()
        {
            var responseData = new ResponseData();

            var groupID = FormatParamValue(Request("groupId"));

            var itemCategoryService = new ItemCategoryService(this.CurrentUserInfo);
            itemCategoryService.DeleteItemCategoryAreaData(groupID);
            itemCategoryService.DeleteItemCategoryAreaGroupData(groupID);
            responseData.success = true;
            responseData.msg = "删除成功";
            return responseData.ToJSON();
        }

        #endregion

        #region 获取活动商品列表
        public string GetItemAreaByEventTypeID()
        {
            var responseData = new ResponseData();

            //请求参数
            var eventTypeID = FormatParamValue(Request("eventTypeId"));
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

                var content = new GetMHCategoryAreaInfoRespContentData {categoryList = new List<CategoryEntity>()};
                try
                {
                    var homeBll = new MobileHomeBLL(this.CurrentUserInfo);
                    var homeList = homeBll.QueryByEntity(new MobileHomeEntity { CustomerId = this.CurrentUserInfo.ClientID }, null);
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