using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.DTO.Module.VIP.VipList.Request;
using JIT.CPOS.DTO.Module.VIP.VipList.Response;
using JIT.Utility.ExtensionMethod;
using System.Data;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.Web.ApplicationInterface.Module.NewAppContent
{
    /// <summary>
    /// NewWXGateway 的摘要说明
    /// </summary>
    public class NewWXGateway : BaseGateway
    {

        #region 错误码
        private const int ERROR_USERID_NOTNULL = 801;        //USerID不能为空
        private const int ERROR_WDAMOUNT_TOOBIG = 802;     //日累计提现金额等能大于设置金额
        private const int ERROR_WDAMOUNT_NOTWDTIME = 803;  //超出提现次数限制
        #endregion
        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst;
            switch (pAction)
            {
                
                case "SaveAgentCustomer":  //保存会员标签
                    rst = SaveAgentCustomer(pRequest);
                    break;

                default:
                    throw new APIException(string.Format("找不到名为：{0}的Action方法。", pAction));
            }
            return rst;
        }

        #region  保存产品试用或渠道代理信息
        public string SaveAgentCustomer(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SaveAgentCustomerRP>>();
            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
         

            if (rp.Parameters.AgentCustomerInfo == null)
            {
                throw new APIException("缺少参数【AgentCustomerInfo】或参数值为空") { ErrorCode = 135 };
            }

            if (string.IsNullOrEmpty(rp.Parameters.AgentCustomerInfo.AgentName))
            {
                throw new APIException("缺少参数【AgentName】或参数值为空") { ErrorCode = 135 };
            }


            AgentCustomerBLL _AgentCustomerBLL = new AgentCustomerBLL(loggingSessionInfo);


            var AgentCustomerInfo = rp.Parameters.AgentCustomerInfo;
            if (string.IsNullOrEmpty(AgentCustomerInfo.FromSource))//如果没有标识来源，则默认为微信
            {
                AgentCustomerInfo.FromSource = "WeiXin";
            }

            //如果该标签的id为空//创建一条记录
            if (string.IsNullOrEmpty(AgentCustomerInfo.AgentID))
            {
                //TagsEntity en = new TagsEntity();
                AgentCustomerInfo.AgentID = Guid.NewGuid().ToString();
                AgentCustomerInfo.CreateTime = DateTime.Now;
                AgentCustomerInfo.CreateBy = rp.UserID;
                AgentCustomerInfo.LastUpdateTime = DateTime.Now;
                AgentCustomerInfo.LastUpdateBy = rp.UserID;
                AgentCustomerInfo.IsDelete = 0;
                AgentCustomerInfo.CustomerID = rp.CustomerID;
                _AgentCustomerBLL.Create(AgentCustomerInfo);
            }
            else
            {

                AgentCustomerInfo.LastUpdateTime = DateTime.Now;
                AgentCustomerInfo.LastUpdateBy = rp.UserID;
                AgentCustomerInfo.IsDelete = 0;
                AgentCustomerInfo.CustomerID = rp.CustomerID;
                _AgentCustomerBLL.Update(AgentCustomerInfo, null, false);
            }
            var rd = new EmptyRD();//返回值
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        #endregion

    }
    public class SaveAgentCustomerRP : IAPIRequestParameter
    {

        //标签
        public AgentCustomerEntity AgentCustomerInfo { get; set; }



        public void Validate()
        {
        }
    }
}