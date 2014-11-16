using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Product.QiXinManage
{
    /// <summary>
    /// DelJobFunc的摘要说明
    /// </summary>
    [Export(typeof(IQiXinRequestHandler))]
    [ExportMetadata("Action", "DelJobFunc")]
    public class DelJobFuncHandler : IQiXinRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return DelJobFunc(pRequest);
        }

        public string DelJobFunc(string pRequest)
        {
            var rd = new APIResponse<DelJobFuncRD>();
            var rdData = new DelJobFuncRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<DelJobFuncRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = new LoggingSessionManager().CurrentSession;

            try
            {
                JobFunctionBLL jobBll = new JobFunctionBLL(loggingSessionInfo);
                string[] jobIdArr = rp.Parameters.JobFunctionID.Split(',');
                //object[] pIDs = { rp.Parameters.JobFunctionID };
                jobBll.Delete(jobIdArr);
                rdData.IsSuccess = true;
                rd.ResultCode = 0;
            }
            catch (Exception ex)
            {
                rd.ResultCode = 103;
                rd.Message = ex.Message;
            }
            rd.Data = rdData;
            return rd.ToJSON();
        }
    }

    #region 删除职衔
    public class DelJobFuncRP : IAPIRequestParameter
    {
        public string JobFunctionID { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(JobFunctionID)) throw new APIException("JobFunctionID不能为空") { ErrorCode = 102 };
        }
    }
    public class DelJobFuncRD : IAPIResponseData
    {
        public bool IsSuccess { set; get; }
    }
    #endregion
}