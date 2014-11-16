using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.WeiXin.KeyWord.Request;
using JIT.CPOS.DTO.Module.WeiXin.KeyWord.Response;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WX.KeyWord
{
    public class DeleteKeyWordAH : BaseActionHandler<DeleteKeyWordRP, DeleteKeyWordRD>
    {
        protected override DeleteKeyWordRD ProcessRequest(DTO.Base.APIRequest<DeleteKeyWordRP> pRequest)
        {
            var rd = new DeleteKeyWordRD();

            string replyId = pRequest.Parameters.ReplyId;


            var bll = new WKeywordReplyBLL(CurrentUserInfo);

            var entity = bll.QueryByEntity(new WKeywordReplyEntity()
            {
                ReplyId = replyId
            }, null);

            if (entity != null)
            {
                bll.Delete(entity);
            }
            else
            {
                throw new APIException("无效的关键字标识") { ErrorCode = 120 };
            }
            return rd;
        }
    }
}