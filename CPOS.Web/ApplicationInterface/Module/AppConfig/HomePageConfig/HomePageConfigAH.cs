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
                DiscountRate = t.DiscountRate == null ? 0 : Convert.ToDecimal((t.DiscountRate / 10).ToString("0.0")),    //折扣 update by Henry 2014-10-20
                DisplayIndex = t.DisplayIndex,
                DeadlineTime = t.DeadlineTime,
                DeadlineSecond = t.DeadlineSecond,
                AddedTime = t.AddedTime.To19FormatString(),
                BeginTime = t.BeginTime.To19FormatString(),
                EndTime = t.EndTime.To19FormatString(),
                TypeID = t.TypeId,
                areaFlag = t.areaFlag
            }));

            #endregion
            #region 废弃代码
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
            #endregion

            //获取分组ID

            var homeBll = new MobileHomeBLL(this.CurrentUserInfo);
            var homeList = homeBll.QueryByEntity(new MobileHomeEntity { CustomerId = this.CurrentUserInfo.ClientID }, null);

            var homeEntity = homeList.FirstOrDefault();
            resData.sortActionJson = homeEntity.sortActionJson == null ? "" : homeEntity.sortActionJson;//返回排序数据

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
                            category.styleType = Convert.ToString(modelDs.Tables[0].Rows[0]["styleType"]);  //直接Convert.ToString会把null值变为“”
                            category.titleName = Convert.ToString(modelDs.Tables[0].Rows[0]["titleName"]);
                            category.titleStyle = Convert.ToString(modelDs.Tables[0].Rows[0]["titleStyle"]);
                        }

                        category.CategoryAreaList = DataTableToObject.ConvertToList<CategoryAreaInfo>(dsItem.Tables[0]).ToArray();

                        categoryList.Add(category);
                    }


                }
            }


            resData.AdAreaList = AdAreaList.OrderBy(t => t.DisplayIndex).ToArray();
            //这里要根据areaFlag来分出eventList和secondKill
            //   resData.ItemEventAreaList = ItemEventAreaList.ToArray();
            if (ItemEventAreaList != null && ItemEventAreaList.Count > 0)
            {
                resData.ItemEventAreaList = new EventListEntity();//要先实例化
                //原来的团购部分，三块分别是抢购、团购、热销的
                resData.ItemEventAreaList.arrayList = ItemEventAreaList.Where(p => p.areaFlag == "eventList").ToList();
                // content.eventList.shopType =-1;//不是任何的一个值,不赋值
                resData.ItemEventAreaList.areaFlag = "eventList";//不是任何的一个值
                //新秒杀部分，要么团购，要么全是秒杀
                //  secondKill
                resData.secondKill = new EventListEntity();//要先实例化
                resData.secondKill.arrayList = ItemEventAreaList.Where(p => p.areaFlag == "secondKill").ToList();
                if (resData.secondKill.arrayList != null && resData.secondKill.arrayList.Count != 0)
                {
                    resData.secondKill.shopType = resData.secondKill.arrayList[0].TypeID;//不是任何的一个值
                }
                resData.secondKill.areaFlag = "secondKill";//不是任何的一个值

            }

            // 过滤分类集合，把ModelTypeID=8的取出来(获取唯一的)
            if (categoryList.Where(p => p.ModelTypeId == 8) != null && categoryList.Where(p => p.ModelTypeId == 8).Count() != 0)
            {
                resData.CategoryEntrance = categoryList.Where(p => p.ModelTypeId == 8).OrderByDescending(p => p.GroupID).ToList()[0];
            }
            if (categoryList.Where(p => p.ModelTypeId == 4) != null && categoryList.Where(p => p.ModelTypeId ==4).Count() != 0)
            {
                resData.navList = categoryList.Where(p => p.ModelTypeId == 4).OrderByDescending(p => p.GroupID).ToList()[0];//只 取第一条 
            }
            //List<CategoryGroupInfo> lc = categoryList.Where(p => p.ModelTypeId == 8).ToList();
            //if (lc != null && lc.Count != 0)
            //{
            //    resData.CategoryEntrance = lc[0];
            //}
            //过滤分类集合，把ModelTypeID<>8的取出来
            resData.CategoryGroupList = categoryList.Where(p => p.ModelTypeId != 8).Where(p => p.ModelTypeId != 4).ToList().ToArray();


            #region 搜索框

            var dsSearch = adBll.GetMHSearchArea(homeEntity.HomeId.ToString());//获取搜索框
            if (dsSearch != null && dsSearch.Tables.Count > 0 && dsSearch.Tables[0].Rows.Count > 0)
            {
                resData.search = DataTableToObject.ConvertToObject<MHSearchAreaEntity>(dsSearch.Tables[0].Rows[0]);//转换成一个对象时，里面的参数不能是一个表，而是一行数据
            }

            #endregion

            return resData;
        }
        #endregion
    }
}
