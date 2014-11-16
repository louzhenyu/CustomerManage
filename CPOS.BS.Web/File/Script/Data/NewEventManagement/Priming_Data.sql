DECLARE @MaxDefinedID INT = 0
SELECT @MaxDefinedID = MAX(DefinedID) + 1 FROM Options
SELECT @MaxDefinedID

IF NOT EXISTS (SELECT 1 FROM Options WHERE OptionName = 'EventSponsor')
BEGIN
	insert into Options(DefinedID, OptionName, OptionValue, OptionText, CreateTime, IsDelete, ClientID)
	select @MaxDefinedID, 'EventSponsor', 1, '杰亦特', GETDATE(), 0, 'e703dbedadd943abacf864531decdac1'
	union all
	select @MaxDefinedID, 'EventSponsor', 2, '阿拉丁', GETDATE(), 0, 'e703dbedadd943abacf864531decdac1'
	union all
	select @MaxDefinedID, 'EventSponsor', 3, 'Willie', GETDATE(), 0, 'e703dbedadd943abacf864531decdac1'
END

SELECT @MaxDefinedID = MAX(DefinedID) + 1 FROM Options
SELECT @MaxDefinedID

IF NOT EXISTS (SELECT 1 FROM Options WHERE OptionName = 'ClientBussinessDefinedHierarchy')
BEGIN
	insert into Options(DefinedID, OptionName, OptionValue, OptionText, CreateTime, IsDelete, ClientID)
	select @MaxDefinedID, 'ClientBussinessDefinedHierarchy', 1, '个人信息', GETDATE(), 0, 'e703dbedadd943abacf864531decdac1'
	union all
	select @MaxDefinedID, 'ClientBussinessDefinedHierarchy', 2, '公司信息', GETDATE(), 0, 'e703dbedadd943abacf864531decdac1'
	union all
	select @MaxDefinedID, 'ClientBussinessDefinedHierarchy', 3, '活动信息', GETDATE(), 0, 'e703dbedadd943abacf864531decdac1'
END

SELECT @MaxDefinedID = MAX(DefinedID) + 1 FROM Options
SELECT @MaxDefinedID

IF NOT EXISTS (SELECT 1 FROM Options WHERE OptionName = 'EventStatus')
BEGIN
	insert into Options(DefinedID, OptionName, OptionValue, OptionText, CreateTime, IsDelete)
	select @MaxDefinedID, 'EventStatus', 10, '未开始', GETDATE(), 0
	union all
	select @MaxDefinedID, 'EventStatus', 20, '运行中', GETDATE(), 0
	union all
	select @MaxDefinedID, 'EventStatus', 30, '暂停', GETDATE(), 0
	union all
	select @MaxDefinedID, 'EventStatus', 40, '结束', GETDATE(), 0
END