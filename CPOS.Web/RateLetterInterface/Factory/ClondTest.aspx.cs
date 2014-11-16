using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.CPOS.Web.RateLetterInterface.Common;

namespace JIT.CPOS.Web.RateLetterInterface.Factory
{
    public partial class ClondTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CreateSubAccount();
            //GetSubAccounts();
            //CreateGroup();
            //JoinGroup();
            // InviteJoinGroup();
            //QueryGroupDetail();
            // LogoutGroup();
            //QueryGroup();

            //GetPublicGroups(DateTime.Now);

            //CreateSubAccount();
            // DeleteGroup();
            //DeleteGroups();


            //ModifyGroup();
        }

        /// <summary>
        /// 修改群组属性。
        /// </summary>
        private void ModifyGroup()
        {
            CloudRequestFactory rf = new CloudRequestFactory();
            var retData = rf.ModifyGroup("sandboxapp.cloopen.com", "8883", "2b0cfb57e57411e389eed89d672b9690", "ee40425d3bc8ae987aa67b729e7a6f4a", "g8014706361710", "修改群组属性", "0", "群组测试成功 吼吼吼吼吼————————");
            Response.Write(retData);
        }

        /// <summary>
        /// 批量删除群组
        /// </summary>
        private void DeleteGroups()
        {
            List<string> strList = new List<string>() 
            {

                "g8014702868003",
"g8014708143423",
"g8014704668427",
"g8014702695782",
"g8014707657048",
"g8014700940271",
"g8014704882397",
"g8014703321018",
"g8014702231988",
"g8014706335370",
"g8014703494436",
"g8014709254299",
"g8014706760522",
"g8014708787796",
"g8014707244394",
"g8014704137506",
"g8014703944289",
"g8014701263380",
"g8014708584464",
"g8014708908249",
"g8014702595569",
"g8014703280220",
"g8014709390850",
"g8014704074514",
"g8014701295508",
"g8014706728520",
"g8014708437028",
"g8014705112858",
"g8014700211827",
"g8014706668864",
"g8014704652418",
"g8014700749053",
"g8014709986406",
"g8014709234665",
"g8014706186882",
"g8014704927223",
"g8014709213470",
"g8014707470886",
"g8014701805305",
"g8014706324673",
"g8014706325414",
"g8014705798833",
"g8014704667862",
"g8014702799349",
"g8014707690763",
"g8014705190912",
"g8014708089610",
"g8014708084994",
"g8014700231620",
"g8014700026232",
"g8014706272540",
"g8014704686844",
"g8014708809105",
"g8014707818215",
"g8014701785593",
"g8014705348494",
"g8014708916728",
"g8014700899258",
"g8014706176914",
"g8014708176229",
"g8014707136691",
"g8014704076068",
"g8014708439671",
"g8014709411462",
"g8014708575026",
"g8014702891079",
"g8014704979992",
"g8014708864398",
"g8014709801519",
"g8014702913352",
"g8014705691801",
"g8014702542753",
"g8014700550351",
"g8014701963144"
//                "g8014702206026",
//"g8014704541148",
//"g8014701344183",
//"g8014707330391",
//"g8014700022558",
//"g8014705109592",
//"g8014700692962",
//"g8014707685061",
//"g8014707969400",
//"g8014708964341",
//"g8014706425605",
//"g8014709072334",
//"g8014706361710",
//"g8014703815318",
//"g8014701226178",
//"g8014700036927",
//"g8014705730842",
//"g8014702452496",
//"g8014708347597",
//"g8014703168133",
//"g8014700859948",
//"g8014702010054",
//"g8014704108188",
//"g8014703896408",
//"g8014702757158",
//"g8014706314453",
//"g8014708163632",
//"g8014704867080",
//"g8014707500076",
//"g8014706668538",
//"g8014701880703",
//"g8014702810400",
//"g8014709462258",
//"g8014705424640",
//"g8014707335527",
//"g8014708354605",
//"g8014707726307",
//"g8014708416574",
//"g8014704371350",
//"g8014709676724",
//"g8014706206507",
//"g8014706149239",
//"g8014707124538",
//"g8014700877665",
//"g8014709697253",
//"g8014708289882",
//"g8014701670548",
//"g8014705740563",
//"g8014702937905",
//"g8014709135104",
//"g8014700683645",
//"g8014701183698"
                 //"g8014704391922",
                 //"g8014707196006",
                 //"g8014701751487",
                 //"g8014701751487",
                 //"g8014706080749",
                 //"g8014707225148",
                 //"g8014701104566",
                 //"g8014700324886",
                 //"g8014709341978",
                 //"g8014704558111",
                 //"g8014702165658",
                 //"g8014705107352",
                 //"g8014700894808",
                 //"g8014709113202",
                 //"g8014702004505",
                 //"g8014708143411",
                 //"g8014708028928",
                 //  "g8014709727754",
                 //"g8014700960135",
                 //"g8014706817292",
                 //"g8014701522753",
                 //"g8014704867080",
                 //"g8014703340518",
                 //"g8014701670548",
                 //"g8014706446699",
                 //"g8014701870592",
                 //"g8014704599372",
                 //"g8014704456727",
                 //"g8014709733188",
                 //"g8014701260518",
                 //"g8014707335769",
                 //"g8014708894874",
                 //"g8014706011117",
                 //  "g8014708837783",
                 //"g8014708141563",
                 //"g8014704002278"
            };
            int index = 0;
            foreach (var item in strList)
            {
                index++;
                CloudRequestFactory rf = new CloudRequestFactory();
                var retData = rf.DeleteGroup("sandboxapp.cloopen.com", "8883", "8418ce72e58311e389eed89d672b9690", "3977d99a147a467f6136000a912edde5", item);
                //Response.Write(retData);
            }

            Response.Write(index);
        }

        private void DeleteGroup()
        {
            CloudRequestFactory rf = new CloudRequestFactory();
            var retData = rf.DeleteGroup("sandboxapp.cloopen.com", "8883", "2b0cfb57e57411e389eed89d672b9690", "ee40425d3bc8ae987aa67b729e7a6f4a", "g8014701344183");
            Response.Write(retData);
        }

        /// <summary>
        /// 查询群组属性
        /// </summary>
        private void QueryGroupDetail()
        {
            CloudRequestFactory rf = new CloudRequestFactory();
            ResponseViewModel retData = rf.QueryGroupDetail("sandboxapp.cloopen.com", "8883", "2b0cfb57e57411e389eed89d672b9690", "ee40425d3bc8ae987aa67b729e7a6f4a", "g8014707470886");
            Response.Write(retData);


        }
        /// <summary>
        /// 获取所有公共群组
        /// </summary>
        private void GetPublicGroups(DateTime dt)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            int date = (int)(dt - startTime).TotalSeconds;

            CloudRequestFactory rf = new CloudRequestFactory();
            PublicGroups retData = rf.GetPublicGroups("sandboxapp.cloopen.com", "8883", "5bf0ea6ce64311e389eed89d672b9690", "0cbd7b22b70dfe63edb3aa8f36f230d6", date.ToString());
            Response.Write(retData);
        }

        /// <summary>
        /// 查询成员所加入的组
        /// </summary>
        private void QueryGroup()
        {
            CloudRequestFactory rf = new CloudRequestFactory();
            QueryGroupViewModel retData = rf.QueryGroup("sandboxapp.cloopen.com", "8883", "2b0cfb57e57411e389eed89d672b9690", "553f106e0b02d17d7218b51ac6dc4b883");
            Response.Write(retData);
        }

        ///// <summary>
        ///// 用户主动退出群组
        ///// </summary>
        //private void LogoutGroup()
        //{
        //    CloudRequestFactory rf = new CloudRequestFactory();
        //    bool retData = rf.LogoutGroup("sandboxapp.cloopen.com", "8883", "a4041498e56e11e389eed89d672b9690", "e905191387cb8098309b03efd622d7b0", "g8014706989095");
        //    Response.Write(retData);

        //}

        ///// <summary>
        ///// 群主拉人
        ///// </summary>
        //private void InviteJoinGroup()
        //{
        //    CloudRequestFactory rf = new CloudRequestFactory();
        //    List<string> list = new List<string>();
        //    list.Add("80147000000133");
        //    //bool retData = rf.InviteJoinGroup("sandboxapp.cloopen.com", "8883", "5663f9ffe57111e389eed89d672b9690", "4dff67097723e24776da1f5ac0428ed7", "g8014706989095", list, "", "1", "群组管理员邀请用户加入群组");
        //    //Response.Write(retData);
        //}

        ///// <summary>
        ///// 用户申请加入群组
        ///// </summary>
        //private void JoinGroup()
        //{
        //    CloudRequestFactory rf = new CloudRequestFactory();
        //    bool retData = rf.JoinGroup("sandboxapp.cloopen.com", "8883", "a4041498e56e11e389eed89d672b9690", "e905191387cb8098309b03efd622d7b0", "g8014706989095", "相互学习1");
        //    Response.Write(retData);
        //}

        /// <summary>
        /// 创建群组
        /// </summary>
        private void CreateGroup()
        {
            CloudRequestFactory rf = new CloudRequestFactory();
            CreateGroupViewModel retData = rf.CreateGroup("sandboxapp.cloopen.com", "8883", "5663f9ffe57111e389eed89d672b9690", "4dff67097723e24776da1f5ac0428ed7", "企信32", "0", "0", "大家好。。。。");
            // string jsonData = getDictionaryData(retData);
            Response.Write(retData);
        }

        /// <summary>
        /// 创建子账号。
        /// </summary>
        private void CreateSubAccount()
        {
            CloudRequestFactory rf = new CloudRequestFactory();
            string retData = rf.CreateSubAccount(YunTongXunAppSetting.RestAddress, YunTongXunAppSetting.RestPort, YunTongXunAppSetting.MainAccount, YunTongXunAppSetting.MainToken, YunTongXunAppSetting.AppID, "2014100032021212");
            // string jsonData = getDictionaryData(retData);
            Response.Write(retData);
        }

        private string getDictionaryData(Dictionary<string, object> data)
        {
            string ret = null;
            foreach (var item in data)
            {
                return item.Value.ToString();
            }
            return ret;
        }
    }
}