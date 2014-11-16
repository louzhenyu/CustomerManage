using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
using System.Data;

namespace JIT.CPOS.Web.ApplicationInterface.Product.QiXinManage.ActionHandler
{
    /// <summary>
    /// SearchGroupByKeyword的摘要说明
    /// </summary>
    [Export(typeof(IQiXinRequestHandler))]
    [ExportMetadata("Action", "SearchGroupByKeyword")]
    public class SearchGroupByKeywordHandler : IQiXinRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return SearchGroupByKeyword(pRequest);
        }

        public string SearchGroupByKeyword(string pRequest)
        {
            var rd = new APIResponse<SearchGroupByKeywordRD>();
            var rdData = new SearchGroupByKeywordRD();

            var rp = pRequest.DeserializeJSONTo<APIRequest<SearchGroupByKeywordRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            try
            {
                IMGroupBLL groupBll = new IMGroupBLL(loggingSessionInfo);
                DataTable dTbl = groupBll.GetIMGroupByGroupName(rp.Parameters.Keyword);
                if (dTbl != null)
                    rdData.IMGroupInfoList = DataTableToObject.ConvertToList<IMGroupItemInfo>(dTbl);
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

    #region 关键子搜索群组
    public class SearchGroupByKeywordRP : IAPIRequestParameter
    {
        public string Keyword { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(Keyword)) throw new APIException("Keyword不能为空") { ErrorCode = 102 };
        }
    }
    public class SearchGroupByKeywordRD : IAPIResponseData
    {
        public List<IMGroupItemInfo> IMGroupInfoList { set; get; }
    }
    #endregion
}