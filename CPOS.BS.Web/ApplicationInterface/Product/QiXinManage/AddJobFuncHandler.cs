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
    /// AddJobFunc的摘要说明
    /// </summary>
    [Export(typeof(IQiXinRequestHandler))]
    [ExportMetadata("Action", "AddJobFunc")]
    public class AddJobFuncHandler : IQiXinRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return AddJobFunc(pRequest);
        }

        public string AddJobFunc(string pRequest)
        {
            var rd = new APIResponse<AddJobFuncRD>();
            var rdData = new AddJobFuncRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<AddJobFuncRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = new LoggingSessionManager().CurrentSession;

            try
            {
                JobFunctionBLL jobBll = new JobFunctionBLL(loggingSessionInfo);
                Guid guid = Guid.NewGuid();
                JobFunctionEntity entity = new JobFunctionEntity
                {
                    JobFunctionID = guid,
                    Name = rp.Parameters.Name,
                    Description = rp.Parameters.Description,
                    Status = rp.Parameters.Status,
                    CustomerID = loggingSessionInfo.ClientID
                };
                jobBll.Create(entity);
                rdData.JobFunctionID = guid.ToString();
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

    #region 添加职衔
    public class AddJobFuncRP : IAPIRequestParameter
    {
        public string Name { set; get; }
        public string Description { set; get; }
        public int? Status { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(Name)) throw new APIException("Name不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(Description)) throw new APIException("Description不能为空") { ErrorCode = 102 };
            if (Status == null) Status = 1;//默认1
        }
    }
    public class AddJobFuncRD : IAPIResponseData
    {
        public string JobFunctionID { set; get; }
    }
    #endregion
}