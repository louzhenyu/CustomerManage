using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.Web.Interface.Data.Base
{
    public interface IValidCheck
    {
        /// <summary>
        /// 有效性检查
        /// </summary>
        /// <returns></returns>
        bool IsValid(out string msg);
    }
}
