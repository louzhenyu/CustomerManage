using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL.WX.Const;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.WX;
using System.Collections.Generic;
using System.Configuration;
using JIT.Utility.ExtensionMethod;
using System.Data;

namespace JIT.CPOS.BS.BLL.WX.BaseClass
{
    /// <summary>
    /// 微信认证服务号基类
    /// </summary>
    public class CertificationBLL : BaseBLL
    {
        #region 构造函数

        public CertificationBLL(HttpContext httpContext, RequestParams requestParams)
            : base(httpContext, requestParams)
        {

        }

        #endregion

        #region 用户关注微信号

        //用户关注微信号
        public override void UserSubscribe()
        {
            var eventsBll = new LEventsBLL(requestParams.LoggingSessionInfo);

            //设置关注信息
            var modelDAO = new WModelDAO(requestParams.LoggingSessionInfo);
            var ds = new DataSet();// /// <param name="KeyworkType">1=关键字回复 2=关注回复 3=自动回复</param>

            //优先处理二维码
            var qrcodeId = string.Empty;
            var eventKey = requestParams.XmlNode.SelectSingleNode("//EventKey");
            if (eventKey != null && eventKey.InnerText.Contains("qrscene_"))//如果是二维码的就之返回二维码的
            {
                qrcodeId = eventKey.InnerText.Substring(8);
                //eventsBll.SendQrCodeWxMessage(requestParams.LoggingSessionInfo, requestParams.LoggingSessionInfo.CurrentLoggingManager.Customer_Id, requestParams.WeixinId, qrcodeId,
                //requestParams.OpenId, this.httpContext, requestParams); //保存用户信息时，有推送消息
                //eventsBll.QrCodeHandlerText(qrcodeId, requestParams.LoggingSessionInfo,
                // requestParams.WeixinId, 4, requestParams.OpenId, httpContext, requestParams);
                //ds = modelDAO.GetMaterialByWeixinIdJermyn(requestParams.WeixinId, 4);

            }
            else//否则返回自动回复的
            {
                ds = modelDAO.GetMaterialByWeixinIdJermyn(requestParams.WeixinId, 2);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //string typeId = ds.Tables[0].Rows[0]["MaterialTypeId"].ToString();  //素材类型
                    //string materialId = ds.Tables[0].Rows[0]["MaterialId"].ToString();  //素材ID

                    string typeId = ds.Tables[0].Rows[0]["ReplyType"].ToString();  //素材类型 1=文字2=图片3=图文4=语音5=视频6=其他
                    string ReplyId = ds.Tables[0].Rows[0]["ReplyId"].ToString();  //素材ID
                    string Text = ds.Tables[0].Rows[0]["text"].ToString();  //素材ID

                    BaseService.WriteLogWeixin("自动回复： typeId：" + typeId);
                    BaseService.WriteLogWeixin("自动回复：ReplyId：" + ReplyId);



                    switch (typeId)
                    {
                        case MaterialType.TEXT:         //回复文字消息 
                            //ReplyText(materialId);
                            ReplyTextJermyn(Text);
                            break;
                        case MaterialType.IMAGE_TEXT:   //回复图文消息 
                            //ReplyNews(materialId);
                            ReplyNewsJermyn(ReplyId, 2, 1);
                            break;
                        case MaterialType.OTHER:    //后台处理
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    UserSubscribeOld();
                }
            }



            var application = new WApplicationInterfaceDAO(requestParams.LoggingSessionInfo);
            var appEntitys = application.QueryByEntity(new WApplicationInterfaceEntity() { WeiXinID = requestParams.WeixinId }, null);

            if (appEntitys != null && appEntitys.Length > 0)
            {
                var entity = appEntitys.FirstOrDefault();

                BaseService.WriteLogWeixin("AppID:  " + entity.AppID);
                BaseService.WriteLogWeixin("AppSecret:  " + entity.AppSecret);

                //扫描带参数二维码事件

                BaseService.WriteLogWeixin("二维码 eventKey:  " + eventKey.InnerText);


                if (!string.IsNullOrEmpty(eventKey.InnerText))//这里如果是二维码扫锚，就找出了二维码的code，可以在这里推送该二维码对应的图文素材******
                {
                    qrcodeId = eventKey.InnerText.Substring(8);

                    eventsBll.SendQrCodeWxMessage(requestParams.LoggingSessionInfo, requestParams.LoggingSessionInfo.CurrentLoggingManager.Customer_Id, requestParams.WeixinId, eventKey.ToString(),
                    requestParams.OpenId, this.httpContext, requestParams);
                }

                BaseService.WriteLogWeixin("二维码 qrcodeId:  " + qrcodeId);

                //保存用户信息///// <param name="isShow">1： 关注  0： 取消关注</param>
                commonService.SaveUserInfo(requestParams.OpenId, requestParams.WeixinId, "1", entity.AppID, entity.AppSecret, qrcodeId, requestParams.LoggingSessionInfo);
            }
        }

