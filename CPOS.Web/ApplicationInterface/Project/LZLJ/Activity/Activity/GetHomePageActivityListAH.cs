/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/4/17 15:30:55
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Project.LZLJ.Activity.Activity.Request;
using JIT.CPOS.DTO.Project.LZLJ.Activity.Activity.Response;
using JIT.CPOS.BS.BLL;

namespace JIT.CPOS.Web.ApplicationInterface.Project.LZLJ.Activity.Activity
{
    /// <summary>
    /// GetHomePageActivityListAH 
    /// </summary>
    public class GetHomePageActivityListAH : BaseActionHandler<GetHomePageActivityListRP, GetHomePageActivityListRD>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public GetHomePageActivityListAH()
        {
        }
        #endregion

        protected override GetHomePageActivityListRD ProcessRequest(APIRequest<GetHomePageActivityListRP> pRequest)
        {
            var bll = new LEventsBLL(this.CurrentUserInfo);
            GetHomePageActivityListRD rd = new GetHomePageActivityListRD();
            if (pRequest.Parameters.PageIndex.HasValue == false)
                pRequest.Parameters.PageIndex = 0;
            if (pRequest.Parameters.PageSize.HasValue == false)
                pRequest.Parameters.PageSize = 15;
            var datas = bll.GetHomePageActivityList(pRequest.Parameters.PageIndex.Value, pRequest.Parameters.PageSize.Value);
            if (datas != null)
            {
                rd.Items = datas.Select(item => new ActivityListItemInfo() { 
                     Title =item.Title
                     , ImageUrl =item.ImageURL
                     , LinkUrl =item.URL
                }).ToList();
            }
            //
            return rd;
        }
    }
}
