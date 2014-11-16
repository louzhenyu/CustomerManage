﻿using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL.WX.Const;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.WX;
using System.Collections.Generic;
using System.Configuration;

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
            //设置关注信息
            BaseService.WriteLogWeixin("公众号贱人贱人");
            var modelDAO = new WModelDAO(requestParams.LoggingSessionInfo);
            var ds = modelDAO.GetMaterialByWeixinIdJermyn(requestParams.WeixinId,2);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //string typeId = ds.Tables[0].Rows[0]["MaterialTypeId"].ToString();  //素材类型
                //string materialId = ds.Tables[0].Rows[0]["MaterialId"].ToString();  //素材ID

                string typeId = ds.Tables[0].Rows[0]["ReplyType"].ToString();  //素材类型 1=文字2=图片3=图文4=语音5=视频6=其他
                string ReplyId = ds.Tables[0].Rows[0]["ReplyId"].ToString();  //素材ID
                string Text = ds.Tables[0].Rows[0]["text"].ToString();  //素材ID

                BaseService.WriteLogWeixin("typeId：" + typeId);
                BaseService.WriteLogWeixin("ReplyId：" + ReplyId);

                switch (typeId)
                {
                    case MaterialType.TEXT:         //回复文字消息 
                        //ReplyText(materialId);
                        ReplyTextJermyn(Text);
                        break;
                    case MaterialType.IMAGE_TEXT:   //回复图文消息 
                        //ReplyNews(materialId);
                        ReplyNewsJermyn(ReplyId,2,1);
                        break;
                    case MaterialType.OTHER:    //后台处理
                        break;
                    default:
                        break;
                }
            }
            else {
                UserSubscribeOld();
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
