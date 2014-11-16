using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Project.Gree.ActionHandler.GreeMock
{
    /// <summary>
    /// 获取所有未定预约单信息的Handle
    /// </summary>
    [Export(typeof(IGreeMockRequestHandler))]
    [ExportMetadata("Action", "GetRunningServiceOrder")]
    public class GetRunningServiceOrderHandler : IGreeMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetRunningServiceOrder(pRequest);
        }

        /// <summary>
        /// 获取所有未定预约单信息
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetRunningServiceOrder(string pRequest)
        {
            var rd = new APIResponse<GetRunningServiceOrderRD>();
            var rdData = new GetRunningServiceOrderRD();

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

            SubscribeOrderViewModel smodel = new SubscribeOrderViewModel();
            smodel.VipID = "82B04CE0C05E4AFF9D2C51743B2E0A08";
            smodel.DeviceList = listDvm;
            smodel.CustomerName = "王明";
            smodel.CustomerPhone = "12345678901";
            smodel.Distance = "100";
            //smodel.Coordinate = "123.222";
            smodel.Latitude = (decimal)23.123;
            smodel.Longitude = (decimal)121.456;
            smodel.InstallOrderDate = DateTime.Now.AddDays(10).Date;
            smodel.ServiceOrderDate = DateTime.Now.Date;
            smodel.ServiceOrderDateEnd = DateTime.Now.AddDays(11).Date;
            smodel.ServiceType = 1;
            smodel.Message = "无";
            smodel.InstallCount = 2;
            smodel.ServiceOrderNO = "0001";
            smodel.ServiceAddress = "上海市静安区延平路";
            List<SubscribeOrderViewModel> slist = new List<SubscribeOrderViewModel>();
            slist.Add(smodel);

            rdData.SubOrderList = slist;
            rd.Data = rdData;
            rd.ResultCode = 0;
            return rd.ToJSON();
        }
    }

    #region 获取所有未定预约单信息
    public class GetRunningServiceOrderRP : IAPIRequestParameter
    {
        public void Validate()
        {
        }
    }

    public class GetRunningServiceOrderRD : IAPIResponseData
    {
        public List<SubscribeOrderViewModel> SubOrderList { set; get; }
    }

    public class SubscribeOrderViewModel
    {
        public string ServiceOrderNO { set; get; }
        public int? ServiceType { set; get; }
        public DateTime? ServiceOrderDate { set; get; }
        public DateTime? ServiceOrderDateEnd { set; get; }
        public DateTime? InstallOrderDate { set; get; }
        public string ServiceAddress { set; get; }
        public string VipID { set; get; }
        public string CustomerName { set; get; }
        public string CustomerPhone { set; get; }
        //public string Coordinate { set; get; }
        public decimal? Latitude { set; get; }
        public decimal? Longitude { set; get; }
        public string Distance { set; get; }
        public string Message { set; get; }
        public int? InstallCount { set; get; }
        public List<InstallDeviceViewModel> DeviceList { set; get; }
    }
    #endregion
}