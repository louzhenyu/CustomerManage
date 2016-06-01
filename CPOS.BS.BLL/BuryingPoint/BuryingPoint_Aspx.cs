using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.BS.BLL
{
    public class BuryingPoint_Aspx : BaseBuryingPoint
    {
        private Action<BuryingPointEntity> parseCommonParameters;//自定义解析参数

        public BuryingPoint_Aspx(HttpContext context, DateTime requestTime, Action<BuryingPointEntity> parseCommonParameters, string returnData)
            : base(context)
        {
            this.parseCommonParameters = parseCommonParameters;
            this.returnData = returnData;
            Process();
        }

        //公众参数
        public override void CommonParameters()
        {
            if (parseCommonParameters != null)
            {
                parseCommonParameters(buryingPoint);
            }
        }
    }
}
