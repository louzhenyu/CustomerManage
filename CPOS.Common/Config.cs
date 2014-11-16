using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace JIT.CPOS.Common
{
    public class Config
    {
        /// <summary>
        /// Global namespace
        /// </summary>
        public const string NS = "http://www.jitmarketing.cn/cpos";

        /// <summary>
        /// TopMaxVal
        /// </summary>
        public const double TopMaxVal = 999999999999;

        /// <summary>
        /// 查询数据库数据最大行数，默认：500
        /// </summary>
        public const long QueryDBMaxCount = 500;

        /// <summary>
        /// 返回客户端错误日志字符串最大长度，默认：200
        /// </summary>
        public const int ErrorLogMaxLengthForClient = 200;

        /// <summary>
        /// 管理平台关键字
        /// </summary>
        public const string PosAP = "CposAP";

        /// <summary>
        /// 业务平台关键字
        /// </summary>
        public const string PosBS = "CposBS";

        /// <summary>
        /// 终端关键字
        /// </summary>
        public const string PosPOS = "CposPOS";

        /// <summary>
        /// 令牌存活周期关键字（单位：毫秒）
        /// </summary>
        public const string CertTokenCycleTime = "CertTokenCycleTime";

        /// <summary>
        /// Trace日志关键字
        /// </summary>
        public const string LogTraceTypeCode = "Trace";

        /// <summary>
        /// Error日志关键字
        /// </summary>
        public const string LogErrorTypeCode = "Error";

        /// <summary>
        /// 业务平台凭证类型关键字
        /// </summary>
        public const string PosBsCertTypeCode = "PosBsCertTypeCode";

        /// <summary>
        /// 是否开启连接业务平台关键字
        /// </summary>
        public const string EnableConnectPosBS = "EnableConnectPosBS";

        /// <summary>
        /// 日志文件目录
        /// </summary>
        public static string LogFolder()
        {
            string folder = ConfigurationManager.AppSettings["Dex_LogFolder"];
            if (folder == null) folder = @"C:\cpos_dex\log";
            folder = folder.Trim();
            if (!folder.EndsWith(@"\")) folder = folder + @"\";
            return folder;
        }

        /// <summary>
        /// 文件（FTP）存放路径
        /// </summary>
        public const string FileServerFolder = "FileServerFolder";

        /// <summary>
        /// 数据包文件扩展名
        /// </summary>
        public const string PackageFileExtension = "PackageFileExtension";

        /// <summary>
        /// 读取FTP文件账户类型
        /// </summary>
        public const string FtpServerReadAccountType = "FtpServerReadAccountType";

        /// <summary>
        /// 写入FTP文件账户类型
        /// </summary>
        public const string FtpServerWriteAccountType = "FtpServerWriteAccountType";

        /// <summary>
        /// UsersProfile文件夹
        /// </summary>
        public const string UsersProfileFolder = "UsersProfileFolder";

        /// <summary>
        /// Items文件夹
        /// </summary>
        public const string ItemsFolder = "ItemsFolder";

        /// <summary>
        /// Skus文件夹
        /// </summary>
        public const string SkusFolder = "SkusFolder";

        /// <summary>
        /// Units文件夹
        /// </summary>
        public const string UnitsFolder = "UnitsFolder";

        /// <summary>
        /// ItemCategorys文件夹
        /// </summary>
        public const string ItemCategorysFolder = "ItemCategorysFolder";

        /// <summary>
        /// SkuProps文件夹
        /// </summary>
        public const string SkuPropsFolder = "SkuPropsFolder";

        /// <summary>
        /// ItemProps文件夹
        /// </summary>
        public const string ItemPropsFolder = "ItemPropsFolder";

        /// <summary>
        /// SkuPrices文件夹
        /// </summary>
        public const string SkuPricesFolder = "SkuPricesFolder";

        /// <summary>
        /// ItemPrices文件夹
        /// </summary>
        public const string ItemPricesFolder = "ItemPricesFolder";

        /// <summary>
        /// ItemPropDefs文件夹
        /// </summary>
        public const string ItemPropDefsFolder = "ItemPropDefsFolder";

        /// <summary>
        /// PosVersionFolder
        /// </summary>
        public const string PosVersionFolder = "PosVersionFolder";

        /// <summary>
        /// OriginalKey
        /// </summary>
        public const string OriginalKey = "WillieYan1!";
    }
}
