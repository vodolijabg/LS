--SELECT ARL_ART_ID AS Artikal_ID, ARL_SEARCH_NUMBER AS BrojZaPretragu, ARL_DISPLAY_NR AS BrojZaPrikazivanje, ARL_KIND AS VrstaBrojaZaPretragu, 
--               ARL_BRA_ID AS OrginalanProizvodjac_ID
--FROM  TOF_ART_LOOKUP
--GROUP BY ARL_ART_ID, ARL_SEARCH_NUMBER, ARL_DISPLAY_NR, ARL_KIND, ARL_BRA_ID

--===============================================================================
With a as
(
SELECT ARL_ART_ID AS Artikal_ID, ARL_SEARCH_NUMBER AS BrojZaPretragu, ARL_DISPLAY_NR AS BrojZaPrikazivanje, ARL_KIND AS VrstaBrojaZaPretragu, 
               ARL_BRA_ID AS OrginalanProizvodjac_ID
FROM  TOF_ART_LOOKUP
GROUP BY ARL_ART_ID, ARL_SEARCH_NUMBER, ARL_DISPLAY_NR, ARL_KIND, ARL_BRA_ID
)
select COUNT (*) from A
--=20 395 654; 23 000 941- TD_02_2010
--23 990 799 - TD_03_2010
go

SELECT ARL_ART_ID AS Artikal_ID, ARL_SEARCH_NUMBER AS BrojZaPretragu, ARL_DISPLAY_NR AS BrojZaPrikazivanje, ARL_KIND AS VrstaBrojaZaPretragu, 
               ARL_BRA_ID AS OrginalanProizvodjac_ID
into #TempVezaArtikalBrojZaPretragu
FROM  TOF_ART_LOOKUP
GROUP BY ARL_ART_ID, ARL_SEARCH_NUMBER, ARL_DISPLAY_NR, ARL_KIND, ARL_BRA_ID
--(20395654 row(s) affected)(23 000 941 row(s) affected)- TD_02_2010
--23 990 799 - TD_03_2010
go

select #TempVezaArtikalBrojZaPretragu.* from #TempVezaArtikalBrojZaPretragu
inner join dbo.TOF_ARTICLES__Duplirani_Vraceni_Obrisani_ZaUpdate
on #TempVezaArtikalBrojZaPretragu.Artikal_ID = dbo.TOF_ARTICLES__Duplirani_Vraceni_Obrisani_ZaUpdate.ART_ID_Obrisani
--(103 row(s) affected)(5 row(s) affected)- TD_02_2010
go

UPDATE #TempVezaArtikalBrojZaPretragu
SET       Artikal_ID = TOF_ARTICLES__Duplirani_Vraceni_Obrisani_ZaUpdate.ART_ID_Vraceni
FROM  #TempVezaArtikalBrojZaPretragu INNER JOIN
               TOF_ARTICLES__Duplirani_Vraceni_Obrisani_ZaUpdate ON 
               #TempVezaArtikalBrojZaPretragu.Artikal_ID = TOF_ARTICLES__Duplirani_Vraceni_Obrisani_ZaUpdate.ART_ID_Obrisani
--(103 row(s) affected)(5 row(s) affected)- TD_02_2010

go

with A as
(
select Artikal_ID, BrojZaPretragu, BrojZaPrikazivanje, VrstaBrojaZaPretragu, OrginalanProizvodjac_ID
from #TempVezaArtikalBrojZaPretragu
group by Artikal_ID, BrojZaPretragu, BrojZaPrikazivanje, VrstaBrojaZaPretragu, OrginalanProizvodjac_ID
)
select COUNT(*) from A
-- sad ih ima 20395627; 23 000 936- TD_02_2010

go 

With a as
(
select t0.Artikal_ID, t0.BrojZaPretragu, t0.BrojZaPrikazivanje, t0.VrstaBrojaZaPretragu, t0.OrginalanProizvodjac_ID
from #TempVezaArtikalBrojZaPretragu t0
inner join lav.dbo.Artikal on t0.Artikal_ID=Artikal.Artikal_ID
group by t0.Artikal_ID, t0.BrojZaPretragu, t0.BrojZaPrikazivanje, t0.VrstaBrojaZaPretragu, t0.OrginalanProizvodjac_ID
)
select COUNT (*) from A
--20395627;23000936 => ne postoje brojevi za artikle koji ne postoje- TD_02_2010
--23 990 799 => ne postoje brojevi za artikle koji ne postoje- TD_03_2010
go

