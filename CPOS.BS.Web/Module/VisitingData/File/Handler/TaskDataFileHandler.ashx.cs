using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using JIT.CPOS.BS.BLL;
using JIT.Utility.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.Extension;
using JIT.Utility.ExtensionMethod;
using System.Data;
using System.Text;
using System.Collections;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace JIT.CPOS.BS.Web.Module.VisitingData.Photo.Handler
{
    /// <summary>
    /// TaskDataPhotoHandler 的摘要说明
    /// </summary>
    public class TaskDataFileHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        /// <summary>
        /// 页面入口
        /// </summary>
        /// <param name="pContext"></param>
        protected override void AjaxRequest(HttpContext pContext)
        {
            string res = "";
            switch (this.BTNCode)
            {
                case "search":
                    if (!string.IsNullOrEmpty(this.Method))
                    {
                        switch (this.Method)
                        {
                            case "GetVisitingTaskStepSum":
                                res = GetVisitingTaskStepSum(pContext.Request.Form);
                                break;
                        }
                    }
                    break;
            }
            pContext.Response.Write(res);
            pContext.Response.End();
        }

        #region GetVisitingTaskPhoto
        private string GetVisitingTaskStepSum(NameValueCollection rParams)
        {
            int SumCount = 0;
            TaskStepSumViewEntity[] entityList = new VisitingTaskDataBLL(CurrentUserInfo).GetVisitingTaskStepSum(out SumCount);
            return string.Format("[{{\"totalCount\":{1},\"topics\":{0}}}]",
               entityList.ToJSON(),
               SumCount);
        }
        #endregion
    }
}