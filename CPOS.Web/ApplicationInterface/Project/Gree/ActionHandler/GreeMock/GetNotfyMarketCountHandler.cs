using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Project.Gree.ActionHandler.GreeMock
{
    /// <summary>
    /// 获取通知师傅人数的handle
    /// </summary>
    [Export(typeof(IGreeMockRequestHandler))]
    [ExportMetadata("Action", "GetNotfyMarketCount")]
    public class GetNotfyMarketCountHandler : IGreeMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetNotfyMarketCount(pRequest);
        }

        /// <summary>
        /// 获取通知师傅人数
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetNotfyMarketCount(string pRequest)
        {
            var rd = new APIResponse<GetNotfyMarketCountRD>();
            var rdData = new GetNotfyMarketCountRD();
            rdData.Count = 3 + new Random().Next(2, 20);
            rd.Data = rdData;
            rd.ResultCode = 0;
            return rd.ToJSON();
        }
    }

    #region 获取通知师傅人数
    public class GetNotfyMarketCountRP : IAPIRequestParameter
    {
        public void Validate()
        {
        }
    }
    public class GetNotfyMarketCountRD : IAPIResponseData
    {
        public int? Count { set; get; }
    }
    #endregion
}