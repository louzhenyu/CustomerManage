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
    /// ʵ�壺  
    /// </summary>
    public partial class VipWithDrawRuleEntity : BaseEntity
    {
        #region ���Լ�
        /// <summary>
        /// 0��������   1����   2����   3����
        /// </summary>
        public string WithDrawNumTypeName
        {
            get
            {
                string _WithDrawNumTypeName = "������";
                switch (WithDrawNumType)
                {
                    case 0:
                        _WithDrawNumTypeName = "������";
                        break;
                    case 1:
                        _WithDrawNumTypeName = "��";
                        break;
                    case 2:
                        _WithDrawNumTypeName = "��";
                        break;
                    case 3:
                        _WithDrawNumTypeName = "��";
                        break;
                    default:
                        _WithDrawNumTypeName = "������";
                        break;
                }
                return _WithDrawNumTypeName;
            }
           
        }
        #endregion
    }
}