using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Product.QiXinManage.ActionHandler.QiXinMock
{
    /// <summary>
    /// GetDeptDirectPersMembers的摘要说明
    /// </summary>
    [Export(typeof(IQiXinMockRequestHandler))]
    [ExportMetadata("Action", "GetDeptDirectPersMembers")]
    public class GetDeptDirectPersMembersHandler : IQiXinMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetDeptDirectPersMembers(pRequest);
        }

        public string GetDeptDirectPersMembers(string pRequest)
        {
            var rd = new APIResponse<GetDeptDirectPersMembersRD>();
            var rdData = new GetDeptDirectPersMembersRD();

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

            rdData.PersonMemberList = list;
            rd.Data = rdData;
            rd.ResultCode = 0;
            return rd.ToJSON();
        }
    }

    #region 获取部门的全部直接个人成员
    public class GetDeptDirectPersMembersRP : IAPIRequestParameter
    {
        public string Test { set; get; }
        public void Validate()
        {
        }
    }
    public class GetDeptDirectPersMembersRD : IAPIResponseData
    {
        public List<PersonListItemInfo> PersonMemberList { set; get; }
    }
    #endregion
}