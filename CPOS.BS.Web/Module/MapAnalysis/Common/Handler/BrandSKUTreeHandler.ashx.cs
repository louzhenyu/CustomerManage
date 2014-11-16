using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JIT.Utility.Web;
using JIT.Utility.Web.ComponentModel;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.Common;
using System.Net;
using System.IO;
using System.Text;

namespace JIT.CPOS.BS.Web.Module.MapAnalysis.Common.Handler
{
    /// <summary>
    /// 品类、产品处理
    /// </summary>
    public class BrandSKUTreeHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        protected override void AjaxRequest(HttpContext pContext)
        {
            string op = pContext.Request.QueryString["op"];
            if (!string.IsNullOrWhiteSpace(op))
            {
                switch (op)
                {
                    case "1":
                        var treeNodes = new TreeNodes();

                        var treeNodeJson = treeNodes.ToTreeStoreJSON(false, false, GetNodes().ToArray());
                        this.ResponseContent(treeNodeJson);
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }
        /// <summary>
        /// 获取子节点数据
        /// </summary>
        /// <returns></returns>
        protected List<TreeNode> GetNodes()
        {
            //获取所有品类
            //BrandBLL brandBll = new BrandBLL(this.CurrentUserInfo);
            BrandEntity brandQueryEntity = new BrandEntity();
            brandQueryEntity.IsLeaf = -1;
            //var brandList = brandBll.QueryByEntity(brandQueryEntity, null);
            var brandList = GetBrandList(brandQueryEntity);

            //获取所有产品
            //SKUBLL skuBll = new SKUBLL(this.CurrentUserInfo, true);
            SKUEntity skuQueryEntity = new SKUEntity();
            skuQueryEntity.IsMain = -1;
            //var skuList = skuBll.QueryByEntity(skuQueryEntity, null);
            var skuList = GetSKUList(skuQueryEntity);

            //组织数据
            List<TreeNode> nodes = new List<TreeNode>();
            Dictionary<int, List<SKUEntity>> dictBrandSKU = new Dictionary<int, List<SKUEntity>>();
            Dictionary<int, string> dictBrandName = new Dictionary<int, string>();
            foreach (var brandItem in brandList)
            {
                dictBrandSKU.Add(brandItem.BrandID.Value, new List<SKUEntity>());
                dictBrandName.Add(brandItem.BrandID.Value, brandItem.BrandName);
            }
            //按品类组织产品
            foreach (var skuItem in skuList)
            {
                if (skuItem.BrandID.HasValue && dictBrandSKU.ContainsKey(skuItem.BrandID.Value))
                {
                    dictBrandSKU[skuItem.BrandID.Value].Add(skuItem);
                }
            }
            //先添加父级节点（品类）
            foreach (var brandItem in dictBrandSKU.Keys)
            {
                string brandNodeID = "Brand" + brandItem;
                if (dictBrandSKU[brandItem].Count > 0)
                {//有子节点
                    nodes.Add(new TreeNode(){ ID = brandNodeID, Text = dictBrandName[brandItem], ParentID= null,  IsLeaf= false});
                }
                else
                {//无子节点
                    nodes.Add(new TreeNode() { ID = brandNodeID, Text = dictBrandName[brandItem], ParentID = null, IsLeaf = true });
                }
                //添加子节点（产品）
                foreach (var skuItem in dictBrandSKU[brandItem])
                {
                    nodes.Add(new TreeNode() { ID = "SKU" + skuItem.SKUID.Value, Text = skuItem.SKUName, ParentID = brandNodeID, IsLeaf = true });
                }
            }
            return nodes;
        }

        //protected override TenantPlatformUserInfo CurrentUserInfo
        //{
        //    get
        //    {
        //        var userInfo  = new SessionManager().CurrentUserLoginInfo;
        //        userInfo.ClientID = "27";
        //        return userInfo;
        //    }
        //}

        protected override void Authenticate()
        {
        }

        public static string SendHttpRequest(string requestURI, string requestMethod, string json)
        {
            //json格式请求数据
            string requestData = json;
            //拼接URL
            string serviceUrl = string.Format("{0}/{1}", requestURI, requestMethod);
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            //utf-8编码
            byte[] buf = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(requestData);

            //post请求 
            myRequest.Method = "POST";
            myRequest.ContentLength = buf.Length;
            //指定为json否则会出错
            myRequest.Accept = "application/json";
            myRequest.ContentType = "application/json";

            //Content-type: application/json; charset=utf-8

            //myRequest.ContentType = "text/json";
            myRequest.MaximumAutomaticRedirections = 1;
            myRequest.AllowAutoRedirect = true;

            Stream newStream = myRequest.GetRequestStream();
            newStream.Write(buf, 0, buf.Length);
            newStream.Close();

            //获得接口返回值，格式为： {"VerifyResult":"aksjdfkasdf"} 
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string ReqResult = reader.ReadToEnd();
            reader.Close();
            myResponse.Close();
            return ReqResult;
        }

        public static IList<BrandEntity> GetBrandList(BrandEntity brandQueryEntity)
        {
            var list = new List<BrandEntity>();
            string url = "http://121.199.42.125:3721/Handler.ashx?action=BrandQueryByEntity&pQueryEntity=" +
                brandQueryEntity.ToJSON();
            var response = Utils.GetRemoteData(url, "GET", "");
            if (response != null && response.Length > 0)
                list = response.DeserializeJSONTo<List<BrandEntity>>();
            return list;
        }
        public static IList<SKUEntity> GetSKUList(SKUEntity skuQueryEntity)
        {
            var list = new List<SKUEntity>();
            string url = "http://121.199.42.125:3721/Handler.ashx?action=SKUQueryByEntity&pQueryEntity=" +
                skuQueryEntity.ToJSON();
            var response = Utils.GetRemoteData(url, "GET", "");
            if (response != null && response.Length > 0)
                list = response.DeserializeJSONTo<List<SKUEntity>>();
            return list;
        }
        public static StoreEntity GetStoreByID(string storeId)
        {
            var item = new StoreEntity();
            string url = "http://121.199.42.125:3721/Handler.ashx?action=StoreGetByID&pID=" +
                storeId;
            var response = Utils.GetRemoteData(url, "GET", "");
            if (response != null && response.Length > 0)
                item = response.DeserializeJSONTo<StoreEntity>();
            return item;
        }
        public static IList<CategoryEntity> GetCategoryList(CategoryEntity categoryQueryEntity)
        {
            var list = new List<CategoryEntity>();
            string url = "http://121.199.42.125:3721/Handler.ashx?action=CategoryQueryByEntity&pQueryEntity=" +
                categoryQueryEntity.ToJSON();
            var response = Utils.GetRemoteData(url, "GET", "");
            if (response != null && response.Length > 0)
                list = response.DeserializeJSONTo<List<CategoryEntity>>();
            return list;
        }
        public static IList<ControlChannelEntity> GetChannelByClientID(string key, int key2)
        {
            var list = new List<ControlChannelEntity>();
            string url = "http://121.199.42.125:3721/Handler.ashx?action=ControlGetChannelByClientID&pParentID=" +
                key + "&pIsLeaf=" + key2;
            var response = Utils.GetRemoteData(url, "GET", "");
            if (response != null && response.Length > 0)
                list = response.DeserializeJSONTo<List<ControlChannelEntity>>();
            return list;
        }
        public static IList<ControlChainEntity> GetChainByClientID(string key, int key2)
        {
            var list = new List<ControlChainEntity>();
            string url = "http://121.199.42.125:3721/Handler.ashx?action=ControlGetChainByClientID&pParentID=" +
                key + "&pIsLeaf=" + key2;
            var response = Utils.GetRemoteData(url, "GET", "");
            if (response != null && response.Length > 0)
                list = response.DeserializeJSONTo<List<ControlChainEntity>>();
            return list;
        }
        public static ChannelEntity GetChannelByID(string channelId)
        {
            var item = new ChannelEntity();
            string url = "http://121.199.42.125:3721/Handler.ashx?action=ChanelGetByID&pID=" +
                channelId;
            var response = Utils.GetRemoteData(url, "GET", "");
            if (response != null && response.Length > 0)
                item = response.DeserializeJSONTo<ChannelEntity>();
            return item;
        }
    }

