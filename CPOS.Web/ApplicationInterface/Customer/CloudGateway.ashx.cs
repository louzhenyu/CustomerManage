using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using System.Configuration;
using System.Data;

namespace JIT.CPOS.Web.ApplicationInterface.Customer
{
    /// <summary>
    /// CloudGeteway 的摘要说明
    /// </summary>
    public class CloudGateway : BaseGateway
    {
        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            {
                string rst;
                switch (pAction)
                {
                    case "GetCardBag":  //获取我的卡包
                        rst = GetCardBag(pRequest);
                        break;
                    default:
                        throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                        {
                            ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                        };
                }
                return rst;
            }
        }
        /// <summary>
        /// 获取我的卡包
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetCardBag(string pRequest)
        {
            try
            {
                string cloudCustomerId = ConfigurationManager.AppSettings["CloudCustomerId"];//云店标识
                var rp = pRequest.DeserializeJSONTo<APIRequest<EmptyRequestParameter>>();
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                var vipBll = new VipBLL(loggingSessionInfo);
                var vipInfo = vipBll.GetByID(rp.UserID);
                string userId = string.Empty;
                var rd = new CardBagRD();
                if (vipInfo != null)
                {
                    if (!string.IsNullOrEmpty(vipInfo.WeiXinUserId))
                    {
                        DataSet dsVip = vipBll.GetCardBag(vipInfo.WeiXinUserId,cloudCustomerId);
                        if (dsVip.Tables[0].Rows.Count > 0)
                        {
                            rd.CardBagList = DataTableToObject.ConvertToList<CardBag>(dsVip.Tables[0]);
                        }
                    }
                }
                var rsp = new SuccessResponse<IAPIResponseData>(rd);
                return rsp.ToJSON();
            }
            catch (Exception ex)
            {
                throw new APIException(ex.Message);
            }
        }
        public class CardBagRD : IAPIResponseData
        {
            public List<CardBag> CardBagList { get; set; }
        }
        public class CardBag
        {
            public string CustomerID { get; set; }
            public string CustomerName { get; set; }
            public string ImageUrl { get; set; }
            public string UserId { get; set; }
            private string targetUrl;
            public string TargetUrl
            {
                get
                {
                    //微商城地址
                    return ConfigurationManager.AppSettings["website_url"] + "HtmlApps/html/public/index/index_shop_app.html?customerId=" + CustomerID + "&userId=" + UserId + "&APP_TYPE=SUBSCIBE&FromYun=1";
                }
                set { targetUrl = value; }
            }
        }
    }
}