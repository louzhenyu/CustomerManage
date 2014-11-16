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
    /// ModifyDept的摘要说明
    /// </summary>
    [Export(typeof(IQiXinRequestHandler))]
    [ExportMetadata("Action", "ModifyDept")]
    public class ModifyDeptHandler : IQiXinRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return ModifyDept(pRequest);
        }

        public string ModifyDept(string pRequest)
        {
            var rd = new APIResponse<ModifyDeptRD>();
            var rdData = new ModifyDeptRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<ModifyDeptRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            try
            {
                TUnitBLL unitBll = new TUnitBLL(loggingSessionInfo);
                TUnitEntity entity = unitBll.GetByID(rp.Parameters.UnitID);
                if (entity == null)
                {
                    rdData.IsSuccess = false;
                    rd.Data = rdData;
                    rd.ResultCode = 101;
                    rd.Message = "部门不存在";
                    return rd.ToJSON();
                }
                entity.UnitName = rp.Parameters.UnitName;
                entity.UnitCode = rp.Parameters.UnitCode;
                if (!string.IsNullOrEmpty(rp.Parameters.Leader))
                    entity.UnitContact = rp.Parameters.Leader;
                if (!string.IsNullOrEmpty(rp.Parameters.DeptDesc))
                    entity.UnitRemark = rp.Parameters.DeptDesc;
                entity.ModifyUserID = rp.UserID;
                entity.ModifyTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                unitBll.Update(entity);

                rdData.IsSuccess = true;
                rd.Data = rdData;
                rd.ResultCode = 0;
            }
            catch (Exception ex)
            {
                rdData.IsSuccess = false;
                rd.Data = rdData;
                rd.ResultCode = 103;
                rd.Message = ex.Message;
            }
            return rd.ToJSON();
        }
    }

    #region 部门信息变更
    public class ModifyDeptRP : IAPIRequestParameter
    {
        public string UnitID { set; get; }
        public string UnitCode { set; get; }
        public string UnitName { set; get; }
        public string Leader { set; get; }
        public string DeptDesc { set; get; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(UnitID))
                throw new APIException("【UnitID】不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(UnitCode))
                throw new APIException("【UnitCode】不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(UnitName))
                throw new APIException("【UnitName】不能为空") { ErrorCode = 102 };
        }
    }
    public class ModifyDeptRD : IAPIResponseData
    {
        public bool IsSuccess { set; get; }
    }
    #endregion
}