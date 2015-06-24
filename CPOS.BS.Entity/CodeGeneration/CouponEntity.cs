/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013-12-14 15:57
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
    /// 实体：  
    /// </summary>
    public partial class CouponEntity : BaseEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public CouponEntity()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public String CouponID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CouponCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CouponDesc { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public DateTime?  BeginDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndDate { get; set; }


        public string BeginDate2 { get { return BeginDate == null ? "" : ((DateTime)BeginDate).ToString("yyyy-MM-dd"); } }


        public string EndDate2 { get { return EndDate == null ? "" : ((DateTime)EndDate).ToString("yyyy-MM-dd"); } }

        /// <summary>
        /// 
        /// </summary>
        public String CouponUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ImageUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? Status { get; set; }

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
        public Int32? IsDelete { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public object CouponTypeID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public object DoorID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public String CouponName { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string CoupontypeName { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public int IsRepeatable { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public int IsMixable { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public decimal ParValue { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string isexpired { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public decimal Discount { set; get; }

        /// <summary>
        ///生成二维码url
        /// </summary>
        public string QRUrl { set; get; }

        public string iseffective { set; get; }

        public string VipName { set; get; }

        public string WeiXin { set; get; }

        public int diffDay { set; get; }

        #region 新增50个动态字段 2014-10-20

        /// <summary>
        /// 组单号字段
        /// </summary>
        public String Col1 { get; set; }

        /// <summary>
        /// 核销时间
        /// </summary>
        public String Col2 { get; set; }

        /// <summary>
        /// 核销人
        /// </summary>
        public String Col3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col5 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col6 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col7 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col8 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col9 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col10 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col11 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col12 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col13 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col14 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col15 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col16 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col17 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col18 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col19 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col20 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col21 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col22 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col23 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col24 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col25 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col26 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col27 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col28 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col29 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col30 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col31 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col32 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col33 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col34 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col35 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col36 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col37 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col38 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col39 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col40 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col41 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col42 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col43 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col44 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col45 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col46 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col47 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col48 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col49 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Col50 { get; set; }

        #endregion

        #endregion

    }
}