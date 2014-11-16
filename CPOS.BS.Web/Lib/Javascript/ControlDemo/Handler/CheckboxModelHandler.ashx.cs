using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;

namespace JIT.ManagementPlatform.Web.Lib.Javascript.ControlDemo.Handler
{
    /// <summary>
    /// CheckboxModelHandler 的摘要说明
    /// </summary>
    public class CheckboxModelHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext pContext)
        {
            pContext.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            string res = "";
            switch (pContext.Request["method"])
            {
                case "GetStore":
                    res = GetStore(pContext);
                    break; 
            }
            pContext.Response.Write(res);
            pContext.Response.End();
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="pContext">HttpContext</param>
        /// <returns>string</returns>
        private string GetStore(HttpContext pContext)
        {
            int pageSize = Convert.ToInt32( pContext.Request.QueryString["limit"]);
            int pageIndex = Convert.ToInt32( pContext.Request.QueryString["page"]);
            int TestID = Convert.ToInt32( pContext.Request.QueryString["TestID"]);
            //int rowCount = 0;
            TestEntity[] entities;
            if (pageIndex == 1)
            {
                entities = new TestEntity[10];
                for (int i = 0; i < 10; i++)
                {
                    entities[i] = new TestEntity() { ID = i, IsSelected = 0, Name = "姓名" + i.ToString() };
                }
                entities[3].IsSelected = 1;
                entities[5].IsSelected = 1;
                entities[6].IsSelected = 1;
            }
            else
            {
                entities = new TestEntity[8];
                for (int i = 10; i < 18; i++)
                {
                    entities[i-10] = new TestEntity() { ID = i, IsSelected = 0, Name = "姓名" + i.ToString() };
                }
                entities[2].IsSelected = 1;
            }
            return string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",

            entities.ToJSON(),
                18);
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
    public class TestEntity
    {
        public int ID;
        public string  Name;
        public int IsSelected;
        //                    { "ID": 11, "Name": "门店1", "IsSelected": 1 }, { "ID": 21, "Name": "经销商1", "IsSelected": 0 }
        //                    , { "ID": 12, "Name": "门店2", "IsSelected": 1 }, { "ID": 22, "Name": "经销商2", "IsSelected": 0 }
        //                    , { "ID": 13, "Name": "门店3", "IsSelected": 1 }, { "ID": 23, "Name": "经销商3", "IsSelected": 0 }
        //                    , { "ID": 14, "Name": "门店4", "IsSelected": 1 }, { "ID": 24, "Name": "经销商4", "IsSelected": 0 }
        //                    , { "ID": 15, "Name": "门店5", "IsSelected": 1 }, { "ID": 25, "Name": "经销商5", "IsSelected": 0 }
        //                    , { "ID": 16, "Name": "门店6", "IsSelected": 1 }, { "ID": 26, "Name": "经销商6", "IsSelected": 0 }
        //                    , { "ID": 17, "Name": "门店7", "IsSelected": 1 }, { "ID": 27, "Name": "经销商7", "IsSelected": 0 }
        //                    , { "ID": 18, "Name": "门店8", "IsSelected": 1 }, { "ID": 28, "Name": "经销商8", "IsSelected": 0 }
        //                    , { "ID": 19, "Name": "门店9", "IsSelected": 1 }, { "ID": 29, "Name": "经销商9", "IsSelected": 0 }
        //                    ]
    }

}