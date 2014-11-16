/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/30 13:13:39
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
using System.Data;
using System.Data.SqlClient;
using System.Text;

using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{

    /// <summary>
    /// ���ݷ��ʣ�  
    /// ��APPUpgrade�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class APPUpgradeDAO : Base.BaseCPOSDAO, ICRUDable<APPUpgradeEntity>, IQueryable<APPUpgradeEntity>
    {
        #region ��ȡ������ַ
        /// <summary>
        /// ��ȡ������ַ
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pAppName"></param>
        /// <param name="pVersion"></param>
        /// <returns></returns>
        public DataSet GetAppUpgrade(string pCustomerID, string pAppName, string pVersion)
        {
            string sql = "SELECT IOSUpgradeUrl,AndroidUpgradeUrl,AppName,Versions,IsMandatoryUpdate,AndroidUpgradeCon,IOSUpgradeCon,ServerUrl FROM APPUpgrade";
            sql += " WHERE CustomerID=@CustomerID AND AppName=@AppName AND Versions >@Versions AND IsDelete=0";
            sql += " ORDER BY Versions DESC";

            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@CustomerID", pCustomerID));
            para.Add(new SqlParameter("@AppName", pAppName));
            para.Add(new SqlParameter("@Versions", pVersion));

            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
        }
        #endregion
    }
}
