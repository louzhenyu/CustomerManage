using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Product.QiXinManage.ActionHandler.QiXinMock
{
    /// <summary>
    /// GetDeptDirectSubDept的摘要说明
    /// </summary>
    [Export(typeof(IQiXinMockRequestHandler))]
    [ExportMetadata("Action", "GetDeptDirectSubDept")]
    public class GetDeptDirectSubDeptHandler : IQiXinMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetDeptDirectSubDept(pRequest);
        }

        public string GetDeptDirectSubDept(string pRequest)
        {
            var rd = new APIResponse<GetDeptDirectSubDeptRD>();
            var rdData = new GetDeptDirectSubDeptRD();
            List<DepartmentInfo> list = new List<DepartmentInfo>();
            DepartmentInfo dept = new DepartmentInfo
            {
                UnitID = "B0A786BC-F343-4748-A726-F31A0B164686",
                UnitName = "子部门1",
                Leader = "王明",
                ParentUnitID = ""
            };
            list.Add(dept);

            dept = new DepartmentInfo
            {
                UnitID = "C492F187-E151-4CCD-9C76-B80FD75A5B90",
                UnitName = "子部门2",
                Leader = "李华",
                ParentUnitID = ""
            };
            list.Add(dept);

            rdData.DepartmentList = list;
            rd.Data = rdData;
            rd.ResultCode = 0;
            return rd.ToJSON();
        }
    }

    #region 获取部门全部直接子部门
    public class GetDeptDirectSubDeptRP : IAPIRequestParameter
    {
        public string Test { set; get; }
        public void Validate()
        {
        }
    }
    public class GetDeptDirectSubDeptRD : IAPIResponseData
    {
        public List<DepartmentInfo> DepartmentList { set; get; }
    }
    #endregion
}