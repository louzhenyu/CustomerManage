using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.Interface.Data.Base
{
    public class APIResponse
    {
        public string code { get; set; }        //		结果编码，200 操作成功
        public string description { get; set; } //	结果描述：操作成功
        public object content { get; set; }     //			传输内容

    }
}