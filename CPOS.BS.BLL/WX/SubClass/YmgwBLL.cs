using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL.WX.Const;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.WX;

namespace JIT.CPOS.BS.BLL.WX
{
    /// <summary>
    /// 逸马顾问
    /// </summary>
    public class YmgwBLL : BaseBLL
    {
        #region 构造函数

        public YmgwBLL(HttpContext httpContext, RequestParams requestParams)
            : base(httpContext, requestParams)
        {

        }

        #endregion

        #region 处理文本消息

        //处理文本消息
        public override void HandlerText()
        {
            var content = requestParams.XmlNode.SelectSingleNode("//Content").InnerText.Trim();   //文本消息内容

            var keywordDAO = new WKeywordReplyDAO(requestParams.LoggingSessionInfo);
            var ds = keywordDAO.GetMaterialByKeyword(content);

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
        }

        #endregion

        #region 用户关注微信号

        //用户关注微信号
        public override void UserSubscribe()
        {
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

        #region 处理点击事件

        //处理点击事件
        public override void HandlerClick(string eventKey)
        {
            //事件KEY值，与自定义菜单接口中KEY值对应 
            BaseService.WriteLogWeixin("EventKey：" + eventKey);

            #region 动态处理事件KEY值

            var menuDAO = new WMenuDAO(requestParams.LoggingSessionInfo);
            var ds = menuDAO.GetMenusByKey(requestParams.WeixinId, eventKey);

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

            #endregion
        }

        #endregion
    }
}
