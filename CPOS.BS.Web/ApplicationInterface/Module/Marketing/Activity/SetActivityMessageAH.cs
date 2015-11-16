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
                    switch (para.OperationType)
                    {
                        case 1:



                            foreach (var item in para.ActivityMessageList)
                            {
                                //模板
                                C_MessageTemplateEntity TemplateData = C_MessageTemplateBLL.GetByID(new Guid(item.TemplateID));

                                if (string.IsNullOrWhiteSpace(item.MessageID))
                                {
                                    //添加
                                    C_ActivityMessageEntity AddData = new C_ActivityMessageEntity();
                                    //AddData.MessageID = System.Guid.NewGuid();
                                    AddData.ActivityID = new Guid(para.ActivityID);
                                    AddData.TemplateID = new Guid(item.TemplateID);
                                    AddData.MessageType = item.MessageType;
                                    AddData.Content = TemplateData == null ? "" : TemplateData.Content + item.Content == null ? "" : item.Content;
                                    AddData.SendTime = Convert.ToDateTime(item.SendTime);
                                    AddData.CustomerID = loggingSessionInfo.ClientID;
                                    ActivityMessageBLL.Create(AddData);//执行
                                }
                                else
                                {
                                    //更换模板
                                    //添加
                                    C_ActivityMessageEntity AddData = ActivityMessageBLL.GetByID(new Guid(item.MessageID));
                                    if(AddData==null)
                                        throw new APIException("消息对象为NULL！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };

                                    AddData.Content = TemplateData == null ? "" : TemplateData.Content;
                                }
                            }

                            break;
                        case 2:
                            //编辑消息集合
                            List<C_ActivityMessageEntity> UpdateDataList = new List<C_ActivityMessageEntity>();
                            //删除消息集合
                            List<Guid> DelList = new List<Guid>();

                            #region 操作数据处理
                            //编辑
                            foreach (var item in para.ActivityMessageList)
                            {
                                C_ActivityMessageEntity Data = ActivityMessageBLL.GetByID(item.MessageID);
                                if (Data == null)
                                    throw new APIException("消息对象为NULL！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };

                                if (item.IsEnable.Value)
                                {
                                    Data.SendTime = Convert.ToDateTime(item.SendTime);
                                    Data.Content = item.Content;
                                    Data.TemplateID = new Guid(item.TemplateID);
                                    UpdateDataList.Add(Data);
                                }
                                else
                                {
                                    DelList.Add(Data.MessageID.Value);
                                }
                            }
                            #endregion

                            #region 执行


                            if (UpdateDataList.Count > 0)
                            {
                                //if (UpdateDataList.Min(m => m.SendTime).Value != null)
                                //{
                                //    C_PrizesEntity UpdPrizesData = PrizesBLL.QueryByEntity(new C_PrizesEntity() { ActivityID = new Guid(para.ActivityID) }, null).FirstOrDefault();
                                //    if (UpdPrizesData == null)
                                //        throw new APIException("奖品对象为NULL！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                                //}
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
                            break;
                        default:
                            throw new APIException("当前操作类型不匹配！") { ErrorCode = ERROR_CODES.INVALID_REQUEST_LACK_REQUEST_PARAMETER };
                            break;
                    }
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