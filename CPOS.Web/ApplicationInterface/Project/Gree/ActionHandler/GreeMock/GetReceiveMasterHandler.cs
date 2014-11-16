using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Project.Gree.ActionHandler.GreeMock
{
    /// <summary>
    /// 处理获取接单师傅信息列表的Handler
    /// </summary>
    [Export(typeof(IGreeMockRequestHandler))]
    [ExportMetadata("Action", "GetReceiveMaster")]
    public class GetReceiveMasterHandler : IGreeMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetReceiveMaster(pRequest);
        }

        /// <summary>
        /// 获取接单师傅信息列表
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetReceiveMaster(string pRequest)
        {
            var rd = new APIResponse<GetReceiveMasterRD>();
            var rdData = new GetReceiveMasterRD();
            ServicePersonViewModel m1 = new ServicePersonViewModel();
            m1.ServicePersonId = "1";
            m1.Name = "王明";
            m1.Mobile = "123456";
            m1.Picture = "Picture";
            m1.Star = (decimal)10;
            m1.OrderCount = 1;
            m1.TodayOrder = 1;
            List<ServicePersonViewModel> list = new List<ServicePersonViewModel>();
            list.Add(m1);
            rdData.ServicePersonList = list;
            rd.Data = rdData;
            rd.ResultCode = 0;
            return rd.ToJSON();
        }
    }

    #region 获取接单师傅信息列表
    public class GetReceiveMasterRP : IAPIRequestParameter
    {
        public string OrderNO { set; get; }

        public void Validate()
        {
        }
    }
    public class GetReceiveMasterRD : IAPIResponseData
    {
        public List<ServicePersonViewModel> ServicePersonList { set; get; }
    }
    public class ServicePersonViewModel
    {
        public string ServicePersonId { set; get; }
        public string Name { set; get; }
        public string Mobile { set; get; }
        public string Picture { set; get; }
        public decimal? Star { set; get; }
        public int? OrderCount { set; get; }
        public int? TodayOrder { set; get; }
    }
    #endregion
}