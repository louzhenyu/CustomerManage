using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 签名字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SignatureFieldAttribute : Attribute
    {
    }
}
