using System;
using System.Net;
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
using System.Configuration;
using System.IO;
using JIT.Utility.Log;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Web.Module.WEvents.Handler;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.Web.Module.Basic.Item.Handler
{
    /// <summary>
    /// ItemHandler
    /// </summary>
    public class ItemHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
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
                case "search_item":
                    content = GetItemListData();
                    break;
                case "get_item_by_id":
                    content = GetItemInfoById();
                    break;
                case "save_item":
                    content = SaveItemData();
                    break;
                case "save_sku":
                    content = SaveSkuData();
                    break;
                case "get_item_price_info_by_item_id":
                    content = GetItemPriceInfoByItemIdData();
                    break;
                case "get_item_image_info_by_item_id":        //根据商品标识获取图片信息
                    content = GetItemImageInfoByItemIdData();
                    break;
                case "get_item_unit_info_by_item_id":
                    content = GetItemUnitInfoByItemIdData();
                    break;
                case "item_enable":
                    content = setItemStatus();
                    break;
                case "get_treegrid_json":
                    content = GetTreeStoreJSON();
                    break;
                case "download_qrcode":
                    DownloadQRCode();
                    break;
                case "CretaeWxCode":
                    content = CretaeWxCode();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        private void DownloadQRCode()
        {
            string weixinDomain = ConfigurationManager.AppSettings["original_url"];
            string sourcePath = this.CurrentContext.Server.MapPath("/QRCodeImage/qrcode.jpg");
            string targetPath = this.CurrentContext.Server.MapPath("/QRCodeImage/");
            string currentDomain = this.CurrentContext.Request.Url.Host;
            string itemId = FormatParamValue(Request("item_id"));//商品ID
            string itemName = FormatParamValue(Request("item_name"));//商品名
            string imageURL;

            ObjectImagesBLL objectImagesBLL = new ObjectImagesBLL(CurrentUserInfo);
            //查找是否已经生成了二维码
            ObjectImagesEntity[] objectImagesEntityArray = objectImagesBLL.QueryByEntity(new ObjectImagesEntity() { ObjectId = itemId, Description = "自动生成的产品二维码" }, null);

            if (objectImagesEntityArray.Length == 0)
            {
                imageURL = Utils.GenerateQRCode(weixinDomain + "/HtmlApps/Auth.html?pageName=GoodsDetail&rootPage=true&customerId=" + CurrentUserInfo.ClientID + "&goodsId=" + itemId, currentDomain, sourcePath, targetPath);
                //把下载下来的图片的地址存到ObjectImages
                objectImagesBLL.Create(new ObjectImagesEntity() { ImageId = Utils.NewGuid(),
                    CustomerId = CurrentUserInfo.ClientID, ImageURL = imageURL, ObjectId = itemId, Title = itemName
                    , Description = "自动生成的产品二维码" });

                Loggers.Debug(new DebugLogInfo() { Message = "二维码已生成，imageURL:" + imageURL });
            }
            else
            {
                imageURL = objectImagesEntityArray[0].ImageURL;
            }

            string imagePath = targetPath + imageURL.Substring(imageURL.LastIndexOf("/"));
            Loggers.Debug(new DebugLogInfo() { Message = "二维码路径，imagePath:" + imageURL });

            try
            {
                //要下载的文件名
                FileInfo DownloadFile = new FileInfo(imagePath);

                if (DownloadFile.Exists)
                {
                    CurrentContext.Response.Clear();
                    CurrentContext.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + System.Web.HttpUtility.UrlEncode(itemName, System.Text.Encoding.UTF8) + ".jpg" + "\"");
                    CurrentContext.Response.AddHeader("Content-Length", DownloadFile.Length.ToString());
                    CurrentContext.Response.ContentType = "application/octet-stream";
                    CurrentContext.Response.TransmitFile(DownloadFile.FullName);
                    CurrentContext.Response.Flush();
                }
                else
                    Loggers.Debug(new DebugLogInfo() { Message = "二维码未找到" });
            }
            catch (Exception ex)
            {
                CurrentContext.Response.ContentType = "text/plain";
                CurrentContext.Response.Write(ex.Message);
            }
            finally
            {
                CurrentContext.Response.End();
            }
        }

        #region GetItemListData
        /// <summary>
        /// 商品列表
        /// </summary>
        public string GetItemListData()
        {
            var form = Request("form").DeserializeJSONTo<ItemQueryEntity>();

            var itemService = new ItemService(CurrentUserInfo);
            ItemInfo data = new ItemInfo();
            string content = string.Empty;

            string item_code = FormatParamValue(form.item_code);
            string item_name = FormatParamValue(form.item_name);
            string item_category_id = FormatParamValue(Request("item_category_id"));
            string item_status = FormatParamValue(form.item_status);
            string item_can_redeem = FormatParamValue(form.item_can_redeem);
            string SalesPromotion_id = FormatParamValue(Request("SalesPromotion_id"));



            int maxRowCount = PageSize;
            int startRowIndex = Utils.GetIntVal(Request("start"));

            string key = string.Empty;
            if (Request("id") != null && Request("id") != string.Empty)
            {
                key = Request("id").ToString().Trim();
            }

            data = itemService.SearchItemList(item_code, item_name, item_category_id, item_status, item_can_redeem,SalesPromotion_id,
                maxRowCount, startRowIndex);

            var jsonData = new JsonData();
            jsonData.totalCount = data.ICount.ToString();
            jsonData.data = data.ItemInfoList;
            
            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ItemInfoList.ToJSON(),
                data.ICount);
            return content;
        }
        #endregion

        #region GetItemInfoById
        /// <summary>
        /// 获取商品信息
        /// </summary>
        public string GetItemInfoById()
        {
            var itemService = new ItemService(CurrentUserInfo);
            ItemInfo data = new ItemInfo();
            string content = string.Empty;

            string key = string.Empty;
            if (Request("item_id") != null && Request("item_id") != string.Empty)
            {
                key = Request("item_id").ToString().Trim();
            }

            data = itemService.GetItemInfoById(CurrentUserInfo, key);

            //图片信息
            //根据实体条件查询实体
            var imageService = new ObjectImagesBLL(CurrentUserInfo);
            var itemObj = imageService.QueryByEntity(new ObjectImagesEntity() { ObjectId = key }, null);
            if (itemObj != null && itemObj.Length > 0)
            {
                //itemObj竟然是个数组，把自动生成的产品二维码给过滤掉，不显示出来
                itemObj = itemObj.Where<ObjectImagesEntity>(t => t.Description != "自动生成的产品二维码").ToArray<ObjectImagesEntity>();
                data.ItemImageList = itemObj.OrderBy(item => item.DisplayIndex).ToList();
            }

            WQRCodeManagerBLL wQRCodeManagerBLL = new WQRCodeManagerBLL(CurrentUserInfo);
            var entity = wQRCodeManagerBLL.QueryByEntity(new WQRCodeManagerEntity
            {
                ObjectId = key
                ,
                IsDelete = 0
            }, null).FirstOrDefault();

            #region 图文|二维码

           
            if (entity != null)
            {
                data.imageUrl = entity.ImageUrl;
                var WKeywordReplyentity = new WKeywordReplyBLL(this.CurrentUserInfo).QueryByEntity(new WKeywordReplyEntity()
                {
                    Keyword = entity.QRCodeId.ToString()

                }, null).FirstOrDefault();
                if (WKeywordReplyentity != null)
                {
                    if (WKeywordReplyentity.ReplyType == 1)
                    {
                        data.ReplyType = "1";
                        data.Text = WKeywordReplyentity.Text;

                    }
                    else if (WKeywordReplyentity.ReplyType == 3)
                    {
                        data.ReplyType = "3";
                        WMenuMTextMappingBLL bll = new WMenuMTextMappingBLL(this.CurrentUserInfo);
                        WMaterialTextBLL wmbll = new WMaterialTextBLL(this.CurrentUserInfo);
                        OrderBy[] pOrderBys = new OrderBy[]{
                             new OrderBy(){ FieldName="CreateTime", Direction= OrderByDirections.Asc}
                            };
                        var textMapping = bll.QueryByEntity(new WMenuMTextMappingEntity
                        {
                            MenuId = WKeywordReplyentity.ReplyId,
                            IsDelete = 0
                        }, pOrderBys);
                        if (textMapping != null && textMapping.Length > 0)
                        {
                            List<WMaterialTextEntity> list = new List<WMaterialTextEntity>();
                            foreach (var item in textMapping)
                            {
                                WMaterialTextEntity WMaterialTextentity = wmbll.QueryByEntity(new WMaterialTextEntity { TextId = item.TextId, IsDelete = 0 }, null)[0];
                                list.Add(WMaterialTextentity);
                            }
                            data.listMenutext = list;
                        }

                    }
                }


            }
            #endregion


            var jsonData = new JsonData();
            jsonData.totalCount = data == null ? "0" : "1";
            jsonData.data = data;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region SaveItemData
        /// <summary>
        /// 保存商品
        /// </summary>
        public string SaveItemData()
        {
            var itemService = new ItemService(CurrentUserInfo);
            ItemInfo obj = new ItemInfo();
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

            obj = key.DeserializeJSONTo<ItemInfo>();

            if (item_id.Trim().Length == 0)
            {
                obj.Item_Id = Utils.NewGuid();
                //user.UnitList = loggingSessionInfo.CurrentUserRole.UnitId;
            }
            else
            {
                obj.Item_Id = item_id;
            }

            if (obj.Item_Code == null || obj.Item_Code.Trim().Length == 0)
            {
                obj.Item_Code = itemService.GetGreatestItemCode(CurrentUserInfo);
                if (obj.Item_Code.Length == 0)
                {
                    responseData.success = false;
                    responseData.msg = "商品编码自动生成失败，请联系管理员。";
                    return responseData.ToJSON();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(obj.Item_Id))
                {
                    if (!itemService.IsExistItemCode(CurrentUserInfo, obj.Item_Code, obj.Item_Id))
                    {
                        responseData.success = false;
                        responseData.msg = "商品编码不能重复";
                        return responseData.ToJSON();
                    }
                }
            }

            //操作类型
            if (!string.IsNullOrWhiteSpace(Request("operation")))
            {
                obj.OperationType = Request("operation").ToUpper().Trim();
            }

            if (obj.Item_Name == null || obj.Item_Name.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "商品名称不能为空";
                return responseData.ToJSON();
            }
            if (obj.Item_Category_Id == null || obj.Item_Category_Id.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "商品分类不能为空";
                return responseData.ToJSON();
            }

            if (obj.SkuList != null && obj.SkuList.Count > 0)
            {
                foreach (var tmpSku in obj.SkuList)
                {
                    tmpSku.item_id = obj.Item_Id;
                    if (tmpSku.sku_id == null || tmpSku.sku_id.Length == 0)
                    {
                        tmpSku.sku_id = Utils.NewGuid();
                    }

                    //处理sku相关价格(jifeng.cao 20140224)
                    foreach (var item in tmpSku.sku_price_list)
                    {
                        item.sku_id = tmpSku.sku_id;
                        if (item.sku_price_id == null || item.sku_price_id.Length == 0)
                        {
                            item.sku_price_id = Utils.NewGuid();
                        }
                    }

                }
            }
            else
            {
                //对于无SKU配置的客户，默认生成一个SKU
                SkuInfo skuInfo = new SkuInfo();
                skuInfo.sku_id = Utils.NewGuid();
                skuInfo.item_id = obj.Item_Id;
                item_id = skuInfo.sku_id;
                //skuInfo.barcode = skuInfo.item_code;
                //skuInfo.prop_1_detail_id = "--";
                //skuInfo.prop_2_detail_id = "--";
                //skuInfo.prop_3_detail_id = "--";
                //skuInfo.prop_4_detail_id = "--";
                //skuInfo.prop_5_detail_id = "--";
                //skuInfo.status = "1";
                //skuInfo.create_user_id = "System";
                //skuInfo.create_time = DateTime.Now.ToString();

                obj.SkuList.Add(skuInfo);
            }

            itemService.SetItemInfo(obj, out error);

            #region 生成二维码
     /**
            item_id = obj.Item_Id;
            var wapentity = new WApplicationInterfaceBLL(this.CurrentUserInfo).QueryByEntity(new WApplicationInterfaceEntity
            {

                CustomerId = this.CurrentUserInfo.ClientID,
                IsDelete = 0

            }, null).FirstOrDefault();
         

            var QRCodeId = Guid.NewGuid();
            if (!string.IsNullOrEmpty(obj.imageUrl))
            {
                var QRCodeManagerentity = new WQRCodeManagerBLL(this.CurrentUserInfo).QueryByEntity(new WQRCodeManagerEntity
                {
                    ObjectId = item_id
                }, null).FirstOrDefault();
                if (QRCodeManagerentity != null)
                {
                    QRCodeId = (Guid)QRCodeManagerentity.QRCodeId;
                }
                if (QRCodeManagerentity == null)
                {
                    var wqrentity = new WQRCodeTypeBLL(this.CurrentUserInfo).QueryByEntity(

                                      new WQRCodeTypeEntity { TypeCode = "ItemQrCode" }

                                       , null).FirstOrDefault();

           

                    var WQRCodeManagerbll = new WQRCodeManagerBLL(this.CurrentUserInfo);


                    WQRCodeManagerbll.Create(new WQRCodeManagerEntity
                    {
                        QRCodeId = QRCodeId,
                        QRCode = obj.MaxWQRCod,
                        QRCodeTypeId = wqrentity.QRCodeTypeId,
                        IsUse = 1,
                        ObjectId = item_id,
                        CreateBy = this.CurrentUserInfo.UserID,
                        ApplicationId = wapentity.ApplicationId,
                        IsDelete = 0,
                        ImageUrl = obj.imageUrl,
                        CustomerId = this.CurrentUserInfo.ClientID

                    });

                }
            }
           **/
            #endregion
          
            
            #region 添加图文消息|文本信息
   /*
            var WKeywordReplyentity = new WKeywordReplyBLL(this.CurrentUserInfo).QueryByEntity(new WKeywordReplyEntity()
            {
                Keyword = QRCodeId.ToString()

            }, null).FirstOrDefault();

            if (WKeywordReplyentity == null)
            {
                WKeywordReplyentity = new WKeywordReplyEntity();
                Guid ReplyId = Guid.NewGuid();
                WKeywordReplyentity.ReplyId = ReplyId.ToString();
                WKeywordReplyentity.Keyword = QRCodeId.ToString();
                WKeywordReplyentity.ApplicationId = wapentity.ApplicationId;
                WKeywordReplyentity.KeywordType = 4;
                WKeywordReplyentity.IsDelete = 0;
                if (obj.ReplyType == "1")
                {
                    WKeywordReplyentity.ReplyType = 1;
                    WKeywordReplyentity.Text = obj.Text;
                }
                else if (obj.ReplyType == "3")
                {
                    WKeywordReplyentity.ReplyType = 3;
                    WMenuMTextMappingBLL MenuMTextMappingServer = new WMenuMTextMappingBLL(this.CurrentUserInfo);
                    foreach (var item in obj.listMenutextMapping)
                    {
                        WMenuMTextMappingEntity MappingEntity = new WMenuMTextMappingEntity();
                        MappingEntity.MappingId = Guid.NewGuid();
                        MappingEntity.MenuId = ReplyId.ToString();
                        MappingEntity.TextId = item.TextId;
                        MappingEntity.DisplayIndex = item.DisplayIndex;
                        MappingEntity.CustomerId = this.CurrentUserInfo.ClientID;
                        MenuMTextMappingServer.Create(MappingEntity);
                    }

                }
                new WKeywordReplyBLL(this.CurrentUserInfo).Create(WKeywordReplyentity);
            }
            else
            {
                WMenuMTextMappingBLL MenuMTextMappingServer = new WMenuMTextMappingBLL(this.CurrentUserInfo);
                var MenuMTextMappinglist = MenuMTextMappingServer.QueryByEntity(new WMenuMTextMappingEntity
                {
                    MenuId = WKeywordReplyentity.ReplyId,
                    CustomerId = this.CurrentUserInfo.ClientID,
                    IsDelete = 0
                }, null);

                if (MenuMTextMappinglist != null && MenuMTextMappinglist.Length > 0)
                {
                    foreach (var item in MenuMTextMappinglist)
                    {

                        MenuMTextMappingServer.Delete(new WMenuMTextMappingEntity
                        {
                            MappingId = item.MappingId
                        }, null);
                    }
                }
                WKeywordReplyentity.KeywordType = 4;
                if (obj.ReplyType == "1")
                {
                    WKeywordReplyentity.ReplyType = 1;
                    WKeywordReplyentity.Text = obj.Text;
              

                }
                else if (obj.ReplyType == "3")
                {
                    WKeywordReplyentity.ReplyType = 3;
                    WKeywordReplyentity.Text = "";
                    foreach (var item in obj.listMenutextMapping)
                    {
                        WMenuMTextMappingEntity MappingEntity = new WMenuMTextMappingEntity();
                        MappingEntity.MappingId = Guid.NewGuid();
                        MappingEntity.MenuId = WKeywordReplyentity.ReplyId;
                        MappingEntity.TextId = item.TextId;
                        MappingEntity.DisplayIndex = item.DisplayIndex;
                        MappingEntity.CustomerId = this.CurrentUserInfo.ClientID;
                        MenuMTextMappingServer.Create(MappingEntity);
                    }
                }
                new WKeywordReplyBLL(this.CurrentUserInfo).Update(WKeywordReplyentity);
            }
          **/
            #endregion

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        /// <summary>
        /// 保存商品sku
        /// </summary>
        /// <returns></returns>
       /// add by donal 2014-10-13 09:39:24
        public string SaveSkuData() 
        {
            var itemService = new ItemService(CurrentUserInfo);
            ItemInfo obj = new ItemInfo();
            string content = string.Empty;
            string msg = "";
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

            obj = key.DeserializeJSONTo<ItemInfo>();

            //操作类型
            if (!string.IsNullOrWhiteSpace(Request("operation")))
            {
                obj.OperationType = Request("operation").ToUpper().Trim();
            }
            else
            {
                responseData.success = false;
                responseData.msg = "参数错误";
                content = responseData.ToJSON();
                return content;
            }

            //参数错误
            if (obj == null)
            {
                responseData.success = false;
                responseData.msg = "参数错误";
                content = responseData.ToJSON();
                return content;
            }

            //找不到修改商品
            if (item_id.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "没有修改商品";
                content = responseData.ToJSON();
                return content;
            }

            //没有修改SKU
            if (obj.SkuList != null && obj.SkuList.Count == 0)
            {
                responseData.success = false;
                responseData.msg = "没有修改SKU";
                content = responseData.ToJSON();
                return content;
            }

            foreach (var tmpSku in obj.SkuList)
            {
                tmpSku.item_id = obj.Item_Id;
                if (string.IsNullOrWhiteSpace(tmpSku.sku_id))
                {
                    tmpSku.sku_id = Utils.NewGuid();
                }

                //处理sku相关价格(jifeng.cao 20140224)
                foreach (var item in tmpSku.sku_price_list)
                {
                    item.sku_id = tmpSku.sku_id;
                    if (string.IsNullOrWhiteSpace(item.sku_price_id))
                    {
                        item.sku_price_id = Utils.NewGuid();
                    }
                }

            }

            string errmsg = string.Empty;

            msg = itemService.SetSkuInfo(obj, out errmsg);

            if (errmsg == "失败")
            {
                responseData.success = false;
                responseData.msg = "操作失败";
            }
            else
            {
                responseData.success = true;
                responseData.msg = msg;
            }

            responseData.success = true;
            responseData.msg = msg;
            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region GetItemPriceInfoByItemIdData
        /// <summary>
        /// 通过商品ID获取商品价格信息
        /// </summary>
        public string GetItemPriceInfoByItemIdData()
        {
            var itemService = new ItemService(CurrentUserInfo);
            IList<ItemPriceInfo> data = new List<ItemPriceInfo>();
            string content = string.Empty;

            string key = string.Empty;
            if (Request("item_id") != null && Request("item_id") != string.Empty)
            {
                key = Request("item_id").ToString().Trim();
            }

            var itemObj = itemService.GetItemInfoById(CurrentUserInfo, key);
            if (itemObj != null)
            {
                data = itemObj.ItemPriceList;
            }

            var jsonData = new JsonData();
            jsonData.totalCount = data.Count.ToString();
            jsonData.data = data;

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
                key = Request("item_id").ToString().Trim();//商品标识
            }
            //根据实体条件查询实体
            var itemObj = imageService.QueryByEntity(new ObjectImagesEntity() { ObjectId = key }, null);
            if (itemObj != null && itemObj.Length > 0)
            {
                //itemObj竟然是个数组，把自动生成的产品二维码给过滤掉，不显示出来
                itemObj = itemObj.Where<ObjectImagesEntity>(t => t.Description != "自动生成的产品二维码").ToArray<ObjectImagesEntity>();
                data = itemObj.OrderBy(item => item.DisplayIndex).ToList();
            }

            var jsonData = new JsonData();
            jsonData.totalCount = data.Count.ToString();
            jsonData.data = data;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region GetItemUnitInfoByItemIdData
        /// <summary>
        /// 通过商品ID获取商品Unit信息
        /// </summary>
        public string GetItemUnitInfoByItemIdData()
        {
            var itemService = new ItemService(CurrentUserInfo);
            IList<ItemStoreMappingEntity> data = new List<ItemStoreMappingEntity>();
            string content = string.Empty;

            string key = string.Empty;
            if (Request("item_id") != null && Request("item_id") != string.Empty)
            {
                key = Request("item_id").ToString().Trim();
            }

            var itemObj = itemService.GetItemInfoById(CurrentUserInfo, key);
            if (itemObj != null)
            {
                data = itemObj.ItemUnitList;
            }

            var jsonData = new JsonData();
            jsonData.totalCount = data.Count.ToString();
            jsonData.data = data;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region 删除
        public string setItemStatus()
        {
            var itemService = new ItemService(CurrentUserInfo);
            string content = string.Empty;
            string error = "停用成功";
            var responseData = new ResponseData();

            string key = string.Empty;

            if (Request("ids") != null && Request("ids") != string.Empty)
            {
                key = Request("ids").ToString().Trim();
            }
            if (key == null || key.Length == 0)
            {
                responseData.status = "1";
                responseData.msg = "请选择商品";
                return responseData.ToJSON(); ;
            }

            var status = "-1";
            if (FormatParamValue(Request("status")) != null && FormatParamValue(Request("status")) != string.Empty)
            {
                status = FormatParamValue(Request("status")).ToString().Trim();
            }

            var idList = key.Split(',');
            foreach (var tmpId in idList)
            {
                if (tmpId.Trim().Length > 0)
                {
                    itemService.SetItemStatus(CurrentUserInfo, tmpId.Trim(), status);
                }
            }

            responseData.status = "0";
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }
        #endregion
        
        #region 商品分类修改(jifeng.cao)

        #region 绑定TreeGrid数据
        /// <summary>
        /// 绑定TreeGrid数据
        /// </summary>
        /// <returns></returns>
        public string GetTreeStoreJSON()
        {
            IList<TItemTagEntity> list = new List<TItemTagEntity>();

            //获取商品与分类关系
            IList<ItemCategoryMappingEntity> mappinglist = null;
            if (Request("itemID") != null && Request("itemID") != string.Empty)
            {
                mappinglist = new ItemCategoryMappingBLL(CurrentUserInfo).GetItemCategoryListByItemId(Request("itemID").ToString().Trim());
            }            

            foreach (TItemTagEntity item in new ItemCategoryService(CurrentUserInfo).GetItemTagListByParentId(""))
            {
                if (item.Status == 1)
                {
                    AddChildNode(item, mappinglist);

                    if (item.children.Count == 0)
                    {
                        item.leaf = true;
                    }
                    else
                    {
                        item.expanded = true;
                    }

                    //勾选相关分类
                    if (mappinglist != null)
                    {
                        foreach (ItemCategoryMappingEntity mappinginfo in mappinglist)
                        {
                            if (mappinginfo.ItemCategoryId == item.ItemTagID.Value.ToString())
                            {
                                item.@checked = true;
                                item.IsFirstVisit = mappinginfo.IsFirstVisit;
                                break;
                            }
                        }

                        if (item.@checked != true)
                        {
                            item.@checked = false;
                            item.IsFirstVisit = 0;
                        }
                    }
                    else
                    {
                        item.@checked = false;
                        item.IsFirstVisit = 0;
                    }

                    list.Add(item);
                }
            }

            return string.Format("{{\"text\":\"\",\"children\":{0}}}", list.ToJSON());
        }
        #endregion

        #region 绑定子节点数据
        /// <summary>
        /// 绑定子节点数据
        /// </summary>
        /// <param name="parentInfo">父节点</param>
        /// <param name="mappinglist">相关分类</param>
        public void AddChildNode(TItemTagEntity parentInfo, IList<ItemCategoryMappingEntity> mappinglist)
        {

            //循环添加子节点
            foreach (TItemTagEntity item in new ItemCategoryService(CurrentUserInfo).GetItemTagListByParentId(parentInfo.ItemTagID.ToString()))
            {
                if (item.Status.Value == 1)
                {
                    //递归调用添加子节点
                    AddChildNode(item, mappinglist);

                    parentInfo.children.Add(item);
                }        
            }

            if (parentInfo.children.Count == 0)
            {
                parentInfo.leaf = true;
            }
            else
            {
                parentInfo.expanded = true;
            }

            //勾选相关分类
            if (mappinglist != null)
            {
                foreach (ItemCategoryMappingEntity mappinginfo in mappinglist)
                {
                    if (mappinginfo.ItemCategoryId == parentInfo.ItemTagID.Value.ToString())
                    {
                        parentInfo.@checked = true;
                        parentInfo.IsFirstVisit = mappinginfo.IsFirstVisit;
                        break;
                    }
                }

                if (parentInfo.@checked != true)
                {
                    parentInfo.@checked = false;
                    parentInfo.IsFirstVisit = 0;
                }
            }
            else
            {
                parentInfo.@checked = false;
                parentInfo.IsFirstVisit = 0;
            }


        }
        #endregion

        #endregion

        #region new 生成门店二维码
        public string CretaeWxCode()
        {
            var responseData = new ResponseData();
            try
            {
                //微信 公共平台
                var wapentity = new WApplicationInterfaceBLL(this.CurrentUserInfo).QueryByEntity(new WApplicationInterfaceEntity
                {

                    CustomerId = this.CurrentUserInfo.ClientID,
                    IsDelete = 0

                }, null).FirstOrDefault();

                //获取当前二维码 最大值
                var MaxWQRCod = new WQRCodeManagerBLL(this.CurrentUserInfo).GetMaxWQRCod() + 1;

                if (wapentity == null || wapentity.ApplicationId==null)
                {
                    responseData.success = false;
                    responseData.msg = "无设置微信公众平台!";
                    return responseData.ToJSON();
                }

                string imageUrl = string.Empty;

                #region 生成二维码
                JIT.CPOS.BS.BLL.WX.CommonBLL commonServer = new JIT.CPOS.BS.BLL.WX.CommonBLL();
                imageUrl = commonServer.GetQrcodeUrl(wapentity.AppID.ToString().Trim()
                                                          , wapentity.AppSecret.Trim()
                                                          , "1", MaxWQRCod
                                                          , this.CurrentUserInfo);

                if (string.IsNullOrEmpty(imageUrl))
                {
                    responseData.success = false;
                    responseData.msg = "二维码生成失败!";
                }
                else
                {
                    string host = ConfigurationManager.AppSettings["DownloadImageUrl"];
                    CPOS.Common.DownloadImage downloadServer = new DownloadImage();
                    imageUrl = downloadServer.DownloadFile(imageUrl, host);
                }
                #endregion
                responseData.success = true;
                responseData.msg = "二维码生成成功!";
                var rp = new ReposeData()
                {
                    imageUrl = imageUrl,
                    MaxWQRCod = MaxWQRCod
                };
                responseData.data = rp;
                return responseData.ToJSON();
            }
            catch (Exception ex)
            {
                throw new APIException(ex.Message);
            }


        }
        #endregion
    }

    #region QueryEntity
    public class ItemQueryEntity
    {
        public string item_code;
        public string item_name;
        public string item_status;
        public string item_category_id;
        public string item_can_redeem;
    }
    #endregion
    public class ReposeData
    {
        public string imageUrl { set; get; }

        public int MaxWQRCod { set; get; }

    }

}