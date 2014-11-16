using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface
{
    /// <summary>
    /// 接口开发示例
    /// </summary>
    public class DemoGateway : BaseGateway
    /*
     * 1.一个模块一个Handler
     * 2.Handler必须继承自BaseGateway
     */
    {
        #region 定义接口的请求参数及响应结果的数据结构

        #region 接口1
        /// <summary>
        /// 接口请求参数
        /// </summary>
        class DemoRP:IAPIRequestParameter
        {
            #region IAPIRequestParameter 成员
            /// <summary>
            /// 参数验证
            /// </summary>
            public void Validate()
            {
                
            }
            #endregion

            /// <summary>
            /// 定义参数数据
            /// </summary>
            public string P1 { get; set; }
        }

        /// <summary>
        /// 接口响应数据
        /// </summary>
        class DemoRD : IAPIResponseData
        {
            /// <summary>
            /// 响应结果数据
            /// </summary>
            public string R1 { get; set; }
        }
        #endregion 

        #endregion

        #region 接口处理逻辑
        /*
         * 接口处理最好一个接口一个处理方法,命令规则可为 Do+接口名
         */
        protected string DoDemo(string pRequest)
        {
            //1.反序列化请求参数对象
            var rp = pRequest.DeserializeJSONTo<APIRequest<DemoRP>>();
            //2.对请求参数进行验证
            if (rp.Parameters != null)
                rp.Parameters.Validate();
            //TODO something 

            //3.调用bll进行业务处理

            //TODO something

            //4.拼装响应结果
            var rd = new APIResponse<DemoRD>();
            //TODO something

            //5.将响应结果序列化为JSON并返回
            return rd.ToJSON();
        }
        #endregion

        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="pType"></param>
        /// <param name="pAction"></param>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            var rst = string.Empty;
            //1.根据type和action找到不同对应的处理程序
            switch (pAction)
            {
                case "Demo":
                    {
                        rst = this.DoDemo(pRequest);
                    }
                    break;
                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.",pAction)) { ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER };

            }
            //
            return rst;
        }
    }
}