    #region BrandEntity

    /// <summary>
    /// 实体： 品牌 
    /// </summary>
    public partial class BrandEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public BrandEntity()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 自动编号
        /// </summary>
        public int? BrandID { get; set; }

        /// <summary>
        /// 品牌编号
        /// </summary>
        public string BrandNo { get; set; }

        /// <summary>
        /// 品牌名称
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// 品牌名称(英文)
        /// </summary>
        public string BrandNameEn { get; set; }

        /// <summary>
        /// 是否自有品牌(0-否,1-是)
        /// </summary>
        public int? IsOwner { get; set; }

        /// <summary>
        /// 所属公司
        /// </summary>
        public string Firm { get; set; }

        /// <summary>
        /// 品牌等级
        /// </summary>
        public int? BrandLevel { get; set; }

        /// <summary>
        /// 是否叶子节点(0-否,1-是)
        /// </summary>
        public int? IsLeaf { get; set; }

        /// <summary>
        /// 上级品牌编号
        /// </summary>
        public int? ParentID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ClientID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ClientDistributorID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? CreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? LastUpdateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? IsDelete { get; set; }


        #endregion

    }
    #endregion

    #region SKUEntity
    /// <summary>
    /// 实体： 产品表 
    /// </summary>
    public partial class SKUEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public SKUEntity()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 自动编号
        /// </summary>
        public int? SKUID { get; set; }

