DECLARE @EVENTID VARCHAR(50) = 'C17DA7B41FF54383BD28B047A6773EF2'
DECLARE @ClientId VARCHAR(50) = 'e703dbedadd943abacf864531decdac1'

--exec ProcSetEventPrize @ClientId, @EventId, '52�����1573 ���ȴ�ʦ�����', '52�����1573 ���ȴ�ʦ�����', '', '', '', '', 1, 22, 'E96D6FF21EB047E4A9896F8E85010E46', 1

select * from leventround where EventId=@EVENTID

--exec ProcSetEventRoundPrize @ClientId --�ͻ���ʶ
--,@EVENTID	--���ʶ
--,'E96D6FF21EB047E4A9896F8E85010E46'	--��Ʒ��ʶ
--,'11B14A9E71FA461492062AD365590CAC'	--�ִα�ʶ
--,'22';	--���н�����