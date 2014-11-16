using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 一级客户的帐期信息
    /// </summary>
    public class UnitAccountTermInfo
    {
        #region 大区
        private UnitInfo _branch_unit = new UnitInfo();
        /// <summary>
        /// 大区
        /// </summary>
        public UnitInfo BranchUnit
        {
            get { return _branch_unit; }
            set { _branch_unit = value; }
        }
        #endregion

        #region 一级客户
        private UnitInfo _unit = new UnitInfo();
        /// <summary>
        /// 一级客户
        /// </summary>
        public UnitInfo Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }
        #endregion

        #region 商品大类
        private ItemInfo _item = new ItemInfo();
        /// <summary>
        /// 商品大类
        /// </summary>
        public ItemInfo TypeItem
        {
            get { return _item; }
            set { _item = value; }
        }
        #endregion

        private string _pay_category;
        private string _pay_category_desc;
        /// <summary>
        /// 结算方式:1:款到发货;2:月结(指定天数);3:月结(指定日期)
        /// </summary>
        public string PayCategory
        {
            get { return _pay_category; }
            set { _pay_category = value; }
        }
        public string PayCategoryDescription
        {
            get { return _pay_category_desc; }
            set { _pay_category_desc = value; }
        }

        private int _pay_duration;
        /// <summary>
        /// 月结(指定天数)的月结天数
        /// </summary>
        public int PayDuration
        {
            get { return _pay_duration; }
            set { _pay_duration = value; }
        }

        private int _monpay_begin_date;
        private int _monpay_end_date;
        private int _monpay_duration;
        /// <summary>
        /// 月结(指定日期)的起始日期
        /// </summary>
        public int MonpayBeginDate
        {
            get { return _monpay_begin_date; }
            set { _monpay_begin_date = value; }
        }
        /// <summary>
        /// 月结(指定日期)的结束日期
        /// </summary>
        public int MonpayEndDate
        {
            get { return _monpay_end_date; }
            set { _monpay_end_date = value; }
        }
        /// <summary>
        /// 月结(指定日期)的月结天数
        /// </summary>
        public int MonpayDuration
        {
            get { return _monpay_duration; }
            set { _monpay_duration = value; }
        }

        private string _custpay_eom;
        private int _custpay_date;
        /// <summary>
        /// 是否指定付款日期(1:是;0:否)
        /// </summary>
        public string CustpayEom
        {
            get { return _custpay_eom; }
            set { _custpay_eom = value; }
        }
        /// <summary>
        /// 指定付款日期
        /// </summary>
        public int CustpayDate
        {
            get { return _custpay_date; }
            set { _custpay_date = value; }
        }

        private string _account_term_id;
        public string Id
        {
            get { return _account_term_id; }
            set { _account_term_id = value; }
        }
    }
}
