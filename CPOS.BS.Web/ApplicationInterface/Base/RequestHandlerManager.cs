using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Web.ApplicationInterface.Product.QiXinManage;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Base
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
        /// 存放所有QiXinRequestHandler的容器
        /// </summary>
        [ImportMany]
        private IEnumerable<Lazy<IQiXinRequestHandler, IQiXinRequestHandlerData>> QiXinRequestHandlers;

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
        /// 企信后台管理
        /// </summary>
        /// <param name="pAction"></param>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string HandleQiXinRequest(string pAction, string pRequest)
        {
            foreach (var i in QiXinRequestHandlers)
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