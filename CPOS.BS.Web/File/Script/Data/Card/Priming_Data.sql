declare @id int = 0;
select @id = MAX(isnull(VipCardGradeID, 0)) from SysVipCardGrade;
if not exists (select 1 from SysVipCardGrade where VipCardGradeName = '潜在会员')
begin
	insert into SysVipCardGrade 
		select @id + 1, '潜在会员', GETDATE(), null, null, null, 0, '92a251898d63474f96b2145fcee2860c', 0, null, null, 1, 1, null, null, null, null
		union all
		select @id + 2,'注册会员', GETDATE(), null, null, null, 0, '92a251898d63474f96b2145fcee2860c', 0, null, null, 1, 1, null, null, null, null
		union all
		select @id + 3,'普通会员', GETDATE(), null, null, null, 0, '92a251898d63474f96b2145fcee2860c', 0, null, null, 1, 1, null, null, null, null
		union all
		select @id + 4,'高级会员', GETDATE(), null, null, null, 0, '92a251898d63474f96b2145fcee2860c', 0, null, null, 1, 1, null, null, null, null
		union all
		select @id + 5,'VIP会员', GETDATE(), null, null, null, 0, '92a251898d63474f96b2145fcee2860c', 0, null, null, 1, 1, null, null, null, null
 		union all
		select @id + 6,'VVIP会员', GETDATE(), null, null, null, 0, '92a251898d63474f96b2145fcee2860c', 0, null, null, 1, 1, null, null, null, null
end