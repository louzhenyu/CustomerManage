using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.BLL
{
    public class VIPCollectionDataBLL : BaseService
    {
        JIT.CPOS.BS.DataAccess.VIPCollectionDataService vIPCollectionDataService = null;
        public VIPCollectionDataBLL(LoggingSessionInfo loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
            vIPCollectionDataService = new DataAccess.VIPCollectionDataService(loggingSessionInfo);
        }

        public IList<VIPCollectionDatEntity> GetCollectionPropertyList(VIPCollectionDatEntity queryEntity, int pageIndex, int pageSize)
        {
            if (pageSize <= 0) pageSize = 15;

            IList<VIPCollectionDatEntity> list = new List<VIPCollectionDatEntity>();
            DataSet ds = new DataSet();
            ds = vIPCollectionDataService.GetCollectionPropertyList(queryEntity, pageIndex, pageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<VIPCollectionDatEntity>(ds.Tables[0]);
            }
            return list;
          
        }

        public int GetCollectionPropertyListCount(VIPCollectionDatEntity queryEntity)
        {
            return vIPCollectionDataService.GetCollectionPropertyListCount(queryEntity);
        }
    }
}
