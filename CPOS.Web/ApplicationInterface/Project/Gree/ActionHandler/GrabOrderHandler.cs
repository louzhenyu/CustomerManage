using System;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Project.Gree.ActionHandler
{
    /// <summary>
    /// 抢单的Handler
    /// </summary>
    [Export(typeof(IGreeRequestHandler))]
    [ExportMetadata("Action", "GrabOrder")]
    public class GrabOrderHandler : IGreeRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GrabOrder(pRequest);
        }

        /// <summary>
        /// 抢单
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string GrabOrder(string pRequest)
        {
            var rd = new APIResponse<GrabOrderRD>();
            var rdData = new GrabOrderRD();

            var rp = pRequest.DeserializeJSONTo<APIRequest<GrabOrderRP>>();

            if (string.IsNullOrEmpty(rp.UserID))
                throw new APIException("【UserID】不能为空");

            if (rp.Parameters == null)
            {
                throw new ArgumentException();
            }

            rp.Parameters.Validate();
            ServiceOrderManager.Instance.ApplyOrder(rp.Parameters.ServiceOrderNO, rp.UserID);
            rdData.Msg = "抢单中…";
            rd.Data = rdData;
            return rd.ToJSON();
        }
    }

    #region 抢单
    public class GrabOrderRP : IAPIRequestParameter
    {
        /// <summary>
        /// 服务单号
        /// </summary>
        public string ServiceOrderNO { set; get; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(ServiceOrderNO))
                throw new APIException("【ServiceOrderNO】不能为空") { ErrorCode = 101 };
        }
    }

    public class GrabOrderRD : IAPIResponseData
    {
        public string Msg { set; get; }
    }
    #endregion
}