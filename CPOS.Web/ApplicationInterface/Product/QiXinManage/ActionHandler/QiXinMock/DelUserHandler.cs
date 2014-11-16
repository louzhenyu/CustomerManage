using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Product.QiXinManage.ActionHandler.QiXinMock
{
    /// <summary>
    /// DelUser的摘要说明
    /// </summary>
    [Export(typeof(IQiXinMockRequestHandler))]
    [ExportMetadata("Action", "DelUser")]
    public class DelUserHandler : IQiXinMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return DelUser(pRequest);
        }

        public string DelUser(string pRequest)
        {
            var rd = new APIResponse<DelUserRD>();
            var rdData = new DelUserRD();
            rdData.IsSuccess = true;
            rd.Data = rdData;
            rd.ResultCode = 0;
            return rd.ToJSON();
        }
    }

    #region 移除用户
    public class DelUserRP : IAPIRequestParameter
    {
        public string Test { set; get; }
        public void Validate()
        {
        }
    }
    public class DelUserRD : IAPIResponseData
    {
        public bool IsSuccess { set; get; }
    }
    #endregion
}