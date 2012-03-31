--SELECT ACR_ART_ID AS Artikal_ID, ACR_CRI_ID AS Kriterijum_ID, ACR_VALUE AS Vrednost, ACR_KV_DES_ID AS Opis_ID
--FROM  TOF_ARTICLE_CRITERIA
--GROUP BY ACR_ART_ID, ACR_CRI_ID, ACR_VALUE, ACR_KV_DES_ID

--===============================================================================

SELECT ACR_ART_ID AS Artikal_ID, ACR_CRI_ID AS Kriterijum_ID, ACR_VALUE AS Vrednost, ACR_KV_DES_ID AS Opis_ID
into #TempVezaArtikalKriterijum
FROM  TOF_ARTICLE_CRITERIA
GROUP BY ACR_ART_ID, ACR_CRI_ID, ACR_VALUE, ACR_KV_DES_ID
--(6 090 103 row(s) affected)( 6 911 444 row(s) affected)- TD_02_2010
--(7 146 271 row(s) affected) - TD_03_2010

go

select #TempVezaArtikalKriterijum.* from #TempVezaArtikalKriterijum
inner join dbo.TOF_ARTICLES__Duplirani_Vraceni_Obrisani_ZaUpdate
on #TempVezaArtikalKriterijum.Artikal_ID = dbo.TOF_ARTICLES__Duplirani_Vraceni_Obrisani_ZaUpdate.ART_ID_Obrisani
--(9 row(s) affected)(0 row(s) affected)- TD_02_2010
go

UPDATE #TempVezaArtikalKriterijum
SET       Artikal_ID = TOF_ARTICLES__Duplirani_Vraceni_Obrisani_ZaUpdate.ART_ID_Vraceni
FROM  #TempVezaArtikalKriterijum INNER JOIN
               TOF_ARTICLES__Duplirani_Vraceni_Obrisani_ZaUpdate ON 
               #TempVezaArtikalKriterijum.Artikal_ID = TOF_ARTICLES__Duplirani_Vraceni_Obrisani_ZaUpdate.ART_ID_Obrisani
--(9 row(s) affected)(0 row(s) affected)- TD_02_2010
go

with A as
(
select Artikal_ID, Kriterijum_ID, Vrednost, Opis_ID
from #TempVezaArtikalKriterijum
group by Artikal_ID, Kriterijum_ID, Vrednost, Opis_ID
)
select COUNT(*) from A
-- sad ih ima 6 090 094- TD_02_2010

with A as
(
select t0.Artikal_ID, t0.Kriterijum_ID, t0.Vrednost, t0.Opis_ID
from #TempVezaArtikalKriterijum t0 inner join lav.dbo.Artikal t1
on t0.Artikal_ID = t1.Artikal_ID
group by t0.Artikal_ID, t0.Kriterijum_ID, t0.Vrednost, t0.Opis_ID
)
select COUNT(*) from A
--6090094 => ne postoje kriterijumi za artikle koji ne postoje- TD_02_2010
--(7 146 271 row(s) affected) - TD_03_2010 => ne postoje kriterijumi za artikle koji ne postoje

insert into lav.dbo.VezaArtikalKriterijum
select Artikal_ID, Kriterijum_ID, Vrednost, Opis_ID, 1
from #TempVezaArtikalKriterijum
group by Artikal_ID, Kriterijum_ID, Vrednost, Opis_ID
order by Artikal_ID, Kriterijum_ID, Vrednost, Opis_ID
--(6090094 row(s) affected)(6911444 row(s) affected)- TD_02_2010
--(7 146 271 row(s) affected) - TD_03_2010

drop table #TempVezaArtikalKriterijum


