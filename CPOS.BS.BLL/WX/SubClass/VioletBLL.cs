using System.Web;
using JIT.CPOS.BS.BLL.WX.Const;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity.WX;

namespace JIT.CPOS.BS.BLL.WX
{
    /// <summary>
    /// 紫罗兰
    /// </summary>
    public class VioletBLL : BaseBLL
    {
        #region 构造函数

        public VioletBLL(HttpContext httpContext, RequestParams requestParams)
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
