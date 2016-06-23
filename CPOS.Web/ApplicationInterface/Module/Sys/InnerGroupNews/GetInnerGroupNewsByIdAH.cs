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


            //分页查找消息列表
            DateTime CreateTime = DateTime.Now;
            var vipinfo = new VipBLL(loggingSessionInfo).GetByID(loggingSessionInfo.CurrentUser.User_Id);
            if (vipinfo != null)  //按照时间过滤
            {
                CreateTime = Convert.ToDateTime(vipinfo.CreateTime);
            }
            var userinfo = new T_UserBLL(loggingSessionInfo).GetByID(loggingSessionInfo.CurrentUser.User_Id);
            if (userinfo != null)
            {
                CreateTime = Convert.ToDateTime(userinfo.create_time);
            }

            if (vipinfo == null && userinfo == null)
            {
                var T_SuperRetailTrader = new T_SuperRetailTraderBLL(loggingSessionInfo).GetByID(loggingSessionInfo.CurrentUser.User_Id);

                if (T_SuperRetailTrader != null)
                {
                    userinfo = new T_UserBLL(loggingSessionInfo).GetByID(T_SuperRetailTrader.SuperRetailTraderFromId);
                    if (userinfo != null)
                    {
                        CreateTime = Convert.ToDateTime(userinfo.create_time);
                    }
                    else
                    {
                        vipinfo = new VipBLL(loggingSessionInfo).GetByID(T_SuperRetailTrader.SuperRetailTraderFromId);
                        if (vipinfo != null)  //按照时间过滤
                        {
                            CreateTime = Convert.ToDateTime(vipinfo.CreateTime);
                        }
                    }
                }
            }

            var model = bll.GetVipInnerGroupNewsDetailsByPaging(parameter.Operationtype, pRequest.CustomerID, parameter.NoticePlatformTypeId, parameter.GroupNewsID, CreateTime);

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

            List<IWhereCondition> lstWhereCondition = new List<IWhereCondition>();
            lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pRequest.CustomerID });
            lstWhereCondition.Add(new EqualsCondition() { FieldName = "NoticePlatformType", Value = parameter.NoticePlatformTypeId });
            lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = "0" });
            lstWhereCondition.Add(new DirectCondition() { Expression = "CreateTime>='" + CreateTime + "'" });



            var MessageList = bll.PagedQuery(lstWhereCondition.ToArray(), null, 1, 1); //分页获取数据

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
                    rd.PageIndex = -(model.PageIndex - rd.TotalPageCount)+1;
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