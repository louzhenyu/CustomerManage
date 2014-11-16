using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.ExtensionMethod;
using System.Net;
using System.IO;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Common;
using System.Data;
using System.Web;
using JIT.Utility;

namespace JIT.CPOS.ZODataTransfer
{
    /// <summary>
    /// 同步福利数据接口
    /// </summary>
    public class SynWelfare
    {
        #region 1、同步福利类型 syncwelFarePreferentialType

        public void SyncWelfareType()
        {
            var interfaceName = "syncwelFarePreferentialType";
            var loggingSessionInfo = BaseService.GetLoggingSession();
            var categoryService = new ItemCategoryService(loggingSessionInfo);

            var dsTypes = new DataSet();
            var preTypeList = new PreferentialTypeList();
            preTypeList.preferentialtypelist = new List<PreferentialType>();

            //更新接口同步表
            var queryList = UpdateInterfaceTimestamp(interfaceName, loggingSessionInfo);

            if (queryList != null && queryList.Length > 0)
            {
                //存在，根据日期条件查询
                dsTypes = categoryService.GetSynWelfareTypeList(queryList.FirstOrDefault().LatestTime.ToString());
            }
            else
            {
                //不存在，查询所有数据
                dsTypes = categoryService.GetSynWelfareTypeList(string.Empty);
            }

            if (dsTypes != null && dsTypes.Tables.Count > 0 && dsTypes.Tables[0].Rows.Count > 0)
            {
                preTypeList.preferentialtypelist = DataTableToObject.ConvertToList<PreferentialType>(dsTypes.Tables[0]);

                //上传数据
                var content = preTypeList.ToJSON();
                var result = UploadData(interfaceName, content);

                //写入接口日志
                var logEntity = new ZInterfaceLogEntity()
                {
                    LogId = Utils.NewGuid(),
                    InterfaceName = interfaceName,
                    Params = content,
                    ResultCode = result.code,
                    ResultDesc = result.description
                };

                InsertInterfaceLog(logEntity, loggingSessionInfo);
            }
        }

        public class PreferentialTypeList
        {
            public List<PreferentialType> preferentialtypelist { get; set; }
        }

        public class PreferentialType
        {
            /// <summary>
            /// 主标识
            /// </summary>
            public string ptypeid { get; set; }
            /// <summary>
            /// 优惠名称
            /// </summary>
            public string ptypename { get; set; }
            /// <summary>
            /// 优惠号码
            /// </summary>
            public string ptypecode { get; set; }
            /// <summary>
            /// 描述
            /// </summary>
            public string ptypedesc { get; set; }
            /// <summary>
            /// 排序
            /// </summary>
            public int displayIndex { get; set; }
            /// <summary>
            /// 删除
            /// </summary>
            public int Isdelete { get; set; }
        }

        #endregion

        #region 2、同步福利品牌 syncwelFareBrand

        public void SyncWelfareBrand()
        {
            var interfaceName = "syncwelFareBrand";
            var loggingSessionInfo = BaseService.GetLoggingSession();
            var brandService = new BrandDetailBLL(loggingSessionInfo);

            var dsBrands = new DataSet();
            var brands = new BrandList();
            brands.brandlist = new List<Brand>();

            //更新接口同步表
            var queryList = UpdateInterfaceTimestamp(interfaceName, loggingSessionInfo);

            if (queryList != null && queryList.Length > 0)
            {
                //存在，根据日期条件查询
                dsBrands = brandService.GetSynWelfareBrandList(queryList.FirstOrDefault().LatestTime.ToString());
            }
            else
            {
                //不存在，查询所有数据
                dsBrands = brandService.GetSynWelfareBrandList(string.Empty);
            }

            if (dsBrands != null && dsBrands.Tables.Count > 0 && dsBrands.Tables[0].Rows.Count > 0)
            {
                brands.brandlist = DataTableToObject.ConvertToList<Brand>(dsBrands.Tables[0]);
                var content = brands.ToJSON();

                //将描述字段Encode
                foreach (var item in brands.brandlist)
                {
                    item.branddesc = HttpUtility.UrlEncode(item.branddesc, Encoding.UTF8);
                    byte[] buff = Encoding.UTF8.GetBytes(item.branddesc);
                    item.branddesc = Convert.ToBase64String(buff);
                }

                //上传数据
                var result = UploadData(interfaceName, brands.ToJSON());

                //写入接口日志
                var logEntity = new ZInterfaceLogEntity()
                {
                    LogId = Utils.NewGuid(),
                    InterfaceName = interfaceName,
                    Params = content,
                    ResultCode = result.code,
                    ResultDesc = result.description
                };

                InsertInterfaceLog(logEntity, loggingSessionInfo);
            }
        }

