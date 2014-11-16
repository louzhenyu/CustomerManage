using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Project.Gree.ActionHandler.GreeMock
{
    /// <summary>
    /// GetApplyCountHandler 的摘要说明
    /// </summary>
    [Export(typeof(IGreeMockRequestHandler))]
    [ExportMetadata("Action", "GetApplyCount")]
    public class GetApplyCountHandler : IGreeMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetApplyCount(pRequest);
        }

        /// <summary>
        /// 获取接单师傅人数
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetApplyCount(string pRequest)
        {
            var rd = new APIResponse<GetApplyCountRD>();
            var rdData = new GetApplyCountRD();
            rdData.Count = 5;
            rd.Data = rdData;
            rd.ResultCode = 0;
            return rd.ToJSON();
        }
    }

    #region 获取接单师傅人数
    public class GetApplyCountRP : IAPIRequestParameter
    {
        public string ServiceOrderNO { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(ServiceOrderNO)) throw new APIException("【ServiceOrderNO】不能为空");
        }
    }
    public class GetApplyCountRD : IAPIResponseData
    {
        public int? Count { set; get; }
    }
    #endregion
}