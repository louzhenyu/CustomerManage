using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;

namespace JIT.CPOS.Web.ApplicationInterface.Product.MobileLibrary.ActionHandler
{
    /// <summary>
    /// SetComment的摘要说明
    /// </summary>
    [Export(typeof(ILibraryRequestHandler))]
    [ExportMetadata("Action", "SetComment")]
    public class SetCommentHandler : ILibraryRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return SetComment(pRequest);
        }

        public string SetComment(string pRequest)
        {
            var rd = new APIResponse<SetCommentRD>();
            var rdData = new SetCommentRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<SetCommentRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            try
            {
                ItemCommentBLL commentBll = new ItemCommentBLL(loggingSessionInfo);
                string itemCommentId = string.IsNullOrEmpty(rp.Parameters.ItemCommentId) == true ? "-1" : rp.Parameters.ItemCommentId;
                ItemCommentEntity entity = commentBll.GetByID(itemCommentId);
                //commentBll.GetItemCommentByUser(rp.Parameters.OnlineCourseID, rp.UserID);
                if (entity == null)
                {
                    entity = new ItemCommentEntity()
                    {
                        ItemCommentId = Guid.NewGuid().ToString().Replace("-", ""),
                        CustomerId = rp.CustomerID,
                        ItemId = rp.Parameters.OnlineCourseID,
                        VipId = rp.UserID,
                        CommentContent = rp.Parameters.CommentContent,
                        Star = rp.Parameters.Star,
                        Topic = rp.Parameters.Topic
                    };
                    commentBll.Create(entity);
                }
                else
                {
                    entity.CommentContent = rp.Parameters.CommentContent;
                    entity.Topic = rp.Parameters.Topic;
                    entity.Star = rp.Parameters.Star;
                    commentBll.Update(entity);
                }
                rd.ResultCode = 0;
                rdData.IsSuccess = true;
                rdData.ItemCommentId = entity.ItemCommentId;
                //刷新课程评论星级
                RefreshCourseAvgScore(loggingSessionInfo, rp.Parameters.OnlineCourseID);
            }
            catch (Exception ex)
            {
                rd.ResultCode = 103;
                rdData.IsSuccess = false;
                rd.Message = ex.Message;
            }
            rd.Data = rdData;
            return rd.ToJSON();
        }

        /// <summary>
        /// 刷新分数
        /// </summary>
        /// <param name="pLoggingSessionInfo"></param>
        public void RefreshCourseAvgScore(LoggingSessionInfo pLoggingSessionInfo, string pOnlineCourseId)
        {
            try
            {
                ItemCommentBLL commentBll = new ItemCommentBLL(pLoggingSessionInfo);
                int avgStar = commentBll.GetCourseAvgStar(pOnlineCourseId);
                MLOnlineCourseBLL courseBll = new MLOnlineCourseBLL(pLoggingSessionInfo);
                MLOnlineCourseEntity entity = courseBll.GetByID(pOnlineCourseId);
                if (entity != null)
                {
                    entity.AverageStar = avgStar;
                    courseBll.Update(entity);
                }
            }
            catch (Exception ex)
            { }
        }
    }

    #region 发表评论
    public class SetCommentRP : IAPIRequestParameter
    {
        public string OnlineCourseID { set; get; }
        public string ItemCommentId { set; get; }
        public string Topic { set; get; }
        public string CommentContent { set; get; }
        public int Star { set; get; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(OnlineCourseID)) throw new APIException("OnlineCourseID不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(Topic)) throw new APIException("Topic不能为空") { ErrorCode = 102 };
            //if (string.IsNullOrEmpty(CommentContent)) throw new APIException("CommentContent不能为空") { ErrorCode = 102 };
            if (Star <= 0 || Star > 5) throw new APIException("Star不能合法") { ErrorCode = 102 };
        }
    }
    public class SetCommentRD : IAPIResponseData
    {
        public bool IsSuccess { set; get; }
        public string ItemCommentId { set; get; }
    }
    #endregion
}