select * from
#TempVezaArtikalBrojZaPretragu where BrojZaPretragu is null
--(2005 row(s) affected)(2147 row(s) affected) - postoje artikli kojima je broj za pretragu null- TD_02_2010
--((2209 row(s) affected row(s) affected) - postoje artikli kojima je broj za pretragu null- TD_03_2010
go
--obrisi ih 
delete from
#TempVezaArtikalBrojZaPretragu where BrojZaPretragu is null
--(2005 row(s) affected)(2147 row(s) affected)- TD_02_2010
--(2209 row(s) affected)- TD_03_2010

go

insert into lav.dbo.VezaArtikalBrojZaPretragu
select Artikal_ID, BrojZaPretragu, BrojZaPrikazivanje, VrstaBrojaZaPretragu, OrginalanProizvodjac_ID, 1
from #TempVezaArtikalBrojZaPretragu
group by Artikal_ID, BrojZaPretragu, BrojZaPrikazivanje, VrstaBrojaZaPretragu, OrginalanProizvodjac_ID
order by Artikal_ID
--(20393622 row(s) affected) => 20395627 - 2005
--(22998789 row(s) affected) => 23000936 - 2147- TD_02_2010
--(23 988 590 row(s) affected) => 23990799 - 2209- TD_03_2010

--===============================================================================

--proveri da li svaki artikal ima u pretrazi po brojevima upisan broj proizvodjaca
-- I DA LI JE ZA TAJ BROJ DISPLAY NUMBER = NULL

with A as
(
SELECT lav.dbo.Artikal.Artikal_ID AS Expr1
FROM  lav.dbo.VezaArtikalBrojZaPretragu AS W INNER JOIN
                lav.dbo.Artikal ON W.Artikal_ID =  lav.dbo.Artikal.Artikal_ID
WHERE (W.VrstaBrojaZaPretragu_ID = 1)
)
select count(*) from A
--2549947 ukupno artikala, 2549946 onih koji imaju BrojProizvodjaca- TD_02_2010
--20 932 238 ukupno artikala, 2 932 238 onih koji imaju broj proizvodjaca - TD_3_2010

--prikazi ga
select * from lav.dbo.Artikal 
where Artikal_ID not in (
SELECT Artikal_ID
FROM  lav.dbo.VezaArtikalBrojZaPretragu
WHERE (VrstaBrojaZaPretragu_ID = 1))
--
--Artikal_ID  Opis_ID     BrojProizvodjaca     Proizvodjac_ID
------------- ----------- -------------------- --------------
--462643      50096       -                    10565
--obrisem ovaj artikal 

DELETE FROM Artikal
WHERE (Artikal_ID = 462643)
-- ne moze ima veze- TD_02_2010


--zbog pretrage prepisi broj za prikazivanje iz broja proizvodjaca
--i prepisi proizvodjaca
UPDATE lav.dbo.VezaArtikalBrojZaPretragu
SET       BrojZaPrikazivanje = Artikal.BrojProizvodjaca, Proizvodjac_ID = Artikal.Proizvodjac_ID
FROM  lav.dbo.VezaArtikalBrojZaPretragu INNER JOIN
               lav.dbo.Artikal ON lav.dbo.VezaArtikalBrojZaPretragu.Artikal_ID = lav.dbo.Artikal.Artikal_ID
WHERE (lav.dbo.VezaArtikalBrojZaPretragu.VrstaBrojaZaPretragu_ID = 1)
--(2549946 row(s) affected)(2795554 row(s) affected)- TD_02_2010
--(2 932 238 row(s) affected)- TD_03_2010

--postoje neki brojevi koji su greska al tako je i u TekDoku
--kao na primer
SELECT VezaArtikalBrojZaPretragu.Artikal_ID, VezaArtikalBrojZaPretragu.BrojZaPrikazivanje, VezaArtikalBrojZaPretragu.Proizvodjac_ID, Artikal.BrojProizvodjaca, 
               VezaArtikalBrojZaPretragu.BrojZaPretragu
FROM  VezaArtikalBrojZaPretragu INNER JOIN
               Artikal ON VezaArtikalBrojZaPretragu.Artikal_ID = Artikal.Artikal_ID AND VezaArtikalBrojZaPretragu.BrojZaPrikazivanje = Artikal.BrojProizvodjaca AND 
               VezaArtikalBrojZaPretragu.Proizvodjac_ID = Artikal.Proizvodjac_ID
WHERE (VezaArtikalBrojZaPretragu.VrstaBrojaZaPretragu_ID = 1) AND (VezaArtikalBrojZaPretragu.Artikal_ID = 500302)

