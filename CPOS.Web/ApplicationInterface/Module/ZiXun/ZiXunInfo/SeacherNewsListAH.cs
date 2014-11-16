using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.ZiXun.ZiXunInfo.Request;
using JIT.CPOS.DTO.Module.ZiXun.ZiXunInfo.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.DTO.Base;
using JIT.Utility.Reflection;

namespace JIT.CPOS.Web.ApplicationInterface.Module.ZiXun.ZiXunInfo
{
    /// <summary>
    /// 查询咨询分类及列表
    /// </summary>
    public class SeacherNewsListAH : BaseActionHandler<SeacherZiXunRP, SeacherZiXunRD>
    {

        #region 错误码
        const int ERROR_LNEWS_NOCUSTOMERID = 110;
        const int ERROR_LNEWS_FAILURE = 330;
        #endregion

        protected override SeacherZiXunRD ProcessRequest(DTO.Base.APIRequest<SeacherZiXunRP> pRequest)
        {
            SeacherZiXunRD rd = new SeacherZiXunRD();
            rd.LNewsListByTypeList = new List<LNewsListByTypeEntity>();

            if (string.IsNullOrEmpty(pRequest.CustomerID))
                throw new APIException("客户ID为空") { ErrorCode = ERROR_LNEWS_NOCUSTOMERID };

            #region 查询咨询列表
            try
            {
                var codebll = new LNewsBLL(base.CurrentUserInfo);

                var ds = codebll.GetLNewsTypeList(pRequest.CustomerID);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    IList<LNewsTypeEntity> list = DataLoader.LoadFrom<LNewsTypeEntity>(ds.Tables[0]);

                    foreach (LNewsTypeEntity item in list)
                    {
                        var dss = codebll.GetLNewsList(pRequest.CustomerID, item.NewsTypeId);

                        LNewsListByTypeEntity info = new LNewsListByTypeEntity();
                        info.LNewsType = item.NewsTypeName;

                        if (dss.Tables.Count > 0 && dss.Tables[0].Rows.Count > 0)
                        {
                            info.LNewsList = DataLoader.LoadFrom<LNewsEntity>(dss.Tables[0]);
                        }

                        rd.LNewsListByTypeList.Add(info);
                    }
                }
            }
            catch (Exception)
            {
                throw new APIException("查询数据错误") { ErrorCode = ERROR_LNEWS_FAILURE };
            }
            #endregion

            return rd;
        }

    }


    #region 咨询类型
    public class LNewsTypeEntity
    {
        /// <summary>
        /// 咨询类型ID
        /// </summary>
        public string NewsTypeId { get; set; }
        /// <summary>
        /// 咨询类型
        /// </summary>
        public string NewsTypeName { get; set; }
    }
    #endregion

}