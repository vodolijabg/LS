alter view vwNajpovoljnijiDobavljac
WITH SCHEMABINDING
as
--select 
--a1.Artikal_ID, 
--vad2.Cena, 
--vad2.KolicinaNaStanju, 
--vad2.PoslovniPartnerID, 
----vad2.PoslovniPartner,
--vad2.KorisnikProgramaID,
----vad2.KorisnikPrograma,
--ISNULL (vad2.KorisnikPrograma, vad2.PoslovniPartner) as Dobavljac
--from dbo.Artikal a1 --(NoLock)
--inner join
--(
--   SELECT 
--       ROW_NUMBER() OVER(PARTITION BY vad3.ArtikalID ORDER BY vad3.KorisnikProgramaID DESC) AS RowNumber,
--       vad3.VezaArtikalDobavljacID,
--       vad3.PoslovniPartnerID,
--       vad3.KorisnikProgramaID,
--       vad3.ArtikalID,
--       vad3.Cena,
--       vad3.DatumAzuriranja,
--       vad3.KolicinaNaStanju,
--       kp4.Naziv as KorisnikPrograma,
--       pp5.SkracenNaziv as PoslovniPartner
--   FROM dbo.VezaArtikalDobavljac vad3 --(NoLock)
--   left outer join dbo.KorisnikPrograma kp4
--   on vad3.KorisnikProgramaID = kp4.KorisnikProgramaID
--   left outer join dbo.PoslovniPartner pp5
--   on vad3.PoslovniPartnerID = pp5.PoslovniPartnerID
--) vad2
--on vad2.ArtikalID = a1.Artikal_ID
--where vad2.RowNumber <2



with A as
(
SELECT 
	ROW_NUMBER() OVER(PARTITION BY vad.ArtikalID ORDER BY vad.KorisnikProgramaID DESC, vad.Cena ASC) AS RowNumber,
	vad.ArtikalID,
	vad.Cena,
	vad.KolicinaNaStanju as Kolicina,
	vad.PoslovniPartnerID,
	vad.KorisnikProgramaID,
	kp.Naziv as KorisnikPrograma,
	pp.SkracenNaziv as PoslovniPartner
FROM dbo.VezaArtikalDobavljac vad
left outer join dbo.KorisnikPrograma kp
on vad.KorisnikProgramaID = kp.KorisnikProgramaID
left outer join dbo.PoslovniPartner pp
on vad.PoslovniPartnerID = pp.PoslovniPartnerID
)
select ArtikalID, Cena, Kolicina, PoslovniPartnerID, KorisnikProgramaID, 
ISNULL(KorisnikPrograma, PoslovniPartner) as Dobavljac 
from A
where RowNumber <2



