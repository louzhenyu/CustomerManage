using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.WeiXin.KeyWord.Request;
using JIT.CPOS.DTO.Module.WeiXin.KeyWord.Response;
using JIT.CPOS.DTO.Module.WeiXin.Menu.Response;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WX.KeyWord
{
    public class GetKeyWordAH : BaseActionHandler<GetKeyWordRP, GetKeyWordRD>
    {
        protected override GetKeyWordRD ProcessRequest(DTO.Base.APIRequest<GetKeyWordRP> pRequest)
        {
            var rd = new GetKeyWordRD();
            string replyId = pRequest.Parameters.ReplyId;

            var bll = new WKeywordReplyBLL(CurrentUserInfo);
            var ds = bll.GetKeyWordListByReplyId(replyId);
            var textDs = bll.GetWMaterialTextByReplyId(replyId);

            if (ds.Tables[0].Rows.Count > 0)
            {
                var temp = ds.Tables[0].AsEnumerable().Select(t => new KeyWordInfo()
                {
                    ApplicationId =
                        string.IsNullOrEmpty(t["ApplicationId"].ToString()) ? "" : t["ApplicationId"].ToString(),
                    ReplyId = string.IsNullOrEmpty(t["ReplyId"].ToString()) ? "" : t["ReplyId"].ToString(),
                    KeyWord = string.IsNullOrEmpty(t["Keyword"].ToString()) ? "" : t["Keyword"].ToString(),
                    Text = string.IsNullOrEmpty(t["text"].ToString()) ? "" : t["text"].ToString(),
                    BeLinkedType =
                        string.IsNullOrEmpty(t["BeLinkedType"].ToString()) ? 0 : Convert.ToInt32(t["BeLinkedType"]),
                    DisplayIndex =
                        string.IsNullOrEmpty(t["DisplayIndex"].ToString()) ? -1 : Convert.ToInt32(t["DisplayIndex"]),
                    ReplyType =
                        string.IsNullOrEmpty(t["ReplyType"].ToString()) ? 0 : Convert.ToInt32(t["ReplyType"].ToString()),
                    KeywordType =
                        string.IsNullOrEmpty(t["KeywordType"].ToString())
                            ? 0
                            : Convert.ToInt32(t["KeywordType"].ToString()),
                    MaterialTextIds = textDs.Tables[0].AsEnumerable().Select(tt => new MaterialTextIdInfo()
                    {
                        TestId = string.IsNullOrEmpty(tt["TextId"].ToString()) ? "" : tt["TextId"].ToString(),
                        DisplayIndex =
                            string.IsNullOrEmpty(tt["DisplayIndex"].ToString())
                                ? 0
                                : Convert.ToInt32(tt["DisplayIndex"]),
                        ImageUrl =
                            string.IsNullOrEmpty(tt["CoverImageUrl"].ToString()) ? "" : tt["CoverImageUrl"].ToString(),
                        Title = string.IsNullOrEmpty(tt["Title"].ToString()) ? "" : tt["Title"].ToString(),
                        Author = string.IsNullOrEmpty(tt["Author"].ToString()) ? "" : tt["Author"].ToString(),
                        Text = string.IsNullOrEmpty(tt["Text"].ToString()) ? "" : tt["Text"].ToString(),
                        OriginalUrl = string.IsNullOrEmpty(tt["OriginalUrl"].ToString()) ? "" : tt["OriginalUrl"].ToString()
                    }).DefaultIfEmpty().ToArray()
                });

                rd.KeyWordList = temp.FirstOrDefault();
            }
            else
            {
                throw new APIException("无效的关键字标识") {ErrorCode = 120};
            }

            return rd;
        }
    }
}