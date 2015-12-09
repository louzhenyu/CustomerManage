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
using JIT.CPOS.BS.Web.Base.Excel;
using Aspose.Cells;
using JIT.Utility;
using JIT.CPOS.Common;
using JIT.CPOS.BS.Entity.Interface;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Coupon
{
    /// <summary>
    /// VipGateway 的摘要说明
    /// </summary>
    public class CouponEntry : BaseGateway
    {
        #region

        public string GenerateCoupon(string pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            var rp = pRequest.DeserializeJSONTo<APIRequest<GenerateCouponRP>>();
            rp.Parameters.Validate();

            var rd = new EmptyRD();
            var couponBLL = new CouponBLL(loggingSessionInfo);

            rd = couponBLL.GenerateCoupon(rp.Parameters);

            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }

        public string ManageCouponPagedSearch(string pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var couponBLL = new CouponBLL(loggingSessionInfo);

            var rd = new CouponManagePagedSearchRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<CouponManagePagedSearchRP>>();
            rp.Parameters.Validate();

            rd = couponBLL.ManageCouponPagedSearch(rp.Parameters);
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }

        public string SetCouponCode(string pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var couponBLL = new CouponBLL(loggingSessionInfo);

            var rd = new EmptyRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<SetCouponCodeRP>>();
            rp.Parameters.Validate();

            //couponBLL.SetCouponCode(rp.Parameters);

            //var rsp = new SuccessResponse<IAPIResponseData>(rd);

            var rsp = couponBLL.SetCouponCode(rp.Parameters);

            return rsp.ToJSON();
        }

        public string BindCoupon(string pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var couponBLL = new CouponBLL(loggingSessionInfo);

            var rp = pRequest.DeserializeJSONTo<APIRequest<BindCouponRP>>();
            rp.Parameters.Validate();

            var rsp = couponBLL.BindCoupon(rp.Parameters);

            return rsp.ToJSON();
        }

        public string BindCouponLog(string pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var couponBLL = new CouponBLL(loggingSessionInfo);

            var rd = new CouponBindLogPagedSearchRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<CouponBindLogPagedSearchRP>>();
            rp.Parameters.Validate();

            rd = couponBLL.BindCouponLog(rp.Parameters);
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }

        public string GetCouponList(string pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var couponBLL = new CouponBLL(loggingSessionInfo);

            var rd = new CouponManagePagedSearchRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetCouponListRP>>();
            rp.Parameters.Validate();

            rd = couponBLL.GetCouponList(rp.Parameters);
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }

        /// <summary>
        /// 核销优惠券
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string WriteOffCoupon(string pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var couponBLL = new CouponBLL(loggingSessionInfo);

            var rp = pRequest.DeserializeJSONTo<APIRequest<WriteOffCouponRP>>();
            rp.Parameters.Validate();
            
            ////创建虚拟订单
            //string order_no = setOrderInfo();

            ////绑定虚拟订单和优惠券的关系
            //InoutService inoutBll=new InoutService(loggingSessionInfo);
            //string orderID= inoutBll.GetOrderIDByOrderNo(order_no, loggingSessionInfo.CurrentLoggingManager.Customer_Id);

            //TOrderCouponMappingEntity entity = new TOrderCouponMappingEntity();
            //TOrderCouponMappingBLL mappingBLL = new TOrderCouponMappingBLL(loggingSessionInfo);
            //entity.CouponId = rp.Parameters.CouponID;
            //entity.OrderId = orderID;
            //entity.MappingId = BaseService.NewGuidPub(); 
            //mappingBLL.Create(entity);



            //核销优惠券
            var rsp = couponBLL.WriteOffCoupon(rp.Parameters);

            return rsp.ToJSON();
        }

        /// <summary>
        /// 核销优惠券列表
        /// Create By: Sun Xu
        /// Create Date:2015-11-02
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string WriteOffCouponList(string pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var couponBLL = new CouponBLL(loggingSessionInfo);

            var rq = pRequest.DeserializeJSONTo<APIRequest<WriteOffCouponListRP>>();
            rq.Parameters.Validate();
            
            ErrorResponse er = new ErrorResponse();
           
            try
            {
                SuccessResponse<IAPIResponseData> rp = null;

                if (!string.IsNullOrWhiteSpace(rq.Parameters.CouponCode))
                {
                    var couponEntity = couponBLL.QueryByEntity(new CouponEntity()
                    {
                        CouponCode = rq.Parameters.CouponCode
                    }, null);

                    if (couponEntity == null || couponEntity.Length == 0)
                    {
                        rp = new SuccessResponse<IAPIResponseData>();
                        rp.ResultCode = 103;
                        rp.Message = "优惠券无效";
                        return rp.ToJSON();
                    }
                }
                var rd = new WriteOffCouponListRD();
                rd = couponBLL.WriteOffCouponList(rq.Parameters.Mobile, rq.Parameters.CouponCode, rq.Parameters.Status, rq.Parameters.PageSize, rq.Parameters.PageIndex);
                rp = new SuccessResponse<IAPIResponseData>(rd);
                return rp.ToJSON();                                
            }
            catch (Exception ex) {
                er.Message = ex.Message;
                return er.ToJSON();
            }

        }

        /// <summary>
        /// 更改优惠券状态
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string SetCouponStates(string pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var couponBLL = new CouponBLL(loggingSessionInfo);

            var rd = new EmptyRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<SetCouponCodeRP>>();
            //rp.Parameters.Validate();
            var rsp = couponBLL.SetCouponStates(rp.Parameters);

            return rsp.ToJSON();
        }

        #region 核销优惠券时插入虚拟订单

        public string setOrderInfo()
        {
            //string content = string.Empty;
            var respData = new setOrderInfoRespData();
            //try
            //{
                string reqContent = "";

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setOrderInfo: {0}", reqContent)
                });

                #region //解析请求字符串 chech

                //var reqObj = reqContent.DeserializeJSONTo<setOrderInfoReqData>();
                //reqObj = reqObj == null ? new setOrderInfoReqData() : reqObj;
                //if (reqObj.special == null)
                //{
                //    reqObj.special = new setOrderInfoReqSpecialData();
                //}
                //if (reqObj.special == null)
                //{
                //    respData.code = "102";
                //    respData.description = "没有特殊参数";
                //    return respData.ToJSON().ToString();
                //}

                //if (reqObj.special.skuId == null || reqObj.special.skuId.Equals(""))
                //{
                //    respData.code = "2201";
                //    respData.description = "skuId不能为空";
                //    return respData.ToJSON().ToString();
                //}

                //if (reqObj.special.qty == null || reqObj.special.qty.Equals(""))
                //{
                //    respData.code = "2202";
                //    respData.description = "qty购买的数量不能为空";
                //    return respData.ToJSON().ToString();
                //}

                //if (reqObj.special.salesPrice == null || reqObj.special.salesPrice.Equals(""))
                //{
                //    respData.code = "2203";
                //    respData.description = "salesPrice销售价格不能为空";
                //    return respData.ToJSON().ToString();
                //}

                //if (reqObj.special.stdPrice == null || reqObj.special.stdPrice.Equals(""))
                //{
                //    respData.code = "2204";
                //    respData.description = "stdPrice原价格不能为空";
                //    return respData.ToJSON().ToString();
                //}
                //if (reqObj.special.deliveryId == null || reqObj.special.deliveryId.Equals(""))
                //{
                //    respData.code = "2205";
                //    respData.description = "deliveryId提货方式不能为空";
                //    return respData.ToJSON().ToString();
                //}

                //if (reqObj.common.userId == null || reqObj.common.userId.Equals(""))
                //{
                //    respData.code = "2206";
                //    respData.description = "userId不能为空";
                //    return respData.ToJSON().ToString();
                //}

                #endregion

                #region //判断客户ID是否传递

                //if (!string.IsNullOrEmpty(reqObj.common.customerId))
                //{
                //    customerId = reqObj.common.customerId;
                //}
                var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

                #endregion

                #region 设置参数

                InoutService service = new InoutService(loggingSessionInfo);
                SetOrderEntity orderInfo = new SetOrderEntity();
                orderInfo.SkuId = "1";
                orderInfo.TotalQty = 1;
                //if (reqObj.special.storeId == null || reqObj.special.storeId.Trim().Equals(""))
                //{
                //    UnitService unitServer = new UnitService(loggingSessionInfo);
                //    orderInfo.StoreId = unitServer.GetUnitByUnitType("OnlineShopping", null).Id; //获取在线商城的门店标识
                //}
                //else
                //{
                //    orderInfo.StoreId = ToStr(reqObj.special.storeId);
                //}
                orderInfo.StoreId = "1";
                orderInfo.SalesPrice = 0;
                orderInfo.StdPrice = -1;
                orderInfo.TotalAmount = 0;
                orderInfo.Mobile = "";
                orderInfo.Email = "";
                orderInfo.Remark = "";
                orderInfo.CreateBy = "";
                orderInfo.LastUpdateBy = "";
                orderInfo.DeliveryId = "";
                orderInfo.DeliveryTime = "";
                orderInfo.DeliveryAddress = "";
                orderInfo.CustomerId = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
                orderInfo.OpenId = "";
                orderInfo.username = "";
                if (orderInfo.OrderId == null || orderInfo.OrderId.Equals(""))
                {
                    orderInfo.OrderId = BaseService.NewGuidPub();
                    orderInfo.OrderDate = System.DateTime.Now.ToString("yyyy-MM-dd");
                    orderInfo.Status = "100";
                    orderInfo.StatusDesc = "待审核";
                    orderInfo.DiscountRate = Convert.ToDecimal((orderInfo.SalesPrice / orderInfo.StdPrice) * 100);
                    //获取订单号
                    TUnitExpandBLL serviceUnitExpand = new TUnitExpandBLL(loggingSessionInfo);
                    orderInfo.OrderCode = serviceUnitExpand.GetUnitOrderNo();
                }

                #endregion

                string strError = string.Empty;
                string strMsg = string.Empty;

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setOrderInfo: {0}", "开始保存")
                });

                bool bReturn = service.SetOrderOnlineShoppingCoupon(orderInfo, out strError, out strMsg);

                #region 返回信息设置

                //respData.content = new setOrderInfoRespContentData();
                //respData.content.orderId = orderInfo.OrderId;
                //respData.exception = strError;
                //respData.description = strMsg;
                //if (!bReturn)
                //{
                //    respData.code = "111";
                //}

                #endregion
            //}

            //content = respData.ToJSON();
            //return content;

                return orderInfo.OrderCode;
        }

        #endregion

        #region 导出使用记录功能 2014-10-9

        public string exportExcel(string pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var couponBLL = new CouponBLL(loggingSessionInfo);

            var rd = new CouponManagePagedSearchRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<CouponManagePagedSearchRP>>();
            rp.Parameters.Validate();           

            string fileName = "";

            DataTable dataTable = couponBLL.GetExportData(rp.Parameters);
            //var rsp = new SuccessResponse<IAPIResponseData>(rd);
            

            fileName = Utils.DataTableToExcel(dataTable, "list", "使用记录", "post");

            return fileName;
        }

        #endregion

        #region 导出分发记录功能 2014-10-9

        public string exportBindExcel(string pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var couponBLL = new CouponBLL(loggingSessionInfo);

            var rd = new CouponBindLogPagedSearchRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<CouponBindLogPagedSearchRP>>();
            rp.Parameters.Validate();

            string fileName = "";

            DataTable dataTable = couponBLL.GetExportBindData(rp.Parameters);
            //var rsp = new SuccessResponse<IAPIResponseData>(rd);


            fileName = Utils.DataTableToExcel(dataTable, "list", "分发记录", "post");

            return fileName;
        }

        #endregion

        #endregion

        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst;
            switch (pAction)
            {
                case "GenerateCoupon":
                    rst = this.GenerateCoupon(pRequest);
                    break;
                case "ManageCouponPagedSearch":
                    rst = this.ManageCouponPagedSearch(pRequest);
                    break;
                case "SetCouponCode":
                    rst = this.SetCouponCode(pRequest);
                    break;
                case "BindCoupon":
                    rst = this.BindCoupon(pRequest);
                    break;
                case "BindCouponLog":
                    rst = this.BindCouponLog(pRequest);
                    break;
                case "GetCouponList":
                    rst = this.GetCouponList(pRequest);
                    break;
                case "WriteOffCoupon":
                    rst = this.WriteOffCoupon(pRequest);
                    break;
                case "SetCouponStates":
                    rst = this.SetCouponStates(pRequest);
                    break;
                case "exportExcel":
                    rst = this.exportExcel(pRequest);
                    break;
                case "exportBindExcel":
                    rst = this.exportBindExcel(pRequest);
                    break; 
                case "WriteOffCouponList":
                    rst = this.WriteOffCouponList(pRequest);
                    break;
                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                    {
                        ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                    };
            }
            return rst;
        }


    }
}