using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.DataAccess.Base;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Common;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.Login.Request;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Module.VIP.Register.Response;
using JIT.CPOS.DTO.Module.VIP.Register.Request;
using JIT.CPOS.DTO.Module.VIP.Login.Response;
using System.Configuration;
using JIT.Utility.Log;
using JIT.Utility.Web;
using JIT.Utility.DataAccess.Query;
using JIT.Utility;

namespace JIT.CPOS.Web.ApplicationInterface.AllWin
{
    /// <summary>
    /// Sales 的摘要说明
    /// </summary>
    public class Sales : BaseGateway
    {

        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst = "";
            switch (pAction)
            {

                case "GetItemList"://商品列表带分润价格
                    rst = this.GetItemList(pRequest);
                    break;
                case "SaveRetailTraderItemMapping"://保存分销商和商品的关联
                    rst = this.SaveRetailTraderItemMapping(pRequest);
                    break;
                case "GetRetailTraderItemMapping"://获取分销商和商品的关联
                    rst = this.GetRetailTraderItemMapping(pRequest);
                    break;
                case "GetVipCount15Days"://15天集客会员数量变化
                    rst = this.GetVipCount15Days(pRequest);
                    break;
                case "GetRetailTraderEarnings"://分销商销售情况
                    rst = this.GetRetailTraderEarnings(pRequest);
                    break;
                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                    {
                        ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                    };
            }
            return rst;
        }
        /// <summary>
        /// 商品列表带分润价格
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetItemList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetItemListRP>>();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

