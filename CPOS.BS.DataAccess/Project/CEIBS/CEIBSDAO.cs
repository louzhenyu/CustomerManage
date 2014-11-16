/*
 * Author		:jun.tian
 * EMail		:jun.tian@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/20 14:40:09
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
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using System.Diagnostics;

namespace JIT.CPOS.BS.DataAccess
{
    public partial class CEIBSDAO : JIT.CPOS.BS.DataAccess.Base.BaseCPOSDAO
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public CEIBSDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region EventStatsPageData
        public DataSet EventStatsPageData(string Type,string ObjectID, int? pPageSize, int? pPageIndex)
        {

            #region 拼接SQL
            StringBuilder strb = new StringBuilder();
            strb.Append(string.Format(@"
                 select EventStatsID,ObjectID,ObjectType,Op.OptionText as Type_Name,BrowseNum,PraiseNum,BookMarkNum
                  ,ROW_NUMBER() Over (Order By Ev.Sequence ASC)ROW_NUMBER,Oi.Title
                  into #eventStatsTemp from Eventstats as Ev
                 ")
                );
            strb.AppendLine(string.Format(@" inner join Options as Op on Op.ClientID=Ev.CustomerID AND Op.OptionName='Eventtats' AND Op.OptionValue=Ev.ObjectType "));
            strb.AppendLine(string.Format(@" left join 
                                  ( select NewsID,Title from 
                                  (   
                                   select 
                                 AlbumId NewsID
                    ,1 NewsType
                    ,'视频' NewsTypeText
                    ,Title
                 
                from LEventsAlbum
                where IsDelete=0 
                union all
                select 
                    EventID NewsID 
                    ,2 NewsType
                    ,'活动' NewsTypeText
                    ,Title
                   
                from LEvents
                where IsDelete=0  
                union all
                select 
                    NewsID
                    ,3 NewsType
                    ,'资讯' NewsTypeText
                    ,NewsTitle Title
                    
                from LNews
                where IsDelete=0 ) Oi) as Oi on Oi.NewsID=Ev.ObjectID
             "));
            strb.AppendFormat("where Ev.CustomerID='{0}' AND Ev.IsDelete='0' ", CurrentUserInfo.ClientID);
            if (!string.IsNullOrEmpty(Type))
            {
                strb.AppendLine(string.Format(@" AND Ev.ObjectType='{0}'",Type));
            }
            if (!string.IsNullOrEmpty(ObjectID))
            {
                strb.AppendLine(string.Format(@" AND Ev.ObjectID='{0}'", ObjectID));
            }
            strb.AppendLine(GetEvPageSQL(pPageSize, pPageIndex).ToString());
            #endregion

            return this.SQLHelper.ExecuteDataset(strb.ToString());
        }
        #endregion

        #region GetEvPageSQL
        /// <summary>
        /// 返回分页SQL
        /// </summary>
        /// <param name="pPageSize"></param>
        /// <param name="pPageIndex"></param>
        /// <returns></returns>
        public StringBuilder GetEvPageSQL(int? pPageSize, int? pPageIndex)
        {
            StringBuilder pageSql = new StringBuilder();
            pageSql.AppendLine(string.Format(@"
            declare @PageIndex int ={0}
            declare @PageSize int={1}
            declare @PageCount int
            declare @RowsCount int
            declare @PageStart int
            declare @PageEnd int
            SELECT @RowsCount=COUNT(1) FROM #eventStatsTemp
            if(@RowsCount%@PageSize=0)
                begin
                    set @PageCount=@RowsCount/@PageSize
                end
            else
                begin
                    set @PageCount=@RowsCount/@PageSize+1
                end
            if(@PageIndex<0)
                begin
                    set @PageIndex=0
                end
            else if(@PageIndex>=@PageCount)
                begin
                    set @PageIndex=@PageCount
                end
            set @PageStart=@PageIndex*@PageSize
            set @PageEnd=@PageStart+@PageSize
            set @PageEnd=@PageStart+@PageSize
            SELECT * FROM #eventStatsTemp WHERE ROW_NUMBER between  @PageStart+1 and @PageEnd
            SELECT @RowsCount RowsCount,@PageCount PageCount
            DROP TABLE #eventStatsTemp", pPageIndex, pPageSize));
            return pageSql;
        }
        #endregion

        #region DelEventStats
        public int DelEventStats(string EventStatsID)
        {
            StringBuilder strbSql = new StringBuilder();
            strbSql.Append(
                string.Format(@"
               declare @ObjectID nvarchar
               select @ObjectID=ObjectID from EventStats where EventStatsID='{0}'
               update Eventstats set IsDelete='1' where EventStatsID='{0}'
               update EventStatsDetail set IsDelete='1' where ObjectID=@ObjectID and CustomerID='{1}'
            ", EventStatsID,CurrentUserInfo.ClientID));
            return this.SQLHelper.ExecuteNonQuery(strbSql.ToString());    
        }
        #endregion

        #region SaveEventStats
        public int SaveEventStats(string EventStatsID, string ObjectType, string ObjectID, string BrowseNum, string PraiseNum, string BookMarkNum, string Sequence)
        {
            StringBuilder strbSql = new StringBuilder();
            if (!string.IsNullOrEmpty(EventStatsID))
            {
                strbSql.AppendFormat(@" declare @rowcount int 
                                        select @rowcount=count(1) from Eventstats where ObjectID='{0}' and CustomerID='{1}' and IsDelete='0' and EventStatsID!='{2}'
                                        ", ObjectID, CurrentUserInfo.ClientID,EventStatsID);
                strbSql.AppendFormat(@" if @rowcount<=0
                                          begin ");

                strbSql.Append(string.Format(
                    @"
                     update Eventstats set ObjectID='{1}',ObjectType='{2}',Sequence='{3}' where EventStatsID='{0}' 
                    "
                    , EventStatsID, ObjectID, ObjectType, Sequence));
                strbSql.AppendFormat(" end ");

            }
            else
            {
                strbSql.AppendLine(string.Format(@"declare @rowcount int"));
                strbSql.Append(string.Format(@"select @rowcount=count(1) from Eventstats where CustomerID='{0}' AND ObjectID='{1}'
                                     if @rowcount<=0 ", CurrentUserInfo.ClientID, ObjectID));
                strbSql.AppendFormat(" begin ");
                strbSql.AppendLine(string.Format(@" declare @GUID uniqueidentifier"));
                strbSql.AppendLine(string.Format(@" set @GUID=NEWID()"));
                strbSql.AppendLine(string.Format(@" insert into Eventstats(EventStatsID,ObjectID,ObjectType,BrowseNum,PraiseNum,BookmarkNum,Sequence,CustomerID)
                                   Values(@GUID,'{1}','{2}',{3},'{4}','{5}','{6}','{7}')",
                                   EventStatsID, ObjectID, ObjectType, BrowseNum,PraiseNum,BookMarkNum,Sequence,CurrentUserInfo.ClientID));
                strbSql.AppendFormat("end");
            }
            return this.SQLHelper.ExecuteNonQuery(strbSql.ToString());
        }
        
        #endregion

        #region GetEventStatsType
        public DataSet GetEventStatsType()
        {
            StringBuilder strbSql = new StringBuilder();
            strbSql.AppendFormat(@"
            select OptionValue,OptionText 
              from Options
              where
              OptionName='{0}'
              AND ClientID='{1}'
            ", "Eventtats", CurrentUserInfo.ClientID);
            return this.SQLHelper.ExecuteDataset(strbSql.ToString());
        }
        #endregion

        #region GetOptionID
        public DataSet GetOptionID(string ObjectType)
        {
            StringBuilder strbSql = new StringBuilder();
            strbSql.Append(string.Format(@"
 select * from 
   (   
      select 
             AlbumId NewsID
                    ,1 NewsType
                    ,'视频' NewsTypeText
                    ,Title
                 
                from LEventsAlbum
                where IsDelete=0 
                union all
                select 
                    EventID NewsID 
                    ,2 NewsType
                    ,'活动' NewsTypeText
                    ,Title
                   
                from LEvents
                where IsDelete=0  And CustomerId='{0}'
                union all
                select 
                    NewsID
                    ,3 NewsType
                    ,'资讯' NewsTypeText
                    ,NewsTitle Title
                    
                from LNews
                where IsDelete=0  And CustomerId='{0}' )Oi
               where NewsType='{1}'

         ", CurrentUserInfo.ClientID, ObjectType));
         return  this.SQLHelper.ExecuteDataset(strbSql.ToString());
        }
        #endregion

        #region GetEventStatsDetail
        public DataSet GetEventStatsDetail(string EventStatsID)
        {
            StringBuilder strbSql = new StringBuilder();
            strbSql.AppendFormat("select ObjectType,ObjectID,Sequence from Eventstats where EventStatsID='{0}'", EventStatsID);
            return this.SQLHelper.ExecuteDataset(strbSql.ToString());
        }
        #endregion
    }
}
