using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.AppConfig.HomePageConfig.Request;
using JIT.CPOS.DTO.Module.AppConfig.HomePageConfig.Response;
using JIT.CPOS.Web.ApplicationInterface.Util.ExtensionMethods;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.ExtensionMethod;
using System.Data;

namespace JIT.CPOS.Web.ApplicationInterface.Module.AppConfig.HomePageConfig
{
    public class HomePageConfigAH : BaseActionHandler<HomePageConfigRP, HomePageConfigRD>
    {
        #region 构造函数
        public HomePageConfigAH()
        { }
        #endregion

        #region 错误码
        #endregion

        #region 接口实现
        protected override HomePageConfigRD ProcessRequest(DTO.Base.APIRequest<HomePageConfigRP> pRequest)
        {
            //创建连接用户对象
            var logginUserInfo = base.CurrentUserInfo;
            var param = pRequest.Parameters;
            HomePageConfigRD resData = new HomePageConfigRD();
            resData.eventList = new List<EventListEntity>();
            resData.secondKill = new List<EventListEntity>();
            resData.groupBuy = new List<EventListEntity>();
            resData.hotBuy = new List<EventListEntity>();
            resData.bargain = new List<EventListEntity>();

            var bllHome = new MobileHomeBLL(logginUserInfo);
            MobileHomeEntity entityHome = new MobileHomeEntity(); ;
            if (!string.IsNullOrEmpty(param.HomeId))
            {
                entityHome = bllHome.QueryByEntity(new MobileHomeEntity() { CustomerId = logginUserInfo.ClientID, HomeId = new Guid(param.HomeId) }, null).FirstOrDefault();
            }
            else
            {
                entityHome = bllHome.QueryByEntity(new MobileHomeEntity() { CustomerId = logginUserInfo.ClientID, IsActivate = 1 }, null).FirstOrDefault();

            }
            if(entityHome==null)
            {
                resData.Success = false;
                resData.ErrMsg = "商城正在维护";
                return resData;
            }
            resData.Success = true;
            string strHomeId=entityHome.HomeId.ToString();

            resData.sortActionJson = entityHome.sortActionJson == null ? "" : entityHome.sortActionJson;//返回排序数据


            #region 广告部分
            List<AdAreaInfo> AdAreaList = new List<AdAreaInfo> { };
            var adBll = new MHAdAreaBLL(logginUserInfo);
            var tempAdArealist = adBll.GetAdByHomeId(strHomeId);
            AdAreaList.AddRange(tempAdArealist.Select(t => new AdAreaInfo
            {
                DisplayIndex = t.DisplayIndex,
                ImageUrl = t.ImageUrl,
                ObjectID = t.ObjectId,
                ObjectTypeID = t.ObjectTypeId,
                Url = t.Url
            }));
            #endregion
            resData.adAreaList = AdAreaList.ToArray();
            
            #region 搜索框

            var dsSearch = adBll.GetMHSearchArea(strHomeId);//获取搜索框
            if (dsSearch != null && dsSearch.Tables.Count > 0 && dsSearch.Tables[0].Rows.Count > 0)
            {
                resData.search = DataTableToObject.ConvertToObject<MHSearchAreaEntity>(dsSearch.Tables[0].Rows[0]);//转换成一个对象时，里面的参数不能是一个表，而是一行数据
            }

            #endregion

            #region 活动部分
            var adAreaBll = new MHAdAreaBLL(logginUserInfo);
            var itemEventBll = new MHItemAreaBLL(logginUserInfo);

            var bllCategoryAreaGroup = new MHCategoryAreaGroupBLL(this.CurrentUserInfo);

            var allGroup = bllCategoryAreaGroup.QueryByEntity(new MHCategoryAreaGroupEntity { CustomerID = this.CurrentUserInfo.ClientID, HomeId = strHomeId }, null);

            var eventGroup = allGroup.Where(a => a.ModelTypeId == 5 || a.ModelTypeId == 6 || a.ModelTypeId == 7 || a.ModelTypeId == 8|| a.ModelTypeId == 9);
            foreach (var item in eventGroup)
            {

                DataSet dsEvent = new DataSet();
                if (item.ModelTypeId == 8)
                    dsEvent = itemEventBll.GetEventListItemDetails(strHomeId, item.GroupId.ToString());//获取EventList
                else
                    dsEvent = itemEventBll.GetItemDetails(strHomeId, item.GroupId.ToString());

                if (dsEvent != null && dsEvent.Tables.Count > 0 && dsEvent.Tables[0].Rows.Count > 0)
                {
                    var dsEventList = DataTableToObject.ConvertToList<ItemEventAreaInfo>(dsEvent.Tables[0]);
                    var category = new EventListEntity
                    {
                        showStyle = dsEventList.FirstOrDefault().ShowStyle,
                        shopType = dsEventList.FirstOrDefault().TypeID,
                        displayIndex = item.DisplayIndex,
                        areaFlag=dsEventList.FirstOrDefault().areaFlag,
                        arrayList = dsEventList
                    };

                    if (dsEventList.FirstOrDefault().areaFlag == "eventList")
                    {
                        
                        resData.eventList.Add(category);
                    }
                    if (dsEventList.FirstOrDefault().areaFlag == "secondKill")
                    {
                        resData.secondKill.Add(category);
                    }
                    if (dsEventList.FirstOrDefault().areaFlag == "groupBuy")
                    {
                        resData.groupBuy.Add(category);
                    }
                    if (dsEventList.FirstOrDefault().areaFlag == "hotBuy")
                    {
                        resData.hotBuy.Add(category);
                    }
                    if (dsEventList.FirstOrDefault().areaFlag == "bargain")
                    {
                        resData.bargain.Add(category);
                    }
                }

            }
            #endregion
            List<CategoryGroupInfo> allList = new List<CategoryGroupInfo>();
            eventGroup = allGroup.Where(a => a.ModelTypeId != 5 && a.ModelTypeId != 6 && a.ModelTypeId != 7 && a.ModelTypeId != 8 && a.ModelTypeId != 9 && a.ModelTypeId != 2);

            if (eventGroup != null)
            {
                foreach (var groupItem in eventGroup)
                {
                    var category = new CategoryGroupInfo
                    {
                        groupID = groupItem.GroupId,
                        CategoryAreaList = new List<CategoryAreaInfo>()
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


                    //根据groupId和HomeId来取MHCategoryArea
                    if (groupItem.ModelTypeId == 2)
                    {

                    }
                    else
                    {
                    }
                    var dsItem = adAreaBll.GetItemList(category.groupID.ToString(), entityHome.HomeId.ToString());
                    if (dsItem != null && dsItem.Tables.Count > 0 && dsItem.Tables[0].Rows.Count > 0)
                    {
                        category.CategoryAreaList = DataTableToObject.ConvertToList<CategoryAreaInfo>(dsItem.Tables[0]);
                    }
                    allList.Add(category);
                }
            }
            //分类组合
            List<CategoryGroupInfo> lc = allList.Where(p => p.modelTypeId == 1).ToList();
            if (lc != null && lc.Count != 0)
            {
                resData.categoryEntrance = lc.OrderByDescending(p => p.groupID).ToList()[0]; ;
            }
      

            //创意组合
            if (allList.Where(p => p.modelTypeId == 3).ToList() != null && allList.Where(p => p.modelTypeId == 3).ToList().Count != 0)
            {
                resData.originalityList = allList.Where(p => p.modelTypeId == 3).ToList();

            }
            //导航
            List<CategoryGroupInfo> lc4 = allList.Where(p => p.modelTypeId == 4).ToList();
            if (lc4 != null && lc4.Count != 0)
            {
                resData.navList = lc4.OrderByDescending(p => p.groupID).ToList()[0]; ;//(获取唯一的)
            }
            //商品列表
            eventGroup = allGroup.Where(a => a.ModelTypeId == 2);
            List<ProductListInfo> productList = new List<ProductListInfo>();
            if (eventGroup != null)
            {
                foreach (var groupItem in eventGroup)
                {
                    var category = new ProductListInfo
                    {
                        CategoryAreaList = new List<ProductInfo>()
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

                    MHCategoryAreaBLL bllCategoryArea = new MHCategoryAreaBLL(logginUserInfo);
                    int intObjectId = bllCategoryArea.GetObjectTypeIDByGroupId((int)groupItem.GroupId);
                    DataSet dsItem = null;
                    if (intObjectId == 4)
                        dsItem = adAreaBll.GetGroupProductList(groupItem.GroupId.ToString(), entityHome.HomeId.ToString(), category.showCount);
                    else
                        dsItem = adAreaBll.GetCategoryProductList(groupItem.GroupId.ToString(), entityHome.HomeId.ToString(), category.showCount);
                    if (dsItem != null && dsItem.Tables.Count > 0 && dsItem.Tables[0].Rows.Count > 0)
                    {
                        // 设置分类ID
                        category.categoryId = dsItem.Tables[0].Rows[0]["objectId"].ToString();
                        category.CategoryAreaList = DataTableToObject.ConvertToList<ProductInfo>(dsItem.Tables[0]);
                        foreach (ProductInfo pf in category.CategoryAreaList)
                        {
                            double rate = (int)((pf.SalesPrice / pf.Price) * 100) / 10.0;
                            if (rate < 10)
                            {
                                pf.DiscountRate = string.Format("{0}折", rate);
                            }
                            else
                            {
                                pf.DiscountRate = string.Empty;
                            }
                            // 获取sku id
                            pf.SkuList =
                                dsItem.Tables[1].AsEnumerable()
                                    .Where(r => r.Field<string>("item_id").Equals(pf.ItemID))
                                    .Select(r => r.Field<string>("sku_id"))
                                    .ToList();
                            if (string.IsNullOrEmpty(pf.SalesCount))
                            {
                                pf.SalesCount = "0";
                            }
                        }
                    }
                    if (category.CategoryAreaList.Count > 0)
                    {
                        productList.Add(category);
                    }
                }
            }


            if (productList != null && productList.Count != 0)
            {
                resData.productList = productList.ToList(); ;
            }
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
            resData.follow = follow;
            #endregion
            return resData;
        }
        #endregion
    }
}
