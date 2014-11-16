using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json; 
using System.IO; 
using System.Runtime.Serialization; 

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 地图
    /// </summary>
    [DataContract]
    public class MapEntity
    {
        /// <summary>
        /// 整数，唯一标识商店，必须
        /// </summary>
        [DataMember]
        public string StoreID { get; set; }
        /// <summary>
        /// 浮点数，商店GPS坐标的经度，必须，范围0-180.
        /// </summary>
        [DataMember]
        public string Lng { get; set; }
        /// <summary>
        /// 浮点数，商店GPS坐标的纬度，必须，范围0-90
        /// </summary>
        [DataMember]
        public string Lat { get; set; }
        /// <summary>
        /// 图片样式，g.png绿色点（gs.png选择后），b.png蓝色点（bs.png选择后），o.png橙色点（os.png选择后），必须
        /// </summary>
        [DataMember]
        public string Icon { get; set; }
        /// <summary>
        /// 是否已分配，可选
        /// </summary>
        [DataMember]
        public string IsAssigned { get; set; }
        /// <summary>
        /// 是否可编辑，可选
        /// </summary>
        [DataMember]
        public string IsEdit { get; set; }
        /// <summary>
        /// 图片上放的文字，可选
        /// </summary>
        [DataMember]
        public string LabelID { get; set; } 
        /// <summary>
        /// 图片边上文字，可选
        /// </summary>
        [DataMember]
        public string LabelContent{ get; set; } 
        /// <summary>
        /// 可选，默认为1（选择，清除）0（没有右键菜单），2（加入路线中、路线中移除、人店分离）
        /// </summary>
        [DataMember]
        public string Menu { get; set; } 
        /// <summary>
        /// 弹出框长度
        /// </summary>
        [DataMember]
        public string PopInfoHeight { get; set; } 
        /// <summary>
        /// 弹出框宽度
        /// </summary>
        [DataMember]
        public string PopInfoWidth { get; set; } 
        /// <summary>
        /// 字符串，鼠标悬停到商店上时显示的文本信息，（mapabc不具备）
        /// </summary>
        [DataMember]
        public string Tips{ get; set; } 
        /// <summary>
        /// 门店信息,必须
        /// </summary>
        [DataMember]
        public IList<PopInfo> PopInfo{ get; set; }
        [IgnoreDataMember]
        public IList<MapEntity> MapList { get; set; }

        /// <summary>
        /// 节点类型1=门店，2=会员，3=订单
        /// </summary>
        [DataMember]
        public string NodeTypeId { get; set; }

        [DataMember]
        public string SendUserList { get; set; }
        //[DataMember]
        //public IList<VipEntity> SendUserList2 { get; set; }
    }
}
