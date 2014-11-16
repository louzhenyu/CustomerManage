/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/19 10:39:35
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
    public partial class PgUserEntity : BaseEntity ,IExtensionable
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public PgUserEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String USER_ID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String MARKET { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LASTNAME { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String FIRSTNAME { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String KNOWNAS { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String GENDER { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? BIRTHDATE { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String EMAIL { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String MANAGEREMAIL { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String FUNC { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? JOBBAND { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? JOBBANDDATE { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? SERVICEYEAR { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? SUBORDINATECOUNT { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LOCATION { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ONBLACKLIST { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? BLACKDATE { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String BLACKREASON { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ADMINUPDATEDMOBILE { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? USERPOINT { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String MOBILE { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ChannelID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? GroupID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? HeadphotoID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? OnlineCourseCommentCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? POINT { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Type { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String BRAND { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ChannelGroupID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? DCCPOINT { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

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
		public String City { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SpecialTitle { get; set; }


        #endregion
        
        #region 根据属性名[获取/设置]属性值,子类应该采用组合模式重写该方法
        /// <summary>
        /// 根据属性名获取属性值,子类应该采用组合模式重写该方法
        /// </summary>
        /// <param name="pPropertyName">属性名</param>
        /// <param name="pValue">值</param>
        /// <returns>是否获取成功</returns>
        public  bool GetValueByPropertyName(string pPropertyName, out object pValue)
        {
            pValue = null;
            //参数处理
            if (string.IsNullOrEmpty(pPropertyName))
                throw new ArgumentException("pPropertyName不能为空或null.");
            //获取数据
            bool result = false;
            switch (pPropertyName)
            {
				case "USERID":
					pValue = this.USER_ID;
					result = true;
					break;
				case "ID":
					pValue = this.ID;
					result = true;
					break;
				case "MARKET":
					pValue = this.MARKET;
					result = true;
					break;
				case "LASTNAME":
					pValue = this.LASTNAME;
					result = true;
					break;
				case "FIRSTNAME":
					pValue = this.FIRSTNAME;
					result = true;
					break;
				case "KNOWNAS":
					pValue = this.KNOWNAS;
					result = true;
					break;
				case "GENDER":
					pValue = this.GENDER;
					result = true;
					break;
				case "BIRTHDATE":
					pValue = this.BIRTHDATE;
					result = true;
					break;
				case "EMAIL":
					pValue = this.EMAIL;
					result = true;
					break;
				case "MANAGEREMAIL":
					pValue = this.MANAGEREMAIL;
					result = true;
					break;
				case "FUNC":
					pValue = this.FUNC;
					result = true;
					break;
				case "JOBBAND":
					pValue = this.JOBBAND;
					result = true;
					break;
				case "JOBBANDDATE":
					pValue = this.JOBBANDDATE;
					result = true;
					break;
				case "SERVICEYEAR":
					pValue = this.SERVICEYEAR;
					result = true;
					break;
				case "SUBORDINATECOUNT":
					pValue = this.SUBORDINATECOUNT;
					result = true;
					break;
				case "LOCATION":
					pValue = this.LOCATION;
					result = true;
					break;
				case "ONBLACKLIST":
					pValue = this.ONBLACKLIST;
					result = true;
					break;
				case "BLACKDATE":
					pValue = this.BLACKDATE;
					result = true;
					break;
				case "BLACKREASON":
					pValue = this.BLACKREASON;
					result = true;
					break;
				case "ADMINUPDATEDMOBILE":
					pValue = this.ADMINUPDATEDMOBILE;
					result = true;
					break;
				case "USERPOINT":
					pValue = this.USERPOINT;
					result = true;
					break;
				case "MOBILE":
					pValue = this.MOBILE;
					result = true;
					break;
				case "ChannelID":
					pValue = this.ChannelID;
					result = true;
					break;
				case "GroupID":
					pValue = this.GroupID;
					result = true;
					break;
				case "HeadphotoID":
					pValue = this.HeadphotoID;
					result = true;
					break;
				case "OnlineCourseCommentCount":
					pValue = this.OnlineCourseCommentCount;
					result = true;
					break;
				case "POINT":
					pValue = this.POINT;
					result = true;
					break;
				case "Type":
					pValue = this.Type;
					result = true;
					break;
				case "BRAND":
					pValue = this.BRAND;
					result = true;
					break;
				case "ChannelGroupID":
					pValue = this.ChannelGroupID;
					result = true;
					break;
				case "DCCPOINT":
					pValue = this.DCCPOINT;
					result = true;
					break;
				case "CreateBy":
					pValue = this.CreateBy;
					result = true;
					break;
				case "CreateTime":
					pValue = this.CreateTime;
					result = true;
					break;
				case "LastUpdateBy":
					pValue = this.LastUpdateBy;
					result = true;
					break;
				case "LastUpdateTime":
					pValue = this.LastUpdateTime;
					result = true;
					break;
				case "IsDelete":
					pValue = this.IsDelete;
					result = true;
					break;
				case "City":
					pValue = this.City;
					result = true;
					break;
				case "SpecialTitle":
					pValue = this.SpecialTitle;
					result = true;
					break;

            }
            //返回
            return result;
        }
        /// <summary>
        /// 根据属性名设置值，子类应该采用组合模式重写该方法
        /// </summary>
        /// <param name="pPropertyName">属性名</param>
        /// <param name="pValue">值</param>
        /// <returns>是否设置成功</returns>
        public bool SetValueByPropertyName(string pPropertyName, object pValue)
        {
            //参数处理
            if (string.IsNullOrEmpty(pPropertyName))
                throw new ArgumentException("pPropertyName不能为空或null.");
            //设置数据
            bool result = false;
            switch (pPropertyName)
            {
				case "USERID":
					{
						if (pValue != null)
							this.USER_ID = Convert.ToString(pValue);
						else
							this.USER_ID = null;
						result = true;
					 }
					break;
				case "ID":
					{
						if (pValue != null)
							this.ID =  Convert.ToInt32(pValue);
						else
							this.ID = null;
						result = true;
					 }
					break;
				case "MARKET":
					{
						if (pValue != null)
							this.MARKET = Convert.ToString(pValue);
						else
							this.MARKET = null;
						result = true;
					 }
					break;
				case "LASTNAME":
					{
						if (pValue != null)
							this.LASTNAME = Convert.ToString(pValue);
						else
							this.LASTNAME = null;
						result = true;
					 }
					break;
				case "FIRSTNAME":
					{
						if (pValue != null)
							this.FIRSTNAME = Convert.ToString(pValue);
						else
							this.FIRSTNAME = null;
						result = true;
					 }
					break;
				case "KNOWNAS":
					{
						if (pValue != null)
							this.KNOWNAS = Convert.ToString(pValue);
						else
							this.KNOWNAS = null;
						result = true;
					 }
					break;
				case "GENDER":
					{
						if (pValue != null)
							this.GENDER = Convert.ToString(pValue);
						else
							this.GENDER = null;
						result = true;
					 }
					break;
				case "BIRTHDATE":
					{
						if (pValue != null)
							this.BIRTHDATE = Convert.ToDateTime(pValue);
						else
							this.BIRTHDATE = null;
						result = true;
					 }
					break;
				case "EMAIL":
					{
						if (pValue != null)
							this.EMAIL = Convert.ToString(pValue);
						else
							this.EMAIL = null;
						result = true;
					 }
					break;
				case "MANAGEREMAIL":
					{
						if (pValue != null)
							this.MANAGEREMAIL = Convert.ToString(pValue);
						else
							this.MANAGEREMAIL = null;
						result = true;
					 }
					break;
				case "FUNC":
					{
						if (pValue != null)
							this.FUNC = Convert.ToString(pValue);
						else
							this.FUNC = null;
						result = true;
					 }
					break;
				case "JOBBAND":
					{
						if (pValue != null)
							this.JOBBAND =  Convert.ToInt32(pValue);
						else
							this.JOBBAND = null;
						result = true;
					 }
					break;
				case "JOBBANDDATE":
					{
						if (pValue != null)
							this.JOBBANDDATE = Convert.ToDateTime(pValue);
						else
							this.JOBBANDDATE = null;
						result = true;
					 }
					break;
				case "SERVICEYEAR":
					{
						if (pValue != null)
							this.SERVICEYEAR =  Convert.ToInt32(pValue);
						else
							this.SERVICEYEAR = null;
						result = true;
					 }
					break;
				case "SUBORDINATECOUNT":
					{
						if (pValue != null)
							this.SUBORDINATECOUNT =  Convert.ToInt32(pValue);
						else
							this.SUBORDINATECOUNT = null;
						result = true;
					 }
					break;
				case "LOCATION":
					{
						if (pValue != null)
							this.LOCATION = Convert.ToString(pValue);
						else
							this.LOCATION = null;
						result = true;
					 }
					break;
				case "ONBLACKLIST":
					{
						if (pValue != null)
							this.ONBLACKLIST =  Convert.ToInt32(pValue);
						else
							this.ONBLACKLIST = null;
						result = true;
					 }
					break;
				case "BLACKDATE":
					{
						if (pValue != null)
							this.BLACKDATE = Convert.ToDateTime(pValue);
						else
							this.BLACKDATE = null;
						result = true;
					 }
					break;
				case "BLACKREASON":
					{
						if (pValue != null)
							this.BLACKREASON = Convert.ToString(pValue);
						else
							this.BLACKREASON = null;
						result = true;
					 }
					break;
				case "ADMINUPDATEDMOBILE":
					{
						if (pValue != null)
							this.ADMINUPDATEDMOBILE = Convert.ToString(pValue);
						else
							this.ADMINUPDATEDMOBILE = null;
						result = true;
					 }
					break;
				case "USERPOINT":
					{
						if (pValue != null)
							this.USERPOINT =  Convert.ToInt32(pValue);
						else
							this.USERPOINT = null;
						result = true;
					 }
					break;
				case "MOBILE":
					{
						if (pValue != null)
							this.MOBILE = Convert.ToString(pValue);
						else
							this.MOBILE = null;
						result = true;
					 }
					break;
				case "ChannelID":
					{
						if (pValue != null)
							this.ChannelID =  Convert.ToInt32(pValue);
						else
							this.ChannelID = null;
						result = true;
					 }
					break;
				case "GroupID":
					{
						if (pValue != null)
							this.GroupID =  Convert.ToInt32(pValue);
						else
							this.GroupID = null;
						result = true;
					 }
					break;
				case "HeadphotoID":
					{
						if (pValue != null)
							this.HeadphotoID =  Convert.ToInt32(pValue);
						else
							this.HeadphotoID = null;
						result = true;
					 }
					break;
				case "OnlineCourseCommentCount":
					{
						if (pValue != null)
							this.OnlineCourseCommentCount =  Convert.ToInt32(pValue);
						else
							this.OnlineCourseCommentCount = null;
						result = true;
					 }
					break;
				case "POINT":
					{
						if (pValue != null)
							this.POINT =  Convert.ToInt32(pValue);
						else
							this.POINT = null;
						result = true;
					 }
					break;
				case "Type":
					{
						if (pValue != null)
							this.Type = Convert.ToString(pValue);
						else
							this.Type = null;
						result = true;
					 }
					break;
				case "BRAND":
					{
						if (pValue != null)
							this.BRAND = Convert.ToString(pValue);
						else
							this.BRAND = null;
						result = true;
					 }
					break;
				case "ChannelGroupID":
					{
						if (pValue != null)
							this.ChannelGroupID =  Convert.ToInt32(pValue);
						else
							this.ChannelGroupID = null;
						result = true;
					 }
					break;
				case "DCCPOINT":
					{
						if (pValue != null)
							this.DCCPOINT =  Convert.ToInt32(pValue);
						else
							this.DCCPOINT = null;
						result = true;
					 }
					break;
				case "CreateBy":
					{
						if (pValue != null)
							this.CreateBy = Convert.ToString(pValue);
						else
							this.CreateBy = null;
						result = true;
					 }
					break;
				case "CreateTime":
					{
						if (pValue != null)
							this.CreateTime = Convert.ToDateTime(pValue);
						else
							this.CreateTime = null;
						result = true;
					 }
					break;
				case "LastUpdateBy":
					{
						if (pValue != null)
							this.LastUpdateBy = Convert.ToString(pValue);
						else
							this.LastUpdateBy = null;
						result = true;
					 }
					break;
				case "LastUpdateTime":
					{
						if (pValue != null)
							this.LastUpdateTime = Convert.ToDateTime(pValue);
						else
							this.LastUpdateTime = null;
						result = true;
					 }
					break;
				case "IsDelete":
					{
						if (pValue != null)
							this.IsDelete =  Convert.ToInt32(pValue);
						else
							this.IsDelete = null;
						result = true;
					 }
					break;
				case "City":
					{
						if (pValue != null)
							this.City = Convert.ToString(pValue);
						else
							this.City = null;
						result = true;
					 }
					break;
				case "SpecialTitle":
					{
						if (pValue != null)
							this.SpecialTitle = Convert.ToString(pValue);
						else
							this.SpecialTitle = null;
						result = true;
					 }
					break;

            }
            //重置持久化状态
            if (result == true && this.PersistenceHandle != null)
                this.PersistenceHandle.Modify();
            //返回
            return result;
        }
       
        #endregion
    }
}