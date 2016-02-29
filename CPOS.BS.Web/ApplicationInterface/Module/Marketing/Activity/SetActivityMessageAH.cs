using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Marketing.Activity.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Marketing.Activity
{
    public class SetActivityMessageAH : BaseActionHandler<SetActivityMessageRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<SetActivityMessageRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var ActivityMessageBLL = new C_ActivityMessageBLL(loggingSessionInfo);
            var PrizesBLL = new C_PrizesBLL(loggingSessionInfo);
            var C_MessageTemplateBLL = new C_MessageTemplateBLL(loggingSessionInfo);
            //事物
            var pTran = ActivityMessageBLL.GetTran();
            using (pTran)
            {
                try
                {
                    //新增消息集合
                    var AddDataList=new List<C_ActivityMessageEntity>();
                    //编辑消息集合
                    var UpdateDataList = new List<C_ActivityMessageEntity>();
                    //删除消息集合
                    var DelList = new List<Guid>();
                    #region 数据处理
                    foreach (var item in para.ActivityMessageList)
                    {
                        //模板
                        //C_MessageTemplateEntity TemplateData = C_MessageTemplateBLL.GetByID(new Guid(item.TemplateID));

                        if (string.IsNullOrWhiteSpace(item.MessageID))
                        {
                            #region 新增
                            C_ActivityMessageEntity AddData = new C_ActivityMessageEntity();
                            //AddData.MessageID = System.Guid.NewGuid();
                            AddData.ActivityID = new Guid(para.ActivityID);
                            AddData.TemplateID =new Guid();
                            AddData.MessageType = item.MessageType;
                            AddData.Content = item.Content == null ? "" : item.Content;
                            AddData.SendTime = Convert.ToDateTime(item.SendTime);
                            AddData.CustomerID = loggingSessionInfo.ClientID;
                            AddDataList.Add(AddData);
                            #endregion
                        }
                        else
                        {
                            C_ActivityMessageEntity Data = ActivityMessageBLL.GetByID(item.MessageID);
                            if (Data == null)
                                throw new APIException("消息对象为NULL！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };

                            if (item.IsEnable.Value)
                            {
                                //更新
                                Data.SendTime = Convert.ToDateTime(item.SendTime);
                                Data.Content = item.Content;
                                Data.TemplateID = new Guid();
                                UpdateDataList.Add(Data);
                            }
                            else
                            {
                                //删除
                                DelList.Add(Data.MessageID.Value);
                            }
                        }

                    }
                    #endregion
                    
                    #region 执行
                    if (AddDataList.Count > 0)
                    {
                        //新增消息
                        foreach (var item in AddDataList)
                        {
                            ActivityMessageBLL.Create(item, pTran);
                        }

                    }

                    if (UpdateDataList.Count > 0)
                    {
                        //编辑消息
                        foreach (var item in UpdateDataList)
                        {
                            ActivityMessageBLL.Update(item, pTran);
                        }
                    }
                    if (DelList.Count > 0)
                    {
                        //删除消息
                        foreach (var item in DelList)
                        {
                            ActivityMessageBLL.Delete(item, pTran);
                        }
                    }

                    pTran.Commit();
                    #endregion

                }
                catch (APIException apiEx)
                {
                    pTran.Rollback();//回滚事物
                    throw new APIException(apiEx.ErrorCode, apiEx.Message);
                }
                catch (Exception ex)
                {
                    pTran.Rollback();//回滚事务
                    throw new APIException(ex.Message);
                }
            }
            return rd;
        }
    }
}