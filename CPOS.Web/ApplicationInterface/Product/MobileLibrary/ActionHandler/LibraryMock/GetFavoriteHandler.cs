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
    /// GetFavorite的摘要说明
    /// </summary>
    [Export(typeof(ILibraryMockRequestHandler))]
    [ExportMetadata("Action", "GetFavorite")]
    public class GetFavoriteHandler : ILibraryMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetFavorite(pRequest);
        }

        public string GetFavorite(string pRequest)
        {
            var rd = new APIResponse<GetFavoriteRD>();
            var rdData = new GetFavoriteRD();
            List<OnlineCourse> list = new List<OnlineCourse>();
            OnlineCourse favorite = new OnlineCourse
            {
                OnlineCourseId = "1",
                Topic = "大数据的得与失",
                Icon = "icon",
                AccessCount = 10,
                AverageStar = 66,
                CourseType = 1,
                KeepType = "1"
            };
            list.Add(favorite);
            rdData.FavoriteList = list;
            rd.Data = rdData;
            rd.ResultCode = 0;
            return rd.ToJSON();
        }
    }

    #region 获取收藏列表
    public class GetFavoriteRP : IAPIRequestParameter
    {
        public int? PageIndex { set; get; }
        public int? PageSize { set; get; }
        public void Validate()
        {
            if (PageIndex == null) throw new APIException("PageIndex不能为空") { ErrorCode = 102 };
            if (PageSize == null) throw new APIException("PageSize不能为空") { ErrorCode = 102 };
        }
    }
    public class GetFavoriteRD : IAPIResponseData
    {
        public List<OnlineCourse> FavoriteList { set; get; }
    }
    #endregion
}