using System;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.ExtensionMethod;


namespace JIT.CPOS.Web.ApplicationInterface.Project.Gree.ActionHandler
{
    /// <summary>
    /// GetApplyCountHandler 的摘要说明
    /// </summary>
    [Export(typeof(IGreeRequestHandler))]
    [ExportMetadata("Action", "GetApplyCount")]
    public class GetApplyCountHandler : IGreeRequestHandler
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
            var rd = new APIResponse<GetApplyCountRespData>();
            try
            {

                var req = pRequest.DeserializeJSONTo<APIRequest<GetApplyCountReqParameter>>();
                if (req.Parameters == null)
                {
                    throw new ArgumentException();
                }

                req.Parameters.Validate();

                var rdData = new GetApplyCountRespData
                {
                    Count = ServiceOrderManager.Instance.GetAppliedCount(req.Parameters.ServiceOrderNo)
                };
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

    #region 获取接单师傅人数
    public class GetApplyCountReqParameter : IAPIRequestParameter
    {
        public string ServiceOrderNo { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(ServiceOrderNo)) throw new APIException("【ServiceOrderNO】不能为空");
        }
    }

    public class GetApplyCountRespData : IAPIResponseData
    {
        public int? Count { set; get; }
    }
    #endregion
}