using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using Aspose.Cells;
using JIT.Utility;
using JIT.CPOS.BS.Web.Base.Excel;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Entity;
using System.Web.Script.Serialization;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Vip
{
    /// <summary>
    /// VipTags 的摘要说明
    /// </summary>
    public class VipTags : BaseGateway
    {

        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst = string.Empty;
            switch (pAction)
            {
                //case "GetWithdrawDeposit":  //提现记录列表
                //    rst = GetWithdrawDeposit(pRequest);
                //    break;
                //case "UpdateWDApply":       //提现确认/完成操作
                //    rst = UpdateWDApply(pRequest);
                //    break;
                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                    {
                        ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                    };
            }
            return rst;
        }
    }
}