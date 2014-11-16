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
    public partial class LEventsAlbumPhotoBLL
    {
        public object GetByAlbumID(string pAlbumID)
        {
            var ds = this._currentDAO.GetByAlbumID(pAlbumID);
            List<LEventsAlbumPhotoEntity> list = new List<LEventsAlbumPhotoEntity> { };
            using (var rd = ds.Tables[0].CreateDataReader())
            {
                while (rd.Read())
                {
                    LEventsAlbumPhotoEntity m;
                    this._currentDAO.NewLoad(rd, out m);
                    list.Add(m);
                }
            }
            var count = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            var data = new
            {
                count = count,
                photoList = list.Select(t => new
                {
                    photoId = t.PhotoId,
                    title = t.Title,
                    readerCount = t.ReaderCount,
                    linkUrl = t.LinkUrl,
                    displayIndex = t.SortOrder
                }).ToArray()
            };
            return data;

        }

        public void DeleAlbumPhoto(string pAlbumID)
        {
            this._currentDAO.DeleAlbumPhoto(pAlbumID);
        
        }
    }
}