using System;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Project.Gree.ActionHandler
{
    /// <summary>
    /// 抢单状态的Handler
    /// </summary>
    [Export(typeof(IGreeRequestHandler))]
    [ExportMetadata("Action", "GrabState")]
    public class GrabStateHandler : IGreeRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GrabState(pRequest);
        }

        /// <summary>
        /// 抢单状态
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string GrabState(string pRequest)
        {
            var rd = new APIResponse<GrabStateRD>();
            var rdData = new GrabStateRD();

            var rp = pRequest.DeserializeJSONTo<APIRequest<GrabStateRP>>();

            if (string.IsNullOrEmpty(rp.UserID))
                throw new APIException("【UserID】不能为空");

            if (rp.Parameters == null)
            {
                throw new ArgumentException();
            }

            rp.Parameters.Validate();

            //-1:未定， 0: 失败，1: 成功 
            rdData.IsSuccess = ServiceOrderManager.Instance.IsOrderWinner(rp.Parameters.ServiceOrderNO, rp.UserID);
            rd.Data = rdData;
            return rd.ToJSON();
        }
    }

    #region 抢单
    public class GrabStateRP : IAPIRequestParameter
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

    public class GrabStateRD : IAPIResponseData
    {
        public int IsSuccess { set; get; }
    }
    #endregion
}