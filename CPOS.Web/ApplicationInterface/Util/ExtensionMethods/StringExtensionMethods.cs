/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/26 15:52:26
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
using System.Text;

using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.Web.ApplicationInterface.Util.ExtensionMethods
{
    /// <summary>
    /// String的扩展方法
    /// </summary>
    public static class StringExtensionMethods
    {
        /// <summary>
        /// 扩展方法：将接口请求的JSON反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pCaller"></param>
        /// <returns></returns>
        public static APIRequest<T> DeserializeJSONToAPIRequest<T>(this string pCaller)
            where T : IAPIRequestParameter, new()
        {
            if (pCaller == null)
                throw new NullReferenceException();
            if (string.IsNullOrWhiteSpace(pCaller))
                return null;
            //
            return pCaller.DeserializeJSONTo<APIRequest<T>>();
        }
    }
}
