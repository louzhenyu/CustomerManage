/*
 * Author		:Alex.Tian
 * EMail		:changjian.tian@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/7/2 12:00:00
 * Description	:获取首页
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using System.Data;

namespace JIT.CPOS.Web.ApplicationInterface.Module.AppConfig.HomePageConfig
{
    public class HomePageConfigV2AH : BaseActionHandler<HomePageConfigV2RP, HomePageConfigV2RD>
    {

        protected override HomePageConfigV2RD ProcessRequest(APIRequest<HomePageConfigV2RP> pRequest)
        {
            //创建连接用户对象
            var logginUserInfo = base.CurrentUserInfo;
            HomePageConfigV2RD RD = new HomePageConfigV2RD();
            var adBll = new MHAdAreaBLL(logginUserInfo); //广告部分
            #region 1.获取首页layout模块
            List<layoutList> ArrlayoutList = new List<layoutList> { };  //用于填充layoutList数据集合
            MHCategoryAreaGroupBLL mhBLL = new MHCategoryAreaGroupBLL(logginUserInfo); //Layout部分
            DataSet layout = mhBLL.GetLayoutList(logginUserInfo.ClientID);
            if (layout != null && layout.Tables[0].Rows.Count > 0)
            {
                layoutList layoutinfo = new layoutList();
                layoutinfo.layoutid = "001";
                layoutinfo.x = 0;
                layoutinfo.y = 0;
                layoutinfo.width = 320;
                layoutinfo.height = 101;
                layoutinfo.type = "ad_1";  //广告
                ArrlayoutList.Add(layoutinfo);
                int strhight = layoutinfo.height;
                int bY = 0;
                for (int i = 0; i < layout.Tables[0].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        layoutinfo = new layoutList();
                        layoutinfo.layoutid = layout.Tables[0].Rows[i]["GroupValue"].ToString();
                        layoutinfo.x = 0;
                        layoutinfo.y = strhight;
                        layoutinfo.width = 320;
                        layoutinfo.height = Convert.ToInt32(layout.Tables[0].Rows[i]["Height"].ToString());
                        layoutinfo.type = layout.Tables[0].Rows[i]["ModelType"].ToString();
                        bY = layoutinfo.y;
                    }
                    else
                    {
                        int bHeight = Convert.ToInt32(layout.Tables[0].Rows[i - 1]["height"]);
                        int bTopDistance = Convert.ToInt32(layout.Tables[0].Rows[i - 1]["TopDistance"]);
                        layoutinfo = new layoutList();
                        layoutinfo.layoutid = layout.Tables[0].Rows[i]["GroupValue"].ToString();
                        layoutinfo.x = 0;
                        layoutinfo.y = bY + bHeight + bTopDistance;
                        layoutinfo.width = 320;
                        layoutinfo.height = Convert.ToInt32(layout.Tables[0].Rows[i]["height"].ToString());
                        layoutinfo.type = layout.Tables[0].Rows[i]["modeltype"].ToString();
                        bY = layoutinfo.y;
                    }
                    ArrlayoutList.Add(layoutinfo);
                }
            }
            #endregion

            #region 2.获取homedata模块
            var ArrhomedataList = new List<homedataList> { };//用于填充homedata数据集合

            var homeBll = new MobileHomeBLL(this.CurrentUserInfo);
            //根据客户ID获取homeList集合
            var homeList = homeBll.QueryByEntity(new MobileHomeEntity { CustomerId = this.CurrentUserInfo.ClientID }, null);
            if (homeList != null)
            {
                var homeEntity = homeList.FirstOrDefault();
                var dsGroup = adBll.GetCategoryGroupId(homeEntity.HomeId.ToString());
                var ArriteminfoList = new List<itemsList> { };
                homedataList homeinfo = new homedataList();
                itemsList tempAd = new itemsList();

                #region  获取广告部分
                var tempAdArealist = adBll.GetByCustomerID().OrderBy(t => t.DisplayIndex);//获取客户下图片广告集合
                if (tempAdArealist != null)
                {
                    homeinfo.layoutid = "001";
                    homeinfo.title = "Title";
                    foreach (var item in tempAdArealist)
                    {
                        tempAd = new itemsList();
                        tempAd.imgUrl = item.ImageUrl;
                        tempAd.tagartUrl = GettagartUrl("MHAdArea", Convert.ToInt32(item.ObjectTypeId), CurrentUserInfo.ClientID.ToString(), item.ObjectId, item.Url,"");
                        tempAd.biztype = "";
                        tempAd.trackid = "";
                        ArriteminfoList.Add(tempAd); //广告
                    }
                    homeinfo.items = ArriteminfoList.ToArray();
                    ArrhomedataList.Add(homeinfo);
                }
                #endregion

                #region 获取分类部分
                if (dsGroup != null && dsGroup.Tables.Count > 0 && dsGroup.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsGroup.Tables[0].Rows)
                    {
                        ArriteminfoList = new List<itemsList> { };
                        homeinfo = new homedataList();
                        homeinfo.layoutid = dr["GroupId"].ToString();
                        homeinfo.title = "Title";
                        var dsItem = adBll.GetItemList(homeinfo.layoutid, homeEntity.HomeId.ToString());
                        if (dsItem != null && dsItem.Tables.Count > 0 && dsItem.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow itemrow in dsItem.Tables[0].Rows)
                            {
                                tempAd = new itemsList();
                                tempAd.imgUrl = itemrow["imageUrl"].ToString();
                                tempAd.tagartUrl = GettagartUrl("MHCategoryArea", Convert.ToInt32(itemrow["typeId"]), CurrentUserInfo.ClientID.ToString(), itemrow["objectId"].ToString(), itemrow["imageUrl"].ToString(), itemrow["categoryAreaId"].ToString());
                                tempAd.biztype = "";
                                tempAd.trackid = "1314";
                                ArriteminfoList.Add(tempAd);
                            }
                            homeinfo.items = ArriteminfoList.ToArray(); //分类部分
                            ArrhomedataList.Add(homeinfo);
                        }
                    }
                }
                #endregion
            }
            #endregion

            RD.timespan = "";
            RD.version = "";
            RD.layout = ArrlayoutList.ToArray();
            RD.homedata = ArrhomedataList.ToArray();
            return RD;

        }
        /// <summary>
        /// 根据表名。对象类别获取tagartUrl
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public static string GettagartUrl(string TableName, int objectTypeId, string customerID, string ObjectId, string Url,string categoryId)
        {
            string tagartUrl = string.Empty;
            switch (TableName)
            {
                case "MHAdArea":  //移动终端首页广告区域
                    if (objectTypeId == 1)  //对象类型1=活动2=资讯3=商品4=门店5=商品详情
                    {
                        // tagartUrl = "aldlinks://panicbuy/detail/customerid="+customerID+"/itemid="+ObjectId+"";
                        tagartUrl = "aldlinks://storealbum/list/";
                    }
                    else if (objectTypeId == 2)  //资讯
                    {
                        tagartUrl = Url;
                    }
                    else if (objectTypeId == 3) //商品列表 
                    {
                        tagartUrl = "aldlinks://product/list/customerid=" + customerID + "";
                    }
                    else if (objectTypeId == 4) //门店
                    {
                        tagartUrl = "aldlinks://store/home/customerid=" + customerID + "";
                    }
                    else if (objectTypeId == 5) //商品详情
                    {
                        tagartUrl = "aldlinks://product/detail/customerid=" + customerID + "/itemid=" + ObjectId + "";
                    }
                    break;
                case "MHCategoryArea"://移动终端首页商品分类区域
                    string strUrl = System.Configuration.ConfigurationManager.AppSettings["BizAppPrefixUrl"];
                    if (objectTypeId == 1)  //1.商品分类 2.商品
                    {
                        //tagartUrl = "aldlinks://store/category/customerid=" + customerID + "";
                        if (!string.IsNullOrWhiteSpace(strUrl))
                        {
                            tagartUrl = strUrl + "goodList.html?action=show&customerId=" + customerID + "&itemTypeId=" + ObjectId + "";
                        }
                        
                    }
                    else if (objectTypeId == 2) //商品
                    {
                        tagartUrl = "aldlinks://product/detail/customerid=" + customerID + "/itemid=" + ObjectId + "";
                    }else if(objectTypeId==8)//全部分类，调用H5链接
                    {
                        tagartUrl =strUrl+"category.html?Action=GetItemCategoryList&customerId="+customerID;
                    }
                    break;
                default:
                    break;
            }
            return tagartUrl;
        }
    }
    public class HomePageConfigV2RP : IAPIRequestParameter
    {
        public int CityID { get; set; }
        public void Validate()
        {

        }
    }
    public class HomePageConfigV2RD : IAPIResponseData
    {
        public string timespan { get; set; }
        public string version { get; set; }
        public layoutList[] layout { get; set; }
        public homedataList[] homedata { get; set; }
    }
    public class layoutList
    {
        public string layoutid { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string type { get; set; }
    }
    public class homedataList
    {
        public string layoutid { get; set; }
        public string title { get; set; }
        public itemsList[] items { get; set; }
    }
    public class itemsList
    {
        public string imgUrl { get; set; }
        public string tagartUrl { get; set; }
        public string biztype { get; set; }
        public string trackid { get; set; }
    }
}