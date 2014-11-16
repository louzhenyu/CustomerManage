using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.Web.PageBase;

namespace JIT.CPOS.BS.Web.Framework.WebControl
{
    /// <summary>
    ///属性生成类
    /// </summary>
    public class PropHelper : JITPage
    {
        private static PropHelper _propHelper = null;
        private static readonly object _locker = new object();
        public static PropHelper PropHelperSingleton
        {
            get
            {
                lock (_locker)
                {
                    if (_propHelper == null)
                        _propHelper = new PropHelper();
                }
                return _propHelper;
            }
        }
        private PropHelper()
        { }
        private IList<JIT.CPOS.BS.Entity.PropInfo> GetPropGroupList(string domin)
        {
            var propService = new JIT.CPOS.BS.BLL.PropService(CurrentUserInfo);
            var grouplist = new List<JIT.CPOS.BS.Entity.PropInfo>();
            try
            {
                return propService.GetPropListFirstByDomain(domin);
            }
            catch (Exception ex)
            {
                PageLog.Current.Write(ex);
                return grouplist;
            }
        }
        //获取sku属性集合
        private IList<JIT.CPOS.BS.Entity.SkuPropInfo> GetSkuPropList()
        {
            var skuPropService = new JIT.CPOS.BS.BLL.SkuPropServer(CurrentUserInfo);
            var skuList = new List<JIT.CPOS.BS.Entity.SkuPropInfo>();
            try
            {
                return skuPropService.GetSkuPropList();
            }
            catch (Exception ex)
            {
                PageLog.Current.Write(ex);
                return skuList;
            }
        }
        //获取属性（明细）集合
        private IList<JIT.CPOS.BS.Entity.PropInfo> GetPropList(string parent_id, string domin)
        {
            var propService = new JIT.CPOS.BS.BLL.PropService(CurrentUserInfo);
            try
            {
                return propService.GetPropListByParentId(domin, parent_id);
            }
            catch (Exception ex)
            {
                PageLog.Current.Write(ex);
                return null;
            }
        }
        //获取所有价格类型(jifeng.cao 20140221)
        private IList<JIT.CPOS.BS.Entity.ItemPriceTypeInfo> GetItemPriceTypeList()
        {
            var priceTypeService = new JIT.CPOS.BS.BLL.ItemPriceTypeService(CurrentUserInfo);
            var priceTypeList = new List<JIT.CPOS.BS.Entity.ItemPriceTypeInfo>();
            try
            {
                return priceTypeService.GetItemPriceTypeList();
            }
            catch (Exception ex)
            {
                PageLog.Current.Write(ex);
                return priceTypeList;
            }
        }
        #region 生成sku表头
        public string CreationSkuProp(string type)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            var source = this.GetSkuPropList();
            if (source != null || source.Count > 0)
            {
                int i = 0;
                foreach (var item in source)
                {
                    if (i % 4 == 0)
                        sb.Append("<tr style='line-height:30px;'>");

                    sb.Append("<td width = '25%'>");
                    sb.Append("<div style='float:left; width:80px;'>" + item.prop_name + "</div>");
                    //sb.Append("</td>");
                    //sb.Append("<td width = '12%'>");
                    sb.Append(CreationSkuPropDetail(item, type));
                    sb.Append("</td>");

                    i++;

                    if (i % 4 == 0)
                        sb.Append("</tr>");
                }

                //非4的整除时，最后补</tr>
                if (i % 4 != 0)
                    sb.Append("</tr>");
            }

