using System;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;
using JIT.CPOS.Web.Module.Log.InterfaceWebLog;
using JIT.CPOS.Web.RateLetterInterface.Common;
using JIT.Utility.Log;

namespace JIT.CPOS.Web.RateLetterInterface
{
    public class YtxCallbackHandler : IHttpHandler
    {
        /// <summary>
        /// You will need to configure this handler in the Web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            Loggers.Debug(new DebugLogInfo {Message = "Callback from yuntongxun received."});
            var req = context.Request;

            var urlAction = req["action"];
            if (string.IsNullOrEmpty(urlAction))
            {
                return;
            }

            if (req.RequestType != "POST") return;

            // 定义接收xml数据
            string xmlStr;
            using (Stream stream = req.InputStream)
            {
                // 获取请求xml的文件流
                var tmpData = new byte[stream.Length];
                stream.Read(tmpData, 0, tmpData.Length);
                xmlStr = Encoding.UTF8.GetString(tmpData);

                //写日志
                Loggers.Debug(new DebugLogInfo { Message = "Callback request: " + xmlStr });
            }

            // 默认请求返回的内容
            string xmlResult = @"<?xml version=""1.0"" encoding=""UTF-8"" ?><response><statuscode>400</statuscode><statusmsg>未解析该请求</statusmsg></response>";

            // 解析xml文件
            try
            {
                //声明一个XMLDoc文档对象，LOAD（）xml字符串  
                var doc = new XmlDocument();
                doc.LoadXml(xmlStr);
                //得到XML文档根节点  
                XmlElement root = doc.DocumentElement;
                if (root != null && root.Name == "request")
                {
                    XmlNodeList nls = root.ChildNodes;
                    foreach (XmlNode xn1 in nls)
                    {
                        var valueElement = (XmlElement) xn1;
                        if (valueElement.Name == "action")
                        {
                            string action = valueElement.InnerText;
                            switch (action)
                            {
                                case "CallAuth":
                                    // 处理呼叫鉴权
                                    xmlResult = parseCallAuth(nls);
                                    break;
                                    //case "CallEstablish":
                                    //    // 处理呼叫完成
                                    //    xmlResult = parseCallEstablish(nls);
                                    //    break;
                                    //case "Hangup":
                                    //    // 处理挂断
                                    //    xmlResult = parseHangup(nls);
                                    //    break;
                                    //case "OfflineMsg":
                                    //    // 处理离线消息
                                    //    xmlResult = parseOfflineMessage(nls);
                                    //    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Loggers.DEFAULT.Exception(new ExceptionLogInfo(ex));
            }

            // 返回xml格式的结果
            context.Response.Write(xmlResult);
            context.Response.End(); // 结束输出
        }

        #endregion

        /// <summary>
        /// 解析呼叫鉴权
        /// </summary>
        /// <param name="nodelist"></param>
        /// <returns></returns>
        private string parseCallAuth(XmlNodeList nodelist)
        {
            var info = new CallAuthen();
            foreach (XmlNode node in nodelist)
            {
                var valueElement = (XmlElement)node;
                if (valueElement.Name == "type")
                {
                    info.Type = valueElement.InnerText;
                }
                if (valueElement.Name == "orderid")
                {
                    info.OrderId = valueElement.InnerText;
                }
                if (valueElement.Name == "subid")
                {
                    info.SubId = valueElement.InnerText;
                }
                if (valueElement.Name == "caller")
                {
                    info.Caller = valueElement.InnerText;
                }
                if (valueElement.Name == "called")
                {
                    info.Called = valueElement.InnerText;
                }
                if (valueElement.Name == "subtype")
                {
                    info.SubType = valueElement.InnerText;
                }
                if (valueElement.Name == "callSid")
                {
                    info.CallSid = valueElement.InnerText;
                }
            }

            //返回的数据
            var retStr = new StringBuilder();
            retStr.Append(@"<?xml version=""1.0"" encoding=""UTF-8"" ?><Response>");

            // 必选字段 状态码 0000表示成功 
            retStr.Append("<statuscode>").Append("0000").Append("</statuscode>");

            // 可选字段 状态描述 
            retStr.Append("<statusmsg>").Append("Success").Append("</statusmsg>");

            // 可选字段 是否录音 0:不录音 1:录音
            retStr.Append("<record>").Append("0").Append("</record>");

            // 可选字段 此次通话时长单位为秒 默认1小时（小于等于0，默认1小时）
            retStr.Append("<sessiontime>").Append("0").Append("</sessiontime>");

            retStr.Append("</Response>");
            return retStr.ToString();
        }
    }
}
