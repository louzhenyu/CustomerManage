/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/1/7 11:10:18
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
    public partial class FStaffEntity : BaseEntity 
    {
        #region ���Լ�
        public string VipId { get; set; }
        public string WeiXin { get; set; }
        public string WeiXinUserId { get; set; }
        public string HeadImgUrl { get; set; }
        public int Status { get; set; }
        public Int64 DisplayIndex { get; set; }
        public int IsSign { get; set; }
        #endregion
    }
}