        /// <summary>
        /// 老版本的微信关注自动回复 Jermyn20140512
        /// </summary>
        public void UserSubscribeOld()
        {
            //设置关注信息
            var modelDAO = new WModelDAO(requestParams.LoggingSessionInfo);
            var ds = modelDAO.GetMaterialByWeixinId(requestParams.WeixinId);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string typeId = ds.Tables[0].Rows[0]["MaterialTypeId"].ToString();  //素材类型
                string materialId = ds.Tables[0].Rows[0]["MaterialId"].ToString();  //素材ID

                BaseService.WriteLogWeixin("typeId：" + typeId);
                BaseService.WriteLogWeixin("materialId：" + materialId);

                switch (typeId)
                {
                    case MaterialType.TEXT:         //回复文字消息 
                        ReplyText(materialId);
                        break;
                    case MaterialType.IMAGE_TEXT:   //回复图文消息 
                        ReplyNews(materialId);
                        break;
                    case MaterialType.OTHER:    //后台处理
                        break;
                    default:
                        break;
                }
            }

            var application = new WApplicationInterfaceDAO(requestParams.LoggingSessionInfo);
            var appEntitys = application.QueryByEntity(new WApplicationInterfaceEntity() { WeiXinID = requestParams.WeixinId }, null);

            if (appEntitys != null && appEntitys.Length > 0)
            {
                var entity = appEntitys.FirstOrDefault();

                BaseService.WriteLogWeixin("AppID:  " + entity.AppID);
                BaseService.WriteLogWeixin("AppSecret:  " + entity.AppSecret);

                //扫描带参数二维码事件
                var eventKey = requestParams.XmlNode.SelectSingleNode("//EventKey");
                BaseService.WriteLogWeixin("eventKey:  " + eventKey.InnerText);

                var qrcodeId = string.Empty;
                if (!string.IsNullOrEmpty(eventKey.InnerText))
                {
                    qrcodeId = eventKey.InnerText.Substring(8);
                }

                BaseService.WriteLogWeixin("qrcodeId:  " + qrcodeId);

                //保存用户信息
                commonService.SaveUserInfo(requestParams.OpenId, requestParams.WeixinId, "1", entity.AppID, entity.AppSecret, qrcodeId, requestParams.LoggingSessionInfo);
            }
        }
        #endregion

        #region 用户取消关注微信号

        //用户取消关注微信号
        public override void UserUnSubscribe()
        {
            var application = new WApplicationInterfaceDAO(requestParams.LoggingSessionInfo);
            var appEntitys = application.QueryByEntity(new WApplicationInterfaceEntity() { WeiXinID = requestParams.WeixinId }, null);

            if (appEntitys != null && appEntitys.Length > 0)
            {
                var entity = appEntitys.FirstOrDefault();

                BaseService.WriteLogWeixin("AppID:  " + entity.AppID);
                BaseService.WriteLogWeixin("AppSecret:  " + entity.AppSecret);

                //保存用户信息
                commonService.SaveUserInfo(requestParams.OpenId, requestParams.WeixinId, "0", entity.AppID, entity.AppSecret, string.Empty, requestParams.LoggingSessionInfo);
            }
        }

        #endregion

        #region 处理位置消息

        public override void HandlerLocation()
        {
            string locationX = requestParams.XmlNode.SelectSingleNode("//Location_X").InnerText;   //地理位置维度
            string locationY = requestParams.XmlNode.SelectSingleNode("//Location_Y").InnerText;   //地理位置精度
            string scale = requestParams.XmlNode.SelectSingleNode("//Scale").InnerText;   //地图缩放大小
            string label = requestParams.XmlNode.SelectSingleNode("//Label").InnerText;   //地理位置信息
            string msgId = requestParams.XmlNode.SelectSingleNode("//MsgId").InnerText;   //消息id，64位整型
            BaseService.WriteLogWeixin("地理位置维度Location_X：---------------" + locationX);
            BaseService.WriteLogWeixin("地理位置精度Location_Y：---------------" + locationY);
            BaseService.WriteLogWeixin("地图缩放大小Scale：---------------" + scale);
            BaseService.WriteLogWeixin("地理位置信息Label：---------------" + label);
            BaseService.WriteLogWeixin("消息id，64位整型MsgId：---------------" + msgId);

            SubscriptionBLL.WXLocation(httpContext, commonService, requestParams, locationX, locationY);
        }

        #endregion
    }
}
