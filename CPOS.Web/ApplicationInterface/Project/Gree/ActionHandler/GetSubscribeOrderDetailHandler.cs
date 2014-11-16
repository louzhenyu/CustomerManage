using System;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Project.Gree.ActionHandler
{
    /// <summary>
    /// 获取预约单详细信息的handle
    /// </summary>
    [Export(typeof(IGreeRequestHandler))]
    [ExportMetadata("Action", "GetSubscribeOrderDetail")]
    public class GetSubscribeOrderDetailHandler : IGreeRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetSubscribeOrderDetail(pRequest);
        }

        /// <summary>
        /// 获取预约单详细信息
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetSubscribeOrderDetail(string pRequest)
        {
            var rd = new APIResponse<GetSubscribeOrderDetailRD>();
            var rdData = new GetSubscribeOrderDetailRD();

            var rp = pRequest.DeserializeJSONTo<APIRequest<GetSubscribeOrderDetailRP>>();

            if (rp.Parameters == null)
            {
                throw new ArgumentException();
            }

            rp.Parameters.Validate();
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            rdData.UnOrder = new ServiceOrderDataAccess(loggingSessionInfo).GetSubscribeOrderDetail(rp.CustomerID, rp.Parameters.ServiceOrderNO);
            rd.Data = rdData;
            rd.ResultCode = 0;
            return rd.ToJSON();
        }
    }

    #region 获取预约单详细信息
    public class GetSubscribeOrderDetailRP : IAPIRequestParameter
    {
        public string ServiceOrderNO { set; get; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(ServiceOrderNO))
                throw new APIException("【ServiceOrderNO】不能为空") { ErrorCode = 101 };
        }
    }

    public class GetSubscribeOrderDetailRD : IAPIResponseData
    {
        public SubscribeOrderViewModel UnOrder { set; get; }
    }

    #endregion

}