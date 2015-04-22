using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using JIT.CPOS.BS.Entity.Pos;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 单位
    /// </summary>
    [Serializable]
    [XmlRootAttribute("data")]
    public class UnitInfo
    {
        /// <summary>
        /// 销售组织
        /// </summary>
        public const string FLAG_SALE_ORGANIZATION = "U";
        /// <summary>
        /// 明细客户
        /// </summary>
        public const string FLAG_CUSTOMER_I = "I";
        /// <summary>
        /// 汇总客户
        /// </summary>
        public const string FLAG_CUSTOMER_S = "S";
        /// <summary>
        /// 被汇总客户
        /// </summary>
        public const string FLAG_CUSTOMER_M = "M";

        private string id;
        private string typeId;
        private string code;
        private string name;
        private string englishName;
        private string shortName;
        private string cityId;
        private string cityCode;
        private string address;
        private string contact;
        private string postcode;
        private string telephone;
        private string fax;
        private string email;
        private string remark;
        private string status = "1";
        private string flag = "U";
        private int customerLevel = 0;

        private IList<UnitInfo> subUnitList = new List<UnitInfo>();
        private IList<UnitPropertyInfo> propertyList = new List<UnitPropertyInfo>();

        private string cityName;
        private string typeName;
        private string flagName;
        private string pathName;
        private string provinceName;
        private string stockCode;
        /// <summary>
        /// 路径(查询时用)
        /// </summary>
        [XmlIgnore()]
        public string PathName
        {
            get { return pathName; }
            set { pathName = value; }
        }
        private string collectUnit;
        /// <summary>
        /// 汇总企业(查询时用)
        /// </summary>
        [XmlIgnore()]
        public string CollectUnit
        {
            get { return collectUnit; }
            set { collectUnit = value; }
        }
        private string channelName;
        /// <summary>
        /// 渠道(查询时用)
        /// </summary>
        [XmlIgnore()]
        public string ChannelName
        {
            get { return channelName; }
            set { channelName = value; }
        }
        private string channelLevel;
        /// <summary>
        /// 渠道级别(查询时用)
        /// </summary>
        [XmlIgnore()]
        public string ChannelLevel
        {
            get { return channelLevel; }
            set { channelLevel = value; }
        }
        private string _ka;
        /// <summary>
        /// 关联企业(查询时用)
        /// </summary>
        [XmlIgnore()]
        public string KAUnit
        {
            get { return _ka; }
            set { _ka = value; }
        }
        private string _kaBranch;
        /// <summary>
        /// 关联KA分公司(查询时用)
        /// </summary>
        [XmlIgnore()]
        public string KABranch
        {
            get { return _kaBranch; }
            set { _kaBranch = value; }
        }
        private string _saleUnit;
        /// <summary>
        /// 销售单位(查询时用)
        /// </summary>
        [XmlIgnore()]
        public string SaleUnit
        {
            get { return _saleUnit; }
            set { _saleUnit = value; }
        }
        /// <summary>
        /// 门店标识
        /// </summary>
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// 单位的类型的Id
        /// </summary>
        [XmlIgnore()]
        public string TypeId
        {
            get { return typeId; }
            set { typeId = value; }
        }
        /// <summary>
        /// 单位的类型的名称(查询时用)
        /// </summary>
        [XmlIgnore()]
        public string TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }
        /// <summary>
        /// 编码
        /// </summary>
        [XmlElement("unit_code")]
        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        /// <summary>
        /// 单位名称
        /// </summary>
        [XmlElement("unit_name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        /// <summary>
        /// 英文名称
        /// </summary>
        [XmlElement("unit_name_en")]
        public string EnglishName
        {
            get { return englishName; }
            set { englishName = value; }
        }
        /// <summary>
        /// 简称
        /// </summary>
        [XmlElement("unit_name_short")]
        public string ShortName
        {
            get { return shortName; }
            set { shortName = value; }
        }
        /// <summary>
        /// 城市Id
        /// </summary>
        [XmlIgnore()]
        public string CityId
        {
            get { return cityId; }
            set { cityId = value; }
        }
        /// <summary>
        /// 县级市名称(查询时用)
        /// </summary>
        [XmlElement("unit_city")]
        public string CityName
        {
            get { return cityName; }
            set { cityName = value; }
        }
        /// <summary>
        /// 城市Code
        /// </summary>
        [XmlIgnore()]
        public string CityCode
        {
            get { return cityCode; }
            set { cityCode = value; }
        }
        /// <summary>
        /// 省名称(查询时用)
        /// </summary>
        [XmlElement("unit_province")]
        public string ProvinceName
        {
            get { return provinceName; }
            set { provinceName = value; }
        }

        private string stateName;
        /// <summary>
        /// 地级市名称（查询时用）
        /// </summary>
        [XmlElement("unit_country")]
        public string StateName
        {
            get { return stateName; }
            set { stateName = value; }
        }
        /// <summary>
        /// 邮箱
        /// </summary>
        [XmlElement("unit_email")]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        /// <summary>
        /// 电话
        /// </summary>
        [XmlElement("unit_tel")]
        public string Telephone
        {
            get { return telephone; }
            set { telephone = value; }
        }
        /// <summary>
        /// 传真
        /// </summary>
        [XmlElement("unit_fax")]
        public string Fax
        {
            get { return fax; }
            set { fax = value; }
        }
        /// <summary>
        /// 地址
        /// </summary>
        [XmlElement("unit_address")]
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        /// <summary>
        /// 邮编
        /// </summary>
        [XmlElement("unit_post_code")]
        public string Postcode
        {
            get { return postcode; }
            set { postcode = value; }
        }
        /// <summary>
        /// 联系人
        /// </summary>
        [XmlElement("unit_contact")]
        public string Contact
        {
            get { return contact; }
            set { contact = value; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        [XmlElement("unit_remark")]
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        /// <summary>
        /// 客户级别/渠道层次(0:非客户/销售组织,1:一级客户,2:二级客户,3:三级客户)
        /// </summary>
        [XmlIgnore()]
        public int CustomerLevel
        {
            get { return customerLevel; }
            set { customerLevel = value; }
        }
        /// <summary>
        /// 单位标志(U:非客户/销售组织,I:明细客户,M:被汇总客户,S:汇总客户)
        /// </summary>
        [XmlIgnore()]
        public string Flag
        {
            get { return flag; }
            set
            {
                flag = value;
                if (string.IsNullOrEmpty(value))
                    flagName = "";
                else
                {
                    switch (value.ToUpper())
                    {
                        case FLAG_CUSTOMER_I:
                            flagName = "明细";
                            break;
                        case FLAG_CUSTOMER_M:
                            flagName = "被汇总";
                            break;
                        case FLAG_CUSTOMER_S:
                            flagName = "汇总";
                            break;
                        default:
                            flagName = "";
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// 标志
        /// </summary>
        [XmlIgnore()]
        public string FlagName
        {
            get { return flagName; }
        }
        /// <summary>
        /// 单位下的子单位列表
        /// </summary>
        [XmlIgnore()]
        public IList<UnitInfo> SubUnitList
        {
            get { return subUnitList; }
            set { subUnitList = value; }
        }
        /// <summary>
        /// 单位的属性列表
        /// </summary>
        [XmlIgnore()]
        public IList<UnitPropertyInfo> PropertyList
        {
            get { return propertyList; }
            set { propertyList = value; }
        }

        /// <summary>
        /// 下拉树中的显示名称
        /// </summary>
        [XmlIgnore()]
        public string DisplayName
        {
            get { return code + " - " + shortName; }
        }
        /// <summary>
        /// 仓库
        /// </summary>
        [XmlIgnore()]
        public string Stock
        {
            get { return stockCode; }
            set { stockCode = value; }
        }

        /// <summary>
        /// 创建人
        /// </summary>
        [XmlIgnore()]
        public string Create_User_Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [XmlIgnore()]
        public string Create_Time { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [XmlIgnore()]
        public string Modify_User_Id { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [XmlIgnore()]
        public string Modify_Time { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        [XmlIgnore()]
        public string Create_User_Name { get; set; }

        /// <summary>
        /// 修改人名称
        /// </summary>
        [XmlIgnore()]
        public string Modify_User_Name { get; set; }

        /// <summary>
        /// 状态描述
        /// </summary>
        [XmlElement("unit_status_desc")]
        public string Status_Desc { get; set; }

        /// <summary>
        /// 总记录树
        /// </summary>
        [XmlIgnore()]
        public int ICount { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int Row_No { get; set; }
        /// <summary>
        /// 门店标识
        /// </summary>
        public string unit_id { get; set; }
        /// <summary>
        /// 父节点标识[保存必须]
        /// </summary>
        [XmlIgnore()]
        public string Parent_Unit_Id { get; set; }
        /// <summary>
        /// 上级名称
        /// </summary>
        [XmlIgnore()]
        public string Parent_Unit_Name { get; set; }
        /// <summary>
        /// 组织集合
        /// </summary>
        [XmlIgnore()]
        public IList<UnitInfo> UnitInfoList { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string bat_id { get; set; }
        /// <summary>
        /// 仓库集合
        /// </summary>
        [XmlIgnore()]
        public IList<WarehouseInfo> WarehouseInfoList { get; set; }
        /// <summary>
        /// 属性集合
        /// </summary>
        [XmlIgnore()]
        public IList<UnitPropertyInfo> UnitPropertyInfoList { get; set; }
        /// <summary>
        /// 客户标识
        /// </summary>
        [XmlIgnore()]
        public string customer_id { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string longitude { get; set; }
        /// <summary>
        /// 维度
        /// </summary>
        public string dimension { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        [XmlElement("imageURL")]
        public string imageURL { get; set; }

        /// <summary>
        /// 图片访问地址
        /// </summary>
        [XmlElement("ftpImagerURL")]
        public string ftpImagerURL { get; set; }

        /// <summary>
        /// webservers访问地址
        /// </summary>
        [XmlElement("webserversURL")]
        public string webserversURL { get; set; }

        /// <summary>
        /// 微信公众平台唯一码
        /// </summary>
        [XmlElement("weiXinId")]
        public string weiXinId { get; set; }

        /// <summary>
        /// 门店 二维码图片地址
        /// </summary>
        [XmlElement("dimensionalCodeURL")]
        public string dimensionalCodeURL { get; set; }

        /// <summary>
        /// 商品图片集合
        /// </summary>
        [XmlIgnore()]
        public IList<ObjectImagesEntity> ItemImageList { get; set; }
        /// <summary>
        /// 微信固定二维码
        /// </summary>
        [XmlIgnore()]
        public string WXCode { get; set; }
        /// <summary>
        /// 微信固定二维码图片链接
        /// </summary>
        [XmlIgnore()]
        public string WXCodeImageUrl { get; set; }
        /// <summary>
        /// 微信固定二维码分类
        /// </summary>
        [XmlIgnore()]
        public string QRCodeTypeId { get; set; }
        /// <summary>
        /// 品牌关系集合
        /// </summary>
        [XmlIgnore()]
        public IList<StoreBrandMappingEntity> ItemBrandList { get; set; }
        /// <summary>
        /// 操作
        /// </summary>
        [XmlIgnore()]
        public string strDo { get; set; }

        [XmlIgnore()]
        public IList<TUnitSortEntity> UnitSortList { get; set; }
        [XmlIgnore()]
        public IList<string> UnitSortIdList { get; set; }
        [XmlIgnore()]
        public IList<TUnitUnitSortMappingEntity> UnitSortMappingList { get; set; }

        public List<WMenuMTextMappingEntity> listMenutextMapping { set; get; }

        /// <summary>
        /// 关系表
        /// </summary>
        public List<WMaterialTextEntity> listMenutext { set; get; }

        public string ReplyType { set; get; }

        public string Text { set; get; }

        public string MaxWQRCod { set; get; }

        public string imageUrl { set; get; }

       
    }
}
