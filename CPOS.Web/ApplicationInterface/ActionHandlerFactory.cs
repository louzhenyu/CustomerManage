/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/27 15:30:25
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
using System.Reflection;
using System.Text;

using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.ValueObject;
using JIT.CPOS.Web.ApplicationInterface.Base;

namespace JIT.CPOS.Web.ApplicationInterface
{
    /// <summary>
    /// 接口处理器工厂 
    /// </summary>
    public static class ActionHandlerFactory
    {
        /// <summary>
        /// 产品接口的处理类的基本命名空间
        /// </summary>
        const string PRODUCT_ACTION_HANDLER_BASE_NAMESPACE = "JIT.CPOS.Web.ApplicationInterface.Module";

        /// <summary>
        /// 项目接口的处理类的基本命名空间
        /// </summary>
        const string PROJECT_ACTION_HANDLER_BASE_NAMESPACE = "JIT.CPOS.Web.ApplicationInterface.Project";

        /// <summary>
        /// 演示接口的处理类的基本命名空间
        /// </summary>
        const string DEMO_ACTION_HANDLER_BASE_NAMESPACE = "JIT.CPOS.Web.ApplicationInterface.Demo";

        /// <summary>
        /// 获取处理器
        /// </summary>
        /// <param name="pType">接口类型</param>
        /// <param name="pAction">接口请求操作</param>
        /// <param name="pVersion">接口版本号</param>
        /// <returns>接口请求处理器</returns>
        public static IActionHandler GetActionHandler(APITypes pType, string pAction)
        {
            var sections = pAction.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            switch (pType)
            {
                case APITypes.Product:
                    #region 产品接口处理器创建
                    {
                        if (sections.Length < 3)
                        {
                            throw new APIException(ERROR_CODES.INVALID_REQUEST_INVALID_ACTION_FORMAT, "action值的格式错误,在产品接口下,action的格式为：[模块名].[对象名].[操作名]");
                        }
                        var className = string.Format("{0}.{1}.{2}.{3}AH", ActionHandlerFactory.PRODUCT_ACTION_HANDLER_BASE_NAMESPACE, sections[0], sections[1], sections[2]);
                        IActionHandler handler = null;
                        try
                        {
                            var oh = Activator.CreateInstance(null, className);
                            if (oh != null)
                            {
                                handler = oh.Unwrap() as IActionHandler;
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new APIException(ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER, string.Format("根据action找不到指定的ActionHandler类.原因：[{0}]", ex.Message), ex);
                        }
                        //
                        return handler;
                    }
                    #endregion
                case APITypes.Project:
                    #region 项目接口处理器创建
                    {
                        if (sections.Length < 4)
                        {
                            throw new APIException(ERROR_CODES.INVALID_REQUEST_INVALID_ACTION_FORMAT, "action值的格式错误,在产品接口下,action的格式为：[客户ID].[模块名].[对象名].[操作名]");
                        }
                        var className = string.Format("{0}.{1}.{2}.{3}.{4}AH", ActionHandlerFactory.PROJECT_ACTION_HANDLER_BASE_NAMESPACE, sections[0], sections[1], sections[2], sections[3]);
                        IActionHandler handler = null;
                        try
                        {
                            var oh = Activator.CreateInstance(null, className);
                            if (oh != null)
                            {
                                handler = oh.Unwrap() as IActionHandler;
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new APIException(ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER, string.Format("根据action找不到指定的ActionHandler类.原因：[{0}]", ex.Message), ex);
                        }
                        //
                        return handler;
                    }
                    #endregion
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
