using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Product.QiXinManage.ActionHandler.QiXinMock
{
    /// <summary>
    /// GetDeptAllMembers的摘要说明
    /// </summary>
    [Export(typeof(IQiXinMockRequestHandler))]
    [ExportMetadata("Action", "GetDeptAllMembers")]
    public class GetDeptAllMembersHandler : IQiXinMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetDeptAllMembers(pRequest);
        }

        public string GetDeptAllMembers(string pRequest)
        {
            var rd = new APIResponse<GetDeptAllMembersRD>();
            var rdData = new GetDeptAllMembersRD();

            #region 根
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

            #endregion

            #region 子级1
            List<PersonListItemInfo> listPersSub = new List<PersonListItemInfo>();
            PersonListItemInfo memberSub = new PersonListItemInfo
            {
                UserID = "7C62A039D6FD4CC58F0E054394A78C7A",
                UserName = "王明",
                UnitName = "部门1子部门",
                JobFuncName = "ios工程师",
                UserEmail = "789@jitmarketing.cn"
            };
            listPersSub.Add(memberSub);

            List<DepartmentTotalMember> listDeptSub = new List<DepartmentTotalMember>();
            DepartmentTotalMember deptSub = new DepartmentTotalMember
            {
                UnitID = "A0B06B2D-3327-4C06-A8B0-FF17FD0B61E6",
                DeptDirectPersMemberList = listPersSub
            };
            listDeptSub.Add(deptSub);

            #endregion

            List<DepartmentTotalMember> list2 = new List<DepartmentTotalMember>();
            DepartmentTotalMember mb = new DepartmentTotalMember
            {
                UnitID = "B0A786BC-F343-4748-A726-F31A0B164686",
                DeptDirectPersMemberList = list,
                SubDepartmentList = listDeptSub
            };
            rdData.Member = list2;
            rd.Data = rdData;
            rd.ResultCode = 0;
            return rd.ToJSON();
        }
    }

    #region 获取部门及其各级子部门的所有成员
    public class GetDeptAllMembersRP : IAPIRequestParameter
    {
        public string Test { set; get; }
        public void Validate()
        {
        }
    }
    public class GetDeptAllMembersRD : IAPIResponseData
    {
        public List<DepartmentTotalMember> Member { set; get; }
    }
    #endregion
}