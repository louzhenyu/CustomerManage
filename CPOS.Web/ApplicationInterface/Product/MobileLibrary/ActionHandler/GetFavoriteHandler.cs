using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
using System.Data;

namespace JIT.CPOS.Web.ApplicationInterface.Product.MobileLibrary.ActionHandler
{
    /// <summary>
    /// GetFavorite的摘要说明
    /// </summary>
    [Export(typeof(ILibraryRequestHandler))]
    [ExportMetadata("Action", "GetFavorite")]
    public class GetFavoriteHandler : ILibraryRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetFavorite(pRequest);
        }

        public string GetFavorite(string pRequest)
        {
            var rd = new APIResponse<GetFavoriteRD>();
            var rdData = new GetFavoriteRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetFavoriteRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            try
            {
                MLOnlineCourseBLL courseBll = new MLOnlineCourseBLL(loggingSessionInfo);
                DataTable dTbl = courseBll.GetFavoriteCourse(rp.UserID, rp.Parameters.PageIndex, rp.Parameters.PageSize);// courseBll.GetOnlineCourse(courseType, rp.Parameters.SortKey, rp.Parameters.SortOrientation, rp.Parameters.PageIndex, rp.Parameters.PageSize);
                if (dTbl != null)
                    rdData.FavoriteList = DataTableToObject.ConvertToList<OnlineCourse>(dTbl);
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

    #region 获取收藏列表
    public class GetFavoriteRP : IAPIRequestParameter
    {
        public int PageIndex { set; get; }
        public int PageSize { set; get; }
        public void Validate()
        {
            if (PageSize <= 0) PageSize = 15;
        }
    }
    public class GetFavoriteRD : IAPIResponseData
    {
        public List<OnlineCourse> FavoriteList { set; get; }
    }
    #endregion
}