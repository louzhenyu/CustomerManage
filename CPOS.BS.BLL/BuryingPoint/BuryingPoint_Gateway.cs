using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace JIT.CPOS.BS.BLL
{
    public class BuryingPoint_Gateway<RP, RD> : BaseBuryingPoint
        where RP : IAPIRequestParameter
        where RD : IAPIResponseData
    {
        public BuryingPoint_Gateway(HttpContext context) : base(context)
        {
            //系统类型
            buryingPoint.SysType = context.Request.QueryString["type"];

            //action
            buryingPoint.Action = context.Request["action"];

            //请求参数
            buryingPoint.RequstParameters = context.Request["req"];
        }
        public void Context()
        {

        }
    }
}
