using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity.WX
{
    /// <summary>
    /// 发送消息接口
    /// </summary>
    public class SendMessageEntity
    {
        /// <summary>
        /// 错误编码
        /// </summary>
        public string errcode { get; set; }
        /// <summary>
        /// 错误内容
        /// </summary>
        public string errmsg { get; set; }
        /// <summary>
        /// 调用接口凭证
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 普通用户openid
        /// </summary>
        public string touser { get; set; }
        /// <summary>
        /// 消息类型，支持文本（text）、图片（image）、语音（voice）、视频（video）
        /// </summary>
        public string msgtype { get; set; }
        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 媒体文件id
        /// </summary>
        public string media_id { get; set; }
        /// <summary>
        /// 媒体文件id
        /// </summary>
        public string thumb_media_id { get; set; }
        /// <summary>
        /// 音乐链接
        /// </summary>
        public string musicurl { get; set; }
        /// <summary>
        /// 高品质音乐链接，wifi环境优先使用该链接播放音乐
        /// </summary>
        public string hqmusicurl { get; set; }
        /// <summary>
        /// 消息标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 消息描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 点击链接跳转地址
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80
        /// </summary>
        public string picurl { get; set; }
        /// <summary>
        ///  图文消息集合
        /// </summary>
        public List<NewsEntity> articles { get; set; }
    }

    /// <summary>
    /// 图文消息实体类
    /// </summary>
    public class NewsEntity
    {
        /// <summary>
        /// 消息标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 消息描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 点击链接跳转地址
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80
        /// </summary>
        public string picurl { get; set; }
    }
}
