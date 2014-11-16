/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/3/27 11:34:46
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
    /// ʵ�壺 �ݷ�·�߶��� 
    /// </summary>
    public class RouteViewEntity :RouteEntity
    {
        /// <summary>
        /// �û���
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// ְλ
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// ����ID������ѯ�ã�
        /// </summary>
        public Guid? ClientStructureID { get; set; }

        /// <summary>
        /// ������ID������ѯ�ã�
        /// </summary>
        public int? ClientUserID { get; set; }
    }
}