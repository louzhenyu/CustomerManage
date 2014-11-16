/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/1/11 11:45:55
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
    /// ʵ�壺  
    /// </summary>
    public partial class VipAddressEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VipAddressEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public String VipAddressID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VIPID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LinkMan { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LinkTel { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CityID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Address { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDefault { get; set; }

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
        /// ʡ
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// ��
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// ��
        /// </summary>
        public string DistrictName { get; set; }
        #endregion

    }
}