
namespace JIT.CPOS.BS.BLL.WX.Const
{
    /// <summary>
    /// 微信平台事件类型
    /// </summary>
    public class EventType
    {
        /// <summary>
        /// 关注
        /// </summary>
        public const string SUBSCRIBE = "subscribe";
        /// <summary>
        /// 取消关注
        /// </summary>
        public const string UNSUBSCRIBE = "unsubscribe";
        /// <summary>
        /// 点击
        /// </summary>
        public const string CLICK = "click";
        /// <summary>
        /// 扫描带参数二维码事件
        /// </summary>
        public const string SCAN = "scan";
        /// <summary>
        /// 模版消息发送任务完成
        /// </summary>
        public const string TEMPLATESENDJOBFINISH = "templatesendjobfinish";
    }
}
