using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace JIT.CPOS.Web.RateLetterInterface
{
    /// <summary>
    /// 创建群组视图模型。
    /// </summary>
    [XmlRoot("Response")]
    public class CreateGroupViewModel
    {
        /// <summary>
        /// 请求状态码，取值000000（成功）
        /// </summary>
        public string statusCode { get; set; }

        /// <summary>
        /// 群组ID  长度为14位
        /// </summary>
        public string groupId { get; set; }

        /// <summary>
        /// 状态消息。
        /// </summary>
        public string statusMsg { get; set; }
    }

    /// <summary>
    /// 基础视图模型
    /// </summary>
    [XmlRoot("Response")]
    public class FoundationsViewModel
    {
        /// <summary>
        /// 请求状态码，取值000000（成功）
        /// </summary>
        public string statusCode { get; set; }

        /// <summary>
        /// 状态消息。
        /// </summary>
        public string statusMsg { get; set; }

    }

    /// <summary>
    /// 查询群组属性视图模型
    /// </summary>
    [XmlRoot("Response")]
    public class ResponseViewModel : FoundationsViewModel
    {
        /// <summary>
        /// 群组名字
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 群组所有者（默认为管理员）
        /// </summary>
        public string owner { get; set; }

        /// <summary>
        /// 群组公告
        /// </summary>
        public string declared { get; set; }

        /// <summary>
        /// 群组成员数量
        /// </summary>
        public string count { get; set; }

        /// <summary>
        /// 群组创建时间
        /// </summary>
        public string dateCreated { get; set; }

        /// <summary>
        /// 申请加入模式 0：默认直接加入 1：需要身份验证 2：私有群组
        /// </summary>
        public string permission { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string createType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
    }

    /// <summary>
    ///  查询成员所加入的组
    /// </summary>
    [XmlRoot("Response")]
    public class QueryGroupViewModel
    {
        /// <summary>
        /// 请求状态码，取值000000（成功）
        /// </summary>
        public string statusCode { get; set; }

        /// <summary>
        /// 成员加入的群组列表外层节点
        /// </summary>
        //[XmlElement("groups")]
        public group[] groups { get; set; }

        /// <summary>
        /// 状态消息。
        /// </summary>
        public string statusMsg { get; set; }
    }

    /// <summary>
    /// 成员加入的群组
    /// </summary>
    public class group
    {
        /// <summary>
        /// 群组ID
        /// </summary>
        public string groupId { get; set; }

        /// <summary>
        /// 群组名字
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 群组成员数量
        /// </summary>
        public string count { get; set; }

        /// <summary>
        /// 群组类型 0：临时群组（上限100人） 1：普通群组（上限300人） 2：vip群组（上限500人）
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 申请加入模式 0：默认直接加入 1：需要身份验证 2：私有群组
        /// </summary>
        public string permission { get; set; }
    }

    /// <summary>
    /// 获取所有公共群组
    /// </summary>
    [XmlRoot("Response")]
    public class PublicGroups
    {
        /// <summary>
        /// 请求状态码，取值000000（成功）
        /// </summary>
        public string statusCode { get; set; }

        /// <summary>
        /// 最后一条数据的时间戳（用于分页）
        /// </summary>
        public string updateTime { get; set; }

        /// <summary>
        /// 成员加入的群组列表外层节点
        /// </summary>
        public group[] groups { get; set; }

        /// <summary>
        /// 状态消息。
        /// </summary>
        public string statusMsg { get; set; }
    }
}