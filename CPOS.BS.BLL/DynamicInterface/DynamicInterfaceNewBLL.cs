/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/23 17:47:45
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
using JIT.Utility.Reflection;
using JIT.CPOS.BS.Entity.Interface;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class DynamicInterfaceBLL
    {
        #region GetEventAlbumByAlbumID 
        /// <summary>
        /// 根据ID查询视频详细
        /// </summary>
        /// <param name="pEntity"></param>
        /// <returns></returns>
        public reqLEventsAlbumEntity GetEventAlbumByAlbumID(ReqData<getLEventsAlbumEntity> pEntity)
        {
            reqLEventsAlbumEntity pAlbumEntity= new reqLEventsAlbumEntity();
            DataSet ds = this._currentDAO.GetEventAlbumByAlbumID(pEntity);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                LEventsAlbumDetail[] entity = DataLoader.LoadFrom<LEventsAlbumDetail>(ds.Tables[0]);
                pAlbumEntity.Album = entity[0];
            }
            return pAlbumEntity;
        }
        #endregion
    }
}