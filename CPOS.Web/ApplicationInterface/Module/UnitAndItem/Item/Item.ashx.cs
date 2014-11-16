/*
 * Author		:Alex.tian
 * EMail		:changjian.tian@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/5/29 19:13
 * Description	:门店和商品
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
using System.Data;
using System.Linq;
using System.Web;

using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
using System.Data;
using System.Collections;
using System.Net;
using JIT.CPOS.Web.OnlineShopping.data;
namespace JIT.CPOS.Web.ApplicationInterface.Module.UnitAndItem.Item
{
    /// <summary>
    /// getItemDetail 的摘要说明
    /// </summary>
    public class Item : BaseGateway
    {
        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst = string.Empty;
            switch (pAction)
            {
                case "getItemDetail":  //获取商品详情
                    rst = getItemDetail(pRequest);
                    break;
                case "getItemHomePageList"://获取首页商品
                    rst = getItemHomePageList(pRequest);
                    break;
                //case "SetItemComment": //商品评论
                //    rst = SetItemComment(pRequest);
                //    break;
                case "GetItemCategoryList":
                    rst = GetItemCategoryList(pRequest);
                    break;
                case "GetGoodsList":
                    rst = GetGoodsList(pRequest);
                    break;
                case "GetItemCategoryByParentId"://根据父分类查询分类
                    rst = GetItemCategoryByParentId(pRequest);
                    break;
                case "GetItemCommentByItemId"://根据父分类查询分类
                    rst = GetItemCommentByItemId(pRequest);
                    break;
                case "GetInoutOrderByItemId"://根据父分类查询分类
                    rst = GetInoutOrderByItemId(pRequest);
                    break;
                    
                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction)) { ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER };
            }
            return rst;
        }
        #region 获取商品详细信息
        public string getItemDetail(string pRequest)
        {
            getItemDetailRespData rd = new getItemDetailRespData();
            const int NULL_CUSTOMERID = 304;
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<getItemDetailRP>>();  //将请求参数反序列化成对象
                if (string.IsNullOrWhiteSpace(rp.CustomerID))
                {
                    throw new APIException("客户ID不能为空！") { ErrorCode = NULL_CUSTOMERID };
                }
                else
                {
                    var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
                    string userId = rp.UserID;
                    string itemId = rp.Parameters.itemId;
                    string Lng = rp.Parameters.Lng;
                    string Dim = rp.Parameters.Dim;
                    //初始化返回对象
                    rd.content = new getItemDetailRespContentData();
                    OnlineShoppingItemBLL itemService = new OnlineShoppingItemBLL(loggingSessionInfo);
                    //商品基本信息
                    var dsItems = itemService.GetItemDetailByItemId(itemId, userId);
                    if (dsItems != null && dsItems.Tables.Count > 0 && dsItems.Tables[0].Rows.Count > 0)
                    {
                        rd.content =
                            DataTableToObject.ConvertToObject<getItemDetailRespContentData>(dsItems.Tables[0].Rows[0]);
                    }
                    rd.content.imageList = new List<getItemDetailRespContentDataImage>();
                    rd.content.skuList = new List<getItemDetailRespContentDataSku>();
                    rd.content.salesUserList = new List<getItemDetailRespContentDataSalesUser>();
                    rd.content.storeInfo = new getItemDetailRespContentDataStore();
                    rd.content.brandInfo = new getItemDetailRespContentDataBrand();
                    rd.content.skuInfo = new getItemDetailRespContentDataSkuInfo();
                    rd.content.prop1List = new List<getItemDetailRespContentDataProp1>();

                    //rd.content.itemCommentList = new getItemComment(); //获取评论
                    rd.content.NearbyStore = new GetNearbyStore(); //获取门店

                    //商品图片信息
                    
                    var dsImages = itemService.GetItemImageList(itemId);
                    if (dsImages != null && dsImages.Tables.Count > 0 && dsImages.Tables[0].Rows.Count > 0)
                    {
                        //rd.content.imageList = DataTableToObject.ConvertToList<getItemDetailRespContentDataImage>(dsImages.Tables[0]);
                        var temimageList = new List<getItemDetailRespContentDataImage> { };
                        foreach (DataRow item in dsImages.Tables[0].Rows)
                        {
                            getItemDetailRespContentDataImage image = new getItemDetailRespContentDataImage();
                            image.imageId = item["imageId"].ToString();
                            if (!string.IsNullOrWhiteSpace(item["imageURL"].ToString()))
                            {
                                image.imageURL = ImagePathUtil.GetImagePathStr(item["imageURL"].ToString(), "480");
                            }
                            temimageList.Add(image);
                        }
                        rd.content.imageList = temimageList;
                    }
                    //商品sku信息
                    var dsSkus = itemService.GetItemSkuList(itemId);
                    if (dsSkus != null && dsSkus.Tables.Count > 0 && dsSkus.Tables[0].Rows.Count > 0)
                    {
                        rd.content.skuList =
                            DataTableToObject.ConvertToList<getItemDetailRespContentDataSku>(dsSkus.Tables[0]);
                        // 获取sku对象
                        SkuInfo skuInfo = new SkuInfo();
                        SkuService skuService = new SkuService(loggingSessionInfo);
                        skuInfo = skuService.GetSkuInfoById(rd.content.skuList[0].skuId);
                        rd.content.skuInfo.skuId = skuInfo.sku_id;
                        rd.content.skuInfo.prop1DetailId = skuInfo.prop_1_detail_id;
                        rd.content.skuInfo.prop2DetailId = skuInfo.prop_2_detail_id;
                        //---------------------------------------------------------------------
                    }
                    //购买用户集合
                    var dsSalesUsers = itemService.GetItemSalesUserList(itemId);
                    if (dsSalesUsers != null && dsSalesUsers.Tables.Count > 0 && dsSalesUsers.Tables[0].Rows.Count > 0)
                    {
                        rd.content.salesUserList =
                            DataTableToObject.ConvertToList<getItemDetailRespContentDataSalesUser>(dsSalesUsers.Tables[0]);
                    }

                    //门店信息
                    var dsStore = itemService.GetItemStoreInfo(itemId);
                    if (dsStore != null && dsStore.Tables.Count > 0 && dsStore.Tables[0].Rows.Count > 0)
                    {
                        rd.content.storeInfo =
                            DataTableToObject.ConvertToObject<getItemDetailRespContentDataStore>(dsStore.Tables[0].Rows[0]);
                    }
                    //品牌信息
                    var dsBrand = itemService.GetItemBrandInfo(itemId);
                    if (dsBrand != null && dsBrand.Tables.Count > 0 && dsBrand.Tables[0].Rows.Count > 0)
                    {
                        rd.content.brandInfo =
                            DataTableToObject.ConvertToObject<getItemDetailRespContentDataBrand>(dsBrand.Tables[0].Rows[0]);
                    }
                    var dsProp1 = itemService.GetItemProp1List(itemId);
                    if (dsProp1 != null && dsProp1.Tables.Count > 0 && dsProp1.Tables[0].Rows.Count > 0)
                    {
                        rd.content.prop1List =
                            DataTableToObject.ConvertToList<getItemDetailRespContentDataProp1>(dsProp1.Tables[0]);
                    }

                    ////获取评论信息（取最新评论的第一条）
                    //var dsItemComment = itemService.GetItemCommentList(itemId);
                    //if (dsItemComment != null && dsItemComment.Tables.Count > 0 && dsItemComment.Tables[0].Rows.Count > 0)
                    //{
                    //    rd.content.itemCommentList =
                    //        DataTableToObject.ConvertToObject<getItemComment>(dsItemComment.Tables[0].Rows[0]);
                    //}
                    //获取门店信息（1.如果有经度和纬度，取最近的一家门店。2如果没有最近门店。取第一家门店）
                    ItemService itemServices = new ItemService(loggingSessionInfo);
                    var dsNearbyStore = itemServices.GetNearbyOneStore(Lng, Dim);
                    if (dsNearbyStore != null && dsNearbyStore.Tables.Count > 0)
                    {
                        if (dsNearbyStore.Tables[0].Rows.Count > 0)  //当根据经度纬度能找数据
                        {
                            rd.content.NearbyStore =
                           DataTableToObject.ConvertToObject<GetNearbyStore>(dsNearbyStore.Tables[0].Rows[0]);
                        }
                        else    //取top 1门店
                            rd.content.NearbyStore = DataTableToObject.ConvertToObject<GetNearbyStore>(dsNearbyStore.Tables[1].Rows[0]);
                    }
                    #region 获取商户信息 add by Henry 2014-10-10
                    var customerBll = new t_customerBLL(loggingSessionInfo);
                    var customerBasicSettingBll = new CustomerBasicSettingBLL(loggingSessionInfo);
                    t_customerEntity customerEntity = customerBll.GetByCustomerID(rp.CustomerID);           //获取商户名称
                    CustomerInfo customerInfo=new CustomerInfo ();
                    customerInfo.CustomerName = customerEntity == null ? "" : customerEntity.customer_name; //商户名称
                    customerInfo.ImageURL = customerBasicSettingBll.GetSettingValueByCode("AppLogo");       //商户Logo
                    customerInfo.CustomerMobile = customerBasicSettingBll.GetSettingValueByCode("CustomerMobile");  //商户电话
                    customerInfo.DeliveryStrategyDesc = customerBasicSettingBll.GetSettingValueByCode("DeliveryStrategy");//获取配送费策略描述 add by Herny 2014-10-31
                    rd.content.CustomerInfo = customerInfo;
                    #endregion
                }
                var rsp = new SuccessResponse<IAPIResponseData>(rd);
                return rsp.ToJSON();
            }
            catch (Exception ex)
            {
                throw new APIException(ex.Message);
            }
        }
        //Add by changjian.tian 2014-7-02
        public class ImagePathUtil
        {
            /// <summary>
            /// 获取图片缩略图路径
            /// </summary>
            /// <param name="imagePath">原图路径</param>
            /// <param name="size">缩略图大小</param>
            /// <returns></returns>
            public static string GetImagePathStr(string imagePath, string size)
            {
                if (!string.IsNullOrEmpty(imagePath))
                {
                    string temp = imagePath.Substring(0, imagePath.Length - 4);
                    string extension = imagePath.Substring(imagePath.Length - 3);
                    string newPath = temp + "_" + size + "." + extension;
                    if (RemoteFileExists(newPath))
                        return newPath;
                    else
                        return imagePath;
                }
                else
                    return imagePath;
            }
            /// <summary>
            /// 判断远程文件是否存在
            /// </summary>
            /// <param name="fileUrl"></param>
            /// <returns></returns>
            private static bool RemoteFileExists(string fileUrl)
            {
                bool result = false;//判断结果

                WebResponse response = null;
                try
                {
                    WebRequest req = WebRequest.Create(fileUrl);

                    response = req.GetResponse();

                    result = response == null ? false : true;

                }
                catch (Exception ex)
                {
                    result = false;
                }
                finally
                {
                    if (response != null)
                    {
                        response.Close();
                    }
                }

                return result;
            }
        }
        public class getChildCategoryReqData : ReqData
        {
            public getChildCategoryListReqSpecialData special{ get; set; }
        }
        public class getItemCategoryReqData : ReqData
        {
            public getItemListReqSpecialData special {get;set;}
        }
        public class getItemDetailRespData : IAPIResponseData
        {
            public getItemDetailRespContentData content { get; set; }
        }
        public class getItemDetailRespContentData
        {
            public string itemId { get; set; }      //商品标识
            public string itemName { get; set; }    //商品名称（譬如：浪漫主题房）
            public string pTypeId { get; set; }     //福利类别标识（团购=2，优惠=1）
            public string pTypeCode { get; set; }   //福利类别缩写（券，团）
            public string buyType { get; set; }     //优惠团购类型（1=预定2=购买；特别注意判断是否卖光，卖光了，但是没有下架，则为3，表示卖完啦）
            public string buyTypeDesc { get; set; } //根据buyType,显示1=马上预订，2=立即抢购，3=卖完啦
            public int salesPersonCount { get; set; }    //已购买人数量
            public int downloadPersonCount { get; set; } //已下载数量
            public decimal overCount { get; set; }  //剩余数量
            public string useInfo { get; set; }     //使用须知
            public string tel { get; set; }         //联系电话
            public string endTime { get; set; }     //下架日期
            public string couponURL { get; set; }   //优惠券下载地址
            public string offersTips { get; set; }  //优惠提示
            public int isKeep { get; set; }         //是否已收藏 1=是，0=否
            public int isShoppingCart { get; set; }  //是否已经加入购物车（1=已加入，0=未加入）
            public string prop1Name { get; set; }   //属性1名称
            public string prop2Name { get; set; }   //属性2名称
            public string isProp2 { get; set; }     //是否有属性2；1=有，0=无
            public string itemCategoryId { get; set; }
            public string itemCategoryName { get; set; }
            public string itemTypeDesc { get; set; }
            public string itemSortDesc { get; set; }
            public int salesQty { get; set; }
            public string remark { get; set; }
            //public string Forpoints { get; set; }//购买商品获取的积分数
            public decimal Forpoints { get; set; }//购买商品获取的积分数
            public string RoomDesc { get; set; }//房间描述
            public string RoomImg { get; set; }//房间图片
            public string itemIntroduce { get; set; } //商品介绍
            public string itemParaIntroduce { get; set; } // 商品参数介绍
            public string salesCount { get; set; }      //已购买数量
            public string deadlineTime { get; set; } //还有多少时间截止
            public IList<getItemDetailRespContentDataImage> imageList { get; set; }     //图片集合
            public IList<getItemDetailRespContentDataSku> skuList { get; set; }         //sku集合
            public IList<getItemDetailRespContentDataSalesUser> salesUserList { get; set; }   //购买用户集合
            public getItemDetailRespContentDataStore storeInfo { get; set; }            //门店对象（一家门店）
            public getItemDetailRespContentDataBrand brandInfo { get; set; }            //品牌信息
            public getItemDetailRespContentDataSkuInfo skuInfo { get; set; }                //默认sku标识
            public IList<getItemDetailRespContentDataProp1> prop1List { get; set; }     //属性1集合
            public IList<StoreItemDailyStatusEntity> storeItemDailyStatus; //状态集合

            //public getItemComment itemCommentList { get; set; }  //获取评论
            public GetNearbyStore NearbyStore { get; set; }   //获取附近门店 

            public CustomerInfo CustomerInfo { get; set; }    //商户信息 add by Henry 2014-10-10

            public string integralExchange { get; set; }    //兑换所需积分
            public string isExchange { get; set; }//是否积分兑换商品
            public decimal discountRate { get; set; }    //折扣
        }
        public class getItemDetailRespContentDataImage
        {
            public string imageId { get; set; }     //图片标识
            public string imageURL { get; set; }    //图片链接地址
        }
        public class getItemDetailRespContentDataSku
        {
            public string skuId { get; set; }        //sku标识
            public string skuProp1 { get; set; }     //规格
            public string skuProp2 { get; set; }
            public decimal price { get; set; }       //原价
            public decimal salesPrice { get; set; }  //优惠价（零售价格）
            public decimal discountRate { get; set; }//折扣
            public decimal integral { get; set; }    //获得积分

            public int integralExchange { get; set; }//需要积分值
        }
        public class getItemDetailRespContentDataSalesUser
        {
            public string userId { get; set; }      //用户标识
            public string imageURL { get; set; }    //用户头像链接地址
        }
        public class getItemDetailRespContentDataStore
        {
            public string storeId { get; set; }     //门店标识
            public string storeName { get; set; }   //门店名称
            public string address { get; set; }     //门店地址
            public string imageURL { get; set; }    //门店图片连接地址
            public int storeCount { get; set; }  //门店数量
        }

        public class getItemDetailRespContentDataBrand
        {
            public string brandId { get; set; }         //品牌标识
            public string brandLogoURL { get; set; }    //品牌logo图片链接地址
            public string brandName { get; set; }       //品牌名称
            public string brandEngName { get; set; }    //品牌英文名
        }
        /// <summary>
        /// 默认的sku信息
        /// </summary>
        public class getItemDetailRespContentDataSkuInfo
        {
            public string skuId { get; set; }               //sku标识
            public string prop1DetailId { get; set; }       //属性1明细标识
            public string prop2DetailId { get; set; }       //属性2明细标识
        }
        public class getItemDetailRespContentDataProp1
        {
            public string skuId { get; set; }               //sku标识
            public string prop1DetailId { get; set; }       //属性1明细标识
            public string prop1DetailName { get; set; }     //属性1明细名称
        }

        ////评价详情
        //public class getItemComment
        //{
        //    public string CommentUserName { get; set; }  //评论用户
        //    public string CommentUserImageUrl { get; set; }  //评论用户头像
        //    public string CommentContent { get; set; }   //评论内容
        //}
        //门店详情
        public class GetNearbyStore
        {
            public string StoreId { get; set; }  //门店ID
            public string StoreName { get; set; }   //门店名称
            public string imageURL { get; set; }  //门店图片
            public string Address { get; set; }  //门店地址
            public string Tel { get; set; }  //门店电话
            public double Distance { get; set; } //距离
        }
        /// <summary>
        /// 商户信息 add by Henry 2014-10-10
        /// </summary>
        public class CustomerInfo
        {
            public string CustomerName { get; set; }    //商户名称
            public string ImageURL { get; set; }        //商户Logo
            public string CustomerMobile { get; set; }  //商户电话
            public string DeliveryStrategyDesc { get; set; }//配送描述
        }
        #region  错误码
        public const int NULL_EXISTS_ITEMID = 301;
        public const int NULL_EXISTS_LNG = 302;
        public const int NULL_EXISTS_DIM = 303;
        #endregion
        public class getItemDetailRP : IAPIRequestParameter
        {
            public string itemId { get; set; } //商品标识
            public string Lng { get; set; }  //经度
            public string Dim { get; set; } // 维度
            public void Validate()
            {
                if (string.IsNullOrWhiteSpace(this.itemId))
                {
                    throw new APIException(string.Format("商品ID不能为空！")) { ErrorCode = NULL_EXISTS_ITEMID };
                }
                if (string.IsNullOrWhiteSpace(this.Lng))
                {
                    throw new APIException(string.Format("经度不能为空")) { ErrorCode = NULL_EXISTS_LNG };
                }
                if (string.IsNullOrWhiteSpace(this.Dim))
                {
                    throw new APIException(string.Format("纬度不能为空")) { ErrorCode = NULL_EXISTS_DIM };
                }
            }
        }
        #endregion

        #region 获取首页商品
        public string getItemHomePageList(string pRequest)
        {
            getItemHomePageRD rd = new getItemHomePageRD();
            const int NULL_CUSTOMERID = 304;
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<getItemHomePageRP>>();  //将请求参数反序列化成对象
                if (string.IsNullOrWhiteSpace(rp.CustomerID))
                {
                    throw new APIException("客户ID不能为空！") { ErrorCode = NULL_CUSTOMERID };
                }
                else
                {
                    var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
                    ItemService itemService = new ItemService(loggingSessionInfo);
                    DataSet ds = itemService.GetItemHomePageList(rp.CustomerID);
                    if (ds != null)
                    {
                        rd.CountItem = Convert.ToInt32(ds.Tables[0].Rows[0]["ConuntItem"].ToString());//全部商品数量
                        rd.CountUrl = "aldlinks://store/product/list/customerid=" + rp.CustomerID + "";
                        rd.NewCountItem = Convert.ToInt32(ds.Tables[1].Rows[0]["NewCount"].ToString()); //新上商品数量
                        rd.NewCountURL = "aldlinks://store/product/list/customerid=" + rp.CustomerID + "/type=newproduct";
                        List<ModuleObj> list = new List<ModuleObj> { };
                        if (ds.Tables[2].Rows.Count > 0)
                        {
                            ModuleObj obj = new ModuleObj();
                            obj.ModuleName = "热卖商品";
                            obj.GoodListInfo = DataTableToObject.ConvertToList<GoodsList>(ds.Tables[2]);
                            list.Add(obj);
                            rd.ModuleList = list.ToArray();
                        }
                        if (ds.Tables[3].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds.Tables[3].Rows)
                            {
                                if (row["SettingCode"].ToString().Equals("AppLogo"))
                                {
                                    rd.StoreLogoUrl = row["SettingValue"].ToString();   //店铺Logo
                                }
                                if (row["SettingCode"].ToString().Equals("AppTopBackground"))
                                {
                                    rd.StoreTopBackGroundUrl = row["SettingValue"].ToString(); ;  //顶部大图
                                }
                            }
                        }
                        else
                        {
                            if (string.IsNullOrWhiteSpace(System.Configuration.ConfigurationManager.AppSettings["AppLogo"]))
                            {
                                throw new APIException("WebConfig未配置店铺默认图片路径。") { ErrorCode = 301 };
                            }
                            else
                            {
                                //店铺首页Logo
                                rd.StoreLogoUrl = System.Configuration.ConfigurationManager.AppSettings["AppLogo"].ToString();
                            }
                            if (string.IsNullOrWhiteSpace(System.Configuration.ConfigurationManager.AppSettings["AppTopBackground"]))
                            {
                                throw new APIException("WebConfig未配置店铺默认Logo图片路径。") { ErrorCode = 302 };
                            }
                            else
                            {
                                //店铺大图
                                rd.StoreTopBackGroundUrl = System.Configuration.ConfigurationManager.AppSettings["AppTopBackground"].ToString();
                            }

                        }
                    }
                }
                var rsp = new SuccessResponse<IAPIResponseData>(rd);
                return rsp.ToJSON();
            }
            catch (Exception ex)
            {
                throw new APIException(ex.Message);
            }
        }
        /// <summary>
        /// 商品集合
        /// </summary>
        public class getItemHomePageRD : IAPIResponseData
        {
            public string StoreTopBackGroundUrl { get; set; }//顶部大图
            public string StoreLogoUrl { get; set; } //店铺Logo
            public int CountItem { get; set; } //全部宝贝
            public string CountUrl { get; set; } //全部宝贝URL
            public int NewCountItem { get; set; }// 新上宝贝
            public string NewCountURL { get; set; }//新上宝贝URL
            // public bool Iscollect { get; set; }  //是否收藏 【废弃。不能获取会员ID】

            public ModuleObj[] ModuleList { get; set; }//商品模块信息
        }
        /// <summary>
        /// 
        /// </summary>
        public class ModuleObj
        {
            public string ModuleName { get; set; } //模块名称
            public List<GoodsList> GoodListInfo { get; set; }  //商品模块列表
        }
        public class GoodsList
        {
            public string ItemDescription { get; set; }  //商品描述
            public string ItemUrl { get; set; }  //商品图片
            public decimal CostPrice { get; set; } //原价
            public decimal SalesPrice { get; set; } //促销价格
            public string ItemDetailUrl { get; set; } //点击商品详情URL
        }
        public class getItemHomePageRP : IAPIRequestParameter
        {
            public string CustomerID { get; set; } //客户ID
            public void Validate()
            {

            }
        }


        #endregion

        public string GetItemCategoryList(string pRequest)
        {
            //string strUrl = System.Configuration.ConfigurationManager.AppSettings["BizAppPrefixUrl"];
            var rd = new GetItemCategoryListRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<EmptyRequestParameter>>();
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            if (loggingSessionInfo == null)
            {
                throw new APIException("无效的客户ID【CustomerID】") { ErrorCode = 121 };
            }

            var categoryService = new ItemCategoryService(loggingSessionInfo);

            var ds = categoryService.GetItemCategoryInfoList(rp.CustomerID);
            var childDs = categoryService.GetItemCategoryChildList(rp.CustomerID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                rd.GetItemCategoryList =
                    DataTableToObject.ConvertToList<GetItemCategoryInfo>(ds.Tables[0]);

                foreach (var categoryInfo in rd.GetItemCategoryList)
                {
                    var info = categoryInfo;            
                    if (info.ItemCategoryParentId != "-99")
                    {
                        var temp =
                            childDs.Tables[0].AsEnumerable()
                                .Where(t => t["ItemCategoryParentId"].ToString() == info.ItemCategoryId)
                                .Where(t => t["ItemCategoryParentId"].ToString() != "-99")
                                .Select(t => new GetItemCategoryInfo
                                {
                                    ItemCategoryId = t["ItemCategoryId"].ToString(),
                                    ItemCategoryName = t["ItemCategoryName"].ToString(),
                                    ItemCategoryParentId = t["ItemCategoryParentId"].ToString(),
                                    ImageUrl = t["imageurl"].ToString(),
                                    IsHave = childDs.Tables[0].AsEnumerable()
                                            .Where(h => h["ItemCategoryParentId"].ToString() == t["ItemCategoryId"].ToString()).Count()
                                });
                        categoryInfo.ItemCategoryChildList = temp.ToArray();
                        //目标链接
                        //categoryInfo.tagartUrl = strUrl + "goodList.html?action=show&customerId=" + rp.CustomerID + "&itemTypeId=" + info.ItemCategoryId + ""; ;
                    }
                }
            }
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }

        public string GetItemCategories(string pRequest)
        {
            //string strUrl = System.Configuration.ConfigurationManager.AppSettings["BizAppPrefixUrl"];
            var rd = new GetItemCategoryListRD();
            var rp = pRequest.DeserializeJSONTo<getItemCategoryReqData>();
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.common.customerId, "1");
            if (loggingSessionInfo == null)
            {
                throw new APIException("无效的客户ID【CustomerID】") { ErrorCode = 121 };
            }

            var categoryService = new ItemCategoryService(loggingSessionInfo);

            var ds = categoryService.GetItemCategoryInfoList(rp.common.customerId);
            var childDs = categoryService.GetItemCategoryChildList(rp.common.customerId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                rd.GetItemCategoryList =
                    DataTableToObject.ConvertToList<GetItemCategoryInfo>(ds.Tables[0]);

                foreach (var categoryInfo in rd.GetItemCategoryList)
                {
                    var info = categoryInfo;
                    if (info.ItemCategoryParentId != "-99")
                    {
                        var temp =
                            childDs.Tables[0].AsEnumerable()
                                .Where(t => t["ItemCategoryParentId"].ToString() == info.ItemCategoryId)
                                .Where(t => t["ItemCategoryParentId"].ToString() != "-99")
                                .Select(t => new GetItemCategoryInfo
                                {
                                    ItemCategoryId = t["ItemCategoryId"].ToString(),
                                    ItemCategoryName = t["ItemCategoryName"].ToString(),
                                    ItemCategoryParentId = t["ItemCategoryParentId"].ToString(),
                                    ImageUrl = t["imageurl"].ToString(),
                                    IsHave = childDs.Tables[0].AsEnumerable()
                                            .Where(h => h["ItemCategoryParentId"].ToString() == t["ItemCategoryId"].ToString()).Count()
                                });
                        categoryInfo.ItemCategoryChildList = temp.ToArray();
                        //目标链接
                        //categoryInfo.tagartUrl = strUrl + "goodList.html?action=show&customerId=" + rp.CustomerID + "&itemTypeId=" + info.ItemCategoryId + ""; ;
                    }
                }
            }
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }
        /// <summary>
        /// 根据父分类查询分类
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetItemCategoryByParentId(string pRequest)
        {
            var rd = new GetItemCategoryListRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetCategoryByParentIdRP>>();
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            if (loggingSessionInfo == null)
                throw new APIException("无效的请求参数") { ErrorCode = 121 };
            var categoryService = new ItemCategoryService(loggingSessionInfo);
            var dsCategory = categoryService.GetItemCategoryByParentID(rp.Parameters.ParentID);
            if (dsCategory.Tables[0].Rows.Count > 0)
                rd.GetItemCategoryList = DataTableToObject.ConvertToList<GetItemCategoryInfo>(dsCategory.Tables[0]);
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            if (rsp != null)
                return rsp.ToJSON();
            return "";
        }
        /// <summary>
        /// 微信接口
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetItemCategoriesByParentId(string pRequest)
        {
            var rd = new GetItemCategoryListRD();
            var rp = pRequest.DeserializeJSONTo<getChildCategoryReqData>();
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.common.customerId, "1");
            if (loggingSessionInfo == null)
                throw new APIException("无效的请求参数") { ErrorCode = 121 };
            var categoryService = new ItemCategoryService(loggingSessionInfo);
            var dsCategory = categoryService.GetItemCategoryByParentID(rp.special.ParentId);
            if (dsCategory.Tables[0].Rows.Count > 0)
                rd.GetItemCategoryList = DataTableToObject.ConvertToList<GetItemCategoryInfo>(dsCategory.Tables[0]);
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            if (rsp != null)
                return rsp.ToJSON();
            return "";
        }
        public string GetGoodsList(string pRequest)
        {
            var rd = new GetGoodsListRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetGoodsListRP>>();//泛型的类里还带泛型，牛
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            if (loggingSessionInfo == null)
            {
                throw new APIException("无效的客户ID【CustomerID】") { ErrorCode = 121 };
            }

            var categoryService = new ItemCategoryService(loggingSessionInfo);

            var ds = categoryService.GetGoodsList(rp.CustomerID, rp.Parameters.CategoryId,
                rp.Parameters.GoodsTypeId, rp.Parameters.TypeDisplayIndex, rp.Parameters.GoodsName,
                rp.Parameters.PageIndex ?? 0,
                rp.Parameters.PageSize ?? 15);

            if (ds.Tables[0].Rows.Count > 0)
            {
                var temp = ds.Tables[0].AsEnumerable().Select(t => new GetGoodsListInfo
                {
                    ItemId = t["ItemId"].ToString(),
                    ItemName = t["ItemName"].ToString(),
                    ItemUrl = t["ItemUrl"].ToString(),
                    SalesQty = Convert.ToInt32(t["SalesQty"].ToString()),
                    SalesPrice = Convert.ToDecimal(t["SalesPrice"].ToString()),
                    CategoryId = t["CategoryId"].ToString(),
                    CategoryName = t["CategoryName"].ToString(),
                    SkuId = t["SkuId"].ToString(),
                    Price = Convert.ToDecimal(t["Price"].ToString())
                });
                rd.GetGoodsList = temp.ToArray();
            }
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }

        /// <summary>
        /// 根据商品标识获取商品的评论，按照评论时间，进行倒序排序
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetItemCommentByItemId(string pRequest)
        {
            var rd = new GetItemCommentByItemIdRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetItemCommentByItemIdRP>>();//泛型的类里还带泛型，牛
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            if (loggingSessionInfo == null)
            {
                throw new APIException("无效的客户ID【CustomerID】") { ErrorCode = 121 };
            }

            var itemService = new ItemService(loggingSessionInfo);

            var ds = itemService.GetItemCommentByItemId(rp.CustomerID, rp.Parameters.ItemId,
            rp.Parameters.PageIndex ?? 0,
                rp.Parameters.PageSize ?? 15);

            if (ds.Tables[0].Rows.Count > 0)
            {
                var temp = ds.Tables[0].AsEnumerable().Select(t => new GetItemCommentByItemIdInfo
                {
                    ItemId = t["ItemId"].ToString(),
                    ItemCommentId = t["ItemCommentId"].ToString(),
                    VipId = t["VipId"].ToString(),
                    HeadImgUrl = t["HeadImgUrl"].ToString(),
                    VipName = t["VipName"].ToString(),
                    VipLevel = t["VipLevel"].ToString(),
                    CommentContent = t["CommentContent"].ToString(),
                    CreateTime = t["CreateTime"].ToString(),
                   
                });
                rd.GetItemCommentByItemIdInfoList = temp.ToArray();
            }
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        } 
        /// <summary>
        /// 根据商品标识，获取成交支付成功的订单集合，按照支付时间倒序
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetInoutOrderByItemId(string pRequest)
        {
            var rd = new GetInoutOrderByItemIdRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetInoutOrderByItemIdRP>>();//泛型的类里还带泛型，外围的泛型类是一个通用的类，里面传给他的类是动态的，占位符
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            if (loggingSessionInfo == null)
            {
                throw new APIException("无效的客户ID【CustomerID】") { ErrorCode = 121 };
            }

            var itemService = new ItemService(loggingSessionInfo);

            var ds = itemService.GetInoutOrderByItemId(rp.CustomerID, rp.Parameters.ItemId,
            rp.Parameters.PageIndex ?? 0,
                rp.Parameters.PageSize ?? 15);

            if (ds.Tables[0].Rows.Count > 0)
            {
                var temp = ds.Tables[0].AsEnumerable().Select(t => new GetInoutOrderByItemIdInfo
                {
                    OrderId = t["OrderId"].ToString(),
                    VipId = t["VipId"].ToString(),
                    HeadImgUrl = t["HeadImgUrl"].ToString(),
                    VipName = t["VipName"].ToString(),
                    VipLevel = t["VipLevel"].ToString(),
                    Price = t["Price"] == null || t["Price"] is DBNull ? 0 : Convert.ToDecimal(t["Price"]),
                    Qty = t["Qty"] == null || t["Qty"] is DBNull ? 0 : Convert.ToInt32(t["Qty"]),
                    ItemDesc = t["ItemDesc"].ToString(),
                    PayTime = Convert.ToDateTime(t["PayTime"])

                });
                rd.GetInoutOrderByItemIdList = temp.ToArray();
            }
            var rsp = new SuccessResponse<IAPIResponseData>(rd);//IAPIResponseDataAPI响应数据的接口rd继承自它。
            return rsp.ToJSON();
        }


       #region 插入商品评论 弃用
        //public string SetItemComment(string pRequest)
        //{
        //    var rd = new EmptyResponseData();
        //    try
        //    {
        //        var rp = pRequest.DeserializeJSONTo<APIRequest<ItemCommentRP>>();  //将请求参数反序列化成对象
        //        var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

        //          ItemCommentBLL bll = new ItemCommentBLL(loggingSessionInfo);
        //          var entity = new ItemCommentEntity() { 
        //          ItemCommentId=BaseService.NewGuidPub(),
        //          ItemId=rp.Parameters.ItemId,
        //          CustomerId=rp.CustomerID,
        //          CommentType=rp.Parameters.CommentType,
        //          CommentImageUrl=rp.Parameters.CommentImageUrl,
        //          CommentVideoUrl=rp.Parameters.CommentVideoUrl,
        //          CommentContent=rp.Parameters.CommentContent,
        //          CommentTime=DateTime.Now,
        //          CommentUserName=loggingSessionInfo.CurrentUser.Create_User_Name,
        //          CommentUserImageUrl=loggingSessionInfo.CurrentUser.imageUrl,
        //          IsVip=1,
        //          VipId=rp.UserID,
        //        };
        //        bll.Create(entity);
        //        var rsp = new SuccessResonse<IAPIResponseData>(rd);
        //        return rsp.ToJSON();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new APIException(ex.Message);
        //    }
        //}
        //public class ItemCommentRP:IAPIRequestParameter
        //{
        //    /// <summary>
        //    /// 主标识
        //    /// </summary>
        //    public string ItemCommentId { get; set; }

        //    /// <summary>
        //    /// 商品ID
        //    /// </summary>
        //    public string ItemId { get; set; }

        //    /// <summary>
        //    /// 客户ID
        //    /// </summary>
        //   // public string Customer_id { get; set; }

        //    /// <summary>
        //    /// 评论介质类型 1.图片。2视屏
        //    /// </summary>
        //    public int CommentType { get; set; }

        //    /// <summary>
        //    /// 评论介质图片
        //    /// </summary>
        //    public string CommentImageUrl { get; set; }

        //    /// <summary>
        //    /// 评论介质视屏
        //    /// </summary>
        //    public string CommentVideoUrl { get; set; }

        //    /// <summary>
        //    /// 评论内容
        //    /// </summary>
        //    public string CommentContent { get; set; }

        //    //评论时间
        //    //评论用户
        //    //评论用户头像
        //    /// <summary>
        //    /// 是否是VIP用户
        //    /// </summary>
        //    public string IsVip { get; set; }

        //    /// <summary>
        //    /// VIPID
        //    /// </summary>
        //    public string VIPID { get; set; }


        //    #region 错误码
        //    const int NULL_COMMENTIMAGEURL = 305;
        //    const int NULL_COMMENTVIDEOURL = 306;
        //    #endregion

        //    public void Validate()
        //    {
        //        if (this.CommentType==1)  //当选择的评论介质是图片时。图片地址不能为空
        //        {
        //            if (string.IsNullOrWhiteSpace(this.CommentImageUrl))
        //            {
        //                throw new APIException("您选择的评论介质是图片类型。请写图片地址！") {ErrorCode=NULL_COMMENTIMAGEURL };
        //            }
        //        }
        //        if (this.CommentType==2) //当选择的评论介质是视屏类型时候。视频不能为空
        //        {
        //            if (string.IsNullOrWhiteSpace(this.CommentVideoUrl))
        //            {
        //                throw new APIException("您选择的评论介质是视屏类型。请选择视频链接！") {ErrorCode=NULL_COMMENTVIDEOURL };
        //            }
        //        }
        //    }
        //}
        //public class ItemCommentRD : IAPIResponseData
        //{     

        //}
       #endregion
    }

    public class GetItemCategoryListRD : IAPIResponseData
    {
        public List<GetItemCategoryInfo> GetItemCategoryList { get; set; }
    }
    public class getItemListReqSpecialData
    {
        public string itemName { get; set; } //模糊查询商品名称
        public string itemTypeId { get; set; } //商品类别标识
        public int page { get; set; } //页码
        public int pageSize { get; set; } //页面数量
        public string isExchange { get; set; } //兑换商品
        public string storeId { get; set; } //门店标识 Jermyn20131008
    }
    public class getChildCategoryListReqSpecialData
    {
        public string ParentId { get; set; }
    }
    public class GetItemCategoryInfo
    {
        public string ItemCategoryId { get; set; }
        public string ItemCategoryName { get; set; }
        public string ItemCategoryParentId { get; set; }

        //public string tagartUrl { get; set; }
        public string ImageUrl { get;set; }
        /// <summary>
        /// 是否包含子集，大于0表示包含
        /// </summary>
        public int IsHave { get; set; }

        public GetItemCategoryInfo[] ItemCategoryChildList { get; set; }
    }
    
    public class GetGoodsListRP : IAPIRequestParameter
    {
        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }
        /// <summary>
        /// 商品分类ID
        /// </summary>
        public string CategoryId { get; set; }
        /// <summary>
        /// 促销= 1，新品=2，价格=3，销量=4
        /// </summary>
        public int GoodsTypeId { get; set; }
        /// <summary>
        /// 0=正序，1=倒序
        /// </summary>
        public int TypeDisplayIndex { get; set; }
        /// <summary>
        /// 搜索条件【目前只支持商品名称】
        /// </summary>
        public string GoodsName { get; set; }

        public void Validate()
        {
        }

    }

    /// <summary>
    /// 根据父分类获取集合
    /// </summary>
    public class GetCategoryByParentIdRP : IAPIRequestParameter
    {
        //父分类ID
        public string ParentID { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(this.ParentID))
            {
                throw new APIException(string.Format("ParentID不能为空")) { ErrorCode = 301 };
            }
        }
    }

    public class GetGoodsListRD : IAPIResponseData
    {
        public GetGoodsListInfo[] GetGoodsList { get; set; }
    }

    public class GetGoodsListInfo
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemUrl { get; set; }
        public int SalesQty { get; set; }
        public decimal SalesPrice { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string SkuId { get; set; }
        public decimal Price { get; set; }


    }
 
    #region 根据商品标识获取商品的评论，请求参数和 response的Data数据
    public class GetItemCommentByItemIdRP : IAPIRequestParameter
    {
        /// <summary>
        /// 根据商品标识获取商品评论集合,special参数
        /// </summary>

        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }
        /// <summary>
        /// 商品分类ID
        /// </summary>
        public string ItemId { get; set; }
        public void Validate()
        {
        }

    }
    public class GetItemCommentByItemIdRD : IAPIResponseData
    {
        public GetItemCommentByItemIdInfo[] GetItemCommentByItemIdInfoList { get; set; }
    }

    public class GetItemCommentByItemIdInfo
    {
        public string ItemId { get; set; }
        public string ItemCommentId { get; set; }
        public string VipId { get; set; }
        public string HeadImgUrl { get; set; }
        public string VipName { get; set; }
        public string VipLevel { get; set; }
        public string CommentContent { get; set; }
        public string CreateTime { get; set; }     

    }
#endregion
    #region 根据商品标识，获取成交支付成功的订单集合   ，请求参数和response的Data数据

    /// <summary>
    /// 根据商品标识，获取成交支付成功的订单集合，按照支付时间倒序,special参数
    /// </summary>
    public class GetInoutOrderByItemIdRP : IAPIRequestParameter
    {
        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }
        /// <summary>
        /// 商品分类ID
        /// </summary>
        public string ItemId { get; set; }
        public void Validate()
        {
        }

    }
    public class GetInoutOrderByItemIdRD : IAPIResponseData
    {
        public GetInoutOrderByItemIdInfo[] GetInoutOrderByItemIdList { get; set; }
    }

    public class GetInoutOrderByItemIdInfo
    {
        public string OrderId { get; set; }
        public string VipId { get; set; }
        public string HeadImgUrl { get; set; }
        public string VipName { get; set; }
        public string VipLevel { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public string ItemDesc { get; set; }
        public DateTime PayTime { get; set; }
    }
    #endregion

}