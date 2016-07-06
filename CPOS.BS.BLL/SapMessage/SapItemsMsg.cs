using JIT.CPOS.BS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.BLL.SapMessage
{
    public class SapItemsMsg : BaseSapMsg
    {
        public T_ItemBLL ItemBLL { get; set; }

        public T_SkuBLL SkuBLL { get; set; }

        public T_PropBLL PropBLL { get; set; }

        public StockBalanceService SockBalanceBLL { get; set; }

        public t_unitBLL unitBLL { get; set; }

        public T_Item_PropertyBLL itemPropBLL { get; set; }

        public T_Sku_PropertyBLL skuPropBLL { get; set; }

        public T_ItemSkuPropBLL itemSkuPropBLL { get; set; }

        // public ItemCategoryMappingBLL itemCategoryMapping { get; set; }

        public bool IsExist { get; set; }

        public SapItemsMsg()
            : base()
        {
            IsExist = false;
            ItemBLL = new T_ItemBLL(loggingSessionInfo);
            SkuBLL = new T_SkuBLL(loggingSessionInfo);
            PropBLL = new T_PropBLL(loggingSessionInfo);
            SockBalanceBLL = new StockBalanceService(loggingSessionInfo);
            unitBLL = new t_unitBLL(loggingSessionInfo);
            itemPropBLL = new T_Item_PropertyBLL(loggingSessionInfo);
            skuPropBLL = new T_Sku_PropertyBLL(loggingSessionInfo);
            itemSkuPropBLL = new T_ItemSkuPropBLL(loggingSessionInfo);
            // itemCategoryMapping = new ItemCategoryMappingBLL(loggingSessionInfo);
        }

        public override bool Add()
        {
            T_ItemEntity item = GetItemEntity();

            if (IsExist)
            {
                return true;
            }

            item.item_id = Guid.NewGuid().ToString().Replace("-", "");
            T_SkuEntity skuEntity = GetSkuEntity();
            skuEntity.item_id = item.item_id;
            string itemLocPath = "BOM/BO/Items/row/";

            #region 规格-1-U_Spec
            T_PropEntity prop1 = new T_PropEntity();
            prop1.prop_code = ReadXml(itemLocPath + "U_Spec");
            prop1.prop_name = ReadXml(itemLocPath + "U_Spec");
            prop1.parent_prop_id = "13accc74-2788-4f5e-b996-1af0ed39b1f7";
            prop1.prop_id = Guid.NewGuid().ToString().Replace("-", "");
            prop1.prop_input_flag = "select";
            prop1.prop_domain = "SKU";
            prop1.display_index = 1;
            prop1.status = 1;
            prop1.prop_type = "2";
            prop1.prop_level = 2;
            prop1.create_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            prop1.modify_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            T_Sku_PropertyEntity skuProp = new T_Sku_PropertyEntity();
            skuProp.CustomerID = "7e144bf108b94505a890ec3a7820db8d";
            skuProp.display_index = 1;
            skuProp.status = "1";
            skuProp.prop_id = prop1.prop_id;
            skuProp.sku_prop_id = Guid.NewGuid().ToString().Replace("-", "");

            #endregion

            skuEntity.sku_prop_id1 = prop1.prop_id;
            skuEntity.item_id = item.item_id;

            #region 库存单位->InvntryUom
            // 库存单位->InvntryUom
            T_Item_PropertyEntity itemProp1 = new T_Item_PropertyEntity();
            itemProp1.create_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            itemProp1.modify_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            itemProp1.item_property_id = Guid.NewGuid().ToString().Replace("-", "");
            itemProp1.item_id = item.item_id;
            // itemProp1.prop_id = prop1.prop_id;
            itemProp1.prop_id = GetPropIdByPropCode("InvntryUom", "库存单位");
            itemProp1.prop_value = ReadXml(itemLocPath + "InvntryUom");
            itemProp1.status = "1";
            itemProp1.create_user_id = "";
            itemProp1.modify_user_id = "";
            #endregion

            T_ItemSkuPropEntity itemSkuProp = new T_ItemSkuPropEntity();
            itemSkuProp.ItemSkuPropID = Guid.NewGuid().ToString().Replace("-", "");
            itemSkuProp.Item_id = item.item_id;
            itemSkuProp.ItemSku_prop_1_id = GetPropIdByPropCode("其他", "其他");
            itemSkuProp.status = "1";
            itemSkuProp.create_time = DateTime.Now;
            itemSkuProp.modify_time = DateTime.Now;
            itemSkuProp.IsDelete = 0;

            #region 包装损耗率->PackageLoseRate
            // 包装损耗率->PackageLoseRate
            T_Item_PropertyEntity itemProp2 = new T_Item_PropertyEntity();
            itemProp2.create_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            itemProp2.modify_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            itemProp2.item_property_id = Guid.NewGuid().ToString().Replace("-", "");
            itemProp2.item_id = item.item_id;
            // itemProp2.prop_id = prop2.prop_id;
            itemProp2.prop_id = GetPropIdByPropCode("PackageLoseRate", "包装损耗率");
            itemProp2.prop_value = ReadXml(itemLocPath + "PackageLoseRate");
            itemProp2.status = "1";
            itemProp2.create_user_id = "";
            itemProp2.modify_user_id = "";
            #endregion

            #region 计量损耗率->MeteringLoseRate
            // 计量损耗率->MeteringLoseRate
            T_Item_PropertyEntity itemProp3 = new T_Item_PropertyEntity();
            itemProp3.create_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            itemProp3.modify_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            itemProp3.item_property_id = Guid.NewGuid().ToString().Replace("-", "");
            itemProp3.item_id = item.item_id;
            // itemProp3.prop_id = prop3.prop_id;
            itemProp3.prop_id = GetPropIdByPropCode("MeteringLoseRate", "计量损耗率");
            itemProp3.prop_value = ReadXml(itemLocPath + "MeteringLoseRate");
            itemProp3.status = "1";
            itemProp3.create_user_id = "";
            itemProp3.modify_user_id = "";
            #endregion

            #region 预分拣->IsPreSort
            // 预分拣->IsPreSort
            T_Item_PropertyEntity itemProp4 = new T_Item_PropertyEntity();
            itemProp4.create_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            itemProp4.modify_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            itemProp4.item_property_id = Guid.NewGuid().ToString().Replace("-", "");
            itemProp4.item_id = item.item_id;
            // itemProp4.prop_id = prop3.prop_id;
            itemProp4.prop_id = GetPropIdByPropCode("IsPreSort", "预分拣商品");
            itemProp4.prop_value = ReadXml(itemLocPath + "IsPreSort") == "Y" ? "是" : "否";
            itemProp4.status = "1";
            itemProp4.create_user_id = "";
            itemProp4.modify_user_id = "";
            #endregion

            #region 存储方式->SortaModName
            // 存储方式->SortaModName
            T_Item_PropertyEntity itemProp5 = new T_Item_PropertyEntity();
            itemProp5.create_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            itemProp5.modify_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            itemProp5.item_property_id = Guid.NewGuid().ToString().Replace("-", "");
            itemProp5.item_id = item.item_id;
            itemProp5.prop_id = GetPropIdByPropCode("SortaMod", "存储方式");
            itemProp5.prop_value = ReadXml(itemLocPath + "SortaModName");
            itemProp5.status = "1";
            itemProp5.create_user_id = "";
            itemProp5.modify_user_id = "";
            #endregion

            #region 商品属性->ItemAttr
            // 商品属性--SortaModName
            T_Item_PropertyEntity itemProp6 = new T_Item_PropertyEntity();
            itemProp6.create_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            itemProp6.modify_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            itemProp6.item_property_id = Guid.NewGuid().ToString().Replace("-", "");
            itemProp6.item_id = item.item_id;
            itemProp6.prop_id = GetPropIdByPropCode("ItemAttr", "商品属性");
            itemProp6.prop_value = ReadXml(itemLocPath + "ItemAttrDesc");
            itemProp6.status = "1";
            itemProp6.create_user_id = "";
            itemProp6.modify_user_id = "";
            #endregion

            #region 商品属性->ItemAttr
            // 商品属性--SortaModName
            T_Item_PropertyEntity itemProp7 = new T_Item_PropertyEntity();
            itemProp7.create_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            itemProp7.modify_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            itemProp7.item_property_id = Guid.NewGuid().ToString().Replace("-", "");
            itemProp7.item_id = item.item_id;
            itemProp7.prop_id = "34FF4445D39F49AD8174954D18BC1347";
            itemProp7.prop_value = "0";
            itemProp7.status = "1";
            itemProp7.create_user_id = "SAP";
            itemProp7.modify_user_id = "SAP";
            #endregion

            //string itemPath = "BOM/BO/Items/row/";
            //ItemCategoryMappingEntity map = new ItemCategoryMappingEntity();
            //map.ItemCategoryId = item.item_category_id;
            //// map.ItemCategoryName = 
            //map.ItemId = item.item_id;
            //map.MappingId = Guid.NewGuid();
            //map.IsDelete = 0;
            //map.CreateBy = "SAP";
            //map.CreateTime = DateTime.Now;
            //map.LastUpdateTime = DateTime.Now;
            //map.LastUpdateBy = "SAP";
            //map.IsFirstVisit = 0;
            //map.ItemCategoryName = ReadXml(itemPath + "ItemClassName3");

            if (item != null)
            {
                ItemBLL.Create(item);
                PropBLL.Create(prop1);
                itemSkuPropBLL.Create(itemSkuProp);
                SkuBLL.Create(skuEntity);
                itemPropBLL.Create(itemProp1);
                itemPropBLL.Create(itemProp2);
                itemPropBLL.Create(itemProp3);
                itemPropBLL.Create(itemProp4);
                itemPropBLL.Create(itemProp5);
                itemPropBLL.Create(itemProp6);

                itemPropBLL.Create(itemProp7);

                string sql = @"INSERT	dbo.T_Stock_Balance
                                ( stock_balance_id ,
                                  unit_id ,
                                  warehouse_id ,
                                  sku_id ,
                                  begin_qty ,
                                  in_qty ,
                                  out_qty ,
                                  adjust_in_qty ,
                                  adjust_out_qty ,
                                  reserver_qty ,
                                  on_way_qty ,
                                  end_qty ,
                                  price ,
                                  item_label_type_id ,
                                  status ,
                                  create_time ,
                                  create_user_id ,
                                  modify_time ,
                                  modify_user_id ,
                                  sync_bs_flag
                                )
                        VALUES  ( N'{0}' , -- stock_balance_id - nvarchar(50)
                                  N'{1}' , -- unit_id - nvarchar(50)
                                  N'{2}' , -- warehouse_id - nvarchar(50)
                                  N'{3}' , -- sku_id - nvarchar(50)
                                  0 , -- begin_qty - decimal
                                  0 , -- in_qty - decimal
                                  0 , -- out_qty - decimal
                                  0 , -- adjust_in_qty - decimal
                                  0 , -- adjust_out_qty - decimal
                                  0 , -- reserver_qty - decimal
                                  0 , -- on_way_qty - decimal
                                  0 , -- end_qty - decimal
                                  0 , -- price - decimal
                                  N'' , -- item_label_type_id - nvarchar(50)
                                  N'1' , -- status - nvarchar(50)
                                  N'{4}' , -- create_time - nvarchar(50)
                                  N'' , -- create_user_id - nvarchar(50)
                                  N'{5}' , -- modify_time - nvarchar(50)
                                  N'' , -- modify_user_id - nvarchar(50)
                                  N''  -- sync_bs_flag - nvarchar(50)
                                )";
                string unitPath = "BOM/BO/ItemsLocation/row/LocationCode";
                sql = string.Format(sql, Guid.NewGuid().ToString().Replace("-", ""), GetUnitId(ReadXml(unitPath)), "", skuEntity.sku_id, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                SockBalanceBLL.InsertStockBalance(sql);
                return true;
            }
            return false;
        }

        public override bool Delete()
        {
            var item = ItemBLL.QueryByEntity(new T_ItemEntity() { item_code = MsgObjRD.Omsg.FieldValues }, null).FirstOrDefault();

            if (item != null)
            {
                item.status = "-1";
                item.modify_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                ItemBLL.Update(item);
                Msg = "商品置下架处理成功：" + item.item_code;
                return true;
            }

            Msg = "未找到商品item_code：" + MsgObjRD.Omsg.FieldValues;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private string GetPropIdByPropCode(string propCode, string prop_Name, string sapCode = "")
        {
            string zmindCOde = string.IsNullOrEmpty(sapCode) ? propCode : sapCode;
            var prop = PropBLL.QueryByEntity(new T_PropEntity() { prop_code = zmindCOde }, null).FirstOrDefault();
            if (prop == null)
            {
                prop = new T_PropEntity();
                prop.prop_code = zmindCOde;
                prop.prop_name = prop_Name;
                prop.parent_prop_id = "F70BC0A9FBA84F35963B3245AF25565A";
                prop.prop_id = Guid.NewGuid().ToString().Replace("-", "");
                prop.prop_input_flag = "label";
                prop.prop_domain = "ITEM";
                prop.display_index = 1;
                prop.status = 1;

                PropBLL.Create(prop);
            }
            return prop.prop_id;
        }

        private string GetUnitId(string locCode)
        {
            var unit = unitBLL.QueryByEntity(new t_unitEntity() { unit_code = locCode }, null).FirstOrDefault();
            if (unit != null)
            {
                return unit.unit_id;
            }
            return string.Empty;
        }

        #region 获取sku信息
        /// <summary>
        /// 获取sku信息
        /// </summary>
        /// <returns></returns>
        private T_SkuEntity GetSkuEntity()
        {
            string itemPath = "BOM/BO/Items/row/";
            T_SkuEntity sku = new T_SkuEntity();
            sku.barcode = ReadXml(itemPath + "CodeBars");
            sku.sku_id = Guid.NewGuid().ToString().Replace("-", "");
            sku.status = "1";
            sku.create_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            sku.modify_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            sku.create_user_id = "";
            sku.modify_user_id = "";
            sku.if_flag = "0";
            sku.bat_id = "";
            return sku;
        }
        #endregion

        private T_ItemEntity GetItemEntity(T_ItemEntity item = null)
        {
            string itemPath = "BOM/BO/Items/row/";
            string itemcode = ReadXml(itemPath + "ItemCode");
            if (item == null)
            {
                var temp = ItemBLL.QueryByEntity(new T_ItemEntity() { item_code = itemcode }, null);
                if (temp != null && temp.Length > 0)
                {
                    IsExist = true;
                    return item;
                }

                item = new T_ItemEntity();
                item.create_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                item.CustomerId = "7e144bf108b94505a890ec3a7820db8d";
                item.display_index = 0;
                item.data_from = "3";
                item.IsGB = 0;
                item.bat_id = "";
                item.if_flag = "0";
                item.ifgifts = 0;
                item.ifoften = 0;

                item.status_desc = "下架";
                item.status = "-1";
                item.item_remark = "";
                item.pyzjm = "";
                item.item_name_en = "";
                item.item_name_short = "";
                item.item_code = itemcode;
            }

            if (item.status != "-1")
            {
                string deletedStatus = ReadXml(itemPath + "Deleted");
                string activedStatus = ReadXml(itemPath + "Actived");
                if (deletedStatus == "N" || activedStatus == "N")
                {
                    item.status_desc = "下架";
                    item.status = "-1";
                }
            }


            item.bat_id = ReadXml(itemPath + "CodeBars");
            item.item_name = ReadXml(itemPath + "ItemName");
            item.item_category_id = GetCategoryIdByRefCode(ReadXml(itemPath + "ItemClassCode3"));
            item.ifservice = ReadXml(itemPath + "IsVirItem") == "N" ? 0 : 1;
            item.modify_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            return item;
        }

        #region 获取分类Id
        /// <summary>
        /// 获取分类Id
        /// </summary>
        /// <param name="categoryCode"></param>
        /// <returns></returns>
        private string GetCategoryIdByRefCode(string categoryCode)
        {
            string categoryId = string.Empty;
            T_Item_CategoryEntity category = new T_Item_CategoryBLL(loggingSessionInfo).QueryByEntity(new T_Item_CategoryEntity() { item_category_code = categoryCode }, null).FirstOrDefault();
            if (category != null)
            {
                categoryId = category.item_category_id;
            }
            return categoryId;
        }
        #endregion


        public override bool Update()
        {

            string itemPath = "BOM/BO/Items/row/";
            string item_code = ReadXml(itemPath + "ItemCode");
            T_ItemEntity item = ItemBLL.QueryByEntity(new T_ItemEntity() { item_code = item_code }, null).FirstOrDefault();

            if (item == null)
            {
                Msg = "商品不存在：" + item_code;
                return false;
            }
            item = GetItemEntity(item);

            ItemBLL.Update(item);
            return true;
        }
    }
}
