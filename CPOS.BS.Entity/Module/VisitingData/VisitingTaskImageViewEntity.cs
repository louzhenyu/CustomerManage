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
    /// 实体： 拜访执行明细查看 (某人某天在某个店的产品检查明细)
    /// </summary>
    public class VisitingTaskImageViewEntity : BaseEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VisitingTaskImageViewEntity()
        {
        }

        #endregion
        public VisitingTaskImageViewEntity(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("路径不能为空。");
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(string.Format("{0}不存在。", filePath));
            }

            this.FilePath = filePath;
            this.FileType = filePath.Substring(filePath.LastIndexOf('.')).ToLower();
            this.FileName = filePath.Substring(filePath.LastIndexOf('\\') + 1);

            if (!IsEnableType())
            {
                throw new Exception(string.Format("{0}未知的文件类型。仅支持{1}的文件类型。", filePath, String.Join(",", ENABLE_FILETYPE)));
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