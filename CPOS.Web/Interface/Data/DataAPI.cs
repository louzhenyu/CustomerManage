using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.Web.Interface.Data
{
    public static class DataAPI
    {
        internal static object GetPanicbuyingItemList(Base.APIRequest request)
        {
            var para = request.GetParameters<GetPanicbuyingItemListReqPara>();
            string msg;
            if (!para.IsValid(out msg))
                throw new Exception("参数不完整:" + msg);
            var bll = new vwItemPEventDetailBLL(request.GetUserInfo());
            return bll.GetListByParameters(para);
        }
        /// <summary>
        /// 新版获取活动列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        internal static object GetPanicbuyingEventList(Base.APIRequest request)
        {
            var para = request.GetParameters<GetPanicbuyingItemListReqPara>();
            string msg;
            if (!para.IsValid(out msg))
                throw new Exception("参数不完整:" + msg);
            var bll = new vwItemPEventDetailBLL(request.GetUserInfo());
            return bll.GetPanicbuyingEventList(para);
        }
        internal static object GetPanicbuyingItemDetail(Base.APIRequest request)
        {
            var para = request.GetParameters<GetPanicbuyingItemDetailReqPara>();
            string msg;
            if (!para.IsValid(out msg))
                throw new Exception("参数不完整:" + msg);
            var bll = new vwItemPEventDetailBLL(request.GetUserInfo());
            return bll.GetDetailByParameters(para,request.common.userId);
        }

        internal static object SetOrderInfo(Base.APIRequest request)
        {
            

            var para = request.GetParameters<SetOrderInfoReqPara>();
            string msg;
            if (!para.IsValid(out msg))
            {
                throw new APIException(msg) { ErrorCode=350 };
            }
            if(string.IsNullOrEmpty(para.storeId))
            {
                UnitService unitServer = new UnitService(request.GetUserInfo());
                para.storeId = unitServer.GetUnitByUnitTypeForWX("OnlineShopping", null).Id; //获取在线商城的门店标识
            }
            #region BLL用到common的参数,所以要赋一下值
            para.customerId = request.common.customerId;
            para.userId = request.common.userId;
            para.openId = request.common.openId;
            #endregion
            var bll = new T_InoutBLL(request.GetUserInfo());
            var orderID =bll.SetOrderInfo(para);
            return new { orderId = orderID };
        }
    }
}