using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.ValueObject;
using JIT.CPOS.Web.Module.Log.InterfaceWebLog;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;

namespace JIT.CPOS.Web.ApplicationInterface.Project.HuaAn
{
    public partial class HuaAnCallBack : System.Web.UI.Page
    {
        public string AreaContent { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ProcessRequest();
            }
        }

        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest()
        {
            HttpContext context = HttpContext.Current;
            try
            {
                //参数解析
                var action = context.Request.QueryString["action"];
                //var reqContent = context.Request["req"];
                //获取华安响应表单对象
                HanAnMessage message = new HanAnMessage();
                message.verNum = context.Request.Form["verNum"];
                message.sysdate = context.Request.Form["sysdate"];
                message.systime = context.Request.Form["systime"];
                message.txcode = context.Request.Form["txcode"];
                message.seqNO = context.Request.Form["seqNO"];
                message.maccode = context.Request.Form["maccode"];
                message.content = context.Request.Form["content"];
                //AES解密
                //message.content = Utility.AESDecrypt(context.Request.Form["content"], HuaAnConst.AesKey);
                AreaContent = message.content;
                #region 插入日志信息
                Loggers.DEFAULT.Debug(new DebugLogInfo() { Message = message.content });
                #endregion

                ////分流逻辑处理
                //var rsp = this.ProcessAction(strAPIType, action, message);

            }
            catch (APIException ex)
            {
                Loggers.DEFAULT.Exception(new ExceptionLogInfo(ex));
                ErrorResponse rsp = new ErrorResponse(ex.ErrorCode, ex.Message);
                this.DoResponse(context, rsp.ToJSON());
            }
            catch (ThreadAbortException) { }    //Response.End通过抛出ThreadAbortException异常来实现中止执行后续代码的功能
            catch (Exception ex)
            {
                Loggers.DEFAULT.Exception(new ExceptionLogInfo(ex));
                ErrorResponse rsp = new ErrorResponse(ERROR_CODES.DEFAULT_ERROR, ex.Message);
                this.DoResponse(context, rsp.ToJSON());
            }
        }

        #region
        /// <summary>
        /// 过户。
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private string Transfer(HanAnMessage message)
        {

            throw new NotImplementedException();
        }
        #endregion


        #region
        /// <summary>
        /// 赎回回调处理。
        /// </summary>
        /// <returns></returns>
        private string Ransom(HanAnMessage message)
        {

            throw new NotImplementedException();
        }
        #endregion

        #region 工具方法
        /// <summary>
        /// 执行响应输出
        /// </summary>
        /// <param name="pContext">Http上下文</param>
        /// <param name="pRspContent">响应内容</param>
        protected void DoResponse(HttpContext pContext, string pRspContent)
        {
            if (!string.IsNullOrWhiteSpace(this.JSONP))
            {
                pRspContent = string.Format("{0}({1})", this.JSONP, pRspContent);
            }

            pContext.Response.Clear();
            pContext.Response.StatusCode = 200;
            pContext.Response.Write(pRspContent);
            pContext.Response.End();
        }

        #endregion

        #region 属性集
        /// <summary>
        /// javascript的跨域支持
        /// </summary>
        protected string JSONP { get; set; }
        #endregion

        #region 基础结构
        /// <summary>
        /// 是否允许并发访问
        /// </summary>
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        #endregion
    }
}