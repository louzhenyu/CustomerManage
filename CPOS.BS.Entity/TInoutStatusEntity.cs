/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/2/27 13:31:25
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
    public partial class TInoutStatusEntity : BaseEntity 
    {
        #region ���Լ�

        /// <summary>
        /// ����״̬����
        /// </summary>
        public string OrderStatusName { get; set; }
        /// <summary>
        /// ���δͨ������
        /// </summary>
        public string CheckResultName { get; set; }
        /// <summary>
        /// ֧����ʽ����
        /// </summary>
        public string PayMethodName { get; set; }
        /// <summary>
        /// ���͹�˾����
        /// </summary>
        public string unit_name { get; set; }
        /// <summary>
        /// ���ڸ�ʽ��
        /// </summary>
        public string LastUpdateTimeFormat {
            get {
                return LastUpdateTime == null ? "" : DateTime.Parse(LastUpdateTime.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        #endregion
    }
}