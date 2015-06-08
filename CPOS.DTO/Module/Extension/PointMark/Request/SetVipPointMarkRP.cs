using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Extension.PointMark.Request
{
    public class SetVipPointMarkRP:IAPIRequestParameter
    {
        #region 属性
        /// <summary>
        /// 点标来源（1=答题；2=兑换）
        /// </summary>
        public int Source{ get; set; }
        /// <summary>
        /// 收入/获支出个数(收入为正数，支出为负数)
        /// </summary>
        public int Count { get; set; }
     
        #endregion

        public void Validate()
        {

        }
    }
}
