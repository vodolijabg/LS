with A as
(
SELECT 
a.Proizvodjac_ID, a.Naziv, 
b.Proizvodjac_ID as bProizvodjac_ID, b.Naziv as bNaziv
FROM  Lav_Pocetna.dbo.Proizvodjac a
full outer join LAV_X_2009.dbo.Proizvodjac b 
on a.Proizvodjac_ID = b.Proizvodjac_ID and a.Naziv = b.Naziv
)
select * 
--into Proizvodjac_Temp
from A
where Proizvodjac_ID is null or bProizvodjac_ID is null


