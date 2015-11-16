/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-27 19:35:01
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
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ���� �����ƿ� 
    /// </summary>
    public partial class VipCardBatchBLL
    {  
        /// <summary>
        /// �ƿ�
        /// </summary>
        /// <param name="p_Data"></param>
        public void BatchMakeVipCard(VipCardBatchEntity p_Data) {
            this._currentDAO.BatchMakeVipCard(p_Data);
        }

        /// <summary>
        /// ���뿨����
        /// </summary>
        /// <param name="VipCardInfoList"></param>
        public void ImportVipCardISN(Dictionary<string, string> p_VipCardInfoCollection, string p_BatchNo)
        {
            this._currentDAO.ImportVipCardISN(p_VipCardInfoCollection, p_BatchNo);
        }
    }
}