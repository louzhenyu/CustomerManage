/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013-12-18 17:58
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
    /// 数据访问：  
    /// 表LEventsAlbumPhoto的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class LEventsAlbumPhotoDAO : Base.BaseCPOSDAO, ICRUDable<LEventsAlbumPhotoEntity>, IQueryable<LEventsAlbumPhotoEntity>
    {
        public DataSet GetByAlbumID(string pAlbumID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("select * from LEventsAlbumPhoto where isdelete=0 and albumid='{0}'", pAlbumID));
            sb.AppendLine(string.Format("select count(*) from LEventsAlbumPhoto where isdelete=0 and albumid='{0}'", pAlbumID));
            DataSet ds = this.SQLHelper.ExecuteDataset(sb.ToString());
            return ds;
        }

        public void NewLoad(IDataReader rd, out LEventsAlbumPhotoEntity m)
        {
            this.Load(rd, out m);
        }

        public void DeleAlbumPhoto(string pAlbumID)
        {
            string str = "update LEventsAlbumPhoto set IsDelete=1 where albumid='" + pAlbumID + "' ";
            this.SQLHelper.ExecuteNonQuery(str);
        }
    }
}
