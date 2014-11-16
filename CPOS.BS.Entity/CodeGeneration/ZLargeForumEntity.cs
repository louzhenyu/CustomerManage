/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/10/16 11:05:55
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
    public partial class ZLargeForumEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public ZLargeForumEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String ForumId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Title { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ForumTypeId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Desc { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Organizer { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Schedule { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Speakers { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Food { get; set; }

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
		public String Email { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String EmailTitle { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Sponsor { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Roundtable { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PreviousForum { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ContactUs { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String City { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDesc { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsOrganizer { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsSchedule { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsSpeakers { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsRoundtable { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsSponsor { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsFood { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsPreviousForum { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsContactUs { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsSignUp { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Register { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsRegister { get; set; }


        #endregion

    }
}