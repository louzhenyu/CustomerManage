/*
 * Author		:Yuangxi
 * EMail		:
 * Company		:JIT
 * Create On	:2013/3/11 14:18:56
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
using System.IO;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// ʵ�壺 �ݷ�ִ����ϸ�鿴 (ĳ��ĳ����ĳ����Ĳ�Ʒ�����ϸ)
    /// </summary>
    public class VisitingTaskImageViewEntity : BaseEntity
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VisitingTaskImageViewEntity()
        {
        }

        #endregion
        public VisitingTaskImageViewEntity(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("·������Ϊ�ա�");
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(string.Format("{0}�����ڡ�", filePath));
            }

            this.FilePath = filePath;
            this.FileType = filePath.Substring(filePath.LastIndexOf('.')).ToLower();
            this.FileName = filePath.Substring(filePath.LastIndexOf('\\') + 1);

            if (!IsEnableType())
            {
                throw new Exception(string.Format("{0}δ֪���ļ����͡���֧��{1}���ļ����͡�", filePath, String.Join(",", ENABLE_FILETYPE)));
            }
        }

        public VisitingTaskImageViewEntity(string filePath, string newName, string FileZipPath)
            : this(filePath)
        {
            if (!string.IsNullOrEmpty(newName))
            {
                this.FileName = newName;
            }

            this.File_ZipPath = FileZipPath;
        }

        readonly string[] ENABLE_FILETYPE = { ".jpg", ".jepg", ".png", ".gif", ".bmp" };

        public string FilePath { get; set; }
        public string NewName { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string File_ZipPath { get; set; }

        protected bool IsEnableType()
        {
            return (Array.IndexOf<string>(ENABLE_FILETYPE, FileType) != -1);
        }
    }
}