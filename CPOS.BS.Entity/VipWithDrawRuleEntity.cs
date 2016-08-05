/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/22 14:06:57
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
using System.Collections;

namespace JIT.CPOS.BS.Entity
{

    /// <summary>
    /// 实体：  
    /// </summary>
    public partial class VipWithDrawRuleEntity : BaseEntity
    {
        #region 属性集
        /// <summary>
        /// 0：不限制   1：日   2：周   3：月
        /// </summary>
        public string WithDrawNumTypeName
        {
            get
            {
                string _WithDrawNumTypeName = "不限制";
                switch (WithDrawNumType)
                {
                    case 0:
                        _WithDrawNumTypeName = "不限制";
                        break;
                    case 1:
                        _WithDrawNumTypeName = "日";
                        break;
                    case 2:
                        _WithDrawNumTypeName = "周";
                        break;
                    case 3:
                        _WithDrawNumTypeName = "月";
                        break;
                    default:
                        _WithDrawNumTypeName = "不限制";
                        break;
                }
                return _WithDrawNumTypeName;
            }
           
        }
        #endregion
    }
}