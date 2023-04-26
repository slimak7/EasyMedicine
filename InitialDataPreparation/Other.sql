select top 50 * from Medicines
select top 50 * from ActiveSubstances

delete from Medicines
delete from ActiveSubstances
delete from MedicineActiveSubstances

select distinct A.MedicineName, A.Power from Medicines A, Medicines B where A.MedicineName = B.MedicineName and A.Power != B.Power

select top 50 M.MedicineName, M.Power, Y.p from Medicines M inner join (select S.SubstanceName as p, MS.MedicineID as id from MedicineActiveSubstances MS inner join ActiveSubstances S on  s.SubstanceID = MS.ActiveSubstanceSubstanceID) Y on Y.id = M.MedicineID order by Y.p

select M.MedicineName, M.Power, S.SubstanceName from Medicines M inner join MedicineActiveSubstances MS on MS.MedicineID = M.MedicineID inner join ActiveSubstances S on S.SubstanceID = MS.ActiveSubstanceSubstanceID where M.MedicineName = 'Abacavir + Lamivudine Zentiva' 

select * from Medicines where MedicineName = 'Clozapine Aristo'

select * from Medicines where MedicineName = 'Tantum Verde'

select * from MedicineActiveSubstances where MedicineID = '6D4BFC16-67F2-4672-AFF8-F5D5B7570B58'