using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.ValueObject;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Product.QiXinManage
{
    public class YTXManager
    {
        private static YTXManager single;

        private YTXManager()
        { }

        public static YTXManager Instance
        {
            get { return single ?? (single = new YTXManager()); }
        }

        public APIResponse<SingleRegisterRD> SingleReisterCall(string pRisUser, string pCustomerID, string pUserID)
        {
            var rp = new APIRequest<SingleRegisterRP>();
            rp.CustomerID = pCustomerID;
            rp.UserID = pUserID;
            rp.Parameters.RisUserID = pRisUser;

            string path = "/RateLetterInterface/User/UserHandler.ashx";
            var rsp1 = APIClientProxy.CallAPI<SingleRegisterRP, SingleRegisterRD>(APITypes.Project, path, "SingleRegister", rp);

            return rsp1;
        }
    }

    #region SingleRegister
    public class SingleRegisterRP : IAPIRequestParameter
    {
        public string RisUserID { set; get; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(RisUserID))
                throw new APIException("RisUserID不能为空");
        }
    }
    public class SingleRegisterRD : IAPIResponseData
    {
        /// <summary>
        /// 帐号
        /// </summary>
        public string VoipAccount { set; get; }
        /// <summary>
        /// 密码
        /// </summary>
        public string VoipPwd { set; get; }
    }
    #endregion
}