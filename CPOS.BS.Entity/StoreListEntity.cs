using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.Entity;

namespace JIT.CPOS.BS.Entity
{
    public partial class StoreListEntity : BaseEntity
    {
        #region 构造函数
        public StoreListEntity()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 酒店ID
        /// </summary>
        public string StoreID { get; set; }

        /// <summary>
        /// 酒店名称
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// 酒店图片地址
        /// </summary>
        public string imageURL { get; set; }

        /// <summary>
        /// 酒店地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 酒店电话
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 酒店经纬度
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// 酒店经纬度
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// 酒店距离
        /// </summary>
        public double? Distance { get; set; }

        /// <summary>
        /// 酒店起始价格
        /// </summary>
        public string MinPrice { get; set; }

        /// <summary>
        /// 是否满房
        /// </summary>
        public int IsFull { get; set; }

        #endregion
    }
}
