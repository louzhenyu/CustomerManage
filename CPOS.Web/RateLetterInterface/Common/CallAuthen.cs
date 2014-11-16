namespace JIT.CPOS.Web.RateLetterInterface.Common
{
    public class CallAuthen
    {
        //呼叫类型 0:普通呼叫 1:回拨 2 :VoIP网络通话
        public string Type;

        //订单id
        public string OrderId;

        //子账号id  type=1时提供 
        public string SubId;

        //主叫号码
        public string Caller;

        //被叫号码
        public string Called;

        //外呼显号标示 0:不显号 1:一方显号 2:双方均显号（回拨功能）
        public string SubType;

        //（只回拨存在）与回拨功能调用返回callSid对应，一路呼叫唯一标示
        public string CallSid;
    }
}
