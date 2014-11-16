/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/1/11 11:45:55
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
    public partial class VipAddressBLL
    {
        #region GetVIPAddressList
        public VipAddressEntity[] GetVIPAddressList(string pVipID)
        {
            return this._currentDAO.GetVIPAddressList(pVipID);
        }
        #endregion

        #region EditVipAddress
        public bool EditVipAddress(VipAddressEntity pEntity)
        {
            if (!string.IsNullOrEmpty(pEntity.VipAddressID))
            {//�޸ġ�ɾ��
                if (pEntity.IsDelete == 1)
                {
                    this._currentDAO.Delete(pEntity);
                }
                else
                {
                    this._currentDAO.UpdateAddress(pEntity);
                }
            }
            else
            {
                pEntity.VipAddressID = Guid.NewGuid().ToString("N");
                this._currentDAO.CreateAddress(pEntity);
            }
            return true;
        }
        #endregion

    }
}