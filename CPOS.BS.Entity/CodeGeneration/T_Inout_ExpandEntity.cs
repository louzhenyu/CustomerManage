/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/26 11:20:39
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

namespace CPOS.BS.Entity
{
    /// <summary>
    /// 实体：  
    /// </summary>
    public partial class T_Inout_ExpandEntity : BaseEntity ,IExtensionable
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_Inout_ExpandEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String OrderExpandID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OrderId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String DeliveryMode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PackageRemark { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LogisticRemark { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String TransType { get; set; }

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
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LastUpdateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ProvinceCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Province { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CityCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String City { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AreaCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Area { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String DiscRemarks { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String IsCallBeDeli { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String GoodsAndInvoice { get; set; }


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
				case "OrderExpandID":
					pValue = this.OrderExpandID;
					result = true;
					break;
				case "OrderId":
					pValue = this.OrderId;
					result = true;
					break;
				case "DeliveryMode":
					pValue = this.DeliveryMode;
					result = true;
					break;
				case "PackageRemark":
					pValue = this.PackageRemark;
					result = true;
					break;
				case "LogisticRemark":
					pValue = this.LogisticRemark;
					result = true;
					break;
				case "TransType":
					pValue = this.TransType;
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
				case "LastUpdateTime":
					pValue = this.LastUpdateTime;
					result = true;
					break;
				case "LastUpdateBy":
					pValue = this.LastUpdateBy;
					result = true;
					break;
				case "IsDelete":
					pValue = this.IsDelete;
					result = true;
					break;
				case "CustomerID":
					pValue = this.CustomerID;
					result = true;
					break;
				case "ProvinceCode":
					pValue = this.ProvinceCode;
					result = true;
					break;
				case "Province":
					pValue = this.Province;
					result = true;
					break;
				case "CityCode":
					pValue = this.CityCode;
					result = true;
					break;
				case "City":
					pValue = this.City;
					result = true;
					break;
				case "AreaCode":
					pValue = this.AreaCode;
					result = true;
					break;
				case "Area":
					pValue = this.Area;
					result = true;
					break;
				case "DiscRemarks":
					pValue = this.DiscRemarks;
					result = true;
					break;
				case "IsCallBeDeli":
					pValue = this.IsCallBeDeli;
					result = true;
					break;
				case "GoodsAndInvoice":
					pValue = this.GoodsAndInvoice;
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
				case "OrderExpandID":
					{
						if (pValue != null)
							this.OrderExpandID = Convert.ToString(pValue);
						else
							this.OrderExpandID = null;
						result = true;
					 }
					break;
				case "OrderId":
					{
						if (pValue != null)
							this.OrderId = Convert.ToString(pValue);
						else
							this.OrderId = null;
						result = true;
					 }
					break;
				case "DeliveryMode":
					{
						if (pValue != null)
							this.DeliveryMode = Convert.ToString(pValue);
						else
							this.DeliveryMode = null;
						result = true;
					 }
					break;
				case "PackageRemark":
					{
						if (pValue != null)
							this.PackageRemark = Convert.ToString(pValue);
						else
							this.PackageRemark = null;
						result = true;
					 }
					break;
				case "LogisticRemark":
					{
						if (pValue != null)
							this.LogisticRemark = Convert.ToString(pValue);
						else
							this.LogisticRemark = null;
						result = true;
					 }
					break;
				case "TransType":
					{
						if (pValue != null)
							this.TransType = Convert.ToString(pValue);
						else
							this.TransType = null;
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
				case "LastUpdateTime":
					{
						if (pValue != null)
							this.LastUpdateTime = Convert.ToDateTime(pValue);
						else
							this.LastUpdateTime = null;
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
				case "IsDelete":
					{
						if (pValue != null)
							this.IsDelete =  Convert.ToInt32(pValue);
						else
							this.IsDelete = null;
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
				case "ProvinceCode":
					{
						if (pValue != null)
							this.ProvinceCode = Convert.ToString(pValue);
						else
							this.ProvinceCode = null;
						result = true;
					 }
					break;
				case "Province":
					{
						if (pValue != null)
							this.Province = Convert.ToString(pValue);
						else
							this.Province = null;
						result = true;
					 }
					break;
				case "CityCode":
					{
						if (pValue != null)
							this.CityCode = Convert.ToString(pValue);
						else
							this.CityCode = null;
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
				case "AreaCode":
					{
						if (pValue != null)
							this.AreaCode = Convert.ToString(pValue);
						else
							this.AreaCode = null;
						result = true;
					 }
					break;
				case "Area":
					{
						if (pValue != null)
							this.Area = Convert.ToString(pValue);
						else
							this.Area = null;
						result = true;
					 }
					break;
				case "DiscRemarks":
					{
						if (pValue != null)
							this.DiscRemarks = Convert.ToString(pValue);
						else
							this.DiscRemarks = null;
						result = true;
					 }
					break;
				case "IsCallBeDeli":
					{
						if (pValue != null)
							this.IsCallBeDeli = Convert.ToString(pValue);
						else
							this.IsCallBeDeli = null;
						result = true;
					 }
					break;
				case "GoodsAndInvoice":
					{
						if (pValue != null)
							this.GoodsAndInvoice = Convert.ToString(pValue);
						else
							this.GoodsAndInvoice = null;
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