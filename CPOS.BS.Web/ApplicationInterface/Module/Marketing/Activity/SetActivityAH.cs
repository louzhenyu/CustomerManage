using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Marketing.Activity.Request;
using JIT.CPOS.DTO.Module.Marketing.Activity.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Marketing.Activity
{
    public class SetActivityAH : BaseActionHandler<SetActivityRP, SetActivityRD>
    {
        protected override SetActivityRD ProcessRequest(DTO.Base.APIRequest<SetActivityRP> pRequest)
        {
            var rd = new SetActivityRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            var ActivityBLL = new C_ActivityBLL(loggingSessionInfo);
            var C_TargetGroupBLL = new C_TargetGroupBLL(loggingSessionInfo);
            var C_ActivityMessageBLL = new C_ActivityMessageBLL(loggingSessionInfo);
            //事务
            var pTran = ActivityBLL.GetTran();
            
                try
                {
                    if (string.IsNullOrWhiteSpace(para.StartTime))
                        throw new APIException("请输入活动开始日期！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };

                    if (!string.IsNullOrWhiteSpace(para.ActivityID))
                    {
                        //编辑
                        C_ActivityEntity ChangeData = ActivityBLL.GetByID(para.ActivityID);
                        if (ChangeData == null)
                        {
                            throw new APIException("活动对象为NULL！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                        }
                        //
                        string m_OldStarTime = ChangeData.StartTime.Value.ToString("yyyy-MM-dd");

                        ChangeData.ActivityName = para.ActivityName;
                        ChangeData.StartTime = Convert.ToDateTime(para.StartTime);
                        if (!string.IsNullOrWhiteSpace(para.EndTime))
                            ChangeData.EndTime = Convert.ToDateTime(para.EndTime);
                        else
                            ChangeData.EndTime = null;
                        ChangeData.IsLongTime = para.IsLongTime;
                        if (!string.IsNullOrWhiteSpace(para.VipCardTypeID))
                            ChangeData.IsAllVipCardType = 1;
                        else
                            ChangeData.IsAllVipCardType = 0;
                        if (!string.IsNullOrWhiteSpace(para.VIPID))
                            ChangeData.IsVipGrouping = 1;
                        else
                            ChangeData.IsVipGrouping = 0;
                        ChangeData.CustomerID = loggingSessionInfo.ClientID;
                        //执行
                        ActivityBLL.Update(ChangeData);

                        #region 更新活动消息发送时间
                        List<C_ActivityMessageEntity> m_MessageList = C_ActivityMessageBLL.QueryByEntity(new C_ActivityMessageEntity (){ActivityID=new Guid(para.ActivityID) },null).ToList();
                        if (m_MessageList.Count > 0)
                        {
                            if (!m_OldStarTime.Equals(para.StartTime))
                            {
                                DateTime dt1 = Convert.ToDateTime(m_OldStarTime);
                                DateTime dt2 = Convert.ToDateTime(para.StartTime);
                                TimeSpan ts = (TimeSpan)(dt2 - dt1);
                                int s = Convert.ToInt32(ts.TotalDays);

                                foreach (var item in m_MessageList)
                                {
                                    item.SendTime = item.SendTime.Value.AddDays(s);
                                    C_ActivityMessageBLL.Update(item);
                                }
                            }
                        }
                        #endregion

                        #region 目标群日信息
                        //卡
                        if (ChangeData.IsAllVipCardType == 1)
                        {
                            if (string.IsNullOrWhiteSpace(para.VipCardTypeID))
                                throw new APIException("卡关联ID参数为NULL！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };


                            C_TargetGroupEntity UpdateData = C_TargetGroupBLL.QueryByEntity(new C_TargetGroupEntity() { ActivityID = new Guid(para.ActivityID), GroupType = 1 }, null).FirstOrDefault();
                            if (UpdateData != null)
                            {
                                UpdateData.ObjectID = para.VipCardTypeID;
                                C_TargetGroupBLL.Update(UpdateData);
                            }
                            else
                            {
                                C_TargetGroupEntity AddTargetGroupData = new C_TargetGroupEntity();
                                AddTargetGroupData.ActivityID = ChangeData.ActivityID;
                                AddTargetGroupData.GroupType = 1;
                                AddTargetGroupData.ObjectID = para.VipCardTypeID;
                                AddTargetGroupData.CustomerID = loggingSessionInfo.ClientID;
                                C_TargetGroupBLL.Create(AddTargetGroupData);
                            }
                        }
                        //会员
                        if (ChangeData.IsVipGrouping == 1)
                        {
                            if (string.IsNullOrWhiteSpace(para.VIPID))
                                throw new APIException("卡关联ID参数为NULL！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };


                            C_TargetGroupEntity UpdateData = C_TargetGroupBLL.QueryByEntity(new C_TargetGroupEntity() { ActivityID = new Guid(para.ActivityID), GroupType = 2 }, null).FirstOrDefault();
                            if (UpdateData != null)
                            {
                                UpdateData.ObjectID = para.VIPID;
                                C_TargetGroupBLL.Update(UpdateData);
                            }
                            else
                            {
                                C_TargetGroupEntity AddTargetGroupData = new C_TargetGroupEntity();
                                AddTargetGroupData.ActivityID = ChangeData.ActivityID;
                                AddTargetGroupData.GroupType = 2;
                                AddTargetGroupData.ObjectID = para.VipCardTypeID;
                                AddTargetGroupData.CustomerID = loggingSessionInfo.ClientID;
                                C_TargetGroupBLL.Create(AddTargetGroupData);
                            }


                        }
                        #endregion
                        rd.ActivityID = ChangeData.ActivityID.ToString();
                    }
                    else
                    {
                        //新增
                        C_ActivityEntity AddData = new C_ActivityEntity();
                        AddData.ActivityID = System.Guid.NewGuid();
                        AddData.ActivityType = para.ActivityType;
                        AddData.ActivityName = para.ActivityName;
                        AddData.StartTime = Convert.ToDateTime(para.StartTime);
                        if (!string.IsNullOrWhiteSpace(para.EndTime))
                            AddData.EndTime = Convert.ToDateTime(para.EndTime);
                        else
                            AddData.EndTime = null;
                        AddData.IsLongTime = para.IsLongTime==null?0:para.IsLongTime;
                        if (!string.IsNullOrWhiteSpace(para.VipCardTypeID))
                            AddData.IsAllVipCardType = 1;
                        else
                            AddData.IsAllVipCardType = 0;
                        if (!string.IsNullOrWhiteSpace(para.VIPID))
                            AddData.IsVipGrouping = 1;
                        else
                            AddData.IsVipGrouping = 0;
                        AddData.PointsMultiple = 0;
                        AddData.SendCouponQty = 0;
                        AddData.Status = 0;
                        AddData.CustomerID = loggingSessionInfo.ClientID;
                        //执行
                        ActivityBLL.Create(AddData);
                        #region 新增目标群日信息
                        //卡
                        if (AddData.IsAllVipCardType == 1)
                        {
                            if (string.IsNullOrWhiteSpace(para.VipCardTypeID))
                                throw new APIException("卡关联ID参数为NULL！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };

                            C_TargetGroupEntity AddTargetGroupData = new C_TargetGroupEntity();
                            AddTargetGroupData.ActivityID = AddData.ActivityID;
                            AddTargetGroupData.GroupType = 1;
                            AddTargetGroupData.ObjectID = para.VipCardTypeID;
                            AddTargetGroupData.CustomerID = loggingSessionInfo.ClientID;
                            C_TargetGroupBLL.Create(AddTargetGroupData);
                        }
                        //分组会员
                        if (AddData.IsVipGrouping == 1)
                        {
                            if (string.IsNullOrWhiteSpace(para.VIPID))
                                throw new APIException("卡关联ID参数为NULL！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };

                            C_TargetGroupEntity AddTargetGroupData = new C_TargetGroupEntity();
                            AddTargetGroupData.ActivityID = AddData.ActivityID;
                            AddTargetGroupData.GroupType = 2;
                            AddTargetGroupData.ObjectID = para.VIPID;
                            AddTargetGroupData.CustomerID = loggingSessionInfo.ClientID;
                            C_TargetGroupBLL.Create(AddTargetGroupData);
                        }
                        #endregion

                        rd.ActivityID = AddData.ActivityID.ToString();
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