        public class BrandList
        {
            public List<Brand> brandlist { get; set; }
        }

        public class Brand
        {
            /// <summary>
            /// 品牌主键
            /// </summary>
            public string brandid { get; set; }
            /// <summary>
            /// 品牌名
            /// </summary>
            public string brandname { get; set; }
            /// <summary>
            /// 品牌code
            /// </summary>
            public string brandcode { get; set; }
            /// <summary>
            /// 描述
            /// </summary>
            public string branddesc { get; set; }
            /// <summary>
            /// 品牌英文名
            /// </summary>
            public string brandengname { get; set; }
            /// <summary>
            /// 排序
            /// </summary>
            public Int64 displayIndex { get; set; }
            /// <summary>
            /// logo地址
            /// </summary>
            public string brandlogourl { get; set; }
            /// <summary>
            /// 电话
            /// </summary>
            public string tel { get; set; }
        }

        #endregion

        #region 3、同步福利商品 syncwelFareItem

        public void SyncWelfareItem()
        {
            var interfaceName = "syncwelFareItem";
            var loggingSessionInfo = BaseService.GetLoggingSession();
            var itemService = new ItemService(loggingSessionInfo);

            var dsItems = new DataSet();
            var items = new ItemList();
            items.itemlist = new List<Item>();

            //更新接口同步表
            var queryList = UpdateInterfaceTimestamp(interfaceName, loggingSessionInfo);

            if (queryList != null && queryList.Length > 0)
            {
                //存在，根据日期条件查询
                dsItems = itemService.GetItemTypeList(queryList.FirstOrDefault().LatestTime.ToString());
            }
            else
            {
                //不存在，查询所有数据
                dsItems = itemService.GetItemTypeList(string.Empty);
            }

            if (dsItems != null && dsItems.Tables.Count > 0 && dsItems.Tables[0].Rows.Count > 0)
            {
                items.itemlist = DataTableToObject.ConvertToList<Item>(dsItems.Tables[0]);

                for (int i = 0; i < dsItems.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = dsItems.Tables[0].Rows[i];

                    //商品品牌Mapping集合
                    items.itemlist[i].itembrandmapping = new List<ItemBrandMapping>();
                    if (!string.IsNullOrEmpty(dr["brandid"].ToString()))
                    {
                        items.itemlist[i].itembrandmapping.Add(new ItemBrandMapping()
                        {
                            mappingid = Utils.NewGuid(),
                            brandid = dr["brandid"].ToString(),
                            isdelete = "0"
                        });
                    }

                    //门店与商品Mapping集合
                    items.itemlist[i].itemstoremapping = new List<ItemStoreMapping>();
                    var dsMappings = itemService.GetItemStoreMapping(dr["itemid"].ToString());
                    if (dsMappings != null && dsMappings.Tables.Count > 0 && dsMappings.Tables[0].Rows.Count > 0)
                    {
                        items.itemlist[i].itemstoremapping = DataTableToObject.ConvertToList<ItemStoreMapping>(dsMappings.Tables[0]);
                    }

                    //商品与优惠券类型
                    items.itemlist[i].itemptypemapping = new List<ItemPtypeMapping>();
                    if (!string.IsNullOrEmpty(dr["ptypeid"].ToString()))
                    {
                        items.itemlist[i].itemptypemapping.Add(new ItemPtypeMapping()
                        {
                            mappingid = Utils.NewGuid(),
                            tpypeId = dr["ptypeid"].ToString(),
                            isdelete = "0"
                        });
                    }
                }

                //上传数据
                var content = items.ToJSON();
                var result = UploadData(interfaceName, content);

                //写入接口日志
                var logEntity = new ZInterfaceLogEntity()
                {
                    LogId = Utils.NewGuid(),
                    InterfaceName = interfaceName,
                    Params = content,
                    ResultCode = result.code,
                    ResultDesc = result.description
                };

                InsertInterfaceLog(logEntity, loggingSessionInfo);
            }
        }

