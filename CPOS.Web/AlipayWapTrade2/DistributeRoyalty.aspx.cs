using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using JIT.CPOS.BS.BLL.DistributeRoyalty;
using JIT.CPOS.BS.BLL;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.AlipayWapTrade2
{
    /// <summary>
    /// 功能：多级分润接口接入页
    /// 版本：3.3
    /// 日期：2012-07-05
    /// 说明：
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    /// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
    /// 
    /// /////////////////注意///////////////////////////////////////////////////////////////
    /// 如果您在接口集成过程中遇到问题，可以按照下面的途径来解决
    /// 1、商户服务中心（https://b.alipay.com/support/helperApply.htm?action=consultationApply），提交申请集成协助，我们会有专业的技术工程师主动联系您协助解决
    /// 2、商户帮助中心（http://help.alipay.com/support/232511-16307/0-16307.htm?sh=Y&info_type=9）
    /// 3、支付宝论坛（http://club.alipay.com/read-htm-tid-8681712.html）
    /// 
    /// 如果不想使用扩展功能请把扩展功能参数赋空值。
    /// </summary>
    public partial class DistributeRoyalty : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SubmitDistribute();
            }
        }

        private void SubmitDistribute()
        {
            BaseService.WriteLog("多级分润接口-----------------------AlipayWapTrade2/DistributeRoyalty.aspx");

            ////////////////////////////////////////////请求参数////////////////////////////////////////////

            string out_bill_no = string.Empty;
            string out_trade_no = string.Empty;
            string trade_no = string.Empty;
            string royalty_parameters = string.Empty;

            //该次分润的分润号
            //必填，保证其唯一性
            if (!string.IsNullOrEmpty(Request["out_bill_no"]))
            {
                out_bill_no = Request["out_bill_no"].Trim();
                BaseService.WriteLog("out_bill_no:  " + Request["out_bill_no"]);
            }
            else
            {
                BaseService.WriteLog("请求参数out_bill_no is null!!!!!");
            }

            //商户订单号
            //商户网站已经付款完成的商户网站订单号，out_trade_no、trade_no须至少填写一项
            if (!string.IsNullOrEmpty(Request["out_trade_no"]))
            {
                out_trade_no = Request["out_trade_no"].Trim();
                BaseService.WriteLog("out_trade_no:  " + Request["out_trade_no"]);
            }
            else
            {
                BaseService.WriteLog("请求参数out_trade_no is null!!!!!");
            }

            //支付宝交易号
            //已经付款完成的支付宝交易号，与商户网站订单号out_trade_no相对应
            if (!string.IsNullOrEmpty(Request["trade_no"]))
            {
                trade_no = Request["trade_no"].Trim();
                BaseService.WriteLog("trade_no:  " + Request["trade_no"]);
            }
            else
            {
                BaseService.WriteLog("请求参数trade_no is null!!!!!");
            }

            //提成信息集
            //必填，格式设置参见接口技术文档
            if (!string.IsNullOrEmpty(Request["royalty_parameters"]))
            {
                royalty_parameters = Request["royalty_parameters"].Trim();
                BaseService.WriteLog("royalty_parameters:  " + Request["royalty_parameters"]);
            }
            else
            {
                BaseService.WriteLog("请求参数royalty_parameters is null!!!!!");
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////

            BaseService.WriteLog("把请求参数打包成数组");
            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("partner", Config.Partner);
            sParaTemp.Add("_input_charset", Config.Input_charset.ToLower());
            sParaTemp.Add("service", "distribute_royalty");
            sParaTemp.Add("out_bill_no", out_bill_no);
            sParaTemp.Add("out_trade_no", out_trade_no);
            sParaTemp.Add("trade_no", trade_no);
            sParaTemp.Add("royalty_type", "10");
            sParaTemp.Add("royalty_parameters", royalty_parameters);

            BaseService.WriteLog("建立请求，以模拟远程HTTP的POST请求方式构造并获取支付宝的处理结果");
            //建立请求
            string sHtmlText = Submit.BuildRequest(sParaTemp);

            RespData respData = new RespData();
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.LoadXml(sHtmlText);

                string is_success = xmlDoc.SelectSingleNode("/alipay/is_success").InnerText;
                if (!is_success.Equals("T"))
                {
                    string error = xmlDoc.SelectSingleNode("/alipay/error").InnerText;
                    respData.Code = "F";
                    respData.Description = "操作失败";
                    respData.Exception = error;
                }
            }
            catch (Exception exp)
            {
                respData.Code = "F";
                respData.Description = "操作失败";
                respData.Exception = exp.ToString();
            }

            Response.Write(respData.ToJSON());
        }

        private class RespData
        {
            public string Code = "T";
            public string Description = "操作成功";
            public string Exception = null;
        }

        #region 支付宝分润公共方法

        /// <summary>
        /// 支付宝分润公共方法
        /// </summary>
        /// <param name="out_bill_no">该次分润的分润号，必填，保证其唯一性</param>
        /// <param name="out_trade_no">商户订单号，商户网站已经付款完成的商户网站订单号，out_trade_no、trade_no须至少填写一项</param>
        /// <param name="trade_no">支付宝交易号，已经付款完成的支付宝交易号，与商户网站订单号out_trade_no相对应</param>
        /// <param name="royalty_parameters">提成信息集，必填，格式设置参见接口技术文档</param>
        /// <returns>返回分润结果JSON字符串</returns>
        public string SubmitDistribute(string out_bill_no, string out_trade_no, string trade_no, string royalty_parameters)
        {
            BaseService.WriteLog("分润公共方法SubmitDistribute()");

            BaseService.WriteLog("把请求参数打包成数组");
            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("partner", Config.Partner);
            sParaTemp.Add("_input_charset", Config.Input_charset.ToLower());
            sParaTemp.Add("service", "distribute_royalty");
            sParaTemp.Add("out_bill_no", out_bill_no);
            sParaTemp.Add("out_trade_no", out_trade_no);
            sParaTemp.Add("trade_no", trade_no);
            sParaTemp.Add("royalty_type", "10");
            sParaTemp.Add("royalty_parameters", royalty_parameters);

            BaseService.WriteLog("建立请求，以模拟远程HTTP的POST请求方式构造并获取支付宝的处理结果");
            //建立请求
            string sHtmlText = Submit.BuildRequest(sParaTemp);

            RespData respData = new RespData();
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.LoadXml(sHtmlText);

                string is_success = xmlDoc.SelectSingleNode("/alipay/is_success").InnerText;
                if (!is_success.Equals("T"))
                {
                    string error = xmlDoc.SelectSingleNode("/alipay/error").InnerText;
                    respData.Code = "F";
                    respData.Description = "操作失败";
                    respData.Exception = error;
                }
            }
            catch (Exception exp)
            {
                respData.Code = "F";
                respData.Description = "操作失败";
                respData.Exception = exp.ToString();
            }

            return respData.ToJSON();
        }

        #endregion
    }
}