using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Product.QiXinManage.ActionHandler.QiXinMock
{
    /// <summary>
    /// DelJobFunc的摘要说明
    /// </summary>
    [Export(typeof(IQiXinMockRequestHandler))]
    [ExportMetadata("Action", "DelJobFunc")]
    public class DelJobFuncHandler : IQiXinMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return DelJobFunc(pRequest);
        }

        public string DelJobFunc(string pRequest)
        {
            var rd = new APIResponse<DelJobFuncRD>();
            var rdData = new DelJobFuncRD();
            rdData.IsSuccess = true;
            rd.Data = rdData;
            rd.ResultCode = 0;
            return rd.ToJSON();
        }
    }

    #region 删除职衔
    public class DelJobFuncRP : IAPIRequestParameter
    {
        public string Test { set; get; }
        public void Validate()
        {
        }
    }
    public class DelJobFuncRD : IAPIResponseData
    {
        public bool IsSuccess { set; get; }
    }
    #endregion
}