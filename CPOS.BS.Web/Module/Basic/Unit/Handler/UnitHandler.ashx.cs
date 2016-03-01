using System;
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
using System.IO;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Net;
using System.Net.Security;
using System.Globalization;
using System.Data.OleDb;
using System.Configuration;
using JIT.CPOS.DTO.Base;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.Log;
using CPOS.Common;
using JIT.CPOS.BS.Web.Base.Excel;
using Aspose.Cells;


namespace JIT.CPOS.BS.Web.Module.Basic.Unit.Handler
{
    /// <summary>
    /// UnitHandler
    /// </summary>
    public class UnitHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
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
                case "search_unit":
                    content = GetUnitListData();
                    break;
                case "get_unit_by_id":
                    content = GetUnitInfoById();
                    break;
                case "unit_save":
                    content = SaveUnitData();
                    break;
                case "unit_delete":
                    content = DeleteData();
                    break;
                case "get_item_image_info_by_unit_id":
                    content = GetItemImageInfoByUnitIdData();
                    break;
                case "get_item_brand_info_by_unit_id":
                    content = GetItemBrandInfoByUnitIdData();
                    break;
                case "SetUnitWXCode":
                    content = SetUnitWXCode();
                    break;
                case "CretaeWxCode":
                    content = CretaeWxCode();
                    break;
                case "UploadImagerUrl":
                    content = UploadImagerUrl();
                    break;
                case "ImportUnit":
                    content=ImportUnit();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetUnitListData
        /// <summary>
        /// 门店列表
        /// </summary>
        public string GetUnitListData()
        {
            var form = Request("form").DeserializeJSONTo<UnitQueryEntity>();//把传过来的参数，反序列化成对象。

            var unitService = new UnitService(CurrentUserInfo);//实例化BLL层，传递当前的用户信息，以确定用哪个数据库
            UnitInfo data = new UnitInfo();
            string content = string.Empty;

            string unit_code = FormatParamValue(form.unit_code);
            string unit_name = FormatParamValue(form.unit_name);
            string unit_type_id = FormatParamValue(form.unit_type_id);
            string unit_tel = FormatParamValue(form.unit_tel);
            string unit_city_id = FormatParamValue(Request("unit_city_id"));
            string unit_status = FormatParamValue(form.unit_status);

            string StoreType = FormatParamValue(form.StoreType);
            string Parent_Unit_ID = FormatParamValue(form.Parent_Unit_ID);
            string OnlyShop = form.OnlyShop;
              int maxRowCount = PageSize;//每页数量
              int limit = Utils.GetIntVal(Request("limit"));//传过来的参数
              if (limit != 0)
              {
                  maxRowCount = PageSize = limit;
              }


            int page = Utils.GetIntVal(Request("page"));//第几行
            if (page == 0) { page = 1; }
            int startRowIndex = (page - 1) * PageSize+1;//因为row_number()从1开始
          
          //  int startRowIndex = Utils.GetIntVal(Request("start"));//第几行(原有的)

            string key = string.Empty;
            if (Request("id") != null && Request("id") != string.Empty)
            {
                key = Request("id").ToString().Trim();
            }

            if (unit_city_id == "root") unit_city_id = "";
            if (unit_city_id != null && unit_city_id.Trim().Length > 0 && unit_city_id.Trim().Length < 10)
            {
                unit_city_id = (new CityService(CurrentUserInfo)).GetCityGUIDByCityCode(unit_city_id);
                //CityService也是传的当前用户作为筛选使用数据库的依据
            }

            data = unitService.SearchUnitInfo(CurrentUserInfo,
                unit_code, unit_name, unit_type_id,
                unit_tel, unit_city_id, unit_status,
                maxRowCount, startRowIndex, StoreType, Parent_Unit_ID, OnlyShop);//分页的

            var jsonData = new JsonData();
            jsonData.totalCount = data.ICount.ToString();
            jsonData.data = data.UnitInfoList;

            content = string.Format("{{\"totalCount\":{1},\"TotalPage\":{2},\"topics\":{0}}}",
                data.UnitInfoList.ToJSON(),
                data.ICount, data.TotalPage);
            return content;
        }
        #endregion

