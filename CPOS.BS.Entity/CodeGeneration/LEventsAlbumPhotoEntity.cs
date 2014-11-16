/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013-12-18 17:58
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
    public partial class LEventsAlbumPhotoEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public LEventsAlbumPhotoEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// ����ʶ
		/// </summary>
		public String PhotoId { get; set; }

		/// <summary>
		/// ����ʶ
		/// </summary>
		public String AlbumId { get; set; }

		/// <summary>
		/// ���ӵ�ַ
		/// </summary>
		public String LinkUrl { get; set; }

		/// <summary>
		/// ����
		/// </summary>
		public String Title { get; set; }

		/// <summary>
		/// ����
		/// </summary>
		public String Description { get; set; }

		/// <summary>
		/// ���
		/// </summary>
		public Int32? SortOrder { get; set; }

		/// <summary>
		/// �Ķ�����
		/// </summary>
		public Int32? ReaderCount { get; set; }

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


        #endregion

    }
}