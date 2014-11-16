using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Product.QiXinManage.ActionHandler.QiXinMock
{
    /// <summary>
    /// ModifyDeptLeader的摘要说明
    /// </summary>
    [Export(typeof(IQiXinMockRequestHandler))]
    [ExportMetadata("Action", "ModifyDeptLeader")]
    public class ModifyDeptLeaderHandler : IQiXinMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return ModifyDeptLeader(pRequest);
        }

        public string ModifyDeptLeader(string pRequest)
        {
            var rd = new APIResponse<ModifyDeptLeaderRD>();
            var rdData = new ModifyDeptLeaderRD();
            rdData.IsSuccess = true;
            rd.Data = rdData;
            rd.ResultCode = 0;
            return rd.ToJSON();
        }
    }

    #region 变更部门Leader
    public class ModifyDeptLeaderRP : IAPIRequestParameter
    {
        public string Test { set; get; }
        public void Validate()
        {
        }
    }
    public class ModifyDeptLeaderRD : IAPIResponseData
    {
        public bool IsSuccess { set; get; }
    }
    #endregion
}