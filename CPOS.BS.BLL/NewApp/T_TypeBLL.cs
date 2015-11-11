/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-10-18 11:08:58
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
    /// 业务处理：  
    /// </summary>
    public partial class T_TypeBLL
    {

        public void UpdateShop(int LevelCount,  string CustomerID)
        {
            this._currentDAO.UpdateShop(LevelCount, CustomerID);
        }


        public DataSet GetUnitStructList(string CustomerID, string loginUserID, int hasShop)
        {
            return this._currentDAO.GetUnitStructList(CustomerID, loginUserID, hasShop);
        }
        public DataSet GetUnitStructByID(string CustomerID, string unit_id)
        {
            return this._currentDAO.GetUnitStructByID(CustomerID, unit_id);
        }
        public DataSet GetTypeTree(string CustomerID, string user_id)
        {
            return this._currentDAO.GetTypeTree(CustomerID, user_id);
        }
    }
}