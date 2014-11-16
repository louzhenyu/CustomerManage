using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Threading;
using System.Data.SqlClient;
//using JIT.CPOS.BS.BLL.NoticeEmail;
//using JIT.Utility;
//using JIT.Utility.DataAccess;
//using JIT.Utility.Log;
using JIT.CPOS.BS.BLL.NoticeEmail;
using JIT.Utility;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;


namespace CPOS.CMBAService
{
    public partial class Service1 : ServiceBase
    {
        private TimeSpan _timeSpan;
        private BackgroundWorker _worker;
        private string connStr;
        public Service1()
        {
            _worker = new BackgroundWorker();
            var time = ConfigurationManager.AppSettings["Timing"];
            connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            TimeSpan.TryParse(time, out _timeSpan);
            _worker.DoWork += (s, e) => DoWork();
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _worker.RunWorkerAsync();
        }

        protected override void OnStop()
        {
            _worker.Dispose();
        }

        private void DoWork()
        {
            while (true)
            {
                try
                {
                    ConfigurationManager.RefreshSection("appSettings");
                    var time = ConfigurationManager.AppSettings["Timing"];
                    TimeSpan.TryParse(time, out _timeSpan);
                    if (DateTime.Now.TimeOfDay.Hours == _timeSpan.Hours && DateTime.Now.TimeOfDay.Minutes == _timeSpan.Minutes)
                    {
                        var sql = @"
declare @Date Date = DATEADD({1}, {2}, GETDATE());
select 
    VIPID, 
	VIPName,
	Phone,
	Weixin,
	op1.OptionText,
	op2.OptionText,
	Col5,
	Col6
from Vip v
left join Options as op1 on convert(nvarchar(10),op1.OptionValue)=v.Col1 and op1.OptionName='VipSchool' and isnull(op1.ClientID,v.ClientID)=v.ClientID
and op1.IsDelete=v.IsDelete
left join Options as op2 on convert(nvarchar(10),op2.OptionValue)=v.Col2 and op2.OptionName='VipCourse' and isnull(op2.ClientID,v.ClientID)=v.ClientID
and op2.IsDelete=v.IsDelete
where v.Isdelete = 0 and DATEDIFF(day, v.CreateTime, @Date) = 0 and v.ClientID = '{0}'
and v.Status = 2

declare @DailyCount int = 0;
declare @MonthlyCount int = 0;
declare @WeeklyCount int = 0;

select @DailyCount = COUNT(1) from Vip where Isdelete = 0 and Status = 2 and DATEDIFF(DAY, CreateTime, @Date) = 0 and ClientID = '{0}'
select @WeeklyCount = COUNT(1) from Vip where Isdelete = 0 and Status = 2 and DATEDIFF(WEEK, CreateTime, @Date) = 0 and ClientID = '{0}'
select @MonthlyCount = COUNT(1) from Vip where Isdelete = 0 and Status = 2 and DATEDIFF(MONTH, CreateTime, @Date) = 0 and ClientID = '{0}'

select @DailyCount DailyCount, @WeeklyCount WeeklyCount, @MonthlyCount MonthlyCount;
";
                        sql = string.Format(sql, 
                            ConfigurationManager.AppSettings["CustomerID"],
                            ConfigurationManager.AppSettings["Interval"]??"DAY",
                            ConfigurationManager.AppSettings["Increment"]??"0");
                        var sqlHlper = new DefaultSQLHelper(connStr);
                        var ds = sqlHlper.ExecuteDataset(sql);
                        var result = NoticeMailSender<MailListTemplate>.SendMail(ConfigurationManager.AppSettings["MailTo"], ConfigurationManager.AppSettings["Subject"], ds);
                        Loggers.Debug(new DebugLogInfo
                        {
                            Message = string.Format("{0} {1} EMBA通知邮件发送{2}", DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay, result ? "成功" : "失败")
                        });
                        Loggers.Database(new DatabaseLogInfo
                        {
                            TSQL = new TSQL
                            {
                                CommandText = sql
                            }
                        });
                    }
                }
                catch (Exception ex)
                {
                    Loggers.Exception(new BasicUserInfo(), ex);
                }
                Thread.Sleep(new TimeSpan(0, 0, 1, 0));
                //Loggers.Debug(new DebugLogInfo
                //{
                //    Message = DateTime.Now.ToString() 
                //    +"\r\n" + DateTime.Now.TimeOfDay.Hours.ToString() + "\t" + _timeSpan.Hours 
                //    +"\r\n"  + DateTime.Now.TimeOfDay.Minutes  +"\t" + _timeSpan.Minutes
                //});
            }
        }

    }
}