            var bll = new SysRetailRewardRuleBLL(loggingSessionInfo);
            var rd = new GetItemListRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            rd.ItemList = DataTableToObject.ConvertToList<ItemInfo>(bll.GetItemListWithSharePrice(rp.CustomerID, rp.Parameters.RetailTraderID, rp.Parameters.PageIndex, rp.Parameters.PageSize, rp.Parameters.Sort, rp.Parameters.SortName).Tables[0]).ToArray();
            return rsp.ToJSON();
        }
        #region 保存分销商和商品关联
        public string SaveRetailTraderItemMapping(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<RetailTraderItemMappingRP>>();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            var bll = new RetailTraderItemMappingBLL(loggingSessionInfo);
            var rd = new EmptyRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            bll.DeleteData(rp.Parameters.RetailTraderId);

            foreach (var item in rp.Parameters.ItemList)
            {
                RetailTraderItemMappingEntity entityRTIM = new RetailTraderItemMappingEntity();
                entityRTIM.ItemId = item.ItemId;
                entityRTIM.RetailTraderId = rp.Parameters.RetailTraderId;
                bll.Create(entityRTIM);
            }
            return rsp.ToJSON();
        }
        #endregion

        #region 获取分销商和商品关联
        public string GetRetailTraderItemMapping(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetItemListRP>>();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

            var bll = new SysRetailRewardRuleBLL(loggingSessionInfo);
            var rd = new GetItemListRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            var allList = DataTableToObject.ConvertToList<ItemInfo>(bll.GetItemListWithSharePrice(rp.CustomerID, rp.Parameters.RetailTraderID, rp.Parameters.PageIndex, rp.Parameters.PageSize, rp.Parameters.Sort, rp.Parameters.SortName).Tables[0]).ToArray();
            rd.ItemList = allList.Where(a => a.IsCheck == 1).ToArray();
            return rsp.ToJSON();
        }
        #endregion

        #region 获取分销商的类型

        #endregion

        #region 15天集客会员数量变化
        public string GetVipCount15Days(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GeneralRP>>();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

            var bll = new SysRetailRewardRuleBLL(loggingSessionInfo);
            var rd = new VipCountRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            rd.VipCountList = DataTableToObject.ConvertToList<VipCountInfo>(bll.GetRetailTraderVipCountByDays(rp.Parameters.RetailTraderId, 15).Tables[0]).ToArray();

            var dsMain = bll.GetRetailTraderEarnings(rp.Parameters.RetailTraderId, "All");
            if (dsMain.Tables[0].Rows.Count > 0)
            {
                rd.Bonus = Convert.ToDecimal(dsMain.Tables[0].Rows[0]["Bonus"]);
                rd.SalesMoney = Convert.ToDecimal(dsMain.Tables[0].Rows[0]["SalesMoney"]);
            }
            var dsMonth = bll.GetRetailTraderEarnings(rp.Parameters.RetailTraderId, "Month");
            if (dsMonth.Tables[0].Rows.Count > 0)
            {
                rd.Bonus_Month = Convert.ToDecimal(dsMonth.Tables[0].Rows[0]["Bonus"]);
                rd.SalesMoney_Month = Convert.ToDecimal(dsMonth.Tables[0].Rows[0]["SalesMoney"]);
            }
            return rsp.ToJSON();
        }
        #endregion

        #region 分销商销售额情况
        public string GetRetailTraderEarnings(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<RetailTraderEarningsRP>>();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

            var bll = new SysRetailRewardRuleBLL(loggingSessionInfo);
            var rd = new RetailTraderEarningsRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            var dsMain=bll.GetRetailTraderEarnings(rp.Parameters.RetailTraderId, rp.Parameters.Type);
            if(dsMain.Tables[0].Rows.Count>0)
            {
                rd.Bonus = Convert.ToDecimal(dsMain.Tables[0].Rows[0]["Bonus"]);
                rd.SalesMoney = Convert.ToDecimal(dsMain.Tables[0].Rows[0]["SalesMoney"]);
            }
            if (dsMain.Tables[1].Rows.Count > 0)
            {
                rd.ItemSalesInfoList = DataTableToObject.ConvertToList<ItemSalesInfo>(dsMain.Tables[1]).ToArray();
            }
            
            return rsp.ToJSON();
        }

        #endregion

        public class GeneralRP : IAPIRequestParameter
        {
            public string RetailTraderId { get; set; }
            public void Validate()
            {
            }
        }

        public class RetailTraderItemMappingRP : IAPIRequestParameter
        {
            public string RetailTraderId { get; set; }
            public List<ItemInfo> ItemList { get; set; }
            public void Validate()
            {
            }
        }
         public class RetailTraderItemMappingRD : IAPIResponseData
        {
            public string RetailTraderId { get; set; }
            public RetailTraderItemMappingEntity[] ItemList { get; set; }
        }
        public class GetItemListRP : IAPIRequestParameter
        {
            /// <summary>
            /// 页码
            /// </summary>
            public int PageIndex { get; set; }
            /// <summary>
            /// 每页数量
            /// </summary>
            public int PageSize { get; set; }
            /// <summary>
            /// 排序方式 DESC ,ASC
            /// </summary>
            public string Sort { get; set; }
            /// <summary>
            /// 排序字段
            /// </summary>
            public string SortName { get; set; }
            /// <summary>
            /// 分销商Id
            /// </summary>
            public string RetailTraderID { get; set; }
            public void Validate()
            {
            }
        }
        public class GetItemListRD : IAPIResponseData
        {
            public string RetailTraderID { get; set; }
            public ItemInfo[] ItemList { get; set; }
        }
        public class ItemInfo
        {
            public string ItemId { get; set; }
            public string ItemName { get; set; }
            private string imageurl;
            public string imageUrl
            {
                get { return imageurl; }  //请求图片缩略图 
                set { imageurl = ImagePathUtil.GetImagePathStr(value, "240"); }
            }
            /// <summary>
            /// 销售价格
            /// </summary>
            public decimal SalesPrice { get; set; }
            /// <summary>
            /// 分润价格
            /// </summary>
            public decimal SharePrice { get; set; }
            /// <summary>
            /// 佣金
            /// </summary>
            public decimal Commission { get; set; }
            public int IsCheck { get; set; }
            public int SalesQty { get; set; }
        }
        public class SaveRetailTraderRP : IAPIRequestParameter
        {
            public int? IsNewHeadImg { get; set; }
            public RetailTraderInfo RetailTraderInfo { get; set; }
            public void Validate()
            {
            }
        }

        public class VipCountRD : IAPIResponseData
        {
            public string RetailTraderID { get; set; }
            /// <summary>
            /// 销售额
            /// </summary>
            public decimal SalesMoney { get; set; }
            /// <summary>
            /// 分润
            /// </summary>
            public decimal Bonus { get; set; }
            /// <summary>
            /// 销售额
            /// </summary>
            public decimal SalesMoney_Month { get; set; }
            /// <summary>
            /// 分润
            /// </summary>
            public decimal Bonus_Month { get; set; }
            public VipCountInfo[] VipCountList { get; set; }
        }
        public class VipCountInfo
        {
            public string Date { get; set;}
            public int VipCount { get; set; }
        }

        #region 分销商销售情况
        public class RetailTraderEarningsRP : IAPIRequestParameter
        {
            public string RetailTraderId { get; set; }
            /// <summary>
            /// 计算类型  All:总额，Month:月度 ,Daily:每日
            /// </summary>
            public string Type { get; set; }
            public void Validate()
            {
            }
        }
        public class RetailTraderEarningsRD : IAPIResponseData
        {
            public string RetailTraderID { get; set; }
            /// <summary>
            /// 销售额
            /// </summary>
            public decimal SalesMoney { get; set; }
            /// <summary>
            /// 分润
            /// </summary>
            public decimal Bonus { get; set; }
            public ItemSalesInfo[] ItemSalesInfoList { get; set; }
        }
        public class ItemSalesInfo
        {
            public string ItemName { get; set; }
            public int SalesQty { get; set; }
            /// <summary>
            /// 单价
            /// </summary>
            public decimal SalesPrice { get; set; }
            /// <summary>
            /// 分润
            /// </summary>
            public decimal Bonus { get; set; }
        }
        #endregion
    }
}