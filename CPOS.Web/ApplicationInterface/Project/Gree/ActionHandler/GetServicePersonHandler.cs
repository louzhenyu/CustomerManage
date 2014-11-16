using System;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.ExtensionMethod;


namespace JIT.CPOS.Web.ApplicationInterface.Project.Gree.ActionHandler
{
    /// <summary>
    ///获取师傅个人信息的handle
    /// </summary>
    [Export(typeof(IGreeRequestHandler))]
    [ExportMetadata("Action", "GetServicePerson")]
    public class GetServicePersonHandler : IGreeRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetServicePerson(pRequest);
        }

        /// <summary>
        /// 获取师傅个人信息
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetServicePerson(string pRequest)
        {
            var rd = new APIResponse<GetServicePersonRD>();

            try
            {
                var req = pRequest.DeserializeJSONTo<APIRequest<GetServicePersonRP>>();
                if (req.Parameters == null)
                {
                    throw new ArgumentException();
                }

                req.Parameters.Validate();

                // 根据师傅id查询师傅详情
                var model = GreeCommon.GetServicePerson(req.CustomerID, req.UserID, req.Parameters.ServicePersonId);

                var rdData = new GetServicePersonRD {ServicePerson = model};
                rd.Data = rdData;
                rd.ResultCode = 0;
            }
            catch (Exception ex)
            {
                rd.Message = ex.Message;
                rd.ResultCode = 101;
            }

            return rd.ToJSON();
        }
    }

    #region 获取师傅个人信息
    public class GetServicePersonRP : IAPIRequestParameter
    {
        public string ServicePersonId { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(ServicePersonId)) throw new APIException("【ServicePersonId】不能为空") { ErrorCode = 101 };
        }
    }

    public class GetServicePersonRD : IAPIResponseData
    {
        public ServicePersonViewModel ServicePerson { set; get; }
    }
    #endregion
}