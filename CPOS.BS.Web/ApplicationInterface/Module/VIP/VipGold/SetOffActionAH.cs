using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.VipGold.Request;
using JIT.CPOS.BS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.VipGold
{
    public class SetOffActionAH : BaseActionHandler<SetOffActionRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<SetOffActionRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var SetoffEventBll = new SetoffEventBLL(this.CurrentUserInfo);
            var IincentiveRuleBll = new IincentiveRuleBLL(this.CurrentUserInfo);
            var ObjectImagesBll = new ObjectImagesBLL(this.CurrentUserInfo);
            var SetoffToolsBll = new SetoffToolsBLL(this.CurrentUserInfo);
            var SetoffPosterBll = new SetoffPosterBLL(this.CurrentUserInfo);
            var CustomerBasicSettingBll = new CustomerBasicSettingBLL(this.CurrentUserInfo);
            //
            var pTran = SetoffEventBll.GetTran();
            string _CustomerId = this.CurrentUserInfo.ClientID;

            if (para.SetOffActionList.Count > 0)
            {
                //using (pTran.Connection)
                //{
                    try
                    {


                        foreach (var item in para.SetOffActionList)
                        {
                            
                            List<CustomerBasicSettingEntity> settingList = new List<CustomerBasicSettingEntity>();//基础配置数据

                            //获取已发布的集客行动
                            var SetoffEvenResult = SetoffEventBll.QueryByEntity(new SetoffEventEntity() { SetoffType = item.SetoffType, Status = "10" }, null).ToList();
                            if (SetoffEvenResult.Count > 1)
                                throw new APIException("数据异常，只能有一种集客行动！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };

                            if (SetoffEvenResult.Count > 0)
                            {
                                var Result = SetoffEvenResult.First();//集客行动
                                var RuleData = IincentiveRuleBll.QueryByEntity(new IincentiveRuleEntity() { SetoffEventID = Result.SetoffEventID }, null).FirstOrDefault();
                                if (RuleData == null)
                                    throw new APIException("数据异常，找不到奖励规则！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                                //判断奖励是否变动
                                if (item.SetoffRegAwardType != RuleData.SetoffRegAwardType ||
                                    item.SetoffRegPrize != RuleData.SetoffRegPrize || item.SetoffOrderPer != RuleData.SetoffOrderPer ||
                                    item.SetoffOrderTimers != RuleData.SetoffOrderTimers || item.IsEnabled != Convert.ToInt32(RuleData.Status))
                                {
                                    //原集客行动状态设置为失效
                                    SetoffEventBll.SetFailStatus(item.SetoffType);

                                    #region 规则变动，重新新建集客行动
                                    //集客行动
                                    //集客行动
                                    var SetoffEvenData = new SetoffEventEntity();//集客行动
                                    SetoffEvenData.SetoffEventID = System.Guid.NewGuid();
                                    SetoffEvenData.SetoffType = item.SetoffType;
                                    SetoffEvenData.Status = "10";
                                    SetoffEvenData.CustomerId = _CustomerId;
                                    SetoffEventBll.Create(SetoffEvenData);//

                                    var IincentiveRuleData = new IincentiveRuleEntity();//奖励规则
                                    IincentiveRuleData.IincentiveRuleID = System.Guid.NewGuid();
                                    IincentiveRuleData.SetoffEventID = SetoffEvenData.SetoffEventID;
                                    IincentiveRuleData.SetoffType = item.SetoffType;
                                    IincentiveRuleData.ApplyUnit = 0;
                                    IincentiveRuleData.SetoffRegAwardType = item.SetoffRegAwardType;
                                    IincentiveRuleData.SetoffRegPrize = item.SetoffRegPrize;
                                    IincentiveRuleData.SetoffOrderPer = item.SetoffOrderPer;
                                    IincentiveRuleData.SetoffOrderTimers = item.SetoffOrderTimers;
                                    IincentiveRuleData.Status = item.IsEnabled.ToString();
                                    IincentiveRuleData.CustomerId = _CustomerId;
                                    IincentiveRuleBll.Create(IincentiveRuleData);//

                                    #region 新增集客海报、海报集客工具关系
                                    foreach (var PosterItem in item.SetoffPosterList)
                                    {
                                        //图片表
                                        var ObjectImageData = new ObjectImagesEntity();
                                        ObjectImageData.ImageId = System.Guid.NewGuid().ToString("N");
                                        ObjectImageData.ImageURL = PosterItem.ImageUrl;
                                        ObjectImageData.Description = "集客海报";
                                        ObjectImageData.CustomerId = _CustomerId;
                                        ObjectImagesBll.Create(ObjectImageData);//
                                        //集客海报
                                        var SetoffPosterData = new SetoffPosterEntity();
                                        SetoffPosterData.SetoffPosterID = System.Guid.NewGuid();
                                        SetoffPosterData.Name = PosterItem.Name;
                                        SetoffPosterData.Desc = "集客海报";
                                        SetoffPosterData.ImageId = ObjectImageData.ImageId;
                                        SetoffPosterData.Status = "10";
                                        SetoffPosterData.CustomerId = _CustomerId;
                                        SetoffPosterBll.Create(SetoffPosterData);

                                        //海报工具关系
                                        var SetoffToolsData = new SetoffToolsEntity();
                                        SetoffToolsData.SetoffToolID = System.Guid.NewGuid();
                                        SetoffToolsData.SetoffEventID = SetoffEvenData.SetoffEventID;
                                        SetoffToolsData.ToolType = "SetoffPoster";
                                        SetoffToolsData.ObjectId = SetoffPosterData.SetoffPosterID.ToString();
                                        SetoffToolsData.Status = "10";
                                        SetoffToolsData.CustomerId = _CustomerId;
                                        SetoffToolsBll.Create(SetoffToolsData);//
                                    }
                                    #endregion

                                    #region 新增集客工具关系
                                    foreach (var ToolsItem in item.SetoffTools)
                                    {
                                        //判断是否添加重复集客工具
                                        bool Flag = SetoffEventBll.IsToolsRepeat(SetoffEvenData.SetoffEventID.ToString(), ToolsItem.ObjectId);
                                        if (Flag)
                                            throw new APIException("集客行动不能添加相同的集客工具!") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };

                                        var SetoffToolsData = new SetoffToolsEntity();
                                        SetoffToolsData.SetoffToolID = System.Guid.NewGuid();
                                        SetoffToolsData.SetoffEventID = SetoffEvenData.SetoffEventID;
                                        SetoffToolsData.ToolType = ToolsItem.ToolType;
                                        SetoffToolsData.ObjectId = ToolsItem.ObjectId;
                                        SetoffToolsData.Status = "10";
                                        SetoffToolsData.CustomerId = _CustomerId;
                                        SetoffToolsBll.Create(SetoffToolsData);//
                                    }
                                    #endregion

                                    #endregion

                                    #region 更新配置奖励系数
                                    //集客销售订单分润配置
                                    //var SettingData = CustomerBasicSettingBll.QueryByEntity(new CustomerBasicSettingEntity() { SettingCode = "GetVipUserOrderPer", CustomerID = CurrentUserInfo.ClientID, IsDelete = 0 }, null).FirstOrDefault();
                                    //if (SettingData != null)
                                    //{
                                    //    SettingData.SettingValue = IincentiveRuleData.SetoffOrderPer.ToString();
                                    //    CustomerBasicSettingBll.Update(SettingData);
                                    //}
                                    if (IincentiveRuleData.SetoffType == 2)
                                    {
                                        settingList.Add(new CustomerBasicSettingEntity()
                                        {
                                            SettingCode = "GetVipUserOrderPer",
                                            SettingValue = (IincentiveRuleData.SetoffOrderPer ?? 0).ToString()
                                        });
                                      
                                    }
                                    if (IincentiveRuleData.SetoffRegAwardType == 2)
                                    {
                                        settingList.Add(new CustomerBasicSettingEntity()
                                        {
                                            SettingCode = "InvitePartnersPoints",
                                            SettingValue = (IincentiveRuleData.SetoffRegPrize ?? 0).ToString()
                                        });
                                    }
                                    CustomerBasicSettingBll.SaveCustomerBasicInfo(loggingSessionInfo.ClientID, settingList);
                                    //集客注册奖励配置
                                    #endregion
                                }
                                else
                                {
                                    #region 新增集客海报、海报集客工具关系
                                    foreach (var PosterItem in item.SetoffPosterList)
                                    {
                                        //图片表
                                        var ObjectImageData = new ObjectImagesEntity();
                                        ObjectImageData.ImageId = System.Guid.NewGuid().ToString("N");
                                        ObjectImageData.ImageURL = PosterItem.ImageUrl;
                                        ObjectImageData.Description = "集客海报";
                                        ObjectImageData.CustomerId = _CustomerId;
                                        ObjectImagesBll.Create(ObjectImageData);//
                                        //集客海报
                                        var SetoffPosterData = new SetoffPosterEntity();
                                        SetoffPosterData.SetoffPosterID = System.Guid.NewGuid();
                                        SetoffPosterData.Name = PosterItem.Name;
                                        SetoffPosterData.Desc = "集客海报";
                                        SetoffPosterData.ImageId = ObjectImageData.ImageId;
                                        SetoffPosterData.Status = "10";
                                        SetoffPosterData.CustomerId = _CustomerId;
                                        SetoffPosterBll.Create(SetoffPosterData);

                                        //海报工具关系
                                        var SetoffToolsData = new SetoffToolsEntity();
                                        SetoffToolsData.SetoffToolID = System.Guid.NewGuid();
                                        SetoffToolsData.SetoffEventID = Result.SetoffEventID;
                                        SetoffToolsData.ToolType = "SetoffPoster";
                                        SetoffToolsData.ObjectId = SetoffPosterData.SetoffPosterID.ToString();
                                        SetoffToolsData.Status = "10";
                                        SetoffToolsData.CustomerId = _CustomerId;
                                        SetoffToolsBll.Create(SetoffToolsData);//
                                    }
                                    #endregion

                                    #region 新增集客工具关系
                                    foreach (var ToolsItem in item.SetoffTools)
                                    {
                                        //
                                        if(Result==null)
                                            throw new APIException("数据异常，未找到集客行动!") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                                        //判断是否添加重复集客工具
                                        bool Flag = SetoffEventBll.IsToolsRepeat(Result.SetoffEventID.ToString(), ToolsItem.ObjectId);
                                        if (Flag)
                                            throw new APIException("集客行动不能添加相同的集客工具!") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };

                                        var SetoffToolsData = new SetoffToolsEntity();
                                        SetoffToolsData.SetoffToolID = System.Guid.NewGuid();
                                        SetoffToolsData.SetoffEventID = Result.SetoffEventID;
                                        SetoffToolsData.ToolType = ToolsItem.ToolType;
                                        SetoffToolsData.ObjectId = ToolsItem.ObjectId;
                                        SetoffToolsData.Status = "10";
                                        SetoffToolsData.CustomerId = _CustomerId;
                                        SetoffToolsBll.Create(SetoffToolsData);//
                                    }
                                    #endregion
                                }
                            }
                            else
                            {
                                //原集客行动状态设置为失效
                                SetoffEventBll.SetFailStatus(item.SetoffType);

                                #region 新建集客行动
                                //集客行动
                                var SetoffEvenData = new SetoffEventEntity();//集客行动
                                SetoffEvenData.SetoffEventID = System.Guid.NewGuid();
                                SetoffEvenData.SetoffType = item.SetoffType;
                                SetoffEvenData.Status = "10";
                                SetoffEvenData.CustomerId = _CustomerId;
                                SetoffEventBll.Create(SetoffEvenData);//

                                var IincentiveRuleData = new IincentiveRuleEntity();//奖励规则
                                IincentiveRuleData.IincentiveRuleID = System.Guid.NewGuid();
                                IincentiveRuleData.SetoffEventID = SetoffEvenData.SetoffEventID;
                                IincentiveRuleData.SetoffType = item.SetoffType;
                                IincentiveRuleData.ApplyUnit = 0;
                                IincentiveRuleData.SetoffRegAwardType = item.SetoffRegAwardType;
                                IincentiveRuleData.SetoffRegPrize = item.SetoffRegPrize;
                                IincentiveRuleData.SetoffOrderPer = item.SetoffOrderPer;
                                IincentiveRuleData.SetoffOrderTimers = item.SetoffOrderTimers;
                                IincentiveRuleData.Status = item.IsEnabled.ToString();
                                IincentiveRuleData.CustomerId = _CustomerId;
                                IincentiveRuleBll.Create(IincentiveRuleData);//

                                #region 新增集客海报、海报集客工具关系
                                foreach (var PosterItem in item.SetoffPosterList)
                                {
                                    //图片表
                                    var ObjectImageData = new ObjectImagesEntity();
                                    ObjectImageData.ImageId = System.Guid.NewGuid().ToString("N");
                                    ObjectImageData.ImageURL = PosterItem.ImageUrl;
                                    ObjectImageData.Description = "集客海报";
                                    ObjectImageData.CustomerId = _CustomerId;
                                    ObjectImagesBll.Create(ObjectImageData);//
                                    //集客海报
                                    var SetoffPosterData = new SetoffPosterEntity();
                                    SetoffPosterData.SetoffPosterID = System.Guid.NewGuid();
                                    SetoffPosterData.Name = PosterItem.Name;
                                    SetoffPosterData.Desc = "集客海报";
                                    SetoffPosterData.ImageId = ObjectImageData.ImageId;
                                    SetoffPosterData.Status = "10";
                                    SetoffPosterData.CustomerId = _CustomerId;
                                    SetoffPosterBll.Create(SetoffPosterData);

                                    //海报工具关系
                                    var SetoffToolsData = new SetoffToolsEntity();
                                    SetoffToolsData.SetoffToolID = System.Guid.NewGuid();
                                    SetoffToolsData.SetoffEventID = SetoffEvenData.SetoffEventID;
                                    SetoffToolsData.ToolType = "SetoffPoster";
                                    SetoffToolsData.ObjectId = SetoffPosterData.SetoffPosterID.ToString();
                                    SetoffToolsData.Status = "10";
                                    SetoffToolsData.CustomerId = _CustomerId;
                                    SetoffToolsBll.Create(SetoffToolsData);//
                                }
                                #endregion

                                #region 新增集客工具关系
                                foreach (var ToolsItem in item.SetoffTools)
                                {
                                    //判断是否添加重复集客工具
                                    bool Flag= SetoffEventBll.IsToolsRepeat(SetoffEvenData.SetoffEventID.ToString(), ToolsItem.ObjectId);
                                    if (Flag)
                                        throw new APIException("集客行动不能添加相同的集客工具!") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };

                                    var SetoffToolsData = new SetoffToolsEntity();
                                    SetoffToolsData.SetoffToolID = System.Guid.NewGuid();
                                    SetoffToolsData.SetoffEventID = SetoffEvenData.SetoffEventID;
                                    SetoffToolsData.ToolType = ToolsItem.ToolType;
                                    SetoffToolsData.ObjectId = ToolsItem.ObjectId;
                                    SetoffToolsData.Status = "10";
                                    SetoffToolsData.CustomerId = _CustomerId;
                                    SetoffToolsBll.Create(SetoffToolsData);//
                                }
                                #endregion


                                #endregion


                                #region 更新配置奖励系数

                                if (IincentiveRuleData.SetoffType == 2)
                                {
                                    //同步到原CustomerBasicSetting集客销售订单分润配置
                                    //var SettingData = CustomerBasicSettingBll.QueryByEntity(new CustomerBasicSettingEntity() { SettingCode = "GetVipUserOrderPer", CustomerID = CurrentUserInfo.ClientID, IsDelete = 0 }, null).FirstOrDefault();
                                    //if (SettingData != null)
                                    //{
                                    //    SettingData.SettingValue = (IincentiveRuleData.SetoffOrderPer ?? 0).ToString();
                                    //    CustomerBasicSettingBll.Update(SettingData);
                                    //}
                                    settingList.Add(new CustomerBasicSettingEntity()
                                    {
                                        SettingCode = "GetVipUserOrderPer",
                                        SettingValue = (IincentiveRuleData.SetoffOrderPer ?? 0).ToString()
                                    });

                                }
                                 if (IincentiveRuleData.SetoffRegAwardType == 2)
                                {
                                    //同步到原CustomerBasicSetting集客销售订单分润配置
                                    //var SettingData = CustomerBasicSettingBll.QueryByEntity(new CustomerBasicSettingEntity() { SettingCode = "InvitePartnersPoints", CustomerID = CurrentUserInfo.ClientID, IsDelete = 0 }, null).FirstOrDefault();
                                    //if (SettingData != null)
                                    //{
                                    //    SettingData.SettingValue = (IincentiveRuleData.SetoffRegPrize ?? 0).ToString();
                                    //    CustomerBasicSettingBll.Update(SettingData);
                                    //}
                                    settingList.Add(new CustomerBasicSettingEntity()
                                    {
                                        SettingCode = "InvitePartnersPoints",
                                        SettingValue = (IincentiveRuleData.SetoffRegPrize ?? 0).ToString()
                                    });
                                }
                                CustomerBasicSettingBll.SaveCustomerBasicInfo(loggingSessionInfo.ClientID, settingList);
                                #endregion
                            }
                        }
                        //
                        //pTran.Commit();
                    }
                    catch (APIException ex)
                    {
                        //pTran.Rollback();
                        throw ex;
                    }
                //}
            }
            return rd;
        }

    }
}