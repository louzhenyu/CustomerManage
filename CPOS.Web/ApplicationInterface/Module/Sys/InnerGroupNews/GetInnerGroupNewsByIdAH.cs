using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Sys.GetInnerGroupNewsList.Request;
using JIT.CPOS.DTO.Module.Sys.GetInnerGroupNewsList.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Sys.InnerGroupNews
{
    public class GetInnerGroupNewsByIdAH : BaseActionHandler<GetInnerGroupNewsByIdRP, GetInnerGroupNewsByIdRD>
    {
        /// <summary>
        /// 消息详情展示{业务：查看详情信息并且标识为已读账户}
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected override GetInnerGroupNewsByIdRD ProcessRequest(DTO.Base.APIRequest<GetInnerGroupNewsByIdRP> pRequest)
        {

            #region 设置参数
            var parameter = pRequest.Parameters;
            var rd = new GetInnerGroupNewsByIdRD();  //返回数据
            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(pRequest.CustomerID, pRequest.UserID);
            InnerGroupNewsBLL bll = new InnerGroupNewsBLL(loggingSessionInfo);
            NewsUserMappingBLL newsusermappingService = new NewsUserMappingBLL(loggingSessionInfo);
            #endregion

            var model = bll.GetVipInnerGroupNewsDetailsByPaging(parameter.Operationtype, pRequest.CustomerID, parameter.NoticePlatformTypeId, parameter.GroupNewsID);

            if (model == null || String.IsNullOrEmpty(model.GroupNewsId))
            {
                if (parameter.Operationtype == 1)  //0=当前消息 1=下一条消息 2=上一条消息
                {
                    throw new APIException("已经是最后一条消息啦。") { ErrorCode = 135 };
                }
                else if (parameter.Operationtype == 2)
                {
                    throw new APIException("已经是第一条消息啦。") { ErrorCode = 135 };
                }
            }

            var MessageList = bll.PagedQueryByEntity(new InnerGroupNewsEntity()
            {
                CustomerID = pRequest.CustomerID,
                NoticePlatformType = parameter.NoticePlatformTypeId,
                IsDelete = 0
            }, null, 1, 1); //分页获取数据

            rd.TotalPageCount = MessageList.RowCount; //获取总数据

            if (model != null)
            {
                //获取上一条数据 或者下一条 数据

                rd.NewsInfo = new InnerGroupNewsInfo() { Title = model.Title, Text = model.Text, CreateTime = model.CreateTime, GroupNewsId = model.GroupNewsId };

                bool IsRead = bll.CheckUserIsReadMessage(pRequest.UserID, pRequest.CustomerID, rd.NewsInfo.GroupNewsId);

                if (IsRead)
                {
                    NewsUserMappingEntity _model = new NewsUserMappingEntity() { CustomerId = pRequest.CustomerID, UserID = pRequest.UserID, GroupNewsID = rd.NewsInfo.GroupNewsId, HasRead = 1, IsDelete = 0 };
                    _model.GroupNewsID = rd.NewsInfo.GroupNewsId;
                    newsusermappingService.Create(_model);
                }

                if (parameter.Operationtype == 1)  //0=当前消息 1=下一条消息 2=上一条消息
                {
                    rd.PageIndex = -(model.PageIndex - rd.TotalPageCount) + 1;
                }
                else if (parameter.Operationtype == 2 || parameter.Operationtype == 0)
                {
                    rd.PageIndex = model.PageIndex;
                }
            }
            return rd;
        }
    }
}