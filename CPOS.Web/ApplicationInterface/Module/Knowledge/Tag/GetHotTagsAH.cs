using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.Knowledge.Tag.Request;
using JIT.CPOS.DTO.Module.Knowledge.Tag.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Knowledge.Tag
{
    /// <summary>
    /// 4.5.1	获取热门标签
    /// </summary>
    public class GetHotTagsAH : BaseActionHandler<GetHotTagsRP, GetHotTagsRD>
    {

        #region 错误码
        #endregion

        protected override GetHotTagsRD ProcessRequest(DTO.Base.APIRequest<GetHotTagsRP> pRequest)
        {
            GetHotTagsRD rd = new GetHotTagsRD();
            var bll = new KnowledgeTagBLL(CurrentUserInfo);
            var entitys = bll.GetByCount(pRequest.Parameters.Count);
            var list = entitys.OrderBy(t => t.DisplayIndex).Select(t => new KnowledgeTagInfo
            {
                Description = t.TagRemark,
                ID = t.KnowledgeTagId,
                Name = t.TagName
            }).ToArray();
            rd.Tags = list;
            return rd;
        }
    }
}
