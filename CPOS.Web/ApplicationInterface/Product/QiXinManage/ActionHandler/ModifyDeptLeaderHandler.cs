using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;

namespace JIT.CPOS.Web.ApplicationInterface.Product.QiXinManage.ActionHandler
{
    /// <summary>
    /// ModifyDeptLeader的摘要说明
    /// </summary>
    [Export(typeof(IQiXinRequestHandler))]
    [ExportMetadata("Action", "ModifyDeptLeader")]
    public class ModifyDeptLeaderHandler : IQiXinRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return ModifyDeptLeader(pRequest);
        }

        public string ModifyDeptLeader(string pRequest)
        {
            var rd = new APIResponse<ModifyDeptLeaderRD>();
            var rdData = new ModifyDeptLeaderRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<ModifyDeptLeaderRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            try
            {
                TUnitBLL unitBll = new TUnitBLL(loggingSessionInfo);
                TUnitEntity entity = unitBll.GetByID(rp.Parameters.UnitID);
                if (entity != null)
                {
                    entity.UnitContact = rp.Parameters.Leader;
                    entity.ModifyTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    entity.ModifyUserID = rp.UserID;
                    unitBll.Update(entity);
                    rdData.IsSuccess = true;
                    rd.ResultCode = 0;
                }
                else
                {
                    rdData.IsSuccess = false;
                    rd.ResultCode = 101;
                    rd.Message = "部门不存在";
                }
            }
            catch (Exception ex)
            {
                rd.ResultCode = 103;
                rd.Message = ex.Message;
            }
            rd.Data = rdData;
            return rd.ToJSON();
        }
    }

    #region 变更部门Leader
    public class ModifyDeptLeaderRP : IAPIRequestParameter
    {
        public string UnitID { set; get; }
        public string Leader { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(UnitID)) throw new APIException("UnitID不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(Leader)) throw new APIException("Leader不能为空") { ErrorCode = 102 };
        }
    }
    public class ModifyDeptLeaderRD : IAPIResponseData
    {
        public bool IsSuccess { set; get; }
    }
    #endregion
}