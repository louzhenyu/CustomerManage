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
    /// ModifyJobFunc的摘要说明
    /// </summary>
    [Export(typeof(IQiXinRequestHandler))]
    [ExportMetadata("Action", "ModifyJobFunc")]
    public class ModifyJobFuncHandler : IQiXinRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return ModifyJobFunc(pRequest);
        }

        public string ModifyJobFunc(string pRequest)
        {
            var rd = new APIResponse<ModifyJobFuncRD>();
            var rdData = new ModifyJobFuncRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<ModifyJobFuncRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = new LoggingSessionManager().CurrentSession;

            try
            {
                JobFunctionBLL jobBll = new JobFunctionBLL(loggingSessionInfo);
                JobFunctionEntity entity = jobBll.GetByID(rp.Parameters.JobFunctionID);
                if (entity != null)
                {
                    entity.Name = rp.Parameters.Name;
                    entity.Description = rp.Parameters.Description;
                    if (rp.Parameters.Status != null)
                        entity.Status = rp.Parameters.Status;
                    jobBll.Update(entity);
                    rdData.IsSuccess = true;
                    rd.ResultCode = 0;
                }
                else
                {
                    rdData.IsSuccess = false;
                    rd.ResultCode = 101;
                    rd.Message = "职衔不存在";
                }
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

    #region 职衔信息变更
    public class ModifyJobFuncRP : IAPIRequestParameter
    {
        public string JobFunctionID { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public int Status { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(JobFunctionID)) throw new APIException("JobFunctionID不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(Name)) throw new APIException("Name不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(Description)) throw new APIException("Description不能为空") { ErrorCode = 102 };
        }
    }
    public class ModifyJobFuncRD : IAPIResponseData
    {
        public bool IsSuccess { set; get; }
    }
    #endregion
}