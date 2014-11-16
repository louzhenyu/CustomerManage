/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/11/5 9:24:38
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
    public partial class VersionManagerEntity : BaseEntity 
    {
        #region ���Լ�
        /// <summary>
        /// �Ƿ����°汾����  0��1
        /// </summary>
        public string IsNewVersionAvailable { get; set; }

        /// <summary>
        /// �ð汾�Ƿ�ɱ����Բ�ǿ�����ء�1���ɺ��ԣ�0�����ɺ���
        /// </summary>
        public string CanSkip { get; set; }

        /// <summary>
        /// ��������  0���г���  1����ҵ��
        /// </summary>
        public string ChannelDesc { get; set; }

        /// <summary>
        /// �����û���Χ����  0��ȫ���û�  1����ʽ�û�  2�������û�
        /// </summary>
        public string UserScopeDesc { get; set; }
        #endregion
    }
}