using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.Web.RateLetterInterface.Group;
using JIT.CPOS.Web.RateLetterInterface.User;
using JIT.CPOS.BS.Entity;
using JIT.Utility.Log;
using JIT.CPOS.BS.BLL.CS;
using JIT.CPOS.BS.BLL.Product.CW;

namespace JIT.CPOS.Web.RateLetterInterface
{
    public partial class demo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var rd = new APIResponse<UserInfoListRD>();

            //string customerID = "e703dbedadd943abacf864531decdac1";
            //string userID = "418EDCBFDF9E4BEF9A1B43E51346B7CE";
            //var loggingSessionInfo = Default.GetBSLoggingSession(customerID,userID);
            //T_UserBLL bll = new T_UserBLL(loggingSessionInfo);
            //DataSet ds = bll.GetUserInfo(customerID,"", "", "", "", 1, 3,"");
            //if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    var rdData = new UserInfoListRD();
            //    rdData.UserInfo = DataTableToObject.ConvertToList<UserInfo>(ds.Tables[0]);
            //}
            //Response.Write(test());

            TestButton.Click += TestButton_Click;
        }

        void TestButton_Click(object sender, EventArgs e)
        {
            string customerID = "e703dbedadd943abacf864531decdac1";
            var loggingSessionInfo = Default.GetBSLoggingSession(customerID, "1");

            string userID = "2EF28338AC3A48CFAE973F9900C912AF";
            string deviceToken = @"a87c9a69 785b9be6 608b34b6 8af3f17f 86859cd7 d1769c1c e9a2b1de e2d8f58f";
            //IPushMessage pushIOSMessage = new PushIOSMessage(loggingSessionInfo);
            //pushIOSMessage.PushMessage(userID, msg);

            string deviceToken2 = @"a87c9a69785b9be6608b34b68af3f17f86859cd7d1769c1ce9a2b1dee2d8f58f";
            string msg = "您有一条新消息请查收";
            SendIOSMessageTest test = new SendIOSMessageTest();
            test.Send(userID, 14, deviceToken2, msg);
        }

        private TUserThirdPartyMappingEntity CreateThirdUser(TUserThirdPartyMappingEntity tutpmEntiy, ThirdUserViewModel token, string userID)
        {
            tutpmEntiy = new TUserThirdPartyMappingEntity();
            tutpmEntiy.UserId = userID;
            tutpmEntiy.SubAccountSid = token.SubAccount.subAccountSid;
            tutpmEntiy.SubToken = token.SubAccount.subToken;
            tutpmEntiy.StatusCode = token.statusCode;

            tutpmEntiy.DateCreated = token.SubAccount.dateCreated;
            tutpmEntiy.VoipAccount = token.SubAccount.voipAccount;
            tutpmEntiy.VoipPwd = token.SubAccount.voipPwd;

            return tutpmEntiy;
        }

        public string test()
        {
            try
            {
                var loggingSessionInfo = Default.GetBSLoggingSession("e703dbedadd943abacf864531decdac1", "423751DC01104839A92A655D165D3435");
                List<string> strList = new List<string>()
                {
                    "423751DC01104839A92A655D165D3435",
                    "EE5486C106E043D198402C133B542EF4",
                    "4E43D95A3EC0461C811EB4027C1CF332",
                    "0290078CF9964091950298AEE27838CC"
                };

                GroupHandler h = new GroupHandler();
                string str = h.ValidateUser("423751DC01104839A92A655D165D3435", strList, "e703dbedadd943abacf864531decdac1", loggingSessionInfo);
                return str;
            }
            catch (Exception)
            {

            }

            return "";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string customerID = "e703dbedadd943abacf864531decdac1";
            string userID = "7c292994c45143028cbf0b60c9555aec";

            string strUserID = "DD9ADDE3553F4D87AB8C619BF4AA0690,1FD5AD09E69A40B8A4375A12735EC283,AFF07CF7C5284854A39188AB4205AE1E,A85F922FE74C4FB99B69EB3C8FA00A2E";
            strUserID = @"0184281DFCAA41B5A5677FBC44BC12A1,
02AA1B9C39E941F498B2D406D4EB32F7,
039C8E9739824317A0FBC672E99F9E2A,
04839D77E7A543AFBE7F74BDCE2ADD33,
0DBC9929C91043939B92F979C251DCA1,
0EA09800A5D64A4AB40B42B0C4EFB29E,
11A41E03B17E485687C1C210E5D33B3A,
180AB32388684E019DC6FA600408A121,
1A2B719047F34154832EF158A3D7F06E,
1A555A9FE379406BBCA11F811DAB0FF0,
1DC2216D9B9D4B499AB6713C5A66AD5D,
242070129F97437F862D2C601FBCD0C1,
24F8C88D6DF142EE864FE0660DCD64BF";
            //10
            strUserID = @"29B190D43C9748E1BC06449EE51897D0,
2C2282DB10EB4447AB137C2576C2EF20,
2EF28338AC3A48CFAE973F9900C912AF,
35072C3E9BDC483FA9A81CF872438878,
357B20FE14E64FE99D6D544A42DA43AE,
3AA6D6F373644120BC2C068D7224E34C,
3B1AA8BA629544FF80A1F7621EE114D2,
3EA3112E383F4FA3BF87B6E7756FC4E5,
45EBAAC968B045958F155E90981ACF79,
47E5B4C196044C119787E7D7E3FA5917";
            //10
            strUserID = @"4C2BCFC00D014ABB9BC5D8B5E9AB88C7,
4E7218E3238D41D8AFEC1A668605F3E9,
51C982115B424607BF05571F165E3F49,
55650665A3A243B0AABD1F334D7B26FF,
5780E53AD70D4B60BD2603321B3E9410,
62AACF0614594CEDAC639E90B62AC96B,
721C3FF043A84E6A9787553FAB022AF9,
724995905F844B6B82A511366F9707FE,
76EC9C0C7D0947C8A017E95C1F05B98C,
7A013BB3FCDE4B0CB0138EF8D9679AE3";

            //20
            strUserID = @"7DCBA541DEFA4029BA78D46DCD582851,
7E6541C688B2498B8CF3C5782EE8FCEE,
84C01AA65E5847F3A3E276B1A8EA2D21,
88A606E463364A5180ED7B672C3CF9D2,
896486B41D004D07A908CD0FF61B9FC2,
8B449CB46C9C468DAD4F8066AE839077,
8E898BFDC6D643ED92D716881C43E1B8,
97149F4837D24F6CA2CB4FFEE96519B2,
9A6F53293DD44A14B10743AFED93C9FD,
9BE6DECFE39947AD97403A095EC044D8,
9FB82E8189D14C49A3106F197B5699FD,
A01A651BD0064BB98A237BD6EBE61394,
A0D828CB14A9416F9C858ACD00FDA2AE,
A47BBE39B65845DD8BAB608DFCACA763,
A5236A553C5D45439C208A16BF071F93,
A70809FD0CEB4A00AADDC23C26F77606,
A891E4A460344419B9D0D8E4372D39EE,
ADB60ECA82884A738DA373F1DAC037B8,
AF6BA62E618A4613AB479A71B460DBCE,
B6AF1C32356942E89BD9BC4A8035BDE5";
            //17
            strUserID = @"B6EAD054631B4FD0856378E759E066D6,
B7C15CCD21BF48DB9464622AB801C3A7,
BCE7451C86A0499C8C985F10E2A0EC1A,
BD493AFCE1AE4221868110109D8DB1C6,
C33D2F0EB4744ACCBE4ED90C0388BDA1,
C824F2FB61744CAEB6C4779018887901,
D38BB2A5831A4605B334322BA38B0153,
D882A720F21D4264B1321D1BEF02F133,
DC15B0B0AB1A4CF8BB7EB92BAB62CEDB,
DEB9EF7124F245888486F77B4C75EB26,
EEEE5C893D684D049A4832B5304EF511,
F2008EC7AEE44A9EB07DFC558F39B007,
F59E5C834BC04D1691F0E74E04CC32F5,
F6A8C513BE8A47278D0DAAB7F456441F,
FAA27FDC1FD64058A12A0E350CE0BC7E,
FBDA86854E5A4D1DB7346D92BB36918A,
FED8394E94D54D8CAC66AB15DE69F403";

            strUserID = "DD9ADDE3553F4D87AB8C619BF4AA0691";
            strUserID = strUserID.Replace("\r\n", "").Replace(" ", "");
            string[] arrUserID = strUserID.Split(',');
            var loggingSessionInfo = Default.GetBSLoggingSession(customerID, userID);
            T_UserBLL bll = new T_UserBLL(loggingSessionInfo);
            //验证是否在第三方注册
            TUserThirdPartyMappingBLL tutpmBll = new TUserThirdPartyMappingBLL(loggingSessionInfo);
            int index = 0;
            System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();
            foreach (string itemUserID in arrUserID)
            {
                Loggers.DEFAULT.Debug(new DebugLogInfo() { Message = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "userID:" + itemUserID });
                try
                {
                    TUserThirdPartyMappingEntity tutpmEntiy = tutpmBll.GetByID(itemUserID);
                    if (tutpmEntiy == null)
                    {
                        ThirdUserViewModel userViewModel = null;
                        CloudRequestFactory factory = new CloudRequestFactory();
                        //调用云通讯创建子账户
                        string retData = factory.CreateSubAccount("sandboxapp.cloopen.com", "8883", "ff8080813bbcae3f013bcc39c18a0022", "8f32e2023d804e1390a3b0b8b36d6e28", "aaf98f893e7df943013e8728b2b400c7", itemUserID);
                        //string jsonData = getDictionaryData(retData);
                        strBuilder.AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "create userID:" + itemUserID + "=====" + retData);
                        userViewModel = CWHelper.Deserialize<ThirdUserViewModel>(retData);
                        if (userViewModel.statusCode == MessageStatusCode.Success)
                        {
                            tutpmEntiy = CreateThirdUser(tutpmEntiy, userViewModel, itemUserID);
                            tutpmBll.Create(tutpmEntiy);
                        }
                    }
                    else
                    {
                        Loggers.DEFAULT.Debug(new DebugLogInfo() { Message = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "云通讯已注册userID:" + itemUserID });
                    }
                    index++;
                }
                catch (Exception ex)
                {
                    Loggers.DEFAULT.Exception(new ExceptionLogInfo(ex));
                }
            }
            //记录云通讯创建子账号
            Loggers.DEFAULT.Debug(new DebugLogInfo() { Message = strBuilder.ToString() });
            Response.Write(index);
            //Response.Write("<script>alert('完成批量注册！');</script>");
        }


        #region 导入数据
        public void ExportData()
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new APIException("导入失败：" + ex.Message);
            }
        }
        #endregion
    }
}