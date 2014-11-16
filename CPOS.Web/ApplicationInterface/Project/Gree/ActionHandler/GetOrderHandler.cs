using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Project.Gree.ActionHandler
{
    /// <summary>
    /// 获取订单信息的Handler
    /// </summary>
    [Export(typeof(IGreeRequestHandler))]
    [ExportMetadata("Action", "GetOrder")]
    public class GetOrderHandler : IGreeRequestHandler
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

                if (rp.Parameters == null)
                {
                    throw new ArgumentException();
                }

                rp.Parameters.Validate();
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                #region 获取订单

                GLProductOrderBLL bll = new GLProductOrderBLL(loggingSessionInfo);

                GLProductOrderEntity glpoe = bll.GetProductOrderByOrderNo(rp.CustomerID, rp.Parameters.OrderNO);
                rdData.CustomerName = glpoe.CustomerName;
                rdData.ServiceAddress = glpoe.CustomerAddress;
                rdData.OrderNO = glpoe.ProductOrderSN;

                //var gldiibll = new GLDeviceInstallItemBLL(loggingSessionInfo);
                //DataSet ds = gldiibll.GetDeviceInstallItemByOrderNo(rp.CustomerID, rp.Parameters.OrderNO);
                //if (ds.Tables[0] != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                //{
                //    rdData.CustomerName = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                //    rdData.ServiceAddress = ds.Tables[0].Rows[0]["ServiceAddress"].ToString();
                //    rdData.OrderNO = ds.Tables[0].Rows[0]["ProductOrderSN"].ToString();

                //    rdData.DeviceList = DataTableToObject.ConvertToList<InstallDeviceViewModel>(ds.Tables[0]);
                //}
                #endregion
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

    #region 获取订单
    public class GetOrderRP : IAPIRequestParameter
    {
        public string OrderNO { set; get; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(OrderNO)) throw new APIException("【OrderNO】不能为空");
        }
    }

    public class GetOrderRD : IAPIResponseData
    {
        public string OrderNO { set; get; }
        public string ServiceAddress { set; get; }
        public string CustomerName { set; get; }
        public List<InstallDeviceViewModel> DeviceList { set; get; }
    }

    /// <summary>
    /// 安装设备信息
    /// </summary>
    public struct InstallDeviceViewModel
    {
        public string DeviceID { set; get; }
        public string DeviceName { set; get; }
        public int? InstallPosition { set; get; }
        public int? DeviceCount { set; get; }
    }
    #endregion
}