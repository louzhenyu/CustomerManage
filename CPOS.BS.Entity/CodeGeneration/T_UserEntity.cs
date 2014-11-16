/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/23 14:32:45
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
    public partial class T_UserEntity : BaseEntity ,IExtensionable
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_UserEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String user_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String user_code { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String user_name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String user_gender { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String user_birthday { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String user_password { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String user_email { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String user_identity { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String user_telephone { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String user_cellphone { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String user_address { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String user_postcode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String user_remark { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String user_status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String qq { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String msn { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String blog { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String create_user_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String create_time { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String modify_user_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String modify_time { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String user_status_desc { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String fail_date { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String user_name_en { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String customer_id { get; set; }


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
				case "user_id":
					pValue = this.user_id;
					result = true;
					break;
				case "user_code":
					pValue = this.user_code;
					result = true;
					break;
				case "user_name":
					pValue = this.user_name;
					result = true;
					break;
				case "user_gender":
					pValue = this.user_gender;
					result = true;
					break;
				case "user_birthday":
					pValue = this.user_birthday;
					result = true;
					break;
				case "user_password":
					pValue = this.user_password;
					result = true;
					break;
				case "user_email":
					pValue = this.user_email;
					result = true;
					break;
				case "user_identity":
					pValue = this.user_identity;
					result = true;
					break;
				case "user_telephone":
					pValue = this.user_telephone;
					result = true;
					break;
				case "user_cellphone":
					pValue = this.user_cellphone;
					result = true;
					break;
				case "user_address":
					pValue = this.user_address;
					result = true;
					break;
				case "user_postcode":
					pValue = this.user_postcode;
					result = true;
					break;
				case "user_remark":
					pValue = this.user_remark;
					result = true;
					break;
				case "user_status":
					pValue = this.user_status;
					result = true;
					break;
				case "qq":
					pValue = this.qq;
					result = true;
					break;
				case "msn":
					pValue = this.msn;
					result = true;
					break;
				case "blog":
					pValue = this.blog;
					result = true;
					break;
				case "create_user_id":
					pValue = this.create_user_id;
					result = true;
					break;
				case "create_time":
					pValue = this.create_time;
					result = true;
					break;
				case "modify_user_id":
					pValue = this.modify_user_id;
					result = true;
					break;
				case "modify_time":
					pValue = this.modify_time;
					result = true;
					break;
				case "user_status_desc":
					pValue = this.user_status_desc;
					result = true;
					break;
				case "fail_date":
					pValue = this.fail_date;
					result = true;
					break;
				case "user_name_en":
					pValue = this.user_name_en;
					result = true;
					break;
				case "customer_id":
					pValue = this.customer_id;
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
				case "user_id":
					{
						if (pValue != null)
							this.user_id = Convert.ToString(pValue);
						else
							this.user_id = null;
						result = true;
					 }
					break;
				case "user_code":
					{
						if (pValue != null)
							this.user_code = Convert.ToString(pValue);
						else
							this.user_code = null;
						result = true;
					 }
					break;
				case "user_name":
					{
						if (pValue != null)
							this.user_name = Convert.ToString(pValue);
						else
							this.user_name = null;
						result = true;
					 }
					break;
				case "user_gender":
					{
						if (pValue != null)
							this.user_gender = Convert.ToString(pValue);
						else
							this.user_gender = null;
						result = true;
					 }
					break;
				case "user_birthday":
					{
						if (pValue != null)
							this.user_birthday = Convert.ToString(pValue);
						else
							this.user_birthday = null;
						result = true;
					 }
					break;
				case "user_password":
					{
						if (pValue != null)
							this.user_password = Convert.ToString(pValue);
						else
							this.user_password = null;
						result = true;
					 }
					break;
				case "user_email":
					{
						if (pValue != null)
							this.user_email = Convert.ToString(pValue);
						else
							this.user_email = null;
						result = true;
					 }
					break;
				case "user_identity":
					{
						if (pValue != null)
							this.user_identity = Convert.ToString(pValue);
						else
							this.user_identity = null;
						result = true;
					 }
					break;
				case "user_telephone":
					{
						if (pValue != null)
							this.user_telephone = Convert.ToString(pValue);
						else
							this.user_telephone = null;
						result = true;
					 }
					break;
				case "user_cellphone":
					{
						if (pValue != null)
							this.user_cellphone = Convert.ToString(pValue);
						else
							this.user_cellphone = null;
						result = true;
					 }
					break;
				case "user_address":
					{
						if (pValue != null)
							this.user_address = Convert.ToString(pValue);
						else
							this.user_address = null;
						result = true;
					 }
					break;
				case "user_postcode":
					{
						if (pValue != null)
							this.user_postcode = Convert.ToString(pValue);
						else
							this.user_postcode = null;
						result = true;
					 }
					break;
				case "user_remark":
					{
						if (pValue != null)
							this.user_remark = Convert.ToString(pValue);
						else
							this.user_remark = null;
						result = true;
					 }
					break;
				case "user_status":
					{
						if (pValue != null)
							this.user_status = Convert.ToString(pValue);
						else
							this.user_status = null;
						result = true;
					 }
					break;
				case "qq":
					{
						if (pValue != null)
							this.qq = Convert.ToString(pValue);
						else
							this.qq = null;
						result = true;
					 }
					break;
				case "msn":
					{
						if (pValue != null)
							this.msn = Convert.ToString(pValue);
						else
							this.msn = null;
						result = true;
					 }
					break;
				case "blog":
					{
						if (pValue != null)
							this.blog = Convert.ToString(pValue);
						else
							this.blog = null;
						result = true;
					 }
					break;
				case "create_user_id":
					{
						if (pValue != null)
							this.create_user_id = Convert.ToString(pValue);
						else
							this.create_user_id = null;
						result = true;
					 }
					break;
				case "create_time":
					{
						if (pValue != null)
							this.create_time = Convert.ToString(pValue);
						else
							this.create_time = null;
						result = true;
					 }
					break;
				case "modify_user_id":
					{
						if (pValue != null)
							this.modify_user_id = Convert.ToString(pValue);
						else
							this.modify_user_id = null;
						result = true;
					 }
					break;
				case "modify_time":
					{
						if (pValue != null)
							this.modify_time = Convert.ToString(pValue);
						else
							this.modify_time = null;
						result = true;
					 }
					break;
				case "user_status_desc":
					{
						if (pValue != null)
							this.user_status_desc = Convert.ToString(pValue);
						else
							this.user_status_desc = null;
						result = true;
					 }
					break;
				case "fail_date":
					{
						if (pValue != null)
							this.fail_date = Convert.ToString(pValue);
						else
							this.fail_date = null;
						result = true;
					 }
					break;
				case "user_name_en":
					{
						if (pValue != null)
							this.user_name_en = Convert.ToString(pValue);
						else
							this.user_name_en = null;
						result = true;
					 }
					break;
				case "customer_id":
					{
						if (pValue != null)
							this.customer_id = Convert.ToString(pValue);
						else
							this.customer_id = null;
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