        public class ItemList
        {
            public List<Item> itemlist { get; set; }
        }

        public class Item
        {
            /// <summary>
            /// 商品主标识
            /// </summary>
            public string itemid { get; set; }
            /// <summary>
            /// 商品号码
            /// </summary>
            public string itemcode { get; set; }
            /// <summary>
            /// 商品名称
            /// </summary>
            public string itemname { get; set; }
            /// <summary>
            /// 商品英文名
            /// </summary>
            public string itemengname { get; set; }
            /// <summary>
            /// 商品缩写
            /// </summary>
            public string itemshortname { get; set; }
            /// <summary>
            /// 商品描述
            /// </summary>
            public string itemdesc { get; set; }
            /// <summary>
            /// 商品图片链接
            /// </summary>
            public string imageurl { get; set; }
            /// <summary>
            /// 排序
            /// </summary>
            public int displayindex { get; set; }
            /// <summary>
            /// 地址
            /// </summary>
            public string address { get; set; }
            /// <summary>
            /// 单位
            /// </summary>
            public string itemunit { get; set; }
            /// <summary>
            /// 联系电话
            /// </summary>
            public string tel { get; set; }
            /// <summary>
            /// 数量
            /// </summary>
            public string qty { get; set; }
            /// <summary>
            /// 开始时间
            /// </summary>
            public string begintime { get; set; }
            /// <summary>
            /// 结束时间
            /// </summary>
            public string endtime { get; set; }
            /// <summary>
            /// 使用须知
            /// </summary>
            public string useinfo { get; set; }
            /// <summary>
            /// 状态
            /// </summary>
            public string status { get; set; }
            /// <summary>
            /// 团购类型
            /// </summary>
            public string buytype { get; set; }
            /// <summary>
            /// 优惠提示
            /// </summary>
            public string offerstips { get; set; }
            /// <summary>
            /// 优惠券下载链接
            /// </summary>
            public string couponurl { get; set; }
            /// <summary>
            /// 是否删除
            /// </summary>
            public string isdelete { get; set; }
            /// <summary>
            /// 商品品牌Mapping集合
            /// </summary>
            public List<ItemBrandMapping> itembrandmapping { get; set; }
            /// <summary>
            /// 门店与商品Mapping集合
            /// </summary>
            public List<ItemStoreMapping> itemstoremapping { get; set; }
            /// <summary>
            /// 商品与优惠券类型
            /// </summary>
            public List<ItemPtypeMapping> itemptypemapping { get; set; }
        }

        public class ItemBrandMapping
        {
            /// <summary>
            /// 主标识
            /// </summary>
            public string mappingid { get; set; }
            /// <summary>
            /// 品牌标志
            /// </summary>
            public string brandid { get; set; }
            /// <summary>
            /// 是否删除
            /// </summary>
            public string isdelete { get; set; }
        }

        public class ItemStoreMapping
        {
            /// <summary>
            /// 主标识
            /// </summary>
            public string mappingid { get; set; }
            /// <summary>
            /// 门店标识
            /// </summary>
            public string storeid { get; set; }
            /// <summary>
            /// 是否删除
            /// </summary>
            public int isdelete { get; set; }
        }

        public class ItemPtypeMapping
        {
            /// <summary>
            /// 主标识
            /// </summary>
            public string mappingid { get; set; }
            /// <summary>
            /// 福利类型标识
            /// </summary>
            public string tpypeId { get; set; }
            /// <summary>
            /// 是否删除
            /// </summary>
            public string isdelete { get; set; }
        }

        #endregion

        #region 4、同步福利SKU syncwelFareSKU

