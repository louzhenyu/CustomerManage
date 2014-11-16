using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Product.QiXinManage.ActionHandler.QiXinMock
{
    /// <summary>
    /// BatchImportUserList的摘要说明
    /// </summary>
    [Export(typeof(IQiXinMockRequestHandler))]
    [ExportMetadata("Action", "BatchImportUserList")]
    public class BatchImportUserListHandler : IQiXinMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return BatchImportUserList(pRequest);
        }

        public string BatchImportUserList(string pRequest)
        {
            var rd = new APIResponse<BatchImportUserListRD>();
            var rdData = new BatchImportUserListRD();
            rdData.UnitID = Guid.NewGuid().ToString();
            rd.Data = rdData;
            rd.ResultCode = 0;
            return rd.ToJSON();
        }
    }

    #region 批量导入用户名单
    public class BatchImportUserListRP : IAPIRequestParameter
    {
        public string Test { set; get; }
        public void Validate()
        {
        }
    }
    public class BatchImportUserListRD : IAPIResponseData
    {
        public string UnitID { set; get; }
    }
    #endregion
}