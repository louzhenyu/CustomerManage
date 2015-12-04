﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.BLL;
using JIT.Utility.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Event.ContactEvent.Request;
using JIT.CPOS.DTO.Module.Event.ContactEvent.Response;
namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WEvents.ContactEvent
{
    public class SetContactEventAH : BaseActionHandler<SetContactEventRP, SetContactEventRD>
    {
        protected override SetContactEventRD ProcessRequest(DTO.Base.APIRequest<SetContactEventRP> pRequest)
        {
            var rd = new SetContactEventRD();

            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var bllContactEvent = new ContactEventBLL(loggingSessionInfo);
            var bllEvent = new LEventsBLL(loggingSessionInfo);
            
            try
            {
           
                
                
                if (para.ContactEventId != null && para.ContactEventId != "")
                {
                    var contactEvent = bllContactEvent.GetByID(para.ContactEventId);
                    //if (contactEvent.Status == 2 || contactEvent.Status==3)//状态为运行时只可追加数量
                    if (para.Method == "Append")
                    {
                        contactEvent.PrizeCount = (contactEvent.PrizeCount == null ? 0 : contactEvent.PrizeCount) + para.PrizeCount;
                    }
                    else
                    {
                        if (para.PrizeType == "Point")
                            contactEvent.Integral = para.Integral;
                        if (para.PrizeType == "Coupon")
                            contactEvent.CouponTypeID = para.CouponTypeID;
                        if (para.PrizeType == "Chance")
                        {
                            contactEvent.EventId = para.EventId;
                            contactEvent.ChanceCount = para.ChanceCount;
                        }

                        contactEvent.PrizeCount = para.PrizeCount;
                        contactEvent.ContactTypeCode = para.ContactTypeCode;
                        contactEvent.ContactEventName = para.ContactEventName;
                        contactEvent.BeginDate = para.BeginDate;
                        contactEvent.EndDate = para.EndDate;
                        contactEvent.PrizeType = para.PrizeType;
                        contactEvent.CustomerID = CurrentUserInfo.ClientID;
                        contactEvent.RewardNumber = para.RewardNumber;
                        contactEvent.ShareEventId = para.ShareEventId;
                        if (para.ContactTypeCode=="Share" && para.ShareEventId != null && para.ShareEventId != "")
                            bllEvent.UpdateEventIsShare(para.ShareEventId);

                    }
                    bllContactEvent.Update(contactEvent);
                    rd.ContactEventId = para.ContactEventId.ToString();

                }
                else
                {
                    ContactEventEntity entityContactEvent = new ContactEventEntity();

      
                    //RewardType:Point,Coupon,Chance
                    if (para.PrizeType == "Point")
                        entityContactEvent.Integral = para.Integral;
                    if (para.PrizeType == "Coupon")
                        entityContactEvent.CouponTypeID = para.CouponTypeID;
                    if (para.PrizeType == "Chance")
                    {
                        entityContactEvent.EventId = para.EventId;
                        entityContactEvent.ChanceCount = para.ChanceCount;
                    }
                                  
                    if (bllContactEvent.ExistsContact(entityContactEvent) > 0)
                    {
                        if (para.ContactTypeCode == "Share" && para.ShareEventId != null && para.ShareEventId.Length > 0)
                        {

                            rd.ErrMsg = "该分享活动已存在";
                        }
                        else
                        {
                            rd.ErrMsg = "有效期与已存在的触点活动有冲突";
                        }
                        rd.Success = false;
                        return rd;
                    }
                    else
                    {
                        if (para.ContactTypeCode == "Share" && para.ShareEventId != null && para.ShareEventId.Length > 0)
                        {
                            //判断触点中的分享设置的开始时间和结束时间是否在被分享的活动时间范围内
                            var entityEvent = bllEvent.GetByID(para.ShareEventId);
                            if (DateTime.Compare(Convert.ToDateTime(para.BeginDate), Convert.ToDateTime(entityEvent.BeginTime)) < 0 || DateTime.Compare(Convert.ToDateTime(para.EndDate), Convert.ToDateTime(entityEvent.EndTime)) > 0)
                            {
                                rd.Success = false;
                                rd.ErrMsg = "活动的时间不在被分享的活动时间范围内";
                                return rd;
                            }
                            entityContactEvent.ShareEventId = para.ShareEventId;
                            bllEvent.UpdateEventIsShare(para.ShareEventId);
                        }
                    }

                    entityContactEvent.PrizeCount = para.PrizeCount;
                    entityContactEvent.ContactTypeCode = para.ContactTypeCode;
                    entityContactEvent.ContactEventName = para.ContactEventName;
                    entityContactEvent.BeginDate = para.BeginDate;
                    entityContactEvent.EndDate = para.EndDate;
                    entityContactEvent.PrizeType = para.PrizeType;
                    entityContactEvent.CustomerID = CurrentUserInfo.ClientID;
                    entityContactEvent.RewardNumber = para.RewardNumber;
                    //开始日期是当天的 状态直接变为运行中
                    if (DateTime.Compare(Convert.ToDateTime(para.BeginDate),Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")))==0)
                    {
                        entityContactEvent.Status = 2;
                    }

                    bllContactEvent.Create(entityContactEvent);

                    ///保存奖品 生成奖品池
                    var entityPrize = new LPrizesEntity();
                    entityPrize.EventId = entityContactEvent.ContactEventId.ToString();
                    entityPrize.PrizeName = para.ContactEventName;
                    entityPrize.PrizeTypeId = para.PrizeType;
                    entityPrize.Point = para.Integral;
                    entityPrize.CouponTypeID = para.CouponTypeID;
                    entityPrize.CountTotal = para.PrizeCount;
                    entityPrize.CreateBy = loggingSessionInfo.UserID;

                   

                    bllContactEvent.AddContactEventPrize(entityPrize);

                    rd.ContactEventId = entityContactEvent.ContactEventId.ToString();

                }

            }
            catch (APIException apiEx)
            {
                throw new APIException(apiEx.ErrorCode, apiEx.Message);
            }

            return rd;
        }
    }
}