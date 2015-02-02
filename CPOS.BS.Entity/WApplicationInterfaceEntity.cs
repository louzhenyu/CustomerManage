/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/8/13 9:26:57
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Text;
using JIT.Utility;
using JIT.Utility.Entity;

namespace JIT.CPOS.BS.Entity
{

    /// <summary>
    /// 实体：  
    /// </summary>
    public partial class WApplicationInterfaceEntity : BaseEntity
    {
        #region 属性集
        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreateByName { get; set; }
        /// <summary>
        /// 前一次的消息加密key
        /// </summary>
        public string PrevEncodingAESKey { get; set; }
        /// <summary>
        /// 当前使用的消息加密key
        /// </summary>
        public string CurrentEncodingAESKey { get; set; }
        /// <summary>
        /// 消息加密类型0:默认明文模式，不加密，1:兼容模式，接收的消息包含明文和密文，
        /// 发送消息可以使用密文或明文，但不能同时使用
        /// 2:安全模式，采用AES加密
        /// </summary>
        public int? EncryptType { get; set; }

        /// <summary>
        /// from JIT  Add by Henry 2015-2-2
        /// </summary>
        public DateTime TicketExpirationTime { get; set; }
        /// <summary>
        /// from JIT 接口调用凭证 Add by Henry 2015-2-2
        /// </summary>
        public string JsApiTicket { get; set; }
        #endregion
    }
}