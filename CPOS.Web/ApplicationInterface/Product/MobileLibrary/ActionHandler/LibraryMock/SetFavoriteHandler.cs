using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;

namespace JIT.CPOS.Web.ApplicationInterface.Product.MobileLibrary.ActionHandler.LibraryMock
{
    /// <summary>
    /// SetFavorite的摘要说明
    /// </summary>
    [Export(typeof(ILibraryMockRequestHandler))]
    [ExportMetadata("Action", "SetFavorite")]
    public class SetFavoriteHandler : ILibraryMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return SetFavorite(pRequest);
        }

        public string SetFavorite(string pRequest)
        {
            var rd = new APIResponse<SetFavoriteRD>();
            var rdData = new SetFavoriteRD();
            rdData.IsSuccess = true;
            rd.Data = rdData;
            rd.ResultCode = 0;
            return rd.ToJSON();
        }
    }

    #region 设置收藏
    public class SetFavoriteRP : IAPIRequestParameter
    {
        public string OnlineCourseID { set; get; }
        public string IsFavorite { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(OnlineCourseID)) throw new APIException("OnlineCourseID不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(IsFavorite)) throw new APIException("IsFavorite不能为空") { ErrorCode = 102 };
        }
    }
    public class SetFavoriteRD : IAPIResponseData
    {
        public bool IsSuccess { set; get; }
    }
    #endregion
}