using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 表单模板
    /// </summary>
    [Serializable]
    public class BillModel
    {
        /// <summary>
        /// 默认
        /// </summary>
        public const string LANGUAGE_OBJECT_KIND_CODE = "Bill.Bill";

        private string id;
        private string unitId;
        private string kindId;
        private string status;
        private string code;
        private string workMonth;
        private string workDate;
        private int uploadBatchId = 0;
        private string remark;
        private decimal money;
        private string addUserId;
        private string addDate;
        private string modifyUserId;
        private string modifyDate;

        private string unitName;
        private string kindDescription;
        private string statusDescription;
        private string addUserName;
        private string modifyUserName;

        /// <summary>
        /// Id
        /// </summary>
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// 单位
        /// </summary>
        public string UnitId
        {
            get { return unitId; }
            set { unitId = value; }
        }
        /// <summary>
        /// 表单的种类
        /// </summary>
        public string KindId
        {
            get { return kindId; }
            set { kindId = value; }
        }
        /// <summary>
        /// 表单的状态
        /// </summary>
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        /// <summary>
        /// 编码
        /// </summary>
        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        /// <summary>
        /// 工作月份
        /// </summary>
        public string WorkMonth
        {
            get { return workMonth; }
            set { workMonth = value; }
        }
        /// <summary>
        /// 工作时间
        /// </summary>
        public string WorkDate
        {
            get { return workDate; }
            set { workDate = value; }
        }
        /// <summary>
        /// 上传批次号
        /// </summary>
        public int UploadBatchId
        {
            get { return uploadBatchId; }
            set { uploadBatchId = value; }
        }
        /// <summary>
        /// 添加表单时的用户Id
        /// </summary>
        public string AddUserId
        {
            get { return addUserId; }
            set { addUserId = value; }
        }
        /// <summary>
        /// 添加表单时的时间
        /// </summary>
        public string AddDate
        {
            get { return addDate; }
            set { addDate = value; }
        }
        /// <summary>
        /// 修改表单时的用户Id
        /// </summary>
        public string ModifyUserId
        {
            get { return modifyUserId; }
            set { modifyUserId = value; }
        }

        /// <summary>
        /// 添加表单时的时间
        /// </summary>
        public string ModifyDate
        {
            get { return modifyDate; }
            set { modifyDate = value; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Money
        {
            get { return money; }
            set { money = value; }
        }

        /// <summary>
        /// 单位的名称(查询时用)
        /// </summary>
        public string UnitName
        {
            get { return unitName; }
            set { unitName = value; }
        }
        /// <summary>
        /// 表单的种类的描述(查询时用)
        /// </summary>
        public string BillKindDescription
        {
            get { return kindDescription; }
            set { kindDescription = value; }
        }
        /// <summary>
        /// 状态描述(查询时用)
        /// </summary>
        public string BillStatusDescription
        {
            get { return statusDescription; }
            set { statusDescription = value; }
        }
        /// <summary>
        /// 添加表单时的用户名(查询时用)
        /// </summary>
        public string AddUserName
        {
            get { return addUserName; }
            set { addUserName = value; }
        }
        /// <summary>
        /// 修改表单时的用户名称(查询时用)
        /// </summary>
        public string ModifyUserName
        {
            get { return modifyUserName; }
            set { modifyUserName = value; }
        }
    }
}
