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
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetWDRP>>();
            //var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var vipWDApplyBll = new VipWithdrawDepositApplyBLL(loggingSessionInfo);  //提现申请BLL实例化

            //查询参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            if (!string.IsNullOrEmpty(rp.Parameters.WithdrawNo))  //提现申请编号
                complexCondition.Add(new LikeCondition() { FieldName = "a.WithdrawNo", Value = "%" + rp.Parameters.WithdrawNo + "%" });

            //下面要加上对分销商的兼容性*****
            if (!string.IsNullOrEmpty(rp.Parameters.VipName))
            {
                if (rp.Parameters.IsVip == 1)//会员
                    complexCondition.Add(new LikeCondition() { FieldName = "v.VipName", Value = "%" + rp.Parameters.VipName + "%" });
                else if (rp.Parameters.IsVip == 2)//店员
                    complexCondition.Add(new LikeCondition() { FieldName = "u.user_name", Value = "%" + rp.Parameters.VipName + "%" });
                else if (rp.Parameters.IsVip == 3)//分销商
                    complexCondition.Add(new LikeCondition() { FieldName = "u.RetailTraderName", Value = "%" + rp.Parameters.VipName + "%" });
            }
            if (!string.IsNullOrEmpty(rp.Parameters.Status))//判断状态
                complexCondition.Add(new EqualsCondition() { FieldName = "a.Status", Value = rp.Parameters.Status });
            complexCondition.Add(new EqualsCondition() { FieldName = "a.CustomerID", Value = loggingSessionInfo.ClientID });
            //排序参数
            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = "a.ApplyDate", Direction = OrderByDirections.Desc });
            //根据rp.Parameters.IsVip查询不同的数据源
            var wdApplyList = vipWDApplyBll.PagedQuery(complexCondition.ToArray(), lstOrder.ToArray(), rp.Parameters.PageSize, rp.Parameters.PageIndex + 1, rp.Parameters.IsVip);

            var rd = new WDManageInfoRD();
            rd.TotalPageCount = wdApplyList.PageCount;
            rd.WithdrawDepositList = wdApplyList.Entities.Select(t => new WDManageInfo() { ApplyID = t.ApplyID, WithdrawNo = t.WithdrawNo, ApplyDate = t.ApplyDate, VipName = t.VipName, VipId = t.VipID, Amount = t.Amount, Status = t.Status, CompleteDate = t.CompleteDate }).ToArray();

            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
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

            string[] applyIdArray = rp.Parameters.ApplyID.Split(',');
            if (applyIdArray.Length > 0)
            {
                foreach (var applyId in applyIdArray)
                {
                    var vipWDApplyEntity = vipWDApplyBll.GetByID(applyId);     //获取提现申请对象
                    if (vipWDApplyEntity != null)
                    {
                        switch (rp.Parameters.Status)
                        {
                            case 1://已确认
                                vipWDApplyEntity.Status = 1;
                                vipWDApplyEntity.ConfirmDate = DateTime.Now;
                                break;
                            case 2://已完成
                                vipWDApplyEntity.Status = 2;
                                vipWDApplyEntity.CompleteDate = DateTime.Now;
                                break;
                        }
                        vipWDApplyBll.Update(vipWDApplyEntity); //修改
                    }
                }
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
        public string Status { get; set; }
        /// <summary>
        /// 是否是会员(1=会员；2=店员)
        /// </summary>
        public int IsVip { get; set; }

        public void Validate()
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// 提现申请列表
    /// </summary>
    public class WDManageInfoRD : IAPIResponseData
    {
        public int TotalPageCount { get; set; }
        public WDManageInfo[] WithdrawDepositList { get; set; }
    }
    /// <summary>
    /// 提现申请对象
    /// </summary>
    public class WDManageInfo
    {
        public Guid? ApplyID { get; set; }
        public string WithdrawNo { get; set; }
        public DateTime? ApplyDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        /// <summary>
        /// 会员/店员ID
        /// </summary>
        public string VipName { get; set; }
        public string VipId { get; set; }
        public decimal? Amount { get; set; }
        /// <summary>
        /// 状态(0=待确认；1=已确认；2=已完成)
        /// </summary>
        public int? Status { get; set; }
    }
    /// <summary>
    /// 提现确认/完成操作参数
    /// </summary>
    public class UpdateWDApplyRP : IAPIRequestParameter
    {
        public string ApplyID { set; get; }
        public int Status { set; get; }
        public void Validate()
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}