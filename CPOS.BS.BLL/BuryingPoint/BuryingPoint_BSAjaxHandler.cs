using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity.User;

namespace JIT.CPOS.BS.BLL
{
    public class BuryingPoint_BSAjaxHandler : BaseBuryingPoint
    {
        private UserInfo userInfo;//用户信息

        public BuryingPoint_BSAjaxHandler(HttpContext context, UserInfo userInfo)
            : base(context)
        {
            this.userInfo = userInfo;
        }

        /// <summary>
        /// 接口返回前，处理
        /// </summary>
        /// <param name="returnData"></param>
        public void Finish(string returnData)
        {
            this.returnData = returnData;
            Process();
        }

        //获取请求参数的公共参数部分
        public override void CommonParameters()
        {
            if (userInfo != null)
            {
                //商户
                buryingPoint.CustomerID = userInfo.customer_id;
                //用户
                buryingPoint.UserID = userInfo.User_Id;
            }
        }
    }
}
