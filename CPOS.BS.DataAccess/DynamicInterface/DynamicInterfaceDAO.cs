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
using System.Linq;
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
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public DynamicInterfaceDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region getUserIDByOpenID
        public string getUserIDByOpenID(getUserIDByOpenIDEntity pEntity)
        {
            string sql = "select  top 1 VipID as UserID  from vip where ClientID='{0}' and WeiXinUserId='{1}'";
            sql = string.Format(sql, pEntity.common.customerId, pEntity.common.openId);
            return this.SQLHelper.ExecuteScalar(sql).ToString();
        }
        #endregion

        #region getCodeByPhone
        public int getCodeByPhone(getCodeByPhoneEntity pEntity)
        {
            string sql = @"
                        update RegisterValidationCode set IsValidated=1,LastUpdateTime=getdate() where Mobile='{0}' and IsValidated=0; 
                        insert RegisterValidationCode(Mobile,Code,IsValidated,IsDelete,CreateBy)
                            values('{0}','{1}',0,0,1);";
            sql = string.Format(sql, pEntity.special.Phone, pEntity.special.Code, pEntity.common.customerId);
            return this.SQLHelper.ExecuteNonQuery(sql);
        }
        #endregion

        #region getSign
        public string getSign(getCodeByPhoneEntity pEntity)
        {
            string sql = @"select unit_name  from T_unit where TYPE_ID='2F35F85CF7FF4DF087188A7FB05DED1D' and Customer_ID='{0}';";
            sql = string.Format(sql, pEntity.common.customerId);
            return this.SQLHelper.ExecuteScalar(sql).ToString();
        }
        #endregion

        #region getUserByPhoneAndCode
        public string getUserByPhoneAndCode(getUserByPhoneAndCodeEntity pEntity)
        {
            string sql = @"if exists( select * from RegisterValidationCode where Mobile='{0}' and Code='{2}' and IsDelete=0 
                        and IsValidated=0)
                        begin
                        if  exists( select vipid  from Vip where Phone='{0}' and ClientID='{1}')
                        begin
                        select vipid  from Vip where Phone='{0}' and ClientID='{1}'
                        end
                        else 
                        begin
                        insert VIP(vipID,Phone,ClientID,IsDelete)
                        values(newID(),'{0}','{1}',0)
                        insert VIPRoleMapping(RoleID,VipID)
                        select '{3}',vipid  from Vip where Phone='{0}' and ClientID='{1}';
                        select vipid  from Vip where Phone='{0}' and ClientID='{1}'
                        end
                        end
                        else
                        begin
                        select ''
                        end
                        ";
            sql = string.Format(sql, pEntity.special.Phone, pEntity.common.customerId, pEntity.special.Code, pEntity.common.roleid);
            return this.SQLHelper.ExecuteScalar(sql).ToString();
        }
        #endregion

        #region getUserDefinedByUserID
        public DataSet getUserDefinedByUserID(ReqData<getUserDefinedByUserIDEntity> pEntity)
        {
            if (string.IsNullOrEmpty(pEntity.common.userId))
            {
                pEntity.common.userId = Guid.NewGuid().ToString();
            }
            string sql = "";
            if (pEntity.special.TypeID == 1)//注册，目前协会宝注册每个客户只有一套注册配置，后面要改成传入Module的Code也可以兼容
            {
                sql = string.Format(@"
select * from mobilePageBlock  as mpb  where
mpb.CustomerID='{0}'
and mpb.tablename='vip'
order by mpb.Type,mpb.Sort

select mbd.* into #Temp_Table from MobileBussinessDefined as mbd
inner join MobilePageBlock as mpb
on mpb.IsDelete=mbd.IsDelete and mbd.CustomerID=mpb.CustomerID  and mpb.TableName='vip'
and mbd.mobilePageBlockID=mpb.mobilePageBlockID
where mbd.IsDelete=0 and mbd.CustomerID='{0}' and TypeID={2}
 
select * from #Temp_Table

select * from Options  as op 
inner join #Temp_Table as tt
on tt.CorrelationValue=op.OptionName and tt.ControlType in(7,8,6)
where ISNULL(op.ClientID,'{0}')='{0}' and op.isdelete=0
order by op.OptionText

select top 1 * from Vip where VipId='{1}'
drop table #Temp_Table",
                        pEntity.common.customerId,
                        pEntity.common.userId,
                        pEntity.special.TypeID);
            }
            else if (pEntity.special.TypeID == 2)//活动，活动必须使用MobileModuleObjectMapping关联表进行查询
            {
                sql = string.Format(@"
--根据关联表取页的信息
SELECT mpb.* 
into #mobilepageblock
FROM dbo.MobileModuleObjectMapping mmom
INNER JOIN dbo.MobileModule mm ON mmom.MobileModuleID = mm.MobileModuleID AND mmom.CustomerID = mm.CustomerID AND mmom.IsDelete = mm.IsDelete
INNER JOIN dbo.MobilePageBlock mpb ON mm.MobileModuleID = mpb.MobileModuleID AND mm.CustomerID = mpb.CustomerID AND mm.IsDelete = mpb.IsDelete
WHERE mmom.ObjectID='{2}' AND mmom.CustomerID='{0}' AND mmom.IsDelete=0
ORDER BY mpb.Type,mpb.Sort

--根据页取控件的信息
SELECT mbd.*
INTO #mobilebusinessdefined
FROM #mobilepageblock mpb
INNER JOIN dbo.MobileBussinessDefined mbd ON mpb.MobilePageBlockID = mbd.MobilePageBlockID AND mpb.CustomerID = mbd.CustomerID AND mpb.IsDelete = mbd.IsDelete

select * from #mobilepageblock
select * from #mobilebusinessdefined

--根据控件取下拉选项的信息
select * from Options  as op 
inner join #mobilebusinessdefined as tt
on tt.CorrelationValue=op.OptionName and tt.ControlType in(7,8,6)
where ISNULL(op.ClientID,'{0}')='{0}' and op.isdelete=0
order by op.OptionText

--取会员的信息，用于前台页面绑定默认值（如果有vipid）
select top 1 * from Vip where VipId='{1}'

drop table #mobilepageblock
drop table #mobilebusinessdefined
",
                       pEntity.common.customerId,
                       pEntity.common.userId,
                       pEntity.special.EventID);
            }

            return this.SQLHelper.ExecuteDataset(sql);
        }
        /// <summary>
        /// 获取报名表单 Add By Alan 2014-08-29
        /// </summary>
        /// <param name="eventId">活动Id</param>
        /// <returns></returns>
        public DataSet GetSignUpForm(string eventId)
        {
            string sql = string.Format(@"
                    --根据关联表取页的信息
                    SELECT mpb.* 
                    into #mobilepageblock
                    FROM dbo.MobileModuleObjectMapping mmom
                    INNER JOIN dbo.MobileModule mm ON mmom.MobileModuleID = mm.MobileModuleID AND mmom.CustomerID = mm.CustomerID AND mmom.IsDelete = mm.IsDelete
                    INNER JOIN dbo.MobilePageBlock mpb ON mm.MobileModuleID = mpb.MobileModuleID AND mm.CustomerID = mpb.CustomerID AND mm.IsDelete = mpb.IsDelete
                    WHERE mmom.ObjectID='{2}' AND mmom.CustomerID='{0}' AND mmom.IsDelete=0
                    ORDER BY mpb.Type,mpb.Sort
                    
                    --根据页取控件的信息
                    SELECT mbd.*
                    INTO #mobilebusinessdefined
                    FROM #mobilepageblock mpb
                    INNER JOIN dbo.MobileBussinessDefined mbd ON mpb.MobilePageBlockID = mbd.MobilePageBlockID AND mpb.CustomerID = mbd.CustomerID AND mpb.IsDelete = mbd.IsDelete
                    
                    select * from #mobilepageblock
                    select * from #mobilebusinessdefined
                    
                    --根据控件取下拉选项的信息
                    select * from Options  as op 
                    inner join #mobilebusinessdefined as tt
                    on tt.CorrelationValue=op.OptionName and tt.ControlType in(7,8,6)
                    where ISNULL(op.ClientID,'{0}')='{0}' and op.isdelete=0
                    order by op.OptionText
                    
                    --取会员的信息，用于前台页面绑定默认值（如果有vipid）
                    select top 1 * from Vip where VipId='{1}'
                    
                    drop table #mobilepageblock
                    drop table #mobilebusinessdefined
                    ", CurrentUserInfo.ClientID, CurrentUserInfo.UserID, eventId);

            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        #region submitUserInfo
        public int submitUserInfo(string pSql)
        {
            return this.SQLHelper.ExecuteNonQuery(pSql);
        }
        #endregion

        #region getNewsList
        public DataSet getNewsList(ReqData<getNewsListEntity> pEntity)
        {
            string pWhere = "";
            string sql = @"select  NewsId,NewsType,NewsTitle,NewsSubTitle,'' as Content,convert(nvarchar(10),PublishTime,120)as PublishTime,ContentUrl,
                        ImageUrl,ThumbnailImageUrl,Author,[BrowseCount],[PraiseCount],CollCount from 
                        ( select row_number()over( order by CreateTime desc)as ID,* from 
                        (select NewsId,NewsType,NewsTitle,NewsSubTitle,'' as Content,convert(nvarchar(10),PublishTime,120)as PublishTime,ContentUrl,
                        ImageUrl,ThumbnailImageUrl,Author,CreateTime,[BrowseCount],[PraiseCount],CollCount from LNews
                        where CustomerId='{0}' and IsDelete=0 {3}) as ASBC) as aaa
                            where ID>{1} and ID<={2};
                        select count(*) as TableCount from LNews
                        where CustomerId='{0}' and IsDelete=0 {3}";
            if (!string.IsNullOrEmpty(pEntity.special.NewsType))
            {
                pWhere = "and NewsType= '" + pEntity.special.NewsType + "'";
            }
            sql = string.Format(sql, pEntity.common.customerId, pEntity.special.pageSize * (pEntity.special.page - 1), pEntity.special.pageSize * (pEntity.special.page), pWhere);
            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        #region getNewsDetailByNewsID
        public DataSet getNewsDetailByNewsID(ReqData<getNewsDetailByNewsIDEntity> pEntity)
        {
            string sql = @"select L.NewsId,L.NewsType,L.NewsTitle,L.NewsSubTitle,L.Content,
                            convert(nvarchar(10),L.PublishTime,120)as PublishTime,L.ContentUrl,
                            L.ImageUrl,L.ThumbnailImageUrl,L.Author,L.BrowseCount,L.PraiseCount,L.CollCount,nc.NewsCountID
                            from LNews  as L
                            left join NewsCount as nc
                            on  nc.NewsID=l.NewsID
                            and nc.IsDelete=l.IsDelete  and nc.VipID='{1}' and nc.CountType='3'
                            where
                            l.NewsId='{0}';
                            update LNews set  BrowseCount=isnull(BrowseCount,0)+1  where NewsID='{0}';";
            sql = string.Format(sql, pEntity.special.NewsID, pEntity.common.userId);
            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        #region GetUserInfo
        public DataSet GetUserInfo(string pUserID)
        {
            string sql = @"select op1.OptionText as VipSchool,op2.OptionText as VipCourse,op2.OptionText as IsMarital,v.* from
                        vip as v
                        left join Options as op1
                        on convert(nvarchar(10),op1.OptionValue)=v.Col1 and op1.OptionName='VipSchool' and isnull(op1.ClientID,v.ClientID)=v.ClientID
                        and op1.IsDelete=v.IsDelete
                        left join Options as op2
                        on convert(nvarchar(10),op2.OptionValue)=v.Col2 and op2.OptionName='VipCourse' and isnull(op2.ClientID,v.ClientID)=v.ClientID
                        and op2.IsDelete=v.IsDelete
                        left join Options as op3
                        on convert(nvarchar(10),op3.OptionValue)=v.Col9 and op3.OptionName='IsMarital' and isnull(op3.ClientID,v.ClientID)=v.ClientID
                        and op3.IsDelete=v.IsDelete
                        where  v.IsDelete=0 and vipID='{0}'";
            sql = string.Format(sql, pUserID);
            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        #region GetUserEmail
        public string GetUserEmail(string pUserID)
        {
            string sql = @"select Email from Vip where VIPID='{0}'";
            sql = string.Format(sql, pUserID);
            return this.SQLHelper.ExecuteScalar(sql).ToString();
        }
        #endregion

        #region getUserByLogin  注册和登陆接口
        public string getUserByLogin(ReqData<getUserByLoginEntity> pEntity)
        {
            string sql = "";
            if (pEntity.special.IsLogin)
            {
                sql = @"declare @VipLoginID  nvarchar(100)  
                select @VipLoginID=VIPID from Vip where (VipName='{0}' or Email='{0}' or Phone='{0}') and VipPasswrod='{1}' and ClientID='{2}'
                and IsDelete=0;
                if @VipLoginID<>''
                begin
                select @VipLoginID
                end
                else
                begin
                select '-1'
                end";
                sql = string.Format(sql, pEntity.special.LoginName, pEntity.special.PassWord, pEntity.common.customerId);
            }
            else
            {
                sql = @"declare @VipID nvarchar(100) 
                    select @VipID=VIPID from Vip where VipName='{0}' and ClientID='{2}'
                    and IsDelete=0;
                    if @VipID<>''
                    begin
                    select '-2'
                    end
                    else 
                    begin 
                    set @VipID=newID()
                    insert VIP(vipID,VipName,VipPasswrod,ClientID,IsDelete)
                    values(@VipID,'{0}','{1}','{2}',0)
                    insert VIPRoleMapping(RoleID,VipID)
                    values('{3}',@VipID);
                    select @VipID
                    end";
                sql = string.Format(sql, pEntity.special.LoginName, pEntity.special.PassWord, pEntity.common.customerId, pEntity.common.roleid);
            }
            return this.SQLHelper.ExecuteScalar(sql).ToString();
        }
        #endregion

        #region checkUserEmail  验证Email是否存在
        public bool checkUserEmail(string email, string userId, string clientId)
        {
            string sql = "select count(*) from Vip where IsDelete=0 and Status=2 and  ClientID='" + clientId + "' and Email='" + email + "'";
            if (!string.IsNullOrEmpty(userId))
            {
                sql += " and VIPID<>'" + userId + "'";
            }
            var obj = this.SQLHelper.ExecuteScalar(sql);
            if (obj == DBNull.Value) return false;
            var count = Convert.ToInt32(obj);
            return count > 0 ? true : false;
        }
        #endregion

        #region checkUserPhone  验证手机号是否存在
        public bool checkUserPhone(string phone, string userId, string clientId)
        {
            string sql = "select count(*) from Vip where IsDelete=0 and Status=2 and ClientID='" + clientId + "' and Phone='" + phone + "'";
            if (!string.IsNullOrEmpty(userId))
            {
                sql += " and VIPID<>'" + userId + "'";
            }
            var obj = this.SQLHelper.ExecuteScalar(sql);
            if (obj == DBNull.Value) return false;
            var count = Convert.ToInt32(obj);
            return count > 0 ? true : false;
        }
        #endregion

        #region getTopList 获取活动列表
        public DataSet getTopList(ReqData<getTopListEntity> pEntity)
        {
            string sql = "";
            string pWhere = "";
            if (pEntity.special.Type == 1)
            {
                sql = @"select Top {0} NewsId,NewsTitle,1 as Type,NewsType,ImageUrl,Convert(nvarchar(20),PublishTime,120) as PublishTime,ThumbnailImageUrl,CreateTime from LNews  where CustomerId='{1}' 
                            and IsDelete=0 and IsTop=1 {2}
                            order by CreateTime desc";
                if (!string.IsNullOrEmpty(pEntity.special.NewsType) && pEntity.special.NewsType != "0")
                {
                    pWhere = "and NewsType='" + pEntity.special.NewsType + "'";
                }
            }
            else if (pEntity.special.Type == 2)
            {
                pWhere = "";
                sql = @"select top {0} EventID as NewsId,Title as NewsTitle,2 as Type,'0'   as NewsType,ImageUrl,
                            Convert(nvarchar(20),BeginTime,120) as PublishTime,ImageUrl as ThumbnailImageUrl,CreateTime   from LEvents  where CustomerId='{1}' 
                            and IsDelete=0 and IsTop=1 {2}
                            order by CreateTime desc  ";
            }
            else
            {
                sql = @"select top {0} * from (
                    select NewsId,NewsTitle,1 as Type,NewsType,ImageUrl,Convert(nvarchar(20),PublishTime,120) as PublishTime,ThumbnailImageUrl,CreateTime from LNews  where CustomerId='{1}' 
                    and IsDelete=0 and IsDefault=1  {2}
                    union all
                    select EventID as NewsId,Title as NewsTitle,2 as Type ,'0' as NewsType,ImageUrl,
                     Convert(nvarchar(20),BeginTime,120) as PublishTime,ImageUrl as ThumbnailImageUrl,CreateTime   from LEvents  where CustomerId='{1}' 
                    and IsDelete=0 and IsDefault=1 ) as abcd
                    order by CreateTime desc";
                if (!string.IsNullOrEmpty(pEntity.special.NewsType) && pEntity.special.NewsType != "0")
                {
                    pWhere = "and NewsType='" + pEntity.special.NewsType + "'";
                }
            }
            sql = string.Format(sql, pEntity.special.pageSize, pEntity.common.customerId, pWhere);
            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        #region getActivityList 获取活动列表
        public DataSet getActivityList(ReqData<getActivityListEntity> pEntity)
        {
            string sql = @" select COUNT(*) as  EventVipTickCount,isnull(SUM(case status when 1 then 0 end),0) as StatusCount
                            ,EventID into #Temp_B from EventVipTicket 
                            where IsDelete=0 and CustomerId='{0}' 
                            group by EventID;
                            select * into #Temp_C  from ( select row_number()over(  order by btss desc,bt)as ID,* from 
                            (select  case  when DATEDIFF(DD,GETDATE(),BeginTime)>-1 then 1 else 0 end as btss,
                            abs(DATEDIFF(DD,GETDATE(),BeginTime)) as bt,L.EventID as ActivityID, L.Title as  ActivityTitle,L.CityID as ActivityCity,
                            Convert(nvarchar(16),L.BeginTime,120) as BeginTime,Convert(nvarchar(16),L.EndTime,120) as EndTime,
                            cast(DATEDIFF(mi,getdate(),L.BeginTime)*1.0/24/60  as decimal(18,5)) as BeginDay,
                            cast(DATEDIFF(mi,getdate(),L.EndTime)*1.0/60/24 as decimal(18,5)) as EndDay,CreateTime
                            ,b.EventVipTickCount as UserCount,b.StatusCount
                            from LEvents  as L
                            left join #Temp_B as b
                            on b.EventID=l.EventID
                            where L.CustomerId='{0}'
                            and L.IsDelete=0) as ASBC) as ABCDE
                            where ID>{1} and ID<={2};                            
                            drop table #Temp_B;
                            select * from #Temp_C
                            select count(*) as Counts from LEvents
                            where CustomerId='{0}' and IsDelete=0;
                            select v.VIPID,v.VipName,evt.EventID,evt.Status from EventVipTicket  as evt 
                            inner  join Vip  as v
                            on v.Status=3 and v.VIPID=evt.VipID and v.IsDelete=0
                            inner join #Temp_C as c
                            on c.ActivityID=evt.EventID
                            where evt.CustomerId='{0}'  and  evt.IsDelete=0 order by  evt.CreateTime
                            drop table #Temp_C;";
            sql = string.Format(sql, pEntity.common.customerId, pEntity.special.pageSize * (pEntity.special.page - 1), pEntity.special.pageSize * (pEntity.special.page));
            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        #region getActivityByActivityID
        public DataSet getActivityByActivityID(ReqData<getActivityByActivityIDEntity> pEntity)
        {
            string sql = @"SELECT EventID as ActivityID,Title as ActivityTitle,Organizer as ActivityCompany,
                            BeginTime,EndTime,cast(DATEDIFF(mi,getdate(),BeginTime)*1.0/24/60  as decimal(18,5)) as BeginDay,
                            cast(DATEDIFF(mi,getdate(),EndTime)*1.0/60/24 as decimal(18,5)) as EndDay,Address as ActivityAddr,
                            Content as ActivityLinker,PhoneNumber as  ActivityPhone,'' as ActivityUp,Description as ActivityContent
                            FROM LEvents where EventID='{0}';                           
                            select TicketID,COUNT(ticketID) as TicketCount into #Temp_A from EventVipTicket where 
                            EventID='{0}' and IsDelete=0
                            group by ticketID ;
                            select convert(nvarchar(50),t.TicketID) as TicketID,t.TicketName,t.TicketRemark,t.TicketPrice,t.TicketNum,t.TicketSort,(t.TicketNum-isnull(evt.TicketCount,0)) as TicketMore from Ticket as t 
                            left join #Temp_A as evt on
                            evt.TicketID=t.TicketID
                            where t.EventID='{0}' and t.IsDelete=0; 
                            drop table #Temp_A
                            select COUNT(*) as  EventVipTickCount,isnull(SUM(case status when 1 then 0 end),0) as StatusCount from EventVipTicket  where EventId='{0}' and IsDelete=0;
                            select TicketID from EventVipTicket  where EventId='{0}' and IsDelete=0 and SginVipID='{1}';";
            sql = string.Format(sql, pEntity.special.ActivityID, pEntity.common.userId);
            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        #region submitActivityInfo
        /// <summary>
        /// 订单提交
        /// </summary>
        /// <param name="pSql"></param>
        /// <returns></returns>
        public int submitActivityInfo(string pSql)
        {
            return this.SQLHelper.ExecuteNonQuery(pSql);
        }
        #endregion

        #region ExecuteNonQuery
        /// <summary>
        /// 执行语句，返回是否成功
        /// </summary>
        /// <param name="pSql"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string pSql)
        {
            return this.SQLHelper.ExecuteNonQuery(pSql);
        }
        #endregion

        #region ExecuteScalar
        /// <summary>
        /// 返回第一行第一列的值
        /// </summary>
        /// <param name="pSql"></param>
        /// <returns></returns>
        public object ExecuteScalar(string pSql)
        {
            return this.SQLHelper.ExecuteScalar(pSql);
        }
        #endregion


        #region 中欧活动相关接口
        #region getEventList 获取活动列表
        /// <summary>
        /// 获取活动列表信息
        /// </summary>
        /// <param name="pEntity"></param>
        /// <returns></returns>
        public DataSet getEventList(ReqData<getActivityListEntity> pEntity)
        {
            StringBuilder pWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(pEntity.special.EventTypeID))
            {
                pWhere.AppendFormat(" and EventTypeID='{0}' ", pEntity.special.EventTypeID);
            }

            if (!string.IsNullOrEmpty(pEntity.special.type) && pEntity.special.type == "new")
            {
                var reader = this.SQLHelper.ExecuteReader("select ActiveTime from [TimingPushMessageRule] where isdelete = 0 and ActiveTime is not null and clientid='" + base.CurrentUserInfo.ClientID + "'");
                if (reader.Read())
                {
                    if (reader["ActiveTime"] != null && !string.IsNullOrEmpty(reader["ActiveTime"].ToString()))
                        pWhere.AppendFormat(" and CreateTime between '{0}' and '{1}'", DateTime.Now.Date.AddDays(-1).ToShortDateString() + " " + DateTime.Parse(reader["ActiveTime"].ToString()).AddSeconds(1).TimeOfDay, DateTime.Now.Date.ToShortDateString() + " " + DateTime.Parse(reader["ActiveTime"].ToString()).AddSeconds(-1).TimeOfDay);
                }
                
            }

            string sql = @"  
            SELECT 
                COUNT(*) AS EventVipTickCount ,
                isnull(SUM(case IsCheck when 1 then 0 end),0) as StatusCount ,
                EventID
            INTO   #Temp_B
            FROM   LEventsVipObject
            WHERE  IsDelete = 0
            GROUP BY EventID ;

            select * into #Temp_C  from ( select row_number()over(  order by istop desc,btss desc,bt)as ID,* from 
            (select  case  when DATEDIFF(DD,GETDATE(),BeginTime)>-1 then 1 else 0 end as btss,
            abs(DATEDIFF(DD,GETDATE(),BeginTime)) as bt,L.EventID as ActivityID, L.Title as  ActivityTitle,L.CityID as ActivityCity, L.Address, L.IsSignUpList,
            Convert(nvarchar(16),L.BeginTime,120) as BeginTime,Convert(nvarchar(16),L.EndTime,120) as EndTime,
            cast(DATEDIFF(mi,getdate(),L.BeginTime)*1.0/24/60  as decimal(18,5)) as BeginDay,
            cast(DATEDIFF(mi,getdate(),DATEADD(DAY,1,L.EndTime))*1.0/60/24 as decimal(18,5)) as EndDay,CreateTime
            ,(
	            select COUNT(*) from leventsignup  where IsDelete=0 and EventID=L.EventID
            ) UserCount,b.StatusCount
			,ISNULL(L.IsTop,0) IsTop
            from LEvents  as L
            left join #Temp_B as b
            on b.EventID=l.EventID
            where L.CustomerId='{0}' {3}
            and L.IsDelete=0) as ASBC
            where EndDay>0--小于0的活动已结束
            ) as ABCDE
            where ID>{1} and ID<={2};                            
            drop table #Temp_B;
            select * from #Temp_C
            select count(*) as Counts from LEvents
            where CustomerId='{0}' and IsDelete=0 {3} ;

             SELECT 
                v.VIPID ,
                v.VipName ,
                evt.EventID ,
                Status=0--evt.Status
             FROM   LEventsVipObject AS evt
            INNER  JOIN Vip AS v ON v.Status = 3 AND v.VIPID = evt.VipID AND v.IsDelete = 0
            INNER JOIN #Temp_C AS c ON c.ActivityID = evt.EventID
            WHERE  evt.IsDelete = 0

            drop table #Temp_C;";
            sql = string.Format(sql, pEntity.common.customerId, pEntity.special.pageSize * (pEntity.special.page - 1), pEntity.special.pageSize * (pEntity.special.page), pWhere);
            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        #region getEventByEventID
        /// <summary>
        /// 根据活动ID获取活动信息，包含已报名和已缴费的会员信息，以及票的信息
        /// </summary>
        /// <param name="pEntity"></param>
        /// <returns></returns>
        public DataSet getEventByEventID(ReqData<getActivityByActivityIDEntity> pEntity)
        {
            string sql = @"
                            --取活动信息
                            SELECT l.EventID as ActivityID,l.Title as ActivityTitle,l.Email as ActivityEmail,l.Intro as ActivityIntro,
                            l.Organizer as ActivityCompany,l.BeginTime,l.EndTime,l.Longitude,l.Latitude,
                            cast(DATEDIFF(mi,getdate(),l.BeginTime)*1.0/24/60  as decimal(18,5)) as BeginDay,
                            cast(DATEDIFF(mi,getdate(),l.EndTime)*1.0/60/24 as decimal(18,5)) as EndDay,
                            l.Address as ActivityAddr,l.Content as ActivityLinker,l.PhoneNumber as  ActivityPhone,'' as ActivityUp,
                            l.Description as ActivityContent,l.IsTicketRequired,es.BrowseNum as BrowseCount
                            ,es.PraiseNum as PraiseCount,es.BookmarkNum as  BookmarkCount,es.ShareNum  as ShareCount
                            FROM LEvents  as l
                            left join EventStats as es
                            on es.ObjectID=l.EventID and es.IsDelete=l.IsDelete  
                            where EventID='{0}';    

                            --取活动报名信息
                            select sku_id,lvo.*,a.Field1 
                            into #Temp_C 
                            from LEventsVipObject as lvo
                            left join T_Inout as a on a.order_id=lvo.ObjectId  and a.status!=900 and a.status!=800
                            left join t_inout_detail as b on a.order_id=b.order_id
                            where lvo.EventId='{0}' and IsDelete=0

                            select sku_id as TicketID,COUNT(sku_id) as TicketCount 
                            into #Temp_A 
                            from #Temp_C
                            group by sku_id;

                            select a.*,lesm.Sort as TicketSort 
                            into #Temp_B 
                            from LEventsSKUMapping  as lesm
                            inner join 
                            ( select 
                            b.sku_id as TicketID ,--票ID
                            item_name as TicketName,--票的名称
                            item_remark as  TicketRemark,--票的简介
                            salesprice as TicketPrice,--票的价格
                            convert(int,qty) as TicketNum --票的数量
                            from vw_item_detail a,t_sku b
                            where a.item_id=b.item_id
                            and a.IsDelete=0 and a.CustomerId='{2}' ) as a on a.TicketID=lesm.skuID
                            where lesm.EventID='{0}' and lesm.IsDelete=0

                            select t.*,(t.TicketNum-isnull(evt.TicketCount,0)) as TicketMore 
                            from #Temp_B as t 
                            left join #Temp_A as evt on evt.TicketID=t.TicketID
                            order by TicketSort

                            --取报名人数,缴费人数
                            select EventVipTickCount=(select count(*) EventVipTickCount from LEventSignUp where IsDelete='0' and eventId='{0}'),
                            isnull(SUM(case Field1 when 1 then 0 end),0) as StatusCount
                            from #Temp_C

                            --是否报名
                            select top 1 signupid as TicketID 
                            from LEventSignUp
                            where EventID='{0}' AND VipID='{1}' AND IsDelete=0

                            drop table #Temp_A
                            drop table #Temp_B
                            drop table #Temp_C;";
            sql = sql + @" 
                        update EventStats 
                        set  BrowseNum=isnull(BrowseNum,0)+1  
                        where ObjectID='{0}' and CustomerID='{2}'            
                        insert into EventStatsDetail(DetailID,ObjectID,ObjectType,CountType,VipID,CustomerID,CreateBy,IsDelete)
                        values('" + Guid.NewGuid().ToString() + @"','{0}','2','1','{1}','{2}','{1}','0')";
            sql = string.Format(sql, pEntity.special.ActivityID, pEntity.common.userId, pEntity.common.customerId);

            return this.SQLHelper.ExecuteDataset(sql);
        }

        /// <summary>
        /// 根据活动ID获取活动信息，包含已报名和已缴费的会员信息，以及票的信息 Add By Alan 2014-08-29
        /// </summary>
        /// <param name="eventId">活动Id</param>
        /// <returns></returns>
        public DataSet GetEventDetail(string eventId)
        {
            string sql = @"
                            --取活动信息
                            SELECT l.EventID as ActivityID,l.Title as ActivityTitle,l.Email as ActivityEmail,l.Intro as ActivityIntro,
                            l.Organizer as ActivityCompany,l.BeginTime,l.EndTime,l.Longitude,l.Latitude,
                            cast(DATEDIFF(mi,getdate(),l.BeginTime)*1.0/24/60  as decimal(18,5)) as BeginDay,
                            cast(DATEDIFF(mi,getdate(),l.EndTime)*1.0/60/24 as decimal(18,5)) as EndDay,
                            l.Address as ActivityAddr,l.Content as ActivityLinker,l.PhoneNumber as  ActivityPhone,'' as ActivityUp,
                            l.Description as ActivityContent,l.IsTicketRequired,es.BrowseNum as BrowseCount
                            ,es.PraiseNum as PraiseCount,es.BookmarkNum as  BookmarkCount,es.ShareNum  as ShareCount
                            FROM LEvents  as l
                            left join EventStats as es
                            on es.ObjectID=l.EventID and es.IsDelete=l.IsDelete  
                            where EventID='{0}';    

                            --取活动报名信息
                            select sku_id,lvo.*,a.Field1 
                            into #Temp_C 
                            from LEventsVipObject as lvo
                            left join T_Inout as a on a.order_id=lvo.ObjectId  and a.status!=900 and a.status!=800
                            left join t_inout_detail as b on a.order_id=b.order_id
                            where lvo.EventId='{0}' and IsDelete=0

                            select sku_id as TicketID,COUNT(sku_id) as TicketCount 
                            into #Temp_A 
                            from #Temp_C
                            group by sku_id;

                            select a.*,lesm.Sort as TicketSort 
                            into #Temp_B 
                            from LEventsSKUMapping  as lesm
                            inner join 
                            ( select 
                            b.sku_id as TicketID ,--票ID
                            item_name as TicketName,--票的名称
                            item_remark as  TicketRemark,--票的简介
                            salesprice as TicketPrice,--票的价格
                            convert(int,qty) as TicketNum --票的数量
                            from vw_item_detail a,t_sku b
                            where a.item_id=b.item_id
                            and a.IsDelete=0 and a.CustomerId='{2}' ) as a on a.TicketID=lesm.skuID
                            where lesm.EventID='{0}' and lesm.IsDelete=0

                            select t.*,(t.TicketNum-isnull(evt.TicketCount,0)) as TicketMore 
                            from #Temp_B as t 
                            left join #Temp_A as evt on evt.TicketID=t.TicketID
                            order by TicketSort

                            --取报名人数,缴费人数
                            select EventVipTickCount=(select count(*) EventVipTickCount from LEventSignUp where IsDelete='0' and eventId='{0}'),
                            isnull(SUM(case Field1 when 1 then 0 end),0) as StatusCount
                            from #Temp_C

                            --是否报名
                            select top 1 signupid as TicketID 
                            from LEventSignUp
                            where EventID='{0}' AND VipID='{1}' AND IsDelete=0

                            drop table #Temp_A
                            drop table #Temp_B
                            drop table #Temp_C;";
            sql = sql + @" 
                        update EventStats 
                        set  BrowseNum=isnull(BrowseNum,0)+1  
                        where ObjectID='{0}' and CustomerID='{2}'            
                        insert into EventStatsDetail(DetailID,ObjectID,ObjectType,CountType,VipID,CustomerID,CreateBy,IsDelete)
                        values('" + Guid.NewGuid().ToString() + @"','{0}','2','1','{1}','{2}','{1}','0')";
            sql = string.Format(sql, eventId, CurrentUserInfo.UserID, CurrentUserInfo.ClientID);

            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        #region submitEventInfo
        public int submitEventInfo(string pSql)
        {
            return this.SQLHelper.ExecuteNonQuery(pSql);
        }
        #endregion
        #endregion

        #region getNewsType
        public LNewsTypeEntity[] getNewsType(int ChannelCode)
        {
            LNewsTypeEntity[] entity = null;
            StringBuilder sql = new StringBuilder();
            //容错性，如果原来的是1，
            sql.AppendFormat(@"
            select * from LNewsType where CustomerId='{0}' and isdelete=0 and TypeLevel = 1 and isVisble=0  and isnull(ChannelCode,1)={1}
            ", CurrentUserInfo.ClientID,ChannelCode);//根据channelCode来查
            DataSet ds = this.SQLHelper.ExecuteDataset(sql.ToString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                entity = DataLoader.LoadFrom<LNewsTypeEntity>(ds.Tables[0]);
            }
            return entity;
        }
        #endregion

        #region getEventTypeList
        public EventsTypeEntity[] getEventTypeList()
        {
            EventsTypeEntity[] entity = null;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
            select * from LEventsType where clientid='{0}' and isdelete=0
            ", CurrentUserInfo.ClientID);
            DataSet ds = this.SQLHelper.ExecuteDataset(sql.ToString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                entity = DataLoader.LoadFrom<EventsTypeEntity>(ds.Tables[0]);
            }
            return entity;
        }
        #endregion

        #region getMyEventList
        public DataSet getMyEventList()
        {
            EventsTypeEntity[] entity = null;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
            select * from LEventSignUp where vipid='{0}' and isdelete=0
            ", loggingSessionInfo.ClientID);
            DataSet ds = this.SQLHelper.ExecuteDataset(sql.ToString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                entity = DataLoader.LoadFrom<EventsTypeEntity>(ds.Tables[0]);
            }
            return null;
        }
        #endregion

        #region GetPageBlockID
        /// <summary>
        /// 根据配置ID获取模块ID
        /// </summary>
        /// <param name="pDefindID"></param>
        /// <returns></returns>
        public DataSet GetPageBlockID(string pDefindID)
        {
            //拼接SQL
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            select
            *
            from MobilePageBlock
            where MobilePageBlockID=(
	            select
		            MobilePageBlockID
	            from MobileBussinessDefined
	            where CustomerID='{0}' and MobileBussinessDefinedID='{1}'
            )", CurrentUserInfo.ClientID, pDefindID);
            return this.SQLHelper.ExecuteDataset(strSql.ToString());
        }
        #endregion
    }
}
