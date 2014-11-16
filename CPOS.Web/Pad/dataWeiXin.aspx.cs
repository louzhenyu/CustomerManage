using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using System.Net;

namespace JIT.CPOS.Web.Pad
{
    public partial class dataWeiXin : System.Web.UI.Page
    {
        public string strText = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("认证界面: {0}", "comein")
            });
            if (!IsPostBack)
            {
                string amount = "0";
                string openId = Request["OpenId"];
                if (!string.IsNullOrEmpty(Request["amount"]))
                {
                    amount = Request["amount"];
                }

                
                
                //Response.Write("openId:" + openId + "" + "</br>");
                if (openId != null && !openId.Equals(""))
                {
                    try
                    {
                        if (Convert.ToDecimal(amount) > 0)
                        {

                        }
                    }
                    catch (Exception ex)
                    {
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("认证界面amount: {0}",amount + "--金额格式错误")
                        });
                        strText = "<div style=\"position:absolute; left:50px; top:70px; line-height:30px; z-index:2; font-size:18px; font-weight:bold; color:#697d7d\">金额格式错误</div>";
                        return;
                    }
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("认证界面openId: {0}", openId)
                    });
                    SetLegalizeInfo(openId, amount);
                }
                else {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("认证界面openId: {0}", "客户微信唯一码为空")
                    });
                    strText = "<div style=\"position:absolute; left:50px; top:70px; line-height:30px; z-index:2; font-size:18px; font-weight:bold; color:#697d7d\">客户微信唯一码为空</div>";
                    return;
                }
            }
        }
        #region 设置认证信息
        private void SetLegalizeInfo(string OpenId,string amount)
        {
            #region 链接客户参数
            var loggingSessionInfo = Default.GetLoggingSession();
            //根据客户标识获取连接字符串  qianzhi  2013-07-30
            if (Request["customerId"].Equals("null") || Request["customerId"].ToString().Equals(""))
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("认证界面customerId: {0}", "没有接收到客户信息")
                });
                strText = "<div style=\"position:absolute; left:50px; top:70px; line-height:30px; z-index:2; font-size:18px; font-weight:bold; color:#697d7d\">没有接收到客户信息</div>";
                return;
            }
            if (!string.IsNullOrEmpty(Request["customerId"]))
            {
                loggingSessionInfo = Default.GetBSLoggingSession(Request["customerId"].Trim(), "");
            }
            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("认证界面loggingSessionInfo: {0}", loggingSessionInfo.ToJSON())
            });
            #endregion

            WLegalizeBLL server = new WLegalizeBLL(loggingSessionInfo);
            string strError = string.Empty;
            string strNo = string.Empty;
            VipEntity vipInfo = new VipEntity();
            vipInfo = server.SetVipLegalize(OpenId, loggingSessionInfo.CurrentUser.customer_id.ToString(),amount,out strError);
            string str = "<div style=\"position:absolute; left:50px; top:70px; line-height:30px; z-index:2; font-size:18px; font-weight:bold; color:#697d7d\">";
            if (strError.Equals("OK"))
            {
                //vipInfo.Integration 
//                某某某，您的本次积分为****，累积积分为****
                int theIntegral = CPOS.Common.Utils.GetParseInt(amount);
                if (vipInfo.Integration == null)
                {
                    vipInfo.Integration = theIntegral;
                }
                else {
                    vipInfo.Integration = vipInfo.Integration + theIntegral;
                }
                str += "" + vipInfo.VipName + "<br>您的本次积分为" + theIntegral.ToString() + "<br>累积积分为" + Convert.ToString(CPOS.Common.Utils.GetParseInt((vipInfo.Integration))) + "";
                str += "<br>";
                str += "请向门店工作人员展示：";
                str += "<br>";
                str += "流水号 ：" + vipInfo.SerialNumber + "";
            }
            else
            {
                //Response.Write("提示 : " + strError + "");
                str += strError;
            }
            str += "</div>";
            strText = str;
        }
        #endregion
    }
}