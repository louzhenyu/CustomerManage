using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Sys.GetInnerGroupNewsList.Request;
using JIT.CPOS.DTO.Module.Sys.GetInnerGroupNewsList.Response;
using JIT.CPOS.DTO.Module.Sys.InnerGroupNews.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Sys.InnerGroupNews
{
    public class GetInnerGroupNewsCountAH : BaseActionHandler<GetInnerGroupNewsCountRP, GetInnerGroupNewsCountRD>
    {
        protected override GetInnerGroupNewsCountRD ProcessRequest(DTO.Base.APIRequest<GetInnerGroupNewsCountRP> pRequest)
        {
            var rd = new GetInnerGroupNewsCountRD();

            var parameter = pRequest.Parameters;

            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(pRequest.CustomerID, pRequest.UserID);
            InnerGroupNewsBLL InnerGroupNewsService = new InnerGroupNewsBLL(loggingSessionInfo);
            SetoffToolsBLL setofftoolService = new SetoffToolsBLL(loggingSessionInfo);
            int SetoffTypeId = 2;
            if (parameter.SetoffType != 0)
            {
                SetoffTypeId = parameter.SetoffType;
            }


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


            //获取站内信消息列表 默认过滤之前的消息
            var innergroupnewsdsCount = InnerGroupNewsService.GetVipInnerGroupNewsUnReadCount(pRequest.UserID, pRequest.CustomerID, parameter.NoticePlatformTypeId, null, CreateTime);
            rd.InnerGroupNewsCount = innergroupnewsdsCount;

            //获取集客工具列表
            var setofftooldsCount = setofftoolService.GetUnReadSetoffToolsCount(pRequest.UserID, pRequest.CustomerID, parameter.NoticePlatformTypeId, SetoffTypeId);
            rd.SetoffToolsCount = setofftooldsCount;
            return rd;
        }
    }
}