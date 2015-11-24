/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/28 11:13:26
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
    /// 表LEvents的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class LEventsDAO : Base.BaseCPOSDAO, ICRUDable<LEventsEntity>, IQueryable<LEventsEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public LEventsDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(LEventsEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(LEventsEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //初始化固定字段
            pEntity.CreateTime = DateTime.Now;
            pEntity.CreateBy = CurrentUserInfo.UserID;
            pEntity.LastUpdateTime = pEntity.CreateTime;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;
            pEntity.IsDelete = 0;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [LEvents](");
            strSql.Append("[Title],[EventLevel],[ParentEventID],[BeginTime],[EndTime],[WeiXinID],[Address],[CityID],[Description],[ImageURL],[URL],[Content],[PhoneNumber],[Email],[ApplyQuesID],[PollQuesID],[IsSubEvent],[Longitude],[Latitude],[EventStatus],[DisplayIndex],[PersonCount],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[CustomerId],[ModelId],[EventManagerUserId],[IsDefault],[IsTop],[Organizer],[EventFlag],[EventTypeID],[Intro],[EventGenreId],[CanSignUpCount],[IsTicketRequired],[ReplyType],[Text],[Distance],[IsShare],[ShareRemark],[PosterImageUrl],[OverRemark],[BootURL],[MailSendInterval],[ShareLogoUrl],[IsPointsLottery],[PointsLottery],[RewardPoints],[BeginPersonCount],[EventFee],[IsSignUpList],[EventID],[DrawMethodId],[VipCardType],[VipCardGrade])");
            strSql.Append(" values (");
            strSql.Append("@Title,@EventLevel,@ParentEventID,@BeginTime,@EndTime,@WeiXinID,@Address,@CityID,@Description,@ImageURL,@URL,@Content,@PhoneNumber,@Email,@ApplyQuesID,@PollQuesID,@IsSubEvent,@Longitude,@Latitude,@EventStatus,@DisplayIndex,@PersonCount,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@CustomerId,@ModelId,@EventManagerUserId,@IsDefault,@IsTop,@Organizer,@EventFlag,@EventTypeID,@Intro,@EventGenreId,@CanSignUpCount,@IsTicketRequired,@ReplyType,@Text,@Distance,@IsShare,@ShareRemark,@PosterImageUrl,@OverRemark,@BootURL,@MailSendInterval,@ShareLogoUrl,@IsPointsLottery,@PointsLottery,@RewardPoints,@BeginPersonCount,@EventFee,@IsSignUpList,@EventID,@DrawMethodId,@VipCardType,@VipCardGrade)");            

			string pkString = pEntity.EventID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@Title",SqlDbType.NVarChar),
					new SqlParameter("@EventLevel",SqlDbType.Int),
					new SqlParameter("@ParentEventID",SqlDbType.NVarChar),
					new SqlParameter("@BeginTime",SqlDbType.NVarChar),
					new SqlParameter("@EndTime",SqlDbType.NVarChar),
					new SqlParameter("@WeiXinID",SqlDbType.NVarChar),
					new SqlParameter("@Address",SqlDbType.NVarChar),
					new SqlParameter("@CityID",SqlDbType.NVarChar),
					new SqlParameter("@Description",SqlDbType.NVarChar),
					new SqlParameter("@ImageURL",SqlDbType.NVarChar),
					new SqlParameter("@URL",SqlDbType.NVarChar),
					new SqlParameter("@Content",SqlDbType.NVarChar),
					new SqlParameter("@PhoneNumber",SqlDbType.NVarChar),
					new SqlParameter("@Email",SqlDbType.NVarChar),
					new SqlParameter("@ApplyQuesID",SqlDbType.NVarChar),
					new SqlParameter("@PollQuesID",SqlDbType.NVarChar),
					new SqlParameter("@IsSubEvent",SqlDbType.Int),
					new SqlParameter("@Longitude",SqlDbType.NVarChar),
					new SqlParameter("@Latitude",SqlDbType.NVarChar),
					new SqlParameter("@EventStatus",SqlDbType.Int),
					new SqlParameter("@DisplayIndex",SqlDbType.Int),
					new SqlParameter("@PersonCount",SqlDbType.Int),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@ModelId",SqlDbType.NVarChar),
					new SqlParameter("@EventManagerUserId",SqlDbType.NVarChar),
					new SqlParameter("@IsDefault",SqlDbType.Int),
					new SqlParameter("@IsTop",SqlDbType.Int),
					new SqlParameter("@Organizer",SqlDbType.NVarChar),
					new SqlParameter("@EventFlag",SqlDbType.NVarChar),
					new SqlParameter("@EventTypeID",SqlDbType.NVarChar),
					new SqlParameter("@Intro",SqlDbType.NVarChar),
					new SqlParameter("@EventGenreId",SqlDbType.Int),
					new SqlParameter("@CanSignUpCount",SqlDbType.Int),
					new SqlParameter("@IsTicketRequired",SqlDbType.Int),
					new SqlParameter("@ReplyType",SqlDbType.Int),
					new SqlParameter("@Text",SqlDbType.NVarChar),
					new SqlParameter("@Distance",SqlDbType.Int),
					new SqlParameter("@IsShare",SqlDbType.Int),
					new SqlParameter("@ShareRemark",SqlDbType.NVarChar),
					new SqlParameter("@PosterImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@OverRemark",SqlDbType.NVarChar),
					new SqlParameter("@BootURL",SqlDbType.NVarChar),
					new SqlParameter("@MailSendInterval",SqlDbType.Int),
					new SqlParameter("@ShareLogoUrl",SqlDbType.NVarChar),
					new SqlParameter("@IsPointsLottery",SqlDbType.Int),
					new SqlParameter("@PointsLottery",SqlDbType.Int),
					new SqlParameter("@RewardPoints",SqlDbType.Int),
					new SqlParameter("@BeginPersonCount",SqlDbType.Int),
					new SqlParameter("@EventFee",SqlDbType.Int),
					new SqlParameter("@IsSignUpList",SqlDbType.Int),
					new SqlParameter("@EventID",SqlDbType.NVarChar),
					new SqlParameter("@DrawMethodId",SqlDbType.Int),
					new SqlParameter("@VipCardType",SqlDbType.NVarChar),
					new SqlParameter("@VipCardGrade",SqlDbType.NVarChar)

            };
			parameters[0].Value = pEntity.Title;
			parameters[1].Value = pEntity.EventLevel;
			parameters[2].Value = pEntity.ParentEventID;
			parameters[3].Value = pEntity.BeginTime;
			parameters[4].Value = pEntity.EndTime;
			parameters[5].Value = pEntity.WeiXinID;
			parameters[6].Value = pEntity.Address;
			parameters[7].Value = pEntity.CityID;
			parameters[8].Value = pEntity.Description;
			parameters[9].Value = pEntity.ImageURL;
			parameters[10].Value = pEntity.URL;
			parameters[11].Value = pEntity.Content;
			parameters[12].Value = pEntity.PhoneNumber;
			parameters[13].Value = pEntity.Email;
			parameters[14].Value = pEntity.ApplyQuesID;
			parameters[15].Value = pEntity.PollQuesID;
			parameters[16].Value = pEntity.IsSubEvent;
			parameters[17].Value = pEntity.Longitude;
			parameters[18].Value = pEntity.Latitude;
			parameters[19].Value = pEntity.EventStatus;
			parameters[20].Value = pEntity.DisplayIndex;
			parameters[21].Value = pEntity.PersonCount;
			parameters[22].Value = pEntity.CreateTime;
			parameters[23].Value = pEntity.CreateBy;
			parameters[24].Value = pEntity.LastUpdateBy;
			parameters[25].Value = pEntity.LastUpdateTime;
			parameters[26].Value = pEntity.IsDelete;
			parameters[27].Value = pEntity.CustomerId;
			parameters[28].Value = pEntity.ModelId;
			parameters[29].Value = pEntity.EventManagerUserId;
			parameters[30].Value = pEntity.IsDefault;
			parameters[31].Value = pEntity.IsTop;
			parameters[32].Value = pEntity.Organizer;
			parameters[33].Value = pEntity.EventFlag;
			parameters[34].Value = pEntity.EventTypeID;
			parameters[35].Value = pEntity.Intro;
			parameters[36].Value = pEntity.EventGenreId;
			parameters[37].Value = pEntity.CanSignUpCount;
			parameters[38].Value = pEntity.IsTicketRequired;
			parameters[39].Value = pEntity.ReplyType;
			parameters[40].Value = pEntity.Text;
			parameters[41].Value = pEntity.Distance;
			parameters[42].Value = pEntity.IsShare;
			parameters[43].Value = pEntity.ShareRemark;
			parameters[44].Value = pEntity.PosterImageUrl;
			parameters[45].Value = pEntity.OverRemark;
			parameters[46].Value = pEntity.BootURL;
			parameters[47].Value = pEntity.MailSendInterval;
			parameters[48].Value = pEntity.ShareLogoUrl;
			parameters[49].Value = pEntity.IsPointsLottery;
			parameters[50].Value = pEntity.PointsLottery;
			parameters[51].Value = pEntity.RewardPoints;
			parameters[52].Value = pEntity.BeginPersonCount;
			parameters[53].Value = pEntity.EventFee;
			parameters[54].Value = pEntity.IsSignUpList;
            parameters[55].Value = pkString;
            parameters[56].Value = pEntity.DrawMethodId;
            parameters[57].Value = pEntity.VipCardType;
            parameters[58].Value = pEntity.VipCardGrade; 

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.EventID = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public LEventsEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [LEvents] where EventID='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            LEventsEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                    break;
                }
            }
            //返回
            return m;
        }

        /// <summary>
        /// 获取所有实例
        /// </summary>
        /// <returns></returns>
        public LEventsEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [LEvents] where isdelete=0");
            //读取数据
            List<LEventsEntity> list = new List<LEventsEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    LEventsEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //返回
            return list.ToArray();
        }
   

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(LEventsEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(LEventsEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.EventID==null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [LEvents] set ");
            if (pIsUpdateNullField || pEntity.Title!=null)
                strSql.Append( "[Title]=@Title,");
            if (pIsUpdateNullField || pEntity.EventLevel!=null)
                strSql.Append( "[EventLevel]=@EventLevel,");
            if (pIsUpdateNullField || pEntity.ParentEventID!=null)
                strSql.Append( "[ParentEventID]=@ParentEventID,");
            if (pIsUpdateNullField || pEntity.BeginTime!=null)
                strSql.Append( "[BeginTime]=@BeginTime,");
            if (pIsUpdateNullField || pEntity.EndTime!=null)
                strSql.Append( "[EndTime]=@EndTime,");
            if (pIsUpdateNullField || pEntity.WeiXinID!=null)
                strSql.Append( "[WeiXinID]=@WeiXinID,");
            if (pIsUpdateNullField || pEntity.Address!=null)
                strSql.Append( "[Address]=@Address,");
            if (pIsUpdateNullField || pEntity.CityID!=null)
                strSql.Append( "[CityID]=@CityID,");
            if (pIsUpdateNullField || pEntity.Description!=null)
                strSql.Append( "[Description]=@Description,");
            if (pIsUpdateNullField || pEntity.ImageURL!=null)
                strSql.Append( "[ImageURL]=@ImageURL,");
            if (pIsUpdateNullField || pEntity.URL!=null)
                strSql.Append( "[URL]=@URL,");
            if (pIsUpdateNullField || pEntity.Content!=null)
                strSql.Append( "[Content]=@Content,");
            if (pIsUpdateNullField || pEntity.PhoneNumber!=null)
                strSql.Append( "[PhoneNumber]=@PhoneNumber,");
            if (pIsUpdateNullField || pEntity.Email!=null)
                strSql.Append( "[Email]=@Email,");
            if (pIsUpdateNullField || pEntity.ApplyQuesID!=null)
                strSql.Append( "[ApplyQuesID]=@ApplyQuesID,");
            if (pIsUpdateNullField || pEntity.PollQuesID!=null)
                strSql.Append( "[PollQuesID]=@PollQuesID,");
            if (pIsUpdateNullField || pEntity.IsSubEvent!=null)
                strSql.Append( "[IsSubEvent]=@IsSubEvent,");
            if (pIsUpdateNullField || pEntity.Longitude!=null)
                strSql.Append( "[Longitude]=@Longitude,");
            if (pIsUpdateNullField || pEntity.Latitude!=null)
                strSql.Append( "[Latitude]=@Latitude,");
            if (pIsUpdateNullField || pEntity.EventStatus!=null)
                strSql.Append( "[EventStatus]=@EventStatus,");
            if (pIsUpdateNullField || pEntity.DisplayIndex!=null)
                strSql.Append( "[DisplayIndex]=@DisplayIndex,");
            if (pIsUpdateNullField || pEntity.PersonCount!=null)
                strSql.Append( "[PersonCount]=@PersonCount,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.ModelId!=null)
                strSql.Append( "[ModelId]=@ModelId,");
            if (pIsUpdateNullField || pEntity.EventManagerUserId!=null)
                strSql.Append( "[EventManagerUserId]=@EventManagerUserId,");
            if (pIsUpdateNullField || pEntity.IsDefault!=null)
                strSql.Append( "[IsDefault]=@IsDefault,");
            if (pIsUpdateNullField || pEntity.IsTop!=null)
                strSql.Append( "[IsTop]=@IsTop,");
            if (pIsUpdateNullField || pEntity.Organizer!=null)
                strSql.Append( "[Organizer]=@Organizer,");
            if (pIsUpdateNullField || pEntity.EventFlag!=null)
                strSql.Append( "[EventFlag]=@EventFlag,");
            if (pIsUpdateNullField || pEntity.EventTypeID!=null)
                strSql.Append( "[EventTypeID]=@EventTypeID,");
            if (pIsUpdateNullField || pEntity.Intro!=null)
                strSql.Append( "[Intro]=@Intro,");
            if (pIsUpdateNullField || pEntity.EventGenreId!=null)
                strSql.Append( "[EventGenreId]=@EventGenreId,");
            if (pIsUpdateNullField || pEntity.CanSignUpCount!=null)
                strSql.Append( "[CanSignUpCount]=@CanSignUpCount,");
            if (pIsUpdateNullField || pEntity.IsTicketRequired!=null)
                strSql.Append( "[IsTicketRequired]=@IsTicketRequired,");
            if (pIsUpdateNullField || pEntity.ReplyType!=null)
                strSql.Append( "[ReplyType]=@ReplyType,");
            if (pIsUpdateNullField || pEntity.Text!=null)
                strSql.Append( "[Text]=@Text,");
            if (pIsUpdateNullField || pEntity.Distance!=null)
                strSql.Append( "[Distance]=@Distance,");
            if (pIsUpdateNullField || pEntity.IsShare!=null)
                strSql.Append( "[IsShare]=@IsShare,");
            if (pIsUpdateNullField || pEntity.ShareRemark!=null)
                strSql.Append( "[ShareRemark]=@ShareRemark,");
            if (pIsUpdateNullField || pEntity.PosterImageUrl!=null)
                strSql.Append( "[PosterImageUrl]=@PosterImageUrl,");
            if (pIsUpdateNullField || pEntity.OverRemark!=null)
                strSql.Append( "[OverRemark]=@OverRemark,");
            if (pIsUpdateNullField || pEntity.BootURL!=null)
                strSql.Append( "[BootURL]=@BootURL,");
            if (pIsUpdateNullField || pEntity.MailSendInterval!=null)
                strSql.Append( "[MailSendInterval]=@MailSendInterval,");
            if (pIsUpdateNullField || pEntity.ShareLogoUrl!=null)
                strSql.Append( "[ShareLogoUrl]=@ShareLogoUrl,");
            if (pIsUpdateNullField || pEntity.IsPointsLottery!=null)
                strSql.Append( "[IsPointsLottery]=@IsPointsLottery,");
            if (pIsUpdateNullField || pEntity.PointsLottery!=null)
                strSql.Append( "[PointsLottery]=@PointsLottery,");
            if (pIsUpdateNullField || pEntity.RewardPoints!=null)
                strSql.Append( "[RewardPoints]=@RewardPoints,");
            if (pIsUpdateNullField || pEntity.BeginPersonCount!=null)
                strSql.Append( "[BeginPersonCount]=@BeginPersonCount,");
            if (pIsUpdateNullField || pEntity.EventFee!=null)
                strSql.Append( "[EventFee]=@EventFee,");
            if (pIsUpdateNullField || pEntity.IsSignUpList!=null)
                strSql.Append( "[IsSignUpList]=@IsSignUpList,");

            if (pIsUpdateNullField || pEntity.DrawMethodId != null)
                strSql.Append("[DrawMethodId]=@DrawMethodId,");

            if (pIsUpdateNullField || pEntity.VipCardType != null)
                strSql.Append("[VipCardType]=@VipCardType,");

            if (pIsUpdateNullField || pEntity.VipCardGrade != null)
                strSql.Append("[VipCardGrade]=@VipCardGrade");

            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where EventID=@EventID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@Title",SqlDbType.NVarChar),
					new SqlParameter("@EventLevel",SqlDbType.Int),
					new SqlParameter("@ParentEventID",SqlDbType.NVarChar),
					new SqlParameter("@BeginTime",SqlDbType.NVarChar),
					new SqlParameter("@EndTime",SqlDbType.NVarChar),
					new SqlParameter("@WeiXinID",SqlDbType.NVarChar),
					new SqlParameter("@Address",SqlDbType.NVarChar),
					new SqlParameter("@CityID",SqlDbType.NVarChar),
					new SqlParameter("@Description",SqlDbType.NVarChar),
					new SqlParameter("@ImageURL",SqlDbType.NVarChar),
					new SqlParameter("@URL",SqlDbType.NVarChar),
					new SqlParameter("@Content",SqlDbType.NVarChar),
					new SqlParameter("@PhoneNumber",SqlDbType.NVarChar),
					new SqlParameter("@Email",SqlDbType.NVarChar),
					new SqlParameter("@ApplyQuesID",SqlDbType.NVarChar),
					new SqlParameter("@PollQuesID",SqlDbType.NVarChar),
					new SqlParameter("@IsSubEvent",SqlDbType.Int),
					new SqlParameter("@Longitude",SqlDbType.NVarChar),
					new SqlParameter("@Latitude",SqlDbType.NVarChar),
					new SqlParameter("@EventStatus",SqlDbType.Int),
					new SqlParameter("@DisplayIndex",SqlDbType.Int),
					new SqlParameter("@PersonCount",SqlDbType.Int),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@ModelId",SqlDbType.NVarChar),
					new SqlParameter("@EventManagerUserId",SqlDbType.NVarChar),
					new SqlParameter("@IsDefault",SqlDbType.Int),
					new SqlParameter("@IsTop",SqlDbType.Int),
					new SqlParameter("@Organizer",SqlDbType.NVarChar),
					new SqlParameter("@EventFlag",SqlDbType.NVarChar),
					new SqlParameter("@EventTypeID",SqlDbType.NVarChar),
					new SqlParameter("@Intro",SqlDbType.NVarChar),
					new SqlParameter("@EventGenreId",SqlDbType.Int),
					new SqlParameter("@CanSignUpCount",SqlDbType.Int),
					new SqlParameter("@IsTicketRequired",SqlDbType.Int),
					new SqlParameter("@ReplyType",SqlDbType.Int),
					new SqlParameter("@Text",SqlDbType.NVarChar),
					new SqlParameter("@Distance",SqlDbType.Int),
					new SqlParameter("@IsShare",SqlDbType.Int),
					new SqlParameter("@ShareRemark",SqlDbType.NVarChar),
					new SqlParameter("@PosterImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@OverRemark",SqlDbType.NVarChar),
					new SqlParameter("@BootURL",SqlDbType.NVarChar),
					new SqlParameter("@MailSendInterval",SqlDbType.Int),
					new SqlParameter("@ShareLogoUrl",SqlDbType.NVarChar),
					new SqlParameter("@IsPointsLottery",SqlDbType.Int),
					new SqlParameter("@PointsLottery",SqlDbType.Int),
					new SqlParameter("@RewardPoints",SqlDbType.Int),
					new SqlParameter("@BeginPersonCount",SqlDbType.Int),
					new SqlParameter("@EventFee",SqlDbType.Int),
					new SqlParameter("@IsSignUpList",SqlDbType.Int),
					new SqlParameter("@DrawMethodId",SqlDbType.Int),
					new SqlParameter("@VipCardType",SqlDbType.Int),
					new SqlParameter("@VipCardGrade",SqlDbType.Int),
					new SqlParameter("@EventID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.Title;
			parameters[1].Value = pEntity.EventLevel;
			parameters[2].Value = pEntity.ParentEventID;
			parameters[3].Value = pEntity.BeginTime;
			parameters[4].Value = pEntity.EndTime;
			parameters[5].Value = pEntity.WeiXinID;
			parameters[6].Value = pEntity.Address;
			parameters[7].Value = pEntity.CityID;
			parameters[8].Value = pEntity.Description;
			parameters[9].Value = pEntity.ImageURL;
			parameters[10].Value = pEntity.URL;
			parameters[11].Value = pEntity.Content;
			parameters[12].Value = pEntity.PhoneNumber;
			parameters[13].Value = pEntity.Email;
			parameters[14].Value = pEntity.ApplyQuesID;
			parameters[15].Value = pEntity.PollQuesID;
			parameters[16].Value = pEntity.IsSubEvent;
			parameters[17].Value = pEntity.Longitude;
			parameters[18].Value = pEntity.Latitude;
			parameters[19].Value = pEntity.EventStatus;
			parameters[20].Value = pEntity.DisplayIndex;
			parameters[21].Value = pEntity.PersonCount;
			parameters[22].Value = pEntity.LastUpdateBy;
			parameters[23].Value = pEntity.LastUpdateTime;
			parameters[24].Value = pEntity.CustomerId;
			parameters[25].Value = pEntity.ModelId;
			parameters[26].Value = pEntity.EventManagerUserId;
			parameters[27].Value = pEntity.IsDefault;
			parameters[28].Value = pEntity.IsTop;
			parameters[29].Value = pEntity.Organizer;
			parameters[30].Value = pEntity.EventFlag;
			parameters[31].Value = pEntity.EventTypeID;
			parameters[32].Value = pEntity.Intro;
			parameters[33].Value = pEntity.EventGenreId;
			parameters[34].Value = pEntity.CanSignUpCount;
			parameters[35].Value = pEntity.IsTicketRequired;
			parameters[36].Value = pEntity.ReplyType;
			parameters[37].Value = pEntity.Text;
			parameters[38].Value = pEntity.Distance;
			parameters[39].Value = pEntity.IsShare;
			parameters[40].Value = pEntity.ShareRemark;
			parameters[41].Value = pEntity.PosterImageUrl;
			parameters[42].Value = pEntity.OverRemark;
			parameters[43].Value = pEntity.BootURL;
			parameters[44].Value = pEntity.MailSendInterval;
			parameters[45].Value = pEntity.ShareLogoUrl;
			parameters[46].Value = pEntity.IsPointsLottery;
			parameters[47].Value = pEntity.PointsLottery;
			parameters[48].Value = pEntity.RewardPoints;
			parameters[49].Value = pEntity.BeginPersonCount;
			parameters[50].Value = pEntity.EventFee;
            parameters[51].Value = pEntity.IsSignUpList;
            parameters[52].Value = pEntity.DrawMethodId;
            parameters[53].Value = pEntity.VipCardType;
            parameters[54].Value = pEntity.VipCardGrade;
			parameters[55].Value = pEntity.EventID;

            //执行语句
            int result = 0;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Update(LEventsEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(LEventsEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(LEventsEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(LEventsEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.EventID==null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.EventID, pTran);           
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pID">标识符的值</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(object pID, IDbTransaction pTran)
        {
            if (pID == null)
                return ;   
            //组织参数化SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [LEvents] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where EventID=@EventID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@EventID",SqlDbType=SqlDbType.VarChar,Value=pID}
            };
            //执行语句
            int result = 0;
            if (pTran != null)
                result=this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), parameters);
            else
                result=this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), parameters);
            return ;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(LEventsEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.EventID==null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.EventID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(LEventsEntity[] pEntities)
        { 
            Delete(pEntities, null);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pIDs">标识符值数组</param>
        public void Delete(object[] pIDs)
        {
            Delete(pIDs,null);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pIDs">标识符值数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(object[] pIDs, IDbTransaction pTran) 
        {
            if (pIDs == null || pIDs.Length==0)
                return ;
            //组织参数化SQL
            StringBuilder primaryKeys = new StringBuilder();
            foreach (object item in pIDs)
            {
                primaryKeys.AppendFormat("'{0}',",item.ToString());
            }
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [LEvents] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where EventID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
            //执行语句
            int result = 0;   
            if (pTran == null)
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), null);
            else
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran,CommandType.Text, sql.ToString());       
        }
        #endregion

        #region IQueryable 成员
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <returns></returns>
        public LEventsEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [LEvents] where isdelete=0 ");
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    sql.AppendFormat(" and {0}", item.GetExpression());
                }
            }
            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                sql.AppendFormat(" order by ");
                foreach (var item in pOrderBys)
                {
                    sql.AppendFormat(" {0} {1},", item.FieldName, item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                }
                sql.Remove(sql.Length - 1, 1);
            }
            //执行SQL
            List<LEventsEntity> list = new List<LEventsEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    LEventsEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //返回结果
            return list.ToArray();
        }
        /// <summary>
        /// 执行分页查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <param name="pPageSize">每页的记录数</param>
        /// <param name="pCurrentPageIndex">以0开始的当前页码</param>
        /// <returns></returns>
        public PagedQueryResult<LEventsEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            //组织SQL
            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();
            //分页SQL
            pagedSql.AppendFormat("select * from (select row_number()over( order by ");
            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                foreach (var item in pOrderBys)
                {
                    if(item!=null)
                    {
                        pagedSql.AppendFormat(" {0} {1},", StringUtils.WrapperSQLServerObject(item.FieldName), item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                    }
                }
                pagedSql.Remove(pagedSql.Length - 1, 1);
            }
            else
            {
                pagedSql.AppendFormat(" [EventID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [LEvents] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [LEvents] where isdelete=0 ");
            //过滤条件
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    if(item!=null)
                    {
                        pagedSql.AppendFormat(" and {0}", item.GetExpression());
                        totalCountSql.AppendFormat(" and {0}", item.GetExpression());
                    }
                }
            }
            pagedSql.AppendFormat(") as A ");
            //取指定页的数据
            pagedSql.AppendFormat(" where ___rn >{0} and ___rn <={1}", pPageSize * (pCurrentPageIndex-1), pPageSize * (pCurrentPageIndex));
            //执行语句并返回结果
            PagedQueryResult<LEventsEntity> result = new PagedQueryResult<LEventsEntity>();
            List<LEventsEntity> list = new List<LEventsEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    LEventsEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            result.Entities = list.ToArray();
            int totalCount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(totalCountSql.ToString()));    //计算总行数
            result.RowCount = totalCount;
            int remainder = 0;
            result.PageCount = Math.DivRem(totalCount, pPageSize, out remainder);
            if (remainder > 0)
                result.PageCount++;
            return result;
        }

        /// <summary>
        /// 根据实体条件查询实体
        /// </summary>
        /// <param name="pQueryEntity">以实体形式传入的参数</param>
        /// <param name="pOrderBys">排序组合</param>
        /// <returns>符合条件的实体集</returns>
        public LEventsEntity[] QueryByEntity(LEventsEntity pQueryEntity, OrderBy[] pOrderBys)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity(pQueryEntity);
            return Query(queryWhereCondition,  pOrderBys);            
        }

        /// <summary>
        /// 分页根据实体条件查询实体
        /// </summary>
        /// <param name="pQueryEntity">以实体形式传入的参数</param>
        /// <param name="pOrderBys">排序组合</param>
        /// <returns>符合条件的实体集</returns>
        public PagedQueryResult<LEventsEntity> PagedQueryByEntity(LEventsEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity( pQueryEntity);
            return PagedQuery(queryWhereCondition, pOrderBys, pPageSize, pCurrentPageIndex);
        }

        #endregion

        #region 工具方法
        /// <summary>
        /// 根据实体非Null属性生成查询条件。
        /// </summary>
        /// <returns></returns>
        protected IWhereCondition[] GetWhereConditionByEntity(LEventsEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.EventID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventID", Value = pQueryEntity.EventID });
            if (pQueryEntity.Title!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Title", Value = pQueryEntity.Title });
            if (pQueryEntity.EventLevel!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventLevel", Value = pQueryEntity.EventLevel });
            if (pQueryEntity.ParentEventID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ParentEventID", Value = pQueryEntity.ParentEventID });
            if (pQueryEntity.BeginTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BeginTime", Value = pQueryEntity.BeginTime });
            if (pQueryEntity.EndTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EndTime", Value = pQueryEntity.EndTime });
            if (pQueryEntity.WeiXinID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WeiXinID", Value = pQueryEntity.WeiXinID });
            if (pQueryEntity.Address!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Address", Value = pQueryEntity.Address });
            if (pQueryEntity.CityID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CityID", Value = pQueryEntity.CityID });
            if (pQueryEntity.Description!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Description", Value = pQueryEntity.Description });
            if (pQueryEntity.ImageURL!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ImageURL", Value = pQueryEntity.ImageURL });
            if (pQueryEntity.URL!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "URL", Value = pQueryEntity.URL });
            if (pQueryEntity.Content!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Content", Value = pQueryEntity.Content });
            if (pQueryEntity.PhoneNumber!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PhoneNumber", Value = pQueryEntity.PhoneNumber });
            if (pQueryEntity.Email!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Email", Value = pQueryEntity.Email });
            if (pQueryEntity.ApplyQuesID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ApplyQuesID", Value = pQueryEntity.ApplyQuesID });
            if (pQueryEntity.PollQuesID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PollQuesID", Value = pQueryEntity.PollQuesID });
            if (pQueryEntity.IsSubEvent!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsSubEvent", Value = pQueryEntity.IsSubEvent });
            if (pQueryEntity.Longitude!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Longitude", Value = pQueryEntity.Longitude });
            if (pQueryEntity.Latitude!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Latitude", Value = pQueryEntity.Latitude });
            if (pQueryEntity.EventStatus!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventStatus", Value = pQueryEntity.EventStatus });
            if (pQueryEntity.DisplayIndex!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DisplayIndex", Value = pQueryEntity.DisplayIndex });
            if (pQueryEntity.PersonCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PersonCount", Value = pQueryEntity.PersonCount });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.LastUpdateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.LastUpdateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.IsDelete!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
            if (pQueryEntity.ModelId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ModelId", Value = pQueryEntity.ModelId });
            if (pQueryEntity.EventManagerUserId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventManagerUserId", Value = pQueryEntity.EventManagerUserId });
            if (pQueryEntity.IsDefault!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDefault", Value = pQueryEntity.IsDefault });
            if (pQueryEntity.IsTop!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsTop", Value = pQueryEntity.IsTop });
            if (pQueryEntity.Organizer!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Organizer", Value = pQueryEntity.Organizer });
            if (pQueryEntity.EventFlag!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventFlag", Value = pQueryEntity.EventFlag });
            if (pQueryEntity.EventTypeID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventTypeID", Value = pQueryEntity.EventTypeID });
            if (pQueryEntity.Intro!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Intro", Value = pQueryEntity.Intro });
            if (pQueryEntity.EventGenreId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventGenreId", Value = pQueryEntity.EventGenreId });
            if (pQueryEntity.CanSignUpCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CanSignUpCount", Value = pQueryEntity.CanSignUpCount });
            if (pQueryEntity.IsTicketRequired!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsTicketRequired", Value = pQueryEntity.IsTicketRequired });
            if (pQueryEntity.ReplyType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReplyType", Value = pQueryEntity.ReplyType });
            if (pQueryEntity.Text!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Text", Value = pQueryEntity.Text });
            if (pQueryEntity.Distance!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Distance", Value = pQueryEntity.Distance });
            if (pQueryEntity.IsShare!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsShare", Value = pQueryEntity.IsShare });
            if (pQueryEntity.ShareRemark!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ShareRemark", Value = pQueryEntity.ShareRemark });
            if (pQueryEntity.PosterImageUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PosterImageUrl", Value = pQueryEntity.PosterImageUrl });
            if (pQueryEntity.OverRemark!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OverRemark", Value = pQueryEntity.OverRemark });
            if (pQueryEntity.BootURL!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BootURL", Value = pQueryEntity.BootURL });
            if (pQueryEntity.MailSendInterval!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MailSendInterval", Value = pQueryEntity.MailSendInterval });
            if (pQueryEntity.ShareLogoUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ShareLogoUrl", Value = pQueryEntity.ShareLogoUrl });
            if (pQueryEntity.IsPointsLottery!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsPointsLottery", Value = pQueryEntity.IsPointsLottery });
            if (pQueryEntity.PointsLottery!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PointsLottery", Value = pQueryEntity.PointsLottery });
            if (pQueryEntity.RewardPoints!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RewardPoints", Value = pQueryEntity.RewardPoints });
            if (pQueryEntity.BeginPersonCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BeginPersonCount", Value = pQueryEntity.BeginPersonCount });
            if (pQueryEntity.EventFee!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventFee", Value = pQueryEntity.EventFee });
            if (pQueryEntity.IsSignUpList!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsSignUpList", Value = pQueryEntity.IsSignUpList });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out LEventsEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new LEventsEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["EventID"] != DBNull.Value)
			{
				pInstance.EventID =  Convert.ToString(pReader["EventID"]);
			}
			if (pReader["Title"] != DBNull.Value)
			{
				pInstance.Title =  Convert.ToString(pReader["Title"]);
			}
			if (pReader["EventLevel"] != DBNull.Value)
			{
				pInstance.EventLevel =   Convert.ToInt32(pReader["EventLevel"]);
			}
			if (pReader["ParentEventID"] != DBNull.Value)
			{
				pInstance.ParentEventID =  Convert.ToString(pReader["ParentEventID"]);
			}
            if (pReader["BeginTime"] != DBNull.Value)
            {
                pInstance.BeginTime = Convert.ToDateTime(pReader["BeginTime"]).ToString("yyyy-MM-dd");
            }
            if (pReader["EndTime"] != DBNull.Value)
            {
                pInstance.EndTime = Convert.ToDateTime(pReader["EndTime"]).ToString("yyyy-MM-dd");
            }
			if (pReader["WeiXinID"] != DBNull.Value)
			{
				pInstance.WeiXinID =  Convert.ToString(pReader["WeiXinID"]);
			}
			if (pReader["Address"] != DBNull.Value)
			{
				pInstance.Address =  Convert.ToString(pReader["Address"]);
			}
			if (pReader["CityID"] != DBNull.Value)
			{
				pInstance.CityID =  Convert.ToString(pReader["CityID"]);
			}
			if (pReader["Description"] != DBNull.Value)
			{
				pInstance.Description =  Convert.ToString(pReader["Description"]);
			}
			if (pReader["ImageURL"] != DBNull.Value)
			{
				pInstance.ImageURL =  Convert.ToString(pReader["ImageURL"]);
			}
			if (pReader["URL"] != DBNull.Value)
			{
				pInstance.URL =  Convert.ToString(pReader["URL"]);
			}
			if (pReader["Content"] != DBNull.Value)
			{
				pInstance.Content =  Convert.ToString(pReader["Content"]);
			}
			if (pReader["PhoneNumber"] != DBNull.Value)
			{
				pInstance.PhoneNumber =  Convert.ToString(pReader["PhoneNumber"]);
			}
			if (pReader["Email"] != DBNull.Value)
			{
				pInstance.Email =  Convert.ToString(pReader["Email"]);
			}
			if (pReader["ApplyQuesID"] != DBNull.Value)
			{
				pInstance.ApplyQuesID =  Convert.ToString(pReader["ApplyQuesID"]);
			}
			if (pReader["PollQuesID"] != DBNull.Value)
			{
				pInstance.PollQuesID =  Convert.ToString(pReader["PollQuesID"]);
			}
			if (pReader["IsSubEvent"] != DBNull.Value)
			{
				pInstance.IsSubEvent =   Convert.ToInt32(pReader["IsSubEvent"]);
			}
			if (pReader["Longitude"] != DBNull.Value)
			{
				pInstance.Longitude =  Convert.ToString(pReader["Longitude"]);
			}
			if (pReader["Latitude"] != DBNull.Value)
			{
				pInstance.Latitude =  Convert.ToString(pReader["Latitude"]);
			}
			if (pReader["EventStatus"] != DBNull.Value)
			{
				pInstance.EventStatus =   Convert.ToInt32(pReader["EventStatus"]);
			}
			if (pReader["DisplayIndex"] != DBNull.Value)
			{
				pInstance.DisplayIndex =   Convert.ToInt32(pReader["DisplayIndex"]);
			}
			if (pReader["PersonCount"] != DBNull.Value)
			{
				pInstance.PersonCount =   Convert.ToInt32(pReader["PersonCount"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}
			if (pReader["CreateBy"] != DBNull.Value)
			{
				pInstance.CreateBy =  Convert.ToString(pReader["CreateBy"]);
			}
			if (pReader["LastUpdateBy"] != DBNull.Value)
			{
				pInstance.LastUpdateBy =  Convert.ToString(pReader["LastUpdateBy"]);
			}
			if (pReader["LastUpdateTime"] != DBNull.Value)
			{
				pInstance.LastUpdateTime =  Convert.ToDateTime(pReader["LastUpdateTime"]);
			}
			if (pReader["IsDelete"] != DBNull.Value)
			{
				pInstance.IsDelete =   Convert.ToInt32(pReader["IsDelete"]);
			}
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}
			if (pReader["ModelId"] != DBNull.Value)
			{
				pInstance.ModelId =  Convert.ToString(pReader["ModelId"]);
			}
			if (pReader["EventManagerUserId"] != DBNull.Value)
			{
				pInstance.EventManagerUserId =  Convert.ToString(pReader["EventManagerUserId"]);
			}
			if (pReader["IsDefault"] != DBNull.Value)
			{
				pInstance.IsDefault =   Convert.ToInt32(pReader["IsDefault"]);
			}
			if (pReader["IsTop"] != DBNull.Value)
			{
				pInstance.IsTop =   Convert.ToInt32(pReader["IsTop"]);
			}
			if (pReader["Organizer"] != DBNull.Value)
			{
				pInstance.Organizer =  Convert.ToString(pReader["Organizer"]);
			}
			if (pReader["EventFlag"] != DBNull.Value)
			{
				pInstance.EventFlag =  Convert.ToString(pReader["EventFlag"]);
			}
			if (pReader["EventTypeID"] != DBNull.Value)
			{
				pInstance.EventTypeID =  Convert.ToString(pReader["EventTypeID"]);
			}
			if (pReader["Intro"] != DBNull.Value)
			{
				pInstance.Intro =  Convert.ToString(pReader["Intro"]);
			}
			if (pReader["EventGenreId"] != DBNull.Value)
			{
				pInstance.EventGenreId =   Convert.ToInt32(pReader["EventGenreId"]);
			}
			if (pReader["CanSignUpCount"] != DBNull.Value)
			{
				pInstance.CanSignUpCount =   Convert.ToInt32(pReader["CanSignUpCount"]);
			}
			if (pReader["IsTicketRequired"] != DBNull.Value)
			{
				pInstance.IsTicketRequired =   Convert.ToInt32(pReader["IsTicketRequired"]);
			}
			if (pReader["ReplyType"] != DBNull.Value)
			{
				pInstance.ReplyType =   Convert.ToInt32(pReader["ReplyType"]);
			}
			if (pReader["Text"] != DBNull.Value)
			{
				pInstance.Text =  Convert.ToString(pReader["Text"]);
			}
			if (pReader["Distance"] != DBNull.Value)
			{
				pInstance.Distance =   Convert.ToInt32(pReader["Distance"]);
			}
			if (pReader["IsShare"] != DBNull.Value)
			{
				pInstance.IsShare =   Convert.ToInt32(pReader["IsShare"]);
			}
			if (pReader["ShareRemark"] != DBNull.Value)
			{
				pInstance.ShareRemark =  Convert.ToString(pReader["ShareRemark"]);
			}
			if (pReader["PosterImageUrl"] != DBNull.Value)
			{
				pInstance.PosterImageUrl =  Convert.ToString(pReader["PosterImageUrl"]);
			}
			if (pReader["OverRemark"] != DBNull.Value)
			{
				pInstance.OverRemark =  Convert.ToString(pReader["OverRemark"]);
			}
			if (pReader["BootURL"] != DBNull.Value)
			{
				pInstance.BootURL =  Convert.ToString(pReader["BootURL"]);
			}
			if (pReader["MailSendInterval"] != DBNull.Value)
			{
				pInstance.MailSendInterval =   Convert.ToInt32(pReader["MailSendInterval"]);
			}
			if (pReader["ShareLogoUrl"] != DBNull.Value)
			{
				pInstance.ShareLogoUrl =  Convert.ToString(pReader["ShareLogoUrl"]);
			}
			if (pReader["IsPointsLottery"] != DBNull.Value)
			{
				pInstance.IsPointsLottery =   Convert.ToInt32(pReader["IsPointsLottery"]);
			}
			if (pReader["PointsLottery"] != DBNull.Value)
			{
				pInstance.PointsLottery =   Convert.ToInt32(pReader["PointsLottery"]);
			}
			if (pReader["RewardPoints"] != DBNull.Value)
			{
				pInstance.RewardPoints =   Convert.ToInt32(pReader["RewardPoints"]);
			}
			if (pReader["BeginPersonCount"] != DBNull.Value)
			{
				pInstance.BeginPersonCount =   Convert.ToInt32(pReader["BeginPersonCount"]);
			}
			if (pReader["EventFee"] != DBNull.Value)
			{
				pInstance.EventFee =   Convert.ToInt32(pReader["EventFee"]);
			}
			if (pReader["IsSignUpList"] != DBNull.Value)
			{
				pInstance.IsSignUpList =   Convert.ToInt32(pReader["IsSignUpList"]);
			}
            if (pReader["DrawMethodId"] != DBNull.Value)
            {
                pInstance.DrawMethodId = Convert.ToInt32(pReader["DrawMethodId"]);
            }
            if (pReader["VipCardType"] != DBNull.Value)
            {
                pInstance.VipCardType = Convert.ToInt32(pReader["VipCardType"]);
            }
            if (pReader["VipCardGrade"] != DBNull.Value)
            {
                pInstance.VipCardGrade = Convert.ToInt32(pReader["VipCardGrade"]);
            }

        }
        #endregion
    }
}