        public void syncwelFareSKU()
        {
            var interfaceName = "syncwelFareSKU";
            var loggingSessionInfo = BaseService.GetLoggingSession();
            var skuService = new SkuService(loggingSessionInfo);

            var dsSkus = new DataSet();
            var skus = new SkuList();
            skus.skulist = new List<Sku>();

            //更新接口同步表
            var queryList = UpdateInterfaceTimestamp(interfaceName, loggingSessionInfo);

            if (queryList != null && queryList.Length > 0)
            {
                //存在，根据日期条件查询
                dsSkus = skuService.GetSynWelfareSkuList(queryList.FirstOrDefault().LatestTime.ToString());
            }
            else
            {
                //不存在，查询所有数据
                dsSkus = skuService.GetSynWelfareSkuList(string.Empty);
            }

            if (dsSkus != null && dsSkus.Tables.Count > 0 && dsSkus.Tables[0].Rows.Count > 0)
            {
                skus.skulist = DataTableToObject.ConvertToList<Sku>(dsSkus.Tables[0]);
                
                //上传数据
                var content = skus.ToJSON();
                var result = UploadData(interfaceName, skus.ToJSON());

                //写入接口日志
                var logEntity = new ZInterfaceLogEntity()
                {
                    LogId = Utils.NewGuid(),
                    InterfaceName = interfaceName,
                    Params = content,
                    ResultCode = result.code,
                    ResultDesc = result.description
                };

                InsertInterfaceLog(logEntity, loggingSessionInfo);
            }
        }

        public class SkuList
        {
            public List<Sku> skulist { get; set; }
        }

        public class Sku
        {
            /// <summary>
            /// SKU主标识
            /// </summary>
            public string skuId { get; set; }
            /// <summary>
            /// 商品主标识
            /// </summary>
            public string itemId { get; set; }
            /// <summary>
            /// 属性1
            /// </summary>
            public string skuprop1 { get; set; }
            /// <summary>
            /// 属性2
            /// </summary>
            public string skuprop2 { get; set; }
            /// <summary>
            /// 属性3
            /// </summary>
            public string skuprop3 { get; set; }
            /// <summary>
            /// 属性4
            /// </summary>
            public string skuprop4 { get; set; }
            /// <summary>
            /// 商品零售价
            /// </summary>
            public decimal salesprice { get; set; }
            /// <summary>
            /// 商品原价
            /// </summary>
            public decimal price { get; set; }
            /// <summary>
            /// 商品折扣
            /// </summary>
            public decimal discountrate { get; set; }
            /// <summary>
            /// 排序
            /// </summary>
            public Int64 displayindex { get; set; }
            /// <summary>
            /// 是否删除
            /// </summary>
            public string isdelete { get; set; }
            /// <summary>
            /// 积分
            /// </summary>
            public decimal integral { get; set; }
        }

        #endregion

        #region 5、同步福利门店 syncwelFareStore

        public void syncwelFareStore()
        {
            var interfaceName = "syncwelFareStore";
            var loggingSessionInfo = BaseService.GetLoggingSession();
            var storeService = new StoreBLL(loggingSessionInfo);

            var dsStores = new DataSet();
            var stores = new StoreList();
            stores.storelist = new List<Store>();

            //更新接口同步表
            var queryList = UpdateInterfaceTimestamp(interfaceName, loggingSessionInfo);

            if (queryList != null && queryList.Length > 0)
            {
                //存在，根据日期条件查询
                dsStores = storeService.GetSynWelfareStoreList(queryList.FirstOrDefault().LatestTime.ToString());
            }
            else
            {
                //不存在，查询所有数据
                dsStores = storeService.GetSynWelfareStoreList(string.Empty);
            }

            if (dsStores != null && dsStores.Tables.Count > 0 && dsStores.Tables[0].Rows.Count > 0)
            {
                stores.storelist = DataTableToObject.ConvertToList<Store>(dsStores.Tables[0]);

                //上传数据
                var content = stores.ToJSON();
                var result = UploadData(interfaceName, stores.ToJSON());

                //写入接口日志
                var logEntity = new ZInterfaceLogEntity()
                {
                    LogId = Utils.NewGuid(),
                    InterfaceName = interfaceName,
                    Params = content,
                    ResultCode = result.code,
                    ResultDesc = result.description
                };

                InsertInterfaceLog(logEntity, loggingSessionInfo);
            }
        }

        public class StoreList
        {
            public List<Store> storelist { get; set; }
        }

