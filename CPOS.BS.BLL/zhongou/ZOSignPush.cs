using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.BLL.WX.Const;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.WX;
using System.Data;
using JIT.Utility.Log;

namespace JIT.CPOS.BS.BLL.zhongou
{
    public class ZOSignPush:BaseService
    {
        #region 构造函数
        public ZOSignPush(LoggingSessionInfo loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
            //ZOSignPush = new DataAccess.ZOSignPush(loggingSessionInfo);
        }
        #endregion

        #region //中欧工商学院，永久二维码关注推送信息
        public void SetPushSignIn(string qrcode_id, string WeiXin, string OpenId)
        {
            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("中欧扫描--关注SetPushSignIn 进入 qrcode_id:{0},WeiXin:{1},OpenId:{2}", qrcode_id, WeiXin, OpenId)
            });
            try
            {
                if (qrcode_id == null || qrcode_id.Equals(""))
                {

                }
                else
                {
                    string eventKey = string.Empty;
                    switch (qrcode_id)
                    {
                        case "1"://MBA课程
                            eventKey = "K_002001";
                            break;
                        case "2"://EMBA课程
                            eventKey = "K_002002";
                            break;
                        case "3"://FMBA课程
                            eventKey = "K_002003"; 
                            break;
                        case "8"://高层经理培训课程
                            eventKey = "K_002004"; 
                            break;
                        case "9"://捐赠
                            eventKey = "F0D62CB"; 
                            break;
                        case "10"://中欧新闻
                            eventKey = "991644C";
                            break;
                    }
                    if (eventKey == null || eventKey.Equals(""))
                    { }
                    else {
                        SetPushModel(eventKey, WeiXin, OpenId);
                    }
                }
            }
            catch (Exception ex)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("中欧扫描--关注SetPushSignIn 进入 错误:{0}",ex.ToString())
                });
            }
        }

        private void SetPushModel(string eventKey,string WeiXin,string OpenId)
        {
            try
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("中欧扫描--关注SetPushModel 进入 eventKey:{0},WeiXin:{1},OpenId:{2}", eventKey, WeiXin, OpenId)
                });

                #region 动态处理事件KEY值

                var menuDAO = new WMenuDAO(loggingSessionInfo);
                var ds = menuDAO.GetMenusByKey(WeiXin, eventKey);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string typeId = ds.Tables[0].Rows[0]["MaterialTypeId"].ToString();  //素材类型
                    string materialId = ds.Tables[0].Rows[0]["MaterialId"].ToString();  //素材ID
                    string modelId = ds.Tables[0].Rows[0]["ModelId"].ToString();        //模型ID

                    BaseService.WriteLogWeixin("typeId：" + typeId);
                    BaseService.WriteLogWeixin("materialId：" + materialId);
                    BaseService.WriteLogWeixin("modelId：" + modelId);

                    switch (typeId)
                    {
                        //case MaterialType.TEXT:         //回复文字消息 
                        //    ReplyText(materialId);
                        //    break;
                        case MaterialType.IMAGE_TEXT:   //回复图文消息 
                            GetImageText(materialId,WeiXin,OpenId);
                            break;
                        //case MaterialType.OTHER:    //后台处理
                        //    break;
                        default:
                            break;
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("中欧扫描--关注SetPushModel 进入 错误:{0}", ex.ToString())
                });
            }
        }

        /// <summary>
        /// 推送图文消息
        /// </summary>
        /// <param name="materialId"></param>
        public void GetImageText(string materialId, string WeiXin, string OpenId)
        {
            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("中欧扫描--关注materialId 进入 materialId:{0},WeiXin:{1},OpenId:{2}", materialId, WeiXin, OpenId)
            });
            try
            {
                var dsMaterialText = new WMaterialTextDAO(loggingSessionInfo).GetMaterialTextByID(materialId);
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("中欧扫描--关注loggingSessionInfo 进入 loggingSessionInfo:{0}", loggingSessionInfo.CurrentUser.customer_id)
                });
                if (dsMaterialText != null && dsMaterialText.Tables.Count > 0 && dsMaterialText.Tables[0].Rows.Count > 0)
                {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("中欧扫描--关注materialId 进入 dsMaterialText:")
                    });
                    var newsList = new List<NewsEntity>();

                    foreach (DataRow dr in dsMaterialText.Tables[0].Rows)
                    {
                        newsList.Add(new NewsEntity()
                        {
                            title = dr["Title"].ToString(),
                            description = dr["Author"].ToString(),
                            picurl = dr["CoverImageUrl"].ToString(),
                            url = dr["OriginalUrl"].ToString()
                        });
                    }
                    JIT.CPOS.BS.BLL.WX.CommonBLL commonService = new WX.CommonBLL();
                    SendMessageEntity sendInfo = new SendMessageEntity();
                    sendInfo.msgtype = "news";
                    sendInfo.touser = OpenId;
                    sendInfo.articles = newsList;
                    WApplicationInterfaceBLL waServer = new WApplicationInterfaceBLL(loggingSessionInfo);
                    var waObj = waServer.QueryByEntity(new WApplicationInterfaceEntity
                    {
                        WeiXinID = WeiXin
                    }, null);
                    if (waObj == null || waObj.Length == 0 || waObj[0] == null)
                    {
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("中欧扫描--关注materialId 进入 waObj为空:}")
                        });
                    }
                    else
                    {
                        string appID = string.Empty;
                        string appSecret = string.Empty;
                        appID = waObj[0].AppID;
                        appSecret = waObj[0].AppSecret;
                        JIT.CPOS.BS.Entity.WX.ResultEntity resultInfo = new JIT.CPOS.BS.Entity.WX.ResultEntity();
                        resultInfo = commonService.SendMessage(sendInfo, appID, appSecret, loggingSessionInfo);
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("中欧扫描--关注materialId 进入 waObj:appID:{0},appSecret {1}",appID,appSecret)
                        });
                    }
                }
            }
            catch (Exception ex) {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("中欧扫描--关注GetImageText 进入 错误:{0}", ex.ToString())
                });
            }
        }
        #endregion
    }
}