        /// <summary>
        /// 产品编码
        /// </summary>
        public string SKUNo { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string SKUName { get; set; }

        /// <summary>
        /// 产品名称(英文)
        /// </summary>
        public string SKUNameEn { get; set; }

        /// <summary>
        /// 产品名称简称
        /// </summary>
        public string SKUNameAbbr { get; set; }

        /// <summary>
        /// 产品条码
        /// </summary>
        public string Barcode { get; set; }

        /// <summary>
        /// 品牌编号
        /// </summary>
        public int? BrandID { get; set; }

        /// <summary>
        /// 类别编号
        /// </summary>
        public int? CategoryID { get; set; }

        /// <summary>
        /// 产品规格
        /// </summary>
        public string Spec { get; set; }

        /// <summary>
        /// 包装规格
        /// </summary>
        public string PackSpec { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        public decimal? Weight { get; set; }

        /// <summary>
        /// 体积
        /// </summary>
        public decimal? Volume { get; set; }

        /// <summary>
        /// 单位编号(关联Unit表)
        /// </summary>
        public int? UnitID { get; set; }

        /// <summary>
        /// 是否主打
        /// </summary>
        public int? IsMain { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 预留
        /// </summary>
        public string Col1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Col2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Col3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Col4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Col5 { get; set; }

        /// <summary>
        /// 客户编号
        /// </summary>
        public int? ClientID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ClientDistributorID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? CreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? LastUpdateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? IsDelete { get; set; }


        #endregion

    }
    #endregion

    #region entity

    /// <summary>
    /// 实体： 渠道 
    /// </summary>
    public partial class ChannelEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public ChannelEntity()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 渠道编号
        /// </summary>
        public Guid? ChannelID { get; set; }

        /// <summary>
        /// 渠道名称
        /// </summary>
        public string ChannelName { get; set; }

        /// <summary>
        /// 英文
        /// </summary>
        public string ChannelNameEn { get; set; }

        /// <summary>
        /// 层级
        /// </summary>
        public int? ChannelLevel { get; set; }

        /// <summary>
        /// 上级编号
        /// </summary>
        public Guid? ParentID { get; set; }

        /// <summary>
        /// 是否叶子节点(0-否,1-是)
        /// </summary>
        public int? IsLeaf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Col1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Col2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Col3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Col4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Col5 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ClientID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ClientDistributorID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? CreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? LastUpdateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? IsDelete { get; set; }


        #endregion

    }

    /// <summary>
    /// 实体： ControlChainEntity 
    /// </summary>
    public class ControlChainEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public ControlChainEntity()
        {
        }
        #endregion
        #region 属性集
        /// <summary>
        /// ChainName
        /// </summary>
        public string ChainName { get; set; }

        /// <summary>
        /// ChainID
        /// </summary>
        public Guid? ChainID { get; set; }

        /// <summary>
        /// ParentID
        /// </summary>
        public Guid? ParentID { get; set; }


        /// <summary>
        /// IsLeaf
        /// </summary>
        public int? IsLeaf { get; set; }
        #endregion
    }

