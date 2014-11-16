using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using Microsoft.SqlServer.Types;
using Newtonsoft.Json;

namespace JIT.CPOS.DTO.Module.UnitAndItem.Unit.Response
{
    public class SearchStoreListRD : IAPIResponseData
    {
        
        public StoreListInfo[] StoreListInfo { get; set; }

    }
    public class StoreListInfo
    {
        public string StoreID { get; set; }	//商户ID
        public string ImageURL { get; set; }		//图片URL
        public string StoreName { get; set; }		//商户名称
        public string Tel { get; set; }		//电话
        public string TypeCode { get; set; }		//门店
        public imageInfo[] ImageList { get; set; }		//图片列表图片信息
        /// <summary>
        /// APP的坐标
        /// </summary>
        [JsonIgnore]
        public string SPosition { get; set; }
        public string DisplayIndex { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string BrandStory { get; set; }
        public string BrandRelation { get; set; }
        public decimal? Distance { get; set; }
        public string DistanceDesc { get; set; }

       
        //{
        //    get
        //    {
        //        if (!string.IsNullOrEmpty(SPosition))
        //        {
        //            var temp2 = SPosition.Split(',');
        //            var temp1 = new string[] { this.Lng, this.Lat };
        //            var IsDouble = temp1.Aggregate(true, (i, j) =>
        //            {
        //                double m;
        //                return i && double.TryParse(j, out m);
        //            });
        //            var IsDouble2 = temp2.Aggregate(true, (i, j) =>
        //            {
        //                double m;
        //                return i && double.TryParse(j, out m);
        //            });
        //            if (!IsDouble || !IsDouble2)
        //                return null;
        //            else
        //            {
        //                var point1 = SqlGeography.STGeomFromText(new System.Data.SqlTypes.SqlChars(string.Format("POINT ({0} {1})", temp1[0], temp1[1])), 4326);
        //                var point2 = SqlGeography.STGeomFromText(new System.Data.SqlTypes.SqlChars(string.Format("POINT ({0} {1})", temp2[0], temp2[1])), 4326);
        //                return point1.STDistance(point2).Value;
        //            }
        //        }
                
        //        else
        //            return null;
        //    }
          
        //}
    }

    public class imageInfo
    { 
       public string  ImageId{get;set;} //图片ID
       public string ImageUrl { get; set; } //图片URL
    }
}
