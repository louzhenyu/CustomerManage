/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/25 13:51:20
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
    /// 业务处理：  
    /// </summary>
    public partial class PanicbuyingEventSkuMappingBLL
    {

        public DataSet GetEventMerchandise(string pEventId)
        {

            return this._currentDAO.GetEventMerchandise(pEventId);
        }

        public DataSet GetGetEventMerchandiseSku(string pEventItemMappingId)
        {
            return this._currentDAO.GetGetEventMerchandiseSku(pEventItemMappingId);
        }

        public DataSet GetItemSku(string pEventId, string pItemId, string EventItemMappingId)
        {
            return this._currentDAO.GetItemSku(pEventId, pItemId, EventItemMappingId);
        }

        public void DeleteEventItemSku(string pEventItemMappingId)
        {

            this._currentDAO.DeleteEventItemSku(pEventItemMappingId);
        }

        public PanicbuyingEventSkuMappingEntity GetEventItemSku(object pID)
        {
            return this._currentDAO.GetEventItemSku(pID);
        }



        public DataSet GetTCTWPanicbuyingEventKV(string EventId)
        {
            return this._currentDAO.GetTCTWPanicbuyingEventKV(EventId);
        }
    }
}