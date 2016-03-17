using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.Common;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.WeiXin.KeyWord.Request;
using JIT.CPOS.DTO.Module.WeiXin.KeyWord.Response;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WX.KeyWord
{
    public class SetKeyWordAH : BaseActionHandler<SetKeyWordRP, SetKeyWordRD>
    {
        protected override SetKeyWordRD ProcessRequest(DTO.Base.APIRequest<SetKeyWordRP> pRequest)
        {
            var rd = new SetKeyWordRD();
            var keywordList = pRequest.Parameters.KeyWordList;

             

            if (keywordList.DisplayIndex == 0)
            {
                throw new APIException("序号不能为空或为零") { ErrorCode = 122 };
            }
            if (string.IsNullOrEmpty(keywordList.KeyWord))
            {
                throw new APIException("关键字不能为空") { ErrorCode = 123 };
            }
            if (string.IsNullOrEmpty(keywordList.ApplicationId))
            {
                throw new APIException("微信公众号不能为空") { ErrorCode = 124 };
            }
            if (keywordList.BeLinkedType ==0)
            {
                throw new APIException("关联类型不能为空") { ErrorCode = 125 };
            }
            if (keywordList.ReplyType == 0)
            {
                throw new APIException("回复类型不能为空") { ErrorCode = 126 };
            }
            var bll = new WKeywordReplyBLL(CurrentUserInfo);

            #region 关键字不能重复
            WKeywordReplyEntity wKeywordReplyEntity = new WKeywordReplyEntity()
            {
                Keyword = keywordList.KeyWord,
                ApplicationId = keywordList.ApplicationId

            };
            var queryByEntityArray = bll.QueryByEntity(wKeywordReplyEntity, null);
            if (string.IsNullOrEmpty(keywordList.ReplyId))
            {                
                if (queryByEntityArray.Length != 0)
                {
                    throw new APIException("关键字不能重复") { ErrorCode = 127 };
                }
            }
            else
            {
                if (queryByEntityArray.Length != 0 && queryByEntityArray[0].ReplyId != keywordList.ReplyId)
                {
                    throw new APIException("关键字不能重复") { ErrorCode = 127 };
                }
            }
            #endregion

            var entity = new WKeywordReplyEntity
            {
                ApplicationId = keywordList.ApplicationId,
                Keyword = keywordList.KeyWord,
                ReplyType = keywordList.ReplyType,
                Text = keywordList.Text
            };

            if (keywordList.ReplyType == 1)
            {
                if (string.IsNullOrEmpty(keywordList.Text) || keywordList.Text == "")
                {
                    throw new APIException("文本不能为空") {ErrorCode = 120};
                }
                if (Encoding.Default.GetBytes(keywordList.Text).Length > 2048)
                {
                    throw new APIException("文本超过了最大限制（2M）") {ErrorCode = 121};
                }
            }
            if (keywordList.ReplyType == 3)
            {
                if (keywordList.MaterialTextIds == null || keywordList.MaterialTextIds.Any() == false)
                {
                    throw new APIException("图文消息不能为空") {ErrorCode = 124};
                }
                if (keywordList.MaterialTextIds.Any() == true && keywordList.MaterialTextIds.Length > 10)
                {
                    throw new APIException("图文消息最大不能超过10条数据") {ErrorCode = 125};
                }
            }


            if (string.IsNullOrEmpty(keywordList.ReplyId))
            {
                var replyId = Utils.NewGuid();
                entity.ReplyId = replyId;
                bll.Create(entity);

                rd.ReplyId = replyId;
            }
            else
            {
                entity.ReplyId = keywordList.ReplyId;
                bll.Update(entity);

                rd.ReplyId = keywordList.ReplyId;
            }

            bll.UpdateWkeywordReplyByReplyId(rd.ReplyId, keywordList.BeLinkedType, keywordList.KeywordType,
                keywordList.DisplayIndex);

            if (keywordList.ReplyType == 3)
            {
                var mappingBll = new WMenuMTextMappingBLL(CurrentUserInfo);
                var mappingEntity = mappingBll.QueryByEntity(new WMenuMTextMappingEntity()
                {
                    MenuId = rd.ReplyId
                }, null);

                if (mappingEntity.Length > 0)
                {
                    mappingBll.Delete(mappingEntity);
                }
                var textMappingEntity = new WMenuMTextMappingEntity();
                foreach (var materialTextIdInfo in keywordList.MaterialTextIds)
                {
                    textMappingEntity.MappingId = Guid.NewGuid();
                    textMappingEntity.MenuId = rd.ReplyId;
                    textMappingEntity.DisplayIndex = materialTextIdInfo.DisplayIndex;
                    textMappingEntity.TextId = materialTextIdInfo.TestId;
                    textMappingEntity.CustomerId = CurrentUserInfo.ClientID;
                    mappingBll.Create(textMappingEntity);
                }
            }
            return rd;
        }
    }
}