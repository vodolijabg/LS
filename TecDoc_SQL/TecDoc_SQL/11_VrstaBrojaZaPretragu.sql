 with A as
 (
SELECT 1 AS VrstaBrojaZaPretragu_ID, 'Broj proizvodjaca' as Naziv
union
SELECT 2 AS VrstaBrojaZaPretragu_ID,  'Koriscen broj' as Naziv
union
SELECT 3 AS VrstaBrojaZaPretragu_ID,  'OE broj' as Naziv
union
SELECT 4 AS VrstaBrojaZaPretragu_ID,  'Uporedni broj' as Naziv
union
SELECT 5 AS VrstaBrojaZaPretragu_ID,  'EAN broj' as Naziv
)
insert into Lav.dbo.VrstaBrojaZaPretragu
select VrstaBrojaZaPretragu_ID,Naziv 
from A