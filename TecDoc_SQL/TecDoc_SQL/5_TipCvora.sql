--SELECT 1 AS TipCvora_ID, 'Putnicko vozilo' as Naziv
--union
--SELECT 2 AS TipCvora_ID,  'Teretno vozilo' as Naziv
--union
--SELECT 3 AS TipCvora_ID,  'Motor' as Naziv
--union
--SELECT 4 AS TipCvora_ID,  'Univerzalni' as Naziv
--union
--SELECT 5 AS TipCvora_ID,  'Osovina' as Naziv

=========================================================================================
With A as
(
SELECT 1 AS TipCvora_ID, 'Putnicko vozilo' as Naziv
union
SELECT 2 AS TipCvora_ID,  'Teretno vozilo' as Naziv
union
SELECT 3 AS TipCvora_ID,  'Motor' as Naziv
union
SELECT 4 AS TipCvora_ID,  'Univerzalni' as Naziv
union
SELECT 5 AS TipCvora_ID,  'Osovina' as Naziv
)
insert into lav.dbo.TipCvora
select TipCvora_ID, Naziv from A

