/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/26 17:39:13
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web;

using JIT.Utility.ExtensionMethod;
using JIT.Utility.Web;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.ValueObject;


namespace JIT.CPOS.BS.Web.ApplicationInterface.Product
{
    /// <summary>
    /// API客户端代理
    /// </summary>
    public static class APIClientProxy
    {
        private static readonly string URL = string.Empty;

        private static readonly string URL2 = string.Empty;

        static APIClientProxy()
        {
            APIClientProxy.URL = ConfigurationManager.AppSettings["glt_service_url"];
        }

        /// <summary>
        /// 调用API
        /// </summary>
        /// <param name="pQueryString">查询字符串</param>
        /// <param name="pPostContent">请求内容</param>
        /// <returns></returns>
        public static string CallAPI(string pQueryString, string pPostContent)
        {
            var url = APIClientProxy.URL;
            if (!string.IsNullOrWhiteSpace(pQueryString))
            {
                if (pQueryString.StartsWith("?"))
                {
                    pQueryString = pQueryString.Substring(1);
                }
                url = url + pQueryString;
            }
            var rsp = HttpWebClient.DoHttpRequest(url, pPostContent);
            return rsp;
        }

        /// <summary>
        /// 调用API
        /// </summary>
        /// <typeparam name="TRequestParameter">接口参数类别</typeparam>
        /// <typeparam name="TResponseData">响应数据</typeparam>
        /// <param name="pType">接口类型</param>
        /// <param name="pAction">请求操作</param>
        /// <param name="pRequest">请求参数</param>
        /// <returns></returns>
        public static APIResponse<TResponseData> CallAPI<TRequestParameter, TResponseData>(APITypes pType, string pAction, APIRequest<TRequestParameter> pRequest)
            where TRequestParameter : IAPIRequestParameter, new()
            where TResponseData : IAPIResponseData
        {
            string queryString = string.Format("?type={0}&action={1}", pType.ToCode(), pAction);
            var content = string.Format("req={0}", HttpUtility.UrlEncode(pRequest.ToJSON()));
            var strRsp = APIClientProxy.CallAPI(queryString, content);
            if (!string.IsNullOrWhiteSpace(strRsp))
            {
                if (!string.IsNullOrWhiteSpace(pRequest.JSONP))
                {
                    strRsp = strRsp.Substring(pRequest.JSONP.Length + 1);
                    strRsp = strRsp.Substring(0, strRsp.Length - 1);
                }
                return strRsp.DeserializeJSONTo<APIResponse<TResponseData>>();
            }
            //
            return null;
        }

        public static APIResponse<TResponseData> CallAPI<TRequestParameter, TResponseData>(APITypes pType, string pPath, string pAction, APIRequest<TRequestParameter> pRequest)
            where TRequestParameter : IAPIRequestParameter, new()
            where TResponseData : IAPIResponseData
        {
            //string queryString = string.Format("?type={0}&action={1}", pType.ToCode(), pAction);
            string queryString = string.Format("{0}?type={1}&action={2}", pPath, pType.ToCode(), pAction);
            var content = string.Format("req={0}", HttpUtility.UrlEncode(pRequest.ToJSON()));
            var strRsp = APIClientProxy.CallAPI(queryString, content);
            if (!string.IsNullOrWhiteSpace(strRsp))
            {
                if (!string.IsNullOrWhiteSpace(pRequest.JSONP))
                {
                    strRsp = strRsp.Substring(pRequest.JSONP.Length + 1);
                    strRsp = strRsp.Substring(0, strRsp.Length - 1);
                }
                return strRsp.DeserializeJSONTo<APIResponse<TResponseData>>();
            }
            //
            return null;
        }

        /// <summary>
        /// 调用产品接口
        /// </summary>
        /// <typeparam name="TRequestParameter">接口参数类别</typeparam>
        /// <typeparam name="TResponseData">响应数据</typeparam>
        /// <param name="pAction">请求操作</param>
        /// <param name="pRequest">请求参数</param>
        /// <returns></returns>
        public static APIResponse<TResponseData> CallProductAPI<TRequestParameter, TResponseData>(string pAction, APIRequest<TRequestParameter> pRequest)
            where TRequestParameter : IAPIRequestParameter, new()
            where TResponseData : IAPIResponseData
        {
            return APIClientProxy.CallAPI<TRequestParameter, TResponseData>(APITypes.Product, pAction, pRequest);
        }

        /// <summary>
        /// 调用项目接口
        /// </summary>
        /// <typeparam name="TRequestParameter">接口参数类别</typeparam>
        /// <typeparam name="TResponseData">响应数据</typeparam>
        /// <param name="pAction">请求操作</param>
        /// <param name="pRequest">请求参数</param>
        /// <returns></returns>
        public static APIResponse<TResponseData> CallProjectAPI<TRequestParameter, TResponseData>(string pAction, APIRequest<TRequestParameter> pRequest)
            where TRequestParameter : IAPIRequestParameter, new()
            where TResponseData : IAPIResponseData
        {
            return APIClientProxy.CallAPI<TRequestParameter, TResponseData>(APITypes.Project, pAction, pRequest);
        }

        /// <summary>
        /// 调用Demo接口
        /// </summary>
        /// <typeparam name="TRequestParameter">接口参数类别</typeparam>
        /// <typeparam name="TResponseData">响应数据</typeparam>
        /// <param name="pAction">请求操作</param>
        /// <param name="pRequest">请求参数</param>
        /// <returns></returns>
        public static APIResponse<TResponseData> CallDemoAPI<TRequestParameter, TResponseData>(string pAction, APIRequest<TRequestParameter> pRequest)
            where TRequestParameter : IAPIRequestParameter, new()
            where TResponseData : IAPIResponseData
        {
            return APIClientProxy.CallAPI<TRequestParameter, TResponseData>(APITypes.Demo, pAction, pRequest);
        }
    }
}
