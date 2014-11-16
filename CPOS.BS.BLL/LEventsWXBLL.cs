/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/11/29 10:34:16
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
    public partial class LEventsWXBLL
    {
        #region ��ȡ�����ŵ��ά��
        /// <summary>
        /// ��ȡ�����ŵ��ά��
        /// </summary>
        /// <returns></returns>
        public int GetEventsWXCode(string EventId)
        {
            int iCode = 0;
            string weiXinCode = _currentDAO.GetEventsWXCode(EventId);
            if (weiXinCode == null || weiXinCode.Equals(""))
            {
                iCode = 100;
            }
            else
            {
                iCode = Convert.ToInt32(weiXinCode) + 1;
            }
            return iCode;
        }
        #endregion

        /// <summary>
        /// ��ӻ���ʾ�Ĺ�ϵ
        /// </summary>
        /// <param name="pEventID">�ID</param>
        /// <param name="pMobileModuleID">�ʾ�ID</param>
        /// <returns></returns>
        public int CreateMobileModuleObjectMapping(string pEventID, string pMobileModuleID)
        {
            return _currentDAO.CreateMobileModuleObjectMapping(pEventID, pMobileModuleID);
        }

        public int CreateObjectImages(string pEventID, string pImageURL)
        {
            return _currentDAO.CreateObjectImages(pEventID, pImageURL);
        }

        /// <summary>
        /// ��ѯMobileModuleID
        /// </summary>
        /// <param name="pEventID"></param>
        /// <returns></returns>
        public string GetMobileModuleID(string pEventID)
        {
            return this._currentDAO.GetMobileModuleID(pEventID);
        }
    }
}