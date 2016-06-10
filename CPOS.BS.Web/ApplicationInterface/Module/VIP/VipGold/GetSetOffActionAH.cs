using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Module.VIP.VipGold.Request;
using JIT.CPOS.DTO.Module.VIP.VipGold.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using System.Data;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.VipGold
{
    public class GetSetOffActionAH : BaseActionHandler<GetSetOffActionRP, GetSetOffActionRD>
    {
        protected override GetSetOffActionRD ProcessRequest(DTO.Base.APIRequest<GetSetOffActionRP> pRequest)
        {
            var rd = new GetSetOffActionRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var SetoffEventBll = new SetoffEventBLL(loggingSessionInfo);
            var SetoffToolsBll = new SetoffToolsBLL(loggingSessionInfo);
            var SetoffPosterBll = new SetoffPosterBLL(loggingSessionInfo);
            var IincentiveRuleBll = new IincentiveRuleBLL(loggingSessionInfo);
            //集客行动主数据
            var SetoffEventResult = SetoffEventBll.QueryByEntity(new SetoffEventEntity() { Status = "10", CustomerId=loggingSessionInfo.ClientID }, null).ToList();
            //
            rd.GetSetOffActionInfoList = new List<GetSetOffActionInfo>();
            foreach (var item in SetoffEventResult)
            {
                //规则
                var RuleData = IincentiveRuleBll.QueryByEntity(new IincentiveRuleEntity() { SetoffEventID = item.SetoffEventID, CustomerId=loggingSessionInfo.ClientID }, null).FirstOrDefault();

                var DataInfo = new GetSetOffActionInfo();
                DataInfo.SetoffEventID = item.SetoffEventID.ToString();
                DataInfo.SetoffType = Convert.ToInt32(item.SetoffType);
                if (RuleData != null)
                {
                    DataInfo.SetoffRegAwardType = RuleData.SetoffRegAwardType.Value;
                    DataInfo.SetoffRegPrize = RuleData.SetoffRegPrize ?? 0;
                    DataInfo.SetoffOrderPer = RuleData.SetoffOrderPer ?? 0;
                    DataInfo.SetoffOrderTimers = RuleData.SetoffOrderTimers ?? 0;
                    DataInfo.IincentiveRuleStatus = Convert.ToInt32(RuleData.Status);
                }
                //集客工具关系
                var ToolsData = SetoffToolsBll.QueryByEntity(new SetoffToolsEntity() { SetoffEventID = item.SetoffEventID, Status = "10", CustomerId=loggingSessionInfo.ClientID }, null).ToList();
                if (ToolsData != null)
                {
                    var ds = SetoffToolsBll.GetToolsDetails(item.SetoffEventID.ToString());
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        DataInfo.GetSetoffToolsInfoList = new List<GetSetoffToolsInfo>();
                        var dt = ds.Tables[0];
                        foreach (DataRow dr in dt.Rows)
                        {
                            #region 过滤
                            if (dr["ToolType"].ToString().Equals("Coupon"))
                            {
                                if (string.IsNullOrWhiteSpace(dr["Name"].ToString()))
                                    continue;

                                if (dr["BeginData"] == DBNull.Value && dr["EndData"] == DBNull.Value)
                                {
                                    if (dr["ServiceLife"] != DBNull.Value)
                                    {
                                        if (Convert.ToInt32(dr["ServiceLife"]) <= 0)
                                            continue;
                                    }
                                }

                                //if (dr["SurplusCount"] != DBNull.Value)
                                //{
                                //    if (Convert.ToInt32(dr["SurplusCount"]) <= 0)
                                //        continue;
                                //}

                            }
                            #endregion

                            var Data = new GetSetoffToolsInfo();
                            Data.SetoffToolID = dr["SetoffToolID"].ToString();
                            Data.ToolType = dr["ToolType"].ToString();
                            Data.Name = dr["Name"].ToString();
                            Data.SurplusCount = Convert.ToInt32(dr["SurplusCount"]);
                            if (dr["ServiceLife"] != DBNull.Value)
                                Data.ServiceLife = Convert.ToInt32(dr["ServiceLife"]);
                            if (dr["BeginData"] != DBNull.Value)
                                Data.BeginData = Convert.ToDateTime(dr["BeginData"]).ToString("yyyy年MM月dd日");
                            if (dr["EndData"] != DBNull.Value)
                                Data.EndData = Convert.ToDateTime(dr["EndData"]).ToString("yyyy年MM月dd日");
                            if (dr["SetoffPosterUrl"] != DBNull.Value)
                                Data.SetoffPosterUrl = dr["SetoffPosterUrl"].ToString();
                            if (dr["ObjectId"] != DBNull.Value)
                                Data.ObjectId = dr["ObjectId"].ToString();

                            DataInfo.GetSetoffToolsInfoList.Add(Data);//
                        }
                    }
                }
                //
                rd.GetSetOffActionInfoList.Add(DataInfo);
            }


            return rd;
        }
    }
}