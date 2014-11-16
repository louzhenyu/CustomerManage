using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity.WX
{
    /// <summary>
    /// 微信菜单 实体
    /// </summary>
    public class MenusEntity
    {
        /// <summary>
        /// 按钮数组，按钮个数应为2~3个
        /// </summary>
        public List<MenuEntity> button { get; set; }
    }

    public class MenuEntity
    {
        /// <summary>
        /// 按钮类型，目前有click类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 按钮描述，既按钮名字，不超过16个字节，子菜单不超过40个字节
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 按钮KEY值，用于消息接口(event类型)推送，不超过128字节
        /// </summary>
        public string key { get; set; }
        /// <summary>
        /// 子按钮数组，按钮个数应为2~5个
        /// </summary>
        public string sub_button { get; set; }
        /// <summary>
        /// 用户点击view类型按钮后，会直接跳转到开发者指定的url中。
        /// </summary>
        public string url { get; set; }
    }
}
