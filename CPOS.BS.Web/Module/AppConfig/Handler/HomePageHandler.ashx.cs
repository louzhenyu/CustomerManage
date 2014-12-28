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
                case "GetItemList":             //商品查询
                    content = GetItemList();
                    break;
                case "GetHomePageConfigInfo":   //获取客户端首页所有配置信息
                    content = GetHomePageConfigInfo();
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
                case "GetItemAreaByEventTypeID":
                    content = GetItemAreaByEventTypeID();
                    break;
                case "UpdateMHCategoryAreaByGroupId":
                    content = UpdateMHCategoryAreaByGroupId();
                    break;
                case "UpdateMobileHomeSort":
                    content = UpdateMobileHomeSort();
                    break;
                case "SaveSeach":
                    content = SaveSeach();
                    break;

            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

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
            var homeBll = new MobileHomeBLL(this.CurrentUserInfo);
            var homeList = homeBll.QueryByEntity(new MobileHomeEntity { CustomerId = this.CurrentUserInfo.ClientID }, null);

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
            responseData.success = true;
            responseData.msg = "更新成功";
            return responseData.ToJSON();
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
                        eventList = new EventListEntity(),
                        secondKill = new EventListEntity(),
                        categoryList = new List<CategoryEntity>(),
                        categoryEntrance = new CategoryEntity(),
                        navList = new CategoryEntity(),
                        search = new MHSearchAreaEntity(),
                        sortActionJson = ""
                    };  //客户端首页所有配置信息

                    content.sortActionJson =  homeEntity.sortActionJson == null ? "" : homeEntity.sortActionJson;//返回排序数据

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
                    //团购整体的
                    var dsEvent = adAreaBll.GetEventInfo(homeEntity.HomeId.ToString());//获取
                   
                
                     
                    if (dsEvent != null && dsEvent.Tables.Count > 0 && dsEvent.Tables[0].Rows.Count > 0)
                    {
                        var dsEventList= DataTableToObject.ConvertToList<EventAreaEntity>(dsEvent.Tables[0]);
                        //原来的团购部分，三块分别是抢购、团购、热销的
                        content.eventList.arrayList = dsEventList.Where(p => p.areaFlag == "eventList").ToList<EventAreaEntity>();
                       // content.eventList.shopType =-1;//不是任何的一个值,不赋值
                        content.eventList.areaFlag = "eventList";//不是任何的一个值
                        //新秒杀部分，要么团购，要么全是秒杀
                        //  secondKill
                        content.secondKill.arrayList = dsEventList.Where(p => p.areaFlag == "secondKill").ToList<EventAreaEntity>();
                        if (content.secondKill.arrayList != null && content.secondKill.arrayList.Count != 0)
                        {
                            content.secondKill.shopType = content.secondKill.arrayList[0].typeId;//不是任何的一个值
                        }
                        content.secondKill.areaFlag = "secondKill";//不是任何的一个值
                     
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
                                groupId = dr[0].ToString(),//有个groupId
                                itemList = new List<ItemEntity>()
                            };
                            //根据groupId和HomeId来取MHCategoryArea
                            var dsItem = adAreaBll.GetItemList(category.groupId, homeEntity.HomeId.ToString());
                            if (dsItem != null && dsItem.Tables.Count > 0 && dsItem.Tables[0].Rows.Count > 0)
                            {
                                //获取MHCategoryAreaGroup的modelTypeId和modelname，先取子元素，再取父元素的原因是防止冗余数据。
                                DataSet modelDs = adAreaBll.GetModelTypeIdByGroupId(category.groupId);//传category的GroupID
                                if (modelDs.Tables[0].Rows.Count > 0)
                                {
                                    category.modelTypeId = Convert.ToInt32(modelDs.Tables[0].Rows[0]["modelTypeId"]);
                                    category.modelTypeName = Convert.ToString(modelDs.Tables[0].Rows[0]["modelTypeName"]);
                                    category.styleType = Convert.ToString(modelDs.Tables[0].Rows[0]["styleType"]);  //直接Convert.ToString会把null值变为“”
                                    category.titleName = Convert.ToString(modelDs.Tables[0].Rows[0]["titleName"]);
                                    category.titleStyle = Convert.ToString(modelDs.Tables[0].Rows[0]["titleStyle"]);
                                }

                                category.itemList = DataTableToObject.ConvertToList<ItemEntity>(dsItem.Tables[0]);
                            }

                            content.categoryList.Add(category);//一个分组下面有好多个具体的图片对象。
                        }
                    }


                    #endregion

                    // 过滤分类集合，把ModelTypeID=8的取出来(获取唯一的)
                    List<CategoryEntity> lc = content.categoryList.Where(p => p.modelTypeId == 8).ToList();
                    if (lc != null && lc.Count != 0)
                    {
                        content.categoryEntrance = lc.OrderByDescending(p => p.groupId).ToList()[0]; ;
                    }
                    // 过滤分类集合，把ModelTypeID=4的取出来(导航部门)
                    List<CategoryEntity> lc4 = content.categoryList.Where(p => p.modelTypeId == 4).ToList();
                    if (lc4 != null && lc4.Count != 0)
                    {
                        content.navList = lc4.OrderByDescending(p => p.groupId).ToList()[0]; ;//(获取唯一的)
                    }
                    //过滤分类集合，把ModelTypeID<>8的和不等于8的取出来
                    content.categoryList = content.categoryList.Where(p => p.modelTypeId != 8).Where(p => p.modelTypeId != 4).ToList();

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
          
            public IList<CategoryEntity> categoryList { get; set; } //首页分类分组信息(分组8以外的)
            public CategoryEntity categoryEntrance { get; set; } //C8区分类分组信息(新增)
            public CategoryEntity navList { get; set; } //导航区域，c区模块4
            public MHSearchAreaEntity search { get; set; } //搜索框
            public string sortActionJson { get; set; } //整体排序字段
            public EventListEntity eventList { get; set; }   //活动集合
            public EventListEntity secondKill { get; set; }   //秒杀区
        }
        public class EventListEntity {
            public string areaFlag { get; set; }  //区域标识，eventList,secondKill
            public int shopType { get; set; }  //用与存放秒杀区的整体类型数据，便于前端获取。
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

            public String styleType { get; set; }

            public String titleName { get; set; }

            public String titleStyle { get; set; }

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

            int modelTypeId = (Int32)Utils.GetIntVal(FormatParamValue(Request("modelTypeId")));//模块ID，含有1,3,8这样的
            string modelTypeName = FormatParamValue(Request("modelTypeName"));//模块名称
            string _styleType = FormatParamValue(Request("styleType"));//样式
            string _titleName = FormatParamValue(Request("titleName"));//标题名称
            string _titleStyle = FormatParamValue(Request("titleStyle"));//标题样式

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
            int groupId = -1;
            try
            {
                groupId = (Int32)Utils.GetIntVal(FormatParamValue(Request("groupId")));//分组信息  ，保存c区模板8时，传了这个值，其他c区模板都没有传
            }
            catch
            {
                groupId = -1;
            }
            //集合数据
            var itemCategory = this.CurrentContext.Request["categoryList"].DeserializeJSONTo<List<ItemCategoryEntity>>();
            var adAreaBll = new MHAdAreaBLL(this.CurrentUserInfo);
            var categoryAreaBll = new MHCategoryAreaBLL(this.CurrentUserInfo);
            var homeBll = new MobileHomeBLL(this.CurrentUserInfo);
            var homeList = homeBll.QueryByEntity(new MobileHomeEntity { CustomerId = this.CurrentUserInfo.ClientID }, null);
            var homeId = homeList.FirstOrDefault().HomeId;//HomeId
            if (homeList == null && homeList.Length == 0)
            {
                responseData.success = false;
                responseData.msg = "没有查询到对应首页主表的数据";
                return responseData.ToJSON();
            }
            if (itemCategory != null && itemCategory.Count > 0)//防错判断
            {
                //分组ID
                //兼容性，保证除c8区的通用性**
                groupId = itemCategory.FirstOrDefault().groupId;
                #region
                //if (homeList != null && homeList.Length > 0)
                //{
                //判断groupId是否为空,为空则新增,不为空则更新
                //如果新增，则传入 模板ID，修改则不需要传入模板ID
                //下面这种不适用于modelTypeid=8的情况
                if (modelTypeId != 8)
                {
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
                }
                else
                {
                    //modelTypeId==8的情况
                    //先把所有的都删除，然后再插入      //根据HomeId和GroupId删除     
                    if (groupId == 0 || string.IsNullOrEmpty(groupId.ToString()))
                    {
                        //查找表中最大的groupid
                        groupId = categoryAreaBll.GetMaxGroupId();
                        groupId++;//不要忘了++
                    }
                    else
                    {
                        DeleteItemCategoryByGroupIdandHomeID(groupId, (Guid)homeId);//如果之前有数据，先删除。必须同时根据groupId和homeId
                    }
                    InsertItemCategory(itemCategory, groupId);//全部都是插入数据
                }

                //查找大块的信息
                var mHCategoryAreaGroup = new MHCategoryAreaGroupBLL(this.CurrentUserInfo);
                var categoryAreaGroup = mHCategoryAreaGroup.QueryByEntity(new MHCategoryAreaGroupEntity()   //QueryByEntity取出来的是一个集合
                {
                    GroupValue = groupId,//这里对应的是categoryAreaGroup表里的GroupValue，而不是groupId
                    CustomerId = this.CurrentUserInfo.ClientID
                }, null);//根据GroupValue取值，查找出相关信息

                var categoryAreaGroupEntity = new MHCategoryAreaGroupEntity()  //这里直接用传request穿过来的参数了
                {
                    ModelTypeId = modelTypeId,
                    ModelName = modelTypeName,
                    GroupValue = groupId,
                    CustomerId = this.CurrentUserInfo.ClientID,
                    titleName = _titleName,
                    titleStyle = _titleStyle,
                    styleType = _styleType

                };
                if (categoryAreaGroup == null || !categoryAreaGroup.Any())
                {
                    categoryAreaGroupEntity.GroupId = mHCategoryAreaGroup.GetMaxGroupId();//这时因为之前在插入mHCategoryArea已经创建了最新的MaxGroupId
                    mHCategoryAreaGroup.Create(categoryAreaGroupEntity);//创建
                }
                else
                {
                    categoryAreaGroupEntity.GroupId = categoryAreaGroup[0].GroupId;
                    mHCategoryAreaGroup.Update(categoryAreaGroupEntity);//修改
                }

                //}
                //else        //对应homelist
                //{
                //    responseData.success = false;
                //    responseData.msg = "没有查询到对应首页主表的数据";
                //    return responseData.ToJSON();
                //}

                #endregion

                #region 返回商品分类数据

                var content = new CategoryEntity();  //商品分类

                //获取分组ID
                content.modelTypeId = modelTypeId;
                content.modelTypeName = modelTypeName;
                content.groupId = groupId.ToString();   //返回groupID做为主表的
                content.styleType = _styleType;
                content.titleName = _titleName;
                content.titleStyle = _titleStyle;

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
            else
            {

                DeleteItemCategoryByGroupIdandHomeID(groupId, (Guid)homeId);//删除小块的内容
                //删除大块的内容
                DeleteCategoryGroupByGroupIdandCustomerId(groupId, this.CurrentUserInfo.ClientID);//删除大块的内容,,CustomerId = this.CurrentUserInfo.ClientID

                // responseData.success = false;
                responseData.success = true;
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
                    CategoryAreaId = Guid.NewGuid(),//创建guid
                    HomeId = homeId,
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
                    DisplayIndex = item.displayIndex,
                    navName=item.navName,
                    url=item.url
                };
                adAreaBll.UpdateMHCategoryArea(entity);
            }
        }

        public void DeleteItemCategoryByGroupIdandHomeID(int GroupID, Guid HomeId)
        {
            var adAreaBll = new MHAdAreaBLL(this.CurrentUserInfo);
            adAreaBll.DeleteItemCategoryByGroupIdandHomeID(GroupID, HomeId); ;

        }
        public void DeleteCategoryGroupByGroupIdandCustomerId(int GroupID, string customerId)
        {
            var adAreaBll = new MHAdAreaBLL(this.CurrentUserInfo);
            adAreaBll.DeleteCategoryGroupByGroupIdandCustomerId(GroupID, customerId); //删除大块
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
            public int groupId { get; set; }            //分组ID（1、2、3…….）
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
                var homeList = homeBll.QueryByEntity(new MobileHomeEntity { CustomerId = this.CurrentUserInfo.ClientID }, null);

                string customerId = this.CurrentUserInfo.CurrentUser.customer_id;
                if (homeList != null && homeList.Length > 0)
                {
                    var homeId = homeList.FirstOrDefault().HomeId;
                    //比较底层的求和方式
                    //string adsIdList = itemAds.Where(item => !string.IsNullOrEmpty(item.adId.ToString())).Aggregate("", (current, item) => current + "'" + item.adId.ToString() + "',");
                    //根据AdAreaId删除MHAdArea中旧数据（not in adsList） 
                    var itemCategoryService = new ItemCategoryService(this.CurrentUserInfo);
                    if (itemMHSearchArea.MHSearchAreaID != null)//不为null
                    {
                        itemCategoryService.UpdateMHSearchAreaData((Guid)itemMHSearchArea.MHSearchAreaID, customerId);
                    }
                    else
                    {
                        itemCategoryService.DeleteMHSearchAreaData(customerId);
                    }

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
            responseData.msg = "更新成功";
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
        }
        public string SaveEventItemArea()
        {
            var responseData = new ResponseData();

            var itemArea = this.CurrentContext.Request["eventItemList"].DeserializeJSONTo<List<SaveItemArea>>();
             //eventList  ||  secondKill ---一个是放原来的团购的，另一个是放秒杀区的
            var _areaFlag = this.CurrentContext.Request["areaFlag"] == null ? "eventList" : this.CurrentContext.Request["areaFlag"].ToString().Trim();

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
                        itemCategoryService.UpdateMHItemAreaData(itemAreaIdList, customerId, _areaFlag);
                    }
                    else {
                        var itemCategoryService = new ItemCategoryService(this.CurrentUserInfo);
                        itemCategoryService.DeleteMHItemAreaData(customerId, _areaFlag);//主要针对秒杀区的，一个客户秒杀区的信息值能存一个类型（eventtypeid）的
             
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
                                areaFlag=_areaFlag,//所属区域
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
                                areaFlag=_areaFlag,//所属区域
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

                var content = new GetMHCategoryAreaInfoRespContentData { categoryList = new List<CategoryEntity>() };
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