using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using Aspose.Cells;
using JIT.Utility;
using JIT.CPOS.BS.Web.Base.Excel;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Entity;
using System.Web.Script.Serialization;
using JIT.CPOS.BS.Web.ApplicationInterface.Vip;
using JIT.CPOS.Common;


namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Item
{
    /// <summary>
    /// ItemNewHandler 的摘要说明
    /// </summary>
    public class ItemNewHandler : BaseGateway
    {
        #region 错误码
        const int ERROR_AUTHCODE_NOTEXISTS = 330;
        const int ERROR_AUTHCODE_INVALID = 331;
        const int ERROR_AUTHCODE_NOT_EQUALS = 333;
        const int ERROR_AUTHCODE_WAS_USED = 332;
        const int ERROR_LACK_MOBILE = 312;
        const int ERROR_LACK_VIP_SOURCE = 315;
        const int ERROR_AUTO_MERGE_MEMBER_FAILED = 313;
        const int ERROR_MEMBER_REGISTERED = 314;

        const int ERROR_VIPCARD_EXISTS = 340;   //会员已办卡
        #endregion


        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst;
            switch (pAction)
            {
                case "GetItemBaseData":  //获取商品基础数据（属性、sku、商品价格类型）
                    rst = this.GetItemBaseData(pRequest);
                    break;
                case "save_itemNew":  //新的保存商品方法
                    rst = SaveItemData(pRequest);
                    break;
                case "UpdateSalesPromotion":  //更改促销分组
                    rst = UpdateSalesPromotion(pRequest);
                    break;
                case "SaveSku":  //更改促销分组
                    rst = SaveSku(pRequest);
                    break;
                case "SaveSkuValue":  //更改促销分组
                    rst = SaveSkuValue(pRequest);
                    break;
                case "GetSkuList":  //更改促销分组
                    rst = GetSkuList(pRequest);
                    break;
                case "ItemToggleStatus":  //更改促销分组
                    rst = ItemToggleStatus(pRequest);
                    break;
                case "CategorySort":  //更改促销分组
                    rst = CategorySort(pRequest);
                    break;
                case "GetItemType":
                    rst = GetItemType();
                    break;

                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                    {
                        ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                    };
            }
            //HttpContext.Current.Response.ContentType = "text/html;charset=UTF-8";  
            return rst;
        }

        #region  获取商品基础数据（属性、sku、商品价格类型）
        public string GetItemBaseData(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetItemBaseDataRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var rd = new GetItemBaseDataRD();//返回值
            /// Prop_Type属性类型 1=组,2=属性,3=属性明细';
            var propService = new JIT.CPOS.BS.BLL.PropService(loggingSessionInfo);
            var ItemDomain = "ITEM";
            var grouplist = propService.GetPropListFirstByDomain(ItemDomain);//获取商品属性组
            foreach (var group in grouplist)
            {
                var propList = propService.GetPropListByParentId(ItemDomain, group.Prop_Id);//获取属性                
                //Prop_Default_Value,每个属性上有默认的属性值
                foreach (var prop in propList)
                {
                    var propDetailList = propService.GetPropListByParentId(ItemDomain, prop.Prop_Id);//获取属性明细 （即属性的选项值）   
                    prop.Children = propDetailList;
                }
                group.Children = propList;
            }
            rd.ItemPropGroupList = grouplist;
            //sku部分
            var SKUDomain = "SKU";
            var skuPropService = new JIT.CPOS.BS.BLL.SkuPropServer(loggingSessionInfo);
            var skuList = skuPropService.GetSkuPropList();//获取了所有的规格（T_SKU_PROPerty a  和  T_Prop关联有数据的），T_SKU_PROPerty只存T_Prop表里prop_type为2（即属性级别的）并且domain为sku的
            foreach (var sku in skuList)
            {
                sku.Children = propService.GetPropListByParentId(SKUDomain, sku.prop_id);//sku的明细项
            }
            rd.SKUPropList = skuList;

            //获取价格类型
            var priceTypeService = new JIT.CPOS.BS.BLL.ItemPriceTypeService(loggingSessionInfo);
            var priceTypeList = priceTypeService.GetItemPriceTypeList();
            rd.ItemPriceTypeList = priceTypeList;

            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }
        #endregion
        #region 保存商品信息
        public string SaveItemData(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SaveItemDataRP>>();

            if (string.IsNullOrEmpty(rp.Parameters.Item_Category_Id))
            {
                throw new APIException("缺少参数【Item_Category_Id】或参数值为空") { ErrorCode = 135 };
            }
            if (string.IsNullOrEmpty(rp.Parameters.Item_Name))
            {
                throw new APIException("缺少参数【Item_Name】或参数值为空") { ErrorCode = 135 };
            }
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;//获取session数据           
            var itemService = new ItemService(loggingSessionInfo);
            var item = new ItemInfo();
            if (string.IsNullOrEmpty(rp.Parameters.Item_Id))
            {
                item.Item_Id = Utils.NewGuid();
            }
            else
            {
                item.Item_Id = rp.Parameters.Item_Id;
            }
            item.Item_Code = rp.Parameters.Item_Code;
            item.Item_Name = rp.Parameters.Item_Name;
            item.Item_Category_Id = rp.Parameters.Item_Category_Id;

            if (string.IsNullOrEmpty(rp.Parameters.Item_Code))
            {
                item.Item_Code = itemService.GetGreatestItemCode(loggingSessionInfo);
                if (item.Item_Code.Length == 0)
                {
                    throw new APIException("商品编码自动生成失败，请联系管理员。") { ErrorCode = 135 };
                }
            }
            else
            {
                //if (!string.IsNullOrEmpty(item.Item_Id))
                //{
                    if (!itemService.IsExistItemCode(loggingSessionInfo, item.Item_Code, item.Item_Id))
                    {
                        throw new APIException("商品编码不能重复。") { ErrorCode = 135 };
                    }
                //}
            }
            item.OperationType = rp.Parameters.OperationType;
            item.ItemImageList = rp.Parameters.ItemImageList;
            item.ItemPropList = rp.Parameters.ItemPropList;

            item.T_ItemSkuProp = rp.Parameters.T_ItemSkuProp;
            item.SkuList = rp.Parameters.SkuList;//
            item.SalesPromotionList = rp.Parameters.SalesPromotionList;//促销分组
            item.OperationType = rp.Parameters.OperationType;//促销分组
            item.ifservice = rp.Parameters.ifservice;
            item.isGB = rp.Parameters.IsGB; // 0-非标商品，1-标准商品


            //sku值
            if (item.SkuList != null && item.SkuList.Count > 0)//在单个的时候都已经添加过了
            {
                foreach (var tmpSku in item.SkuList)
                {
                    tmpSku.item_id = item.Item_Id;
                    if (tmpSku.sku_id == null || tmpSku.sku_id.Length == 0)
                    {
                        tmpSku.sku_id = Utils.NewGuid();
                    }
                    //处理sku相关价格(jifeng.cao 20140224)
                    foreach (var skuprice in tmpSku.sku_price_list)
                    {
                        skuprice.sku_id = tmpSku.sku_id;
                        if (skuprice.sku_price_id == null || skuprice.sku_price_id.Length == 0)
                        {
                            skuprice.sku_price_id = Utils.NewGuid();
                        }
                    }

                }
            }
            else
            {
                //对于无SKU配置的客户，默认生成一个SKU
                SkuInfo skuInfo = new SkuInfo();
                skuInfo.sku_id = Utils.NewGuid();
                skuInfo.item_id = item.Item_Id;
                // item_id = skuInfo.sku_id;
                //skuInfo.barcode = skuInfo.item_code;
                //skuInfo.prop_1_detail_id = "--";
                //skuInfo.prop_2_detail_id = "--";
                //skuInfo.prop_3_detail_id = "--";
                //skuInfo.prop_4_detail_id = "--";
                //skuInfo.prop_5_detail_id = "--";
                //skuInfo.status = "1";
                //skuInfo.create_user_id = "System";
                //skuInfo.create_time = DateTime.Now.ToString();

                item.SkuList.Add(skuInfo);
            }

            if (rp.Parameters.ifservice == 1)
            {
                T_VirtualItemTypeSettingBLL bllVirtualItem = new T_VirtualItemTypeSettingBLL(loggingSessionInfo);
                T_VirtualItemTypeSettingEntity entityVirtualItem = new T_VirtualItemTypeSettingEntity();
                //entityVirtualItem = bllVirtualItem.QueryByEntity(new T_VirtualItemTypeSettingEntity() { ItemId = rp.Parameters.Item_Id, SkuId = rp.Parameters.SkuList[0].sku_id }, null).FirstOrDefault();
                entityVirtualItem = bllVirtualItem.QueryByEntity(new T_VirtualItemTypeSettingEntity() { ItemId = rp.Parameters.Item_Id}, null).FirstOrDefault();
                if (entityVirtualItem != null)
                {
                    entityVirtualItem.VirtualItemTypeId = new Guid(rp.Parameters.VirtualItemTypeId);
                    entityVirtualItem.ObjecetTypeId = rp.Parameters.ObjecetTypeId;
                    bllVirtualItem.Update(entityVirtualItem);
                }
                else
                {
                    entityVirtualItem = new T_VirtualItemTypeSettingEntity
                    {
                        Id = Guid.NewGuid(),
                        ItemId =item.Item_Id,
                        SkuId = rp.Parameters.SkuList[0].sku_id,
                        VirtualItemTypeId = new Guid(rp.Parameters.VirtualItemTypeId),
                        ObjecetTypeId = rp.Parameters.ObjecetTypeId,
                        CustomerId = loggingSessionInfo.ClientID
                    };
                    bllVirtualItem.Create(entityVirtualItem);
                }
            }
            //保存sku属性名****

            string error = "";
            bool result = itemService.SetItemInfoNew(item, out error);
            //item.Item_Id
            var rd = new SaveItemDataRD();//返回值
            rd.Item_Id = item.Item_Id;
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            rsp.Message = error;

            return rsp.ToJSON();
        }

        #endregion
        #region  更新促销分组
        public string UpdateSalesPromotion(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<UpdateSalesPromotionRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var rd = new UpdateSalesPromotionRD();//返回值

            //处理促销分组

            ItemCategoryMappingBLL itemCategoryMappingBLL = new ItemCategoryMappingBLL(loggingSessionInfo);

            foreach (var itemInfo in rp.Parameters.ItemInfoList)//数组，更新数据
            {
                itemCategoryMappingBLL.DeleteByItemID(itemInfo.Item_Id);
                //这里不应该删除之前的促销分组，而应该根据商品的id和促销分组的id找一找，如果有isdelete=0的，就不要加，没有就加
                ;
                foreach (var SalesPromotion in rp.Parameters.SalesPromotionList)
                {
                    ItemCategoryMappingEntity en = new ItemCategoryMappingEntity();
                    en.ItemId = itemInfo.Item_Id;
                    en.ItemCategoryId = SalesPromotion.ItemCategoryId;
                    var enlist = itemCategoryMappingBLL.QueryByEntity(en, null);
                    if (enlist == null || enlist.Length == 0)
                    {
                        SalesPromotion.MappingId = Guid.NewGuid();
                        SalesPromotion.ItemId = itemInfo.Item_Id;

                        //   SalesPromotion.status = "1";
                        SalesPromotion.IsDelete = 0;
                        SalesPromotion.CreateBy = "";
                        SalesPromotion.CreateTime = DateTime.Now;
                        SalesPromotion.LastUpdateTime = DateTime.Now;
                        SalesPromotion.LastUpdateBy = "";
                        itemCategoryMappingBLL.Create(SalesPromotion);
                    }
                }
            }


            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }
        #endregion
        #region  商品上架下架
        public string ItemToggleStatus(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<UpdateSalesPromotionRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var rd = new UpdateSalesPromotionRD();//返回值
            //更改商品上架下架状态 
            ItemService itemService = new ItemService(loggingSessionInfo);
            foreach (var itemInfo in rp.Parameters.ItemInfoList)//数组，更新数据
            {
                itemService.SetItemStatus(loggingSessionInfo, itemInfo.Item_Id, rp.Parameters.Status);
            }
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        #endregion
        #region  保存sku
        public string SaveSku(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SaveSkuRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var rd = new SaveSkuRD();//返回值

            //处理促销分组
            //先把之前的删除掉
            var service = new PropService(loggingSessionInfo);//保存到t_sku表
            //    var brandDetailBLL = new BrandDetailBLL(CurrentUserInfo);
            var skuPropServer = new SkuPropServer(loggingSessionInfo);//保存到T_Sku_Property
            var propInfo = rp.Parameters.SkuProp;
            if (string.IsNullOrEmpty(propInfo.Prop_Name))
            {
                throw new APIException("缺少参数【Prop_Name】或参数值为空") { ErrorCode = 135 };
            }

            bool isNew = false;
            if (string.IsNullOrEmpty(propInfo.Prop_Id))
            {
                propInfo.Prop_Id = Guid.NewGuid().ToString();
                isNew = true;
            }
            propInfo.Prop_Code = propInfo.Prop_Name;
            propInfo.Prop_Eng_Name = "";
            propInfo.Prop_Type = "2";//属性类型 1=组,2=属性,3=属性明细';
            propInfo.Parent_Prop_id = "-99";
            propInfo.Prop_Level = 1;
            propInfo.Prop_Domain = "SKU";
            propInfo.Prop_Input_Flag = "select";
            propInfo.Prop_Max_Length = 1000;
            propInfo.Prop_Default_Value = "";
            propInfo.Prop_Status = 1;
            propInfo.Display_Index = 0;
            if (isNew)
            {
                propInfo.Create_User_Id = loggingSessionInfo.UserID;
                propInfo.Create_Time = DateTime.Now.ToString();
            }
            propInfo.Modify_User_Id = loggingSessionInfo.UserID;
            propInfo.Modify_Time = DateTime.Now.ToString();
            string error = "";
            service.SaveProp(propInfo, ref error);
            if (!string.IsNullOrEmpty(error))
            {
                throw new APIException(error) { ErrorCode = 135 };
            }
            if (!isNew)//不是新的
            {
                string propIds = "";
                foreach (var itemInfo in propInfo.Children)//数组，更新数据
                {
                    if (!string.IsNullOrEmpty(itemInfo.Prop_Id))
                    {
                        if (propIds != "")
                        {
                            propIds += ",";
                        }
                        propIds += "'" + itemInfo.Prop_Id + "'";
                    }
                }
                //删除不在这个里面的
                service.DeletePropByIds(propIds, propInfo);
            }

            int i = 0;
            foreach (var itemInfo in propInfo.Children)//数组，更新数据
            {
                if (string.IsNullOrEmpty(itemInfo.Prop_Id))
                {
                    itemInfo.Prop_Id = Guid.NewGuid().ToString();
                }
                itemInfo.Prop_Code = itemInfo.Prop_Name;
                itemInfo.Prop_Eng_Name = "";
                itemInfo.Prop_Type = "3";//属性类型 1=组,2=属性,3=属性明细';
                itemInfo.Parent_Prop_id = propInfo.Prop_Id;//父类id
                itemInfo.Prop_Level = 2;
                itemInfo.Prop_Domain = "SKU";
                itemInfo.Prop_Input_Flag = "select";
                itemInfo.Prop_Max_Length = 1000;
                itemInfo.Prop_Default_Value = "";
                itemInfo.Prop_Status = 1;
                itemInfo.Display_Index = ++i;
                service.SaveProp(itemInfo, ref error);
            }

            //如果是新建的情况
            //如果不存在属性关系（sku和属性之间的关系）
            if (!skuPropServer.CheckSkuProp(propInfo.Prop_Id))
            {
                SkuPropInfo skuPropInfo = new SkuPropInfo();
                skuPropInfo.sku_prop_id = Utils.NewGuid();
                skuPropInfo.prop_id = propInfo.Prop_Id;
                skuPropInfo.CustomerId = loggingSessionInfo.ClientID;
                skuPropInfo.status = propInfo.Prop_Status.ToString();
                skuPropInfo.display_index = propInfo.Display_Index;
                skuPropServer.AddSkuProp(skuPropInfo);
            }

            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }
        #endregion
        #region  保存商品页面，临时添加一个sku的值

        public string SaveSkuValue(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SaveSkuValueRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var rd = new SaveSkuRD();//返回值

            //处理促销分组
            //先把之前的删除掉
            var service = new PropService(loggingSessionInfo);//保存到t_sku表

            //  var skuPropServer = new SkuPropServer(loggingSessionInfo);//保存到T_Sku_Property

            if (string.IsNullOrEmpty(rp.Parameters.parent_prop_id))
            {
                throw new APIException("缺少参数【parent_prop_id】或参数值为空") { ErrorCode = 135 };
            }
            if (string.IsNullOrEmpty(rp.Parameters.prop_name))
            {
                throw new APIException("缺少参数【prop_name】或参数值为空") { ErrorCode = 135 };
            }

            string error = "";
            PropInfo itemInfo = new PropInfo();
            itemInfo.Prop_Id = Guid.NewGuid().ToString();
            itemInfo.Prop_Name = rp.Parameters.prop_name;
            itemInfo.Prop_Code = itemInfo.Prop_Name;
            itemInfo.Prop_Eng_Name = "";
            itemInfo.Prop_Type = "3";//属性类型 1=组,2=属性,3=属性明细';
            itemInfo.Parent_Prop_id = rp.Parameters.parent_prop_id;//父类id
            itemInfo.Prop_Level = 2;
            itemInfo.Prop_Domain = "SKU";
            itemInfo.Prop_Input_Flag = "select";
            itemInfo.Prop_Max_Length = 1000;
            itemInfo.Prop_Default_Value = "";
            itemInfo.Prop_Status = 1;
            // itemInfo.Display_Index = ++i;
            if (service.CheckProp(itemInfo))//判断在同一个父类下面，是不是有这个属性代码了。
            {
                throw new APIException("该sku下面，已经有这个sku值了") { ErrorCode = 135 };
            }


            var children = service.GetPropListByParentId("SKU", rp.Parameters.parent_prop_id);
            int i = children == null ? 0 : children.Count;
            itemInfo.Display_Index = ++i;
            service.SaveProp(itemInfo, ref error);



            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }
        #endregion
        #region  获取sku数据
        public string GetSkuList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetSkuListRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var rd = new GetSkuListRD();//返回值

            var propService = new JIT.CPOS.BS.BLL.PropService(loggingSessionInfo);
            //sku部分
            var SKUDomain = "SKU";
            var skuPropService = new JIT.CPOS.BS.BLL.SkuPropServer(loggingSessionInfo);
            var skuList = skuPropService.GetSkuPropList();//获取了所有的规格（T_SKU_PROPerty a  和  T_Prop关联有数据的），T_SKU_PROPerty只存T_Prop表里prop_type为2（即属性级别的）并且domain为sku的
            foreach (var sku in skuList)
            {
                sku.Children = propService.GetPropListByParentId(SKUDomain, sku.prop_id);//sku的明细项,暂时用不到就不要加上了。
                if (sku.Children != null && sku.Children.Count != 0)
                {
                    sku.SkuValues = "";
                    foreach (var child in sku.Children)
                    {
                        if (sku.SkuValues != "")
                        {
                            sku.SkuValues += ",";
                        }
                        sku.SkuValues += child.Prop_Name;
                    }

                }

            }
            rd.SKUPropList = skuList;

            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }
        #endregion
        #region  保存交换后的分类商品分类的顺序

        public string CategorySort(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<CategorySortRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var rd = new SaveSkuRD();//返回值


            var service = new ItemCategoryService(loggingSessionInfo);//保存到商品分类表

            //  var skuPropServer = new SkuPropServer(loggingSessionInfo);//保存到T_Sku_Property

            if (rp.Parameters.CategoryList == null)
            {
                throw new APIException("缺少参数【CategoryList】或参数值为空") { ErrorCode = 135 };
            }


            foreach (var item in rp.Parameters.CategoryList)
            {
                service.SetItemCategoryDisplayIndex(loggingSessionInfo, item.Item_Category_Id, item.DisplayIndex == null ? 0 : (int)item.DisplayIndex);
            }



            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }
        #endregion

        /// <summary>
        /// 商品种类
        /// </summary>
        /// <returns></returns>
        public string GetItemType()
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
        
            SysVirtualItemTypeBLL bllSysVirtualItemType = new SysVirtualItemTypeBLL(loggingSessionInfo);
            var itemService = new ItemService(loggingSessionInfo);
            string content = string.Empty;
            var rd = new VirtualItemType();

            rd.VirtualItemTypeInfo = bllSysVirtualItemType.QueryByEntity(new SysVirtualItemTypeEntity() {IsDelete=0}, null).ToList();
            
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }
    }
    public class GetItemBaseDataRP : IAPIRequestParameter
    {
        public void Validate()
        {
        }
    }
    public class VirtualItemType : IAPIResponseData
    {
        public List<SysVirtualItemTypeEntity> VirtualItemTypeInfo { get; set; }
    }
    public class GetItemBaseDataRD : IAPIResponseData
    {
        public IList<PropInfo> ItemPropGroupList { get; set; }//属性组
        public IList<SkuPropInfo> SKUPropList { get; set; }//sku属性列表
        public IList<ItemPriceTypeInfo> ItemPriceTypeList { get; set; }//sku属性列表

    }


    public class SaveItemDataRP : IAPIRequestParameter
    {
        public string Item_Id { get; set; }
        public string Item_Category_Id { get; set; }
        public string Item_Code { get; set; }
        public string Item_Name { get; set; }
        public string OperationType { get; set; }

        //促销分组列表
        public List<ItemCategoryMappingEntity> SalesPromotionList { get; set; }
        //商品图片列表
        public IList<ObjectImagesEntity> ItemImageList { get; set; }
        /// <summary>
        /// 商品与属性关系集合
        /// </summary>
        public IList<ItemPropInfo> ItemPropList { get; set; }
        //商品sku名 (基础数据)
        //  public IList<SkuPropInfo> SkuPropList { get; set; }   //应该比这个类的属性值少，只需要prop_id就可以了
        public T_ItemSkuPropInfo T_ItemSkuProp { get; set; }
        //商品sku值列表
        public IList<SkuInfo> SkuList { get; set; }
        /// <summary>
        /// 是否虚拟商品
        /// </summary>
        public int ifservice { get; set; }
        /// <summary>
        /// 虚拟商品种类
        /// </summary>
        public string VirtualItemTypeId { get; set; }

        /// <summary>
        /// 优惠券类型ID或者卡类型ID
        /// </summary>
        public string ObjecetTypeId { get; set; }

        /// <summary>
        /// 是否为标准商品 
        /// </summary>
        public int IsGB { get; set; } 
        public void Validate()
        {
        }
    }

    public class SaveItemDataRD : IAPIResponseData
    {
        public string Item_Id { get; set; }
    }


    public class UpdateSalesPromotionRP : IAPIRequestParameter
    {
        //促销分组列表
        public List<ItemCategoryMappingEntity> SalesPromotionList { get; set; }
        //商品图片列表
        public IList<ItemInfo> ItemInfoList { get; set; }
        public string Status { get; set; }


        public void Validate()
        {
        }
    }
    public class UpdateSalesPromotionRD : IAPIResponseData
    {

    }


    public class SaveSkuRP : IAPIRequestParameter
    {
        //促销分组
        public PropInfo SkuProp { get; set; }


        public void Validate()
        {
        }
    }
    public class SaveSkuRD : IAPIResponseData
    {

    }

    public class SaveSkuValueRP : IAPIRequestParameter
    {
        //促销分组
        public string prop_name { get; set; }
        public string parent_prop_id { get; set; }

        public void Validate()
        {
        }
    }




    public class GetSkuListRP : IAPIRequestParameter
    {


        public void Validate()
        {
        }
    }
    public class GetSkuListRD : IAPIResponseData
    {
        public IList<SkuPropInfo> SKUPropList { get; set; }//sku属性列表
    }



    public class CategorySortRP : IAPIRequestParameter
    {
        //促销分组
        public string prop_name { get; set; }
        public string parent_prop_id { get; set; }

        public List<ItemCategoryInfo> CategoryList { get; set; }
        public void Validate()
        {
        }
    }

}