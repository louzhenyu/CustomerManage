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

namespace JIT.CPOS.BS.Web.ApplicationInterface.Vip
{
    /// <summary>
    /// WithdrawDeposit 的摘要说明
    /// </summary>
    public class WithdrawDepositGateway : BaseGateway
    {
        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst = string.Empty;
            switch (pAction)
            {
                case "GetWithdrawDeposit":  //提现记录列表
                    rst = GetWithdrawDeposit(pRequest);
                    break;
                case "UpdateWDApply":       //提现确认/完成操作
                    rst = UpdateWDApply(pRequest);
                    break;
                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                    {
                        ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                    };
            }
            return rst;
        }
        /// <summary>
        /// 提现记录列表
        /// </summary>
        /// <returns></returns>
        public string GetWithdrawDeposit(string pRequest)
        {

            return null;
            //return "{\"ResultCode\":0,\"Message\":\"OK\",\"IsSuccess\":true,\"Data\":{\"WithdrawDepositList\":[{\"ApplyID\":\"2E0AB4E2-53ED-4E19-90FE-8E1A56B324AB\",\"WithdrawNo\":\"2014123124030003\",\"ApplyDate\":\"2014-12-31\",\"Amount\":10,\"Status\":0,\"CompleteDate\":\"\",\"VipName\":\"demo1\",\"VipID\":\"0184281DFCAA41B5A5677FBC44BC12AA\"}],\"TotalPageCount\":1}}";
        }
        /// <summary>
        /// 提现确认/完成操作
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string UpdateWDApply(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<UpdateWDApplyRP>>();
            //var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var vipWDApplyBll = new VipWithdrawDepositApplyBLL(loggingSessionInfo);  //提现申请BLL实例化
            var vipWDApplyEntity = vipWDApplyBll.GetByID(rp.Parameters.ApplyID);     //获取提现申请对象
            if (vipWDApplyEntity != null)
            {
                switch (rp.Parameters.Status)
                {
                    case 1://已确认
                        vipWDApplyEntity.Status = 1;
                        vipWDApplyEntity.ConfirmDate = DateTime.Now;
                        break;
                    case 2://已完成
                        vipWDApplyEntity.Status = 1;
                        vipWDApplyEntity.CompleteDate = DateTime.Now;
                        break;
                }
                vipWDApplyBll.Update(vipWDApplyEntity); //修改
            }
            var rsp = new SuccessResponse<IAPIResponseData>();
            return rsp.ToJSON();
        }
    }

    #region 请求和返回参数
    /// <summary>
    /// 提现查询参数
    /// </summary>
    public class GetWDRP : IAPIRequestParameter
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string WithdrawNo { get; set; }
        /// <summary>
        /// 店员/会员名称
        /// </summary>
        public string VipName { get; set; }
        /// <summary>
        /// 提现申请状态(1=已确认；2=已完成)
        /// </summary>
        public int Status { get; set; }

        public void Validate()
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// 提现确认/完成操作参数
    /// </summary>
    public class UpdateWDApplyRP:IAPIRequestParameter{
         public string ApplyID { set; get; }
         public int Status { set; get; }
        public void Validate()
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}