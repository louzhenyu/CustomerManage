using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity.Pos
{
    /// <summary>
    /// 对象模板
    /// </summary>
    public class ObjectOperateInfo
    {
        /// <summary>
        /// 对象的创建者ID
        /// </summary>
        [XmlIgnore()]
        public string CreateUserID
        { get; set; }

        /// <summary>
        /// 对象的创建者姓名
        /// </summary>
        [XmlIgnore()]
        public string CreateUserName
        { get; set; }

        /// <summary>
        /// 对象的创建时间
        /// </summary>
        [XmlIgnore()]
        public DateTime CreateTime
        { get; set; }

        /// <summary>
        /// 对象的最后编辑者ID
        /// </summary>
        [XmlIgnore()]
        public string ModifyUserID
        { get; set; }

        /// <summary>
        /// 对象的最后编辑者姓名
        /// </summary>
        [XmlIgnore()]
        public string ModifyUserName
        { get; set; }

        /// <summary>
        /// 对象的最后编辑时间
        /// </summary>
        [XmlIgnore()]
        public DateTime ModifyTime
        { get; set; }

        /// <summary>
        /// 对象的最后修改时间戳
        /// </summary>
        [XmlIgnore()]
        public DateTime SystemModifyStamp
        { get; set; }
    }
}
