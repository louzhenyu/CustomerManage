using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.Web.ApplicationInterface.Base;

namespace JIT.CPOS.Web.ApplicationInterface.Product.MobileSurveyTest
{

    /// <summary>
    /// Mobile Survey&Test Handler 的摘要说明
    /// </summary>
    public class MobileSurveyTestHandler : BaseGateway
    {
        /// <summary>
        /// Mobile Library
        /// </summary>
        /// <param name="pType"></param>
        /// <param name="pAction"></param>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            //根据action做不同的业务处理
            return RequestHandlerManager.Instance.HandleMSurveyTestRequest(pAction, pRequest);
        }
    }
}