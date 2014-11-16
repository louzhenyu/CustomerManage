using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JIT.Utility.Web;
using JIT.Utility.Web.ComponentModel;
using JIT.ManagementPlatform.Web.Base;

namespace JIT.ManagementPlatform.Web.Lib.Javascript.ControlDemo.Handler
{
    /// <summary>
    /// TreeHandler 的摘要说明
    /// </summary>
    public class TreeHandler : JITMPTreeHandler
    {
        /// <summary>
        /// 获取子节点数据
        /// </summary>
        /// <param name="pParentNodeID">父节点ID</param>
        /// <returns></returns>
        protected override TreeNodes GetNodes(string pParentNodeID)
        {
            TreeNodes nodes = new TreeNodes();
            nodes.Add("1", "上海市", null, false);
            nodes.Add("2", "普陀区", "1", true);
            nodes.Add("3", "长宁区", "1", true);
            nodes.Add("4", "静安区", "1", true);
            nodes.Add("5", "浦东新区", "1", true);
            nodes.Add("6", "浙江省", null, false);
            nodes.Add("7", "杭州市", "6", true);
            nodes.Add("8", "1浙江省", null, true);
            nodes.Add("9", "2杭州市", "8", true);
            nodes.Add("10", "3杭州市", "9", true);
            nodes.Add("11", "1杭州市", null, true);
            return nodes;
        }
    }
}