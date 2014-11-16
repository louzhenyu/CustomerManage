using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.ETCL.Base;
using JIT.Utility.ETCL.DataStructure;
using JIT.CPOS.BS.Entity;
using JIT.Utility.ETCL.Interface;
using System.Data;

namespace JIT.CPOS.BS.BLL.Module.VIP.VIPImport
{
    public class VIPImportETCL : BaseExcelETCL
    {
        protected ETCLTemplateInfo _currentBusinessInfo;
        public ETCLTemplateInfo TemplateInfo
        {
            get
            {
                return this._currentBusinessInfo;
            }
        }

        public LoggingSessionInfo _TenantPlatformUserInfo;

        #region 构造函数
        public VIPImportETCL(LoggingSessionInfo pUserInfo)
        {
            this._currentBusinessInfo = new ETCLTemplateInfo();
            this._currentBusinessInfo.Lang = pUserInfo.Lang;
            this.CurrentUserInfo = pUserInfo;
 

        }
        #endregion

        #region Transform
        protected override IETCLDataItem[] Transform(DataTable pDataSource, out IETCLResultItem[] oResult)
        {
            oResult = null;
            return null;
        }
        #endregion

        #region Check
        protected override bool Check(IETCLDataItem[] pDataItems, out IETCLResultItem[] oResults)
        {
            oResults = null;
            return true;
        }
        #endregion

        #region Load
        protected override int Load(JIT.Utility.ETCL.Interface.IETCLDataItem[] pItems, out IETCLResultItem[] pResult, IDbTransaction pTran)
        {
            pResult = null;
            if (pItems == null || pItems.Length == 0)
            {
                return 0;
            }
            int rowCount = pItems.Length;
            VIPImportLoader mRouteImportLoader = new VIPImportLoader();
            //mRouteImportLoader.Process(null, pItems, this.CurrentUserInfo as LoggingSessionInfo, out pResult, null);
            return rowCount;
        }
        #endregion

    }
}
