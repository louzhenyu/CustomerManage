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

            if (string.IsNullOrWhiteSpace(para.StartTime))
                throw new APIException("请输入活动开始日期！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
            //卡
            if (para.VipCardTypeIDList == null || para.VipCardTypeIDList.Count == 0)
                throw new APIException("请选择目标人群！") {ErrorCode = ERROR_CODES.INVALID_BUSINESS};
            //对充值活动，不允许时间重叠
            if (ActivityBLL.IsActivityOverlap(loggingSessionInfo.ClientID, para.ActivityID, para.ActivityType, para.StartTime, para.EndTime, para.VipCardTypeIDList))
                throw new APIException("与已有活动时间重叠！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };

            //事务
            var pTran = ActivityBLL.GetTran();
            using (pTran.Connection)
            {
                try
                {                    
                    if (!string.IsNullOrWhiteSpace(para.ActivityID))
                    {
                        //编辑
                        C_ActivityEntity ChangeData = ActivityBLL.GetByID(para.ActivityID);                   
                        if (ChangeData == null)
                        {
                            throw new APIException("活动对象为NULL！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                        }

                        string m_OldStarTime = ChangeData.StartTime.Value.ToString("yyyy-MM-dd");

                        ChangeData.ActivityName = para.ActivityName;
                        ChangeData.IsAllVipCardType = int.Parse(para.IsAllCardType);
                        ChangeData.ActivityType = para.ActivityType;
                        ChangeData.StartTime = Convert.ToDateTime(para.StartTime);
                        if (!string.IsNullOrWhiteSpace(para.EndTime))
                            ChangeData.EndTime = Convert.ToDateTime(para.EndTime + " 23:59:59");
                        else
                            ChangeData.EndTime = DateTime.Parse("2099-01-01 23:59:59");
                        ChangeData.IsLongTime = para.IsLongTime;
                        ChangeData.CustomerID = loggingSessionInfo.ClientID;
                        ChangeData.TargetCount = 0;
                        //执行
                        ActivityBLL.Update(ChangeData, pTran);

                        #region 更新活动消息发送时间
                        List<C_ActivityMessageEntity> m_MessageList = C_ActivityMessageBLL.QueryByEntity(new C_ActivityMessageEntity() { ActivityID = new Guid(para.ActivityID) }, null).ToList();
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
                                    C_ActivityMessageBLL.Update(item, pTran);
                                }
                            }
                        }
                        #endregion

                        #region 目标群日信息
                        //卡
                        List<C_TargetGroupEntity> UpdateData = C_TargetGroupBLL.QueryByEntity(new C_TargetGroupEntity() { ActivityID = new Guid(para.ActivityID), GroupType = 1 }, null).ToList();
                        foreach (var i in UpdateData)
                        {
                            C_TargetGroupBLL.Delete(i, pTran);
                        }
                        foreach (var i in para.VipCardTypeIDList)
                        {
                            C_TargetGroupEntity AddTargetGroupData = new C_TargetGroupEntity();
                            AddTargetGroupData.ActivityID = ChangeData.ActivityID;
                            AddTargetGroupData.GroupType = 1;
                            AddTargetGroupData.ObjectID = i;
                            AddTargetGroupData.CustomerID = loggingSessionInfo.ClientID;
                            C_TargetGroupBLL.Create(AddTargetGroupData);
                        }
                        #endregion
                        rd.ActivityID = ChangeData.ActivityID.ToString();
                    }
                    else
                    {
                        //新增
                        if (!ActivityBLL.IsActivityNameValid(para.ActivityName))
                        {
                            throw new APIException("名称重复！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                        }
                        C_ActivityEntity AddData = new C_ActivityEntity();
                        AddData.ActivityID = System.Guid.NewGuid();
                        AddData.ActivityType = para.ActivityType;
                        AddData.ActivityName = para.ActivityName;
                        AddData.StartTime = Convert.ToDateTime(para.StartTime);
                        if (!string.IsNullOrWhiteSpace(para.EndTime))
                            AddData.EndTime = Convert.ToDateTime(para.EndTime + " 23:59:59");
                        else
                            AddData.EndTime = DateTime.Parse("2099-01-01 23:59:59");
                        AddData.IsLongTime = para.IsLongTime;
                        AddData.IsAllVipCardType = int.Parse(para.IsAllCardType);
                        AddData.SendCouponQty = 0;
                        AddData.Status = 5;
                        AddData.CustomerID = loggingSessionInfo.ClientID;
                        AddData.TargetCount = 0;
                        //执行
                        ActivityBLL.Create(AddData);
                        #region 新增目标群日信息
                        foreach (var i in para.VipCardTypeIDList)
                        {
                            C_TargetGroupEntity AddTargetGroupData = new C_TargetGroupEntity();
                            AddTargetGroupData.ActivityID = AddData.ActivityID;
                            AddTargetGroupData.GroupType = 1;
                            AddTargetGroupData.ObjectID = i;
                            AddTargetGroupData.CustomerID = loggingSessionInfo.ClientID;
                            C_TargetGroupBLL.Create(AddTargetGroupData, pTran);
                        }
                        #endregion

                        rd.ActivityID = AddData.ActivityID.ToString();
                    }
                    pTran.Commit();
                }
                catch (APIException apiEx)
                {
                    pTran.Rollback();
                    throw new APIException(apiEx.ErrorCode, apiEx.Message);
                }
            }
            return rd;
        }
    }
}