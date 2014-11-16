using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Product.QiXinManage.ActionHandler.QiXinMock
{
    /// <summary>
    /// GetDeptDirectMembers的摘要说明
    /// </summary>
    [Export(typeof(IQiXinMockRequestHandler))]
    [ExportMetadata("Action", "GetDeptDirectMembers")]
    public class GetDeptDirectMembersHandler : IQiXinMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetDeptDirectMembers(pRequest);
        }

        public string GetDeptDirectMembers(string pRequest)
        {
            var rd = new APIResponse<GetDeptDirectMembersRD>();
            var rdData = new GetDeptDirectMembersRD();

            List<PersonListItemInfo> list = new List<PersonListItemInfo>();
            PersonListItemInfo member = new PersonListItemInfo
            {
                UserID = "7C62A039D6FD4CC58F0E054394A78C7F",
                UserName = "王明",
                UnitName = "部门1",
                JobFuncName = ".net工程师",
                UserEmail = "123@jitmarketing.cn"
            };
            list.Add(member);
            member = new PersonListItemInfo
            {
                UserID = "7C62A039D6FD4CC58F0E054394A78C7G",
                UserName = "李华",
                UnitName = "部门1",
                JobFuncName = "ios工程师",
                UserEmail = "456@jitmarketing.cn"
            };
            list.Add(member);

            List<DepartmentTotalMember> list2 = new List<DepartmentTotalMember>();
            DepartmentTotalMember mb = new DepartmentTotalMember
            {
                UnitID = "B0A786BC-F343-4748-A726-F31A0B164686",
                DeptDirectPersMemberList = list
            };

            rdData.DeptDirectMemberList = list2;

            rd.Data = rdData;
            rd.ResultCode = 0;
            return rd.ToJSON();
        }
    }

    #region 获取部门直接成员
    public class GetDeptDirectMembersRP : IAPIRequestParameter
    {
        public string Test { set; get; }
        public void Validate()
        {
        }
    }
    public class GetDeptDirectMembersRD : IAPIResponseData
    {
        public List<DepartmentTotalMember> DeptDirectMemberList { set; get; }
    }
    #endregion
}