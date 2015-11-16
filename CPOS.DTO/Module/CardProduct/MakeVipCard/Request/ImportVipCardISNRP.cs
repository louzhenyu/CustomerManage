using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.CardProduct.MakeVipCard.Request
{
    /// <summary>
    /// 导入卡内码请求对象
    /// </summary>
    public class ImportVipCardISNRP : IAPIRequestParameter
    {
        ///// <summary>
        ///// 导入卡编号，卡内码集合
        ///// </summary>
        //public List<ImportInfo> ImportVipCardList { get; set; }
        /// <summary>
        /// Excel地址
        /// </summary>
        public string ExcelPath { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string BatchNo { get; set; }
        public void Validate()
        {

        }
    }

    //public class ImportInfo
    //{
    //    /// <summary>
    //    /// 卡编号
    //    /// </summary>
    //    public string VipCardCode { get; set; }
    //    /// <summary>
    //    /// 卡内码
    //    /// </summary>
    //    public string VipCardISN { get; set; }
    //}
}