            //jifeng.cao
            var list = this.GetItemPriceTypeList();
            if (list != null && list.Count > 0)
            {
                int i = 0;
                foreach (var item in list)
                {
                    if (i % 4 == 0)
                        sb.Append("<tr style='line-height:30px;'>");

                    sb.Append("<td width = '25%'>");
                    sb.Append("<div style='float:left; width:80px;'> <font color=red>*</font>" + item.item_price_type_name + "</div>");
                    //sb.Append("<td width = '12%'>");
                    sb.Append("<div style='float:left;margin-top:5px;' is_price=\"1\" price_type_name=\"" + item.item_price_type_name + "\" input_flag=\"text\" class=\"itemSku text\" type=\"text\" id=\"" + item.item_price_type_id + "\" ></div>");
                    sb.Append("<script>Ext.onReady(function() { createTextbox(\"" +
                       item.item_price_type_id + "\", null, \"\"); });</script>");
                    sb.Append("</td>");

                    i++;

                    if (i % 4 == 0)
                        sb.Append("</tr>");
                }
                //非4的整除时，最后补</tr>
                if (i % 4 != 0)
                    sb.Append("</tr>");
            }

            return sb.ToString();
        }

        public string CreationSkuProp()
        {
            return CreationSkuProp("ITEM");
        }

