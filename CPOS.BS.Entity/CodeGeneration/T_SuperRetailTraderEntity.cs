/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/24 14:53:08
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
    public partial class T_SuperRetailTraderEntity : BaseEntity ,IExtensionable
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_SuperRetailTraderEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? SuperRetailTraderID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SuperRetailTraderCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SuperRetailTraderName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SuperRetailTraderLogin { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SuperRetailTraderPass { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SuperRetailTraderMan { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SuperRetailTraderPhone { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SuperRetailTraderAddress { get; set; }

		/// <summary>
		/// 分销商来源   User  员工   Vip    会员
		/// </summary>
		public String SuperRetailTraderFrom { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SuperRetailTraderFromId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? HigheSuperRetailTraderID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? JoinTime { get; set; }

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
		public String Status { get; set; }


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
				case "SuperRetailTraderID":
					pValue = this.SuperRetailTraderID;
					result = true;
					break;
				case "SuperRetailTraderCode":
					pValue = this.SuperRetailTraderCode;
					result = true;
					break;
				case "SuperRetailTraderName":
					pValue = this.SuperRetailTraderName;
					result = true;
					break;
				case "SuperRetailTraderLogin":
					pValue = this.SuperRetailTraderLogin;
					result = true;
					break;
				case "SuperRetailTraderPass":
					pValue = this.SuperRetailTraderPass;
					result = true;
					break;
				case "SuperRetailTraderMan":
					pValue = this.SuperRetailTraderMan;
					result = true;
					break;
				case "SuperRetailTraderPhone":
					pValue = this.SuperRetailTraderPhone;
					result = true;
					break;
				case "SuperRetailTraderAddress":
					pValue = this.SuperRetailTraderAddress;
					result = true;
					break;
				case "SuperRetailTraderFrom":
					pValue = this.SuperRetailTraderFrom;
					result = true;
					break;
				case "SuperRetailTraderFromId":
					pValue = this.SuperRetailTraderFromId;
					result = true;
					break;
				case "HigheSuperRetailTraderID":
					pValue = this.HigheSuperRetailTraderID;
					result = true;
					break;
				case "JoinTime":
					pValue = this.JoinTime;
					result = true;
					break;
				case "CreateTime":
					pValue = this.CreateTime;
					result = true;
					break;
				case "CreateBy":
					pValue = this.CreateBy;
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
				case "CustomerId":
					pValue = this.CustomerId;
					result = true;
					break;
				case "Status":
					pValue = this.Status;
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
				case "SuperRetailTraderID":
					{
						if (pValue != null)
							this.SuperRetailTraderID = (Guid)pValue;
						else
							this.SuperRetailTraderID = null;
						result = true;
					 }
					break;
				case "SuperRetailTraderCode":
					{
						if (pValue != null)
							this.SuperRetailTraderCode = Convert.ToString(pValue);
						else
							this.SuperRetailTraderCode = null;
						result = true;
					 }
					break;
				case "SuperRetailTraderName":
					{
						if (pValue != null)
							this.SuperRetailTraderName = Convert.ToString(pValue);
						else
							this.SuperRetailTraderName = null;
						result = true;
					 }
					break;
				case "SuperRetailTraderLogin":
					{
						if (pValue != null)
							this.SuperRetailTraderLogin = Convert.ToString(pValue);
						else
							this.SuperRetailTraderLogin = null;
						result = true;
					 }
					break;
				case "SuperRetailTraderPass":
					{
						if (pValue != null)
							this.SuperRetailTraderPass = Convert.ToString(pValue);
						else
							this.SuperRetailTraderPass = null;
						result = true;
					 }
					break;
				case "SuperRetailTraderMan":
					{
						if (pValue != null)
							this.SuperRetailTraderMan = Convert.ToString(pValue);
						else
							this.SuperRetailTraderMan = null;
						result = true;
					 }
					break;
				case "SuperRetailTraderPhone":
					{
						if (pValue != null)
							this.SuperRetailTraderPhone = Convert.ToString(pValue);
						else
							this.SuperRetailTraderPhone = null;
						result = true;
					 }
					break;
				case "SuperRetailTraderAddress":
					{
						if (pValue != null)
							this.SuperRetailTraderAddress = Convert.ToString(pValue);
						else
							this.SuperRetailTraderAddress = null;
						result = true;
					 }
					break;
				case "SuperRetailTraderFrom":
					{
						if (pValue != null)
							this.SuperRetailTraderFrom = Convert.ToString(pValue);
						else
							this.SuperRetailTraderFrom = null;
						result = true;
					 }
					break;
				case "SuperRetailTraderFromId":
					{
						if (pValue != null)
							this.SuperRetailTraderFromId = Convert.ToString(pValue);
						else
							this.SuperRetailTraderFromId = null;
						result = true;
					 }
					break;
				case "HigheSuperRetailTraderID":
					{
						if (pValue != null)
							this.HigheSuperRetailTraderID = (Guid)pValue;
						else
							this.HigheSuperRetailTraderID = null;
						result = true;
					 }
					break;
				case "JoinTime":
					{
						if (pValue != null)
							this.JoinTime = Convert.ToDateTime(pValue);
						else
							this.JoinTime = null;
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
				case "CreateBy":
					{
						if (pValue != null)
							this.CreateBy = Convert.ToString(pValue);
						else
							this.CreateBy = null;
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
				case "CustomerId":
					{
						if (pValue != null)
							this.CustomerId = Convert.ToString(pValue);
						else
							this.CustomerId = null;
						result = true;
					 }
					break;
				case "Status":
					{
						if (pValue != null)
							this.Status = Convert.ToString(pValue);
						else
							this.Status = null;
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