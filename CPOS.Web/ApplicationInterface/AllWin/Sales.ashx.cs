using CPOS.Common;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Common;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Order.Order.Request;
using JIT.CPOS.DTO.Module.Order.Order.Response;
using JIT.Utility.ExtensionMethod;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


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
                case "GetRetailTraderSellOrderList"://获取分销商销售订单列表--订单
                    rst = this.GetRetailTraderSellOrderList(pRequest);
                    break;
                case "GetVipCount15Days"://15天集客会员数量变化
                    rst = this.GetVipCount15Days(pRequest);
                    break;
                case "GetRetailTraderEarnings"://分销商销售情况
                    rst = this.GetRetailTraderEarnings(pRequest);
                    break;
                case "GetRetailTraderEarningsDetails"://15天集客会员数量变化
                    rst = this.GetRetailTraderEarningsDetails(pRequest);
                    break;
                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                    {
                        ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                    };
            }
            return rst;
        }

        #region 商品列表带分润价格
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
        #endregion

        #region 保存分销商和商品关联
        public string SaveRetailTraderItemMapping(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<RetailTraderItemMappingRP>>();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            var bll = new RetailTraderItemMappingBLL(loggingSessionInfo);
            var rd = new EmptyRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            var itemService = new ItemService(loggingSessionInfo);
            var bllTrader = new RetailTraderBLL(loggingSessionInfo);
            var entityTrader = bllTrader.GetByID(rp.Parameters.RetailTraderId);
            entityTrader.SalesType = "Sales";
            bllTrader.Update(entityTrader, null, false);


            bll.DeleteData(rp.Parameters.RetailTraderId);
            if (rp.Parameters.ItemList.Count() > 0)
            {
                foreach (var item in rp.Parameters.ItemList)
                {
                    RetailTraderItemMappingEntity entityRTIM = new RetailTraderItemMappingEntity();
                    entityRTIM.ItemId = item.ItemId;
                    entityRTIM.RetailTraderId = rp.Parameters.RetailTraderId;
                    entityRTIM.CustomerID = rp.CustomerID;
                    bll.Create(entityRTIM);
                    itemService.CreateTraderQRCode(rp.CustomerID, item.ItemId, item.ItemName, rp.Parameters.RetailTraderId, entityRTIM.MappingId.ToString());
                }
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
            rd.RetailTraderID = rp.Parameters.RetailTraderID;
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            var allList = DataTableToObject.ConvertToList<ItemInfo>(bll.GetItemListWithSharePrice(rp.CustomerID, rp.Parameters.RetailTraderID, rp.Parameters.PageIndex, rp.Parameters.PageSize, rp.Parameters.Sort, rp.Parameters.SortName).Tables[0]).ToArray();
            rd.ItemList = allList.Where(a => a.IsCheck == 1).ToArray();
            return rsp.ToJSON();
        }
        #endregion

        #region 获取分销商销售订单列表
        /// <summary>
        /// 获取分销商销售订单列表
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string GetRetailTraderSellOrderList(string rp)
        {
            var pRequest = rp.DeserializeJSONTo<APIRequest<GetOrderListByUserIdRP>>();
            GetOrderListByUserIdRD rd = new GetOrderListByUserIdRD();
            var loggingSessionInfo = Default.GetBSLoggingSession(pRequest.CustomerID, "1");
            T_InoutBLL bll = new T_InoutBLL(loggingSessionInfo);
            string retailTraderId = pRequest.Parameters.UserID;
            string customerId = pRequest.CustomerID;
            int pageSize = pRequest.Parameters.PageSize;
            int pageIndex = pRequest.Parameters.PageIndex;
            string isPayment = pRequest.Parameters.IsPayment;
            var orderStatusInfo = pRequest.Parameters.OrderStatus;
            string orderStatusList = "100";

            var ds = bll.GetRetailTraderOrdersList(retailTraderId, orderStatusList, isPayment, customerId, pageSize, pageIndex);

            var tmp = ds.Tables[0].AsEnumerable().Select(t => new OrdersInfo()
            {
                OrderID = Convert.ToString(t["order_id"]),
                OrderNO = Convert.ToString(t["order_no"]),
                DeliveryTypeID = Convert.ToString(t["DeliveryTypeId"]),
                OrderDate = Convert.ToString(t["OrderDate"]),
                VipName = Convert.ToString(t["vipName"]),
                OrderStatusDesc = Convert.ToString(t["OrderStatusDesc"]),
                OrderStatus = Convert.ToInt32(Utils.GetIntVal(t["OrderStatus"].ToString())),
                TotalAmount = Convert.ToDecimal(t["total_amount"]),
                TotalQty = Convert.ToDecimal(t["TotalQty"]),
                RetailTraderName = Convert.ToString(t["RetailTraderName"]),
                ServiceMan = Convert.ToString(t["ServiceMan"]),
                CollectIncome = Convert.ToDecimal(t["CollectIncome"])
            });


            rd.OrderList = tmp.ToArray();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        #endregion

        #region 获取分销商的类型

        #endregion

        #region 15天集客会员数量变化
        public string GetVipCount15Days(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GeneralRP>>();
            var para = rp.Parameters;
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

            var bll = new SysRetailRewardRuleBLL(loggingSessionInfo);
            var rd = new VipCountRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            rd.VipCountList = DataTableToObject.ConvertToList<VipCountInfo>(bll.GetRetailTraderVipCountByDays(rp.Parameters.RetailTraderId, 15).Tables[0]).ToArray();

            var dsMain = bll.GetRetailTraderEarnings(para.RetailTraderId, "All", 0, 0);
            if (dsMain.Tables[0].Rows.Count > 0)
            {
                rd.Bonus = Convert.ToDecimal(dsMain.Tables[0].Rows[0]["Bonus"]);
                rd.SalesMoney = Convert.ToDecimal(dsMain.Tables[0].Rows[0]["SalesMoney"]);
            }
            var dsMonth = bll.GetRetailTraderEarnings(para.RetailTraderId, "Month", 0, 0);
            if (dsMonth.Tables[0].Rows.Count > 0)
            {
                rd.Bonus_Month = Convert.ToDecimal(dsMonth.Tables[0].Rows[0]["Bonus"]);
                rd.SalesMoney_Month = Convert.ToDecimal(dsMonth.Tables[0].Rows[0]["SalesMoney"]);
            }
            var dsDaily = bll.GetRetailTraderEarnings(para.RetailTraderId, "Daily", 0, 0);
            if (dsDaily.Tables[0].Rows.Count > 0)
            {
                rd.Bonus_Day = Convert.ToDecimal(dsDaily.Tables[0].Rows[0]["Bonus"]);
                rd.SalesMoney_Day = Convert.ToDecimal(dsDaily.Tables[0].Rows[0]["SalesMoney"]);
            }

            rd.isHidePayBack = GetCustomerPayBackSetting(rp.CustomerID, loggingSessionInfo);

            return rsp.ToJSON();
        }
        #endregion

        /// <summary>
        /// 获取供应商佣金呈现配置
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        private int GetCustomerPayBackSetting(string customerId, LoggingSessionInfo loggingSessionInfo)
        {
            CustomerBasicSettingBLL bll = new CustomerBasicSettingBLL(loggingSessionInfo);
            var setting = bll.QueryByEntity(new CustomerBasicSettingEntity()
            {
                CustomerID = customerId,
                SettingCode = "IsShowRetailTraderPayBack"
            }, null).FirstOrDefault();

            int rsp = 0;
            if (setting != null)
            {
                rsp = TypeParse.ToInt(setting.SettingValue);
            }
            return rsp;
        }



        #region 分销商销售额情况
        public string GetRetailTraderEarnings(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<RetailTraderEarningsRP>>();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

            var bll = new SysRetailRewardRuleBLL(loggingSessionInfo);
            var rd = new RetailTraderEarningsRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            var dsMain = bll.GetRetailTraderEarnings(rp.Parameters.RetailTraderId, rp.Parameters.Type, rp.Parameters.PageIndex, rp.Parameters.PageSize);
            if (dsMain.Tables[0].Rows.Count > 0)
            {
                rd.Bonus = Convert.ToDecimal(dsMain.Tables[0].Rows[0]["Bonus"]);
                rd.SalesMoney = Convert.ToDecimal(dsMain.Tables[0].Rows[0]["SalesMoney"]);
            }
            if (dsMain.Tables[1].Rows.Count > 0)
            {
                rd.ItemSalesInfoList = DataTableToObject.ConvertToList<ItemSalesInfo>(dsMain.Tables[1]).ToArray();
            }

            rd.isHidePayBack = GetCustomerPayBackSetting(rp.CustomerID, loggingSessionInfo);
            return rsp.ToJSON();
        }

        #endregion

        #region 分销商每日奖励详情
        public string GetRetailTraderEarningsDetails(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GeneralRP>>();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

            var bll = new SysRetailRewardRuleBLL(loggingSessionInfo);
            var rd = new RetailTraderEarningsDetailsRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            var dsMain = bll.GetRetailTraderEarningsDetails(rp.Parameters.RetailTraderId);

            if (dsMain.Tables[0].Rows.Count > 0)
            {
                rd.BonusList = DataTableToObject.ConvertToList<BonusInfo>(dsMain.Tables[0]).ToArray();
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
        #region 分销商保存商品
        public class RetailTraderItemMappingRP : IAPIRequestParameter
        {
            public string RetailTraderId { get; set; }
            public List<ItemInfo> ItemList { get; set; }
            public void Validate()
            {
                if (string.IsNullOrEmpty(RetailTraderId))
                {
                    throw new APIException("RetailTraderId不能为空") { ErrorCode = 103 };
                }
            }
        }
        public class RetailTraderItemMappingRD : IAPIResponseData
        {
            public string RetailTraderId { get; set; }
            public RetailTraderItemMappingEntity[] ItemList { get; set; }
        }
        #endregion
        #region 获取商品列表
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
            public string ifservice { get; set; }

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
        #endregion
        #region 15天集客会员数量变化
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
            /// <summary>
            /// 销售额
            /// </summary>
            public decimal SalesMoney_Day { get; set; }
            /// <summary>
            /// 分润
            /// </summary>
            public decimal Bonus_Day { get; set; }
            public VipCountInfo[] VipCountList { get; set; }

            /// <summary>
            /// 是否呈现佣金：0-呈现，1-不呈现
            /// </summary>
            public int isHidePayBack { get; set; }
        }
        public class VipCountInfo
        {
            public string Date { get; set; }
            public int VipCount { get; set; }
        }

        #endregion
        #region 分销商销售情况
        public class RetailTraderEarningsRP : IAPIRequestParameter
        {
            public string RetailTraderId { get; set; }
            /// <summary>
            /// 计算类型  All:总额，Month:月度 ,Daily:每日
            /// </summary>
            public string Type { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public void Validate()
            {
            }
        }
        public class RetailTraderEarningsRD : IAPIResponseData
        {
            /// <summary>
            /// 是否呈现佣金：0-呈现，1-不呈现
            /// </summary>
            public int isHidePayBack { get; set; }
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

            private decimal _salesQty;
            public decimal SalesQty
            {
                get { return Decimal.Truncate(_salesQty); }
                set { this._salesQty = value; }
            }
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
        #region 分销商每日奖励详情
        public class RetailTraderEarningsDetailsRD : IAPIResponseData
        {
            public string RetailTraderID { get; set; }

            public BonusInfo[] BonusList { get; set; }

        }
        public class BonusInfo
        {
            /// <summary>
            /// 销售额
            /// </summary>
            public string BonusType { get; set; }
            /// <summary>
            /// 分润
            /// </summary>
            public decimal Bonus { get; set; }
        }
        #endregion
    }
}