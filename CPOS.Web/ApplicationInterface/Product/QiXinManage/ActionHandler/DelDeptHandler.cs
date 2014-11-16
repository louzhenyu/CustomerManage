using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;

namespace JIT.CPOS.Web.ApplicationInterface.Product.QiXinManage.ActionHandler
{
    /// <summary>
    /// DelDept的摘要说明
    /// </summary>
    [Export(typeof(IQiXinRequestHandler))]
    [ExportMetadata("Action", "DelDept")]
    public class DelDeptHandler : IQiXinRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return DelDept(pRequest);
        }

        public string DelDept(string pRequest)
        {
            var rd = new APIResponse<DelDeptRD>();
            var rdData = new DelDeptRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<DelDeptRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            try
            {
                TUnitBLL unitBll = new TUnitBLL(loggingSessionInfo);
                unitBll.Delete(rp.Parameters.UnitID, null);
                rdData.IsSuccess = true;
                rd.ResultCode = 0;
                rd.Data = rdData;
            }
            catch (Exception ex)
            {
                rd.ResultCode = 103;
                rd.Message = ex.Message;
            }
            return rd.ToJSON();
        }
    }

    #region 删除部门
    public class DelDeptRP : IAPIRequestParameter
    {
        public string UnitID { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(UnitID)) throw new APIException("【UnitID】不能为空") { ErrorCode = 102 };
        }
    }
    public class DelDeptRD : IAPIResponseData
    {
        public bool IsSuccess { set; get; }
    }
    #endregion
}