        public class Store
        {
            /// <summary>
            /// 主标识
            /// </summary>
            public string storeId { get; set; }
            /// <summary>
            /// 门店号码
            /// </summary>
            public string storeCode { get; set; }
            /// <summary>
            /// 门店名称
            /// </summary>
            public string storeName { get; set; }
            /// <summary>
            /// 门店描述
            /// </summary>
            public string storeDesc { get; set; }
            /// <summary>
            /// 排序
            /// </summary>
            public Int64 displayIndex { get; set; }
            /// <summary>
            /// 地址
            /// </summary>
            public string address { get; set; }
            /// <summary>
            /// 联系人
            /// </summary>
            public string content { get; set; }
            /// <summary>
            /// 联系电话
            /// </summary>
            public string tel { get; set; }
            /// <summary>
            /// 公共平台号码
            /// </summary>
            public string weiXinCode { get; set; }
            /// <summary>
            /// email
            /// </summary>
            public string email { get; set; }
            /// <summary>
            /// 经度
            /// </summary>
            public string longitude { get; set; }
            /// <summary>
            /// 维度
            /// </summary>
            public string latitude { get; set; }
            /// <summary>
            /// 是否删除
            /// </summary>
            public string isdelete { get; set; }  
        }

        #endregion

        #region 6、同步福利门店品牌关系 syncwelFareStoreBrandMapping

        public void syncwelFareStoreBrandMapping()
        {
            var interfaceName = "syncwelFareStoreBrandMapping";
            var loggingSessionInfo = BaseService.GetLoggingSession();
            var storeBrandService = new StoreBrandMappingBLL(loggingSessionInfo);

            var dsStoreBrands = new DataSet();
            var storeBrands = new StoreBrandMappingList();
            storeBrands.mappinglist = new List<StoreBrandMapping>();

            //更新接口同步表
            var queryList = UpdateInterfaceTimestamp(interfaceName, loggingSessionInfo);

            if (queryList != null && queryList.Length > 0)
            {
                //存在，根据日期条件查询
                dsStoreBrands = storeBrandService.GetSynWelfareStoreBrandMappingList(queryList.FirstOrDefault().LatestTime.ToString());
            }
            else
            {
                //不存在，查询所有数据
                dsStoreBrands = storeBrandService.GetSynWelfareStoreBrandMappingList(string.Empty);
            }

            if (dsStoreBrands != null && dsStoreBrands.Tables.Count > 0 && dsStoreBrands.Tables[0].Rows.Count > 0)
            {
                storeBrands.mappinglist = DataTableToObject.ConvertToList<StoreBrandMapping>(dsStoreBrands.Tables[0]);

                //上传数据
                var content = storeBrands.ToJSON();
                var result = UploadData(interfaceName, storeBrands.ToJSON());

                //写入接口日志
                var logEntity = new ZInterfaceLogEntity()
                {
                    LogId = Utils.NewGuid(),
                    InterfaceName = interfaceName,
                    Params = content,
                    ResultCode = result.code,
                    ResultDesc = result.description
                };

                InsertInterfaceLog(logEntity, loggingSessionInfo);
            }
        }

        public class StoreBrandMappingList
        {
            public List<StoreBrandMapping> mappinglist { get; set; }
        }

        public class StoreBrandMapping
        {
            /// <summary>
            /// 主标识
            /// </summary>
            public string mappingid { get; set; }
            /// <summary>
            /// 门店标识
            /// </summary>
            public string storeid { get; set; }
            /// <summary>
            /// 品牌标志
            /// </summary>
            public string brandid { get; set; }
            /// <summary>
            /// 是否删除
            /// </summary>
            public int isdelete { get; set; }
        }

        #endregion

        #region 7、同步福利图片 syncwelFareObjectImages

