/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/4 11:56:05
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
    public partial class GLDeviceInstallItemBLL
    {
        /// <summary>
        /// ���ݶ����Ż�ȡ����װ�豸
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pOrderNo"></param>
        /// <returns></returns>
        public DataSet GetDeviceInstallItemByOrderNo(string pCustomerID, string pOrderNo)
        {
            return _currentDAO.GetDeviceInstallItemByOrderNo(pCustomerID, pOrderNo);
        }

        //public DataSet GetDeviceInstallItemByServiceOrderNo(string pCustomerID, string pServiceOrderNo)
        //{
        //    return _currentDAO.GetDeviceInstallItemByServiceOrderNo(pCustomerID, pServiceOrderNo);
        //}

        /// <summary>
        /// ���ݷ��񵥺Ż�ȡ����װ�豸
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pOrderNo"></param>
        /// <returns></returns>
        public DataSet GetDeviceInstallItemByServiceOrderID(string pCustomerID, string pServiceOrderID)
        {
            return _currentDAO.GetDeviceInstallItemByServiceOrderID(pCustomerID, pServiceOrderID);
        }

        /// <summary>
        /// ɾ��ԤԼ���µ��豸
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pOrderNo"></param>
        /// <returns></returns>
        public DataSet DelDeviceInstallItemByServerOrderID(string pCustomerID, string pServiceOrderID)
        {
            return _currentDAO.DelDeviceInstallItemByServerOrderID(pCustomerID, pServiceOrderID);
        }
    }
}