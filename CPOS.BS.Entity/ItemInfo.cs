using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 商品类
    /// </summary>
    [Serializable]
    public class ItemInfo
    {
        private string id;
        private string code;
        private string name;
        private string englishName;
        private string shortName;
        private string typeId;
        private string typeCode;
        private string typeDescription;
        private int isSku = 0;
        private int isBom = 0;
        private string barcode;
        private string status = "1";
        private string remark;
        private string isPause;
        private string isItemCategory;
        
        
        
  
        //private IList<ItemPriceInfo> priceList = new List<ItemPriceInfo>();
        /// <summary>
        /// Id【保存必须】
        /// </summary>
        public string Item_Id
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// 商品编码【保存必须】
        /// </summary>
        public string Item_Code
        {
            get { return code; }
            set { code = value; }
        }
        /// <summary>
        /// 商品分类(Id)【保存必须】
        /// </summary>
        public string Item_Category_Id
        {
            get { return typeId; }
            set { typeId = value; }
        }
        /// <summary>
        /// 商品分类(编码)
        /// </summary>
        public string Item_Category_Code
        {
            get { return typeCode; }
            set { typeCode = value; }
        }
        /// <summary>
        /// 商品分类(描述)
        /// </summary>
        public string Item_Category_Name
        {
            get { return typeDescription; }
            set { typeDescription = value; }
        }
        /// <summary>
        /// 商品中文名称【保存必须】
        /// </summary>
        public string Item_Name
        {
            get { return name; }
            set { name = value; }
        }
        /// <summary>
        /// 商品英文名称
        /// </summary>
        public string Item_Name_En
        {
            get { return englishName; }
            set { englishName = value; }
        }
        /// <summary>
        /// 商品简称
        /// </summary>
        public string Item_Name_Short
        {
            get { return shortName; }
            set { shortName = value; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Item_Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        /// <summary>
        /// 状态(1:有效)
        /// </summary>
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        /// <summary>
        /// 是否过期
        /// </summary>
        public string IsPause
        {
            get { return isPause; }
            set { isPause = value; }
        }
        /// <summary>
        /// 物资类别是否过期
        /// </summary>
        public string IsItemCategory
        {
            get { return  isItemCategory;}
            set { isItemCategory=value ;}
        
        }
        /// <summary>
        /// 状态描述
        /// </summary>
        public string Status_Desc { get; set; }
        /// <summary>
        /// 是否是商品(1:是,0:分类)
        /// </summary>
        public int IsSku
        {
            get { return isSku; }
            set { isSku = value; }
        }
        /// <summary>
        /// 是否是混合商品(1:是)
        /// </summary>
        public int IsBom
        {
            get { return isBom; }
            set { isBom = value; }
        }
        /// <summary>
        /// 条码
        /// </summary>
        public string Barcode
        {
            get { return barcode; }
            set { barcode = value; }
        }
        //商品的价格列表
        //public IList<ItemPriceInfo> PriceList
        //{
        //    get { return priceList; } 
        //    set { priceList = value; }
        //}

        /// <summary>
        /// 下拉树中的显示名称
        /// </summary>
        public string DisplayName
        {
            get { return code + " - " + shortName; }
        }

        private string createTime;
        /// <summary>
        /// CreateTime
        /// </summary>
        public string Create_Time
        {
            get { return createTime; }
            set { createTime = value; }
        }

        private string createUserId;
        /// <summary>
        /// CreateUserId
        /// </summary>
        public string Create_User_Id
        {
            get { return createUserId; }
            set { createUserId = value; }
        }

        private string modifyTime;
        /// <summary>
        /// ModifyTime
        /// </summary>
        public string Modify_Time
        {
            get { return modifyTime; }    //string.IsNullOrEmpty(modifyTime) ? "" : Convert.ToDateTime(modifyTime).ToShortDateString();
            set { modifyTime = value; }
        }

        private string modifyUserId;
        /// <summary>
        /// ModifyUserId
        /// </summary>
        public string Modify_User_Id
        {
            get { return modifyUserId; }
            set { modifyUserId = value; }
        }
        /// <summary>
        /// 拼音助记码
        /// </summary>
        public string Pyzjm { get; set; }
        /// <summary>
        /// 行号
        /// </summary>
        public int Row_No { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int ICount { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Create_User_Name { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string Modify_User_Name { get; set; }

        /// <summary>
        /// 商品与属性关系集合
        /// </summary>
        public IList<ItemPropInfo> ItemPropList { get; set; }
        /// <summary>
        /// 商品sku名 (基础数据)
        /// </summary>
       // public T_ItemSkuPropEntity T_ItemSkuProp { get; set; }
        public T_ItemSkuPropInfo T_ItemSkuProp { get; set; }
        /// <summary>
        /// 商品价格集合
        /// </summary>
        public IList<ItemPriceInfo> ItemPriceList { get; set; }
        /// <summary>
        /// 商品图片集合
        /// </summary>
        public IList<ObjectImagesEntity> ItemImageList { get; set; }
        /// <summary>
        /// 商品门店集合
        /// </summary>
        public IList<ItemStoreMappingEntity> ItemUnitList { get; set; }
        /// <summary>
        /// sku集合
        /// </summary>
        public IList<SkuInfo> SkuList { get; set; }
        /// <summary>
        /// 促销分组集合
        /// </summary>
        public IList<ItemCategoryMappingEntity> SalesPromotionList { get; set; }
        /// <summary>
        /// 商品集合
        /// </summary>
        public IList<ItemInfo> ItemInfoList { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string bat_id { get; set; }
        /// <summary>
        /// 是否赠品
        /// </summary>
        public int ifgifts	{get;set;}
        /// <summary>
        /// 是否常用商品
        /// </summary>
        public int ifoften	{ get; set; }
        /// <summary>
        /// 是否服务性商品
        /// </summary>
        public int ifservice { get; set; }
        /// <summary>
        /// 非国标商品
        /// </summary>
        public int isGB { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string data_from { get; set; }
        /// <summary>
        /// 显示次序
        /// </summary>
        public int display_index { get; set; }
        /// <summary>
        /// 客户标识
        /// </summary>
        public string customer_id { get; set; }
        /// <summary>
        /// 图片Url
        /// </summary>
        public string Image_Url { get; set; }

        /// <summary>
        /// 积分
        /// </summary>
        public string integration { get; set; }
        public IList<ObjectImagesEntity> SkuImageList { get; set; }

        /// <summary>
        /// 选择的商品标签
        /// </summary>
        public IList<TItemTagEntity> ItemCategoryList { get; set; }
        /// <summary>
        /// 商品分类列表
        /// </summary>
        public IList<ItemCategoryMappingEntity> ItemCategoryMappingList { get; set; }

        public List<WMenuMTextMappingEntity> listMenutextMapping { set; get; }

        /// <summary>
        /// 关系表
        /// </summary>
        public List<WMaterialTextEntity> listMenutext { set; get; }

        public string ReplyType { set; get; }

        public string Text { set; get; }

        public string MaxWQRCod { set; get; }

        public string imageUrl { set; get; }

        /// <summary>
        /// 操作方式  DEL.删除  EDIT.修改 ADD.新增
        /// </summary>
        ///   add by donal 2014-10-11 18:25:31
        public string OperationType { get; set; }
        //新加商品库存、销量、最小价格、促销分组
        public string stock { get; set; }
        public string SalesCount { get; set; }
        public decimal minPrice { get; set; }
        public string SalesPromotion { get; set; }
        /// <summary>
        /// 虚拟商品种类
        /// </summary>
        public string VirtualItemTypeId { get; set; }

        /// <summary>
        /// 优惠券类型ID或者卡类型ID
        /// </summary>
        public string ObjecetTypeId { get; set; }

    }

    public class VwItemDetailEntity
    {
        public string item_id { get; set; }
        public string item_category_id { get; set; }
        public string item_code { get; set; }
        public string item_name { get; set; }
        public string item_name_en { get; set; }
        public string item_name_short { get; set; }
        public string pyzjm { get; set; }
        public string item_remark { get; set; }
        public string status { get; set; }
        public string status_desc { get; set; }
        public string create_user_id { get; set; }
        public string create_time { get; set; }
        public string modify_user_id { get; set; }
        public string modify_time { get; set; }
        public string bat_id { get; set; }
        public string if_flag { get; set; }
        public Int32? ifgifts { get; set; }
        public Int32? ifoften { get; set; }
        public Int32? ifservice { get; set; }
        public Int32? IsGB { get; set; }
        public string data_from { get; set; }
        public Int32? display_index { get; set; }
        public Int64? DisplayIndexLast { get; set; }
        public string imageUrl { get; set; }
        public string PTypeId { get; set; }
        public string PTypeCode { get; set; }
        public string Tel { get; set; }
        public string ItemUnit { get; set; }
        public string Qty { get; set; }
        public string BeginTime { get; set; }
        public string EndTime { get; set; }
        public string UseInfo { get; set; }
        public string BuyType { get; set; }
        public string CouponURL { get; set; }
        public string OffersTips { get; set; }
        public string IsOnline { get; set; }
        public decimal? Price { get; set; }
        public decimal? SalesPrice { get; set; }
        public decimal? DiscountRate { get; set; }
        public decimal? Integral { get; set; }
        public Int32? SalesPersonCount { get; set; }
        public Int32? DownloadPersonCount { get; set; }
        public decimal? OverCount { get; set; }
        public string BrandId { get; set; }
        public string IsIAlumniItem { get; set; }
        public string IsExchange { get; set; }
        public string IntegralExchange { get; set; }
        public string CreateTime { get; set; }
        public string CreateBy { get; set; }
        public string LastUpdateTime { get; set; }
        public string LastUpdateBy { get; set; }
        public string IsDelete { get; set; }
        public decimal? MonthSalesCount { get; set; }
        public string itemCategoryName { get; set; }
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
        public int DayCount { get; set; }       
    }


    public class T_ItemSkuPropInfo {
        public String ItemSkuPropID { get; set; }
        public String Item_id { get; set; }
        /// <summary>
        /// 
        public String prop_1_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String prop_2_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String prop_3_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String prop_4_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String prop_5_id { get; set; }
        /// 
        /// </summary>
      

        /// <summary>
        /// 
        /// </summary>
      


        public String prop_1_name { get; set; }
        public String prop_2_name { get; set; }
        public String prop_3_name { get; set; }
        public String prop_4_name { get; set; }
        public String prop_5_name { get; set; }

        public String prop_1_code { get; set; }
        public String prop_2_code { get; set; }
        public String prop_3_code { get; set; }
        public String prop_4_code { get; set; }
        public String prop_5_code { get; set; }

    }
}
