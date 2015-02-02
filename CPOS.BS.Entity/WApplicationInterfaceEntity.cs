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
    /// ʵ�壺  
    /// </summary>
    public partial class WApplicationInterfaceEntity : BaseEntity
    {
        #region ���Լ�
        /// <summary>
        /// ����������
        /// </summary>
        public string CreateByName { get; set; }
        /// <summary>
        /// ǰһ�ε���Ϣ����key
        /// </summary>
        public string PrevEncodingAESKey { get; set; }
        /// <summary>
        /// ��ǰʹ�õ���Ϣ����key
        /// </summary>
        public string CurrentEncodingAESKey { get; set; }
        /// <summary>
        /// ��Ϣ��������0:Ĭ������ģʽ�������ܣ�1:����ģʽ�����յ���Ϣ�������ĺ����ģ�
        /// ������Ϣ����ʹ�����Ļ����ģ�������ͬʱʹ��
        /// 2:��ȫģʽ������AES����
        /// </summary>
        public int? EncryptType { get; set; }

        /// <summary>
        /// from JIT  Add by Henry 2015-2-2
        /// </summary>
        public DateTime TicketExpirationTime { get; set; }
        /// <summary>
        /// from JIT �ӿڵ���ƾ֤ Add by Henry 2015-2-2
        /// </summary>
        public string JsApiTicket { get; set; }
        #endregion
    }
}