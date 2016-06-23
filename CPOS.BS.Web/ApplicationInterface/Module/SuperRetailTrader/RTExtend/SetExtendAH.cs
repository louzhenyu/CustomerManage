using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.RTExtend.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.SuperRetailTrader.RTExtend
{
    public class SetExtendAH : BaseActionHandler<SetExtendRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(APIRequest<SetExtendRP> pRequest)
        {
            //SessionManager.GetLoggingSessionByCustomerId(pRequest.CustomerID);
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            if (loggingSessionInfo == null)
                throw new APIException("用户未登录") { ErrorCode = ERROR_CODES.INVALID_REQUEST };
            var bll = new SetoffEventBLL(this.CurrentUserInfo);
            var objectImagesBll = new ObjectImagesBLL(this.CurrentUserInfo);
            var setoffToolsBll = new SetoffToolsBLL(this.CurrentUserInfo);
            var setoffPosterBll = new SetoffPosterBLL(this.CurrentUserInfo);
            var setoffEventBll = new SetoffEventBLL(this.CurrentUserInfo);
            var pTran = bll.GetTran();

            //获取已发布的集客行动
            var SetoffEvenResult = bll.QueryByEntity(new SetoffEventEntity() { SetoffType = 3, Status = "10" }, null).ToList();
            if (SetoffEvenResult.Count > 1)
                throw new APIException("数据异常，只能一条分销拓展！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
            else if (SetoffEvenResult.Count == 1)
            {
                var Result = SetoffEvenResult.First();

                #region 新增招募海报、海报集客工具关系
                foreach (var PosterItem in pRequest.Parameters.PosterList)
                {
                    //图片表
                    var ObjectImageData = new ObjectImagesEntity();
                    ObjectImageData.ImageId = System.Guid.NewGuid().ToString("N");
                    ObjectImageData.ImageURL = PosterItem.ImageUrl;
                    ObjectImageData.Description = "招募海报";
                    ObjectImageData.CustomerId = loggingSessionInfo.ClientID;

                    objectImagesBll.Create(ObjectImageData);//
                    //招募海报
                    var SetoffPosterData = new SetoffPosterEntity();
                    SetoffPosterData.SetoffPosterID = System.Guid.NewGuid();
                    SetoffPosterData.Name = PosterItem.Name;
                    SetoffPosterData.Desc = "招募海报";
                    SetoffPosterData.ImageId = ObjectImageData.ImageId;
                    SetoffPosterData.Status = "10";
                    SetoffPosterData.CustomerId = loggingSessionInfo.ClientID;

                    setoffPosterBll.Create(SetoffPosterData);

                    //海报工具关系
                    var SetoffToolsData = new SetoffToolsEntity();
                    SetoffToolsData.SetoffToolID = System.Guid.NewGuid();
                    SetoffToolsData.SetoffEventID = Result.SetoffEventID;
                    SetoffToolsData.ToolType = "SetoffPoster";
                    SetoffToolsData.ObjectId = SetoffPosterData.SetoffPosterID.ToString();
                    SetoffToolsData.Status = "10";
                    SetoffToolsData.CustomerId = loggingSessionInfo.ClientID;

                    setoffToolsBll.Create(SetoffToolsData);//
                }
                #endregion

                #region 新增集客工具关系
                foreach (var ToolsItem in pRequest.Parameters.CommonList)
                {
                    if (Result == null)
                        throw new APIException("数据异常，未找到集客行动!") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                    //判断是否添加重复集客工具
                    bool Flag = setoffEventBll.IsToolsRepeat(Result.SetoffEventID.ToString(), ToolsItem.ObjectId);
                    if (Flag)
                        throw new APIException("集客行动不能添加项同的集客工具!") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };

                    var SetoffToolsData = new SetoffToolsEntity();
                    SetoffToolsData.SetoffToolID = System.Guid.NewGuid();
                    SetoffToolsData.SetoffEventID = Result.SetoffEventID;
                    SetoffToolsData.ToolType = ToolsItem.ToolType;
                    SetoffToolsData.ObjectId = ToolsItem.ObjectId;
                    SetoffToolsData.Status = "10";
                    SetoffToolsData.CustomerId = loggingSessionInfo.ClientID;
                    setoffToolsBll.Create(SetoffToolsData);//
                }
                #endregion
            }
            else
            {
                //原集客行动状态设置为失效
                setoffEventBll.SetFailStatus(3);

                #region 新建集客行动
                //集客行动
                var SetoffEvenData = new SetoffEventEntity();//集客行动
                SetoffEvenData.SetoffEventID = System.Guid.NewGuid();
                SetoffEvenData.SetoffType = 3;
                SetoffEvenData.Status = "10";
                SetoffEvenData.CustomerId = loggingSessionInfo.ClientID;
                setoffEventBll.Create(SetoffEvenData);//

                #region 新增集客海报、海报集客工具关系
                foreach (var PosterItem in pRequest.Parameters.PosterList)
                {
                    //图片表
                    var ObjectImageData = new ObjectImagesEntity();
                    ObjectImageData.ImageId = System.Guid.NewGuid().ToString("N");
                    ObjectImageData.ImageURL = PosterItem.ImageUrl;
                    ObjectImageData.Description = "招募海报";
                    ObjectImageData.CustomerId = loggingSessionInfo.ClientID;
                    objectImagesBll.Create(ObjectImageData);//
                    //集客海报
                    var SetoffPosterData = new SetoffPosterEntity();
                    SetoffPosterData.SetoffPosterID = System.Guid.NewGuid();
                    SetoffPosterData.Name = PosterItem.Name;
                    SetoffPosterData.Desc = "招募海报";
                    SetoffPosterData.ImageId = ObjectImageData.ImageId;
                    SetoffPosterData.Status = "10";
                    SetoffPosterData.CustomerId = loggingSessionInfo.ClientID;
                    setoffPosterBll.Create(SetoffPosterData);

                    //海报工具关系
                    var SetoffToolsData = new SetoffToolsEntity();
                    SetoffToolsData.SetoffToolID = System.Guid.NewGuid();
                    SetoffToolsData.SetoffEventID = SetoffEvenData.SetoffEventID;
                    SetoffToolsData.ToolType = "SetoffPoster";
                    SetoffToolsData.ObjectId = SetoffPosterData.SetoffPosterID.ToString();
                    SetoffToolsData.Status = "10";
                    SetoffToolsData.CustomerId = loggingSessionInfo.ClientID;
                    setoffToolsBll.Create(SetoffToolsData);//
                }
                #endregion

                #region 新增集客工具关系
                foreach (var ToolsItem in pRequest.Parameters.CommonList)
                {
                    //判断是否添加重复集客工具
                    bool Flag = setoffEventBll.IsToolsRepeat(SetoffEvenData.SetoffEventID.ToString(), ToolsItem.ObjectId);
                    if (Flag)
                        throw new APIException("集客行动不能添加相同的集客工具!") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };

                    var SetoffToolsData = new SetoffToolsEntity();
                    SetoffToolsData.SetoffToolID = System.Guid.NewGuid();
                    SetoffToolsData.SetoffEventID = SetoffEvenData.SetoffEventID;
                    SetoffToolsData.ToolType = ToolsItem.ToolType;
                    SetoffToolsData.ObjectId = ToolsItem.ObjectId;
                    SetoffToolsData.Status = "10";
                    SetoffToolsData.CustomerId = loggingSessionInfo.ClientID;
                    setoffToolsBll.Create(SetoffToolsData);//
                }
                #endregion


                #endregion
            }
            return new EmptyResponseData();
        }
    }
}