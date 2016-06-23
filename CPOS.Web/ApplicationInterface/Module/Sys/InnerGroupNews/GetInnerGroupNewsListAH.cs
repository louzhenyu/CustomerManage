using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Sys.InnerGroupNews.Request;
using JIT.CPOS.DTO.Module.Sys.InnerGroupNews.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Sys.InnerGroupNews
{
    public class GetInnerGroupNewsListAH : BaseActionHandler<GetInnerGroupNewsListRP, GetInnerGroupNewsListRD>
    {
        protected override GetInnerGroupNewsListRD ProcessRequest(DTO.Base.APIRequest<GetInnerGroupNewsListRP> pRequest)
        {
            var parameter = pRequest.Parameters;
            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(pRequest.CustomerID, pRequest.UserID);

            InnerGroupNewsBLL bll = new InnerGroupNewsBLL(loggingSessionInfo);

            var rd = new GetInnerGroupNewsListRD();

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
                    CreateTime = Convert.ToDateTime(T_SuperRetailTrader.CreateTime);

                    userinfo = new T_UserBLL(loggingSessionInfo).GetByID(T_SuperRetailTrader.SuperRetailTraderFromId);
                    if (userinfo != null)
                    {
                        CreateTime = Convert.ToDateTime(userinfo.create_time);
                    }

                    vipinfo = new VipBLL(loggingSessionInfo).GetByID(T_SuperRetailTrader.SuperRetailTraderFromId);
                    if (vipinfo != null)  //按照时间过滤
                    {
                        CreateTime = Convert.ToDateTime(vipinfo.CreateTime);
                    }
                }
            }
            PagedQueryResult<InnerGroupNewsEntity> lst = bll.GetVipInnerGroupNewsList(parameter.PageIndex, parameter.PageSize, pRequest.UserID, pRequest.CustomerID, parameter.NoticePlatformTypeId, parameter.BusType, CreateTime);
            rd.InnerGroupNewsList = lst.Entities.Select(m => new InnerGroupNewsInfo()
                                                        {
                                                            Title = m.Title,
                                                            IsRead = m.IsRead,
                                                            BusType = m.BusType,
                                                            GroupNewsId = m.GroupNewsId,
                                                            MsgTime = m.CreateTime + "",
                                                            Text = m.Text
                                                        }).ToList();

            rd.TotalCount = lst.RowCount;
            rd.TotalPages = lst.PageCount;
            return rd;
        }
    }
}