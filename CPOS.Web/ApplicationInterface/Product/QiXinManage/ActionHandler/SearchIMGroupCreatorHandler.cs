using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
using System.Data;
using JIT.CPOS.Web.RateLetterInterface;

namespace JIT.CPOS.Web.ApplicationInterface.Product.QiXinManage.ActionHandler
{
    /// <summary>
    /// SearchIMGroupCreator的摘要说明
    /// </summary>
    [Export(typeof(IQiXinRequestHandler))]
    [ExportMetadata("Action", "SearchIMGroupCreator")]
    public class SearchIMGroupCreatorHandler : IQiXinRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return SearchIMGroupCreator(pRequest);
        }

        public string SearchIMGroupCreator(string pRequest)
        {
            var rd = new APIResponse<SearchIMGroupCreatorRD>();
            var rdData = new SearchIMGroupCreatorRD();

            var rp = pRequest.DeserializeJSONTo<APIRequest<SearchIMGroupCreatorRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            try
            {
                T_UserBLL bll = new T_UserBLL(loggingSessionInfo);
                DataTable dTbl = bll.GetIMGroupCreatorByUserName(rp.Parameters.Keyword, UserRightCode.USER_CREATE_GROUP_RIGHT_CODE);
                if (dTbl != null)
                {
                    rdData.IMGroupCreatorList = DataTableToObject.ConvertToList<PersonDetailInfo>(dTbl);
                    foreach (var item in rdData.IMGroupCreatorList)
                        item.IsIMGroupCreator = true;
                }
                rd.ResultCode = 0;
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

    #region 搜索拥有建群权限的员工
    public class SearchIMGroupCreatorRP : IAPIRequestParameter
    {
        public string Keyword { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(Keyword)) throw new APIException("Keyword不能为空") { ErrorCode = 102 };
        }
    }
    public class SearchIMGroupCreatorRD : IAPIResponseData
    {
        public List<PersonDetailInfo> IMGroupCreatorList { set; get; }
    }
    #endregion
}