        private string CreationSkuPropDetail(JIT.CPOS.BS.Entity.SkuPropInfo prop, string type)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            switch (prop.prop_input_flag)
            {
                case "text":
                    //sb.Append("<input prop_name=\"" + prop.prop_name + "\" columnindex=\"" +
                    //    prop.display_index + "\" input_flag=\"text\" sku_prop_id=\"" + prop.prop_id +
                    //    "\" class=\"itemSku text\" type=\"text\" id=\"" + prop.prop_id + "\" />");
                    sb.Append("<div style='float:left;margin-top:5px;' prop_name=\"" + prop.prop_name + "\" columnindex=\"" +
                        prop.display_index + "\" input_flag=\"text\" sku_prop_id=\"" + prop.prop_id +
                        "\" class=\"itemSku text\" type=\"text\" id=\"" + prop.prop_id + "\" ></div>");
                    sb.Append("<script>Ext.onReady(function() { createTextbox(\"" +
                        prop.prop_id + "\", null, \"\"); });</script>");
                    break;
                case "textarea":
                    sb.Append("<textarea style='float:left;' prop_name=\"" + prop.prop_name + "\" columnindex=\"" +
                        prop.display_index + "\" input_flag=\"text\" sku_prop_id=\"" + prop.prop_id +
                        "\" class=\"itemSku text\" type=\"text\" id=\"" + prop.prop_id + "\" style=\"width:200px;height:100px;margin-top:5px;margin-bottom:5px;\"></textarea>");
                    break;
                case "select": CreationSkuSelect(sb, prop, type); break;
                case "label": CreationSkuLabel(sb, prop); break;
                case "select-date-(yyyy-MM)": CreationSkuselectDate(sb, prop, "short"); break;
                case "select-date-(yyyy-MM-dd)": CreationSkuselectDate(sb, prop, "full"); break;
                case "radio": CreationSkuRadio(sb, prop); break;
                default: break;
            }
            return sb.ToString();
        }
        private void CreationSkuLabel(System.Text.StringBuilder sb, JIT.CPOS.BS.Entity.SkuPropInfo prop)
        {
            sb.Append("<label ");
            sb.Append("columnindex = \"" + prop.display_index + "\" input_flag=\"" + prop.prop_input_flag +
                "\" sku_prop_id=\"" + prop.prop_id + "\" class=\"itemSku\" ");
            sb.Append("prop_name=\"" + prop.prop_name + "\" id=\"" + prop.prop_id + "\">" + prop.prop_name + "</label>");
        }
        private void CreationSkuselectDate(System.Text.StringBuilder sb, JIT.CPOS.BS.Entity.SkuPropInfo prop, string type)
        {
            var onchangeFunc = "";
            if (type == "short")
            {
                onchangeFunc = "getShortDate(this);";
            }
            sb.Append("<input ");
            sb.Append("columnindex = \"" + prop.display_index + "\" input_flag=\"" + prop.prop_input_flag +
                "\" sku_prop_id=\"" + prop.prop_id + "\" class=\"itemSku\" ");
            sb.Append("id=\"" + prop.prop_id + "\" type=\"text\" readonly=\"readonly\" onclick=\"Calendar('" + prop.prop_id + "');\" ");
            sb.Append("title=\"双击清除日期\" ondblclick=\"this.value='';\" ");
            sb.Append("onchange=\"" + onchangeFunc + "\" />");
        }
        private void CreationSkuRadio(System.Text.StringBuilder sb, JIT.CPOS.BS.Entity.SkuPropInfo prop)
        {
            var items = GetPropList(prop.prop_id, "ITEM");
            if (items == null || items.Count == 0)
                return;
            foreach (var item in items)
            {
                sb.Append("<input ");
                sb.Append("columnindex = \"" + prop.display_index + "\" input_flag=\"" + prop.prop_input_flag +
                    "\" sku_prop_id=\"" + prop.prop_id + "\" class=\"itemSku\" ");
                sb.Append("type=\"radio\" prop_name=\"" + prop.prop_name + "\" name=\"" + prop.prop_id +
                    "\" class=\"_prop_detail_radio\" PropertyDetailId=\"" + prop.prop_id + "\"  id=\"" + item.Prop_Id + "\" />");
                sb.Append("<label for=\"" + item.Prop_Id + "\">" + item.Prop_Name + "</label>");
                sb.Append("&nbsp;&nbsp;");
            }
        }
        private void CreationSkuSelect(System.Text.StringBuilder sb, JIT.CPOS.BS.Entity.SkuPropInfo prop, string type)
        {
            var items = GetPropList(prop.prop_id, type);
            if (items == null || items.Count == 0)
                return;
            sb.Append("<select columnindex=\"" + prop.display_index + "\" class=\"itemSku\" input_flag=\"" +
                prop.prop_input_flag + "\" sku_prop_id=\"" + prop.prop_id + "\"  id=\"" + prop.prop_id +
                "\" prop_name=\"" + prop.prop_name + "\" style=\"margin-left:10px;\" >");
            foreach (var item in items)
            {
                sb.Append("<option id=\"" + item.Prop_Id + "\" value=\"" + item.Prop_Id + "\">" + item.Prop_Name + "</option>");
            }
            sb.Append("</select>");
        }
        #endregion

        #region 生成属性
        /// <summary>
        /// 生成属性组、属性以及属性详细
        /// </summary>
        /// <param name="domin">属性域</param>
        /// <returns>属性字符串</returns>
        public string CreationPropGroup(string domin)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            var items = GetPropGroupList(domin);
            if (items == null || items.Count == 0)
                return sb.ToString();
            int i = 0;
            foreach (var item in items)
            {
                if (i == 0)
                {
                    sb.Append("<div class=\"tit_con\"><div style='float: left;'>" + item.Prop_Name + "</div><div class='collapseHeader" + item.Prop_Id + "' style='cursor: pointer; font-weight: bold;float: right;'>点击隐藏↑</div></div>");
                    sb.Append("<table class='collapse" + item.Prop_Id + "' border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"con_tab\" style='display:block;'>");
                }
                else
                {
                    sb.Append("<div class=\"tit_con\"><div style='float: left;'>" + item.Prop_Name + "</div><div class='collapseHeader" + item.Prop_Id + "' style='cursor: pointer; font-weight: bold;float: right;'>点击展开↓</div></div>");
                    sb.Append("<table class='collapse" + item.Prop_Id + "' border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"con_tab\" style='display:none;'>");
                }

                sb.Append(CreationProp(item, domin));
                sb.Append("</table><div class='clear:both;'>&nbsp;</div>");
                i += 1;
            }
            return sb.ToString();
        }

        private string CreationProp(JIT.CPOS.BS.Entity.PropInfo prop, string domin)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            var items = GetPropList(prop.Prop_Id, domin);
            if (items == null || items.Count == 0)
            {
                return sb.ToString();
            }
            foreach (var item in items)
            {
                sb.Append("<tr><td class=\"td_co\">");
                sb.Append(item.Prop_Name + " ");
                sb.Append("</td>");
                sb.Append("<td class=\"td_lp\">");
                sb.Append(CreationPropDetail(item, domin));
                sb.Append("</td>");
                sb.Append("</tr>");
            }
            return sb.ToString();
        }
        private string CreationPropDetail(JIT.CPOS.BS.Entity.PropInfo prop, string domin)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            switch (prop.Prop_Input_Flag)
            {
                case "text": sb.Append("<input class=\"_prop_detail x-form-field x-form-text\" id=\"" + prop.Prop_Id +
                    "\" type=\"text\" prop_name=\"" + prop.Prop_Name + "\" maxlength=\"" +
                    (prop.Prop_Max_Length == 0 ? 4000 : prop.Prop_Max_Length) + "\"  style=\"width: 100%; height: 22px;\" autocomplete=\"off\" aria-invalid=\"false\" data-errorqtip=\"\" value=\"" + prop.Prop_Default_Value + "\" />");
                    break;
                case "textarea": sb.Append("<textarea class=\"_prop_detail x-form-field x-form-text\" id=\"" + prop.Prop_Id +
                    "\" type=\"text\" prop_name=\"" + prop.Prop_Name + "\" maxlength=\"" +
                    (prop.Prop_Max_Length == 0 ? 4000 : prop.Prop_Max_Length) + "\" style=\"width:400px;height:100px;margin-top:5px;margin-bottom:5px;\" value=\"" + prop.Prop_Default_Value + "\"></textarea>");
                    break;
                case "textnumber": CreationTextNumber(sb, prop); break;
                case "select": CreationSelect(sb, prop, domin); break;
                case "label": CreationLable(sb, prop, domin); break;
                case "select-date-(yyyy-MM)": CreationDate(sb, prop, domin, "short"); break;
                case "select-date-(yyyy-MM-dd)": CreationDate(sb, prop, domin, "full"); break;
                case "radio": CreationRadio(sb, prop, domin); break;
                case "htmltextarea":
                    sb.Append("<textarea class=\"_prop_detail x-form-field x-form-text\" id=\"" + prop.Prop_Id +
                    "\" type=\"text\" name=\"kindeditorcontent\" prop_name=\"" + prop.Prop_Name + "\" style=\"width:400px;height:100px;margin-top:5px;margin-bottom:5px;\">" + prop.Prop_Default_Value + "</textarea>");
                    //sb.Append("<script type=\"text/javascript\" language=\"javascript\">Ext.onReady(function () {");
                    //sb.Append(@"var kindeditor" + prop.Prop_Id.Substring(0, 5) + " = KindEditor.create('#" + prop.Prop_Id + "', {resizeType: 1,uploadJson: '/Framework/Javascript/Other/editor/EditorFileHandler.ashx?method=EditorFile&FileUrl=unit',allowFileManager: true});");
                    //sb.Append("});</script>");
                    break;
                case "fileupload":
                    sb.Append("<input id=\"" + prop.Prop_Id + "\" type=\"text\" name=\"fileupload\" prop_name=\"" + prop.Prop_Name + "\" readonly=\"readonly\" style=\"width:300px;height: 22px;\" class=\"_prop_detail x-form-field x-form-text\" autocomplete=\"off\" aria-invalid=\"false\" data-errorqtip=\"\">" +
                        "<input type=\"button\" id=\"uploadImage_" + prop.Prop_Id + "\" class=\"uploadImageUrl\" value=\"选择图片\" />");
                    break;
                case "keyvalue":
                    CreationKeyValue(sb, prop);
                    break;
                default: break;
            }
            return sb.ToString();
        }
        private void CreationRadio(System.Text.StringBuilder sb, JIT.CPOS.BS.Entity.PropInfo prop, string domin)
        {
            var items = GetPropList(prop.Prop_Id, domin);
            if (items == null || items.Count == 0)
                return;
            foreach (var item in items)
            {
                sb.Append("<input type=\"radio\" prop_name=\"" + prop.Prop_Name + "\" name=\"" + prop.Prop_Id +
                    "\" class=\"_prop_detail_radio\" PropertyDetailId=\"" + prop.Prop_Id + "\"  id=\"" + item.Prop_Id + "\" />");
                sb.Append("<label for=\"" + item.Prop_Id + "\">" + item.Prop_Name + "</label>");
                sb.Append("&nbsp;&nbsp;");
            }
        }
        private void CreationDate(System.Text.StringBuilder sb, JIT.CPOS.BS.Entity.PropInfo prop, string domin, string type)
        {
            var format = "Y-m-d";
            if (type == "short")
            {
                format = "Y-m";
            }
            //sb.Append("<div id=\"" + prop.Prop_Id + "\" type=\"text\" class=\"_prop_detail\" readonly=\"readonly\" ");
            sb.Append("<div id=\"" + prop.Prop_Id + "\" type=\"date\" class=\"_prop_detail\" readonly=\"readonly\" ");
            //sb.Append("title=\"双击清除日期\" ondblclick=\"this.value='';\" ");
            sb.Append("><div>");

            sb.Append("<script>Ext.onReady(function() { createDateSelect(\"" + prop.Prop_Id + "\", 150, \"" + format + "\",\"" + prop.Prop_Default_Value + "\"); });</script>");
        }
        private void CreationLable(System.Text.StringBuilder sb, JIT.CPOS.BS.Entity.PropInfo prop, string domin)
        {
            sb.Append("<label prop_name=\"" + prop.Prop_Name + "\" class=\"_prop_detail\" id=\"" + prop.Prop_Id + "\">" +
                prop.Prop_Name + "</label>");
        }
        private void CreationSelect(System.Text.StringBuilder sb, JIT.CPOS.BS.Entity.PropInfo prop, string domin)
        {
            var items = GetPropList(prop.Prop_Id, domin);
            if (items == null || items.Count == 0)
                return;
            sb.Append("<select class=\"_prop_detail\" id=\"" + prop.Prop_Id + "\" prop_name=\"" + prop.Prop_Name + "\" >");
            sb.Append("<option id=\"\" value=\"\">" + "" + "</option>");
            foreach (var item in items)
            {
                string selected = "";
                if (item.Prop_Code == prop.Prop_Default_Value || item.Prop_Name == prop.Prop_Default_Value)//设置值或者name均可以选中
                {
                    selected = "selected=\"selected\"";
                }

                sb.Append("<option " + selected + " id=\"" + item.Prop_Id + "\" value=\"" + item.Prop_Id + "\">" + item.Prop_Name + "</option>");
            }
            sb.Append("</select>");
        }

        private void CreationTextNumber(System.Text.StringBuilder sb, JIT.CPOS.BS.Entity.PropInfo prop)
        {

            sb.Append("<div id=\"" + prop.Prop_Id + "\" class=\"_prop_detail\" type=\"textnumber\"");
            sb.Append("><div>");

            sb.AppendFormat(@"<script>
Ext.onReady(function() {{ 
Ext.create('Jit.form.field.Number', {{
        id: '{0}',
        value: '{1}',
        margin:'0 0 0 0',
        renderTo: '{0}',
        width: '100'
    }});
        }});</script>", prop.Prop_Id, prop.Prop_Default_Value);
        }

        private void CreationKeyValue(System.Text.StringBuilder sb, JIT.CPOS.BS.Entity.PropInfo prop)
        {
            sb.Append("<div id=\"" + prop.Prop_Id + "\" class=\"_prop_detail\" width=\"390\" type=\"keyvalue\"");
            sb.Append("><div>");

            sb.Append("<script>Ext.onReady(function() { createKeyValue(\"" + prop.Prop_Id + "\",\"" + prop.Prop_Default_Value + "\"); });</script>");
        }
        #endregion
    }

    [Serializable]
    public class UnitDetailDTO
    {
        public string order_id { get; set; }
        public string create_time { get; set; }
        public string create_user_name { get; set; }
        public string sales_user { get; set; }
        public string order_no { get; set; }
        public string vip_no { get; set; }
        public string payment_name { get; set; }
        public decimal total_amount { get; set; }
    }
}