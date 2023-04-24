select top 50 * from Medicines
select top 50 * from ActiveSubstances

delete from Medicines
delete from ActiveSubstances

select distinct A.MedicineName, A.Power from Medicines A, Medicines B where A.MedicineName = B.MedicineName and A.Power != B.Power

select top 50 M.MedicineName, M.Power, Y.p from Medicines M inner join (select S.SubstanceName as p, MS.MedicineID as id from MedicineActiveSubstances MS inner join ActiveSubstances S on  s.SubstanceID = MS.ActiveSubstanceSubstanceID) Y on Y.id = M.MedicineID order by Y.p

select M.MedicineName, M.Power, S.SubstanceName from Medicines M inner join MedicineActiveSubstances MS on MS.MedicineID = M.MedicineID inner join ActiveSubstances S on S.SubstanceID = MS.ActiveSubstanceSubstanceID where M.MedicineName = 'Abacavir + Lamivudine Zentiva' 
