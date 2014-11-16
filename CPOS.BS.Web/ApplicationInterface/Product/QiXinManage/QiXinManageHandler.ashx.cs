using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Product.QiXinManage
{
    /// <summary>
    /// QiXinManageHandler的摘要说明
    /// </summary>
    public class QiXinManageHandler : BaseGateway
    {
        /// <summary>
        /// 企信后台管理
        /// </summary>
        /// <param name="pType"></param>
        /// <param name="pAction"></param>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            //根据action做不同的业务处理
            return RequestHandlerManager.Instance.HandleQiXinRequest(pAction, pRequest);
        }
    }
}