        #region GetUnitInfoById
        /// <summary>
        /// 获取门店信息
        /// </summary>
        public string GetUnitInfoById()
        {
            var unitService = new UnitService(CurrentUserInfo);
            var tUnitSortBLL = new TUnitSortBLL(CurrentUserInfo);
            var tUnitUnitSortMappingBLL = new TUnitUnitSortMappingBLL(CurrentUserInfo);
            UnitInfo data = new UnitInfo();
            string content = string.Empty;
            var responseData = new ResponseData();
            string key = string.Empty;
            if (Request("unit_id") != null && Request("unit_id") != string.Empty && Request("unit_id") != "''")
            {
                key = Request("unit_id").ToString().Trim();
            } else
            {
                responseData.success = false;
                responseData.msg = "unit_id";
                return responseData.ToJSON();
            }

            data = unitService.GetUnitById(key);

            //门店关联的公众号
            var tueBll = new TUnitExpandBLL(CurrentUserInfo);
            var tueEntity = new TUnitExpandEntity();
            if (!string.IsNullOrEmpty(key))
            {
                tueEntity = tueBll.QueryByEntity(new TUnitExpandEntity() { UnitId = key }, null).FirstOrDefault();
                data.wxAppid = tueEntity != null && !string.IsNullOrEmpty(tueEntity.Field1) ? tueEntity.Field1 : string.Empty ;
            }

            if (data != null)
            {
                //var unitSortMappingList = tUnitUnitSortMappingBLL.QueryByEntity(new TUnitUnitSortMappingEntity()
                //{
                //    UnitId = key
                //}, null);

                //data.UnitSortIdList = new List<string>();
                //foreach (var unitSortMapping in unitSortMappingList)
                //{
                //    data.UnitSortIdList.Add(unitSortMapping.UnitSortId);
                //}

                WQRCodeManagerBLL wQRCodeManagerBLL = new WQRCodeManagerBLL(CurrentUserInfo);
                var eventsWXList = wQRCodeManagerBLL.QueryByEntity(new WQRCodeManagerEntity
                {
                    ObjectId = key
                    ,
                    IsDelete = 0
                }, null);
                if (eventsWXList != null && eventsWXList.Length > 0 && eventsWXList[0] != null)
                {
                    data.WXCodeImageUrl = eventsWXList[0].ImageUrl;
                    data.WXCode = eventsWXList[0].QRCode;
                    data.QRCodeTypeId = eventsWXList[0].QRCodeTypeId != null ? eventsWXList[0].QRCodeTypeId.ToString() : "";
                    data.MaxWQRCod = data.WXCode;
                    
                }

                #region 图文|二维码

                var entity = eventsWXList.FirstOrDefault();
                if (entity != null)
                {
                    // data.imageUrl = entity.ImageUrl;
                    var WKeywordReplyentity = new WKeywordReplyBLL(this.CurrentUserInfo).QueryByEntity(new WKeywordReplyEntity()
                    {
                        Keyword = entity.QRCodeId.ToString()

                    }, null).FirstOrDefault();
                    if (WKeywordReplyentity != null)
                    {
                        if (WKeywordReplyentity.ReplyType == 1)
                        {
                            data.ReplyType = "1";
                            data.Text = WKeywordReplyentity.Text;//文本信息

                        }
                        else if (WKeywordReplyentity.ReplyType == 3)
                        {
                            data.ReplyType = "3";
                            WMenuMTextMappingBLL bll = new WMenuMTextMappingBLL(this.CurrentUserInfo);
                            WMaterialTextBLL wmbll = new WMaterialTextBLL(this.CurrentUserInfo);
                            OrderBy[] pOrderBys = new OrderBy[]{
                             new OrderBy(){ FieldName="CreateTime", Direction= OrderByDirections.Asc}
                            };
                            //图文映射
                          //  List<WMenuMTextMappingEntity> listMapping = new List<WMenuMTextMappingEntity>();
                            var textMapping = bll.QueryByEntity(new WMenuMTextMappingEntity
                            {
                                MenuId = WKeywordReplyentity.ReplyId,
                                IsDelete = 0
                            }, pOrderBys);
                           /// listMapping.Add(textMapping);


                            if (textMapping != null && textMapping.Length > 0)
                            {
                                List<WMaterialTextEntity> list = new List<WMaterialTextEntity>();
                                foreach (var item in textMapping)
                                {
                                    WMaterialTextEntity WMaterialTextentity = wmbll.QueryByEntity(new WMaterialTextEntity { TextId = item.TextId, IsDelete = 0 }, null)[0];
                                    WMaterialTextentity.TestId = WMaterialTextentity.TextId;//
                                    WMaterialTextentity.ImageUrl = WMaterialTextentity.CoverImageUrl;
                                    list.Add(WMaterialTextentity);
                                }
                                data.listMenutext = list;
                                data.listMenutextMapping = textMapping.ToList();
                            }

                        }
                    }


                }
                #endregion

                //取图片
                var imageService = new ObjectImagesBLL(CurrentUserInfo);
                var itemObj = imageService.QueryByEntity(new ObjectImagesEntity() { ObjectId = key }, null);
                if (itemObj != null && itemObj.Length > 0)
                {
                    data.ItemImageList= itemObj.OrderBy(item => item.DisplayIndex).ToList();
                }



            }


            var jsonData = new JsonData();
            jsonData.totalCount = data == null ? "0" : "1";
            jsonData.data = data;

            
            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region SaveUnitData
        /// <summary>
        /// 保存门店
        /// </summary>
        public string SaveUnitData()
        {
            var unitService = new UnitService(CurrentUserInfo);
            var cityService = new CityService(CurrentUserInfo);
            UnitInfo obj = new UnitInfo();
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string unit_id = string.Empty;
            if (Request("unit") != null && Request("unit") != string.Empty)
            {
                key = Request("unit").ToString().Trim();
            }
            if (Request("unit_id") != null && Request("unit_id") != string.Empty)
            {
                unit_id = Request("unit_id").ToString().Trim();
            }

            obj = key.DeserializeJSONTo<UnitInfo>();

            if (unit_id.Trim().Length != 0)
            {
                obj.Id = unit_id;
            }

            if (obj.CityId == "root") obj.CityId = "";//省份的上级，就是没有选择城市
            if (obj.CityId != null && obj.CityId.Trim().Length > 0 && obj.CityId.Trim().Length < 10)
            {
                obj.CityId = cityService.GetCityGUIDByCityCode(obj.CityId);//这里传过来的是区县的code
            }

            //if (obj.Parent_Unit_Id == null || obj.Parent_Unit_Id.Trim().Length == 0)
            //{
            //    responseData.success = false;
            //    responseData.msg = "上级单位不能为空";
            //    return responseData.ToJSON();
            //}
            //二维码类别
            var wqrentity = new WQRCodeTypeBLL(this.CurrentUserInfo).QueryByEntity(

                new WQRCodeTypeEntity { TypeCode = "UnitQrCode" }

                , null).FirstOrDefault();
            if (wqrentity == null)
            {
                responseData.success = false;
                responseData.msg = "无法获取门店二维码类别";
                return responseData.ToJSON();
            }
          
            if (obj.Name == null || obj.Name.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "门店名称不能为空";
                return responseData.ToJSON();
            }
            obj.Code = obj.Name;//没有编码的时候，就传名称
            if (obj.Code == null || obj.Code.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "门店编码不能为空";
                return responseData.ToJSON();
            }
            if (obj.CityId == null || obj.CityId.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "城市不能为空";
                return responseData.ToJSON();
            }
            if (obj.Address == null || obj.Address.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "地址不能为空";
                return responseData.ToJSON();
            }
            if (obj.Contact == null || obj.Contact.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "联系人不能为空";
                return responseData.ToJSON();
            }
            //if (obj.Telephone == null || obj.Telephone.Trim().Length == 0)
            //{
            //    responseData.success = false;
            //    responseData.msg = "电话不能为空";
            //    return responseData.ToJSON();
            //}

            if (obj.PropertyList != null)
            {
                foreach (var tmpProp in obj.PropertyList)
                {
                    tmpProp.UnitId = obj.Id;
                    tmpProp.Id = Utils.NewGuid();
                }
            }

            if (obj.UnitSortIdList != null)
            {
                obj.UnitSortMappingList = new List<TUnitUnitSortMappingEntity>();
                foreach (var tmpProp in obj.UnitSortIdList)
                {
                    if (tmpProp != null && tmpProp.Length != 0)
                        obj.UnitSortMappingList.Add(new TUnitUnitSortMappingEntity()
                        {
                            MappingId = Utils.NewGuid(),
                            UnitId = obj.Id,
                            UnitSortId = tmpProp
                        });
                }
            }
            if (obj.Parent_Unit_Id == "")
            {
                obj.Parent_Unit_Id = "-99";
            }
            if (string.IsNullOrEmpty(obj.TypeId))//如果是空的时候就查找该商户下的门店类型(兼容原来老的接口和新的接口)
            {
                T_TypeBLL t_TypeBLL = new T_TypeBLL(this.CurrentUserInfo);
                T_TypeEntity typeEn = new T_TypeEntity();
                typeEn.type_code = "门店";
                typeEn.customer_id = this.CurrentUserInfo.ClientID;
                var typeList = t_TypeBLL.QueryByEntity(typeEn, null);
                if (typeList != null && typeList.Length != 0)
                {
                    obj.TypeId = typeList[0].type_id;
                }
                // obj.TypeId=
            }

            unitService.SetUnitInfo(CurrentUserInfo, obj);



            #region 生成二维码
            var QRCodeId = Guid.NewGuid();
            //在新建门店时，就创建了二维码，会用到下面的代码****
            var wapentity = new WApplicationInterfaceBLL(this.CurrentUserInfo).QueryByEntity(new WApplicationInterfaceEntity
            {

                CustomerId = this.CurrentUserInfo.ClientID,
                IsDelete = 0

            }, null).FirstOrDefault();
            if (!string.IsNullOrEmpty(obj.WXCodeImageUrl) && wapentity != null)
            {
                var QRCodeManagerentity = new WQRCodeManagerBLL(this.CurrentUserInfo).QueryByEntity(new WQRCodeManagerEntity
                {
                    ObjectId = unit_id
                }, null).FirstOrDefault();
                if (QRCodeManagerentity != null)
                {
                    QRCodeId = (Guid)QRCodeManagerentity.QRCodeId;
                }
                if (QRCodeManagerentity == null)
                {
                    //var wqrentity = new WQRCodeTypeBLL(this.CurrentUserInfo).QueryByEntity(

                    //                  new WQRCodeTypeEntity { TypeCode = "UnitQrCode" }

                    //                   , null).FirstOrDefault();



                    var WQRCodeManagerbll = new WQRCodeManagerBLL(this.CurrentUserInfo);


                    WQRCodeManagerbll.Create(new WQRCodeManagerEntity
                    {
                        QRCodeId = QRCodeId,
                        QRCode = obj.MaxWQRCod,
                        QRCodeTypeId = wqrentity.QRCodeTypeId,
                        IsUse = 1,
                        ObjectId = unit_id,
                        CreateBy = this.CurrentUserInfo.UserID,
                        ApplicationId = wapentity.ApplicationId,
                        IsDelete = 0,
                        ImageUrl = obj.WXCodeImageUrl,
                        CustomerId = this.CurrentUserInfo.ClientID

                    });

                }
            }
            #endregion


            #region 添加图文消息|文本信息
            var WKeywordReplyentity = new WKeywordReplyBLL(this.CurrentUserInfo).QueryByEntity(new WKeywordReplyEntity()
            {
                Keyword = QRCodeId.ToString()

            }, null).FirstOrDefault();

            if (wapentity == null)
            {
                //无设置微信公众平台,不保存图文信息
            }
            else if (WKeywordReplyentity == null)
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

            #endregion
            responseData.success = true;
            responseData.msg = error;
            responseData.data = obj.Id;


            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region DeleteData
        /// <summary>
        /// 删除
        /// </summary>
        public string DeleteData()
        {
            var service = new UnitService(CurrentUserInfo);
            var serviceUnit = new RoleService(CurrentUserInfo);

            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            try
            {
                string key = string.Empty;
                if (FormatParamValue(Request("ids")) != null && FormatParamValue(Request("ids")) != string.Empty)
                {
                    key = FormatParamValue(Request("ids")).ToString().Trim();
                }

                if (key == null || key.Trim().Length == 0)
                {
                    responseData.success = false;
                    responseData.msg = "ID不能为空";
                    return responseData.ToJSON();
                }
                if (serviceUnit.GetRoleCountByUnit(key)>0)
                {
                    responseData.success = false;
                    responseData.msg = "门店关联了员工,请勿删除！";
                    return responseData.ToJSON();
                }
                if (!string.IsNullOrEmpty(Request("IsDelete")) && Request("IsDelete").ToString() == "1")
                {
                    service.physicalDeleteUnit(key);
                }
                else
                {
                    var status = "-1";
                    if (FormatParamValue(Request("status")) != null && FormatParamValue(Request("status")) != string.Empty)
                    {
                        status = FormatParamValue(Request("status")).ToString().Trim();
                    }
                    string[] ids = key.Split(',');
                    foreach (var id in ids)
                    {
                        service.SetUnitStatus(key, status);
                    }

                }
                responseData.success = true;
                responseData.msg = error;
            }
            catch (Exception ex)
            {
                responseData.success = false;
                responseData.msg = ex.Message;
            }

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region GetItemImageInfoByUnitIdData
        /// <summary>
        /// 通过门店ID获取商品图片信息
        /// </summary>
        public string GetItemImageInfoByUnitIdData()
        {
            var imageService = new ObjectImagesBLL(CurrentUserInfo);
            IList<ObjectImagesEntity> data = new List<ObjectImagesEntity>();
            string content = string.Empty;

            string key = string.Empty;
            if (Request("unit_id") != null && Request("unit_id") != string.Empty)
            {
                key = Request("unit_id").ToString().Trim();
            }

            var itemObj = imageService.QueryByEntity(new ObjectImagesEntity() { ObjectId = key }, null);
            if (itemObj != null && itemObj.Length > 0)
            {
                data = itemObj.OrderBy(item => item.DisplayIndex).ToList();
            }

            var jsonData = new JsonData();
            jsonData.totalCount = data.Count.ToString();
            jsonData.data = data;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region GetItemBrandInfoByUnitIdData
        /// <summary>
        /// 通过门店ID获取商品Brand信息
        /// </summary>
        public string GetItemBrandInfoByUnitIdData()
        {
            var service = new StoreBrandMappingBLL(CurrentUserInfo);
            var brandDetailBLL = new BrandDetailBLL(CurrentUserInfo);
            IList<StoreBrandMappingEntity> data = new List<StoreBrandMappingEntity>();
            string content = string.Empty;

            string key = string.Empty;
            if (Request("unit_id") != null && Request("unit_id") != string.Empty)
            {
                key = Request("unit_id").ToString().Trim();
            }

            var itemObj = service.QueryByEntity(new StoreBrandMappingEntity() { StoreId = key }, null);
            if (itemObj != null && itemObj.Length > 0)
            {
                //data = itemObj.OrderBy(item => item.StoreName).ToList();
                foreach (var item in itemObj)
                {
                    var brandObj = brandDetailBLL.GetByID(item.BrandId);
                    item.BrandName = brandObj.BrandName;
                }
                data = itemObj.ToList();
            }

            var jsonData = new JsonData();
            jsonData.totalCount = data.Count.ToString();
            jsonData.data = data;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region 生成门店二维码
        /// <summary>
        /// 获取门店二维码
        /// </summary>
        /// <returns></returns>
        public string SetUnitWXCode()
        {
            #region 参数处理
            string WeiXinId = Request("WeiXinId");
            string UnitId = Request("UnitId");
            string WXCode = Request("WXCode");
            var responseData = new ResponseData();
            if (WeiXinId == null || WeiXinId.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "公众号不能为空";
                return responseData.ToJSON();
            }

            if (UnitId == null || UnitId.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "门店标识不能为空";
                return responseData.ToJSON();
            }
            //if (WXCode == null || WXCode.Equals(""))
            //{
            //    VwUnitPropertyBLL unitServer = new VwUnitPropertyBLL(CurrentUserInfo);
            //    WXCode = unitServer.GetUnitWXCode(UnitId).ToString();

            //}
            #endregion

            #region 获取微信公众号信息
            WApplicationInterfaceBLL server = new WApplicationInterfaceBLL(CurrentUserInfo);
            var wxObj = server.QueryByEntity(new WApplicationInterfaceEntity
            {
                CustomerId = CurrentUserInfo.CurrentUser.customer_id
                ,
                IsDelete = 0
                ,
                WeiXinID = WeiXinId
            }, null);
            if (wxObj == null || wxObj.Length == 0)
            {
                responseData.success = false;
                responseData.msg = "不存在对应的微信帐号";
                return responseData.ToJSON().ToString();
            }
            else
            {
                WApplicationInterfaceBLL wApplicationInterfaceBLL = new WApplicationInterfaceBLL(CurrentUserInfo);
                var appObj = wApplicationInterfaceBLL.QueryByEntity(new WApplicationInterfaceEntity()
                {
                    WeiXinID = WeiXinId
                }, null);
                var appId = "";
                if (appObj != null && appObj.Length > 0)
                {
                    appId = appObj[0].ApplicationId;
                }

                WQRCodeManagerBLL wQRCodeManagerBLL = new WQRCodeManagerBLL(CurrentUserInfo);
                WQRCodeTypeBLL wQRCodeTypeBLL = new WQRCodeTypeBLL(CurrentUserInfo);
                var qrTypeList = wQRCodeTypeBLL.GetList(new WQRCodeTypeEntity()
                {
                    QRCodeTypeId = Guid.Parse(WXCode)
                }, 0, 1);
                if (qrTypeList == null && qrTypeList.Count == 0)
                {
                    responseData.success = false;
                    responseData.msg = "二维码类型获取失败";
                    return responseData.ToJSON();
                }
                string qrTypeId = qrTypeList[0].QRCodeTypeId.ToString();
                if (!wQRCodeManagerBLL.CheckByObjectId(UnitId))
                {
                    var tmpQRObj = wQRCodeManagerBLL.GetOne(appId, qrTypeId);
                    if (tmpQRObj != null)
                    {
                        tmpQRObj.ObjectId = UnitId;
                        tmpQRObj.IsUse = 1;
                        wQRCodeManagerBLL.Update(tmpQRObj, false);
                        responseData.success = true;
                        responseData.msg = tmpQRObj.ImageUrl;
                        responseData.data = tmpQRObj.QRCode;
                        return responseData.ToJSON().ToString();
                    }
                    else
                    {
                        responseData.success = false;
                        responseData.msg = "二维码获取失败，请先去添加固定二维码";
                        return responseData.ToJSON();
                    }
                }
                else
                {
                    responseData.success = false;
                    responseData.msg = "二维码已绑定";
                    return responseData.ToJSON();
                }

            }
            #endregion

        }
        #endregion

        #region new 生成门店二维码
        public string CretaeWxCode()
        {
            var responseData = new ResponseData();
            try
            {
                //微信 公共平台
                var wapentity = new WApplicationInterfaceEntity();
                if (!string.IsNullOrEmpty(Request("unit_id")))
                {
                    //门店关联的公众号
                    var tuBll = new t_unitBLL(CurrentUserInfo);
                    var tuEntity = new t_unitEntity();
                    tuEntity = tuBll.QueryByEntity(new t_unitEntity() { unit_id = Request("unit_id") }, null).FirstOrDefault();
                    if (tuEntity != null)
                    {
                        wapentity = new WApplicationInterfaceBLL(this.CurrentUserInfo).QueryByEntity(new WApplicationInterfaceEntity
                        {
                            WeiXinID = tuEntity.weiXinId,
                            CustomerId = this.CurrentUserInfo.ClientID,
                            IsDelete = 0
                        }, null).FirstOrDefault();
                    }
                    else
                    {
                        wapentity = new WApplicationInterfaceBLL(this.CurrentUserInfo).QueryByEntity(new WApplicationInterfaceEntity
                        {
                            CustomerId = this.CurrentUserInfo.ClientID,
                            IsDelete = 0
                        }, null).FirstOrDefault();
                    }
                }
                else
                {
                    wapentity = new WApplicationInterfaceBLL(this.CurrentUserInfo).QueryByEntity(new WApplicationInterfaceEntity
                    {
                        CustomerId = this.CurrentUserInfo.ClientID,
                        IsDelete = 0
                    }, null).FirstOrDefault();
                }

                //获取当前二维码 最大值
                var MaxWQRCod = new WQRCodeManagerBLL(this.CurrentUserInfo).GetMaxWQRCod() + 1;

                if (wapentity == null)
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
                    var unitTypeBll = new WQRCodeTypeBLL(this.CurrentUserInfo);
                    var unitType = unitTypeBll.QueryByEntity(new WQRCodeTypeEntity() { TypeCode = "UnitQrCode" }, null);
                    var unit_id = Request("unit_id");
                    if (!string.IsNullOrEmpty(unit_id) && unitType != null && unitType.Length > 0)
                    {
                        var unitQrcodeBll = new WQRCodeManagerBLL(this.CurrentUserInfo);
                        var unitQrCode = new WQRCodeManagerEntity();
                        unitQrCode.QRCode = MaxWQRCod.ToString();
                        unitQrCode.QRCodeTypeId = unitType[0].QRCodeTypeId;
                        unitQrCode.IsUse = 1;
                        unitQrCode.CustomerId = this.CurrentUserInfo.ClientID;
                        unitQrCode.ImageUrl = imageUrl;
                        unitQrCode.ApplicationId = wapentity.ApplicationId;//微信公众号的编号
                        //objectId 为门店ID
                        unitQrCode.ObjectId = Request("unit_id");
                        unitQrcodeBll.Create(unitQrCode);
                    }
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

        #region 下载图片
        public string UploadImagerUrl()
        {
            var responseData = new ResponseData();
            if (string.IsNullOrEmpty(Request("imageUrl")))
            {
                responseData.success = false;
                responseData.msg = "请先生成二维码";
                return responseData.ToJSON();
            }


            //  Url = "http://localhost:2330///HeadImage/20140903/20140903150820_9422.jpg";//测试url
            //   Url = "http://dev.o2omarketing.cn:8400/Framework/Upload/Image/20140820/271ADE72AD7F46A09E4952147F4876D3.png";
            //System.Net.WebClient wb = new System.Net.WebClient();
            //byte[] buffer = wb.DownloadData(Url);
            //System.IO.MemoryStream ms = new System.IO.MemoryStream(buffer);
            //System.Drawing.Image.FromStream(ms);
            //ms.Close();
            //wb.Dispose();
            //responseData.success = true;
            //return responseData.ToJSON();
            string imageURL = Request("imageUrl");
            string smallImagePath = imageURL.Substring(imageURL.IndexOf("HeadImage") + "HeadImage".Length + 1);//取这一部分

            string targetPath = this.CurrentContext.Server.MapPath("/HeadImage/");
            string imagePath = targetPath + smallImagePath.Replace('/', '\\');// imageURL.Substring(imageURL.LastIndexOf("/")+1);
            string imageName = imageURL.Substring(imageURL.LastIndexOf("/") + 1);
            string itemName = imageName.Substring(0, imageName.IndexOf("."));
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
            return "";
        }

        #endregion

        #region 导入门店
        public string ImportUnit()
        {
             var responseData = new ResponseData();
             var unitService = new UnitService(CurrentUserInfo);
             ExcelHelper excelHelper = new ExcelHelper();
             if (Request("filePath")!=null && Request("filePath").ToString()!="")
             {
                 try
                 {
                     var rp = new ImportRP();
                     string strPath = Request("filePath").ToString();
                     string strFileName = string.Empty;
                     DataSet ds = unitService.ExcelToDb(HttpContext.Current.Server.MapPath(strPath), CurrentUserInfo);
                     if (ds != null && ds.Tables.Count > 1 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                     {

                         Workbook wb = JIT.Utility.DataTableExporter.WriteXLS(ds.Tables[0], 0);
                         string savePath = HttpContext.Current.Server.MapPath(@"~/File/ErrFile/Unit");
                         if (!System.IO.Directory.Exists(savePath))
                         {
                             System.IO.Directory.CreateDirectory(savePath);
                         }
                         strFileName = "\\门店错误信息导出" + DateTime.Now.ToFileTime() + ".xls";
                         savePath = savePath + strFileName;
                         wb.Save(savePath);//保存Excel文件
                         rp = new ImportRP()
                         {
                             Url = "/File/ErrFile/Unit" + strFileName,
                             TotalCount = Convert.ToInt32(ds.Tables[1].Rows[0]["TotalCount"].ToString()),
                             ErrCount = Convert.ToInt32(ds.Tables[1].Rows[0]["ErrCount"].ToString())
                         };
                     }
                     else
                     {
                         rp = new ImportRP()
                         {
                             Url = "",
                             TotalCount = Convert.ToInt32(ds.Tables[1].Rows[0]["TotalCount"].ToString()),
                             ErrCount = Convert.ToInt32(ds.Tables[1].Rows[0]["ErrCount"].ToString())
                         };

                         responseData.success = true;
                     }
                     responseData.success = true;
                     responseData.data = rp;
                 }
                 catch (Exception err)
                 {
                     responseData.success = false;
                     responseData.msg = err.Message.ToString();
                 }
             }
             return responseData.ToJSON();
           

        }
        #endregion
    }

    #region QueryEntity
    public class UnitQueryEntity
    {
        public string unit_code;
        public string unit_name;
        public string unit_type_id;
        public string unit_tel;
        public string city_code;
        public string unit_city_id;
        public string unit_status;
        public string StoreType;
        public string Parent_Unit_ID;
        public string OnlyShop;
    }
    #endregion
    public class ReposeData
    {
        public string imageUrl { set; get; }

        public int MaxWQRCod { set; get; }

    }
    public class ImportRP
    {
        public string Url { get; set; }
        public int TotalCount { get; set; }
        public int ErrCount { get; set; }
    }
}