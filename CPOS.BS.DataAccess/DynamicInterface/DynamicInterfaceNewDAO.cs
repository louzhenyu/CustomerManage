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
using System.Data;
using System.Text;
using JIT.CPOS.BS.Entity;
using JIT.Utility.Reflection;

namespace JIT.CPOS.BS.DataAccess
{

    /// <summary>
    /// 数据访问：  
    /// 表ShoppingCart的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class DynamicInterfaceDAO : Base.BaseCPOSDAO
    {
        #region GetEventAlbumByAlbumID
        /// <summary>
        /// 根据ID查询视频详细
        /// </summary>
        /// <param name="pEntity"></param>
        /// <returns></returns>
        public DataSet GetEventAlbumByAlbumID(ReqData<getLEventsAlbumEntity> pEntity)
        {
            string pSql = @"select AlbumId,ImageUrl,Title,Intro,Description as VideoUrl,es.BrowseNum as BrowseCount
            ,es.PraiseNum as PraiseCount,es.BookmarkNum as BookmarkCount,es.ShareNum as ShareCount
            from LEventsAlbum as Lea    
            left join EventStats as es
            on es.ObjectID=lea.AlbumId and es.IsDelete=lea.IsDelete        
            where Lea.IsDelete=0            
            and Lea.AlbumId='{0}'  
            update EventStats 
            set  BrowseNum=isnull(BrowseNum,0)+1  
            where ObjectID='{0}' 
            insert into EventStatsDetail(DetailID,ObjectID,ObjectType,CountType,VipID,CustomerID,CreateBy,IsDelete)
			values(newID(),'{0}','1','1','{1}','{2}','{1}','0')";
            pSql = string.Format(pSql, pEntity.special.AlbumID, pEntity.common.userId,CurrentUserInfo.ClientID);
            return this.SQLHelper.ExecuteDataset(pSql);
        }
        #endregion
    }
}
