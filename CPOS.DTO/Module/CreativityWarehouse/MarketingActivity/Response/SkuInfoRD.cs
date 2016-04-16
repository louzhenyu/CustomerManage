using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;
namespace JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingActivity.Response
{
    public class SkuInfoRD : IAPIResponseData
    {
        public List<ItemSkuInfo> ItemSkuInfoList { get; set; }
    }
    public class ItemSkuInfo
    {
        public string item_name { get; set; }
        public string item_code { get; set; }
        public string sku_id { get; set; }
        public string bat_id { get; set; }
        public string item_id { get; set; }
        public string prop_1_detail_id { get; set; }
        public string prop_1_detail_name { get; set; }
        public string prop_2_detail_id { get; set; }
        public string prop_2_detail_name { get; set; }
        public string prop_3_detail_id { get; set; }
        public string prop_3_detail_name { get; set; }
        public string prop_4_detail_id { get; set; }
        public string prop_4_detail_name { get; set; }
        public string prop_5_detail_id { get; set; }
        public string prop_5_detail_name { get; set; }
        public string prop_1_detail_code { get; set; }
        public string prop_2_detail_code { get; set; }
        public string prop_3_detail_code { get; set; }
        public string prop_4_detail_code { get; set; }
        public string prop_5_detail_code { get; set; }
        public string status { get; set; }
        public string create_user_id { get; set; }
        public string create_time { get; set; }
        public string modify_user_id { get; set; }
        public string modify_time { get; set; }
        public string prop_1_id { get; set; }
        public string prop_1_name { get; set; }
        public string prop_2_id { get; set; }
        public string prop_2_name { get; set; }
        public string prop_3_id { get; set; }
        public string prop_3_name { get; set; }
        public string prop_4_id { get; set; }
        public string prop_4_name { get; set; }
        public string prop_5_id { get; set; }
        public string prop_5_name { get; set; }
        public string barcode { get; set; }
        public string prop_1_code { get; set; }
        public string prop_2_code { get; set; }
        public string prop_3_code { get; set; }
        public string prop_4_code { get; set; }
        public string prop_5_code { get; set; }
    }
}