        public void syncwelFareObjectImages()
        {
            var interfaceName = "syncwelFareObjectImages";
            var loggingSessionInfo = BaseService.GetLoggingSession();
            var imageService = new ObjectImagesBLL(loggingSessionInfo);

            var dsImages = new DataSet();
            var images = new ImageList();
            images.mapping = new List<Image>();

            //更新接口同步表
            var queryList = UpdateInterfaceTimestamp(interfaceName, loggingSessionInfo);

            if (queryList != null && queryList.Length > 0)
            {
                //存在，根据日期条件查询
                dsImages = imageService.GetSynWelfareObjectImageList(queryList.FirstOrDefault().LatestTime.ToString());
            }
            else
            {
                //不存在，查询所有数据
                dsImages = imageService.GetSynWelfareObjectImageList(string.Empty);
            }

            if (dsImages != null && dsImages.Tables.Count > 0 && dsImages.Tables[0].Rows.Count > 0)
            {
                images.mapping = DataTableToObject.ConvertToList<Image>(dsImages.Tables[0]);

                //上传数据
                var content = images.ToJSON();
                var result = UploadData(interfaceName, images.ToJSON());

                //写入接口日志
                var logEntity = new ZInterfaceLogEntity()
                {
                    LogId = Utils.NewGuid(),
                    InterfaceName = interfaceName,
                    Params = content,
                    ResultCode = result.code,
                    ResultDesc = result.description
                };

                InsertInterfaceLog(logEntity, loggingSessionInfo);
            }
        }

        public class ImageList
        {
            public List<Image> mapping { get; set; }
        }

        public class Image
        {
            /// <summary>
            /// 主标识
            /// </summary>
            public string imageid { get; set; }
            /// <summary>
            /// 对象标识
            /// </summary>
            public string objectid { get; set; }
            /// <summary>
            /// 图片链接地址
            /// </summary>
            public string imageurl { get; set; }
            /// <summary>
            /// 是否删除
            /// </summary>
            public int isdelete { get; set; }
        }

        #endregion

        #region 更新接口同步表 UpdateInterfaceTimestamp

        /// <summary>
        /// 更新接口同步表
        /// </summary>
        /// <param name="interfaceName">接口名称</param>
        /// <param name="latestTime">最新同步时间</param>
        /// <param name="timestampBLL"></param>
        /// <returns></returns>
        private ZInterfaceTimestampEntity[] UpdateInterfaceTimestamp(string interfaceName, LoggingSessionInfo loggingSessionInfo)
        {
            var timestampBLL = new ZInterfaceTimestampBLL(loggingSessionInfo);
            var queryList = timestampBLL.QueryByEntity(new ZInterfaceTimestampEntity() { InterfaceName = interfaceName }, null);

            if (queryList != null && queryList.Length > 0)
            {
                //存在，更新接口同步表
                var entity = queryList.FirstOrDefault().Clone() as ZInterfaceTimestampEntity;
                entity.LatestTime = DateTime.Now;
                timestampBLL.Update(entity);
            }
            else
            {
                //不存在，新增接口同步表
                var entity = new ZInterfaceTimestampEntity()
                {
                    TimestampId = Utils.NewGuid(),
                    InterfaceName = interfaceName,
                    LatestTime = DateTime.Now
                };
                timestampBLL.Create(entity);
            }

            return queryList;
        }

        #endregion

        #region 添加接口同步日志 InsertInterfaceLog

        private void InsertInterfaceLog(ZInterfaceLogEntity entity, LoggingSessionInfo loggingSessionInfo)
        {
            var interfaceLog = new ZInterfaceLogBLL(loggingSessionInfo);
            interfaceLog.Create(entity);
        }

        #endregion

        #region 返回参数 RespData

        public class RespData
        {
            public string code = "200";
            public string description = "操作成功";
        }

        #endregion

        #region 模拟表单上传数据 UploadData

        /// <summary>
        /// 模拟表单上传数据
        /// </summary>
        /// <param name="interfaceName"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        private RespData UploadData(string interfaceName, string content)
        {
            var uriString = "http://alumniapp.ceibs.edu:8080/ceibs_test/welfareSync";
            //var uriString = "http://192.168.0.71:8088/ceibs_test/welfareSync";    //测试地址
            // 创建一个新的 WebClient 实例.
            WebClient myWebClient = new WebClient();
            var postData = "action=" + interfaceName + "&reqContent=" + content;
            // 注意这种拼字符串的ContentType
            myWebClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            // 转化成二进制数组
            byte[] byteArray = Encoding.ASCII.GetBytes(postData);
            // 上传数据，并获取返回的二进制数据.
            byte[] responseArray = myWebClient.UploadData(uriString, "POST", byteArray);
            var data = System.Text.Encoding.UTF8.GetString(responseArray);
            var result = data.DeserializeJSONTo<RespData>();
            return result;
        }

        #endregion
    }
}
