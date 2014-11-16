using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Project.Gree.ActionHandler.GreeMock
{
    /// <summary>
    /// 处理提交预约单的Handler
    /// </summary>
    [Export(typeof(IGreeMockRequestHandler))]
    [ExportMetadata("Action", "SubmitAppOrder")]
    public class SubmitAppOrderHandler : IGreeMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return SubmitAppOrder(pRequest);
        }

        /// <summary>
        /// 提交预约订单
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string SubmitAppOrder(string pRequest)
        {
            var rd = new APIResponse<SubmitAppOrderRD>();
            var rdData = new SubmitAppOrderRD();
            rdData.ServiceOrderNO = "05FC0F4A-BB2E-44BC-A771-54E2763F8D3F";
            rd.Data = rdData;
            return rd.ToJSON();
        }

    }

    #region 提交预约单
    public class SubmitAppOrderRP : IAPIRequestParameter
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNO { set; get; }
        /// <summary>
        /// 1:安装，2:维修
        /// </summary>
        public int? ServiceType { set; get; }
        /// <summary>
        /// 预约日期
        /// </summary>
        public DateTime? ServiceOrderDate { set; get; }
        /// <summary>
        /// 预约结束日期
        /// </summary>
        public DateTime? ServiceOrderDateEnd { set; get; }
        /// <summary>
        /// 安装地址
        /// </summary>
        public string ServiceAddress { set; get; }
        /// <summary>
        /// 发送给安装师傅的消息
        /// </summary>
        public string Msg { set; get; }
        /// <summary>
        /// 性别
        /// </summary>
        public int? Gender { set; get; }
        /// <summary>
        /// 姓氏
        /// </summary>
        public string Surname { set; get; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string Latitude { set; get; }
        /// <summary>
        /// 经度
        /// </summary>
        public string Longitude { set; get; }

        public string VipID { set; get; }

        /// <summary>
        /// 要安装的设备列表
        /// </summary>
        public List<InstallDeviceViewModel> DeviceList { set; get; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(OrderNO))
                throw new APIException("【OrderNO】不能为空");
            if (ServiceType == null)
                throw new APIException("【ServiceType】不能为空");
            if (ServiceOrderDate == null)
                throw new APIException("【ServiceOrderDate】不能为空");
            if (ServiceOrderDateEnd == null)
                throw new APIException("【ServiceOrderDateEnd】不能为空");
            if (string.IsNullOrEmpty(ServiceAddress))
                throw new APIException("【ServiceAddress】不能为空");
            if (Gender == null)
                throw new APIException("【Gender】不能为空");
            if (string.IsNullOrEmpty(Surname))
                throw new APIException("【Surname】不能为空");
            if (DeviceList == null && DeviceList.Count <= 0)
                throw new APIException("【DeviceList】不能为空");
            if (Longitude == null)
                throw new APIException("【Longitude】不能为空");
            if (Latitude == null)
                throw new APIException("【Latitude】不能为空");
        }
    }

    public class SubmitAppOrderRD : IAPIResponseData
    {
        /// <summary>
        /// 预约单号
        /// </summary>
        public string ServiceOrderNO { set; get; }
    }
    #endregion

}