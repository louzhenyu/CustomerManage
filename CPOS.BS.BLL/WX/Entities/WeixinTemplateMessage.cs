/*******************************************
  zoukun 20141028 微信模板消息映射类
 ******************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.BLL.WX
{
    /// <summary>
    /// 微信模板消息映射类
    /// </summary>
    public class WeixinTemplateMessage
    {
        public string touser { get; set; }
        public string template_id { get; set; }
        public string url { get; set; }
        public string topcolor { get; set; }
        public IDictionary<string, WeixinTemplateMessageData> data { get; set; }
        /// <summary>
        /// 添加 要替换的消息 到模板中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="color"></param>
        public void Add(string key, string value, string color)
        {
            if (null == data) 
                data = new Dictionary<string, WeixinTemplateMessageData>();
            data.Add(key, new WeixinTemplateMessageData() { value = value, color = color });
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class WeixinTemplateMessageData
    {
        public string value { get; set; }
        public string color { get; set; }
    }
}
