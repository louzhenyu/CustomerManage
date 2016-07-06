using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Module.SapMessageApi.Response;
using JIT.CPOS.DTO.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace JIT.CPOS.BS.BLL.SapMessage
{
    public class BaseSapMsg
    {
        #region 获取登录信息
        public string Msg { get; set; }

        private LoggingSessionInfo _loggingSessionInfo;
        /// <summary>
        /// 获取登录信息
        /// </summary>
        public LoggingSessionInfo loggingSessionInfo
        {
            get { return this._loggingSessionInfo; }
            set { this._loggingSessionInfo = value; }
        }
        #endregion

        public GetMsgObjRD MsgObjRD { get; set; }

        public XmlDocument XmlReader { get; set; }

        public BaseSapMsg()
        {
            // _loggingSessionInfo = BaseService.GetLoggingSession("7e144bf108b94505a890ec3a7820db8d");
            // _loggingSessionInfo = BaseService.GetLoggingSession2();
            _loggingSessionInfo = BaseService.GetLoggingSession();
        }

        public bool SpiltDiffOperation(GetMsgObjRD obj)
        {
            SapTransType objType = (SapTransType)Enum.Parse(typeof(SapTransType), obj.Omsg.TransType);
            this.MsgObjRD = obj;
            bool rest = false;
            switch (objType)
            {
                case SapTransType.A:
                    rest = Add();
                    break;
                case SapTransType.U:
                    rest = Update();
                    break;
                case SapTransType.D:
                    rest = Delete();
                    break;
                case SapTransType.C:
                    rest = Cancel();
                    break;
                case SapTransType.L:
                    rest = Locked();
                    break;
            }
            return rest;
        }

        public virtual bool Add()
        {
            return false;
        }
        public virtual bool Update()
        {
            return false;
        }

        public virtual bool Cancel()
        {
            return false;
        }

        public virtual bool Delete()
        {
            return false;
        }

        public virtual bool Locked()
        {
            return false;
        }


        public string ReadXml(string nodePath)
        {
            if (XmlReader == null)
            {
                XmlReader = new XmlDocument();
                XmlReader.LoadXml(MsgObjRD.Msg1.Content);
                /*XmlReader.LoadXml(@"<BOM>
  <BO>
    <AdmInfo>
      <Object>4</Object>
      <Version>2</Version>
    </AdmInfo>
    <Items>
      <row>
        <ItemCode>F01020084</ItemCode>
        <ItemName>新鲜水果标贴</ItemName>
        <U_Spec>张</U_Spec>
        <CodeBars>01170020151</CodeBars>
        <SortaMod>1</SortaMod>
        <SortaModName>常温</SortaModName>
        <InvntryUom>张</InvntryUom>
        <IsVirItem>N</IsVirItem>
        <ItemAttr>1</ItemAttr>
        <ItemAttrDesc>单品</ItemAttrDesc>
        <ItmsGrpCod>115</ItmsGrpCod>
        <RefCode>F</RefCode>
        <ItmsGrpNam>包材辅料</ItmsGrpNam>
        <ItemClassCode2>F01</ItemClassCode2>
        <ItemClassName2>包材辅料</ItemClassName2>
        <ItemClassCode3>F0102</ItemClassCode3>
        <ItemClassName3>辅料</ItemClassName3>
        <ItemClassCode4></ItemClassCode4>
        <ItemClassName4></ItemClassName4>
        <Canceled>N</Canceled>
        <Deleted>N</Deleted>
        <Actived>Y</Actived>
        <DocEntry>1822</DocEntry>
        <ObjectCode>Items</ObjectCode>
      </row>
    </Items>
    <ItemsLocation>
      <row>
        <ItemCode>F01020084</ItemCode>
        <ItemName>新鲜水果标贴</ItemName>
        <LocationCode>A0101</LocationCode>
        <LocationName>上海</LocationName>
        <WarehouseCode>10101014</WarehouseCode>
        <WarehouseName>大团-原料库14</WarehouseName>
        <OnHand>0.000000</OnHand>
        <InvntryUom>张</InvntryUom>
      </row>
    </ItemsLocation>
  </BO>
</BOM>");*/
            }

            string pathValue = string.Empty;
            if (XmlReader.SelectSingleNode(nodePath) != null)
            {
                pathValue = XmlReader.SelectSingleNode(nodePath).InnerText;
            }
            return pathValue;
        }
    }
}
