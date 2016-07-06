/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/24 14:51:06
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
    public partial class T_Order_Reason_TypeEntity : BaseEntity ,IExtensionable
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_Order_Reason_TypeEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String reason_type_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String reason_type_code { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String reason_type_name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String remark { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? level { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String parent_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String create_time { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String create_user_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String modify_time { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String modify_user_id { get; set; }


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
				case "reason_type_id":
					pValue = this.reason_type_id;
					result = true;
					break;
				case "reason_type_code":
					pValue = this.reason_type_code;
					result = true;
					break;
				case "reason_type_name":
					pValue = this.reason_type_name;
					result = true;
					break;
				case "remark":
					pValue = this.remark;
					result = true;
					break;
				case "status":
					pValue = this.status;
					result = true;
					break;
				case "level":
					pValue = this.level;
					result = true;
					break;
				case "parent_id":
					pValue = this.parent_id;
					result = true;
					break;
				case "create_time":
					pValue = this.create_time;
					result = true;
					break;
				case "create_user_id":
					pValue = this.create_user_id;
					result = true;
					break;
				case "modify_time":
					pValue = this.modify_time;
					result = true;
					break;
				case "modify_user_id":
					pValue = this.modify_user_id;
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
				case "reason_type_id":
					{
						if (pValue != null)
							this.reason_type_id = Convert.ToString(pValue);
						else
							this.reason_type_id = null;
						result = true;
					 }
					break;
				case "reason_type_code":
					{
						if (pValue != null)
							this.reason_type_code = Convert.ToString(pValue);
						else
							this.reason_type_code = null;
						result = true;
					 }
					break;
				case "reason_type_name":
					{
						if (pValue != null)
							this.reason_type_name = Convert.ToString(pValue);
						else
							this.reason_type_name = null;
						result = true;
					 }
					break;
				case "remark":
					{
						if (pValue != null)
							this.remark = Convert.ToString(pValue);
						else
							this.remark = null;
						result = true;
					 }
					break;
				case "status":
					{
						if (pValue != null)
							this.status = Convert.ToString(pValue);
						else
							this.status = null;
						result = true;
					 }
					break;
				case "level":
					{
						if (pValue != null)
							this.level =  Convert.ToInt32(pValue);
						else
							this.level = null;
						result = true;
					 }
					break;
				case "parent_id":
					{
						if (pValue != null)
							this.parent_id = Convert.ToString(pValue);
						else
							this.parent_id = null;
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
				case "create_user_id":
					{
						if (pValue != null)
							this.create_user_id = Convert.ToString(pValue);
						else
							this.create_user_id = null;
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
				case "modify_user_id":
					{
						if (pValue != null)
							this.modify_user_id = Convert.ToString(pValue);
						else
							this.modify_user_id = null;
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