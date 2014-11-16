using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.Web.ApplicationInterface.Product.QiXinManage.ActionHandler
{
    /// <summary>
    /// QiXinManageLogin的摘要说明
    /// </summary>
    [Export(typeof(IQiXinRequestHandler))]
    [ExportMetadata("Action", "AddDept")]
    public class AddDeptHandler : IQiXinRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return AddDept(pRequest);
        }

        public string AddDept(string pRequest)
        {
            var rd = new APIResponse<AddDeptRD>();
            var rdData = new AddDeptRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<AddDeptRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

            try
            {
                TUnitBLL unitBll = new TUnitBLL(loggingSessionInfo);
                string typeID = System.Configuration.ConfigurationManager.AppSettings["TypeID"].ToString();
                if (!string.IsNullOrEmpty(rp.Parameters.TypeID))
                    typeID = rp.Parameters.TypeID;
                TUnitEntity[] unitEntityArr = unitBll.Query(new IWhereCondition[]
                    {
                        new EqualsCondition{DateTimeAccuracy = null, FieldName = "customer_id", Value =rp.CustomerID },
                        new EqualsCondition{DateTimeAccuracy = null, FieldName = "unit_name", Value =rp.Parameters.UnitName },
                        new EqualsCondition{DateTimeAccuracy = null, FieldName = "type_id", Value = typeID},
                        new EqualsCondition{DateTimeAccuracy = null, FieldName = "Status", Value ="1" }
                    }, null);
                if (unitEntityArr != null && unitEntityArr.Length > 0)
                {
                    rd.ResultCode = 101;
                    rd.Message = rp.Parameters.UnitName + "已存在";
                    return rd.ToJSON();
                }
                string unitID = Guid.NewGuid().ToString().Replace("-", "");
                TUnitEntity unitEntity = new TUnitEntity
                {
                    UnitID = unitID,
                    UnitName = rp.Parameters.UnitName,
                    UnitCode = rp.Parameters.UnitCode,
                    UnitContact = rp.Parameters.Leader,
                    UnitRemark = rp.Parameters.DeptDesc,
                    TypeID = typeID,
                    CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    CreateUserID = loggingSessionInfo.CurrentUser.User_Id,
                    ModifyTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    ModifyUserID = loggingSessionInfo.CurrentUser.User_Id,
                    CustomerID = loggingSessionInfo.CurrentUser.customer_id,
                    CUSTOMERLEVEL = 0,
                    Status = "1",
                    StatusDesc = "正常",
                    IfFlag = "1"
                };
                unitBll.Create(unitEntity);

                rdData.UnitID = unitID;
                rd.Data = rdData;
                rd.ResultCode = 0;
            }
            catch (Exception ex)
            {
                rd.ResultCode = 103;
                rd.Message = ex.Message;
            }
            return rd.ToJSON();
        }
    }

    #region 添加部门
    public class AddDeptRP : IAPIRequestParameter
    {
        public string TypeID { set; get; }
        public string UnitCode { set; get; }
        public string UnitName { set; get; }
        public string Leader { set; get; }
        public string DeptDesc { set; get; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(UnitCode))
                throw new APIException("【UnitCode】不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(UnitName))
                throw new APIException("【UnitName】不能为空") { ErrorCode = 102 };
        }
    }
    public class AddDeptRD : IAPIResponseData
    {
        public string UnitID { set; get; }
    }
    #endregion
}