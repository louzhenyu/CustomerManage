/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/26 17:13:41
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

namespace JIT.CPOS.DTO.ValueObject
{
    /// <summary>
    /// 接口类型 
    /// </summary>
    public enum APITypes
    {
        /// <summary>
        /// 产品接口
        /// </summary>
        Product
        ,
        /// <summary>
        /// 项目接口
        /// </summary>
        Project
        ,
        /// <summary>
        /// 演示接口
        /// </summary>
        Demo
    }

    /// <summary>
    /// APITypes的扩展方法类
    /// </summary>
    public static class APITypesExtensionMethods
    {
        /// <summary>
        /// 扩展方法：获取API类型的编码
        /// </summary>
        /// <param name="pCaller"></param>
        /// <returns></returns>
        public static string ToCode(this APITypes pCaller)
        {
            switch (pCaller)
            {
                case APITypes.Demo:
                    return "Demo";
                case APITypes.Product:
                    return "Product";
                case APITypes.Project:
                    return "Project";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
