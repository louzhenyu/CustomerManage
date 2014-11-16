/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/7 8:38:47
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
using System.Text;
using JIT.Utility;
using JIT.Utility.Entity;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 实体：  
    /// </summary>
    public partial class LEventsEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public LEventsEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String EventID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Title { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? EventLevel { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ParentEventID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String BeginTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String EndTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String WeiXinID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Address { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CityID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Description { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ImageURL { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String URL { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Content { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PhoneNumber { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Email { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ApplyQuesID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PollQuesID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsSubEvent { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Longitude { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Latitude { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? EventStatus { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? DisplayIndex { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? PersonCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LastUpdateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ModelId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String EventManagerUserId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDefault { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsTop { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Organizer { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String EventFlag { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String EventTypeID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Intro { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? EventGenreId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? CanSignUpCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsTicketRequired { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ReplyType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Text { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Distance { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsShare { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ShareRemark { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PosterImageUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OverRemark { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String BootURL { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? MailSendInterval { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ShareLogoUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsPointsLottery { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? PointsLottery { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? RewardPoints { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? BeginPersonCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? EventFee { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsSignUpList { get; set; }
        #endregion

    }
}