using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.Module.CustomerBasicSetting.Handler;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Request;
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.SuperRetailTrader.Login
{
    public class GetLoginInfoAH : BaseActionHandler<GetLoginInfoRP, GetLoginInfoRD>
    {
        #region 错误码
        private const int Error_CustomerCode_NotNull = 103;
        public const int Error_CustomerCode_NotExist = 104;
        public const int Error_UserName_InValid = 105;
        public const int Error_Password_InValid = 106;
        public const int Error_UserRole_NotExist = 107;
        #endregion

        /// <summary>
        /// 超级分销 App 登录 
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected override GetLoginInfoRD ProcessRequest(DTO.Base.APIRequest<GetLoginInfoRP> pRequest)
        {
            string customerCode = pRequest.Parameters.SuperRetailTraderCode;
            string phone = pRequest.Parameters.AccountName;
            string password = pRequest.Parameters.PassWord;
            if (string.IsNullOrEmpty(customerCode))
            {
                throw new APIException("客户代码不能为空") { ErrorCode = Error_CustomerCode_NotNull };
            }

            WMenuBLL menuServer = new WMenuBLL(Default.GetAPLoggingSession(""));
            string customerId = menuServer.GetCustomerIDByCustomerCode(customerCode);

            if (string.IsNullOrEmpty(customerId))
            {
                throw new APIException("客户代码对应的客户不存在") { ErrorCode = Error_CustomerCode_NotExist };
            }
            var currentUserInfo = Default.GetBSLoggingSession(customerId, "1");

            #region 判断分销商是否存在
            T_SuperRetailTraderBLL bll = new T_SuperRetailTraderBLL(currentUserInfo);
            List<T_SuperRetailTraderEntity> _model = bll.QueryByEntity(new T_SuperRetailTraderEntity() { SuperRetailTraderLogin = pRequest.Parameters.AccountName, SuperRetailTraderPass=pRequest.Parameters.PassWord }, null).ToList();

            if (_model == null || _model.Count == 0)
            {
                throw new APIException("账号或密码不正确！") { ErrorCode = 135 };
            }
            var result = _model[0];
            #endregion

            #region 获取分销商信息
            var rd = new GetLoginInfoRD();
            rd.JoinTime = result.JoinTime;
            rd.SuperRetailTraderAddress = result.SuperRetailTraderAddress;
            rd.SuperRetailTraderFrom = result.SuperRetailTraderFrom;
            rd.SuperRetailTraderMan = result.SuperRetailTraderMan;
            rd.SuperRetailTraderName = result.SuperRetailTraderName;
            rd.SuperRetailTraderPhone = result.SuperRetailTraderPhone;
            rd.CustomerId = currentUserInfo.ClientID;
            rd.UserId = result.SuperRetailTraderID.ToString();
            if (result.Status == "00") //未统一协议
            {
                rd.Status = 0;
            }
            else if (result.Status == "10")
            {
                rd.Status = 1;
            }
            #endregion

            return rd;
        }
    }
}