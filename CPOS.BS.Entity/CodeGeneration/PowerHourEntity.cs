/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/25 13:29:48
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
    public partial class PowerHourEntity : BaseEntity ,IExtensionable
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public PowerHourEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? PowerHourID { get; set; }

		/// <summary>
		/// 培训地址
		/// </summary>
		public String SiteAddress { get; set; }

		/// <summary>
		/// 培训讲师的UserID(外键)
		/// </summary>
		public String TrainerID { get; set; }

		/// <summary>
		/// 培训主题
		/// </summary>
		public String Topic { get; set; }

		/// <summary>
		/// 培训所在城市(外键)
		/// </summary>
		public String CityID { get; set; }

		/// <summary>
		/// 培训开始时间
		/// </summary>
		public DateTime? StartTime { get; set; }

		/// <summary>
		/// 培训结束时间
		/// </summary>
		public DateTime? EndTime { get; set; }

		/// <summary>
		/// 培训现场照片
		/// </summary>
		public String SitePictureUrl { get; set; }

		/// <summary>
		/// 培训所属财年
		/// </summary>
		public Int32? FinanceYear { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }

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
		public Int32? ApproveState { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Approver { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? ApproveTime { get; set; }


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
				case "PowerHourID":
					pValue = this.PowerHourID;
					result = true;
					break;
				case "SiteAddress":
					pValue = this.SiteAddress;
					result = true;
					break;
				case "TrainerID":
					pValue = this.TrainerID;
					result = true;
					break;
				case "Topic":
					pValue = this.Topic;
					result = true;
					break;
				case "CityID":
					pValue = this.CityID;
					result = true;
					break;
				case "StartTime":
					pValue = this.StartTime;
					result = true;
					break;
				case "EndTime":
					pValue = this.EndTime;
					result = true;
					break;
				case "SitePictureUrl":
					pValue = this.SitePictureUrl;
					result = true;
					break;
				case "FinanceYear":
					pValue = this.FinanceYear;
					result = true;
					break;
				case "CustomerID":
					pValue = this.CustomerID;
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
				case "ApproveState":
					pValue = this.ApproveState;
					result = true;
					break;
				case "Approver":
					pValue = this.Approver;
					result = true;
					break;
				case "ApproveTime":
					pValue = this.ApproveTime;
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
				case "PowerHourID":
					{
						if (pValue != null)
							this.PowerHourID = (Guid)pValue;
						else
							this.PowerHourID = null;
						result = true;
					 }
					break;
				case "SiteAddress":
					{
						if (pValue != null)
							this.SiteAddress = Convert.ToString(pValue);
						else
							this.SiteAddress = null;
						result = true;
					 }
					break;
				case "TrainerID":
					{
						if (pValue != null)
							this.TrainerID = Convert.ToString(pValue);
						else
							this.TrainerID = null;
						result = true;
					 }
					break;
				case "Topic":
					{
						if (pValue != null)
							this.Topic = Convert.ToString(pValue);
						else
							this.Topic = null;
						result = true;
					 }
					break;
				case "CityID":
					{
						if (pValue != null)
							this.CityID = Convert.ToString(pValue);
						else
							this.CityID = null;
						result = true;
					 }
					break;
				case "StartTime":
					{
						if (pValue != null)
							this.StartTime = Convert.ToDateTime(pValue);
						else
							this.StartTime = null;
						result = true;
					 }
					break;
				case "EndTime":
					{
						if (pValue != null)
							this.EndTime = Convert.ToDateTime(pValue);
						else
							this.EndTime = null;
						result = true;
					 }
					break;
				case "SitePictureUrl":
					{
						if (pValue != null)
							this.SitePictureUrl = Convert.ToString(pValue);
						else
							this.SitePictureUrl = null;
						result = true;
					 }
					break;
				case "FinanceYear":
					{
						if (pValue != null)
							this.FinanceYear =  Convert.ToInt32(pValue);
						else
							this.FinanceYear = null;
						result = true;
					 }
					break;
				case "CustomerID":
					{
						if (pValue != null)
							this.CustomerID = Convert.ToString(pValue);
						else
							this.CustomerID = null;
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
				case "ApproveState":
					{
						if (pValue != null)
							this.ApproveState =  Convert.ToInt32(pValue);
						else
							this.ApproveState = null;
						result = true;
					 }
					break;
				case "Approver":
					{
						if (pValue != null)
							this.Approver = Convert.ToString(pValue);
						else
							this.Approver = null;
						result = true;
					 }
					break;
				case "ApproveTime":
					{
						if (pValue != null)
							this.ApproveTime = Convert.ToDateTime(pValue);
						else
							this.ApproveTime = null;
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