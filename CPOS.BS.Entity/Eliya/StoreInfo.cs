using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace JIT.CPOS.BS.Entity.Eliya
{
    public class StoreInfo
    {
        public string storeId { get; set; }
        public string imageURL { get; set; }
        public ImageInfo[] imageList { get; set; }
        public string storeName { get; set; }
        public string tel { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public string typeCode { get; set; }
        /// <summary>
        /// APP的坐标
        /// </summary>
        [JsonIgnore]
        public string SPosition { get; set; }
        public string displayIndex { get; set; }
        public string address { get; set; }
        public string description { get; set; }
        public string brandStory { get; set; }
        public string brandRelation { get; set; }
        //public double? distance
        //{
        //    get
        //    {
        //        if (!string.IsNullOrEmpty(SPosition))
        //        {
        //            var temp2 = SPosition.Split(',');
        //            var temp1 = new string[] { this.lng, this.lat };
        //            var IsDouble = temp1.Aggregate(true, (i, j) =>
        //                {
        //                    double m;
        //                    return i && double.TryParse(j, out m);
        //                });
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

    public class ImageInfo
    {
        public string imageId { get; set; }
        public string imageUrl { get; set; }
    }
}
