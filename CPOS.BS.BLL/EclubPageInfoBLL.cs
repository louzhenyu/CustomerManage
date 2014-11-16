/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/4 15:08:10
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
    public partial class EclubPageInfoBLL
    {   
        /// <summary>
        /// �����ʷ
        /// </summary>
        /// <param name="userID">��ǰ��¼�û�ID</param>
        /// <param name="alumniID">У��ID</param>
        /// <param name="PageCode">ҳ����</param>
        /// <param name="footType">0��������1.��Ѷ,  2.��Ƶ ,3.�, 4.�γ�, 5.У��</param>
        /// <param name="operationType">1.��ѯ��2.�޸ģ�3.����,4.ɾ��,5��½��6�ղ�</param>
        public void BrowsingHistoryInfo(string userID, string alumniID, string PageCode, int footType, int operationType)
        {
            _currentDAO.BrowsingHistoryInfo(userID, alumniID, PageCode, footType, operationType);
        }
    }
}