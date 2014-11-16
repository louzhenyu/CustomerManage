using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 城市模板
    /// </summary>
    public class CityInfo
    {
        /// <summary>
        /// 城市标识[保存必须]
        /// </summary>
        public string City_Id { get; set; }

        /// <summary>
        /// 省[保存必须]
        /// </summary>
        public string City1_Name { get; set; }

        /// <summary>
        /// 市[保存必须]
        /// </summary>
        public string City2_Name { get; set; }
        /// <summary>
        /// 区[保存必须]
        /// </summary>
        public string City3_Name{ get; set; }
        /// <summary>
        /// 号码[保存必须]
        /// </summary>
        public string City_Code { get; set; }
        /// <summary>
        /// 城市名称
        /// </summary>
        public string City_Name { get; set; }
    }
}
