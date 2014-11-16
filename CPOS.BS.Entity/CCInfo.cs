using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 盘点单类
    /// </summary>
    public class CCInfo
    {
        /// <summary>
        /// 盘点单标识【必须输入，长度不大于50】 guid
        /// </summary>
       public string order_id { get; set; }
        /// <summary>
        /// 盘点单单号 【必须输入，长度不大于50】
        /// </summary>
       public string order_no { get; set; }
        /// <summary>
       /// 盘点单日期 【必须输入，长度不大于10，yyyy-MM-dd】
        /// </summary>
       public string order_date { get; set; }
        /// <summary>
       /// 盘点单类型1 【必须输入，长度不大于50】
        /// </summary>
       public string order_type_id { get; set; }
        /// <summary>
       /// 盘点单类型2 【长度不大于50】CC='8F330D04320F4F05AC78912577BCE0FD',AJ='58EE2F8732144C8F95E7809FFAF45827'
        /// </summary>
       public string order_reason_id { get; set; }
        /// <summary>
       /// 原单据标识【长度不大于50】
        /// </summary>
       public string ref_order_id { get; set; }
        /// <summary>
       /// 原单据号码 【长度不大于50】
        /// </summary>
       public string ref_order_no { get; set; }
        /// <summary>
       /// 要求完成日期【长度不大于50 yyyy-MM-dd】
        /// </summary>
       public string request_date { get; set; }
        /// <summary>
       /// 完成日期 【长度不大于50 yyyy-MM-dd】
        /// </summary>
       public string complete_date { get; set; }
        /// <summary>
        /// 盘点单位【必须输入 长度不大于50】
        /// </summary>
       public string unit_id { get; set; }
        /// <summary>
       /// 终端标识 【长度不大于50】
        /// </summary>
       public string pos_id { get; set; }
        /// <summary>
       /// 盘点仓库【必须输入 长度不大于50】
        /// </summary>
       public string warehouse_id { get; set; }
        /// <summary>
       /// 描述 【 长度不大于500】
        /// </summary>
       public string remark { get; set; }
        /// <summary>
       /// 数据来源 【必须输入 长度不大于50】
        /// </summary>
       public string data_from_id { get; set; }
        /// <summary>
       /// 状态 【必须输入 长度不大于50】
        /// </summary>
       public string status { get; set; }
        /// <summary>
       /// 状态描述 【显示 长度不大于50】
        /// </summary>
       public string status_desc { get; set; }
        /// <summary>
       /// 创建时间 【必须输入 长度不大于50】
        /// </summary>
       public string create_time { get; set; }
        /// <summary>
       /// 创建人标识 【必须输入 长度不大于50】
        /// </summary>
       public string create_user_id { get; set; }
       /// <summary>
       /// 创建人
       /// </summary>
       public string create_user_name { get; set; }
        /// <summary>
       /// 修改时间 显示
        /// </summary>
       public string modify_time { get; set; }
        /// <summary>
       /// 修改人标识 显示
        /// </summary>
       public string modify_user_id { get; set; }
       /// <summary>
       /// 修改人 显示
       /// </summary>
       public string modify_user_name { get; set; }
        /// <summary>
        /// 发送时间 
        /// </summary>
       public string send_time { get; set; }
        /// <summary>
        /// 发送人标识
        /// </summary>
       public string send_user_id { get; set; }
        /// <summary>
        /// 审批时间
        /// </summary>
       public string approve_time { get; set; }
        /// <summary>
        /// 审批人标识
        /// </summary>
       public string approve_user_id { get; set; }
        /// <summary>
        /// 验收时间
        /// </summary>
       public string accpect_time { get; set; }
        /// <summary>
        /// 验收标识
        /// </summary>
       public string accpect_user_id { get; set; }
        /// <summary>
        /// 仓库名称【显示】
        /// </summary>
       public string warehouse_name { get; set; }
        /// <summary>
       /// 数据来源名称【显示】
        /// </summary>
       public string data_from_name { get; set; }
        /// <summary>
       /// 盘点单位 【显示】
        /// </summary>
       public string unit_name { get; set; }
        /// <summary>
       /// 行号 【显示】
        /// </summary>
       public int Row_No { get; set; }
        /// <summary>
       /// 总数量【显示】
        /// </summary>
       public int ICount { get; set; }
        /// <summary>
       /// 明细总数量 【显示】
        /// </summary>
       public int CCDetail_ICount { get; set; }
        /// <summary>
        /// 盘点单集合【显示】
        /// </summary>
       public IList<CCInfo> CCInfoList { get; set; }
        /// <summary>
        /// 盘点单明细集合
        /// </summary>
       public IList<CCDetailInfo> CCDetailInfoList { get; set; }
       /// <summary>
       /// 表单类型 
       /// </summary>
       public string BillKindCode { get; set; }
        /// <summary>
        /// 上传下载标志
        /// </summary>
       public string if_flag { get; set; }
        /// <summary>
        /// 盘点总数【显示】
        /// </summary>
       public decimal total_qty { get; set; }

       /// <summary>
       /// 操作 新建=“Create”，修改=“Modify”
       /// </summary>
       public string operate { get; set; }

       /// <summary>
       /// 客户标识
       /// </summary>
       public string customer_id { get; set; }
    }
}
