using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Project.Gree.ActionHandler.GreeMock
{
    /// <summary>
    /// 抢单的Handler
    /// </summary>
    [Export(typeof(IGreeMockRequestHandler))]
    [ExportMetadata("Action", "GrabOrder")]
    public class GrabOrderHandler : IGreeMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GrabOrder(pRequest);
        }

        /// <summary>
        /// 抢单
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string GrabOrder(string pRequest)
        {
            var rd = new APIResponse<GrabOrderRD>();
            var rdData = new GrabOrderRD();
            rdData.Msg = "抢单中…";
            rd.Data = rdData;
            return rd.ToJSON();
        }
    }

    #region 抢单
    public class GrabOrderRP : IAPIRequestParameter
    {
        /// <summary>
        /// 服务单号
        /// </summary>
        public string ServiceOrderNO { set; get; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(ServiceOrderNO))
                throw new APIException("【ServiceOrderNO】不能为空");
        }
    }

    public class GrabOrderRD : IAPIResponseData
    {
        public string Msg { set; get; }
    }
    #endregion
}