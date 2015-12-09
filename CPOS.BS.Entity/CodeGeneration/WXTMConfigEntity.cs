/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/11/26 19:56:00
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
    public partial class WXTMConfigEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public WXTMConfigEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public String TemplateID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String WeiXinId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String TemplateIdShort { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AppId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String FirstText { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String RemarkText { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String FirstColour { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String RemarkColour { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AmountColour { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Colour1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Colour2 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Colour3 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Colour4 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Colour5 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Colour6 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }

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
		public String Title { get; set; }


        #endregion

    }
}