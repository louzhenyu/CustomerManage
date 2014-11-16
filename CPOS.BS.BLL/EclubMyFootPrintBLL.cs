/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/5 17:14:24
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
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class EclubMyFootPrintBLL
    {  
        /// <summary>
        /// ��¼�㼣
        /// </summary>
        /// <param name="pageCode">ҳ���</param>
        /// <param name="userID">�û�ID</param>
        /// <param name="objectID">�������ID</param>
        /// <param name="footType">0��������1.��Ѷ,  2.��Ƶ ,3.�, 4.�γ�, 5.У��</param>
        /// <param name="operationType">1.��ѯ��2.�޸ģ�3.����,4.ɾ��,5��½��6�ղ�</param>
        public void RecordSpoorInfo(string pageCode, string userID, string objectID, int footType, int operationType)
        {
            _currentDAO.RecordSpoorInfo(pageCode, userID, objectID, footType, operationType);
        }
    }
}