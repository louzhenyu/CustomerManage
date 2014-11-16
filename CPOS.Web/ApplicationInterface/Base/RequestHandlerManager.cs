using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.Web.ApplicationInterface.Project.Gree.ActionHandler;
using JIT.CPOS.Web.ApplicationInterface.Project.Gree.ActionHandler.GreeMock;
//using JIT.CPOS.Web.ApplicationInterface.Product.QiXinManage.ActionHandler.QiXinMock;
//using JIT.CPOS.Web.ApplicationInterface.Product.QiXinManage.ActionHandler;
using JIT.CPOS.Web.ApplicationInterface.Product.MobileLibrary.ActionHandler;
using JIT.CPOS.Web.ApplicationInterface.Product.MobileLibrary.ActionHandler.LibraryMock;
using JIT.CPOS.Web.ApplicationInterface.Product.MobileSurveyTest.ActionHandler.SurveyTestMock;
using JIT.CPOS.Web.ApplicationInterface.Product.MobileSurveyTest.ActionHandler;

namespace JIT.CPOS.Web.ApplicationInterface.Base
{
    /// <summary>
    /// Handler管理器，集中处理APP请求。单例模式。
    /// </summary>
    public sealed class RequestHandlerManager
    {
        /// <summary>
        /// 聚合容器，自动实现接口和实现配对
        /// </summary>
        private CompositionContainer _container;

        /// <summary>
        /// 存放所有GreeRequestHandler的容器
        /// </summary>
        [ImportMany]
        private IEnumerable<Lazy<IGreeRequestHandler, IGreeRequestHandlerData>> greeRequestHandlers;

        /// <summary>
        /// 存放所有QiXinRequestHandler的容器
        /// </summary>
        //[ImportMany]
        //private IEnumerable<Lazy<IQiXinRequestHandler, IQiXinRequestHandlerData>> QiXinRequestHandlers;

        /// <summary>
        /// 存放所有Mobile Library Handler的容器
        /// </summary>
        [ImportMany]
        private IEnumerable<Lazy<ILibraryRequestHandler, ILibraryRequestHandlerData>> MLibraryRequestHandlers;

        /// <summary>
        /// 存放所有Mobile Survey&Test Handler的容器
        /// </summary>
        [ImportMany]
        private IEnumerable<Lazy<ISurveyTestRequestHandler, ISurveyTestRequestHandlerData>> MSurveyTestRequestHandlers;

        /// <summary>
        /// 单例对象
        /// </summary>
        private static RequestHandlerManager _manager;

        /// <summary>
        /// 私有构造函数，用于创建单例对象
        /// </summary>
        private RequestHandlerManager()
        {
            var catalog = new AggregateCatalog();
            //Adds all the parts found in the same assembly as the Program class
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(RequestHandlerManager).Assembly));

            //Create the CompositionContainer with the parts in the catalog
            _container = new CompositionContainer(catalog);

            //Fill the imports of this object
            try
            {
                _container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }

        /// <summary>
        /// 单例属性
        /// </summary>
        public static RequestHandlerManager Instance
        {
            get { return _manager ?? (_manager = new RequestHandlerManager()); }
        }

        /// <summary>
        /// 处理请求的方法
        /// </summary>
        /// <param name="pAction">请求名</param>
        /// <param name="pRequest">参数</param>
        /// <returns></returns>
        public string HandleGreeRequest(string pAction, string pRequest)
        {
            foreach (var i in greeRequestHandlers)
            {
                // 用pAction和handler的元数据进行匹配
                if (i.Metadata.Action.Equals(pAction))
                {
                    return i.Value.DoAction(pRequest);
                }
            }

            throw new APIException(string.Format("未实现Action名为{0}的处理.", pAction)) { ErrorCode = 201 };
        }
        /// <summary>
        /// 企信后台管理
        /// </summary>
        /// <param name="pAction"></param>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        //public string HandleQiXinRequest(string pAction, string pRequest)
        //{
        //    foreach (var i in QiXinRequestHandlers)
        //    {
        //        // 用pAction和handler的元数据进行匹配
        //        if (i.Metadata.Action.Equals(pAction))
        //        {
        //            return i.Value.DoAction(pRequest);
        //        }
        //    }
        //    throw new APIException(string.Format("未实现Action名为{0}的处理.", pAction)) { ErrorCode = 201 };
        //}
        /// <summary>
        /// Mobile Library 
        /// </summary>
        /// <param name="pAction"></param>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string HandleMLibraryRequest(string pAction, string pRequest)
        {
            foreach (var i in MLibraryRequestHandlers)
            {
                // 用pAction和handler的元数据进行匹配
                if (i.Metadata.Action.Equals(pAction))
                {
                    return i.Value.DoAction(pRequest);
                }
            }
            throw new APIException(string.Format("未实现Action名为{0}的处理.", pAction)) { ErrorCode = 201 };
        }

        /// <summary>
        /// Mobile Survey&Test 
        /// </summary>
        /// <param name="pAction"></param>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string HandleMSurveyTestRequest(string pAction, string pRequest)
        {
            foreach (var i in MSurveyTestRequestHandlers)
            {
                // 用pAction和handler的元数据进行匹配
                if (i.Metadata.Action.Equals(pAction))
                {
                    return i.Value.DoAction(pRequest);
                }
            }
            throw new APIException(string.Format("未实现Action名为{0}的处理.", pAction)) { ErrorCode = 201 };
        }
    }
}