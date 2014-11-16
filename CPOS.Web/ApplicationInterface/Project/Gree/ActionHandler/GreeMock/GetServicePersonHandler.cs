using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Project.Gree.ActionHandler.GreeMock
{
    /// <summary>
    ///获取师傅个人信息的handle
    /// </summary>
    [Export(typeof(IGreeMockRequestHandler))]
    [ExportMetadata("Action", "GetServicePerson")]
    public class GetServicePersonHandler : IGreeMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetServicePerson(pRequest);
        }

        /// <summary>
        /// 获取师傅个人信息
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetServicePerson(string pRequest)
        {
            var rd = new APIResponse<GetServicePersonRD>();
            var rdData = new GetServicePersonRD();

            ServicePersonViewModel m1 = new ServicePersonViewModel();
            m1.ServicePersonId = "1";
            m1.Name = "王明";
            m1.Mobile = "123456";
            m1.Picture = "Picture";
            m1.Star = (decimal)10;
            m1.OrderCount = 1;
            m1.TodayOrder = 1;

            rdData.ServicePerson = m1;
            rd.Data = rdData;
            rd.ResultCode = 0;
            return rd.ToJSON();
        }
    }

    #region 获取师傅个人信息
    public class GetServicePersonRP : IAPIRequestParameter
    {
        public string ServicePersonId { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(ServicePersonId)) throw new APIException("【ServicePersonId】不能为空");
        }
    }
    public class GetServicePersonRD : IAPIResponseData
    {
        public ServicePersonViewModel ServicePerson { set; get; }
    }
    #endregion
}