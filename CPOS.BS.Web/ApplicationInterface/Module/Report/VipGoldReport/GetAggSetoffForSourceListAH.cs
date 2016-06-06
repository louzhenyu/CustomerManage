using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.VipGold.Request;
using JIT.CPOS.DTO.Module.VIP.VipGold.Response;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Report.VipGoldReport
{
    /// <summary>
    /// 集客来源报表 接口
    /// </summary>
    public class GetAggSetoffForSourceListAH : BaseActionHandler<GetAggSetoffForSourceListRP, GetAggSetoffForSourceListRD>
    {
        protected override GetAggSetoffForSourceListRD ProcessRequest(APIRequest<GetAggSetoffForSourceListRP> pRequest)
        {
            #region 设置参数 获取数据源
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo; //登录状态信息

            var rd = new GetAggSetoffForSourceListRD();

            int Days = 7;
            int type = pRequest.Parameters.type;
            string customerid = loggingSessionInfo.ClientID;
            if (type == 2) { Days = 30; } //typeId(1=近7天,2=近30天)
            DateTime currentTime = DateTime.Now.AddDays(-6);

            Agg_SetoffForSourceBLL SetoffRoleToolSourceService = new Agg_SetoffForSourceBLL(loggingSessionInfo);

            List<int> setoffrolelst = new List<int>() { 1, 2, 3 };
            #endregion

            #region 柱状图
            for (int i = 0; i < 5; i++) //获取前五天柱状图 柱状图 永远是5天。
            {
                currentTime = currentTime.AddDays(1); //当前时间的累积
                SetOffSourcesDate _model = new SetOffSourcesDate();
                _model.datetime = currentTime.ToString("yyyy-MM-dd");
                // 3个类别所以循环三次分别统计该商户某一天的某一个类别下面的集客人数和分享人数
                foreach (var item in setoffrolelst)  //     员工 = 1, 客服 = 2, 会员 = 3
                {
                    var basemodel = GetCountByCustomerIdAndDateCode(customerid, _model.datetime, item, SetoffRoleToolSourceService);
                    SetOffSourcesContent content = new SetOffSourcesContent();
                    content.PeopleCount = basemodel.PeopleCount;
                    content.ShareCount = basemodel.ShareNumber;
                    content.SetoffRoleId = item;
                    _model.RoleContent.Add(content);
                }
                rd.lst.Add(_model);
            }
            #endregion

            //根据类型 和商户编号 来获取数据 {分页就可以了}
            #region 统计上面方块基础数据
            //3 个角色 一个角色有好几天的 信息 总计  记住是前 7天而不是前 7条数据
            foreach (var item in setoffrolelst) //员工 = 1, 客服 = 2, 会员 = 3
            {
                string begintime = DateTime.Now.AddDays(-Days).ToString("yyyy-MM-dd");
                string endtime = DateTime.Now.ToString("yyyy-MM-dd");
                string prevbegintime = DateTime.Now.AddDays(-2 * Days).ToString("yyyy-MM-dd");

                var modles = SetoffRoleToolSourceService.GetSetofSourcesListByCustomerId(customerid, item, begintime, endtime);
                SetoffSourcesCount currentmodel = ConvertToEntity(modles);
                var oldmodels = SetoffRoleToolSourceService.GetSetofSourcesListByCustomerId(customerid, item, prevbegintime, begintime);
                SetoffSourcesCount oldmodel = ConvertToEntity(oldmodels);
                int? DiffCount = currentmodel.PeopleCount - oldmodel.PeopleCount;
                rd.roletoolsources.Add(new SetOffRoleSources(item, currentmodel.ShareNumber, currentmodel.PeopleCount, DiffCount));
            }
            #endregion

            #region 统计总数据
            //统计总数
            rd.ShareTotal = rd.roletoolsources.Sum(m => m.ShareCount); //分享总数
            rd.TotalSetOff = rd.roletoolsources.Sum(m => m.SetoffCount); //集客总数
            rd.AddTotalSetOff = rd.roletoolsources.Sum(m => m.DiffCount);//新增集客人数
            #endregion
            return rd;
        }
        #region 辅助方法
        /// <summary>
        /// 根据商户编号、时间、集客角色类型 获取 单条信息
        /// </summary>
        /// <param name="customerid">商户编号</param>
        /// <param name="createtime">统计时间 2016-5-5</param>
        /// <param name="tooltype">类型（ 员工 = 1, 客服 = 2, 会员 = 3）</param>
        /// <param name="SetoffRoleToolSourceService"></param>
        /// <returns></returns>
        public SetoffSourcesCount GetCountByCustomerIdAndDateCode(string customerid, string createtime, int tooltype, Agg_SetoffForSourceBLL SetoffRoleToolSourceService)
        {
            SetoffSourcesCount result = new SetoffSourcesCount();
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            lstWhereCondition.Add(new EqualsCondition() { FieldName = "DateCode", Value = createtime });
            lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = customerid });
            lstWhereCondition.Add(new EqualsCondition() { FieldName = "SetoffRole", Value = tooltype });
            List<Agg_SetoffForSourceEntity> lst = SetoffRoleToolSourceService.Query(lstWhereCondition.ToArray(), null).ToList(); //按照条件查询
            result.PeopleCount = lst.Sum(m => m.SetoffCount);
            result.ShareNumber = lst.Sum(m => m.ShareCount);
            return result;
        }
        public class SetoffSourcesCount
        {
            /// <summary>
            /// 集客人数
            /// </summary>
            public int? PeopleCount { get; set; }
            /// <summary>
            /// 分享人数
            /// </summary>
            public int? ShareNumber { get; set; }
            /// <summary>
            /// 和上一个时间节点的集客差
            /// </summary>
            public int? DiffCount { get; set; }
        }

        /// <summary>
        /// 求和 统计方块数据
        /// </summary>
        /// <param name="ds">数据源 统计总数</param>
        /// <returns></returns>
        public SetoffSourcesCount ConvertToEntity(DataSet ds)
        {
            SetoffSourcesCount result = new SetoffSourcesCount();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string ShareCount = ds.Tables[0].Rows[0]["ShareCount"] + "" == "" ? "0" : ds.Tables[0].Rows[0]["ShareCount"].ToString();
                    string SetoffCount=ds.Tables[0].Rows[0]["SetoffCount"]+"" == "" ? "0" : ds.Tables[0].Rows[0]["SetoffCount"].ToString();
                    result.ShareNumber = Convert.ToInt32(ShareCount);
                    result.PeopleCount = Convert.ToInt32(SetoffCount);
                }
            }
            return result;
        }
        #endregion
    }
}