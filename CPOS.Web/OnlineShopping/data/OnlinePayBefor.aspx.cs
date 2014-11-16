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
using JIT.CPOS.BS.Entity.Interface;
using JIT.CPOS.Common;

namespace JIT.CPOS.Web.OnlineShopping.data
{
    public partial class OnlinePayBefor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("支付跳转: {0}", "come in")
            });
            if (!IsPostBack)
            {
                if (Request["order_id"] != null)
                {
                    SetPaymentInfo();
                }
            }
        }

        private void SetPaymentInfo()
        {
            // 创建一个新的 WebClient 实例.
            WebClient myWebClient = new WebClient();

            try
            {
                string strError = string.Empty;
                string OrderCustomerInfo = Request["order_id"];
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("支付跳转--order_id: {0}", OrderCustomerInfo)
                });
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("支付跳转--merchant_url: {0}", Request["merchant_url"].ToString())
                });
                if (OrderCustomerInfo == null || OrderCustomerInfo.Trim().Equals(""))
                {
                    strError = "订单标识为空";
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("支付跳转--单据: {0}", strError)
                    });
                }
                else
                {
                    #region 处理业务
                    var infos = OrderCustomerInfo.Split(',');
                    if (infos.Length != 2)
                    {
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("支付跳转: {0}", "长度错误")
                        });
                        return;
                    }
                    string customerId = infos[0].ToString().Trim();
                    string orderCode = infos[1].ToString().Trim();
                    var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                    InoutService service = new InoutService(loggingSessionInfo);
                    InoutInfo inoutInfo = new InoutInfo();
                    inoutInfo = service.GetInoutInfoByOrderCode(orderCode);
                    if (inoutInfo == null)
                    {
                        strError = "没有获取合法的订单信息";
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("支付跳转 {0}", "没有获取合法的订单信息")
                        });
                    }
                    else
                    {
                        #region Jermyn20131011 处理订单支付时订单信息
                        InoutInfo inoutModel = new InoutInfo();
                        string phone = string.Empty;
                        string address = string.Empty;
                        string userName = string.Empty;
                        inoutModel.order_id = inoutInfo.order_id;
                        inoutModel.modify_time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        #region
                        if (Request["phone"] != null)
                        {
                            inoutModel.Field6 = Request["phone"].Trim();
                            Loggers.Debug(new DebugLogInfo()
                            {
                                Message = string.Format("手机号码: {0}", inoutModel.Field6)
                            });
                        }
                        //else {
                        //    Loggers.Debug(new DebugLogInfo()
                        //    {
                        //        Message = string.Format("支付跳转--手机号码 {0}", "为空")
                        //    });
                        //}
                        if (Request["address"] != null)
                        {
                            inoutModel.Field4 = Request["address"].Trim();
                            Loggers.Debug(new DebugLogInfo()
                            {
                                Message = string.Format("地址: {0}", inoutModel.Field4)
                            });
                        }
                        if (Request["userName"] != null)
                        {
                            inoutModel.Field14 = Request["userName"].Trim();
                            Loggers.Debug(new DebugLogInfo()
                            {
                                Message = string.Format("用户名: {0}", inoutModel.Field14)
                            });
                        }
                        #endregion
                        bool bReturn = service.Update(inoutModel, out strError);
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("更新订单用户信息是否成功: {0}", strError)
                        });
                        #endregion
                      //请求参数：
                        //order_id：  订单号
                        //prod_name：  产品名称
                        //prod_price：  产品价格
                        //merchant_url：  用户付款中途退出或者付款成功后返回URL http://xxxx:9004/OnlineClothing/detail.html?itemId=C5BBD04EEE1643F381A522AD68D58828&back=list&customerId=f6a7da3d28f74f2abedfc3ea0cf65c01
                        //call_back_url：  交易结果返回URL（最终将会以POST方式推送交易结果到该地址）
                        string uriString = ConfigurationManager.AppSettings["PaypalUrl"].Trim();
                        string webUrl = ConfigurationManager.AppSettings["website_url"];
                        var postData = "order_id=" + OrderCustomerInfo 
                            + "&prod_name=" + inoutInfo.InoutDetailList[0].item_name
                            + "&prod_price=" + Convert.ToString(double.Parse(Convert.ToString(inoutInfo.actual_amount)).ToString("f2"))
                            + "&merchant_url=" + Request["merchant_url"]
                            + "&call_back_url=" + webUrl + "OnlineShopping/data/OnlinePayAfter.aspx";
                        CustomerPayAssignBLL customerPayServer = new CustomerPayAssignBLL(loggingSessionInfo);
                        CustomerPayAssignEntity customerPayInfo = new CustomerPayAssignEntity();
                        var list = customerPayServer.QueryByEntity(new CustomerPayAssignEntity
                        {
                            CustomerId = loggingSessionInfo.CurrentUser.customer_id
                            ,IsDelete = 0
                            ,PaymentTypeId = "BB04817882B149838B19DE2BDDA5E91B"
                        }, null);
                        if (list != null && list.Length > 0)
                        {
                            customerPayInfo = list[0];
                            if (customerPayInfo != null && customerPayInfo.AssignId != null && !customerPayInfo.AssignId.Equals(""))
                            {
                                string strFLAmount = (double.Parse(Convert.ToString(Convert.ToDecimal(customerPayInfo.CustomerProportion) * Convert.ToDecimal(inoutInfo.actual_amount) / Convert.ToDecimal(100)))).ToString("f2");
                                postData += "&params=" + customerPayInfo.CustomerAccountNumber + "^" + strFLAmount + "^";
                            }
                        }

                        //order_id=1234
                        //&prod_name=test8
                        //&prod_price=0.02
                        //&merchant_url=http://xxxx
                        //&call_back_url=http://180.153.154.21:9004/AlipayWapTrade/PaySuccess.aspx
                        //&params=qianzhi818@126.com^0.02^
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("支付跳转-链接字符串: {0}", postData)
                        });
                        // 注意这种拼字符串的ContentType
                        myWebClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                        // 转化成二进制数组
                        byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                        // 上传数据，并获取返回的二进制数据.
                        byte[] responseArray = myWebClient.UploadData(uriString, "POST", byteArray);
                        var data = System.Text.Encoding.UTF8.GetString(responseArray);
                        Response.Write(data);
                        strError = "ok";
                    }
                    #endregion
                }

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("支付跳转--完成: {0}", strError)
                });
                //myWebClient.UploadData(Request["merchant_url"].ToString(), "POST",Encoding.ASCII.GetBytes("&strError="+strError+""));
            }
            catch (Exception ex ) {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("支付跳转--出错: {0}", ex.ToString())
                });
               // myWebClient.UploadData(Request["merchant_url"].ToString(), "POST", Encoding.ASCII.GetBytes("&strError=" + ex.ToString() + ""));
            }
        }
    }
}