    /// <summary>
    /// 实体： ControlChannelEntity 
    /// </summary>
    public class ControlChannelEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public ControlChannelEntity()
        {
        }
        #endregion
        #region 属性集
        /// <summary>
        /// ChannelID
        /// </summary>
        public Guid? ChannelID { get; set; }

        /// <summary>
        /// ChannelName
        /// </summary>
        public string ChannelName { get; set; }

        /// <summary>
        /// ParentID
        /// </summary>
        public Guid? ParentID { get; set; }


        /// <summary>
        /// IsLeaf
        /// </summary>
        public int? IsLeaf { get; set; }
        #endregion
    }

    /// <summary>
    /// 实体： 品类 
    /// </summary>
    public partial class CategoryEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public CategoryEntity()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 自动编号
        /// </summary>
        public int? CategoryID { get; set; }

        /// <summary>
        /// 品类编号
        /// </summary>
        public string CategoryNo { get; set; }

        /// <summary>
        /// 品类名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 品类名称(英文)
        /// </summary>
        public string CategoryNameEn { get; set; }

        /// <summary>
        /// 品类等级
        /// </summary>
        public int? CategoryLevel { get; set; }

        /// <summary>
        /// 是否叶子节点(0-否,1-是)
        /// </summary>
        public int? IsLeaf { get; set; }

        /// <summary>
        /// 上级品类
        /// </summary>
        public int? ParentID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ClientID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ClientDistributorID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? CreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? LastUpdateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? IsDelete { get; set; }


        #endregion

    }

    /// <summary>
    /// 实体： 门店表 
    /// </summary>
    [Serializable()]
    public partial class StoreEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public StoreEntity()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 终端自动编号
        /// </summary>
        public Guid? StoreID { get; set; }

        /// <summary>
        /// 终端自定义编号
        /// </summary>
        public string StoreNo { get; set; }

        /// <summary>
        /// 终端名称
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// 经纬度
        /// </summary>
        public string Coordinate { get; set; }

        /// <summary>
        /// 原始坐标
        /// </summary>
        public string OldCoordinate { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Addr { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        public string Postcode { get; set; }

        /// <summary>
        /// 门头照
        /// </summary>
        public string BannerPhoto { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public string Manager { get; set; }

        /// <summary>
        /// 渠道(关联ClientStructDefined表)
        /// </summary>
        public Guid? ChannelID { get; set; }

        /// <summary>
        /// 连锁(关联ClientStructDefined表)
        /// </summary>
        public Guid? ChainID { get; set; }

        /// <summary>
        /// 所在省
        /// </summary>
        public int? ProvinceID { get; set; }

        /// <summary>
        /// 所在市
        /// </summary>
        public int? CityID { get; set; }

        /// <summary>
        /// 所在县
        /// </summary>
        public int? DistrictID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid? ClientStructureID { get; set; }

        /// <summary>
        /// 状态(0-未启用,1-启用)
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 账期(天数)
        /// </summary>
        public int? CreditPeriod { get; set; }

        /// <summary>
        /// 信用额度(金额)
        /// </summary>
        public decimal? CreditLine { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 所属仓库
        /// </summary>
        public Guid? WarehouseID { get; set; }

        /// <summary>
        /// 预留
        /// </summary>
        public string Col1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Col2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Col3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Col4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Col5 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Col6 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Col7 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Col8 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Col9 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Col10 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Col11 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Col12 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Col13 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Col14 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Col15 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Col16 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Col17 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Col18 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Col19 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Col20 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? Col21 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? Col22 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? Col23 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? Col24 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? Col25 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ClientID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ClientDistributorID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? CreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? LastUpdateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? IsDelete { get; set; }


        #endregion

    } 
    #endregion
}