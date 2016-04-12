using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CPOS.Common.Core
{
    /// <summary>
    /// 单例   LM:2016/04/01
    /// </summary>
    public class LazyInstance<T> where T : new()
    {
        private static Lazy<T> lazyObj = new Lazy<T>(() => new T());

        public static T Instance
        {
            get
            {
                return lazyObj.Value;
            }
        }
    }
}
