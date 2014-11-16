using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.ZiXun.ZiXunInfo.Request;
using JIT.CPOS.DTO.Module.ZiXun.ZiXunInfo.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.Web.ApplicationInterface.Module.ZiXun.ZiXunInfo
{
    /// <summary>
    /// 添加咨询评论
    /// </summary>
    public class AddZiXunCommentAH : BaseActionHandler<AddZiXunCommentRP, AddZiXunCommentRD>
    {

        #region 错误码
        const int ERROR_COMMENT_FAILURE = 330;
        const int ERROR_COMMENT_NOCUSTOMERID = 110;
        #endregion

        protected override AddZiXunCommentRD ProcessRequest(DTO.Base.APIRequest<AddZiXunCommentRP> pRequest)
        {
            AddZiXunCommentRD rd = new AddZiXunCommentRD();

            pRequest.Parameters.Validate();

            if (string.IsNullOrEmpty(pRequest.CustomerID))
                throw new APIException("客户ID为空") { ErrorCode = ERROR_COMMENT_NOCUSTOMERID };

            #region 添加评论
            try
            {
                var bll = new LNewsCommentBLL(base.CurrentUserInfo);

                LNewsCommentEntity entity = new LNewsCommentEntity();
                entity.CustomerId = pRequest.CustomerID;
                entity.Content = pRequest.Parameters.Content;
                entity.VIPId = pRequest.Parameters.VIPId;
                entity.NewsId = pRequest.Parameters.NewsId;

                bll.Create(entity);
            }
            catch (Exception)
            {
                throw new APIException("添加数据错误") { ErrorCode = ERROR_COMMENT_FAILURE };
            }
            #endregion

            return rd;
        }


    }
}