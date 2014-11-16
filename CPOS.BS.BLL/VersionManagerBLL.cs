/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/11/5 9:24:38
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
using JIT.CPOS.BS.Entity.Interface;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class VersionManagerBLL
    {
        #region �汾��Ϣ
        public NewVersionEntity GetNewVersionEntity(string Plat,string Channel,string Version,string UserId)
        {
            NewVersionEntity newVersionInfo = new NewVersionEntity();
            try
            {
                
                #region ��ȡ������Ϣ
                VersionManagerEntity versionInfo = new VersionManagerEntity();
                DataSet ds = new DataSet();
                ds = _currentDAO.GetVersionInfoByQuery(Plat, Channel, Version, UserId);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    versionInfo = DataTableToObject.ConvertToObject<VersionManagerEntity>(ds.Tables[0].Rows[0]);
                }
                if (versionInfo == null || versionInfo.Plat == null)
                {
                    newVersionInfo.Code = "200";
                    newVersionInfo.Description = "û���µ�������Ϣ";
                    newVersionInfo.message = "û���µ�������Ϣ";
                    newVersionInfo.isNewVersionAvailable = "0";
                    newVersionInfo.canSkip = "1";
                    newVersionInfo.updateUrl = "";
                    newVersionInfo.Version = Version;
                }
                else
                {
                    newVersionInfo.Code = "220";
                    newVersionInfo.Description = "���°汾";
                    newVersionInfo.message = versionInfo.Notice;
                    newVersionInfo.isNewVersionAvailable = versionInfo.IsNewVersionAvailable;
                    newVersionInfo.canSkip = versionInfo.CanSkip;
                    newVersionInfo.updateUrl = versionInfo.DownloadURL;
                    newVersionInfo.Version = versionInfo.VersionNoUpdate;
                }

                #endregion
                return newVersionInfo;
            }
            catch (Exception ex)
            {
                newVersionInfo.Code = "1003";
                newVersionInfo.Description = ex.ToString();
                newVersionInfo.canSkip = "0";
                return newVersionInfo;
            }
        }
        #endregion
    }
}