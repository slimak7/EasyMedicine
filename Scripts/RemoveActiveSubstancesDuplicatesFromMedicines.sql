declare medicineIDs cursor for
select distinct MedicineID, ActiveSubstanceSubstanceID from MedicineActiveSubstances

declare @medID uniqueidentifier
declare @subID uniqueidentifier

declare @number integer

open medicineIDs

fetch next from medicineIDs
into @medID, @subID

WHILE @@FETCH_STATUS = 0  
begin
set @number = (select count(*) from (select M1.MedicineID from MedicineActiveSubstances M1 where M1.MedicineID = @medID and M1.ActiveSubstanceSubstanceID = @subID)M group by M.MedicineID)
if (@number > 1)
begin
declare @connID uniqueidentifier = (select top 1 ConnectionID from MedicineActiveSubstances where MedicineID = @medID and ActiveSubstanceSubstanceID = @subID)
delete from MedicineActiveSubstances where MedicineID = @medID and ActiveSubstanceSubstanceID = @subID and ConnectionID != @connID
end
fetch next from medicineIDs
into @medID, @subID
end
close medicineIDs
deallocate medicineIDs
