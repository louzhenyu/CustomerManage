using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.SapMessageApi.Request
{
    public class AddMsgObjRP : IAPIRequestParameter
    {
        public Omsg Omsg { get; set; }
        public Msg1 Msg1 { get; set; }

        public void Validate()
        {
        }
    }

    /// <summary>
    /// 消息实体
    /// </summary>
    public class Omsg
    {
        /// <summary>
        /// 消费消息的时候该字段标识正在消费的消息ID
        /// </summary>
        public int SequenceID { get; set; }
        /// <summary>
        /// 生成记录的时间
        /// </summary>
        public DateTime Timestamp { get; set; }
        private string _fromSystem = "CRM";
        /// <summary>
        /// 消息来源于那个系统 如：SAP、 WMS、 CRM、 BMP、 OA 等
        /// </summary>
        public string FromSystem { get { return _fromSystem; } set { _fromSystem = value; } }
        /// <summary>
        /// 消息来源于那个公司
        /// </summary>
        public string FromCompany { get; set; }

        private string _flag = "-";
        /// <summary>
        /// 预留标示   默认“ -”
        /// </summary>
        public string Flag { get { return _flag; } set { _flag = value; } }
        /// <summary>
        /// SAP 系统内的对象编号
        /// </summary>
        public string ObjectType { get; set; }
        /// <summary>
        /// 新增(A)，修改(U)，删除(D)，取消(C)，关闭(L)
        /// </summary>
        public string TransType { get; set; }
        /// <summary>
        /// 主键字段数量
        /// </summary>
        public int FieldsInKey { get; set; }
        /// <summary>
        /// 主键字段
        /// </summary>
        public string FieldNames { get; set; }
        /// <summary>
        /// 主键字段值
        /// </summary>
        public string FieldValues { get; set; }
        /// <summary>
        /// 状态  默认为 0
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Comments { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpDateTime { get; set; }
    }

    /// <summary>
    /// 消息实体详情
    /// </summary>
    public class Msg1
    {
        /// <summary>
        /// 消费消息的时候该字段标识正在消费的消息ID
        /// </summary>
        public int SequenceID { get; set; }

        /// <summary>
        /// 消息实体内容,通常是Json 格式的数据，也可以是xml 或者其他数据，推荐使用json
        /// 目前平安对接需要存xml数据
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 消息实体长度,判断 content 的长度和iLenght 是否相等，验证数据的完整性
        /// </summary>
        public int iLength { get; set; }
    }
}
