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

            HomePageConfigRD resData = new HomePageConfigRD();

            #region 广告部分
            List<AdAreaInfo> AdAreaList = new List<AdAreaInfo> { };
            var adBll = new MHAdAreaBLL(logginUserInfo);
            var tempAdArealist = adBll.GetByCustomerID();
            AdAreaList.AddRange(tempAdArealist.Select(t => new AdAreaInfo
            {
                DisplayIndex = t.DisplayIndex,
                ImageUrl = t.ImageUrl,
                ObjectID = t.ObjectId,
                ObjectTypeID = t.ObjectTypeId,
                Url = t.Url
            }));
            #endregion

            #region 活动部分
            List<ItemEventAreaInfo> ItemEventAreaList = new List<ItemEventAreaInfo> { };
            var itemEventBll = new MHItemAreaBLL(logginUserInfo);
            var tempItemEventAreaList = itemEventBll.GetItemDetails();
            ItemEventAreaList.AddRange(tempItemEventAreaList.Select(t => new ItemEventAreaInfo
            {
                ItemID = t.ItemId,
                ItemName = t.ItemName,
                ImageUrl = t.ImageUrl,
                Price = t.Price,
                SalesPrice = t.SalesPrice,
                DiscountRate = t.DiscountRate,    //折扣
                DisplayIndex = t.DisplayIndex,
                DeadlineTime = t.DeadlineTime,
                DeadlineSecond = t.DeadlineSecond,
                AddedTime = t.AddedTime.To19FormatString(),
                BeginTime = t.BeginTime.To19FormatString(),
                EndTime = t.EndTime.To19FormatString(),
                TypeID = t.TypeId
            }));

            #endregion

            //#region 分类和商品部分
            //List<CategoryGroupInfo> CategoryAreaList = new List<CategoryGroupInfo> { };
            //var categoryBll = new MHCategoryAreaBLL(logginUserInfo);
            //var tempCategoryAreaList = categoryBll.GetByCustomerID();
            //var tempCategoryInfoList = tempCategoryAreaList.Select(t => new CategoryAreaInfo
            //{
            //    ObjectID = t.ObjectId,
            //    ImageUrl = t.ImageUrlObject,
            //    DisplayIndex = t.DisplayIndex,
            //    TypeID = t.ObjectTypeId,
            //    GroupID = t.GroupID
            //});
            //tempCategoryInfoList.Select(t => t.GroupID).Distinct().ToList().ForEach(t =>
            //    {
            //        CategoryAreaList.Add(new CategoryGroupInfo()
            //        {
            //            GroupID = t,
            //            ModelTypeId =Convert.ToInt32(adBll.GetModelTypeIdByGroupId(t.ToString()).Tables[0].Rows[0]["modelTypeId"]),
            //            ModelTypeName = Convert.ToString(adBll.GetModelTypeIdByGroupId(t.ToString()).Tables[0].Rows[0]["modelTypeName"]),
            //            CategoryAreaList = tempCategoryInfoList.Where(tt => tt.GroupID == t).OrderBy(tt => tt.DisplayIndex).ToArray()
            //        });
            //    });
            //#endregion


            //获取分组ID

            var homeBll = new MobileHomeBLL(this.CurrentUserInfo);
            var homeList = homeBll.QueryByEntity(new MobileHomeEntity { CustomerId = this.CurrentUserInfo.ClientID }, null);

            var homeEntity = homeList.FirstOrDefault();
            
            var dsGroup = adBll.GetCategoryGroupId(homeEntity.HomeId.ToString());
             var categoryList = new List<CategoryGroupInfo>();
            if (dsGroup != null && dsGroup.Tables.Count > 0 && dsGroup.Tables[0].Rows.Count > 0)
            {
               

                foreach (DataRow dr in dsGroup.Tables[0].Rows)
                {
                    var category = new CategoryGroupInfo();
                    category.GroupID = Convert.ToInt32(dr[0]);
                    category.CategoryAreaList = new List<CategoryAreaInfo>().ToArray();

                    var dsItem = adBll.GetItemList(category.GroupID.ToString(), homeEntity.HomeId.ToString());
                    if (dsItem != null && dsItem.Tables.Count > 0 && dsItem.Tables[0].Rows.Count > 0)
                    {
                        DataSet modelDs = adBll.GetModelTypeIdByGroupId(category.GroupID.ToString());
                        if (modelDs.Tables[0].Rows.Count > 0)
                        {
                            category.ModelTypeId = Convert.ToInt32(modelDs.Tables[0].Rows[0]["modelTypeId"]);
                            category.ModelTypeName = Convert.ToString(modelDs.Tables[0].Rows[0]["modelTypeName"]);
                        }

                        category.CategoryAreaList = DataTableToObject.ConvertToList<CategoryAreaInfo>(dsItem.Tables[0]).ToArray();

                        categoryList.Add(category); 
                    }
                    
                  
                }
            }


            resData.AdAreaList = AdAreaList.OrderBy(t => t.DisplayIndex).ToArray();           
            resData.ItemEventAreaList = ItemEventAreaList.ToArray();
            // 过滤分类集合，把ModelTypeID=8的取出来(获取唯一的)
            resData.CategoryEntrance = categoryList.Where(p => p.ModelTypeId == 8).SingleOrDefault();
            //过滤分类集合，把ModelTypeID<>8的取出来
            resData.CategoryGroupList = categoryList.Where(p => p.ModelTypeId != 8).ToList().ToArray();
         

            return resData;
        }
        #endregion
    }
}
