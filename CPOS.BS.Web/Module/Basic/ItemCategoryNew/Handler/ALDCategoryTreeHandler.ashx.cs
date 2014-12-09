using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JIT.CPOS.BS.Web.PageBase;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.Extension;
using JIT.CPOS.Common;
using JIT.Utility.Web.ComponentModel;
using System.Configuration;
using JIT.Utility.Log;
using JIT.Utility.Web;
using JIT.Utility.ExtensionMethod;

//using JIT.CPOS.Web.OnlineShopping.data;
/// <summary>
/// 获取商品分类树数据
/// </summary>
namespace JIT.CPOS.BS.Web.Module.Basic.ItemCategoryNew.Handler
{
    /// <summary>
    /// ALDCategoryTreeHandler 的摘要说明
    /// </summary>
    public class ALDCategoryTreeHandler : JITCPOSTreeHandler
    {
        /// <summary>
        /// 获取节点数据
        /// </summary>
        /// <param name="pParentNodeID"></param>
        /// <returns></returns>
        protected override TreeNodes GetNodes(string pParentNodeID)
        {
            string status = "";
            if (Request("Status") != null && Request("Status") != "")
                status = Request("Status");
            //获取数据          
            var url = ConfigurationManager.AppSettings["ALDApiURL"].ToString() + "/Gateway.ashx";

            var res=new ALDResponse();
            try
            {
                //拼json字符串不能有空格
         var resstr = JIT.Utility.Web.HttpClient.GetQueryString(url,
                       "Action=GetALLALDCategories&ReqContent={\"Locale\":null,\"UserID\":\"\",\"BunessZoneID\":null,\"ClientID\":null,\"Platform\":null,\"Parameters\":null,\"Token\":null,\"CallBack\":null}");
                Loggers.Debug(new DebugLogInfo() { Message = "调用ALD获取分类:" + resstr });
                res = resstr.DeserializeJSONTo<ALDResponse>();
            //    List<MallALDCategoryEntity> listAld = res.Data.DeserializeJSONTo<>();

            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception("调用ALD平台失败:" + ex.Message);
            }
            //var bll = new ItemCategoryService(this.CurrentUserInfo);
          //  var list = bll.GetItemCagegoryList(status);
            var list = res.Data;
            //组织数据
            TreeNodes nodes = new TreeNodes();
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    TreeNode node = new TreeNode();
                    node.ID = item.CategoryID;
                    //如果父类id不为空，也不是-99，赋值给树id。否则树id为空
                    if (!string.IsNullOrEmpty(item.ParentID)  && item.ParentID  != "-99")
                    {
                        node.ParentID = item.ParentID;
                    }
                    node.Text = item.CategoryName;
                    node.IsLeaf = true;
                    //
                    nodes.Add(node);
                }
            }
            //
            return nodes;
        }
    }

    public class ALDResponse
    {
        public int? ResultCode { get; set; }
        public string Message { get; set; }
        public List<MallALDCategoryEntity> Data { get; set; }
        public bool IsSuccess()
        {
            if (this.ResultCode.HasValue && this.ResultCode.Value >= 200 && this.ResultCode.Value < 300)
                return true;
            else
                return false;
        }

    }
    /// <summary>
    /// 实体： 阿拉丁商品种类 
    /// </summary>
    public partial class MallALDCategoryEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public MallALDCategoryEntity()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public string  CategoryID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CategoryName { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public string ParentID { get; set; }

        /// <summary>
        /// 图片URL
        /// </summary>
        public String ImageUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? DisPlayIndex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String LastUpdateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsDelete { get; set; }


        #endregion

    }
}