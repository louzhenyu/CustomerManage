/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/4/23 15:21:39
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
    public partial class LEventsAlbumEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public LEventsAlbumEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// ����ʶ
		/// </summary>
		public String AlbumId { get; set; }

		/// <summary>
		/// ģ��ID
		/// </summary>
		public String ModuleId { get; set; }

		/// <summary>
		/// ģ������ 1��� 
		/// </summary>
		public String ModuleType { get; set; }

		/// <summary>
		/// ģ������
		/// </summary>
		public String ModuleName { get; set; }

		/// <summary>
		/// ����  1�� ��Ƭ  2�� ��Ƶ
		/// </summary>
		public String Type { get; set; }

		/// <summary>
		/// ����ͼƬ
		/// </summary>
		public String ImageUrl { get; set; }

		/// <summary>
		/// ����
		/// </summary>
		public String Title { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Intro { get; set; }

		/// <summary>
		/// ����
		/// </summary>
		public String Description { get; set; }

		/// <summary>
		/// ���
		/// </summary>
		public Int32? SortOrder { get; set; }

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
		public String CustomerID { get; set; }


        #endregion

    }
}