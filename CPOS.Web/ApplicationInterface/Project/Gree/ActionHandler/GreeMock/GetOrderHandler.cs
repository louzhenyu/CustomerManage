using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Project.Gree.ActionHandler.GreeMock
{
    /// <summary>
    /// 获取订单信息的Handler
    /// </summary>
    [Export(typeof(IGreeMockRequestHandler))]
    [ExportMetadata("Action", "GetOrder")]
    public class GetOrderHandler : IGreeMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetOrder(pRequest);
        }

        /// <summary>
        /// 获取订单
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string GetOrder(string pRequest)
        {
            var rd = new APIResponse<GetOrderRD>();
            var rdData = new GetOrderRD();
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<GetOrderRP>>();
                rp.CustomerID = "CustomerID";
                rp.OpenID = "-1";
                //if (rp.Parameters != null)
                //    rp.Parameters.Validate();
                //var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                rp.Parameters.OrderNO = "201407021000";

                InstallDeviceViewModel idvm1 = new InstallDeviceViewModel();
                idvm1.DeviceID = "01";
                idvm1.DeviceName = "格力01";
                idvm1.InstallPosition = 1;
                idvm1.DeviceCount = 1;
                InstallDeviceViewModel idvm2 = new InstallDeviceViewModel();
                idvm2.DeviceID = "02";
                idvm2.DeviceName = "格力02";
                idvm2.InstallPosition = 2;
                idvm2.DeviceCount = 1;
                List<InstallDeviceViewModel> listDvm = new List<InstallDeviceViewModel>();
                listDvm.Add(idvm1);
                listDvm.Add(idvm2);
                rdData.DeviceList = listDvm;

                rdData.CustomerName = "王明";
                rdData.OrderNO = rp.Parameters.OrderNO;
                rdData.ServiceAddress = "上海市静安区延平路121号15楼";

                rd.Data = rdData;
                rd.ResultCode = 0;
            }
            catch (Exception ex)
            {
                rd.Message = ex.Message;
                rd.ResultCode = 101;
            }
            return rd.ToJSON();
        }

    }

    public class GetOrderRP : IAPIRequestParameter
    {
        public string OrderNO { set; get; }

        public void Validate()
        {
            // if (string.IsNullOrEmpty(OrderNO)) throw new APIException("【OrderNO】不能为空");
        }
    }

    public class GetOrderRD : IAPIResponseData
    {
        public string OrderNO { set; get; }
        public string ServiceAddress { set; get; }
        public string CustomerName { set; get; }
        public List<InstallDeviceViewModel> DeviceList { set; get; }
    }
}