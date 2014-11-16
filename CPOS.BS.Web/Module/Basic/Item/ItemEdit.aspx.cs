/*
 * Author		:roy.tian
 * EMail		:jun.tian@jitmarketing.cn
 * Company		:JIT
 * Create On	:19/2/2012 10:03:10 AM
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using JIT.Utility.Log;
using System.Configuration;
using System.Web.UI.WebControls;
using JIT.CPOS.BS.Web.Session;
using JIT.Utility;
using JIT.CPOS.BS.BLL;

namespace JIT.CPOS.BS.Web.Module.Basic.Item
{
    public partial class ItemEdit : JIT.CPOS.BS.Web.PageBase.JITChildPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadSkuProp();
        }

        #region Sku属性加载列表
        protected IList<JIT.CPOS.BS.Entity.SkuPropInfo> SkuProInfos
        {
            get;
            set;
        }
        //价格类型集合(jifeng.cao 20140221)
        protected IList<JIT.CPOS.BS.Entity.ItemPriceTypeInfo> ItemPriceTypeInfos
        {
            get;
            set;
        }

        protected bool SKUExist { get; set; }

        private void LoadSkuProp()
        {
            CustomerBasicSettingBLL customerBasicSettingBLL = new CustomerBasicSettingBLL(CurrentUserInfo);
            SKUExist = customerBasicSettingBLL.CheckSKUExist();

            var skuPropService = new JIT.CPOS.BS.BLL.SkuPropServer(CurrentUserInfo);
            var source = skuPropService.GetSkuPropList();
            SkuProInfos = source;

            //价格类型集合(jifeng.cao 20140221)
            var itemPriceTypeService = new JIT.CPOS.BS.BLL.ItemPriceTypeService(CurrentUserInfo);
            var list = itemPriceTypeService.GetItemPriceTypeList();
            ItemPriceTypeInfos = list;
        }
        #endregion
    }
}